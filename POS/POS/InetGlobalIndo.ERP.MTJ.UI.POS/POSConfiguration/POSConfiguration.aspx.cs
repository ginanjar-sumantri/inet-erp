using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.POSConfiguration
{
    public partial class POSConfigurationAdd : POSConfigurationBase
    {
        private POSConfigurationBL _posConfigurationBLBL = new POSConfigurationBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }
                       

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton1.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.SaveButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.SaveButton3.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.SaveButton4.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.SaveButton5.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                
                this.IgnoreItemDiscountLabel.Text = "IgnoreItemDiscount";
                this.POSBookingTimeLimitAfterLabel.Text = "POSBookingTimeLimitAfter";
                this.POSBookingTimeLimitBeforeLabel.Text = "POSBookingTimeLimitBefore";
                this.POSDefaultCustCodeLabel.Text = "POSDefaultCustCode";
                this.POSRoundingLabel.Text = "POSRounding";

                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    this.SaveButton1.Visible = false;
                    this.SaveButton2.Visible = false;
                    this.SaveButton3.Visible = false;
                    this.SaveButton4.Visible = false;
                    this.SaveButton5.Visible = false;
                }

                this.ShowData1();
                this.ShowData2();
                this.ShowData3();
                this.ShowData4();
                this.ShowData5();
                this.SetAttribute();
            }
        }

        private void SetAttribute()
        {
            this.IgnoreItemDiscountTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.IgnoreItemDiscountTextBox.ClientID + "," + this.IgnoreItemDiscountTextBox.ClientID + ",500" + ");");
            this.POSBookingTimeLimitBeforeTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.POSBookingTimeLimitBeforeTextBox.ClientID + "," + this.POSBookingTimeLimitBeforeTextBox.ClientID + ",500" + ");");
            this.POSBookingTimeLimitAfterTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.POSBookingTimeLimitAfterTextBox.ClientID + "," + this.POSBookingTimeLimitAfterTextBox.ClientID + ",500" + ");");
            this.POSRoundingTextBox.Attributes.Add("OnKeyup", "return formatangka2(" + this.POSRoundingTextBox.ClientID + "," + this.POSRoundingTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData1()
        {
            CompanyConfiguration _companyConfiguration = this._posConfigurationBLBL.GetSingle(this.IgnoreItemDiscountLabel.Text);

            this.IgnoreItemDiscountLabel.Text = _companyConfiguration.ConfigCode;
            this.IgnoreItemDiscountTextBox.Text = _companyConfiguration.SetValue;
        }

        public void ShowData2()
        {
            CompanyConfiguration _companyConfiguration = this._posConfigurationBLBL.GetSingle(this.POSBookingTimeLimitAfterLabel.Text);

            this.POSBookingTimeLimitAfterLabel.Text = _companyConfiguration.ConfigCode;
            this.POSBookingTimeLimitAfterTextBox.Text = _companyConfiguration.SetValue;
        }

        public void ShowData3()
        {
            CompanyConfiguration _companyConfiguration = this._posConfigurationBLBL.GetSingle(this.POSBookingTimeLimitBeforeLabel.Text);

            this.POSBookingTimeLimitBeforeLabel.Text = _companyConfiguration.ConfigCode;
            this.POSBookingTimeLimitBeforeTextBox.Text = _companyConfiguration.SetValue;
        }

        public void ShowData4()
        {
            CompanyConfiguration _companyConfiguration = this._posConfigurationBLBL.GetSingle(this.POSDefaultCustCodeLabel.Text);

            this.POSDefaultCustCodeLabel.Text = _companyConfiguration.ConfigCode;
            this.POSDefaultCustCodeValueLabel.Text = _companyConfiguration.SetValue;
        }

        public void ShowData5()
        {
            CompanyConfiguration _companyConfiguration = this._posConfigurationBLBL.GetSingle(this.POSRoundingLabel.Text);

            this.POSRoundingLabel.Text = _companyConfiguration.ConfigCode;
            this.POSRoundingTextBox.Text = _companyConfiguration.SetValue;
        }

        protected void SaveButton_Click1(object sender, ImageClickEventArgs e)
        {
            CompanyConfiguration _companyConfiguration = this._posConfigurationBLBL.GetSingle(this.IgnoreItemDiscountLabel.Text);

            //_companyConfiguration.ConfigCode = this.IgnoreItemDiscountLabel.Text;
            _companyConfiguration.SetValue = this.IgnoreItemDiscountTextBox.Text;

            bool _result = this._posConfigurationBLBL.Edit(_companyConfiguration);

            if (_result == true)
            {
                //Response.Redirect(this._homePage);
                this.ClearLabel();
                this.WarningLabel.Text = "You Succeded Edit Data";
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void SaveButton_Click2(object sender, ImageClickEventArgs e)
        {
            CompanyConfiguration _companyConfiguration = this._posConfigurationBLBL.GetSingle(this.POSBookingTimeLimitAfterLabel.Text);

            //_companyConfiguration.ConfigCode = this.POSBookingTimeLimitAfterLabel.Text;
            _companyConfiguration.SetValue = this.POSBookingTimeLimitAfterTextBox.Text;

            bool _result = this._posConfigurationBLBL.Edit(_companyConfiguration);

            if (_result == true)
            {
                //Response.Redirect(this._homePage);
                this.ClearLabel();
                this.WarningLabel.Text = "You Succeded Add Data";
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void SaveButton_Click3(object sender, ImageClickEventArgs e)
        {
            CompanyConfiguration _companyConfiguration = this._posConfigurationBLBL.GetSingle(this.POSBookingTimeLimitBeforeLabel.Text);

            //_msMemberType.MemberTypeCode = this.POSBookingTimeLimitBeforeLabel.Text;
            _companyConfiguration.SetValue = this.POSBookingTimeLimitBeforeTextBox.Text;

            bool _result = this._posConfigurationBLBL.Edit(_companyConfiguration);

            if (_result == true)
            {
                //Response.Redirect(this._homePage);
                this.ClearLabel();
                this.WarningLabel.Text = "You Succeded Add Data";
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void SaveButton_Click4(object sender, ImageClickEventArgs e)
        {
            CompanyConfiguration _companyConfiguration = this._posConfigurationBLBL.GetSingle(this.POSDefaultCustCodeLabel.Text);

            //_msMemberType.MemberTypeCode = this.POSDefaultCustCodeLabel.Text;
            _companyConfiguration.SetValue = this.POSDefaultCustCodeValueLabel.Text;

            bool _result = this._posConfigurationBLBL.Edit(_companyConfiguration);

            if (_result == true)
            {
                //Response.Redirect(this._homePage);
                this.ClearLabel();
                this.WarningLabel.Text = "You Succeded Add Data";
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void SaveButton_Click5(object sender, ImageClickEventArgs e)
        {
            CompanyConfiguration _companyConfiguration = this._posConfigurationBLBL.GetSingle(this.POSRoundingLabel.Text);

            //_companyConfiguration.MemberTypeCode = this.POSRoundingLabel.Text;
            _companyConfiguration.SetValue = this.POSRoundingTextBox.Text;

            bool _result = this._posConfigurationBLBL.Edit(_companyConfiguration);

            if (_result == true)
            {
                //Response.Redirect(this._homePage);
                this.ClearLabel();
                this.WarningLabel.Text = "You Succeded Add Data";
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }
    }
}