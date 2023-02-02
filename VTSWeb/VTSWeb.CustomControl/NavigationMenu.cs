using System;
using System.Linq;
using System.Web.UI.WebControls;
using VTSWeb.Database;
using VTSWeb.SystemConfig;
using VTSWeb.BusinessRule;

namespace VTSWeb.CustomControl
{
    public sealed class NavigationMenu : Base
    {
        public NavigationMenu()
        {
        }

        public void RenderMenu(int _prmParentID, Menu _prmMenu, String _prmHomeURL)
        {
            var _menuQuery = (
                                from _menu in this.db.MsMenus
                                where _menu.ParentID == _prmParentID
                                   && _menu.IsActive == true
                                select _menu
                              ).Distinct().OrderBy(a => a.Priority);

            foreach (MsMenu _menuRow in _menuQuery)
            {
                MenuItem _menuItem = new MenuItem();

                _menuItem.NavigateUrl = _prmHomeURL + _menuRow.NavigateURL;
                _menuItem.Text = _menuRow.Text;

                this.PopulateSubMenu(_menuItem, _menuRow.MenuID, _prmHomeURL);

                _prmMenu.Items.Add(_menuItem);
            }
        }

        private void PopulateSubMenu(MenuItem _prmMenuItem, int _prmMenuID, String _prmHomeURL)
        {
            var _querySubMenu = (
                                from _masterMenu in this.db.MsMenus
                                where _masterMenu.ParentID == _prmMenuID
                                select _masterMenu
                            ).Distinct().OrderBy(a => a.Priority);
            if (_querySubMenu.Count() > 0)
                foreach (MsMenu _rsSubMenu in _querySubMenu)
                {
                    MenuItem _childItems = new MenuItem();
                    _childItems.NavigateUrl = _prmHomeURL + _rsSubMenu.NavigateURL;
                    _childItems.Text = _rsSubMenu.Text;
                    _prmMenuItem.ChildItems.Add(_childItems);
                    this.PopulateSubMenu(_childItems, _rsSubMenu.MenuID, _prmHomeURL);
                }
        }

        public void RenderMenu(int _prmParentID, int _prmPermissionLevel, Menu _prmMenu, String _prmHomeURL, String _prmUserName)
        {
            UserBL _userBL = new UserBL();
            String _userGroup = (from _vUserPermissionGroup in this.db.V_UserPermissionGroups
                                 where _vUserPermissionGroup.UserName == _prmUserName
                                 select _vUserPermissionGroup.UserGroupName
                                 ).FirstOrDefault();

            var _menuQuery = (
                                from _menu in this.db.MsMenus
                                where _menu.ParentID == _prmParentID
                                   && _menu.IsActive == true
                                   && (from _msAccessPermission in this.db.MsAccessPermissions
                                       where _msAccessPermission.UserGroup == _userGroup
                                            && _msAccessPermission.AllowAccess == true
                                       select _msAccessPermission.MenuID).Contains(_menu.MenuID)
                                select _menu
                              ).Distinct().OrderBy(a => a.Priority);

            foreach (MsMenu _menuRow in _menuQuery)
            {
                MenuItem _menuItem = new MenuItem();

                _menuItem.NavigateUrl = _prmHomeURL + _menuRow.NavigateURL;
                _menuItem.Text = _menuRow.Text;

                this.PopulateSubMenu(_menuItem, _menuRow.MenuID, _prmPermissionLevel, _prmHomeURL,_prmUserName);

                _prmMenu.Items.Add(_menuItem);
            }
        }

        private void PopulateSubMenu(MenuItem _prmMenuItem, int _prmMenuID, int _prmPermissionLevel, String _prmHomeURL, String _prmUserName)
        {
            UserBL _userBL = new UserBL();
            String _userGroup = (from _vUserPermissionGroup in this.db.V_UserPermissionGroups
                                 where _vUserPermissionGroup.UserName == _prmUserName
                                 select _vUserPermissionGroup.UserGroupName
                                 ).FirstOrDefault();
            var _querySubMenu = (
                                from _masterMenu in this.db.MsMenus
                                where _masterMenu.ParentID == _prmMenuID
                                    && _masterMenu.IsActive == true
                                   && (from _msAccessPermission in this.db.MsAccessPermissions
                                       where _msAccessPermission.UserGroup == _userGroup
                                            && _msAccessPermission.AllowAccess == true
                                       select _msAccessPermission.MenuID).Contains(_masterMenu.MenuID)
                                select _masterMenu
                            ).Distinct().OrderBy(a => a.Priority);

            if (_querySubMenu.Count() > 0)
                foreach (MsMenu _rsSubMenu in _querySubMenu)
                {
                    MenuItem _childItems = new MenuItem();
                    _childItems.NavigateUrl = _prmHomeURL + _rsSubMenu.NavigateURL;
                    _childItems.Text = _rsSubMenu.Text;
                    _prmMenuItem.ChildItems.Add(_childItems);
                    this.PopulateSubMenu(_childItems, _rsSubMenu.MenuID, _prmPermissionLevel, _prmHomeURL,_prmUserName);
                }
        }

        ~NavigationMenu()
        {
        }
    }
}
