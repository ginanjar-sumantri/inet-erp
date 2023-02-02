using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FATenancy
{
    public partial class FATenancyEdit : FATenancyBase
    {
        private FATenancyBL _faTenancyBL = new FATenancyBL();
        private TermBL _termBL = new TermBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CustomerBL _customerBL = new CustomerBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
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
                this.PPNDateLiteral.Text = "<input id='button2' type='button' style='visibility:hidden' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowTermDropdownlist();
                this.ShowCurrencyDropdownlist();
                this.ShowCustomerDropdownlist();

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
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrencyForexTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");

            this.CurrencyRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrencyRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.PPNTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.BaseForexTextBox.ClientID + "," + this.DiscountTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscountTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.BaseForexTextBox.ClientID + "," + this.DiscountTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            GLFATenancyHd _glFATenancyHd = this._faTenancyBL.GetSingleGLFATenancyHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_glFATenancyHd.CurrCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.TransNumberTextBox.Text = _glFATenancyHd.TransNmbr;
            this.FileNmbrTextBox.Text = _glFATenancyHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_glFATenancyHd.TransDate);
            this.CustCodeDropDownList.SelectedValue = _glFATenancyHd.CustCode;
            this.CurrencyForexTextBox.Text = _glFATenancyHd.CurrCode;
            this.CurrencyDropDownList.SelectedValue = _glFATenancyHd.CurrCode;
            this.CurrencyRateTextBox.Text = _glFATenancyHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TermDropDownList.SelectedValue = _glFATenancyHd.Term;
            this.AttnTextBox.Text = _glFATenancyHd.Attn;
            if (_glFATenancyHd.PPN == 0)
            {
                this.PPNTextBox.Text = "0";
                this.PPNDateLiteral.Text = "<input id='button2' type='button' style='visibility:hidden' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
            }
            else
            {
                if (_glFATenancyHd.PPNDate != null)
                {
                    this.PPNDateTextBox.Text = DateFormMapper.GetValue(_glFATenancyHd.PPNDate);
                    this.PPNDateLiteral.Text = "<input id='button2' type='button' style='visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                }
                this.PPNTextBox.Text = (_glFATenancyHd.PPN == 0) ? "0" : _glFATenancyHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                this.PPNNoTextBox.Text = _glFATenancyHd.PPNNo;
            }

            if (_glFATenancyHd.PPNRate != null)
            {
                decimal _ppnRateValue = Convert.ToDecimal((_glFATenancyHd.PPNRate == null) ? 0 : _glFATenancyHd.PPNRate);
                this.PPNRateTextBox.Text = (_ppnRateValue == 0) ? "0" : _ppnRateValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            this.PPNForexTextBox.Text = (_glFATenancyHd.PPNForex == 0) ? "0" : _glFATenancyHd.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.BaseForexTextBox.Text = (_glFATenancyHd.BaseForex == 0) ? "0" : _glFATenancyHd.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DiscountTextBox.Text = (_glFATenancyHd.DiscForex == 0) ? "0" : _glFATenancyHd.DiscForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_glFATenancyHd.TotalForex == 0) ? "0" : _glFATenancyHd.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _glFATenancyHd.Remark;
        }

        private void ShowTermDropdownlist()
        {
            this.TermDropDownList.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCurrencyDropdownlist()
        {
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCustomerDropdownlist()
        {
            this.CustCodeDropDownList.DataSource = this._customerBL.GetListCustomerForDDL();
            this.CustCodeDropDownList.DataValueField = "CustCode";
            this.CustCodeDropDownList.DataTextField = "CustName";
            this.CustCodeDropDownList.DataBind();
            this.CustCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CustCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrencyDropDownList.SelectedValue == "null")
            {
                string _custContact = _customerBL.GetCustContact(this.CustCodeDropDownList.SelectedValue);
                string _currCust = _customerBL.GetCurr(this.CustCodeDropDownList.SelectedValue);

                this.AttnTextBox.Text = _custContact;
                if (_currCust == "")
                {
                    this.CurrencyDropDownList.SelectedValue = "null";
                }
                else
                {
                    this.CurrencyDropDownList.SelectedValue = _currCust;
                }

                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrencyDropDownList.SelectedValue);
                decimal _tempForexRate = _currencyRateBL.GetSingleLatestCurrRate(this.CurrencyDropDownList.SelectedValue);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.CurrencyRateTextBox.Text = this.PPNRateTextBox.Text = (_tempForexRate == 0) ? "0" : _tempForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (this.CurrencyDropDownList.SelectedValue == _currCodeHome)
                {
                    this.CurrencyRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrencyRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.CurrencyRateTextBox.Attributes.Remove("OnBlur");
                    this.CurrencyRateTextBox.Text = "1";

                    this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.PPNRateTextBox.Attributes.Remove("OnBlur");
                    this.PPNRateTextBox.Text = "1";
                }
                else
                {
                    this.CurrencyRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrencyRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.CurrencyRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrencyRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

                    this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
                }

                this.CurrencyForexTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrencyForexTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrencyForexTextBox.Text = CurrencyDropDownList.SelectedValue;
            }
            else
            {
                this.AttnTextBox.Text = "";
                this.CurrencyDropDownList.SelectedValue = "null";
                this.CurrencyForexTextBox.Text = "";

                this.CurrencyRateTextBox.Text = "0";
                this.CurrencyRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrencyRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");

                this.PPNRateTextBox.Text = "0";
                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");
            }
        }

        protected void CurrencyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrencyDropDownList.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrencyDropDownList.SelectedValue);
                decimal _tempForexRate = _currencyRateBL.GetSingleLatestCurrRate(this.CurrencyDropDownList.SelectedValue);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.CurrencyRateTextBox.Text = this.PPNRateTextBox.Text = (_tempForexRate == 0) ? "0" : _tempForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (this.CurrencyDropDownList.SelectedValue == _currCodeHome)
                {
                    this.CurrencyRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrencyRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.CurrencyRateTextBox.Attributes.Remove("OnBlur");
                    this.CurrencyRateTextBox.Text = "1";

                    this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.PPNRateTextBox.Attributes.Remove("OnBlur");
                    this.PPNRateTextBox.Text = "1";
                }
                else
                {
                    this.CurrencyRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrencyRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.CurrencyRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrencyRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

                    this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
                }

                this.CurrencyForexTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrencyForexTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrencyForexTextBox.Text = CurrencyDropDownList.SelectedValue;
            }
            else
            {
                this.CurrencyRateTextBox.Text = "0";
                this.CurrencyRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrencyRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");

                this.PPNRateTextBox.Text = "0";
                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("Style", "Background-Color:#CCCCCC");
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFATenancyHd _glFATenancyHd = this._faTenancyBL.GetSingleGLFATenancyHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _glFATenancyHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFATenancyHd.CustCode = this.CustCodeDropDownList.SelectedValue;
            _glFATenancyHd.Attn = this.AttnTextBox.Text;
            _glFATenancyHd.Term = this.TermDropDownList.SelectedValue;
            _glFATenancyHd.Remark = this.RemarkTextBox.Text;
            _glFATenancyHd.DiscForex = Convert.ToDecimal(this.DiscountTextBox.Text);
            _glFATenancyHd.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
            _glFATenancyHd.PPNNo = this.PPNNoTextBox.Text;
            _glFATenancyHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _glFATenancyHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);

            if (this.PPNDateTextBox.Text != "")
            {
                _glFATenancyHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _glFATenancyHd.PPNDate = null;
            }

            _glFATenancyHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFATenancyHd.DatePrep = DateTime.Now;

            bool _result = this._faTenancyBL.EditGLFATenancyHd(_glFATenancyHd);

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

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFATenancyHd _glFATenancyHd = this._faTenancyBL.GetSingleGLFATenancyHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _glFATenancyHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFATenancyHd.CustCode = this.CustCodeDropDownList.SelectedValue;
            _glFATenancyHd.Attn = this.AttnTextBox.Text;
            _glFATenancyHd.Term = this.TermDropDownList.SelectedValue;
            _glFATenancyHd.Remark = this.RemarkTextBox.Text;
            _glFATenancyHd.DiscForex = Convert.ToDecimal(this.DiscountTextBox.Text);
            _glFATenancyHd.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
            _glFATenancyHd.PPNNo = this.PPNNoTextBox.Text;
            _glFATenancyHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _glFATenancyHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);

            if (this.PPNDateTextBox.Text != "")
            {
                _glFATenancyHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _glFATenancyHd.PPNDate = null;
            }

            _glFATenancyHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFATenancyHd.DatePrep = DateTime.Now;

            bool _result = this._faTenancyBL.EditGLFATenancyHd(_glFATenancyHd);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }
    }
}