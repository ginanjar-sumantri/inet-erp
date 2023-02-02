using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Company
{
    public partial class CompanyDatabaseAdd : CompanyBase
    {
        private CompanyBL _companyBL = new CompanyBL();
        private DatabaseBL _databaseBL = new DatabaseBL();

              protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral2;
                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowDatabase();

                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.DatabaseDropDownList.SelectedValue = "null";
        }

        private void ShowDatabase()
        {
            this.DatabaseDropDownList.DataTextField = "Name";
            this.DatabaseDropDownList.DataValueField = "DatabaseID";
            this.DatabaseDropDownList.DataSource = this._databaseBL.GetListForDDL();
            this.DatabaseDropDownList.DataBind();
            this.DatabaseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            master_Company_master_Database _master_Company_master_Database = new master_Company_master_Database();

            _master_Company_master_Database.CompanyID = new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey));
            _master_Company_master_Database.DatabaseID = new Guid(this.DatabaseDropDownList.SelectedValue);

            bool _result = _companyBL.AddCompanyDatabase(_master_Company_master_Database);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}