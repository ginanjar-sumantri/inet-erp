using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Product
{
    public partial class ProductAdd : ProductBase
    {
        private CurrencyBL _currencyBL = new CurrencyBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private CurrencyRateBL _currencyRate = new CurrencyRateBL();
        private DiscountBL _discountBL = new DiscountBL();
        private CompanyConfig _compConfig = new CompanyConfig();

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

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowUnit();
                this.ShowUnitOrder();
                this.ShowProductSubGroup();
                this.ShowProductType();
                this.ShowPriceGroup();
                this.ShowCurrency();

                this.SetAttribute();
                this.ClearLabel();
                this.ClearData();
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

        public void ClearData()
        {
            this.PriceGroupTR.Visible = false;
            this.WarningLabel.Text = "";
            this.ProductCodeTextBox.Text = "";
            this.ProductNameTextBox.Text = "";
            this.ProductSubGroupDropDownList.SelectedValue = "null";
            this.ProductTypeDropDownList.SelectedValue = "null";
            this.Spec1TextBox.Text = "";
            this.Spec2TextBox.Text = "";
            this.Spec3TextBox.Text = "";
            this.Spec4TextBox.Text = "";
            this.BuyingPriceTextBox.Text = "0";
            this.MinQtyTextBox.Text = "0";
            this.MaxQtyTextBox.Text = "0";
            this.UnitDropDownList.SelectedValue = "null";
            this.UnitOrderDropDownList.SelectedValue = "null";
            //this.DiscountDropDownList.SelectedValue = "null";
            this.LengthTextBox.Text = "0";
            this.WeightTextBox.Text = "0";
            this.HeightTextBox.Text = "0";
            this.WidthTextBox.Text = "0";
            this.VolumeTextBox.Text = "0";
            //this.DiscAmountTextBox.Text = "0";
            //this.TotalTextBox.Text = "0";
            //this.ProductValTypeDropDownList.SelectedValue = "null";
            this.SellingPriceTextBox.Text = "0";
            this.BarcodeTextBox.Text = "";
            this.ItemDurationTextBox.Text = "0";
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        private void ShowUnit()
        {
            this.UnitDropDownList.DataTextField = "UnitName";
            this.UnitDropDownList.DataValueField = "UnitCode";
            this.UnitDropDownList.DataSource = this._unitBL.GetListForDDL();
            this.UnitDropDownList.DataBind();
            this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        //private void ShowDiscount()
        //{
        //    this.DiscountDropDownList.DataTextField = "DiscountName";
        //    this.DiscountDropDownList.DataValueField = "DiscountCode";
        //    this.DiscountDropDownList.DataSource = this._discountBL.GetListForDDL();
        //    this.DiscountDropDownList.DataBind();
        //    this.DiscountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void ShowCurrency()
        {
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            //if (Convert.ToDecimal(this.SellingPriceTextBox.Text) > 0)
            //{

            MsProduct _msProduct = new MsProduct();

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
            _msProduct.Photo = ApplicationConfig.ProductImageDefault;

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
            _msProduct.SellingPrice = (this.SellingPriceTextBox.Text == "") ? 0 : Convert.ToDecimal(this.SellingPriceTextBox.Text);
            _msProduct.ConsignmentSuppCode = this.SuppNmbrTextBox.Text;
            _msProduct.fgPackage = this.FgPackageCheckBox.Checked;
            _msProduct.fgAssembly = this.FgAssemblyCheckBox.Checked;
            _msProduct.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msProduct.Remark = this.RemarkTextBox.Text;
            _msProduct.CreatedBy = HttpContext.Current.User.Identity.Name;
            _msProduct.CreatedDate = DateTime.Now;
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

            bool _result = this._productBL.AddProduct(_msProduct);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ProductCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
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
            this.ClearData();
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
            }
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
    }
}