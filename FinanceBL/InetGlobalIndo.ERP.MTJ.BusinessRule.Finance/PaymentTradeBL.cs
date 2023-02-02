using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class PaymentTradeBL : Base
    {
        public PaymentTradeBL()
        {
        }

        #region PaymentTrade
        public double RowsCountPaymentTrade(string _prmCategory, string _prmKeyword)
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
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            var _query =
                        (
                            from _fINPayTradeHd in this.db.FINPayTradeHds
                            join _msSupp in this.db.MsSuppliers
                                on _fINPayTradeHd.SuppCode equals _msSupp.SuppCode
                            where (SqlMethods.Like(_fINPayTradeHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_fINPayTradeHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _fINPayTradeHd.Status != PaymentTradeDataMapper.GetStatus(TransStatus.Deleted)
                            select _fINPayTradeHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINPayTradeHd> GetListFINPayTradeHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINPayTradeHd> _result = new List<FINPayTradeHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _fINPayTradeHd in this.db.FINPayTradeHds
                                join _msSupp in this.db.MsSuppliers
                                    on _fINPayTradeHd.SuppCode equals _msSupp.SuppCode
                                where (SqlMethods.Like(_fINPayTradeHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_fINPayTradeHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _fINPayTradeHd.Status != PaymentTradeDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _fINPayTradeHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _fINPayTradeHd.TransNmbr,
                                    FileNmbr = _fINPayTradeHd.FileNmbr,
                                    TransDate = _fINPayTradeHd.TransDate,
                                    Status = _fINPayTradeHd.Status,
                                    SuppCode = _fINPayTradeHd.SuppCode,
                                    SuppName = _msSupp.SuppName,
                                    Remark = _fINPayTradeHd.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINPayTradeHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.SuppCode, _row.SuppName, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPayTradeHd GetSingleFINPayTradeHd(string _prmCode)
        {
            FINPayTradeHd _result = null;

            try
            {
                _result = this.db.FINPayTradeHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPayTradeHd GetSingleFINPayTradeHdView(string _prmCode)
        {
            FINPayTradeHd _result = new FINPayTradeHd();

            try
            {
                var _query = (
                               from _fINPayTradeHd in this.db.FINPayTradeHds
                               join _msSupp in this.db.MsSuppliers
                                   on _fINPayTradeHd.SuppCode equals _msSupp.SuppCode
                               orderby _fINPayTradeHd.DatePrep descending
                               where _fINPayTradeHd.TransNmbr == _prmCode
                               select new
                               {
                                   TransNmbr = _fINPayTradeHd.TransNmbr,
                                   FileNmbr = _fINPayTradeHd.FileNmbr,
                                   TransDate = _fINPayTradeHd.TransDate,
                                   Status = _fINPayTradeHd.Status,
                                   SuppCode = _fINPayTradeHd.SuppCode,
                                   SuppName = _msSupp.SuppName,
                                   //CurrCode = _fINPayTradeHd.CurrCode,
                                   Remark = _fINPayTradeHd.Remark
                               }
                           ).Single();

                _result.TransNmbr = _query.TransNmbr;
                _result.FileNmbr = _query.FileNmbr;
                _result.TransDate = _query.TransDate;
                _result.Status = _query.Status;
                _result.SuppCode = _query.SuppCode;
                _result.SuppName = _query.SuppName;
                //_result.CurrCode = _query.CurrCode;
                _result.Remark = _query.Remark;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINPayTradeHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINPayTradeHd _fINPayTradeHd = this.db.FINPayTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_fINPayTradeHd != null)
                    {
                        if ((_fINPayTradeHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINPayTradeDbs
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINPayTradeDbs.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINPayTradeCrs
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINPayTradeCrs.DeleteAllOnSubmit(_query2);

                            this.db.FINPayTradeHds.DeleteOnSubmit(_fINPayTradeHd);

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

        public string AddFINPayTradeHd(FINPayTradeHd _prmFINPayTradeHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINPayTradeHd.TransDate.Year, _prmFINPayTradeHd.TransDate.Month, AppModule.GetValue(TransactionType.PaymentTrade), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINPayTradeHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINPayTradeHds.InsertOnSubmit(_prmFINPayTradeHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINPayTradeHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINPayTradeHd(FINPayTradeHd _prmFINPayTradeHd)
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

            try
            {
                //TEMPORARY SOLUTION
                //BY : DEWI
                //DATE : 3/2/2009
                //TUJUAN : UNTUK MEMENUHI REQUEST INET (PRINT)

                //using (TransactionScope _scope = new TransactionScope())
                //{
                this.db.S_FNPayTradeGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    //FINPayTradeHd _fINPayTradeHd = this.GetSingleFINPayTradeHd(_prmTransNmbr);
                    //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_fINPayTradeHd.TransDate.Year, _fINPayTradeHd.TransDate.Month, AppModule.GetValue(TransactionType.PaymentTrade), this._companyTag, ""))
                    //{
                    //    _fINPayTradeHd.FileNmbr = item.Number;
                    //}

                    //this.db.SubmitChanges();

                    //_scope.Complete();

                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.PaymentTrade);
                    _transActivity.TransNmbr = _prmTransNmbr;
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
                //}
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                //TEMPORARY SOLUTION
                //BY : DEWI
                //DATE : 3/2/2009
                //TUJUAN : UNTUK MEMENUHI REQUEST INET (PRINT)

                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_FNPayTradeApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINPayTradeHd _fINPayTradeHd = this.GetSingleFINPayTradeHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_fINPayTradeHd.TransDate.Year, _fINPayTradeHd.TransDate.Month, AppModule.GetValue(TransactionType.PaymentTrade), this._companyTag, ""))
                        {
                            _fINPayTradeHd.FileNmbr = item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.PaymentTrade);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINPayTradeHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

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

        public string Posting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINPayTradeHd _fINPayTradeHd = this.db.FINPayTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_fINPayTradeHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNPayTradePost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.PaymentTrade);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINPayTradeHd(_prmTransNmbr).FileNmbr;
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

        public string Unposting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINPayTradeHd _fINPayTradeHd = this.db.FINPayTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_fINPayTradeHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNPayTradeUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid()();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.PaymentTrade);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINPayTradeHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINPayTradeHd(_prmTransNmbr).TransDate;
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

        public bool GetSingleFINPayTradeHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINPayTradeHd _fINPayTradeHd = this.db.FINPayTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_fINPayTradeHd != null)
                    {
                        if (_fINPayTradeHd.Status != PaymentTradeDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINPayTradeHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINPayTradeHd _fINPayTradeHd = this.db.FINPayTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_fINPayTradeHd.Status == PaymentTradeDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _fINPayTradeHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _fINPayTradeHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_fINPayTradeHd != null)
                    {
                        if ((_fINPayTradeHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINPayTradeDbs
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINPayTradeDbs.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINPayTradeCrs
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINPayTradeCrs.DeleteAllOnSubmit(_query2);

                            this.db.FINPayTradeHds.DeleteOnSubmit(_fINPayTradeHd);

                            _result = true;
                        }
                        else if (_fINPayTradeHd.FileNmbr != "" && _fINPayTradeHd.Status == PaymentTradeDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _fINPayTradeHd.Status = PaymentTradeDataMapper.GetStatus(TransStatus.Deleted);
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

        #region Credit
        public List<FINPayTradeCr> GetListFINPayTradeCr(string _prmCode)
        {
            List<FINPayTradeCr> _result = new List<FINPayTradeCr>();

            try
            {
                var _query = (
                                from _finPayTradeCr in this.db.FINPayTradeCrs
                                join _msPayType in this.db.MsPayTypes
                                    on _finPayTradeCr.PayType equals _msPayType.PayCode
                                where _finPayTradeCr.TransNmbr == _prmCode
                                orderby _finPayTradeCr.ItemNo descending
                                select new
                                {
                                    ItemNo = _finPayTradeCr.ItemNo,
                                    PayType = _finPayTradeCr.PayType,
                                    PayName = _msPayType.PayName,
                                    DocumentNo = _finPayTradeCr.DocumentNo,
                                    AmountHome = _finPayTradeCr.AmountHome,
                                    CurrCode = _finPayTradeCr.CurrCode,
                                    BankExpense = _finPayTradeCr.BankExpense,
                                    Remark = _finPayTradeCr.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINPayTradeCr(_row.ItemNo, _row.PayType, _row.PayName, _row.DocumentNo, _row.AmountHome, _row.CurrCode, _row.BankExpense, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPayTradeCr GetViewFINPayTradeCr(string _prmCode, string _prmItemNo)
        {
            FINPayTradeCr _result = new FINPayTradeCr();

            try
            {
                var _query = (
                                from _finPayTradeCr in this.db.FINPayTradeCrs
                                join _msAccount in this.db.MsAccounts
                                    on _finPayTradeCr.AccBank equals _msAccount.Account
                                where _finPayTradeCr.TransNmbr == _prmCode && _finPayTradeCr.ItemNo == Convert.ToInt32(_prmItemNo)
                                orderby _finPayTradeCr.ItemNo descending
                                select new
                                {
                                    ItemNo = _finPayTradeCr.ItemNo,
                                    Account = _finPayTradeCr.AccBank,
                                    AccountName = _msAccount.AccountName,
                                    AmountForex = _finPayTradeCr.AmountForex,
                                    Remark = _finPayTradeCr.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.ItemNo = _row.ItemNo;
                    _result.AccBank = _row.Account;
                    _result.AccountName = _row.AccountName;
                    _result.AmountForex = _row.AmountForex;
                    _result.Remark = _row.Remark;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPayTradeCr GetSingleFINPayTradeCr(string _prmCode, string _prmItemNo)
        {
            FINPayTradeCr _result = null;

            try
            {
                _result = this.db.FINPayTradeCrs.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == Convert.ToInt32(_prmItemNo));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINPayTradeCr(string[] _prmItemNo, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmItemNo.Length; i++)
                {
                    FINPayTradeCr _finPayTradeCr = this.db.FINPayTradeCrs.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmItemNo[i]) && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.FINPayTradeCrs.DeleteOnSubmit(_finPayTradeCr);
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

        public bool AddFINPayTradeCr(FINPayTradeCr _prmFINPayTradeCr)
        {
            bool _result = false;

            try
            {
                this.db.FINPayTradeCrs.InsertOnSubmit(_prmFINPayTradeCr);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINPayTradeCr(FINPayTradeCr _prmFINPayTradeCr)
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

        public int GetMaxNoItemFINPayTradeCr(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINPayTradeCrs.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region Debit
        public List<FINPayTradeDb> GetListFINPayTradeDb(string _prmCode)
        {
            List<FINPayTradeDb> _result = new List<FINPayTradeDb>();

            try
            {
                var _query = (
                                from _finPayTradeDb in this.db.FINPayTradeDbs
                                where _finPayTradeDb.TransNmbr == _prmCode
                                orderby _finPayTradeDb.InvoiceNo descending
                                select new
                                {
                                    InvoiceNo = _finPayTradeDb.InvoiceNo,
                                    FileNmbr = this.GetFileNmbrFINAPPostingByInvoiceNo(_finPayTradeDb.InvoiceNo),
                                    SuppInvoice = _finPayTradeDb.SuppInvoice,
                                    SuppName = (
                                                    from _msSupplier in this.db.MsSuppliers
                                                    where _msSupplier.SuppCode == _finPayTradeDb.SuppInvoice
                                                    select _msSupplier.SuppName
                                                ).FirstOrDefault(),
                                    CurrCode = _finPayTradeDb.CurrCode,
                                    APPaid = _finPayTradeDb.APPaid,
                                    PPNPaid = _finPayTradeDb.PPNPaid,
                                    AmountForex = _finPayTradeDb.AmountForex,
                                    AmountHome = _finPayTradeDb.AmountHome,
                                    FgValue = _finPayTradeDb.FgValue,
                                    Remark = _finPayTradeDb.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINPayTradeDb(_row.InvoiceNo, _row.FileNmbr, _row.SuppInvoice, _row.SuppName, _row.CurrCode, _row.APPaid, _row.PPNPaid, _row.AmountForex, _row.AmountHome, _row.FgValue, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINPayTradeDb> GetListInvoiceNoForDDL(string _prmCode)
        {
            List<FINPayTradeDb> _result = new List<FINPayTradeDb>();

            try
            {
                var _query = (
                                from _v_FNAPPosting in this.db.V_FNAPPostings
                                where _v_FNAPPosting.Supp_Code == _prmCode
                                && _v_FNAPPosting.Amount_Saldo > 0
                                && (_v_FNAPPosting.FileNmbr ?? "").Trim() == _v_FNAPPosting.FileNmbr.Trim()
                                orderby _v_FNAPPosting.Invoice_No descending
                                select new
                                {
                                    InvoiceNo = _v_FNAPPosting.Invoice_No,
                                    FileNmbr = _v_FNAPPosting.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINPayTradeDb(_row.InvoiceNo, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPayTradeDb GetSingleFINPayTradeDb(string _prmCode, string _prmInvoiceNo)
        {
            FINPayTradeDb _result = null;

            try
            {
                _result = this.db.FINPayTradeDbs.Single(_temp => _temp.TransNmbr == _prmCode && _temp.InvoiceNo == _prmInvoiceNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public V_FNAPPosting GetSingleV_FNAPPosting(string _prmInvoiceNo)
        {
            V_FNAPPosting _result = null;

            try
            {
                _result = this.db.V_FNAPPostings.Single(_temp => _temp.Invoice_No == _prmInvoiceNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFileNmbrFINAPPostingByInvoiceNo(string _prmInvoiceNo)
        {
            string _result = null;

            try
            {
                _result = (this.db.FINAPPostings.Single(_temp => _temp.InvoiceNo == _prmInvoiceNo).FileNmbr ?? "").Trim();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINAPPosting GetSingleFINAPPosting(string _prmInvoiceNo)
        {
            FINAPPosting _result = null;

            try
            {
                _result = this.db.FINAPPostings.Single(_temp => _temp.InvoiceNo == _prmInvoiceNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsSuppCodeExists(string _prmSuppCode)
        {
            bool _result = false;

            try
            {
                var _query = from _finAPPosting in this.db.FINAPPostings
                             where (_finAPPosting.SuppCode == _prmSuppCode)
                             select new
                             {
                                 _finAPPosting.SuppCode
                             };

                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINPayTradeDb(FINPayTradeDb _prmFINPayTradeDb)
        {
            bool _result = false;

            try
            {
                this.db.FINPayTradeDbs.InsertOnSubmit(_prmFINPayTradeDb);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINPayTradeDb(FINPayTradeDb _prmFINPayTradeDb)
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

        public bool DeleteMultiFINPayTradeDb(string[] _prmInvoiceNo, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmInvoiceNo.Length; i++)
                {
                    FINPayTradeDb _finPayTradeDb = this.db.FINPayTradeDbs.Single(_temp => _temp.InvoiceNo == _prmInvoiceNo[i] && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.FINPayTradeDbs.DeleteOnSubmit(_finPayTradeDb);
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

        public string GetSuppCodeHeader(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finFINPayTradeHd in this.db.FINPayTradeHds
                                where _finFINPayTradeHd.TransNmbr == _prmCode
                                select new
                                {
                                    SuppCode = _finFINPayTradeHd.SuppCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.SuppCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        public List<FINAPPosting> GetListInvoiceNoForDDLAPRate(string _prmCurrCode, decimal _prmNewForexRate)
        {
            List<FINAPPosting> _result = new List<FINAPPosting>();

            try
            {
                var _query = (
                                from _finAPPosting in this.db.FINAPPostings
                                where (_finAPPosting.CurrCode.Trim().ToLower() == _prmCurrCode.Trim().ToLower())
                                    && (_finAPPosting.ForexRate != _prmNewForexRate)
                                    && (_finAPPosting.Amount > _finAPPosting.Balance)
                                    && (_finAPPosting.FileNmbr ?? "").Trim() == _finAPPosting.FileNmbr.Trim()
                                orderby _finAPPosting.InvoiceNo ascending
                                select new
                                {
                                    InvoiceNo = _finAPPosting.InvoiceNo,
                                    FileNmbr = _finAPPosting.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINAPPosting(_row.InvoiceNo, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        ~PaymentTradeBL()
        {
        }
    }
}
