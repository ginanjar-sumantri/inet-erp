using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.Authentication;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Diagnostics;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Data.Linq.SqlClient;
using System.Transactions;
using System.Web.Security;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;




namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Settings
{
    public sealed class UserBL : Base
    {
        public UserBL()
        {
        }

        #region User

        public double RowsCount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                           from _aspnetUser in this.db.aspnet_Users
                           where (SqlMethods.Like(_aspnetUser.UserName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           select _aspnetUser.UserId
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<aspnet_User> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<aspnet_User> _result = new List<aspnet_User>();

            string _pattern1 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (from _msUser in this.db.aspnet_Users
                              join _msMembership in this.db.aspnet_Memberships
                                    on _msUser.UserId equals _msMembership.UserId
                              where (SqlMethods.Like(_msUser.UserName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              orderby _msUser.LastActivityDate descending
                              select new
                              {
                                  UserId = _msUser.UserId,
                                  UserName = _msUser.UserName,
                                  Email = _msMembership.Email,
                                  LastLoginDate = _msMembership.LastLoginDate,
                                  IsLockedOut = _msMembership.IsLockedOut
                              }).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new aspnet_User(_row.UserId, _row.UserName, _row.Email, _row.LastLoginDate, _row.IsLockedOut));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<aspnet_User> GetList()
        {
            List<aspnet_User> _result = new List<aspnet_User>();

            try
            {
                var _query = (
                                from _msUser in this.db.aspnet_Users
                                orderby _msUser.LastLoginDate descending
                                select new
                                {
                                    UserId = _msUser.UserId,
                                    UserName = _msUser.UserName,
                                    Email = _msUser.Email,
                                    LastLoginDate = _msUser.LastLoginDate
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { UserId = this._guid, UserName = this._string, Email = this._string, LastLoginDate = this._datetime });

                    _result.Add(new aspnet_User(_row.UserId, _row.UserName, _row.Email, _row.LastLoginDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public aspnet_User GetSingle(string _prmCode)
        {
            aspnet_User _result = null;

            try
            {
                _result = this.db.aspnet_Users.Single(_temp => _temp.UserId == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public aspnet_Membership GetSingleMembership(string _prmCode)
        {
            aspnet_Membership _result = null;

            try
            {
                _result = this.db.aspnet_Memberships.Single(_temp => _temp.UserId == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<aspnet_User> GetListForDDL(Guid _prmCompID)
        {
            List<aspnet_User> _result = new List<aspnet_User>();

            try
            {
                var _query = (
                                from _user in this.db.aspnet_Users
                                where !(
                                            from _companyUser in this.db.master_Company_aspnet_Users
                                            where _companyUser.CompanyID == _prmCompID
                                            select _companyUser.UserID
                                        ).Contains(_user.UserId)
                                select new
                                {
                                    UserID = _user.UserId,
                                    Name = _user.UserName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { UserID = this._guid, Name = this._string });

                    _result.Add(new aspnet_User(_row.UserID, _row.Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public List<aspnet_User> GetListUserForDDL(Guid _prmCompID)
        //{
        //    List<aspnet_User> _result = new List<aspnet_User>();

        //    try
        //    {
        //        var _query = (
        //                        from _user in this.db.aspnet_Users
        //                        join _companyUser in this.db.master_Company_aspnet_Users
        //                            on _user.UserId equals _companyUser.UserID
        //                        where _companyUser.CompanyID == _prmCompID 
        //                            && !(
        //                                    from _userPermission in this.db.master_UserPermissions
        //                                    where _user.UserId == _userPermission.UserID
        //                                    select _userPermission.UserID
        //                                ).Contains(_user.UserId)
        //                        select new
        //                        {
        //                            UserID = _user.UserId,
        //                            Name = _user.UserName
        //                        }
        //                    );

        //        foreach (object _obj in _query)
        //        {
        //            var _row = _obj.Template(new { UserID = this._guid, Name = this._string });

        //            _result.Add(new aspnet_User(_row.UserID, _row.Name));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public aspnet_User GetSingleForEdit(string _prmCode)
        {
            aspnet_User _result = new aspnet_User();

            try
            {
                var _query = (
                                from _msUser in this.db.aspnet_Users
                                join _msMembership in this.db.aspnet_Memberships
                                on _msUser.UserId equals _msMembership.UserId
                                where _msUser.UserId == new Guid(_prmCode)
                                select new
                                {
                                    UserId = _msUser.UserId,
                                    Password = _msMembership.Password,
                                    UserName = _msUser.UserName,
                                    Email = _msMembership.Email,
                                    PasswordQuestion = _msMembership.PasswordQuestion,
                                    PasswordAnswer = _msMembership.PasswordAnswer,
                                    LastLogin = _msMembership.LastLoginDate,
                                    IsLockedOut = _msMembership.IsLockedOut
                                }
                              ).Single();

                _result.UserId = _query.UserId;
                _result.UserName = _query.UserName;
                _result.Password = _query.Password;
                _result.Email = _query.Email;
                _result.PasswordQuestion = _query.PasswordQuestion;
                _result.PasswordAnswer = _query.PasswordAnswer;
                _result.LastLoginDate = _query.LastLogin;
                _result.IsLockedOut = _query.IsLockedOut;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetUserNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msUser in this.db.aspnet_Users
                                where _msUser.UserId == new Guid(_prmCode)
                                select new
                                {
                                    UserName = _msUser.UserName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.UserName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetQuestionByUserId(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msUser in this.db.aspnet_Memberships
                                where _msUser.UserId == new Guid(_prmCode)
                                select new
                                {
                                    PasswordQuestion = _msUser.PasswordQuestion
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.PasswordQuestion;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAnswerByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msUser in this.db.aspnet_Users
                                where _msUser.UserId == new Guid(_prmCode)
                                select new
                                {
                                    PasswordAnswer = _msUser.PasswordAnswer
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.PasswordAnswer;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //for add/edit user
        public string GetUserIdByUserName(string _prmName)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msUser in this.db.aspnet_Users
                                where _msUser.UserName == _prmName
                                select new
                                {
                                    UserId = _msUser.UserId
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.UserId.ToString();
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
                    string _userName = this.GetUserNameByCode(_prmCode[i]);
                    MembershipService _service = new MembershipService();

                    //delete data in membership.master.UserConnMode                  
                    var _userConn = (
                                        from _userConnMode in this.db.master_UserConnModes
                                        where _userConnMode.UserId == new Guid(_prmCode[i])
                                        select _userConnMode
                                     );
                    if (_userConn.Count() > 0)
                    {
                        this.db.master_UserConnModes.DeleteAllOnSubmit(_userConn);
                    }

                    //delete data in membership.user_employee                    
                    var _userEmp = (
                                    from _userEmployee in this.db.User_Employees
                                    where _userEmployee.UserId.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                    select _userEmployee
                                   );
                    if (_userEmp.Count() > 0)
                    {
                        this.db.User_Employees.DeleteAllOnSubmit(_userEmp);
                    }

                    //delete data in membership.master.Company_aspnet.User                    
                    var _msCompUser = (
                                    from _compUser in this.db.master_Company_aspnet_Users
                                    where _compUser.UserID == new Guid(_prmCode[i])
                                    select _compUser
                                 );
                    if (_msCompUser.Count() > 0)
                    {
                        this.db.master_Company_aspnet_Users.DeleteAllOnSubmit(_msCompUser);
                    }

                    //delete data in membership.master.Database_aspnet.User                    
                    var _msDbUser = (
                                    from _databaseUser in this.db.master_Database_aspnet_Users
                                    where _databaseUser.UserID == new Guid(_prmCode[i])
                                    select _databaseUser
                                 );
                    if (_msCompUser.Count() > 0)
                    {
                        this.db.master_Database_aspnet_Users.DeleteAllOnSubmit(_msDbUser);
                    }

                    // delete master_userExtension
                    master_UserExtension _userExtension = this.db.master_UserExtensions.Single(a => a.UserId == new Guid(_prmCode[i]));
                    this.db.master_UserExtensions.DeleteOnSubmit(_userExtension);

                    // delete roles
                    string[] _tempDelRole;
                    _tempDelRole = _service.GetRolesForUser(_userName);
                    bool _resultDelRole = _service.RemoveUserFromRoles(_userName, _tempDelRole);
                }

                this.db.SubmitChanges();

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string _userName = this.GetUserNameByCode(_prmCode[i]);
                    MembershipService _service = new MembershipService();

                    // delete user
                    _service.DeleteUser(_userName, true);
                }

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(aspnet_User _prmAspnet_User)
        {
            bool _result = false;

            try
            {
                this.db.aspnet_Users.InsertOnSubmit(_prmAspnet_User);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(aspnet_User _prmAspnet_User)
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

        public Guid GetUserIDByName(String _prmUserName)
        {
            Guid _result = new Guid();

            try
            {
                _result = this.db.aspnet_Users.Single(_user => _user.UserName == _prmUserName).UserId;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditRoles(String _prmUserName, String _prmTempRole)
        {
            MembershipService _serviceBL = new MembershipService();
            bool _result = false;

            try
            {
                string[] _tempDelRole;
                bool _resultDelRole = true;
                bool _resultRole = true;

                _tempDelRole = _serviceBL.GetRolesForUser(_prmUserName);

                if (_tempDelRole.Length > 0)
                {
                    _resultDelRole = _serviceBL.RemoveUserFromRoles(_prmUserName, _tempDelRole);
                }

                if (_resultDelRole == true)
                {
                    string[] _addrole;
                    // cek role kosong atau tidak (kalau ada isi jalankan AssignUserToRoles)
                    if (_prmTempRole != "")
                    {
                        _addrole = _prmTempRole.Split(',');

                        _resultRole = _serviceBL.AssignUserToRoles(_prmUserName, _addrole);
                    }
                }

                if (_tempDelRole.Length > 0)
                {
                    if (_resultDelRole == true & _resultRole == true)
                    {
                        _result = true;
                    }
                }
                else
                {
                    if (_resultRole == true)
                    {
                        _result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        public Boolean SaveLastConnectionMode(String _prmUserName, String _prmCompID, String _prmDatabaseID)
        {
            Boolean _result = false;
            Boolean _isExists = false;
            UserBL _user = new UserBL();

            try
            {
                Guid _userID = _user.GetUserIDByName(_prmUserName);

                var _query = from _userConnMode in this.db.master_UserConnModes
                             where _userConnMode.UserId == _userID
                             select _userConnMode;

                foreach (var _item in _query)
                {
                    _isExists = true;

                    master_UserConnMode _userCM = this.db.master_UserConnModes.Single(_userConnMode => _userConnMode.UserId == _userID);

                    _userCM.CompanyID = new Guid(_prmCompID);
                    _userCM.DatabaseID = new Guid(_prmDatabaseID);
                }

                if (_isExists == false)
                {
                    master_UserConnMode _userCM = new master_UserConnMode();

                    _userCM.UserId = _userID;
                    _userCM.CompanyID = new Guid(_prmCompID);
                    _userCM.DatabaseID = new Guid(_prmDatabaseID);

                    this.db.master_UserConnModes.InsertOnSubmit(_userCM);
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

        public String CompanyLogo(String _prmUserName)
        {
            String _result = "";

            Guid _compID = this.db.master_UserConnModes.Single(_user => _user.UserId == this.GetUserIDByName(_prmUserName)).CompanyID;

            CompanyBL _compBL = new CompanyBL();
            master_Company _msComp = _compBL.GetSingle(_compID.ToString());

            _result = _msComp.Logo;

            return _result;
        }

        public String CompanyAddress(String _prmUserName)
        {
            String _result = "";

            Guid _compID = this.db.master_UserConnModes.Single(_user => _user.UserId == this.GetUserIDByName(_prmUserName)).CompanyID;

            CompanyBL _compBL = new CompanyBL();
            master_Company _msComp = _compBL.GetSingle(_compID.ToString());

            _result = _msComp.PrimaryAddress;

            return _result;
        }

        public String ConnectionCompany(String _prmUserName)
        {
            String _result = "";
            Guid _compID = new Guid();
            master_Company _msComp = new master_Company();
            CompanyBL _compBL = new CompanyBL();

            //try
            //{
            _compID = this.db.master_UserConnModes.Single(_user => _user.UserId == this.GetUserIDByName(_prmUserName)).CompanyID;
            _msComp = _compBL.GetSingle(_compID.ToString());

            _result = _msComp.Name;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception((_msComp == null) ? "Y" : "N" + _result + " " + this.db.Connection.ConnectionString + " " + _compID + " " + _prmUserName);
            //}

            return _result;
        }

        public Guid ConnectionCompanyID(String _prmUserName)
        {
            Guid _compID = new Guid();
            master_Company _msComp = new master_Company();
            CompanyBL _compBL = new CompanyBL();

            _compID = this.db.master_UserConnModes.Single(_user => _user.UserId == this.GetUserIDByName(_prmUserName)).CompanyID;
            _msComp = _compBL.GetSingle(_compID.ToString());

            return _compID;
        }

        public ConnectionType ConnectionMode(String _prmUserName)
        {
            ConnectionType _result = ConnectionType.Development;

            Guid _dbID = this.db.master_UserConnModes.Single(_user => _user.UserId == this.GetUserIDByName(_prmUserName)).DatabaseID;

            DatabaseBL _dbBL = new DatabaseBL();
            master_Database _msDB = _dbBL.GetSingle(_dbID.ToString());

            _result = ConnectionModeMapper.MapThis(_msDB.Status);

            return _result;
        }

        public String ConnectionString(String _prmUserName)
        {
            String _result = "";

            Guid _dbID = this.db.master_UserConnModes.Single(_user => _user.UserId == this.GetUserIDByName(_prmUserName)).DatabaseID;

            DatabaseBL _dbBL = new DatabaseBL();
            master_Database _msDB = _dbBL.GetSingle(_dbID.ToString());

            _result = "Database=" + _msDB.Name + "; Server=" + _msDB.Server + "; UID=" + _msDB.UID + "; PWD=" + Rijndael.Decrypt(_msDB.PWD, ApplicationConfig.PasswordEncryptionKey) + ";";

            //ConnectionType _connMode = this.ConnectionMode(_prmUserName);

            //switch (_connMode)
            //{
            //    case ConnectionType.Development:
            //        //_result = ApplicationConfig.DevelopmentConnString;
            //        break;
            //    case ConnectionType.Testing:
            //        //_result = ApplicationConfig.TestingConnString;
            //        break;
            //    case ConnectionType.Production:
            //        //_result = ApplicationConfig.ProductionConnString;
            //        break;
            //    default:
            //        //_result = ApplicationConfig.DevelopmentConnString;
            //        break;
            //}

            return _result;
        }

        public Guid GetCompanyId(String _prmUserName)
        {
            Guid _result = new Guid();

            master_Company _msComp = new master_Company();
            CompanyBL _compBL = new CompanyBL();

            //try
            //{
            _result = this.db.master_UserConnModes.Single(_user => _user.UserId == this.GetUserIDByName(_prmUserName)).CompanyID;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception((_msComp == null) ? "Y" : "N" + _result + " " + this.db.Connection.ConnectionString + " " + _compID + " " + _prmUserName);
            //}

            return _result;
        }

        public String GetCompanyTag(Guid _prmCompanyId)
        {
            String _result = "";

            try
            {
                _result = this.db.master_Companies.Single(_temp => _temp.CompanyID == _prmCompanyId).CompanyTag;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetCompanyTaxBranchNo(Guid _prmCompanyId)
        {
            String _result = "";

            try
            {
                _result = this.db.master_Companies.Single(_temp => _temp.CompanyID == _prmCompanyId).TaxBranchNo;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean ChangeQuestionAndAnswer(string _prmUserName, string _prmPassword, string _prmQuestion, string _prmAnswer)
        {
            Boolean _result = false;

            try
            {
                MembershipService _membershipService = new MembershipService();

                _result = _membershipService.ChangePasswordQuestionAndAnswer(_prmUserName, _prmPassword, _prmQuestion, _prmAnswer);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean ChangePassword(string _prmUserName, string _prmOldPassword, string _prmNewPassword)
        {
            Boolean _result = false;
            try
            {
                MembershipService _membershipService = new MembershipService();
                _result = _membershipService.ChangePassword(_prmUserName, _prmOldPassword, _prmNewPassword);
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public Boolean ChangePasswordWithoutOld(string _prmUserName, string _prmNewPassword)
        {
            Boolean _result = false;
            try
            {
                MembershipService _membershipService = new MembershipService();
                MembershipUser _member = _membershipService.GetUser(_prmUserName, false);
                _member.UnlockUser();
                _result = _member.ChangePassword(_member.ResetPassword(), _prmNewPassword);
                //_result = _membershipService.ChangePassword(_prmUserName, _member.GetPassword(), _prmNewPassword);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public String getOldPassword(String _prmUserName)
        {
            String _result = "";
            try
            {
                _result = (
                        from _userExt in this.db.master_UserExtensions
                        where _userExt.UserId == GetUserIDByName(_prmUserName)
                        select _userExt.password
                    ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean addPasswordExtension(String _prmUserId, String _prmPassword)
        {
            Boolean _result = false;
            try
            {
                master_UserExtension _addData = new master_UserExtension();
                _addData.UserId = new Guid(_prmUserId);
                _addData.password = _prmPassword;
                this.db.master_UserExtensions.InsertOnSubmit(_addData);
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String GetThemeComponent(ThemeComponent _themeComponentCode, String _prmThemeCode)
        {
            String _result = "";
            switch (_themeComponentCode)
            {
                case ThemeComponent.ThemeName:
                    _result = this.db.master_themes.Single(a => a.ThemeCode == _prmThemeCode).ThemeName;
                    break;
                case ThemeComponent.BackgroundColorBody:
                    _result = this.db.master_themes.Single(a => a.ThemeCode == _prmThemeCode).BackgroundColorBody;
                    break;
                case ThemeComponent.BackgroundImage:
                    _result = this.db.master_themes.Single(a => a.ThemeCode == _prmThemeCode).BackgroundImage;
                    break;
                case ThemeComponent.BackgroundImageBawah:
                    _result = this.db.master_themes.Single(a => a.ThemeCode == _prmThemeCode).BackgroundImageBawah;
                    break;
                case ThemeComponent.RowColor:
                    _result = this.db.master_themes.Single(a => a.ThemeCode == _prmThemeCode).RowColor;
                    break;
                case ThemeComponent.RowColorAlternate:
                    _result = this.db.master_themes.Single(a => a.ThemeCode == _prmThemeCode).RowColorAlternate;
                    break;
                case ThemeComponent.RowColorHover:
                    _result = this.db.master_themes.Single(a => a.ThemeCode == _prmThemeCode).RowColorHover;
                    break;
                case ThemeComponent.WelcomeTextColor:
                    _result = this.db.master_themes.Single(a => a.ThemeCode == _prmThemeCode).WelcomeTextColor;
                    break;
                case ThemeComponent.BackgroundColorLogin:
                    _result = this.db.master_themes.Single(a => a.ThemeCode == _prmThemeCode).BackgroundColorLogin;
                    break;
                case ThemeComponent.BackgroundimageLogin:
                    _result = this.db.master_themes.Single(a => a.ThemeCode == _prmThemeCode).BackgroundImageLogin;
                    break;
                case ThemeComponent.BackgroundImagePanelLogin:
                    _result = this.db.master_themes.Single(a => a.ThemeCode == _prmThemeCode).BackgroundImagePanelLogin;
                    break;
                case ThemeComponent.PanelLoginWidth:
                    _result = this.db.master_themes.Single(a => a.ThemeCode == _prmThemeCode).PanelLoginWidth;
                    break;
                case ThemeComponent.PanelLoginHeight:
                    _result = this.db.master_themes.Single(a => a.ThemeCode == _prmThemeCode).PanelLoginHeight;
                    break;
            }
            return _result;
        }

        public Boolean ReActivateUser(String _prmUserName)
        {
            Boolean _result = false;

            try
            {
                MembershipUser _user = Membership.GetUser(_prmUserName);
                _result = _user.UnlockUser();
            }
            catch (Exception Ex)
            {
                new ErrorLog(Guid.NewGuid(), DateTime.Now, Ex.Message, Ex.StackTrace, _prmUserName, "ReActivateUser", "");
            }

            return _result;
        }

        ~UserBL()
        {
        }
    }
}
