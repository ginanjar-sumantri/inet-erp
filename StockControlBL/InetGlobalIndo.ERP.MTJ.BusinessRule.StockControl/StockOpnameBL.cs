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
    public sealed class StockOpnameBL : Base
    {
        public StockOpnameBL()
        {

        }

        #region STCOpnameHd
        public double RowsCountSTCOpnameHd(string _prmCategory, string _prmKeyword)
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
                    from _stcOpnameHd in this.db.STCOpnameHds
                    where (SqlMethods.Like(_stcOpnameHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like((_stcOpnameHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && _stcOpnameHd.Status != StockOpnameDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcOpnameHd
                ).Count();

            _result = _query;

            return _result;
        }

        public List<STCOpnameHd> GetListSTCOpnameHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCOpnameHd> _result = new List<STCOpnameHd>();

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
                                from _stcOpnameHd in this.db.STCOpnameHds
                                where (SqlMethods.Like(_stcOpnameHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_stcOpnameHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _stcOpnameHd.Status != StockOpnameDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcOpnameHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _stcOpnameHd.TransNmbr,
                                    FileNmbr = _stcOpnameHd.FileNmbr,
                                    TransDate = _stcOpnameHd.TransDate,
                                    Status = _stcOpnameHd.Status,
                                    WrhsCode = _stcOpnameHd.WrhsCode,
                                    WrhsName = (
                                                    from _msWrhs in this.db.MsWarehouses
                                                    where _msWrhs.WrhsCode == _stcOpnameHd.WrhsCode
                                                    select _msWrhs.WrhsName
                                                ).FirstOrDefault(),
                                    WrhsFgSubLed = _stcOpnameHd.WrhsFgSubLed,
                                    WrhsSubLed = _stcOpnameHd.WrhsSubLed,
                                    Operator = _stcOpnameHd.Operator,
                                    Remark = _stcOpnameHd.Remark,
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCOpnameHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.WrhsCode, _row.WrhsName, _row.WrhsFgSubLed, _row.WrhsSubLed, _row.Operator, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCOpnameHd> GetListSTCOpnameForDDL()
        {
            List<STCOpnameHd> _result = new List<STCOpnameHd>();

            try
            {
                var _query = (
                                from _stcOpnameHd in this.db.STCOpnameHds
                                where _stcOpnameHd.Status == StockOpnameDataMapper.GetStatus(TransStatus.Posted)
                                    && _stcOpnameHd.DoneAdjust == YesNoDataMapper.GetYesNo(YesNo.No)
                                    && ((_stcOpnameHd.FileNmbr ?? "").Trim() == _stcOpnameHd.FileNmbr.Trim())
                                orderby _stcOpnameHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _stcOpnameHd.TransNmbr,
                                    FileNmbr = _stcOpnameHd.FileNmbr
                                }
                              );

                foreach (var _row in _query)
                {
                    _result.Add(new STCOpnameHd(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        public List<MsProduct> GetListProductFromSTCOpnameForDDL(string _prmOpnameNo)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _stcOpnameDt in this.db.STCOpnameDts
                                where _stcOpnameDt.TransNmbr == _prmOpnameNo
                                select new
                                {
                                    ProductCode = _stcOpnameDt.ProductCode,
                                    ProductName = (
                                                        from _msProduct in this.db.MsProducts
                                                        where _msProduct.ProductCode == _stcOpnameDt.ProductCode
                                                        select _msProduct.ProductName
                                                   ).FirstOrDefault()
                                }
                             ).Distinct();

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

        public STCOpnameHd GetSingleSTCOpnameHd(string _prmCode)
        {
            STCOpnameHd _result = null;

            try
            {
                _result = this.db.STCOpnameHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCOpnameHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCOpnameHd _stcOpnameHd = this.db.STCOpnameHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcOpnameHd != null)
                    {
                        if (_stcOpnameHd.Status != StockOpnameDataMapper.GetStatus(TransStatus.Posted))
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

        public string AddSTCOpnameHd(STCOpnameHd _prmSTCOpnameHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCOpnameHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCOpnameHds.InsertOnSubmit(_prmSTCOpnameHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCOpnameHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCOpnameHd(STCOpnameHd _prmSTCOpnameHd)
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

        public bool DeleteMultiSTCOpnameHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCOpnameHd _stcOpnameHd = this.db.STCOpnameHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcOpnameHd != null)
                    {
                        if ((_stcOpnameHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCOpnameDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCOpnameDts.DeleteAllOnSubmit(_query);

                            this.db.STCOpnameHds.DeleteOnSubmit(_stcOpnameHd);

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

        public bool DeleteMultiApproveSTCOpnameHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCOpnameHd _stcOpnameHd = this.db.STCOpnameHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcOpnameHd.Status == StockOpnameDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcOpnameHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcOpnameHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcOpnameHd != null)
                    {
                        if ((_stcOpnameHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCOpnameDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCOpnameDts.DeleteAllOnSubmit(_query);

                            this.db.STCOpnameHds.DeleteOnSubmit(_stcOpnameHd);

                            _result = true;
                        }
                        else if (_stcOpnameHd.FileNmbr != "" && _stcOpnameHd.Status == StockOpnameDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcOpnameHd.Status = StockOpnameDataMapper.GetStatus(TransStatus.Deleted);
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

        public string GetWarehouseCodeByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _stcOpnameHd in this.db.STCOpnameHds
                                where _stcOpnameHd.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    WrhsCode = _stcOpnameHd.WrhsCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.WrhsCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_STOpnameGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockOpname);
                    _transActivity.TransNmbr = _prmCode.ToString();
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

        public string Approve(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_STOpnameApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        STCOpnameHd _stcOpnameHd = this.GetSingleSTCOpnameHd(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcOpnameHd.TransDate.Year, _stcOpnameHd.TransDate.Month, AppModule.GetValue(TransactionType.StockOpname), this._companyTag, ""))
                        {
                            _stcOpnameHd.FileNmbr = item.Number;
                        }
                        
                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockOpname);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCOpnameHd(_prmCode).FileNmbr;
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

        public string Posting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCOpnameHd _stcOpnameHd = this.db.STCOpnameHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcOpnameHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_STOpnamePost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockOpname);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCOpnameHd(_prmCode).FileNmbr;
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

        public string UnPosting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                STCOpnameHd _stcOpnameHd = this.db.STCOpnameHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcOpnameHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_STOpnameUnPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockOpname);
                        //_transActivity.TransNmbr = _prmCode.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCOpnameHd(_prmCode).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCOpnameHd(_prmCode).TransDate;
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
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        #endregion

        #region STCOpnameDt

        public int RowsCountSTCOpnameDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.STCOpnameDts.Where(_row => _row.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()).Count();

            return _result;
        }

        public List<STCOpnameDt> GetListSTCOpnameDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCOpnameDt> _result = new List<STCOpnameDt>();

            try
            {
                var _query =
                            (
                                from _stcOpnameDt in this.db.STCOpnameDts
                                where _stcOpnameDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _stcOpnameDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcOpnameDt.TransNmbr,
                                    ProductCode = _stcOpnameDt.ProductCode,
                                    LocationCode = _stcOpnameDt.LocationCode,
                                    QtySystem = _stcOpnameDt.QtySystem,
                                    QtyActual = _stcOpnameDt.QtyActual,
                                    QtyOpname = _stcOpnameDt.QtyOpname,
                                    Unit = _stcOpnameDt.Unit,
                                    Remark = _stcOpnameDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCOpnameDt(_row.TransNmbr, _row.ProductCode, _row.LocationCode, _row.QtySystem, _row.QtyActual, _row.QtyOpname, _row.Unit, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCOpnameDt GetSingleSTCOpnameDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        {
            STCOpnameDt _result = null;

            try
            {
                _result = this.db.STCOpnameDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _prmLocationCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCOpnameDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('|');

                    STCOpnameDt _stcOpnameDt = this.db.STCOpnameDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    this.db.STCOpnameDts.DeleteOnSubmit(_stcOpnameDt);
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


        public bool AddSTCOpnameDt(STCOpnameDt _prmSTCOpnameDt)
        {
            bool _result = false;
            try
            {
                this.db.STCOpnameDts.InsertOnSubmit(_prmSTCOpnameDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCOpnameDt(STCOpnameDt _prmSTCOpnameDt)
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

        public List<MsWrhsLocation> GetListLocationFromSTCOpnameDt(string _prmCode, string _prmProductCode)
        {
            List<MsWrhsLocation> _result = new List<MsWrhsLocation>();

            try
            {
                var _query =
                            (
                                from _stcOpnameDt in this.db.STCOpnameDts
                                where _stcOpnameDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                    && _stcOpnameDt.ProductCode == _prmProductCode
                                orderby _stcOpnameDt.ProductCode ascending
                                select new
                                {
                                    WLocationCode = _stcOpnameDt.LocationCode,
                                    WLocationName = (
                                                        from _msWrhsLocation in this.db.MsWrhsLocations
                                                        where _msWrhsLocation.WLocationCode == _stcOpnameDt.LocationCode
                                                        select _msWrhsLocation.WLocationName
                                                    ).FirstOrDefault()
                                }
                            ).Distinct();

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

        public decimal GetQtyOpnameSTCOpnameDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _stcOpnameDt in this.db.STCOpnameDts
                                where _stcOpnameDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                    && _stcOpnameDt.ProductCode == _prmProductCode
                                    && _stcOpnameDt.LocationCode == _prmLocationCode
                                select _stcOpnameDt.QtyOpname
                            ).FirstOrDefault();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetUnitSTCOpnameDt(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _stcOpnameDt in this.db.STCOpnameDts
                                where _stcOpnameDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select _stcOpnameDt.Unit
                            ).FirstOrDefault();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~StockOpnameBL()
        {

        }
    }
}
