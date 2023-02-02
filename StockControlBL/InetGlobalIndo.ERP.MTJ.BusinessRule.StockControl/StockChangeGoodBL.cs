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
    public sealed class StockChangeGoodBL : Base
    {
        public StockChangeGoodBL()
        {
        }

        #region STCChangeHd

        public double RowsCountSTCChangeHd(string _prmCategory, string _prmKeyword)
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
                    from _stcChangeHd in this.db.STCChangeHds
                    where (SqlMethods.Like(_stcChangeHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like((_stcChangeHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && _stcChangeHd.Status != StockChangeGoodDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcChangeHd
                ).Count();

            _result = _query;
            return _result;
        }

        public List<STCChangeHd> GetListSTCChangeHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCChangeHd> _result = new List<STCChangeHd>();

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
                                from _stcChangeHd in this.db.STCChangeHds
                                where (SqlMethods.Like(_stcChangeHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_stcChangeHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _stcChangeHd.Status != StockChangeGoodDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcChangeHd.CreatedDate descending
                                select new
                                {
                                    TransNmbr = _stcChangeHd.TransNmbr,
                                    FileNmbr = _stcChangeHd.FileNmbr,
                                    TransDate = _stcChangeHd.TransDate,
                                    Status = _stcChangeHd.Status,
                                    WrhsSrc = _stcChangeHd.WrhsSrc,
                                    WrhsSrcName = (
                                                        from _msWarehouseSrc in this.db.MsWarehouses
                                                        where _stcChangeHd.WrhsSrc == _msWarehouseSrc.WrhsCode
                                                        select _msWarehouseSrc.WrhsName
                                                    ).FirstOrDefault(),
                                    WrhsDest = _stcChangeHd.WrhsDest,
                                    WrhsDestName = (
                                                        from _msWarehouseDest in this.db.MsWarehouses
                                                        where _stcChangeHd.WrhsDest == _msWarehouseDest.WrhsCode
                                                        select _msWarehouseDest.WrhsName
                                                    ).FirstOrDefault(),
                                    Remark = _stcChangeHd.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCChangeHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.WrhsSrc, _row.WrhsSrcName, _row.WrhsDest, _row.WrhsDestName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCChangeHd GetSingleSTCChangeHd(string _prmCode)
        {
            STCChangeHd _result = null;

            try
            {
                _result = this.db.STCChangeHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCChangeHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCChangeHd _stcChangeHd = this.db.STCChangeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcChangeHd != null)
                    {
                        if (_stcChangeHd.Status != StockChangeGoodDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiSTCChangeHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCChangeHd _stcChangeHd = this.db.STCChangeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcChangeHd != null)
                    {
                        if ((_stcChangeHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCChangeDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCChangeDts.DeleteAllOnSubmit(_query);

                            this.db.STCChangeHds.DeleteOnSubmit(_stcChangeHd);

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

        public bool DeleteMultiApproveSTCChangeHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCChangeHd _stcChangeHd = this.db.STCChangeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcChangeHd.Status == StockChangeGoodDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcChangeHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcChangeHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcChangeHd != null)
                    {
                        if ((_stcChangeHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCChangeDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCChangeDts.DeleteAllOnSubmit(_query);

                            this.db.STCChangeHds.DeleteOnSubmit(_stcChangeHd);

                            _result = true;
                        }
                        else if (_stcChangeHd.FileNmbr != "" && _stcChangeHd.Status == StockChangeGoodDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcChangeHd.Status = StockChangeGoodDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddSTCChangeHd(STCChangeHd _prmSTCChangeHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCChangeHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCChangeHds.InsertOnSubmit(_prmSTCChangeHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCChangeHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCChangeHd(STCChangeHd _prmSTCChangeHd)
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
                int _success = this.db.S_STChangeGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockChangeGood);
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
                    int _success = this.db.S_STChangeApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        STCChangeHd _stcChangeHd = this.GetSingleSTCChangeHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcChangeHd.TransDate.Year, _stcChangeHd.TransDate.Month, AppModule.GetValue(TransactionType.StockChangeGood), this._companyTag, ""))
                        {
                            _stcChangeHd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockChangeGood);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCChangeHd(_prmTransNmbr).FileNmbr;
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

                STCChangeHd _stcChangeHd = this.db.STCChangeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcChangeHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STChangePost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockChangeGood);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCChangeHd(_prmTransNmbr).FileNmbr;
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

                STCChangeHd _stcChangeHd = this.db.STCChangeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcChangeHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STChangeUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockChangeGood);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCChangeHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCChangeHd(_prmTransNmbr).TransDate;
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

        #region STCChangeDt
        public int RowsCountSTCChangeDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _stcChangeDt in this.db.STCChangeDts
                                 where _stcChangeDt.TransNmbr == _prmCode
                                 select _stcChangeDt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCChangeDt> GetListSTCChangeDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCChangeDt> _result = new List<STCChangeDt>();

            try
            {
                var _query = (
                                from _stcChangeDt in this.db.STCChangeDts
                                where _stcChangeDt.TransNmbr == _prmCode
                                orderby _stcChangeDt.ProductSrc ascending
                                select new
                                {
                                    TransNmbr = _stcChangeDt.TransNmbr,
                                    ProductSrc = _stcChangeDt.ProductSrc,
                                    ProductSrcName = (
                                                        from _msProduct in this.db.MsProducts
                                                        where _msProduct.ProductCode == _stcChangeDt.ProductSrc
                                                        select _msProduct.ProductName
                                                     ).FirstOrDefault(),
                                    LocationSrc = _stcChangeDt.LocationSrc,
                                    LocationSrcName = (
                                                        from _msWrhsLoc in this.db.MsWrhsLocations
                                                        where _msWrhsLoc.WLocationCode == _stcChangeDt.LocationSrc
                                                        select _msWrhsLoc.WLocationName
                                                      ).FirstOrDefault(),
                                    ProductDest = _stcChangeDt.ProductDest,
                                    ProductDestName = (
                                                        from _msProduct in this.db.MsProducts
                                                        where _msProduct.ProductCode == _stcChangeDt.ProductDest
                                                        select _msProduct.ProductName
                                                     ).FirstOrDefault(),
                                    LocationDest = _stcChangeDt.LocationDest,
                                    LocationDestName = (
                                                        from _msWrhsLoc in this.db.MsWrhsLocations
                                                        where _msWrhsLoc.WLocationCode == _stcChangeDt.LocationDest
                                                        select _msWrhsLoc.WLocationName
                                                      ).FirstOrDefault(),
                                    Qty = _stcChangeDt.Qty,
                                    Unit = _stcChangeDt.Unit
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCChangeDt(_row.TransNmbr, _row.ProductSrc, _row.ProductSrcName, _row.LocationSrc, _row.LocationSrcName, _row.ProductDest, _row.ProductDestName, _row.LocationDest, _row.LocationDestName, _row.Qty, _row.Unit));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCChangeDt GetSingleSTCChangeDt(string _prmCode, string _prmProductCode, string _prmLocationSrc)
        {
            STCChangeDt _result = null;

            try
            {
                _result = this.db.STCChangeDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductSrc == _prmProductCode && _temp.LocationSrc == _prmLocationSrc);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCChangeDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('|');

                    STCChangeDt _STCChangeDt = this.db.STCChangeDts.Single(_temp => _temp.ProductSrc == _tempSplit[0] && _temp.LocationSrc == _tempSplit[1] && _temp.TransNmbr == _prmTransNo);

                    this.db.STCChangeDts.DeleteOnSubmit(_STCChangeDt);
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

        public bool AddSTCChangeDt(STCChangeDt _prmSTCChangeDt)
        {
            bool _result = false;

            try
            {
                this.db.STCChangeDts.InsertOnSubmit(_prmSTCChangeDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCChangeDt(STCChangeDt _prmSTCChangeDt)
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

        ~StockChangeGoodBL()
        {
        }
    }
}
