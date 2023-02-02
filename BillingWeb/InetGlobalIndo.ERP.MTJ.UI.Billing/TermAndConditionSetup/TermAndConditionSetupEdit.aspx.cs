using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.TermAndConditionSetup
{
    public partial class TermAndConditionSetupEdit : TermAndConditionSetupBase
    {
        private TermAndConditionSetupBL _setupBL = new TermAndConditionSetupBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.TypeTextBox.Attributes.Add("ReadOnly", "true");
        }

        protected void ShowData()
        {
            BILMsTermAndConditionSetup _termAndConditionSetup = this._setupBL.GetSingle(Convert.ToByte(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            this.TypeTextBox.Text = TermAndConditionDataMapper.GetTypeText(_termAndConditionSetup.Type);
            this.BodyTextBox.Text = _termAndConditionSetup.Body;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            BILMsTermAndConditionSetup _masterTermAndConditionSetup = _setupBL.GetSingle(Convert.ToByte(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            _masterTermAndConditionSetup.Body = this.BodyTextBox.Text;

            bool _result = this._setupBL.Edit(_masterTermAndConditionSetup);

            if (_result == true)
            {
                Response.Redirect(_homePage);
            }
            else
            {
                this.WarningLabel.Text = "Edit data gagal";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}
