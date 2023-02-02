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
    public sealed class RequestSalesReturBL : Base
    {
        public RequestSalesReturBL()
        {
        }

        #region MKTReqReturHd
        public double RowsCountMKTReqReturHd(string _prmCategory, string _prmKeyword)
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
                    from _mktReqReturHd in this.db.MKTReqReturHds
                    where (SqlMethods.Like(_mktReqReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like((_mktReqReturHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && _mktReqReturHd.Status != RequestSalesReturDataMapper.GetStatus(TransStatus.Deleted)
                    select _mktReqReturHd

                ).Count();

            _result = _query;
            return _result;
        }

        public List<MKTReqReturHd> GetListMKTReqReturHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MKTReqReturHd> _result = new List<MKTReqReturHd>();

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
                                from _mktReqReturHd in this.db.MKTReqReturHds
                                where (SqlMethods.Like(_mktReqReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_mktReqReturHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _mktReqReturHd.Status != RequestSalesReturDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _mktReqReturHd.TransDate descending
                                select new
                                {
                                    TransNmbr = _mktReqReturHd.TransNmbr,
                                    FileNmbr = _mktReqReturHd.FileNmbr,
                                    TransDate = _mktReqReturHd.TransDate,
                                    CurrCode = _mktReqReturHd.CurrCode,
                                    Status = _mktReqReturHd.Status,
                                    CustCode = _mktReqReturHd.CustCode,
                                    CustName = (
                                                    from _msCust in this.db.MsCustomers
                                                    where _msCust.CustCode == _mktReqReturHd.CustCode
                                                    select _msCust.CustName
                                                ).FirstOrDefault()
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

                if (_prmOrderBy == "Customer")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.CustCode)) : (_query.OrderByDescending(a => a.CustCode));

                if (_prmOrderBy == "Currency")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.CurrCode)) : (_query.OrderByDescending(a => a.CurrCode));

                _query = _query.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MKTReqReturHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.CustCode, _row.CustName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MKTReqReturHd> GetListReqReturForDDL(string _prmCustCode)
        {
            List<MKTReqReturHd> _result = new List<MKTReqReturHd>();

            try
            {
                var _query = (
                                from _mktReqReturForRR in this.db.V_MKTReqReturForRRs
                                where ((_mktReqReturForRR.FileNmbr ?? "").Trim() == _mktReqReturForRR.FileNmbr.Trim())
                                    && _mktReqReturForRR.Customer_Code == _prmCustCode
                                orderby _mktReqReturForRR.Request_No ascending
                                select new
                                {
                                    FileNmbr = _mktReqReturForRR.FileNmbr,
                                    Request_No = _mktReqReturForRR.Request_No
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MKTReqReturHd(_row.Request_No, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MKTReqReturHd GetSingleMKTReqReturHd(string _prmCode)
        {
            MKTReqReturHd _result = null;

            try
            {
                _result = this.db.MKTReqReturHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiMKTReqReturHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MKTReqReturHd _mktReqReturHd = this.db.MKTReqReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_mktReqReturHd != null)
                    {
                        if ((_mktReqReturHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.MKTReqReturDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.MKTReqReturDts.DeleteAllOnSubmit(_query);

                            this.db.MKTReqReturHds.DeleteOnSubmit(_mktReqReturHd);

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

        public string AddMKTReqReturHd(MKTReqReturHd _prmMKTReqReturHd, List<MKTReqReturDt> _prmListMKTReqReturDt)
        {
            string _result = "";

            try
            {
                List<MKTReqReturDt> _listDetail = new List<MKTReqReturDt>();

                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmMKTReqReturHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.MKTReqReturHds.InsertOnSubmit(_prmMKTReqReturHd);

                if (_prmListMKTReqReturDt.Count > 0)
                {
                    foreach (MKTReqReturDt _item in _prmListMKTReqReturDt)
                    {
                        MKTReqReturDt _mktReqReturDt = new MKTReqReturDt();

                        _mktReqReturDt.TransNmbr = _prmMKTReqReturHd.TransNmbr;
                        _mktReqReturDt.ProductCode = _item.ProductCode;
                        _mktReqReturDt.Qty = _item.Qty;
                        _mktReqReturDt.PriceForex = _item.PriceForex;
                        _mktReqReturDt.AmountForex = _item.AmountForex;
                        _mktReqReturDt.Unit = _item.Unit;
                        _mktReqReturDt.Remark = _item.Remark;

                        _listDetail.Add(new MKTReqReturDt(_mktReqReturDt.TransNmbr, _mktReqReturDt.ProductCode, _mktReqReturDt.Qty, _mktReqReturDt.Unit, _mktReqReturDt.PriceForex, _mktReqReturDt.AmountForex));
                    }

                    this.db.MKTReqReturDts.InsertAllOnSubmit(_listDetail);
                }

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmMKTReqReturHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditMKTReqReturHd(MKTReqReturHd _prmMKTReqReturHd)
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

            try
            {
                int _success = this.db.S_MKTReqReturGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.RequestSalesRetur);
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
                    this.db.S_MKTReqReturApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        MKTReqReturHd _mktReqReturHd = this.GetSingleMKTReqReturHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_mktReqReturHd.TransDate.Year, _mktReqReturHd.TransDate.Month, AppModule.GetValue(TransactionType.RequestSalesRetur), this._companyTag, ""))
                        {
                            _mktReqReturHd.FileNmbr = item.Number;
                        }

                        
                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.RequestSalesRetur);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleMKTReqReturHd(_prmTransNmbr).FileNmbr;
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

                MKTReqReturHd _mktReqReturHd = this.db.MKTReqReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_mktReqReturHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_MKTReqReturPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.RequestSalesRetur);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleMKTReqReturHd(_prmTransNmbr).FileNmbr;
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

                MKTReqReturHd _mktReqReturHd = this.db.MKTReqReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_mktReqReturHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_MKTReqReturUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockIssueSlip);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleMKTReqReturHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleMKTReqReturHd(_prmTransNmbr).TransDate;
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

        public string Closing(string _prmTransNmbr, string _prmProduct, string _prmRemark, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                MKTReqReturHd _mktReqReturHd = this.db.MKTReqReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_mktReqReturHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_MKTReqReturClosing(_prmTransNmbr, _prmProduct, _prmRemark, _prmuser, ref _errorMsg);

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

        public string GetFileNmbrMKTReqReturHd (string _prmReqNo)
        {
            string _result = "";

            try
            {
                _result = this.db.MKTReqReturHds.Single(_temp => _temp.TransNmbr == _prmReqNo).FileNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleMKTReqReturHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MKTReqReturHd _mktReqReturHd = this.db.MKTReqReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_mktReqReturHd != null)
                    {
                        if (_mktReqReturHd.Status != RequestSalesReturDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveMMKTReqReturHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MKTReqReturHd _mktReqReturHd = this.db.MKTReqReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_mktReqReturHd.Status == RequestSalesReturDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _mktReqReturHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _mktReqReturHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_mktReqReturHd != null)
                    {
                        if ((_mktReqReturHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.MKTReqReturDts
                                          where _detail.TransNmbr == _prmCode[i]
                                          select _detail);

                            this.db.MKTReqReturDts.DeleteAllOnSubmit(_query);

                            this.db.MKTReqReturHds.DeleteOnSubmit(_mktReqReturHd);

                            _result = true;
                        }
                        else if (_mktReqReturHd.FileNmbr != "" && _mktReqReturHd.Status == RequestSalesReturDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _mktReqReturHd.Status = RequestSalesReturDataMapper.GetStatus(TransStatus.Deleted);
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

        #region MKTReqReturDt
        public int RowsCountMKTReqReturDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _MKTReqReturDt in this.db.MKTReqReturDts
                                 where _MKTReqReturDt.TransNmbr == _prmCode
                                 select _MKTReqReturDt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MKTReqReturDt> GetListMKTReqReturDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<MKTReqReturDt> _result = new List<MKTReqReturDt>();

            try
            {
                var _query = (
                                from _MKTReqReturDt in this.db.MKTReqReturDts
                                where _MKTReqReturDt.TransNmbr == _prmCode
                                orderby _MKTReqReturDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _MKTReqReturDt.TransNmbr,
                                    ProductCode = _MKTReqReturDt.ProductCode,
                                    ProductName = (
                                                        from _msProduct in this.db.MsProducts
                                                        where _MKTReqReturDt.ProductCode == _msProduct.ProductCode
                                                        select _msProduct.ProductName
                                                   ).FirstOrDefault(),
                                    Qty = _MKTReqReturDt.Qty,
                                    Unit = _MKTReqReturDt.Unit,
                                    PriceForex = _MKTReqReturDt.PriceForex,
                                    AmountForex = _MKTReqReturDt.AmountForex,
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MKTReqReturDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.Qty, _row.Unit, _row.PriceForex, _row.AmountForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MKTReqReturDt GetSingleMKTReqReturDt(string _prmCode, string _prmProductCode)
        {
            MKTReqReturDt _result = null;

            try
            {
                _result = this.db.MKTReqReturDts.Single(_temp => _temp.TransNmbr.Trim() == _prmCode.Trim() && _temp.ProductCode.Trim() == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiMKTReqReturDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            MKTReqReturHd _mktReqReturHd = new MKTReqReturHd();

            decimal _total = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MKTReqReturDt _mktReqReturDt = this.db.MKTReqReturDts.Single(_temp => _temp.ProductCode == _prmCode[i] && _temp.TransNmbr == _prmTransNo);

                    this.db.MKTReqReturDts.DeleteOnSubmit(_mktReqReturDt);
                }

                var _query = (
                               from _mktReqReturDt2 in this.db.MKTReqReturDts
                               where !(
                                           from _code in _prmCode
                                           select _code
                                       ).Contains(_mktReqReturDt2.ProductCode)
                                       && _mktReqReturDt2.TransNmbr == _prmTransNo
                               group _mktReqReturDt2 by _mktReqReturDt2.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }

                _mktReqReturHd = this.db.MKTReqReturHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _mktReqReturHd.TotalForex = _total;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddMKTReqReturDt(MKTReqReturDt _prmMKTReqReturDt)
        {
            bool _result = false;

            MKTReqReturHd _mktReqReturHd = new MKTReqReturHd();

            decimal _total = 0;

            try
            {
                var _query = (
                               from _mktReqReturDt in this.db.MKTReqReturDts
                               where !(
                                           from _mktReqReturDt2 in this.db.MKTReqReturDts
                                           where _mktReqReturDt2.ProductCode == _prmMKTReqReturDt.ProductCode && _mktReqReturDt2.TransNmbr == _prmMKTReqReturDt.TransNmbr
                                           select _mktReqReturDt2.ProductCode
                                       ).Contains(_mktReqReturDt.ProductCode)
                                       && _mktReqReturDt.TransNmbr == _prmMKTReqReturDt.TransNmbr
                               group _mktReqReturDt by _mktReqReturDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }


                _mktReqReturHd = this.db.MKTReqReturHds.Single(_fa => _fa.TransNmbr == _prmMKTReqReturDt.TransNmbr);

                _mktReqReturHd.TotalForex = Convert.ToDecimal(_total + _prmMKTReqReturDt.AmountForex);

                this.db.MKTReqReturDts.InsertOnSubmit(_prmMKTReqReturDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditMKTReqReturDt(MKTReqReturDt _prmMKTReqReturDt)
        {
            bool _result = false;

            MKTReqReturHd _mktReqReturHd = new MKTReqReturHd();

            decimal _total = 0;

            try
            {
                var _query = (
                              from _mktReqReturDt in this.db.MKTReqReturDts
                              where !(
                                          from _mktReqReturDt2 in this.db.MKTReqReturDts
                                          where _mktReqReturDt2.ProductCode == _prmMKTReqReturDt.ProductCode && _mktReqReturDt2.TransNmbr == _prmMKTReqReturDt.TransNmbr
                                          select _mktReqReturDt2.ProductCode
                                      ).Contains(_mktReqReturDt.ProductCode)
                                      && _mktReqReturDt.TransNmbr == _prmMKTReqReturDt.TransNmbr
                              group _mktReqReturDt by _mktReqReturDt.TransNmbr into _grp
                              select new
                              {
                                  AmountForex = _grp.Sum(a => a.AmountForex)
                              }
                            );

                foreach (var _obj in _query)
                {
                    _total = _obj.AmountForex;
                }


                _mktReqReturHd = this.db.MKTReqReturHds.Single(_fa => _fa.TransNmbr == _prmMKTReqReturDt.TransNmbr);

                _mktReqReturHd.TotalForex = _total + _prmMKTReqReturDt.AmountForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductForDDL(string _prmReqNo)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _mktReqReturForRR in this.db.V_MKTReqReturForRRs
                                where _mktReqReturForRR.Request_No == _prmReqNo
                                select new
                                {
                                    ProductNo = _mktReqReturForRR.Product_Code,
                                    ProductName = _mktReqReturForRR.Product_Name
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductNo = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductNo, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~RequestSalesReturBL()
        {
        }
    }
}
