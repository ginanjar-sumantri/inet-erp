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
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class BillOfLadingBL : Base
    {
        public BillOfLadingBL()
        {

        }

        # region STCSJHd
        public double RowsCountSTCSJHd(string _prmCategory, string _prmKeyword)
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
                    from _stcSJHd in this.db.STCSJHds
                    where (SqlMethods.Like(_stcSJHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like((_stcSJHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && _stcSJHd.Status != BillOfLadingDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcSJHd
                ).Count();

            _result = _query;
            return _result;
        }

        public string AddSTCSJHd(STCSJHd _prmSTCSJHd)
        {
            string _result = "";

            try
            {
                List<STCSJDt> _listDetail = new List<STCSJDt>();

                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCSJHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCSJHds.InsertOnSubmit(_prmSTCSJHd);

                //if (_prmListSTCSJDt.Count > 0)
                //{
                //    foreach (STCSJDt _item in _prmListSTCSJDt)
                //    {
                //        STCSJDt _stcSJDt = new STCSJDt();

                //        _stcSJDt.TransNmbr = _prmSTCSJHd.TransNmbr;
                //        _stcSJDt.DONo = _item.DONo;
                //        _stcSJDt.ProductCode = _item.ProductCode;
                //        _stcSJDt.LocationCode = _item.LocationCode;
                //        _stcSJDt.Qty = _item.Qty;
                //        _stcSJDt.Unit = _item.Unit;
                //        _stcSJDt.Remark = _item.Remark;
                //        _stcSJDt.QtyLoss = 0;
                //        _stcSJDt.QtyRetur = 0;
                //        _stcSJDt.QtyReceive = _item.Qty;

                //        _listDetail.Add(new STCSJDt(_stcSJDt.TransNmbr, _stcSJDt.ProductCode, _stcSJDt.LocationCode, _stcSJDt.DONo, _stcSJDt.Qty, _stcSJDt.Unit, _stcSJDt.Remark, _stcSJDt.QtyLoss, _stcSJDt.QtyRetur, _stcSJDt.QtyReceive));
                //    }

                //    this.db.STCSJDts.InsertAllOnSubmit(_listDetail);
                //}

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCSJHd.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddSTCSJHd(STCSJHd _prmSTCSJHd, List<STCSJDt> _prmListSTCSJDt)
        {
            string _result = "";

            try
            {
                List<STCSJDt> _listDetail = new List<STCSJDt>();

                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCSJHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCSJHds.InsertOnSubmit(_prmSTCSJHd);

                if (_prmListSTCSJDt.Count > 0)
                {
                    foreach (STCSJDt _item in _prmListSTCSJDt)
                    {
                        STCSJDt _stcSJDt = new STCSJDt();

                        _stcSJDt.TransNmbr = _prmSTCSJHd.TransNmbr;
                        _stcSJDt.DONo = _item.DONo;
                        _stcSJDt.WrhsCode = _item.WrhsCode;
                        _stcSJDt.WrhsFgSubLed = Convert.ToChar(_item.WrhsFgSubLed);
                        _stcSJDt.WrhsSubLed = _item.WrhsSubLed;
                        _stcSJDt.ProductCode = _item.ProductCode;
                        _stcSJDt.LocationCode = _item.LocationCode;
                        _stcSJDt.Qty = _item.Qty;
                        _stcSJDt.Unit = _item.Unit;
                        _stcSJDt.Remark = _item.Remark;
                        _stcSJDt.QtyLoss = 0;
                        _stcSJDt.QtyRetur = 0;
                        _stcSJDt.QtyReceive = _item.Qty;

                        _listDetail.Add(new STCSJDt(_stcSJDt.TransNmbr, _stcSJDt.ProductCode, _stcSJDt.LocationCode, _stcSJDt.WrhsCode, Convert.ToChar(_stcSJDt.WrhsFgSubLed), _stcSJDt.WrhsSubLed, _stcSJDt.DONo, _stcSJDt.Qty, _stcSJDt.Unit, _stcSJDt.Remark, _stcSJDt.QtyLoss, Convert.ToDecimal(_stcSJDt.QtyRetur), _stcSJDt.QtyReceive));
                    }

                    this.db.STCSJDts.InsertAllOnSubmit(_listDetail);
                }

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCSJHd.TransNmbr.ToString();
            }
            catch (Exception ex)
            {
                new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "AddSTCSJHd", "StockControl");
            }

            return _result;
        }

        public List<STCSJHd> GetListSTCSJHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<STCSJHd> _result = new List<STCSJHd>();

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
                                from _stcSJHd in this.db.STCSJHds
                                where (SqlMethods.Like(_stcSJHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_stcSJHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _stcSJHd.Status != BillOfLadingDataMapper.GetStatus(TransStatus.Deleted)
                                select new
                                {
                                    TransNmbr = _stcSJHd.TransNmbr,
                                    FileNmbr = _stcSJHd.FileNmbr,
                                    TransDate = _stcSJHd.TransDate,
                                    Status = _stcSJHd.Status,
                                    SONo = new SalesOrderBL().GetFileNmbrMKTSOHd(_stcSJHd.SONo),
                                    CustCode = _stcSJHd.CustCode,
                                    CustName = (
                                                    from _msCustomer in this.db.MsCustomers
                                                    where _stcSJHd.CustCode == _msCustomer.CustCode
                                                    select _msCustomer.CustName
                                                ).FirstOrDefault(),
                                    WrhsCode = _stcSJHd.WrhsCode,
                                    WrhsName = (
                                                    from _msWarehouse in this.db.MsWarehouses
                                                    where _stcSJHd.WrhsCode == _msWarehouse.WrhsCode
                                                    select _msWarehouse.WrhsName
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
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.CustName)) : (_query.OrderByDescending(a => a.CustName));

                if (_prmOrderBy == "SO No.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.SONo)) : (_query.OrderByDescending(a => a.SONo));

                _query = _query.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);


                foreach (var _row in _query)
                {
                    _result.Add(new STCSJHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.SONo, _row.CustCode, _row.CustName, _row.WrhsCode, _row.WrhsName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCSJHd GetSingleSTCSJHd(string _prmTransNmbr)
        {
            STCSJHd _result = null;

            try
            {
                _result = this.db.STCSJHds.Single(_temp => _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCSJHd(STCSJHd _prmSTCSJHd)
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

        public bool DeleteMultiSTCSJHd(string[] _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTransNmbr.Length; i++)
                {
                    STCSJHd _stcSJHd = this.db.STCSJHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower());

                    if (_stcSJHd != null)
                    {
                        if ((_stcSJHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCSJDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower()
                                          select _detail);

                            var _query1 = (from _detail1 in this.db.STCSJDtReferences
                                           where _detail1.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower()
                                           select _detail1);

                            this.db.STCSJDtReferences.DeleteAllOnSubmit(_query1);

                            this.db.STCSJDts.DeleteAllOnSubmit(_query);

                            this.db.STCSJHds.DeleteOnSubmit(_stcSJHd);

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

        public string GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_STCSJGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = new Guid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.BillOfLading);
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
                    int _success = this.db.S_STCSJApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        STCSJHd _stcSJHd = this.GetSingleSTCSJHd(_prmTransNmbr);

                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcSJHd.TransDate.Year, _stcSJHd.TransDate.Month, AppModule.GetValue(TransactionType.BillOfLading), this._companyTag, ""))
                        {
                            _stcSJHd.FileNmbr = item.Number;
                        }

                        this.db.SubmitChanges();

                        _scope.Complete();

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = new Guid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.BillOfLading);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCSJHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
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

                STCSJHd _stcSJHd = this.db.STCSJHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcSJHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STCSJPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = new Guid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.BillOfLading);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCSJHd(_prmTransNmbr).FileNmbr;
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

                STCSJHd _stcSJHd = this.db.STCSJHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcSJHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STCSJUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = new Guid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.BillOfLading);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCSJHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCSJHd(_prmTransNmbr).TransDate;
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

        public List<STCSJHd> GetSJFileNmbrForDDL(string _prmCustCode)
        {
            List<STCSJHd> _result = new List<STCSJHd>();

            try
            {
                var _query = (
                                from _stcSJHd in this.db.STCSJHds
                                where ((_stcSJHd.FileNmbr ?? "").Trim() == _stcSJHd.FileNmbr.Trim())
                                    && _stcSJHd.CustCode.Trim().ToLower() == _prmCustCode.Trim().ToLower()
                                select new
                                {
                                    TransNmbr = _stcSJHd.TransNmbr,
                                    FileNmbr = _stcSJHd.FileNmbr
                                }
                             );

                foreach (var _row in _query)
                {
                    _result.Add(new STCSJHd(_row.TransNmbr, _row.FileNmbr));
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFileNmbrFromSTCSJHd(string _prmTransNmbr)
        {
            string _result = "";

            try
            {
                _result = this.db.STCSJHds.Single(_temp => _temp.TransNmbr == _prmTransNmbr).FileNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCSJHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCSJHd _mktSTCSJHd = this.db.STCSJHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_mktSTCSJHd != null)
                    {
                        if (_mktSTCSJHd.Status != BillOfLadingDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveSTCSJHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCSJHd _mktSTCSJHd = this.db.STCSJHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_mktSTCSJHd.Status == BillOfLadingDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _mktSTCSJHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _mktSTCSJHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_mktSTCSJHd != null)
                    {
                        if ((_mktSTCSJHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCSJDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            var _query1 = (from _detail1 in this.db.STCSJDtReferences
                                           where _detail1.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail1);

                            this.db.STCSJDtReferences.DeleteAllOnSubmit(_query1);

                            this.db.STCSJDts.DeleteAllOnSubmit(_query);

                            this.db.STCSJHds.DeleteOnSubmit(_mktSTCSJHd);

                            _result = true;
                        }
                        else if (_mktSTCSJHd.FileNmbr != "" && _mktSTCSJHd.Status == BillOfLadingDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _mktSTCSJHd.Status = BillOfLadingDataMapper.GetStatus(TransStatus.Deleted);
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

        # region STCSJDt

        public int RowsCountSTCSJDt(string _prmTransNmbr)
        {
            int _result = 0;
            try
            {

                _result = this.db.STCSJDts.Where(_temp => _temp.TransNmbr == _prmTransNmbr).Count();

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<STCSJDt> GetListSTCSJDt(int _prmReqPage, int _prmPageSize, string _prmTransNmbr)
        {
            List<STCSJDt> _result = new List<STCSJDt>();

            try
            {
                var _query =
                            (
                                from _stcSJDt in this.db.STCSJDts
                                where _stcSJDt.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    TransNmbr = _stcSJDt.TransNmbr,
                                    ProductCode = _stcSJDt.ProductCode,
                                    ProductName = (
                                                        from _msProduct in this.db.MsProducts
                                                        where _msProduct.ProductCode == _stcSJDt.ProductCode
                                                        select _msProduct.ProductName
                                                   ).FirstOrDefault(),
                                    ItemID = _stcSJDt.ItemID,
                                    WrhsCode = _stcSJDt.WrhsCode,
                                    WrhsSubLed = _stcSJDt.WrhsSubLed,
                                    LocationCode = _stcSJDt.LocationCode,
                                    LocationName = (
                                                        from _msWrhsLoc in this.db.MsWrhsLocations
                                                        where _msWrhsLoc.WLocationCode == _stcSJDt.LocationCode
                                                        select _msWrhsLoc.WLocationName
                                                   ).FirstOrDefault(),
                                    DONo = _stcSJDt.DONo,
                                    Qty = _stcSJDt.Qty,
                                    Unit = _stcSJDt.Unit,
                                    UnitName = (
                                                        from _msUnit in this.db.MsUnits
                                                        where _msUnit.UnitCode == _stcSJDt.Unit
                                                        select _msUnit.UnitName
                                                   ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCSJDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.ItemID, _row.WrhsCode, _row.WrhsSubLed, _row.LocationCode, _row.LocationName, _row.DONo, _row.Qty, _row.Unit, _row.UnitName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCSJDt> GetListSTCSJDt(string _prmTransNmbr)
        {
            List<STCSJDt> _result = new List<STCSJDt>();

            try
            {
                var _query =
                            (
                                from _stcSJDt in this.db.STCSJDts
                                where _stcSJDt.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    TransNmbr = _stcSJDt.TransNmbr,
                                    ProductCode = _stcSJDt.ProductCode,
                                    ProductName = (
                                                        from _msProduct in this.db.MsProducts
                                                        where _msProduct.ProductCode == _stcSJDt.ProductCode
                                                        select _msProduct.ProductName
                                                   ).FirstOrDefault(),
                                    LocationCode = _stcSJDt.LocationCode,
                                    LocationName = (
                                                        from _msWrhsLoc in this.db.MsWrhsLocations
                                                        where _msWrhsLoc.WLocationCode == _stcSJDt.LocationCode
                                                        select _msWrhsLoc.WLocationName
                                                   ).FirstOrDefault(),
                                    DONo = _stcSJDt.DONo,
                                    Qty = _stcSJDt.Qty,
                                    Unit = _stcSJDt.Unit,
                                    UnitName = (
                                                        from _msUnit in this.db.MsUnits
                                                        where _msUnit.UnitCode == _stcSJDt.Unit
                                                        select _msUnit.UnitName
                                                   ).FirstOrDefault()
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new STCSJDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.LocationCode, _row.LocationName, _row.DONo, _row.Qty, _row.Unit, _row.UnitName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCSJDt GetSingleSTCSJDt(string _prmTransNmbr, string _prmDONo, string _prmProductCode, string _prmLocationCode)
        {
            STCSJDt _result = null;

            try
            {
                _result = this.db.STCSJDts.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.DONo == _prmDONo && _temp.ProductCode == _prmProductCode && _temp.LocationCode == _prmLocationCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCSJDt GetSingleSTCSJDtSO(string _prmTransNmbr, string _prmDONo, string _prmProductCode, string _prmWrhsCode, string _prmLocationCode)
        {
            STCSJDt _result = null;

            try
            {
                _result = this.db.STCSJDts.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.DONo == _prmDONo && _temp.ProductCode == _prmProductCode && _temp.WrhsCode == _prmWrhsCode && _temp.LocationCode == _prmLocationCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCSJDt(STCSJDt _prmSTCSJDt)
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

        public bool DeleteMultiSTCSJDt(string _prmTransNmbr, string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _code = _prmCode[i].Split('|');

                    STCSJDt _stcSJDt = this.db.STCSJDts.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.DONo == _code[0] && _temp.ProductCode == _code[1] && _temp.LocationCode == _code[2]);

                    this.db.STCSJDts.DeleteOnSubmit(_stcSJDt);
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

        public bool AddSTCSJDt(STCSJDt _prmSTCSJDt)
        {
            bool _result = false;

            try
            {
                this.db.STCSJDts.InsertOnSubmit(_prmSTCSJDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public decimal GetSingleQtyFromSTCSJDt(string _prmSJNo, string _prmProductCode)
        {
            decimal _result = 0;

            try
            {
                var _query =
                            (
                                from _stcSJDt in this.db.STCSJDts
                                where _stcSJDt.TransNmbr.ToLower() == _prmSJNo.ToLower() && _stcSJDt.ProductCode == _prmProductCode
                                select new
                                {
                                    Qty = _stcSJDt.Qty
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
        #endregion

        public STCSJDt GetSingleProductFromV_STSJForCI(string _prmSJNo, string _prmProductCode)
        {
            STCSJDt _result = new STCSJDt();

            try
            {
                var _query =
                            (
                                from _vSTSJForCI in this.db.V_STSJForCIs
                                where _vSTSJForCI.SJ_No.ToLower() == _prmSJNo.ToLower() && _vSTSJForCI.Product_Code == _prmProductCode
                                select new
                                {
                                    SONo = _vSTSJForCI.SO_No,
                                    Qty = (_vSTSJForCI.Qty == null) ? 0 : Convert.ToDecimal(_vSTSJForCI.Qty),
                                    Unit = _vSTSJForCI.Unit
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.SONo = _row.SONo;
                    _result.Qty = _row.Qty;
                    _result.Unit = _row.Unit;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCSJHd> GetListDDLV_STSJForCI()
        {
            List<STCSJHd> _result = new List<STCSJHd>();

            try
            {
                var _query =
                            (
                                from _vSTSJForCI in this.db.V_STSJForCIs
                                where (_vSTSJForCI.FileNmbr ?? "").Trim() == _vSTSJForCI.FileNmbr.Trim()
                                select new
                                {
                                    SJ_No = _vSTSJForCI.SJ_No,
                                    FileNmbr = _vSTSJForCI.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCSJHd(_row.SJ_No, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListDDLProductFromV_STSJForCI(string _prmSJNo)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query =
                            (
                                from _vSTSJForCI in this.db.V_STSJForCIs
                                where _vSTSJForCI.SJ_No.ToLower() == _prmSJNo.ToLower()
                                select new
                                {
                                    Product_Code = _vSTSJForCI.Product_Code,
                                    Product_Name = _vSTSJForCI.Product_Name
                                }
                            );

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

        public string GetSingleBOLReferenceType()
        {
            string _result = "";

            try
            {
                _result = this.db.CompanyConfigurations.Single(_temp => _temp.ConfigCode == CompanyConfigureDataMapper.GetCompanyConfigure(CompanyConfigure.BOLReferenceType)).SetValue;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddSTCSJDtReference(STCSJDtReference _prmSTCSJDtReference)
        {
            bool _result = false;

            try
            {
                this.db.STCSJDtReferences.InsertOnSubmit(_prmSTCSJDtReference);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCSJDtReference> GetListSTCSjDtReference(string _prmTransNmbr)
        {
            List<STCSJDtReference> _result = new List<STCSJDtReference>();

            try
            {
                var _query =
                            (
                                from _stcSjReference in this.db.STCSJDtReferences
                                where _stcSjReference.TransNmbr == _prmTransNmbr
                                select _stcSjReference
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

        public bool GenerateSTCSJDt(string _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                var _delSTCSjDt = (
                                from _stcSjDt in this.db.STCSJDts
                                where _stcSjDt.TransNmbr == _prmTransNmbr
                                select _stcSjDt
                               );

                this.db.STCSJDts.DeleteAllOnSubmit(_delSTCSjDt);
                this.db.SubmitChanges();

                var _query = (
                                from _stcSJDtReference in this.db.STCSJDtReferences
                                join _mktSOHd in this.db.MKTSOHds
                                    on _stcSJDtReference.ReferenceNmbr equals _mktSOHd.TransNmbr
                                //join _mktSODt in this.db.MKTSODts
                                //    on _mktSOHd.TransNmbr equals _mktSODt.TransNmbr
                                join _mktSOProduct in this.db.MKTSOProducts
                                on _mktSOHd.TransNmbr equals _mktSOProduct.TransNmbr
                                where _stcSJDtReference.TransNmbr == _prmTransNmbr
                                    //&& (((_mktSODt.QtyOrder - _mktSODt.QtyDO) == 0) ? false : true)
                                    && ((((_mktSOProduct.Qty - _mktSOProduct.QtyDO) - _mktSOProduct.QtyClose) == 0) ? false : true)
                                    && _mktSOHd.Status == BillOfLadingDataMapper.GetStatus(TransStatus.Posted)
                                select new
                                {
                                    TransNmbr = _stcSJDtReference.TransNmbr,
                                    DONo = _mktSOProduct.TransNmbr,
                                    ProductCode = _mktSOProduct.ProductCode.ToString(),
                                    ItemID = _mktSOProduct.ItemID,
                                    WrhsCode = _stcSJDtReference.WrhsCode,
                                    WrhsFgSubLed = _stcSJDtReference.WrhsFgSubLed,
                                    WrhsSubLed = _stcSJDtReference.WrhsSubLed,
                                    LocationCode = _stcSJDtReference.LocationCode,
                                    Qty = (_mktSOProduct.Qty - _mktSOProduct.QtyDO),
                                    Unit = _mktSOProduct.Unit
                                }
                             );
                ////////////////////////////////////////////////
                foreach (var _row in _query)
                {
                    List<STCSJDt> _list = new List<STCSJDt>();
                    _list.Add(new STCSJDt(_row.TransNmbr, _row.DONo, _row.ProductCode, _row.ItemID, _row.WrhsCode, Convert.ToChar(_row.WrhsFgSubLed), _row.WrhsSubLed, _row.LocationCode, Convert.ToInt32(_row.Qty), _row.Unit));

                    this.db.STCSJDts.InsertAllOnSubmit(_list);
                    this.db.SubmitChanges();
                }

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GenerateSTCSJDtDO(string _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                var _delSTCSjDt = (
                                from _stcSjDt in this.db.STCSJDts
                                where _stcSjDt.TransNmbr == _prmTransNmbr
                                select _stcSjDt
                               );

                this.db.STCSJDts.DeleteAllOnSubmit(_delSTCSjDt);
                this.db.SubmitChanges();

                var _query = (
                                from _stcSJDtReference in this.db.STCSJDtReferences
                                join _mktDOHd in this.db.MKTDOHds
                                    on _stcSJDtReference.ReferenceNmbr equals _mktDOHd.TransNmbr
                                join _mktDODt in this.db.MKTDODts
                                    on _mktDOHd.TransNmbr equals _mktDODt.TransNmbr
                                //join _mktSOProduct in this.db.MKTSOProducts
                                //on _mktSOHd.TransNmbr equals _mktSOProduct.TransNmbr
                                where _stcSJDtReference.TransNmbr == _prmTransNmbr
                                    //&& (((_mktSODt.QtyOrder - _mktSODt.QtyDO) == 0) ? false : true)
                                    && ((((_mktDODt.Qty - _mktDODt.QtySJ) - _mktDODt.QtyClose) == 0) ? false : true)
                                    && _mktDOHd.Status == BillOfLadingDataMapper.GetStatus(TransStatus.Posted)
                                select new
                                {
                                    TransNmbr = _stcSJDtReference.TransNmbr,
                                    DONo = _mktDODt.TransNmbr,
                                    ProductCode = _mktDODt.ProductCode.ToString(),
                                    ItemID = _mktDODt.ItemID,
                                    WrhsCode = _stcSJDtReference.WrhsCode,
                                    WrhsFgSubLed = _stcSJDtReference.WrhsFgSubLed,
                                    WrhsSubLed = _stcSJDtReference.WrhsSubLed,
                                    LocationCode = _stcSJDtReference.LocationCode,
                                    Qty = ((_mktDODt.Qty - _mktDODt.QtyClose) - _mktDODt.QtySJ),
                                    Unit = _mktDODt.Unit
                                }
                             );
                ////////////////////////////////////////////////
                foreach (var _row in _query)
                {
                    List<STCSJDt> _list = new List<STCSJDt>();
                    _list.Add(new STCSJDt(_row.TransNmbr, _row.DONo, _row.ProductCode, _row.ItemID, _row.WrhsCode, Convert.ToChar(_row.WrhsFgSubLed), _row.WrhsSubLed, _row.LocationCode, Convert.ToInt32(_row.Qty), _row.Unit));

                    this.db.STCSJDts.InsertAllOnSubmit(_list);
                    this.db.SubmitChanges();
                }

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCSJDtReference(string _prmTransNmbr, string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    //string[] _code = _prmCode[i].Split('-');

                    STCSJDtReference _stcSJDtReference = this.db.STCSJDtReferences.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.ReferenceNmbr == _prmCode[i]);

                    this.db.STCSJDtReferences.DeleteOnSubmit(_stcSJDtReference);
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

        public MKTSOHd GetSingleMKTSOHd(String _prmFileNmbr)
        {
            MKTSOHd _result = null;

            try
            {
                _result = this.db.MKTSOHds.Single(temp => temp.TransNmbr == _prmFileNmbr);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public MKTDOHd GetSingleMKTDOHd(String _prmFileNmbr)
        {
            MKTDOHd _result = null;

            try
            {
                _result = this.db.MKTDOHds.Single(temp => temp.TransNmbr == _prmFileNmbr);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public bool CekGenerateButton(String _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                var _query = (from _stcSJDt in this.db.STCSJDts
                              where _stcSJDt.TransNmbr == _prmTransNmbr
                              select _stcSJDt.TransNmbr
                            ).Count();

                if (_query == 0)
                    _result = false;
                else
                    _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public String GetSJRef()
        {
            String _result = "";

            try
            {
                _result = this.db.CompanyConfigurations.Single(temp => temp.ConfigCode == "BOLReferenceType").SetValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public List<SALTrDirectSalesHd> GetDSFileNmbrForDDL(string _prmCustCode)
        {
            List<SALTrDirectSalesHd> _result = new List<SALTrDirectSalesHd>();

            try
            {
                var _query = (
                                from _salTrDirectSalesHd in this.db.SALTrDirectSalesHds
                                where ((_salTrDirectSalesHd.FileNo ?? "").Trim() == _salTrDirectSalesHd.FileNo.Trim())
                                    && _salTrDirectSalesHd.CustCode.Trim().ToLower() == _prmCustCode.Trim().ToLower()
                                select new
                                {
                                    TransNmbr = _salTrDirectSalesHd.TransNmbr,
                                    FileNmbr = _salTrDirectSalesHd.FileNo
                                }
                             );

                foreach (var _row in _query)
                {
                    _result.Add(new SALTrDirectSalesHd(_row.TransNmbr, _row.FileNmbr));
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<SAL_TrRetail> GetPOSFileNmbrForDDL(string _prmCustCode)
        {
            List<SAL_TrRetail> _result = new List<SAL_TrRetail>();

            try
            {
                var _query = (
                                from _salTrRetail in this.db.SAL_TrRetails
                                where ((_salTrRetail.FileNmbr ?? "").Trim() == _salTrRetail.FileNmbr.Trim())
                                    && _salTrRetail.CustName.Trim().ToLower() == _prmCustCode.Trim().ToLower()
                                select new
                                {
                                    TransNmbr = _salTrRetail.TransNmbr,
                                    FileNmbr = _salTrRetail.FileNmbr
                                }
                             );

                foreach (var _row in _query)
                {
                    _result.Add(new SAL_TrRetail(_row.TransNmbr, _row.FileNmbr));
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public SALTrDirectSalesDt GetSingleSALTrDirectSalesDt(String _prmSJNo)
        {
            SALTrDirectSalesDt _result = null;

            try
            {
                _result = this.db.SALTrDirectSalesDts.FirstOrDefault(temp => temp.TransNmbr == _prmSJNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public SAL_TrRetailItem GetSingleSAL_TrRetailItem(String _prmSJNo)
        {
            SAL_TrRetailItem _result = null;

            try
            {
                _result = this.db.SAL_TrRetailItems.FirstOrDefault(temp => temp.TransNmbr == _prmSJNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        
        ~BillOfLadingBL()
        {

        }
    }
}
