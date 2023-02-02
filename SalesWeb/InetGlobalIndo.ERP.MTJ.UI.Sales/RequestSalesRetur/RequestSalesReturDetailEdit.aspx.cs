using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.RequestSalesRetur
{
    public partial class RequestSalesReturDetailEdit : RequestSalesReturBase
    {
        private RequestSalesReturBL _requestSalesReturBL = new RequestSalesReturBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
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

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
                //this.cekDeliveryBack();
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
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.PriceTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.QtyTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ShowData()
        {
            MKTReqReturDt _mktReqReturDt = this._requestSalesReturBL.GetSingleMKTReqReturDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey));

            MKTReqReturHd _mktReqReturHd = this._requestSalesReturBL.GetSingleMKTReqReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_mktReqReturHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_mktReqReturDt.ProductCode);
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_mktReqReturDt.Unit);
            this.ProductScrapRadioButtonList.SelectedValue = _mktReqReturDt.ProductScrap.ToString();
            this.QtyTextBox.Text = (_mktReqReturDt.Qty == 0) ? "0" : _mktReqReturDt.Qty.ToString("#,###.##");
            this.AmountTextBox.Text = (_mktReqReturDt.AmountForex == 0) ? "0" : _mktReqReturDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PriceTextBox.Text = (_mktReqReturDt.PriceForex == 0) ? "0" : _mktReqReturDt.PriceForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _mktReqReturDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MKTReqReturDt _mktReqReturDt = this._requestSalesReturBL.GetSingleMKTReqReturDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey));

            _mktReqReturDt.ProductScrap = Convert.ToChar(this.ProductScrapRadioButtonList.SelectedValue);
            _mktReqReturDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _mktReqReturDt.PriceForex = Convert.ToDecimal(this.PriceTextBox.Text);
            _mktReqReturDt.AmountForex = Convert.ToDecimal(this.AmountTextBox.Text);
            _mktReqReturDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._requestSalesReturBL.EditMKTReqReturDt(_mktReqReturDt);

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
    }
}