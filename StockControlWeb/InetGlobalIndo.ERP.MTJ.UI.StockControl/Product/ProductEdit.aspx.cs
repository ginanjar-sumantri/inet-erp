using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Product
{
    public partial class ProductEdit : ProductBase
    {
        private CurrencyBL _currencyBL = new CurrencyBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private CurrencyRateBL _currencyRate = new CurrencyRateBL();
        private DiscountBL _discountBL = new DiscountBL();
        private CompanyConfig _compConfig = new CompanyConfig();
        private SupplierBL _suppBL = new SupplierBL();

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

                this.btnSearchSupplier.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findSupplier&configCode=supplier','_popSearch','width=400,height=550,toolbar=0,location=0,status=0,scrollbars=1')";

                String spawnJS = "<script language='JavaScript'>\n";

                ////////////////////DECLARE FUNCTION FOR CATCHING CUSTOMER SEARCH
                spawnJS += "function findSupplier(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.SuppNmbrTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.SupplierNameTextBox.ClientID + "').value = dataArray[1];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";
                spawnJS += "</script>\n";

                this.javascriptReceiver.Text = spawnJS;


                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowUnit();
                this.ShowUnitOrder();
                this.ShowProductSubGroup();
                this.ShowProductType();
                this.ShowPriceGroup();
                //this.ShowDiscount();
                this.ShowCurrency();

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
                this.ShowSupplier();

                String _haveProductItemDuration = this._compConfig.GetSingle(CompanyConfigure.HaveProductItemDuration).SetValue;
                if (_haveProductItemDuration == "0")
                {
                    this.ItemDurationRow.Visible = false;
                }
                else
                {
                    this.ItemDurationRow.Visible = true;
                }
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.BuyingPriceTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.BuyingPriceTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.SellingPriceTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.SellingPriceTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.ItemDurationTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.ItemDurationTextBox.ClientID + ");");
        }

        public void ShowData()
        {
            MsProduct _msProduct = this._productBL.GetSingleProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_msProduct.PurchaseCurr);

            this.ProductCodeTextBox.Text = _msProduct.ProductCode;
            this.ProductNameTextBox.Text = _msProduct.ProductName;
            this.ProductSubGroupDropDownList.SelectedValue = _msProduct.ProductSubGroup;
            this.ProductTypeDropDownList.SelectedValue = _msProduct.ProductType;
            this.Spec1TextBox.Text = _msProduct.Specification1;
            this.Spec2TextBox.Text = _msProduct.Specification2;
            this.Spec3TextBox.Text = _msProduct.Specification3;
            this.Spec4TextBox.Text = _msProduct.Specification4;
            this.CurrencyDropDownList.SelectedValue = _msProduct.PurchaseCurr;
            this.ProductPhoto.Attributes.Add("src", "" + ApplicationConfig.ProductPhotoVirDirPath + _msProduct.Photo + "?t=" + System.DateTime.Now.ToString());

            if (_msProduct.PriceGroupCode != null)
            {

            }
            else
            {
                this.PriceGroupTR.Visible = false;

            }
            Boolean _isUsingPG = this._productBL.GetSingleProductType(_msProduct.ProductType).IsUsingPG;

            if (_isUsingPG == true)
            {
                this.PriceGroupTR.Visible = true;
                if (_msProduct.PriceGroupCode != null)
                {
                    //this.PGTextBox.Text = this._priceGroupBL.GetSingle(_msProduct.PriceGroupCode.ToString()).PriceGroupCode;
                    this.PriceGroupDropDownList.SelectedValue = _msProduct.PriceGroupCode.ToString();
                    this.BuyingPriceTextBox.Attributes.Add("ReadOnly", "True");
                    this.BuyingPriceTextBox.Attributes.Add("style", "background-color:#cccccc");
                    this.CurrencyDropDownList.Enabled = false;
                }
                else
                {
                    this.PriceGroupDropDownList.SelectedValue = "null";
                    this.BuyingPriceTextBox.Attributes.Remove("ReadOnly");
                    this.BuyingPriceTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.CurrencyDropDownList.Enabled = true;
                }
            }
            else
            {
                this.PriceGroupTR.Visible = false;
            }

            this.BuyingPriceTextBox.Text = (_msProduct.BuyingPrice == 0) ? "0" : _msProduct.BuyingPrice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.MinQtyTextBox.Text = (_msProduct.MinQty == 0) ? "0" : _msProduct.MinQty.ToString("#,##0.##");
            this.MaxQtyTextBox.Text = (_msProduct.MaxQty == 0) ? "0" : _msProduct.MaxQty.ToString("#,##0.##");
            this.UnitDropDownList.SelectedValue = _msProduct.Unit;
            this.UnitOrderDropDownList.SelectedValue = _msProduct.UnitOrder;
            this.LengthTextBox.Text = (_msProduct.Length == 0) ? "0" : _msProduct.Length.ToString("#,##0.##");
            this.WeightTextBox.Text = (_msProduct.Weight == 0) ? "0" : _msProduct.Weight.ToString("#,##0.##");
            this.HeightTextBox.Text = (_msProduct.Height == 0) ? "0" : _msProduct.Height.ToString("#,##0.##");
            this.WidthTextBox.Text = (_msProduct.Width == 0) ? "0" : _msProduct.Width.ToString("#,##0.##");
            this.VolumeTextBox.Text = (_msProduct.Volume == 0) ? "0" : _msProduct.Volume.ToString("#,##0.##");
            //this.ActiveDropDownList.SelectedValue = _msProduct.FgActive.ToString();
            this.BarcodeTextBox.Text = _msProduct.Barcode;
            this.FgConsignmentCheckBox.Checked = Convert.ToBoolean(_msProduct.FgConsignment);
            this.FgAssemblyCheckBox.Checked = Convert.ToBoolean(_msProduct.fgAssembly);
            this.FgPackageCheckBox.Checked = Convert.ToBoolean(_msProduct.fgPackage);
            this.SuppNmbrTextBox.Text = _msProduct.ConsignmentSuppCode;
            this.SupplierNameTextBox.Text = _suppBL.GetSuppNameByCode(_msProduct.ConsignmentSuppCode) + " - " + _msProduct.ConsignmentSuppCode;
            //this.DiscountDropDownList.SelectedValue = _msProduct.DiscountCode.ToString();
            this.SellingPriceTextBox.Text = (_msProduct.SellingPrice == 0) ? "0" : _msProduct.SellingPrice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            //this.DiscAmountTextBox.Text = (_msProduct.DiscAmount == null) ? "0" : Convert.ToDecimal(_msProduct.DiscAmount).ToString("#,##0.##");
            //this.TotalTextBox.Text = (_msProduct.SellingPrice - _msProduct.DiscAmount == null) ? "0" : Convert.ToDecimal(_msProduct.SellingPrice - _msProduct.DiscAmount).ToString("#,##0.##");
            this.ItemDurationTextBox.Text = (_msProduct.ItemDuration == null) ? "0" : _msProduct.ItemDuration.ToString();
            this.FgActiveCheckBox.Checked = (_msProduct.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _msProduct.Remark;
        }

        private void ShowUnit()
        {
            this.UnitDropDownList.DataTextField = "UnitName";
            this.UnitDropDownList.DataValueField = "UnitCode";
            this.UnitDropDownList.DataSource = this._unitBL.GetListForDDL();
            this.UnitDropDownList.DataBind();
            this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //private void ShowDiscount()
        //{
        //    this.DiscountDropDownList.DataTextField = "DiscountName";
        //    this.DiscountDropDownList.DataValueField = "DiscountCode";
        //    this.DiscountDropDownList.DataSource = this._discountBL.GetListForDDL();
        //    this.DiscountDropDownList.DataBind();
        //    this.DiscountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowUnitOrder()
        {
            this.UnitOrderDropDownList.DataTextField = "UnitName";
            this.UnitOrderDropDownList.DataValueField = "UnitCode";
            this.UnitOrderDropDownList.DataSource = this._unitBL.GetListForDDL();
            this.UnitOrderDropDownList.DataBind();
            this.UnitOrderDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowProductSubGroup()
        {
            this.ProductSubGroupDropDownList.DataTextField = "ProductSubGrpName";
            this.ProductSubGroupDropDownList.DataValueField = "ProductSubGrpCode";
            this.ProductSubGroupDropDownList.DataSource = this._productBL.GetListProductSubGroupForDDL();
            this.ProductSubGroupDropDownList.DataBind();
            this.ProductSubGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowProductType()
        {
            this.ProductTypeDropDownList.DataTextField = "ProductTypeName";
            this.ProductTypeDropDownList.DataValueField = "ProductTypeCode";
            this.ProductTypeDropDownList.DataSource = this._productBL.GetListProductTypeForDDL();
            this.ProductTypeDropDownList.DataBind();
            this.ProductTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowPriceGroup()
        {
            string _pgYear = this._compConfig.GetSingle(CompanyConfigure.ActivePGYear).SetValue;

            this.PriceGroupDropDownList.DataTextField = "PriceGroupCode";
            this.PriceGroupDropDownList.DataValueField = "PriceGroupCode";
            this.PriceGroupDropDownList.DataSource = this._priceGroupBL.GetListForDDLForProduct(Convert.ToInt32(_pgYear.Trim()));
            this.PriceGroupDropDownList.DataBind();
            this.PriceGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCurrency()
        {
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            //if (Convert.ToDecimal(this.SellingPriceTextBox.Text) > 0)
            //{
            MsProduct _msProduct = this._productBL.GetSingleProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msProduct.ProductCode = this.ProductCodeTextBox.Text;
            _msProduct.ProductName = this.ProductNameTextBox.Text;
            _msProduct.ProductType = this.ProductTypeDropDownList.SelectedValue;
            _msProduct.ProductSubGroup = this.ProductSubGroupDropDownList.SelectedValue;
            _msProduct.Specification1 = this.Spec1TextBox.Text;
            _msProduct.Specification2 = this.Spec2TextBox.Text;
            _msProduct.Specification3 = this.Spec3TextBox.Text;
            _msProduct.Specification4 = this.Spec4TextBox.Text;
            _msProduct.PurchaseCurr = this.CurrencyDropDownList.SelectedValue;
            _msProduct.BuyingPrice = Convert.ToDecimal(this.BuyingPriceTextBox.Text);
            if (this.PriceGroupDropDownList.SelectedValue != "null")
            {
                _msProduct.PriceGroupCode = this.PriceGroupDropDownList.SelectedValue;
            }
            else
            {
                _msProduct.PriceGroupCode = null;
            }
            _msProduct.MinQty = Convert.ToDecimal(this.MinQtyTextBox.Text);
            _msProduct.MaxQty = Convert.ToDecimal(this.MaxQtyTextBox.Text);
            _msProduct.Unit = this.UnitDropDownList.SelectedValue;
            _msProduct.UnitOrder = this.UnitOrderDropDownList.SelectedValue;
            _msProduct.Length = Convert.ToDecimal(this.LengthTextBox.Text);
            _msProduct.Width = Convert.ToDecimal(this.WidthTextBox.Text);
            _msProduct.Height = Convert.ToDecimal(this.HeightTextBox.Text);
            _msProduct.Volume = Convert.ToDecimal(this.VolumeTextBox.Text);
            _msProduct.Weight = Convert.ToDecimal(this.WeightTextBox.Text);
            //_msProduct.FgActive = ProductDataMapper.IsActive(ProductDataMapper.IsActive(Convert.ToChar(this.ActiveDropDownList.SelectedValue)));
            _msProduct.Barcode = this.BarcodeTextBox.Text;
            _msProduct.SellingPrice = (this.SellingPriceTextBox.Text == "") ? 0 : Convert.ToDecimal(this.SellingPriceTextBox.Text);
            _msProduct.FgConsignment = this.FgConsignmentCheckBox.Checked;
            _msProduct.fgPackage = this.FgPackageCheckBox.Checked;
            _msProduct.fgAssembly = this.FgAssemblyCheckBox.Checked;
            _msProduct.ConsignmentSuppCode = this.SuppNmbrTextBox.Text;
            _msProduct.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msProduct.Remark = this.RemarkTextBox.Text;
            _msProduct.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _msProduct.ModifiedDate = DateTime.Now;
            String _haveProductItemDuration = this._compConfig.GetSingle(CompanyConfigure.HaveProductItemDuration).SetValue;
            if (_haveProductItemDuration == "1")
            {
                _msProduct.ItemDuration = Convert.ToInt32(this.ItemDurationTextBox.Text);
            }
            //_msProduct.DiscAmount = (this.DiscAmountTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscAmountTextBox.Text);

            //if (this.DiscountDropDownList.SelectedValue != "null")
            //{
            //    _msProduct.DiscountCode = new Guid(this.DiscountDropDownList.SelectedValue);
            //}
            //else
            //{
            //    _msProduct.DiscountCode = null;
            //}

            bool _result = this._productBL.EditProduct(_msProduct);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
            //}
            //else
            //{
            //    this.WarningLabel.Text = "Please fill selling price";
            //}
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            //if (Convert.ToDecimal(this.SellingPriceTextBox.Text) > 0)
            //{
            MsProduct _msProduct = this._productBL.GetSingleProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msProduct.ProductCode = this.ProductCodeTextBox.Text;
            _msProduct.ProductName = this.ProductNameTextBox.Text;
            _msProduct.ProductType = this.ProductTypeDropDownList.SelectedValue;
            _msProduct.ProductSubGroup = this.ProductSubGroupDropDownList.SelectedValue;
            _msProduct.Specification1 = this.Spec1TextBox.Text;
            _msProduct.Specification2 = this.Spec2TextBox.Text;
            _msProduct.Specification3 = this.Spec3TextBox.Text;
            _msProduct.Specification4 = this.Spec4TextBox.Text;
            _msProduct.PurchaseCurr = this.CurrencyDropDownList.SelectedValue;
            _msProduct.BuyingPrice = Convert.ToDecimal(this.BuyingPriceTextBox.Text);
            if (this.PriceGroupDropDownList.SelectedValue != "null")
            {
                _msProduct.PriceGroupCode = this.PriceGroupDropDownList.SelectedValue;
            }
            else
            {
                _msProduct.PriceGroupCode = null;
            }
            _msProduct.MinQty = Convert.ToDecimal(this.MinQtyTextBox.Text);
            _msProduct.MaxQty = Convert.ToDecimal(this.MaxQtyTextBox.Text);
            _msProduct.Unit = this.UnitDropDownList.SelectedValue;
            _msProduct.UnitOrder = this.UnitOrderDropDownList.SelectedValue;
            _msProduct.Length = Convert.ToDecimal(this.LengthTextBox.Text);
            _msProduct.Width = Convert.ToDecimal(this.WidthTextBox.Text);
            _msProduct.Height = Convert.ToDecimal(this.HeightTextBox.Text);
            _msProduct.Volume = Convert.ToDecimal(this.VolumeTextBox.Text);
            _msProduct.Weight = Convert.ToDecimal(this.WeightTextBox.Text);
            //_msProduct.FgActive = ProductDataMapper.IsActive(ProductDataMapper.IsActive(Convert.ToChar(this.ActiveDropDownList.SelectedValue)));
            _msProduct.Barcode = this.BarcodeTextBox.Text;
            _msProduct.FgConsignment = this.FgConsignmentCheckBox.Checked;
            _msProduct.fgPackage = this.FgPackageCheckBox.Checked;
            _msProduct.fgAssembly = this.FgAssemblyCheckBox.Checked;
            _msProduct.ConsignmentSuppCode = this.SuppNmbrTextBox.Text;
            //_msProduct.ProductValType = Convert.ToByte(this.ProductValTypeDropDownList.SelectedValue);
            _msProduct.SellingPrice = (this.SellingPriceTextBox.Text == "") ? 0 : Convert.ToDecimal(this.SellingPriceTextBox.Text);
            //_msProduct.DiscAmount = (this.DiscAmountTextBox.Text == "") ? 0 : Convert.ToDecimal(this.DiscAmountTextBox.Text);

            //if (this.DiscountDropDownList.SelectedValue != "null")
            //{
            //    _msProduct.DiscountCode = new Guid(this.DiscountDropDownList.SelectedValue);
            //}
            //else
            //{
            //    _msProduct.DiscountCode = null;
            //}
            _msProduct.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msProduct.Remark = this.RemarkTextBox.Text;
            _msProduct.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _msProduct.ModifiedDate = DateTime.Now;
            String _haveProductItemDuration = this._compConfig.GetSingle(CompanyConfigure.HaveProductItemDuration).SetValue;
            if (_haveProductItemDuration == "1")
            {
                _msProduct.ItemDuration = Convert.ToInt32(this.ItemDurationTextBox.Text);
            }

            bool _result = this._productBL.EditProduct(_msProduct);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
            //}
            //else
            //{
            //    this.WarningLabel.Text = "Please fill selling price";
            //}
        }

        protected void PriceGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PriceGroupDropDownList.SelectedValue != "null")
            {
                decimal _amountForex;
                string _currency;
                _amountForex = 0;
                _currency = "";

                string _pgYear = this._compConfig.GetSingle(CompanyConfigure.ActivePGYear).SetValue;

                _amountForex = this._priceGroupBL.GetSingle(this.PriceGroupDropDownList.SelectedValue, Convert.ToInt32(_pgYear.Trim())).AmountForex;
                _currency = this._priceGroupBL.GetSingle(this.PriceGroupDropDownList.SelectedValue, Convert.ToInt32(_pgYear.Trim())).CurrCode;

                byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(this._currencyBL.GetCurrDefault());
                byte _decimalPlaceForex = this._currencyBL.GetDecimalPlace(_currency);

                this.BuyingPriceTextBox.Text = (_amountForex == 0 ? "0" : _amountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceForex)));
                this.CurrencyDropDownList.SelectedValue = _currency;
            }
            else
            {
                this.BuyingPriceTextBox.Text = "0";
                this.CurrencyDropDownList.SelectedValue = "null";
            }
        }

        protected void ProductTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ProductTypeDropDownList.SelectedValue != "null")
            {
                Boolean _isUsingPG;
                _isUsingPG = false;
                _isUsingPG = this._productBL.GetSingleProductType(this.ProductTypeDropDownList.SelectedValue).IsUsingPG;

                if (_isUsingPG == true)
                {
                    this.PriceGroupTR.Visible = true;
                    this.BuyingPriceTextBox.Text = "0";
                    this.BuyingPriceTextBox.Attributes.Add("ReadOnly", "True");
                    this.BuyingPriceTextBox.Attributes.Add("style", "background-color:#cccccc");

                    this.CurrencyDropDownList.Enabled = false;
                    this.CurrencyDropDownList.SelectedValue = "null";
                }
                else
                {
                    this.PriceGroupTR.Visible = false;
                    this.PriceGroupDropDownList.SelectedValue = null;
                    this.BuyingPriceTextBox.Attributes.Remove("ReadOnly");
                    this.BuyingPriceTextBox.Text = "0";
                    this.BuyingPriceTextBox.Attributes.Add("style", "background-color:#ffffff");

                    this.CurrencyDropDownList.Enabled = true;
                    this.CurrencyDropDownList.SelectedValue = "null";
                }
            }
            else
            {
                this.PriceGroupTR.Visible = false;
                this.PriceGroupDropDownList.SelectedValue = null;
                this.BuyingPriceTextBox.Attributes.Remove("ReadOnly");
                this.BuyingPriceTextBox.Text = "0";
                this.BuyingPriceTextBox.Attributes.Add("style", "background-color:#ffffff");

                this.CurrencyDropDownList.Enabled = true;
                this.CurrencyDropDownList.SelectedValue = "null";
            }
            this.SellingPriceTextBox.Text = "0";
        }

        protected void CurrencyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrencyDropDownList.SelectedValue != "null")
            {
                this.DecimalPlaceHiddenField.Value = "";

                string _currCodeHome = _currencyBL.GetCurrDefault();

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrencyDropDownList.SelectedValue);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            }
            else
            {
                this.DecimalPlaceHiddenField.Value = "";
            }
        }
        protected void FgPackageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.FgPackageCheckBox.Checked == true)
            {
                this.FgAssemblyCheckBox.Enabled = false;
                //this.FgAssemblyCheckBox.Attributes.Add("Enable", "False");
            }
            else
            {
                //this.FgAssemblyCheckBox.Attributes.Add("Enable", "True");
                this.FgAssemblyCheckBox.Enabled = true;
            }
        }
        protected void FgAssemblyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.FgAssemblyCheckBox.Checked == true)
            {
                //this.FgPackageCheckBox.Attributes.Add("Enable", "False");
                this.FgPackageCheckBox.Enabled = false;
            }
            else
            {
                //this.FgPackageCheckBox.Attributes.Add("Enable", "True");
                this.FgPackageCheckBox.Enabled = true;
            }
        }

        protected void FgConsignment_OnCheckedBox(object sender, EventArgs e)
        {
            this.ShowSupplier();
            this.SupplierNameTextBox.Text = "";
            this.SuppNmbrTextBox.Text = "";
        }
        public void ShowSupplier()
        {
            if (this.FgConsignmentCheckBox.Checked == true)
            {
                this.SupplierNameTextBox.Visible = true;
                this.SuppNmbrTextBox.Visible = true;
                this.btnSearchSupplier.Visible = true;
            }
            else
            {
                this.SupplierNameTextBox.Visible = false;
                this.SuppNmbrTextBox.Visible = false;
                this.btnSearchSupplier.Visible = false;
            }

        }
    }
}