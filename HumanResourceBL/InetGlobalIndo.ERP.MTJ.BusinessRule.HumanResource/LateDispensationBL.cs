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
    public sealed class LateDispensationBL : Base
    {
        public LateDispensationBL()
        {
        }

        #region HRMTrLateDispensationHd
        public double RowsCountHRMTrLateDispensationHd(string _prmCategory, string _prmKeyword)
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

            var _query =
                        (
                           from _LateDispensationHd in this.db.HRMTrLateDispensationHds
                           where (SqlMethods.Like(_LateDispensationHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like((_LateDispensationHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                              && _LateDispensationHd.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted)
                           select _LateDispensationHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrLateDispensationHd> GetListHRMTrLateDispensationHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrLateDispensationHd> _result = new List<HRMTrLateDispensationHd>();

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

            try
            {
                var _query = (
                                from _LateDispensationHd in this.db.HRMTrLateDispensationHds
                                where (SqlMethods.Like(_LateDispensationHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like((_LateDispensationHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && _LateDispensationHd.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _LateDispensationHd.EditDate descending
                                select new
                                {
                                    TransNmbr = _LateDispensationHd.TransNmbr,
                                    FileNmbr = _LateDispensationHd.FileNmbr,
                                    TransDate = _LateDispensationHd.TransDate,
                                    Status = _LateDispensationHd.Status,
                                    Remark = _LateDispensationHd.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLateDispensationHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLateDispensationHd GetSingleHRMTrLateDispensationHd(String _prmCode)
        {
            HRMTrLateDispensationHd _result = null;

            try
            {
                _result = this.db.HRMTrLateDispensationHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleHRMTrLateDispensationHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLateDispensationHd _hRMTrLateDispensationHd = this.db.HRMTrLateDispensationHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrLateDispensationHd != null)
                    {
                        if (_hRMTrLateDispensationHd.Status != TransactionDataMapper.GetStatus(TransStatus.Posted))
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

        public HRMTrLateDispensationHd GetSingleHRMTrLateDispensationHdView(String _prmCode)
        {
            HRMTrLateDispensationHd _result = new HRMTrLateDispensationHd();

            try
            {
                var _query = (
                               from _LateDispensationHd in this.db.HRMTrLateDispensationHds
                               orderby _LateDispensationHd.EditDate descending
                               where _LateDispensationHd.TransNmbr == _prmCode
                               select new
                               {
                                   TransNmbr = _LateDispensationHd.TransNmbr,
                                   FileNmbr = _LateDispensationHd.FileNmbr,
                                   TransDate = _LateDispensationHd.TransDate,
                                   Status = _LateDispensationHd.Status,
                                   Remark = _LateDispensationHd.Remark
                               }
                           ).Single();

                _result.TransNmbr = _query.TransNmbr;
                _result.FileNmbr = _query.FileNmbr;
                _result.TransDate = _query.TransDate;
                _result.Status = _query.Status;
                _result.Remark = _query.Remark;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMTrLateDispensationHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLateDispensationHd _LateDispensationHd = this.db.HRMTrLateDispensationHds.Single(_temp => _temp.TransNmbr == _prmCode[i].ToString().ToLower());

                    if (_LateDispensationHd != null)
                    {
                        if ((_LateDispensationHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrLateDispensationDts
                                          where _detail.TransNmbr == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.HRMTrLateDispensationDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrLateDispensationHds.DeleteOnSubmit(_LateDispensationHd);

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

        public bool DeleteMultiApproveHRMTrLateDispensationHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrLateDispensationHd _hRMTrLateDispensationHd = this.db.HRMTrLateDispensationHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrLateDispensationHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrLateDispensationHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrLateDispensationHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrLateDispensationHd != null)
                    {
                        if ((_hRMTrLateDispensationHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrLateDispensationDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrLateDispensationDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrLateDispensationHds.DeleteOnSubmit(_hRMTrLateDispensationHd);

                            _result = true;
                        }
                        else if (_hRMTrLateDispensationHd.FileNmbr != "" && _hRMTrLateDispensationHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrLateDispensationHd.Status = TransactionDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddHRMTrLateDispensationHd(HRMTrLateDispensationHd _prmHRMTrLateDispensationHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmHRMTrLateDispensationHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrLateDispensationHds.InsertOnSubmit(_prmHRMTrLateDispensationHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmHRMTrLateDispensationHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMTrLateDispensationHd(HRMTrLateDispensationHd _prmHRMTrLateDispensationHd)
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

        public string GetAppr(String _prmLateDispensationCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_LateDispensationGetAppr(_prmLateDispensationCode, HttpContext.Current.User.Identity.Name, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLateDispensation);
                    _transActivity.TransNmbr = _prmLateDispensationCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = HttpContext.Current.User.Identity.Name;
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
                _result = "Get Approval Failed " + ex.Message;
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(String _prmLateDispensationCode)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spHRM_LateDispensationApprove(_prmLateDispensationCode, HttpContext.Current.User.Identity.Name, ref _result);

                    if (_result == "")
                    {
                        HRMTrLateDispensationHd _LateDispensationHd = this.GetSingleHRMTrLateDispensationHd(_prmLateDispensationCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_LateDispensationHd.TransDate.Year, _LateDispensationHd.TransDate.Month, AppModule.GetValue(TransactionType.HRMLateDispensation), this._companyTag, ""))
                        {
                            _LateDispensationHd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLateDispensation);
                        _transActivity.TransNmbr = _prmLateDispensationCode.ToString();
                        _transActivity.FileNmbr = _LateDispensationHd.FileNmbr;
                        _transActivity.Username = HttpContext.Current.User.Identity.Name;
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
                _result = "Approve Failed " + ex.Message;
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Posting(String _prmLateDispensationCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_LateDispensationPosting(_prmLateDispensationCode, HttpContext.Current.User.Identity.Name, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMLateDispensation);
                    _transActivity.TransNmbr = _prmLateDispensationCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleHRMTrLateDispensationHd(_prmLateDispensationCode).FileNmbr;
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
                _result = "Posting Failed " + ex.Message;
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Unposting(String _prmLateDispensationCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_LateDispensationUnPost(_prmLateDispensationCode, HttpContext.Current.User.Identity.Name, ref _result);

                if (_result == "")
                {
                    _result = "Unposting Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Unposting Failed " + ex.Message;
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        //public List<HRMTrLateDispensationHd> GetListDDLLateDispensation()
        //{
        //    List<HRMTrLateDispensationHd> _result = new List<HRMTrLateDispensationHd>();

        //    try
        //    {
        //        var _query = (
        //                        from _LateDispensationHd in this.db.HRMTrLateDispensationHds
        //                        where _LateDispensationHd.Status == TransactionDataMapper.GetStatus(LateDispensationStatus.Approved)
        //                        orderby _LateDispensationHd.FileNmbr ascending
        //                        select new
        //                        {
        //                            TransNmbr = _LateDispensationHd.TransNmbr,
        //                            FileNmbr = _LateDispensationHd.FileNmbr
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new HRMTrLateDispensationHd(_row.TransNmbr, _row.FileNmbr));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}
        #endregion

        #region HRMTrLateDispensationDt
        public double RowsCountHRMTrLateDispensationDt(String _prmCode)
        {
            double _result = 0;

            var _query =
                        (
                           from _LateDispensationDt in this.db.HRMTrLateDispensationDts
                           where _LateDispensationDt.TransNmbr == _prmCode
                           select _LateDispensationDt.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrLateDispensationDt> GetListHRMTrLateDispensationDt(int _prmReqPage, int _prmPageSize, String _prmCode)
        {
            List<HRMTrLateDispensationDt> _result = new List<HRMTrLateDispensationDt>();

            try
            {
                var _query = (
                                from _LateDispensationDt in this.db.HRMTrLateDispensationDts
                                where _LateDispensationDt.TransNmbr == _prmCode
                                orderby _LateDispensationDt.EmpNumb
                                select new
                                {
                                    TransNmbr = _LateDispensationDt.TransNmbr,
                                    EmpNumb = _LateDispensationDt.EmpNumb,
                                    EmpName = (
                                                    from _msEmp in this.db.MsEmployees
                                                    where _LateDispensationDt.EmpNumb == _msEmp.EmpNumb
                                                    select _msEmp.EmpName
                                                ).FirstOrDefault(),
                                    ReasonCode = _LateDispensationDt.ReasonCode,
                                    Description = (
                                                    from _msReason in this.db.HRMMsReasons
                                                    where _LateDispensationDt.ReasonCode == _msReason.ReasonCode
                                                    select _msReason.ReasonName
                                                  ).FirstOrDefault(),
                                    Remark = _LateDispensationDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrLateDispensationDt(_row.TransNmbr, _row.EmpNumb, _row.EmpName, _row.ReasonCode, _row.Description, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrLateDispensationDt GetSingleHRMTrLateDispensationDt(String _prmCode, String _prmEmpNumb)
        {
            HRMTrLateDispensationDt _result = null;

            try
            {
                _result = this.db.HRMTrLateDispensationDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.EmpNumb == _prmEmpNumb);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMTrLateDispensationDt(String[] _prmEmpNumb)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpNumb.Length; i++)
                {
                    String[] _tempSplit = _prmEmpNumb[i].Split('=');
                    HRMTrLateDispensationDt _LateDispensationDt = this.db.HRMTrLateDispensationDts.Single(_temp => _temp.EmpNumb == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr == _tempSplit[0]);

                    if (_LateDispensationDt != null)
                    {
                        this.db.HRMTrLateDispensationDts.DeleteOnSubmit(_LateDispensationDt);
                    }
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

        public bool AddHRMTrLateDispensationDt(HRMTrLateDispensationDt _prmHRMTrLateDispensationDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrLateDispensationDts.InsertOnSubmit(_prmHRMTrLateDispensationDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMTrLateDispensationDt(HRMTrLateDispensationDt _prmHRMTrLateDispensationDt)
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

        ~LateDispensationBL()
        {
        }
    }
}
