using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.UI.Billing.UnSubscription;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.UnSubscription
{
    public partial class UnSubscriptionDetailAdd : UnSubscriptionBase
    {
        private DirectSalesBL _directSalesBL = new DirectSalesBL();
        private ProductBL _productBL = new ProductBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CustomerBL _customerBL = new CustomerBL();
        private UnSubscriptionBL _unSubscriptionBL = new UnSubscriptionBL();

        private byte _decimalPlace = 0;

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
                String spawnJS = "<script language='JavaScript'>\n";
                String _custCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
                this.btnSearchProductCode.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findunsubscriptiondt_add&configCode=UnSubscriptionDt_add&paramWhere=A.CustCodesamadenganpetik" + _custCode + "petik','_popSearch','width=500,height=300,toolbar=0,location=0,status=0,scrollbars=1')";
                ////////////////////DECLARE FUNCTION FOR CATCHING PRODUCT SEARCH
                spawnJS += "function findunsubscriptiondt_add(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.CustBillAccountTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.getElementById('" + this.ProductNameTextBox.ClientID + "').value = dataArray [1];\n";
                spawnJS += "document.getElementById('" + this.CurrTextBox.ClientID + "').value = dataArray [2];\n";
                spawnJS += "document.getElementById('" + this.AmountTextBox.ClientID + "').value = dataArray [3];\n";
                spawnJS += "document.getElementById('" + this.TypePaymentTextBox.ClientID + "').value = dataArray [4];\n";
                spawnJS += "document.getElementById('" + this.ActivateDateTextBox.ClientID + "').value = dataArray [5];\n";
                spawnJS += "document.getElementById('" + this.ExpiredDateTextBox.ClientID + "').value = dataArray [6];\n";
                spawnJS += "document.getElementById('" + this.CustBillCodeHiddenFiled.ClientID + "').value = dataArray [7];\n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.CustBillAccountTextBox.Text != "" && this.CustBillCodeHiddenFiled.Value != "")
            {
                BILTrUnSubscriptionDt _bilTrUnSubscriptionDt = _unSubscriptionBL.GetSingleUnSubscriptionDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), this.CustBillCodeHiddenFiled.Value);
                if (_bilTrUnSubscriptionDt == null)
                {

                    BILTrUnSubscriptionDt _unSubscriptionDt = new BILTrUnSubscriptionDt();

                    string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

                    _unSubscriptionDt.TransNmbr = _transNo;

                    _unSubscriptionDt.CustBillCode = new Guid(this.CustBillCodeHiddenFiled.Value);

                    bool _result = this._unSubscriptionBL.AddUnSubscriptionDt(_unSubscriptionDt);

                    if (_result == true)
                    {
                        Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                    }
                    else
                    {
                        this.WarningLabel.Text = "Your Failed Add Data";
                    }
                }
                else
                {
                    this.WarningLabel.Text = "Product Code & Warehouse has been inputted";
                }
            }
            else
            {
                this.WarningLabel.Text = "Please Insert Customer Billing Account";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}