using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
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
    public sealed class CogsProcessBL : Base
    {
        public CogsProcessBL()
        {
        }

        #region CogsProcessHd

        public double RowsCountStcProcessHd(string _prmCategory, string _prmKeyword)
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
                    from _STCProcessHd in this.db.STCProcessHds
                    where (SqlMethods.Like(_STCProcessHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like((_STCProcessHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && _STCProcessHd.Status != StockAdjustmentDataMapper.GetStatus(TransStatus.Deleted)
                    select _STCProcessHd

                ).Count();

            _result = _query;
            return _result;

        }

        public List<STCProcessHd> GetListStcProcessHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCProcessHd> _result = new List<STCProcessHd>();

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
                                from _STCProcessHd in this.db.STCProcessHds
                                where
                                    (SqlMethods.Like(_STCProcessHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_STCProcessHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _STCProcessHd.Status != StockAdjustmentDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _STCProcessHd.DatePrep descending
                                select _STCProcessHd
                    //{
                    //    TransNmbr = _STCProcessHd.TransNmbr,
                    //    FileNmbr = _STCProcessHd.FileNmbr,
                    //    TransDate = _STCProcessHd.TransDate,
                    //    Status = _STCProcessHd.Status,
                    //    Sta = _STCProcessHd.OpnameNo,
                    //    FileNo = this.GetFileNmbrSTCOpnameHd(_STCProcessHd.OpnameNo),
                    //    WrhsCode = _STCProcessHd.WrhsCode,
                    //    WrhsName = (
                    //                    from _msWrhs in this.db.MsWarehouses
                    //                    where _msWrhs.WrhsCode == _STCProcessHd.WrhsCode
                    //                    select _msWrhs.WrhsName
                    //                ).FirstOrDefault(),
                    //    StockType = _STCProcessHd.StockType,
                    //    StockTypeName = (
                    //                        from _msStockType in this.db.MsStockTypes
                    //                        where _msStockType.StockTypeCode == _STCProcessHd.StockType
                    //                        select _msStockType.StockTypeName
                    //                    ).FirstOrDefault()
                    //}
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

        public STCProcessHd GetSingleStcProcessHd(string _prmCode)
        {
            STCProcessHd _result = null;

            try
            {
                _result = this.db.STCProcessHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleStcProcessHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCProcessHd _STCProcessHd = this.db.STCProcessHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_STCProcessHd != null)
                    {
                        if (_STCProcessHd.Status != StockAdjustmentDataMapper.GetStatus(TransStatus.Posted))
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

        public STCProcessHd GetStcProcessHdByCode(string _prmCode)
        {
            STCProcessHd _result = null;

            try
            {

                var _query = (from _STCProcessHd in this.db.STCProcessHds
                              where _STCProcessHd.TransNmbr == _prmCode
                              select _STCProcessHd
                             );

                foreach (var _row in _query)
                {
                    _result = _row;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public bool DeleteMultiStcProcessHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCProcessHd _STCProcessHd = this.db.STCProcessHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_STCProcessHd != null)
                    {
                        if ((_STCProcessHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCProcessDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCProcessDts.DeleteAllOnSubmit(_query);

                            this.db.STCProcessHds.DeleteOnSubmit(_STCProcessHd);

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

        public bool DeleteMultiApproveStcProcessHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCProcessHd _STCProcessHd = this.db.STCProcessHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_STCProcessHd.Status == StockAdjustmentDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _STCProcessHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _STCProcessHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_STCProcessHd != null)
                    {
                        if ((_STCProcessHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCProcessDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCProcessDts.DeleteAllOnSubmit(_query);

                            this.db.STCProcessHds.DeleteOnSubmit(_STCProcessHd);

                            _result = true;
                        }
                        else if (_STCProcessHd.FileNmbr != "" && _STCProcessHd.Status == StockAdjustmentDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _STCProcessHd.Status = StockAdjustmentDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddStcProcessHd(STCProcessHd _prmStcProcessHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();

                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmStcProcessHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCProcessHds.InsertOnSubmit(_prmStcProcessHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmStcProcessHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditStcProcessHd(STCProcessHd _prmStcProcessHd)
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
                STCProcessHd _STCProcessHd = this.GetSingleStcProcessHd(_prmTransNmbr);
                _STCProcessHd.Status = CogsProcessDataMapper.GetStatus(TransStatus.WaitingForApproval);
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_STCProcessHd.TransDate.Year, _STCProcessHd.TransDate.Month, AppModule.GetValue(TransactionType.CogsProcess), this._companyTag, ""))
                //{
                //    _STCProcessHd.FileNmbr = item.Number;
                //}

                this.db.SubmitChanges();
                _result = "Get Approval Success";
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
                STCProcessHd _STCProcessHd = this.GetSingleStcProcessHd(_prmTransNmbr);
                _STCProcessHd.Status = CogsProcessDataMapper.GetStatus(TransStatus.Approved);
                foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_STCProcessHd.TransDate.Year, _STCProcessHd.TransDate.Month, AppModule.GetValue(TransactionType.CogsProcess), this._companyTag, ""))
                {
                    _STCProcessHd.FileNmbr = item.Number;
                }

                this.db.SubmitChanges();
                _result = "Approval Success";
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

                DateTime _transDate = this.db.STCProcessHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()).TransDate;
                String _locked = _transCloseBL.IsExistAndLocked(_transDate);
                if (_locked == "")
                {
                    int _success = this.db.SpSTC_ProcessCOGSCompute1(_prmTransNmbr, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        //STCProcessHd _STCProcessHd = this.GetSingleStcProcessHd(_prmTransNmbr);
                        //_STCProcessHd.Status = CogsProcessDataMapper.GetStatus(TransStatus.Posted);
                        _result = "Posting Success";
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

        public string Complete(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCProcessHd _STCProcessHd = this.db.STCProcessHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_STCProcessHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.SpSTC_ProcessCOGSCompute2(_prmTransNmbr, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        //_STCProcessHd.Status = CogsProcessDataMapper.GetStatus(TransStatus.Posted);
                        _result = "Complete Success";
                        this.db.SubmitChanges();
                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockAdjustment);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCProcessHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCProcessHd(_prmTransNmbr).TransDate;
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

        public string Unposting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCProcessHd _STCProcessHd = this.db.STCProcessHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_STCProcessHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.SpSTC_ProcessCOGSUnCompute(_prmTransNmbr, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        //_STCProcessHd.Status = CogsProcessDataMapper.GetStatus(TransStatus.Approved);
                        _result = "Unposting Success";
                        this.db.SubmitChanges();
                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockAdjustment);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCProcessHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCProcessHd(_prmTransNmbr).TransDate;
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

        #region CogsProcessDt

        public double RowsCountSTCProcessDt(string _prmCategory, string _prmKeyword, string _prmCode)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "ProductCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "ProductName")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
            }

            var _query =
                (
                    from _STCProcessDt in this.db.STCProcessDts
                    join _msProduct in this.db.MsProducts on _STCProcessDt.ProductCode equals _msProduct.ProductCode
                    where _STCProcessDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                        && (SqlMethods.Like(_STCProcessDt.ProductCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like((_msProduct.ProductName ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _STCProcessDt

                ).Count();

            _result = _query;
            return _result;
        }

        public List<STCProcessDt> GetListSTCProcessDt(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmCode)
        {
            List<STCProcessDt> _result = new List<STCProcessDt>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "ProductCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "ProductName")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _STCProcessDt in this.db.STCProcessDts
                                join _msProduct in this.db.MsProducts on _STCProcessDt.ProductCode equals _msProduct.ProductCode
                                where _STCProcessDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                      && (SqlMethods.Like(_STCProcessDt.ProductCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                      && (SqlMethods.Like((_msProduct.ProductName ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _STCProcessDt
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

        //public STCProcessDt GetSingleSTCProcessDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        //{
        //    STCProcessDt _result = null;

        //    try
        //    {
        //        _result = this.db.STCProcessDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode && _temp.LocationCode == _prmLocationCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool DeleteMultiSTCProcessDt(string[] _prmCode, string _prmTransNo)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        for (int i = 0; i < _prmCode.Length; i++)
        //        {
        //            string[] _tempSplit = _prmCode[i].Split('|');

        //            STCProcessDt _STCProcessDt = this.db.STCProcessDts.Single(_temp => _temp.ProductCode == _tempSplit[0] && _temp.LocationCode == _tempSplit[1] && _temp.TransNmbr == _prmTransNo);

        //            this.db.STCProcessDts.DeleteOnSubmit(_STCProcessDt);
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

        //public bool AddSTCProcessDt(STCProcessDt _prmSTCProcessDt)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        this.db.STCProcessDts.InsertOnSubmit(_prmSTCProcessDt);

        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool EditSTCProcessDt(STCProcessDt _prmSTCProcessDt)
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

        //public decimal GetCOGS(string _prmProductCode, string _prmWrhs, string _prmSubled, string _prmLocation, string _prmUnit)
        //{
        //    decimal _result = 0;

        //    try
        //    {
        //        var _query = (
        //                       from _stockCOGS in this.db.StockControl_COGs
        //                       where _stockCOGS.ProductCode == _prmProductCode &&
        //                       _stockCOGS.UnitCode == _prmUnit &&
        //                       _stockCOGS.WrhsCode == _prmWrhs &&
        //                       _stockCOGS.WrhsSubled == _prmSubled &&
        //                       _stockCOGS.LocationCode == _prmLocation
        //                       select _stockCOGS.Price
        //                   ).Take(1).FirstOrDefault();

        //        _result = _query;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        #endregion

        ~CogsProcessBL()
        {
        }
    }
}
