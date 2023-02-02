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
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.UnSubscription
{
    public partial class UnSubscriptionAdd : UnSubscriptionBase
    {
        private BillingInvoiceBL _billingInvoiceBL = new BillingInvoiceBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private CustomerBL _customerBL = new CustomerBL();
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();
        private SalesConfirmationBL _salesConfirmationBL = new SalesConfirmationBL();
        private UnSubscriptionBL _unSubscriptionBL = new UnSubscriptionBL();

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

                this.Customer();
                this.ClearData();
                this.SetAttribute();
            }
        }


        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
        }

        protected void Customer()
        {
            this.CustCodeDDL.Items.Clear();
            this.CustCodeDDL.DataTextField = "CustName";
            this.CustCodeDDL.DataValueField = "CustCode";
            this.CustCodeDDL.DataSource = this._customerBL.GetListCustomerForDDLfromCustomerBillAccount();
            this.CustCodeDDL.DataBind();
            this.CustCodeDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            this.WarningLabel.Text = "";
            DateTime _now = DateTime.Now;

            this.DateTextBox.Text = DateFormMapper.GetValue(_now);
            this.RemarkTextBox.Text = "";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            BILTrUnSubscriptionHd _bilTrUnSub = new BILTrUnSubscriptionHd();

            _bilTrUnSub.FileNmbr = "";
            _bilTrUnSub.TransDate = Convert.ToDateTime(this.DateTextBox.Text);
            _bilTrUnSub.CustCode = this.CustCodeDDL.SelectedValue;
            _bilTrUnSub.Status = UnSubDataMapper.GetStatusByte(TransStatus.OnHold);
            _bilTrUnSub.Remark = this.RemarkTextBox.Text;
            _bilTrUnSub.CreatedBy = HttpContext.Current.User.Identity.Name;
            _bilTrUnSub.CreatedDate = DateTime.Now;
            _bilTrUnSub.EditBy = HttpContext.Current.User.Identity.Name;
            _bilTrUnSub.EditDate = DateTime.Now;

            String _generatedTransNmbr = _unSubscriptionBL.AddtoBILTrUnSub(_bilTrUnSub);

            if (_generatedTransNmbr != "")
            {
                Response.Redirect(_detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_generatedTransNmbr, ApplicationConfig.EncryptionKey)));
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
            this.ClearData();
        }
    }
}