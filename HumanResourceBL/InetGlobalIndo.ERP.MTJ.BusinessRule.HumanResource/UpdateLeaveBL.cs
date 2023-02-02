using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class UpdateLeaveBL : Base
    {
        public UpdateLeaveBL()
        {

        }

        #region HRM_UpdateLeave
        public double RowsCountHRMUpdateLeave(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            var _query =
                        (
                           from _updateLeave in this.db.HRM_UpdateLeaves
                           join _msEmployee in this.db.MsEmployees
                                on _updateLeave.EmpNumb equals _msEmployee.EmpNumb
                           where (SqlMethods.Like(_updateLeave.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like((_updateLeave.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                              && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                           select _updateLeave.UpdateLeaveCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_UpdateLeave> GetListHRMUpdateLeave(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_UpdateLeave> _result = new List<HRM_UpdateLeave>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "EmpName")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _updateLeave in this.db.HRM_UpdateLeaves
                                join _msEmployee in this.db.MsEmployees
                                on _updateLeave.EmpNumb equals _msEmployee.EmpNumb
                                where (SqlMethods.Like(_updateLeave.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_updateLeave.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                orderby _updateLeave.EditDate descending
                                select new
                                {
                                    UpdateLeaveCode = _updateLeave.UpdateLeaveCode,
                                    TransNmbr = _updateLeave.TransNmbr,
                                    FileNmbr = _updateLeave.FileNmbr,
                                    TransDate = _updateLeave.TransDate,
                                    Status = _updateLeave.Status,
                                    EmpNumb = _updateLeave.EmpNumb,
                                    EmpName = _msEmployee.EmpName,
                                    LeaveDayRemain = _updateLeave.LeaveDayRemain,
                                    LeaveCurrent = _updateLeave.LeaveCurrent,
                                    ExpiredDateLeaveCurrent = _updateLeave.ExpiredDateLeaveCurrent,
                                    ExpiredDateLeaveRemain = _updateLeave.ExpiredDateLeaveRemain,
                                    Remark = _updateLeave.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_UpdateLeave(_row.UpdateLeaveCode, _row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.EmpNumb, _row.EmpName, _row.LeaveDayRemain, _row.LeaveCurrent, _row.ExpiredDateLeaveCurrent, _row.ExpiredDateLeaveRemain, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_UpdateLeave GetSingleHRMUpdateLeave(Guid _prmCode)
        {
            HRM_UpdateLeave _result = null;

            try
            {
                _result = this.db.HRM_UpdateLeaves.Single(_temp => _temp.UpdateLeaveCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMUpdateLeave(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_UpdateLeave _updateLeave = this.db.HRM_UpdateLeaves.Single(_temp => _temp.UpdateLeaveCode == new Guid(_prmCode[i]));

                    if (_updateLeave != null)
                    {
                        if ((_updateLeave.FileNmbr ?? "").Trim() == "")
                        {
                            this.db.HRM_UpdateLeaves.DeleteOnSubmit(_updateLeave);

                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            break;
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

        public string AddHRMUpdateLeave(HRM_UpdateLeave _prmHRM_UpdateLeave)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmHRM_UpdateLeave.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRM_UpdateLeaves.InsertOnSubmit(_prmHRM_UpdateLeave);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmHRM_UpdateLeave.UpdateLeaveCode.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMUpdateLeave(HRM_UpdateLeave _prmHRM_UpdateLeave)
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

        public string GetAppr(Guid _prmUpdateLeaveCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_UpdateLeaveGetAppr(_prmUpdateLeaveCode, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMUpdateLeave);
                    _transActivity.TransNmbr = _prmUpdateLeaveCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = HttpContext.Current.User.Identity.Name;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();

                    _result = "Waiting For Approval Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Waiting For Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(Guid _prmUpdateLeaveCode)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spHRM_UpdateLeaveApprove(_prmUpdateLeaveCode, ref _result);

                    if (_result == "")
                    {
                        HRM_UpdateLeave _updateLeave = this.GetSingleHRMUpdateLeave(_prmUpdateLeaveCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_updateLeave.TransDate.Year, _updateLeave.TransDate.Month, AppModule.GetValue(TransactionType.HRMUpdateLeave), this._companyTag, ""))
                        {
                            _updateLeave.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMUpdateLeave);
                        _transActivity.TransNmbr = _prmUpdateLeaveCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleHRMUpdateLeave(_prmUpdateLeaveCode).FileNmbr;
                        _transActivity.Username = HttpContext.Current.User.Identity.Name;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        
                        this.db.SubmitChanges();

                        _scope.Complete();

                        _result = "Approved Success";
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Approved Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public String GetFileNmbrByCode(Guid _prmCode)
        {
            String _result = "";

            try
            {
                _result = (
                                from _updateLeave in this.db.HRM_UpdateLeaves
                                where _updateLeave.UpdateLeaveCode == _prmCode
                                select _updateLeave.FileNmbr
                            ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~UpdateLeaveBL()
        {
        }
    }
}
