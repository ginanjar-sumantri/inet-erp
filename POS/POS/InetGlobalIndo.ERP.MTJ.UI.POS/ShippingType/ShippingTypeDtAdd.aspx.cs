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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingType
{
    public partial class ShippingTypeDtAdd : ShippingTypeBase
    {
        private ShippingBL _shippingBL = new ShippingBL();
        private PermissionBL _permBL = new PermissionBL();
        private CityBL _cityBL = new CityBL();
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
                this.PageTitleLiteral.Text = this._pageTitleDetailLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.Price1TextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.Price2TextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.EstimationTimeTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.ShowCode();
                this.ShowCity();
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
            POSMsShippingType _posMsShippingType = this._shippingBL.GetSinglePOSMsShippingType(_tempSplit[0]);
            this.CodeTextBox.Text = _posMsShippingType.ShippingTypeCode;
            this.NameTextBox.Text = _posMsShippingType.ShippingTypeName;
        }

        protected void ShowCity()
        {
            this.CityDropDownList.Items.Clear();
            this.CityDropDownList.DataTextField = "CityName";
            this.CityDropDownList.DataValueField = "CityCode";
            this.CityDropDownList.DataSource = this._cityBL.GetListCityForDDL();
            this.CityDropDownList.DataBind();
            this.CityDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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
            this.CurrencyDropDownList.SelectedIndex = 0;
            this.CityDropDownList.SelectedIndex = 0;
            this.ProductShapeRBL.SelectedIndex = 0;
            this.Price1TextBox.Text = "0";
            this.Price2TextBox.Text = "0";
            this.UnitDropDownList.SelectedIndex = 0;
            this.EstimationTimeTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();
            if (Convert.ToDecimal(this.Price1TextBox.Text) <= 0 | Convert.ToDecimal(this.Price2TextBox.Text) <= 0)
                _errorMsg = _errorMsg + " Price Must More Than 0.";

            if (this.CurrencyDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Currency.";

            if (this.CityDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of City.";

            if (this.UnitDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Unit.";

            POSMsShippingTypeDt _posMsShippingTypeDt = this._shippingBL.GetSinglePOSMsShippingTypeDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.CityDropDownList.SelectedValue, this.ProductShapeRBL.SelectedValue);
            if (_posMsShippingTypeDt != null)
                _errorMsg = _errorMsg + this.CityDropDownList.SelectedItem + " City With Product Shape = " + this.ProductShapeRBL.SelectedItem + " Already Exist.";

            this.WarningLabel.Text = _errorMsg.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsShippingTypeDt _posMsShippingTypeDt = new POSMsShippingTypeDt();
                _posMsShippingTypeDt.ShippingTypeCode = this.CodeTextBox.Text;
                _posMsShippingTypeDt.ProductShape = this.ProductShapeRBL.SelectedValue;
                _posMsShippingTypeDt.CurrCode = this.CurrencyDropDownList.SelectedValue;
                _posMsShippingTypeDt.CityCode = this.CityDropDownList.SelectedValue;
                _posMsShippingTypeDt.Price1 = Convert.ToDecimal(this.Price1TextBox.Text);
                _posMsShippingTypeDt.Price2 = Convert.ToDecimal(this.Price2TextBox.Text);
                _posMsShippingTypeDt.UnitCode = this.UnitDropDownList.SelectedValue;
                _posMsShippingTypeDt.EstimationTime = this.EstimationTimeTextBox.Text;
                if (this.FgActiveCheckBox.Checked == true)
                    _posMsShippingTypeDt.FgActive = 'Y';
                else
                    _posMsShippingTypeDt.FgActive = 'N'; _posMsShippingTypeDt.Remark = this.RemarkTextBox.Text;
                _posMsShippingTypeDt.CreatedBy = HttpContext.Current.User.Identity.Name;
                _posMsShippingTypeDt.CreatedDate = DateTime.Now;
                _posMsShippingTypeDt.ModifiedBy = "";
                _posMsShippingTypeDt.ModifiedDate = this._defaultdate;

                bool _result = this._shippingBL.AddPOSMsShippingTypeDt(_posMsShippingTypeDt);

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