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
    public sealed class LeaveRequestBL : Base
    {
        public LeaveRequestBL()
        {

        }

        #region HRMTrLeaveRequestHd
        public double RowsCountLeaveRequest(string _prmCategory, string _prmKeyword)
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
                               from _leaveRequest in this.db.HRMTrLeaveRequestHds
                               where (SqlMethods.Like(_leaveRequest.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_leaveRequest.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _leaveRequest.Status != LeaveRequestDataMapper.GetStatusByte(TransStatus.Deleted)
                               select _leaveRequest.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrLeaveRequestHd> GetListLeaveRequest(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrLeaveRequestHd> _result = new List<HRMTrLeaveRequestHd>();

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
                                from _leaveRequest in this.db.HRMTrLeaveRequestHds
                                where (SqlMethods.Like(_leaveRequest.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_leaveRequest.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _leaveRequest.Status != LeaveRequestDataMapper.GetStatusByte(TransStatus.Deleted)
                                orderby _leaveRequest.EditDate descending
                                select new
                                {
                                    TransNmbr = _leaveRequest.TransNmbr,
                                    FileNmbr = _leaveRequest.FileNmbr,
                                    Status = _leaveRequest.Status,
                                    TransDate = _leaveRequest.TransDate,
                                    Remark = _leaveRequest.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLeaveRequestHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLeaveRequestHd GetSingleLeaveRequest(string _prmCode)
        {
            HRMTrLeaveRequestHd _result = null;

            try
            {
                _result = this.db.HRMTrLeaveRequestHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleLeaveRequestApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLeaveRequestHd _hRMTrLeaveRequestHd = this.db.HRMTrLeaveRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrLeaveRequestHd != null)
                    {
                        if (_hRMTrLeaveRequestHd.Status != LeaveRequestDataMapper.GetStatusByte(TransStatus.Posted))
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

        public bool DeleteMultiLeaveRequest(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLeaveRequestHd _leaveRequest = this.db.HRMTrLeaveRequestHds.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if ((_leaveRequest.FileNmbr ?? "").Trim() == "")
                    {
                        var _query = (from _detail in this.db.HRMTrLeaveRequestDts
                                      where _detail.TransNmbr == _prmCode[i]
                                      select _detail);

                        this.db.HRMTrLeaveRequestDts.DeleteAllOnSubmit(_query);

                        this.db.HRMTrLeaveRequestHds.DeleteOnSubmit(_leaveRequest);

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

        public bool DeleteMultiApproveLeaveRequest(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLeaveRequestHd _hRMTrLeaveRequestHd = this.db.HRMTrLeaveRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrLeaveRequestHd.Status == LeaveRequestDataMapper.GetStatusByte(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrLeaveRequestHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrLeaveRequestHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrLeaveRequestHd != null)
                    {
                        if ((_hRMTrLeaveRequestHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrLeaveRequestDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrLeaveRequestDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrLeaveRequestHds.DeleteOnSubmit(_hRMTrLeaveRequestHd);

                            _result = true;
                        }
                        else if (_hRMTrLeaveRequestHd.FileNmbr != "" && _hRMTrLeaveRequestHd.Status == LeaveRequestDataMapper.GetStatusByte(TransStatus.Approved))
                        {
                            _hRMTrLeaveRequestHd.Status = LeaveRequestDataMapper.GetStatusByte(TransStatus.Deleted);
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

        public bool EditLeaveRequest(HRMTrLeaveRequestHd _prmLeaveRequest)
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

        public string AddLeaveRequest(HRMTrLeaveRequestHd _prmLeaveRequest)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmLeaveRequest.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrLeaveRequestHds.InsertOnSubmit(_prmLeaveRequest);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmLeaveRequest.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMTrLeaveRequestHd> GetListDDLLeaveRequest()
        {
            List<HRMTrLeaveRequestHd> _result = new List<HRMTrLeaveRequestHd>();

            try
            {
                var _query = (
                                from _leaveRequestHd in this.db.HRMTrLeaveRequestHds
                                where _leaveRequestHd.Status == LeaveRequestDataMapper.GetStatus(TransStatus.Posted)
                                orderby _leaveRequestHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _leaveRequestHd.TransNmbr,
                                    FileNmbr = _leaveRequestHd.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLeaveRequestHd(_row.TransNmbr, _row.FileNmbr));
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
                this.db.spHRM_LeaveRequestGetAppr(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.LeaveRequest);
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
                    this.db.spHRM_LeaveRequestApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        HRMTrLeaveRequestHd _leaveRequest = this.GetSingleLeaveRequest(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_leaveRequest.TransDate).Year, Convert.ToDateTime(_leaveRequest.TransDate).Month, AppModule.GetValue(TransactionType.LeaveRequest), this._companyTag, ""))
                        {
                            _leaveRequest.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.LeaveRequest);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = _leaveRequest.FileNmbr;
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
                this.db.spHRM_LeaveRequestPosting(_prmCode, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.LeaveRequest);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleLeaveRequest(_prmCode).FileNmbr;
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
                this.db.spHRM_LeaveRequestUnPost(_prmCode, _prmuser, ref _result);

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

        #region HRMTrLeaveRequestDt
        public double RowsCountLeaveRequestDt(string _prmCode)
        {
            double _result = 0;

            var _query = (
                            from _leaveRequestDt in this.db.HRMTrLeaveRequestDts
                            where _leaveRequestDt.TransNmbr == _prmCode
                            select _leaveRequestDt.EmpNumb
                         ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrLeaveRequestDt> GetListLeaveRequestDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMTrLeaveRequestDt> _result = new List<HRMTrLeaveRequestDt>();

            try
            {
                var _query = (
                                from _leaveRequestDt in this.db.HRMTrLeaveRequestDts
                                where _leaveRequestDt.TransNmbr == _prmCode
                                orderby _leaveRequestDt.EmpNumb ascending
                                select new
                                {
                                    TransNmbr = _leaveRequestDt.TransNmbr,
                                    EmpNumb = _leaveRequestDt.EmpNumb,
                                    EmpName = (
                                                    from _msEmployee in this.db.MsEmployees
                                                    where _msEmployee.EmpNumb == _leaveRequestDt.EmpNumb
                                                    select _msEmployee.EmpName
                                               ).FirstOrDefault(),
                                    JobTitle = _leaveRequestDt.JobTitle,
                                    JobTitleName = (
                                                            from _msEmpGroup in this.db.MsJobTitles
                                                            where _msEmpGroup.JobTtlCode == _leaveRequestDt.JobTitle
                                                            select _msEmpGroup.JobTtlName
                                                       ).FirstOrDefault(),
                                    LeavesCode = _leaveRequestDt.LeavesCode,
                                    LeavesName = (
                                                            from _msEmpGroup2 in this.db.HRMMsLeaves
                                                            where _msEmpGroup2.LeavesCode == _leaveRequestDt.LeavesCode
                                                            select _msEmpGroup2.LeavesName
                                                       ).FirstOrDefault(),
                                    IsLess1Day = _leaveRequestDt.IsLess1Day,
                                    StartDate = _leaveRequestDt.StartDate,
                                    EndDate = _leaveRequestDt.EndDate,
                                    Days = _leaveRequestDt.Days,
                                    Holiday = _leaveRequestDt.Holiday,
                                    Dispensation = _leaveRequestDt.Dispensation,
                                    Balance = _leaveRequestDt.Balance,
                                    Taken = _leaveRequestDt.Taken,
                                    ContactAddress = _leaveRequestDt.ContactAddress,
                                    ContactPhone = _leaveRequestDt.ContactPhone,
                                    ReasonLeave = _leaveRequestDt.ReasonLeave
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLeaveRequestDt(_row.TransNmbr, _row.EmpNumb, _row.EmpName, _row.JobTitle, _row.JobTitleName, _row.LeavesCode, _row.LeavesName, _row.IsLess1Day, _row.StartDate, _row.EndDate, _row.Days, _row.Holiday, _row.Dispensation, _row.Balance, _row.Taken, _row.ContactAddress, _row.ContactPhone, _row.ReasonLeave));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLeaveRequestDt GetSingleLeaveRequestDt(string _prmEmpNumb, string _prmTransNmbr)
        {
            HRMTrLeaveRequestDt _result = null;

            try
            {
                _result = this.db.HRMTrLeaveRequestDts.Single(_temp => _temp.EmpNumb == _prmEmpNumb && _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiLeaveRequestDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    HRMTrLeaveRequestDt _leaveRequestDt = this.db.HRMTrLeaveRequestDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.EmpNumb == _tempSplit[1]);

                    this.db.HRMTrLeaveRequestDts.DeleteOnSubmit(_leaveRequestDt);
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

        public bool AddLeaveRequestDt(HRMTrLeaveRequestDt _prmLeaveRequestDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrLeaveRequestDts.InsertOnSubmit(_prmLeaveRequestDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditLeaveRequestDt(HRMTrLeaveRequestDt _prmLeaveRequestDt)
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

        ~LeaveRequestBL()
        {

        }
    }
}
