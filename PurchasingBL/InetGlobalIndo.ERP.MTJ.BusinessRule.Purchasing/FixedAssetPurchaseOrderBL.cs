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
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing
{
    public sealed class FixedAssetPurchaseOrderBL : Base
    {
        public FixedAssetPurchaseOrderBL()
        {

        }

        private UnitBL _unitBL = new UnitBL();
        private StockIssueRequestFABL _stcIssueRequestFABL = new StockIssueRequestFABL();
        private GLBudgetBL _glBudgetBL = new GLBudgetBL();

        #region PRCFAPOHd
        public double RowsCountPRCFAPOHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }

            var _query =
                        (
                            from _PRCFAPOHds in this.db.PRCFAPOHds
                            join _msSupplier in this.db.MsSuppliers
                                on _PRCFAPOHds.SuppCode equals _msSupplier.SuppCode
                            where (SqlMethods.Like(_PRCFAPOHds.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_PRCFAPOHds.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _PRCFAPOHds.Status != FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Deleted)
                            select _PRCFAPOHds.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<PRCFAPOHd> GetListPRCFAPOHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<PRCFAPOHd> _result = new List<PRCFAPOHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }

            try
            {
                var _query = (
                                from _PRCFAPOHd in this.db.PRCFAPOHds
                                join _msSupplier in this.db.MsSuppliers
                                    on _PRCFAPOHd.SuppCode equals _msSupplier.SuppCode
                                where (SqlMethods.Like(_PRCFAPOHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && (SqlMethods.Like((_PRCFAPOHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                  && _PRCFAPOHd.Status != FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _PRCFAPOHd.TransDate descending
                                select new
                                {
                                    TransNmbr = _PRCFAPOHd.TransNmbr,
                                    FileNmbr = _PRCFAPOHd.FileNmbr,
                                    TransDate = _PRCFAPOHd.TransDate,
                                    Status = _PRCFAPOHd.Status,
                                    CurrCode = _PRCFAPOHd.CurrCode,
                                    SuppName = _msSupplier.SuppName,
                                    TotalForex = _PRCFAPOHd.TotalForex
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new PRCFAPOHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.CurrCode, _row.SuppName, _row.TotalForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSinglePRCFAPOHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {

                    PRCFAPOHd _prcFAPOHd = this.db.PRCFAPOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_prcFAPOHd != null)
                    {
                        if (_prcFAPOHd.Status != FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Posted))
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

        public PRCFAPOHd GetSinglePRCFAPOHd(string _prmCode)
        {
            PRCFAPOHd _result = null;

            try
            {
                _result = this.db.PRCFAPOHds.Single(_temp => (_temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public PRCFAPOHd GetSinglePRCFAPOHdForFAPONoDDL(string _prmCode)
        {
            PRCFAPOHd _result = new PRCFAPOHd();

            try
            {
                var _queryDt = (from _prcFAPODt in this.db.PRCFAPODts
                                where _prcFAPODt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                    {
                                        _prcFAPODt.TransNmbr,
                                        _prcFAPODt.FANAme,
                                        _prcFAPODt.QtyRR,
                                        _prcFAPODt.Qty,
                                        _prcFAPODt.PriceForex
                                    }
                                );
                decimal _price = 0;
                foreach (var _row in _queryDt)
	            {
                    Decimal _qtyRR = Convert.ToDecimal(_row.QtyRR== null ? 0:_row.QtyRR);
                    if ((_row.Qty - _qtyRR) > 0)
            	        _price = _price + ((_row.Qty - _qtyRR)* _row.PriceForex);                    
	            }

                //_result = this.db.PRCFAPOHds.Single(_temp => (_temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()));
                var _query = (from _prcFAPOHd in this.db.PRCFAPOHds
                              where _prcFAPOHd.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                              select new
                              {
                                  _prcFAPOHd.TransNmbr,
                                  _prcFAPOHd.PPN,
                                  _prcFAPOHd.BaseForex,
                                  _prcFAPOHd.PPNForex,
                                  _prcFAPOHd.Disc,
                                  _prcFAPOHd.DiscForex
                              }
                            ).FirstOrDefault();

                _result.TransNmbr = _query.TransNmbr;
                _result.PPN = _query.PPN;
                _result.BaseForex = _price;
                Decimal _discon = (_price * _query.Disc) /100;
                _result.PPNForex = ((_price - _discon) * _query.PPN)/100;
                _result.DiscForex = (_discon);
                _result.TotalForex = (_price - _discon) + _result.PPNForex;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFileNmbrPRCFAPOHd(string _prmCode)
        {
            string _result = null;

            try
            {
                _result = (this.db.PRCFAPOHds.Single(_temp => (_temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower())).FileNmbr ?? "").Trim();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddPRCFAPOHd(PRCFAPOHd _prmPRCFAPOHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmPRCFAPOHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.PRCFAPOHds.InsertOnSubmit(_prmPRCFAPOHd);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmPRCFAPOHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPRCFAPOHd(PRCFAPOHd _prmPRCFAPOHd)
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

        public bool DeleteMultiPRCFAPOHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    PRCFAPOHd _PRCFAPOHd = this.db.PRCFAPOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_PRCFAPOHd != null)
                    {
                        if ((_PRCFAPOHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query2 = (from _detail in this.db.PRCFAPODts
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.PRCFAPODts.DeleteAllOnSubmit(_query2);

                            this.db.PRCFAPOHds.DeleteOnSubmit(_PRCFAPOHd);

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

        public bool DeleteMultiApprovePRCFAPOHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    PRCFAPOHd _PRCFAPOHd = this.db.PRCFAPOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_PRCFAPOHd.Status == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _PRCFAPOHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _PRCFAPOHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_PRCFAPOHd != null)
                    {
                        if ((_PRCFAPOHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query2 = (from _detail in this.db.PRCFAPODts
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.PRCFAPODts.DeleteAllOnSubmit(_query2);

                            this.db.PRCFAPOHds.DeleteOnSubmit(_PRCFAPOHd);

                            _result = true;
                        }
                        else if (_PRCFAPOHd.FileNmbr != "" && _PRCFAPOHd.Status == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _PRCFAPOHd.Status = FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Deleted);
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

        public List<PRCFAPOHd> GetListDDLFAPOBySupplier(string _prmSuppCode)
        {
            List<PRCFAPOHd> _result = new List<PRCFAPOHd>();

            try
            {
                var _query = (
                                from _PRCFAPOHd in this.db.PRCFAPOHds
                                join _prcFAPODt in this.db.PRCFAPODts
                                on _PRCFAPOHd.TransNmbr equals _prcFAPODt.TransNmbr
                                where _PRCFAPOHd.SuppCode.Trim().ToLower() == _prmSuppCode.Trim().ToLower()
                                    && _PRCFAPOHd.Status == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Posted)
                                    && (_prcFAPODt.Qty - ((_prcFAPODt.QtyRR == null) ? 0 : _prcFAPODt.QtyRR)) > 0
                                orderby _PRCFAPOHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _PRCFAPOHd.TransNmbr,
                                    FileNmbr = _PRCFAPOHd.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new PRCFAPOHd(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string Closing(string _prmTransNmbr, string _prmFAName, string _prmRemark, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spPRC_FAPOClosing(_prmTransNmbr, _prmFAName, _prmRemark, _prmuser, ref _result);
            }
            catch (Exception ex)
            {
                _result = "Closing Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string GetApproval(string _prmTransNmbr, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spPRC_FAPOGetAppr(_prmTransNmbr, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.FixedAssetPurchaseOrder);
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

        public string Approve(string _prmTransNmbr, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.spPRC_FAPOApprove(_prmTransNmbr, _prmuser, ref _result);

                    if (_result == "")
                    {
                        PRCFAPOHd _prcFAPOHd = this.GetSinglePRCFAPOHd(_prmTransNmbr);

                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_prcFAPOHd.TransDate.Year, _prcFAPOHd.TransDate.Month, AppModule.GetValue(TransactionType.FixedAssetPurchaseOrder), this._companyTag, ""))
                        {
                            _prcFAPOHd.FileNmbr = _item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FixedAssetPurchaseOrder);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSinglePRCFAPOHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();
                        _scope.Complete();

                        _result = "Approve Success";
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

        public string Posting(string _prmTransNmbr, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                PRCFAPOHd _PRCFAPOHd = this.db.PRCFAPOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_PRCFAPOHd.TransDate);
                if (_locked == "")
                {
                    this.db.spPRC_FAPOPost(_prmTransNmbr, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FixedAssetPurchaseOrder);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSinglePRCFAPOHd(_prmTransNmbr).FileNmbr;
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

        public string Unposting(string _prmTransNmbr, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                PRCFAPOHd _PRCFAPOHd = this.db.PRCFAPOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_PRCFAPOHd.TransDate);
                if (_locked == "")
                {
                    this.db.spPRC_FAPOUnPost(_prmTransNmbr, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";
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

        #endregion

        #region PRCFAPODt

        public List<PRCFAPODt> GetListPRCFAPODt(string _prmCode)
        {
            List<PRCFAPODt> _result = new List<PRCFAPODt>();

            try
            {
                var _query = (
                                from _PRCFAPODt in this.db.PRCFAPODts
                                where _PRCFAPODt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _PRCFAPODt.FANAme ascending
                                select new
                                {
                                    TransNmbr = _PRCFAPODt.TransNmbr,
                                    FANAme = _PRCFAPODt.FANAme,
                                    Qty = _PRCFAPODt.Qty,
                                    Unit = _PRCFAPODt.Unit,
                                    UnitName = (
                                            from _msUnit in this.db.MsUnits
                                            where _msUnit.UnitCode.Trim().ToLower() == _PRCFAPODt.Unit.Trim().ToLower()
                                            select _msUnit.UnitName
                                           ).FirstOrDefault(),
                                    PriceForex = _PRCFAPODt.PriceForex,
                                    AmountForex = _PRCFAPODt.AmountForex,
                                    DoneClosing = _PRCFAPODt.DoneClosing,
                                    QtyClose = _PRCFAPODt.QtyClose,
                                    CreatedBy = _PRCFAPODt.CreatedBy
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new PRCFAPODt(_row.TransNmbr, _row.FANAme, _row.Qty, _row.Unit, _row.UnitName, _row.PriceForex, _row.AmountForex, _row.DoneClosing, _row.QtyClose, _row.CreatedBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public PRCFAPODt GetSinglePRCFAPODt(string _prmCode, string _prmFAName)
        {
            PRCFAPODt _result = null;

            try
            {
                _result = this.db.PRCFAPODts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.FANAme.Trim().ToLower() == _prmFAName.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPRCFAPODt(string[] _prmCode, String _prmTransNmbr)
        {
            bool _result = false;

            decimal _tempBaseForex = 0;
            decimal _tempDiscForex = 0;
            decimal _tempDPForex = 0;
            decimal _tempPPHForex = 0;
            decimal _tempPPNForex = 0;
            decimal _tempTotalForex = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    PRCFAPODt _PRCFAPODt = this.db.PRCFAPODts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _temp.FANAme.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.PRCFAPODts.DeleteOnSubmit(_PRCFAPODt);

                    ////////

                    PRCFAPOHd _PRCFAPOHd = this.GetSinglePRCFAPOHd(_prmTransNmbr);

                    decimal _nettoForex = Convert.ToDecimal((_PRCFAPODt.AmountForex == null) ? 0 : _PRCFAPODt.AmountForex);

                    _tempBaseForex = _PRCFAPOHd.BaseForex - _nettoForex;
                    _tempDiscForex = _tempBaseForex * (_PRCFAPOHd.Disc / 100);
                    _tempDPForex = (_tempBaseForex - _tempDiscForex) * (_PRCFAPOHd.DP / 100);
                    _tempPPHForex = (_tempBaseForex - _tempDiscForex) * (_PRCFAPOHd.PPH / 100);
                    _tempPPNForex = (_tempBaseForex - _tempDiscForex) * (_PRCFAPOHd.PPN / 100);
                    _tempTotalForex = _tempBaseForex - _tempDiscForex + _tempPPNForex + _tempPPHForex;

                    _PRCFAPOHd.BaseForex = _tempBaseForex;
                    _PRCFAPOHd.DiscForex = _tempDiscForex;
                    _PRCFAPOHd.DPForex = _tempDPForex;
                    _PRCFAPOHd.PPHForex = _tempPPHForex;
                    _PRCFAPOHd.PPNForex = _tempPPNForex;
                    _PRCFAPOHd.TotalForex = _tempTotalForex;
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

        public bool AddPRCFAPODt(PRCFAPODt _prmPRCFAPODt)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;
            decimal _tempDiscForex = 0;
            decimal _tempDPForex = 0;
            decimal _tempPPHForex = 0;
            decimal _tempPPNForex = 0;
            decimal _tempTotalForex = 0;

            try
            {
                PRCFAPOHd _PRCFAPOHd = this.GetSinglePRCFAPOHd(_prmPRCFAPODt.TransNmbr);

                decimal _nettoForex = Convert.ToDecimal((_prmPRCFAPODt.AmountForex == null) ? 0 : _prmPRCFAPODt.AmountForex);

                _tempBaseForex = _PRCFAPOHd.BaseForex + _nettoForex;
                _tempDiscForex = _tempBaseForex * (_PRCFAPOHd.Disc / 100);
                _tempDPForex = (_tempBaseForex - _tempDiscForex) * (_PRCFAPOHd.DP / 100);
                _tempPPHForex = (_tempBaseForex - _tempDiscForex) * (_PRCFAPOHd.PPH / 100);
                _tempPPNForex = (_tempBaseForex - _tempDiscForex) * (_PRCFAPOHd.PPN / 100);
                _tempTotalForex = _tempBaseForex - _tempDiscForex + _tempPPNForex + _tempPPHForex;

                _PRCFAPOHd.BaseForex = _tempBaseForex;
                _PRCFAPOHd.DiscForex = _tempDiscForex;
                _PRCFAPOHd.DPForex = _tempDPForex;
                _PRCFAPOHd.PPHForex = _tempPPHForex;
                _PRCFAPOHd.PPNForex = _tempPPNForex;
                _PRCFAPOHd.TotalForex = _tempTotalForex;

                this.db.PRCFAPODts.InsertOnSubmit(_prmPRCFAPODt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPRCFAPODt(PRCFAPODt _prmPRCFAPODt, decimal _prmNettoOriginal)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;
            decimal _tempDiscForex = 0;
            decimal _tempDPForex = 0;
            decimal _tempPPHForex = 0;
            decimal _tempPPNForex = 0;
            decimal _tempTotalForex = 0;


            try
            {
                PRCFAPOHd _PRCFAPOHd = this.GetSinglePRCFAPOHd(_prmPRCFAPODt.TransNmbr);

                decimal _nettoForex = Convert.ToDecimal((_prmPRCFAPODt.AmountForex == null) ? 0 : _prmPRCFAPODt.AmountForex);

                _tempBaseForex = _PRCFAPOHd.BaseForex - _prmNettoOriginal;
                _tempBaseForex = _tempBaseForex + _nettoForex;
                _tempDiscForex = _tempBaseForex * (_PRCFAPOHd.Disc / 100);
                _tempDPForex = (_tempBaseForex - _tempDiscForex) * (_PRCFAPOHd.DP / 100);
                _tempPPHForex = (_tempBaseForex - _tempDiscForex) * (_PRCFAPOHd.PPH / 100);
                _tempPPNForex = (_tempBaseForex - _tempDiscForex) * (_PRCFAPOHd.PPN / 100);
                _tempTotalForex = _tempBaseForex - _tempDiscForex + _tempPPNForex + _tempPPHForex;

                _PRCFAPOHd.BaseForex = _tempBaseForex;
                _PRCFAPOHd.DiscForex = _tempDiscForex;
                _PRCFAPOHd.DPForex = _tempDPForex;
                _PRCFAPOHd.PPHForex = _tempPPHForex;
                _PRCFAPOHd.PPNForex = _tempPPNForex;
                _PRCFAPOHd.TotalForex = _tempTotalForex;

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

        ~FixedAssetPurchaseOrderBL()
        {

        }
    }
}
