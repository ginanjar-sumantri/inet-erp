using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class FAServiceBL : Base
    {
        public FAServiceBL()
        {
        }

        #region FAServiceHd

        public double RowsCountFAServiceHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "FACode")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "FAName")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                            from _glFAServiceHd in this.db.GLFAServiceHds
                            join _msFixedAsset in this.db.MsFixedAssets
                                on _glFAServiceHd.FACode equals _msFixedAsset.FACode
                            where (SqlMethods.Like(_glFAServiceHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like((_glFAServiceHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like(_glFAServiceHd.FACode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && (SqlMethods.Like(_msFixedAsset.FAName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                               && _glFAServiceHd.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted)
                            select _glFAServiceHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<GLFAServiceHd> GetListFAServiceHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<GLFAServiceHd> _result = new List<GLFAServiceHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "FACode")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "FAName")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query =
                            (
                                from _glFAServiceHd in this.db.GLFAServiceHds
                                join _msFixedAsset in this.db.MsFixedAssets
                                    on _glFAServiceHd.FACode equals _msFixedAsset.FACode
                                where (SqlMethods.Like(_glFAServiceHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_glFAServiceHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_glFAServiceHd.FACode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && (SqlMethods.Like(_msFixedAsset.FAName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                   && _glFAServiceHd.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _glFAServiceHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _glFAServiceHd.TransNmbr,
                                    FileNmbr = _glFAServiceHd.FileNmbr,
                                    TransDate = _glFAServiceHd.TransDate,
                                    Status = _glFAServiceHd.Status,
                                    FACode = _glFAServiceHd.FACode,
                                    FAName = _msFixedAsset.FAName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new GLFAServiceHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.FACode, _row.FAName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFAServiceHd GetSingleFAServiceHd(string _prmTransNmbr)
        {
            GLFAServiceHd _result = null;

            try
            {
                _result = this.db.GLFAServiceHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleFAServiceHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAServiceHd _glFAServiceHd = this.db.GLFAServiceHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFAServiceHd != null)
                    {
                        if (_glFAServiceHd.Status != TransactionDataMapper.GetStatus(TransStatus.Posted))
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

        public string AddFAServiceHd(GLFAServiceHd _prmGLFAServiceHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmGLFAServiceHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.GLFAServiceHds.InsertOnSubmit(_prmGLFAServiceHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmGLFAServiceHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFAServiceHd(GLFAServiceHd _prmGLFAServiceHd)
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

        public bool DeleteMultiFAServiceHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAServiceHd _glFAServiceHd = this.db.GLFAServiceHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFAServiceHd != null)
                    {
                        if ((_glFAServiceHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLFAServiceDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.GLFAServiceDts.DeleteAllOnSubmit(_query);

                            this.db.GLFAServiceHds.DeleteOnSubmit(_glFAServiceHd);

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

        public bool DeleteMultiApproveFAServiceHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAServiceHd _glFAServiceHd = this.db.GLFAServiceHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFAServiceHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _glFAServiceHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _glFAServiceHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_glFAServiceHd != null)
                    {
                        if ((_glFAServiceHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLFAServiceDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.GLFAServiceDts.DeleteAllOnSubmit(_query);

                            this.db.GLFAServiceHds.DeleteOnSubmit(_glFAServiceHd);

                            _result = true;
                        }
                        else if (_glFAServiceHd.FileNmbr != "" && _glFAServiceHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _glFAServiceHd.Status = TransactionDataMapper.GetStatus(TransStatus.Deleted);
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

        public string GetApprove(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_GLFAServiceGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.FAService);
                    _transActivity.TransNmbr = _prmTransNmbr;
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

        public string Approve(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_GLFAServiceApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        GLFAServiceHd _glFAServiceHd = this.GetSingleFAServiceHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_glFAServiceHd.TransDate.Year, _glFAServiceHd.TransDate.Month, AppModule.GetValue(TransactionType.FAService), this._companyTag, ""))
                        {
                            _glFAServiceHd.FileNmbr = _item.Number;
                        }

                        
                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FAService);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFAServiceHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

                        _result = "Approve Success";
                        _scope.Complete();
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

        public string Posting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                GLFAServiceHd _glFAServiceHd = this.db.GLFAServiceHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFAServiceHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFAServicePost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FAService);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFAServiceHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Unposting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                GLFAServiceHd _glFAServiceHd = this.db.GLFAServiceHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFAServiceHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFAServiceUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.FAService);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFAServiceHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFAServiceHd(_prmTransNmbr).TransDate;
                        //_transActivity.Reason = "";

                        //this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        //this.db.SubmitChanges();
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public char GetStatusFAServiceHd(string _prmTransNmbr)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _glFAServiceHd in this.db.GLFAServiceHds
                                where _glFAServiceHd.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()
                                select new
                                {
                                    Status = _glFAServiceHd.Status
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.Status;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region FAServiceDt

        public int RowsCountFAServiceDt(string _prmTransNmbr)
        {
            int _result = 0;

            _result = this.db.GLFAServiceDts.Where(_row => _row.TransNmbr == _prmTransNmbr).Count();

            return _result;
        }

        public List<GLFAServiceDt> GetListFAServiceDt(int _prmReqPage, int _prmPageSize, string _prmTransNmbr)
        {
            List<GLFAServiceDt> _result = new List<GLFAServiceDt>();

            try
            {
                var _query =
                            (
                                from _glFAServiceDt in this.db.GLFAServiceDts
                                where _glFAServiceDt.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()
                                select new
                                {
                                    TransNmbr = _glFAServiceDt.TransNmbr,
                                    ItemNo = _glFAServiceDt.ItemNo,
                                    FAMaintenance = _glFAServiceDt.FAMaintenance,
                                    FAMaintenanceName = (
                                                            from _msFAMaintenance in this.db.MsFAMaintenances
                                                            where _msFAMaintenance.FAMaintenanceCode.Trim().ToLower() == _glFAServiceDt.FAMaintenance.Trim().ToLower()
                                                            select _msFAMaintenance.FAMaintenanceName
                                                        ).FirstOrDefault(),
                                    FgAddValue = _glFAServiceDt.FgAddValue,
                                    Remark = _glFAServiceDt.Remark,
                                    Qty = _glFAServiceDt.Qty,
                                    Unit = _glFAServiceDt.Unit,
                                    PriceForex = _glFAServiceDt.PriceForex,
                                    AmountForex = _glFAServiceDt.AmountForex
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new GLFAServiceDt(_row.TransNmbr, _row.ItemNo, _row.FAMaintenance, _row.FAMaintenanceName, _row.FgAddValue, _row.Remark, _row.Qty, _row.Unit, _row.PriceForex, _row.AmountForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFAServiceDt GetSingleFAServiceDt(string _prmTransNmbr, string _prmItemNo)
        {
            GLFAServiceDt _result = null;

            try
            {
                _result = this.db.GLFAServiceDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _temp.ItemNo == Convert.ToInt32(_prmItemNo));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFAServiceDt(string[] _prmItemCode, string _prmTransNmbr)
        {
            bool _result = false;

            decimal _tempBaseForex = 0;
            decimal _tempPPNForex = 0;
            decimal _tempTotalForex = 0;

            try
            {
                for (int i = 0; i < _prmItemCode.Length; i++)
                {
                    GLFAServiceDt _glFAServiceDt = this.db.GLFAServiceDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _temp.ItemNo == Convert.ToInt32(_prmItemCode[i]));

                    GLFAServiceHd _glFAServiceHd = this.GetSingleFAServiceHd(_prmTransNmbr);

                    decimal _amount = (_glFAServiceDt.AmountForex == null) ? 0 : Convert.ToDecimal(_glFAServiceDt.AmountForex);

                    _tempBaseForex = Convert.ToDecimal(_glFAServiceHd.BaseForex) - _amount;
                    _tempPPNForex = (_tempBaseForex) * (Convert.ToDecimal(_glFAServiceHd.PPN) / 100);
                    _tempTotalForex = _tempBaseForex + _tempPPNForex;

                    _glFAServiceHd.BaseForex = _tempBaseForex;

                    if (_tempBaseForex > 0)
                    {
                        _glFAServiceHd.PPNForex = _tempPPNForex;
                        _glFAServiceHd.TotalForex = _tempTotalForex;
                    }
                    else
                    {
                        _glFAServiceHd.PPNForex = 0;
                        _glFAServiceHd.TotalForex = 0;
                    }
                    this.db.GLFAServiceDts.DeleteOnSubmit(_glFAServiceDt);
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

        public bool AddFAServiceDt(GLFAServiceDt _prmGLFAServiceDt)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;
            decimal _tempPPNForex = 0;
            try
            {
                GLFAServiceHd _glFAServiceHd = this.GetSingleFAServiceHd(_prmGLFAServiceDt.TransNmbr);

                decimal _tempAmountForex = (_prmGLFAServiceDt.AmountForex == null) ? 0 : Convert.ToDecimal(_prmGLFAServiceDt.AmountForex);
                _tempBaseForex = _glFAServiceHd.BaseForex = _glFAServiceHd.BaseForex + _tempAmountForex;
                _tempPPNForex = _glFAServiceHd.PPNForex = (_tempBaseForex) * _glFAServiceHd.PPN / 100;
                _glFAServiceHd.TotalForex = _tempBaseForex + _tempPPNForex;

                this.db.GLFAServiceDts.InsertOnSubmit(_prmGLFAServiceDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFAServiceDt(GLFAServiceDt _prmGLFAServiceDt, decimal _prmAmountForexOriginal)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;
            decimal _tempPPNForex = 0;

            try
            {
                GLFAServiceHd _glFAServiceHd = this.GetSingleFAServiceHd(_prmGLFAServiceDt.TransNmbr);

                _tempBaseForex = _glFAServiceHd.BaseForex - _prmAmountForexOriginal;

                decimal _tempAmountForex = (_prmGLFAServiceDt.AmountForex == null) ? 0 : Convert.ToDecimal(_prmGLFAServiceDt.AmountForex);
                _tempBaseForex = _glFAServiceHd.BaseForex = _tempBaseForex + _tempAmountForex;
                _tempPPNForex = _glFAServiceHd.PPNForex = _tempBaseForex * _glFAServiceHd.PPN / 100;
                _glFAServiceHd.TotalForex = _tempBaseForex + _tempPPNForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxNoItemFAServiceDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.GLFAServiceDts.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~FAServiceBL()
        {

        }
    }
}
