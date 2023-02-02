using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class BankReconBL : Base
    {
        public BankReconBL()
        {
        }

        #region BankRecon
        public double RowsCountFinance_BankRecon(string _prmCategory, string _prmKeyword)
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
                            from _bankRecon in this.db.Finance_BankRecons
                            where (SqlMethods.Like(_bankRecon.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like((_bankRecon.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && _bankRecon.Status != BankReconDataMapper.GetStatusByte(TransStatus.Deleted)    
                            select _bankRecon.BankReconCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<Finance_BankRecon> GetListFinance_BankRecon(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Finance_BankRecon> _result = new List<Finance_BankRecon>();

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
                               from _bankRecon in this.db.Finance_BankRecons
                               where (SqlMethods.Like(_bankRecon.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_bankRecon.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && _bankRecon.Status != BankReconDataMapper.GetStatusByte(TransStatus.Deleted)   
                               orderby _bankRecon.EditDate descending
                               select new
                               {
                                   BankReconCode = _bankRecon.BankReconCode,
                                   TransNmbr = _bankRecon.TransNmbr,
                                   FileNmbr = _bankRecon.FileNmbr,
                                   TransDate = _bankRecon.TransDate,
                                   PayCode = _bankRecon.PayCode,
                                   PayName = (
                                                from _msPayType in this.db.MsPayTypes
                                                where _msPayType.PayCode == _bankRecon.PayCode
                                                select _msPayType.PayName
                                              ).FirstOrDefault(),
                                   AccPay = _bankRecon.AccPay,
                                   FgPay = _bankRecon.FgPay,
                                   SumValueForex = _bankRecon.SumValueForex,
                                   DiffValueForex = _bankRecon.DiffValueForex,
                                   BankValueForex = _bankRecon.BankValueForex,
                                   Status = _bankRecon.Status,
                                   Remark = _bankRecon.Remark
                               }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Finance_BankRecon(_row.BankReconCode, _row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.PayCode, _row.PayName, _row.AccPay, _row.FgPay, _row.SumValueForex, _row.DiffValueForex, _row.BankValueForex, _row.Status, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Finance_BankRecon GetSingleFinance_BankRecon(Guid _prmCode)
        {
            Finance_BankRecon _result = null;

            try
            {
                _result = this.db.Finance_BankRecons.Single(_temp => _temp.BankReconCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAccountByBankReconCode(Guid _prmCode)
        {
            string _result = "";

            try
            {
                _result = (
                            from _bankRecon in this.db.Finance_BankRecons
                            where _bankRecon.BankReconCode == _prmCode
                            select _bankRecon.AccPay
                          ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFinance_BankRecon(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Finance_BankRecon _bankRecon = this.db.Finance_BankRecons.Single(_temp => _temp.BankReconCode.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_bankRecon != null)
                    {
                        if ((_bankRecon.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.Finance_BankReconAccounts
                                          where _detail.BankReconCode.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.Finance_BankReconAccounts.DeleteAllOnSubmit(_query);

                            this.db.Finance_BankRecons.DeleteOnSubmit(_bankRecon);

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

        public string AddFinance_BankRecon(Finance_BankRecon _prmFinance_BankRecon)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFinance_BankRecon.Year, _prmFinance_BankRecon.Period, AppModule.GetValue(TransactionType.BankRecon), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFinance_BankRecon.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.Finance_BankRecons.InsertOnSubmit(_prmFinance_BankRecon);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFinance_BankRecon.BankReconCode.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string EditFinance_BankRecon(Finance_BankRecon _prmFinance_BankRecon)
        {
            string _result = "";

            try
            {
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                _result = "You Failed Edit Data";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(string _prmBankReconCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spFinance_BankReconGetAppr(new Guid(_prmBankReconCode), 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.BankRecon);
                    _transActivity.TransNmbr = GetSingleFinance_BankRecon(new Guid(_prmBankReconCode)).TransNmbr;
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

                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string Approve(string _prmBankReconCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.spFinance_BankReconApprove(new Guid(_prmBankReconCode), 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        Finance_BankRecon _bankRecon = this.GetSingleFinance_BankRecon(new Guid(_prmBankReconCode));
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_bankRecon.TransDate.Year, _bankRecon.TransDate.Month, AppModule.GetValue(TransactionType.BankRecon), this._companyTag, ""))
                        {
                            _bankRecon.FileNmbr = _item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.BankRecon);
                        _transActivity.TransNmbr = GetSingleFinance_BankRecon(new Guid(_prmBankReconCode)).TransNmbr;
                        _transActivity.FileNmbr = GetSingleFinance_BankRecon(new Guid(_prmBankReconCode)).FileNmbr;
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
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string Posting(string _prmBankReconCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                Finance_BankRecon _bankRecon = this.db.Finance_BankRecons.Single(_temp => _temp.BankReconCode.ToString().Trim().ToLower() == _prmBankReconCode.ToString().Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_bankRecon.TransDate);
                if (_locked == "")
                {
                    this.db.spFinance_BankReconPost(new Guid(_prmBankReconCode), 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.BankRecon);
                        _transActivity.TransNmbr = GetSingleFinance_BankRecon(new Guid(_prmBankReconCode)).TransNmbr;
                        _transActivity.FileNmbr = GetSingleFinance_BankRecon(new Guid(_prmBankReconCode)).FileNmbr;
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
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string Unposting(string _prmBankReconCode, string _prmuser)
        {
            string _result = "";
            TransactionCloseBL _transCloseBL = new TransactionCloseBL();

            try
            {
                Finance_BankRecon _bankRecon = this.db.Finance_BankRecons.Single(_temp => _temp.BankReconCode.ToString().Trim().ToLower() == _prmBankReconCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_bankRecon.TransDate);
                if (_locked == "")
                {
                    this.db.spFinance_BankReconUnPost(new Guid(_prmBankReconCode), 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.BankRecon);
                        //_transActivity.TransNmbr = GetSingleFinance_BankRecon(new Guid(_prmBankReconCode)).TransNmbr;
                        //_transActivity.FileNmbr = GetSingleFinance_BankRecon(new Guid(_prmBankReconCode)).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFinance_BankRecon(new Guid(_prmBankReconCode)).TransDate;
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
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleFinance_BankReconStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Finance_BankRecon _bankRecon = this.db.Finance_BankRecons.Single(_temp => _temp.BankReconCode.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_bankRecon != null)
                    {
                        if (_bankRecon.Status != BankReconDataMapper.GetStatusByte(TransStatus.Posted))
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

        public bool DeleteMultiApproveFinance_BankRecon(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Finance_BankRecon _bankRecon = this.db.Finance_BankRecons.Single(_temp => _temp.BankReconCode.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_bankRecon.Status == BankReconDataMapper.GetStatusByte(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _bankRecon.TransNmbr;
                        _unpostingActivity.FileNmbr = _bankRecon.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_bankRecon != null)
                    {
                        if ((_bankRecon.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.Finance_BankReconAccounts
                                          where _detail.BankReconCode.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.Finance_BankReconAccounts.DeleteAllOnSubmit(_query);

                            this.db.Finance_BankRecons.DeleteOnSubmit(_bankRecon);

                            _result = true;
                        }
                        else if (_bankRecon.FileNmbr != "" && _bankRecon.Status == BankReconDataMapper.GetStatusByte(TransStatus.Approved))
                        {
                            _bankRecon.Status = BankReconDataMapper.GetStatusByte(TransStatus.Deleted);
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

        #endregion Finance_BankRecon

        #region BankReconAccount

        public List<Finance_BankReconAccount> GetListFinance_BankReconAccount(string _prmBankReconCode)
        {
            List<Finance_BankReconAccount> _result = new List<Finance_BankReconAccount>();

            try
            {
                var _query = (
                               from _bankReconAccount in this.db.Finance_BankReconAccounts
                               join _msAccount in this.db.MsAccounts
                                   on _bankReconAccount.Account equals _msAccount.Account
                               orderby _msAccount.AccountName ascending
                               where _bankReconAccount.BankReconCode == new Guid(_prmBankReconCode)
                               select new
                               {
                                   BankReconAccountCode = _bankReconAccount.BankReconAccountCode,
                                   BankReconCode = _bankReconAccount.BankReconCode,
                                   Account = _bankReconAccount.Account,
                                   AccountName = (
                                                    from _msAcc in this.db.MsAccounts
                                                    where _msAcc.Account == _bankReconAccount.Account
                                                    select _msAcc.AccountName
                                                 ).FirstOrDefault(),
                                   FgSubled = _bankReconAccount.FgSubLed,
                                   Subled = _bankReconAccount.SubLed,
                                   SubledName = (
                                                    from _viewMsSubled in this.db.V_MsSubleds
                                                    where _viewMsSubled.SubLed_No == _bankReconAccount.SubLed
                                                    select _viewMsSubled.SubLed_Name
                                                ).FirstOrDefault(),
                                   ForexRate = _bankReconAccount.ForexRate,
                                   FgValue = _bankReconAccount.FgValue,
                                   AmountForex = _bankReconAccount.AmountForex,
                                   Remark = _bankReconAccount.Remark
                               }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Finance_BankReconAccount(_row.BankReconAccountCode, _row.BankReconCode, _row.Account, _row.AccountName, _row.FgSubled, _row.Subled, _row.SubledName, _row.ForexRate, _row.FgValue, _row.AmountForex, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Finance_BankReconAccount GetSingleFinance_BankReconAccount(string _prmCode)
        {
            Finance_BankReconAccount _result = null;

            try
            {
                _result = this.db.Finance_BankReconAccounts.Single(_temp => _temp.BankReconAccountCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFinance_BankReconAccount(string[] _prmCode, string _prmHeaderCode)
        {
            bool _result = false;

            Finance_BankRecon _finBankRecon = new Finance_BankRecon();
            decimal _totalPlus = 0;
            decimal _totalMin = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Finance_BankReconAccount _bankReconAccount = this.db.Finance_BankReconAccounts.Single(_temp => _temp.BankReconAccountCode.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.Finance_BankReconAccounts.DeleteOnSubmit(_bankReconAccount);
                }

                var _query = (
                               from _finBankReconAcc in this.db.Finance_BankReconAccounts
                               where !(
                                           from _code in _prmCode
                                           select _code
                                       ).Contains(_finBankReconAcc.BankReconAccountCode.ToString())
                                       && _finBankReconAcc.BankReconCode == new Guid(_prmHeaderCode)
                                       && _finBankReconAcc.FgValue == true
                               group _finBankReconAcc by _finBankReconAcc.BankReconAccountCode into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _totalPlus = _totalPlus + _obj.AmountForex;
                }

                var _query2 = (
                               from _finBankReconAcc in this.db.Finance_BankReconAccounts
                               where !(
                                           from _code in _prmCode
                                           select _code
                                       ).Contains(_finBankReconAcc.BankReconAccountCode.ToString())
                                       && _finBankReconAcc.BankReconCode == new Guid(_prmHeaderCode)
                                       && _finBankReconAcc.FgValue == false
                               group _finBankReconAcc by _finBankReconAcc.BankReconAccountCode into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query2)
                {
                    _totalMin = _totalMin + _obj.AmountForex;
                }

                _finBankRecon = this.db.Finance_BankRecons.Single(_temp => _temp.BankReconCode == new Guid(_prmHeaderCode));

                _finBankRecon.DiffValueForex = _totalPlus - _totalMin;
                _finBankRecon.BankValueForex = _finBankRecon.SumValueForex + _finBankRecon.DiffValueForex;

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    Finance_BankReconAccount _bankReconAccount = this.db.Finance_BankReconAccounts.Single(_temp => _temp.BankReconAccountCode.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.Finance_BankReconAccounts.DeleteOnSubmit(_bankReconAccount);
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

        public bool AddFinance_BankReconAccount(Finance_BankReconAccount _prmFinance_BankReconAccount)
        {
            bool _result = false;

            Finance_BankRecon _finBankRecon = new Finance_BankRecon();
            decimal _totalPlus = 0;
            decimal _totalMin = 0;

            try
            {
                var _query = (
                               from _finBankReconAcc in this.db.Finance_BankReconAccounts
                               where !(
                                           from _finBankReconAcc2 in this.db.Finance_BankReconAccounts
                                           where _finBankReconAcc2.BankReconAccountCode == _prmFinance_BankReconAccount.BankReconAccountCode
                                                && _finBankReconAcc.BankReconCode == _finBankReconAcc2.BankReconCode
                                           select _finBankReconAcc2.BankReconAccountCode
                                       ).Contains(_finBankReconAcc.BankReconAccountCode)
                                       && _finBankReconAcc.BankReconCode == _prmFinance_BankReconAccount.BankReconCode
                                       && _finBankReconAcc.FgValue == true
                               group _finBankReconAcc by _finBankReconAcc.BankReconAccountCode into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _totalPlus = _totalPlus + _obj.AmountForex;
                }

                var _query2 = (
                               from _finBankReconAcc in this.db.Finance_BankReconAccounts
                               where !(
                                           from _finBankReconAcc2 in this.db.Finance_BankReconAccounts
                                           where _finBankReconAcc2.BankReconAccountCode == _prmFinance_BankReconAccount.BankReconAccountCode
                                                && _finBankReconAcc.BankReconCode == _finBankReconAcc2.BankReconCode
                                           select _finBankReconAcc2.BankReconAccountCode
                                       ).Contains(_finBankReconAcc.BankReconAccountCode)
                                       && _finBankReconAcc.BankReconCode == _prmFinance_BankReconAccount.BankReconCode
                                       && _finBankReconAcc.FgValue == false
                               group _finBankReconAcc by _finBankReconAcc.BankReconAccountCode into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query2)
                {
                    _totalMin = _totalMin + _obj.AmountForex;
                }

                _finBankRecon = this.db.Finance_BankRecons.Single(_temp => _temp.BankReconCode == _prmFinance_BankReconAccount.BankReconCode);

                if (_prmFinance_BankReconAccount.FgValue == true)
                {
                    _totalPlus = _totalPlus + _prmFinance_BankReconAccount.AmountForex;
                }
                else
                {
                    _totalMin = _totalMin + _prmFinance_BankReconAccount.AmountForex;
                }
                _finBankRecon.DiffValueForex = _totalPlus - _totalMin;
                _finBankRecon.BankValueForex = _finBankRecon.SumValueForex + _finBankRecon.DiffValueForex;

                this.db.Finance_BankReconAccounts.InsertOnSubmit(_prmFinance_BankReconAccount);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFinance_BankReconAccount(Finance_BankReconAccount _prmFinance_BankReconAccount)
        {
            bool _result = false;

            Finance_BankRecon _finBankRecon = new Finance_BankRecon();
            decimal _totalPlus = 0;
            decimal _totalMin = 0;

            try
            {
                var _query = (
                               from _finBankReconAcc in this.db.Finance_BankReconAccounts
                               where !(
                                           from _finBankReconAcc2 in this.db.Finance_BankReconAccounts
                                           where _finBankReconAcc2.BankReconAccountCode == _prmFinance_BankReconAccount.BankReconAccountCode
                                                && _finBankReconAcc.BankReconCode == _finBankReconAcc2.BankReconCode
                                           select _finBankReconAcc2.BankReconAccountCode
                                       ).Contains(_finBankReconAcc.BankReconAccountCode)
                                       && _finBankReconAcc.BankReconCode == _prmFinance_BankReconAccount.BankReconCode
                                       && _finBankReconAcc.FgValue == true
                               group _finBankReconAcc by _finBankReconAcc.BankReconAccountCode into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _totalPlus = _totalPlus + _obj.AmountForex;
                }

                var _query2 = (
                               from _finBankReconAcc in this.db.Finance_BankReconAccounts
                               where !(
                                           from _finBankReconAcc2 in this.db.Finance_BankReconAccounts
                                           where _finBankReconAcc2.BankReconAccountCode == _prmFinance_BankReconAccount.BankReconAccountCode
                                                && _finBankReconAcc.BankReconCode == _finBankReconAcc2.BankReconCode
                                           select _finBankReconAcc2.BankReconAccountCode
                                       ).Contains(_finBankReconAcc.BankReconAccountCode)
                                       && _finBankReconAcc.BankReconCode == _prmFinance_BankReconAccount.BankReconCode
                                       && _finBankReconAcc.FgValue == false
                               group _finBankReconAcc by _finBankReconAcc.BankReconAccountCode into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query2)
                {
                    _totalMin = _totalMin + _obj.AmountForex;
                }

                _finBankRecon = this.db.Finance_BankRecons.Single(_temp => _temp.BankReconCode == _prmFinance_BankReconAccount.BankReconCode);

                if (_prmFinance_BankReconAccount.FgValue == true)
                {
                    _totalPlus = _totalPlus + _prmFinance_BankReconAccount.AmountForex;
                }
                else
                {
                    _totalMin = _totalMin + _prmFinance_BankReconAccount.AmountForex;
                }

                _finBankRecon.DiffValueForex = _totalPlus - _totalMin;
                _finBankRecon.BankValueForex = _finBankRecon.SumValueForex + _finBankRecon.DiffValueForex;

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

        public decimal GetAmountBankReconByAccount(DateTime _prmDate, string _prmAccount)
        {
            decimal _result = 0;

            var _query = (
                              from _glJournalHd in this.db.GLJournalHds
                              join _glJournalDt in this.db.GLJournalDts
                                on _glJournalHd.Reference equals _glJournalDt.Reference
                              where _glJournalHd.TransDate <= _prmDate
                                && _glJournalDt.Account == _prmAccount
                                && _glJournalHd.TransClass == _glJournalDt.TransClass
                                && _glJournalHd.Status == BankReconDataMapper.GetStatus(TransStatus.Posted)
                              group _glJournalDt by _glJournalDt.Account into _grp
                              select new
                              {
                                  DebitForex = _grp.Sum(a => a.DebitForex),
                                  CreditForex = _grp.Sum(a => a.CreditForex)
                              }
                          );
            foreach (var _row in _query)
            {
                _result = _row.DebitForex - _row.CreditForex;
            }

            return _result;
        }

        public decimal GetAmountHomeBankReconByAccount(DateTime _prmDate, string _prmAccount)
        {
            decimal _result = 0;

            var _query = (
                              from _glJournalHd in this.db.GLJournalHds
                              join _glJournalDt in this.db.GLJournalDts
                                on _glJournalHd.Reference equals _glJournalDt.Reference
                              where _glJournalHd.TransDate <= _prmDate
                                && _glJournalDt.Account == _prmAccount
                                && _glJournalHd.TransClass == _glJournalDt.TransClass
                                && _glJournalHd.Status == BankReconDataMapper.GetStatus(TransStatus.Posted)
                              group _glJournalDt by _glJournalDt.Account into _grp
                              select new
                              {
                                  DebitHome = _grp.Sum(a => a.DebitHome),
                                  CreditHome = _grp.Sum(a => a.CreditHome)
                              }
                          );
            foreach (var _row in _query)
            {
                _result = _row.DebitHome - _row.CreditHome;
            }

            return _result;
        }

        ~BankReconBL()
        {
        }
    }
}
