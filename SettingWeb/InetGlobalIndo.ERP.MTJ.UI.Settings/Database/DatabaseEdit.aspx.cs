using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Database
{
    public partial class DatabaseEdit : DatabaseBase
    {
        private DatabaseBL _database = new DatabaseBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            master_Database _master_Database = this._database.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.NameTextBox.Text = _master_Database.Name;
            this.ServerTextBox.Text = _master_Database.Server;
            this.UIDTextBox.Text = _master_Database.UID;
            this.PWDTextBox.Text = _master_Database.PWD;
            this.StatusDropDownList.SelectedValue = _master_Database.Status.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            master_Database _master_Database = this._database.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _master_Database.Name = this.NameTextBox.Text;
            _master_Database.Server = this.ServerTextBox.Text;
            _master_Database.UID = this.UIDTextBox.Text;
            _master_Database.PWD = Rijndael.Encrypt(this.PWDTextBox.Text, ApplicationConfig.PasswordEncryptionKey);
            _master_Database.Status = Convert.ToByte(this.StatusDropDownList.SelectedValue);

            bool _result = this._database.Edit(_master_Database);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}