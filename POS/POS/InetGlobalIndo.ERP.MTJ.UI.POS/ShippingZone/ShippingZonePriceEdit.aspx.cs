using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingZone
{
    public partial class ShippingZonePriceEdit : ShippingZoneBase
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
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
                this.PriceTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.CodeTextBox.Attributes.Add("ReadOnly", "True");
                this.NameTextBox.Attributes.Add("ReadOnly", "True");
                this.ProductShapeTextBox.Attributes.Add("ReadOnly", "True");
                this.WeightTextBox.Attributes.Add("ReadOnly", "True");
                this.ClearLabel();
                this.ShowUnit();
                this.ShowCurrency();
                this.ShowData();
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

        protected void ShowData()
        {
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            POSMsZone _posMsZone = this._shippingBL.GetSinglePOSMsZone(_tempSplit[0]);
            this.CodeTextBox.Text = _posMsZone.ZoneCode;
            this.NameTextBox.Text = _posMsZone.ZoneName;

            POSMsZonePrice _posMsZonePrice = this._shippingBL.GetSinglePOSMsZonePrice(_tempSplit[0],_tempSplit[1],Convert.ToDecimal(_tempSplit[2]));
            String _productShapeType = "";
            if (_posMsZonePrice.ProductShape == "0")
                _productShapeType = "Document";
            else if (_posMsZonePrice.ProductShape == "1")
                _productShapeType = "Non Document";
            else if (_posMsZonePrice.ProductShape == "2")
                _productShapeType = "International Priority";

            this.ProductShapeTextBox.Text = _productShapeType;
            this.CurrencyDropDownList.SelectedValue = _posMsZonePrice.CurrCode;
            this.WeightTextBox.Text = Convert.ToDecimal(_posMsZonePrice.Weight).ToString("#,#");
            this.UnitDropDownList.SelectedValue = _posMsZonePrice.Unit;
            this.PriceTextBox.Text = Convert.ToDecimal(_posMsZonePrice.Price).ToString("#,#");
            if (_posMsZonePrice.FgActive == 'Y')
                this.FgActiveCheckBox.Checked = true;
            else
                this.FgActiveCheckBox.Checked = false; 
            this.RemarkTextBox.Text = _posMsZonePrice.Remark;
        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();
            if (Convert.ToDecimal(this.PriceTextBox.Text) <= 0)
                _errorMsg = _errorMsg + " Price Must More Than 0.";

            if (this.CurrencyDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Currency.";
            
            if (this.UnitDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Unit.";

            this.WarningLabel.Text = _errorMsg.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
                POSMsZonePrice _POSMsZonePrice = this._shippingBL.GetSinglePOSMsZonePrice(_tempSplit[0],_tempSplit[1], Convert.ToDecimal(_tempSplit[2]));
                _POSMsZonePrice.CurrCode = this.CurrencyDropDownList.SelectedValue;
                _POSMsZonePrice.Unit = this.UnitDropDownList.SelectedValue;
                _POSMsZonePrice.Price = Convert.ToDecimal(this.PriceTextBox.Text);
                if (this.FgActiveCheckBox.Checked == true)
                    _POSMsZonePrice.FgActive = 'Y';
                else
                    _POSMsZonePrice.FgActive = 'N';
                _POSMsZonePrice.Remark = this.RemarkTextBox.Text;
                _POSMsZonePrice.ModifiedBy = HttpContext.Current.User.Identity.Name;
                _POSMsZonePrice.ModifiedDate = DateTime.Now;

                bool _result = this._shippingBL.EditSubmit();

                if (_result == true)
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Edit Data";
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}