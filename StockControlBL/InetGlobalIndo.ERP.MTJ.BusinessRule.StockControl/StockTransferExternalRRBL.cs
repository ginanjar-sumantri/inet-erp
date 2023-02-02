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
    public sealed class StockTransferExternalRRBL : Base
    {
        public StockTransferExternalRRBL()
        {
        }

        #region STCTransInHd
        public double RowsCountSTCTransInHd(string _prmCategory, string _prmKeyword)
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
                    from _stcTransInHd in this.db.STCTransInHds
                    where (SqlMethods.Like(_stcTransInHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    && (SqlMethods.Like((_stcTransInHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                    && _stcTransInHd.Status != StockTransferExternalRRDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcTransInHd
                ).Count();

            _result = _query;

            return _result;
        }

        public List<STCTransInHd> GetListSTCTransInHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCTransInHd> _result = new List<STCTransInHd>();

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
                                from _stcTransInHd in this.db.STCTransInHds
                                where (SqlMethods.Like(_stcTransInHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_stcTransInHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _stcTransInHd.Status != StockTransferExternalRRDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcTransInHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _stcTransInHd.TransNmbr,
                                    FileNmbr = _stcTransInHd.FileNmbr,
                                    TransDate = _stcTransInHd.TransDate,
                                    Status = _stcTransInHd.Status,
                                    TransReff = _stcTransInHd.TransReff,
                                    FileNoTransReff = (
                                                        from _stcTransOutHd in this.db.STCTransOutHds
                                                        where _stcTransInHd.TransReff == _stcTransOutHd.TransNmbr
                                                        select _stcTransOutHd.FileNmbr
                                                    ).FirstOrDefault(),
                                    WrhsSrc = _stcTransInHd.WrhsSrc,
                                    WrhsSrcName = (
                                                        from _msWarehouseSrc in this.db.MsWarehouses
                                                        where _stcTransInHd.WrhsSrc == _msWarehouseSrc.WrhsCode
                                                        select _msWarehouseSrc.WrhsName
                                                    ).FirstOrDefault(),
                                    WrhsDest = _stcTransInHd.WrhsDest,
                                    WrhsDestName = (
                                                        from _msWarehouseDest in this.db.MsWarehouses
                                                        where _stcTransInHd.WrhsDest == _msWarehouseDest.WrhsCode
                                                        select _msWarehouseDest.WrhsName
                                                    ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCTransInHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.TransReff, _row.FileNoTransReff, _row.WrhsSrc, _row.WrhsSrcName, _row.WrhsDest, _row.WrhsDestName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCTransInHd GetSingleSTCTransInHd(string _prmCode)
        {
            STCTransInHd _result = null;

            try
            {
                _result = this.db.STCTransInHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCTransInHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTransInHd _stcTransInHd = this.db.STCTransInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcTransInHd != null)
                    {
                        if (_stcTransInHd.Status != StockTransferExternalRRDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiSTCTransInHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTransInHd _STCTransInHd = this.db.STCTransInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_STCTransInHd != null)
                    {
                        if ((_STCTransInHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCTransInDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCTransInDts.DeleteAllOnSubmit(_query);

                            this.db.STCTransInHds.DeleteOnSubmit(_STCTransInHd);

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

        public bool DeleteMultiApproveSTCTransInHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTransInHd _stcTransInHd = this.db.STCTransInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcTransInHd.Status == StockTransferExternalRRDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcTransInHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcTransInHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcTransInHd != null)
                    {
                        if ((_stcTransInHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCTransInDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCTransInDts.DeleteAllOnSubmit(_query);

                            this.db.STCTransInHds.DeleteOnSubmit(_stcTransInHd);

                            _result = true;
                        }
                        else if (_stcTransInHd.FileNmbr != "" && _stcTransInHd.Status == StockTransferExternalRRDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcTransInHd.Status = StockTransferExternalRRDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddSTCTransInHd(STCTransInHd _prmSTCTransInHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCTransInHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCTransInHds.InsertOnSubmit(_prmSTCTransInHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCTransInHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCTransInHd(STCTransInHd _prmSTCTransInHd)
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

        public string GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_STTransInGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockTransferExternalRR);
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
                    int _success = this.db.S_STTransInApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        STCTransInHd _stcTransInHd = this.GetSingleSTCTransInHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcTransInHd.TransDate.Year, _stcTransInHd.TransDate.Month, AppModule.GetValue(TransactionType.StockTransferExternalRR), this._companyTag, ""))
                        {
                            _stcTransInHd.FileNmbr = item.Number;
                        }


                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockTransferExternalRR);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCTransInHd(_prmTransNmbr).FileNmbr;
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

                STCTransInHd _stcTransInHd = this.db.STCTransInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcTransInHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STTransInPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockTransferExternalRR);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCTransInHd(_prmTransNmbr).FileNmbr;
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

                STCTransInHd _stcTransInHd = this.db.STCTransInHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcTransInHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STTransInUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockTransferExternalRR);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCTransInHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCTransInHd(_prmTransNmbr).TransDate;
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

        #region STCTransInDt
        public int RowsCountSTCTransInDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _STCTransInDt in this.db.STCTransInDts
                                 where _STCTransInDt.TransNmbr == _prmCode
                                 select _STCTransInDt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCTransInDt> GetListSTCTransInDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCTransInDt> _result = new List<STCTransInDt>();

            try
            {
                var _query = (
                                from _stcTransInDt in this.db.STCTransInDts
                                where _stcTransInDt.TransNmbr == _prmCode
                                orderby _stcTransInDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcTransInDt.TransNmbr,
                                    ProductCode = _stcTransInDt.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode == _stcTransInDt.ProductCode
                                                    select _msProduct.ProductName
                                                ).FirstOrDefault(),
                                    LocationSrc = _stcTransInDt.LocationSrc,
                                    LocationCode = _stcTransInDt.LocationCode,
                                    QtySJ = _stcTransInDt.QtySJ,
                                    Qty = _stcTransInDt.Qty,
                                    QtyLoss = _stcTransInDt.QtyLoss,
                                    Unit = _stcTransInDt.Unit
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCTransInDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.LocationSrc, _row.LocationCode, _row.QtySJ, _row.Qty, _row.QtyLoss, _row.Unit));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCTransInDt GetSingleSTCTransInDt(string _prmCode, string _prmProductCode, string _prmLocSrc)
        {
            STCTransInDt _result = null;

            try
            {
                _result = this.db.STCTransInDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode && _temp.LocationSrc == _prmLocSrc);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCTransInDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('|');

                    STCTransInDt _STCTransInDt = this.db.STCTransInDts.Single(_temp => _temp.ProductCode == _tempSplit[0] && _temp.LocationSrc == _tempSplit[1] && _temp.TransNmbr == _prmTransNo);

                    this.db.STCTransInDts.DeleteOnSubmit(_STCTransInDt);
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

        public bool AddSTCTransInDt(STCTransInDt _prmSTCTransInDt)
        {
            bool _result = false;

            try
            {
                this.db.STCTransInDts.InsertOnSubmit(_prmSTCTransInDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCTransInDt(STCTransInDt _prmSTCTransInDt)
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

        ~StockTransferExternalRRBL()
        {
        }
    }
}
