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
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Settings
{
    public sealed class RoleBL : Base
    {
        public RoleBL()
        {
        }

        #region aspnet_Role

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
                          from _aspnetRole in this.db.aspnet_Roles
                          where (SqlMethods.Like(_aspnetRole.RoleName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                          select _aspnetRole.RoleId
                       ).Count();

            _result = _query;

            return _result;
        }

        public List<aspnet_Role> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<aspnet_Role> _result = new List<aspnet_Role>();

            string _pattern1 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _role in this.db.aspnet_Roles
                                where (SqlMethods.Like(_role.RoleName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _role.RoleName ascending
                                select new
                                {
                                    RoleId = _role.RoleId,
                                    RoleName = _role.RoleName,
                                    Description = _role.Description
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { RoleId = this._guid, RoleName = this._string, Description = this._string });

                    _result.Add(new aspnet_Role(_row.RoleId, _row.RoleName, _row.Description));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            _result.Sort(new Comparison<aspnet_Role>(this.SortByRoleName));

            return _result;
        }

        private int SortByRoleName(aspnet_Role _prmRoleName1, aspnet_Role _prmRoleName2)
        {
            return _prmRoleName1.RoleName.CompareTo(_prmRoleName2.RoleName);
        }

        public List<aspnet_Role> GetList()
        {
            List<aspnet_Role> _result = new List<aspnet_Role>();

            try
            {
                var _query = (
                                from _role in this.db.aspnet_Roles
                                where !(from _rolePermission in this.db.master_RolePermissions
                                        where _role.RoleId.ToString().Trim().ToLower() == _rolePermission.RoleID.ToString().Trim().ToLower()
                                        select _rolePermission.RoleID
                                       ).Contains(_role.RoleId)
                                select new
                                {
                                    RoleId = _role.RoleId,
                                    RoleName = _role.RoleName,
                                    Description = _role.Description
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new aspnet_Role(_row.RoleId, _row.RoleName, _row.Description));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<aspnet_Role> GetListRoleForDDLPermission(Guid _prmCompanyId)
        {
            List<aspnet_Role> _result = new List<aspnet_Role>();

            try
            {
                var _query = (
                                from _role in this.db.aspnet_Roles
                                join _companyRole in this.db.master_Company_aspnet_Roles
                                    on _role.RoleId equals _companyRole.RoleId
                                where !(from _rolePermission in this.db.master_RolePermissions
                                        where _role.RoleId.ToString().Trim().ToLower() == _rolePermission.RoleID.ToString().Trim().ToLower()
                                        select _rolePermission.RoleID
                                       ).Contains(_role.RoleId)
                                   && _companyRole.CompanyID == _prmCompanyId
                                select new
                                {
                                    RoleId = _role.RoleId,
                                    RoleName = _role.RoleName,
                                    Description = _role.Description
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new aspnet_Role(_row.RoleId, _row.RoleName, _row.Description));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<aspnet_Role> GetListRole()
        {
            List<aspnet_Role> _result = new List<aspnet_Role>();

            try
            {
                var _query = (
                                from _role in this.db.aspnet_Roles
                                select new
                                {
                                    RoleId = _role.RoleId,
                                    RoleName = _role.RoleName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new aspnet_Role(_row.RoleId, _row.RoleName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<master_RoleMenu> GetListRoleMenu(Guid _prmRoleID)
        {
            List<master_RoleMenu> _result = new List<master_RoleMenu>();

            try
            {
                var _query = (
                                from _roleMenu in this.db.master_RoleMenus
                                where _roleMenu.RoleId == _prmRoleID
                                select new
                                {
                                    RoleId = _roleMenu.RoleId,
                                    MenuId = _roleMenu.MenuId
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new master_RoleMenu(_row.RoleId, _row.MenuId));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<aspnet_Role> GetListRoleName()
        {
            List<aspnet_Role> _result = new List<aspnet_Role>();

            try
            {
                var _query = (
                                from _role in this.db.aspnet_Roles
                                select new
                                {
                                    RoleName = _role.RoleName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { RoleName = this._string });

                    _result.Add(new aspnet_Role(_row.RoleName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public aspnet_Role GetSingle(string _prmCode)
        {
            aspnet_Role _result = null;

            try
            {
                _result = this.db.aspnet_Roles.Single(_temp => _temp.RoleId == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetRoleNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _role in this.db.aspnet_Roles
                                where _role.RoleId == new Guid(_prmCode)
                                select new
                                {
                                    RoleName = _role.RoleName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.RoleName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string DeleteMulti(string[] _prmCode)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    for (int i = 0; i < _prmCode.Length; i++)
                    {
                        master_Role _role = this.db.master_Roles.Single(_temp => _temp.RoleId.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                        if (_role.SystemRole == true)
                        {
                            _result = "Role(s) can't be deleted, probably the role(s) might be used by user or signed as system role";
                            break;
                        }
                        else
                        {
                            var _queryDel1 = (
                                            from _menuRole in this.db.master_Menu_aspnet_Roles
                                            where _menuRole.RoleId == _role.RoleId
                                            select _menuRole 
                                          );

                            this.db.master_Menu_aspnet_Roles.DeleteAllOnSubmit(_queryDel1);

                            var _queryDel2 = (
                                            from _compRole in this.db.master_Company_aspnet_Roles
                                            where _compRole.RoleId == _role.RoleId
                                            select _compRole 
                                          );

                            this.db.master_Company_aspnet_Roles.DeleteAllOnSubmit(_queryDel2);

                            var _queryDel3 = (
                                        from _roleMenu in this.db.master_RoleMenus
                                        where _roleMenu.RoleId == _role.RoleId
                                        select _roleMenu
                                   );

                            this.db.master_RoleMenus.DeleteAllOnSubmit(_queryDel3);

                            var _queryDel4 = (
                                        from _msRole in this.db.master_Roles
                                        where _msRole.RoleId == _role.RoleId
                                        select _msRole
                                   );

                            this.db.master_Roles.DeleteAllOnSubmit(_queryDel4);

                            var _queryDel5 = (
                                        from _msReminder in this.db.MsReminderMappings
                                        where _msReminder.RoleId == _role.RoleId
                                        select _msReminder
                                   );

                            this.db.MsReminderMappings.DeleteAllOnSubmit(_queryDel5);

                            MembershipService _service = new MembershipService();

                            aspnet_Role _aspnetRole = this.db.aspnet_Roles.Single(_temp => _temp.RoleId == _role.RoleId);
                            bool _remove = _service.RemoveRole(_aspnetRole.RoleName);

                            if (_remove == false)
                            {
                                _result = "Role(s) can't be deleted, probably the role(s) might be used by user or signed as system role";
                                break;
                            }
                        }
                    }

                    if (_result == "")
                    {
                        this.db.SubmitChanges();
                        _scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);

                _result = "Role(s) can't be deleted, probably the role(s) might be used by user or signed as system role";
            }

            return _result;
        }

        public Guid GetRoleIDByName(string _prmRoleName)
        {
            Guid _result = new Guid();

            try
            {
                var _query = (
                                from _role in this.db.aspnet_Roles
                                where _role.RoleName == _prmRoleName
                                select new
                                {
                                    RoleID = _role.RoleId
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.RoleID;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String[] GetRolesIDByUserName(String _prmUserName)
        {
            String[] _result;
            String[] _role;
            MembershipService _service = new MembershipService();

            _role = _service.GetRolesForUser(_prmUserName);
            _result = new String[_role.Count()];

            for (int i = 0; i < _role.Count(); i++)
            {
                _result[i] = this.GetRoleIDByName(_role[i]).ToString();
            }

            return _result;
        }

        public bool Add(Boolean _prmSystemRole, String _prmRoleName, String _prmMenuList)
        {
            bool _result = false;

            try
            {
                MembershipService _service = new MembershipService();

                bool _createRole = _service.CreateRole(_prmRoleName);
                if (_createRole == true)
                {
                    master_Role _role = new master_Role();

                    _role.RoleId = GetRoleIDByName(_prmRoleName);
                    _role.SystemRole = _prmSystemRole;

                    this.db.master_Roles.InsertOnSubmit(_role);

                    if (_prmMenuList != "")
                    {
                        //untuk roleMenu
                        String[] _menuID = _prmMenuList.Split(',');
                        List<master_RoleMenu> _roleMenuList = new List<master_RoleMenu>();
                        foreach (var _item in _menuID)
                        {
                            master_RoleMenu _roleMenu = new master_RoleMenu();
                            _roleMenu.RoleId = _role.RoleId;
                            _roleMenu.MenuId = (_item == "") ? Convert.ToInt16(0) : Convert.ToInt16(_item);

                            _roleMenuList.Add(_roleMenu);
                        }
                        this.db.master_RoleMenus.InsertAllOnSubmit(_roleMenuList);
                    }

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

        public bool Edit(aspnet_Role _prmAspRole, String _prmMenuList)
        {
            bool _result = false;

            try
            {
                var _queryDelete = (
                                        from _roleMenu in this.db.master_RoleMenus
                                        where _roleMenu.RoleId == _prmAspRole.RoleId
                                        select _roleMenu
                                   );

                this.db.master_RoleMenus.DeleteAllOnSubmit(_queryDelete);

                if (_prmMenuList != "")
                {
                    String[] _menuID = _prmMenuList.Split(',');
                    List<master_RoleMenu> _roleMenuList = new List<master_RoleMenu>();
                    foreach (var _item in _menuID)
                    {
                        master_RoleMenu _roleMenu = new master_RoleMenu();
                        _roleMenu.RoleId = _prmAspRole.RoleId;
                        _roleMenu.MenuId = (_item == "") ? Convert.ToInt16(0) : Convert.ToInt16(_item);

                        _roleMenuList.Add(_roleMenu);
                    }
                    this.db.master_RoleMenus.InsertAllOnSubmit(_roleMenuList);
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

        public master_Role GetSingleMasterRole(Guid _prmRoleID)
        {
            master_Role _result = null;

            try
            {
                _result = this.db.master_Roles.Single(_temp => _temp.RoleId == _prmRoleID);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~RoleBL()
        {
        }
    }
}
