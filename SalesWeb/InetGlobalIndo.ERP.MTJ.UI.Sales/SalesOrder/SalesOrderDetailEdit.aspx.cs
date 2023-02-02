using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.SalesOrder
{
    public partial class SalesOrderDetailEdit : SalesOrderBase
    {
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

        private byte _decimalPlace = 0;

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

                this.ShowUnitOrder();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.ProductTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyOrderTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyOrderTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ShowUnitOrder()
        {
            this.UnitOrderDDL.Items.Clear();
            this.UnitOrderDDL.DataTextField = "UnitName";
            this.UnitOrderDDL.DataValueField = "UnitCode";
            this.UnitOrderDDL.DataSource = this._unitBL.GetListForDDL();
            this.UnitOrderDDL.DataBind();
            this.UnitOrderDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowUnit()
        {
            this.UnitDDL.Items.Clear();
            this.UnitDDL.DataTextField = "UnitName";
            this.UnitDDL.DataValueField = "UnitCode";
            this.UnitDDL.DataSource = this._unitBL.GetListUnitConvertFromForDDL(this.UnitOrderDDL.SelectedValue);
            this.UnitDDL.DataBind();
            this.UnitDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowData()
        {
            MKTSODt _salesOrderDt = this._salesOrderBL.GetSingleMKTSODt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey)), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey));
            MKTSOHd _mktSOHd = this._salesOrderBL.GetSingleMKTSOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey)));
            MsProduct _msProduct = this._productBL.GetSingleProduct(_salesOrderDt.ProductCode);

            _decimalPlace = _currencyBL.GetDecimalPlace(_mktSOHd.CurrCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.ProductCodeTextBox.Text = _salesOrderDt.ProductCode;
            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_salesOrderDt.ProductCode);
            this.ProductHiddenField.Value = _salesOrderDt.ProductCode;
            this.ProductCodeTextBox.Text = _salesOrderDt.ProductCode;
            this.SpecificationTextBox.Text = _salesOrderDt.Specification;
            this.QtyOrderTextBox.Text = (_salesOrderDt.QtyOrder == 0) ? "0" : _salesOrderDt.QtyOrder.ToString("#,###.##");
            this.UnitOrderDDL.SelectedValue = _salesOrderDt.UnitOrder;
            decimal _price = Convert.ToDecimal((_salesOrderDt.Price == null) ? 0 : _salesOrderDt.Price);

            Boolean _isUsingPG = _productBL.GetSingleProductType(_msProduct.ProductType).IsUsingPG;

            if (_isUsingPG == true)
            {
                this.PriceTextBox.Attributes.Add("ReadOnly", "True");
                this.PriceTextBox.Attributes.Add("Style", "background-color: #CCCCCC;");
            }
            else
            {
                this.PriceTextBox.Attributes.Remove("ReadOnly");
                this.PriceTextBox.Attributes.Add("Style", "background-color: #FFFFFF;");
            }
            this.PriceTextBox.Text = (_price == 0) ? "0" : _price.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _amount = Convert.ToDecimal((_salesOrderDt.Amount == null) ? 0 : _salesOrderDt.Amount);
            this.AmountTextBox.Text = (_amount == 0) ? "0" : _amount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _qty = Convert.ToDecimal((_salesOrderDt.Qty == null) ? 0 : _salesOrderDt.Qty);
            this.QtyTextBox.Text = _qty.ToString("#,###.##");
            if (this.UnitOrderDDL.SelectedValue != "null")
            {
                this.ShowUnit();
            }
            this.UnitDDL.SelectedValue = _salesOrderDt.Unit;
            this.RemarkTextBox.Text = _salesOrderDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MKTSODt _salesOrderDt = this._salesOrderBL.GetSingleMKTSODt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey)), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey));

            _salesOrderDt.Specification = this.SpecificationTextBox.Text;
            _salesOrderDt.QtyOrder = Convert.ToDecimal(this.QtyOrderTextBox.Text);
            _salesOrderDt.UnitOrder = this.UnitOrderDDL.SelectedValue;
            _salesOrderDt.Price = Convert.ToDecimal(this.PriceTextBox.Text);
            decimal _amountOriginal = Convert.ToDecimal((_salesOrderDt.Amount == null) ? 0 : _salesOrderDt.Amount);
            _salesOrderDt.Amount = Convert.ToDecimal(this.AmountTextBox.Text);
            _salesOrderDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _salesOrderDt.Unit = this.UnitDDL.SelectedValue;
            _salesOrderDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._salesOrderBL.EditMKTSODt(_salesOrderDt, _amountOriginal);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisiKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisiKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void UnitDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.UnitOrderDDL.SelectedValue != "null")
            {
                decimal _totalQty = _unitBL.FindConvertionUnit(this.ProductHiddenField.Value, this.UnitOrderDDL.SelectedValue, this.UnitDDL.SelectedValue) * Convert.ToDecimal(this.QtyOrderTextBox.Text);
                this.QtyTextBox.Text = (_totalQty == 0) ? "0" : _totalQty.ToString("#,###.##");
            }
        }

        protected void UnitOrderDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.UnitOrderDDL.SelectedValue != "null")
            {
                this.UnitDDL.Items.Clear();
                this.ShowUnit();
            }
            else
            {
                this.UnitDDL.Items.Clear();
                this.UnitDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.QtyTextBox.Text = "0";
            }

            if (this.UnitDDL.SelectedValue != "null")
            {
                //this.QtyTextBox.Text = Convert.ToString(_unitBL.FindConvertionUnit(this.ProductDropDownList.SelectedValue, this.UnitOrderDDL.SelectedValue, this.UnitDDL.SelectedValue) * Convert.ToDecimal(this.QtyOrderTextBox.Text));
                this.QtyTextBox.Text = (_unitBL.FindConvertionUnit(this.ProductHiddenField.Value, this.UnitOrderDDL.SelectedValue, this.UnitDDL.SelectedValue) * Convert.ToDecimal(this.QtyOrderTextBox.Text)).ToString();
            }
            else
            {
                this.QtyTextBox.Text = "0";
            }
        }
    }
}