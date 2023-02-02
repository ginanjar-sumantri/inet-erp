using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerNote
{
    public partial class CustomerNoteDetailEdit : CustomerNoteBase
    {
        private CustomerNoteBL _customerNoteBL = new CustomerNoteBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
        private BillOfLadingBL _billOfLadingBL = new BillOfLadingBL();
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
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttributeRate()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _currCode = this._customerNoteBL.GetSingleFINCustInvHd(_transNo).CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.PriceTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

        }


        private void SetAttribute()
        {
            this.SJNoTextBox.Attributes.Add("ReadOnly", "True");
            this.ProductTextBox.Attributes.Add("ReadOnly", "True");
            this.SONoTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");
            this.PriceTextBox.Attributes.Add("ReadOnly", "True");
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");

            this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        public void ShowData()
        {
            FINCustInvDt _finDNCustDt = this._customerNoteBL.GetSingleFINCustInvDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey));

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _currCode = this._customerNoteBL.GetSingleFINCustInvHd(_transNo).CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);

            this.SJNoTextBox.Text = this._billOfLadingBL.GetFileNmbrFromSTCSJHd(_finDNCustDt.SJNo);
            this.ProductTextBox.Text = this._productBL.GetProductNameByCode(_finDNCustDt.ProductCode);
            this.SONoTextBox.Text = this._salesOrderBL.GetFileNmbrMKTSOHd(_finDNCustDt.SONo);
            this.QtyTextBox.Text = (_finDNCustDt.Qty == 0) ? "0" : _finDNCustDt.Qty.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PriceTextBox.Text = (_finDNCustDt.PriceForex == 0) ? "0" : _finDNCustDt.PriceForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_finDNCustDt.Unit);
            this.AmountTextBox.Text = (_finDNCustDt.AmountForex == 0) ? "0" : _finDNCustDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finDNCustDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINCustInvDt _finDNCustDt = this._customerNoteBL.GetSingleFINCustInvDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey));

            _finDNCustDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _finDNCustDt.Unit = _finDNCustDt.Unit;
            _finDNCustDt.PriceForex = Convert.ToDecimal(this.PriceTextBox.Text);
            _finDNCustDt.AmountForex = Convert.ToDecimal(this.AmountTextBox.Text);
            _finDNCustDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._customerNoteBL.EditFINCustInvDt(_finDNCustDt);

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