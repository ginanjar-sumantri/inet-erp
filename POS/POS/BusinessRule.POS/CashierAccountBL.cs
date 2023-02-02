using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;

namespace BusinessRule.POS
{
    public sealed class CashierAccountBL : Base
    {
        public CashierAccountBL()
        {
        }

        #region Cashier

        public double RowsCount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "CashierEmpNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Account")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                             from _msCashierAccount in this.db.POSMsCashierAccounts
                             join _msEmployee in this.db.MsEmployees
                             on _msCashierAccount.CashierEmpNmbr equals _msEmployee.EmpNumb
                             join _msAccount in this.db.MsAccounts
                             on _msCashierAccount.Account equals _msAccount.Account

                             where (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_msAccount.AccountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _msCashierAccount
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<POSMsCashierAccount> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsCashierAccount> _result = new List<POSMsCashierAccount>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "CashierEmpNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Account")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                             from _msCashierAccount in this.db.POSMsCashierAccounts
                             join _msEmployee in this.db.MsEmployees
                             on _msCashierAccount.CashierEmpNmbr equals _msEmployee.EmpNumb
                             join _msAccount in this.db.MsAccounts
                             on _msCashierAccount.Account equals _msAccount.Account

                             where (SqlMethods.Like(_msEmployee.EmpName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                             && (SqlMethods.Like(_msAccount.AccountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             orderby _msEmployee.EmpName ascending
                             select new
                             {
                                 CashierEmpCode = _msCashierAccount.CashierEmpNmbr,
                                 CashierName = _msEmployee.EmpName,
                                 AccountName = _msAccount.AccountName
                             }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new POSMsCashierAccount(_row.CashierEmpCode, _row.CashierName, _row.AccountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsCashierAccount> GetList()
        {
            List<POSMsCashierAccount> _result = new List<POSMsCashierAccount>();

            try
            {
                var _query = (
                                from _msCashierAccount in this.db.POSMsCashierAccounts
                                join _msEmployee in this.db.MsEmployees
                                on _msCashierAccount.CashierEmpNmbr equals _msEmployee.EmpNumb
                                join _msAccount in this.db.MsAccounts
                                on _msCashierAccount.Account equals _msAccount.Account

                                orderby _msEmployee.EmpName ascending
                                select new
                                {
                                    CashierEmpCode = _msCashierAccount.CashierEmpNmbr,
                                    CashierName = _msEmployee.EmpName,
                                    AccountName = _msAccount.AccountName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new POSMsCashierAccount(_row.CashierEmpCode, _row.CashierName, _row.AccountName));
                }


            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    POSMsCashierAccount _msCashierAccount = this.db.POSMsCashierAccounts.Single(_temp => _temp.CashierEmpNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.POSMsCashierAccounts.DeleteOnSubmit(_msCashierAccount);
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

        public POSMsCashierAccount GetSingle(string _prmCode)
        {
            POSMsCashierAccount _result = null;

            try
            {
                _result = this.db.POSMsCashierAccounts.Single(_temp => _temp.CashierEmpNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetMemberNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msCashierAccount in this.db.POSMsCashierAccounts
                                where _msCashierAccount.CashierEmpNmbr == _prmCode
                                select new
                                {
                                    CashierAccountName = _msCashierAccount.Account
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CashierAccountName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(POSMsCashierAccount _prmMsCashierAccount)
        {
            bool _result = false;

            try
            {
                this.db.POSMsCashierAccounts.InsertOnSubmit(_prmMsCashierAccount);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(POSMsCashierAccount _prmMsCashierAccount)
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

        ~CashierAccountBL()
        {
        }

    }
}
