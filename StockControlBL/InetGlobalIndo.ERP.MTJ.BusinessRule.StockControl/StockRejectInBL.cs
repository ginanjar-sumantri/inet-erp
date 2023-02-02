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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class StockRejectInBL : Base
    {
        public StockRejectInBL()
        {
        }

        #region STCRejectInHd

        public double RowsCountSTCRejectInHd(string _prmCategory, string _prmKeyword)
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

            var _query =
                (
                    from _stcRejectInHd in this.db.STCRejectInHds
                    where (SqlMethods.Like(_stcRejectInHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    && (SqlMethods.Like((_stcRejectInHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                    && _stcRejectInHd.Status != StockRejectInDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcRejectInHd
                ).Count();

            _result = _query;

            return _result;
        }

        public List<STCRejectInHd> GetListSTCRejectInHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCRejectInHd> _result = new List<STCRejectInHd>();

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
                                from _stcRejectInHd in this.db.STCRejectInHds
                                where (SqlMethods.Like(_stcRejectInHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_stcRejectInHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _stcRejectInHd.Status != StockRejectInDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcRejectInHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _stcRejectInHd.TransNmbr,
                                    FileNmbr = _stcRejectInHd.FileNmbr,
                                    TransDate = _stcRejectInHd.TransDate,
                                    Status = _stcRejectInHd.Status,
                                    TransReff = new StockRejectOutBL().GetFileNmbrSTCRejectOutHd(_stcRejectInHd.TransReff),
                                    SuppCode = _stcRejectInHd.SuppCode,
                                    SuppName = (
                                                    from _supp in this.db.MsSuppliers
                                                    where _stcRejectInHd.SuppCode == _supp.SuppCode
                                                    select _supp.SuppName
                                                ).FirstOrDefault(),
                                    WrhsCode = _stcRejectInHd.WrhsCode,
                                    WrhsName = (
                                                    from _wrhs in this.db.MsWarehouses
                                                    where _stcRejectInHd.WrhsCode == _wrhs.WrhsCode
                                                    select _wrhs.WrhsName
                                                ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCRejectInHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.TransReff, _row.SuppCode, _row.SuppName, _row.WrhsCode, _row.WrhsName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCRejectInHd GetSingleSTCRejectInHd(string _prmCode)
        {
            STCRejectInHd _result = null;

            try
            {
                _result = this.db.STCRejectInHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCRejectInHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCRejectInHd _stcRejectInHd = this.db.STCRejectInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcRejectInHd != null)
                    {
                        if (_stcRejectInHd.Status != StockRejectInDataMapper.GetStatus(TransStatus.Posted))
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

        public decimal GetQtyV_STRejectOutForRRByRejectCode(string _prmRejectCode, string _prmProductCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _vSTRejectOutForRR in this.db.V_STRejectOutForRRs
                                where _vSTRejectOutForRR.Reject_No.Trim().ToLower() == _prmRejectCode.Trim().ToLower()
                                    && _vSTRejectOutForRR.ProductCode == _prmProductCode
                                select new
                                {
                                    Qty = _vSTRejectOutForRR.Qty
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = Convert.ToDecimal((_obj.Qty == null) ? 0 : _obj.Qty);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCRejectInHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCRejectInHd _stcRejectInHd = this.db.STCRejectInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcRejectInHd != null)
                    {
                        if ((_stcRejectInHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCRejectInDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCRejectInDts.DeleteAllOnSubmit(_query);

                            this.db.STCRejectInHds.DeleteOnSubmit(_stcRejectInHd);

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

        public bool DeleteMultiApproveSTCRejectInHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCRejectInHd _stcRejectInHd = this.db.STCRejectInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcRejectInHd.Status == StockRejectInDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcRejectInHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcRejectInHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcRejectInHd != null)
                    {
                        if ((_stcRejectInHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCRejectInDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCRejectInDts.DeleteAllOnSubmit(_query);

                            this.db.STCRejectInHds.DeleteOnSubmit(_stcRejectInHd);

                            _result = true;
                        }
                        else if (_stcRejectInHd.FileNmbr != "" && _stcRejectInHd.Status == StockRejectInDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcRejectInHd.Status = StockRejectInDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddSTCRejectInHd(STCRejectInHd _prmSTCRejectInHd)
        {
            string _result = "";
            StockRejectOutBL _stcRejectOut = new StockRejectOutBL();

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCRejectInHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCRejectInHds.InsertOnSubmit(_prmSTCRejectInHd);

                List<STCRejectOutDt> _listStcRejOut = _stcRejectOut.GetListSTCRejectOutDt(_prmSTCRejectInHd.TransReff);
                foreach (var _item in _listStcRejOut)
                {
                    decimal _qty = _item.Qty;
                    decimal _qtyIn = _item.QtyIn == null ? 0 : Convert.ToDecimal(_item.QtyIn);
                    decimal _totalQty = _qty - _qtyIn;

                    if(_totalQty > 0)
                    {
                        STCRejectInDt _stcrejectInDt = new STCRejectInDt();

                        _stcrejectInDt.TransNmbr = _prmSTCRejectInHd.TransNmbr;
                        _stcrejectInDt.ProductCode = _item.ProductCode;
                        _stcrejectInDt.LocationCode = _item.LocationCode;
                        _stcrejectInDt.Qty = _totalQty;
                        _stcrejectInDt.Unit = _item.Unit;
                        _stcrejectInDt.Remark = _item.Remark;
                        _stcrejectInDt.PriceCost = _item.PriceCost;

                        this.db.STCRejectInDts.InsertOnSubmit(_stcrejectInDt);
                    }
                }

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCRejectInHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCRejectInHd(STCRejectInHd _prmSTCRejectInHd)
        {
            bool _result = false;
            StockRejectOutBL _stcRejOutBL = new StockRejectOutBL();

            try
            {
                var _query = (from _detail in this.db.STCRejectInDts
                              where _detail.TransNmbr.Trim().ToLower() == _prmSTCRejectInHd.TransNmbr.Trim().ToLower()
                              select _detail);

                this.db.STCRejectInDts.DeleteAllOnSubmit(_query);

                List<STCRejectOutDt> _listStcRejOut = _stcRejOutBL.GetListSTCRejectOutDt(_prmSTCRejectInHd.TransReff);
                foreach (var _item in _listStcRejOut)
                {
                    decimal _qty = _item.Qty;
                    decimal _qtyIn = _item.QtyIn == null ? 0 : Convert.ToDecimal(_item.QtyIn);
                    decimal _totalQty = _qty - _qtyIn;

                    if (_totalQty > 0)
                    {
                        STCRejectInDt _stcrejectInDt = new STCRejectInDt();

                        _stcrejectInDt.TransNmbr = _prmSTCRejectInHd.TransNmbr;
                        _stcrejectInDt.ProductCode = _item.ProductCode;
                        _stcrejectInDt.LocationCode = _item.LocationCode;
                        _stcrejectInDt.Qty = _totalQty;
                        _stcrejectInDt.Unit = _item.Unit;
                        _stcrejectInDt.Remark = _item.Remark;
                        _stcrejectInDt.PriceCost = _item.PriceCost;

                        this.db.STCRejectInDts.InsertOnSubmit(_stcrejectInDt);
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

        public string GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_STRejectInGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockRejectIn);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
                else
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string Approve(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.S_STRejectInApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        STCRejectInHd _stcRejectInHd = this.GetSingleSTCRejectInHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcRejectInHd.TransDate.Year, _stcRejectInHd.TransDate.Month, AppModule.GetValue(TransactionType.StockRejectIn), this._companyTag, ""))
                        {
                            _stcRejectInHd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockRejectIn);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCRejectInHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

                        _result = "Approve Success";
                        _scope.Complete();
                    }
                    else
                    {
                        _result = _errorMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string Posting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCRejectInHd _stcRejectInHd = this.db.STCRejectInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcRejectInHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STRejectInPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockRejectIn);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCRejectInHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();
                    }
                    else
                    {
                        _result = _errorMsg;
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
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string Unposting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCRejectInHd _stcRejectInHd = this.db.STCRejectInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcRejectInHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STRejectInUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockRejectIn);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCRejectInHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCRejectInHd(_prmTransNmbr).TransDate;
                        //_transActivity.Reason = "";

                        //this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        //this.db.SubmitChanges();
                    }
                    else
                    {
                        _result = _errorMsg;
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "Unposting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        
        #endregion

        #region STCRejectInDt
        public int RowsCountSTCRejectInDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _STCRejectInDt in this.db.STCRejectInDts
                                 where _STCRejectInDt.TransNmbr == _prmCode
                                 select _STCRejectInDt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCRejectInDt> GetListSTCRejectInDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCRejectInDt> _result = new List<STCRejectInDt>();

            try
            {
                var _query = (
                                from _stcRejectInDt in this.db.STCRejectInDts
                                where _stcRejectInDt.TransNmbr == _prmCode
                                orderby _stcRejectInDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcRejectInDt.TransNmbr,
                                    ProductCode = _stcRejectInDt.ProductCode,
                                    LocationCode = _stcRejectInDt.LocationCode,
                                    Qty = _stcRejectInDt.Qty,
                                    Unit = _stcRejectInDt.Unit,
                                    Remark = _stcRejectInDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCRejectInDt(_row.TransNmbr, _row.ProductCode, _row.LocationCode, _row.Qty, _row.Unit, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCRejectInDt GetSingleSTCRejectInDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        {
            STCRejectInDt _result = null;

            try
            {
                _result = this.db.STCRejectInDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode && _temp.LocationCode == _prmLocationCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCRejectInDt GetSingleSTCRejectInDtExist(string _prmCode, string _prmProductCode)
        {
            STCRejectInDt _result = null;

            try
            {
                _result = this.db.STCRejectInDts.FirstOrDefault(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCRejectInDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    STCRejectInDt _STCRejectInDt = this.db.STCRejectInDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr == _prmTransNo.Trim().ToLower());

                    this.db.STCRejectInDts.DeleteOnSubmit(_STCRejectInDt);
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

        public bool AddSTCRejectInDt(STCRejectInDt _prmSTCRejectInDt)
        {
            bool _result = false;

            try
            {
                this.db.STCRejectInDts.InsertOnSubmit(_prmSTCRejectInDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCRejectInDt(STCRejectInDt _prmSTCRejectInDt)
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

        ~StockRejectInBL()
        {
        }
    }
}
