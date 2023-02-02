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
    public sealed class ScreeningScheduleBL : Base
    {
        public ScreeningScheduleBL()
        {

        }

        #region HRM_ScreeningSchedule
        public double RowsCountHRMScreeningSchedule(string _prmCategory, string _prmKeyword)
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
            else if (_prmCategory == "ProcessType")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            var _query =
                        (
                           from _screeningSchedule in this.db.HRM_ScreeningSchedules
                           join _master_ProcessType in this.db.HRM_Master_ProcessTypes
                                on _screeningSchedule.ProcessTypeCode equals _master_ProcessType.ProcessTypeCode
                           where (SqlMethods.Like(_screeningSchedule.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like((_screeningSchedule.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                              && (SqlMethods.Like(_master_ProcessType.ProcessTypeName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                           select _screeningSchedule.ScreeningScheduleCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_ScreeningSchedule> GetListHRMScreeningSchedule(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_ScreeningSchedule> _result = new List<HRM_ScreeningSchedule>();

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
            else if (_prmCategory == "ProcessType")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _screeningSchedule in this.db.HRM_ScreeningSchedules
                                join _master_ProcessType in this.db.HRM_Master_ProcessTypes
                                    on _screeningSchedule.ProcessTypeCode equals _master_ProcessType.ProcessTypeCode
                                where (SqlMethods.Like(_screeningSchedule.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like((_screeningSchedule.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && (SqlMethods.Like(_master_ProcessType.ProcessTypeName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                orderby _screeningSchedule.EditDate descending
                                select new
                                {
                                    ScreeningScheduleCode = _screeningSchedule.ScreeningScheduleCode,
                                    TransNmbr = _screeningSchedule.TransNmbr,
                                    FileNmbr = _screeningSchedule.FileNmbr,
                                    TransDate = _screeningSchedule.TransDate,
                                    Status = _screeningSchedule.Status,
                                    ApplicantResumeScreeningCode = _screeningSchedule.ApplicantResumeScreeningCode,
                                    ProcessTypeCode = _screeningSchedule.ProcessTypeCode,
                                    ProcessTypeName = _master_ProcessType.ProcessTypeName,
                                    ContactingDate = _screeningSchedule.ContactingDate,
                                    MeetingDate = _screeningSchedule.MeetingDate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ScreeningSchedule(_row.ScreeningScheduleCode, _row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.ApplicantResumeScreeningCode, _row.ProcessTypeCode, _row.ProcessTypeName, _row.ContactingDate, _row.MeetingDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_ScreeningSchedule GetSingleHRMScreeningSchedule(Guid _prmCode)
        {
            HRM_ScreeningSchedule _result = null;

            try
            {
                _result = this.db.HRM_ScreeningSchedules.Single(_temp => _temp.ScreeningScheduleCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_ScreeningSchedule GetSingleHRMScreeningScheduleView(Guid _prmCode)
        {
            HRM_ScreeningSchedule _result = new HRM_ScreeningSchedule();

            try
            {
                var _query = (
                               from _screeningSchedule in this.db.HRM_ScreeningSchedules
                               join _master_ProcessType in this.db.HRM_Master_ProcessTypes
                                    on _screeningSchedule.ProcessTypeCode equals _master_ProcessType.ProcessTypeCode
                               orderby _screeningSchedule.EditDate descending
                               where _screeningSchedule.ScreeningScheduleCode == _prmCode
                               select new
                               {
                                   ScreeningScheduleCode = _screeningSchedule.ScreeningScheduleCode,
                                   TransNmbr = _screeningSchedule.TransNmbr,
                                   FileNmbr = _screeningSchedule.FileNmbr,
                                   TransDate = _screeningSchedule.TransDate,
                                   Status = _screeningSchedule.Status,
                                   ApplicantResumeScreeningCode = _screeningSchedule.ApplicantResumeScreeningCode,
                                   ProcessTypeCode = _screeningSchedule.ProcessTypeCode,
                                   ProcessTypeName = _master_ProcessType.ProcessTypeName,
                                   ContactingDate = _screeningSchedule.ContactingDate,
                                   MeetingDate = _screeningSchedule.MeetingDate
                               }
                           ).Single();

                _result.ScreeningScheduleCode = _query.ScreeningScheduleCode;
                _result.TransNmbr = _query.TransNmbr;
                _result.FileNmbr = _query.FileNmbr;
                _result.TransDate = _query.TransDate;
                _result.Status = _query.Status;
                _result.ApplicantResumeScreeningCode = _query.ApplicantResumeScreeningCode;
                _result.ProcessTypeCode = _query.ProcessTypeCode;
                _result.ProcessTypeName = _query.ProcessTypeName;
                _result.ContactingDate = _query.ContactingDate;
                _result.MeetingDate = _query.MeetingDate;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMScreeningSchedule(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_ScreeningSchedule _screeningSchedule = this.db.HRM_ScreeningSchedules.Single(_temp => _temp.ScreeningScheduleCode == new Guid(_prmCode[i]));

                    if (_screeningSchedule != null)
                    {
                        if ((_screeningSchedule.FileNmbr ?? "").Trim() == "")
                        {
                            this.db.HRM_ScreeningSchedules.DeleteOnSubmit(_screeningSchedule);

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

        public string AddHRMScreeningSchedule(HRM_ScreeningSchedule _prmHRM_ScreeningSchedule)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmHRM_ScreeningSchedule.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRM_ScreeningSchedules.InsertOnSubmit(_prmHRM_ScreeningSchedule);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmHRM_ScreeningSchedule.ScreeningScheduleCode.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMScreeningSchedule(HRM_ScreeningSchedule _prmHRM_ScreeningSchedule)
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

        public string GetAppr(Guid _prmScreeningScheduleCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_ScreeningScheduleGetAppr(_prmScreeningScheduleCode, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMScreeningSchedule);
                    _transActivity.TransNmbr = _prmScreeningScheduleCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = HttpContext.Current.User.Identity.Name;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();

                    _result = "Waiting For Confirmation Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Waiting For Confirmation Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(Guid _prmScreeningScheduleCode)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spHRM_ScreeningScheduleApprove(_prmScreeningScheduleCode, ref _result);

                    if (_result == "")
                    {
                        HRM_ScreeningSchedule _screeningSchedule = this.GetSingleHRMScreeningSchedule(_prmScreeningScheduleCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_screeningSchedule.TransDate.Year, _screeningSchedule.TransDate.Month, AppModule.GetValue(TransactionType.HRMScreeningSchedule), this._companyTag, ""))
                        {
                            _screeningSchedule.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMScreeningSchedule);
                        _transActivity.TransNmbr = _prmScreeningScheduleCode.ToString();
                        _transActivity.FileNmbr = _screeningSchedule.FileNmbr;
                        _transActivity.Username = HttpContext.Current.User.Identity.Name;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);

                        this.db.SubmitChanges();

                        _scope.Complete();

                        _result = "Confirmation Success";
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Confirmation Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Posting(Guid _prmScreeningScheduleCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_ScreeningSchedulePosting(_prmScreeningScheduleCode, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMScreeningSchedule);
                    _transActivity.TransNmbr = _prmScreeningScheduleCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleHRMScreeningSchedule(_prmScreeningScheduleCode).FileNmbr;
                    _transActivity.Username = HttpContext.Current.User.Identity.Name;
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

        public List<HRM_ScreeningSchedule> GetListDDLScreeningSchedule()
        {
            List<HRM_ScreeningSchedule> _result = new List<HRM_ScreeningSchedule>();

            try
            {
                var _query = (
                                from _screeningSchedule in this.db.HRM_ScreeningSchedules
                                where _screeningSchedule.Status == ScreeningScheduleDataMapper.GetStatus(ScreeningScheduleStatus.Confirmed)
                                orderby _screeningSchedule.FileNmbr ascending
                                select new
                                {
                                    ScreeningScheduleCode = _screeningSchedule.ScreeningScheduleCode,
                                    FileNmbr = _screeningSchedule.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ScreeningSchedule(_row.ScreeningScheduleCode, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_ScreeningSchedule> GetListDDLScreeningScheduleNotProcessed()
        {
            List<HRM_ScreeningSchedule> _result = new List<HRM_ScreeningSchedule>();

            try
            {
                var _query = (
                                from _screeningSchedule in this.db.HRM_ScreeningSchedules
                                where _screeningSchedule.Status == ScreeningScheduleDataMapper.GetStatus(ScreeningScheduleStatus.Confirmed)
                                    && !(
                                            from _screeningProcess in this.db.HRM_ScreeningProcesses
                                            where _screeningProcess.ScreeningScheduleCode == _screeningSchedule.ScreeningScheduleCode
                                            select _screeningProcess.ScreeningScheduleCode
                                        ).Contains(_screeningSchedule.ScreeningScheduleCode)
                                orderby _screeningSchedule.FileNmbr ascending
                                select new
                                {
                                    ScreeningScheduleCode = _screeningSchedule.ScreeningScheduleCode,
                                    FileNmbr = _screeningSchedule.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_ScreeningSchedule(_row.ScreeningScheduleCode, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetFileNmbrByCode(Guid _prmCode)
        {
            String _result = "";

            try
            {
                _result = (
                                from _screeningSchedule in this.db.HRM_ScreeningSchedules
                                where _screeningSchedule.ScreeningScheduleCode == _prmCode
                                select _screeningSchedule.FileNmbr
                            ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~ScreeningScheduleBL()
        {
        }
    }
}
