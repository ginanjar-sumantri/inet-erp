using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.RegistrationConfig
{
    public partial class RegistrationConfigEdit : RegistrationConfigBase
    {
        private RegistrationConfigBL _regConfigBL = new RegistrationConfigBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            String spawnJS = "<script language='JavaScript'>\n";
            ////////////////////DECLARE FUNCTION FOR CATCHING EMPLOYEE SEARCH
            spawnJS += "function findProduct(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.RegProductCodeTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.getElementById('" + this.RegProductNameTextBox.ClientID + "').value = dataArray [1];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            ///////////////////FUNCTION FOR KEYPRESS ENTER ON PRODUCT CODE
            spawnJS += "function enterRegProductCode() {\n";
            spawnJS += "document.getElementById('" + this.RegProductNameTextBox.ClientID + "').focus();\n";
            spawnJS += "return false;\n";
            spawnJS += "}\n";

            spawnJS += "</script>\n";
            this.javascriptReceiver.Text = spawnJS;

            this.RegProductCodeTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterRegProductCode();}");

            String spawnJS2 = "<script language='JavaScript'>\n";
            ////////////////////DECLARE FUNCTION FOR CATCHING EMPLOYEE SEARCH
            spawnJS2 += "function findProduct2(x) {\n";
            spawnJS2 += "dataArray = x.split ('|') ;\n";
            spawnJS2 += "document.getElementById('" + this.InstallationProductCodeTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS2 += "document.getElementById('" + this.InstallationProductNameTextBox.ClientID + "').value = dataArray [1];\n";
            spawnJS2 += "document.forms[0].submit();\n";
            spawnJS2 += "}\n";

            ///////////////////FUNCTION FOR KEYPRESS ENTER ON PRODUCT CODE
            spawnJS2 += "function enterInstallationProductCode() {\n";
            spawnJS2 += "document.getElementById('" + this.InstallationProductNameTextBox.ClientID + "').focus();\n";
            spawnJS2 += "return false;\n";
            spawnJS2 += "}\n";

            spawnJS2 += "</script>\n";
            this.javascriptReceiver1.Text = spawnJS2;

            this.InstallationProductCodeTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterInstallationProductCode();}");

            String spawnJS3 = "<script language='JavaScript'>\n";
            ////////////////////DECLARE FUNCTION FOR CATCHING EMPLOYEE SEARCH
            spawnJS3 += "function findProduct3(x) {\n";
            spawnJS3 += "dataArray = x.split ('|') ;\n";
            spawnJS3 += "document.getElementById('" + this.DepositProductCodeTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS3 += "document.getElementById('" + this.DepositProductNameTextBox.ClientID + "').value = dataArray [1];\n";
            spawnJS3 += "document.forms[0].submit();\n";
            spawnJS3 += "}\n";

            ///////////////////FUNCTION FOR KEYPRESS ENTER ON PRODUCT CODE
            spawnJS3 += "function enterDepositProductCode() {\n";
            spawnJS3 += "document.getElementById('" + this.DepositProductNameTextBox.ClientID + "').focus();\n";
            spawnJS3 += "return false;\n";
            spawnJS3 += "}\n";

            spawnJS3 += "</script>\n";
            this.javascriptReceiver2.Text = spawnJS3;

            this.DepositProductCodeTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterDepositProductCode();}");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowPaymentStatus();

                this.ShowData();
                this.ClearLabel();
                this.SetAttribute();
            }

            this.btnRegProductCode.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=productNoStock','_popSearch','width=750,height=300,toolbar=0,location=0,status=0,scrollbars=1')";
            this.btnInstallationProductCode.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct2&configCode=productNoStock','_popSearch','width=750,height=300,toolbar=0,location=0,status=0,scrollbars=1')";
            this.btnDepositProductCode.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct3&configCode=productNoStock','_popSearch','width=750,height=300,toolbar=0,location=0,status=0,scrollbars=1')";
        }

        private void SetAttribute()
        {
            this.RegFeeTextBox.Attributes.Add("OnBlur", "return ChangeFormat(" + this.RegFeeTextBox.ClientID + ",100);");
            this.InstalationFeeTextBox.Attributes.Add("OnBlur", "return ChangeFormat(" + this.InstalationFeeTextBox.ClientID + ",100);");
            this.DepositTextBox.Attributes.Add("OnBlur", "return ChangeFormat(" + this.DepositTextBox.ClientID + ",100);");
            this.RecurringFirstChargeTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.RecurringFirstChargeTextBox.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowPaymentStatus()
        {
            this.PaymentStatusRadioButtonList.Items.Insert(0, new ListItem(PaymentStatusDataMapper.GetPaymentStatusText(PaymentStatus.After), PaymentStatusDataMapper.GetPaymentStatusValue(PaymentStatus.After).ToString()));
            this.PaymentStatusRadioButtonList.Items.Insert(1, new ListItem(PaymentStatusDataMapper.GetPaymentStatusText(PaymentStatus.Before), PaymentStatusDataMapper.GetPaymentStatusValue(PaymentStatus.Before).ToString()));
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            BILMsRegistrationConfig _config = this._regConfigBL.GetSingleRegistrationConfig(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.RegistrationCodeTextBox.Text = _config.RegCode;
            this.RegistrationNameTextBox.Text = _config.RegName;
            this.PaymentStatusRadioButtonList.SelectedValue = _config.PaymentStatus;
            this.RegProductCodeTextBox.Text = _config.RegistrationProductCode;
            this.RegProductNameTextBox.Text = _productBL.GetProductNameByCode(_config.RegistrationProductCode);
            this.RegFeeTextBox.Text = Convert.ToDecimal(_config.RegistrationFee).ToString("#,##0.##");
            this.InstallationProductCodeTextBox.Text = _config.InstalationProductCode;
            this.InstallationProductNameTextBox.Text = _productBL.GetProductNameByCode(_config.InstalationProductCode);
            this.InstalationFeeTextBox.Text = Convert.ToDecimal(_config.InstalationFee).ToString("#,##0.##");
            this.DepositProductCodeTextBox.Text = _config.DepositProductCode;
            this.DepositProductNameTextBox.Text = _productBL.GetProductNameByCode(_config.DepositProductCode);
            this.DepositTextBox.Text = Convert.ToDecimal(_config.Deposit).ToString("#,##0.##");
            this.RemarkTextBox.Text = _config.Description;
            this.RecurringFirstChargeTextBox.Text = _config.RecurringFirstCharge.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            BILMsRegistrationConfig _config = this._regConfigBL.GetSingleRegistrationConfig(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _config.RegName = this.RegistrationNameTextBox.Text;
            _config.PaymentStatus = this.PaymentStatusRadioButtonList.SelectedValue;
            _config.RegistrationProductCode = this.RegProductCodeTextBox.Text;
            _config.RegistrationFee = Convert.ToDecimal(this.RegFeeTextBox.Text);
            _config.InstalationProductCode = this.InstallationProductCodeTextBox.Text;
            _config.InstalationFee = Convert.ToDecimal(this.InstalationFeeTextBox.Text);
            _config.DepositProductCode = this.DepositProductCodeTextBox.Text;
            _config.Deposit = Convert.ToDecimal(this.DepositTextBox.Text);
            _config.RecurringFirstCharge = Convert.ToInt32(this.RecurringFirstChargeTextBox.Text);
            _config.Description = this.RemarkTextBox.Text;

            _config.EditBy = HttpContext.Current.User.Identity.Name;
            _config.EditDate = DateTime.Now;

            bool _result = this._regConfigBL.EditRegistrationConfig(_config);

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
            this.ShowData();
        }

        protected void RegProductCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.RegProductCodeTextBox.Text != "")
            {
                this.RegProductNameTextBox.Text = _productBL.GetProductNameByCode(this.RegProductCodeTextBox.Text);
            }
        }

        protected void InstallationProductCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.InstallationProductCodeTextBox.Text != "")
            {
                this.InstallationProductNameTextBox.Text = _productBL.GetProductNameByCode(this.InstallationProductCodeTextBox.Text);
            }
        }

        protected void DepositProductCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.DepositProductCodeTextBox.Text != "")
            {
                this.DepositProductNameTextBox.Text = _productBL.GetProductNameByCode(this.DepositProductCodeTextBox.Text);
            }
        }
    }
}