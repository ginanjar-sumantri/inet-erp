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
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingZone
{
    public partial class ShippingZoneMultipleAdd : ShippingZoneBase
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
                this.PageTitleLiteral.Text = this._pageTitleMultipleDetailLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.Weight1TextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.Weight2TextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.PriceTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.ShowCode();
                this.ShowUnit();
                this.ClearData();
            }
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
            this.Unit1DropDownList.Items.Clear();
            this.Unit1DropDownList.DataTextField = "UnitName";
            this.Unit1DropDownList.DataValueField = "UnitCode";
            this.Unit1DropDownList.DataSource = this._unitBL.GetListForDDL();
            this.Unit1DropDownList.DataBind();
            this.Unit1DropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.Unit2DropDownList.Items.Clear();
            this.Unit2DropDownList.DataTextField = "UnitName";
            this.Unit2DropDownList.DataValueField = "UnitCode";
            this.Unit2DropDownList.DataSource = this._unitBL.GetListForDDL();
            this.Unit2DropDownList.DataBind();
            this.Unit2DropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();
            this.ProductShapeRBL.SelectedIndex = 0;
            this.Weight1TextBox.Text = "0";
            this.Weight2TextBox.Text = "0";
            this.Unit1DropDownList.SelectedIndex = 0;
            this.Unit2DropDownList.SelectedIndex = 0;
            this.PriceTextBox.Text = "0";
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();

            if (this.Weight1TextBox.Text == "")
                this.Weight1TextBox.Text = "0";
            if (Convert.ToDecimal(this.Weight1TextBox.Text) <= 0)
                _errorMsg = _errorMsg + " Weight 1st Must More Than 0.";

            if (this.Weight2TextBox.Text == "")
                this.Weight2TextBox.Text = "0";
            if (Convert.ToDecimal(this.Weight2TextBox.Text) <= 0)
                _errorMsg = _errorMsg + " Weight 2nd Must More Than 0.";

            if (this.Unit1DropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Unit 1st.";

            if (this.Unit2DropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Unit 2nd.";

            if (this.PriceTextBox.Text == "")
                this.PriceTextBox.Text = "0";
            if (Convert.ToDecimal(this.PriceTextBox.Text) <= 0)
                _errorMsg = _errorMsg + " Price Must More Than 0.";

            POSMsZoneMultiplePrice _posMsZoneMultiple = this._shippingBL.GetSinglePOSMsZoneMultiplePrice(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.ProductShapeRBL.SelectedValue, Convert.ToDecimal(this.Weight1TextBox.Text), Convert.ToDecimal(this.Weight2TextBox.Text));
            if (_posMsZoneMultiple != null)
                _errorMsg = _errorMsg + " Product Shape : " + this.ProductShapeRBL.SelectedItem + " With Weight 1st = " + this.Weight1TextBox.Text + "and Weight 2nd = " + this.Weight2TextBox.Text + " Already Exist.";

            List<General_TemporaryTable> _temp = this._shippingBL.CheckExistWeight(this.CodeTextBox.Text, this.ProductShapeRBL.SelectedValue, Convert.ToDecimal(this.Weight1TextBox.Text), Convert.ToDecimal(this.Weight2TextBox.Text));
            if (_temp != null)
                foreach (var _item in _temp)
                {
                    _errorMsg = _errorMsg + " Product Shape : " + this.ProductShapeRBL.SelectedItem + " With Weight 1st = " + this.Weight1TextBox.Text + " and Weight 2nd = " + this.Weight2TextBox.Text + " Already Exist in Between Weight : " + _item.Field1.Trim() + " - " + _item.Field2.Trim();
                }

            this.WarningLabel.Text = _errorMsg.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsZoneMultiplePrice _posMsZoneMultiple = new POSMsZoneMultiplePrice();
                _posMsZoneMultiple.ZoneCode = this.CodeTextBox.Text;
                _posMsZoneMultiple.ProductShape = this.ProductShapeRBL.SelectedValue;
                _posMsZoneMultiple.Weight1 = Convert.ToDecimal(this.Weight1TextBox.Text);
                _posMsZoneMultiple.Unit1 = this.Unit1DropDownList.SelectedValue;
                _posMsZoneMultiple.Weight2 = Convert.ToDecimal(this.Weight2TextBox.Text);
                _posMsZoneMultiple.Unit2 = this.Unit2DropDownList.SelectedValue;
                _posMsZoneMultiple.Price = Convert.ToDecimal(this.PriceTextBox.Text);
                if (this.FgActiveCheckBox.Checked == true)
                    _posMsZoneMultiple.FgActive = 'Y';
                else
                    _posMsZoneMultiple.FgActive = 'N';
                _posMsZoneMultiple.Remark = this.RemarkTextBox.Text;
                _posMsZoneMultiple.CreatedBy = HttpContext.Current.User.Identity.Name;
                _posMsZoneMultiple.CreatedDate = DateTime.Now;
                _posMsZoneMultiple.ModifiedBy = "";
                _posMsZoneMultiple.ModifiedDate = this._defaultdate;

                bool _result = this._shippingBL.AddPOSMsZoneMultiplePrice(_posMsZoneMultiple);

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