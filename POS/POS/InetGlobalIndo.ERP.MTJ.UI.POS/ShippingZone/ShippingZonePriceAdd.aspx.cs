using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingZone
{
    public partial class ShippingZonePriceAdd : ShippingZoneBase
    {
        private ShippingBL _shippingBL = new ShippingBL();
        private PermissionBL _permBL = new PermissionBL();
        private UnitBL _unitBL = new UnitBL();
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
                this.PageTitleLiteral.Text = this._pageTitlePriceDetailLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.WeightTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.PriceTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.ShowCode();
                this.ShowCurrency();
                this.ShowUnit();
                this.ClearData();
            }
        }

        protected void ShowCurrency()
        {
            this.CurrencyDropDownList.Items.Clear();
            this.CurrencyDropDownList.DataTextField = "CurrName";
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListCurrForDDL();
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCode()
        {
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            POSMsZone _POSMsZone = this._shippingBL.GetSinglePOSMsZone(_tempSplit[0]);
            this.CodeTextBox.Text = _POSMsZone.ZoneCode;
            this.NameTextBox.Text = _POSMsZone.ZoneName;
        }

        protected void ShowUnit()
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

        private void ClearData()
        {
            this.ClearLabel();
            this.ProductShapeRBL.SelectedIndex = 0;
            this.CurrencyDropDownList.SelectedIndex = 0;
            this.WeightTextBox.Text = "0";
            this.UnitDropDownList.SelectedIndex = 0;
            this.PriceTextBox.Text = "0";
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();
            if (this.CurrencyDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Currency.";

            if (Convert.ToDecimal(this.WeightTextBox.Text) <= 0)
                _errorMsg = _errorMsg + " Weight Must More Than 0.";

            if (this.UnitDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Unit.";

            if (Convert.ToDecimal(this.PriceTextBox.Text) <= 0)
                _errorMsg = _errorMsg + " Price Must More Than 0.";

            POSMsZonePrice _POSMsZonePrice = this._shippingBL.GetSinglePOSMsZonePrice(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.ProductShapeRBL.SelectedValue, Convert.ToDecimal(this.WeightTextBox.Text));
            if (_POSMsZonePrice != null)
                _errorMsg = _errorMsg + " Product Shape : " + this.ProductShapeRBL.SelectedItem + " With Weight = " + this.WeightTextBox.Text + " Already Exist.";

            this.WarningLabel.Text = _errorMsg.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsZonePrice _POSMsZonePrice = new POSMsZonePrice();
                _POSMsZonePrice.ZoneCode = this.CodeTextBox.Text;
                _POSMsZonePrice.ProductShape = this.ProductShapeRBL.SelectedValue;
                _POSMsZonePrice.CurrCode = this.CurrencyDropDownList.SelectedValue;
                _POSMsZonePrice.Weight = Convert.ToDecimal(this.WeightTextBox.Text);
                _POSMsZonePrice.Unit = this.UnitDropDownList.SelectedValue;
                _POSMsZonePrice.Price = Convert.ToDecimal(this.PriceTextBox.Text);
                if (this.FgActiveCheckBox.Checked == true)
                    _POSMsZonePrice.FgActive = 'Y';
                else
                    _POSMsZonePrice.FgActive = 'N';
                _POSMsZonePrice.Remark = this.RemarkTextBox.Text;
                _POSMsZonePrice.CreatedBy = HttpContext.Current.User.Identity.Name;
                _POSMsZonePrice.CreatedDate = DateTime.Now;
                _POSMsZonePrice.ModifiedBy = "";
                _POSMsZonePrice.ModifiedDate = this._defaultdate;

                bool _result = this._shippingBL.AddPOSMsZonePrice(_POSMsZonePrice);

                if (_result == true)
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningLabel.Text = "You Failed Add Data";
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}