using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Company
{
    public partial class CompanyUserAdd : CompanyBase
    {
        private CompanyBL _companyBL = new CompanyBL();
        private UserBL _userBL = new UserBL();
        private DatabaseBL _databaseBL = new DatabaseBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral3;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowUser();
                this.ShowDatabase();

                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();
            this.UserDropDownList.SelectedValue = "null";
        }

        private void ShowUser()
        {
            this.UserDropDownList.DataTextField = "UserName";
            this.UserDropDownList.DataValueField = "UserID";
            this.UserDropDownList.DataSource = this._userBL.GetListForDDL(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            this.UserDropDownList.DataBind();
            this.UserDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowDatabase()
        {
            this.DatabaseCheckBoxList.DataTextField = "Name";
            this.DatabaseCheckBoxList.DataValueField = "DatabaseID";
            this.DatabaseCheckBoxList.DataSource = this._databaseBL.GetListForCheckBox(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            this.DatabaseCheckBoxList.DataBind();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            master_Company_aspnet_User _master_Company_aspnet_User = new master_Company_aspnet_User();

            _master_Company_aspnet_User.CompanyID = new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey));
            _master_Company_aspnet_User.UserID = new Guid(this.UserDropDownList.SelectedValue);

            List<master_Database_aspnet_User> _dbUserList = new List<master_Database_aspnet_User>();

            for (int i = 0; i < this.DatabaseCheckBoxList.Items.Count; i++)
            {
                master_Database_aspnet_User _dbUser = new master_Database_aspnet_User();

                if (this.DatabaseCheckBoxList.Items[i].Selected == true)
                {
                    _dbUser.UserID = new Guid(this.UserDropDownList.SelectedValue);
                    _dbUser.DatabaseID = new Guid(this.DatabaseCheckBoxList.Items[i].Value);

                    _dbUserList.Add(_dbUser);
                }
            }

            bool _result = _companyBL.AddCompanyUser(_master_Company_aspnet_User, _dbUserList);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        private Boolean IsDatabaseSelected(Guid prmDatabaseID)
        {
            Boolean _result = false;

            List<master_Database_aspnet_User> _databaseUserList = _companyBL.GetListDatabaseUser(new Guid(this.UserDropDownList.SelectedValue));

            foreach (var _item in _databaseUserList)
            {
                if (prmDatabaseID == _item.DatabaseID)
                {
                    _result = true;

                    break;
                }
            }

            return _result;
        }

        protected void UserDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.DatabaseCheckBoxList.Items.Count; i++)
            {
                this.DatabaseCheckBoxList.Items[i].Selected = this.IsDatabaseSelected(new Guid(this.DatabaseCheckBoxList.Items[i].Value));
            }
        }
    }
}