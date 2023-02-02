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
    public partial class FAServiceDetailAdd : FAServiceBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
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
                this.ClearData();
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

        public void ClearData()
        {
            this.FixedAssetMaintenanceDropDownList.SelectedValue = "null";
            this.AddValueCheckBox.Checked = false;
            this.QtyTextBox.Text = "0";
            this.UnitDropDownList.SelectedValue = "null";
            this.PriceForexTextBox.Text = "0";
            this.AmountForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
            this.DecimalPlaceHiddenField.Value = "";

            GLFAServiceHd _glFAServiceHd = _faServiceBL.GetSingleFAServiceHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = _currencyBL.GetDecimalPlace(_glFAServiceHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            int _maxItemNo = this._faServiceBL.GetMaxNoItemFAServiceDt(_transNo);

            GLFAServiceDt _glFAServiceDt = new GLFAServiceDt();

            _glFAServiceDt.TransNmbr = _transNo;
            _glFAServiceDt.ItemNo = _maxItemNo + 1;
            _glFAServiceDt.FAMaintenance = this.FixedAssetMaintenanceDropDownList.SelectedValue;
            _glFAServiceDt.FgAddValue = FAServiceDataMapper.GetAddValue(this.AddValueCheckBox.Checked);
            _glFAServiceDt.Remark = this.RemarkTextBox.Text;
            _glFAServiceDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _glFAServiceDt.Unit = this.UnitDropDownList.SelectedValue;
            _glFAServiceDt.PriceForex = Convert.ToDecimal(this.PriceForexTextBox.Text);
            _glFAServiceDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);

            bool _result = this._faServiceBL.AddFAServiceDt(_glFAServiceDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
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
    }
}
