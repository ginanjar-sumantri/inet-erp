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

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.SalesOrder
{
    public partial class SalesOrderDetailAdd : SalesOrderBase
    {
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
        private ProductBL _productBL = new ProductBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CompanyConfig _compConfig = new CompanyConfig();

        private byte _decimalPlace = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _pgYear = this._compConfig.GetSingle(CompanyConfigure.ActivePGYear).SetValue;

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

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                //this.ShowProduct();
                this.ShowUnitOrder();

                this.ClearData();
                this.SetAttribute();
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;
                ///////////////////////////////// pindahan dari ProductDropDownList_SelectedIndexChanged
                if (this.ProductPicker1.ProductCode != "")
                {
                    MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode);
                    if (_msProduct.Unit != "")
                    {
                        this.UnitOrderDDL.SelectedValue = _msProduct.Unit;
                        this.ShowUnit();
                    }
                    else
                    {
                        this.UnitOrderDDL.SelectedValue = "null";
                        this.UnitDDL.Items.Clear();
                        this.UnitDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
                        this.UnitDDL.SelectedValue = "null";
                    }
                    Boolean _isUsingPG = _productBL.GetSingleProductType(_msProduct.ProductType).IsUsingPG;
                    String _currCode = this._salesOrderBL.GetSingleMKTSOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey))).CurrCode;

                    if (_isUsingPG == true)
                    {
                        this.PriceTextBox.Attributes.Add("ReadOnly", "True");
                        this.PriceTextBox.Attributes.Add("Style", "background-color: #CCCCCC;");
                        this.PriceTextBox.Text = _priceGroupBL.GetSingle(_msProduct.PriceGroupCode, Convert.ToInt32(_pgYear.Trim())).AmountForex.ToString("#,##0.##");
                    }
                    else
                    {
                        this.PriceTextBox.Attributes.Remove("ReadOnly");
                        this.PriceTextBox.Attributes.Add("Style", "background-color: #FFFFFF;");
                        Master_ProductSalesPrice _salesPrice = _productBL.GetSingleProductSalesPrice(_currCode, this.ProductPicker1.ProductCode);
                        if (_salesPrice != null)
                        {
                            this.PriceTextBox.Text = Convert.ToDecimal(_salesPrice.SalesPrice).ToString("#,##0.##");
                        }
                        else
                        {
                            this.PriceTextBox.Text = "0";
                        }
                    }
                }
                /////////////////////////////////
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.QtyTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyOrderTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyOrderTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearData()
        {
            this.ClearLabel();

            //this.ProductDropDownList.SelectedValue = "null";
            this.SpecificationTextBox.Text = "";
            this.QtyOrderTextBox.Text = "0";
            this.UnitOrderDDL.SelectedValue = "null";
            this.QtyTextBox.Text = "0";
            this.UnitDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.UnitDDL.SelectedValue = "null";
            this.PriceTextBox.Text = "0";
            this.AmountTextBox.Text = "0";
            this.RemarkTextBox.Text = "";

            MKTSOHd _mktSOHd = this._salesOrderBL.GetSingleMKTSOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey)));
            _decimalPlace = _currencyBL.GetDecimalPlace(_mktSOHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }

        //private void ShowProduct()
        //{
        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._productBL.GetListViewProductForDDL();
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            //if (_productBL.GetStockValues(this.ProductPicker1.ProductCode) >= Convert.ToDecimal(this.QtyTextBox.Text))
            //{
                MKTSODt _salesOrderDt = new MKTSODt();
                //MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductDropDownList.SelectedValue);
                MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode);

                string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                string _revisi = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey);

                _salesOrderDt.TransNmbr = _transNo;
                _salesOrderDt.Revisi = Convert.ToInt32(_revisi);
                //_salesOrderDt.ProductCode = this.ProductDropDownList.SelectedValue;
                _salesOrderDt.ProductCode = this.ProductPicker1.ProductCode;
                _salesOrderDt.Specification = this.SpecificationTextBox.Text;
                _salesOrderDt.QtyOrder = Convert.ToDecimal(this.QtyOrderTextBox.Text);
                _salesOrderDt.UnitOrder = this.UnitOrderDDL.SelectedValue;
                _salesOrderDt.Price = Convert.ToDecimal(this.PriceTextBox.Text);
                _salesOrderDt.Amount = Convert.ToDecimal(this.AmountTextBox.Text);
                _salesOrderDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
                _salesOrderDt.Unit = this.UnitDDL.SelectedValue;
                _salesOrderDt.Remark = this.RemarkTextBox.Text;
                _salesOrderDt.DoneClosing = SalesOrderDataMapper.GetStatusDetail(SalesOrderStatusDt.Open);
                _salesOrderDt.QtyDO = 0;

                bool _result = this._salesOrderBL.AddMKTSODt(_salesOrderDt);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisiKey)));
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Add Data";
                }
            //}
            //else
            //{
            //    this.WarningLabel.Text = "Stock of the product is not enough";
            //}
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisiKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        //protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductDropDownList.SelectedValue);

        //    if (_msProduct.Unit != "")
        //    {
        //        this.UnitOrderDDL.SelectedValue = _msProduct.Unit;
        //        this.ShowUnit();
        //    }
        //    else
        //    {
        //        this.UnitOrderDDL.SelectedValue = "null";

        //        this.UnitDDL.Items.Clear();
        //        this.UnitDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //        this.UnitDDL.SelectedValue = "null";
        //    }
        //}

        protected void UnitDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.UnitOrderDDL.SelectedValue != "null")
            {
                //decimal _totalQty = _unitBL.FindConvertionUnit(this.ProductDropDownList.SelectedValue, this.UnitOrderDDL.SelectedValue, this.UnitDDL.SelectedValue) * Convert.ToDecimal(this.QtyOrderTextBox.Text);
                decimal _totalQty = _unitBL.FindConvertionUnit(this.ProductPicker1.ProductCode, this.UnitOrderDDL.SelectedValue, this.UnitDDL.SelectedValue) * Convert.ToDecimal(this.QtyOrderTextBox.Text);
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
                this.QtyTextBox.Text = (_unitBL.FindConvertionUnit(this.ProductPicker1.ProductCode, this.UnitOrderDDL.SelectedValue, this.UnitDDL.SelectedValue) * Convert.ToDecimal(this.QtyOrderTextBox.Text)).ToString();
            }
            else
            {
                this.QtyTextBox.Text = "0";
            }
        }
    }
}