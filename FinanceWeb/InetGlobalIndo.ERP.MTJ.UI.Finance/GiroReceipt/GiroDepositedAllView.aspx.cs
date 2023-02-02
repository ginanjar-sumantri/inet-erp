using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroReceipt
{
    public partial class GiroDepositedAllView : GiroReceiptBase
    {
        private SupplierBL _suppBL = new SupplierBL();
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private BankBL _bankBL = new BankBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private FINGiroInBL _finGiroInBL = new FINGiroInBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _year = DateTime.Now.Year;
        private int _period = DateTime.Now.Month;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ShowData();
            }
        }

        public void ShowData()
        {
            FINGiroIn _finGiroIn = this._finGiroInBL.GetSingleFINGiroIn(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.GiroNoTextBox.Text = _finGiroIn.GiroNo;
            this.ReceiptNoTextBox.Text = _finGiroIn.FileNmbr;
            this.ReceiptDateTextBox.Text = DateFormMapper.GetValue(_finGiroIn.ReceiptDate);
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_finGiroIn.DueDate);
            if (_finGiroIn.SuppCode != null)
            {
                this.ReceiptCodeTextBox.Text = _finGiroIn.SuppCode;
                this.ReceiptNameTextBox.Text = _suppBL.GetSuppNameByCode(_finGiroIn.SuppCode);
            }
            else if (_finGiroIn.CustCode != null)
            {
                this.ReceiptCodeTextBox.Text = _finGiroIn.CustCode;
                this.ReceiptNameTextBox.Text = _custBL.GetNameByCode(_finGiroIn.CustCode);
            }
            this.BankGiroTextBox.Text = _bankBL.GetBankNameByCode(_finGiroIn.BankGiro);
            this.CurrCodeTextBox.Text = _finGiroIn.CurrCode;
            this.RateTextBox.Text = _finGiroIn.ForexRate.ToString("#,###.##");
            this.AmountTextBox.Text = _finGiroIn.AmountForex.ToString("#,###.##");
            this.RemarkTextBox.Text = _finGiroIn.Remark;
            this.StatusLabel.Text = GiroReceiptDataMapper.GetStatusText(_finGiroIn.Status);
            this.StatusHiddenField.Value = _finGiroIn.Status.ToString();
        }
    }
}