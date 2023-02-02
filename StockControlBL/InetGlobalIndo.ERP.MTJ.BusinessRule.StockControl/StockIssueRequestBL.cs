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
    public sealed class StockIssueRequestBL : Base
    {
        public StockIssueRequestBL()
        {
        }

        #region STCRequestHd

        public double RowsCountSTCRequestHd(string _prmCategory, string _prmKeyword)
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
                    from _stcRequestHd in this.db.STCRequestHds
                    where _stcRequestHd.FgType == AppModule.GetValue(TransactionType.StockIssueRequest)
                        && (SqlMethods.Like(_stcRequestHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like((_stcRequestHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && _stcRequestHd.Status != StockIssueRequestDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcRequestHd
                ).Count();

            _result = _query;
            return _result;

        }

        public List<STCRequestHd> GetListSTCRequestHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCRequestHd> _result = new List<STCRequestHd>();

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
                var _query = (
                                from _stcRequestHd in this.db.STCRequestHds
                                where _stcRequestHd.FgType == AppModule.GetValue(TransactionType.StockIssueRequest)
                                    && (SqlMethods.Like(_stcRequestHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_stcRequestHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _stcRequestHd.Status != StockIssueRequestDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcRequestHd.CreatedDate descending
                                select new
                                {
                                    TransNmbr = _stcRequestHd.TransNmbr,
                                    FileNmbr = _stcRequestHd.FileNmbr,
                                    TransDate = _stcRequestHd.TransDate,
                                    Status = _stcRequestHd.Status,
                                    OrgUnit = _stcRequestHd.OrgUnit,
                                    Description = (
                                                    from _msOrgUnit in this.db.Master_OrganizationUnits
                                                    where _msOrgUnit.OrgUnit == _stcRequestHd.OrgUnit
                                                    select _msOrgUnit.Description
                                                ).FirstOrDefault(),
                                    RequestBy = _stcRequestHd.RequestBy,
                                    Remark = _stcRequestHd.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCRequestHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.OrgUnit, _row.Description, _row.RequestBy, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCRequestHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCRequestHd _stcRequestHd = this.db.STCRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcRequestHd != null)
                    {
                        if (_stcRequestHd.Status != StockIssueRequestDataMapper.GetStatus(TransStatus.Posted))
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

        public STCRequestHd GetSingleSTCRequestHd(string _prmCode)
        {
            STCRequestHd _result = null;

            try
            {
                _result = this.db.STCRequestHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCRequestHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCRequestHd _stcRequestHd = this.db.STCRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcRequestHd != null)
                    {
                        if ((_stcRequestHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCRequestDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCRequestDts.DeleteAllOnSubmit(_query);

                            this.db.STCRequestHds.DeleteOnSubmit(_stcRequestHd);

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

        public bool DeleteMultiApproveSTCRequestHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCRequestHd _stcRequestHd = this.db.STCRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcRequestHd.Status == StockIssueRequestDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcRequestHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcRequestHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcRequestHd != null)
                    {
                        if ((_stcRequestHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCRequestDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCRequestDts.DeleteAllOnSubmit(_query);

                            this.db.STCRequestHds.DeleteOnSubmit(_stcRequestHd);

                            _result = true;
                        }
                        else if (_stcRequestHd.FileNmbr != "" && _stcRequestHd.Status == StockIssueRequestDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcRequestHd.Status = StockIssueRequestDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddSTCRequestHd(STCRequestHd _prmSTCRequestHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCRequestHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCRequestHds.InsertOnSubmit(_prmSTCRequestHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCRequestHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCRequestHd(STCRequestHd _prmSTCRequestHd)
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
                int _success = this.db.S_STRequestGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockIssueRequest);
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
                    int _success = this.db.S_STRequestApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        STCRequestHd _stcRequestHd = this.GetSingleSTCRequestHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcRequestHd.TransDate.Year, _stcRequestHd.TransDate.Month, AppModule.GetValue(TransactionType.StockIssueRequest), this._companyTag, ""))
                        {
                            _stcRequestHd.FileNmbr = item.Number;
                        }
                        
                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockIssueRequest);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCRequestHd(_prmTransNmbr).FileNmbr;
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

                STCRequestHd _stcRequestHd = this.db.STCRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _temp.FgType == StockIssueRequestDataMapper.GetFgType(StockRequestType.RequestStock));
                String _locked = _transCloseBL.IsExistAndLocked(_stcRequestHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STRequestPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockIssueRequest);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCRequestHd(_prmTransNmbr).FileNmbr;
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

                STCRequestHd _stcRequestHd = this.db.STCRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _temp.FgType == StockIssueRequestDataMapper.GetFgType(StockRequestType.RequestStock));
                String _locked = _transCloseBL.IsExistAndLocked(_stcRequestHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STRequestUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockIssueRequest);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCRequestHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCRequestHd(_prmTransNmbr).TransDate;
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

        public string Closing(string _prmTransNmbr, string _prmProduct, string _prmRemark, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCRequestHd _stcRequestHd = this.db.STCRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcRequestHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STRequestClosing(_prmTransNmbr, _prmProduct, _prmRemark, _prmuser, ref _errorMsg);

                    if (_errorMsg != "")
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
                _result = "Closing Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }
        #endregion

        #region STCRequestDt
        public int RowsCountSTCRequestDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _STCRequestDt in this.db.STCRequestDts
                                 where _STCRequestDt.TransNmbr == _prmCode
                                 select _STCRequestDt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCRequestDt> GetListSTCRequestDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCRequestDt> _result = new List<STCRequestDt>();

            try
            {
                var _query = (
                                from _STCRequestDt in this.db.STCRequestDts
                                where _STCRequestDt.TransNmbr == _prmCode
                                orderby _STCRequestDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _STCRequestDt.TransNmbr,
                                    ProductCode = _STCRequestDt.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode == _STCRequestDt.ProductCode
                                                    select _msProduct.ProductName
                                                ).FirstOrDefault(),
                                    Qty = _STCRequestDt.Qty,
                                    Unit = _STCRequestDt.Unit,
                                    DoneClosing = _STCRequestDt.DoneClosing,
                                    QtyClose = _STCRequestDt.QtyClose
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCRequestDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.Qty, _row.Unit, _row.DoneClosing, _row.QtyClose));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCRequestDt GetSingleSTCRequestDt(string _prmCode, string _prmProductCode)
        {
            STCRequestDt _result = null;

            try
            {
                //this.db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, new STCRequestDt());

                _result = this.db.STCRequestDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCRequestDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCRequestDt _STCRequestDt = this.db.STCRequestDts.Single(_temp => _temp.ProductCode == _prmCode[i] && _temp.TransNmbr == _prmTransNo);

                    this.db.STCRequestDts.DeleteOnSubmit(_STCRequestDt);
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

        public bool AddSTCRequestDt(STCRequestDt _prmSTCRequestDt)
        {
            bool _result = false;

            try
            {
                this.db.STCRequestDts.InsertOnSubmit(_prmSTCRequestDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCRequestDt(STCRequestDt _prmSTCRequestDt)
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

        public List<STCRequestHd> GetListReqNoBySIRForDDL()
        {
            List<STCRequestHd> _result = new List<STCRequestHd>();

            try
            {
                var _query = (
                                from _v_STReq in this.db.V_STRequestForIssues
                                where _v_STReq.FgType == AppModule.GetValue(TransactionType.StockIssueRequest)
                                    && (_v_STReq.FileNmbr ?? "").Trim() == _v_STReq.FileNmbr.Trim()
                                orderby _v_STReq.Request_No
                                select new
                                {
                                    RequestNo = _v_STReq.Request_No,
                                    FileNmbr = _v_STReq.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCRequestHd(_row.RequestNo, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductForDDLIssueSlipDt(string _prmReqNo)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _v_STReq in this.db.V_STRequestForIssues
                                where _v_STReq.Request_No.Trim().ToLower() == _prmReqNo.Trim().ToLower()
                                orderby _v_STReq.Product_Name
                                select new
                                {
                                    Product_Code = _v_STReq.Product_Code,
                                    Product_Name = _v_STReq.Product_Name
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.Product_Code, _row.Product_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public decimal GetQtyVSTRequestForIssuesByReqNo(string _prmReqNo, string _prmProductCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _v_STReqIssue in this.db.V_STRequestForIssues
                                where _v_STReqIssue.Request_No.Trim().ToLower() == _prmReqNo.Trim().ToLower()
                                && _v_STReqIssue.Product_Code == _prmProductCode
                                select new
                                {
                                    Qty = Convert.ToDecimal(_v_STReqIssue.Qty)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.Qty;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetUnitVSTRequestForIssuesByReqNo(string _prmReqNo, string _prmProductCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _v_STReqIssue in this.db.V_STRequestForIssues
                                where _v_STReqIssue.Request_No.Trim().ToLower() == _prmReqNo.Trim().ToLower()
                                && _v_STReqIssue.Product_Code == _prmProductCode
                                select new
                                {
                                    Unit = _v_STReqIssue.Unit
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.Unit;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        ~StockIssueRequestBL()
        {
        }
    }
}
