using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class StockServiceOutBL : Base
    {
        public StockServiceOutBL()
        {

        }
        private StockServiceInBL _stockServiceInBL = new StockServiceInBL();

        #region STCServiceOutHd
        public double RowsCountSTCServiceOutHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query =
                (
                    from _stcServiceOutHd in this.db.STCServiceOutHds
                    where (SqlMethods.Like(_stcServiceOutHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like((_stcServiceOutHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && _stcServiceOutHd.Status != StockServiceOutDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcServiceOutHd
                ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "RowsCountSTCServiceOutHd", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCServiceOutHd> GetListSTCServiceOutHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCServiceOutHd> _result = new List<STCServiceOutHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                //_pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                //_pattern1 = "%%";
            }
            else if (_prmCategory == "RRNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _stcServiceOutHd in this.db.STCServiceOutHds
                                where (SqlMethods.Like(_stcServiceOutHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_stcServiceOutHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like((_stcServiceOutHd.RRNo ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && _stcServiceOutHd.Status != StockServiceOutDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcServiceOutHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _stcServiceOutHd.TransNmbr,
                                    FileNmbr = _stcServiceOutHd.FileNmbr,
                                    Status = _stcServiceOutHd.Status,
                                    TransDate = _stcServiceOutHd.TransDate,
                                    RRNo = _stcServiceOutHd.RRNo,
                                    CustCode = _stcServiceOutHd.CustCode,
                                    CustName = (
                                                    from _msCust in this.db.MsCustomers
                                                    where _msCust.CustCode == _stcServiceOutHd.CustCode
                                                    select _msCust.CustName
                                                ).FirstOrDefault(),
                                    WrhsCode = _stcServiceOutHd.WrhsCode,
                                    WrhsName = (
                                                    from _msWrhs in this.db.MsWarehouses
                                                    where _msWrhs.WrhsCode == _stcServiceOutHd.WrhsCode
                                                    select _msWrhs.WrhsName
                                                ).FirstOrDefault(),
                                    WrhsFgSubLed = _stcServiceOutHd.WrhsFgSubLed,
                                    WrhsSubLed = _stcServiceOutHd.WrhsSubLed,
                                    IssuedBy = _stcServiceOutHd.IssuedBy,
                                    Remark = _stcServiceOutHd.Remark,
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCServiceOutHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.RRNo, _row.CustCode, _row.CustName, _row.WrhsCode, _row.WrhsName, _row.WrhsFgSubLed, _row.WrhsSubLed, _row.IssuedBy, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetListSTCServiceOutHd", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCServiceOutHd GetSingleSTCServiceOutHd(string _prmCode)
        {
            STCServiceOutHd _result = null;

            try
            {
                _result = this.db.STCServiceOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetSingleSTCServiceOutHd", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCServiceOutHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCServiceOutHd _stcServiceOutHd = this.db.STCServiceOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcServiceOutHd != null)
                    {
                        if (_stcServiceOutHd.Status != StockServiceOutDataMapper.GetStatus(TransStatus.Posted))
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
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetSingleSTCServiceOutHdApprove", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddSTCServiceOutHd(STCServiceOutHd _prmSTCServiceOutHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCServiceOutHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCServiceOutHds.InsertOnSubmit(_prmSTCServiceOutHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCServiceOutHd.TransNmbr;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "AddSTCServiceOutHd", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddSTCServiceOutHdDt(STCServiceOutHd _prmSTCServiceOutHd)
        {
            string _result = "";
            bool _resultdetail = false;
            bool _resultcek = true;
            bool _cekqty = true;
            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCServiceOutHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCServiceOutHds.InsertOnSubmit(_prmSTCServiceOutHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                List<STCServiceInDt> _sTCServiceInDt = this._stockServiceInBL.GetListSTCServiceInDt(0, 1000, _prmSTCServiceOutHd.RRNo);

                foreach (var _row in _sTCServiceInDt)
                {
                    _cekqty = false;
                    if (_row.Qty - _row.QtyOut != 0)
                    {
                        _cekqty = true;
                    }
                    else
                    {
                        _cekqty = false;
                    }
                    if (_cekqty == true)
                    {
                        STCServiceOutDt _sTCServiceOutDt = new STCServiceOutDt();
                        _sTCServiceOutDt.TransNmbr = _prmSTCServiceOutHd.TransNmbr;
                        _sTCServiceOutDt.ImeiNo = _row.ImeiNo;
                        _sTCServiceOutDt.ProductCode = _row.ProductCode;
                        _sTCServiceOutDt.LocationCode = _row.LocationCode;
                        _sTCServiceOutDt.Qty = _row.Qty;
                        _sTCServiceOutDt.Unit = _row.Unit;
                        _sTCServiceOutDt.Remark = _row.Remark;
                        _resultdetail = this.AddSTCServiceOutDt(_sTCServiceOutDt);
                        if (_resultdetail == false)
                        {
                            _resultcek = false;
                        }
                    }
                }

                if (_resultcek == true)
                {
                    this.db.SubmitChanges();
                }
                _result = _prmSTCServiceOutHd.TransNmbr;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "AddSTCServiceOutHd", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCServiceOutHd(STCServiceOutHd _prmSTCServiceOutHd)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "EditSTCServiceOutHd", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCServiceOutHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCServiceOutHd _stcServiceOutHd = this.db.STCServiceOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcServiceOutHd != null)
                    {
                        if ((_stcServiceOutHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCServiceOutDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCServiceOutDts.DeleteAllOnSubmit(_query);

                            this.db.STCServiceOutHds.DeleteOnSubmit(_stcServiceOutHd);

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
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiSTCServiceOutHd", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiApproveSTCServiceOutHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCServiceOutHd _stcServiceOutHd = this.db.STCServiceOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcServiceOutHd.Status == StockServiceOutDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcServiceOutHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcServiceOutHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcServiceOutHd != null)
                    {
                        if ((_stcServiceOutHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCServiceOutDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCServiceOutDts.DeleteAllOnSubmit(_query);

                            this.db.STCServiceOutHds.DeleteOnSubmit(_stcServiceOutHd);

                            _result = true;
                        }
                        else if (_stcServiceOutHd.FileNmbr != "" && _stcServiceOutHd.Status == StockServiceOutDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcServiceOutHd.Status = StockServiceOutDataMapper.GetStatus(TransStatus.Deleted);
                            _result = true;
                        }
                    }
                }

                if (_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiApproveSTCServiceOutHd", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetWarehouseCodeByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _stcServiceOutHd in this.db.STCServiceOutHds
                                where _stcServiceOutHd.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    WrhsCode = _stcServiceOutHd.WrhsCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.WrhsCode;
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetWarehouseCodeByCode", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_STServiceOutGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockServiceOut);
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
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetApproval", AppModule.GetValue(TransactionType.StockServiceOut));
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
                    this.db.S_STServiceOutAppr(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        STCServiceOutHd _stcServiceOutHd = this.GetSingleSTCServiceOutHd(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcServiceOutHd.TransDate.Year, _stcServiceOutHd.TransDate.Month, AppModule.GetValue(TransactionType.StockServiceOut), this._companyTag, ""))
                        {
                            _stcServiceOutHd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockServiceOut);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCServiceOutHd(_prmCode).FileNmbr;
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
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "Approve", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Posting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCServiceOutHd _stcServiceOutHd = this.db.STCServiceOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcServiceOutHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_STServiceOutPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockServiceOut);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCServiceOutHd(_prmCode).FileNmbr;
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
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "Posting", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string UnPosting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCServiceOutHd _stcServiceOutHd = this.db.STCServiceOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcServiceOutHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_STServiceOutUnPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockServiceOut);
                        //_transActivity.TransNmbr = _prmCode.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCServiceOutHd(_prmCode).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCServiceOutHd(_prmCode).TransDate;
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
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "UnPosting", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public List<STCServiceInHd> GetListSTCServiceInDtPosting(String _prmCustCode)
        {
            List<STCServiceInHd> _result = new List<STCServiceInHd>();

            try
            {
                var _query = (
                                from _stcServiceInHd in this.db.STCServiceInHds
                                join _stcServiceInDt in this.db.STCServiceInDts
                                on _stcServiceInHd.TransNmbr equals _stcServiceInDt.TransNmbr
                                where _stcServiceInHd.Status == StockServiceInDataMapper.GetStatus(TransStatus.Posted)
                                && _stcServiceInHd.CustCode == _prmCustCode
                                && _stcServiceInDt.Qty - _stcServiceInDt.QtyOut > 0
                                orderby _stcServiceInHd.TransNmbr descending
                                select new
                                {
                                    _stcServiceInHd.TransNmbr, //TransNmbr = _stcServiceInHd.TransNmbr,
                                    _stcServiceInHd.FileNmbr //FileNmbr = _stcServiceInHd.FileNmbr,
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCServiceInHd(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
            }
            return _result;
        }

        #endregion

        #region STCServiceOutDt

        public int RowsCountSTCServiceOutDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.STCServiceOutDts.Where(_row => _row.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()).Count();

            return _result;
        }

        public List<STCServiceOutDt> GetListSTCServiceOutDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCServiceOutDt> _result = new List<STCServiceOutDt>();

            try
            {
                var _query =
                            (
                                from _stcSTCServiceOutDt in this.db.STCServiceOutDts
                                where _stcSTCServiceOutDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _stcSTCServiceOutDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcSTCServiceOutDt.TransNmbr,
                                    ImeiNo = _stcSTCServiceOutDt.ImeiNo,
                                    ProductCode = _stcSTCServiceOutDt.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode == _stcSTCServiceOutDt.ProductCode
                                                    select _msProduct.ProductName
                                                ).FirstOrDefault(),
                                    LocationCode = _stcSTCServiceOutDt.LocationCode,
                                    LocationName = (
                                                    from _msLocation in this.db.MsWrhsLocations
                                                    where _msLocation.WLocationCode == _stcSTCServiceOutDt.LocationCode
                                                    select _msLocation.WLocationName
                                                ).FirstOrDefault(),
                                    Qty = _stcSTCServiceOutDt.Qty,
                                    Unit = _stcSTCServiceOutDt.Unit,
                                    Remark = _stcSTCServiceOutDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCServiceOutDt(_row.TransNmbr, _row.ImeiNo, _row.ProductCode, _row.ProductName, _row.LocationCode, _row.LocationName, _row.Qty, _row.Unit, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetListSTCServiceOutDt", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public STCServiceOutDt GetSingleSTCServiceOutDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        public STCServiceOutDt GetSingleSTCServiceOutDt(string _prmCode, string _prmProductCode, string _prmImeiNo)
        {
            STCServiceOutDt _result = null;

            try
            {
                //_result = this.db.STCServiceOutDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _prmLocationCode.Trim().ToLower());
                _result = this.db.STCServiceOutDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower() && _temp.ImeiNo.Trim().ToLower() == _prmImeiNo.Trim().ToLower());
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetSingleSTCServiceOutDt", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCServiceOutDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    //STCServiceOutDt _stcServiceOutDt = this.db.STCServiceOutDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());
                    STCServiceOutDt _stcServiceOutDt = this.db.STCServiceOutDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.ImeiNo.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    this.db.STCServiceOutDts.DeleteOnSubmit(_stcServiceOutDt);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiSTCServiceOutDt", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddSTCServiceOutDt(STCServiceOutDt _prmSTCServiceOutDt)
        {
            bool _result = false;
            try
            {
                this.db.STCServiceOutDts.InsertOnSubmit(_prmSTCServiceOutDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "AddSTCServiceOutDt", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCServiceOutDt(STCServiceOutDt _prmSTCServiceOutDt)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "EditSTCServiceOutDt", AppModule.GetValue(TransactionType.StockServiceOut));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool CekImeiSTCServiceOutDt(String _prmTransNmbr, String _prmImeiNo)
        {
            bool _result = true;
            try
            {
                List<STCServiceOutDt> _sTCServiceOutDt = new List<STCServiceOutDt>();
                _sTCServiceOutDt = this.GetListSTCServiceOutDt(0, 1000, _prmTransNmbr);
                foreach (var _row in _sTCServiceOutDt)
                {
                    if (_row.ImeiNo == _prmImeiNo)
                    {
                        _result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "EditSTCServiceInDt", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~StockServiceOutBL()
        {

        }
    }
}
