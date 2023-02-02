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
    public sealed class StockReceivingSupplierBL : Base
    {
        public StockReceivingSupplierBL()
        {

        }

        #region STCRROtherHd
        public double RowsCountSTCRROtherHd(string _prmCategory, string _prmKeyword)
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
                    from _stcRROtherHd in this.db.STCRROtherHds
                    where _stcRROtherHd.RRType.Trim().ToLower() == StockReceivingOtherDataMapper.GetRRTypeStatus(RRTypeStatus.Supplier).ToString().Trim().ToLower()
                    && (SqlMethods.Like(_stcRROtherHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    && (SqlMethods.Like((_stcRROtherHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                    && _stcRROtherHd.Status != StockReceivingSupplierDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcRROtherHd
                ).Count();

            _result = _query;

            return _result;
        }

        public List<STCRROtherHd> GetListSTCRROtherHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCRROtherHd> _result = new List<STCRROtherHd>();

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
                                from _stcRROtherHd in this.db.STCRROtherHds
                                where _stcRROtherHd.RRType.Trim().ToLower() == StockReceivingOtherDataMapper.GetRRTypeStatus(RRTypeStatus.Supplier).ToString().Trim().ToLower()
                                && (SqlMethods.Like(_stcRROtherHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_stcRROtherHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _stcRROtherHd.Status != StockReceivingSupplierDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcRROtherHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _stcRROtherHd.TransNmbr,
                                    FileNmbr = _stcRROtherHd.FileNmbr,
                                    TransDate = _stcRROtherHd.TransDate,
                                    Status = _stcRROtherHd.Status,
                                    WrhsCode = _stcRROtherHd.WrhsCode,
                                    WrhsName = (
                                                    from _msWrhs in this.db.MsWarehouses
                                                    where _msWrhs.WrhsCode == _stcRROtherHd.WrhsCode
                                                    select _msWrhs.WrhsName
                                                ).FirstOrDefault(),
                                    WrhsFgSubLed = _stcRROtherHd.WrhsFgSubLed,
                                    WrhsSubLed = _stcRROtherHd.WrhsSubLed,
                                    StockType = _stcRROtherHd.StockType,
                                    StockTypeName = (
                                                    from _msStockType in this.db.MsStockTypes
                                                    where _msStockType.StockTypeCode == _stcRROtherHd.StockType
                                                    select _msStockType.StockTypeName
                                                ).FirstOrDefault(),
                                    Remark = _stcRROtherHd.Remark,
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCRROtherHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.WrhsCode, _row.WrhsName, _row.WrhsFgSubLed, _row.WrhsSubLed, _row.StockType, _row.StockTypeName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCRROtherHd GetSingleSTCRROtherHd(string _prmCode)
        {
            STCRROtherHd _result = null;

            try
            {
                _result = this.db.STCRROtherHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCRROtherHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCRROtherHd _stcRROtherHd = this.db.STCRROtherHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcRROtherHd != null)
                    {
                        if (_stcRROtherHd.Status != StockReceivingSupplierDataMapper.GetStatus(TransStatus.Posted))
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

        public string AddSTCRROtherHd(STCRROtherHd _prmSTCRROtherHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCRROtherHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCRROtherHds.InsertOnSubmit(_prmSTCRROtherHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCRROtherHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCRROtherHd(STCRROtherHd _prmSTCRROtherHd)
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

        public bool DeleteMultiSTCRROtherHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCRROtherHd _stcRROtherHd = this.db.STCRROtherHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcRROtherHd != null)
                    {
                        if ((_stcRROtherHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCRROtherDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCRROtherDts.DeleteAllOnSubmit(_query);

                            this.db.STCRROtherHds.DeleteOnSubmit(_stcRROtherHd);

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

        public bool DeleteMultiApproveSTCRROtherHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCRROtherHd _stcRROtherHd = this.db.STCRROtherHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcRROtherHd.Status == StockReceivingSupplierDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcRROtherHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcRROtherHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcRROtherHd != null)
                    {
                        if ((_stcRROtherHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCRROtherDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCRROtherDts.DeleteAllOnSubmit(_query);

                            this.db.STCRROtherHds.DeleteOnSubmit(_stcRROtherHd);

                            _result = true;
                        }
                        else if (_stcRROtherHd.FileNmbr != "" && _stcRROtherHd.Status == StockReceivingSupplierDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcRROtherHd.Status = StockReceivingSupplierDataMapper.GetStatus(TransStatus.Deleted);
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
                                from _stcRROtherHd in this.db.STCRROtherHds
                                where _stcRROtherHd.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    WrhsCode = _stcRROtherHd.WrhsCode
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
                this.db.S_STRRSupplierGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingSupplier);
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
                    this.db.S_STRRSupplierApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        STCRROtherHd _stcRROtherHd = this.GetSingleSTCRROtherHd(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcRROtherHd.TransDate.Year, _stcRROtherHd.TransDate.Month, AppModule.GetValue(TransactionType.StockReceivingSupplier), this._companyTag, ""))
                        {
                            _stcRROtherHd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingSupplier);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCRROtherHd(_prmCode).FileNmbr;
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

                STCRROtherHd _stcRROtherHd = this.db.STCRROtherHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.RRType == StockReceivingOtherDataMapper.GetRRTypeStatus(RRTypeStatus.Supplier).ToString());
                String _locked = _transCloseBL.IsExistAndLocked(_stcRROtherHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_STRRSupplierPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingSupplier);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCRROtherHd(_prmCode).FileNmbr;
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

                STCRROtherHd _stcRROtherHd = this.db.STCRROtherHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.RRType == StockReceivingOtherDataMapper.GetRRTypeStatus(RRTypeStatus.Supplier).ToString());
                String _locked = _transCloseBL.IsExistAndLocked(_stcRROtherHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_STRRSupplierUnPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingSupplier);
                        //_transActivity.TransNmbr = _prmCode.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCRROtherHd(_prmCode).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCRROtherHd(_prmCode).TransDate;
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

        #region STCRROtherDt

        public int RowsCountSTCRROtherDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.STCRROtherDts.Where(_row => _row.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()).Count();

            return _result;
        }

        public List<STCRROtherDt> GetListSTCRROtherDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCRROtherDt> _result = new List<STCRROtherDt>();

            try
            {
                var _query =
                            (
                                from _stcSTCRROtherDt in this.db.STCRROtherDts
                                where _stcSTCRROtherDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _stcSTCRROtherDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcSTCRROtherDt.TransNmbr,
                                    ProductCode = _stcSTCRROtherDt.ProductCode,
                                    LocationCode = _stcSTCRROtherDt.LocationCode,
                                    Qty = _stcSTCRROtherDt.Qty,
                                    Unit = _stcSTCRROtherDt.Unit,
                                    Remark = _stcSTCRROtherDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCRROtherDt(_row.TransNmbr, _row.ProductCode, _row.LocationCode, _row.Qty, _row.Unit, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCRROtherDt GetSingleSTCRROtherDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        {
            STCRROtherDt _result = null;

            try
            {
                _result = this.db.STCRROtherDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _prmLocationCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCRROtherDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    STCRROtherDt _stcRROtherDt = this.db.STCRROtherDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    this.db.STCRROtherDts.DeleteOnSubmit(_stcRROtherDt);
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


        public bool AddSTCRROtherDt(STCRROtherDt _prmSTCRROtherDt)
        {
            bool _result = false;
            try
            {
                this.db.STCRROtherDts.InsertOnSubmit(_prmSTCRROtherDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCRROtherDt(STCRROtherDt _prmSTCRROtherDt)
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

        ~StockReceivingSupplierBL()
        {

        }
    }
}
