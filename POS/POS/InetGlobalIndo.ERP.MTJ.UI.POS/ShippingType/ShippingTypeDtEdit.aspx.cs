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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingType
{
    public partial class ShippingTypeDtEdit : ShippingTypeBase
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
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
                this.CodeTextBox.Attributes.Add("ReadOnly", "True");
                this.NameTextBox.Attributes.Add("ReadOnly", "True");
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
            POSMsShippingType _posMsShippingType = this._shippingBL.GetSinglePOSMsShippingType(_tempSplit[0]);
            this.CodeTextBox.Text = _posMsShippingType.ShippingTypeCode;
            this.NameTextBox.Text = _posMsShippingType.ShippingTypeName;

            POSMsShippingTypeDt _posMsShippingTypeDt = this._shippingBL.GetSinglePOSMsShippingTypeDt(_tempSplit[0],_tempSplit[1],_tempSplit[2]);
            this.CityTextBox.Text = _posMsShippingTypeDt.CityCode;
            this.ProductShapeRBL.SelectedValue = _posMsShippingTypeDt.ProductShape;
            this.CurrencyDropDownList.SelectedValue = _posMsShippingTypeDt.CurrCode;
            this.Price1TextBox.Text = Convert.ToDecimal(_posMsShippingTypeDt.Price1).ToString("#0.00");
            this.Price2TextBox.Text = Convert.ToDecimal(_posMsShippingTypeDt.Price2).ToString("#0.00");
            this.UnitDropDownList.SelectedValue = _posMsShippingTypeDt.UnitCode;
            this.EstimationTimeTextBox.Text = _posMsShippingTypeDt.EstimationTime;
            if (_posMsShippingTypeDt.FgActive == 'Y')
                this.FgActiveCheckBox.Checked = true;
            else
                this.FgActiveCheckBox.Checked = false; 
            this.RemarkTextBox.Text = _posMsShippingTypeDt.Remark;
        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();
            if (Convert.ToDecimal(this.Price1TextBox.Text) <= 0 | Convert.ToDecimal(this.Price2TextBox.Text) <= 0)
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
                POSMsShippingTypeDt _posMsShippingTypeDt = this._shippingBL.GetSinglePOSMsShippingTypeDt(_tempSplit[0],_tempSplit[1], _tempSplit[2]);
                _posMsShippingTypeDt.ShippingTypeCode = this.CodeTextBox.Text;
                _posMsShippingTypeDt.ProductShape = this.ProductShapeRBL.SelectedValue;
                _posMsShippingTypeDt.CurrCode = this.CurrencyDropDownList.SelectedValue;
                _posMsShippingTypeDt.Price1 = Convert.ToDecimal(this.Price1TextBox.Text);
                _posMsShippingTypeDt.Price2 = Convert.ToDecimal(this.Price2TextBox.Text);
                _posMsShippingTypeDt.UnitCode = this.UnitDropDownList.SelectedValue;
                _posMsShippingTypeDt.EstimationTime = this.EstimationTimeTextBox.Text;
                if (this.FgActiveCheckBox.Checked == true)
                    _posMsShippingTypeDt.FgActive = 'Y';
                else
                    _posMsShippingTypeDt.FgActive = 'N';
                _posMsShippingTypeDt.Remark = this.RemarkTextBox.Text;
                _posMsShippingTypeDt.ModifiedBy = HttpContext.Current.User.Identity.Name;
                _posMsShippingTypeDt.ModifiedDate = DateTime.Now;

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