using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.IO;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class HRMTermBL : Base
    {
        public HRMTermBL()
        {

        }

        #region HRMTrTermHd
        public double RowsCountTerm(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNmbr")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query =
                            (
                               from _term in this.db.HRMTrTermHds
                               where (SqlMethods.Like(_term.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_term.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _term.Status != HRMTermDataMapper.GetStatus(TransStatus.Deleted)
                               select _term.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrTermHd> GetListTerm(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrTermHd> _result = new List<HRMTrTermHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNmbr")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _term in this.db.HRMTrTermHds
                                where (SqlMethods.Like(_term.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_term.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _term.Status != HRMTermDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _term.EditDate descending
                                select new
                                {
                                    TransNmbr = _term.TransNmbr,
                                    FileNmbr = _term.FileNmbr,
                                    Status = _term.Status,
                                    TransDate = _term.TransDate,
                                    TermType = _term.TermType,
                                    TermTypeName = (
                                                        from _msTermType in this.db.HRMMsTermTypes
                                                        where _term.TermType == _msTermType.TermType
                                                        select _msTermType.TermDescription
                                                    ).FirstOrDefault(),
                                    EffectiveDate = _term.EffectiveDate,
                                    Remark = _term.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrTermHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.TermType, _row.TermTypeName, _row.EffectiveDate, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrTermHd GetSingleTerm(string _prmCode)
        {
            HRMTrTermHd _result = null;

            try
            {
                _result = this.db.HRMTrTermHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleTermApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrTermHd _hRMTrTermHd = this.db.HRMTrTermHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrTermHd != null)
                    {
                        if (_hRMTrTermHd.Status != HRMTermDataMapper.GetStatus(TransStatus.Posted))
                        {
                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiTerm(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrTermHd _Term = this.db.HRMTrTermHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_Term.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrTermDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrTermDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrTermHds.DeleteOnSubmit(_Term);

                        _result = true;
                    }
                    else
                    {
                        _result = false;
                        break;
                    }
                }

                if (_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiApproveTerm(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrTermHd _hRMTrTermHd = this.db.HRMTrTermHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrTermHd.Status == HRMTermDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrTermHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrTermHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrTermHd != null)
                    {
                        if ((_hRMTrTermHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrTermDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrTermDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrTermHds.DeleteOnSubmit(_hRMTrTermHd);

                            _result = true;
                        }
                        else if (_hRMTrTermHd.FileNmbr != "" && _hRMTrTermHd.Status == HRMTermDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrTermHd.Status = HRMTermDataMapper.GetStatus(TransStatus.Deleted);
                            _result = true;
                        }
                    }
                }

                if (_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditTerm(HRMTrTermHd _prmTerm)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddTerm(HRMTrTermHd _prmTerm)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmTerm.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrTermHds.InsertOnSubmit(_prmTerm);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmTerm.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrTermHd> GetListDDLTerm()
        {
            List<HRMTrTermHd> _result = new List<HRMTrTermHd>();

            try
            {
                var _query = (
                                from _termHd in this.db.HRMTrTermHds
                                where _termHd.Status == HRMTermDataMapper.GetStatus(TransStatus.Posted)
                                orderby _termHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _termHd.TransNmbr,
                                    FileNmbr = _termHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrTermHd(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spHRM_TermGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMTerm);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();

                    _result = "Get Approval Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.spHRM_TermApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrTermHd _term = this.GetSingleTerm(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_term.TransDate.Year, _term.TransDate.Month, AppModule.GetValue(TransactionType.HRMTerm), this._companyTag, ""))
                        {
                            _term.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMTerm);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _term.FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        
                        this.db.SubmitChanges();

                        _scope.Complete();

                        _result = "Approve Success";
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Posting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spHRM_TermPosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMTerm);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleTerm(_prmCode).FileNmbr;
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();

                    _result = "Posting Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string UnPosting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spHRM_TermUnPost(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "UnPosting Success";
                }
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        #endregion

        #region HRMTrTermDt
        public double RowsCountTermDt(string _prmCode)
        {
            double _result = 0;

            var _query = (
                            from _termDt in this.db.HRMTrTermDts
                            where _termDt.TransNmbr == _prmCode
                            select _termDt.EmpNumb
                         ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrTermDt> GetListTermDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrTermDt> _result = new List<HRMTrTermDt>();

            try
            {
                var _query = (
                                from _termDt in this.db.HRMTrTermDts
                                where _termDt.TransNmbr == _prmCode
                                orderby _termDt.EmpNumb ascending
                                select new
                                {
                                    TransNmbr = _termDt.TransNmbr,
                                    EmpNumb = _termDt.EmpNumb,
                                    EmpName = (
                                                from _msEmployee in this.db.MsEmployees
                                                where _msEmployee.EmpNumb == _termDt.EmpNumb
                                                select _msEmployee.EmpName
                                              ).FirstOrDefault(),
                                    OldJobTitle = _termDt.OldJobTitle,
                                    OldJobLevel = _termDt.OldJobLevel,
                                    OldEmpType = _termDt.OldEmpType,
                                    OldEndDate = _termDt.OldEndDate,
                                    OldWorkPlace = _termDt.OldWorkPlace,
                                    OldMethod = _termDt.OldMethod,
                                    NewJobTitle = _termDt.NewJobTitle,
                                    NewJobLevel = _termDt.NewJobLevel,
                                    NewEmpType = _termDt.NewEmpType,
                                    NewEndDate = _termDt.NewEndDate,
                                    NewWorkPlace = _termDt.NewWorkPlace,
                                    NewMethod = _termDt.NewMethod,
                                    Remark = _termDt.Remark,
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrTermDt(_row.TransNmbr, _row.EmpNumb, _row.EmpName, _row.OldJobTitle, _row.OldJobLevel, _row.OldEmpType, _row.OldEndDate, _row.OldWorkPlace, _row.OldMethod, _row.NewJobTitle, _row.NewJobLevel, _row.NewEmpType, _row.NewEndDate, _row.NewWorkPlace, _row.NewMethod, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrTermDt GetSingleTermDt(string _prmEmpNumb, string _prmTransNmbr)
        {
            HRMTrTermDt _result = null;

            try
            {
                _result = this.db.HRMTrTermDts.Single(_temp => _temp.EmpNumb == _prmEmpNumb && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiTermDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrTermDt _TermDt = this.db.HRMTrTermDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.EmpNumb == _tempSplit[1]);

                    this.db.HRMTrTermDts.DeleteOnSubmit(_TermDt);
                }

                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddTermDt(HRMTrTermDt _prmTermDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrTermDts.InsertOnSubmit(_prmTermDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditTermDt(HRMTrTermDt _prmTermDt)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~HRMTermBL()
        {

        }
    }
}
