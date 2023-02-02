using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.BudgetEntry
{
    public partial class GLBudgetDetailAdd : GLBudgetBase
    {
        private GLBudgetBL _glBudgetBL = new GLBudgetBL();
        private AccountBL _accountBL = new AccountBL();
        private EmployeeBL _empBL = new EmployeeBL();
        private User_EmployeeBL _userEmpBL = new User_EmployeeBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();

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

                this.ShowAccount();

                this.ClearLabel();
                this.ClearData();
                this.SetAttribute();
            }
        }

        public void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.AmountBudgetRateTextBox.Attributes.Add("OnBlur", "Count(" + this.AmountBudgetRateTextBox.ClientID + "," + this.AmountBudgetForexTextBox.ClientID + "," + this.AmountBudgetHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHomeHiddenField.ClientID + " );");
            this.AmountBudgetForexTextBox.Attributes.Add("OnBlur", "Count(" + this.AmountBudgetRateTextBox.ClientID + "," + this.AmountBudgetForexTextBox.ClientID + "," + this.AmountBudgetHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHomeHiddenField.ClientID + " );");
            //this.AccountDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDropDownList.ClientID + "," + this.AccountTextBox.ClientID + ");");
            //this.AccountTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDropDownList.ClientID + "," + this.AccountTextBox.ClientID + ");");
        }

        public void SetAttribute()
        {
            this.AmountBudgetForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.AmountBudgetRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.AmountActualTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBudgetHomeTextBox.Attributes.Add("ReadOnly", "True");

            this.SetAttributeRate();
        }

        //private void ShowAmountPrev(string _prmAccount)
        //{
        //    decimal _result = 0;
        //    int _perInt;
        //    int _yearInt;
        //    //string _year = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeYear), ApplicationConfig.EncryptionKey);
        //    //string _period = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codePeriod), ApplicationConfig.EncryptionKey);

        //    if (Convert.ToInt32(_period) == 1)
        //    {
        //        _perInt = 12;
        //        _yearInt = Convert.ToInt32(_year) - 1;
        //    }
        //    else
        //    {
        //        _perInt = Convert.ToInt32(_period) - 1;
        //        _yearInt = Convert.ToInt32(_year);
        //    }

        //    _result = _jurnalBL.GetAmountByPeriodAndAccount(_yearInt, _perInt, _prmAccount);

        //    if (_result != null)
        //    {
        //        if (this.AmountPrevTextBox.Text != null)
        //        {
        //            this.AmountPrevTextBox.Text = Convert.ToDecimal(_result) == 0 ? "0" : Convert.ToDecimal(_result).ToString("#,###.##");
        //        }
        //        else
        //        {
        //            this.AmountPrevTextBox.Text = "";
        //        }
        //    }
        //    else
        //    {
        //        this.AmountPrevTextBox.Text = "";
        //    }
        //}

        public void ClearData()
        {
            this.AccountTextBox.Text = "";
            this.AccountDropDownList.SelectedValue = "null";
            this.AmountBudgetRateTextBox.Text = "0";
            this.AmountBudgetHomeTextBox.Text = "0";
            this.AmountBudgetForexTextBox.Text = "0";
            this.AmountActualTextBox.Text = "0";
            this.DecimalPlaceHomeHiddenField.Value = "";
            this.DecimalPlaceHiddenField.Value = "";
        }

        public void ShowAccount()
        {
            this.AccountDropDownList.Items.Clear();
            this.AccountDropDownList.DataTextField = "AccountName";
            this.AccountDropDownList.DataValueField = "Account";
            this.AccountDropDownList.DataSource = this._accountBL.GetListAccForBudgetForDDL(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));
            this.AccountDropDownList.DataBind();
            this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLBudgetAcc _glBudgetAcc = new GLBudgetAcc();

            _glBudgetAcc.BudgetDetailCode = Guid.NewGuid();
            _glBudgetAcc.BudgetCode = new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            _glBudgetAcc.Account = this.AccountTextBox.Text;
            _glBudgetAcc.AmountBudgetRate = Convert.ToDecimal(this.AmountBudgetRateTextBox.Text);
            _glBudgetAcc.AmountBudgetForex = Convert.ToDecimal(this.AmountBudgetForexTextBox.Text);
            _glBudgetAcc.AmountBudgetHome = Convert.ToDecimal(this.AmountBudgetHomeTextBox.Text);
            _glBudgetAcc.AmountActual = Convert.ToDecimal(this.AmountActualTextBox.Text);
            _glBudgetAcc.InsertBy = HttpContext.Current.User.Identity.Name;
            _glBudgetAcc.InsertDate = DateTime.Now;
            _glBudgetAcc.EditBy = HttpContext.Current.User.Identity.Name;
            _glBudgetAcc.EditDate = DateTime.Now;

            bool _result = this._glBudgetBL.AddGLBudgetAcc(_glBudgetAcc);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

        protected void AccountDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DecimalPlaceHiddenField.Value = "";

            if (this.AccountDropDownList.SelectedValue != "null")
            {
                string _currCode = _accountBL.GetCurrByAccCode(this.AccountDropDownList.SelectedValue);

                this.AccountTextBox.Text = this.AccountDropDownList.SelectedValue;

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(_currencyBL.GetCurrDefault());
                this.DecimalPlaceHomeHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlaceHome);

                if (_currCode == _currencyBL.GetCurrDefault())
                {
                    this.AmountBudgetRateTextBox.Text = "1";
                    this.AmountBudgetRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.AmountBudgetRateTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
                }
                else
                {
                    this.AmountBudgetRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(_currCode).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    this.AmountBudgetRateTextBox.Attributes.Remove("ReadOnly");
                    this.AmountBudgetRateTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
                }
            }
            else
            {
                this.AccountTextBox.Text = "";
                this.AmountBudgetRateTextBox.Text = "0";
            }
        }

        protected void AccountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.AccountTextBox.Text != "")
            {
                this.AccountDropDownList.SelectedValue = this.AccountTextBox.Text;

                string _currCode = _accountBL.GetCurrByAccCode(this.AccountDropDownList.SelectedValue);

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(_currencyBL.GetCurrDefault());
                this.DecimalPlaceHomeHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlaceHome);

                if (_currCode == _currencyBL.GetCurrDefault())
                {
                    this.AmountBudgetRateTextBox.Text = "1";
                    this.AmountBudgetRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.AmountBudgetRateTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
                }
                else
                {
                    this.AmountBudgetRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(_currCode).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    this.AmountBudgetRateTextBox.Attributes.Remove("ReadOnly");
                    this.AmountBudgetRateTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
                }
            }
            else
            {
                this.AccountDropDownList.SelectedValue = "null";
                this.AmountBudgetRateTextBox.Text = "0";
            }
        }
    }
}