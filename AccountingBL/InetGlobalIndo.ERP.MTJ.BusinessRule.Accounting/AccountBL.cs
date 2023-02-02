using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class AccountBL : Base
    {
        public AccountBL()
        {

        }

        #region Account

        public double RowsCountAccount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _msAccount in this.db.MsAccounts
                            where (SqlMethods.Like(_msAccount.Account.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_msAccount.AccountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msAccount.Account
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountAccountForReport()
        {
            double _result = 0;

            var _query =
                        (
                            from _msAccount in this.db.MsAccounts
                            where _msAccount.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                            select _msAccount.Account
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountAccountForReport(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _msAccount in this.db.MsAccounts
                            where _msAccount.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                                    && (SqlMethods.Like(_msAccount.Account.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msAccount.AccountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msAccount.Account
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountAccountForMasterDef(string _prmCurrCode)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCurrCode != "")
            {
                _pattern1 = "%" + _prmCurrCode + "%";
            }

            var _query =
                        (
                            from _msAccount in this.db.MsAccounts
                            where _msAccount.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                             && (SqlMethods.Like(_msAccount.CurrCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            select _msAccount.Account
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountAccountForMasterDef(string _prmCurrCode, String _prmCategory, String _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCurrCode != "")
            {
                _pattern1 = "%" + _prmCurrCode + "%";
            }

            if (_prmCategory == "Code")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                            from _msAccount in this.db.MsAccounts
                            where _msAccount.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                             && (SqlMethods.Like(_msAccount.CurrCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                             && (SqlMethods.Like(_msAccount.Account.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             && (SqlMethods.Like(_msAccount.AccountName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                            select _msAccount.Account
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool EditAccount(MsAccount _prmMsAccount)
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

        public bool AddAccount(MsAccount _prmMsAccount)
        {
            bool _result = false;

            try
            {
                this.db.MsAccounts.InsertOnSubmit(_prmMsAccount);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddAccountList(List<MsAccount> _prmMsAccountList)
        {
            bool _result = false;

            try
            {
                foreach (MsAccount _row in _prmMsAccountList)
                {
                    MsAccount _msAccount = new MsAccount();

                    _msAccount.Account = _row.Account;
                    _msAccount.AccountName = _row.AccountName;
                    _msAccount.BranchAccCode = _row.BranchAccCode;
                    _msAccount.AccClass = _row.AccClass;
                    _msAccount.Detail = _row.Detail;
                    _msAccount.FgSubLed = _row.FgSubLed;
                    _msAccount.FgNormal = _row.FgNormal;
                    _msAccount.CurrCode = _row.CurrCode;
                    _msAccount.FgActive = _row.FgActive;
                    _msAccount.UserID = _row.UserID;
                    _msAccount.UserDate = _row.UserDate;
                    _msAccount.UserClose = _row.UserClose;
                    _msAccount.CloseDate = _row.CloseDate;

                    this.db.MsAccounts.InsertOnSubmit(_msAccount);
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

        public bool DeleteMultiAccount(string[] _prmAccount)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmAccount.Length; i++)
                {
                    MsAccount _msAccount = this.db.MsAccounts.Single(_temp => _temp.Account == _prmAccount[i]);

                    var _query = (from _detail in this.db.MsAccGroupDts
                                  where _detail.Account.Trim().ToLower() == _prmAccount[i].Trim().ToLower()
                                  select _detail);

                    this.db.MsAccGroupDts.DeleteAllOnSubmit(_query);

                    this.db.MsAccounts.DeleteOnSubmit(_msAccount);
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

        public MsAccount GetSingleAccount(string _prmAccount)
        {
            MsAccount _result = null;

            try
            {
                _result = this.db.MsAccounts.Single(_temp => _temp.Account == _prmAccount);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetLastAccount(string _prmAccount)
        {
            String _result = "";

            string _pattern1 = _prmAccount + "%";

            try
            {
                var _query = (from _msAccount in this.db.MsAccounts
                              where (SqlMethods.Like(_msAccount.Account.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              orderby _msAccount.Account descending
                              select _msAccount.Account
                    ).FirstOrDefault();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListAccount(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<MsAccount> _result = new List<MsAccount>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query1 =
                            (
                                from _account in this.db.MsAccounts
                                where (SqlMethods.Like(_account.Account.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_account.AccountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _account.Account ascending
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = _account.AccountName,
                                    AccClass = _account.AccClass,
                                    AccClassName = (
                                                    from _accClass in this.db.MsAccClasses
                                                    where _accClass.AccClassCode == _account.AccClass
                                                    select _accClass.AccClassName
                                                  ).FirstOrDefault(),
                                    Detail = _account.Detail,
                                    FgSubled = _account.FgSubLed,
                                    FgNormal = _account.FgNormal,
                                    CurrCode = _account.CurrCode,
                                    FgActive = _account.FgActive
                                }
                            );

                if (_prmOrderBy == "Account Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Account)) : (_query1.OrderByDescending(a => a.Account));
                if (_prmOrderBy == "Account Name")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccountName)) : (_query1.OrderByDescending(a => a.AccountName));
                if (_prmOrderBy == "Class Account")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AccClassName)) : (_query1.OrderByDescending(a => a.AccClassName));
                if (_prmOrderBy == "Detail")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.Detail)) : (_query1.OrderByDescending(a => a.Detail));
                if (_prmOrderBy == "Normal Balance")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FgNormal)) : (_query1.OrderByDescending(a => a.FgNormal));
                if (_prmOrderBy == "Sub Ledger")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FgSubled)) : (_query1.OrderByDescending(a => a.FgSubled));
                if (_prmOrderBy == "Currency")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.CurrCode)) : (_query1.OrderByDescending(a => a.CurrCode));
                if (_prmOrderBy == "Active")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.FgActive)) : (_query1.OrderByDescending(a => a.FgActive));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsAccount(_row.Account, _row.AccountName, _row.AccClass, _row.AccClassName, _row.Detail, _row.FgSubled, _row.FgNormal, _row.CurrCode, _row.FgActive));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListAccount()
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _account in this.db.MsAccounts
                                orderby _account.Account ascending
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = _account.AccountName,
                                    AccClass = _account.AccClass,
                                    AccClassName = (
                                                    from _accClass in this.db.MsAccClasses
                                                    where _accClass.AccClassCode == _account.AccClass
                                                    select _accClass.AccClassName
                                                  ).FirstOrDefault(),
                                    Detail = _account.Detail,
                                    FgSubled = _account.FgSubLed,
                                    FgNormal = _account.FgNormal,
                                    CurrCode = _account.CurrCode,
                                    FgActive = _account.FgActive
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsAccount(_row.Account, _row.AccountName, _row.AccClass, _row.AccClassName, _row.Detail, _row.FgSubled, _row.FgNormal, _row.CurrCode, _row.FgActive));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListForDDL()
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _account in this.db.MsAccounts
                                where _account.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                                orderby _account.AccountName ascending
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = _account.AccountName
                                }
                            ).OrderBy(_temp => _temp.AccountName);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListForDDLSpecTransaction(string _prmTransType, char _prmFgSubled, string _prmCurrCode)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _account in this.db.MsAccounts
                                join _transAccount in this.db.MsTransType_MsAccounts on _account.Account equals _transAccount.Account  
                                where _account.FgActive == AccountMapper.GetYesNo(YesNo.Yes) 
                                //&& _account.FgSubLed == _prmFgSubled 
                                && _account.CurrCode.Trim().ToLower() == _prmCurrCode.Trim().ToLower()
                                && _transAccount.TransTypeCode == _prmTransType
                                orderby _account.AccountName ascending
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = _account.AccountName
                                }
                            ).OrderBy(_temp => _temp.AccountName);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListForDDLSpecTransactionWOSubled(string _prmTransType, string _prmCurrCode)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _account in this.db.MsAccounts
                                join _transAccount in this.db.MsTransType_MsAccounts on _account.Account equals _transAccount.Account
                                where _account.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                                && _account.CurrCode.Trim().ToLower() == _prmCurrCode.Trim().ToLower()
                                && _transAccount.TransTypeCode == _prmTransType
                                orderby _account.AccountName ascending
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = _account.AccountName
                                }
                            ).OrderBy(_temp => _temp.AccountName);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListAccForBudgetForDDL(Guid _prmBudgetCode)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _account in this.db.MsAccounts
                                where !(
                                            from _glBudget in this.db.GLBudgetAccs
                                            where _glBudget.BudgetCode == _prmBudgetCode
                                            select _glBudget.Account
                                        ).Contains(_account.Account)
                                        && _account.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                                orderby _account.AccountName ascending
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = _account.AccountName
                                }
                            ).OrderBy(_temp => _temp.AccountName);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListForDDL(char _prmFgSubled, string _prmCurrCode)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _account in this.db.MsAccounts
                                where _account.FgActive == AccountMapper.GetYesNo(YesNo.Yes) && _account.FgSubLed == _prmFgSubled && _account.CurrCode.Trim().ToLower() == _prmCurrCode.Trim().ToLower()
                                orderby _account.AccountName ascending
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = _account.AccountName
                                }
                            ).OrderBy(_temp => _temp.AccountName);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListForDDL(string _prmCurrCode)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _account in this.db.MsAccounts
                                where _account.FgActive == AccountMapper.GetYesNo(YesNo.Yes) && _account.CurrCode.Trim().ToLower() == _prmCurrCode.Trim().ToLower()
                                orderby _account.AccountName ascending
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = _account.AccountName
                                }
                            ).OrderBy(_temp => _temp.AccountName);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListForReport(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsAccount> _result = new List<MsAccount>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query =
                            (
                                from _account in this.db.MsAccounts
                                where _account.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                                    && (SqlMethods.Like(_account.Account.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_account.AccountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _account.Account ascending
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = "[" + _account.Account + "] " + _account.AccountName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListForReport(int _prmReqPage, int _prmPageSize)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _account in this.db.MsAccounts
                                where _account.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                                orderby _account.Account ascending
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = "[" + _account.Account + "] " + _account.AccountName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListFilterAccount(string _prmCurrCode, int _prmReqPage, int _prmPageSize)
        {
            List<MsAccount> _result = new List<MsAccount>();

            string _pattern1 = "%%";

            if (_prmCurrCode != "")
            {
                _pattern1 = "%" + _prmCurrCode + "%";
            }

            try
            {
                var _query = (
                                from _account in this.db.MsAccounts
                                where _account.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                                    && (SqlMethods.Like(_account.CurrCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _account.Account ascending
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = "[" + _account.Account + "] " + _account.AccountName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListFilterAccount(string _prmCurrCode, int _prmReqPage, int _prmPageSize, String _prmCategory, String _prmKeyword)
        {
            List<MsAccount> _result = new List<MsAccount>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCurrCode != "")
            {
                _pattern1 = "%" + _prmCurrCode + "%";
            }

            if (_prmCategory == "Code")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _account in this.db.MsAccounts
                                where _account.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                                    && (SqlMethods.Like(_account.CurrCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_account.Account.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_account.AccountName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                orderby _account.Account ascending
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = "[" + _account.Account + "] " + _account.AccountName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListPaymentTypeForReport(int _prmReqPage, int _prmPageSize)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _msPayType in this.db.MsPayTypes
                                join _msAccount in this.db.MsAccounts
                                    on _msPayType.Account equals _msAccount.Account
                                orderby _msPayType.Account ascending
                                select new
                                {
                                    Account = _msPayType.Account,
                                    AccountName = _msPayType.PayName + " [" + _msPayType.Account + "-" + _msAccount.AccountName + "]"
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListPettyTypeForReport(int _prmReqPage, int _prmPageSize)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _msPetty in this.db.MsPetties
                                join _msAccount in this.db.MsAccounts
                                    on _msPetty.Account equals _msAccount.Account
                                orderby _msPetty.Account ascending
                                select new
                                {
                                    Account = _msPetty.Account,
                                    AccountName = _msPetty.PettyName + " [" + _msPetty.Account + "-" + _msAccount.AccountName + "]"
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListForReport()
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _account in this.db.MsAccounts
                                where _account.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                                orderby _account.Account ascending
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = "[" + _account.Account + "] " + _account.AccountName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MsAccount GetViewMsAccount(string _prmAccount)
        {
            MsAccount _result = new MsAccount();

            try
            {
                var _query = (
                                from _msAccount in this.db.MsAccounts
                                where _msAccount.Account == _prmAccount
                                select new
                                {
                                    AccClass = _msAccount.AccClass,
                                    BranchAccCode = _msAccount.BranchAccCode,
                                    AccClassName = (
                                                    from _msAccClass in this.db.MsAccClasses
                                                    where _msAccClass.AccClassCode == _msAccount.AccClass
                                                    select _msAccClass.AccClassName
                                                  ).FirstOrDefault(),
                                    Detail = _msAccount.Detail,
                                    CurrCode = _msAccount.CurrCode,
                                    FgNormal = _msAccount.FgNormal,
                                    FgSubLed = _msAccount.FgSubLed,
                                    FgActive = _msAccount.FgActive,
                                    Desc = _msAccount.AccountName
                                }
                            );
                foreach (var _row in _query)
                {
                    _result.AccClass = _row.AccClass;
                    _result.BranchAccCode = _row.BranchAccCode;
                    _result.AccClassName = _row.AccClassName;
                    _result.Detail = _row.Detail;
                    _result.CurrCode = _row.CurrCode;
                    _result.FgNormal = _row.FgNormal;
                    _result.FgSubLed = _row.FgSubLed;
                    _result.FgActive = _row.FgActive;
                    _result.AccountName = _row.Desc;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAccountNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msAccount in this.db.MsAccounts
                                where _msAccount.Account == _prmCode
                                select new
                                {
                                    AccountName = _msAccount.AccountName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.AccountName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCurrByAccCode(string _prmAccCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msAccount in this.db.MsAccounts
                                where _msAccount.Account == _prmAccCode
                                select new
                                {
                                    CurrCode = _msAccount.CurrCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CurrCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAccountNameByCode(string _prmCode, string _prmCurrCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msAccount in this.db.MsAccounts
                                where _msAccount.Account == _prmCode && _msAccount.CurrCode.Trim().ToLower() == _prmCurrCode.Trim().ToLower()
                                select new
                                {
                                    AccountName = _msAccount.AccountName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.AccountName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public char GetAccountSubLed(string _prmAccount)
        {
            char _result = ' ';

            try
            {
                var _query =
                            (
                                from _msAccount in this.db.MsAccounts
                                where _msAccount.Account == _prmAccount
                                select new
                                {
                                    FgSubLed = _msAccount.FgSubLed
                                }
                            );

                foreach (var _obj in _query)
                {
                    _result = _obj.FgSubLed;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListByTypeForDDL(string _prmCode)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _msAccount in this.db.MsAccounts
                                join _msTransType_MsAccount in this.db.MsTransType_MsAccounts
                                    on _msAccount.Account equals _msTransType_MsAccount.Account
                                orderby _msAccount.AccountName ascending
                                where _msTransType_MsAccount.TransTypeCode.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    Account = _msAccount.Account,
                                    AccountName = _msAccount.AccountName
                                }
                            ).OrderBy(_temp => _temp.AccountName);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListByTransType(FINPettyHd _prmPettyHd, string _prmTransType)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _msAccount in this.db.V_MsAccountDts
                                where _msAccount.Currency == _prmPettyHd.CurrCode && _msAccount.TransType == _prmTransType
                                //&& !(
                                //           from _pettyHD in this.db.FINPettyDts
                                //           where _pettyHD.TransNmbr == _prmPettyHd.TransNmbr
                                //           select _pettyHD.Account
                                //       ).Contains(_msAccount.Account)
                                select new
                                {
                                    Account = _msAccount.Account,
                                    AccountName = _msAccount.Description
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListByTransTypeReceive(FINPettyReceiveHd _prmPettyReceiveHd, string _prmTransType)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _msAccount in this.db.V_MsAccountDts
                                where _msAccount.Currency == _prmPettyReceiveHd.CurrCode && _msAccount.TransType == _prmTransType
                                //&& !(
                                //           from _pettyHD in this.db.FINPettyDts
                                //           where _pettyHD.TransNmbr == _prmPettyHd.TransNmbr
                                //           select _pettyHD.Account
                                //       ).Contains(_msAccount.Account)
                                select new
                                {
                                    Account = _msAccount.Account,
                                    AccountName = _msAccount.Description
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public List<MsAccount> GetListForGLBudget(string _prmTransType)
        //{
        //    List<MsAccount> _result = new List<MsAccount>();

        //    try
        //    {
        //        var _query =
        //                    (
        //                        from _msAccount in this.db.V_MsAccountDts
        //                        where _msAccount.FgType == _prmTransType
        //                        select new
        //                        {
        //                            Account = _msAccount.Account,
        //                            AccountName = _msAccount.Description
        //                        }
        //                    ).Distinct();

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

        //            _result.Add(new MsAccount(_row.Account, _row.AccountName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public string GetListCurrCodeForGLBudget(string _prmAccCode)
        {
            string _result = "";

            try
            {
                var _query =
                            (
                                from _msAccount in this.db.MsAccounts
                                where _msAccount.Account == _prmAccCode
                                select new
                                {
                                    CurrCode = _msAccount.CurrCode
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


        //public List<MsAccount> GetListBySP(FINPettyHd _prmPettyHd, String _prmAccount)
        //{
        //    List<MsAccount> _result = new List<MsAccount>();

        //    try
        //    {
        //        var _query =
        //                    (
        //                        from _msAccount in this.db.V_MsAccountDts
        //                        where _msAccount.Currency == _prmPettyHd.CurrCode
        //                        //&& !(
        //                        //           from _pettyHD in this.db.FINPettyDts
        //                        //           where _pettyHD.TransNmbr == _prmPettyHd.TransNmbr && _pettyHD.Account != _prmAccount
        //                        //           select _pettyHD.Account
        //                        //       ).Contains(_msAccount.Account)
        //                        select new
        //                        {
        //                            Account = _msAccount.Account,
        //                            AccountName = _msAccount.Description
        //                        }
        //                    );

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

        //            _result.Add(new MsAccount(_row.Account, _row.AccountName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public List<MsAccount> GetListDDLAccByTransType(string _prmTransType)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _msAccount in this.db.V_MsAccountDts
                                where _msAccount.TransType == _prmTransType
                                orderby _msAccount.Description ascending
                                select new
                                {
                                    Account = _msAccount.Account,
                                    AccountName = _msAccount.Description
                                }
                            ).OrderBy(_temp => _temp.AccountName);

                foreach (var _row in _query)
                {
                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListDDLAccForStockType()
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _msAccount in this.db.V_MsAccountDts
                                where _msAccount.FgType == AccountMapper.GetValue(AccountType.ProfitLost)
                                orderby _msAccount.Description ascending
                                select new
                                {
                                    Account = _msAccount.Account,
                                    AccountName = _msAccount.Description
                                }
                            ).OrderBy(_temp => _temp.AccountName).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsAccount> GetListPattern(int _prmReqPage, int _prmPageSize, string patern, string id)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from account in this.db.MsAccounts
                                where SqlMethods.Like(account.AccountName, patern)
                                    && !(
                                            from transtype in this.db.MsTransType_MsAccounts
                                            where transtype.TransTypeCode == id
                                            select transtype.Account
                                        ).Contains(account.Account)
                                select new
                                {
                                    Account = account.Account,
                                    AccountName = account.Account + " - " + account.AccountName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result.Add(new MsAccount(_row.Account, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsExist(string _prmAccount)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _msAccount in this.db.MsAccounts
                                where _msAccount.Account.Trim().ToLower() == _prmAccount.Trim().ToLower()
                                select _msAccount.Account
                             ).Count();

                if (_query > 0)
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

        public bool IsExistAccount(string _prmCode, string _prmAccount)
        {
            bool _result = false;

            try
            {
                var _query =
                            (
                                from _msAccount in this.db.MsAccounts
                                join _msTransType_MsAccount in this.db.MsTransType_MsAccounts
                                    on _msAccount.Account equals _msTransType_MsAccount.Account
                                orderby _msAccount.AccountName ascending
                                where _msTransType_MsAccount.TransTypeCode.Trim().ToLower() == _prmCode.Trim().ToLower() && _msAccount.Account.Trim().ToLower() == _prmAccount.Trim().ToLower()
                                select new
                                {
                                    Account = _msAccount.Account,
                                    AccountName = _msAccount.AccountName
                                }
                            ).OrderBy(_temp => _temp.AccountName);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsExistAccountByTransType(string _prmCode, string _prmAccount)
        {
            bool _result = false;

            try
            {
                var _query =
                            (
                                from _msAccount in this.db.V_MsAccountDts
                                where _msAccount.TransType.Trim().ToLower() == _prmCode.Trim().ToLower() && _msAccount.Account.Trim().ToLower() == _prmAccount.Trim().ToLower()
                                select new
                                {
                                    Account = _msAccount.Account
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string });

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region EmployeeAuthorization
        public int RowsCountEmployeeAuthorization(string _prmCategory, string _prmKeyword)
        {
            int _result = 0;

            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }

            _result =
                           (
                               (
                                   from _employeeAuthorization in this.db.Master_EmployeeAuthorizations
                                   join _msEmployee in this.db.MsEmployees
                                        on _employeeAuthorization.EmpNumb equals _msEmployee.EmpNumb
                                   where (SqlMethods.Like(_employeeAuthorization.EmpNumb.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                        && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   select new
                                   {
                                       EmpNumb = _employeeAuthorization.EmpNumb
                                   }
                               ).Distinct()
                           ).Count();

            return _result;
        }

        public int RowsCountEmployeeAuthorizationView(string _prmEmpNumb, string _prmTransType)
        {
            int _result = 0;

            string _pattern1 = "%%";

            if (_prmTransType != "null")
            {
                _pattern1 = "%" + _prmTransType + "%";
            }

            _result =
                           (
                                   from _employeeAuthorization in this.db.Master_EmployeeAuthorizations
                                   join _msEmployee in this.db.MsEmployees
                                        on _employeeAuthorization.EmpNumb equals _msEmployee.EmpNumb
                                   where _employeeAuthorization.EmpNumb.Trim().ToLower() == _prmEmpNumb.Trim().ToLower()
                                        && (SqlMethods.Like(_employeeAuthorization.TransTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   select new
                                   {
                                       EmpNumb = _employeeAuthorization.EmpNumb
                                   }
                           ).Count();

            return _result;
        }

        public bool EditEmployeeAuthorization(string _prmEmpNumb, string[] _prmReqToDelete, string[] _prmReqToAdd)
        {
            bool _result = false;

            try
            {
                if (_prmReqToAdd[0] != "")
                {
                    for (int i = 0; i < _prmReqToAdd.Length; i++)
                    {
                        Master_EmployeeAuthorization _msEmpAuthor = new Master_EmployeeAuthorization();

                        string[] _tempReqToAdd = new string[2];

                        _tempReqToAdd = _prmReqToAdd[i].Split('-');

                        _msEmpAuthor.EmployeeAuthCode = Guid.NewGuid();
                        _msEmpAuthor.EmpNumb = _prmEmpNumb;
                        _msEmpAuthor.TransTypeCode = _tempReqToAdd[0];
                        _msEmpAuthor.Account = _tempReqToAdd[1];
                        _msEmpAuthor.InsertBy = HttpContext.Current.User.Identity.Name;
                        _msEmpAuthor.InsertDate = DateTime.Now;
                        _msEmpAuthor.EditBy = HttpContext.Current.User.Identity.Name;
                        _msEmpAuthor.EditDate = DateTime.Now;

                        this.db.Master_EmployeeAuthorizations.InsertOnSubmit(_msEmpAuthor);
                    }
                }

                if (_prmReqToDelete[0] != "")
                {
                    for (int i = 0; i < _prmReqToDelete.Length; i++)
                    {
                        string[] _tempReqToDelete = new string[2];

                        _tempReqToDelete = _prmReqToDelete[i].Split('-');

                        var _query = from _empAuth in this.db.Master_EmployeeAuthorizations
                                     where _empAuth.EmpNumb.Trim().ToLower() == _prmEmpNumb.Trim().ToLower()
                                         && _empAuth.TransTypeCode.Trim().ToLower() == _tempReqToDelete[0].Trim().ToLower()
                                         && _empAuth.Account.Trim().ToLower() == _tempReqToDelete[1].Trim().ToLower()
                                     select _empAuth;

                        this.db.Master_EmployeeAuthorizations.DeleteAllOnSubmit(_query);
                    }
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

        public bool AddEmployeeAuthorization(string _prmEmpNumb, string[] _prmAccount)
        {
            bool _result = false;
            string[] _tempSplit = new string[2];

            try
            {
                for (int i = 0; i < _prmAccount.Length; i++)
                {
                    Master_EmployeeAuthorization _msEmpAuthor = new Master_EmployeeAuthorization();

                    _tempSplit = _prmAccount[i].Split('-');

                    _msEmpAuthor.EmployeeAuthCode = Guid.NewGuid();
                    _msEmpAuthor.EmpNumb = _prmEmpNumb;
                    _msEmpAuthor.TransTypeCode = _tempSplit[0];
                    _msEmpAuthor.Account = _tempSplit[1];
                    _msEmpAuthor.InsertBy = HttpContext.Current.User.Identity.Name;
                    _msEmpAuthor.InsertDate = DateTime.Now;
                    _msEmpAuthor.EditBy = HttpContext.Current.User.Identity.Name;
                    _msEmpAuthor.EditDate = DateTime.Now;

                    this.db.Master_EmployeeAuthorizations.InsertOnSubmit(_msEmpAuthor);
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

        public bool AddList(List<MsTransType_MsAccount> _prmList, string _prmEmpNumb)
        {
            bool _result = false;

            try
            {
                foreach (MsTransType_MsAccount _item in _prmList)
                {
                    Master_EmployeeAuthorization _msEmpAuthor = new Master_EmployeeAuthorization();

                    _msEmpAuthor.EmployeeAuthCode = Guid.NewGuid();
                    _msEmpAuthor.EmpNumb = _prmEmpNumb;
                    _msEmpAuthor.TransTypeCode = _item.TransTypeCode;
                    _msEmpAuthor.Account = _item.Account;
                    _msEmpAuthor.InsertBy = HttpContext.Current.User.Identity.Name;
                    _msEmpAuthor.InsertDate = DateTime.Now;
                    _msEmpAuthor.EditBy = HttpContext.Current.User.Identity.Name;
                    _msEmpAuthor.EditDate = DateTime.Now;

                    this.db.Master_EmployeeAuthorizations.InsertOnSubmit(_msEmpAuthor);
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

        public bool DeleteMultiEmployeeAuthorization(string[] _prmEmpNumb)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmEmpNumb.Length; i++)
                {
                    var _query = (
                                    from _masterEmployeeAuthorization in this.db.Master_EmployeeAuthorizations
                                    where _masterEmployeeAuthorization.EmpNumb == _prmEmpNumb[i]
                                    select _masterEmployeeAuthorization
                                );

                    this.db.Master_EmployeeAuthorizations.DeleteAllOnSubmit(_query);
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

        public List<Master_EmployeeAuthorization> GetListEmpAuthTransTypeAccount(int _prmReqPage, int _prmPageSize, string _prmEmpNumb, string _prmTransType)
        {
            List<Master_EmployeeAuthorization> _result = new List<Master_EmployeeAuthorization>();

            string _pattern1 = "%%";

            if (_prmTransType != "null")
            {
                _pattern1 = "%" + _prmTransType + "%";
            }

            var query =
                        (
                                 from _msEmpAuthor in this.db.Master_EmployeeAuthorizations
                                 join transType in this.db.MsTransTypes
                                     on _msEmpAuthor.TransTypeCode equals transType.TransTypeCode
                                 join account in this.db.MsAccounts
                                     on _msEmpAuthor.Account equals account.Account
                                 where _msEmpAuthor.EmpNumb == _prmEmpNumb
                                    && (SqlMethods.Like(transType.TransTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 select new
                                 {
                                     TransType = _msEmpAuthor.TransTypeCode,
                                     TransTypeName = transType.TransTypeName,
                                     Account = account.Account,
                                     AccountName = account.AccountName
                                 }

                        ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

            foreach (object _obj in query)
            {
                var _row = _obj.Template(new { TransType = this._string, TransTypeName = this._string, Account = this._string, AccountName = this._string });

                _result.Add(new Master_EmployeeAuthorization(_row.TransType, _row.TransTypeName, _row.Account, _row.AccountName));
            }

            return _result;
        }

        public string GetTransTypeAccount(string _prmEmpNumb)
        {
            string _result = "";

            var query =
                        (
                                 from _msEmpAuthor in this.db.Master_EmployeeAuthorizations
                                 where _msEmpAuthor.EmpNumb == _prmEmpNumb
                                 select new
                                 {
                                     Text = (_msEmpAuthor.TransTypeCode + "-" + _msEmpAuthor.Account)
                                 }
                        );

            foreach (var _var in query)
            {
                if (_result == "")
                {
                    _result = _var.Text;
                }
                else
                {
                    _result = _result + "," + _var.Text;
                }
            }

            return _result;
        }

        public Boolean IsAccountAuthorized(String _prmEmpNumb, String _prmTransType, String _prmAccount)
        {
            bool _result = false;

            try
            {
                var _query = from _empAuth in this.db.Master_EmployeeAuthorizations
                             where _empAuth.EmpNumb == _prmEmpNumb
                                && _empAuth.TransTypeCode == _prmTransType
                                && _empAuth.Account == _prmAccount
                             select new
                             {
                                 _empAuth.EmpNumb
                             };

                foreach (var _item in _query)
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

        public bool IsEmployeeExist(string _prmEmpNumb)
        {
            bool _result = false;
            int _row = 0;

            try
            {
                _row = this.db.Master_EmployeeAuthorizations.Where(_temp => _temp.EmpNumb == _prmEmpNumb).Count();

                if (_row > 0)
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

        public List<Master_EmployeeAuthorization> GetListEmployeeAuthorization(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_EmployeeAuthorization> _result = new List<Master_EmployeeAuthorization>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                (
                                    from _employeeAuthorization in this.db.Master_EmployeeAuthorizations
                                    join _msEmployee in this.db.MsEmployees
                                         on _employeeAuthorization.EmpNumb equals _msEmployee.EmpNumb
                                    where (SqlMethods.Like(_employeeAuthorization.EmpNumb.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                         && (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    select new
                                    {
                                        EmpNumb = _employeeAuthorization.EmpNumb,
                                        EmpName = _msEmployee.EmpName
                                    }
                                ).Distinct()
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { EmpNumb = this._string, EmpName = this._string });

                    _result.Add(new Master_EmployeeAuthorization(_row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_EmployeeAuthorization> GetListAccountByTransType(string _prmCurr, string _prmEmpNumb, string _prmTransType)
        {
            List<Master_EmployeeAuthorization> _result = new List<Master_EmployeeAuthorization>();

            try
            {
                var _query =
                            (
                                from _msEmpAuthor in this.db.Master_EmployeeAuthorizations
                                join _msAccount in this.db.MsAccounts
                                    on _msEmpAuthor.Account equals _msAccount.Account
                                where _msEmpAuthor.EmpNumb == _prmEmpNumb
                                    && _msEmpAuthor.TransTypeCode == _prmTransType
                                    && _msAccount.CurrCode == _prmCurr
                                orderby _msAccount.AccountName ascending
                                select new
                                {
                                    Account = _msEmpAuthor.Account,
                                    AccountName = _msAccount.AccountName
                                }
                            ).Distinct();

                if (_query.Count() == 0)
                {
                    TransTypeBL _transType = new TransTypeBL();
                    List<MsTransType_MsAccount> _transTypeAcc = new List<MsTransType_MsAccount>();

                    _transTypeAcc = _transType.GetListAccountByTransTypeCode(_prmTransType);

                    foreach (MsTransType_MsAccount _item in _transTypeAcc)
                    {
                        Master_EmployeeAuthorization _empAuth = new Master_EmployeeAuthorization();

                        _empAuth.Account = _item.Account;
                        _empAuth.AccountName = _item.AccountName;

                        _result.Add(_empAuth);
                    }
                }
                else
                {
                    foreach (object _obj in _query)
                    {
                        var _row = _obj.Template(new { Account = this._string, AccountName = this._string });

                        Master_EmployeeAuthorization _empAuth = new Master_EmployeeAuthorization();

                        _empAuth.Account = _row.Account;
                        _empAuth.AccountName = _row.AccountName;

                        _result.Add(_empAuth);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            _result.Sort(new Comparison<Master_EmployeeAuthorization>(this.SortByAccountName));

            return _result;
        }

        private int SortByAccountName(Master_EmployeeAuthorization _prmAccName1, Master_EmployeeAuthorization _prmAccName2)
        {
            return _prmAccName1.AccountName.CompareTo(_prmAccName2.AccountName);
        }

        public List<Master_EmployeeAuthorization> GetListAccountByTransType(string _prmEmpNumb, string _prmTransType)
        {
            List<Master_EmployeeAuthorization> _result = new List<Master_EmployeeAuthorization>();

            try
            {
                var _query =
                            (
                                from _msEmpAuthor in this.db.Master_EmployeeAuthorizations
                                join _msAccount in this.db.MsAccounts
                                    on _msEmpAuthor.Account equals _msAccount.Account
                                where _msEmpAuthor.EmpNumb == _prmEmpNumb
                                    && _msEmpAuthor.TransTypeCode == _prmTransType
                                orderby _msAccount.Account, _msAccount.AccountName ascending
                                select new
                                {
                                    Account = _msEmpAuthor.Account,
                                    AccountName = _msAccount.AccountName
                                }
                            ).Distinct();

                if (_query.Count() == 0)
                {
                    TransTypeBL _transType = new TransTypeBL();
                    List<MsTransType_MsAccount> _transTypeAcc = new List<MsTransType_MsAccount>();

                    _transTypeAcc = _transType.GetListAccountByTransTypeCode(_prmTransType);

                    if (_transTypeAcc != null)
                    {
                        foreach (var _row2 in _transTypeAcc)
                        {
                            Master_EmployeeAuthorization _empAuth = new Master_EmployeeAuthorization();

                            _empAuth.Account = _row2.Account;
                            _empAuth.AccountName = _row2.AccountName;


                            _result.Add(_empAuth);
                        }
                    }
                }
                else
                {
                    foreach (var _row in _query)
                    {
                        Master_EmployeeAuthorization _empAuth = new Master_EmployeeAuthorization();

                        _empAuth.Account = _row.Account;
                        _empAuth.AccountName = _row.AccountName;

                        _result.Add(_empAuth);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            _result.Sort(new Comparison<Master_EmployeeAuthorization>(this.SortByAccountName));

            return _result;
        }

        #endregion

        #region Branch Account

        public double RowsCountBranchAccount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _msBranchAccount in this.db.Master_BranchAccounts
                            where (SqlMethods.Like(_msBranchAccount.BranchAccID.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_msBranchAccount.BranchAccName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msBranchAccount.BranchAccCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool EditBranchAccount(Master_BranchAccount _prmMsBranchAccount)
        {
            bool _result = false;

            try
            {
                if (this.IsBranchAccountIDExists(_prmMsBranchAccount.BranchAccID, _prmMsBranchAccount.BranchAccCode) == false)
                {
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        private bool IsBranchAccountIDExists(string _prmBranchAccountID, Guid _prmBranchAccountCode)
        {
            bool _result = false;

            try
            {
                var _query = from _branchAccount in this.db.Master_BranchAccounts
                             where (_branchAccount.BranchAccID == _prmBranchAccountID) && (_branchAccount.BranchAccCode != _prmBranchAccountCode)
                             select new
                             {
                                 _branchAccount.BranchAccID
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

        public bool AddBranchAccount(Master_BranchAccount _prmMsBranchAccount)
        {
            bool _result = false;

            try
            {
                if (this.IsBranchAccountIDExists(_prmMsBranchAccount.BranchAccID, _prmMsBranchAccount.BranchAccCode) == false)
                {
                    this.db.Master_BranchAccounts.InsertOnSubmit(_prmMsBranchAccount);

                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiBranchAccount(string[] _prmBranchAccount)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmBranchAccount.Length; i++)
                {
                    Master_BranchAccount _msBranchAccount = this.db.Master_BranchAccounts.Single(_temp => _temp.BranchAccCode == new Guid(_prmBranchAccount[i]));

                    this.db.Master_BranchAccounts.DeleteOnSubmit(_msBranchAccount);
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

        public Master_BranchAccount GetSingleBranchAccount(string _prmBranchAccount)
        {
            Master_BranchAccount _result = null;

            try
            {
                _result = this.db.Master_BranchAccounts.Single(_temp => _temp.BranchAccCode == new Guid(_prmBranchAccount));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_BranchAccount> GetListBranchAccount(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_BranchAccount> _result = new List<Master_BranchAccount>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query =
                            (
                                from _branchAccount in this.db.Master_BranchAccounts
                                where (SqlMethods.Like(_branchAccount.BranchAccID.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_branchAccount.BranchAccName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _branchAccount.BranchAccID ascending
                                select new
                                {
                                    BranchAccCode = _branchAccount.BranchAccCode,
                                    BranchAccID = _branchAccount.BranchAccID,
                                    BranchAccName = _branchAccount.BranchAccName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_BranchAccount(_row.BranchAccCode, _row.BranchAccID, _row.BranchAccName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_BranchAccount> GetListBranchAccountForDDL()
        {
            List<Master_BranchAccount> _result = new List<Master_BranchAccount>();

            try
            {
                var _query =
                            (
                                from _branchAccount in this.db.Master_BranchAccounts
                                orderby _branchAccount.BranchAccName ascending
                                select new
                                {
                                    BranchAccCode = _branchAccount.BranchAccCode,
                                    BranchAccID = _branchAccount.BranchAccID,
                                    BranchAccName = _branchAccount.BranchAccID + " - " + _branchAccount.BranchAccName
                                }
                            ).OrderBy(_temp => _temp.BranchAccID);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_BranchAccount(_row.BranchAccCode, _row.BranchAccID, _row.BranchAccName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetBranchAccountNameByCode(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msBranchAccount in this.db.Master_BranchAccounts
                                where _msBranchAccount.BranchAccCode == _prmCode
                                select new
                                {
                                    BranchAccName = _msBranchAccount.BranchAccName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.BranchAccName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetBranchAccountIDByCode(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msBranchAccount in this.db.Master_BranchAccounts
                                where _msBranchAccount.BranchAccCode == _prmCode
                                select new
                                {
                                    BranchAccID = _msBranchAccount.BranchAccID
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.BranchAccID;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Guid GetBranchAccountCodeByID(String _prmBranchAccID)
        {
            Guid _result = new Guid();

            try
            {
                var _query = (
                                from _msBranchAccount in this.db.Master_BranchAccounts
                                where _msBranchAccount.BranchAccID == _prmBranchAccID
                                select new
                                {
                                    BranchAccCode = _msBranchAccount.BranchAccCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.BranchAccCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsBranchAccountExists(String _prmBranchAccount)
        {
            bool _result = false;

            try
            {
                var _query = from _msBranchAccount in this.db.Master_BranchAccounts
                             where _msBranchAccount.BranchAccID == _prmBranchAccount
                             select new
                             {
                                 _msBranchAccount.BranchAccCode
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
        #endregion

        ~AccountBL()
        {
        }

    }
}
