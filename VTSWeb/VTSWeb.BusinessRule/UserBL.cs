using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTSWeb.Database;
using VTSWeb.Common;
using Microsoft.Win32;
using VTSWeb.SystemConfig;
using System.Data.Linq.SqlClient;
using System.Collections;

namespace VTSWeb.BusinessRule
{
    public sealed class UserBL : Base
    {
        public UserBL()
        {
        }
        ~UserBL()
        {
        }

        #region User
        public int RowsCount(String _prmUser, String _prmKeyword)
        {
            int _result = 0;

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmUser == "UserName")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmUser == "Email")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result = (from _msUser in this.db.MsUsers
                       where (SqlMethods.Like(_msUser.UserName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msUser.Email.Trim().ToLower(), _pattern2.Trim().ToLower()))


                       select _msUser.UserName).Count();
            return _result;
        }

        public List<MsUser> GetList(int _prmReqPage, int _prmPageSize)
        {
            List<MsUser> _result = new List<MsUser>();

            try
            {
                var _query = (
                                from _msUser in this.db.MsUsers
                                orderby _msUser.UserName ascending
                                select new
                                {
                                    UserName = _msUser.UserName,
                                    LoweredUserName = _msUser.LoweredUserName,
                                    Password = _msUser.Password,
                                    Email = _msUser.Email,
                                    PermissionLevelCode = _msUser.PermissionLevelCode
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsUser(_row.UserName, _row.LoweredUserName, _row.Password, _row.Email, _row.PermissionLevelCode));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsUser> GetList(int _prmReqPage, int _prmPageSize, String _prmUser, String _prmKeyword)
        {
            List<MsUser> _result = new List<MsUser>();

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmUser == "UserName")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            if (_prmUser == "Email")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";

            }


            try
            {
                var _query = (
                                from _msUser in this.db.MsUsers
                                where (SqlMethods.Like(_msUser.UserName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msUser.Email.Trim().ToLower(), _pattern2.Trim().ToLower()))


                                orderby _msUser.UserName ascending
                                select new
                                {
                                    UserName = _msUser.UserName,
                                    LoweredUserName = _msUser.LoweredUserName,
                                    Password = _msUser.Password,
                                    Email = _msUser.Email,
                                    PermissionLevelCode = _msUser.PermissionLevelCode
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsUser(_row.UserName, _row.LoweredUserName, _row.Password, _row.Email, _row.PermissionLevelCode));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsUser> GetList()
        {
            List<MsUser> _result = new List<MsUser>();

            try
            {
                var _query = (
                                from _msUser in this.db.MsUsers
                                orderby _msUser.UserName ascending
                                select new
                                {
                                    UserName = _msUser.UserName,
                                    LoweredUserName = _msUser.LoweredUserName,
                                    Password = _msUser.Password,
                                    Email = _msUser.Email,
                                    PermissionLevelCode = _msUser.PermissionLevelCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsUser(_row.UserName, _row.LoweredUserName, _row.Password, _row.Email, _row.PermissionLevelCode));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public MsUser GetSingle(String _prmCode)
        {
            MsUser _result = null;

            try
            {
                _result = this.db.MsUsers.Single(_temp => _temp.UserName == _prmCode);
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String GetModuleNameByCode(String _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msUser in this.db.MsUsers
                                where _msUser.UserName == _prmCode
                                select new
                                {
                                    LoweredUserName = _msUser.LoweredUserName

                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.LoweredUserName;
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool DeleteMulti(String[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    bool _fgAdmin = this.db.V_UserPermissionGroups.Single(_temp => _temp.UserName.Trim().ToLower() == _prmCode[i].Trim().ToLower()).FgAdmin;
                    if (!_fgAdmin)
                    {
                        MsUser _msUser = this.db.MsUsers.Single(_temp => _temp.UserName.Trim().ToLower() == _prmCode[i].Trim().ToLower());
                        MsUser_MsEmployee _msUser_MsEmployee = this.db.MsUser_MsEmployees.Single(_temp1 => _temp1.UserName.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                        this.db.MsUsers.DeleteOnSubmit(_msUser);
                        this.db.MsUser_MsEmployees.DeleteOnSubmit(_msUser_MsEmployee);
                    }
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool Add(MsUser _prmMsUser, MsUser_MsEmployee _prmMsUser_MsEmployee)
        {
            bool _result = false;

            try
            {
                this.db.MsUsers.InsertOnSubmit(_prmMsUser);
                this.db.MsUser_MsEmployees.InsertOnSubmit(_prmMsUser_MsEmployee);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool Edit(MsUser _prmMsUser)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool EditUserEmployee(MsUser_MsEmployee _prmUserEmp, String _prmUserName, String _prmNewEmpNumb)
        {
            bool _result = false;

            try
            {
                MsUser_MsEmployee _oldUserEmp = this.db.MsUser_MsEmployees.Single(_temp => _temp.UserName == _prmUserEmp.UserName && _temp.EmpNumb == _prmUserEmp.EmpNumb);
                this.db.MsUser_MsEmployees.DeleteOnSubmit(_oldUserEmp);

                MsUser_MsEmployee _newUserEmp = new MsUser_MsEmployee();
                _newUserEmp.UserName = _prmUserName;
                _newUserEmp.EmpNumb = _prmNewEmpNumb;

                this.db.MsUser_MsEmployees.InsertOnSubmit(_newUserEmp);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }


        /////////////////AccessPermission

        public void PopulateMenuChoice()
        {
            var _qryUserGroupList = (
                    from _msUserGroupName in this.db.MsUserGroups
                    select _msUserGroupName.UserGroupName
                );
            foreach (var _rsUserGroup in _qryUserGroupList)
            {
                var _qryMenuList = (
                        from _msMenu in this.db.MsMenus
                        where !(
                            from _msAccessPermission in this.db.MsAccessPermissions
                            where _msAccessPermission.UserGroup == _rsUserGroup
                            select _msAccessPermission.MenuID
                        ).Contains(_msMenu.MenuID)
                        select _msMenu.MenuID
                    );
                foreach (int _rowMenuID in _qryMenuList)
                {
                    MsAccessPermission _addAccessPermission = new MsAccessPermission();
                    _addAccessPermission.UserGroup = _rsUserGroup;
                    _addAccessPermission.MenuID = Convert.ToInt32(_rowMenuID);
                    _addAccessPermission.AllowAccess = false;
                    this.db.MsAccessPermissions.InsertOnSubmit(_addAccessPermission);
                }
                this.db.SubmitChanges();
            }
        }

        public SortedList GetUserGroupList()
        {
            SortedList _result = new SortedList();
            try
            {
                var _qry = (from _msUserGroup in this.db.MsUserGroups select _msUserGroup.UserGroupName);
                foreach (String _row in _qry)
                {
                    _result.Add(_row, _row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public int AccessPermissionRowsCount(String _prmUserGroup)
        {
            int _result = 0;
            try
            {
                _result = (
                        from _msAccessPermission in this.db.MsAccessPermissions
                        where _msAccessPermission.UserGroup == _prmUserGroup
                        select _msAccessPermission
                    ).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<MsAccessPermission> GetListAccessPermission(Int32 _prmPage, Int32 _prmMaxPage, String _prmUserGroup)
        {
            List<MsAccessPermission> _result = new List<MsAccessPermission>();
            try
            {
                var _qry = (
                        from _msAccessPermission in this.db.MsAccessPermissions
                        where _msAccessPermission.UserGroup == _prmUserGroup
                        select _msAccessPermission
                    ).Skip(_prmMaxPage * _prmPage).Take(_prmMaxPage);
                foreach (MsAccessPermission _rs in _qry)
                    _result.Add(_rs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String GetMenuText(Int32 _prmMenuID)
        {
            String _result = "";
            try
            {
                _result = (from _msMenu in this.db.MsMenus where _msMenu.MenuID == _prmMenuID select _msMenu.Text).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String GetMenuNavigateUrl(Int32 _prmMenuID)
        {
            String _result = "";
            try
            {
                _result = (from _msMenu in this.db.MsMenus where _msMenu.MenuID == _prmMenuID select _msMenu.NavigateURL).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public void ChangePermissionAccess(String _prmUserGroup, Int32 _prmMenuID, Boolean _prmAllowAccess)
        {
            MsAccessPermission _updateData = this.db.MsAccessPermissions.Single(a => (a.UserGroup == _prmUserGroup && a.MenuID == _prmMenuID));
            _updateData.AllowAccess = _prmAllowAccess;
            this.db.SubmitChanges();
        }
        /////////////////AccessPermission
        #endregion

        public Boolean ValidateUser(String _prmUsername, String _prmPassword, String _prmDatabase)
        {
            Boolean _result = false;

            //String _connString = GetConString.GetConnString(_prmDatabase);

            //SimpleTaskListDatabaseDataContext dbCustom = new SimpleTaskListDatabaseDataContext(_connString);
            VTSDatabaseDataContext dbCustom = new VTSDatabaseDataContext(ApplicationConfig.ConnString);

            var _query = (
                            from _msUser in dbCustom.MsUsers
                            where _msUser.LoweredUserName == _prmUsername.ToLower()
                                && _msUser.Password == _prmPassword
                            select _msUser
                         );

            if (_query.Count() > 0)
            {
                _result = true;
            }

            return _result;
        }

        public String[] GetInstanceFromRegistry()
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey sk = rk.OpenSubKey(@"SOFTWARE\SimpleTaskList");
            String[] _result = sk.GetSubKeyNames();

            return _result;
        }
    }
}

