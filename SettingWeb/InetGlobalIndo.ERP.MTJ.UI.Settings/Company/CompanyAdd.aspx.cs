using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Company
{
    public partial class CompanyAdd : CompanyBase
    {
        private CompanyBL _company = new CompanyBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ClearLabel();
                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.TaxBranchNoTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
        }

        public void ClearData()
        {
            this.ClearLabel();
            this.NameTextBox.Text = "";
            this.LogoTextBox.Text = "";
            this.AddressTextBox.Text = "";
            this.CompanyTagTextBox.Text = "";
            this.TaxBranchNoTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            master_Company _master_Company = new master_Company();
            master_Company _master_Company2 = new master_Company();

            _master_Company.CompanyID = Guid.NewGuid();
            _master_Company.Name = this.NameTextBox.Text;
            _master_Company.Logo = this.LogoTextBox.Text;
            _master_Company.PrimaryAddress = this.AddressTextBox.Text;
            _master_Company.CompanyTag = this.CompanyTagTextBox.Text;
            _master_Company.TaxBranchNo = this.TaxBranchNoTextBox.Text;
            _master_Company.@default = this.DefaultCompanyCheckBox.Checked;


            //Mengedit Settingan Company Default yang telah ada jika Default Checkbox true
            if (this.DefaultCompanyCheckBox.Checked == true)
            {
                _master_Company2 = this._company.GetCompanyIDForPOS();
                if (_master_Company2 != null)
                {
                    _master_Company2.@default = false;

                    bool _result2 = _company.Edit(_master_Company2);
                }
            }

            bool _result = _company.Add(_master_Company);

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