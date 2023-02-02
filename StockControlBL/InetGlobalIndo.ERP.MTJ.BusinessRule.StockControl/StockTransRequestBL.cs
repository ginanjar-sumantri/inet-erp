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
    public sealed class StockTransRequestBL : Base
    {
        public StockTransRequestBL()
        {
        }

        #region STCTransReqHd
        public double RowsCountSTCTransReqHd(string _prmCategory, string _prmKeyword)
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
                    from _stcTransReqHd in this.db.STCTransReqHds
                    where (SqlMethods.Like(_stcTransReqHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    && (SqlMethods.Like((_stcTransReqHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                    && _stcTransReqHd.Status != StockTransRequestDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcTransReqHd
                ).Count();

            _result = _query;

            return _result;
        }

        public List<STCTransReqHd> GetListSTCTransReqHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCTransReqHd> _result = new List<STCTransReqHd>();

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
                                from _stcTransReqHd in this.db.STCTransReqHds
                                where (SqlMethods.Like(_stcTransReqHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_stcTransReqHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _stcTransReqHd.Status != StockTransRequestDataMapper.GetStatus(TransStatus.Deleted)                                
                                orderby _stcTransReqHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _stcTransReqHd.TransNmbr,
                                    FileNmbr = _stcTransReqHd.FileNmbr,
                                    TransDate = _stcTransReqHd.TransDate,
                                    Status = _stcTransReqHd.Status,
                                    WrhsAreaSrc = _stcTransReqHd.WrhsAreaSrc,
                                    WrhsAreaDest = _stcTransReqHd.WrhsAreaDest,
                                    RequestBy = _stcTransReqHd.RequestBy
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCTransReqHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.WrhsAreaSrc, _row.WrhsAreaDest, _row.RequestBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCTransReqHd GetSingleSTCTransReqHd(string _prmCode)
        {
            STCTransReqHd _result = null;

            try
            {
                _result = this.db.STCTransReqHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCTransReqHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTransReqHd _stcTransReqHd = this.db.STCTransReqHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcTransReqHd != null)
                    {
                        if (_stcTransReqHd.Status != StockTransRequestDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiSTCTransReqHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTransReqHd _stcTransReqHd = this.db.STCTransReqHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcTransReqHd != null)
                    {
                        if ((_stcTransReqHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCTransReqDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCTransReqDts.DeleteAllOnSubmit(_query);

                            this.db.STCTransReqHds.DeleteOnSubmit(_stcTransReqHd);

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

        public bool DeleteMultiApproveSTCTransReqHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTransReqHd _stcTransReqHd = this.db.STCTransReqHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcTransReqHd.Status == StockTransRequestDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcTransReqHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcTransReqHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcTransReqHd != null)
                    {
                        if ((_stcTransReqHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCTransReqDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCTransReqDts.DeleteAllOnSubmit(_query);

                            this.db.STCTransReqHds.DeleteOnSubmit(_stcTransReqHd);

                            _result = true;
                        }
                        else if (_stcTransReqHd.FileNmbr != "" && _stcTransReqHd.Status == StockTransRequestDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcTransReqHd.Status = StockTransRequestDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddSTCTransReqHd(STCTransReqHd _prmSTCTransReqHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCTransReqHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCTransReqHds.InsertOnSubmit(_prmSTCTransReqHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCTransReqHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCTransReqHd(STCTransReqHd _prmSTCTransReqHd)
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

        public List<STCTransReqHd> GetListSTCTransReqForDDL()
        {
            List<STCTransReqHd> _result = new List<STCTransReqHd>();

            try
            {
                var _query = (
                                from _stcTransReqHd in this.db.STCTransReqHds
                                orderby _stcTransReqHd.TransNmbr ascending
                                select new
                                {
                                    TransNmbr = _stcTransReqHd.TransNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new STCTransReqHd(_row.TransNmbr));
                }
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

            try
            {
                this.db.S_STTransferReqGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockTransRequest);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
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
                    this.db.S_STTransferReqApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        STCTransReqHd _stcTransReqHd = this.GetSingleSTCTransReqHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_stcTransReqHd.TransDate.Year, _stcTransReqHd.TransDate.Month, AppModule.GetValue(TransactionType.StockTransRequest), this._companyTag, ""))
                        {
                            _stcTransReqHd.FileNmbr = _item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockTransRequest);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCTransReqHd(_prmTransNmbr).FileNmbr;
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

                STCTransReqHd _stcTransReqHd = this.db.STCTransReqHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcTransReqHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_STTransferReqPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockTransRequest);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCTransReqHd(_prmTransNmbr).FileNmbr;
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

                STCTransReqHd _stcTransReqHd = this.db.STCTransReqHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcTransReqHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_STTransferReqUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockTransRequest);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCTransReqHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCTransReqHd(_prmTransNmbr).TransDate;
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
                _result = "Unposting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string GetFileNmbrSTCTransReqHd(string _prmReqNo)
        {
            string _result = "";

            try
            {
                _result = this.db.STCTransReqHds.Single(_temp => _temp.TransNmbr == _prmReqNo).FileNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region STCTransReqDt
        public int RowsCountSTCTransReqDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _STCTransReqDt in this.db.STCTransReqDts
                                 where _STCTransReqDt.TransNmbr == _prmCode
                                 select _STCTransReqDt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public List<STCTransReqDt> GetListSTCTransReqDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCTransReqDt> _result = new List<STCTransReqDt>();

            try
            {
                var _query = (
                                from _stcTransReqDt in this.db.STCTransReqDts
                                where _stcTransReqDt.TransNmbr == _prmCode
                                orderby _stcTransReqDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcTransReqDt.TransNmbr,
                                    ProductCode = _stcTransReqDt.ProductCode,
                                    ProductName = (
                                                    from _product in this.db.MsProducts
                                                    where _stcTransReqDt.ProductCode == _product.ProductCode
                                                    select _product.ProductName
                                                  ).FirstOrDefault(),
                                    Qty = _stcTransReqDt.Qty,
                                    Unit = _stcTransReqDt.Unit,
                                    DoneClosing = _stcTransReqDt.DoneClosing,
                                    QtyClose = _stcTransReqDt.QtyClose
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCTransReqDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.Qty, _row.Unit, _row.DoneClosing, _row.QtyClose));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCTransReqDt GetSingleSTCTransReqDt(string _prmCode, string _prmProductCode)
        {
            STCTransReqDt _result = null;

            try
            {
                _result = this.db.STCTransReqDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCTransReqDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTransReqDt _stcTransReqDt = this.db.STCTransReqDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _prmCode[i].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    this.db.STCTransReqDts.DeleteOnSubmit(_stcTransReqDt);
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

        public bool AddSTCTransReqDt(STCTransReqDt _prmSTCTransReqDt)
        {
            bool _result = false;

            try
            {
                this.db.STCTransReqDts.InsertOnSubmit(_prmSTCTransReqDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCTransReqDt(STCTransReqDt _prmSTCTransReqDt)
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

        public string Close(string _prmTransNmbr, string _prmProduct, string _prmRemark, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_STTransferReqClosing(_prmTransNmbr, _prmProduct, _prmRemark, _prmuser, ref _errorMsg);

                if (_errorMsg != "")
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Close Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        #endregion

        #region V_STTransferReqForSJ
        public List<STCTransReqHd> GetRequestNoFromVSTTransferReqForSJ()
        {
            List<STCTransReqHd> _result = new List<STCTransReqHd>();

            try
            {
                var _query = (
                                from _vSTTransferReqForSJ in this.db.V_STTransferReqForSJs
                                where ((_vSTTransferReqForSJ.FileNmbr ?? "").Trim() == _vSTTransferReqForSJ.FileNmbr.Trim())
                                select new
                                {
                                    Request_No = _vSTTransferReqForSJ.Request_No,
                                    FileNmbr = _vSTTransferReqForSJ.FileNmbr
                                }
                             ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCTransReqHd(_row.Request_No, _row.FileNmbr));
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetWrhsAreaSrcFromVSTTransferReqForSJ(string _prmRequestNo)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vSTTransferReqForSJ in this.db.V_STTransferReqForSJs
                                where _vSTTransferReqForSJ.Request_No == _prmRequestNo
                                select new
                                {
                                    WrhsAreaSrc = _vSTTransferReqForSJ.Wrhs_Area_Source
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

        public string GetWrhsAreaDestFromVSTTransferReqForSJ(string _prmRequestNo)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vSTTransferReqForSJ in this.db.V_STTransferReqForSJs
                                where _vSTTransferReqForSJ.Request_No == _prmRequestNo
                                select new
                                {
                                    WrhsAreaDest = _vSTTransferReqForSJ.Wrhs_Area_Destination
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

        public decimal GetQtyFromVSTTransferReqForSJ(string _prmRequestNo, string _prmProductCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _vSTTransferReqForSJ in this.db.V_STTransferReqForSJs
                                where _vSTTransferReqForSJ.Request_No == _prmRequestNo && _vSTTransferReqForSJ.Product_Code == _prmProductCode
                                select new
                                {
                                    Qty = (_vSTTransferReqForSJ.Qty == null) ? 0 : Convert.ToDecimal(_vSTTransferReqForSJ.Qty)
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

        public string GetUnitFromVSTTransferReqForSJ(string _prmRequestNo, string _prmProductCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vSTTransferReqForSJ in this.db.V_STTransferReqForSJs
                                where _vSTTransferReqForSJ.Request_No == _prmRequestNo && _vSTTransferReqForSJ.Product_Code == _prmProductCode
                                select new
                                {
                                    Unit = _vSTTransferReqForSJ.Unit
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

        public string GetFileNmbrVSTTransferReqForSJ(string _prmReqNo)
        {
            string _result = "";

            try
            {
                _result = this.db.V_STTransferReqForSJs.Single(_temp => _temp.Request_No == _prmReqNo).FileNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~StockTransRequestBL()
        {
        }
    }
}
