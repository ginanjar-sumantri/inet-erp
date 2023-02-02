using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;


namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class PaymentNonPurchaseBL : Base
    {
        public PaymentNonPurchaseBL()
        {

        }

        #region Payment Non Purchase
        public double RowsCountPaymentNonPurchase(string _prmCategory, string _prmKeyword)
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
                            from _finPayNonTradeHd in this.db.FINPayNonTradeHds
                            join _msSupp in this.db.MsSuppliers
                                on _finPayNonTradeHd.SuppCode equals _msSupp.SuppCode
                            where (SqlMethods.Like(_finPayNonTradeHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_finPayNonTradeHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _finPayNonTradeHd.Status != PaymentNonPurchaseDataMapper.GetStatus(TransStatus.Deleted)
                            select _finPayNonTradeHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINPayNonTradeHd> GetListFINPayNonTradeHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINPayNonTradeHd> _result = new List<FINPayNonTradeHd>();

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
                                from _finPayNonTradeHd in this.db.FINPayNonTradeHds
                                join _msSupplier in this.db.MsSuppliers
                                    on _finPayNonTradeHd.SuppCode equals _msSupplier.SuppCode
                                where (SqlMethods.Like(_finPayNonTradeHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finPayNonTradeHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finPayNonTradeHd.Status != PaymentNonPurchaseDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _finPayNonTradeHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finPayNonTradeHd.TransNmbr,
                                    FileNmbr = _finPayNonTradeHd.FileNmbr,
                                    TransDate = _finPayNonTradeHd.TransDate,
                                    Status = _finPayNonTradeHd.Status,
                                    SuppCode = _finPayNonTradeHd.SuppCode,
                                    SuppName = _msSupplier.SuppName,
                                    CurrCode = _finPayNonTradeHd.CurrCode,
                                    ForexRate = _finPayNonTradeHd.ForexRate,
                                    Remark = _finPayNonTradeHd.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINPayNonTradeHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.SuppCode, _row.SuppName, _row.CurrCode, _row.ForexRate, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINPayNonTradeDb> GetListFINPayNonTradeDb(string _prmCode)
        {
            List<FINPayNonTradeDb> _result = new List<FINPayNonTradeDb>();

            try
            {
                var _query = (
                                from _finFINPayNonTradeDb in this.db.FINPayNonTradeDbs
                                join _msAccount in this.db.MsAccounts
                                    on _finFINPayNonTradeDb.Account equals _msAccount.Account
                                where _finFINPayNonTradeDb.TransNmbr == _prmCode
                                orderby _finFINPayNonTradeDb.ItemNo descending
                                select new
                                {
                                    ItemNo = _finFINPayNonTradeDb.ItemNo,
                                    Account = _finFINPayNonTradeDb.Account,
                                    AccountName = _msAccount.AccountName,
                                    SubLed = _finFINPayNonTradeDb.SubLed,
                                    SubledName = (
                                                   from _vMsSubled in this.db.V_MsSubleds
                                                   where _vMsSubled.SubLed_No == _finFINPayNonTradeDb.SubLed
                                                   select _vMsSubled.SubLed_Name
                                                 ).FirstOrDefault(),
                                    AmountForex = _finFINPayNonTradeDb.AmountForex,
                                    Remark = _finFINPayNonTradeDb.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINPayNonTradeDb(_row.ItemNo, _row.Account, _row.AccountName, _row.SubLed, _row.SubledName, _row.AmountForex, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINPayNonTradeCr> GetListFINPayNonTradeCr(string _prmCode)
        {
            List<FINPayNonTradeCr> _result = new List<FINPayNonTradeCr>();

            try
            {
                var _query = (
                                from _finFINPayNonTradeCr in this.db.FINPayNonTradeCrs
                                where _finFINPayNonTradeCr.TransNmbr == _prmCode
                                orderby _finFINPayNonTradeCr.ItemNo descending
                                select new
                                {
                                    ItemNo = _finFINPayNonTradeCr.ItemNo,
                                    PayType = _finFINPayNonTradeCr.PayType,
                                    DocumentNo = _finFINPayNonTradeCr.DocumentNo,
                                    AmountForex = _finFINPayNonTradeCr.AmountForex,
                                    Remark = _finFINPayNonTradeCr.Remark,
                                    DueDate = _finFINPayNonTradeCr.DueDate,
                                    BankPayment = _finFINPayNonTradeCr.BankPayment,
                                    BankExpense = _finFINPayNonTradeCr.BankExpense
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINPayNonTradeCr(_row.ItemNo, _row.PayType, _row.DocumentNo, _row.AmountForex, _row.Remark, _row.DueDate, _row.BankPayment, _row.BankExpense));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPayNonTradeHd GetSingleFINPayNonTradeHd(string _prmCode)
        {
            FINPayNonTradeHd _result = null;

            try
            {
                _result = this.db.FINPayNonTradeHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPayNonTradeHd GetViewFINPayNonTradeHd(string _prmCode)
        {
            FINPayNonTradeHd _result = new FINPayNonTradeHd();

            try
            {
                var _query = (
                                from _finFINPayNonTradeHd in this.db.FINPayNonTradeHds
                                where _finFINPayNonTradeHd.TransNmbr == _prmCode
                                select new
                                {
                                    TransNmbr = _finFINPayNonTradeHd.TransNmbr,
                                    FileNmbr = _finFINPayNonTradeHd.FileNmbr,
                                    TransDate = _finFINPayNonTradeHd.TransDate,
                                    SuppCode = _finFINPayNonTradeHd.SuppCode,
                                    SuppName = (
                                                from _msSupplier in this.db.MsSuppliers
                                                where _msSupplier.SuppCode == _finFINPayNonTradeHd.SuppCode
                                                select _msSupplier.SuppName
                                              ).FirstOrDefault(),
                                    CurrCode = _finFINPayNonTradeHd.CurrCode,
                                    ForexRate = _finFINPayNonTradeHd.ForexRate,
                                    Remark = _finFINPayNonTradeHd.Remark,
                                    Status = _finFINPayNonTradeHd.Status
                                }
                            );
                foreach (var _row in _query)
                {
                    _result.TransNmbr = _row.TransNmbr;
                    _result.FileNmbr = _row.FileNmbr;
                    _result.TransDate = _row.TransDate;
                    _result.SuppCode = _row.SuppCode;
                    _result.SuppName = _row.SuppName;
                    _result.CurrCode = _row.CurrCode;
                    _result.ForexRate = _row.ForexRate;
                    _result.Remark = _row.Remark;
                    _result.Status = _row.Status;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPayNonTradeDb GetSingleFINPayNonTradeDb(string _prmCode, string _prmItemNo)
        {
            FINPayNonTradeDb _result = null;

            try
            {
                _result = this.db.FINPayNonTradeDbs.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == Convert.ToInt32(_prmItemNo));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPayNonTradeDb GetViewFINPayNonTradeDb(string _prmCode, string _prmItemNo)
        {
            FINPayNonTradeDb _result = new FINPayNonTradeDb();

            try
            {
                var _query = (
                                from _finFINPayNonTradeDb in this.db.FINPayNonTradeDbs
                                where _finFINPayNonTradeDb.TransNmbr == _prmCode && _finFINPayNonTradeDb.ItemNo == Convert.ToInt32(_prmItemNo)
                                select new
                                {
                                    Account = _finFINPayNonTradeDb.Account,
                                    AccountName = (
                                                    from _msAccount in this.db.MsAccounts
                                                    where _msAccount.Account == _finFINPayNonTradeDb.Account
                                                    select _msAccount.AccountName
                                                  ).FirstOrDefault(),
                                    SubledName = (
                                                    from _vMsSubled in this.db.V_MsSubleds
                                                    where _vMsSubled.SubLed_No == _finFINPayNonTradeDb.SubLed
                                                    select _vMsSubled.SubLed_Name
                                                  ).FirstOrDefault(),
                                    AmountForex = _finFINPayNonTradeDb.AmountForex,
                                    Remark = _finFINPayNonTradeDb.Remark
                                }
                            );
                foreach (var _row in _query)
                {
                    _result.Account = _row.Account;
                    _result.AccountName = _row.AccountName;
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

        public FINPayNonTradeCr GetSingleFINPayNonTradeCr(string _prmCode, string _prmItemNo)
        {
            FINPayNonTradeCr _result = null;

            try
            {
                _result = this.db.FINPayNonTradeCrs.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == Convert.ToInt32(_prmItemNo));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINPayNonTradeCr GetViewFINPayNonTradeCr(string _prmCode, string _prmItemNo)
        {
            FINPayNonTradeCr _result = new FINPayNonTradeCr();

            try
            {
                var _query = (
                                from _finFINPayNonTradeCr in this.db.FINPayNonTradeCrs
                                where _finFINPayNonTradeCr.TransNmbr == _prmCode && _finFINPayNonTradeCr.ItemNo == Convert.ToInt32(_prmItemNo)
                                select new
                                {
                                    PayName = (
                                                from _msPayType in this.db.MsPayTypes
                                                where _msPayType.PayCode == _finFINPayNonTradeCr.PayType
                                                select _msPayType.PayName
                                              ).FirstOrDefault(),
                                    DocumentNo = _finFINPayNonTradeCr.DocumentNo,
                                    AmountForex = _finFINPayNonTradeCr.AmountForex,
                                    BankName = (
                                                    from _vMsBankPayment in this.db.V_MsBankPayments
                                                    where _vMsBankPayment.BankCode == _finFINPayNonTradeCr.BankPayment
                                                    select _vMsBankPayment.BankName
                                                ).FirstOrDefault(),
                                    DueDate = _finFINPayNonTradeCr.DueDate,
                                    BankExpense = _finFINPayNonTradeCr.BankExpense,
                                    Remark = _finFINPayNonTradeCr.Remark
                                }
                            );
                foreach (var _row in _query)
                {
                    _result.PayName = _row.PayName;
                    _result.DocumentNo = _row.DocumentNo;
                    _result.AmountForex = _row.AmountForex;
                    _result.BankName = _row.BankName;
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

        public bool DeleteMultiFINPayNonTradeHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINPayNonTradeHd _finPayNonTradeHd = this.db.FINPayNonTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finPayNonTradeHd != null)
                    {
                        if ((_finPayNonTradeHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINPayNonTradeDbs
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINPayNonTradeDbs.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINPayNonTradeCrs
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINPayNonTradeCrs.DeleteAllOnSubmit(_query2);

                            this.db.FINPayNonTradeHds.DeleteOnSubmit(_finPayNonTradeHd);

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

        public bool DeleteMultiFINPayNonTradeDb(string[] _prmItemNo, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmItemNo.Length; i++)
                {
                    FINPayNonTradeDb _finPayNonTradeDb = this.db.FINPayNonTradeDbs.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmItemNo[i]) && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.FINPayNonTradeDbs.DeleteOnSubmit(_finPayNonTradeDb);
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

        public bool DeleteMultiFINPayNonTradeCr(string[] _prmItemNo, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmItemNo.Length; i++)
                {
                    FINPayNonTradeCr _finPayNonTradeCr = this.db.FINPayNonTradeCrs.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmItemNo[i]) && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.FINPayNonTradeCrs.DeleteOnSubmit(_finPayNonTradeCr);
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

        public string AddFINPayNonTradeHd(FINPayNonTradeHd _prmFINPayNonTradeHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINPayNonTradeHd.TransDate.Year, _prmFINPayNonTradeHd.TransDate.Month, AppModule.GetValue(TransactionType.PaymentNonPurchase), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINPayNonTradeHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINPayNonTradeHds.InsertOnSubmit(_prmFINPayNonTradeHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINPayNonTradeHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINPayNonTradeDb(FINPayNonTradeDb _prmFINPayNonTradeDb)
        {
            bool _result = false;

            try
            {
                this.db.FINPayNonTradeDbs.InsertOnSubmit(_prmFINPayNonTradeDb);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINPayNonTradeCr(FINPayNonTradeCr _prmFINPayNonTradeCr)
        {
            bool _result = false;

            try
            {
                this.db.FINPayNonTradeCrs.InsertOnSubmit(_prmFINPayNonTradeCr);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINPayNonTradeHd(FINPayNonTradeHd _prmFINPayNonTradeHd)
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

        public bool EditFINPayNonTradeDb(FINPayNonTradeDb _prmFINPayNonTradeDb)
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

        public bool EditFINPayNonTradeCr(FINPayNonTradeCr _prmFINPayNonTradeCr)
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

        public int GetMaxNoItemFINPayNonTradeDb(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINPayNonTradeDbs.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxNoItemFINPayNonTradeCr(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINPayNonTradeCrs.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
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
                                from _finFINPayNonTradeHd in this.db.FINPayNonTradeHds
                                where _finFINPayNonTradeHd.TransNmbr == _prmCode
                                select new
                                {
                                    CurrCode = _finFINPayNonTradeHd.CurrCode
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

        public string GetApproval(string _prmCode, string _prmuser)
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
                this.db.S_FNPayNonTradeGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    FINPayNonTradeHd _finPayNonTradeHd = this.GetSingleFINPayNonTradeHd(_prmCode);
                    foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finPayNonTradeHd.TransDate.Year, _finPayNonTradeHd.TransDate.Month, AppModule.GetValue(TransactionType.PaymentNonPurchase), this._companyTag, ""))
                    {
                        _finPayNonTradeHd.FileNmbr = item.Number;
                    }
                    //_scope.Complete();

                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.PaymentNonPurchase);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
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

        public string Approve(string _prmCode, string _prmuser)
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
                this.db.S_FNPayNonTradeApprove(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    //    FINPayNonTradeHd _finPayNonTradeHd = this.GetSingleFINPayNonTradeHd(_prmCode);
                    //    foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finPayNonTradeHd.TransDate.Year, _finPayNonTradeHd.TransDate.Month, AppModule.GetValue(TransactionType.PaymentNonPurchase), this._companyTag, ""))
                    //    {
                    //        _finPayNonTradeHd.FileNmbr = item.Number;
                    //    }

                    //    this.db.SubmitChanges();

                    //    _scope.Complete();

                    _result = "Approve Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.PaymentNonPurchase);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = this.GetSingleFINPayNonTradeHd(_prmCode).FileNmbr;
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
                FINPayNonTradeHd _finPayNonTradeHd = this.db.FINPayNonTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finPayNonTradeHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNPayNonTradePost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.PaymentNonPurchase);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleFINPayNonTradeHd(_prmCode).FileNmbr;
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

        public string Unposting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINPayNonTradeHd _finPayNonTradeHd = this.db.FINPayNonTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finPayNonTradeHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNPayNonTradeUnPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";
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

        public bool GetSingleFINPayNonTradeHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINPayNonTradeHd _finPayNonTradeHd = this.db.FINPayNonTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finPayNonTradeHd != null)
                    {
                        if (_finPayNonTradeHd.Status != PaymentNonPurchaseDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINPayNonTradeHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINPayNonTradeHd _finPayNonTradeHd = this.db.FINPayNonTradeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finPayNonTradeHd.Status == PaymentNonPurchaseDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finPayNonTradeHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finPayNonTradeHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finPayNonTradeHd != null)
                    {
                        if ((_finPayNonTradeHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINPayNonTradeDbs
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINPayNonTradeDbs.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINPayNonTradeCrs
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINPayNonTradeCrs.DeleteAllOnSubmit(_query2);

                            this.db.FINPayNonTradeHds.DeleteOnSubmit(_finPayNonTradeHd);

                            _result = true;
                        }
                        else if (_finPayNonTradeHd.FileNmbr != "" && _finPayNonTradeHd.Status == PaymentNonPurchaseDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finPayNonTradeHd.Status = PaymentNonPurchaseDataMapper.GetStatus(TransStatus.Deleted);
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

        ~PaymentNonPurchaseBL()
        {

        }
    }
}
