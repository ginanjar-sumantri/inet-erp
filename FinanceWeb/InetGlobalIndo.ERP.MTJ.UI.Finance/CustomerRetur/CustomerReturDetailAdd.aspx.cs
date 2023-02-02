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
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerRetur
{
    public partial class CustomerReturDetailAdd : CustomerReturBase
    {
        //private NotaCreditSupplierBL _notaCreditSupplierBL = new NotaCreditSupplierBL();
        //private AccountBL _accountBL = new AccountBL();
        //private UnitBL _unitBL = new UnitBL();
        //private SubledBL _subledBL = new SubledBL();
        //private EmployeeBL _empBL = new EmployeeBL();
        //private User_EmployeeBL _userEmpBL = new User_EmployeeBL();
        private PermissionBL _permBL = new PermissionBL();
        //private CurrencyBL _currencyBL = new CurrencyBL();
        private CustomerReturBL _customerReturBL = new CustomerReturBL();

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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.TransNmbrHiddenField.Value = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                
                this.ClearData();
                this.SetAttribute();
            }
        }

        protected void SetAttribute()
        {
            //this.AmountTextBox.Attributes.Add("ReadOnly", "True");
            //this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            //this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            //this.QtyTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            //this.PriceTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            //this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();
            //this.AccountTextBox.Text = "";
            //this.AccountDropDownList.SelectedValue = "null";
            //this.FgSubledHiddenField.Value = "";
            //this.SJTypeDropDownList.Items.Clear();
            //this.SJTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.RRNoDropDownList.Items.Clear();
            this.RRNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.RRNoDDL();
            //this.RemarkTextBox.Text = "";
            //this.PriceTextBox.Text = "0";
            //this.AmountTextBox.Text = "0";
            //this.QtyTextBox.Text = "1";
            //this.UnitDropDownList.SelectedValue = "null";
            //this.DecimalPlaceHiddenField.Value = "";

            //string _currHeader = this._notaCreditSupplierBL.GetSingleFINCNSuppHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).CurrCode;
            //byte _decimalPlace = _currencyBL.GetDecimalPlace(_currHeader);
            //this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }

        public void RRNoDDL()
        {
            this.RRNoDropDownList.Items.Clear();
            this.RRNoDropDownList.DataTextField = "TransNmbr";
            this.RRNoDropDownList.DataValueField = "TransNmbr";
            this.RRNoDropDownList.DataSource = this._customerReturBL.GetListRRnoForDDL(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._custCode), ApplicationConfig.EncryptionKey));
            this.RRNoDropDownList.DataBind();
            this.RRNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //public void SJTypeDDL()
        //{
        //    this.SJTypeDropDownList.Items.Clear();
        //    this.SJTypeDropDownList.DataTextField = "SJ_Type";
        //    this.SJTypeDropDownList.DataValueField = "SJ_Type";
        //    this.SJTypeDropDownList.DataSource = this._suppInvConBL.GetDDLforSJType(this.SuppCodeHiddenField.Value);
        //    this.SJTypeDropDownList.DataBind();
        //    this.SJTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //protected void SJTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    if (this.SJTypeDropDownList.SelectedValue != "null")
        //    {
        //        this.SJNoDDL();
        //    }
        //    else
        //    {
        //        this.SJNoDropDownList.Items.Clear();
        //        this.SJNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //    }

        //    //this.GetSubled();
        //}

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            String _retun = _customerReturBL.SaveFINCustReturDt(this.RRNoDropDownList.SelectedValue,this.TransNmbrHiddenField.Value);

            if (_retun == "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNmbrHiddenField.Value, ApplicationConfig.EncryptionKey)));
                //this.WarningLabel.Text = "Save Success";
            }
            else
            {
                this.WarningLabel.Text = _retun;
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNmbrHiddenField.Value, ApplicationConfig.EncryptionKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}