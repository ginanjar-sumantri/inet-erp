using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Database
{
    public partial class DatabaseAdd : DatabaseBase
    {
        private DatabaseBL _databaseBL = new DatabaseBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

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
            this.NameTextBox.Text = "";
            this.ServerTextBox.Text = "";
            this.UIDTextBox.Text = "";
            this.PWDTextBox.Text = "";
            this.StatusDropDownList.SelectedValue = "null";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            master_Database _master_Database = new master_Database();

            _master_Database.DatabaseID = Guid.NewGuid();
            _master_Database.Name = this.NameTextBox.Text;
            _master_Database.Server = this.ServerTextBox.Text;
            _master_Database.UID = this.UIDTextBox.Text;
            _master_Database.PWD = Rijndael.Encrypt(this.PWDTextBox.Text, ApplicationConfig.PasswordEncryptionKey);
            _master_Database.Status = Convert.ToByte(this.StatusDropDownList.SelectedValue);

            bool _result = _databaseBL.Add(_master_Database);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}