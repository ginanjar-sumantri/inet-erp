using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using System.Data.Linq.SqlClient;
using System.Xml;

namespace InetGlobalIndo.ERP.MTJ.CustomControl
{
    public sealed class QuickLaunchNavigationMenu : Base
    {
        public QuickLaunchNavigationMenu()
        {
        }

        //public Menu RenderControl(short _prmParentID, string _prmModuleID, string _userRole)
        //{
        //    Menu _result = new Menu();

        //    _result.StaticDisplayLevels = Convert.ToInt32(ApplicationConfig.QuickLaunchNavStaticDisplayLevel);
        //    _result.Items.Add(this.RenderItem(_prmParentID, _prmModuleID, _userRole, null));

        //    return _result;
        //}

        public TreeNode RenderNode(short _prmParentID, string _prmModuleID, string _userRole, TreeNode _prmTreeNode, string _prmImageUrl, string _prmTitle)
        {
            MTJMembershipDataContext dbMembership = new MTJMembershipDataContext(ApplicationConfig.MembershipConnString);
            TreeNode _result;


            if (_prmTreeNode == null)
            {
                _result = new TreeNode();
                _result.ImageUrl = _prmImageUrl;
                _result.Text = _prmTitle;
            }
            else
            {
                _result = _prmTreeNode;
            }

            string _pattern = "%" + _userRole.Trim().ToLower() + "%";
            var _nodeQuery = from _node in dbMembership.master_Menus
                             where (_node.ParentID == _prmParentID)
                                        && (_node.ModuleID == _prmModuleID)
                                        && (_node.IsActive == true)
                                        //&& (SqlMethods.Like(_node.Roles.Trim().ToLower(), _pattern))
                                        && (_node.ShowInQuickLaunch == true)
                             orderby _node.Priority ascending
                             select _node;

            foreach (master_Menu _nodeRow in _nodeQuery)
            {
                TreeNode _treeNode = new TreeNode(_nodeRow.Text, _nodeRow.Value, "", _nodeRow.NavigateURL, "_self");
                _treeNode.Expanded = false;

                _result.ChildNodes.Add(this.RenderNode(_nodeRow.MenuID, _prmModuleID, _userRole, _treeNode, _prmImageUrl, _prmTitle));
            }

            return _result;
        }

        public MenuItem RenderItem(short _prmParentID, string _prmModuleID, string _userRole, MenuItem _prmMenuItem)
        {
            MTJMembershipDataContext dbMembership = new MTJMembershipDataContext(ApplicationConfig.MembershipConnString);
            MenuItem _result;

            if (_prmMenuItem == null)
                _result = new MenuItem();
            else
                _result = _prmMenuItem;

            string _pattern = "%" + _userRole.Trim().ToLower() + "%";
            var _menuQuery = from _menu in dbMembership.master_Menus
                             where (_menu.ParentID == _prmParentID)
                                        && (_menu.ModuleID == _prmModuleID)
                                        && (_menu.IsActive == true)
                                        //&& (SqlMethods.Like(_menu.Roles.Trim().ToLower(), _pattern))
                             orderby _menu.Priority ascending
                             select _menu;

            foreach (master_Menu _menuRow in _menuQuery)
            {
                MenuItem _menuItem = new MenuItem(_menuRow.Text, _menuRow.Value, ApplicationConfig.HomeWebAppURL + _menuRow.MenuIcon, _menuRow.NavigateURL);

                _result.ChildItems.Add(this.RenderItem(_menuRow.MenuID, _prmModuleID, _userRole, _menuItem));
            }

            return _result;
        }

        public XmlTextWriter GenerateSiteMapPath(short _prmParentID, string _prmModuleID, string _userRole, XmlTextWriter _prmXML, string _prmDir, string _prmTitle, short _prmParent)
        {
            MTJMembershipDataContext dbMembership = new MTJMembershipDataContext(ApplicationConfig.MembershipConnString);
            XmlTextWriter _objXMLTW;

            if (_prmXML == null)
            {
                string _fileXML = _prmDir + "Web.sitemap";
                Encoding _enc = Encoding.UTF8;
                _objXMLTW = new XmlTextWriter(_fileXML, _enc);

                _objXMLTW.WriteStartDocument();//xml document open
                _objXMLTW.WriteStartElement("siteMap");
                _objXMLTW.WriteAttributeString("xmlns", "http://schemas.microsoft.com/AspNet/SiteMap-File-1.0");

                _objXMLTW.WriteStartElement("siteMapNode");
                _objXMLTW.WriteAttributeString("title", _prmTitle);
                _objXMLTW.WriteAttributeString("url", "Default.aspx");
            }
            else
            {
                _objXMLTW = _prmXML;
            }
            

            string _pattern = "%" + _userRole.Trim().ToLower() + "%";
            var _menuQuery = from _menu in dbMembership.master_Menus
                             where (_menu.ParentID == _prmParentID)
                                        && (_menu.ModuleID == _prmModuleID)
                                        && (_menu.IsActive == true)
                                        //&& (SqlMethods.Like(_menu.Roles.Trim().ToLower(), _pattern))
                             orderby _menu.Priority ascending
                             select _menu;
            
            foreach (master_Menu _menu in _menuQuery)
            {
                _objXMLTW.WriteStartElement("siteMapNode");
                _objXMLTW.WriteAttributeString("title", _menu.Text);
                _objXMLTW.WriteAttributeString("url", _menu.SiteMapURL);

                this.GenerateSiteMapPath(_menu.MenuID, _prmModuleID, _userRole, _objXMLTW, _prmDir, _prmTitle, _prmParent);

                _objXMLTW.WriteEndElement();
               
            }

            if (_prmParentID == _prmParent)
            {
                _objXMLTW.WriteEndElement(); //SiteMapNode
                _objXMLTW.WriteEndElement(); //SiteMap
                _objXMLTW.WriteEndDocument();
                _objXMLTW.Flush();
                _objXMLTW.Close();
            }

            return _objXMLTW;
        }
                    
        ~QuickLaunchNavigationMenu()
        {
        }
    }
}