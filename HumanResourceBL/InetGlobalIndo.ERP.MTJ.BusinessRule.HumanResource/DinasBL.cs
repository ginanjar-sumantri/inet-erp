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
    public sealed class DinasBL : Base
    {
        public DinasBL()
        {
        }

        #region HRMTrDinasHd
        public double RowsCountHRMTrDinasHd(string _prmCategory, string _prmKeyword)
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
                           from _dinasHd in this.db.HRMTrDinasHds
                           where (SqlMethods.Like(_dinasHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like((_dinasHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                              && _dinasHd.Status != DinasDataMapper.GetStatus(TransStatus.Deleted)
                           select _dinasHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrDinasHd> GetListHRMTrDinasHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMTrDinasHd> _result = new List<HRMTrDinasHd>();

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
                                from _dinasHd in this.db.HRMTrDinasHds
                                where (SqlMethods.Like(_dinasHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like((_dinasHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && _dinasHd.Status != DinasDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _dinasHd.EditDate descending
                                select new
                                {
                                    TransNmbr = _dinasHd.TransNmbr,
                                    FileNmbr = _dinasHd.FileNmbr,
                                    TransDate = _dinasHd.TransDate,
                                    Status = _dinasHd.Status,
                                    Diketahui = _dinasHd.Diketahui,
                                    Remark = _dinasHd.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrDinasHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.Diketahui, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrDinasHd GetSingleHRMTrDinasHd(String _prmCode)
        {
            HRMTrDinasHd _result = null;

            try
            {
                _result = this.db.HRMTrDinasHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleHRMTrDinasHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrDinasHd _hRMTrDinasHd = this.db.HRMTrDinasHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrDinasHd != null)
                    {
                        if (_hRMTrDinasHd.Status != DinasDataMapper.GetStatus(TransStatus.Posted))
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

        public HRMTrDinasHd GetSingleHRMTrDinasHdView(String _prmCode)
        {
            HRMTrDinasHd _result = new HRMTrDinasHd();

            try
            {
                var _query = (
                               from _dinasHd in this.db.HRMTrDinasHds
                               orderby _dinasHd.EditDate descending
                               where _dinasHd.TransNmbr == _prmCode
                               select new
                               {
                                   TransNmbr = _dinasHd.TransNmbr,
                                   FileNmbr = _dinasHd.FileNmbr,
                                   TransDate = _dinasHd.TransDate,
                                   Status = _dinasHd.Status,
                                   Diketahui = _dinasHd.Diketahui,
                                   Remark = _dinasHd.Remark
                               }
                           ).Single();

                _result.TransNmbr = _query.TransNmbr;
                _result.FileNmbr = _query.FileNmbr;
                _result.TransDate = _query.TransDate;
                _result.Status = _query.Status;
                _result.Diketahui = _query.Diketahui;
                _result.Remark = _query.Remark;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMTrDinasHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrDinasHd _dinasHd = this.db.HRMTrDinasHds.Single(_temp => _temp.TransNmbr == _prmCode[i].ToString().ToLower());

                    if (_dinasHd != null)
                    {
                        if ((_dinasHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrDinasDts
                                          where _detail.TransNmbr == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.HRMTrDinasDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrDinasHds.DeleteOnSubmit(_dinasHd);

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

        public bool DeleteMultiApproveHRMTrDinasHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMTrDinasHd _hRMTrDinasHd = this.db.HRMTrDinasHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_hRMTrDinasHd.Status == DinasDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _hRMTrDinasHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _hRMTrDinasHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }


                    if (_hRMTrDinasHd != null)
                    {
                        if ((_hRMTrDinasHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.HRMTrDinasDts
                                          where _detail.TransNmbr.ToLower().Trim() == _prmCode[i].ToLower().Trim()
                                          select _detail);

                            this.db.HRMTrDinasDts.DeleteAllOnSubmit(_query);

                            this.db.HRMTrDinasHds.DeleteOnSubmit(_hRMTrDinasHd);

                            _result = true;
                        }
                        else if (_hRMTrDinasHd.FileNmbr != "" && _hRMTrDinasHd.Status == DinasDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _hRMTrDinasHd.Status = DinasDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddHRMTrDinasHd(HRMTrDinasHd _prmHRMTrDinasHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmHRMTrDinasHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRMTrDinasHds.InsertOnSubmit(_prmHRMTrDinasHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmHRMTrDinasHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMTrDinasHd(HRMTrDinasHd _prmHRMTrDinasHd)
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

        public string GetAppr(String _prmDinasCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TrDinasGetAppr(_prmDinasCode, HttpContext.Current.User.Identity.Name, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMTrDinas);
                    _transActivity.TransNmbr = _prmDinasCode.ToString();
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
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(String _prmDinasCode)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spHRM_TrDinasApprove(_prmDinasCode, HttpContext.Current.User.Identity.Name, ref _result);

                    if (_result == "")
                    {
                        HRMTrDinasHd _dinasHd = this.GetSingleHRMTrDinasHd(_prmDinasCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_dinasHd.TransDate.Year, _dinasHd.TransDate.Month, AppModule.GetValue(TransactionType.HRMTrDinas), this._companyTag, ""))
                        {
                            _dinasHd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMTrDinas);
                        _transActivity.TransNmbr = _prmDinasCode.ToString();
                        _transActivity.FileNmbr = _dinasHd.FileNmbr;
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
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Posting(String _prmDinasCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TrDinasPosting(_prmDinasCode, HttpContext.Current.User.Identity.Name, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMTrDinas);
                    _transActivity.TransNmbr = _prmDinasCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleHRMTrDinasHd(_prmDinasCode).FileNmbr;
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

        public string Unposting(String _prmDinasCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TrDinasUnPost(_prmDinasCode, HttpContext.Current.User.Identity.Name, ref _result);

                if (_result == "")
                {
                    _result = "Unposting Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Unposting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        //public List<HRMTrDinasHd> GetListDDLDinas()
        //{
        //    List<HRMTrDinasHd> _result = new List<HRMTrDinasHd>();

        //    try
        //    {
        //        var _query = (
        //                        from _dinasHd in this.db.HRMTrDinasHds
        //                        where _dinasHd.Status == DinasDataMapper.GetStatus(DinasStatus.Approved)
        //                        orderby _dinasHd.FileNmbr ascending
        //                        select new
        //                        {
        //                            TransNmbr = _dinasHd.TransNmbr,
        //                            FileNmbr = _dinasHd.FileNmbr
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new HRMTrDinasHd(_row.TransNmbr, _row.FileNmbr));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}
        #endregion

        #region HRMTrDinasDt
        public double RowsCountHRMTrDinasDt(String _prmCode)
        {
            double _result = 0;

            var _query =
                        (
                           from _dinasDt in this.db.HRMTrDinasDts
                           where _dinasDt.TransNmbr == _prmCode
                           select _dinasDt.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMTrDinasDt> GetListHRMTrDinasDt(int _prmReqPage, int _prmPageSize, String _prmCode)
        {
            List<HRMTrDinasDt> _result = new List<HRMTrDinasDt>();

            try
            {
                var _query = (
                                from _dinasDt in this.db.HRMTrDinasDts
                                where _dinasDt.TransNmbr == _prmCode
                                orderby _dinasDt.StartDate
                                select new
                                {
                                    TransNmbr = _dinasDt.TransNmbr,
                                    EmpNumb = _dinasDt.EmpNumb,
                                    EmpName = (
                                                    from _msEmp in this.db.MsEmployees
                                                    where _dinasDt.EmpNumb == _msEmp.EmpNumb
                                                    select _msEmp.EmpName
                                                ).FirstOrDefault(),
                                    StartDate = _dinasDt.StartDate,
                                    EndDate = _dinasDt.EndDate,
                                    Place = _dinasDt.Place,
                                    Purpose = _dinasDt.Purpose,
                                    Remark = _dinasDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMTrDinasDt(_row.TransNmbr, _row.EmpNumb, _row.EmpName, _row.StartDate, _row.EndDate, _row.Place, _row.Purpose, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMTrDinasDt GetSingleHRMTrDinasDt(String _prmCode, String _prmEmpNumb)
        {
            HRMTrDinasDt _result = null;

            try
            {
                _result = this.db.HRMTrDinasDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.EmpNumb == _prmEmpNumb);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMTrDinasDt(String[] _prmEmpNumb)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpNumb.Length; i++)
                {
                    String[] _tempSplit = _prmEmpNumb[i].Split('=');
                    HRMTrDinasDt _dinasDt = this.db.HRMTrDinasDts.Single(_temp => _temp.EmpNumb == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr == _tempSplit[0]);

                    if (_dinasDt != null)
                    {
                        this.db.HRMTrDinasDts.DeleteOnSubmit(_dinasDt);
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

        public bool AddHRMTrDinasDt(HRMTrDinasDt _prmHRMTrDinasDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMTrDinasDts.InsertOnSubmit(_prmHRMTrDinasDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMTrDinasDt(HRMTrDinasDt _prmHRMTrDinasDt)
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

        ~DinasBL()
        {
        }
    }
}
