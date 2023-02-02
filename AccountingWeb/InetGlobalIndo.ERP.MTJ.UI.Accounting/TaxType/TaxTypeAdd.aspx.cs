using System;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.UI.Accounting.Currency;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.TaxType
{
    public partial class TaxTypeAdd : TaxTypeBase
    {
        private TaxTypeBL _taxTypeBL = new TaxTypeBL();
        private AccountBL _accountBL = new AccountBL();
        private PermissionBL _permBL = new PermissionBL();

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

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.AccountDDL();
                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void AccountDDL()
        {
            this.AccountDropDownList.Items.Clear();
            this.AccountDropDownList.DataSource = this._accountBL.GetListForDDL();
            this.AccountDropDownList.DataValueField = "Account";
            this.AccountDropDownList.DataTextField = "AccountName";
            this.AccountDropDownList.DataBind();
            this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SetAttribute()
        {
            this.ValueTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            //this.DecimalPlaceReportTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();

            this.TaxTypeCodeTextBox.Text = "";
            this.TaxTypeNameTextBox.Text = "";
            this.ValueTextBox.Text = "0";
            //this.DecimalPlaceReportTextBox.Text = "0";
            this.AccountDropDownList.SelectedValue = null;
            this.fgActiveCheckBox.Checked = false;
        }

        //private string IsValidDecimalPlace()
        //{
        //    string _result = "";

        //    byte _decimalPlace = Convert.ToByte(this.DecimalPlaceTextBox.Text);
        //    //byte _decimalPlaceReport = Convert.ToByte(this.DecimalPlaceReportTextBox.Text);

        //    if (_decimalPlace < 0 || _decimalPlace > 8)
        //        return _result = "Decimal Place input range 0-8";

        //    //if (_decimalPlaceReport < 0 || _decimalPlaceReport > 8)
        //    //    return _result = "Decimal Place Report input range 0-8";

        //    //if (_decimalPlaceReport > _decimalPlace)
        //    //    return _result = "Decimal Place Report must less than Decimal Place";

        //    return _result;
        //}

        protected void SaveButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            //string _warning = this.IsValidDecimalPlace();
            //if (_warning == "")
            //{
            Int32 _countTax = _taxTypeBL.CountTaxType(this.TaxTypeCodeTextBox.Text);

            if (_countTax == 0)
            {
                MsTaxType _msTaxType = new MsTaxType();

                _msTaxType.TaxTypeCode = this.TaxTypeCodeTextBox.Text;
                _msTaxType.TaxTypeName = this.TaxTypeNameTextBox.Text;
                _msTaxType.DefaultValue = Convert.ToDecimal(this.ValueTextBox.Text);
                _msTaxType.Account = this.AccountDropDownList.SelectedValue;
                _msTaxType.fgActive = this.fgActiveCheckBox.Checked;
                _msTaxType.CreatedBy = HttpContext.Current.User.Identity.Name;
                _msTaxType.CreatedDate = DateTime.Now;
                _msTaxType.EditBy = HttpContext.Current.User.Identity.Name;
                _msTaxType.EditDate = DateTime.Now;

                bool _result = this._taxTypeBL.Add(_msTaxType);

                if (_result == true)
                {
                    Response.Redirect(this._homePage);
                }
                else
                {
                    this.WarningLabel.Text = "You Failed Add Currency";
                }
                //}
                //else
                //{
                //    this.WarningLabel.Text = _warning;
                //}
            }
            else
            { this.WarningLabel.Text = "Code Already Use"; }
        }

        protected void CancelButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}