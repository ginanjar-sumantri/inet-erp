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
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class StockIssueSlipBL : Base
    {
        public StockIssueSlipBL()
        {
        }

        #region STCIssueSlipHd

        private StockIssueRequestBL _stcIssueRequestBL = new StockIssueRequestBL();
        private GLBudgetBL _glBudgetBL = new GLBudgetBL();

        public double RowsCountSTCIssueSlipHd(string _prmCategory, string _prmKeyword)
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
                    from _stcIssueSlipHd in this.db.STCIssueSlipHds
                    where (SqlMethods.Like(_stcIssueSlipHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like((_stcIssueSlipHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && _stcIssueSlipHd.Status != StockIssueSlipDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcIssueSlipHd
                ).Count();

            _result = _query;
            return _result;
        }

        public List<STCIssueSlipHd> GetListSTCIssueSlipHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCIssueSlipHd> _result = new List<STCIssueSlipHd>();

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
                                from _stcIssueSlipHd in this.db.STCIssueSlipHds
                                where (SqlMethods.Like(_stcIssueSlipHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_stcIssueSlipHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _stcIssueSlipHd.Status != StockIssueSlipDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcIssueSlipHd.CreatedDate descending
                                select new
                                {
                                    TransNmbr = _stcIssueSlipHd.TransNmbr,
                                    FileNmbr = _stcIssueSlipHd.FileNmbr,
                                    TransDate = _stcIssueSlipHd.TransDate,
                                    Status = _stcIssueSlipHd.Status,
                                    WrhsCode = _stcIssueSlipHd.WrhsCode,
                                    WrhsName = (
                                                    from _msWrhs in this.db.MsWarehouses
                                                    where _msWrhs.WrhsCode == _stcIssueSlipHd.WrhsCode
                                                    select _msWrhs.WrhsName
                                                ).FirstOrDefault(),
                                    RequestNo = _stcIssueSlipHd.RequestNo,
                                    FileNo = this.GetFileNmbrSTCRequestHd(_stcIssueSlipHd.RequestNo),
                                    RequestBy = _stcIssueSlipHd.RequestBy
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCIssueSlipHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.WrhsCode, _row.WrhsName, _row.RequestNo, _row.FileNo, _row.RequestBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFileNmbrSTCRequestHd(string _prmRequestNo)
        {
            string _result = "";

            try
            {
                _result = (this.db.STCRequestHds.Single(_temp => _temp.TransNmbr == _prmRequestNo).FileNmbr ?? "").Trim();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCIssueSlipHd GetSingleSTCIssueSlipHd(string _prmCode)
        {
            STCIssueSlipHd _result = null;

            try
            {
                _result = this.db.STCIssueSlipHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCIssueSlipHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCIssueSlipHd _stcIssueSlipHd = this.db.STCIssueSlipHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcIssueSlipHd != null)
                    {
                        if (_stcIssueSlipHd.Status != StockIssueSlipDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiSTCIssueSlipHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCIssueSlipHd _stcIssueSlipHd = this.db.STCIssueSlipHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcIssueRequestBL != null)
                    {
                        if ((_stcIssueSlipHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCIssueSlipDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCIssueSlipDts.DeleteAllOnSubmit(_query);

                            this.db.STCIssueSlipHds.DeleteOnSubmit(_stcIssueSlipHd);

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

        public bool DeleteMultiApproveSTCIssueSlipHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCIssueSlipHd _stcIssueSlipHd = this.db.STCIssueSlipHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcIssueSlipHd.Status == StockIssueSlipDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcIssueSlipHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcIssueSlipHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcIssueRequestBL != null)
                    {
                        if ((_stcIssueSlipHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCIssueSlipDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCIssueSlipDts.DeleteAllOnSubmit(_query);

                            this.db.STCIssueSlipHds.DeleteOnSubmit(_stcIssueSlipHd);

                            _result = true;
                        }
                        else if (_stcIssueSlipHd.FileNmbr != "" && _stcIssueSlipHd.Status == StockIssueSlipDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcIssueSlipHd.Status = StockIssueSlipDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddSTCIssueSlipHd(STCIssueSlipHd _prmSTCIssueSlipHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCIssueSlipHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                } 

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCIssueSlipHds.InsertOnSubmit(_prmSTCIssueSlipHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCIssueSlipHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        
        //public string AddSTCIssueSlipHd(STCIssueSlipHd _prmSTCIssueSlipHd, Boolean _prmFgSingleLocation, String _prmWhrsLocationCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
        //        foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
        //        {
        //            _prmSTCIssueSlipHd.TransNmbr = item.Number;
        //            _transactionNumber.TempTransNmbr = item.Number;
        //        }

        //        if (_prmFgSingleLocation)
        //        {
        //            var _queryReq = (from _stcReqDt in this.db.STCRequestDts
        //                             where _stcReqDt.TransNmbr == _prmSTCIssueSlipHd.RequestNo
        //                             select _stcReqDt
        //                );
        //            foreach (var _row in _queryReq)
        //            {
        //                STCIssueSlipDt _stcIssueSlipDt = new STCIssueSlipDt();
        //                _stcIssueSlipDt.TransNmbr = _prmSTCIssueSlipHd.TransNmbr;
        //                _stcIssueSlipDt.ProductCode = _row.ProductCode;
        //                _stcIssueSlipDt.LocationCode = _prmWhrsLocationCode;
        //                _stcIssueSlipDt.StockType = "SELIDRSBY";
        //                _stcIssueSlipDt.PriceCost = 0;
        //                _stcIssueSlipDt.Qty = _row.Qty;
        //                _stcIssueSlipDt.Unit = _row.Unit;
        //                _stcIssueSlipDt.Remark = _row.Remark;
        //                this.db.STCIssueSlipDts.InsertOnSubmit(_stcIssueSlipDt);
        //            }
        //        }


        //        this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
        //        this.db.STCIssueSlipHds.InsertOnSubmit(_prmSTCIssueSlipHd);

        //        var _query = (
        //                        from _temp in this.db.Temporary_TransactionNumbers
        //                        where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
        //                        select _temp
        //                      );

        //        this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

        //        this.db.SubmitChanges();

        //        _result = _prmSTCIssueSlipHd.TransNmbr;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool EditSTCIssueSlipHd(STCIssueSlipHd _prmSTCIssueSlipHd)
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

        //public bool EditSTCIssueSlipHd(STCIssueSlipHd _prmSTCIssueSlipHd, Boolean _prmFgSingleLocation, String _prmWhrsLocationCode)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        if(_prmFgSingleLocation)
        //        {
        //            var _query = (
        //                    from _stcIssueSlipDt in this.db.STCIssueSlipDts
        //                    where _stcIssueSlipDt.TransNmbr == _prmSTCIssueSlipHd.TransNmbr
        //                    select _stcIssueSlipDt
        //                );
        //            foreach (var _row in _query)
        //            {
        //                STCIssueSlipDt _stcIssueSlipDt = new STCIssueSlipDt();
        //                _stcIssueSlipDt.TransNmbr = _row.TransNmbr;
        //                _stcIssueSlipDt.ProductCode = _row.ProductCode;
        //                _stcIssueSlipDt.LocationCode = _prmWhrsLocationCode;
        //                _stcIssueSlipDt.Qty = _row.Qty;
        //                _stcIssueSlipDt.Unit = _row.Unit;
        //                _stcIssueSlipDt.Remark = _row.Remark;
        //                _stcIssueSlipDt.AccInvent = _row.AccInvent;
        //                _stcIssueSlipDt.FgInvent = _row.FgInvent;                        
        //                _stcIssueSlipDt.PriceCost = _row.PriceCost;
        //                _stcIssueSlipDt.TotalCost = _row.TotalCost;
        //                _stcIssueSlipDt.AccExpense = _row.AccExpense;
        //                _stcIssueSlipDt.TotalCost = _row.TotalCost;
        //                this.db.STCIssueSlipDts.DeleteOnSubmit(_row);
        //                this.db.STCIssueSlipDts.InsertOnSubmit(_stcIssueSlipDt);
        //            }
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

        public string GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_STIssueSlipGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.StockIssueSlip);
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

        public Boolean IsBudgetNotExist(String _prmTransNmbr)
        {
            Boolean _result = false;

            String _requestNo = this.GetSingleSTCIssueSlipHd(_prmTransNmbr).RequestNo;
            List<STCIssueSlipDt> _stcIssueSlipDt = this.GetListSTCIssueSlipDt(_prmTransNmbr);
            DateTime _date = this.GetSingleSTCIssueSlipHd(_prmTransNmbr).TransDate;
            String _orgUnit = _stcIssueRequestBL.GetSingleSTCRequestHd(_requestNo).OrgUnit;
            string _budgetCode = _glBudgetBL.GetBudgetCodeByOrgUnitAndDate(_orgUnit, _date);
            List<Guid> _budgetDetailCode = new List<Guid>();
            List<String> _account = new List<String>();

            if (_budgetCode == "")
            {
                _result = true;
            }
            else
            {
                foreach (STCIssueSlipDt _row in _stcIssueSlipDt)
                {
                    _account.Add((
                                    from _msStockType in this.db.MsStockTypes
                                    where _row.StockType == _msStockType.StockTypeCode
                                    select _msStockType.Account
                                ).FirstOrDefault());
                }

                foreach (String _row2 in _account)
                {
                    if (_glBudgetBL.GetBudgetDetailCodeByAccountAndBudgetCode(new Guid(_budgetCode), _row2) != new Guid())
                    {
                        _budgetDetailCode.Add(_glBudgetBL.GetBudgetDetailCodeByAccountAndBudgetCode(new Guid(_budgetCode), _row2));
                    }
                }

                if (_budgetDetailCode.Count == 0)
                {
                    _result = true;
                }
            }

            return _result;
        }

        public Boolean CheckAmountBudget(String _prmTransNmbr)
        {
            Boolean _result = false;

            String _requestNo = this.GetSingleSTCIssueSlipHd(_prmTransNmbr).RequestNo;
            List<STCIssueSlipDt> _stcIssueSlipDt = this.GetListSTCIssueSlipDt(_prmTransNmbr);
            List<String> _account = new List<String>();
            List<Guid> _budgetDetailCode = new List<Guid>();
            DateTime _date = this.GetSingleSTCIssueSlipHd(_prmTransNmbr).TransDate;
            String _orgUnit = _stcIssueRequestBL.GetSingleSTCRequestHd(_requestNo).OrgUnit;
            List<Decimal> _amount = new List<Decimal>();
            Decimal _price = 0;
            string _budgetCode = _glBudgetBL.GetBudgetCodeByOrgUnitAndDate(_orgUnit, _date);

            foreach (STCIssueSlipDt _row in _stcIssueSlipDt)
            {
                _account.Add((
                                from _msStockType in this.db.MsStockTypes
                                where _row.StockType == _msStockType.StockTypeCode
                                select _msStockType.Account
                            ).FirstOrDefault());

                _price = (
                                from _msProduct in this.db.MsProducts
                                where _row.ProductCode == _msProduct.ProductCode
                                select _msProduct.BuyingPrice
                          ).First();

                _amount.Add(_row.Qty * _price);
            }

            foreach (String _row2 in _account)
            {
                _budgetDetailCode.Add(_glBudgetBL.GetBudgetDetailCodeByAccountAndBudgetCode(new Guid(_budgetCode), _row2));
            }

            decimal _amountBudget = 0;
            decimal _amountActual = 0;

            foreach (Guid _row3 in _budgetDetailCode)
            {
                _amountBudget = _glBudgetBL.GetSingleGLBudgetAcc(new Guid(_budgetCode), _row3).AmountBudgetHome;
                _amountActual = _glBudgetBL.GetSingleGLBudgetAcc(new Guid(_budgetCode), _row3).AmountActual;

                foreach (Decimal _row4 in _amount)
                {
                    if (_amountBudget - _amountActual - _row4 >= 0)
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
                    int _success = this.db.S_STIssueSlipApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        STCIssueSlipHd _stcIssueSlipHd = this.GetSingleSTCIssueSlipHd(_prmTransNmbr);

                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcIssueSlipHd.TransDate.Year, _stcIssueSlipHd.TransDate.Month, AppModule.GetValue(TransactionType.StockIssueSlip), this._companyTag, ""))
                        {
                            _stcIssueSlipHd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockIssueSlip);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCIssueSlipHd(_prmTransNmbr).FileNmbr;
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

                STCIssueSlipHd _stcIssueSlipHd = this.db.STCIssueSlipHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcIssueSlipHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STIssueSlipPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockIssueSlip);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCIssueSlipHd(_prmTransNmbr).FileNmbr;
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

                STCIssueSlipHd _stcIssueSlipHd = this.db.STCIssueSlipHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcIssueSlipHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STIssueSlipUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = new Guid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockIssueSlip);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCIssueSlipHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCIssueSlipHd(_prmTransNmbr).TransDate;
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

        #region STCIssueSlipDt
        public int RowsCountSTCIssueSlipDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.STCIssueSlipDts.Where(_temp => _temp.TransNmbr == _prmCode).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCIssueSlipDt> GetListSTCIssueSlipDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCIssueSlipDt> _result = new List<STCIssueSlipDt>();

            try
            {
                var _query = (
                                from _stcIssueSlipDt in this.db.STCIssueSlipDts
                                where _stcIssueSlipDt.TransNmbr == _prmCode
                                orderby _stcIssueSlipDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcIssueSlipDt.TransNmbr,
                                    ProductCode = _stcIssueSlipDt.ProductCode,
                                    LocationCode = _stcIssueSlipDt.LocationCode,
                                    Qty = _stcIssueSlipDt.Qty,
                                    Unit = _stcIssueSlipDt.Unit,
                                    StockType = _stcIssueSlipDt.StockType,
                                    Remark = _stcIssueSlipDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCIssueSlipDt(_row.TransNmbr, _row.ProductCode, _row.LocationCode, _row.Qty, _row.Unit, _row.StockType, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCIssueSlipDt> GetListSTCIssueSlipDt(string _prmCode)
        {
            List<STCIssueSlipDt> _result = new List<STCIssueSlipDt>();

            try
            {
                var _query = (
                                from _stcIssueSlipDt in this.db.STCIssueSlipDts
                                where _stcIssueSlipDt.TransNmbr == _prmCode
                                orderby _stcIssueSlipDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcIssueSlipDt.TransNmbr,
                                    ProductCode = _stcIssueSlipDt.ProductCode,
                                    LocationCode = _stcIssueSlipDt.LocationCode,
                                    Qty = _stcIssueSlipDt.Qty,
                                    Unit = _stcIssueSlipDt.Unit,
                                    StockType = _stcIssueSlipDt.StockType,
                                    Remark = _stcIssueSlipDt.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new STCIssueSlipDt(_row.TransNmbr, _row.ProductCode, _row.LocationCode, _row.Qty, _row.Unit, _row.StockType, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCIssueSlipDt GetSingleSTCIssueSlipDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        {
            STCIssueSlipDt _result = null;

            try
            {
                _result = this.db.STCIssueSlipDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode && _temp.LocationCode == _prmLocationCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCIssueSlipDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('|');

                    STCIssueSlipDt _stcIssueSlipDt = this.db.STCIssueSlipDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr == _prmTransNo.Trim().ToLower());

                    this.db.STCIssueSlipDts.DeleteOnSubmit(_stcIssueSlipDt);
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

        public bool AddSTCIssueSlipDt(STCIssueSlipDt _prmSTCIssueSlipDt)
        {
            bool _result = false;

            try
            {
                this.db.STCIssueSlipDts.InsertOnSubmit(_prmSTCIssueSlipDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCIssueSlipDt(STCIssueSlipDt _prmSTCIssueSlipDt)
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

        ~StockIssueSlipBL()
        {
        }
    }
}
