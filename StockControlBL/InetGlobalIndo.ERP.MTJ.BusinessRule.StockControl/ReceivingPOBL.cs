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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Web;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class ReceivingPOBL : Base
    {
        public ReceivingPOBL()
        {
        }

        #region STCReceiveHd
        public double RowsCountSTCReceiveHd(string _prmCategory, string _prmKeyword)
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
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query =
                            (
                                from _stcReceiveHd in this.db.STCReceiveHds
                                join _msSupplier in this.db.MsSuppliers
                                    on _stcReceiveHd.SuppCode equals _msSupplier.SuppCode
                                where (SqlMethods.Like(_stcReceiveHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_stcReceiveHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _stcReceiveHd.Status != ReceivingPODataMapper.GetStatus(TransStatus.Deleted)
                                select _stcReceiveHd.TransNmbr
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "RowsCountSTCReceiveHd", "STC");
            }

            return _result;
        }

        public List<STCReceiveHd> GetListSTCReceiveHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<STCReceiveHd> _result = new List<STCReceiveHd>();

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
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _stcReceiveHd in this.db.STCReceiveHds
                                join _msSupplier in this.db.MsSuppliers
                                on _stcReceiveHd.SuppCode equals _msSupplier.SuppCode
                                where (SqlMethods.Like(_stcReceiveHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_stcReceiveHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && _stcReceiveHd.Status != ReceivingPODataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcReceiveHd.TransDate descending
                                select new
                                {
                                    TransNmbr = _stcReceiveHd.TransNmbr,
                                    FileNmbr = _stcReceiveHd.FileNmbr,
                                    TransDate = _stcReceiveHd.TransDate,
                                    Status = _stcReceiveHd.Status,
                                    SuppCode = _stcReceiveHd.SuppCode,
                                    SuppName = _msSupplier.SuppName,
                                    PONo = _stcReceiveHd.PONo,
                                    FileNo = this.GetFileNmbrPRCPOHd(_stcReceiveHd.PONo, 0),
                                    WrhsCode = _stcReceiveHd.WrhsCode,
                                    WrhsSubLed = _stcReceiveHd.WrhsSubLed,
                                    WrhsFgSubLed = _stcReceiveHd.WrhsFgSubLed
                                }
                            );

                if (_prmOrderBy == "Trans No.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.TransNmbr)) : (_query.OrderByDescending(a => a.TransNmbr));

                if (_prmOrderBy == "File No.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.FileNmbr)) : (_query.OrderByDescending(a => a.FileNmbr));

                if (_prmOrderBy == "Trans Date")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.TransDate)) : (_query.OrderByDescending(a => a.TransDate));

                if (_prmOrderBy == "Status")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.Status)) : (_query.OrderByDescending(a => a.Status));

                if (_prmOrderBy == "Supplier")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.SuppName)) : (_query.OrderByDescending(a => a.SuppName));

                if (_prmOrderBy == "PO No.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.PONo)) : (_query.OrderByDescending(a => a.PONo));

                _query = _query.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCReceiveHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.SuppCode, _row.SuppName, _row.PONo, _row.FileNo, _row.WrhsCode, _row.WrhsSubLed, _row.WrhsFgSubLed));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetListSTCReceiveHd", "STC");
            }

            return _result;
        }

        public string GetFileNmbrSTCReceiveHd(string _prmCode)
        {
            string _result = null;

            try
            {
                _result = this.db.STCReceiveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()).FileNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetFileNmbrSTCReceiveHd", "STC");
            }

            return _result;
        }

        public string GetFileNmbrPRCPOHd(string _prmCode, int _prmRevisi)
        {
            string _result = null;

            try
            {
                _result = (this.db.PRCPOHds.Single(_temp => (_temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()) && (_temp.Revisi == _prmRevisi)).FileNmbr ?? "").Trim();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetFileNmbrPRCPOHd", "STC");
            }

            return _result;
        }

        public string GetPONoSTCReceiveHd(string _prmCode)
        {
            string _result = null;

            try
            {
                _result = (
                            from _stcReceive in this.db.STCReceiveHds
                            where _stcReceive.TransNmbr == _prmCode
                            select _stcReceive.PONo
                          ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetPONoSTCReceiveHd", "STC");
            }

            return _result;
        }

        //public string GetWarehouseByCode(string _prmCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        _result = (
        //                    from _stcReceive in this.db.STCReceiveHds
        //                    where _stcReceive.TransNmbr == _prmCode
        //                    select _stcReceive.WrhsCode
        //                  ).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public string GetWarehouseSubledByCode(string _prmCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        _result = (
        //                    from _stcReceive in this.db.STCReceiveHds
        //                    where _stcReceive.TransNmbr == _prmCode
        //                    select _stcReceive.WrhsSubLed
        //                  ).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public char GetWarehouseFgSubledByCode(string _prmCode)
        //{
        //    char _result = ' ';

        //    try
        //    {
        //        _result = (
        //                    from _stcReceive in this.db.STCReceiveHds
        //                    where _stcReceive.TransNmbr == _prmCode
        //                    select _stcReceive.WrhsFgSubLed
        //                  ).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public STCReceiveHd GetSingleSTCReceiveHd(string _prmCode)
        {
            STCReceiveHd _result = null;

            try
            {
                _result = this.db.STCReceiveHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetSingleSTCReceiveHd", "STC");
            }

            return _result;
        }

        public bool GetSingleSTCReceiveHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCReceiveHd _stcReceiveHd = this.db.STCReceiveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcReceiveHd != null)
                    {
                        if (_stcReceiveHd.Status != ReceivingPODataMapper.GetStatus(TransStatus.Posted))
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

        public String DeleteMultiSTCReceiveHd(string[] _prmCode)
        {
            String _result = "";
            Boolean _success = false;
            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCReceiveHd _stcReceiveHd = this.db.STCReceiveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcReceiveHd != null)
                    {
                        if ((_stcReceiveHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCReceiveDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCReceiveDts.DeleteAllOnSubmit(_query);

                            this.db.STCReceiveHds.DeleteOnSubmit(_stcReceiveHd);

                            _success = true;
                        }
                        else
                        {
                            _success = false;
                            break;
                        }
                    }
                }

                if (_success == true)
                    this.db.SubmitChanges();
                else
                    _result = "panel";
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _ErrorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteSTCReceiveHd", "STC");
                _result = ApplicationConfig.Error + " You Failed Delete Data. For futher information please contact your system Administrator. Code Case : " + _ErrorCode;
            }

            return _result;
        }

        public bool DeleteMultiApproveSTCReceiveHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCReceiveHd _stcReceiveHd = this.db.STCReceiveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcReceiveHd.Status == ReceivingPODataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcReceiveHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcReceiveHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcReceiveHd != null)
                    {
                        if ((_stcReceiveHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCReceiveDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCReceiveDts.DeleteAllOnSubmit(_query);

                            this.db.STCReceiveHds.DeleteOnSubmit(_stcReceiveHd);

                            _result = true;
                        }
                        else if (_stcReceiveHd.FileNmbr != "" && _stcReceiveHd.Status == ReceivingPODataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcReceiveHd.Status = ReceivingPODataMapper.GetStatus(TransStatus.Deleted);
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

        //public string AddSTCReceiveHd(STCReceiveHd _prmSTCReceiveHd, List<STCReceiveDt> _prmListSTCReceiveDt)
        //{
        //    string _result = "";

        //    try
        //    {
        //        List<STCReceiveDt> _listDetail = new List<STCReceiveDt>();

        //        Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
        //        foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
        //        {
        //            _prmSTCReceiveHd.TransNmbr = item.Number;
        //            _transactionNumber.TempTransNmbr = item.Number;
        //        }

        //        this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
        //        this.db.STCReceiveHds.InsertOnSubmit(_prmSTCReceiveHd);
        //        if (_prmListSTCReceiveDt.Count > 0)
        //        {
        //            foreach (STCReceiveDt _item in _prmListSTCReceiveDt)
        //            {
        //                STCReceiveDt _stcReceiveDt = new STCReceiveDt();

        //                _stcReceiveDt.TransNmbr = _prmSTCReceiveHd.TransNmbr;
        //                _stcReceiveDt.ProductCode = _item.ProductCode;
        //                _stcReceiveDt.LocationCode = _item.LocationCode;
        //                _stcReceiveDt.Qty = _item.Qty;
        //                _stcReceiveDt.Unit = _item.Unit;
        //                _stcReceiveDt.Remark = _item.Remark;

        //                _listDetail.Add(new STCReceiveDt(_stcReceiveDt.TransNmbr, _stcReceiveDt.ProductCode, _stcReceiveDt.LocationCode, _stcReceiveDt.Qty, _stcReceiveDt.Unit, _stcReceiveDt.Remark));
        //            }

        //            this.db.STCReceiveDts.InsertAllOnSubmit(_listDetail);
        //        }
        //        var _query = (
        //                        from _temp in this.db.Temporary_TransactionNumbers
        //                        where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
        //                        select _temp
        //                      );

        //        this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

        //        this.db.SubmitChanges();

        //        _result = _prmSTCReceiveHd.TransNmbr;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);

        //        String _ErrorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "AddSTCReceiveHd", "STC") ;
        //        _result = ApplicationConfig.Error + " You Failed Add Data. For futher information please contact your system Administrator. Code Case : " + _ErrorCode;
        //    }

        //    return _result;
        //}

        public string AddSTCReceiveHd(STCReceiveHd _prmSTCReceiveHd)
        {
            string _result = "";
            PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();

            try
            {
                List<STCReceiveDt> _listDetail = new List<STCReceiveDt>();

                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCReceiveHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCReceiveHds.InsertOnSubmit(_prmSTCReceiveHd);

                if (_prmSTCReceiveHd.FgLocation == true)
                {
                    List<PRCPODt> _prcPODt = _purchaseOrderBL.GetListPRCPODt(_prmSTCReceiveHd.PONo, _purchaseOrderBL.GetLastRevisiPRCPOHd(_prmSTCReceiveHd.PONo));
                    foreach (PRCPODt _item in _prcPODt)
                    {
                        decimal _qty = _item.Qty;
                        decimal _qtyClose = (_item.QtyClose == null ? 0 : Convert.ToDecimal(_item.QtyClose));
                        decimal _qtyRR = (_item.QtyRR == null ? 0 : Convert.ToDecimal(_item.QtyRR));
                        decimal _totalQty = _qty - _qtyRR - _qtyClose;
                        if (_totalQty > 0)
                        {
                            STCReceiveDt _stcReceiveDt = new STCReceiveDt();
                            _stcReceiveDt.TransNmbr = _prmSTCReceiveHd.TransNmbr;
                            _stcReceiveDt.ProductCode = _item.ProductCode;
                            _stcReceiveDt.LocationCode = _prmSTCReceiveHd.LocationCode;
                            _stcReceiveDt.Qty = _totalQty;
                            _stcReceiveDt.Unit = _item.Unit;
                            _stcReceiveDt.Remark = _item.Remark;

                            this.db.STCReceiveDts.InsertOnSubmit(_stcReceiveDt);
                        }
                    }
                }

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCReceiveHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _ErrorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "AddSTCReceiveHd", "STC");
                _result = ApplicationConfig.Error + " You Failed Add Data. For futher information please contact your system Administrator. Code Case : " + _ErrorCode;
            }

            return _result;
        }

        public String EditSTCReceiveHd(STCReceiveHd _prmSTCReceiveHd)
        {
            String _result = "";

            try
            {
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _ErrorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "EditSTCReceiveHd", "STC");
                _result = ApplicationConfig.Error + " You Failed Edit Data. For futher information please contact your system Administrator. Code Case : " + _ErrorCode;
            }

            return _result;
        }

        public string GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                int _success = this.db.S_STRRPOGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                _transActivity.ActivitiesCode = Guid.NewGuid();
                _transActivity.TransType = AppModule.GetValue(TransactionType.ReceivingPO);
                _transActivity.TransNmbr = _prmTransNmbr.ToString();
                _transActivity.FileNmbr = "";
                _transActivity.Username = _prmuser;
                _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                _transActivity.ActivitiesDate = DateTime.Now;
                _transActivity.Reason = "";

                this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _ErrorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetApprSTCReceive", "STC");
                _result = ApplicationConfig.Error + " Get Approval Failed. For futher information please contact your system Administrator. Code Case : " + _ErrorCode;
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
                    int _success = this.db.S_STRRPOApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        STCReceiveHd _stcReceiveHd = this.GetSingleSTCReceiveHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcReceiveHd.TransDate.Year, _stcReceiveHd.TransDate.Month, AppModule.GetValue(TransactionType.ReceivingPO), this._companyTag, ""))
                        {
                            _stcReceiveHd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ReceivingPO);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCReceiveHd(_prmTransNmbr).FileNmbr;
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
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _ErrorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "ApproveSTCReceive", "STC");
                _result = ApplicationConfig.Error + " Approve Failed. For futher information please contact your system Administrator. Code Case : " + _ErrorCode;
            }

            return _result;
        }

        public string Posting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCReceiveHd _stcReceiveHd = this.db.STCReceiveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcReceiveHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STRRPOPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ReceivingPO);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
                    _transActivity.FileNmbr = this.GetSingleSTCReceiveHd(_prmTransNmbr).FileNmbr;
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _ErrorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "PostingSTCReceive", "STC");
                _result = ApplicationConfig.Error + " Posting Failed. For futher information please contact your system Administrator. Code Case : " + _ErrorCode;
            }

            return _result;
        }

        public string Unposting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCReceiveHd _stcReceiveHd = this.db.STCReceiveHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcReceiveHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STRRPOUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    //_transActivity.ActivitiesCode = Guid.NewGuid();
                    //_transActivity.TransType = AppModule.GetValue(TransactionType.ReceivingPO);
                    //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                    //_transActivity.FileNmbr = this.GetSingleSTCReceiveHd(_prmTransNmbr).FileNmbr;
                    //_transActivity.Username = _prmuser;
                    //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                    //_transActivity.ActivitiesDate = this.GetSingleSTCReceiveHd(_prmTransNmbr).TransDate;
                    //_transActivity.Reason = "";

                    //this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    //this.db.SubmitChanges();
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _ErrorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "UnpostingSTCReceive", "STC");
                _result = ApplicationConfig.Error + " Unposting Failed. For futher information please contact your system Administrator. Code Case : " + _ErrorCode;
            }

            return _result;
        }

        public List<STCReceiveHd> GetListDDLRRQtyRemainForReturBySuppCode(string _prmSuppCode)
        {
            List<STCReceiveHd> _result = new List<STCReceiveHd>();

            try
            {
                var _query = (
                                from _stcReceive in this.db.STCReceiveHds
                                where (_stcReceive.FileNmbr ?? "").Trim() == _stcReceive.FileNmbr.Trim()
                                    && _stcReceive.SuppCode == _prmSuppCode
                                    && _stcReceive.Status == ReceivingPODataMapper.GetStatus(TransStatus.Posted)
                                    && _stcReceive.DoneInvoice == YesNoDataMapper.GetYesNo(YesNo.No)
                                    && (
                                            from _stcReceiveDt in this.db.STCReceiveDts
                                            where _stcReceiveDt.Qty - _stcReceiveDt.QtyRetur > 0
                                            select _stcReceiveDt.TransNmbr
                                       ).Contains(_stcReceive.TransNmbr)
                                select new
                                {
                                    TransNmbr = _stcReceive.TransNmbr,
                                    FileNmbr = _stcReceive.FileNmbr
                                }
                             ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCReceiveHd(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetListDDLRRQtyRemainForReturBySuppCode", "STC");
            }

            return _result;
        }
        #endregion

        #region STCReceiveDt
        public int RowsCountSTCReceiveDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.STCReceiveDts.Where(_temp => _temp.TransNmbr == _prmCode).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "RowsCountSTCReceiveDt", "STC");
            }

            return _result;
        }

        public List<STCReceiveDt> GetListSTCReceiveDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCReceiveDt> _result = new List<STCReceiveDt>();

            try
            {
                var _query = (
                                from _stcReceiveDt in this.db.STCReceiveDts
                                where _stcReceiveDt.TransNmbr == _prmCode
                                orderby _stcReceiveDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcReceiveDt.TransNmbr,
                                    ProductCode = _stcReceiveDt.ProductCode,
                                    LocationCode = _stcReceiveDt.LocationCode,
                                    Qty = _stcReceiveDt.Qty,
                                    Unit = _stcReceiveDt.Unit,
                                    Remark = _stcReceiveDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCReceiveDt(_row.TransNmbr, _row.ProductCode, _row.LocationCode, _row.Qty, _row.Unit, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetListSTCReceiveDt", "STC");
            }

            return _result;
        }

        public STCReceiveDt GetSingleSTCReceiveDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        {
            STCReceiveDt _result = new STCReceiveDt();

            try
            {
                var _query = (
                                from _stcReceiveDt in this.db.STCReceiveDts
                                where _stcReceiveDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                    && _stcReceiveDt.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower()
                                    && _stcReceiveDt.LocationCode.Trim().ToLower() == _prmLocationCode.Trim().ToLower()
                                select new
                                {
                                    _stcReceiveDt.TransNmbr,
                                    _stcReceiveDt.ProductCode,
                                    _stcReceiveDt.LocationCode,
                                    _stcReceiveDt.Qty,
                                    _stcReceiveDt.QtyRetur,
                                    _stcReceiveDt.Unit,
                                    _stcReceiveDt.Remark,
                                    _stcReceiveDt.AccInvent,
                                    _stcReceiveDt.FgInvent,
                                    _stcReceiveDt.PriceForex,
                                    _stcReceiveDt.AmountForex,
                                    _stcReceiveDt.DiscForex,
                                    _stcReceiveDt.TotalForex,
                                    _stcReceiveDt.TotalHome,
                                    _stcReceiveDt.ShippingForex,
                                    _stcReceiveDt.ShippingHome,
                                    _stcReceiveDt.TotalHPP
                                }
                            ).FirstOrDefault();

                _result.AccInvent = _query.AccInvent;
                _result.AmountForex = _query.AmountForex;
                _result.DiscForex = _query.DiscForex;
                _result.FgInvent = _query.FgInvent;
                _result.LocationCode = _query.LocationCode;
                _result.PriceForex = _query.PriceForex;
                _result.ProductCode = _query.ProductCode;
                _result.Qty = _query.Qty;
                _result.QtyRetur = _query.QtyRetur;
                _result.Remark = _query.Remark;
                _result.ShippingForex = _query.ShippingForex;
                _result.ShippingHome = _query.ShippingHome;
                _result.TotalForex = _query.TotalForex;
                _result.TotalHome = _query.TotalHome;
                _result.TotalHPP = _query.TotalHPP;
                _result.TransNmbr = _query.TransNmbr;
                _result.Unit = _query.Unit;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetSingleSTCReceiveDt", "STC");
            }

            return _result;
        }

        public STCReceiveDt GetSingleDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        {
            STCReceiveDt _result = new STCReceiveDt();

            try
            {
                _result = this.db.STCReceiveDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode && _temp.LocationCode == _prmLocationCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetSingleDt", "STC");
            }

            return _result;
        }

        public String DeleteMultiSTCReceiveDt(string[] _prmCode, string _prmTransNo)
        {
            String _result = "";

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    STCReceiveDt _STCReceiveDt = this.db.STCReceiveDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr == _prmTransNo.Trim().ToLower());

                    this.db.STCReceiveDts.DeleteOnSubmit(_STCReceiveDt);
                }

                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _ErrorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "DeleteSTCReceiveDt", "STC");
                _result = ApplicationConfig.Error + " You Failed Delete Data. For futher information please contact your system Administrator. Code Case : " + _ErrorCode;
            }

            return _result;
        }

        public String AddSTCReceiveDt(STCReceiveDt _prmSTCReceiveDt)
        {
            String _result = "";

            try
            {
                this.db.STCReceiveDts.InsertOnSubmit(_prmSTCReceiveDt);

                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _ErrorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "AddSTCReceiveDt", "STC");
                _result = ApplicationConfig.Error + " You Failed Add Data. For futher information please contact your system Administrator. Code Case : " + _ErrorCode;
            }

            return _result;
        }

        public String EditSTCReceiveDt(STCReceiveDt _prmSTCReceiveDt)
        {
            String _result = "";

            try
            {
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _ErrorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "EditSTCReceiveDt", "STC");
                _result = ApplicationConfig.Error + " You Failed Edit Data. For futher information please contact your system Administrator. Code Case : " + _ErrorCode;
            }

            return _result;
        }

        public List<MsWrhsLocation> GetListDDLWrhsLocationByProductCode(string _prmCode, string _prmProductCode)
        {
            List<MsWrhsLocation> _result = new List<MsWrhsLocation>();

            try
            {
                var _query = (
                                from _stcReceiveDt in this.db.STCReceiveDts
                                where _stcReceiveDt.TransNmbr == _prmCode
                                    && _stcReceiveDt.ProductCode == _prmProductCode
                                select new
                                {
                                    LocationCode = _stcReceiveDt.LocationCode,
                                    LocationName = (
                                                    from _location in this.db.MsWrhsLocations
                                                    where _location.WLocationCode == _stcReceiveDt.LocationCode
                                                    select _location.WLocationName
                                                   ).FirstOrDefault()
                                }
                             ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsWrhsLocation(_row.LocationCode, _row.LocationName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetListDDLWrhsLocationByProductCode", "STC");
            }

            return _result;
        }

        public List<MsProduct> GetListProductForDDLRRQtyRemain(string _prmRRNo)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _rcvDt in this.db.STCReceiveDts
                                join _msProduct in this.db.MsProducts
                                    on _rcvDt.ProductCode equals _msProduct.ProductCode
                                where _rcvDt.TransNmbr.Trim().ToLower() == _prmRRNo.Trim().ToLower()
                                    && (_rcvDt.Qty - _rcvDt.QtyRetur) > 0
                                orderby _msProduct.ProductName
                                select new
                                {
                                    ProductCode = _rcvDt.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetListProductForDDLRRQtyRemain", "STC");
            }

            return _result;
        }

        public decimal GetQtyFromReceiveDtForRR(string _prmRRNo, string _prmProductCode, string _prmLocationCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _rcvDt in this.db.STCReceiveDts
                                where _rcvDt.TransNmbr.Trim().ToLower() == _prmRRNo.Trim().ToLower()
                                    && (_rcvDt.Qty - _rcvDt.QtyRetur) > 0
                                    && _rcvDt.ProductCode == _prmProductCode
                                    && _rcvDt.LocationCode == _prmLocationCode
                                select new
                                {
                                    QtyRemain = _rcvDt.Qty - _rcvDt.QtyRetur
                                }
                             );

                foreach (var _row in _query)
                {
                    _result = _row.QtyRemain;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetQtyFromReceiveDtForRR", "STC");
            }

            return _result;
        }
        #endregion

        #region V_STRRForSI
        //public decimal GetPPNFromVSTRRForSI(string _prmPONo, string _prmProductCode)
        //{
        //    decimal _result = 0;

        //    try
        //    {
        //        var _query = (
        //                        from _vSTRRForSI in this.db.V_STRRForSIs
        //                        where _vSTRRForSI.PO_No == _prmPONo && _vSTRRForSI.Product_Code == _prmProductCode
        //                        select new
        //                        {
        //                            PPN = _vSTRRForSI.PPN
        //                        }
        //                     );
        //        foreach (var _row in _query)
        //        {
        //            _result = _row.PPN;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public List<STCReceiveDt> GetListDDLRRNoFromVSTRRForSI()
        {
            List<STCReceiveDt> _result = new List<STCReceiveDt>();

            try
            {
                var _query = (
                                from _vSTRRForSI in this.db.V_STRRForSIs
                                where (_vSTRRForSI.FileNmbr ?? "").Trim() == _vSTRRForSI.FileNmbr.Trim()
                                select new
                                {
                                    RR_No = _vSTRRForSI.RR_No,
                                    FileNmbr = _vSTRRForSI.FileNmbr
                                }
                             ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCReceiveDt(_row.RR_No, _row.FileNmbr));
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetListDDLRRNoFromVSTRRForSI", "STC");
            }

            return _result;
        }

        public List<STCReceiveDt> GetListDDLRRNoFromVSTRRForSI(string _prmPoNo)
        {
            List<STCReceiveDt> _result = new List<STCReceiveDt>();

            try
            {
                var _query = (
                                from _vSTRRForSI in this.db.V_STRRForSIs
                                where (_vSTRRForSI.FileNmbr ?? "").Trim() == _vSTRRForSI.FileNmbr.Trim()
                                && _vSTRRForSI.PO_No == _prmPoNo.Trim()
                                select new
                                {
                                    RR_No = _vSTRRForSI.RR_No,
                                    FileNmbr = _vSTRRForSI.FileNmbr
                                }
                             ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCReceiveDt(_row.RR_No, _row.FileNmbr));
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetListDDLRRNoFromVSTRRForSI", "STC");
            }

            return _result;
        }

        public List<PRCPOHd> GetListDDLPONoForSI()
        {
            List<PRCPOHd> _result = new List<PRCPOHd>();

            try
            {
                var _query = (
                                from _vSTRRForSI in this.db.V_STRRForSIs
                                where (_vSTRRForSI.FileNmbr ?? "").Trim() == _vSTRRForSI.FileNmbr.Trim()
                                select new
                                {
                                    PO_No = _vSTRRForSI.PO_No,
                                    PO_FileNo = _vSTRRForSI.File_Nmbr
                                }
                             ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new PRCPOHd(_row.PO_No, _row.PO_FileNo));
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetListDDLPONoForSI", "STC");
            }

            return _result;
        }

        public List<MsProduct> GetListDDLProductFromVSTRRForSI(string _prmRRNo)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _vSTRRForSI in this.db.V_STRRForSIs
                                where _vSTRRForSI.RR_No == _prmRRNo
                                orderby _vSTRRForSI.Product_Name
                                select new
                                {
                                    ProductCode = _vSTRRForSI.Product_Code,
                                    ProductName = _vSTRRForSI.Product_Name
                                }
                             );
                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetListDDLProductFromVSTRRForSI", "STC");
            }

            return _result;
        }

        public string GetFileNmbrFromVSTRRForSI(string _prmRRNo)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vSTRRForSI in this.db.V_STRRForSIs
                                where _vSTRRForSI.RR_No == _prmRRNo
                                select new
                                {
                                    PONo = _vSTRRForSI.File_Nmbr
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = _row.PONo;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetFileNmbrFromVSTRRForSI", "STC");
            }

            return _result;
        }

        public string GetPONoFromVSTRRForSI(string _prmRRNo)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vSTRRForSI in this.db.V_STRRForSIs
                                where _vSTRRForSI.RR_No == _prmRRNo
                                select new
                                {
                                    PONo = _vSTRRForSI.PO_No
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = _row.PONo;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetPONoFromVSTRRForSI", "STC");
            }

            return _result;
        }

        public string GetUnitFromVSTRRForSI(string _prmRRNo, string _prmProductCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vSTRRForSI in this.db.V_STRRForSIs
                                where _vSTRRForSI.RR_No == _prmRRNo && _vSTRRForSI.Product_Code == _prmProductCode
                                select new
                                {
                                    Unit = _vSTRRForSI.Unit
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = _row.Unit;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetUnitFromVSTRRForSI", "STC");
            }

            return _result;
        }

        public decimal GetQtyFromVSTRRForSI(string _prmRRNo, string _prmProductCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _vSTRRForSI in this.db.V_STRRForSIs
                                where _vSTRRForSI.RR_No == _prmRRNo && _vSTRRForSI.Product_Code == _prmProductCode
                                select new
                                {
                                    Qty = _vSTRRForSI.Qty
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = (_row.Qty == null) ? 0 : Convert.ToDecimal(_row.Qty);
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetQtyFromVSTRRForSI", "STC");
            }

            return _result;
        }

        public decimal GetPriceFromVSTRRForSI(string _prmRRNo, string _prmProductCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _vSTRRForSI in this.db.V_STRRForSIs
                                where _vSTRRForSI.RR_No == _prmRRNo && _vSTRRForSI.Product_Code == _prmProductCode
                                select new
                                {
                                    Price = _vSTRRForSI.Price
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = (_row.Price == null) ? 0 : Convert.ToDecimal(_row.Price);
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "GetPriceFromVSTRRForSI", "STC");
            }

            return _result;
        }
        #endregion

        //public List<STCReceiveHd> GetListDDLRRQtyRemainForReturBySuppCode(string _prmSuppCode)
        //{
        //    List<STCReceiveHd> _result = new List<STCReceiveHd>();

        //    try
        //    {
        //        var _query = (
        //                        from _vRRQtyRemain in this.db.V_RRQtyRemains
        //                        where (_vRRQtyRemain.FileNmbr ?? "").Trim() == _vRRQtyRemain.FileNmbr.Trim()
        //                            && _vRRQtyRemain.SuppCode == _prmSuppCode
        //                        select new
        //                        {
        //                            RRNo = _vRRQtyRemain.RRNo,
        //                            FileNmbr = _vRRQtyRemain.FileNmbr
        //                        }
        //                     ).Distinct();

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new STCReceiveHd(_row.RRNo, _row.FileNmbr));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        ~ReceivingPOBL()
        {
        }
    }
}
