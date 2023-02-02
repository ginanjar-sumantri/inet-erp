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
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAPurchase
{
    public partial class FAPurchaseDetailAdd : FAPurchaseBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowFAStatus();
                this.ShowFASubGroup();

                this.ClearLabel();
                this.ClearData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttribute()
        {
            CompanyConfiguration _compConfig = new CompanyConfig().GetSingle(CompanyConfigure.FACodeAutoNumber);

            if (_compConfig.SetValue == CompanyConfigureDataMapper.GetFACodeAutoNumber(FACodeAutoNumber.True))
            {
                this.EnableCodeCounter.Visible = true;
                this.FACodeRequiredFieldValidator.Enabled = false;
            }
            else
            {
                this.EnableCodeCounter.Visible = false;
                this.FACodeRequiredFieldValidator.Enabled = true;
            }

            this.LifeMonthTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
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

        public void ClearData()
        {
            this.WarningLabel.Text = "";

            this.FACodeTextBox.Text = "";
            this.FANameTextBox.Text = "";
            this.FAStatusDropDownList.SelectedValue = "null";
            this.FAOwnerCheckBox.Checked = false;
            this.FASubGroupDropDownList.SelectedValue = "null";
            this.FALocationTypeDropDownList.SelectedValue = "null";
            this.FALocationDropDownList.Items.Clear();
            this.FALocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.LifeMonthTextBox.Text = "";
            this.AmountForexTextBox.Text = "";
            this.StatusProcessCheckBox.Checked = false;
            this.SpecificationTextBox.Text = "";
            this.DecimalPlaceHiddenField.Value = "";

            GLFAPurchaseHd _glFAPurchaseHd = _faPurchaseBL.GetSingleFAPurchaseHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = _currencyBL.GetDecimalPlace(_glFAPurchaseHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            int _maxItemNo = this._faPurchaseBL.GetMaxNoItemFAPurchaseDt(_transNo);
            GLFAPurchaseDt _glFAPurchaseDt = new GLFAPurchaseDt();

            _glFAPurchaseDt.TransNmbr = _transNo;
            _glFAPurchaseDt.ItemNo = _maxItemNo + 1;
            _glFAPurchaseDt.FACode = this.FACodeTextBox.Text;
            _glFAPurchaseDt.FAName = this.FANameTextBox.Text;
            _glFAPurchaseDt.FAStatus = this.FAStatusDropDownList.SelectedValue;
            _glFAPurchaseDt.FAOwner = FixedAssetsDataMapper.IsAllowAddValue(this.FAOwnerCheckBox.Checked);
            _glFAPurchaseDt.FASubGroup = this.FASubGroupDropDownList.SelectedValue;
            _glFAPurchaseDt.FALocationType = this.FALocationTypeDropDownList.SelectedValue;
            _glFAPurchaseDt.FALocationCode = this.FALocationDropDownList.SelectedValue;
            _glFAPurchaseDt.LifeMonth = Convert.ToInt32(this.LifeMonthTextBox.Text);
            _glFAPurchaseDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _glFAPurchaseDt.FgProcess = FixedAssetsDataMapper.IsAllowAddValue(this.StatusProcessCheckBox.Checked);
            _glFAPurchaseDt.Spesification = this.SpecificationTextBox.Text;

            bool _result = this._faPurchaseBL.AddFAPurchaseDt(_glFAPurchaseDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}