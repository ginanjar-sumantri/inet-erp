using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.CustomControl
{
    public sealed class TopNavigationMenu : Base
    {
        public TopNavigationMenu()
        {
        }

        private string _MenuMatic;

        public void GenerateMenu(Int32 _parentID, ref String _prmMenu, String[] _roleID)
        {
            String _a = "";

            var _queryMenu = (
                from _mastermenu in this.dbMembership.master_Menus
                join _masterRolePermision in this.dbMembership.master_RolePermissions
                    on _mastermenu.MenuID equals _masterRolePermision.MenuID
                where _mastermenu.ParentID == 0 && _mastermenu.IsActive == true
                && _roleID.Contains(_masterRolePermision.RoleID.ToString())
                && _masterRolePermision.Access == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU)
                orderby _mastermenu.Priority
                select new { _mastermenu.Text, _mastermenu.MenuID, _mastermenu.NavigateURL, _mastermenu.SiteMapURL }
                );

            if (_queryMenu.Count() > 0)
            {
                foreach (var _row in _queryMenu)
                {
                    if (_prmMenu == "")
                    {
                        if (_row.SiteMapURL == null)
                        {
                            _a = "index.aspx";
                        }
                        else
                        {
                            _a = _row.SiteMapURL;
                        }
                        _prmMenu = "<ul id='nav'><li>" + "<a href='" + _a + "' target = 'mainFrame' >" + _row.Text + "</a>";
                        this.GenerateMenuItem(_row.MenuID, ref _prmMenu, _roleID);
                        _prmMenu += "</li>";
                    }
                    else
                    {
                        if (_row.SiteMapURL == null)
                        {
                            _a = "index.aspx";
                        }
                        else
                        {
                            _a = _row.SiteMapURL;
                        }
                        _prmMenu += "<li>" + "<a href='" + _a + "' target = 'mainFrame' >" + _row.Text + "</a>";
                        this.GenerateMenuItem(_row.MenuID, ref _prmMenu, _roleID);
                        _prmMenu += "</li>";
                    }
                }
                _prmMenu += "</ul>";
            }
        }

        public void GenerateMenuItem(short _prmParentID, ref string _prmText, String[] _roleID)
        {
            int _a = 0;
            String _b = "";


            var _queryMenuItem = (
                from _mastermenu in this.dbMembership.master_Menus
                join _masterRolePermision in this.dbMembership.master_RolePermissions
                    on _mastermenu.MenuID equals _masterRolePermision.MenuID
                where _mastermenu.ParentID == _prmParentID && _mastermenu.IsActive == true
                && _mastermenu.ShowInQuickLaunch == true
                && _roleID.Contains(_masterRolePermision.RoleID.ToString())
                && _masterRolePermision.Access == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU)
                orderby _mastermenu.Priority
                select new { _mastermenu.Text, _mastermenu.MenuID, _mastermenu.Indent, _mastermenu.NavigateURL, _mastermenu.SiteMapURL }
                );

            if (_queryMenuItem.Count() > 0)
            {
                foreach (var _item in _queryMenuItem)
                {
                    if (_a == 0)
                    {
                        if (_item.SiteMapURL == null)
                        {
                            _b = "index.aspx";
                        }
                        else
                        {
                            _b = _item.SiteMapURL;
                        }
                        _prmText += "<ul><li>" + "<a href='" + _b + "' target='mainFrame' >" + _item.Text + "</a>";
                        this.GenerateMenuItem(_item.MenuID, ref _prmText, _roleID);
                        _a = 1;
                        _prmText += "</li>";
                    }
                    else
                    {
                        if (_item.SiteMapURL == null)
                        {
                            _b = "index.aspx";
                        }
                        else
                        {
                            _b = _item.SiteMapURL;
                        }
                        _prmText += "<li>" + "<a href='" + _b + "' target='mainFrame' >" + _item.Text + "</a>";
                        this.GenerateMenuItem(_item.MenuID, ref _prmText, _roleID);
                        _a = 1;
                        _prmText += "</li>";
                    }
                }
                _prmText += "</ul>";
            }
        }


        public void GetMenu(Int32 _prmParentID, ref String _prmText, String[] _roleID)
        {
            int _a = 0;
            int _b = 0;

            var _queryMenu = (
                    from _masterMenu in this.dbMembership.master_Menus
                    join _masterRolePermision in this.dbMembership.master_RolePermissions
                    on _masterMenu.MenuID equals _masterRolePermision.MenuID
                    where _masterMenu.ShowInQuickLaunch == true
                    && _roleID.Contains(_masterRolePermision.RoleID.ToString())
                    && _masterRolePermision.Access == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU)
                    select _masterMenu
                    ).Distinct();
            if (_queryMenu.Count() > 0)
            {
                foreach (var _row in _queryMenu)
                {
                    if (_prmText == "")
                    {
                        _prmText = "<ul id='nav'><li>" + "<a href='" + _row.NavigateURL + "'>" + _row.Text + "</a>";
                        this.GetMenu(_row.MenuID, ref _prmText, _roleID);
                        _prmText += "</li>";
                    }
                    else
                    {
                        if (_row.Indent == 0)
                        {
                            _prmText += "<li>" + "<a href='" + _row.NavigateURL + "'>" + _row.Text + "</a>";
                            this.GetMenu(_row.MenuID, ref _prmText, _roleID);
                            _prmText += "</li> </ul>";
                        }
                        else if (_row.Indent > 0)
                        {
                            if (_a == 0)
                            {
                                _prmText += "<ul> <li>" + "<a href='" + _row.NavigateURL + "'>" + _row.Text + "</a>";
                                this.GetMenu(_row.MenuID, ref _prmText, _roleID);
                                _a = 1;
                                _prmText += "</li>";
                            }
                            else
                            {
                                _prmText += "<li>" + "<a href='" + _row.NavigateURL + "'>" + _row.Text + "</a>";
                                this.GetMenu(_row.MenuID, ref _prmText, _roleID);
                                _a = 1;
                                _prmText += "</li></ul>";
                            }
                        }
                    }
                }
            }
        }

        //private void PopulateSubMenu(MenuItem _prmMenuItem, int _prmMenuID, String _prmPath, String[] _roleID)
        //{
        //    var _querySubMenu = (
        //                        from _masterMenu in this.dbMembership.master_Menus
        //                        join _masterRolePermision in this.dbMembership.master_RolePermissions
        //                            on _masterMenu.MenuID equals _masterRolePermision.MenuID
        //                        where _masterMenu.ParentID == _prmMenuID
        //                            && _masterMenu.ShowInQuickLaunch == true
        //                            && _roleID.Contains(_masterRolePermision.RoleID.ToString())
        //                            && _masterRolePermision.Access == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU)
        //                        //orderby _masterMenu.Priority
        //                        select _masterMenu
        //                    ).Distinct().OrderBy(a => a.Priority);
        //    if (_querySubMenu.Count() > 0)
        //        foreach (var _rsSubMenu in _querySubMenu)
        //        {
        //            MenuItem _childItems = new MenuItem();
        //            _childItems.NavigateUrl = _prmPath + _rsSubMenu.NavigateURL;
        //            _childItems.Text = _rsSubMenu.Text;
        //            _prmMenuItem.ChildItems.Add(_childItems);
        //            PopulateSubMenu(_childItems, Convert.ToInt16(_rsSubMenu.MenuID), _prmPath, _roleID);
        //        }
        //}

        private void PopulateSubMenu(MenuItem _prmMenuItem, int _prmMenuID, String _prmPath, String[] _roleID, Boolean _prmToTarget)
        {
            var _querySubMenu = (
                                from _masterMenu in this.dbMembership.master_Menus
                                join _masterRolePermision in this.dbMembership.master_RolePermissions
                                    on _masterMenu.MenuID equals _masterRolePermision.MenuID
                                where _masterMenu.ParentID == _prmMenuID
                                    && _masterMenu.ShowInQuickLaunch == true
                                    && _roleID.Contains(_masterRolePermision.RoleID.ToString())
                                    && _masterRolePermision.Access == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU)
                                //orderby _masterMenu.Priority
                                select _masterMenu
                            ).Distinct().OrderBy(a => a.Priority);
            if (_querySubMenu.Count() > 0)
                foreach (master_Menu _rsSubMenu in _querySubMenu)
                {
                    MenuItem _childItems = new MenuItem();
                    _childItems.NavigateUrl = _prmPath + _rsSubMenu.NavigateURL;
                    _childItems.Text = _rsSubMenu.Text;
                    if (_prmToTarget && _rsSubMenu.NavigateURL != "index.aspx")
                    {
                        //_childItems.Target = "mainFrame";
                        _childItems.NavigateUrl = "javascript:eval(document.getElementById('mainFrame').src='" + _prmPath + _rsSubMenu.NavigateURL.Substring("index.aspx?showPage=".Length) + ".aspx');";
                    }
                    _prmMenuItem.ChildItems.Add(_childItems);
                    this.PopulateSubMenu(_childItems, Convert.ToInt16(_rsSubMenu.MenuID), _prmPath, _roleID, _prmToTarget);
                }
        }

        private void PopulateSubMenuString(MenuItem _prmMenuItem, int _prmMenuID, String _prmPath, String[] _roleID, Boolean _prmToTarget)
        {
            var _querySubMenu = (
                                from _masterMenu in this.dbMembership.master_Menus
                                join _masterRolePermision in this.dbMembership.master_RolePermissions
                                    on _masterMenu.MenuID equals _masterRolePermision.MenuID
                                where _masterMenu.ParentID == _prmMenuID
                                    && _masterMenu.ShowInQuickLaunch == true
                                    && _roleID.Contains(_masterRolePermision.RoleID.ToString())
                                    && _masterRolePermision.Access == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU)
                                //orderby _masterMenu.Priority
                                select _masterMenu
                            ).Distinct().OrderBy(a => a.Priority);
            if (_querySubMenu.Count() > 0)
                foreach (master_Menu _rsSubMenu in _querySubMenu)
                {
                    MenuItem _childItems = new MenuItem();
                    _childItems.NavigateUrl = _prmPath + _rsSubMenu.NavigateURL;
                    _childItems.Text = _rsSubMenu.Text;
                    if (_prmToTarget && _rsSubMenu.NavigateURL != "index.aspx")
                    {
                        //_childItems.Target = "mainFrame";
                        _childItems.NavigateUrl = "javascript:eval(document.getElementById('mainFrame').src='" + _prmPath + _rsSubMenu.NavigateURL.Substring("index.aspx?showPage=".Length) + ".aspx');";
                    }
                    _prmMenuItem.ChildItems.Add(_childItems);
                    this.PopulateSubMenu(_childItems, Convert.ToInt16(_rsSubMenu.MenuID), _prmPath, _roleID, _prmToTarget);
                }
        }


        private Boolean CekMenuPermision(Int16 _prmMenuID, String[] _prmRole)
        {
            Boolean _result = false;
            try
            {
                foreach (String _role in _prmRole)
                {
                    _result = (
                            from _masterRolePermision in this.dbMembership.master_RolePermissions
                            where _masterRolePermision.RoleID == new Guid(_role)
                                && _masterRolePermision.MenuID == _prmMenuID
                                && _masterRolePermision.Access == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU)
                            select _masterRolePermision
                        ).Count() > 0;
                    if (_result) return _result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public void RenderItem(short _prmParentID, String[] _userRole, ERPModule _prmModule, Menu _prmMenu, Guid _prmCompanyID)
        {
            var _menuQuery = (
                                from _menu in this.dbMembership.master_Menus
                                join _roleMenu in this.dbMembership.master_RoleMenus
                                   on _menu.MenuID equals _roleMenu.MenuId
                                join _companyMenu in this.dbMembership.master_CompanyMenus
                                   on _menu.MenuID equals _companyMenu.MenuId
                                where (_menu.ParentID == _prmParentID)
                                   && (_menu.IsActive == true)
                                   && (_menu.ShowInQuickLaunch == true)
                                   && (
                                           _userRole
                                      ).Contains(_roleMenu.RoleId.ToString().Trim().ToLower())
                                   && _companyMenu.CompanyId == _prmCompanyID
                                select _menu
                              ).Distinct().OrderBy(a => a.Priority);

            foreach (master_Menu _menuRow in _menuQuery)
            {
                if (this.CekMenuPermision(_menuRow.MenuID, _userRole))
                {
                    Boolean _targetFlag = false;
                    MenuItem _menuItem = new MenuItem();

                    if (AppModule.GetValue(_prmModule) == _menuRow.ModuleID)
                    {
                        _menuItem.ImageUrl = ApplicationConfig.HomeWebAppURL + _menuRow.OnSelectedImageURL;
                        _targetFlag = true;
                    }
                    else
                    {
                        _menuItem.ImageUrl = ApplicationConfig.HomeWebAppURL + _menuRow.ImageURL;
                    }

                    switch (AppModule.GetValue(_menuRow.ModuleID))
                    {
                        case ERPModule.Home:
                            _menuItem.NavigateUrl = ApplicationConfig.HomeWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.HomeWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.Accounting:
                            _menuItem.NavigateUrl = ApplicationConfig.AccountingWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.AccountingWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.ExecutiveInformation:
                            _menuItem.NavigateUrl = ApplicationConfig.ExecutiveInformationWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.ExecutiveInformationWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.Finance:
                            _menuItem.NavigateUrl = ApplicationConfig.FinanceWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.FinanceWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.HumanResource:
                            _menuItem.NavigateUrl = ApplicationConfig.HumanResourceWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.HumanResourceWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.Payroll:
                            _menuItem.NavigateUrl = ApplicationConfig.PayrollWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.PayrollWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.Production:
                            _menuItem.NavigateUrl = ApplicationConfig.ProductionWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.ProductionWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.Purchasing:
                            _menuItem.NavigateUrl = ApplicationConfig.PurchasingWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.PurchasingWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.Sales:
                            _menuItem.NavigateUrl = ApplicationConfig.SalesWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.SalesWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.Settings:
                            _menuItem.NavigateUrl = ApplicationConfig.SettingsWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.SettingsWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.ShipPorting:
                            _menuItem.NavigateUrl = ApplicationConfig.PortWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.PortWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.StockControl:
                            _menuItem.NavigateUrl = ApplicationConfig.StockControlWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.StockControlWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.Billing:
                            _menuItem.NavigateUrl = ApplicationConfig.BillingWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.BillingWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.NCC:
                            _menuItem.NavigateUrl = ApplicationConfig.NCCWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.NCCWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.SMS:
                            _menuItem.NavigateUrl = ApplicationConfig.SMSWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.SMSWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.Clinic:
                            _menuItem.NavigateUrl = ApplicationConfig.ClinicWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.ClinicWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.POS:
                            _menuItem.NavigateUrl = ApplicationConfig.POSWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.POSWebAppURL, _userRole, _targetFlag);
                            break;
                        case ERPModule.Tour:
                            _menuItem.NavigateUrl = ApplicationConfig.TourWebAppURL + _menuRow.NavigateURL;
                            this.PopulateSubMenu(_menuItem, _menuRow.MenuID, ApplicationConfig.TourWebAppURL, _userRole, _targetFlag);
                            break;
                    }

                    _prmMenu.Items.Add(_menuItem);
                }
            }
        }

        public Int32 MenuCount(short _prmParentID, String[] _userRole, ERPModule _prmModule, Menu _prmMenu, Guid _prmCompanyID)
        {
            Int32 _result = 0;
            try
            {
                _result = (from _menu in this.dbMembership.master_Menus
                           join _roleMenu in this.dbMembership.master_RoleMenus
                              on _menu.MenuID equals _roleMenu.MenuId
                           join _companyMenu in this.dbMembership.master_CompanyMenus
                              on _menu.MenuID equals _companyMenu.MenuId
                           where (_menu.ParentID == _prmParentID)
                              && (_menu.IsActive == true)
                              && (_menu.ShowInQuickLaunch == true)
                              && (_userRole).Contains(_roleMenu.RoleId.ToString().Trim().ToLower())
                              && _companyMenu.CompanyId == _prmCompanyID
                           orderby _menu.Priority ascending
                           select _menu).Count();
            }
            catch (Exception ex)
            {
                throw;
            }
            return _result;
        }

        public List<master_Menu> GetListTopMenu(Guid _prmCompanyId)
        {
            List<master_Menu> _result = new List<master_Menu>();
            try
            {
                var _query = (
                                from _menu in this.dbMembership.master_Menus
                                join _companyMenu in this.dbMembership.master_CompanyMenus
                                    on _menu.MenuID equals _companyMenu.MenuId
                                where (_menu.ParentID == 0)
                                    && (_menu.IsActive == true)
                                    && _companyMenu.CompanyId == _prmCompanyId
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

        public int GetMenuIDByMenuName(String _prmMenuName)
        {
            int _result = 0;
            try
            {
                var _query = (
                                from _menu in this.dbMembership.master_Menus
                                where (_menu.Text == _prmMenuName)
                                select _menu.MenuID
                             ).FirstOrDefault();
                if (_query != null)
                {
                    _result = _query;
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        ~TopNavigationMenu()
        {

        }
    }
}