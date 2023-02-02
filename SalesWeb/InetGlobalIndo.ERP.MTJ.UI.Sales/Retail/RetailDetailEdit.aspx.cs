using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.NCC;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.Retail
{
    public partial class RetailDetailEdit : RetailBase
    {
        private RetailBL _retailBL = new RetailBL();
        private ProductBL _productBL = new ProductBL();
        private PhoneTypeBL _phoneTypeBL = new PhoneTypeBL();
        private PermissionBL _permBL = new PermissionBL();
        private NCPBL _ncpBL = new NCPBL();
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

                //this.ShowProduct();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                if (this.ProductPicker1.ProductCode != "null")
                {
                    String _productType = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode).ProductType;
                    Decimal _sellingPrice = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode).SellingPrice;
                    Boolean _isUsingUniqueID = this._productBL.GetSingleProductType(_productType).IsUsingUniqueID;
                    String _currCode = this._retailBL.GetSingleSAL_TrRetail(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).CurrCode;
                    _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);

                    this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                    this.PriceTextBox.Text = _sellingPrice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                    if (_isUsingUniqueID == true)
                    {
                        this.SerialNmbrTR.Visible = true;
                        this.PhoneTypeTR.Visible = true;

                        this.ShowPhoneType();
                        this.ShowSerialNumber();

                        this.QtyTextBox.Text = "1";
                        this.QtyTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                        this.QtyTextBox.Attributes.Add("ReadOnly", "True");

                    }
                    else
                    {
                        this.SerialNmbrTR.Visible = false;
                        this.PhoneTypeTR.Visible = false;

                        this.PhoneTypeDropDownList.SelectedValue = "null";
                        this.SerialNumberDropDownList.SelectedValue = "null";
                        this.QtyTextBox.Text = "0";
                        this.QtyTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                        this.QtyTextBox.Attributes.Remove("ReadOnly");
                    }
                }
            }

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");
            this.DiscTextBox.Attributes.Add("ReadOnly", "True");
            this.PriceTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyTextBox.Attributes.Add("OnBlur", "BlurAmount(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.DiscTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "BlurAmount(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.DiscTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        //private void ShowProduct()
        //{
        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._productBL.GetListForDDLProduct();
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowPhoneType()
        {
            this.PhoneTypeDropDownList.Items.Clear();
            this.PhoneTypeDropDownList.DataTextField = "PhoneTypeDesc";
            this.PhoneTypeDropDownList.DataValueField = "PhoneTypeCode";
            this.PhoneTypeDropDownList.DataSource = this._phoneTypeBL.GetListPhoneTypeForDDL(this.ProductPicker1.ProductCode );
            this.PhoneTypeDropDownList.DataBind();
            this.PhoneTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowSerialNumber()
        {
            this.SerialNumberDropDownList.Items.Clear();
            this.SerialNumberDropDownList.DataTextField = "SerialNumber";
            this.SerialNumberDropDownList.DataValueField = "SerialNumber";
            this.SerialNumberDropDownList.DataSource = this._ncpBL.GetListMsProductSerialNumberForDDL(this.ProductPicker1.ProductCode);
            this.SerialNumberDropDownList.DataBind();
            this.SerialNumberDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowData()
        {
            SAL_TrRetailItem _sal_TrRetailItem = this._retailBL.GetSingleSAL_TrRetailItem(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeDetail), ApplicationConfig.EncryptionKey)));
            String _currCode = this._retailBL.GetSingleSAL_TrRetail(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).CurrCode;

            _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.ProductPicker1.ProductCode = _sal_TrRetailItem.ProductCode;
            this.ProductPicker1.ProductName = _productBL.GetProductNameByCode(_sal_TrRetailItem.ProductCode);
            String _productType = this._productBL.GetSingleProduct(_sal_TrRetailItem.ProductCode).ProductType;
            Boolean _isUsingUniqueID = this._productBL.GetSingleProductType(_productType).IsUsingUniqueID;
            if (_isUsingUniqueID == true)
            {
                this.SerialNmbrTR.Visible = true;
                this.PhoneTypeTR.Visible = true;

                this.ShowPhoneType();
                this.ShowSerialNumber();

                this.PhoneTypeDropDownList.SelectedValue = _sal_TrRetailItem.PhoneTypeCode.ToString();
                this.SerialNumberDropDownList.SelectedValue = _sal_TrRetailItem.SerialNumber;

                this.QtyTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.QtyTextBox.Attributes.Add("ReadOnly", "True");
            }
            else
            {
                this.SerialNmbrTR.Visible = false;
                this.PhoneTypeTR.Visible = false;

                this.PhoneTypeDropDownList.SelectedValue = "null";
                this.SerialNumberDropDownList.SelectedValue = "null";
                this.QtyTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                this.QtyTextBox.Attributes.Remove("ReadOnly");
            }

            this.IMEITextBox.Text = _sal_TrRetailItem.IMEI;
            this.PriceTextBox.Text = _sal_TrRetailItem.Price.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.QtyTextBox.Text = _sal_TrRetailItem.Qty.ToString();
            this.DiscTextBox.Text = _sal_TrRetailItem.DiscountAmount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountTextBox.Text = _sal_TrRetailItem.Total.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _sal_TrRetailItem.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            SAL_TrRetailItem _sal_TrRetailItem = this._retailBL.GetSingleSAL_TrRetailItem(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeDetail), ApplicationConfig.EncryptionKey)));

            _sal_TrRetailItem.ProductCode = this.ProductPicker1.ProductCode;
            if (this.PhoneTypeDropDownList.SelectedValue == "null")
            {
                _sal_TrRetailItem.PhoneTypeCode = null;
            }
            else
            {
                _sal_TrRetailItem.PhoneTypeCode = new Guid(this.PhoneTypeDropDownList.SelectedValue);
            }
            if (this.SerialNumberDropDownList.SelectedValue == "null")
            {
                _sal_TrRetailItem.SerialNumber = null;
            }
            else
            {
                _sal_TrRetailItem.SerialNumber = this.SerialNumberDropDownList.SelectedValue;
            }
            _sal_TrRetailItem.IMEI = this.IMEITextBox.Text;
            _sal_TrRetailItem.Price = Convert.ToDecimal(this.PriceTextBox.Text);
            decimal _amountOriginal = _sal_TrRetailItem.Total;
            _sal_TrRetailItem.Total = Convert.ToDecimal(this.AmountTextBox.Text);
            _sal_TrRetailItem.Qty = Convert.ToInt32(this.QtyTextBox.Text);
            _sal_TrRetailItem.DiscountAmount = Convert.ToDecimal(this.DiscTextBox.Text);
            _sal_TrRetailItem.Remark = this.RemarkTextBox.Text;

            bool _result = this._retailBL.EditSAL_TrRetailItem(_sal_TrRetailItem, _amountOriginal, _sal_TrRetailItem.SerialNumber);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        //protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ProductPicker1.ProductCode != "null")
        //    {
        //        String _productType = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode).ProductType;
        //        Decimal _sellingPrice = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode).SellingPrice;
        //        Boolean _isUsingUniqueID = this._productBL.GetSingleProductType(_productType).IsUsingUniqueID;
        //        String _currCode = this._retailBL.GetSingleSAL_TrRetail(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).CurrCode;
        //        _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);
        //        //Decimal? _discountAmount = this._productBL.GetSingleProduct(this.ProductDropDownList.SelectedValue).DiscAmount;

        //        this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        //        this.PriceTextBox.Text = _sellingPrice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        //        //this.DiscTextBox.Text = (_discountAmount == null) ? "0" : Convert.ToDecimal(_discountAmount).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        //        //this.AmountTextBox.Text = (_sellingPrice - ((_discountAmount == null) ? 0 : _discountAmount) == null) ? "0" : Convert.ToDecimal(_sellingPrice - ((_discountAmount == null) ? 0 : _discountAmount)).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

        //        if (_isUsingUniqueID == true)
        //        {
        //            this.SerialNmbrTR.Visible = true;
        //            this.PhoneTypeTR.Visible = true;

        //            this.ShowPhoneType();
        //            this.ShowSerialNumber();

        //            this.QtyTextBox.Text = "1";
        //            this.QtyTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
        //            this.QtyTextBox.Attributes.Add("ReadOnly", "True");

        //        }
        //        else
        //        {
        //            this.SerialNmbrTR.Visible = false;
        //            this.PhoneTypeTR.Visible = false;

        //            this.PhoneTypeDropDownList.SelectedValue = "null";
        //            this.SerialNumberDropDownList.SelectedValue = "null";
        //            this.QtyTextBox.Text = "0";
        //            this.QtyTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
        //            this.QtyTextBox.Attributes.Remove("ReadOnly");
        //        }
        //    }
        //}
    }
}