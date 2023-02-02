using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAsset
{
    public partial class FixedAssetEdit : FixedAssetBase
    {
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private AccountBL _accountBL = new AccountBL();
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetCurrRate();

                this.ShowFAStatus();
                this.ShowFASubGroup();
                this.ShowCurrency();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.ForexRateTextBox.Attributes.Add("OnBlur", "CalculateHome(" + this.ForexRateTextBox.ClientID + ", " + this.BuyPriceForexTextBox.ClientID + ", " + this.BuyPriceHomeTextBox.ClientID + ", " + this.BuyPriceLifeInMonthsTextBox.ClientID + ", " + this.LifeInMonthsBeginDeprTextBox.ClientID + ", " + this.LifeInMonthsProcessDeprTextBox.ClientID + ", " + this.LifeInMonthsTotalDeprTextBox.ClientID + ", " + this.AmountBeginDeprTextBox.ClientID + ", " + this.AmountProcessDeprTextBox.ClientID + ", " + this.AmountTotalDeprTextBox.ClientID + ", " + this.CurrentAmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenFieldHome.ClientID + " ); ");
            this.BuyPriceForexTextBox.Attributes.Add("OnBlur", "CalculateHome(" + this.ForexRateTextBox.ClientID + ", " + this.BuyPriceForexTextBox.ClientID + ", " + this.BuyPriceHomeTextBox.ClientID + ", " + this.BuyPriceLifeInMonthsTextBox.ClientID + ", " + this.LifeInMonthsBeginDeprTextBox.ClientID + ", " + this.LifeInMonthsProcessDeprTextBox.ClientID + ", " + this.LifeInMonthsTotalDeprTextBox.ClientID + ", " + this.AmountBeginDeprTextBox.ClientID + ", " + this.AmountProcessDeprTextBox.ClientID + ", " + this.AmountTotalDeprTextBox.ClientID + ", " + this.CurrentAmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + " ," + this.DecimalPlaceHiddenFieldHome.ClientID + " ); ");
            this.BuyPriceLifeInMonthsTextBox.Attributes.Add("OnBlur", "CalculateHome(" + this.ForexRateTextBox.ClientID + ", " + this.BuyPriceForexTextBox.ClientID + ", " + this.BuyPriceHomeTextBox.ClientID + ", " + this.BuyPriceLifeInMonthsTextBox.ClientID + ", " + this.LifeInMonthsBeginDeprTextBox.ClientID + ", " + this.LifeInMonthsProcessDeprTextBox.ClientID + ", " + this.LifeInMonthsTotalDeprTextBox.ClientID + ", " + this.AmountBeginDeprTextBox.ClientID + ", " + this.AmountProcessDeprTextBox.ClientID + ", " + this.AmountTotalDeprTextBox.ClientID + ", " + this.CurrentAmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenFieldHome.ClientID + "  ); ");
            this.LifeInMonthsBeginDeprTextBox.Attributes.Add("OnBlur", "Calculate(" + this.ForexRateTextBox.ClientID + ", " + this.BuyPriceForexTextBox.ClientID + ", " + this.BuyPriceHomeTextBox.ClientID + ", " + this.BuyPriceLifeInMonthsTextBox.ClientID + ", " + this.LifeInMonthsBeginDeprTextBox.ClientID + ", " + this.LifeInMonthsProcessDeprTextBox.ClientID + ", " + this.LifeInMonthsTotalDeprTextBox.ClientID + ", " + this.AmountBeginDeprTextBox.ClientID + ", " + this.AmountProcessDeprTextBox.ClientID + ", " + this.AmountTotalDeprTextBox.ClientID + ", " + this.CurrentAmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + " ," + this.DecimalPlaceHiddenFieldHome.ClientID + " ); ");
            this.AmountBeginDeprTextBox.Attributes.Add("OnBlur", "Calculate(" + this.ForexRateTextBox.ClientID + ", " + this.BuyPriceForexTextBox.ClientID + ", " + this.BuyPriceHomeTextBox.ClientID + ", " + this.BuyPriceLifeInMonthsTextBox.ClientID + ", " + this.LifeInMonthsBeginDeprTextBox.ClientID + ", " + this.LifeInMonthsProcessDeprTextBox.ClientID + ", " + this.LifeInMonthsTotalDeprTextBox.ClientID + ", " + this.AmountBeginDeprTextBox.ClientID + ", " + this.AmountProcessDeprTextBox.ClientID + ", " + this.AmountTotalDeprTextBox.ClientID + ", " + this.CurrentAmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + " ," + this.DecimalPlaceHiddenFieldHome.ClientID + " ); ");
        }

        protected void SetAttribute()
        {
            this.FACodeTextBox.Attributes.Add("ReadOnly", "True");
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.BuyPriceHomeTextBox.Attributes.Add("ReadOnly", "True");
            this.LifeInMonthsProcessDeprTextBox.Attributes.Add("ReadOnly", "True");
            this.LifeInMonthsTotalDeprTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountProcessDeprTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTotalDeprTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrentAmountTextBox.Attributes.Add("ReadOnly", "True");

            this.ForexRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.BuyPriceForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.BuyPriceLifeInMonthsTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.LifeInMonthsBeginDeprTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.AmountBeginDeprTextBox.Attributes.Add("OnKeyDown", "return Numeric();");

            this.SetAttributeRate();
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
            MsFixedAsset _msFixedAsset = this._fixedAssetBL.GetSingleFixedAsset(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_msFixedAsset.CurrCode);
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(this._currencyBL.GetCurrDefault());

            this.FACodeTextBox.Text = _msFixedAsset.FACode;
            this.FANameTextBox.Text = _msFixedAsset.FAName;
            this.SpesificationTextBox.Text = _msFixedAsset.Spesification;
            this.FAStatusDropDownList.SelectedValue = _msFixedAsset.FAStatus.ToString();
            this.FAOwnerCheckBox.Checked = FixedAssetsDataMapper.IsFGAddValue(_msFixedAsset.FAOwner);
            this.FASubGroupDropDownList.SelectedValue = _msFixedAsset.FASubGroup;
            this.DateTextBox.Text = DateFormMapper.GetValue(_msFixedAsset.BuyingDate);
            this.FALocationTypeDropDownList.SelectedValue = _msFixedAsset.FALocationType;
            this.ShowFALocation();
            if (_msFixedAsset.FALocationCode != null && _msFixedAsset.FALocationCode != "")
            {
                this.FALocationDropDownList.SelectedValue = _msFixedAsset.FALocationCode;
            }
            this.CurrencyDropDownList.SelectedValue = _msFixedAsset.CurrCode;
            if (this.CurrencyDropDownList.SelectedValue == this._currencyBL.GetCurrDefault())
            {
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");
            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("Style", "Background-Color:#ffffff");
            }
            this.ForexRateTextBox.Text = (_msFixedAsset.ForexRate == 0 ? "0" : _msFixedAsset.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.BuyPriceForexTextBox.Text = (_msFixedAsset.AmountForex == 0 ? "0" : _msFixedAsset.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.BuyPriceHomeTextBox.Text = (_msFixedAsset.AmountHome == 0 ? "0" : _msFixedAsset.AmountHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome)));
            this.BuyPriceLifeInMonthsTextBox.Text = (_msFixedAsset.TotalLifeMonth == 0 ? "0" : _msFixedAsset.TotalLifeMonth.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));

            this.LifeInMonthsBeginDeprTextBox.Text = (_msFixedAsset.LifeDepr == 0 ? "0" : _msFixedAsset.LifeDepr.ToString());
            this.LifeInMonthsProcessDeprTextBox.Text = (_msFixedAsset.LifeProcess == 0 ? "0" : _msFixedAsset.LifeProcess.ToString());
            this.LifeInMonthsTotalDeprTextBox.Text = (_msFixedAsset.TotalLifeDepr == 0 ? "0" : _msFixedAsset.TotalLifeDepr.ToString());

            this.AmountBeginDeprTextBox.Text = (_msFixedAsset.AmountDepr == 0 ? "0" : _msFixedAsset.AmountDepr.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.AmountProcessDeprTextBox.Text = (_msFixedAsset.AmountProcess == 0 ? "0" : _msFixedAsset.AmountProcess.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.AmountTotalDeprTextBox.Text = (_msFixedAsset.TotalAmountDepr == 0 ? "0" : _msFixedAsset.TotalAmountDepr.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));

            this.CurrentAmountTextBox.Text = (_msFixedAsset.AmountCurrent == 0 ? "0" : _msFixedAsset.AmountCurrent.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));

            this.StatusProcessCheckBox.Checked = FixedAssetsDataMapper.IsFGAddValue(_msFixedAsset.FgProcess);
            this.ActiveCheckBox.Checked = FixedAssetsDataMapper.IsFGAddValue(_msFixedAsset.FgActive);
            this.SoldCheckBox.Checked = FixedAssetsDataMapper.IsFGAddValue(_msFixedAsset.FgSold);
            this.FAPhoto.Attributes.Add("src", "" + ApplicationConfig.FixedAssetPhotoVirDirPath + _msFixedAsset.Photo + "?t=" + System.DateTime.Now.ToString());
        }

        public void ShowFAStatus()
        {
            this.FAStatusDropDownList.DataTextField = "FAStatusName";
            this.FAStatusDropDownList.DataValueField = "FAStatusCode";
            this.FAStatusDropDownList.DataSource = this._fixedAssetBL.GetListFAStatus();
            this.FAStatusDropDownList.DataBind();
            this.FAStatusDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowFASubGroup()
        {
            this.FASubGroupDropDownList.DataTextField = "FASubGrpName";
            this.FASubGroupDropDownList.DataValueField = "FASubGrpCode";
            this.FASubGroupDropDownList.DataSource = this._fixedAssetBL.GetListFAGroupSub();
            this.FASubGroupDropDownList.DataBind();
            this.FASubGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCurrency()
        {
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void FALocationTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FALocationDropDownList.Items.Clear();
            this.FALocationDropDownList.DataTextField = "Name";
            this.FALocationDropDownList.DataValueField = "Code";
            this.FALocationDropDownList.DataSource = this._fixedAssetBL.GetListDDLFixedAssetLocation(FixedAssetsDataMapper.GetValueFALocation(this.FALocationTypeDropDownList.SelectedValue));
            this.FALocationDropDownList.DataBind();
            this.FALocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CurrencyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrencyDropDownList.SelectedValue);
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(this._currencyBL.GetCurrDefault());
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (this.CurrencyDropDownList.SelectedValue != "null")
            {
                if (this.CurrencyDropDownList.SelectedValue == this._currencyBL.GetCurrDefault())
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("Style", "Background-Color:#ffffff");
                }
                decimal _currRate = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrencyDropDownList.SelectedValue);
                if (this.CurrencyDropDownList.SelectedValue != this._currencyBL.GetCurrDefault())
                {
                    this.ForexRateTextBox.Text = _currRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    decimal _buyPriceHome = _currRate * Convert.ToDecimal(this.BuyPriceForexTextBox.Text);
                    this.BuyPriceHomeTextBox.Text = (_buyPriceHome == 0 ? "0" : _buyPriceHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome)));
                    this.LifeInMonthsBeginDeprTextBox.Text = "0";
                    this.LifeInMonthsProcessDeprTextBox.Text = "0";
                    this.LifeInMonthsTotalDeprTextBox.Text = "0";
                    this.AmountBeginDeprTextBox.Text = "0";
                    this.AmountProcessDeprTextBox.Text = "0";
                    this.AmountTotalDeprTextBox.Text = "0";
                    decimal _currentAmount = _buyPriceHome - Convert.ToDecimal(AmountTotalDeprTextBox.Text);
                    this.CurrentAmountTextBox.Text = (_currentAmount == 0 ? "0" : _currentAmount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
                }
                else
                {
                    this.ForexRateTextBox.Text = "1";
                    decimal _buyPriceHome = 1 * Convert.ToDecimal(this.BuyPriceForexTextBox.Text);
                    this.BuyPriceHomeTextBox.Text = (_buyPriceHome == 0 ? "0" : _buyPriceHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome)));
                    this.LifeInMonthsBeginDeprTextBox.Text = "0";
                    this.LifeInMonthsProcessDeprTextBox.Text = "0";
                    this.LifeInMonthsTotalDeprTextBox.Text = "0";
                    this.AmountBeginDeprTextBox.Text = "0";
                    this.AmountProcessDeprTextBox.Text = "0";
                    this.AmountTotalDeprTextBox.Text = "0";
                    decimal _currentAmount = _buyPriceHome - Convert.ToDecimal(AmountTotalDeprTextBox.Text);
                    this.CurrentAmountTextBox.Text = (_currentAmount == 0 ? "0" : _currentAmount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
                }
            }
            else
            {
                this.ForexRateTextBox.Text = "1";
                decimal _buyPriceHome = 1 * Convert.ToDecimal(this.BuyPriceForexTextBox.Text);
                this.BuyPriceHomeTextBox.Text = (_buyPriceHome == 0 ? "0" : _buyPriceHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome)));
                this.LifeInMonthsBeginDeprTextBox.Text = "0";
                this.LifeInMonthsProcessDeprTextBox.Text = "0";
                this.LifeInMonthsTotalDeprTextBox.Text = "0";
                this.AmountBeginDeprTextBox.Text = "0";
                this.AmountProcessDeprTextBox.Text = "0";
                this.AmountTotalDeprTextBox.Text = "0";
                decimal _currentAmount = _buyPriceHome - Convert.ToDecimal(AmountTotalDeprTextBox.Text);
                this.CurrentAmountTextBox.Text = (_currentAmount == 0 ? "0" : _currentAmount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome)));
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsFixedAsset _msFixedAsset = this._fixedAssetBL.GetSingleFixedAsset(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msFixedAsset.FAName = this.FANameTextBox.Text;
            _msFixedAsset.Spesification = this.SpesificationTextBox.Text;
            _msFixedAsset.FAStatus = this.FAStatusDropDownList.SelectedValue;
            _msFixedAsset.FAOwner = FixedAssetsDataMapper.IsAllowAddValue(this.FAOwnerCheckBox.Checked);
            _msFixedAsset.FASubGroup = this.FASubGroupDropDownList.SelectedValue;
            _msFixedAsset.BuyingDate = DateFormMapper.GetValue(this.DateTextBox.Text);
            _msFixedAsset.FALocationType = this.FALocationTypeDropDownList.SelectedValue;
            _msFixedAsset.FALocationCode = this.FALocationDropDownList.SelectedValue;
            _msFixedAsset.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _msFixedAsset.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _msFixedAsset.AmountForex = Convert.ToDecimal(this.BuyPriceForexTextBox.Text);
            _msFixedAsset.AmountHome = Convert.ToDecimal(this.BuyPriceHomeTextBox.Text);
            _msFixedAsset.TotalLifeMonth = Convert.ToInt16(this.BuyPriceLifeInMonthsTextBox.Text);
            _msFixedAsset.LifeDepr = Convert.ToInt16(this.LifeInMonthsBeginDeprTextBox.Text);
            _msFixedAsset.LifeProcess = Convert.ToInt16(this.LifeInMonthsProcessDeprTextBox.Text);
            _msFixedAsset.TotalLifeDepr = Convert.ToInt16(this.LifeInMonthsTotalDeprTextBox.Text);
            _msFixedAsset.AmountDepr = Convert.ToDecimal(this.AmountBeginDeprTextBox.Text);
            _msFixedAsset.AmountProcess = Convert.ToDecimal(this.AmountProcessDeprTextBox.Text);
            _msFixedAsset.TotalAmountDepr = Convert.ToDecimal(this.AmountTotalDeprTextBox.Text);
            _msFixedAsset.AmountCurrent = Convert.ToDecimal(this.CurrentAmountTextBox.Text);
            _msFixedAsset.FgActive = FixedAssetsDataMapper.IsAllowAddValue(this.ActiveCheckBox.Checked);
            _msFixedAsset.FgSold = FixedAssetsDataMapper.IsAllowAddValue(this.SoldCheckBox.Checked);
            _msFixedAsset.FgProcess = FixedAssetsDataMapper.IsAllowAddValue(this.StatusProcessCheckBox.Checked);

            bool _result = this._fixedAssetBL.EditFixedAsset(_msFixedAsset);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        private void SetCurrRate()
        {
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(this._currencyBL.GetCurrDefault());
            this.DecimalPlaceHiddenFieldHome.Value = CurrencyDataMapper.GetDecimal(_decimalPlaceHome);
        }
    }
}