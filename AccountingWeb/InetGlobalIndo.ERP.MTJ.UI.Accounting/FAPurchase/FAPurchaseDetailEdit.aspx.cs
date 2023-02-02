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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAPurchase
{
    public partial class FAPurchaseDetailEdit : FAPurchaseBase
    {
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
        private FAPurchaseBL _faPurchaseBL = new FAPurchaseBL();
        private PermissionBL _permBL = new PermissionBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowFAStatus();
                this.ShowFASubGroup();

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
            this.FANameTextBox.Attributes.Add("ReadOnly", "True");
            this.LifeMonthTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.AmountForexTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.AmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.QtyTextBox.Attributes.Add("OnBlur", "Count(" + this.PriceForexTextBox.ClientID + "," + this.QtyTextBox.ClientID + "," + this.AmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ")");
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
            GLFAPurchaseDt _glFAPurchaseDt = this._faPurchaseBL.GetSingleFAPurchaseDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey));
            GLFAPurchaseHd _glFAPurchaseHd = _faPurchaseBL.GetSingleFAPurchaseHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_glFAPurchaseHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            //this.FACodeTextBox.Text = _glFAPurchaseDt.FACode;
            this.FANameTextBox.Text = _glFAPurchaseDt.FAName;
            this.FAStatusDropDownList.SelectedValue = (_glFAPurchaseDt.FAStatus == null) ? "null" : _glFAPurchaseDt.FAStatus.ToString();
            this.FAOwnerCheckBox.Checked = FixedAssetsDataMapper.IsFGAddValue(_glFAPurchaseDt.FAOwner);
            this.FASubGroupDropDownList.SelectedValue = (_glFAPurchaseDt.FASubGroup == null) ? "null" : _glFAPurchaseDt.FASubGroup;
            this.FALocationTypeDropDownList.SelectedValue = (_glFAPurchaseDt.FALocationType == null) ? "null" : _glFAPurchaseDt.FALocationType;
            this.ShowFALocation();
            this.FALocationDropDownList.SelectedValue = (_glFAPurchaseDt.FALocationCode == null) ? "null" : _glFAPurchaseDt.FALocationCode;
            this.LifeMonthTextBox.Text = (_glFAPurchaseDt.LifeMonth == 0) ? "0" : _glFAPurchaseDt.LifeMonth.ToString();
            this.QtyTextBox.Text = (_glFAPurchaseDt.Qty == null) ? "0" : Convert.ToDecimal(_glFAPurchaseDt.Qty).ToString("#,##0.##");
            this.PriceForexTextBox.Text = (_glFAPurchaseDt.PriceForex == null) ? "0" : Convert.ToDecimal(_glFAPurchaseDt.PriceForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = (_glFAPurchaseDt.AmountForex == null) ? "0" : Convert.ToDecimal(_glFAPurchaseDt.AmountForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.StatusProcessCheckBox.Checked = FixedAssetsDataMapper.IsFGAddValue(_glFAPurchaseDt.FgProcess);
            this.SpecificationTextBox.Text = _glFAPurchaseDt.Spesification;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAPurchaseDt _glFAPurchaseDt = this._faPurchaseBL.GetSingleFAPurchaseDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey));

            //_glFAPurchaseDt.FACode = this.FACodeTextBox.Text;
            _glFAPurchaseDt.FAName = this.FANameTextBox.Text;
            _glFAPurchaseDt.FAStatus = this.FAStatusDropDownList.SelectedValue;
            _glFAPurchaseDt.FAOwner = FixedAssetsDataMapper.IsAllowAddValue(this.FAOwnerCheckBox.Checked);
            _glFAPurchaseDt.FASubGroup = this.FASubGroupDropDownList.SelectedValue;
            _glFAPurchaseDt.FALocationType = this.FALocationTypeDropDownList.SelectedValue;
            _glFAPurchaseDt.FALocationCode = this.FALocationDropDownList.SelectedValue;
            _glFAPurchaseDt.LifeMonth = Convert.ToInt32(this.LifeMonthTextBox.Text);
            _glFAPurchaseDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            decimal _amountForexOriginal = Convert.ToDecimal(_glFAPurchaseDt.AmountForex);
            _glFAPurchaseDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _glFAPurchaseDt.FgProcess = FixedAssetsDataMapper.IsAllowAddValue(this.StatusProcessCheckBox.Checked);
            _glFAPurchaseDt.Spesification = this.SpecificationTextBox.Text;

            bool _result = this._faPurchaseBL.EditFAPurchaseDt(_glFAPurchaseDt, _amountForexOriginal);

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
    }
}