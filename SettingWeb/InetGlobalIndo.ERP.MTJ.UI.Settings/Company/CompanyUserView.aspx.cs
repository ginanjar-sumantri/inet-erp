using System;
using System.Web;
using System.Collections.Generic;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Company
{
    public partial class CompanyUserView : CompanyBase
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

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowDatabase();

                this.ShowData();
            }
        }

        private Boolean IsDatabaseSelected(Guid prmDatabaseID)
        {
            Boolean _result = false;

            List<master_Database_aspnet_User> _databaseUserList = _companyBL.GetListDatabaseUser(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

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

        public void ShowData()
        {
            this.UserTextBox.Text = _userBL.GetUserNameByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));


            for (int i = 0; i < this.DatabaseCheckBoxList.Items.Count; i++)
            {
                this.DatabaseCheckBoxList.Items[i].Selected = this.IsDatabaseSelected(new Guid(this.DatabaseCheckBoxList.Items[i].Value));
            }
        }

        private void ShowDatabase()
        {
            this.DatabaseCheckBoxList.DataTextField = "Name";
            this.DatabaseCheckBoxList.DataValueField = "DatabaseID";
            this.DatabaseCheckBoxList.DataSource = this._databaseBL.GetListForCheckBox(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));
            this.DatabaseCheckBoxList.DataBind();
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage2 + "?" + this._compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
        }
    }
}