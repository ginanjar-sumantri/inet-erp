using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.SalesConfirmation
{
    public partial class SalesConfirmationDetailEdit : SalesConfirmationBase
    {
        private SalesConfirmationBL _salesConfirmationBL = new SalesConfirmationBL();
        private ProductBL _productBL = new ProductBL();
        private CurrencyBL _currBL = new CurrencyBL();
        private PermissionBL _permBL = new PermissionBL();

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

                this.SetAttribute();

                this.ShowCurr();

                this.ShowData();
            }
        }

        protected void SetAttribute()
        {
            this.AmountTextBox.Attributes.Add("OnKeyDown", "formatangka(" + this.AmountTextBox.ClientID + ");");
            this.AmountTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowData()
        {
            this.ClearLabel();

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._detailKey), ApplicationConfig.EncryptionKey);

            BILTrSalesConfirmationDt _salesConfirmationDt = this._salesConfirmationBL.GetSingleSalesConfirmationDt(_transNo, _productCode);

            this.ProductCodeTextBox.Text = _salesConfirmationDt.ProductCode;
            this.ProductNameTextBox.Text = _productBL.GetProductNameByCode(_salesConfirmationDt.ProductCode);
            this.ProductSpecTextBox.Text = _salesConfirmationDt.ProductSpecification;
            this.CurrDropDownList.SelectedValue = _salesConfirmationDt.CurrCode;
            byte _decimalPlace = _currBL.GetDecimalPlace(this.CurrDropDownList.SelectedValue);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.AmountTextBox.Text = Convert.ToDecimal(_salesConfirmationDt.AmountForex).ToString("#,##0.00");
        }

        private void ShowCurr()
        {
            this.CurrDropDownList.Items.Clear();
            this.CurrDropDownList.DataTextField = "CurrName";
            this.CurrDropDownList.DataValueField = "CurrCode";
            this.CurrDropDownList.DataSource = this._currBL.GetListCurrForDDL();
            this.CurrDropDownList.DataBind();
            this.CurrDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._detailKey), ApplicationConfig.EncryptionKey);

            BILTrSalesConfirmationDt _salesConfirmationDt = this._salesConfirmationBL.GetSingleSalesConfirmationDt(_transNo, _productCode);

            _salesConfirmationDt.ProductSpecification = this.ProductSpecTextBox.Text;
            _salesConfirmationDt.CurrCode = this.CurrDropDownList.SelectedValue;
            decimal _amountForex = Convert.ToDecimal(_salesConfirmationDt.AmountForex);
            _salesConfirmationDt.AmountForex = Convert.ToDecimal((this.AmountTextBox.Text == "") ? "0" : this.AmountTextBox.Text);

            Boolean _result = this._salesConfirmationBL.EditSalesConfirmationDt(_salesConfirmationDt, _amountForex);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
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

        protected void CurrDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrDropDownList.SelectedValue != "null")
            {
                byte _decimalPlace = _currBL.GetDecimalPlace(this.CurrDropDownList.SelectedValue);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                this.AmountTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            }
            else
            {
                this.DecimalPlaceHiddenField.Value = "";
            }
        }
    }
}