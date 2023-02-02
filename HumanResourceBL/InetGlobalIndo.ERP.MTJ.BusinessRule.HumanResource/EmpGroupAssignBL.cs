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
    public sealed class EmpGroupAssignBL : Base
    {
        public EmpGroupAssignBL()
        {

        }

        #region HRMTrEmpGroupAssignHd
        public double RowsCountEmpGroupAssign(string _prmCategory, string _prmKeyword)
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
                               from _empGroupAssign in this.db.HRMTrEmpGroupAssignHds
                               where (SqlMethods.Like(_empGroupAssign.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_empGroupAssign.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _empGroupAssign.Status != EmpGroupAssignDataMapper.GetStatus(TransStatus.Deleted)
                               select _empGroupAssign.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrEmpGroupAssignHd> GetListEmpGroupAssign(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrEmpGroupAssignHd> _result = new List<HRMTrEmpGroupAssignHd>();

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
                                from _empGroupAssign in this.db.HRMTrEmpGroupAssignHds
                                where (SqlMethods.Like(_empGroupAssign.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_empGroupAssign.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _empGroupAssign.Status != EmpGroupAssignDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _empGroupAssign.EditDate descending
                                select new
                                {
                                    TransNmbr = _empGroupAssign.TransNmbr,
                                    FileNmbr = _empGroupAssign.FileNmbr,
                                    Status = _empGroupAssign.Status,
                                    TransDate = _empGroupAssign.TransDate,
                                    EffectiveDate = _empGroupAssign.EffectiveDate,
                                    Remark = _empGroupAssign.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrEmpGroupAssignHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.EffectiveDate, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrEmpGroupAssignHd GetSingleEmpGroupAssign(string _prmCode)
        {
            HRMTrEmpGroupAssignHd _result = null;

            try
            {
                _result = this.db.HRMTrEmpGroupAssignHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleHRMTrEmpGroupAssignHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrEmpGroupAssignHd _hRMTrEmpGroupAssignHd = this.db.HRMTrEmpGroupAssignHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrEmpGroupAssignHd != null)
                    {
                        if (_hRMTrEmpGroupAssignHd.Status != EmpGroupAssignDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiEmpGroupAssign(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrEmpGroupAssignHd _EmpGroupAssign = this.db.HRMTrEmpGroupAssignHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_EmpGroupAssign.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrEmpGroupAssignDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrEmpGroupAssignDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrEmpGroupAssignHds.DeleteOnSubmit(_EmpGroupAssign);

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

        public bool DeleteMultiApproveHRMTrEmpGroupAssignHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrEmpGroupAssignHd _hRMTrEmpGroupAssignHd = this.db.HRMTrEmpGroupAssignHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrEmpGroupAssignHd.Status == EmpGroupAssignDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrEmpGroupAssignHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrEmpGroupAssignHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrEmpGroupAssignHd != null)
                    {
                        if ((_hRMTrEmpGroupAssignHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrEmpGroupAssignDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrEmpGroupAssignDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrEmpGroupAssignHds.DeleteOnSubmit(_hRMTrEmpGroupAssignHd);

                            _result = true;
                        }
                        else if (_hRMTrEmpGroupAssignHd.FileNmbr != "" && _hRMTrEmpGroupAssignHd.Status == DinasDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrEmpGroupAssignHd.Status = DinasDataMapper.GetStatus(TransStatus.Deleted);
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

        public bool EditEmpGroupAssign(HRMTrEmpGroupAssignHd _prmEmpGroupAssign)
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

        public string AddEmpGroupAssign(HRMTrEmpGroupAssignHd _prmEmpGroupAssign)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmEmpGroupAssign.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrEmpGroupAssignHds.InsertOnSubmit(_prmEmpGroupAssign);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmEmpGroupAssign.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrEmpGroupAssignHd> GetListDDLEmpGroupAssign()
        {
            List<HRMTrEmpGroupAssignHd> _result = new List<HRMTrEmpGroupAssignHd>();

            try
            {
                var _query = (
                                from _empGroupAssignHd in this.db.HRMTrEmpGroupAssignHds
                                where _empGroupAssignHd.Status == EmpGroupAssignDataMapper.GetStatus(TransStatus.Posted)
                                orderby _empGroupAssignHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _empGroupAssignHd.TransNmbr,
                                    FileNmbr = _empGroupAssignHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrEmpGroupAssignHd(_row.TransNmbr, _row.FileNmbr));
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
                this.db.spHRM_EmpGroupAssignGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.EmpGroupAssign);
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
                    this.db.spHRM_EmpGroupAssignApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrEmpGroupAssignHd _empGroupAssign = this.GetSingleEmpGroupAssign(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_empGroupAssign.TransDate).Year, Convert.ToDateTime(_empGroupAssign.TransDate).Month, AppModule.GetValue(TransactionType.EmpGroupAssign), this._companyTag, ""))
                        {
                            _empGroupAssign.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.EmpGroupAssign);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _empGroupAssign.FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

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
                this.db.spHRM_EmpGroupAssignPosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.EmpGroupAssign);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleEmpGroupAssign(_prmCode).FileNmbr;
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
                this.db.spHRM_EmpGroupAssignUnPost(_prmCode, _prmuser, ref _result);

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

        #region HRMTrEmpGroupAssignDt
        public double RowsCountEmpGroupAssignDt(string _prmCode)
        {
            double _result = 0;

            var _query = (
                            from _empGroupAssignDt in this.db.HRMTrEmpGroupAssignDts
                            where _empGroupAssignDt.TransNmbr == _prmCode
                            select _empGroupAssignDt.EmpNumb
                         ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrEmpGroupAssignDt> GetListEmpGroupAssignDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrEmpGroupAssignDt> _result = new List<HRMTrEmpGroupAssignDt>();

            try
            {
                var _query = (
                                from _empGroupAssignDt in this.db.HRMTrEmpGroupAssignDts
                                where _empGroupAssignDt.TransNmbr == _prmCode
                                orderby _empGroupAssignDt.EmpNumb ascending
                                select new
                                {
                                    TransNmbr = _empGroupAssignDt.TransNmbr,
                                    EmpNumb = _empGroupAssignDt.EmpNumb,
                                    EmpName = (
                                                    from _msEmployee in this.db.MsEmployees
                                                    where _msEmployee.EmpNumb == _empGroupAssignDt.EmpNumb
                                                    select _msEmployee.EmpName
                                               ).FirstOrDefault(),
                                    FromEmpGroup = _empGroupAssignDt.FromEmpGroup,
                                    FromEmpGroupName = (
                                                            from _msEmpGroup in this.db.HRMMsEmpGroups
                                                            where _msEmpGroup.EmpGroupCode == _empGroupAssignDt.FromEmpGroup
                                                            select _msEmpGroup.EmpGroupName
                                                       ).FirstOrDefault(),
                                    ToEmpGroup = _empGroupAssignDt.ToEmpGroup,
                                    ToEmpGroupName = (
                                                            from _msEmpGroup2 in this.db.HRMMsEmpGroups
                                                            where _msEmpGroup2.EmpGroupCode == _empGroupAssignDt.ToEmpGroup
                                                            select _msEmpGroup2.EmpGroupName
                                                       ).FirstOrDefault(),
                                    Remark = _empGroupAssignDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrEmpGroupAssignDt(_row.TransNmbr, _row.EmpNumb, _row.EmpName, _row.FromEmpGroup, _row.FromEmpGroupName, _row.ToEmpGroup, _row.ToEmpGroupName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrEmpGroupAssignDt GetSingleEmpGroupAssignDt(string _prmEmpNumb, string _prmTransNmbr)
        {
            HRMTrEmpGroupAssignDt _result = null;

            try
            {
                _result = this.db.HRMTrEmpGroupAssignDts.Single(_temp => _temp.EmpNumb == _prmEmpNumb && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiEmpGroupAssignDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrEmpGroupAssignDt _EmpGroupAssignDt = this.db.HRMTrEmpGroupAssignDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.EmpNumb == _tempSplit[1]);

                    this.db.HRMTrEmpGroupAssignDts.DeleteOnSubmit(_EmpGroupAssignDt);
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

        public bool AddEmpGroupAssignDt(HRMTrEmpGroupAssignDt _prmEmpGroupAssignDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrEmpGroupAssignDts.InsertOnSubmit(_prmEmpGroupAssignDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditEmpGroupAssignDt(HRMTrEmpGroupAssignDt _prmEmpGroupAssignDt)
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

        ~EmpGroupAssignBL()
        {

        }
    }
}
