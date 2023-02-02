using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAService
{
    public partial class FAServiceDetailEdit : FAServiceBase
    {
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
        private FAServiceBL _faServiceBL = new FAServiceBL();
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
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowFAMaintenance();
                this.ShowUnit();

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        private void ShowFAMaintenance()
        {
            this.FixedAssetMaintenanceDropDownList.Items.Clear();
            this.FixedAssetMaintenanceDropDownList.DataTextField = "FAMaintenanceName";
            this.FixedAssetMaintenanceDropDownList.DataValueField = "FAMaintenanceCode";
            this.FixedAssetMaintenanceDropDownList.DataSource = this._fixedAssetBL.GetListDDLFixedAssetMaintenance();
            this.FixedAssetMaintenanceDropDownList.DataBind();
            this.FixedAssetMaintenanceDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowUnit()
        {
            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.DataTextField = "UnitName";
            this.UnitDropDownList.DataValueField = "UnitCode";
            this.UnitDropDownList.DataSource = this._unitBL.GetListForDDL();
            this.UnitDropDownList.DataBind();
            this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttribute()
        {
            this.AmountForexTextBox.Attributes.Add("ReadOnly", "true");
            this.QtyTextBox.Attributes.Add("OnBlur", "Calculate(" + this.QtyTextBox.ClientID + ", " + this.PriceForexTextBox.ClientID + ", " + this.AmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "); ");
            this.PriceForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.QtyTextBox.ClientID + ", " + this.PriceForexTextBox.ClientID + ", " + this.AmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "); ");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            GLFAServiceDt _glFAServiceDt = this._faServiceBL.GetSingleFAServiceDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));
            GLFAServiceHd _glFAServiceHd = _faServiceBL.GetSingleFAServiceHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_glFAServiceHd.CurrCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.FixedAssetMaintenanceDropDownList.SelectedValue = _glFAServiceDt.FAMaintenance;
            this.AddValueCheckBox.Checked = FAServiceDataMapper.GetAddValue(_glFAServiceDt.FgAddValue);

            decimal _tempQty = (_glFAServiceDt.Qty == null) ? 0 : Convert.ToDecimal(_glFAServiceDt.Qty);
            this.QtyTextBox.Text = (_tempQty == 0) ? "0" : _tempQty.ToString("#,###.##");

            this.UnitDropDownList.SelectedValue = _glFAServiceDt.Unit;

            decimal _priceForex = (_glFAServiceDt.PriceForex == null) ? 0 : Convert.ToDecimal(_glFAServiceDt.PriceForex);
            this.PriceForexTextBox.Text = (_priceForex == 0) ? "0" : _priceForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _amountForex = (_glFAServiceDt.AmountForex == null) ? 0 : Convert.ToDecimal(_glFAServiceDt.AmountForex);
            this.AmountForexTextBox.Text = (_amountForex == 0) ? "0" : _amountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.RemarkTextBox.Text = _glFAServiceDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAServiceDt _glFAServiceDt = this._faServiceBL.GetSingleFAServiceDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            _glFAServiceDt.FAMaintenance = this.FixedAssetMaintenanceDropDownList.SelectedValue;
            _glFAServiceDt.FgAddValue = FAServiceDataMapper.GetAddValue(this.AddValueCheckBox.Checked);
            _glFAServiceDt.Remark = this.RemarkTextBox.Text;
            _glFAServiceDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _glFAServiceDt.Unit = this.UnitDropDownList.SelectedValue;
            _glFAServiceDt.PriceForex = Convert.ToDecimal(this.PriceForexTextBox.Text);
            decimal _amountForexOriginal = (_glFAServiceDt.AmountForex == null) ? 0 : Convert.ToDecimal(_glFAServiceDt.AmountForex);
            _glFAServiceDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);

            bool _result = this._faServiceBL.EditFAServiceDt(_glFAServiceDt, _amountForexOriginal);

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
            this.ClearLabel();
            this.ShowData();
        }
    }
}
