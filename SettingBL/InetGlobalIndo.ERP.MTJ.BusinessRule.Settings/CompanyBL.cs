using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.Authentication;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Diagnostics;
using System.Transactions;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Settings
{
    public sealed class CompanyBL : Base
    {
        public CompanyBL()
        {
        }

        #region master_Company

        public int RowsCount
        {
            get
            {
                return this.db.master_Companies.Count();
            }
        }

        public List<master_Company> GetList(int _prmReqPage, int _prmPageSize)
        {
            List<master_Company> _result = new List<master_Company>();

            try
            {
                //var _query = (
                //                from _company in this.db.master_Companies
                //                select new
                //                {
                //                    CompanyId = _company.CompanyID,
                //                    Name = _company.Name,
                //                    Logo = _company.Logo,
                //                    Address = _company.PrimaryAddress,
                //                    CompanyTag = _company.CompanyTag,
                //                    Default = _company.@default
                //                }
                //            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                //foreach (object _obj in _query)
                //{
                //    var _row = _obj.Template(new { CompanyId = this._guid, Name = this._string, Logo = this._string, Address = this._string, CompanyTag = this._string, @default = this._boolean });

                //    _result.Add(new master_Company(_row.CompanyId, _row.Name, _row.Logo, _row.Address, _row.CompanyTag, Convert.ToBoolean(_row.@default)));
                //}
                var _query = (
                                from _company in this.db.master_Companies
                                select _company
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

        public List<master_Company> GetListForDDL()
        {
            List<master_Company> _result = new List<master_Company>();

            try
            {
                var _query = (
                                from _company in this.db.master_Companies
                                select new
                                {
                                    CompanyId = _company.CompanyID,
                                    Name = _company.Name
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { CompanyId = this._guid, Name = this._string });

                    _result.Add(new master_Company(_row.CompanyId, _row.Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public master_Company GetSingle(string _prmCode)
        {
            master_Company _result = null;

            try
            {
                _result = this.db.master_Companies.Single(_temp => _temp.CompanyID == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public master_Company GetSingleDefault()
        {
            master_Company _result = null;

            try
            {
                _result = this.db.master_Companies.Single(_temp => _temp.@default == true);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _company in this.db.master_Companies
                                where _company.CompanyID == new Guid(_prmCode)
                                select new
                                {
                                    Name = _company.Name
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.Name;
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
                using (TransactionScope _scope = new TransactionScope())
                {
                    for (int i = 0; i < _prmCode.Length; i++)
                    {
                        var _queryDatabase = (
                                                from _companyCompany in this.db.master_Company_master_Databases
                                                where _companyCompany.CompanyID == new Guid(_prmCode[i])
                                                select _companyCompany
                                             );
                        this.db.master_Company_master_Databases.DeleteAllOnSubmit(_queryDatabase);

                        var _queryUser = (
                                            from _companyUser in this.db.master_Company_aspnet_Users
                                            where _companyUser.CompanyID == new Guid(_prmCode[i])
                                            select _companyUser
                                         );
                        this.db.master_Company_aspnet_Users.DeleteAllOnSubmit(_queryUser);

                        var _queryRole = (
                                            from _companyRole in this.db.master_Company_aspnet_Roles
                                            where _companyRole.CompanyID == new Guid(_prmCode[i])
                                            select _companyRole
                                         );
                        this.db.master_Company_aspnet_Roles.DeleteAllOnSubmit(_queryRole);


                        master_Company _company = this.db.master_Companies.Single(_temp => _temp.CompanyID.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                        this.db.master_Companies.DeleteOnSubmit(_company);
                    }

                    this.db.SubmitChanges();

                    _scope.Complete();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(master_Company _prmMaster_Company)
        {
            bool _result = false;
            //master_Company _master_Company = null;

            try
            {
                //if (_prmMaster_Company.@default == true)
                //{
                //    _master_Company = this.dbMembership.master_Companies.SingleOrDefault(_temp => _temp.@default == true);
                //    if (_master_Company != null)
                //    {
                //        _master_Company.@default = false;
                //    }
                //}

                this.db.master_Companies.InsertOnSubmit(_prmMaster_Company);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(master_Company _prmMaster_Company)
        {
            bool _result = false;

            try
            {
                //if (_prmMaster_Company.@default == true)
                //{
                //    master_Company _master_Company = null;
                //    _master_Company = this.dbMembership.master_Companies.SingleOrDefault(_temp => _temp.@default == true);
                //    if (_master_Company != null)
                //    {
                //        _master_Company.@default = false;
                //    }
                //}

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public master_Company GetCompanyIDForPOS()
        {
            master_Company _result = null;

            try
            {
                _result = this.dbMembership.master_Companies.SingleOrDefault(_temp => _temp.@default == true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return _result;
        }

        #endregion

        #region master_Company_master_Database

        public int RowsCountCompanyDatabase(Guid _prmCode)
        {
            int _result = 0;

            var _query = (
                                 from _CompanyDatabase in this.db.master_Company_master_Databases
                                 where _CompanyDatabase.CompanyID == _prmCode
                                 select new
                                 {
                                     DatabaseID = _CompanyDatabase.DatabaseID
                                 }
                             ).Count();

            _result = _query;

            return _result;
        }

        public List<master_Database> GetListDatabaseByCompany(Guid _prmCompID)
        {
            List<master_Database> _result = new List<master_Database>();

            try
            {
                var _query = from _companyDatabase in this.db.master_Company_master_Databases
                             join _database in this.db.master_Databases
                             on _companyDatabase.DatabaseID equals _database.DatabaseID
                             where _companyDatabase.CompanyID == _prmCompID
                             select new
                             {
                                 DatabaseID = _companyDatabase.DatabaseID,
                                 Name = _database.Name,
                                 Status = _database.Status
                             };

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { DatabaseID = this._guid, Name = this._string, Status = this._byte });

                    _result.Add(new master_Database(_row.DatabaseID, _row.Name, _row.Status));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<master_Company_master_Database> GetListCompanyDatabase(Guid _prmCompanyID)
        {
            List<master_Company_master_Database> _result = new List<master_Company_master_Database>();

            try
            {
                var _query = this.db.master_Company_master_Databases.Where(_temp => _temp.CompanyID == _prmCompanyID);

                foreach (var _item in _query)
                {
                    _result.Add(_item);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<master_Company_master_Database> GetListCompanyDatabase(int _prmReqPage, int _prmPageSize, Guid _prmCompID)
        {
            List<master_Company_master_Database> _result = new List<master_Company_master_Database>();

            try
            {
                var _query = (
                                from _companyDatabase in this.db.master_Company_master_Databases
                                join _database in this.db.master_Databases
                                on _companyDatabase.DatabaseID equals _database.DatabaseID
                                where _companyDatabase.CompanyID == _prmCompID
                                select new
                                {
                                    CompanyID = _companyDatabase.CompanyID,
                                    DatabaseID = _companyDatabase.DatabaseID,
                                    Name = _database.Name,
                                    Status = _database.Status
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { CompanyID = this._guid, DatabaseID = this._guid, Name = this._string, Status = this._byte });

                    _result.Add(new master_Company_master_Database(_row.CompanyID, _row.DatabaseID, _row.Name, _row.Status));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<master_Company_master_Database> GetListCompanyDatabase()
        {
            List<master_Company_master_Database> _result = new List<master_Company_master_Database>();

            try
            {
                var _query = (
                                from _companyDatabase in this.db.master_Company_master_Databases
                                join _database in this.db.master_Databases
                                on _companyDatabase.DatabaseID equals _database.DatabaseID
                                select new
                                {
                                    CompanyID = _companyDatabase.CompanyID,
                                    DatabaseID = _companyDatabase.DatabaseID,
                                    Name = _database.Name
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { CompanyId = this._guid, DatabaseID = this._guid, Name = this._string });

                    _result.Add(new master_Company_master_Database(_row.CompanyId, _row.DatabaseID, _row.Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public master_Company_master_Database GetSingleCompanyDatabase(string _prmCompanyID, string _prmDatabaseID)
        {
            master_Company_master_Database _result = null;

            try
            {
                _result = this.db.master_Company_master_Databases.Single(_temp => _temp.CompanyID == new Guid(_prmCompanyID) && _temp.DatabaseID == new Guid(_prmDatabaseID));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiCompanyDatabase(string[] _prmDatabaseID, string _prmCompanyID)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmDatabaseID.Length; i++)
                {
                    master_Company_master_Database _companyDatabase = this.db.master_Company_master_Databases.Single(_temp => _temp.DatabaseID.ToString().Trim().ToLower() == _prmDatabaseID[i].Trim().ToLower() && _temp.CompanyID.ToString().Trim().ToLower() == _prmCompanyID.Trim().ToLower());

                    this.db.master_Company_master_Databases.DeleteOnSubmit(_companyDatabase);
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

        public bool AddCompanyDatabase(master_Company_master_Database _prmMaster_Company_master_Database)
        {
            bool _result = false;

            try
            {
                this.db.master_Company_master_Databases.InsertOnSubmit(_prmMaster_Company_master_Database);
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

        #region master_Company_aspnet_User

        public int RowsCountCompanyUser(Guid _prmCode)
        {
            int _result = 0;

            var _query = (
                                 from _CompanyUser in this.db.master_Company_aspnet_Users
                                 where _CompanyUser.CompanyID == _prmCode
                                 select new
                                 {
                                     UserID = _CompanyUser.UserID
                                 }
                             ).Count();

            _result = _query;

            return _result;
        }

        public List<master_Company_aspnet_User> GetListCompanyUser(int _prmReqPage, int _prmPageSize, Guid _prmCompID)
        {
            List<master_Company_aspnet_User> _result = new List<master_Company_aspnet_User>();

            try
            {
                var _query = (
                                from _CompanyUser in this.db.master_Company_aspnet_Users
                                join _user in this.db.aspnet_Users
                                    on _CompanyUser.UserID equals _user.UserId
                                where _CompanyUser.CompanyID == _prmCompID
                                orderby _user.UserName ascending
                                select new
                                {
                                    CompanyID = _CompanyUser.CompanyID,
                                    UserID = _CompanyUser.UserID,
                                    Name = _user.UserName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { CompanyID = this._guid, UserID = this._guid, Name = this._string });

                    _result.Add(new master_Company_aspnet_User(_row.CompanyID, _row.UserID, _row.Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<master_Company_aspnet_User> GetListCompanyUser()
        {
            List<master_Company_aspnet_User> _result = new List<master_Company_aspnet_User>();

            try
            {
                var _query = (
                                from _CompanyUser in this.db.master_Company_aspnet_Users
                                join _user in this.db.aspnet_Users
                                on _CompanyUser.UserID equals _user.UserId
                                select new
                                {
                                    CompanyID = _CompanyUser.CompanyID,
                                    UserID = _CompanyUser.UserID,
                                    Name = _user.UserName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { CompanyId = this._guid, UserID = this._guid, Name = this._string });

                    _result.Add(new master_Company_aspnet_User(_row.CompanyId, _row.UserID, _row.Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public master_Company_aspnet_User GetSingleCompanyUser(string _prmCompanyID, Guid _prmUserID)
        {
            master_Company_aspnet_User _result = null;

            try
            {
                _result = this.db.master_Company_aspnet_Users.Single(_temp => _temp.CompanyID == new Guid(_prmCompanyID) && _temp.UserID == _prmUserID);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiCompanyUser(string[] _prmUserID, string _prmCompanyID)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmUserID.Length; i++)
                {
                    var _queryDatabaseUser =
                            (
                                from _dtUser in this.db.master_Database_aspnet_Users
                                where _dtUser.UserID == new Guid(_prmUserID[i])
                                select _dtUser
                            );
                    this.db.master_Database_aspnet_Users.DeleteAllOnSubmit(_queryDatabaseUser);

                    master_Company_aspnet_User _CompanyUser = this.db.master_Company_aspnet_Users.Single(_temp => _temp.UserID.ToString().Trim().ToLower() == _prmUserID[i].Trim().ToLower() && _temp.CompanyID.ToString().Trim().ToLower() == _prmCompanyID.Trim().ToLower());
                    this.db.master_Company_aspnet_Users.DeleteOnSubmit(_CompanyUser);
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

        public bool AddCompanyUser(master_Company_aspnet_User _prmCompanyUser, List<master_Database_aspnet_User> _prmDatabaseUser)
        {
            bool _result = false;

            try
            {
                this.db.master_Company_aspnet_Users.InsertOnSubmit(_prmCompanyUser);

                List<master_Company_master_Database> _companyDbList = this.GetListCompanyDatabase(_prmCompanyUser.CompanyID);

                foreach (var _obj in _companyDbList)
                {
                    var _dbUserListOld = this.db.master_Database_aspnet_Users.Where(_temp => _temp.UserID == _prmCompanyUser.UserID && _temp.DatabaseID == _obj.DatabaseID);

                    foreach (var _item in _dbUserListOld)
                    {
                        this.db.master_Database_aspnet_Users.DeleteOnSubmit(_item);
                    }
                }

                this.db.master_Database_aspnet_Users.InsertAllOnSubmit(_prmDatabaseUser);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditCompanyUser(List<master_Database_aspnet_User> _prmDatabaseUserList, Guid _prmCompanyID, Guid _prmUserID)
        {
            bool _result = false;

            try
            {
                List<master_Company_master_Database> _companyDbList = this.GetListCompanyDatabase(_prmCompanyID);

                foreach (var _obj in _companyDbList)
                {
                    var _dbUserListOld = this.db.master_Database_aspnet_Users.Where(_temp => _temp.UserID == _prmUserID && _temp.DatabaseID == _obj.DatabaseID);

                    foreach (var _item in _dbUserListOld)
                    {
                        this.db.master_Database_aspnet_Users.DeleteOnSubmit(_item);
                    }
                }

                this.db.master_Database_aspnet_Users.InsertAllOnSubmit(_prmDatabaseUserList);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsUserCompExists(string _prmUserCompId)
        {
            bool _result = false;

            try
            {
                var _query = from _userComp in this.db.master_Company_aspnet_Users
                             where (_userComp.UserID == new Guid(_prmUserCompId))
                             select new
                             {
                                 _userComp.UserID
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

        #region master_Company_aspnet_Role

        public int RowsCountCompanyRole(Guid _prmCode)
        {
            int _result = 0;

            var _query = (
                                 from _CompanyRole in this.db.master_Company_aspnet_Roles
                                 where _CompanyRole.CompanyID == _prmCode
                                 select new
                                 {
                                     RoleId = _CompanyRole.RoleId
                                 }
                             ).Count();

            _result = _query;

            return _result;
        }

        public List<master_Company_aspnet_Role> GetListCompanyRole(int _prmReqPage, int _prmPageSize, Guid _prmCompID)
        {
            List<master_Company_aspnet_Role> _result = new List<master_Company_aspnet_Role>();

            try
            {
                var _query = (
                                from _CompanyRole in this.db.master_Company_aspnet_Roles
                                join _role in this.db.aspnet_Roles
                                    on _CompanyRole.RoleId equals _role.RoleId
                                where _CompanyRole.CompanyID == _prmCompID
                                orderby _role.RoleName ascending
                                select new
                                {
                                    CompanyID = _CompanyRole.CompanyID,
                                    RoleId = _CompanyRole.RoleId,
                                    Name = _role.RoleName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { CompanyID = this._guid, RoleId = this._guid, Name = this._string });

                    _result.Add(new master_Company_aspnet_Role(_row.CompanyID, _row.RoleId, _row.Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<master_Company_aspnet_Role> GetListCompanyRole()
        {
            List<master_Company_aspnet_Role> _result = new List<master_Company_aspnet_Role>();

            try
            {
                var _query = (
                                from _CompanyRole in this.db.master_Company_aspnet_Roles
                                join _role in this.db.aspnet_Roles
                                on _CompanyRole.RoleId equals _role.RoleId
                                select new
                                {
                                    CompanyID = _CompanyRole.CompanyID,
                                    RoleId = _CompanyRole.RoleId,
                                    Name = _role.RoleName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { CompanyId = this._guid, RoleId = this._guid, Name = this._string });

                    _result.Add(new master_Company_aspnet_Role(_row.CompanyId, _row.RoleId, _row.Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<master_Company_aspnet_Role> GetListCompanyRole(Guid _prmCompanyID)
        {
            List<master_Company_aspnet_Role> _result = new List<master_Company_aspnet_Role>();

            try
            {
                var _query = (
                                from _CompanyRole in this.db.master_Company_aspnet_Roles
                                where _CompanyRole.CompanyID == _prmCompanyID
                                select new
                                {
                                    CompanyID = _CompanyRole.CompanyID,
                                    RoleId = _CompanyRole.RoleId
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new master_Company_aspnet_Role(_row.CompanyID, _row.RoleId));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public master_Company_aspnet_Role GetSingleCompanyRole(Guid _prmCompanyID)
        {
            master_Company_aspnet_Role _result = null;

            try
            {
                _result = this.db.master_Company_aspnet_Roles.Single(_temp => _temp.CompanyID == _prmCompanyID);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiCompanyRole(string[] _prmRoleID, string _prmCompanyID)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmRoleID.Length; i++)
                {
                    master_Company_aspnet_Role _CompanyUser = this.db.master_Company_aspnet_Roles.Single(_temp => _temp.RoleId.ToString().Trim().ToLower() == _prmRoleID[i].Trim().ToLower() && _temp.CompanyID.ToString().Trim().ToLower() == _prmCompanyID.Trim().ToLower());

                    this.db.master_Company_aspnet_Roles.DeleteOnSubmit(_CompanyUser);
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

        public bool AddRole(master_Company_aspnet_Role _prmMasterCompanyRole, string _prmRoleID)
        {
            bool _result = false;

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    var _query = (
                                    from _masterCompanyRole in this.db.master_Company_aspnet_Roles
                                    where _masterCompanyRole.CompanyID == _prmMasterCompanyRole.CompanyID
                                    select _masterCompanyRole
                                 );
                    this.db.master_Company_aspnet_Roles.DeleteAllOnSubmit(_query);

                    string[] _role = null;
                    _role = _prmRoleID.Split(',');

                    List<master_Company_aspnet_Role> _listCompanyRole = new List<master_Company_aspnet_Role>();

                    for (int i = 0; i < _role.Length; i++)
                    {
                        //_prmMasterCompanyRole.RoleId = new Guid(_role[i]);
                        _listCompanyRole.Add(new master_Company_aspnet_Role(_prmMasterCompanyRole.CompanyID, new Guid(_role[i])));
                    }

                    this.db.master_Company_aspnet_Roles.InsertAllOnSubmit(_listCompanyRole);

                    this.db.SubmitChanges();

                    _scope.Complete();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public bool EditRole(master_Company_aspnet_Role _prmMasterCompanyRole, String _prmRoleID)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        using (TransactionScope _scope = new TransactionScope())
        //        {
        //            var _query = (
        //                            from _masterCompanyRole in this.db.master_Company_aspnet_Roles
        //                            where _masterCompanyRole.CompanyID == _prmMasterCompanyRole.CompanyID
        //                            select _masterCompanyRole
        //                         );
        //            this.db.master_Company_aspnet_Roles.DeleteAllOnSubmit(_query);

        //            string[] _role = null;
        //            _role = _prmRoleID.Split(',');

        //            List<master_Company_aspnet_Role> _listCompanyRole = new List<master_Company_aspnet_Role>();

        //            for (int i = 0; i < _role.Length; i++)
        //            {
        //                //_prmMasterCompanyRole.RoleId = new Guid(_role[i]);
        //                _listCompanyRole.Add(new master_Company_aspnet_Role(_prmMasterCompanyRole.CompanyID, new Guid(_role[i])));
        //            }

        //            this.db.master_Company_aspnet_Roles.InsertAllOnSubmit(_listCompanyRole);

        //            _scope.Complete();

        //            this.db.SubmitChanges();

        //            _result = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool IsCompRoleExists(string _prmCompId, string _prmRoleId)
        {
            bool _result = false;

            try
            {
                var _query = from _compRole in this.db.master_Company_aspnet_Roles
                             where (_compRole.CompanyID == new Guid(_prmCompId) && _compRole.RoleId == new Guid(_prmRoleId))
                             select new
                             {
                                 _compRole.RoleId
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

        #region master_Database_aspnet_User

        public Boolean GetSingleDatabaseUser(Guid _prmDatabaseID, Guid _prmUserID)
        {
            Boolean _result = false;

            try
            {
                master_Database_aspnet_User _dbAndUser = this.db.master_Database_aspnet_Users.Single(_temp => _temp.DatabaseID == _prmDatabaseID && _temp.UserID == _prmUserID);

                if (_dbAndUser != null)
                    _result = true;
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public List<master_Database_aspnet_User> GetListDatabaseUser(Guid _prmUserID)
        {
            List<master_Database_aspnet_User> _result = new List<master_Database_aspnet_User>();

            try
            {
                var _query = this.db.master_Database_aspnet_Users.Where(_temp => _temp.UserID == _prmUserID);

                foreach (var _item in _query)
                {
                    _result.Add(_item);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsUserDatabaseExists(string _prmUserDatabaseId)
        {
            bool _result = false;

            try
            {
                var _query = from _userDatabase in this.db.master_Database_aspnet_Users
                             where (_userDatabase.UserID == new Guid(_prmUserDatabaseId))
                             select new
                             {
                                 _userDatabase.UserID
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

        #region _Company_reportList
        public int RowsCountReportList(string _prmCategory, string _prmKeyword, Guid _prmCompanyID)
        {
            int _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "ReportGroup")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "ReportName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                _result = (
                        from _masterReportList in this.db.master_ReportLists
                        where _masterReportList.CompanyID == _prmCompanyID
                        && (SqlMethods.Like(_masterReportList.ReportGroupID.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_masterReportList.ReportName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            && _masterReportList.ReportType == 1
                        select _masterReportList
                    ).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public int RowsCountPrintPreview(string _prmCategory, string _prmKeyword, Guid _prmCompanyID)
        {
            int _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "ReportGroup")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }

            try
            {
                _result = (
                        from _masterReportList in this.db.master_ReportLists
                        where _masterReportList.CompanyID == _prmCompanyID
                        && (SqlMethods.Like(_masterReportList.ReportGroupID.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            && _masterReportList.ReportType == 2
                        select _masterReportList.ReportGroupID
                    ).Distinct().Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<master_ReportList> GetListReportList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, Guid _prmCompanyID)
        {
            List<master_ReportList> _result = new List<master_ReportList>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "ReportGroup")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "ReportName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _masterReportList in this.db.master_ReportLists
                                where _masterReportList.ReportType == 1
                                && (SqlMethods.Like(_masterReportList.ReportGroupID.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_masterReportList.ReportName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _masterReportList.CompanyID == _prmCompanyID
                                orderby _masterReportList.ReportGroupID, _masterReportList.SortNo
                                select _masterReportList
                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
                foreach (var _rs in _query)
                {
                    _result.Add(_rs);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public List<master_ReportList> GetListReportList(int _prmReqPage, int _prmPageSize, Guid _prmCompanyID)
        //{
        //    List<master_ReportList> _result = new List<master_ReportList>();
        //    try
        //    {
        //        var _qry = (
        //                from _masterReportList in this.db.master_ReportLists
        //                where _masterReportList.ReportType == 1
        //                    && _masterReportList.CompanyID == _prmCompanyID
        //                orderby _masterReportList.ReportGroupID, _masterReportList.SortNo
        //                select _masterReportList
        //            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
        //        foreach (var _rs in _qry)
        //        {
        //            _result.Add(_rs);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return _result;
        //}

        public List<String> GetListPrintPreview(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, Guid _prmCompanyID)
        {
            List<String> _result = new List<String>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "ReportGroup")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }

            try
            {
                var _qry = (
                        from _masterReportList in this.db.master_ReportLists
                        where _masterReportList.ReportType == 2
                        && (SqlMethods.Like(_masterReportList.ReportGroupID.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            && _masterReportList.CompanyID == _prmCompanyID
                        orderby _masterReportList.ReportGroupID, _masterReportList.SortNo
                        select _masterReportList.ReportGroupID
                    ).Distinct().Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
                foreach (var _rs in _qry)
                {
                    _result.Add(_rs);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean UpdateReportTemplate(Guid _prmCompanyId)
        {
            Boolean _result = false;
            try
            {
                var _qry = (
                        from _masterListTemplate in this.db.master_ReportListTemplates
                        where
                            !(
                                from _msRList in this.db.master_ReportLists
                                where _msRList.CompanyID == _prmCompanyId
                                select _msRList.ReportType.ToString() + "," + _msRList.ReportGroupID.ToString() + "," + _msRList.SortNo.ToString()
                            ).Contains(_masterListTemplate.ReportType.ToString() + "," + _masterListTemplate.ReportGroupID.ToString() + "," + _masterListTemplate.SortNo.ToString())
                        select _masterListTemplate
                    );
                foreach (var _rs in _qry)
                {
                    if ((
                        from _masterReportList in this.db.master_ReportLists
                        where _masterReportList.CompanyID == _prmCompanyId
                            && _masterReportList.ReportGroupID == _rs.ReportGroupID
                            && _masterReportList.ReportName == _rs.ReportName
                        select _masterReportList
                            ).Count() == 0)
                    {
                        master_ReportList _addData = new master_ReportList();
                        _addData.ReportType = _rs.ReportType;
                        _addData.ReportGroupID = _rs.ReportGroupID;
                        _addData.SortNo = _rs.SortNo;
                        _addData.ReportName = _rs.ReportName;
                        _addData.ReportPath = _rs.ReportPath;
                        _addData.fgActive = _rs.fgActive;
                        _addData.Enabled = Convert.ToBoolean(_rs.fgActive);
                        _addData.CompanyID = _prmCompanyId;
                        this.db.master_ReportLists.InsertOnSubmit(_addData);
                    }
                }
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        #endregion

        #region _Company_EditMenu

        public List<master_Menu> GetListTopMenu()
        {
            List<master_Menu> _result = new List<master_Menu>();
            try
            {
                var _query = (
                                from _menu in this.dbMembership.master_Menus
                                where (_menu.ParentID == 0)
                                    && (_menu.IsActive == true)
                                orderby _menu.Priority ascending
                                select new
                                {
                                    MenuID = _menu.MenuID,
                                    Text = _menu.Text
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new master_Menu(_row.MenuID, _row.Text));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<master_CompanyMenu> GetListCompanyMenu(Guid _prmCompID)
        {
            List<master_CompanyMenu> _result = new List<master_CompanyMenu>();

            try
            {
                var _query = (
                                from _companyMenu in this.db.master_CompanyMenus
                                where _companyMenu.CompanyId == _prmCompID
                                select _companyMenu
                            );

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

        public bool EditCompanyMenu(String _prmCompId, String _prmMenuList)
        {
            bool _result = false;

            try
            {
                var _queryDelete = (
                                        from _compMenu in this.db.master_CompanyMenus
                                        where _compMenu.CompanyId == new Guid(_prmCompId)
                                        select _compMenu
                                   );

                this.db.master_CompanyMenus.DeleteAllOnSubmit(_queryDelete);

                if (_prmMenuList != "")
                {
                    String[] _menuID = _prmMenuList.Split(',');
                    List<master_CompanyMenu> _compMenuList = new List<master_CompanyMenu>();
                    foreach (var _item in _menuID)
                    {
                        master_CompanyMenu _copmMenu = new master_CompanyMenu();
                        _copmMenu.CompanyId = new Guid(_prmCompId);
                        _copmMenu.MenuId = (_item == "") ? Convert.ToInt16(0) : Convert.ToInt16(_item);

                        _compMenuList.Add(_copmMenu);
                    }
                    this.db.master_CompanyMenus.InsertAllOnSubmit(_compMenuList);
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

        ~CompanyBL()
        {
        }
    }
}
