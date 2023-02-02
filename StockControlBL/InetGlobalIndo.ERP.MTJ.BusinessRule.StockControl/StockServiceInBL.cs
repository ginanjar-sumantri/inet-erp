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
    public sealed class StockServiceInBL : Base
    {
        public StockServiceInBL()
        {

        }

        #region STCServiceInHd
        public double RowsCountSTCServiceInHd(string _prmCategory, string _prmKeyword)
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
                    from _stcServiceInHd in this.db.STCServiceInHds
                    where (SqlMethods.Like(_stcServiceInHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like((_stcServiceInHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && _stcServiceInHd.Status != StockServiceInDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcServiceInHd
                ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "RowsCountSTCServiceInHd", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCServiceInHd> GetListSTCServiceInHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCServiceInHd> _result = new List<STCServiceInHd>();

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
                var _query = (
                                from _stcServiceInHd in this.db.STCServiceInHds
                                where (SqlMethods.Like(_stcServiceInHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_stcServiceInHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _stcServiceInHd.Status != StockServiceInDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcServiceInHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _stcServiceInHd.TransNmbr,
                                    FileNmbr = _stcServiceInHd.FileNmbr,
                                    TransDate = _stcServiceInHd.TransDate,
                                    Status = _stcServiceInHd.Status,
                                    CustCode = _stcServiceInHd.CustCode,
                                    CustName = (
                                                    from _msCust in this.db.MsCustomers
                                                    where _msCust.CustCode == _stcServiceInHd.CustCode
                                                    select _msCust.CustName
                                                ).FirstOrDefault(),
                                    WrhsCode = _stcServiceInHd.WrhsCode,
                                    WrhsName = (
                                                    from _msWrhs in this.db.MsWarehouses
                                                    where _msWrhs.WrhsCode == _stcServiceInHd.WrhsCode
                                                    select _msWrhs.WrhsName
                                                ).FirstOrDefault(),
                                    WrhsFgSubLed = _stcServiceInHd.WrhsFgSubLed,
                                    WrhsSubLed = _stcServiceInHd.WrhsSubLed,
                                    ReceivedBy = _stcServiceInHd.ReceivedBy,
                                    Remark = _stcServiceInHd.Remark,
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCServiceInHd(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.CustCode, _row.CustName, _row.WrhsCode, _row.WrhsName, _row.WrhsFgSubLed, _row.WrhsSubLed, _row.ReceivedBy, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetListSTCServiceInHd", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCServiceInHd GetSingleSTCServiceInHd(string _prmCode)
        {
            STCServiceInHd _result = null;

            try
            {
                _result = this.db.STCServiceInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetSingleSTCServiceInHd", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCServiceInHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCServiceInHd _stcServiceInHd = this.db.STCServiceInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcServiceInHd != null)
                    {
                        if (_stcServiceInHd.Status != StockServiceInDataMapper.GetStatus(TransStatus.Posted))
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
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetSingleSTCServiceInHdApprove", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddSTCServiceInHd(STCServiceInHd _prmSTCServiceInHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCServiceInHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCServiceInHds.InsertOnSubmit(_prmSTCServiceInHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCServiceInHd.TransNmbr;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "AddSTCServiceInHd", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCServiceInHd(STCServiceInHd _prmSTCServiceInHd)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "EditSTCServiceInHd", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCServiceInHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCServiceInHd _stcServiceInHd = this.db.STCServiceInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcServiceInHd != null)
                    {
                        if ((_stcServiceInHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCServiceInDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCServiceInDts.DeleteAllOnSubmit(_query);

                            this.db.STCServiceInHds.DeleteOnSubmit(_stcServiceInHd);

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
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiSTCServiceInHd", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiApproveSTCServiceInHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCServiceInHd _stcServiceInHd = this.db.STCServiceInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcServiceInHd.Status == StockServiceInDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcServiceInHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcServiceInHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcServiceInHd != null)
                    {
                        if ((_stcServiceInHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCServiceInDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCServiceInDts.DeleteAllOnSubmit(_query);

                            this.db.STCServiceInHds.DeleteOnSubmit(_stcServiceInHd);

                            _result = true;
                        }
                        else if (_stcServiceInHd.FileNmbr != "" && _stcServiceInHd.Status == StockServiceInDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcServiceInHd.Status = StockServiceInDataMapper.GetStatus(TransStatus.Deleted);
                            _result = true;
                        }
                    }
                }

                if (_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiApproveSTCServiceInHd", AppModule.GetValue(TransactionType.StockServiceIn));
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
                                from _stcServiceInHd in this.db.STCServiceInHds
                                where _stcServiceInHd.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    WrhsCode = _stcServiceInHd.WrhsCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.WrhsCode;
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetWarehouseCodeByCode", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_STServiceInGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockServiceIn);
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
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetApproval", AppModule.GetValue(TransactionType.StockServiceIn));
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
                    this.db.S_STServiceInApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        STCServiceInHd _stcServiceInHd = this.GetSingleSTCServiceInHd(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcServiceInHd.TransDate.Year, _stcServiceInHd.TransDate.Month, AppModule.GetValue(TransactionType.StockServiceIn), this._companyTag, ""))
                        {
                            _stcServiceInHd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockServiceIn);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCServiceInHd(_prmCode).FileNmbr;
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
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "Approve", AppModule.GetValue(TransactionType.StockServiceIn));
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

                STCServiceInHd _stcServiceInHd = this.db.STCServiceInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcServiceInHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_STServiceInPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockServiceIn);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCServiceInHd(_prmCode).FileNmbr;
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
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "Posting", AppModule.GetValue(TransactionType.StockServiceIn));
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

                STCServiceInHd _stcServiceInHd = this.db.STCServiceInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcServiceInHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_STServiceInUnPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockServiceIn);
                        //_transActivity.TransNmbr = _prmCode.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCServiceInHd(_prmCode).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCServiceInHd(_prmCode).TransDate;
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
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "UnPosting", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        #endregion

        #region STCServiceInDt

        public int RowsCountSTCServiceInDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.STCServiceInDts.Where(_row => _row.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()).Count();

            return _result;
        }

        public List<STCServiceInDt> GetListSTCServiceInDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCServiceInDt> _result = new List<STCServiceInDt>();

            try
            {
                var _query =
                            (
                                from _stcSTCServiceInDt in this.db.STCServiceInDts
                                where _stcSTCServiceInDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _stcSTCServiceInDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcSTCServiceInDt.TransNmbr,
                                    ImeiNo = _stcSTCServiceInDt.ImeiNo,
                                    ProductCode = _stcSTCServiceInDt.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode == _stcSTCServiceInDt.ProductCode
                                                    select _msProduct.ProductName
                                                ).FirstOrDefault(),
                                    LocationCode = _stcSTCServiceInDt.LocationCode,
                                    LocationName = (
                                                    from _msLocation in this.db.MsWrhsLocations
                                                    where _msLocation.WLocationCode == _stcSTCServiceInDt.LocationCode
                                                    select _msLocation.WLocationName
                                                ).FirstOrDefault(),
                                    Qty = _stcSTCServiceInDt.Qty,
                                    Unit = _stcSTCServiceInDt.Unit,
                                    EstReturnDate = _stcSTCServiceInDt.EstReturnDate,
                                    FgGaransi = _stcSTCServiceInDt.FgGaransi,
                                    FgSegelOK = _stcSTCServiceInDt.FgSegelOK,
                                    Remark = _stcSTCServiceInDt.Remark,
                                    QtyOut = _stcSTCServiceInDt.QtyOut
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCServiceInDt(_row.TransNmbr, _row.ImeiNo, _row.ProductCode, _row.ProductName, _row.LocationCode, _row.LocationName, _row.Qty, _row.Unit, _row.EstReturnDate, _row.FgGaransi, _row.FgSegelOK, _row.Remark, _row.QtyOut));
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetListSTCServiceInDt", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public STCServiceInDt GetSingleSTCServiceInDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        public STCServiceInDt GetSingleSTCServiceInDt(string _prmCode, string _prmProductCode, string _prmImeiNo)
        {
            STCServiceInDt _result = null;

            try
            {
                //_result = this.db.STCServiceInDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _prmLocationCode.Trim().ToLower());
                _result = this.db.STCServiceInDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower() && _temp.ImeiNo.Trim().ToLower() == _prmImeiNo.Trim().ToLower());
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetSingleSTCServiceInDt", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCServiceInDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    //STCServiceInDt _stcServiceInDt = this.db.STCServiceInDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());
                    STCServiceInDt _stcServiceInDt = this.db.STCServiceInDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.ImeiNo.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    this.db.STCServiceInDts.DeleteOnSubmit(_stcServiceInDt);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteMultiSTCServiceInDt", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddSTCServiceInDt(STCServiceInDt _prmSTCServiceInDt)
        {
            bool _result = false;
            try
            {
                this.db.STCServiceInDts.InsertOnSubmit(_prmSTCServiceInDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "AddSTCServiceInDt", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCServiceInDt(STCServiceInDt _prmSTCServiceInDt)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "EditSTCServiceInDt", AppModule.GetValue(TransactionType.StockServiceIn));
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool CekImeiSTCServiceInDt(String _prmTransNmbr, String _prmImeiNo)
        {
            bool _result = true;
            try
            {
                List<STCServiceInDt> _sTCServiceInDt = new List<STCServiceInDt>();
                _sTCServiceInDt = this.GetListSTCServiceInDt(0, 1000, _prmTransNmbr);
                foreach (var _row in _sTCServiceInDt)
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

        ~StockServiceInBL()
        {

        }
    }
}
