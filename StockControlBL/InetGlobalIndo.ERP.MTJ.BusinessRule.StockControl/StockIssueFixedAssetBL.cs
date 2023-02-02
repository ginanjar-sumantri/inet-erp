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
    public sealed class StockIssueFixedAssetBL : Base
    {
        public StockIssueFixedAssetBL()
        {
        }

        #region STCIssueToFAHd

        private StockIssueRequestFABL _stcIssueRequestFABL = new StockIssueRequestFABL();
        private GLBudgetBL _glBudgetBL = new GLBudgetBL();

        public double RowsCountSTCIssueToFAHd(string _prmCategory, string _prmKeyword)
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
                    from _stcIssueToFAHd in this.db.STCIssueToFAHds
                    where (SqlMethods.Like(_stcIssueToFAHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like((_stcIssueToFAHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && _stcIssueToFAHd.Status != StockIssueFixedAssetDataMapper.GetStatus(TransStatus.Deleted)
                    select _stcIssueToFAHd

                ).Count();

            _result = _query;

            return _result;
        }

        public List<STCIssueToFAHd> GetListSTCIssueToFAHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCIssueToFAHd> _result = new List<STCIssueToFAHd>();

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
                                from _stcIssueToFAHd in this.db.STCIssueToFAHds
                                where (SqlMethods.Like(_stcIssueToFAHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_stcIssueToFAHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _stcIssueToFAHd.Status != StockIssueFixedAssetDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _stcIssueToFAHd.CreatedDate descending
                                select new
                                {
                                    TransNmbr = _stcIssueToFAHd.TransNmbr,
                                    FileNmbr = _stcIssueToFAHd.FileNmbr,
                                    TransDate = _stcIssueToFAHd.TransDate,
                                    Status = _stcIssueToFAHd.Status,
                                    WrhsCode = _stcIssueToFAHd.WrhsCode,
                                    WrhsName =
                                                (
                                                    from _msWarehouse in this.db.MsWarehouses
                                                    where _msWarehouse.WrhsCode == _stcIssueToFAHd.WrhsCode
                                                    select _msWarehouse.WrhsName
                                                ).FirstOrDefault(),
                                    ReqAssetNo = _stcIssueToFAHd.ReqAssetNo,
                                    FileNo = this.GetFileNmbrSTCRequestHd(_stcIssueToFAHd.ReqAssetNo)
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCIssueToFAHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.WrhsCode, _row.WrhsName, _row.ReqAssetNo, _row.FileNo));
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

        public STCIssueToFAHd GetSingleSTCIssueToFAHd(string _prmCode)
        {
            STCIssueToFAHd _result = null;

            try
            {
                _result = this.db.STCIssueToFAHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleSTCIssueToFAHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCIssueToFAHd _stcIssueToFAHd = this.db.STCIssueToFAHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcIssueToFAHd != null)
                    {
                        if (_stcIssueToFAHd.Status != StockIssueFixedAssetDataMapper.GetStatus(TransStatus.Posted))
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


        public string GetReqNoByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _stcIssueFA in this.db.STCIssueToFAHds
                                where _stcIssueFA.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    ReqAssetNo = _stcIssueFA.ReqAssetNo
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ReqAssetNo;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFileNmbrByTransNmbr(string _prmTransNmbr)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _stcIssueFA in this.db.STCIssueToFAHds
                                where _stcIssueFA.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()
                                select new
                                {
                                    FileNmbr = _stcIssueFA.FileNmbr
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.FileNmbr;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCIssueToFAHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCIssueToFAHd _stcIssueToFAHd = this.db.STCIssueToFAHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcIssueToFAHd != null)
                    {
                        if ((_stcIssueToFAHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCIssueToFADts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCIssueToFADts.DeleteAllOnSubmit(_query);

                            this.db.STCIssueToFAHds.DeleteOnSubmit(_stcIssueToFAHd);

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

        public bool DeleteMultiApproveSTCIssueToFAHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCIssueToFAHd _stcIssueToFAHd = this.db.STCIssueToFAHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcIssueToFAHd.Status == StockIssueFixedAssetDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _stcIssueToFAHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _stcIssueToFAHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_stcIssueToFAHd != null)
                    {
                        if ((_stcIssueToFAHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.STCIssueToFADts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.STCIssueToFADts.DeleteAllOnSubmit(_query);

                            this.db.STCIssueToFAHds.DeleteOnSubmit(_stcIssueToFAHd);

                            _result = true;
                        }
                        else if (_stcIssueToFAHd.FileNmbr != "" && _stcIssueToFAHd.Status == StockIssueFixedAssetDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _stcIssueToFAHd.Status = StockIssueFixedAssetDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddSTCIssueToFAHd(STCIssueToFAHd _prmSTCIssueToFAHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCIssueToFAHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCIssueToFAHds.InsertOnSubmit(_prmSTCIssueToFAHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCIssueToFAHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCIssueToFAHd(STCIssueToFAHd _prmSTCIssueToFAHd)
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
                int _success = this.db.S_STIssueToFAGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

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


        public Boolean IsBudgetNotExist(String _prmTransNmbr)
        {
            Boolean _result = false;

            String _requestNo = this.GetSingleSTCIssueToFAHd(_prmTransNmbr).ReqAssetNo;
            List<STCIssueToFADt> _stcIssueToFADt = this.GetListSTCIssueToFADt(_prmTransNmbr);
            String _account = "";
            List<Guid> _budgetDetailCode = new List<Guid>();
            DateTime _date = this.GetSingleSTCIssueToFAHd(_prmTransNmbr).TransDate;
            String _orgUnit = _stcIssueRequestFABL.GetSingleSTCRequestHd(_requestNo).OrgUnit;
            string _budgetCode = _glBudgetBL.GetBudgetCodeByOrgUnitAndDate(_orgUnit, _date);

            if (_budgetCode == "")
            {
                _result = true;
            }
            else
            {
                _account = (
                            from _masterDefaultAcc in this.db.Master_DefaultAccs
                            join _msAcc in this.db.MsAccounts
                                on _masterDefaultAcc.Account equals _msAcc.Account
                            join _msCurr in this.db.MsCurrencies
                                on _msAcc.CurrCode equals _msCurr.CurrCode
                            where _masterDefaultAcc.SetCode == "47" && _msCurr.FgHome == YesNoDataMapper.GetYesNo(YesNo.Yes)
                            select _masterDefaultAcc.Account
                        ).FirstOrDefault();

                if (_glBudgetBL.GetBudgetDetailCodeByAccountAndBudgetCode(new Guid(_budgetCode), _account) != new Guid())
                {
                    _budgetDetailCode.Add(_glBudgetBL.GetBudgetDetailCodeByAccountAndBudgetCode(new Guid(_budgetCode), _account));
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

            String _requestNo = this.GetSingleSTCIssueToFAHd(_prmTransNmbr).ReqAssetNo;
            List<STCIssueToFADt> _stcIssueToFADt = this.GetListSTCIssueToFADt(_prmTransNmbr);
            String _account = "";
            List<Guid> _budgetDetailCode = new List<Guid>();
            DateTime _date = this.GetSingleSTCIssueToFAHd(_prmTransNmbr).TransDate;
            String _orgUnit = _stcIssueRequestFABL.GetSingleSTCRequestHd(_requestNo).OrgUnit;
            List<Decimal> _amount = new List<Decimal>();
            Decimal _price = 0;
            string _budgetCode = _glBudgetBL.GetBudgetCodeByOrgUnitAndDate(_orgUnit, _date);

            _account = (
                            from _masterDefaultAcc in this.db.Master_DefaultAccs
                            join _msAcc in this.db.MsAccounts
                                on _masterDefaultAcc.Account equals _msAcc.Account
                            join _msCurr in this.db.MsCurrencies
                                on _msAcc.CurrCode equals _msCurr.CurrCode
                            where _masterDefaultAcc.SetCode == "47" && _msCurr.FgHome == YesNoDataMapper.GetYesNo(YesNo.Yes)
                            select _masterDefaultAcc.Account
                        ).FirstOrDefault();

            foreach (STCIssueToFADt _row in _stcIssueToFADt)
            {
                _price = (
                                from _msProduct in this.db.MsProducts
                                where _row.ProductCode == _msProduct.ProductCode
                                select _msProduct.BuyingPrice
                          ).First();

                _amount.Add(_row.Qty * _price);
            }

            _budgetDetailCode.Add(_glBudgetBL.GetBudgetDetailCodeByAccountAndBudgetCode(new Guid(_budgetCode), _account));

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
                    int _success = this.db.S_STIssueToFAApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        STCIssueToFAHd _stcIssueToFAHd = this.GetSingleSTCIssueToFAHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcIssueToFAHd.TransDate.Year, _stcIssueToFAHd.TransDate.Month, AppModule.GetValue(TransactionType.StockIssueToFA), this._companyTag, ""))
                        {
                            _stcIssueToFAHd.FileNmbr = item.Number;
                        }
                        
                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingRetur);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCIssueToFAHd(_prmTransNmbr).FileNmbr;
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

                STCIssueToFAHd _stcIssueToFAHd = this.db.STCIssueToFAHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcIssueToFAHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STIssueToFAPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingRetur);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSTCIssueToFAHd(_prmTransNmbr).FileNmbr;
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

                STCIssueToFAHd _stcIssueToFAHd = this.db.STCIssueToFAHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_stcIssueToFAHd.TransDate);
                if (_locked == "")
                {
                    int _success = this.db.S_STIssueToFAUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.StockReceivingRetur);
                        //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                        //_transActivity.FileNmbr = this.GetSingleSTCIssueToFAHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleSTCIssueToFAHd(_prmTransNmbr).TransDate;
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

        public List<STCIssueToFAHd> GetListDDLSTCIssueToFAHd()
        {
            List<STCIssueToFAHd> _result = new List<STCIssueToFAHd>();

            try
            {
                var _query = (
                                from _stcIssueToFAHd in this.db.STCIssueToFAHds
                                where !(
                                            from _glFAAddStockHd in this.db.GLFAAddStockHds
                                            select _glFAAddStockHd.WISNo
                                        ).Contains(_stcIssueToFAHd.TransNmbr)
                                        && _stcIssueToFAHd.Status == StockIssueFixedAssetDataMapper.GetStatus(TransStatus.Posted)
                                        && ((_stcIssueToFAHd.FileNmbr ?? "") == _stcIssueToFAHd.FileNmbr)
                                orderby _stcIssueToFAHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _stcIssueToFAHd.TransNmbr,
                                    FileNmbr = _stcIssueToFAHd.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCIssueToFAHd(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region STCIssueToFADt
        public int RowsCountSTCIssueToFADt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _STCIssueToFADt in this.db.STCIssueToFADts
                                 where _STCIssueToFADt.TransNmbr == _prmCode
                                 select _STCIssueToFADt.TransNmbr
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCIssueToFADt> GetListSTCIssueToFADt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<STCIssueToFADt> _result = new List<STCIssueToFADt>();

            try
            {
                var _query = (
                                from _stcIssueToFADt in this.db.STCIssueToFADts
                                where _stcIssueToFADt.TransNmbr == _prmCode
                                orderby _stcIssueToFADt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcIssueToFADt.TransNmbr,
                                    ProductCode = _stcIssueToFADt.ProductCode,
                                    LocationCode = _stcIssueToFADt.LocationCode,
                                    Qty = _stcIssueToFADt.Qty,
                                    Unit = _stcIssueToFADt.Unit,
                                    Remark = _stcIssueToFADt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCIssueToFADt(_row.TransNmbr, _row.ProductCode, _row.LocationCode, _row.Qty, _row.Unit, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCIssueToFADt> GetListSTCIssueToFADt(string _prmCode)
        {
            List<STCIssueToFADt> _result = new List<STCIssueToFADt>();

            try
            {
                var _query = (
                                from _stcIssueToFADt in this.db.STCIssueToFADts
                                where _stcIssueToFADt.TransNmbr == _prmCode
                                orderby _stcIssueToFADt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _stcIssueToFADt.TransNmbr,
                                    ProductCode = _stcIssueToFADt.ProductCode,
                                    LocationCode = _stcIssueToFADt.LocationCode,
                                    Qty = _stcIssueToFADt.Qty,
                                    Unit = _stcIssueToFADt.Unit,
                                    Remark = _stcIssueToFADt.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new STCIssueToFADt(_row.TransNmbr, _row.ProductCode, _row.LocationCode, _row.Qty, _row.Unit, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCIssueToFADt GetSingleSTCIssueToFADt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        {
            STCIssueToFADt _result = null;

            try
            {
                _result = this.db.STCIssueToFADts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode && _temp.LocationCode == _prmLocationCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCIssueToFADt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('|');

                    STCIssueToFADt _STCIssueToFADt = this.db.STCIssueToFADts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr == _prmTransNo.Trim().ToLower());

                    this.db.STCIssueToFADts.DeleteOnSubmit(_STCIssueToFADt);
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

        public bool AddSTCIssueToFADt(STCIssueToFADt _prmSTCIssueToFADt)
        {
            bool _result = false;

            try
            {
                this.db.STCIssueToFADts.InsertOnSubmit(_prmSTCIssueToFADt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCIssueToFADt(STCIssueToFADt _prmSTCIssueToFADt)
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

        public List<STCIssueToFADt> GetListFAAddStockDtFromSTCIssueToFADt(string _prmWISNo)
        {
            List<STCIssueToFADt> _result = new List<STCIssueToFADt>();

            try
            {
                var _query = (
                                from _stcIssueToFADt in this.db.STCIssueToFADts
                                where _stcIssueToFADt.TransNmbr == _prmWISNo
                                orderby _stcIssueToFADt.TransNmbr ascending
                                select new
                                {
                                    TransNmbr = _stcIssueToFADt.TransNmbr,
                                    ProductCode = _stcIssueToFADt.ProductCode,
                                    LocationCode = _stcIssueToFADt.LocationCode,
                                    Qty = _stcIssueToFADt.Qty
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new STCIssueToFADt(_row.TransNmbr, _row.ProductCode, _row.LocationCode, _row.Qty));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~StockIssueFixedAssetBL()
        {
        }
    }
}
