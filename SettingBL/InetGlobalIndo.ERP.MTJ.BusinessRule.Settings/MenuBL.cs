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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Settings
{
    public sealed class MenuBL : Base
    {
        public MenuBL()
        {
        }

        #region master_Menu

        public List<master_Menu> GetListForDDL()
        {
            List<master_Menu> _result = new List<master_Menu>();

            try
            {
                var _query = (
                                from _menu in this.db.master_Menus
                                where _menu.ParentID == 0
                                select new
                                {
                                    ModuleID = _menu.ModuleID,
                                    Text = _menu.Text
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new master_Menu(_row.ModuleID, _row.Text));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<master_Menu> GetListModuleForDDL(Guid _prmCompany, Guid _prmRoleID)
        {
            List<master_Menu> _result = new List<master_Menu>();

            try
            {
                var _query = (
                                from _menu in this.db.master_Menus
                                join _companyMenu in this.db.master_CompanyMenus
                                    on _menu.MenuID equals _companyMenu.MenuId
                                join _roleMenu in this.db.master_RoleMenus
                                    on _menu.MenuID equals _roleMenu.MenuId
                                where _menu.ParentID == 0
                                    && _companyMenu.CompanyId == _prmCompany
                                    && _roleMenu.RoleId == _prmRoleID
                                select new
                                {
                                    ModuleID = _menu.ModuleID,
                                    Text = _menu.Text
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new master_Menu(_row.ModuleID, _row.Text));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<master_Menu> GetListModuleForDDLForEdit(Guid _prmCompany, Guid _prmRoleID)
        {
            List<master_Menu> _result = new List<master_Menu>();

            try
            {
                var _query = (
                                from _rolePermission in this.db.master_RolePermissions
                                join _menu in this.db.master_Menus
                                    on _rolePermission.MenuID equals _menu.MenuID
                                join _companyMenu in this.db.master_CompanyMenus
                                    on _menu.MenuID equals _companyMenu.MenuId
                                join _roleMenu in this.db.master_RoleMenus
                                    on _menu.MenuID equals _roleMenu.MenuId
                                where _menu.ParentID == 0
                                    && _companyMenu.CompanyId == _prmCompany
                                    && _rolePermission.RoleID == _prmRoleID
                                select new
                                {
                                    ModuleID = _menu.ModuleID,
                                    Text = _menu.Text
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new master_Menu(_row.ModuleID, _row.Text));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~MenuBL()
        {
        }
    }
}
