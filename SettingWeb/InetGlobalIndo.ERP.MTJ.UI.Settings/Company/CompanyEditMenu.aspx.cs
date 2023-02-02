using System;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.UI.Settings.User;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using System.Collections.Generic;


namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Company
{
    public partial class CompanyEditMenu : CompanyBase
    {
        private CompanyBL _CompBL = new CompanyBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowMenu();

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowMenu()
        {
            this.MenuCheckBoxList.ClearSelection();
            this.MenuCheckBoxList.Items.Clear();
            this.MenuCheckBoxList.DataValueField = "MenuID";
            this.MenuCheckBoxList.DataTextField = "Text";
            this.MenuCheckBoxList.DataSource = _CompBL.GetListTopMenu();
            this.MenuCheckBoxList.DataBind();
        }

        public void ShowData()
        {
            String _companyName = new CompanyBL().GetNameByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey));

            this.RoleNameTextBox.Text = _companyName;

            this.GetCompMenu(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey));
        }

        public void GetCompMenu(String _prmCompID)
        {
            List<master_CompanyMenu> _compMenuList = _CompBL.GetListCompanyMenu(new Guid(_prmCompID));
            
            for (int i = 0; i < this.MenuCheckBoxList.Items.Count; i++)
            {
                foreach (var _item in _compMenuList)
                {
                    if (new TopNavigationMenu().GetMenuIDByMenuName(this.MenuCheckBoxList.Items[i].Text).ToString() == _item.MenuId.ToString())
                    {
                        this.MenuCheckBoxList.Items[i].Selected = true;
                    }
                }
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            String _menuList = "";
            String _companyId = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey);
            var _hasilMenu = _CompBL.GetListTopMenu();

            for (var i = 0; i < _hasilMenu.Count; i++)
            {
                if (this.MenuCheckBoxList.Items[i].Selected == true)
                {
                    if (_menuList == "")
                    {
                        //_menuList += _hasilMenu[i].ModuleID;
                        _menuList += this.MenuCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        //_menuList += "," + _hasilMenu[i].ModuleID;
                        _menuList += "," + this.MenuCheckBoxList.Items[i].Value;
                    }
                }
            }

            //aspnet_Role _aspRole = this._CompBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            //_aspRole.RoleName = this.RoleNameTextBox.Text;
            //_aspRole.LoweredRoleName = this.RoleNameTextBox.Text.ToLower();

            bool _result = this._CompBL.EditCompanyMenu(_companyId, _menuList);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}