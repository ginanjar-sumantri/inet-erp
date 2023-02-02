﻿using System;
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
    public sealed class StockBeginningBL : Base
    {
        public StockBeginningBL()
        {
        }

        #region STCAdjustHd

        public double RowsCountSTCAdjustHd(string _prmCategory, string _prmKeyword)
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
                    from _stcAdjustHd in this.db.STCAdjustHds
                    where _stcAdjustHd.AdjustType == "Saldo"
                        && (SqlMethods.Like(_stcAdjustHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like((_stcAdjustHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && _stcAdjustHd.Status != StockBeginningDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcAdjustHd

                ).Count();

            _result = _query;

            return _result;
        }

        public List<STCAdjustHd> GetListSTCAdjustHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCAdjustHd> _result = new List<STCAdjustHd>();

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
                                from _stcAdjustHd in this.db.STCAdjustHds
                                where _stcAdjustHd.AdjustType == "Saldo"
                                    && (SqlMethods.Like(_stcAdjustHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_stcAdjustHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _stcAdjustHd.Status != StockBeginningDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcAdjustHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _stcAdjustHd.TransNmbr,
                                    FileNmbr = _stcAdjustHd.FileNmbr,
                                    TransDate = _stcAdjustHd.TransDate,
                                    Status = _stcAdjustHd.Status,
                                    WrhsCode = _stcAdjustHd.WrhsCode,
                                    WrhsName = (
                                                     from _msWrhs in this.db.MsWarehouses
                                                     where _msWrhs.WrhsCode == _stcAdjustHd.WrhsCode
                                                     select _msWrhs.WrhsName
                                                 ).FirstOrDefault(),
                                    StockType = _stcAdjustHd.StockType,
                                    StockTypeName = (
                                                        from _msStockType in this.db.MsStockTypes
                                                        where _msStockType.StockTypeCode == _stcAdjustHd.StockType
                                                        select _msStockType.StockTypeName
                                                    ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCAdjustHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.WrhsCode, _row.WrhsName, _row.StockType, _row.StockTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCAdjustHd GetSingleSTCAdjustHd(string _prmCode)
        {
            STCAdjustHd _result = null;

            try
            {
                _result = this.db.STCAdjustHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCAdjustHdHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCAdjustHd _stcAdjustHd = this.db.STCAdjustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcAdjustHd != null)
                    {
                        if (_stcAdjustHd.Status != StockBeginningDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiSTCAdjustHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCAdjustHd _stcAdjustHd = this.db.STCAdjustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcAdjustHd != null)
                    {
                        if ((_stcAdjustHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCAdjustDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCAdjustDts.DeleteAllOnSubmit(_query);

                            this.db.STCAdjustHds.DeleteOnSubmit(_stcAdjustHd);

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

        public bool DeleteMultiApproveSTCAdjustHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCAdjustHd _stcAdjustHd = this.db.STCAdjustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcAdjustHd.Status == StockBeginningDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcAdjustHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcAdjustHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcAdjustHd != null)
                    {
                        if ((_stcAdjustHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCAdjustDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCAdjustDts.DeleteAllOnSubmit(_query);

                            this.db.STCAdjustHds.DeleteOnSubmit(_stcAdjustHd);

                            _result = true;
                        }
                        else if (_stcAdjustHd.FileNmbr != "" && _stcAdjustHd.Status == StockBeginningDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcAdjustHd.Status = StockBeginningDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddSTCAdjustHd(STCAdjustHd _prmSTCAdjustHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCAdjustHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCAdjustHds.InsertOnSubmit(_prmSTCAdjustHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCAdjustHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCAdjustHd(STCAdjustHd _prmSTCAdjustHd)
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
                int _success = this.db.S_STBeginGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockBeginning);
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
                    int _success = this.db.S_STBeginApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        STCAdjustHd _stcAdjustHd = this.GetSingleSTCAdjustHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcAdjustHd.TransDate.Year, _stcAdjustHd.TransDate.Month, AppModule.GetValue(TransactionType.StockBeginning), this._companyTag, ""))
                        {
                            _stcAdjustHd.FileNmbr = item.Number;
                        }                        

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockBeginning);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCAdjustHd(_prmTransNmbr).FileNmbr;
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

                STCAdjustHd _stcAdjustHd = this.db.STCAdjustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _temp.AdjustType == StockAdjustmentDataMapper.GetAdjustType(AdjustType.Saldo));
                String _locked = _transCloseBL.IsExistAndLocked(_stcAdjustHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STBeginPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockBeginning);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCAdjustHd(_prmTransNmbr).FileNmbr;
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

                STCAdjustHd _stcAdjustHd = this.db.STCAdjustHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _temp.AdjustType == StockAdjustmentDataMapper.GetAdjustType(AdjustType.Saldo));
                String _locked = _transCloseBL.IsExistAndLocked(_stcAdjustHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STBeginUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockBeginning);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCAdjustHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCAdjustHd(_prmTransNmbr).TransDate;
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

        #region STCAdjustDt
        public int RowsCountSTCAdjustDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _STCAdjustDt in this.db.STCAdjustDts
                                 where _STCAdjustDt.TransNmbr == _prmCode
                                 select _STCAdjustDt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public List<STCAdjustDt> GetListSTCAdjustDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCAdjustDt> _result = new List<STCAdjustDt>();

            try
            {
                var _query = (
                                from _stcAdjustDt in this.db.STCAdjustDts
                                where _stcAdjustDt.TransNmbr == _prmCode
                                orderby _stcAdjustDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcAdjustDt.TransNmbr,
                                    ProductCode = _stcAdjustDt.ProductCode,
                                    LocationCode = _stcAdjustDt.LocationCode,
                                    Qty = _stcAdjustDt.Qty,
                                    Unit = _stcAdjustDt.Unit,
                                    FgAdjust = _stcAdjustDt.FgAdjust,
                                    Remark = _stcAdjustDt.Remark,
                                    PriceCost = _stcAdjustDt.PriceCost,
                                    TotalCost = _stcAdjustDt.TotalCost
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCAdjustDt(_row.TransNmbr, _row.ProductCode, _row.LocationCode, _row.Qty, _row.Unit, _row.FgAdjust, _row.Remark, _row.PriceCost, _row.TotalCost));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCAdjustDt GetSingleSTCAdjustDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        {
            STCAdjustDt _result = null;

            try
            {
                _result = this.db.STCAdjustDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode && _temp.LocationCode == _prmLocationCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCAdjustDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('=');

                    STCAdjustDt _STCAdjustDt = this.db.STCAdjustDts.Single(_temp => _temp.ProductCode == _tempSplit[0] && _temp.LocationCode == _tempSplit[1] && _temp.TransNmbr == _prmTransNo);

                    this.db.STCAdjustDts.DeleteOnSubmit(_STCAdjustDt);
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

        public bool AddSTCAdjustDt(STCAdjustDt _prmSTCAdjustDt)
        {
            bool _result = false;

            try
            {
                this.db.STCAdjustDts.InsertOnSubmit(_prmSTCAdjustDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddSTCAdjustDtList(List<STCAdjustDt> _prmSTCAdjustDtList)
        {
            bool _result = false;

            try
            {
                foreach (STCAdjustDt _row in _prmSTCAdjustDtList)
                {
                    STCAdjustDt _stcAdjustDt = new STCAdjustDt();

                    _stcAdjustDt.TransNmbr = _row.TransNmbr;
                    _stcAdjustDt.ProductCode = _row.ProductCode;
                    _stcAdjustDt.LocationCode = _row.LocationCode;
                    _stcAdjustDt.Qty = _row.Qty;
                    _stcAdjustDt.Unit = _row.Unit;
                    _stcAdjustDt.FgAdjust = _row.FgAdjust;
                    _stcAdjustDt.PriceCost = _row.PriceCost;
                    _stcAdjustDt.TotalCost = _row.TotalCost;
                    _stcAdjustDt.Remark = _row.Remark;

                    this.db.STCAdjustDts.InsertOnSubmit(_stcAdjustDt);
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

        public bool EditSTCAdjustDt(STCAdjustDt _prmSTCAdjustDt)
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

        ~StockBeginningBL()
        {
        }
    }
}
