using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class PaymentNonTradeBL : Base
    {
        public PaymentNonTradeBL()
        {

        }

        public double RowsCountPaymentNonTrade(string _prmCategory, string _prmKeyword)
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
            else if (_prmCategory == "CustName")
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
                            from _finReceiptNonTradeHd in this.db.FINReceiptNonTradeHds
                            join _msCust in this.db.MsCustomers
                                on _finReceiptNonTradeHd.CustCode equals _msCust.CustCode
                            where (SqlMethods.Like(_finReceiptNonTradeHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_finReceiptNonTradeHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _finReceiptNonTradeHd.Status != PaymentNonTradeDataMapper.GetStatus(TransStatus.Deleted)
                            select _finReceiptNonTradeHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINReceiptNonTradeHd> GetListFINReceiptNonTradeHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINReceiptNonTradeHd> _result = new List<FINReceiptNonTradeHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "CustName")
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
                                from _finReceiptNonTradeHd in this.db.FINReceiptNonTradeHds
                                join _msCust in this.db.MsCustomers
                                    on _finReceiptNonTradeHd.CustCode equals _msCust.CustCode
                                where (SqlMethods.Like(_finReceiptNonTradeHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finReceiptNonTradeHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finReceiptNonTradeHd.Status != PaymentNonTradeDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _finReceiptNonTradeHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finReceiptNonTradeHd.TransNmbr,
                                    FileNmbr = _finReceiptNonTradeHd.FileNmbr,
                                    TransDate = _finReceiptNonTradeHd.TransDate,
                                    Status = _finReceiptNonTradeHd.Status,
                                    CustCode = _finReceiptNonTradeHd.CustCode,
                                    CustName = _msCust.CustName,
                                    CurrCode = _finReceiptNonTradeHd.CurrCode
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINReceiptNonTradeHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.CustCode, _row.CustName, _row.CurrCode));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINReceiptNonTradeHd GetSingleFINReceiptNonTradeHd(string _prmCode)
        {
            FINReceiptNonTradeHd _result = null;

            try
            {
                _result = this.db.FINReceiptNonTradeHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINReceiptNonTradeHd GetSingleFINReceiptNonTradeHdView(string _prmCode)
        {
            FINReceiptNonTradeHd _result = new FINReceiptNonTradeHd();

            try
            {
                var _query = (
                               from _finReceiptNonTradeHd in this.db.FINReceiptNonTradeHds
                               join _msCust in this.db.MsCustomers
                                   on _finReceiptNonTradeHd.CustCode equals _msCust.CustCode
                               orderby _finReceiptNonTradeHd.DatePrep descending
                               where _finReceiptNonTradeHd.TransNmbr == _prmCode
                               select new
                               {
                                   TransNmbr = _finReceiptNonTradeHd.TransNmbr,
                                   FileNmbr = _finReceiptNonTradeHd.FileNmbr,
                                   TransDate = _finReceiptNonTradeHd.TransDate,
                                   Status = _finReceiptNonTradeHd.Status,
                                   CustCode = _finReceiptNonTradeHd.CustCode,
                                   CustName = _msCust.CustName,
                                   CurrCode = _finReceiptNonTradeHd.CurrCode,
                                   ForexRate = _finReceiptNonTradeHd.ForexRate,
                                   Remark = _finReceiptNonTradeHd.Remark
                               }
                           ).Single();

                _result.TransNmbr = _query.TransNmbr;
                _result.FileNmbr = _query.FileNmbr;
                _result.TransDate = _query.TransDate;
                _result.Status = _query.Status;
                _result.CustCode = _query.CurrCode;
                _result.CustName = _query.CustName;
                _result.CurrCode = _query.CurrCode;
                _result.ForexRate = _query.ForexRate;
                _result.Remark = _query.Remark;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINReceiptNonTradeHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINReceiptNonTradeHd _finReceiptNonTradeHd = this.db.FINReceiptNonTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finReceiptNonTradeHd != null)
                    {
                        if ((_finReceiptNonTradeHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINReceiptNonTradeDbs
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINReceiptNonTradeDbs.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINReceiptNonTradeCrs
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINReceiptNonTradeCrs.DeleteAllOnSubmit(_query2);

                            this.db.FINReceiptNonTradeHds.DeleteOnSubmit(_finReceiptNonTradeHd);

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

        public string AddFINReceiptNonTradeHd(FINReceiptNonTradeHd _prmFINReceiptNonTradeHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINReceiptNonTradeHd.TransDate.Year, _prmFINReceiptNonTradeHd.TransDate.Month, AppModule.GetValue(TransactionType.ReceiptNonTrade), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINReceiptNonTradeHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINReceiptNonTradeHds.InsertOnSubmit(_prmFINReceiptNonTradeHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINReceiptNonTradeHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINReceiptNonTradeHd(FINReceiptNonTradeHd _prmFINReceiptNonTradeHd)
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
                //TEMPORARY SOLUTION
                //BY : DEWI
                //DATE : 3/2/2009
                //TUJUAN : UNTUK MEMENUHI REQUEST INET (PRINT)

                //using (TransactionScope _scope = new TransactionScope())
                //{
                int _success = this.db.S_FNReceiptNonTradeGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    FINReceiptNonTradeHd _finReceiptNonTradeHd = this.GetSingleFINReceiptNonTradeHd(_prmTransNmbr);
                    foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finReceiptNonTradeHd.TransDate.Year, _finReceiptNonTradeHd.TransDate.Month, AppModule.GetValue(TransactionType.ReceiptNonTrade), this._companyTag, ""))
                    {
                        _finReceiptNonTradeHd.FileNmbr = item.Number;
                    }
                    //_scope.Complete();

                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ReceiptNonTrade);
                    _transActivity.TransNmbr = _prmTransNmbr;
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
                //}
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
                //TEMPORARY SOLUTION
                //BY : DEWI
                //DATE : 3/2/2009
                //TUJUAN : UNTUK MEMENUHI REQUEST INET (PRINT)

                //using (TransactionScope _scope = new TransactionScope())
                //{
                int _success = this.db.S_FNReceiptNonTradeApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    //    FINReceiptNonTradeHd _finReceiptNonTradeHd = this.GetSingleFINReceiptNonTradeHd(_prmTransNmbr);
                    //    foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finReceiptNonTradeHd.TransDate.Year, _finReceiptNonTradeHd.TransDate.Month, AppModule.GetValue(TransactionType.ReceiptNonTrade), this._companyTag, ""))
                    //    {
                    //        _finReceiptNonTradeHd.FileNmbr = item.Number;
                    //    }

                    //    this.db.SubmitChanges();

                    //    _scope.Complete();

                    _result = "Approve Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ReceiptNonTrade);
                    _transActivity.TransNmbr = _prmTransNmbr;
                    _transActivity.FileNmbr = GetSingleFINReceiptNonTradeHd(_prmTransNmbr).FileNmbr;
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
                else
                {
                    _result = _errorMsg;
                }
                //}
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
                FINReceiptNonTradeHd _finReceiptNonTradeHd = this.db.FINReceiptNonTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finReceiptNonTradeHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNReceiptNonTradePost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.ReceiptNonTrade);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINReceiptNonTradeHd(_prmTransNmbr).FileNmbr;
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
                FINReceiptNonTradeHd _finReceiptNonTradeHd = this.db.FINReceiptNonTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finReceiptNonTradeHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNReceiptNonTradeUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.ReceiptNonTrade);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINReceiptNonTradeHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINReceiptNonTradeHd(_prmTransNmbr).TransDate;
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

        //Credit
        public List<FINReceiptNonTradeCr> GetListFINReceiptNonTradeCr(string _prmCode)
        {
            List<FINReceiptNonTradeCr> _result = new List<FINReceiptNonTradeCr>();

            try
            {
                var _query = (
                                from _finReceiptNonTradeCr in this.db.FINReceiptNonTradeCrs
                                join _msAccount in this.db.MsAccounts
                                    on _finReceiptNonTradeCr.Account equals _msAccount.Account
                                where _finReceiptNonTradeCr.TransNmbr == _prmCode
                                orderby _finReceiptNonTradeCr.ItemNo descending
                                select new
                                {
                                    ItemNo = _finReceiptNonTradeCr.ItemNo,
                                    Account = _finReceiptNonTradeCr.Account,
                                    AccountName = _msAccount.AccountName,
                                    FgSubLed = _msAccount.FgSubLed,
                                    SubLed = _finReceiptNonTradeCr.SubLed,
                                    SubledName = (
                                                   from _vMsSubled in this.db.V_MsSubleds
                                                   where _vMsSubled.SubLed_No == _finReceiptNonTradeCr.SubLed
                                                   select _vMsSubled.SubLed_Name
                                                 ).FirstOrDefault(),
                                    AmountForex = _finReceiptNonTradeCr.AmountForex,
                                    Remark = _finReceiptNonTradeCr.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINReceiptNonTradeCr(_row.ItemNo, _row.Account, _row.AccountName, _row.FgSubLed, _row.SubLed, _row.SubledName, _row.AmountForex, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINReceiptNonTradeCr GetViewFINReceiptNonTradeCr(string _prmCode, string _prmItemNo)
        {
            FINReceiptNonTradeCr _result = new FINReceiptNonTradeCr();

            try
            {
                var _query = (
                                from _finReceiptNonTradeCr in this.db.FINReceiptNonTradeCrs
                                join _msAccount in this.db.MsAccounts
                                    on _finReceiptNonTradeCr.Account equals _msAccount.Account
                                where _finReceiptNonTradeCr.TransNmbr == _prmCode && _finReceiptNonTradeCr.ItemNo == Convert.ToInt32(_prmItemNo)
                                orderby _finReceiptNonTradeCr.ItemNo descending
                                select new
                                {
                                    ItemNo = _finReceiptNonTradeCr.ItemNo,
                                    Account = _finReceiptNonTradeCr.Account,
                                    AccountName = _msAccount.AccountName,
                                    SubLed = _finReceiptNonTradeCr.SubLed,
                                    SubledName = (
                                                   from _vMsSubled in this.db.V_MsSubleds
                                                   where _vMsSubled.SubLed_No == _finReceiptNonTradeCr.SubLed
                                                   select _vMsSubled.SubLed_Name
                                                 ).FirstOrDefault(),
                                    AmountForex = _finReceiptNonTradeCr.AmountForex,
                                    Remark = _finReceiptNonTradeCr.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.ItemNo = _row.ItemNo;
                    _result.Account = _row.Account;
                    _result.AccountName = _row.AccountName;
                    _result.SubLed = _row.SubLed;
                    _result.SubledName = _row.SubledName;
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

        public FINReceiptNonTradeCr GetSingleFINReceiptNonTradeCr(string _prmCode, string _prmItemNo)
        {
            FINReceiptNonTradeCr _result = null;

            try
            {
                _result = this.db.FINReceiptNonTradeCrs.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == Convert.ToInt32(_prmItemNo));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINReceiptNonTradeCr(string[] _prmItemNo, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmItemNo.Length; i++)
                {
                    FINReceiptNonTradeCr _finReceiptNonTradeCr = this.db.FINReceiptNonTradeCrs.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmItemNo[i]) && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.FINReceiptNonTradeCrs.DeleteOnSubmit(_finReceiptNonTradeCr);
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

        public bool AddFINReceiptNonTradeCr(FINReceiptNonTradeCr _prmFINReceiptNonTradeCr)
        {
            bool _result = false;

            try
            {
                this.db.FINReceiptNonTradeCrs.InsertOnSubmit(_prmFINReceiptNonTradeCr);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINReceiptNonTradeCr(FINReceiptNonTradeCr _prmFINReceiptNonTradeCr)
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

        public int GetMaxNoItemFINReceiptNonTradeCr(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINReceiptNonTradeCrs.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCurrCodeHeader(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finFINReceiptNonTradeHd in this.db.FINReceiptNonTradeHds
                                where _finFINReceiptNonTradeHd.TransNmbr == _prmCode
                                select new
                                {
                                    CurrCode = _finFINReceiptNonTradeHd.CurrCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.CurrCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //Debit
        public List<FINReceiptNonTradeDb> GetListFINReceiptNonTradeDb(string _prmCode)
        {
            List<FINReceiptNonTradeDb> _result = new List<FINReceiptNonTradeDb>();

            try
            {
                var _query = (
                                from _finReceiptNonTradeDb in this.db.FINReceiptNonTradeDbs
                                where _finReceiptNonTradeDb.TransNmbr == _prmCode
                                orderby _finReceiptNonTradeDb.ItemNo descending
                                select new
                                {
                                    ItemNo = _finReceiptNonTradeDb.ItemNo,
                                    ReceiptType = _finReceiptNonTradeDb.ReceiptType,
                                    ReceiptName = (
                                        from _vMsPayType in this.db.V_MsPayTypes
                                        where _finReceiptNonTradeDb.ReceiptType == _vMsPayType.Payment_Code
                                        select _vMsPayType.Payment_Name
                                    ).FirstOrDefault(),
                                    DocumentNo = _finReceiptNonTradeDb.DocumentNo,
                                    AmountForex = _finReceiptNonTradeDb.AmountForex,
                                    Remark = _finReceiptNonTradeDb.Remark,
                                    DueDate = _finReceiptNonTradeDb.DueDate,
                                    BankGiro = _finReceiptNonTradeDb.BankGiro,
                                    BankGiroName =
                                    (
                                        from _msBank in this.db.MsBanks
                                        where _finReceiptNonTradeDb.BankGiro == _msBank.BankCode
                                        select _msBank.BankName
                                    ).FirstOrDefault(),
                                    BankExpense = _finReceiptNonTradeDb.BankExpense
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINReceiptNonTradeDb(_row.ItemNo, _row.ReceiptType, _row.ReceiptName, _row.DocumentNo, _row.AmountForex, _row.Remark, _row.DueDate, _row.BankGiro, _row.BankGiroName, _row.BankExpense));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINReceiptNonTradeDb GetSingleFINReceiptNonTradeDb(string _prmCode, string _prmItemNo)
        {
            FINReceiptNonTradeDb _result = null;

            try
            {
                _result = this.db.FINReceiptNonTradeDbs.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == Convert.ToInt32(_prmItemNo));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINReceiptNonTradeDb GetViewFINReceiptNonTradeDb(string _prmCode, string _prmItemNo)
        {
            FINReceiptNonTradeDb _result = new FINReceiptNonTradeDb();

            try
            {
                var _query = (
                                from _finReceiptNonTradeDb in this.db.FINReceiptNonTradeDbs
                                where _finReceiptNonTradeDb.TransNmbr == _prmCode && _finReceiptNonTradeDb.ItemNo == Convert.ToInt32(_prmItemNo)
                                select new
                                {
                                    ReceiptName = (
                                        from _vMsPayType in this.db.V_MsPayTypes
                                        where _finReceiptNonTradeDb.ReceiptType == _vMsPayType.Payment_Code
                                        select _vMsPayType.Payment_Name
                                    ).FirstOrDefault(),
                                    DocumentNo = _finReceiptNonTradeDb.DocumentNo,
                                    AmountForex = _finReceiptNonTradeDb.AmountForex,
                                    BankGiroName =
                                    (
                                        from _msBank in this.db.MsBanks
                                        where _finReceiptNonTradeDb.BankGiro == _msBank.BankCode
                                        select _msBank.BankName
                                    ).FirstOrDefault(),
                                    DueDate = _finReceiptNonTradeDb.DueDate,
                                    BankExpense = _finReceiptNonTradeDb.BankExpense,
                                    Remark = _finReceiptNonTradeDb.Remark
                                }
                            );
                foreach (var _row in _query)
                {
                    _result.ReceiptName = _row.ReceiptName;
                    _result.DocumentNo = _row.DocumentNo;
                    _result.AmountForex = _row.AmountForex;
                    _result.BankGiroName = _row.BankGiroName;
                    _result.DueDate = _row.DueDate;
                    _result.BankExpense = _row.BankExpense;
                    _result.Remark = _row.Remark;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINReceiptNonTradeDb(FINReceiptNonTradeDb _prmFINReceiptNonTradeDb)
        {
            bool _result = false;

            try
            {
                this.db.FINReceiptNonTradeDbs.InsertOnSubmit(_prmFINReceiptNonTradeDb);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINReceiptNonTradeDb(FINReceiptNonTradeDb _prmFINReceiptNonTradeDb)
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

        public bool DeleteMultiFINReceiptNonTradeDb(string[] _prmItemNo, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmItemNo.Length; i++)
                {
                    FINReceiptNonTradeDb _finReceiptNonTradeDb = this.db.FINReceiptNonTradeDbs.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmItemNo[i]) && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.FINReceiptNonTradeDbs.DeleteOnSubmit(_finReceiptNonTradeDb);
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

        public int GetMaxNoItemFINReceiptNonTradeDb(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINReceiptNonTradeDbs.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleFINReceiptNonTradeDbForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINReceiptNonTradeHd _finReceiptNonTradeHd = this.db.FINReceiptNonTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finReceiptNonTradeHd != null)
                    {
                        if (_finReceiptNonTradeHd.Status != PaymentNonTradeDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINReceiptNonTradeDb(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINReceiptNonTradeHd _finReceiptNonTradeHd = this.db.FINReceiptNonTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finReceiptNonTradeHd.Status == PaymentNonTradeDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finReceiptNonTradeHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finReceiptNonTradeHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finReceiptNonTradeHd != null)
                    {
                        if ((_finReceiptNonTradeHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINReceiptNonTradeDbs
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINReceiptNonTradeDbs.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINReceiptNonTradeCrs
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINReceiptNonTradeCrs.DeleteAllOnSubmit(_query2);

                            this.db.FINReceiptNonTradeHds.DeleteOnSubmit(_finReceiptNonTradeHd);

                            _result = true;
                        }
                        else if (_finReceiptNonTradeHd.FileNmbr != "" && _finReceiptNonTradeHd.Status == PaymentNonTradeDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finReceiptNonTradeHd.Status = PaymentNonTradeDataMapper.GetStatus(TransStatus.Deleted);
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

        ~PaymentNonTradeBL()
        {

        }

    }
}
