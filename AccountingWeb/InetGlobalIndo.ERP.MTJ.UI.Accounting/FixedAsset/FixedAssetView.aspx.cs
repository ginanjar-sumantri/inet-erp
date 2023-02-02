using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAsset
{
    public partial class FixedAssetView : FixedAssetBase
    {
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private AccountBL _accountBL = new AccountBL();
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _imgCreateJournal = "create_journal.jpg";
        private string _imgDeleteJournal = "delete_journal.jpg";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            MsFixedAsset _msFixedAsset = this._fixedAssetBL.GetSingleFixedAsset(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_msFixedAsset.CurrCode);
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(this._currencyBL.GetCurrDefault());

            this.FACodeTextBox.Text = _msFixedAsset.FACode;
            this.FANameTextBox.Text = _msFixedAsset.FAName;
            this.SpesificationTextBox.Text = _msFixedAsset.Spesification;
            this.FAStatusTexBox.Text = _fixedAssetBL.GetFAStatusSubNameByCode(_msFixedAsset.FAStatus);
            this.FAOwnerCheckBox.Checked = FixedAssetsDataMapper.IsFGAddValue(_msFixedAsset.FAOwner);
            this.FASubGroupTextBox.Text = _fixedAssetBL.GetFAGroupSubNameByCode(_msFixedAsset.FASubGroup);
            this.DateTextBox.Text = DateFormMapper.GetValue(_msFixedAsset.BuyingDate);
            this.FALocationTypeDropDownList.SelectedValue = _msFixedAsset.FALocationType;
            if (_msFixedAsset.FALocationCode != null && _msFixedAsset.FALocationCode != "")
            {
                this.FALocationTextBox.Text = _fixedAssetBL.GetFALocNameByLocTypeAndCode(FixedAssetsDataMapper.GetValueFALocation(_msFixedAsset.FALocationType), _msFixedAsset.FALocationCode).Name;
            }
            this.CurrencyTextBox.Text = _msFixedAsset.CurrCode;
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

            this.CreatedFromLabel.Text = FixedAssetsDataMapper.CreatedFromText(_msFixedAsset.CreatedFrom);
            this.CreateJournalLabel.Text = FixedAssetsDataMapper.CreateJournal(_msFixedAsset.CreateJournal);
            this.CreatedFromHiddenField.Value = _msFixedAsset.CreatedFrom.ToString();
            this.CreateJournalHiddenField.Value = _msFixedAsset.CreateJournal.ToString();

            //this.ShowCreateJournalButton();
        }

        //private void ShowCreateJournalButton()
        //{
        //    if (this.CreatedFromHiddenField.Value.Trim().ToLower() == FixedAssetStatus.CreatedFromStatus(FixedAssetCreatedFrom.Manual).ToString().Trim().ToLower())
        //    {
        //        if (this.CreateJournalHiddenField.Value.Trim().ToLower() == FixedAssetsDataMapper.CreateJournal(YesNo.Yes).ToString().ToLower())
        //        {
        //            this.CreateJournalButton.Visible = true;
        //            this.CreateJournalButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgDeleteJournal;

        //            this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

        //            if (this._permUnposting == PermissionLevel.NoAccess)
        //            {
        //                this.CreateJournalButton.Visible = false;
        //            }
        //        }
        //        else if (this.CreateJournalHiddenField.Value.Trim().ToLower() == FixedAssetsDataMapper.CreateJournal(YesNo.No).ToString().ToLower())
        //        {
        //            this.CreateJournalButton.Visible = true;
        //            this.CreateJournalButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgCreateJournal;

        //            this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

        //            if (this._permPosting == PermissionLevel.NoAccess)
        //            {
        //                this.CreateJournalButton.Visible = false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        this.CreateJournalButton.Visible = false;
        //        this.CreateJournalButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgDeleteJournal;
        //    }
        //}

        protected void CurrencyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrencyTextBox.Text != "")
            {
                decimal _currRate = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrencyTextBox.Text);
                if (_currRate != 0)
                {
                    this.ForexRateTextBox.Text = _currRate.ToString("#,###.##");
                    decimal _buyPriceHome = _currRate * Convert.ToDecimal(this.BuyPriceForexTextBox.Text);
                    this.BuyPriceHomeTextBox.Text = (_buyPriceHome == 0 ? "0" : _buyPriceHome.ToString("#,###.##"));
                    this.LifeInMonthsBeginDeprTextBox.Text = "0";
                    this.LifeInMonthsProcessDeprTextBox.Text = "0";
                    this.LifeInMonthsTotalDeprTextBox.Text = "0";
                    this.AmountBeginDeprTextBox.Text = "0";
                    this.AmountProcessDeprTextBox.Text = "0";
                    this.AmountTotalDeprTextBox.Text = "0";
                    decimal _currentAmount = _buyPriceHome - Convert.ToDecimal(AmountTotalDeprTextBox.Text);
                    this.CurrentAmountTextBox.Text = (_currentAmount == 0 ? "0" : _currentAmount.ToString("#,###.##"));
                }
                else
                {
                    this.ForexRateTextBox.Text = "1";
                    decimal _buyPriceHome = 1 * Convert.ToDecimal(this.BuyPriceForexTextBox.Text);
                    this.BuyPriceHomeTextBox.Text = (_buyPriceHome == 0 ? "0" : _buyPriceHome.ToString("#,###.##"));
                    this.LifeInMonthsBeginDeprTextBox.Text = "0";
                    this.LifeInMonthsProcessDeprTextBox.Text = "0";
                    this.LifeInMonthsTotalDeprTextBox.Text = "0";
                    this.AmountBeginDeprTextBox.Text = "0";
                    this.AmountProcessDeprTextBox.Text = "0";
                    this.AmountTotalDeprTextBox.Text = "0";
                    decimal _currentAmount = _buyPriceHome - Convert.ToDecimal(AmountTotalDeprTextBox.Text);
                    this.CurrentAmountTextBox.Text = (_currentAmount == 0 ? "0" : _currentAmount.ToString("#,###.##"));
                }
            }
            else
            {
                this.ForexRateTextBox.Text = "1";
                decimal _buyPriceHome = 1 * Convert.ToDecimal(this.BuyPriceForexTextBox.Text);
                this.BuyPriceHomeTextBox.Text = (_buyPriceHome == 0 ? "0" : _buyPriceHome.ToString("#,###.##"));
                this.LifeInMonthsBeginDeprTextBox.Text = "0";
                this.LifeInMonthsProcessDeprTextBox.Text = "0";
                this.LifeInMonthsTotalDeprTextBox.Text = "0";
                this.AmountBeginDeprTextBox.Text = "0";
                this.AmountProcessDeprTextBox.Text = "0";
                this.AmountTotalDeprTextBox.Text = "0";
                decimal _currentAmount = _buyPriceHome - Convert.ToDecimal(AmountTotalDeprTextBox.Text);
                this.CurrentAmountTextBox.Text = (_currentAmount == 0 ? "0" : _currentAmount.ToString("#,###.##"));
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        //protected void CreateJournalButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    string[] _date = this.DateTextBox.Text.Split('-');

        //    this.ClearLabel();

        //    if (this.CreateJournalHiddenField.Value.Trim().ToLower() == FixedAssetsDataMapper.CreateJournal(YesNo.No).ToString().ToLower())
        //    {
        //        string _result = this._fixedAssetBL.CreateJournal(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

        //        if (_result == "")
        //        {
        //            this.WarningLabel.Text = "You Success Create Journal";
        //        }
        //        else
        //        {
        //            this.WarningLabel.Text = _result;
        //        }
        //    }
        //    else if (this.CreateJournalHiddenField.Value.Trim().ToLower() == FixedAssetsDataMapper.CreateJournal(YesNo.Yes).ToString().ToLower())
        //    {
        //        string _result = this._fixedAssetBL.DeleteJournal(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

        //        if (_result == "")
        //        {
        //            this.WarningLabel.Text = "You Success Delete Journal";
        //        }
        //        else
        //        {
        //            this.WarningLabel.Text = _result;
        //        }
        //    }

        //    this.ShowData();
        //}
    }
}