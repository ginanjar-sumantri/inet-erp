using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.NCC;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockBeginning
{
    public partial class StockBeginningDetailEdit2 : StockBeginningBase
    {
        private ReceivingPOBL _receivingPOBL = new ReceivingPOBL();
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private NCPBL _ncpBL = new NCPBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();

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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ShowCurrency();

                this.ShowData();
            }
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowData()
        {
            this.ClearLabel();

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _serialNumber = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._detailKey), ApplicationConfig.EncryptionKey);

            MsProduct_SerialNumber _msProductSerialNumber = _ncpBL.GetSingleMsProductSerialNumber(_serialNumber);
            Transaction_NCPImport _transactionNCPImport = _ncpBL.GetSingleTransactionNCPImport(_transNo);

            this.SerialNumberTextBox.Text = _msProductSerialNumber.SerialNumber;
            this.PINTextBox.Text = _msProductSerialNumber.PIN;
            this.ManufactureIDTextBox.Text = _msProductSerialNumber.ManufactureID;
            this.CurrDropDownList.SelectedValue = _transactionNCPImport.Currency;
            if (_transactionNCPImport.Currency == _currencyBL.GetCurrDefault())
            {
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
            }
            this.ForexRateTextBox.Text = _transactionNCPImport.ForexRate.ToString("#,##0.##");
            this.DateTextBox.Text = _msProductSerialNumber.ExpirationDate.Substring(0, 4) + "-" + _msProductSerialNumber.ExpirationDate.Substring(4, 2) + "-" + _msProductSerialNumber.ExpirationDate.Substring(6, 2);
        }

        public void ShowCurrency()
        {
            this.CurrDropDownList.DataTextField = "CurrCode";
            this.CurrDropDownList.DataValueField = "CurrCode";
            this.CurrDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrDropDownList.DataBind();
            this.CurrDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);
            string _serialNumber = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._detailKey), ApplicationConfig.EncryptionKey);

            MsProduct_SerialNumber _msProductSerialNumber = _ncpBL.GetSingleMsProductSerialNumber(_serialNumber);

            _msProductSerialNumber.PIN = this.PINTextBox.Text;
            _msProductSerialNumber.ManufactureID = this.ManufactureIDTextBox.Text;
            _msProductSerialNumber.ExpirationDate = DateFormMapper.GetValue(this.DateTextBox.Text).Year.ToString() + DateFormMapper.GetValue(this.DateTextBox.Text).Month.ToString() + DateFormMapper.GetValue(this.DateTextBox.Text).Day.ToString();

            Transaction_NCPImport _transactionNCPImport = _ncpBL.GetSingleTransactionNCPImport(_transNo);

            _transactionNCPImport.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _transactionNCPImport.Currency = this.CurrDropDownList.SelectedValue;

            bool _result = this._ncpBL.Edit(_transactionNCPImport, _msProductSerialNumber);

            if (_result == true)
            {
                Response.Redirect(this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._locationKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._locationKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._locationKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._locationKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void CurrDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DecimalPlaceHiddenField.Value = "";

            if (this.CurrDropDownList.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();
                this.ForexRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrDropDownList.SelectedValue).ToString("#,###.##");

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrDropDownList.SelectedValue);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                if (this.CurrDropDownList.SelectedValue == _currCodeHome)
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Text = "1";
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#cccccc");
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                }
            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }
    }
}