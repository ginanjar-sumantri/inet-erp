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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class TransTypeBL : Base
    {
        public TransTypeBL()
        {
        }

        #region TransType

        public double RowsCount(string _prmCategory, string _prmKeyword)
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
                            from _msTransType in this.db.MsTransTypes
                            where (SqlMethods.Like(_msTransType.TransTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msTransType.TransTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msTransType.TransTypeCode
                        ).Count();

            _result = _query;

            return _result;
        }


        public bool Edit(MsTransType _prmMsTransType)
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


        public bool AddTransTypeAccount(string label)
        {
            bool _result = false;
            List<MsTransType_MsAccount> _msTrans_msAccount = new List<MsTransType_MsAccount>();
            List<MsAccount> _msAccount = new List<MsAccount>();
            AccountBL _account = new AccountBL();
            MsTransType_MsAccount _gabung = new MsTransType_MsAccount();

            try
            {

                _msAccount = this.GetListNotAssign(label);
                foreach (var item in _msAccount)
                {
                    _gabung = new MsTransType_MsAccount();
                    _gabung.TransTypeCode = label;
                    _gabung.Account = item.Account;

                    _msTrans_msAccount.Add(_gabung);
                }

                Add(_msTrans_msAccount);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;

        }


        public bool Add(List<MsTransType_MsAccount> _prmMsTrans_MsAccount)
        {
            bool _result = false;
            try
            {
                foreach (MsTransType_MsAccount item in _prmMsTrans_MsAccount)
                {
                    this.db.MsTransType_MsAccounts.InsertOnSubmit(item);
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

        public bool Add(string _temp, string id)
        {
            bool _result = false;
            int i = 0;
            string[] _tempsplit = _temp.Split(',');


            try
            {
                for (i = 0; i < _tempsplit.Length; i++)
                {
                    MsTransType_MsAccount _gabung = new MsTransType_MsAccount();
                    _gabung.TransTypeCode = id;
                    _gabung.Account = _tempsplit[i];
                    this.db.MsTransType_MsAccounts.InsertOnSubmit(_gabung);
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

        public bool Add(MsTransType _prmMsTransType)
        {
            bool _result = false;

            try
            {

                this.db.MsTransTypes.InsertOnSubmit(_prmMsTransType);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmMsTransType)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmMsTransType.Length; i++)
                {
                    MsTransType _msTransType = this.db.MsTransTypes.Single(_transType => _transType.TransTypeCode == _prmMsTransType[i]);

                    //var _query = (from _detail in this.db.MsTransType_MsAccounts
                    //              where _detail.TransTypeCode.Trim().ToLower() == _prmMsTransType[i].Trim().ToLower()
                    //              select _detail);

                    //this.db.MsTransType_MsAccounts.DeleteAllOnSubmit(_query);

                    this.db.MsTransTypes.DeleteOnSubmit(_msTransType);
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

        public MsTransType GetSingle(string _prmMsTransType)
        {
            MsTransType _result = null;

            try
            {
                _result = this.db.MsTransTypes.Single(_transType => _transType.TransTypeCode == _prmMsTransType);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsTransType> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsTransType> _result = new List<MsTransType>();

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
                                from _transType in this.db.MsTransTypes
                                where (SqlMethods.Like(_transType.TransTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_transType.TransTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    TransTypeCode = _transType.TransTypeCode,
                                    TransTypeName = _transType.TransTypeName,
                                    FgAccount = _transType.FgAccount
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { TransTypeCode = this._string, TransTypeName = this._string, FgAccount = this._nullableChar });

                    _result.Add(new MsTransType(_row.TransTypeCode, _row.TransTypeName, _row.FgAccount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsTransType> GetList()
        {
            List<MsTransType> _result = new List<MsTransType>();

            try
            {
                var _query =
                            (
                                from _transType in this.db.MsTransTypes
                                select new
                                {
                                    TransTypeCode = _transType.TransTypeCode,
                                    TransTypeName = _transType.TransTypeName,
                                    FgAccount = _transType.FgAccount
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { TransTypeCode = this._string, TransTypeName = this._string, FgAccount = this._nullableChar });

                    _result.Add(new MsTransType(_row.TransTypeCode, _row.TransTypeName, _row.FgAccount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsTransType> GetListTransTypeForDDL()
        {
            List<MsTransType> _result = new List<MsTransType>();

            try
            {
                var _query = (
                                from _msTransType in this.db.MsTransTypes
                                select new
                                {
                                    TransTypeCode = _msTransType.TransTypeCode,
                                    TransTypeName = _msTransType.TransTypeName
                                }
                              );

                foreach (var _row in _query)
                {
                    _result.Add(new MsTransType(_row.TransTypeCode, _row.TransTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetTransTypeNameByCode(string _prmTransTypeCode)
        {
            string _result = "";

            var _query = (from _msTransType in this.db.MsTransTypes
                          where _msTransType.TransTypeCode == _prmTransTypeCode
                          select new
                          {
                              TransTypeName = _msTransType.TransTypeName
                          }
                          );
            foreach (var _obj in _query)
            {
                _result = _obj.TransTypeName;
            }

            return _result;
        }
        #endregion

        #region Author

        public bool DeleteAuthor(string[] _prmMsAccount, string _prmID)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmMsAccount.Length; i++)
                {
                    MsTransType_MsAccount _msTransType_msAccount = this.db.MsTransType_MsAccounts.Single(_author => (_author.Account == _prmMsAccount[i]) && (_author.TransTypeCode == _prmID));

                    this.db.MsTransType_MsAccounts.DeleteOnSubmit(_msTransType_msAccount);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //bikin object untuk handling error
            }

            return _result;
        }

        public List<MsAccount> GetListAuthor(int _prmReqPage, int _prmPageSize, string _prmReqId, String _prmCategory, String _prmKeyword)
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

            var query =
                        (
                                 from trans in this.db.MsTransTypes
                                 join transAccount in this.db.MsTransType_MsAccounts
                                     on trans.TransTypeCode equals transAccount.TransTypeCode
                                 join account in this.db.MsAccounts
                                     on transAccount.Account equals account.Account
                                 join currency in this.db.MsCurrencies
                                     on account.CurrCode equals currency.CurrCode
                                 join subled in this.db.MsSubleds
                                     on account.FgSubLed equals subled.SubledCode
                                 where trans.TransTypeCode == _prmReqId
                                    && (SqlMethods.Like(transAccount.Account.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(account.AccountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                 select new
                                 {
                                     Account = account.Account,
                                     AccountName = account.AccountName,
                                     FgSubled = subled.SubledCode,
                                     FgNormal = account.FgNormal,
                                     Currency = currency.CurrCode
                                 }
                        ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

            foreach (object _obj in query)
            {
                var _row = _obj.Template(new { Account = this._string, AccountName = this._string, FgSubled = this._char, FgNormal = this._char, Currency = this._string });

                _result.Add(new MsAccount(_row.Account, _row.AccountName, _row.FgSubled, _row.FgNormal, _row.Currency));
            }

            return _result;
        }

        public List<MsTransType_MsAccount> GetListTransTypeAccount(int _prmReqPage, int _prmPageSize, string _prmReqId)
        {
            List<MsTransType_MsAccount> _result = new List<MsTransType_MsAccount>();

            string _pattern1 = "%%";

            _pattern1 = "%" + _prmReqId + "%";

            var query =
                        (
                                 from transAccount in this.db.MsTransType_MsAccounts
                                 join transType in this.db.MsTransTypes
                                     on transAccount.TransTypeCode equals transType.TransTypeCode
                                 join account in this.db.MsAccounts
                                     on transAccount.Account equals account.Account
                                 where (SqlMethods.Like(transAccount.TransTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 select new
                                 {
                                     TransType = transAccount.TransTypeCode,
                                     TransTypeName = transType.TransTypeName,
                                     Account = account.Account,
                                     AccountName = account.AccountName
                                 }
                        ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

            foreach (object _obj in query)
            {
                var _row = _obj.Template(new { TransType = this._string, TransTypeName = this._string, Account = this._string, AccountName = this._string });

                _result.Add(new MsTransType_MsAccount(_row.TransType, _row.TransTypeName, _row.Account, _row.AccountName));
            }

            return _result;
        }

        public List<MsTransType_MsAccount> GetListTransTypeAccount()
        {
            List<MsTransType_MsAccount> _result = new List<MsTransType_MsAccount>();

            var query =
                        (
                                 from _transAccount in this.db.MsTransType_MsAccounts
                                 select new
                                 {
                                     TransTypeCode = _transAccount.TransTypeCode,
                                     Account = _transAccount.Account
                                 }
                        );

            foreach (var _obj in query)
            {
                _result.Add(new MsTransType_MsAccount(_obj.TransTypeCode, _obj.Account));
            }

            return _result;
        }

        public List<MsTransType_MsAccount> GetListAccountByTransTypeCode(string _prmTransTypeCode)
        {
            List<MsTransType_MsAccount> _result = new List<MsTransType_MsAccount>();

            var query =
                        (
                                 from _transAccount in this.db.MsTransType_MsAccounts
                                 join _msAcc in this.db.MsAccounts
                                    on _transAccount.Account equals _msAcc.Account
                                 where _transAccount.TransTypeCode == _prmTransTypeCode
                                 orderby _msAcc.Account, _msAcc.AccountName ascending
                                 select new
                                 {
                                     Account = _transAccount.Account,
                                     AccountName = _msAcc.AccountName
                                 }
                        ).Distinct();

            foreach (var _obj in query)
            {
                MsTransType_MsAccount _transTypeAcc = new MsTransType_MsAccount();

                _transTypeAcc.Account = _obj.Account;
                _transTypeAcc.AccountName = _obj.AccountName;

                _result.Add(_transTypeAcc);
            }

            return _result;
        }

        public int RowsCountTransTypeAccount(string _prmReqId)
        {
            string _pattern1 = "%%";

            _pattern1 = "%" + _prmReqId + "%";

            var _query = (
                                 from transAccount in this.db.MsTransType_MsAccounts
                                 join transType in this.db.MsTransTypes
                                     on transAccount.TransTypeCode equals transType.TransTypeCode
                                 join account in this.db.MsAccounts
                                     on transAccount.Account equals account.Account
                                 where (SqlMethods.Like(transAccount.TransTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 select new
                                 {
                                     Account = account.Account
                                 }
                        ).Count();

            return _query;
        }

        public int RowsCountAuthor(string _prmReqId, String _prmCategory, String _prmKeyword)
        {
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

            var _query = (
                                 from trans in this.db.MsTransTypes
                                 join transAccount in this.db.MsTransType_MsAccounts
                                     on trans.TransTypeCode equals transAccount.TransTypeCode
                                 join account in this.db.MsAccounts
                                     on transAccount.Account equals account.Account
                                 join currency in this.db.MsCurrencies
                                     on account.CurrCode equals currency.CurrCode
                                 join subled in this.db.MsSubleds
                                     on account.FgSubLed equals subled.SubledCode
                                 where transAccount.TransTypeCode == _prmReqId
                                    && (SqlMethods.Like(transAccount.Account.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(account.AccountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                 select new
                                 {
                                     Account = account.Account
                                 }
                        ).Count();

            return _query;
        }

        public List<MsAccount> GetListNotAssign(int _prmReqPage, int _prmPageSize, string _prmTransTypeCode, String _prmCategory, String _prmKeyword)
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
                                where !(
                                            from _transtype in this.db.MsTransType_MsAccounts
                                            where _transtype.TransTypeCode == _prmTransTypeCode
                                            select _transtype.Account
                                        ).Contains(_account.Account)
                                        && (SqlMethods.Like(_account.Account.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_account.AccountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    Account = _account.Account,
                                    AccountName = _account.Account + " - " + _account.AccountName
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

        public List<MsAccount> GetListNotAssign(string id)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from account in this.db.MsAccounts
                                where !(
                                            from transtype in this.db.MsTransType_MsAccounts
                                            where transtype.TransTypeCode == id
                                            select transtype.Account
                                        ).Contains(account.Account)
                                select new
                                {
                                    Account = account.Account
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { Account = this._string });

                    _result.Add(new MsAccount(_row.Account));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int RowsCountNotAssign(string id, String _prmCategory, String _prmKeyword)
        {
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
                            from account in this.db.MsAccounts
                            where !(
                                        from transtype in this.db.MsTransType_MsAccounts
                                        where transtype.TransTypeCode == id
                                        select transtype.Account
                                    ).Contains(account.Account)
                                   && (SqlMethods.Like(account.Account.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(account.AccountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select new
                            {
                                Account = account.Account
                            }
                        ).Count();

            return _query;
        }

        #endregion

        ~TransTypeBL()
        {
        }


    }
}
