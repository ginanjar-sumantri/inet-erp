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
    public sealed class TrAppraisalBL : Base
    {
        public TrAppraisalBL()
        {

        }

        #region HRMTrAppraisalHd
        public double RowsCountAppraisal(string _prmCategory, string _prmKeyword)
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
                               from _appraisal in this.db.HRMTrAppraisalHds
                               where (SqlMethods.Like(_appraisal.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_appraisal.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _appraisal.Status != AppraisalDataMapper.GetStatus(TransStatus.Deleted)
                               select _appraisal.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrAppraisalHd> GetListAppraisal(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrAppraisalHd> _result = new List<HRMTrAppraisalHd>();

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
                                from _appraisal in this.db.HRMTrAppraisalHds
                                where (SqlMethods.Like(_appraisal.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_appraisal.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _appraisal.Status != AppraisalDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _appraisal.EditDate descending
                                select new
                                {
                                    TransNmbr = _appraisal.TransNmbr,
                                    FileNmbr = _appraisal.FileNmbr,
                                    Status = _appraisal.Status,
                                    TransDate = _appraisal.TransDate,
                                    PurposeCode = _appraisal.PurposeCode,
                                    PurposeName = (
                                                        from _msPurpose in this.db.HRMMsPurposes
                                                        where _msPurpose.PurposeCode == _appraisal.PurposeCode
                                                        select _msPurpose.PurposeName
                                                    ).FirstOrDefault(),
                                    EmpNumb = _appraisal.EmpNumb,
                                    EmpName = (
                                                    from _msEmp in this.db.MsEmployees
                                                    where _msEmp.EmpNumb == _appraisal.EmpNumb
                                                    select _msEmp.EmpName
                                                ).FirstOrDefault(),
                                    EmpApprBy = _appraisal.EmpApprBy,
                                    EmpApprByName = (
                                                        from _msEmp in this.db.MsEmployees
                                                        where _msEmp.EmpNumb == _appraisal.EmpApprBy
                                                        select _msEmp.EmpName
                                                    ).FirstOrDefault(),
                                    Recomendation = _appraisal.Recomendation,
                                    Average = _appraisal.Average
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrAppraisalHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.PurposeCode, _row.PurposeName, _row.EmpNumb, _row.EmpName, _row.EmpApprBy, _row.EmpApprByName, _row.Recomendation, _row.Average));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrAppraisalHd GetSingleAppraisal(string _prmCode)
        {
            HRMTrAppraisalHd _result = null;

            try
            {
                _result = this.db.HRMTrAppraisalHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleAppraisalApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrAppraisalHd _hRMTrAppraisalHd = this.db.HRMTrAppraisalHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrAppraisalHd != null)
                    {
                        if (_hRMTrAppraisalHd.Status != AppraisalDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiAppraisal(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrAppraisalHd _appraisal = this.db.HRMTrAppraisalHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_appraisal.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrAppraisalDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrAppraisalDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrAppraisalHds.DeleteOnSubmit(_appraisal);

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

        public bool DeleteMultiApproveAppraisal(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrAppraisalHd _hRMTrAppraisalHd = this.db.HRMTrAppraisalHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrAppraisalHd.Status == AppraisalDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrAppraisalHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrAppraisalHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrAppraisalHd != null)
                    {
                        if ((_hRMTrAppraisalHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrAppraisalDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrAppraisalDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrAppraisalHds.DeleteOnSubmit(_hRMTrAppraisalHd);

                            _result = true;
                        }
                        else if (_hRMTrAppraisalHd.FileNmbr != "" && _hRMTrAppraisalHd.Status == AppraisalDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrAppraisalHd.Status = AppraisalDataMapper.GetStatus(TransStatus.Deleted);
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

        public bool EditAppraisal(HRMTrAppraisalHd _prmAppraisal)
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

        public string AddAppraisal(HRMTrAppraisalHd _prmAppraisal)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmAppraisal.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrAppraisalHds.InsertOnSubmit(_prmAppraisal);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmAppraisal.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrAppraisalHd> GetListDDLAppraisal()
        {
            List<HRMTrAppraisalHd> _result = new List<HRMTrAppraisalHd>();

            try
            {
                var _query = (
                                from _appraisalHd in this.db.HRMTrAppraisalHds
                                where _appraisalHd.Status == AppraisalDataMapper.GetStatus(TransStatus.Posted)
                                orderby _appraisalHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _appraisalHd.TransNmbr,
                                    FileNmbr = _appraisalHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrAppraisalHd(_row.TransNmbr, _row.FileNmbr));
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
                this.db.spHRM_AppraisalGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.Appraisal);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
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
                    this.db.spHRM_AppraisalApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrAppraisalHd _appraisal = this.GetSingleAppraisal(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_appraisal.TransDate).Year, Convert.ToDateTime(_appraisal.TransDate).Month, AppModule.GetValue(TransactionType.Appraisal), this._companyTag, ""))
                        {
                            _appraisal.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.Appraisal);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _appraisal.FileNmbr;
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
                this.db.spHRM_AppraisalPosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.Appraisal);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleAppraisal(_prmCode).FileNmbr;
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
                this.db.spHRM_AppraisalUnPost(_prmCode, _prmuser, ref _result);

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

        #region HRMTrAppraisalDt

        public double RowsCountAppraisalDt(string _prmCode)
        {
            double _result = 0;

            var _query =
                         (
                            from _appraisalDt in this.db.HRMTrAppraisalDts
                            where _appraisalDt.TransNmbr == _prmCode
                            select _appraisalDt.AppraisalCode
                         ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrAppraisalDt> GetListAppraisalDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrAppraisalDt> _result = new List<HRMTrAppraisalDt>();

            try
            {
                var _query = (
                                from _appraisalDt in this.db.HRMTrAppraisalDts
                                where _appraisalDt.TransNmbr == _prmCode
                                orderby _appraisalDt.AppraisalCode ascending
                                select new
                                {
                                    TransNmbr = _appraisalDt.TransNmbr,
                                    AppraisalCode = _appraisalDt.AppraisalCode,
                                    AppraisalName = (
                                                       from _Appraisal in this.db.HRMMsAppraisals
                                                       where _Appraisal.AppraisalCode == _appraisalDt.AppraisalCode
                                                       select _Appraisal.AppraisalName
                                                   ).FirstOrDefault(),
                                    Bobot = _appraisalDt.Bobot,
                                    Result = _appraisalDt.Result,
                                    Remark = _appraisalDt.Remark

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrAppraisalDt(_row.TransNmbr, _row.AppraisalCode, _row.AppraisalName, _row.Bobot, _row.Result, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrAppraisalDt> GetListAppraisalDt(string _prmCode)
        {
            List<HRMTrAppraisalDt> _result = new List<HRMTrAppraisalDt>();

            try
            {
                var _query = (
                                from _appraisalDt in this.db.HRMTrAppraisalDts
                                where _appraisalDt.TransNmbr == _prmCode
                                orderby _appraisalDt.AppraisalCode ascending
                                select new
                                {
                                    TransNmbr = _appraisalDt.TransNmbr,
                                    AppraisalCode = _appraisalDt.AppraisalCode,
                                    AppraisalName = (
                                                       from _Appraisal in this.db.HRMMsAppraisals
                                                       where _Appraisal.AppraisalCode == _appraisalDt.AppraisalCode
                                                       select _Appraisal.AppraisalName
                                                   ).FirstOrDefault(),
                                    Bobot = _appraisalDt.Bobot,
                                    Result = _appraisalDt.Result,
                                    Remark = _appraisalDt.Remark

                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrAppraisalDt(_row.TransNmbr, _row.AppraisalCode, _row.AppraisalName, _row.Bobot, _row.Result, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrAppraisalDt GetSingleAppraisalDt(string _prmAppraisalCode, string _prmTransNmbr)
        {
            HRMTrAppraisalDt _result = null;

            try
            {
                _result = this.db.HRMTrAppraisalDts.Single(_temp => _temp.AppraisalCode == _prmAppraisalCode && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiAppraisalDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrAppraisalDt _appraisalDt = this.db.HRMTrAppraisalDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.AppraisalCode == _tempSplit[1]);

                    decimal _total = 0;
                    decimal _bobot = 0;
                    decimal _average = 0;

                    List<HRMTrAppraisalDt> _detail = this.GetListAppraisalDt(_appraisalDt.TransNmbr);

                    foreach (HRMTrAppraisalDt _row in _detail)
                    {
                        _total += _row.Result * _row.Bobot;
                        _bobot += _row.Bobot;
                    }
                    _average = (_total - (_appraisalDt.Result * _appraisalDt.Bobot)) / (_bobot - _appraisalDt.Bobot);

                    HRMTrAppraisalHd _appraisalHd = this.GetSingleAppraisal(_appraisalDt.TransNmbr);
                    _appraisalHd.Average = _average;

                    this.db.HRMTrAppraisalDts.DeleteOnSubmit(_appraisalDt);
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

        public bool AddAppraisalDt(HRMTrAppraisalDt _prmAppraisalDt)
        {
            bool _result = false;

            try
            {
                decimal _total = 0;
                decimal _bobot = 0;
                decimal _average = 0;

                List<HRMTrAppraisalDt> _detail = this.GetListAppraisalDt(_prmAppraisalDt.TransNmbr);

                foreach (HRMTrAppraisalDt _row in _detail)
                {
                    _total += _row.Result * _row.Bobot;
                    _bobot += _row.Bobot;
                }
                _average = (_total + (_prmAppraisalDt.Result * _prmAppraisalDt.Bobot)) / (_bobot + _prmAppraisalDt.Bobot);

                HRMTrAppraisalHd _appraisalHd = this.GetSingleAppraisal(_prmAppraisalDt.TransNmbr);
                _appraisalHd.Average = _average;

                this.db.HRMTrAppraisalDts.InsertOnSubmit(_prmAppraisalDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditAppraisalDt(HRMTrAppraisalDt _prmAppraisalDt, decimal _prmResult)
        {
            bool _result = false;

            try
            {
                decimal _total = 0;
                decimal _bobot = 0;
                decimal _average = 0;

                List<HRMTrAppraisalDt> _detail = this.GetListAppraisalDt(_prmAppraisalDt.TransNmbr);

                foreach (HRMTrAppraisalDt _row in _detail)
                {
                    _total += _row.Result * _row.Bobot;
                    _bobot += _row.Bobot;
                }
                _average = (_total - (_prmResult * _prmAppraisalDt.Bobot) + (_prmAppraisalDt.Result * _prmAppraisalDt.Bobot)) / _bobot;

                HRMTrAppraisalHd _appraisalHd = this.GetSingleAppraisal(_prmAppraisalDt.TransNmbr);
                _appraisalHd.Average = _average;

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

        ~TrAppraisalBL()
        {

        }
    }
}
