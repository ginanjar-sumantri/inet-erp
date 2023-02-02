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
    public sealed class CashflowgroupBL : Base
    {

        public CashflowgroupBL()
        {
        }

        #region CashflowGroup

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
                            from _cfGroup in this.db.FINMsCashFlowGroups
                            where (SqlMethods.Like(_cfGroup.CashFlowGroupCode.Trim(), _pattern1.Trim()))
                               && (SqlMethods.Like(_cfGroup.CashFlowGroupName.Trim(), _pattern2.Trim()))
                            select _cfGroup.CashFlowGroupCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool Edit(FINMsCashFlowGroup _prmCfGroup)
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

        public bool Add(FINMsCashFlowGroup _prmCFGroup)
        {
            bool _result = false;

            try
            {

                this.db.FINMsCashFlowGroups.InsertOnSubmit(_prmCFGroup);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmCFGroupCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCFGroupCode.Length; i++)
                {
                    string[] _tempsplit = _prmCFGroupCode[i].Split('|');

                    FINMsCashFlowGroup _cfGroupSUbCode = this.db.FINMsCashFlowGroups.Single(_cfGroup => _cfGroup.CashFlowGroupCode == _tempsplit[0] && _cfGroup.CashFlowType == _tempsplit[1]);

                    this.db.FINMsCashFlowGroups.DeleteOnSubmit(_cfGroupSUbCode);
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

        public FINMsCashFlowGroup GetSingle(string _prmCfGroup, string _prmCfType)
        {
            FINMsCashFlowGroup _result = null;

            try
            {
                _result = this.db.FINMsCashFlowGroups.Single(_cfGroup => _cfGroup.CashFlowGroupCode.Trim() == _prmCfGroup.Trim() && _cfGroup.CashFlowType == _prmCfType);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCFgroupNamebyCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _cfGroup in this.db.FINMsCashFlowGroups
                                where _cfGroup.CashFlowGroupCode == _prmCode
                                select new
                                {
                                    CashFlowGroupName = _cfGroup.CashFlowGroupName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CashFlowGroupName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINMsCashFlowGroup> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINMsCashFlowGroup> _result = new List<FINMsCashFlowGroup>();

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
                                from _cfGroup in this.db.FINMsCashFlowGroups
                                where (SqlMethods.Like(_cfGroup.CashFlowGroupCode.Trim(), _pattern1.Trim()))
                                   && (SqlMethods.Like(_cfGroup.CashFlowGroupName.Trim(), _pattern2.Trim()))
                                select new
                                {
                                    CashFlowType = _cfGroup.CashFlowType,
                                    CashFlowGroupCode = _cfGroup.CashFlowGroupCode,
                                    CashFlowGroupName = _cfGroup.CashFlowGroupName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINMsCashFlowGroup(_row.CashFlowType, _row.CashFlowGroupCode, _row.CashFlowGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINMsCashFlowGroup> GetListForDDL(string _prmCFType)
        {
            List<FINMsCashFlowGroup> _result = new List<FINMsCashFlowGroup>();

            try
            {
                var _query =
                            (
                                from _Cfgroup in this.db.FINMsCashFlowGroups
                                where _Cfgroup.CashFlowType == _prmCFType
                                select new
                                {
                                    CashFlowGroupCode = _Cfgroup.CashFlowGroupCode,
                                    CashFlowGroupName = _Cfgroup.CashFlowGroupCode + " - " + _Cfgroup.CashFlowGroupName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINMsCashFlowGroup(_row.CashFlowGroupCode, _row.CashFlowGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region CashflowGroupSub

        public double RowsCountCFGroupSub(string _prmCategory, string _prmKeyword)
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
                            from _cfGroupSub in this.db.FINMsCashFlowGroupSubs
                            where (SqlMethods.Like(_cfGroupSub.CashFlowGroupSubCode.Trim(), _pattern1.Trim()))
                               && (SqlMethods.Like(_cfGroupSub.CashFlowGroupSubName.Trim(), _pattern2.Trim()))
                            select _cfGroupSub.CashFlowGroupSubCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool EditCFGroupSub(FINMsCashFlowGroupSub _prmCfGroupSub)
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

        public bool AddCFGroupSub(FINMsCashFlowGroupSub _prmCFGroupSub)
        {
            bool _result = false;

            try
            {

                this.db.FINMsCashFlowGroupSubs.InsertOnSubmit(_prmCFGroupSub);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiCFGroupSub(string[] _prmCFGroupSubCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCFGroupSubCode.Length; i++)
                {
                    string[] _tempsplit = _prmCFGroupSubCode[i].Split('|');

                    FINMsCashFlowGroupSub _cfGroupSUbCode = this.db.FINMsCashFlowGroupSubs.Single(_cfGroupSub => _cfGroupSub.CashFlowGroupSubCode == _tempsplit[0] && _cfGroupSub.CashFlowType == _tempsplit[1]);

                    this.db.FINMsCashFlowGroupSubs.DeleteOnSubmit(_cfGroupSUbCode);
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

        public FINMsCashFlowGroupSub GetSingleCFGroupSub(string _prmCfGroupSub, string _prmCFType)
        {
            FINMsCashFlowGroupSub _result = null;

            try
            {
                _result = this.db.FINMsCashFlowGroupSubs.Single(_cfGroup => _cfGroup.CashFlowGroupSubCode.Trim() == _prmCfGroupSub.Trim() && _cfGroup.CashFlowType.Trim() == _prmCFType.Trim());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCFgroupSubNamebyCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _cfGroupSub in this.db.FINMsCashFlowGroupSubs
                                where _cfGroupSub.CashFlowGroupSubCode == _prmCode
                                select new
                                {
                                    CashFlowGroupName = _cfGroupSub.CashFlowGroupSubName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CashFlowGroupName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINMsCashFlowGroupSub> GetListCFGroupSub(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINMsCashFlowGroupSub> _result = new List<FINMsCashFlowGroupSub>();

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
                                from _cfGroupSub in this.db.FINMsCashFlowGroupSubs
                                where (SqlMethods.Like(_cfGroupSub.CashFlowGroupSubCode.Trim(), _pattern1.Trim()))
                                   && (SqlMethods.Like(_cfGroupSub.CashFlowGroupSubName.Trim(), _pattern2.Trim()))
                                select new
                                {
                                    CashFlowType = _cfGroupSub.CashFlowType,
                                    CashFlowGroupCode = _cfGroupSub.CashFlowGroupCode,
                                    CashFlowGroupSubCode = _cfGroupSub.CashFlowGroupSubCode,
                                    CashFlowGroupSubName = _cfGroupSub.CashFlowGroupSubName,
                                    TypeCode = _cfGroupSub.TypeCode,
                                    Operator = _cfGroupSub.Operator
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINMsCashFlowGroupSub(_row.CashFlowType, _row.CashFlowGroupCode, _row.CashFlowGroupSubCode, _row.CashFlowGroupSubName, _row.TypeCode, Convert.ToChar(_row.Operator)));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region CashflowGroupSubDt

        public double RowsCountCFGroupSubDt(string _prmSubCode, string _prmCategory, string _prmKeyword)
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
                            from _cfGroupSubDt in this.db.FINMsCashFlowGroupSubDts
                            join _msAccount in this.db.MsAccounts
                            on _cfGroupSubDt.Account equals _msAccount.Account
                            where _cfGroupSubDt.CashFlowGroupSubCode.Trim() == _prmSubCode.Trim()
                               && (SqlMethods.Like(_cfGroupSubDt.Account.Trim(), _pattern1.Trim()))
                               && (SqlMethods.Like(_msAccount.AccountName.Trim(), _pattern2.Trim()))
                            select _cfGroupSubDt.CashFlowGroupSubCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool EditCFGroupSubDt(FINMsCashFlowGroupSubDt _prmCfGroupSub)
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

        public bool AddCFGroupSubDt(FINMsCashFlowGroupSubDt _prmCFGroupSub)
        {
            bool _result = false;

            try
            {

                this.db.FINMsCashFlowGroupSubDts.InsertOnSubmit(_prmCFGroupSub);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiCFGroupSubDt(string[] _prmSubCodeDt, string _prmSubCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmSubCodeDt.Length; i++)
                {

                    FINMsCashFlowGroupSubDt _cfGroupSUbDt = this.db.FINMsCashFlowGroupSubDts.Single(_cfGroupSubDt => _cfGroupSubDt.Account == _prmSubCodeDt[i] && _cfGroupSubDt.CashFlowGroupSubCode == _prmSubCode);

                    this.db.FINMsCashFlowGroupSubDts.DeleteOnSubmit(_cfGroupSUbDt);
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


        public List<FINMsCashFlowGroupSubDt> GetListCFGroupSubDt(int _prmReqPage, int _prmPageSize, string _prmSubCode, string _prmCategory, string _prmKeyword)
        {
            List<FINMsCashFlowGroupSubDt> _result = new List<FINMsCashFlowGroupSubDt>();

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
                                from _cfGroupSubDt in this.db.FINMsCashFlowGroupSubDts
                                join _msAccount in this.db.MsAccounts
                                on _cfGroupSubDt.Account equals _msAccount.Account
                                where _cfGroupSubDt.CashFlowGroupSubCode.Trim() == _prmSubCode.Trim()
                                && (SqlMethods.Like(_cfGroupSubDt.Account.Trim(), _pattern1.Trim()))
                                && (SqlMethods.Like(_msAccount.AccountName.Trim(), _pattern2.Trim()))
                                select new
                                {
                                    CashFlowGroupSubCode = _cfGroupSubDt.CashFlowGroupSubCode,
                                    Account = _cfGroupSubDt.Account,
                                    AccountName = (
                                                    from _msAcc in this.db.MsAccounts
                                                    where _msAcc.Account == _cfGroupSubDt.Account
                                                    select _msAcc.AccountName
                                                    ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINMsCashFlowGroupSubDt(_row.CashFlowGroupSubCode, _row.Account, _row.AccountName));
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
                                        from _cfGrpSubdt in this.db.FINMsCashFlowGroupSubDts
                                        where _cfGrpSubdt.CashFlowGroupSubCode == id
                                        select _cfGrpSubdt.Account
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
                                            from _grpSubDt in this.db.FINMsCashFlowGroupSubDts
                                            where _grpSubDt.CashFlowGroupSubCode == _prmTransTypeCode
                                            select _grpSubDt.Account
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
                                            from _finMsCFGroupSubDt in this.db.FINMsCashFlowGroupDts
                                            where _finMsCFGroupSubDt.CashFlowGroupCode == id
                                            select _finMsCFGroupSubDt.Account
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


        public bool AddSubGroupAccount(string label)
        {
            bool _result = false;
            List<FINMsCashFlowGroupSubDt> _CFGrpSubAccount = new List<FINMsCashFlowGroupSubDt>();
            List<MsAccount> _msAccount = new List<MsAccount>();
            AccountBL _account = new AccountBL();
            FINMsCashFlowGroupSubDt _gabung = new FINMsCashFlowGroupSubDt();

            try
            {

                _msAccount = this.GetListNotAssign(label);
                foreach (var item in _msAccount)
                {
                    _gabung = new FINMsCashFlowGroupSubDt();
                    _gabung.CashFlowGroupSubCode = label;
                    _gabung.Account = item.Account;

                    _CFGrpSubAccount.Add(_gabung);
                }

                AddAllccount(_CFGrpSubAccount);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;

        }


        public bool AddAllccount(List<FINMsCashFlowGroupSubDt> _prmCFGrpSubDt)
        {
            bool _result = false;
            try
            {
                foreach (FINMsCashFlowGroupSubDt item in _prmCFGrpSubDt)
                {
                    this.db.FINMsCashFlowGroupSubDts.InsertOnSubmit(item);
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

        public bool AddSelectedAccount(string _temp, string id)
        {
            bool _result = false;
            int i = 0;
            string[] _tempsplit = _temp.Split(',');


            try
            {
                for (i = 0; i < _tempsplit.Length; i++)
                {
                    FINMsCashFlowGroupSubDt _gabung = new FINMsCashFlowGroupSubDt();
                    _gabung.CashFlowGroupSubCode = id;
                    _gabung.Account = _tempsplit[i];
                    this.db.FINMsCashFlowGroupSubDts.InsertOnSubmit(_gabung);
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

        #endregion

        ~CashflowgroupBL()
        {
        }

    }
}
