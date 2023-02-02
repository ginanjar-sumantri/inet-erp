using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierNote
{
    public partial class SupplierNoteDetail2Edit : SupplierNoteBase
    {
        private SupplierNoteBL _supplierNoteBL = new SupplierNoteBL();
        private UnitBL _unitBL = new UnitBL();
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _customerBL = new CustomerBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowUnit();

                this.ShowData();
                this.SetAttribute();
            }
        }

        public void SetAttribute()
        {
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");

            this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            this.ClearLabel();

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _wrhsCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._wrhsKey), ApplicationConfig.EncryptionKey);
            string _wrhsSubled = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._subledKey), ApplicationConfig.EncryptionKey);
            string _wrhsLoc = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            FINSuppInvDt2 _finSuppInvDt2 = this._supplierNoteBL.GetSingleFINSuppInvDt2(_transNo, _wrhsCode, _wrhsSubled, _wrhsLoc, _productCode);

            FINSuppInvHd _finSuppInvHd = _supplierNoteBL.GetSingleFINSuppInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finSuppInvHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(_finSuppInvDt2.WrhsCode);

            if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            {
                this.WarehouseSubledTextBox.Text = _customerBL.GetNameByCode(_finSuppInvDt2.WrhsSubLed);
            }
            else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
            {
                this.WarehouseSubledTextBox.Text = _supplierBL.GetSuppNameByCode(_finSuppInvDt2.WrhsSubLed);
            }
            else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.WarehouseSubledTextBox.Text = "";
            }
            else
            {
                this.WarehouseSubledTextBox.Text = "";
            }
            this.WarehouseTextBox.Text = _warehouseBL.GetWarehouseNameByCode(_finSuppInvDt2.WrhsCode);
            this.LocationTextBox.Text = _warehouseBL.GetWarehouseLocationNameByCode(_finSuppInvDt2.LocationCode);
            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_finSuppInvDt2.ProductCode);
            this.QtyTextBox.Text = (_finSuppInvDt2.Qty == 0) ? "0" : _finSuppInvDt2.Qty.ToString("#,###.##");
            this.UnitDropDownList.SelectedValue = _finSuppInvDt2.Unit;

            this.PriceTextBox.Text = (_finSuppInvDt2.PriceForex == 0 || _finSuppInvDt2.PriceForex == null) ? "0" : Convert.ToDecimal(_finSuppInvDt2.PriceForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountTextBox.Text = (_finSuppInvDt2.PriceForex == 0 || _finSuppInvDt2.AmountForex == null) ? "0" : Convert.ToDecimal(_finSuppInvDt2.AmountForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finSuppInvDt2.Remark;
        }

        //public void ShowAccount()
        //{
        //    string _currHeader = this._supplierNoteBL.GetSingleFINSuppInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).CurrCode;

        //    this.AccountDropDownList.Items.Clear();
        //    this.AccountDropDownList.DataTextField = "AccountName";
        //    this.AccountDropDownList.DataValueField = "Account";
        //    this.AccountDropDownList.DataSource = this._empBL.GetListAccountByTransType(_currHeader, _userEmpBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name), AppModule.GetValue(TransactionType.SupplierNote));
        //    this.AccountDropDownList.DataBind();
        //    this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void ShowUnit()
        {
            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.DataTextField = "UnitName";
            this.UnitDropDownList.DataValueField = "UnitCode";
            this.UnitDropDownList.DataSource = this._unitBL.GetListForDDL();
            this.UnitDropDownList.DataBind();
            this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _wrhsCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._wrhsKey), ApplicationConfig.EncryptionKey);
            string _wrhsSubled = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._subledKey), ApplicationConfig.EncryptionKey);
            string _wrhsLoc = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            FINSuppInvDt2 _finSuppInvDt2 = this._supplierNoteBL.GetSingleFINSuppInvDt2(_transNo, _wrhsCode, _wrhsSubled, _wrhsLoc, _productCode);

            //_finSuppInvDt2.Account = this.AccountDropDownList.SelectedValue;
            _finSuppInvDt2.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _finSuppInvDt2.Unit = this.UnitDropDownList.SelectedValue;
            _finSuppInvDt2.PriceForex = (this.PriceTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PriceTextBox.Text);
            _finSuppInvDt2.AmountForex = (this.AmountTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountTextBox.Text);
            _finSuppInvDt2.Remark = this.RemarkTextBox.Text;

            bool _result = this._supplierNoteBL.EditFINSuppInvDt2(_finSuppInvDt2);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}