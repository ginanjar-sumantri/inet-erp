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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class CashFlowFileBL : Base
    {

        public CashFlowFileBL()
        {

        }

        public List<MsAccount> GetListMsAccGroupDt(int _prmReqPage, int _prmPageSize)
        {
            List<MsAccount> _result = new List<MsAccount>();

            var query =
                        (
                                 from account in this.db.MsAccGroupDts
                                 select new
                                 {
                                     Account = account.Account,
                                     AccountName = (
                                                    from _msAcc in this.db.MsAccounts
                                                    where account.Account == _msAcc.Account
                                                    select _msAcc.AccountName
                                                    ).FirstOrDefault()
                                 }
                        ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

            foreach (var _row in query)
            {
                _result.Add(new MsAccount(_row.Account, _row.AccountName));
            }

            return _result;
        }

        public List<MsAccount> GetListMsAccGroupDt()
        {
            List<MsAccount> _result = new List<MsAccount>();

            var query =
                        (
                                 from account in this.db.MsAccGroupDts
                                 select new
                                 {
                                     Account = account.Account,
                                     AccountName = (
                                                    from _msAcc in this.db.MsAccounts
                                                    where account.Account == _msAcc.Account
                                                    select _msAcc.AccountName
                                                    ).FirstOrDefault()
                                 }
                        );

            foreach (var _row in query)
            {
                _result.Add(new MsAccount(_row.Account, _row.AccountName));
            }

            return _result;
        }

        public List<MsAccount> GetListMsAccGroupDt(int _prmReqPage, int _prmPageSize, String _prmCategory, String _prmKeyword)
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
                                 from _msAccGroupDt in this.db.MsAccGroupDts
                                 join _msAccount in this.db.MsAccounts
                                        on _msAccGroupDt.Account equals _msAccount.Account
                                 where _msAccount.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                                    && (SqlMethods.Like(_msAccGroupDt.Account.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msAccount.AccountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                 select new
                                 {
                                     Account = _msAccGroupDt.Account,
                                     AccountName = "[" + _msAccGroupDt.Account + "] " + _msAccount.AccountName
                                 }
                        ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

            foreach (var _row in query)
            {
                _result.Add(new MsAccount(_row.Account, _row.AccountName));
            }

            return _result;
        }

        public int RowsCountMsAccGroupDt()
        {
            int _result = 0;

            try
            {
                var _query = (
                                     from msAccGroupDt in this.db.MsAccGroupDts
                                     select new
                                     {
                                         Account = msAccGroupDt.Account
                                     }
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int RowsCountMsAccGroupDt(String _prmCategory, String _prmKeyword)
        {
            int _result = 0;

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
                                     from _msAccGroupDt in this.db.MsAccGroupDts
                                     join _msAccount in this.db.MsAccounts
                                        on _msAccGroupDt.Account equals _msAccount.Account
                                     where _msAccount.FgActive == AccountMapper.GetYesNo(YesNo.Yes)
                                        && (SqlMethods.Like(_msAccGroupDt.Account.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msAccount.AccountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                     select _msAccGroupDt.Account
                            ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmMsAccount)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmMsAccount.Length; i++)
                {
                    MsAccGroupDt _msAccGroupDt = this.db.MsAccGroupDts.Single(_accountGroupDt => (_accountGroupDt.Account == _prmMsAccount[i]));

                    this.db.MsAccGroupDts.DeleteOnSubmit(_msAccGroupDt);
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

        public List<MsAccount> GetListNotAssign(int _prmReqPage, int _prmPageSize)
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from _account in this.db.MsAccounts
                                where !(
                                            from _accGroupDt in this.db.MsAccGroupDts
                                            select _accGroupDt.Account
                                        ).Contains(_account.Account)
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

        public List<MsAccount> GetListNotAssign()
        {
            List<MsAccount> _result = new List<MsAccount>();

            try
            {
                var _query =
                            (
                                from account in this.db.MsAccounts
                                where !(
                                            from _accGroupDt in this.db.MsAccGroupDts
                                            select _accGroupDt.Account
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

        public int RowsCountNotAssign()
        {

            var _query =
                        (
                            from account in this.db.MsAccounts
                            where !(
                                        from _accGroupDt in this.db.MsAccGroupDts
                                        select _accGroupDt.Account
                                    ).Contains(account.Account)
                            select new
                            {
                                Account = account.Account
                            }
                        ).Count();



            return _query;
        }

        public bool AddTransTypeAccount(string label)
        {
            bool _result = false;
            List<MsAccGroupDt> _msTrans_msAccount = new List<MsAccGroupDt>();
            List<MsAccount> _msAccount = new List<MsAccount>();
            AccountBL _account = new AccountBL();
            MsAccGroupDt _gabung = new MsAccGroupDt();

            try
            {

                _msAccount = this.GetListNotAssign();
                foreach (var item in _msAccount)
                {
                    _gabung = new MsAccGroupDt();
                    _gabung.GroupType = label;
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

        public bool Add(List<MsAccGroupDt> _prmAccGroupDt)
        {
            bool _result = false;
            try
            {
                foreach (MsAccGroupDt item in _prmAccGroupDt)
                {
                    this.db.MsAccGroupDts.InsertOnSubmit(item);
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
                    MsAccGroupDt _gabung = new MsAccGroupDt();
                    _gabung.GroupType = id;
                    _gabung.Account = _tempsplit[i];
                    this.db.MsAccGroupDts.InsertOnSubmit(_gabung);
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

        ~CashFlowFileBL()
        {

        }
    }
}
