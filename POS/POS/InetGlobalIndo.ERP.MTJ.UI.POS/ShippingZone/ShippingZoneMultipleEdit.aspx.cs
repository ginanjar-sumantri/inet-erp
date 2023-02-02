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
    public partial class ShippingZoneMultipleEdit : ShippingZoneBase
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
                this.PageTitleLiteral.Text = this._pageTitleMultipleDetailLiteral;
                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.PriceTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.CodeTextBox.Attributes.Add("ReadOnly", "True");
                this.NameTextBox.Attributes.Add("ReadOnly", "True");
                this.ProductShapeTextBox.Attributes.Add("ReadOnly", "True");
                this.Weight1TextBox.Attributes.Add("ReadOnly", "True");
                this.Weight2TextBox.Attributes.Add("ReadOnly", "True");
                this.ClearLabel();
                this.ShowUnit();
                this.ShowData();
            }
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

        protected void ShowData()
        {
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            POSMsZone _posMsZone = this._shippingBL.GetSinglePOSMsZone(_tempSplit[0]);
            this.CodeTextBox.Text = _posMsZone.ZoneCode;
            this.NameTextBox.Text = _posMsZone.ZoneName;

            POSMsZoneMultiplePrice _posMsZoneMultiple = this._shippingBL.GetSinglePOSMsZoneMultiplePrice(_tempSplit[0],_tempSplit[1],Convert.ToDecimal(_tempSplit[2]),Convert.ToDecimal(_tempSplit[3]));
            String _productShapeType = "";
            if (_posMsZoneMultiple.ProductShape == "0")
                _productShapeType = "Document";
            else if (_posMsZoneMultiple.ProductShape == "1")
                _productShapeType = "Non Document";
            else if (_posMsZoneMultiple.ProductShape == "2")
                _productShapeType = "International Priority";

            this.ProductShapeTextBox.Text = _productShapeType;
            this.Weight1TextBox.Text = Convert.ToDecimal(_posMsZoneMultiple.Weight1).ToString("#,#");
            this.Unit1DropDownList.SelectedValue = _posMsZoneMultiple.Unit1;
            this.Weight2TextBox.Text = Convert.ToDecimal(_posMsZoneMultiple.Weight2).ToString("#,#");
            this.Unit2DropDownList.SelectedValue = _posMsZoneMultiple.Unit2;
            this.PriceTextBox.Text = Convert.ToDecimal(_posMsZoneMultiple.Price).ToString("#,#");
            if (_posMsZoneMultiple.FgActive == 'Y')
                this.FgActiveCheckBox.Checked = true;
            else
                this.FgActiveCheckBox.Checked = false; 
            this.RemarkTextBox.Text = _posMsZoneMultiple.Remark;
        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();
            if (Convert.ToDecimal(this.PriceTextBox.Text) <= 0)
                _errorMsg = _errorMsg + " Price Must More Than 0.";

            if (this.Unit1DropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Unit.";

            if (this.Unit2DropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Unit.";

            this.WarningLabel.Text = _errorMsg.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
                POSMsZoneMultiplePrice _POSMsZoneMultiple = this._shippingBL.GetSinglePOSMsZoneMultiplePrice(_tempSplit[0], _tempSplit[1], Convert.ToDecimal(_tempSplit[2]), Convert.ToDecimal(_tempSplit[3]));
                _POSMsZoneMultiple.Unit1 = this.Unit1DropDownList.SelectedValue;
                _POSMsZoneMultiple.Unit2 = this.Unit2DropDownList.SelectedValue;
                _POSMsZoneMultiple.Price = Convert.ToDecimal(this.PriceTextBox.Text);
                if (this.FgActiveCheckBox.Checked == true)
                    _POSMsZoneMultiple.FgActive = 'Y';
                else
                    _POSMsZoneMultiple.FgActive = 'N';
                _POSMsZoneMultiple.Remark = this.RemarkTextBox.Text;
                _POSMsZoneMultiple.ModifiedBy = HttpContext.Current.User.Identity.Name;
                _POSMsZoneMultiple.ModifiedDate = DateTime.Now;

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