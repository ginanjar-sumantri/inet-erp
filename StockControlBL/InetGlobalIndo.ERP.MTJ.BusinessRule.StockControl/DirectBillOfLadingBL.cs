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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class DirectBillOfLadingBL : Base
    {
        public DirectBillOfLadingBL()
        {

        }

        # region STCTrDirectSJHd
        public double RowsCountSTCTrDirectSJHd(string _prmCategory, string _prmKeyword)
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
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
            }

            var _query =
                (
                    from _STCTrDirectSJHd in this.db.STCTrDirectSJHds
                    where (SqlMethods.Like(_STCTrDirectSJHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like((_STCTrDirectSJHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && _STCTrDirectSJHd.Status != DirectBillOfLadingDataMapper.GetStatusByte(TransStatus.Deleted)
                    select _STCTrDirectSJHd
                ).Count();

            _result = _query;
            return _result;
        }

        public string AddSTCTrDirectSJHd(STCTrDirectSJHd _prmSTCTrDirectSJHd)//, List<STCTrDirectSJSO> _prmListSTCTrDirectSJSO)
        {
            string _result = "";

            try
            {
                //List<STCTrDirectSJSO> _listDetail = new List<STCTrDirectSJSO>();

                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCTrDirectSJHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCTrDirectSJHds.InsertOnSubmit(_prmSTCTrDirectSJHd);

                //if (_prmListSTCTrDirectSJSO.Count > 0)
                //{
                //    foreach (STCTrDirectSJSO _item in _prmListSTCTrDirectSJSO)
                //    {
                //        STCTrDirectSJSO _STCTrDirectSJSO = new STCTrDirectSJSO();

                //        _STCTrDirectSJSO.TransNmbr = _prmSTCTrDirectSJHd.TransNmbr;
                //        _STCTrDirectSJSO.DONo = _item.DONo;
                //        _STCTrDirectSJSO.ProductCode = _item.ProductCode;
                //        _STCTrDirectSJSO.LocationCode = _item.LocationCode;
                //        _STCTrDirectSJSO.Qty = _item.Qty;
                //        _STCTrDirectSJSO.Unit = _item.Unit;
                //        _STCTrDirectSJSO.Remark = _item.Remark;
                //        _STCTrDirectSJSO.QtyLoss = 0;
                //        _STCTrDirectSJSO.QtyRetur = 0;
                //        _STCTrDirectSJSO.QtyReceive = _item.Qty;

                //        _listDetail.Add(new STCTrDirectSJSO(_STCTrDirectSJSO.TransNmbr, _STCTrDirectSJSO.ProductCode, _STCTrDirectSJSO.LocationCode, _STCTrDirectSJSO.DONo, _STCTrDirectSJSO.Qty, _STCTrDirectSJSO.Unit, _STCTrDirectSJSO.Remark, _STCTrDirectSJSO.QtyLoss, _STCTrDirectSJSO.QtyRetur, _STCTrDirectSJSO.QtyReceive));
                //    }

                //    this.db.STCTrDirectSJSOs.InsertAllOnSubmit(_listDetail);
                //}

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCTrDirectSJHd.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCTrDirectSJHd> GetListSTCTrDirectSJHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCTrDirectSJHd> _result = new List<STCTrDirectSJHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query =
                            (
                                from _STCTrDirectSJHd in this.db.STCTrDirectSJHds
                                where (SqlMethods.Like(_STCTrDirectSJHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_STCTrDirectSJHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _STCTrDirectSJHd.Status != DirectBillOfLadingDataMapper.GetStatusByte(TransStatus.Deleted)                               
                                select _STCTrDirectSJHd
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
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

        public STCTrDirectSJHd GetSingleSTCTrDirectSJHd(string _prmTransNmbr)
        {
            STCTrDirectSJHd _result = null;

            try
            {
                _result = this.db.STCTrDirectSJHds.Single(_temp => _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCTrDirectSJHd()
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

        public bool DeleteMultiSTCTrDirectSJHd(string[] _prmTransNmbr)
        {
            bool _result = false;
            try
            {
                for (int i = 0; i < _prmTransNmbr.Length; i++)
                {
                    STCTrDirectSJHd _STCTrDirectSJHd = this.db.STCTrDirectSJHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower());

                    if (_STCTrDirectSJHd != null)
                    {
                        if ((_STCTrDirectSJHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCTrDirectSJSOs
                                          where _detail.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower()
                                          select _detail);

                            foreach (var _detilSO in _query)
                            {
                                var _deleteDetailProduct = (
                                        from _detailProduct in this.db.STCTrDirectSJProducts
                                        where _detailProduct.SONo == _detilSO.SONo
                                        select _detailProduct
                                    );
                                this.db.STCTrDirectSJProducts.DeleteAllOnSubmit(_deleteDetailProduct);
                            }

                            this.db.STCTrDirectSJSOs.DeleteAllOnSubmit(_query);
                            this.db.STCTrDirectSJHds.DeleteOnSubmit(_STCTrDirectSJHd);
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

        public string GetAppr(string _prmTransNmbr, int _prmYear, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";
            try
            {
                int _success = this.db.spSTC_DirectSJGetAppr(_prmTransNmbr, _prmYear, _prmuser, ref _errorMsg);
                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.DirectBillOfLading);
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

        public string Approve(string _prmTransNmbr, int _prmYear, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spSTC_DirectSJApprove(_prmTransNmbr, _prmYear, _prmuser, ref _errorMsg);
                    if (_errorMsg == "")
                    {
                        STCTrDirectSJHd _STCTrDirectSJHd = this.GetSingleSTCTrDirectSJHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_STCTrDirectSJHd.TransDate.Year, _STCTrDirectSJHd.TransDate.Month, AppModule.GetValue(TransactionType.DirectBillOfLading), this._companyTag, ""))
                        {
                            _STCTrDirectSJHd.FileNmbr = item.Number;
                        } 

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.DirectBillOfLading);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCTrDirectSJHd(_prmTransNmbr).FileNmbr;
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

        public string Posting(string _prmTransNmbr, int _prmYear, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                STCTrDirectSJHd _STCTrDirectSJHd = this.db.STCTrDirectSJHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());

                int _success = this.db.spSTC_DirectSJPost(_prmTransNmbr, _prmYear, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Posting Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.DirectBillOfLading);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
                    _transActivity.FileNmbr = this.GetSingleSTCTrDirectSJHd(_prmTransNmbr).FileNmbr;
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
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string Unposting(string _prmTransNmbr, int _prmYear, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";
            try
            {
                STCTrDirectSJHd _STCTrDirectSJHd = this.db.STCTrDirectSJHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());

                int _success = this.db.spSTC_DirectSJUnPost(_prmTransNmbr, _prmYear, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Unposting Success";

                    //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    //_transActivity.ActivitiesCode = Guid.NewGuid();
                    //_transActivity.TransType = AppModule.GetValue(TransactionType.DirectBillOfLading);
                    //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                    //_transActivity.FileNmbr = this.GetSingleSTCTrDirectSJHd(_prmTransNmbr).FileNmbr;
                    //_transActivity.Username = _prmuser;
                    //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                    //_transActivity.ActivitiesDate = this.GetSingleSTCTrDirectSJHd(_prmTransNmbr).TransDate;
                    //_transActivity.Reason = "";

                    //this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    //this.db.SubmitChanges();
                }
                else
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Unposting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }
            return _result;
        }

        public string GetFileNmbrFromSTCTrDirectSJHd(string _prmTransNmbr)
        {
            string _result = "";
            try
            {
                _result = this.db.STCTrDirectSJHds.Single(_temp => _temp.TransNmbr == _prmTransNmbr).FileNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public bool GetSingleSTCTrDirectSJHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTrDirectSJHd _mktSTCTrDirectSJHd = this.db.STCTrDirectSJHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_mktSTCTrDirectSJHd != null)
                    {
                        if (_mktSTCTrDirectSJHd.Status != DirectBillOfLadingDataMapper.GetStatusByte(TransStatus.Posted))
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

        public bool DeleteMultiApproveSTCTrDirectSJHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTrDirectSJHd _mktSTCTrDirectSJHd = this.db.STCTrDirectSJHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_mktSTCTrDirectSJHd.Status == DirectBillOfLadingDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _mktSTCTrDirectSJHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _mktSTCTrDirectSJHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_mktSTCTrDirectSJHd != null)
                    {
                        if ((_mktSTCTrDirectSJHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCTrDirectSJSOs
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            foreach (var _detilSO in _query)
                            {
                                var _deleteDetailProduct = (
                                        from _detailProduct in this.db.STCTrDirectSJProducts
                                        where _detailProduct.SONo == _detilSO.SONo
                                        select _detailProduct
                                    );
                                this.db.STCTrDirectSJProducts.DeleteAllOnSubmit(_deleteDetailProduct);
                            }

                            this.db.STCTrDirectSJSOs.DeleteAllOnSubmit(_query);
                            this.db.STCTrDirectSJHds.DeleteOnSubmit(_mktSTCTrDirectSJHd);
                            _result = true;
                        }
                        else if (_mktSTCTrDirectSJHd.FileNmbr != "" && _mktSTCTrDirectSJHd.Status == DirectBillOfLadingDataMapper.GetStatusByte(TransStatus.Approved))
                        {
                            _mktSTCTrDirectSJHd.Status = DirectBillOfLadingDataMapper.GetStatusByte(TransStatus.Deleted);
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

        #endregion

        # region STCTrDirectSJSO

        public int RowsCountSTCTrDirectSJSO(string _prmTransNmbr)
        {
            int _result = 0;
            try
            {
                _result = this.db.STCTrDirectSJSOs.Where(_temp => _temp.TransNmbr == _prmTransNmbr).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<STCTrDirectSJSO> GetListSTCTrDirectSJSO(int _prmReqPage, int _prmPageSize, string _prmTransNmbr)
        {
            List<STCTrDirectSJSO> _result = new List<STCTrDirectSJSO>();
            try
            {
                var _query = (from _STCTrDirectSJSO in this.db.STCTrDirectSJSOs
                              where _STCTrDirectSJSO.TransNmbr == _prmTransNmbr
                              select _STCTrDirectSJSO
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
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

        public List<STCTrDirectSJSO> GetListSTCTrDirectSJSO(string _prmTransNmbr)
        {
            List<STCTrDirectSJSO> _result = new List<STCTrDirectSJSO>();
            try
            {
                var _query =
                            (
                                from _STCTrDirectSJSO in this.db.STCTrDirectSJSOs
                                where _STCTrDirectSJSO.TransNmbr == _prmTransNmbr
                                select _STCTrDirectSJSO
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

        public STCTrDirectSJSO GetSingleSTCTrDirectSJSO(string _prmTransNmbr, string _prmSoNo)
        {
            STCTrDirectSJSO _result = new STCTrDirectSJSO();
            try
            {
                _result = this.db.STCTrDirectSJSOs.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.SONo == _prmSoNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public bool EditSTCTrDirectSJSO()
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

        public bool DeleteMultiSTCTrDirectSJSO(string _prmTransNmbr, string[] _prmCode)
        {
            bool _result = false;
            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTrDirectSJSO _STCTrDirectSJSO = this.db.STCTrDirectSJSOs.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.SONo == _prmCode[i]);
                    this.db.STCTrDirectSJSOs.DeleteOnSubmit(_STCTrDirectSJSO);
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

        public bool AddSTCTrDirectSJSO(STCTrDirectSJSO _prmSTCTrDirectSJSO)
        {
            bool _result = false;
            try
            {
                this.db.STCTrDirectSJSOs.InsertOnSubmit(_prmSTCTrDirectSJSO);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public Boolean CekOutstandingQty(String _prmSoNumber)
        {
            Boolean _result = false;
            try
            {
                var _qryDetilSO = this.db.MKTSODts.Where(a => a.TransNmbr == _prmSoNumber && a.Qty > a.QtyDO);
                if (_qryDetilSO.Count() > 0)
                    _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        #endregion

        #region STCTrDirectProduct

        public int RowsCountSTCTrDirectSJProduct(string _prmTransNmbr)
        {
            int _result = 0;
            try
            {
                var _qrySO = this.db.STCTrDirectSJSOs.Where(_temp => _temp.TransNmbr == _prmTransNmbr);
                List<String> _dataCollection = new List<String>();
                foreach (var _rowSO in _qrySO)
                {
                    var _qryProduct = this.db.STCTrDirectSJProducts.Where(a => a.SONo == _rowSO.SONo);
                    foreach (var _rowProduct in _qryProduct)
                        _dataCollection.Add(_rowProduct.ProductCode);
                }
                _result = _dataCollection.Distinct().Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public bool DeleteMultiSTCTrDirectSJProduct(string _prmTransNmbr, string[] _prmCode)
        {
            bool _result = false;
            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTrDirectSJProduct _STCTrDirectSJProduct = this.db.STCTrDirectSJProducts.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.ProductCode == _prmCode[i]);
                    this.db.STCTrDirectSJProducts.DeleteOnSubmit(_STCTrDirectSJProduct);
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

        public void GenerateDetilProduct(String _prmTransNmbr)
        {
            try
            {
                STCTrDirectSJHd _directSJHd = this.db.STCTrDirectSJHds.Where ( a => a.TransNmbr == _prmTransNmbr ).FirstOrDefault() ;

                var _qryDataLama = this.db.STCTrDirectSJProducts.Where(a => a.TransNmbr == _prmTransNmbr);
                this.db.STCTrDirectSJProducts.DeleteAllOnSubmit(_qryDataLama);

                var _qrySoNo = this.db.STCTrDirectSJSOs.Where(a => a.TransNmbr == _prmTransNmbr);
                foreach (var _rowSoNo in _qrySoNo)
                {
                    var _qryDetailSO = this.db.MKTSODts.Where(a => a.TransNmbr == _rowSoNo.SONo && a.Qty > a.QtyDO);
                    foreach (var _rowProduct in _qryDetailSO)
                    {
                        STCTrDirectSJProduct _addData = new STCTrDirectSJProduct();
                        _addData.TransNmbr = _prmTransNmbr;
                        _addData.SONo = _rowSoNo.SONo;
                        _addData.ProductCode = _rowProduct.ProductCode;
                        _addData.Qty = Convert.ToDecimal(_rowProduct.Qty - _rowProduct.QtyDO);
                        if (_directSJHd.FgLocation)
                            _addData.LocationCode = _directSJHd.LocationCode;
                        else
                            _addData.LocationCode = "null";
                        this.db.STCTrDirectSJProducts.InsertOnSubmit(_addData);
                    }
                }


                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public STCTrDirectSJProduct GetSingleSTCTrDirectSJProduct(string _transNmbr, string _SONo, string _productCode)
        {
            STCTrDirectSJProduct _result = new STCTrDirectSJProduct();
            try
            {
                _result = this.db.STCTrDirectSJProducts.Single(a => a.TransNmbr == _transNmbr && a.SONo == _SONo && a.ProductCode == _productCode);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }
        
        public bool EditSTCTrDirectSJProduct()
        {
            try
            {
                this.db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        
        public List<STCTrDirectSJProduct> GetListSTCTrDirectSJProduct(int _prmCurrentPage, int _maxrow2, String _prmTransNmbr)
        {
            List<STCTrDirectSJProduct> _result = new List<STCTrDirectSJProduct>();
            try
            {
                var _qry = (
                        from _stcTrDirectSJProduct in this.db.STCTrDirectSJProducts 
                        where _stcTrDirectSJProduct.TransNmbr == _prmTransNmbr 
                        select _stcTrDirectSJProduct 
                    ).Skip ( _prmCurrentPage * _maxrow2 ).Take (_maxrow2 );
                foreach (STCTrDirectSJProduct _rowData in _qry)
                    _result.Add(_rowData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        #endregion

        ~DirectBillOfLadingBL()
        {

        }

    }
}
