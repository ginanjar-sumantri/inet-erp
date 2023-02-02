using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Linq;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Transactions;
using System.Net.Mail;
using System.Web;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using MySql.Data.MySqlClient;



namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed class CustBillAccountBL : Base
    {
        public CustBillAccountBL()
        {
        }

        #region CustBillAccount

        public int RowsCountCustBillAccount(string _prmCategory, string _prmKeyword)
        {
            int _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "CustomerBillingAccount")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "CustomerName")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword.Trim().ToLower() + "%";
            }

            _result =
                       (
                           from _custBillAccount in this.db.Master_CustBillAccounts
                           join _msCustomer in this.db.MsCustomers
                                on _custBillAccount.CustCode equals _msCustomer.CustCode
                           where
                                (SqlMethods.Like(_custBillAccount.CustBillAccount.Trim().ToLower(), _pattern1))
                                && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2))
                           select new
                           {
                               CustBillCode = _custBillAccount.CustBillCode
                           }
                       ).Count();

            return _result;
        }

        public List<Master_CustBillAccount> GetListCustBillAccount(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_CustBillAccount> _result = new List<Master_CustBillAccount>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "CustomerBillingAccount")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "CustomerName")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword.Trim().ToLower() + "%";
            }

            try
            {
                var _query =
                            (
                                from _custBillAccount in this.db.Master_CustBillAccounts
                                join _msCustomer in this.db.MsCustomers
                                    on _custBillAccount.CustCode equals _msCustomer.CustCode
                                where
                                    (SqlMethods.Like(_custBillAccount.CustBillAccount.Trim().ToLower(), _pattern1))
                                    && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2))
                                orderby _custBillAccount.EditDate descending
                                select new
                                {
                                    CustBillCode = _custBillAccount.CustBillCode,
                                    CustBillAccount = _custBillAccount.CustBillAccount,
                                    CustCode = _custBillAccount.CustCode,
                                    CustName = _msCustomer.CustName,
                                    ProductCode = _custBillAccount.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode == _custBillAccount.ProductCode
                                                    select _msProduct.ProductName
                                                  ).FirstOrDefault(),
                                    CustBillDescription = _custBillAccount.CustBillDescription,
                                    CurrCode = _custBillAccount.CurrCode,
                                    AmountForex = _custBillAccount.AmountForex,
                                    fgActive = _custBillAccount.fgActive
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_CustBillAccount(_row.CustBillCode, _row.CustBillAccount, _row.CustCode, _row.CustName, _row.ProductCode, _row.ProductName, _row.CustBillDescription, _row.CurrCode, _row.AmountForex, _row.fgActive));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditCustBillAccount(Master_CustBillAccount _prmCustBillAccount)
        {
            bool _result = false;

            try
            {
                if (this.IsCustBillAccountExists(_prmCustBillAccount.CustBillCode, _prmCustBillAccount.CustBillAccount) == false)
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

        public bool EditCustBillAccountUpdateRadius(Master_CustBillAccount _prmCustBillAccount, string _prmUserNameOld)
        {
            bool _result = false;

            try
            {
                if (this.IsCustBillAccountExists(_prmCustBillAccount.CustBillCode, _prmCustBillAccount.CustBillAccount) == false)
                {
                    MySqlConnection conn = new MySqlConnection(this.GetDatabaseConnString("RADINET"));

                    this.UpdateUser(conn, _prmCustBillAccount.UserName, _prmUserNameOld, _prmCustBillAccount.PIN, _prmCustBillAccount.fgActive, _prmCustBillAccount.InsertBy);

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

        public String UpdateUser(MySqlConnection _prmConn, string _prmUserName, string _prmUserNameOld, string _prmPassword, bool? _prmEnableUser, string _prmInsertBy)
        {
            String _result = "";
            MySqlCommand cmd = _prmConn.CreateCommand();
            DateTime _now = DateTime.Now;

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 3000;
                cmd.CommandText = "updateuser";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("_username", MySqlDbType.VarChar, 20);
                cmd.Parameters["_username"].Value = _prmUserName;
                cmd.Parameters.Add("_usernameOld", MySqlDbType.VarChar, 20);
                cmd.Parameters["_usernameOld"].Value = _prmUserNameOld;
                cmd.Parameters.Add("_password", MySqlDbType.VarChar, 20);
                cmd.Parameters["_password"].Value = _prmPassword;
                cmd.Parameters.Add("_enableuser", MySqlDbType.Int32);
                cmd.Parameters["_enableuser"].Value = _prmEnableUser;
                cmd.Parameters.Add("_createdby", MySqlDbType.String);
                cmd.Parameters["_createdby"].Value = _prmInsertBy;

                if (_prmConn.State == System.Data.ConnectionState.Open)
                {
                    _prmConn.Close();
                }

                if (_prmConn.State != System.Data.ConnectionState.Open)
                {
                    _prmConn.Open();
                }

                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "UpdateVoucher", "Billing");
                _result = "Connection To Radius Failed.";
            }

            return _result;
        }

        //public string AddCustBillAccount(Master_CustBillAccount _prmCustBillAccount, String _prmPin)
        //{
        //    string _result = "";

        //    try
        //    {
        //        if (this.IsCustBillAccountExists(_prmCustBillAccount.CustBillCode, _prmCustBillAccount.CustBillAccount) == false)
        //        {
        //            MySqlConnection conn = new MySqlConnection(this.GetDatabaseConnString("RADINET"));
        //            this.db.S_SAAutoNmbrAlphabet(DateTime.Now.Year, DateTime.Now.Month, AppModule.GetValue(TransactionType.CustomerBillingAccount), ref _result, _prmCustBillAccount.CustCode);

        //            if (_result != "")
        //            {
        //                _prmCustBillAccount.CustBillAccount = _result;
        //                this.db.Master_CustBillAccounts.InsertOnSubmit(_prmCustBillAccount);

        //                this.CreateUser(conn, _prmCustBillAccount.UserName, _prmPin, Convert.ToInt32(26), _prmCustBillAccount.RadiusExpiredDate, (this.GetCustNameByCustCode(_prmCustBillAccount.CustCode) + '-' + _prmCustBillAccount.CustCode), Convert.ToInt32(1), "", _prmCustBillAccount.InsertBy);


        //                this.db.SubmitChanges();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public string AddCustBillAccount(Master_CustBillAccount _prmCustBillAccount)
        {
            string _result = "";

            try
            {
                if (this.IsCustBillAccountExists(_prmCustBillAccount.CustBillCode, _prmCustBillAccount.CustBillAccount) == false)
                {
                    this.db.S_SAAutoNmbrAlphabet(DateTime.Now.Year, DateTime.Now.Month, AppModule.GetValue(TransactionType.CustomerBillingAccount), ref _result, _prmCustBillAccount.CustCode);

                    if (_result != "")
                    {
                        _prmCustBillAccount.CustBillAccount = _result;
                        this.db.Master_CustBillAccounts.InsertOnSubmit(_prmCustBillAccount);
                        this.db.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }



        public String CreateUser(MySqlConnection _prmConn, string _prmUser, string _prmPassword, int _prmSrvID, DateTime? _prmExpiretime, string _prmCustName, int _prmAcctype, string _prmAdrress, string _prmCreateBy)
        {
            String _result = "";
            MySqlCommand cmd = _prmConn.CreateCommand();
            DateTime _now = DateTime.Now;

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 3000;
                cmd.CommandText = "createuser";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("_username", MySqlDbType.VarChar);
                cmd.Parameters["_username"].Value = _prmUser;
                cmd.Parameters.Add("_password", MySqlDbType.VarChar, 20);
                cmd.Parameters["_password"].Value = _prmPassword;
                cmd.Parameters.Add("_srvid", MySqlDbType.Int32);
                cmd.Parameters["_srvid"].Value = _prmSrvID;
                cmd.Parameters.Add("_expiretime", MySqlDbType.Datetime);
                cmd.Parameters["_expiretime"].Value = _prmExpiretime;
                cmd.Parameters.Add("_custname", MySqlDbType.VarChar, 50);
                cmd.Parameters["_custname"].Value = _prmCustName;
                cmd.Parameters.Add("_acctype", MySqlDbType.Int32);
                cmd.Parameters["_acctype"].Value = _prmAcctype;
                cmd.Parameters.Add("_address", MySqlDbType.VarChar, 100);
                cmd.Parameters["_address"].Value = _prmAdrress;
                cmd.Parameters.Add("_createdby", MySqlDbType.VarChar, 50);
                cmd.Parameters["_createdby"].Value = _prmCreateBy;

                //cmd.Parameters.Add("_userName", MySqlDbType.VarChar, 64);
                //cmd.Parameters["_userName"].Value = HttpContext.Current.User.Identity.Name;
                if (_prmConn.State == System.Data.ConnectionState.Open)
                {
                    _prmConn.Close();
                }

                if (_prmConn.State != System.Data.ConnectionState.Open)
                {
                    _prmConn.Open();
                }

                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "UpdateVoucher", "Billing");
                _result = "Connection To Radius Failed.";
            }

            return _result;
        }

        public bool DeleteMultiCustBillAccount(string[] _prmCustBillCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCustBillCode.Length; i++)
                {
                    Master_CustBillAccount _msCustBillAccount = this.db.Master_CustBillAccounts.Single(_temp => _temp.CustBillCode == new Guid(_prmCustBillCode[i]));

                    this.db.Master_CustBillAccounts.DeleteOnSubmit(_msCustBillAccount);
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

        public Master_CustBillAccount GetSingleCustBillAccount(Guid _prmCustBillCode)
        {
            Master_CustBillAccount _result = null;

            try
            {
                _result = this.db.Master_CustBillAccounts.Single(_temp => _temp.CustBillCode == _prmCustBillCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Master_CustBillAccount GetSingleCustBillAccount(Guid? _prmCustBillCode)
        {
            Master_CustBillAccount _result = null;

            try
            {
                _result = this.db.Master_CustBillAccounts.Single(_temp => _temp.CustBillCode == _prmCustBillCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        private bool IsCustBillAccountExists(Guid _prmCustBillCode, string _prmCustBillAccount)
        {
            bool _result = false;

            try
            {
                var _query = from _custBillAccount in this.db.Master_CustBillAccounts
                             where (_custBillAccount.CustBillAccount == _prmCustBillAccount) && (_custBillAccount.CustBillCode != _prmCustBillCode)
                             select new
                             {
                                 _custBillAccount.CustBillAccount
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

        public List<Master_CustBillAccount> GetListDDLCustBillAccount(string _prmCustCode, string _prmCurrCode)
        {
            List<Master_CustBillAccount> _result = new List<Master_CustBillAccount>();

            try
            {
                var _query =
                            (
                                from _custBillAccount in this.db.Master_CustBillAccounts
                                where _custBillAccount.CustCode.Trim().ToLower() == _prmCustCode.Trim().ToLower() && _custBillAccount.CurrCode.Trim().ToLower() == _prmCurrCode.Trim().ToLower()
                                && _custBillAccount.fgActive == true
                                orderby _custBillAccount.CustBillAccount ascending
                                select new
                                {
                                    CustBillCode = _custBillAccount.CustBillCode,
                                    CustBillAccount = _custBillAccount.CustBillAccount
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_CustBillAccount(_row.CustBillCode, _row.CustBillAccount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_CustBillAccount> GetListDDLCustBillAccount(string _prmCustCode)
        {
            List<Master_CustBillAccount> _result = new List<Master_CustBillAccount>();

            try
            {
                var _query =
                            (
                                from _custBillAccount in this.db.Master_CustBillAccounts
                                where _custBillAccount.CustCode.Trim().ToLower() == _prmCustCode.Trim().ToLower()
                                && _custBillAccount.fgActive == true
                                orderby _custBillAccount.CustBillAccount ascending
                                select new
                                {
                                    CustBillCode = _custBillAccount.CustBillCode,
                                    CustBillAccount = _custBillAccount.CustBillAccount
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_CustBillAccount(_row.CustBillCode, _row.CustBillAccount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_CustBillAccount> GetListDDLCustBillAccount()
        {
            List<Master_CustBillAccount> _result = new List<Master_CustBillAccount>();

            try
            {
                var _query =
                            (
                                from _custBillAccount in this.db.Master_CustBillAccounts
                                join _msCustomer in this.db.MsCustomers
                                on _custBillAccount.CustCode equals _msCustomer.CustCode
                                where _custBillAccount.fgActive == true
                                orderby _custBillAccount.CustBillAccount ascending
                                select new
                                {
                                    CustBillCode = _custBillAccount.CustBillCode,
                                    CustCode = _custBillAccount.CustCode,
                                    CustName = _msCustomer.CustName + "-" + _custBillAccount.CustCode + "-" + _custBillAccount.CustBillAccount
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_CustBillAccount(_row.CustBillCode, _row.CustCode, _row.CustName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_CustBillAccount> GetListDDLCustBillAccountRef()
        {
            List<Master_CustBillAccount> _result = new List<Master_CustBillAccount>();

            try
            {
                var _query =
                            (
                                from _custBillAccount in this.db.Master_CustBillAccounts
                                join _msCustomer in this.db.MsCustomers
                                on _custBillAccount.CustCode equals _msCustomer.CustCode
                                where _custBillAccount.fgActive == true
                                orderby _custBillAccount.CustBillAccount ascending
                                select new
                                {
                                    CustBillAccount = _custBillAccount.CustBillAccount,
                                    CustCode = _custBillAccount.CustCode,
                                    CustName = _msCustomer.CustName + "-" + _custBillAccount.CustBillAccount
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_CustBillAccount(_row.CustBillAccount, _row.CustCode, _row.CustName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_CustBillAccount> GetListDDLCustBillAccountPostpone(string _prmCustCode, string _prmCurrCode)
        {
            List<Master_CustBillAccount> _result = new List<Master_CustBillAccount>();

            try
            {
                var _query =
                            (
                                from _custBillAccount in this.db.Master_CustBillAccounts
                                join _masterProductExtension in this.db.Master_ProductExtensions
                                    on _custBillAccount.ProductCode equals _masterProductExtension.ProductCode
                                where _custBillAccount.CustCode.Trim().ToLower() == _prmCustCode.Trim().ToLower() && _custBillAccount.CurrCode.Trim().ToLower() == _prmCurrCode.Trim().ToLower()
                                    && _masterProductExtension.IsPostponeAllowed == true
                                orderby _custBillAccount.CustBillAccount ascending
                                select new
                                {
                                    CustBillCode = _custBillAccount.CustBillCode,
                                    CustBillAccount = _custBillAccount.CustBillAccount
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_CustBillAccount(_row.CustBillCode, _row.CustBillAccount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCustBillAccount(Guid _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _custBillAccount in this.db.Master_CustBillAccounts
                                where _custBillAccount.CustBillCode == _prmCode
                                select new
                                {
                                    CustBillAccount = _custBillAccount.CustBillAccount
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.CustBillAccount;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Guid GetCustBillCode(String _prmCode)
        {
            Guid _result = new Guid();

            try
            {
                var _query = (
                                from _custBillAccount in this.db.Master_CustBillAccounts
                                where _custBillAccount.CustBillAccount == _prmCode
                                select new
                                {
                                    CustBillAccount = _custBillAccount.CustBillCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.CustBillAccount;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetCustCodeByCustbillcode(String _prmCustbillcode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _masterCustBillAccounts in this.db.Master_CustBillAccounts
                                where _masterCustBillAccounts.CustBillAccount == _prmCustbillcode
                                select new
                                {
                                    CustCode = _masterCustBillAccounts.CustCode
                                }
                            );
                foreach (var _row in _query)
                {
                    _result = _row.CustCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public String GetDatabaseConnString(String _prmRadiusCode)
        {
            String _result = "";

            var _query = (
                            from _radius in this.db.BILMsRadius
                            where _radius.RadiusCode == _prmRadiusCode
                            select _radius
                         ).FirstOrDefault();

            _result = "Server=" + _query.RadiusIP + ";Port=3306;" + "Database=" + _query.RadiusDbName + "; Uid=" + _query.RadiusUserName + "; Pwd=" + Rijndael.Decrypt(_query.RadiusPwd, ApplicationConfig.PasswordEncryptionKey) + ";";

            return _result;
        }

        public Master_CustBillAccount GetRadiusExpiredDateByUserName(string _prmUserName, string _password)
        {
            Master_CustBillAccount _result = new Master_CustBillAccount();
            try
            {
                var _query = (from _custBillAccount in this.db.Master_CustBillAccounts
                              where _custBillAccount.UserName.Trim().ToLower() == _prmUserName.Trim().ToLower()
                                  && _custBillAccount.PIN.Trim().ToLower() == _password.Trim().ToLower()
                              select _custBillAccount
                        ).FirstOrDefault();
                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public Master_CustBillAccount CekAvailabilityUserName(string _prmUserName)
        {
            Master_CustBillAccount _result = new Master_CustBillAccount();
            try
            {
                var _query = (from _custBillAccount in this.db.Master_CustBillAccounts
                              where _custBillAccount.UserName.Trim().ToLower() == _prmUserName.Trim().ToLower()
                              select _custBillAccount
                        ).FirstOrDefault();
                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        //public Master_CustBillAccount GetMsCustBillAccountByCustBillCode(Guid _prmCustBillCode)
        //{
        //    Master_CustBillAccount _result = new Master_CustBillAccount

        //}

        //_username VARCHAR(20), IN _password VARCHAR(20), IN _srvid INT, IN _expiretime DATETIME, IN _custname VARCHAR(50), IN _acctype INT, IN _address VARCHAR(100), IN _createdby VARCHAR(50))



        #endregion

        #region BILTrRadiusActivate

        public int RowsCountBILTrRadiusActivate(string _prmCategory, string _prmKeyword)
        {
            int _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "CustomerBillingCode")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Reference")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword.Trim().ToLower() + "%";
            }

            _result =
                       (from _bILTrRadiusActivates in this.db.BILTrRadiusActivates
                        where
                            (SqlMethods.Like(_bILTrRadiusActivates.CustBillCode.Trim().ToLower(), _pattern1))
                            && (SqlMethods.Like(_bILTrRadiusActivates.PaidRefTransNmbr.Trim().ToLower(), _pattern2))
                        select _bILTrRadiusActivates
                       ).Count();

            return _result;
        }

        public List<BILTrRadiusActivate> GetListBILTrRadiusActivate(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<BILTrRadiusActivate> _result = new List<BILTrRadiusActivate>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "CustomerBillingCode")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Reference")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword.Trim().ToLower() + "%";
            }

            try
            {
                var _query =
                            (
                                from _bILTrRadiusActivates in this.db.BILTrRadiusActivates
                                //join _msCustomer in this.db.MsCustomers
                                //    on _bILTrRadiusActivates.CustCode    equals _msCustomer.CustCode
                                where
                                    (SqlMethods.Like(_bILTrRadiusActivates.CustBillCode.Trim().ToLower(), _pattern1))
                                    && (SqlMethods.Like(_bILTrRadiusActivates.PaidRefTransNmbr.Trim().ToLower(), _pattern2))
                                    && _bILTrRadiusActivates.fgUpdate == false
                                orderby _bILTrRadiusActivates.InsertDate descending
                                select _bILTrRadiusActivates
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public BILTrRadiusActivate GetSingleCustBillAccount(String _prmPeroid, String _prmYear, String _prmCustBillCode)
        {

            BILTrRadiusActivate _result = null;

            try
            {
                _result = this.db.BILTrRadiusActivates.Single(_temp => _temp.Period.ToString().Trim().ToLower() == _prmPeroid.Trim().ToLower() && _temp.Year.ToString().Trim().ToLower() == _prmYear.Trim().ToLower() && _temp.CustBillCode.Trim().ToLower() == _prmCustBillCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditBILTrRadiusActivate(BILTrRadiusActivate _prmBILTrRadiusActivate, Master_CustBillAccount _prmMsCustBillAccount)
        {
            bool _result = false;

            try
            {
                MySqlConnection conn = new MySqlConnection(this.GetDatabaseConnString("RADINET"));

                this.UpdateRadiusExpired(conn, _prmBILTrRadiusActivate.UserName, _prmBILTrRadiusActivate.PIN, _prmMsCustBillAccount.RadiusExpiredDate, _prmBILTrRadiusActivate.InsertBy);

                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        private String UpdateRadiusExpired(MySqlConnection _prmConn, string _prmUserName, string _prmPIN, DateTime? _prmRadiusExperied, string _prmInsertBy)
        {
            String _result = "";
            MySqlCommand cmd = _prmConn.CreateCommand();
            DateTime _now = DateTime.Now;

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 3000;
                cmd.CommandText = "UpdateRadiusExpired";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("_username", MySqlDbType.VarChar, 50);
                cmd.Parameters["_username"].Value = _prmUserName;
                cmd.Parameters.Add("_password", MySqlDbType.VarChar, 50);
                cmd.Parameters["_password"].Value = _prmPIN;
                cmd.Parameters.Add("_expiretime", MySqlDbType.Datetime);
                cmd.Parameters["_expiretime"].Value = _prmRadiusExperied;
                cmd.Parameters.Add("_createdby", MySqlDbType.String);
                cmd.Parameters["_createdby"].Value = _prmInsertBy;

                if (_prmConn.State == System.Data.ConnectionState.Open)
                {
                    _prmConn.Close();
                }

                if (_prmConn.State != System.Data.ConnectionState.Open)
                {
                    _prmConn.Open();
                }

                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "UpdateVoucher", "Billing");
                _result = "Connection To Radius Failed.";
            }

            return _result;

        }

        public String GetCustNameByCustCode(String _prmCustcode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msCustomer in this.db.MsCustomers
                                where _msCustomer.CustCode == _prmCustcode
                                select new
                                {
                                    CustName = _msCustomer.CustName
                                }
                            );
                foreach (var _row in _query)
                {
                    _result = _row.CustName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        #endregion

        #region BILTrRadiusActivateTemporary

        public int RowsCountBILTrRadiusActiveTemp(string _prmCategory, string _prmKeyword)
        {
            int _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNo")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword.Trim().ToLower() + "%";
            }
            try
            {
                _result =
                           (
                               from _bilTrRadiusActivateTemporaries in this.db.BILTrRadiusActivateTemporaries
                               where
                                    (SqlMethods.Like(_bilTrRadiusActivateTemporaries.TransNmbr.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_bilTrRadiusActivateTemporaries.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                               select _bilTrRadiusActivateTemporaries
                           ).Count();
            }

            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<BILTrRadiusActivateTemporary> GetListBILTrRadiusActiveTemp(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<BILTrRadiusActivateTemporary> _result = new List<BILTrRadiusActivateTemporary>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNo")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword.Trim().ToLower() + "%";
            }

            try
            {
                var _query =
                            (
                                from _bilTrRadiusActivateTemporaries in this.db.BILTrRadiusActivateTemporaries
                                where
                                    (SqlMethods.Like(_bilTrRadiusActivateTemporaries.TransNmbr.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like((_bilTrRadiusActivateTemporaries.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _bilTrRadiusActivateTemporaries.Transdate descending
                                select _bilTrRadiusActivateTemporaries
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public BILTrRadiusActivateTemporary GetSingleBILTrRadiusActiveTemp(String _prmTransNmbr)
        {
            BILTrRadiusActivateTemporary _result = null;

            try
            {
                _result = this.db.BILTrRadiusActivateTemporaries.Single(_temp => _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String AddBILTrRadiusActiveTemp(BILTrRadiusActivateTemporary _prmBILTrRadiusActivateTemporary, FileUpload _prmFileUpload, String _imagePath)
        {
            String _result = "";

            try
            {
                String _result2 = "";

                if (_result2 == "")
                {
                    String _prmFotoCode = _prmBILTrRadiusActivateTemporary.CustCode + "-" + _prmBILTrRadiusActivateTemporary.CustBillCode.ToString();
                    _result = this.UploadProductPicture("", _prmFotoCode, _prmBILTrRadiusActivateTemporary.AttachmentFile, _prmFileUpload, _imagePath, "Add");

                    if (_result == "")
                    {
                        String _path = _prmFileUpload.PostedFile.FileName;
                        FileInfo _file = new FileInfo(_path);

                        String _newImagepath = _imagePath + _prmFotoCode.ToString() + _file.Extension;

                        _prmFileUpload.PostedFile.SaveAs(_newImagepath);

                        _prmBILTrRadiusActivateTemporary.AttachmentFile = _prmFotoCode.ToString() + _file.Extension;

                        Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                        foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                        {
                            _prmBILTrRadiusActivateTemporary.TransNmbr = item.Number;
                            _transactionNumber.TempTransNmbr = item.Number;
                        }

                        this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                        this.db.BILTrRadiusActivateTemporaries.InsertOnSubmit(_prmBILTrRadiusActivateTemporary);

                        var _query = (
                                       from _temp in this.db.Temporary_TransactionNumbers
                                       where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                       select _temp
                                     );

                        this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                        this.db.SubmitChanges();

                        _result = _prmBILTrRadiusActivateTemporary.TransNmbr;

                        _file.Refresh();
                    }
                }
                else
                {
                    _result = _result2;
                }
            }
            catch (Exception ex)
            {
                _result = "Anda gagal menambah data";
            }

            return _result;
        }

        public String EditBILTrRadiusActiveTemp(BILTrRadiusActivateTemporary _prmBILTrRadiusActivateTemporary, FileUpload _prmFileUpload, String _imagePath)
        {
            String _result = "";

            try
            {

                String _result2 = "";
                String _path = "";

                if (_result == "")
                {
                    _path = _prmFileUpload.PostedFile.FileName;
                    String _prmFotoCode = _prmBILTrRadiusActivateTemporary.CustCode + "-" + _prmBILTrRadiusActivateTemporary.CustBillCode.ToString();

                    if (_path != "")
                    {
                        _result2 = this.UploadProductPicture("", _prmFotoCode, _prmBILTrRadiusActivateTemporary.AttachmentFile, _prmFileUpload, _imagePath, "Edit");

                        if (_result2 == "")
                        {
                            FileInfo _file = new FileInfo(_path);
                            String _newImagepath = _imagePath + _prmFotoCode.ToString() + _file.Extension;
                            _prmFileUpload.PostedFile.SaveAs(_newImagepath);

                            _prmBILTrRadiusActivateTemporary.AttachmentFile = _prmFotoCode.ToString() + _file.Extension;

                            _file.Refresh();
                        }
                        else
                        {
                            _result = _result2;
                        }
                    }

                    if (_result2 == "")
                    {
                        this.db.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Ubah data gagal";
            }

            return _result;
        }

        public String UploadProductPicture(String _prmCustCode, String _prmItemCode, String _prmPicture, FileUpload _prmFileUpload, String _imagesPath, String _prmAction)
        {
            String _result = "";

            String _path = _prmFileUpload.PostedFile.FileName;

            if (_path == "" && _prmAction == "Add")
            {
                _result = "Image must be filled";
            }

            else
            {
                FileInfo _file = new FileInfo(_path);
                String _imagepath = _imagesPath + _prmCustCode + "-" + _prmItemCode + _file.Extension;

                if (_path == "")
                {
                    _result = "Invalid filename supplied";
                }
                if (_prmFileUpload.PostedFile.ContentLength == 0)
                {
                    _result = "Invalid file content";
                }
                if (_result == "")
                {

                    System.Drawing.Image _uploadedImage = System.Drawing.Image.FromStream(_prmFileUpload.PostedFile.InputStream);

                    Decimal _width = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Width);
                    Decimal _height = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Height);

                    if (_width > Convert.ToDecimal("1200") || _height > Convert.ToDecimal("1600"))
                    {
                        _result = "This image is too big - please resize it!";
                    }
                    else
                    {
                        if (_prmFileUpload.PostedFile.ContentLength <= Convert.ToDecimal("2097152"))
                        {
                            if (_prmPicture != "no_picture.jpg")
                            {
                                if (File.Exists(_imagesPath + _prmPicture) == true)
                                {
                                    File.Delete(_imagesPath + _prmPicture);
                                }
                            }
                        }
                        else
                        {
                            _result = "Unable to upload, file exceeds maximum limit";
                        }
                    }

                }
            }
            return _result;
        }

        public bool GetAppr(BILTrRadiusActivateTemporary _prmBILTrRadiusActivateTemporary)
        {
            //string _result = "";
            bool _result = false;

            try
            {
                //int _success = this.db.spPOS_TicketingGetAppr(_prmTransNmbr, _prmuser, ref _result);

                //if (_result == "")
                //{
                //    _result = "Get Approval Success";

                //    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                //    _transActivity.ActivitiesCode = Guid.NewGuid();
                //    _transActivity.TransType = AppModule.GetValue(TransactionType.Ticketing);
                //    _transActivity.TransNmbr = _prmTransNmbr;
                //    _transActivity.FileNmbr = "";
                //    _transActivity.Username = _prmuser;
                //    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                //    _transActivity.ActivitiesDate = DateTime.Now;
                //    _transActivity.Reason = "";

                //    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);       
                //    this.db.SubmitChanges();
                //}

                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                //_result = "Get Approval Failed";
                //ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Approve(BILTrRadiusActivateTemporary _prmBILTrRadiusActivateTemporary)
        {
            //string _result = "";
            bool _result = false;

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    //int _success = this.db.spPOS_TicketingApprove(_prmTransNmbr, _prmuser, ref _result);

                    //if (_result == "")
                    //{
                    BILTrRadiusActivateTemporary _bilTrRadiusActivateTemporary = this.GetSingleBILTrRadiusActiveTemp(_prmBILTrRadiusActivateTemporary.TransNmbr);
                    foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_bilTrRadiusActivateTemporary.Transdate).Year, Convert.ToDateTime(_bilTrRadiusActivateTemporary.Transdate).Month, AppModule.GetValue(TransactionType.BILTrRadiusActivateTemporary), this._companyTag, ""))
                    {
                        _bilTrRadiusActivateTemporary.FileNmbr = item.Number;
                    }

                    //    _result = "Approve Success";

                    //    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    //    _transActivity.ActivitiesCode = Guid.NewGuid();
                    //    _transActivity.TransType = AppModule.GetValue(TransactionType.APRate);
                    //    _transActivity.TransNmbr = _prmTransNmbr;
                    //    _transActivity.FileNmbr = _bilTrRadiusActivateTemporary.FileNmbr;
                    //    _transActivity.Username = _prmuser;
                    //    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                    //    _transActivity.ActivitiesDate = DateTime.Now;
                    //    _transActivity.Reason = "";

                    //    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    //    this.db.SubmitChanges();

                    //    _scope.Complete();
                    //}

                    this.db.SubmitChanges();
                    _result = true;
                    _scope.Complete();
                }
            }
            catch (Exception ex)
            {
                //_result = "Approve Failed";
                //ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Posting(BILTrRadiusActivateTemporary _prmBILTrRadiusActivateTemporary, Master_CustBillAccount _prmCustBillAccount, DateTime _prmRadiusUpdateDate)
        {
            bool _result = false;

            try
            {
                MySqlConnection conn = new MySqlConnection(this.GetDatabaseConnString("RADINET"));

                this.UpdateRadiusExpired(conn, _prmCustBillAccount.UserName, _prmCustBillAccount.PIN, _prmRadiusUpdateDate, _prmCustBillAccount.InsertBy);
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

        ~CustBillAccountBL()
        {
        }
    }
}
