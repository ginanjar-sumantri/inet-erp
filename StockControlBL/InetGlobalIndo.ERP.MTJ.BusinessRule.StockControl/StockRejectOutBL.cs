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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class StockRejectOutBL : Base
    {
        public StockRejectOutBL()
        {
        }

        #region STCRejectOutHd

        public double RowsCountSTCRejectOutHd(string _prmCategory, string _prmKeyword)
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
                    from _stcRejectOutHd in this.db.STCRejectOutHds
                    where (SqlMethods.Like(_stcRejectOutHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    && (SqlMethods.Like((_stcRejectOutHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                    && _stcRejectOutHd.Status != StockRejectOutDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcRejectOutHd
                ).Count();

            _result = _query;

            return _result;
        }

        public List<STCRejectOutHd> GetListSTCRejectOutHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCRejectOutHd> _result = new List<STCRejectOutHd>();

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
                                from _stcRejectOutHd in this.db.STCRejectOutHds
                                where (SqlMethods.Like(_stcRejectOutHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_stcRejectOutHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _stcRejectOutHd.Status != StockRejectOutDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcRejectOutHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _stcRejectOutHd.TransNmbr,
                                    FileNmbr = _stcRejectOutHd.FileNmbr,
                                    TransDate = _stcRejectOutHd.TransDate,
                                    Status = _stcRejectOutHd.Status,
                                    SuppCode = _stcRejectOutHd.SuppCode,
                                    SuppName = (
                                                    from _supp in this.db.MsSuppliers
                                                    where _stcRejectOutHd.SuppCode == _supp.SuppCode
                                                    select _supp.SuppName
                                                ).FirstOrDefault(),
                                    WrhsCode = _stcRejectOutHd.WrhsCode,
                                    WrhsName = (
                                                    from _wrhs in this.db.MsWarehouses
                                                    where _stcRejectOutHd.WrhsCode == _wrhs.WrhsCode
                                                    select _wrhs.WrhsName
                                                ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCRejectOutHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.SuppCode, _row.SuppName, _row.WrhsCode, _row.WrhsName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCRejectOutHd> GetListRejNoRejectOutDDL()
        {
            List<STCRejectOutHd> _result = new List<STCRejectOutHd>();

            try
            {
                var _query = (
                                from _v_RejectOut in this.db.V_STRejectOutForRRs
                                orderby _v_RejectOut.Reject_No
                                select new
                                {
                                    Reject_No = _v_RejectOut.Reject_No,
                                    FileNmbr = _v_RejectOut.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCRejectOutHd(_row.Reject_No, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCRejectOutHd GetSingleSTCRejectOutHd(string _prmCode)
        {
            STCRejectOutHd _result = null;

            try
            {
                _result = this.db.STCRejectOutHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCRejectOutHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCRejectOutHd _stcRejectOutHd = this.db.STCRejectOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcRejectOutHd != null)
                    {
                        if (_stcRejectOutHd.Status != StockRejectOutDataMapper.GetStatus(TransStatus.Posted))
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

        public string GetSuppCodeByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _stcRejectOut in this.db.STCRejectOutHds
                                where _stcRejectOut.TransNmbr == _prmCode
                                select new
                                {
                                    SuppCode = _stcRejectOut.SuppCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.SuppCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public string GetReqNoByCode(string _prmCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        var _query = (
        //                        from _stcIssueFA in this.db.STCRejectOutHds
        //                        where _stcIssueFA.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
        //                        select new
        //                        {
        //                            ReqAssetNo = _stcIssueFA.ReqAssetNo
        //                        }
        //                      );

        //        foreach (var _obj in _query)
        //        {
        //            _result = _obj.ReqAssetNo;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool DeleteMultiSTCRejectOutHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCRejectOutHd _stcRejectOutHd = this.db.STCRejectOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcRejectOutHd != null)
                    {
                        if ((_stcRejectOutHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCRejectOutDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCRejectOutDts.DeleteAllOnSubmit(_query);

                            this.db.STCRejectOutHds.DeleteOnSubmit(_stcRejectOutHd);

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

        public bool DeleteMultiApproveSTCRejectOutHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCRejectOutHd _stcRejectOutHd = this.db.STCRejectOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcRejectOutHd.Status == StockRejectOutDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcRejectOutHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcRejectOutHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcRejectOutHd != null)
                    {
                        if ((_stcRejectOutHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCRejectOutDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCRejectOutDts.DeleteAllOnSubmit(_query);

                            this.db.STCRejectOutHds.DeleteOnSubmit(_stcRejectOutHd);

                            _result = true;
                        }
                        else if (_stcRejectOutHd.FileNmbr != "" && _stcRejectOutHd.Status == StockRejectOutDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcRejectOutHd.Status = StockRejectOutDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddSTCRejectOutHd(STCRejectOutHd _prmSTCRejectOutHd)
        {
            string _result = "";
            PurchaseReturBL _purchaseBL = new PurchaseReturBL();

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCRejectOutHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCRejectOutHds.InsertOnSubmit(_prmSTCRejectOutHd);

                List<PRCReturDt> _prcreturDt = _purchaseBL.GetListPRCReturDt(_prmSTCRejectOutHd.PurchaseRetur);
                foreach (var _item in _prcreturDt)
                {
                    decimal _qty = _item.Qty;
                    decimal _qtyClose = (_item.QtyClose == null ? 0 : Convert.ToDecimal(_item.QtyClose));
                    decimal _qtySj = (_item.QtySJ == null ? 0 : Convert.ToDecimal(_item.QtySJ));

                    if ((_qty - _qtyClose - _qtySj) > 0)
                    {
                        STCRejectOutDt _stcRejectOutDt = new STCRejectOutDt();

                        _stcRejectOutDt.TransNmbr = _prmSTCRejectOutHd.TransNmbr;
                        _stcRejectOutDt.ProductCode = _item.ProductCode;
                        _stcRejectOutDt.Qty = _qty - _qtyClose - _qtySj;
                        _stcRejectOutDt.Unit = _item.Unit;
                        _stcRejectOutDt.PriceCost = _item.Price;
                        _stcRejectOutDt.Remark = _item.Remark;

                        this.db.STCRejectOutDts.InsertOnSubmit(_stcRejectOutDt);
                    }
                }

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCRejectOutHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCRejectOutHd(STCRejectOutHd _prmSTCRejectOutHd)
        {
            bool _result = false;
            PurchaseReturBL _purchaseBL = new PurchaseReturBL();

            try
            {
                var _query = (from _detail in this.db.STCRejectOutDts
                              where _detail.TransNmbr.Trim().ToLower() == _prmSTCRejectOutHd.TransNmbr.Trim().ToLower()
                              select _detail);

                this.db.STCRejectOutDts.DeleteAllOnSubmit(_query);

                List<PRCReturDt> _prcreturDt = _purchaseBL.GetListPRCReturDt(_prmSTCRejectOutHd.PurchaseRetur);
                foreach (var _item in _prcreturDt)
                {
                    decimal _qty = _item.Qty;
                    decimal _qtyClose = (_item.QtyClose == null ? 0 : Convert.ToDecimal(_item.QtyClose));
                    decimal _qtySj = (_item.QtySJ == null ? 0 : Convert.ToDecimal(_item.QtySJ));

                    if ((_qty - _qtyClose - _qtySj) > 0)
                    {
                        STCRejectOutDt _stcRejectOutDt = new STCRejectOutDt();

                        _stcRejectOutDt.TransNmbr = _prmSTCRejectOutHd.TransNmbr;
                        _stcRejectOutDt.ProductCode = _item.ProductCode;
                        _stcRejectOutDt.Qty = _qty - _qtyClose - _qtySj;
                        _stcRejectOutDt.Unit = _item.Unit;
                        _stcRejectOutDt.PriceCost = _item.Price;
                        _stcRejectOutDt.Remark = _item.Remark;

                        this.db.STCRejectOutDts.InsertOnSubmit(_stcRejectOutDt);
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
                int _success = this.db.S_STRejectOutGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockRejectOut);
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
                    int _success = this.db.S_STRejectOutApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        STCRejectOutHd _stcRejectOutHd = this.GetSingleSTCRejectOutHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcRejectOutHd.TransDate.Year, _stcRejectOutHd.TransDate.Month, AppModule.GetValue(TransactionType.StockRejectOut), this._companyTag, ""))
                        {
                            _stcRejectOutHd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockRejectOut);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCRejectOutHd(_prmTransNmbr).FileNmbr;
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

                STCRejectOutHd _stcRejectOutHd = this.db.STCRejectOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcRejectOutHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STRejectOutPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockRejectOut);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCRejectOutHd(_prmTransNmbr).FileNmbr;
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

                STCRejectOutHd _stcRejectOutHd = this.db.STCRejectOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcRejectOutHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STRejectOutUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockRejectOut);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCRejectOutHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCRejectOutHd(_prmTransNmbr).TransDate;
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

        public string GetFileNmbrSTCRejectOutHd(string _prmTransNmbr)
        {
            string _result = "";

            try
            {
                _result = (this.db.STCRejectOutHds.Single(_temp => _temp.TransNmbr == _prmTransNmbr).FileNmbr ?? "").Trim();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public List<STCRejectOutHd> GetListSJnoForDDL()
        //{
        //    List<STCRejectOutHd> _result = new List<STCRejectOutHd>();
        //    try
        //    {
        //        var _query = (from _stcRejectOutHds in this.db.STCRejectOutHds
        //                      join _stcRejectOutDts in this.db.STCRejectOutDts
        //                      on _stcRejectOutHds.TransNmbr equals _stcRejectOutDts.TransNmbr
        //                      where _stcRejectOutDts.Qty != 0
        //                      && _stcRejectOutHds.Status == StockRejectOutDataMapper.GetStatus(TransStatus.Posted)
        //                      select new
        //                      {
        //                          SJno = _stcRejectOutHds.TransNmbr,
        //                          FileNmbr = _stcRejectOutHds.FileNmbr
        //                      }
        //                      ).Distinct();

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new STCRejectOutHd(_row.SJno, _row.FileNmbr));
        //            //_result.Add(_query);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }
        //    return _result;
        //}


        public List<V_RRForSupplierRetur> GetListSJnoForDDL(String _prmSupCode)
        {
            List<V_RRForSupplierRetur> _result = new List<V_RRForSupplierRetur>();
            try
            {
                var _query = (from _RRForSupplier in this.db.V_RRForSupplierReturs
                              where _RRForSupplier.SuppCode.Trim().ToLower() == _prmSupCode.Trim().ToLower()  
                              select new
                              {
                                  SJno = _RRForSupplier.PurchaseRetur,
                                  SuppCode = _RRForSupplier.SuppCode
                              }
                              ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new V_RRForSupplierRetur(_row.SJno, _row.SuppCode));
                    //_result.Add(_query);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }
        

        public List<STCRejectOutHd> GetListRejectOutForDDL(String _prmSupp)
        {
            List<STCRejectOutHd> _result = new List<STCRejectOutHd>();

            try
            {
                var _query = (
                                from _stcRejectOutHds in this.db.STCRejectOutHds
                                join _stcRejectOutDts in this.db.STCRejectOutDts
                                on _stcRejectOutHds.TransNmbr equals _stcRejectOutDts.TransNmbr
                                where _stcRejectOutHds.FgDeliveryBack == true
                                && _stcRejectOutHds.SuppCode == _prmSupp
                                && _stcRejectOutDts.Qty != 0
                                orderby _stcRejectOutHds.FileNmbr
                                select new
                                {
                                    SJno = _stcRejectOutHds.TransNmbr,
                                    FileNmbr = _stcRejectOutHds.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCRejectOutHd(_row.SJno, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region STCRejectOutDt
        public int RowsCountSTCRejectOutDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _STCRejectOutDt in this.db.STCRejectOutDts
                                 where _STCRejectOutDt.TransNmbr == _prmCode
                                 select _STCRejectOutDt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCRejectOutDt> GetListSTCRejectOutDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCRejectOutDt> _result = new List<STCRejectOutDt>();

            try
            {
                var _query = (
                                from _stcRejectOutDt in this.db.STCRejectOutDts
                                where _stcRejectOutDt.TransNmbr == _prmCode
                                orderby _stcRejectOutDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcRejectOutDt.TransNmbr,
                                    ProductCode = _stcRejectOutDt.ProductCode,
                                    LocationCode = _stcRejectOutDt.LocationCode,
                                    Qty = _stcRejectOutDt.Qty,
                                    Unit = _stcRejectOutDt.Unit,
                                    Remark = _stcRejectOutDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCRejectOutDt(_row.TransNmbr, _row.ProductCode, _row.LocationCode, _row.Qty, _row.Unit, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCRejectOutDt> GetListSTCRejectOutDt(string _prmCode)
        {
            List<STCRejectOutDt> _result = new List<STCRejectOutDt>();

            try
            {
                var _query = (
                                from _stcRejectOutDt in this.db.STCRejectOutDts
                                where _stcRejectOutDt.TransNmbr == _prmCode
                                orderby _stcRejectOutDt.ProductCode ascending
                                select _stcRejectOutDt                                
                            );

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public STCRejectOutDt GetSingleSTCRejectOutDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        //{
        //    STCRejectOutDt _result = null;

        //    try
        //    {
        //        _result = this.db.STCRejectOutDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode && _temp.LocationCode == _prmLocationCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public STCRejectOutDt GetSingleSTCRejectOutDt(string _prmCode, string _prmProductCode)
        {
            STCRejectOutDt _result = null;

            try
            {
                _result = this.db.STCRejectOutDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCRejectOutDt GetSingleSTCRejectOutDtExist(string _prmCode, string _prmProductCode)
        {
            STCRejectOutDt _result = null;

            try
            {
                _result = this.db.STCRejectOutDts.FirstOrDefault(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCRejectOutDt GetSingleSTCRejectOutDtForStcRejIn(string _prmCode, string _prmProductCode)
        {
            STCRejectOutDt _result = null;

            try
            {
                _result = this.db.STCRejectOutDts.FirstOrDefault(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode && _temp.Qty - _temp.QtyIn > 0);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCRejectOutDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    //STCRejectOutDt _STCRejectOutDt = this.db.STCRejectOutDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr == _prmTransNo.Trim().ToLower());
                    STCRejectOutDt _STCRejectOutDt = this.db.STCRejectOutDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.TransNmbr == _prmTransNo.Trim().ToLower());

                    this.db.STCRejectOutDts.DeleteOnSubmit(_STCRejectOutDt);
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

        public bool AddSTCRejectOutDt(STCRejectOutDt _prmSTCRejectOutDt)
        {
            bool _result = false;

            try
            {
                this.db.STCRejectOutDts.InsertOnSubmit(_prmSTCRejectOutDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCRejectOutDt(STCRejectOutDt _prmSTCRejectOutDt)
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

        ~StockRejectOutBL()
        {
        }
    }
}
