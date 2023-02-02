using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.BackEndSMSPortal.BackEndSetting {
    public partial class BackEndSetting_BackEndSetting : AdminSMSWebBase
    {
        protected BackEndBL _backEndBL = new BackEndBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                this.MaskingPriceTextBox.Text = "0";
                this.MaskingPriceTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.MaskingPriceTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.MaskingCIDTextBox.Text = _backEndBL.getSettingValue(1);
                this.MaskingPriceTextBox.Text = Convert.ToDecimal( _backEndBL.getSettingValue(2)).ToString("0.00");
                this.MaskingPWDTextBox.Text = _backEndBL.getSettingValue(3);
                this.MaskingURLTextBox.Text = _backEndBL.getSettingValue(4);
                this.WebDomainNameTextBox.Text = _backEndBL.getSettingValue(7);

                this.BackEndAdminUserTextBox.Text = _backEndBL.getSettingValue(6);
                this.BackEndAdminPasswordTextBox.Text = Rijndael.Decrypt( _backEndBL.getSettingValue(5), ApplicationConfig.EncryptionKey);
            }
            
        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            _backEndBL.updateBackEndSetting(this.MaskingCIDTextBox.Text,
                this.MaskingPriceTextBox.Text, this.MaskingPWDTextBox.Text, MaskingURLTextBox.Text,
                this.WebDomainNameTextBox.Text);
            this.WarnningLabel.Text = "Update Data Success.";
        }
        protected void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            if ((this.BackEndAdminUserTextBox.Text != "") && (this.BackEndAdminPasswordTextBox.Text != ""))
            {
                _backEndBL.ChangePassword(this.BackEndAdminUserTextBox.Text, this.BackEndAdminPasswordTextBox.Text);
                this.WarnningLabel.Text = "Update Data Success.";
            }
            else
                this.WarnningLabel.Text = "Please input User and Password.";
        }
}
}
