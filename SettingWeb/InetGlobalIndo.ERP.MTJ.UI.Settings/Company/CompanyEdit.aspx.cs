using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Company
{
    public partial class CompanyEdit : CompanyBase
    {
        private CompanyBL _companyBL = new CompanyBL();

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
                this.SetAttribute();

                this.ShowData();
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

        public void ShowData()
        {
            master_Company _master_Company = this._companyBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey));

            this.NameTextBox.Text = _master_Company.Name;
            this.LogoTextBox.Text = _master_Company.Logo;
            this.AddressTextBox.Text = _master_Company.PrimaryAddress;
            this.CompanyTagTextBox.Text = _master_Company.CompanyTag;
            this.TaxBranchNoTextBox.Text = _master_Company.TaxBranchNo;
            this.DefaultCompanyCheckBox.Checked = (_master_Company.@default == null) ? false : Convert.ToBoolean(_master_Company.@default);
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            master_Company _master_Company = this._companyBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey));
            master_Company _master_Company2 = new master_Company();

            _master_Company.Name = this.NameTextBox.Text;
            _master_Company.Logo = this.LogoTextBox.Text;
            _master_Company.PrimaryAddress = this.AddressTextBox.Text;
            _master_Company.CompanyTag = this.CompanyTagTextBox.Text;
            _master_Company.TaxBranchNo = this.TaxBranchNoTextBox.Text;
            _master_Company.@default = this.DefaultCompanyCheckBox.Checked == true ? true : false;

            if (this.DefaultCompanyCheckBox.Checked == true)
            {
                _master_Company2 = this._companyBL.GetCompanyIDForPOS();
                if (_master_Company2 != null)
                {
                    _master_Company2.@default = false;

                    bool _result2 = _companyBL.Edit(_master_Company2);
                }
            }

            bool _result = this._companyBL.Edit(_master_Company);

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