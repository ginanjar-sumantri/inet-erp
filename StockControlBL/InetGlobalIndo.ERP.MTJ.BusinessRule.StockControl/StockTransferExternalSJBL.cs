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
    public sealed class StockTransferExternalSJBL : Base
    {
        public StockTransferExternalSJBL()
        {
        }

        #region STCTransOutHd
        public double RowsCountSTCTransOutHd(string _prmCategory, string _prmKeyword)
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
                    from _stcTransOutHd in this.db.STCTransOutHds
                    where (SqlMethods.Like(_stcTransOutHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    && (SqlMethods.Like((_stcTransOutHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                    && _stcTransOutHd.Status != StockTransferExternalSJDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcTransOutHd
                ).Count();

            _result = _query;

            return _result;
        }

        public List<STCTransOutHd> GetListSTCTransOutHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCTransOutHd> _result = new List<STCTransOutHd>();

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
                                from _stcTransOutHd in this.db.STCTransOutHds
                                where (SqlMethods.Like(_stcTransOutHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_stcTransOutHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _stcTransOutHd.Status != StockTransferExternalSJDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcTransOutHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _stcTransOutHd.TransNmbr,
                                    FileNmbr = _stcTransOutHd.FileNmbr,
                                    TransDate = _stcTransOutHd.TransDate,
                                    Status = _stcTransOutHd.Status,
                                    RequestNo = _stcTransOutHd.RequestNo,
                                    FileNoRequest = (
                                                        from _stcTransReqHd in this.db.STCTransReqHds
                                                        where _stcTransReqHd.TransNmbr == _stcTransOutHd.RequestNo
                                                        select _stcTransReqHd.FileNmbr
                                                    ).FirstOrDefault(),
                                    WrhsSrc = _stcTransOutHd.WrhsSrc,
                                    WrhsSrcName = (
                                                        from _msWarehouseSrc in this.db.MsWarehouses
                                                        where _stcTransOutHd.WrhsSrc == _msWarehouseSrc.WrhsCode
                                                        select _msWarehouseSrc.WrhsName
                                                    ).FirstOrDefault(),
                                    WrhsDest = _stcTransOutHd.WrhsDest,
                                    WrhsDestName = (
                                                        from _msWarehouseDest in this.db.MsWarehouses
                                                        where _stcTransOutHd.WrhsDest == _msWarehouseDest.WrhsCode
                                                        select _msWarehouseDest.WrhsName
                                                    ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCTransOutHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.RequestNo, _row.FileNoRequest, _row.WrhsSrc, _row.WrhsSrcName, _row.WrhsDest, _row.WrhsDestName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCTransOutHd GetSingleSTCTransOutHd(string _prmCode)
        {
            STCTransOutHd _result = null;

            try
            {
                _result = this.db.STCTransOutHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCTransOutHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTransOutHd _stcTransOutHd = this.db.STCTransOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcTransOutHd != null)
                    {
                        if (_stcTransOutHd.Status != StockTransferExternalSJDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiSTCTransOutHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTransOutHd _stcTransOutHd = this.db.STCTransOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcTransOutHd != null)
                    {
                        if ((_stcTransOutHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCTransOutDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCTransOutDts.DeleteAllOnSubmit(_query);

                            this.db.STCTransOutHds.DeleteOnSubmit(_stcTransOutHd);

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

        public bool DeleteMultiApproveSTCTransOutHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTransOutHd _stcTransOutHd = this.db.STCTransOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcTransOutHd.Status == StockTransferExternalSJDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcTransOutHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcTransOutHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcTransOutHd != null)
                    {
                        if ((_stcTransOutHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCTransOutDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCTransOutDts.DeleteAllOnSubmit(_query);

                            this.db.STCTransOutHds.DeleteOnSubmit(_stcTransOutHd);

                            _result = true;
                        }
                        else if (_stcTransOutHd.FileNmbr != "" && _stcTransOutHd.Status == StockTransferExternalSJDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcTransOutHd.Status = StockTransferExternalSJDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddSTCTransOutHd(STCTransOutHd _prmSTCTransOutHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCTransOutHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCTransOutHds.InsertOnSubmit(_prmSTCTransOutHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCTransOutHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCTransOutHd(STCTransOutHd _prmSTCTransOutHd)
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
                int _success = this.db.S_STTransOutGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockTransferExternalSJ);
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
                    int _success = this.db.S_STTransOutApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        STCTransOutHd _stcTransOutHd = this.GetSingleSTCTransOutHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcTransOutHd.TransDate.Year, _stcTransOutHd.TransDate.Month, AppModule.GetValue(TransactionType.StockTransferExternalSJ), this._companyTag, ""))
                        {
                            _stcTransOutHd.FileNmbr = item.Number;
                        }
                         
                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockTransferExternalSJ);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCTransOutHd(_prmTransNmbr).FileNmbr;
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

                STCTransOutHd _stcTransOutHd = this.db.STCTransOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcTransOutHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STTransOutPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockTransferExternalSJ);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCTransOutHd(_prmTransNmbr).FileNmbr;
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

                STCTransOutHd _stcTransOutHd = this.db.STCTransOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcTransOutHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STTransOutUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockTransferExternalSJ);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCTransOutHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCTransOutHd(_prmTransNmbr).TransDate;
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

        #region STCTransOutDt
        public int RowsCountSTCTransOutDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _stcTransOutDt in this.db.STCTransOutDts
                                 where _stcTransOutDt.TransNmbr == _prmCode
                                 select _stcTransOutDt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCTransOutDt> GetListSTCTransOutDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCTransOutDt> _result = new List<STCTransOutDt>();

            try
            {
                var _query = (
                                from _stcTransOutDt in this.db.STCTransOutDts
                                where _stcTransOutDt.TransNmbr == _prmCode
                                orderby _stcTransOutDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcTransOutDt.TransNmbr,
                                    ProductCode = _stcTransOutDt.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode == _stcTransOutDt.ProductCode
                                                    select _msProduct.ProductName
                                                ).FirstOrDefault(),
                                    LocationCode = _stcTransOutDt.LocationCode,
                                    Qty = _stcTransOutDt.Qty,
                                    Unit = _stcTransOutDt.Unit,
                                    Remark = _stcTransOutDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCTransOutDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.LocationCode, _row.Qty, _row.Unit, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCTransOutDt GetSingleSTCTransOutDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        {
            STCTransOutDt _result = null;

            try
            {
                _result = this.db.STCTransOutDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode && _temp.LocationCode == _prmLocationCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCTransOutDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('|');

                    STCTransOutDt _STCTransOutDt = this.db.STCTransOutDts.Single(_temp => _temp.ProductCode == _tempSplit[0] && _temp.LocationCode == _tempSplit[1] && _temp.TransNmbr == _prmTransNo);

                    this.db.STCTransOutDts.DeleteOnSubmit(_STCTransOutDt);
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

        public bool AddSTCTransOutDt(STCTransOutDt _prmSTCTransOutDt)
        {
            bool _result = false;

            try
            {
                this.db.STCTransOutDts.InsertOnSubmit(_prmSTCTransOutDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCTransOutDt(STCTransOutDt _prmSTCTransOutDt)
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

        #region V_STTransferSJForRR
        public List<STCTransOutHd> GetSJNoFromVSTTransferSJForRR()
        {
            List<STCTransOutHd> _result = new List<STCTransOutHd>();

            try
            {
                var _query = (
                                from _vSTTransferSJForRR in this.db.V_STTransferSJForRRs
                                where ((_vSTTransferSJForRR.FileNmbr ?? "").Trim() == _vSTTransferSJForRR.FileNmbr.Trim())
                                select new
                                {
                                    FileNmbr = _vSTTransferSJForRR.FileNmbr,
                                    TransNmbr = _vSTTransferSJForRR.SJ_No
                                }
                             ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCTransOutHd(_row.TransNmbr, _row.FileNmbr));
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductFromVSTTransferSJForRR(string _prmTransReff)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _vSTTransferSJForRR in this.db.V_STTransferSJForRRs
                                where _vSTTransferSJForRR.SJ_No == _prmTransReff
                                select new
                                {
                                    ProductCode = _vSTTransferSJForRR.Product_Code,
                                    ProductName = _vSTTransferSJForRR.Product_Name
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
            }

            return _result;
        }

        public List<MsWrhsLocation> GetListLocationSrcFromVSTTransferSJForRR(string _prmTransReff, string _prmProductCode)
        {
            List<MsWrhsLocation> _result = new List<MsWrhsLocation>();

            try
            {
                var _query = (
                                from _vSTTransferSJForRR in this.db.V_STTransferSJForRRs
                                where _vSTTransferSJForRR.SJ_No == _prmTransReff && _vSTTransferSJForRR.Product_Code == _prmProductCode
                                select new
                                {
                                    WLocationCode = _vSTTransferSJForRR.Location_Code,
                                    WLocationName = _vSTTransferSJForRR.Location_Name
                                }
                             );

                foreach (var _row in _query)
                {
                    _result.Add(new MsWrhsLocation(_row.WLocationCode, _row.WLocationName));
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetWrhsSrcFromVSTTransferSJForRR(string _prmSJNo)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vSTTransferSJForRR in this.db.V_STTransferSJForRRs
                                where _vSTTransferSJForRR.SJ_No == _prmSJNo
                                select new
                                {
                                    WrhsAreaSrc = _vSTTransferSJForRR.Wrhs_Source
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = _row.WrhsAreaSrc;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetWrhsSrcSubledFromVSTTransferSJForRR(string _prmSJNo)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vSTTransferSJForRR in this.db.V_STTransferSJForRRs
                                where _vSTTransferSJForRR.SJ_No == _prmSJNo
                                select new
                                {
                                    WrhsSrcSubled = _vSTTransferSJForRR.Wrhs_Source_SubLed
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = _row.WrhsSrcSubled;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetWrhsDestSubledFromVSTTransferSJForRR(string _prmSJNo)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vSTTransferSJForRR in this.db.V_STTransferSJForRRs
                                where _vSTTransferSJForRR.SJ_No == _prmSJNo
                                select new
                                {
                                    WrhsDestSubled = _vSTTransferSJForRR.Wrhs_Destination_SubLed
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = _row.WrhsDestSubled;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetWrhsDestFromVSTTransferSJForRR(string _prmSJNo)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vSTTransferSJForRR in this.db.V_STTransferSJForRRs
                                where _vSTTransferSJForRR.SJ_No == _prmSJNo
                                select new
                                {
                                    WrhsAreaDest = _vSTTransferSJForRR.Wrhs_Destination
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = _row.WrhsAreaDest;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public decimal GetQtyFromVSTTransferSJForRR(string _prmSJNo, string _prmProductCode, string _prmLocationCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _vSTTransferSJForRR in this.db.V_STTransferSJForRRs
                                where _vSTTransferSJForRR.SJ_No == _prmSJNo
                                && _vSTTransferSJForRR.Product_Code == _prmProductCode
                                && _vSTTransferSJForRR.Location_Code == _prmLocationCode
                                select new
                                {
                                    Qty = _vSTTransferSJForRR.Qty
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = _row.Qty;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetUnitFromVSTTransferSJForRR(string _prmSJNo, string _prmProductCode, string _prmLocationCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vSTTransferSJForRR in this.db.V_STTransferSJForRRs
                                where _vSTTransferSJForRR.SJ_No == _prmSJNo
                                && _vSTTransferSJForRR.Product_Code == _prmProductCode
                                && _vSTTransferSJForRR.Location_Code == _prmLocationCode
                                select new
                                {
                                    Unit = _vSTTransferSJForRR.Unit
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
            }

            return _result;
        }

        public string GetFileNmbrFromSTCTransOut(string _prmReffNo)
        {
            string _result = "";

            try
            {
                _result = this.db.STCTransOutHds.Single(_temp => _temp.TransNmbr == _prmReffNo).FileNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~StockTransferExternalSJBL()
        {
        }
    }
}
