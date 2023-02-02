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
using System.Data.Linq;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class STCReturBL : Base
    {
        public STCReturBL()
        {

        }

        //#region STCReturHd
        //public double RowsCountSTCReturHd(string _prmCategory, string _prmKeyword)
        //{
        //    double _result = 0;

        //    string _pattern1 = "%%";
        //    string _pattern2 = "%%";

        //    if (_prmCategory == "TransNmbr")
        //    {
        //        _pattern1 = "%" + _prmKeyword + "%";
        //        _pattern2 = "%%";
        //    }
        //    else if (_prmCategory == "FileNo")
        //    {
        //        _pattern2 = "%" + _prmKeyword + "%";
        //        _pattern1 = "%%";
        //    }

        //    var _query =
        //        (
        //            from _stcReturHd in this.db.STCReturHds
        //            where (SqlMethods.Like(_stcReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //            && (SqlMethods.Like((_stcReturHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
        //            && _stcReturHd.Status != STCReturDataMapper.GetStatus(TransStatus.Deleted)
        //            select _stcReturHd
        //        ).Count();

        //    _result = _query;

        //    return _result;
        //}

        //public List<STCReturHd> GetListSTCReturHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        //{
        //    List<STCReturHd> _result = new List<STCReturHd>();

        //    string _pattern1 = "%%";
        //    string _pattern2 = "%%";

        //    if (_prmCategory == "TransNmbr")
        //    {
        //        _pattern1 = "%" + _prmKeyword + "%";
        //        _pattern2 = "%%";
        //    }
        //    else if (_prmCategory == "FileNo")
        //    {
        //        _pattern2 = "%" + _prmKeyword + "%";
        //        _pattern1 = "%%";
        //    }

        //    try
        //    {
        //        var _query =
        //                    (
        //                        from _stcReturHd in this.db.STCReturHds
        //                        where (SqlMethods.Like(_stcReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
        //                        && (SqlMethods.Like((_stcReturHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
        //                        && _stcReturHd.Status != STCReturDataMapper.GetStatus(TransStatus.Deleted)
        //                        orderby _stcReturHd.TransDate descending
        //                        select new
        //                        {
        //                            TransNmbr = _stcReturHd.TransNmbr,
        //                            FileNmbr = _stcReturHd.FileNmbr,
        //                            TransDate = _stcReturHd.TransDate,
        //                            Status = _stcReturHd.Status,
        //                            CustCode = _stcReturHd.CustCode,
        //                            CustName = (
        //                                            from _msCust in this.db.MsCustomers
        //                                            where _msCust.CustCode == _stcReturHd.CustCode
        //                                            select _msCust.CustName
        //                                        ).FirstOrDefault(),
        //                            ReqReturNo = _stcReturHd.ReqReturNo,
        //                            WrhsCode = _stcReturHd.WrhsCode,
        //                            WrhsName = (
        //                                            from _msWrhs in this.db.MsWarehouses
        //                                            where _msWrhs.WrhsCode == _stcReturHd.WrhsCode
        //                                            select _msWrhs.WrhsName
        //                                        ).FirstOrDefault()
        //                        }
        //                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new STCReturHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.CustCode, _row.CustName, _row.ReqReturNo, _row.WrhsCode, _row.WrhsName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public string AddSTCReturHd(STCReturHd _prmSTCReturHd, Boolean _prmFgSingleLocation, String _prmWhrsLocationCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
        //        foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
        //        {
        //            _prmSTCReturHd.TransNmbr = item.Number;
        //            _transactionNumber.TempTransNmbr = item.Number;
        //        }

        //        if (_prmFgSingleLocation)
        //        {
        //            var _queryMKTReqReturDt = (
        //                    from _mktReqReturDt in this.db.MKTReqReturDts
        //                    where _mktReqReturDt.TransNmbr == _prmSTCReturHd.ReqReturNo
        //                    select _mktReqReturDt
        //                );
        //            foreach (var _rsMKTReqReturDt in _queryMKTReqReturDt)
        //            {
        //                STCReturDt _addDataStcReturDt = new STCReturDt();
        //                _addDataStcReturDt.TransNmbr = _prmSTCReturHd.TransNmbr;
        //                _addDataStcReturDt.ProductCode = _rsMKTReqReturDt.ProductCode;
        //                _addDataStcReturDt.LocationCode = _prmWhrsLocationCode;
        //                _addDataStcReturDt.Qty = _rsMKTReqReturDt.Qty;
        //                _addDataStcReturDt.Unit = _rsMKTReqReturDt.Unit;
        //                _addDataStcReturDt.Remark = _rsMKTReqReturDt.Remark;
        //                this.db.STCReturDts.InsertOnSubmit(_addDataStcReturDt);
        //            }
        //        }

        //        this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
        //        this.db.STCReturHds.InsertOnSubmit(_prmSTCReturHd);

        //        var _query = (
        //                        from _temp in this.db.Temporary_TransactionNumbers
        //                        where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
        //                        select _temp
        //                      );

        //        this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

        //        this.db.SubmitChanges();

        //        _result = _prmSTCReturHd.TransDate.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public STCReturHd GetSingleSTCReturHd(string _prmTransNmbr)
        //{
        //    STCReturHd _result = null;

        //    try
        //    {
        //        _result = this.db.STCReturHds.Single(_temp => _temp.TransNmbr == _prmTransNmbr);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool GetSingleSTCReturHdApprove(string[] _prmCode)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        for (int i = 0; i < _prmCode.Length; i++)
        //        {
        //            STCReturHd _stcReturHd = this.db.STCReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

        //            if (_stcReturHd != null)
        //            {
        //                if (_stcReturHd.Status != STCReturDataMapper.GetStatus(TransStatus.Posted))
        //                {
        //                    _result = true;
        //                }
        //                else
        //                {
        //                    _result = false;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool EditSTCReturHd(STCReturHd _prmSTCReturHd)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool DeleteMultiSTCReturHd(string[] _prmTransNmbr)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        for (int i = 0; i < _prmTransNmbr.Length; i++)
        //        {
        //            STCReturHd _stcReturHd = this.db.STCReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower());

        //            if (_stcReturHd != null)
        //            {
        //                if ((_stcReturHd.FileNmbr ?? "").Trim() == "")
        //                {
        //                    var _query = (from _detail in this.db.STCReturDts
        //                                  where _detail.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower()
        //                                  select _detail);

        //                    this.db.STCReturDts.DeleteAllOnSubmit(_query);

        //                    this.db.STCReturHds.DeleteOnSubmit(_stcReturHd);

        //                    _result = true;
        //                }
        //                else
        //                {
        //                    _result = false;
        //                    break;
        //                }
        //            }
        //        }

        //        if (_result == true)
        //            this.db.SubmitChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool DeleteMultiApproveSTCReturHd(string[] _prmTransNmbr, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        for (int i = 0; i < _prmTransNmbr.Length; i++)
        //        {
        //            STCReturHd _stcReturHd = this.db.STCReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower());

        //            if (_stcReturHd.Status == STCReturDataMapper.GetStatus(TransStatus.Approved))
        //            {
        //                Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

        //                _unpostingActivity.ActivitiesCode = Guid.NewGuid();
        //                _unpostingActivity.TransType = _prmTransType;
        //                _unpostingActivity.TransNmbr = _stcReturHd.TransNmbr;
        //                _unpostingActivity.FileNmbr = _stcReturHd.FileNmbr;
        //                _unpostingActivity.Username = _prmUsername;
        //                _unpostingActivity.ActivitiesDate = DateTime.Now;
        //                _unpostingActivity.ActivitiesID = _prmActivitiesID;
        //                _unpostingActivity.Reason = _prmReason;

        //                this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
        //            }

        //            if (_stcReturHd != null)
        //            {
        //                if ((_stcReturHd.FileNmbr ?? "").Trim() == "")
        //                {
        //                    var _query = (from _detail in this.db.STCReturDts
        //                                  where _detail.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower()
        //                                  select _detail);

        //                    this.db.STCReturDts.DeleteAllOnSubmit(_query);

        //                    this.db.STCReturHds.DeleteOnSubmit(_stcReturHd);

        //                    _result = true;
        //                }
        //                else if (_stcReturHd.FileNmbr != "" && _stcReturHd.Status == STCReturDataMapper.GetStatus(TransStatus.Approved))
        //                {
        //                    _stcReturHd.Status = STCReturDataMapper.GetStatus(TransStatus.Deleted);
        //                    _result = true;
        //                }
        //            }
        //        }

        //        if (_result == true)
        //            this.db.SubmitChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public string GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        //{
        //    string _result = "";
        //    string _errorMsg = "";

        //    try
        //    {
        //        int _success = this.db.S_STCReturGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

        //        if (_errorMsg == "")
        //        {
        //            _result = "Get Approval Success";

        //            Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
        //            _transActivity.ActivitiesCode = Guid.NewGuid();
        //            _transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingRetur);
        //            _transActivity.TransNmbr = _prmTransNmbr.ToString();
        //            _transActivity.FileNmbr = "";
        //            _transActivity.Username = _prmuser;
        //            _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
        //            _transActivity.ActivitiesDate = DateTime.Now;
        //            _transActivity.Reason = "";

        //            this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
        //            this.db.SubmitChanges();
        //        }
        //        else
        //        {
        //            _result = _errorMsg;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = "Get Approval Failed";
        //        ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
        //    }

        //    return _result;
        //}

        //public string Approve(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        //{
        //    string _result = "";
        //    string _errorMsg = "";

        //    try
        //    {
        //        using (TransactionScope _scope = new TransactionScope())
        //        {
        //            int _success = this.db.S_STCReturApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

        //            if (_errorMsg == "")
        //            {
        //                STCReturHd _stcReturHd = this.GetSingleSTCReturHd(_prmTransNmbr);
        //                foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcReturHd.TransDate.Year, _stcReturHd.TransDate.Month, AppModule.GetValue(TransactionType.StockReceivingRetur), this._companyTag, ""))
        //                {
        //                    _stcReturHd.FileNmbr = item.Number;
        //                }


        //                Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
        //                _transActivity.ActivitiesCode = Guid.NewGuid();
        //                _transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingRetur);
        //                _transActivity.TransNmbr = _prmTransNmbr.ToString();
        //                _transActivity.FileNmbr = this.GetSingleSTCReturHd(_prmTransNmbr).FileNmbr;
        //                _transActivity.Username = _prmuser;
        //                _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
        //                _transActivity.ActivitiesDate = DateTime.Now;
        //                _transActivity.Reason = "";

        //                this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
        //                this.db.SubmitChanges();

        //                _result = "Approve Success";
        //                _scope.Complete();
        //            }
        //            else
        //            {
        //                _result = _errorMsg;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = "Approve Failed";
        //        ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
        //    }

        //    return _result;
        //}

        //public string Posting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        //{
        //    string _result = "";
        //    string _errorMsg = "";

        //    try
        //    {
        //        TransactionCloseBL _transCloseBL = new TransactionCloseBL();

        //        STCReturHd _stcReturHd = this.db.STCReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
        //        String _locked = _transCloseBL.IsExistAndLocked(_stcReturHd.TransDate);
        //        if (_locked == "")
        //        {
        //            int _success = this.db.S_STCReturPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

        //            if (_errorMsg == "")
        //            {
        //                _result = "Posting Success";

        //                Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
        //                _transActivity.ActivitiesCode = Guid.NewGuid();
        //                _transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingRetur);
        //                _transActivity.TransNmbr = _prmTransNmbr.ToString();
        //                _transActivity.FileNmbr = this.GetSingleSTCReturHd(_prmTransNmbr).FileNmbr;
        //                _transActivity.Username = _prmuser;
        //                _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
        //                _transActivity.ActivitiesDate = DateTime.Now;
        //                _transActivity.Reason = "";

        //                this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
        //                this.db.SubmitChanges();
        //            }
        //            else
        //            {
        //                _result = _errorMsg;
        //            }
        //        }
        //        else
        //        {
        //            _result = _locked;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = "Posting Failed";
        //        ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
        //    }

        //    return _result;
        //}

        //public string Unposting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        //{
        //    string _result = "";
        //    string _errorMsg = "";

        //    try
        //    {
        //        TransactionCloseBL _transCloseBL = new TransactionCloseBL();

        //        STCReturHd _stcReturHd = this.db.STCReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
        //        String _locked = _transCloseBL.IsExistAndLocked(_stcReturHd.TransDate);
        //        if (_locked == "")
        //        {
        //            int _success = this.db.S_STCReturUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

        //            if (_errorMsg == "")
        //            {
        //                _result = "Unposting Success";

        //                //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
        //                //_transActivity.ActivitiesCode = Guid.NewGuid();
        //                //_transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingRetur);
        //                //_transActivity.TransNmbr = _prmTransNmbr.ToString();
        //                //_transActivity.FileNmbr = this.GetSingleSTCReturHd(_prmTransNmbr).FileNmbr;
        //                //_transActivity.Username = _prmuser;
        //                //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
        //                //_transActivity.ActivitiesDate = this.GetSingleSTCReturHd(_prmTransNmbr).TransDate;
        //                //_transActivity.Reason = "";

        //                //this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
        //                //this.db.SubmitChanges();
        //            }
        //            else
        //            {
        //                _result = _errorMsg;
        //            }
        //        }
        //        else
        //        {
        //            _result = _locked;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = "Unposting Failed";
        //        ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
        //    }

        //    return _result;
        //}

        //#endregion

        #region STCReturRRHd
        public double RowsCountSTCReturRRHd(string _prmCategory, string _prmKeyword)
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
                    from _STCReturRRHd in this.db.STCReturRRHds
                    where (SqlMethods.Like(_STCReturRRHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    && (SqlMethods.Like((_STCReturRRHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                    && _STCReturRRHd.Status != STCReturDataMapper.GetStatus(TransStatus.Deleted)
                    select _STCReturRRHd
                ).Count();

            _result = _query;

            return _result;
        }

        public List<STCReturRRHd> GetListSTCReturRRHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCReturRRHd> _result = new List<STCReturRRHd>();

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
                var _query =
                            (
                                from _STCReturRRHd in this.db.STCReturRRHds
                                where (SqlMethods.Like(_STCReturRRHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_STCReturRRHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _STCReturRRHd.Status != STCReturDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _STCReturRRHd.TransDate descending
                                select new
                                {
                                    TransNmbr = _STCReturRRHd.TransNmbr,
                                    FileNmbr = _STCReturRRHd.FileNmbr,
                                    Status = _STCReturRRHd.Status,
                                    TransDate = _STCReturRRHd.TransDate,
                                    CustName = (
                                                    from _msCust in this.db.MsCustomers
                                                    where _msCust.CustCode == _STCReturRRHd.CustCode
                                                    select _msCust.CustName
                                                ).FirstOrDefault(),
                                    ReqReturNo = _STCReturRRHd.ReqReturNo,
                                    WrhsName = (
                                                    from _msWrhs in this.db.MsWarehouses
                                                    where _msWrhs.WrhsCode == _STCReturRRHd.WrhsCode
                                                    select _msWrhs.WrhsName
                                                ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCReturRRHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.CustName, _row.ReqReturNo, _row.WrhsName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddSTCReturRRHd(STCReturRRHd _prmSTCReturRRHd, Boolean _prmFgSingleLocation, String _prmWhrsLocationCode)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCReturRRHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }


                var _queryMKTReqReturDt = (
                        from _mktReqReturDt in this.db.MKTReqReturDts
                        where _mktReqReturDt.TransNmbr == _prmSTCReturRRHd.ReqReturNo
                        select _mktReqReturDt
                    );
                foreach (var _rsMKTReqReturDt in _queryMKTReqReturDt)
                {
                    if (_rsMKTReqReturDt.Qty > (_rsMKTReqReturDt.QtyRetur == null ? 0 : _rsMKTReqReturDt.QtyRetur) + (_rsMKTReqReturDt.QtyClose == null ? 0 : _rsMKTReqReturDt.QtyClose))
                    {
                        STCReturRRDt _addDataSTCReturRRDt = new STCReturRRDt();
                        _addDataSTCReturRRDt.TransNmbr = _prmSTCReturRRHd.TransNmbr;
                        _addDataSTCReturRRDt.ProductCode = _rsMKTReqReturDt.ProductCode;
                        if (_prmFgSingleLocation)
                            _addDataSTCReturRRDt.LocationCode = _prmWhrsLocationCode;
                        else
                            _addDataSTCReturRRDt.LocationCode = "";

                        _addDataSTCReturRRDt.Qty = Convert.ToDecimal(_rsMKTReqReturDt.Qty - (_rsMKTReqReturDt.QtyRetur == null ? 0 : _rsMKTReqReturDt.QtyRetur) - (_rsMKTReqReturDt.QtyClose == null ? 0 : _rsMKTReqReturDt.QtyClose));
                        _addDataSTCReturRRDt.Unit = _rsMKTReqReturDt.Unit;
                        _addDataSTCReturRRDt.Remark = _rsMKTReqReturDt.Remark;
                        this.db.STCReturRRDts.InsertOnSubmit(_addDataSTCReturRRDt);
                    }
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCReturRRHds.InsertOnSubmit(_prmSTCReturRRHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCReturRRHd.TransDate.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCReturRRHd GetSingleSTCReturRRHd(string _prmTransNmbr)
        {
            STCReturRRHd _result = null;

            try
            {
                _result = this.db.STCReturRRHds.FirstOrDefault(_temp => _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCReturRRHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCReturRRHd _STCReturRRHd = this.db.STCReturRRHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_STCReturRRHd != null)
                    {
                        if (_STCReturRRHd.Status != STCReturDataMapper.GetStatus(TransStatus.Posted))
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

        public bool EditSTCReturRRHd(STCReturRRHd _prmSTCReturRRHd, Boolean _prmFgSingleLocation, String _prmWhrsLocationCode)
        {
            bool _result = false;
            try
            {
                if (_prmFgSingleLocation)
                {
                    var _querySTCReturRRDt = (
                            from _stcReturRRDt in this.db.STCReturRRDts
                            where _stcReturRRDt.TransNmbr == _prmSTCReturRRHd.TransNmbr
                            select _stcReturRRDt
                        );
                    foreach (var _row in _querySTCReturRRDt)
                    {
                        STCReturRRDt _stcReturRRDt = new STCReturRRDt();
                        _stcReturRRDt.TransNmbr = _row.TransNmbr;
                        _stcReturRRDt.ProductCode = _row.ProductCode;
                        _stcReturRRDt.LocationCode = _prmWhrsLocationCode;
                        _stcReturRRDt.Qty = _row.Qty;
                        _stcReturRRDt.Unit = _row.Unit;
                        _stcReturRRDt.Remark = _row.Remark;
                        _stcReturRRDt.AccInvent = _row.AccInvent;
                        _stcReturRRDt.FgInvent = _row.FgInvent;
                        _stcReturRRDt.AccTransit = _row.AccTransit;
                        _stcReturRRDt.FgTransit = _row.FgTransit;
                        _stcReturRRDt.AccKoreksi = _row.AccKoreksi;
                        _stcReturRRDt.FgKoreksi = _row.FgKoreksi;
                        _stcReturRRDt.FgConsignment = _row.FgConsignment;
                        _stcReturRRDt.ProductScrap = _row.ProductScrap;
                        _stcReturRRDt.PriceCost = _row.PriceCost;
                        _stcReturRRDt.TotalCost = _row.TotalCost;
                        _stcReturRRDt.PriceTransit = _row.PriceTransit;
                        _stcReturRRDt.QtySJ = _row.QtySJ;
                        this.db.STCReturRRDts.DeleteOnSubmit(_row);
                        this.db.STCReturRRDts.InsertOnSubmit(_stcReturRRDt);
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

        public bool DeleteMultiSTCReturRRHd(string[] _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTransNmbr.Length; i++)
                {
                    STCReturRRHd _stcReturRRHd = this.db.STCReturRRHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower());

                    if (_stcReturRRHd != null)
                    {
                        if ((_stcReturRRHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCReturRRDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCReturRRDts.DeleteAllOnSubmit(_query);

                            this.db.STCReturRRHds.DeleteOnSubmit(_stcReturRRHd);

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

        public bool DeleteMultiApproveSTCReturRRHd(string[] _prmTransNmbr, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTransNmbr.Length; i++)
                {
                    STCReturRRHd _stcReturRRHd = this.db.STCReturRRHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower());

                    if (_stcReturRRHd.Status == STCReturDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcReturRRHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcReturRRHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcReturRRHd != null)
                    {
                        if ((_stcReturRRHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCReturRRDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCReturRRDts.DeleteAllOnSubmit(_query);

                            this.db.STCReturRRHds.DeleteOnSubmit(_stcReturRRHd);

                            _result = true;
                        }
                        else if (_stcReturRRHd.FileNmbr != "" && _stcReturRRHd.Status == STCReturDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcReturRRHd.Status = STCReturDataMapper.GetStatus(TransStatus.Deleted);
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

        public string GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_STCReturRRGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingRetur);
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

        public string ApproveSTCReturRRHd(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.S_STCReturRRApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        STCReturRRHd _stcReturRRHd = this.GetSingleSTCReturRRHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcReturRRHd.TransDate.Year, _stcReturRRHd.TransDate.Month, AppModule.GetValue(TransactionType.StockReceivingRetur), this._companyTag, ""))
                        {
                            _stcReturRRHd.FileNmbr = item.Number;
                        }


                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingRetur);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCReturRRHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
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

        public string PostingSTCReturRRHd(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCReturRRHd _stcReturRRHd = this.db.STCReturRRHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcReturRRHd.TransDate);
                if (_locked == "")
                {
                    //int _success = this.db.S_STCReturRRPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);
                    ISingleResult<S_STCReturRRPostResult> _success = this.db.S_STCReturRRPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingRetur);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCReturRRHd(_prmTransNmbr).FileNmbr;
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

        public string UnpostingSTCReturRRHd(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCReturRRHd _stcReturRRHd = this.db.STCReturRRHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcReturRRHd.TransDate);
                if (_locked == "")
                {
                    //int _success = this.db.S_STCReturRRUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);
                    ISingleResult<S_STCReturRRUnPostResult> _success = this.db.S_STCReturRRUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);
                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingRetur);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCReturRRHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCReturRRHd(_prmTransNmbr).TransDate;
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

        public List<STCReturRRHd> GetSTCReturRRHdForDDL(string _prmCustCode)
        {
            List<STCReturRRHd> _result = new List<STCReturRRHd>();

            try
            {
                var _query = (
                                from _sTCReturRRHd in this.db.STCReturRRHds
                                join _sTCReturRRDt in this.db.STCReturRRDts on _sTCReturRRHd.TransNmbr equals _sTCReturRRDt.TransNmbr
                                where ((_sTCReturRRHd.FileNmbr ?? "").Trim() == _sTCReturRRHd.FileNmbr.Trim())
                                    && _sTCReturRRHd.CustCode.Trim().ToLower() == _prmCustCode.Trim().ToLower()
                                    && _sTCReturRRHd.Status == STCReturDataMapper.GetStatus(TransStatus.Posted)
                                    && ((_sTCReturRRHd.FgDeliveryBack == null) ? Convert.ToChar("N") : _sTCReturRRHd.FgDeliveryBack) == Convert.ToChar("Y")
                                    && _sTCReturRRDt.Qty - ((_sTCReturRRDt.QtySJ == null) ? 0 : _sTCReturRRDt.QtySJ) > 0

                                select new
                                {
                                    TransNmbr = _sTCReturRRHd.TransNmbr,
                                    FileNmbr = _sTCReturRRHd.FileNmbr
                                }
                             ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCReturRRHd(_row.TransNmbr, _row.FileNmbr));
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region STCReturSJHd
        public double RowsCountSTCReturSJHd(string _prmCategory, string _prmKeyword)
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
                    from _STCReturSJHd in this.db.STCReturSJHds
                    where (SqlMethods.Like(_STCReturSJHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    && (SqlMethods.Like((_STCReturSJHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                    && _STCReturSJHd.Status != STCReturDataMapper.GetStatus(TransStatus.Deleted)
                    select _STCReturSJHd
                ).Count();

            _result = _query;

            return _result;
        }

        public List<STCReturSJHd> GetListSTCReturSJHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCReturSJHd> _result = new List<STCReturSJHd>();

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
                var _query =
                            (
                                from _STCReturSJHd in this.db.STCReturSJHds
                                where (SqlMethods.Like(_STCReturSJHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_STCReturSJHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _STCReturSJHd.Status != STCReturDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _STCReturSJHd.TransDate descending
                                select new
                                {
                                    TransNmbr = _STCReturSJHd.TransNmbr,
                                    FileNmbr = _STCReturSJHd.FileNmbr,
                                    Status = _STCReturSJHd.Status,
                                    TransDate = _STCReturSJHd.TransDate,
                                    CustName = (
                                                    from _msCust in this.db.MsCustomers
                                                    where _msCust.CustCode == _STCReturSJHd.CustCode
                                                    select _msCust.CustName
                                                ).FirstOrDefault(),
                                    RRReturNo = _STCReturSJHd.RRReturNo,
                                    WrhsName = (
                                                    from _msWrhs in this.db.MsWarehouses
                                                    where _msWrhs.WrhsCode == _STCReturSJHd.WrhsCode
                                                    select _msWrhs.WrhsName
                                                ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCReturSJHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.CustName, _row.RRReturNo, _row.WrhsName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddSTCReturSJHd(STCReturSJHd _prmSTCReturSJHd, Boolean _prmFgSingleLocation, String _prmWhrsLocationCode)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCReturSJHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                var _querySTCReturRRDt = (
                        from _stcReturRRDt in this.db.STCReturRRDts
                        where _stcReturRRDt.TransNmbr == _prmSTCReturSJHd.RRReturNo
                        select _stcReturRRDt
                    );
                foreach (var _rsSTCReturRRDt in _querySTCReturRRDt)
                {
                    if (_rsSTCReturRRDt.Qty > (_rsSTCReturRRDt.QtySJ == null ? 0 : _rsSTCReturRRDt.QtySJ))
                    {
                        STCReturSJDt _addDataSTCReturSJDt = new STCReturSJDt();
                        _addDataSTCReturSJDt.TransNmbr = _prmSTCReturSJHd.TransNmbr;
                        _addDataSTCReturSJDt.ProductCode = _rsSTCReturRRDt.ProductCode;
                        if (_prmFgSingleLocation)
                            _addDataSTCReturSJDt.LocationCode = _prmWhrsLocationCode;
                        else
                            _addDataSTCReturSJDt.LocationCode = "";

                        _addDataSTCReturSJDt.Qty = Convert.ToDecimal(_rsSTCReturRRDt.Qty - (_rsSTCReturRRDt.QtySJ == null ? 0 : _rsSTCReturRRDt.QtySJ));
                        _addDataSTCReturSJDt.Unit = _rsSTCReturRRDt.Unit;
                        _addDataSTCReturSJDt.Remark = _rsSTCReturRRDt.Remark;
                        this.db.STCReturSJDts.InsertOnSubmit(_addDataSTCReturSJDt);
                    }
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCReturSJHds.InsertOnSubmit(_prmSTCReturSJHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCReturSJHd.TransDate.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCReturSJHd GetSingleSTCReturSJHd(string _prmTransNmbr)
        {
            STCReturSJHd _result = null;

            try
            {
                _result = this.db.STCReturSJHds.FirstOrDefault(_temp => _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCReturSJHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCReturSJHd _STCReturSJHd = this.db.STCReturSJHds.FirstOrDefault(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_STCReturSJHd != null)
                    {
                        if (_STCReturSJHd.Status != STCReturDataMapper.GetStatus(TransStatus.Posted))
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

        public bool EditSTCReturSJHd(STCReturSJHd _prmSTCReturSJHd, Boolean _prmFgSingleLocation, String _prmWhrsLocationCode)
        {
            bool _result = false;
            try
            {
                if (_prmFgSingleLocation)
                {
                    var _querySTCReturSJDt = (
                            from _stcReturSJDt in this.db.STCReturSJDts
                            where _stcReturSJDt.TransNmbr == _prmSTCReturSJHd.TransNmbr
                            select _stcReturSJDt
                        );
                    foreach (var _row in _querySTCReturSJDt)
                    {
                        //STCReturSJDt _stcReturSJDt = this.db.STCReturSJDts.FirstOrDefault(_temp => _temp.TransNmbr == _row.TransNmbr & _temp.ProductCode == _row.ProductCode & _temp.LocationCode == _row.LocationCode);
                        STCReturSJDt _stcReturSJDt = new STCReturSJDt();
                        _stcReturSJDt.TransNmbr = _row.TransNmbr;
                        _stcReturSJDt.ProductCode = _row.ProductCode;
                        _stcReturSJDt.LocationCode = _prmWhrsLocationCode;
                        _stcReturSJDt.Qty = _row.Qty;
                        _stcReturSJDt.Unit = _row.Unit;
                        _stcReturSJDt.Remark = _row.Remark;
                        _stcReturSJDt.AccInvent = _row.AccInvent;
                        _stcReturSJDt.FgInvent = _row.FgInvent;
                        _stcReturSJDt.AccTransit = _row.AccTransit;
                        _stcReturSJDt.FgTransit = _row.FgTransit;
                        _stcReturSJDt.AccKoreksi = _row.AccKoreksi;
                        _stcReturSJDt.FgKoreksi = _row.FgKoreksi;
                        _stcReturSJDt.FgConsignment = _row.FgConsignment;
                        _stcReturSJDt.PriceCost = _row.PriceCost;
                        _stcReturSJDt.TotalCost = _row.TotalCost;
                        this.db.STCReturSJDts.DeleteOnSubmit(_row);
                        this.db.STCReturSJDts.InsertOnSubmit(_stcReturSJDt);
                        //_row.LocationCode = _prmWhrsLocationCode;
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

        public bool DeleteMultiSTCReturSJHd(string[] _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTransNmbr.Length; i++)
                {
                    STCReturSJHd _STCReturSJHd = this.db.STCReturSJHds.FirstOrDefault(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower());

                    if (_STCReturSJHd != null)
                    {
                        if ((_STCReturSJHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCReturSJDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCReturSJDts.DeleteAllOnSubmit(_query);

                            this.db.STCReturSJHds.DeleteOnSubmit(_STCReturSJHd);

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

        public bool DeleteMultiApproveSTCReturSJHd(string[] _prmTransNmbr, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTransNmbr.Length; i++)
                {
                    STCReturSJHd _STCReturSJHd = this.db.STCReturSJHds.FirstOrDefault(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower());

                    if (_STCReturSJHd.Status == STCReturDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _STCReturSJHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _STCReturSJHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_STCReturSJHd != null)
                    {
                        if ((_STCReturSJHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCReturSJDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmTransNmbr[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCReturSJDts.DeleteAllOnSubmit(_query);

                            this.db.STCReturSJHds.DeleteOnSubmit(_STCReturSJHd);

                            _result = true;
                        }
                        else if (_STCReturSJHd.FileNmbr != "" && _STCReturSJHd.Status == STCReturDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _STCReturSJHd.Status = STCReturDataMapper.GetStatus(TransStatus.Deleted);
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

        public string GetApprSTCReturSJHd(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_STCReturSJGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockDeliveryRetur);
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

        public string ApproveSTCReturSJHd(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.S_STCReturSJApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        STCReturSJHd _STCReturSJHd = this.GetSingleSTCReturSJHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_STCReturSJHd.TransDate.Year, _STCReturSJHd.TransDate.Month, AppModule.GetValue(TransactionType.StockDeliveryRetur), this._companyTag, ""))
                        {
                            _STCReturSJHd.FileNmbr = item.Number;
                        }


                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockDeliveryRetur);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCReturSJHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
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

        public string PostingSTCReturSJHd(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCReturSJHd _STCReturSJHd = this.db.STCReturSJHds.FirstOrDefault(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_STCReturSJHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STCReturSJPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockDeliveryRetur);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCReturSJHd(_prmTransNmbr).FileNmbr;
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

        public string UnpostingSTCReturSJHd(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCReturSJHd _STCReturSJHd = this.db.STCReturSJHds.FirstOrDefault(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_STCReturSJHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STCReturSJUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingRetur);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCReturSJHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCReturSJHd(_prmTransNmbr).TransDate;
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

        //#region STCReturDt

        //public int RowsCountSTCReturDt(string _prmTransNmbr)
        //{
        //    int _result = 0;
        //    try
        //    {

        //        _result = this.db.STCReturDts.Where(_temp => _temp.TransNmbr == _prmTransNmbr).Count();

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }
        //    return _result;
        //}

        //public List<STCReturDt> GetListSTCReturDt(int _prmReqPage, int _prmPageSize, string _prmTransNmbr)
        //{
        //    List<STCReturDt> _result = new List<STCReturDt>();

        //    try
        //    {
        //        var _query =
        //                    (
        //                        from _STCReturDt in this.db.STCReturDts
        //                        where _STCReturDt.TransNmbr == _prmTransNmbr
        //                        select new
        //                        {
        //                            TransNmbr = _STCReturDt.TransNmbr,
        //                            ProductCode = _STCReturDt.ProductCode,
        //                            ProductName = (
        //                                            from _msProduct in this.db.MsProducts
        //                                            where _msProduct.ProductCode == _STCReturDt.ProductCode
        //                                            select _msProduct.ProductName
        //                                          ).FirstOrDefault(),
        //                            LocationName = (
        //                                            from _msLocation in this.db.MsWrhsLocations
        //                                            where _msLocation.WLocationCode == _STCReturDt.LocationCode
        //                                            select _msLocation.WLocationName
        //                                          ).FirstOrDefault(),
        //                            Unit = _STCReturDt.Unit,
        //                            Qty = _STCReturDt.Qty
        //                        }
        //                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

        //        foreach (var _row in _query)
        //        {
        //            //_result.Add(new STCReturDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.LocationName, _row.Qty, _row.Unit));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public STCReturDt GetSingleSTCReturDt(string _prmTransNmbr, string _prmProductCode, string _prmLocationCode)
        //{
        //    STCReturDt _result = null;

        //    try
        //    {
        //        _result = this.db.STCReturDts.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.ProductCode == _prmProductCode && _temp.LocationCode == _prmLocationCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool EditSTCReturHd(STCReturDt _prmSTCReturDt)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool DeleteMultiSTCReturDt(string _prmTransNo, string[] _prmCode)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        for (int i = 0; i < _prmCode.Length; i++)
        //        {
        //            string[] _code = _prmCode[i].Split('-');

        //            STCReturDt _STCReturDt = this.db.STCReturDts.Single(_temp => _temp.TransNmbr == _prmTransNo && _temp.ProductCode == _code[0] && _temp.LocationCode == _code[1]);

        //            this.db.STCReturDts.DeleteOnSubmit(_STCReturDt);
        //        }

        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool AddSTCReturDt(STCReturDt _prmSTCReturDt)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        this.db.STCReturDts.InsertOnSubmit(_prmSTCReturDt);
        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //#endregion

        #region STCReturRRDt

        public int RowsCountSTCReturRRDt(string _prmTransNmbr)
        {
            int _result = 0;
            try
            {

                _result = this.db.STCReturRRDts.Where(_temp => _temp.TransNmbr == _prmTransNmbr).Count();

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<STCReturRRDt> GetListSTCReturRRDt(int _prmReqPage, int _prmPageSize, string _prmTransNmbr)
        {
            List<STCReturRRDt> _result = new List<STCReturRRDt>();

            try
            {
                var _query =
                            (
                                from _STCReturRRDt in this.db.STCReturRRDts
                                where _STCReturRRDt.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    TransNmbr = _STCReturRRDt.TransNmbr,
                                    ProductCode = _STCReturRRDt.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode == _STCReturRRDt.ProductCode
                                                    select _msProduct.ProductName
                                                  ).FirstOrDefault(),
                                    LocationCode = _STCReturRRDt.LocationCode,
                                    LocationName = (
                                                    from _msLocation in this.db.MsWrhsLocations
                                                    where _msLocation.WLocationCode == _STCReturRRDt.LocationCode
                                                    select _msLocation.WLocationName
                                                  ).FirstOrDefault(),
                                    Unit = _STCReturRRDt.Unit,
                                    Qty = _STCReturRRDt.Qty
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCReturRRDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.LocationCode, _row.LocationName, _row.Qty, _row.Unit));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCReturRRDt GetSingleSTCReturRRDt(string _prmTransNmbr, string _prmProductCode, string _prmLocationCode)
        {
            STCReturRRDt _result = null;

            try
            {
                if (_prmLocationCode != "")
                {
                    _result = this.db.STCReturRRDts.FirstOrDefault(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.ProductCode == _prmProductCode && _temp.LocationCode == _prmLocationCode);
                }
                else
                {
                    _result = this.db.STCReturRRDts.FirstOrDefault(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.ProductCode == _prmProductCode);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCReturRRDt(STCReturRRDt _prmSTCReturRRDt)
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

        public bool DeleteMultiSTCReturRRDt(string _prmTransNo, string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _code = _prmCode[i].Split('|');

                    STCReturRRDt _STCReturRRDt = this.db.STCReturRRDts.Single(_temp => _temp.TransNmbr == _prmTransNo && _temp.ProductCode == _code[0] && _temp.LocationCode == _code[1]);

                    this.db.STCReturRRDts.DeleteOnSubmit(_STCReturRRDt);
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

        public bool AddSTCReturRRDt(STCReturRRDt _prmSTCReturRRDt)
        {
            bool _result = false;

            try
            {
                this.db.STCReturRRDts.InsertOnSubmit(_prmSTCReturRRDt);
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

        #region STCReturSJDt

        public int RowsCountSTCReturSJDt(string _prmTransNmbr)
        {
            int _result = 0;
            try
            {

                _result = this.db.STCReturSJDts.Where(_temp => _temp.TransNmbr == _prmTransNmbr).Count();

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<STCReturSJDt> GetListSTCReturSJDt(int _prmReqPage, int _prmPageSize, string _prmTransNmbr)
        {
            List<STCReturSJDt> _result = new List<STCReturSJDt>();

            try
            {
                var _query =
                            (
                                from _STCReturSJDt in this.db.STCReturSJDts
                                where _STCReturSJDt.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    TransNmbr = _STCReturSJDt.TransNmbr,
                                    ProductCode = _STCReturSJDt.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode == _STCReturSJDt.ProductCode
                                                    select _msProduct.ProductName
                                                  ).FirstOrDefault(),
                                    LocationCode = _STCReturSJDt.LocationCode,
                                    LocationName = (
                                                    from _msLocation in this.db.MsWrhsLocations
                                                    where _msLocation.WLocationCode == _STCReturSJDt.LocationCode
                                                    select _msLocation.WLocationName
                                                  ).FirstOrDefault(),
                                    Unit = _STCReturSJDt.Unit,
                                    Qty = _STCReturSJDt.Qty
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCReturSJDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.LocationCode, _row.LocationName, _row.Qty, _row.Unit));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCReturSJDt GetSingleSTCReturSJDt(string _prmTransNmbr, string _prmProductCode, string _prmLocationCode)
        {
            STCReturSJDt _result = null;

            try
            {
                if (_prmLocationCode != "")
                {
                    _result = this.db.STCReturSJDts.FirstOrDefault(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.ProductCode == _prmProductCode && _temp.LocationCode == _prmLocationCode);
                }
                else
                {
                    _result = this.db.STCReturSJDts.FirstOrDefault(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.ProductCode == _prmProductCode);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetLocationCodeSTCReturSJDt(string _prmTransNmbr)
        {
            String _result = "";

            try
            {
                _result = this.db.STCReturSJDts.FirstOrDefault(_temp => _temp.TransNmbr == _prmTransNmbr).LocationCode;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCReturSJDt(STCReturSJDt _prmSTCReturSJDt)
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

        public bool DeleteMultiSTCReturSJDt(string _prmTransNo, string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _code = _prmCode[i].Split('|');

                    STCReturSJDt _STCReturSJDt = this.db.STCReturSJDts.Single(_temp => _temp.TransNmbr == _prmTransNo && _temp.ProductCode == _code[0] && _temp.LocationCode == _code[1]);

                    this.db.STCReturSJDts.DeleteOnSubmit(_STCReturSJDt);
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

        public bool AddSTCReturSJDt(STCReturSJDt _prmSTCReturSJDt)
        {
            bool _result = false;

            try
            {
                this.db.STCReturSJDts.InsertOnSubmit(_prmSTCReturSJDt);
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
        ~STCReturBL()
        {

        }
    }
}
