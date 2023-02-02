using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.RequestSalesRetur
{
    public partial class RequestSalesReturDetailAdd : RequestSalesReturBase
    {
        private RequestSalesReturBL _requestSalesReturBL = new RequestSalesReturBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private BillOfLadingBL _billOfLadingBL = new BillOfLadingBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private CompanyConfig _companyConfigBL = new CompanyConfig();

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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                //this.ShowProduct();

                this.ClearLabel();
                this.ClearData();
                this.SetAttribute();
                //this.cekDeliveryBack();
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                if (this.ProductPicker1.ProductCode != "null")
                {
                    MKTReqReturHd _mktReqReturHd = this._requestSalesReturBL.GetSingleMKTReqReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                    MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode);

                    string _productType = _msProduct.ProductType;
                    Boolean _isUsingPG = _productBL.GetSingleProductType(_productType).IsUsingPG;

                    if (_isUsingPG == true)
                    {
                        String _priceGroupCode = _msProduct.PriceGroupCode;
                        int _year = Convert.ToInt32(_companyConfigBL.GetSingle(CompanyConfigure.ActivePGYear).SetValue);
                        this.PriceTextBox.Text = _priceGroupBL.GetSingle(_priceGroupCode, _year).AmountForex.ToString("#,##0.##");
                        this.PriceTextBox.Attributes.Add("ReadOnly", "True");
                        this.PriceTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                    }
                    else
                    {
                        this.PriceTextBox.Text = "0";
                        this.PriceTextBox.Attributes.Remove("ReadOnly");
                        this.PriceTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                    }

                    this.QtyTextBox.Text = _billOfLadingBL.GetSingleQtyFromSTCSJDt(_mktReqReturHd.SJNo, this.ProductPicker1.ProductCode).ToString("#,###.##");
                    this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_msProduct.Unit);
                }
                else
                {
                    this.PriceTextBox.Text = "0";
                    this.PriceTextBox.Attributes.Remove("ReadOnly");
                    this.PriceTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                    this.QtyTextBox.Text = "0";
                    this.UnitTextBox.Text = "";
                }
            }

        }

        //protected void cekDeliveryBack() 
        //{
        //    String _deliveryBack = this._requestSalesReturBL.GetSingleMKTReqReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).FgDeliveryBack.ToString();
        //    if (_deliveryBack == "Y")
        //    {
        //        this.PriceTextBox.Attributes.Add("ReadOnly", "True");
        //        this.PriceTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
        //    }
        //    else
        //    {
        //        this.PriceTextBox.Attributes.Remove("ReadOnly");
        //        this.PriceTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
        //    }
        //}

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.PriceTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.QtyTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearData()
        {
            //this.ProductDropDownList.SelectedValue = "null";
            this.UnitTextBox.Text = "";
            this.QtyTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.AmountTextBox.Text = "0";
            this.PriceTextBox.Text = "0";
            this.DecimalPlaceHiddenField.Value = "";

            MKTReqReturHd _mktReqReturHd = this._requestSalesReturBL.GetSingleMKTReqReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = _currencyBL.GetDecimalPlace(_mktReqReturHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }

        //private void ShowProduct()
        //{
        //    MKTReqReturHd _mktReqReturHd = this._requestSalesReturBL.GetSingleMKTReqReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._productBL.GetListProductForDDLActiveAndStock(_mktReqReturHd.SJNo);
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MKTReqReturDt _mktReqReturDt = new MKTReqReturDt();
            MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode);

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _mktReqReturDt.TransNmbr = _transNo;
            _mktReqReturDt.ProductCode = this.ProductPicker1.ProductCode;
            _mktReqReturDt.Unit = _msProduct.Unit;
            _mktReqReturDt.ProductScrap = Convert.ToChar(this.ProductScrapRadioButtonList.SelectedValue);
            _mktReqReturDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _mktReqReturDt.Remark = this.RemarkTextBox.Text;
            _mktReqReturDt.PriceForex = Convert.ToDecimal(this.PriceTextBox.Text);
            _mktReqReturDt.AmountForex = Convert.ToDecimal(this.AmountTextBox.Text);
            //_mktReqReturDt.DoneClosing = RequestSalesReturDataMapper.GetStatusDetail(SalesOrderStatusDt.Open);

            bool _result = this._requestSalesReturBL.AddMKTReqReturDt(_mktReqReturDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

        //protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ProductPicker1.ProductCode != "null")
        //    {
        //        MKTReqReturHd _mktReqReturHd = this._requestSalesReturBL.GetSingleMKTReqReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //        MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode);

        //        this.QtyTextBox.Text = _billOfLadingBL.GetSingleQtyFromSTCSJDt(_mktReqReturHd.SJNo, this.ProductPicker1.ProductCode).ToString("#,###.##");
        //        this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_msProduct.Unit);
        //    }
        //    else
        //    {
        //        this.QtyTextBox.Text = "0";
        //        this.UnitTextBox.Text = "";
        //    }
        //}
    }
}