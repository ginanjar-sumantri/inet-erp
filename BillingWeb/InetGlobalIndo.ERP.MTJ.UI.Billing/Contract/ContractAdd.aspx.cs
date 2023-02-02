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

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Contract
{
    public partial class ContractAdd : ContractBase
    {
        private BillingInvoiceBL _billingInvoiceBL = new BillingInvoiceBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private CustomerBL _customerBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();
        private SalesConfirmationBL _salesConfirmationBL = new SalesConfirmationBL();
        private ContractBL _contractBL = new ContractBL();

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

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                
                this.SalesConfirmationNoRef();

                this.ClearData();
                this.SetAttribute();
            }
        }

    
        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.CompanyNameTextBox.Attributes.Add("ReadOnly", "True");
        }

        protected void SalesConfirmationNoRef()
        {
            this.SalesConfirmationNoRefDDL.Items.Clear();
            this.SalesConfirmationNoRefDDL.DataTextField = "FileNmbr";
            this.SalesConfirmationNoRefDDL.DataValueField = "TransNmbr";
            this.SalesConfirmationNoRefDDL.DataSource = this._salesConfirmationBL.GetListForDDLSalesConfirmation();
            this.SalesConfirmationNoRefDDL.DataBind();
            this.SalesConfirmationNoRefDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            this.WarningLabel.Text = "";
            DateTime _now = DateTime.Now;

            this.DateTextBox.Text = DateFormMapper.GetValue(_now);
            this.SalesConfirmationNoRefDDL.SelectedValue = "null";
            this.CompanyNameTextBox.Text = "";
            this.ResponsibleNameTextBox.Text = "";
            this.TitleNameTextBox.Text = "";
            this.LetteProviderInformationTextBox.Text = "";
            this.LetteCustomerInformationTextBox.Text = "";
            this.FinaceCustomerPICTextBox.Text = "";
            this.FinanceCustomerPhoneTextBox.Text = "";
            this.FinanceCustomerFaxTextBox.Text = "";
            this.FinanceCustomerEmailTextBox.Text = "";
        }

        protected void SalesConfirmationNoRefDDL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SalesConfirmationNoRefDDL.SelectedValue != "null")
            {
                BILTrSalesConfirmation _bilTrSalesConfirmation = _salesConfirmationBL.GetSingleSalesConfirmation(this.SalesConfirmationNoRefDDL.SelectedValue);
                this.CompanyNameTextBox.Text = _bilTrSalesConfirmation.CompanyName;
            }
            else
            {
                this.CompanyNameTextBox.Text = "";

            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            BILTrContract _contract = _contractBL.GetSingleContract(this.SalesConfirmationNoRefDDL.SelectedValue);

            if (_contract == null)
            {
                BILTrContract _bilTrContract = new BILTrContract();
                
                _bilTrContract.FileNmbr = "";
                _bilTrContract.TransDate = Convert.ToDateTime(this.DateTextBox.Text);
                _bilTrContract.SalesConfirmationNoRef = this.SalesConfirmationNoRefDDL.SelectedValue;
                _bilTrContract.CompanyName = this.CompanyNameTextBox.Text;
                _bilTrContract.ResponsibleName = this.ResponsibleNameTextBox.Text;
                _bilTrContract.TitleName = this.TitleNameTextBox.Text;
                _bilTrContract.LetterProviderInformation = this.LetteProviderInformationTextBox.Text;
                _bilTrContract.LetterCustomerInformation = this.LetteCustomerInformationTextBox.Text;
                _bilTrContract.FinanceCustomerPIC = this.FinaceCustomerPICTextBox.Text;
                _bilTrContract.FinanceCustomerPhone = this.FinanceCustomerPhoneTextBox.Text;
                _bilTrContract.FinanceCustomerFax = this.FinanceCustomerFaxTextBox.Text;
                _bilTrContract.FinanceCustomerEmail = this.FinanceCustomerEmailTextBox.Text;
                _bilTrContract.Status = ContractDataMapper.GetStatusByte(TransStatus.OnHold);

                String _generatedTransNmbr = _contractBL.AddtoBILTrContract(_bilTrContract);

                if (_generatedTransNmbr != "")
                {
                    Response.Redirect(_homePage);
                }
                else 
                {
                    this.WarningLabel.Text = "Your Failed Add Data";
                }
            }
            else
            {
                this.WarningLabel.Text = "Trans Number has Been Add";
            }

            //Billing_InvoiceHd _billingInvoiceHd = new Billing_InvoiceHd();

            //_billingInvoiceHd.InvoiceHd = Guid.NewGuid();
            //_billingInvoiceHd.Period = Convert.ToInt32(this.PeriodTextBox.Text);
            //_billingInvoiceHd.Year = Convert.ToInt32(this.YearTextBox.Text);
            //_billingInvoiceHd.Status = BillingInvoiceDataMapper.GetStatus(TransStatus.OnHold);
            //_billingInvoiceHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //_billingInvoiceHd.Attn = this.AttnTextBox.Text;
            //_billingInvoiceHd.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            //_billingInvoiceHd.CustCode = this.CustomerDropDownList.SelectedValue;
            //_billingInvoiceHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            //_billingInvoiceHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            //_billingInvoiceHd.Term = this.TermDropDownList.SelectedValue;
            //_billingInvoiceHd.PPNNo = this.PPNNoTextBox.Text;
            //_billingInvoiceHd.TransNmbr = "";

            //if (this.PPNDateTextBox.Text != "")
            //{
            //    _billingInvoiceHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            //}
            //else
            //{
            //    _billingInvoiceHd.PPNDate = null;
            //}

            //_billingInvoiceHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            //_billingInvoiceHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            //_billingInvoiceHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            //_billingInvoiceHd.DiscPercent = Convert.ToDecimal(this.DiscPercentTextBox.Text);
            //_billingInvoiceHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
            //_billingInvoiceHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            //_billingInvoiceHd.StampFee = Convert.ToDecimal(this.StampFeeTextBox.Text);
            //_billingInvoiceHd.OtherFee = Convert.ToDecimal(this.OtherFeeTextBox.Text);
            //_billingInvoiceHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            //_billingInvoiceHd.Remark = this.RemarkTextBox.Text;

            //_billingInvoiceHd.InsertBy = HttpContext.Current.User.Identity.Name;
            //_billingInvoiceHd.InsertDate = DateTime.Now;
            //_billingInvoiceHd.EditBy = HttpContext.Current.User.Identity.Name;
            //_billingInvoiceHd.EditDate = DateTime.Now;

            //Boolean _result = this._billingInvoiceBL.AddBillingInvoiceHd(_billingInvoiceHd);

            //if (_result == true)
            //{
            //    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_billingInvoiceHd.InvoiceHd.ToString(), ApplicationConfig.EncryptionKey)));
            //}
            //else
            //{
            //    this.WarningLabel.Text = "You Failed Add Data";
            //}
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

    }
}