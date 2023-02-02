using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Contract
{
    public partial class ContractEdit : ContractBase
    {
        private CustomerBL _customerBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();
        private ContractBL _contractBL = new ContractBL();
        private SalesConfirmationBL _salesConfirmationBL = new SalesConfirmationBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.SetAttribute();

                this.ShowData();
            }
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
        }

        public void ShowData()
        {
            BILTrContract _bilTrContract = _contractBL.GetSingleContract(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            BILTrSalesConfirmation _bilTrSalesConfirmation = _salesConfirmationBL.GetSingleSalesConfirmation(_bilTrContract.SalesConfirmationNoRef);

            this.TransNmbrTextBox.Text = _bilTrContract.TransNmbr;
            this.FileNmbrTextBox.Text = _bilTrContract.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_bilTrContract.TransDate);
            this.SalesConfirmationNoRefTextBox.Text = _bilTrSalesConfirmation.FileNmbr;
            this.CompanyNameTextBox.Text = _bilTrContract.CompanyName;
            this.ResponsibleNameTextBox.Text = _bilTrContract.ResponsibleName;
            this.TitleNameTextBox.Text = _bilTrContract.TitleName;
            this.LetteProviderInformationTextBox.Text = _bilTrContract.LetterProviderInformation;
            this.LetteCustomerInformationTextBox.Text = _bilTrContract.LetterCustomerInformation;
            this.FinaceCustomerPICTextBox.Text = _bilTrContract.FinanceCustomerPIC;
            this.FinanceCustomerPhoneTextBox.Text = _bilTrContract.FinanceCustomerPhone;
            this.FinanceCustomerFaxTextBox.Text = _bilTrContract.FinanceCustomerFax;
            this.FinanceCustomerEmailTextBox.Text = _bilTrContract.FinanceCustomerEmail;
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            BILTrContract _bilTrContract = _contractBL.GetSingleContract(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _bilTrContract.TransDate = DateFormMapper.GetValue(this.DateTextBox.Text);
            _bilTrContract.ResponsibleName = this.ResponsibleNameTextBox.Text;
            _bilTrContract.TitleName = this.TitleNameTextBox.Text;
            _bilTrContract.LetterProviderInformation = this.LetteProviderInformationTextBox.Text;
            _bilTrContract.LetterCustomerInformation = this.LetteCustomerInformationTextBox.Text;
            _bilTrContract.FinanceCustomerPIC = this.FinaceCustomerPICTextBox.Text;
            _bilTrContract.FinanceCustomerPhone = this.FinanceCustomerPhoneTextBox.Text;
            _bilTrContract.FinanceCustomerFax = this.FinanceCustomerFaxTextBox.Text;
            _bilTrContract.FinanceCustomerEmail = this.FinanceCustomerEmailTextBox.Text;

            bool _result = _contractBL.EditContract(_bilTrContract);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid == true)
            {
                BILTrContract _bilTrContract = _contractBL.GetSingleContract(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _bilTrContract.TransDate = DateFormMapper.GetValue(this.DateTextBox.Text);
                _bilTrContract.ResponsibleName = this.ResponsibleNameTextBox.Text;
                _bilTrContract.TitleName = this.TitleNameTextBox.Text;
                _bilTrContract.LetterProviderInformation = this.LetteProviderInformationTextBox.Text;
                _bilTrContract.LetterCustomerInformation = this.LetteCustomerInformationTextBox.Text;
                _bilTrContract.FinanceCustomerPIC = this.FinaceCustomerPICTextBox.Text;
                _bilTrContract.FinanceCustomerPhone = this.FinanceCustomerPhoneTextBox.Text;
                _bilTrContract.FinanceCustomerFax = this.FinanceCustomerFaxTextBox.Text;
                _bilTrContract.FinanceCustomerEmail = this.FinanceCustomerEmailTextBox.Text;

                bool _result = _contractBL.EditContract(_bilTrContract);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Edit Data";
                }
            }
        }
    }
}