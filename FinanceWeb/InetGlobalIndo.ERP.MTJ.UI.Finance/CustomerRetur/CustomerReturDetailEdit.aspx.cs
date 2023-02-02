using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.UI.Finance.NotaCreditCustomer;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerRetur
{
    public partial class CustomerReturDetailEdit : CustomerReturBase
    {
        private ProductBL _productBL = new ProductBL();
        private CustomerReturBL _customerReturBL = new CustomerReturBL();
        private AccountBL _accountBL = new AccountBL();
        private UnitBL _unitBL = new UnitBL();
        private SubledBL _subledBL = new SubledBL();
        private EmployeeBL _empBL = new EmployeeBL();
        private User_EmployeeBL _userEmpBL = new User_EmployeeBL();
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

                this.ShowUnit();

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PriceTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.QtyTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            String _all = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._allCode), ApplicationConfig.EncryptionKey);
            string[] _tempSplit = _all.Split('|');
            FINCustReturDt _finCustReturDt = this._customerReturBL.GetSingleFINCustReturDt(_tempSplit[0], _tempSplit[1], _tempSplit[2]);

            string _currHeader = this._customerReturBL.GetSingleFINCustReturHd(_tempSplit[0]).CurrCode;
            byte _decimalPlace = _currencyBL.GetDecimalPlace(_currHeader);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            //this.AccountTextBox.Text = _finSuppInvConsignDt.Account;
            //this.AccountDropDownList.SelectedValue = (_finCustReturDt.Account == "" ? "null" : _finCustReturDt.Account);
            //this.FgSubledHiddenField.Value = _finCustReturDt.FgSubLed.ToString();
            //this.SubledDropDownList.SelectedValue = (_finCustReturDt.FgSubLed.ToString() == "" ? "null" : _finCustReturDt.FgSubLed.ToString());
            this.TransNmbrHiddenField.Value = _tempSplit[0];
            //this.SJTypeTextBox.Text = _finSuppInvConsignDt.SJType;
            this.RRNoTextBox.Text = _finCustReturDt.RRNo;
            this.ProductCodeTextBox.Text = _finCustReturDt.ProductCode;
            this.ProductNameTextBox.Text = this._productBL.GetProductNameByCode(_finCustReturDt.ProductCode);
            this.RemarkTextBox.Text = _finCustReturDt.Remark;
            this.PriceTextBox.Text = Convert.ToDecimal(_finCustReturDt.PriceForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountTextBox.Text = Convert.ToDecimal(_finCustReturDt.AmountForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.QtyTextBox.Text = _finCustReturDt.Qty.ToString("#,###.##");
            this.UnitDropDownList.SelectedValue = (_finCustReturDt.Unit == "" ? "null" : _finCustReturDt.Unit);

        }

        public void ShowUnit()
        {
            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.DataTextField = "UnitName";
            this.UnitDropDownList.DataValueField = "UnitCode";
            this.UnitDropDownList.DataSource = this._unitBL.GetListForDDL();
            this.UnitDropDownList.DataBind();
            this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINCustReturDt _finSuppInvConsignDt = this._customerReturBL.GetSingleFINCustReturDt(this.TransNmbrHiddenField.Value, this.RRNoTextBox.Text, this.ProductCodeTextBox.Text);

            _finSuppInvConsignDt.Remark = this.RemarkTextBox.Text;
            _finSuppInvConsignDt.PriceForex = Convert.ToDecimal(this.PriceTextBox.Text);
            //_finSuppInvConsignDt.AmountForex = Convert.ToDecimal(this.AmountTextBox.Text);
            _finSuppInvConsignDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _finSuppInvConsignDt.Unit = this.UnitDropDownList.SelectedValue;

            bool _result = this._customerReturBL.EditFINCustReturDt(_finSuppInvConsignDt, Convert.ToDecimal(this.AmountTextBox.Text));

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNmbrHiddenField.Value, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNmbrHiddenField.Value, ApplicationConfig.EncryptionKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}