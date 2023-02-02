using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAAddStock
{
    public partial class FAAddStockDetailEdit : FAAddStockBase
    {
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
        private StockChangeToFABL _faAddStockBL = new StockChangeToFABL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _wrhsBL = new WarehouseBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private PermissionBL _permBL = new PermissionBL();

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

                this.ShowFAStatus();
                this.ShowFASubGroup();
                this.ShowCurrency();

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttribute()
        {
            this.LifeMonthTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.AmountForexTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.AmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        public void ShowFAStatus()
        {
            this.FAStatusDropDownList.Items.Clear();
            this.FAStatusDropDownList.DataTextField = "FAStatusName";
            this.FAStatusDropDownList.DataValueField = "FAStatusCode";
            this.FAStatusDropDownList.DataSource = this._fixedAssetBL.GetListFAStatus();
            this.FAStatusDropDownList.DataBind();
            this.FAStatusDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowFASubGroup()
        {
            this.FASubGroupDropDownList.Items.Clear();
            this.FASubGroupDropDownList.DataTextField = "FASubGrpName";
            this.FASubGroupDropDownList.DataValueField = "FASubGrpCode";
            this.FASubGroupDropDownList.DataSource = this._fixedAssetBL.GetListFAGroupSub();
            this.FASubGroupDropDownList.DataBind();
            this.FASubGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void FALocationTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FALocationTypeDropDownList.SelectedValue != "null")
            {
                this.FALocationDropDownList.Items.Clear();
                this.FALocationDropDownList.DataTextField = "Name";
                this.FALocationDropDownList.DataValueField = "Code";
                this.FALocationDropDownList.DataSource = this._fixedAssetBL.GetListDDLFixedAssetLocation(FixedAssetsDataMapper.GetValueFALocation(this.FALocationTypeDropDownList.SelectedValue));
                this.FALocationDropDownList.DataBind();
                this.FALocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            else
            {
                this.FALocationDropDownList.Items.Clear();
                this.FALocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
        }

        protected void FASubGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FASubGroupDropDownList.SelectedValue != "null")
            {
                MsFAGroupSub _msFAGroupSub = this._fixedAssetBL.GetSingleFAGroupSub(this.FASubGroupDropDownList.SelectedValue);
                this.StatusProcessCheckBox.Checked = FixedAssetsDataMapper.IsFGAddValue(Convert.ToChar(_msFAGroupSub.FgProcess));
            }
            else
            {
                this.StatusProcessCheckBox.Checked = false;
            }
        }

        public void ShowCurrency()
        {
            this.CurrDropDownList.DataTextField = "CurrCode";
            this.CurrDropDownList.DataValueField = "CurrCode";
            this.CurrDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrDropDownList.DataBind();
            this.CurrDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowFALocation()
        {
            this.FALocationDropDownList.Items.Clear();
            this.FALocationDropDownList.DataTextField = "Name";
            this.FALocationDropDownList.DataValueField = "Code";
            this.FALocationDropDownList.DataSource = this._fixedAssetBL.GetListDDLFixedAssetLocation(FixedAssetsDataMapper.GetValueFALocation(this.FALocationTypeDropDownList.SelectedValue));
            this.FALocationDropDownList.DataBind();
            this.FALocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            GLFAAddStockDt _glFAAddStockDt = this._faAddStockBL.GetSingleFAAddStockDt(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._code), ApplicationConfig.EncryptionKey)));
            GLFAAddStockDt2 _glFAAddStockDt2 = this._faAddStockBL.GetSingleFAAddStockDt2(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._code), ApplicationConfig.EncryptionKey)));

            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_glFAAddStockDt.ProductCode);
            this.LocationTextBox.Text = _wrhsBL.GetWarehouseLocationNameByCode(_glFAAddStockDt.LocationCode);

            if (_glFAAddStockDt2 == null)
            {
                this.FACodeTextBox.Text = "";
                this.FANameTextBox.Text = "";
                this.FAStatusDropDownList.SelectedValue = "null";
                this.FAOwnerCheckBox.Checked = false;
                this.FASubGroupDropDownList.SelectedValue = "null";
                this.FALocationTypeDropDownList.SelectedValue = "null";
                this.ShowFALocation();
                this.CurrDropDownList.SelectedValue = "null";
                this.FALocationDropDownList.SelectedValue = "null";
                this.LifeMonthTextBox.Text = "0";
                this.AmountForexTextBox.Text = "0";
                this.StatusProcessCheckBox.Checked = false;
                this.SpecificationTextBox.Text = "";
                this.DecimalPlaceHiddenField.Value = "";
            }
            else
            {
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_glFAAddStockDt2.CurrCode);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.FACodeTextBox.Text = _glFAAddStockDt2.FACode;
                this.FANameTextBox.Text = _glFAAddStockDt2.FAName;
                this.FAStatusDropDownList.SelectedValue = _glFAAddStockDt2.FAStatus;
                this.FAOwnerCheckBox.Checked = FAAddStockDataMapper.IsChecked(_glFAAddStockDt2.FAOwner);
                this.FASubGroupDropDownList.SelectedValue = _glFAAddStockDt2.FASubGroup;
                this.FALocationTypeDropDownList.SelectedValue = _glFAAddStockDt2.FALocationType;
                this.ShowFALocation();
                this.CurrDropDownList.SelectedValue = _glFAAddStockDt2.CurrCode;
                this.FALocationDropDownList.SelectedValue = _glFAAddStockDt2.FALocationCode;
                this.LifeMonthTextBox.Text = (_glFAAddStockDt2.LifeMonth == 0) ? "0" : _glFAAddStockDt2.LifeMonth.ToString();
                this.AmountForexTextBox.Text = (_glFAAddStockDt2.AmountForex == 0) ? "0" : _glFAAddStockDt2.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.StatusProcessCheckBox.Checked = FAAddStockDataMapper.IsChecked(_glFAAddStockDt2.FgProcess);
                this.SpecificationTextBox.Text = _glFAAddStockDt2.Spesification;
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAAddStockDt2 _glFAAddStockDt2 = null;

            bool _result = false;

            if (_faAddStockBL.GetSingleFAAddStockDt2(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._code), ApplicationConfig.EncryptionKey))) == null)
            {
                _glFAAddStockDt2 = new GLFAAddStockDt2();

                _glFAAddStockDt2.GLFAAddStockDtCode = new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._code), ApplicationConfig.EncryptionKey));
                _glFAAddStockDt2.FACode = this.FACodeTextBox.Text;
                _glFAAddStockDt2.FAName = this.FANameTextBox.Text;
                _glFAAddStockDt2.FAStatus = this.FAStatusDropDownList.SelectedValue;
                _glFAAddStockDt2.FAOwner = FAAddStockDataMapper.IsChecked(this.FAOwnerCheckBox.Checked);
                _glFAAddStockDt2.FASubGroup = this.FASubGroupDropDownList.SelectedValue;
                _glFAAddStockDt2.FALocationType = this.FALocationTypeDropDownList.SelectedValue;
                _glFAAddStockDt2.CurrCode = this.CurrDropDownList.SelectedValue;
                _glFAAddStockDt2.FALocationCode = this.FALocationDropDownList.SelectedValue;
                _glFAAddStockDt2.LifeMonth = Convert.ToInt32(this.LifeMonthTextBox.Text);
                decimal _amountForexOriginal = (this.AmountForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountForexTextBox.Text);
                _glFAAddStockDt2.AmountForex = _amountForexOriginal;
                _glFAAddStockDt2.FgProcess = FAAddStockDataMapper.IsChecked(this.StatusProcessCheckBox.Checked);
                _glFAAddStockDt2.Spesification = this.SpecificationTextBox.Text;

                _result = this._faAddStockBL.AddFAAddStockDt2(_glFAAddStockDt2);
            }
            else
            {
                _glFAAddStockDt2 = _faAddStockBL.GetSingleFAAddStockDt2(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._code), ApplicationConfig.EncryptionKey)));

                _glFAAddStockDt2.FACode = this.FACodeTextBox.Text;
                _glFAAddStockDt2.FAName = this.FANameTextBox.Text;
                _glFAAddStockDt2.FAStatus = this.FAStatusDropDownList.SelectedValue;
                _glFAAddStockDt2.FAOwner = FAAddStockDataMapper.IsChecked(this.FAOwnerCheckBox.Checked);
                _glFAAddStockDt2.FASubGroup = this.FASubGroupDropDownList.SelectedValue;
                _glFAAddStockDt2.FALocationType = this.FALocationTypeDropDownList.SelectedValue;
                _glFAAddStockDt2.CurrCode = this.CurrDropDownList.SelectedValue;
                _glFAAddStockDt2.FALocationCode = this.FALocationDropDownList.SelectedValue;
                _glFAAddStockDt2.LifeMonth = Convert.ToInt32(this.LifeMonthTextBox.Text);
                decimal _amountForexOriginal = (this.AmountForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.AmountForexTextBox.Text);
                _glFAAddStockDt2.AmountForex = _amountForexOriginal;
                _glFAAddStockDt2.FgProcess = FAAddStockDataMapper.IsChecked(this.StatusProcessCheckBox.Checked);
                _glFAAddStockDt2.Spesification = this.SpecificationTextBox.Text;

                _result = this._faAddStockBL.EditFAAddStockDt2(_glFAAddStockDt2);
            }

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

        protected void CurrDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrDropDownList.SelectedValue != "null")
            {
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrDropDownList.SelectedValue);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                this.AmountForexTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.AmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            }
        }
    }
}