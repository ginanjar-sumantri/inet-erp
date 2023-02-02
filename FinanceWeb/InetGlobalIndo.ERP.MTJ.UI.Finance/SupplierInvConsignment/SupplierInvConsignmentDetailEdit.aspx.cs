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

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierInvConsignment
{
    public partial class SupplierInvConsignmentDetailEdit : SupplierInvConsignmentBase
    {
        //private NotaCreditSupplierBL _notaCreditSupplierBL = new NotaCreditSupplierBL();
        private SupplierInvConsignmentBL _suppInvConsignBL = new SupplierInvConsignmentBL();
        private AccountBL _accountBL = new AccountBL();
        private UnitBL _unitBL = new UnitBL();
        private SubledBL _subledBL = new SubledBL();
        private EmployeeBL _empBL = new EmployeeBL();
        private User_EmployeeBL _userEmpBL = new User_EmployeeBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private ProductBL _productBL = new ProductBL();

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

                //this.ShowAccount();
                this.ShowUnit();
                //this.GetSubled();

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
            FINSuppInvConsignmentDt _finSuppInvConsignDt = this._suppInvConsignBL.GetSingleSuppInvConsignDt(_tempSplit[0], _tempSplit[1], _tempSplit[2], _tempSplit[3]);

            string _currHeader = this._suppInvConsignBL.GetSingleSuppInvConsignHd(_tempSplit[0]).CurrCode;
            byte _decimalPlace = _currencyBL.GetDecimalPlace(_currHeader);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            //this.AccountTextBox.Text = _finSuppInvConsignDt.Account;
            //this.AccountDropDownList.SelectedValue = (_finSuppInvConsignDt.Account == "" ? "null" : _finSuppInvConsignDt.Account);
            //this.FgSubledHiddenField.Value = _finSuppInvConsignDt.FgSubLed.ToString();
            //this.SubledDropDownList.SelectedValue = (_finSuppInvConsignDt.FgSubLed.ToString() == "" ? "null" : _finSuppInvConsignDt.FgSubLed.ToString());
            this.TransNmbrHiddenField.Value = _tempSplit[0];
            this.SJTypeTextBox.Text = _finSuppInvConsignDt.SJType;
            this.SJNoTextBox.Text = _finSuppInvConsignDt.SJNo;
            this.ProductCodeTextBox.Text = _finSuppInvConsignDt.ProductCode;
            this.ProductNameTextBox.Text = this._productBL.GetProductNameByCode(_finSuppInvConsignDt.ProductCode);
            this.RemarkTextBox.Text = _finSuppInvConsignDt.Remark;
            this.PriceTextBox.Text = Convert.ToDecimal(_finSuppInvConsignDt.PriceForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountTextBox.Text = Convert.ToDecimal(_finSuppInvConsignDt.AmountForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.QtyTextBox.Text = _finSuppInvConsignDt.Qty.ToString("#,###.##");
            this.UnitDropDownList.SelectedValue = (_finSuppInvConsignDt.Unit == "" ? "null" : _finSuppInvConsignDt.Unit);

        }

        //public void ShowAccount()
        //{
        //    String _all = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._allCode), ApplicationConfig.EncryptionKey);
        //    string[] _tempSplit = _all.Split('|');
        //    string _currHeader = this._suppInvConsignBL.GetSingleSuppInvConsignHd(_tempSplit[0]).CurrCode;

        //    this.AccountDropDownList.Items.Clear();
        //    this.AccountDropDownList.DataTextField = "AccountName";
        //    this.AccountDropDownList.DataValueField = "Account";
        //    this.AccountDropDownList.DataSource = this._accountBL.GetListForDDL(_currHeader);
        //    this.AccountDropDownList.DataBind();
        //    this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //public void SubLed()
        //{
        //    this.UnitDropDownList.Items.Clear();
        //    this.UnitDropDownList.DataTextField = "UnitName";
        //    this.UnitDropDownList.DataValueField = "UnitCode";
        //    this.UnitDropDownList.DataSource = this._unitBL.GetListForDDL();
        //    this.UnitDropDownList.DataBind();
        //    this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void ShowUnit()
        {
            this.UnitDropDownList.Items.Clear();
            this.UnitDropDownList.DataTextField = "UnitName";
            this.UnitDropDownList.DataValueField = "UnitCode";
            this.UnitDropDownList.DataSource = this._unitBL.GetListForDDL();
            this.UnitDropDownList.DataBind();
            this.UnitDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //public void GetSubled()
        //{
        //    this.SubledDropDownList.Items.Clear();
        //    this.SubledDropDownList.DataTextField = "SubledName";
        //    this.SubledDropDownList.DataValueField = "SubledCode";
        //    this.SubledDropDownList.DataSource = this._subledBL.GetList();
        //    this.SubledDropDownList.DataBind();
        //    this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //protected void AccountDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.AccountDropDownList.SelectedValue != "null")
        //    {
        //        this.AccountTextBox.Text = this.AccountDropDownList.SelectedValue;
        //        Char _fgSubLed = _accountBL.GetSingleAccount(this.AccountDropDownList.SelectedValue).FgSubLed;
        //        this.SubledDropDownList.SelectedValue = _fgSubLed.ToString();
        //    }
        //    else
        //    {
        //        this.AccountTextBox.Text = "";
        //        this.SubledDropDownList.SelectedValue = "null";
        //    }

        //    //this.GetSubled();
        //}

        //protected void AccountTextBox_TextChanged(object sender, EventArgs e)
        //{
        //    bool _exist = this._accountBL.IsExist(this.AccountTextBox.Text);
        //    if (_exist == true)
        //    {
        //        this.AccountDropDownList.SelectedValue = this.AccountTextBox.Text;
        //        Char _fgSubLed = _accountBL.GetSingleAccount(this.AccountDropDownList.SelectedValue).FgSubLed;
        //        this.SubledDropDownList.SelectedValue = _fgSubLed.ToString();
        //    }
        //    else
        //    {
        //        this.AccountTextBox.Text = "";
        //        this.AccountDropDownList.SelectedValue = "null";
        //        this.SubledDropDownList.SelectedValue = "null";
        //    }

        //    //this.GetSubled();
        //}

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINSuppInvConsignmentDt _finSuppInvConsignDt = this._suppInvConsignBL.GetSingleSuppInvConsignDt(this.TransNmbrHiddenField.Value, this.SJTypeTextBox.Text, this.SJNoTextBox.Text, this.ProductCodeTextBox.Text);

            //if (this.SubledDropDownList.SelectedValue != "null")
            //{
            //    _finSuppInvConsignDt.FgSubLed = Convert.ToChar(this.SubledDropDownList.SelectedValue);
            //    //_finSuppInvConsignDt.Subled = this.SubledDropDownList.SelectedValue;
            //}
            //else
            //{
                _finSuppInvConsignDt.FgSubLed = 'N';
                //_finSuppInvConsignDt.Subled = null;
            //}
            //_finSuppInvConsignDt.Account = this.AccountTextBox.Text;
            _finSuppInvConsignDt.Remark = this.RemarkTextBox.Text;
            _finSuppInvConsignDt.PriceForex = Convert.ToDecimal(this.PriceTextBox.Text);
            //_finSuppInvConsignDt.AmountForex = Convert.ToDecimal(this.AmountTextBox.Text);
            _finSuppInvConsignDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _finSuppInvConsignDt.Unit = this.UnitDropDownList.SelectedValue;

            bool _result = this._suppInvConsignBL.EditSuppInvConsignDt(_finSuppInvConsignDt, Convert.ToDecimal(this.AmountTextBox.Text));

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