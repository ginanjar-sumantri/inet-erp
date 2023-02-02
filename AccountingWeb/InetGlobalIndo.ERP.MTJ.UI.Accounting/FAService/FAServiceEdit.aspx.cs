using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAService
{
    public partial class FAServiceEdit : FAServiceBase
    {
        private FAServiceBL _faServiceBL = new FAServiceBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
        private PermissionBL _permBL = new PermissionBL();

        private string _imgPPNDate = "ppn_date_start";

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
                this.TransDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.TransDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DueDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.DueDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNDateLiteral.Text = "<input id='button3' type='button' style='Visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowSupplierDropDownList();
                this.ShowTermDropDownList();
                this.ShowCurrencyDropDownList();
                this.ShowFixedAssetDropDownList();
                this.ShowPeriodDropDownList();
                this.ShowYearDropDownList();

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        private void ShowSupplierDropDownList()
        {
            this.SupplierDropDownList.DataSource = this._supplierBL.GetListDDLSupp();
            this.SupplierDropDownList.DataValueField = "SuppCode";
            this.SupplierDropDownList.DataTextField = "SuppName";
            this.SupplierDropDownList.DataBind();
            this.SupplierDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CurrDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrDropDownList.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrDropDownList.SelectedValue);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.ForexRateTextBox.Text = this.PPNRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (this.CurrDropDownList.SelectedValue == _currCodeHome)
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.ForexRateTextBox.Text = "1";
                    this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.PPNRateTextBox.Text = "1";
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
                this.CurrTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrTextBox.Text = CurrDropDownList.SelectedValue;
            }
            else
            {
                this.PPNRateTextBox.Text = "0";
                this.ForexRateTextBox.Text = "0";
                this.CurrDropDownList.SelectedValue = "null";
                this.CurrTextBox.Text = "";
            }
        }

        private void ShowCurrencyDropDownList()
        {
            this.CurrDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrDropDownList.DataValueField = "CurrCode";
            this.CurrDropDownList.DataTextField = "CurrCode";
            this.CurrDropDownList.DataBind();
            this.CurrDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SupplierDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SupplierDropDownList.SelectedValue != "null")
            {
                string _supplierContact = _supplierBL.GetSuppContact(this.SupplierDropDownList.SelectedValue);
                string _currSupplier = _supplierBL.GetCurr(this.SupplierDropDownList.SelectedValue);
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_currSupplier);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.ForexRateTextBox.Text = this.PPNRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.AttnTextBox.Text = _supplierContact;

                if (_currSupplier != "")
                {
                    this.CurrDropDownList.SelectedValue = _currSupplier;
                }

                if (this.CurrDropDownList.SelectedValue == _currCodeHome)
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.ForexRateTextBox.Text = "1";
                    this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.PPNRateTextBox.Text = "1";
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
                this.CurrTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.CurrTextBox.Text = CurrDropDownList.SelectedValue;
            }
            else
            {
                this.AttnTextBox.Text = "";
                this.PPNRateTextBox.Text = "0";
                this.ForexRateTextBox.Text = "0";
                this.CurrDropDownList.SelectedValue = "null";
                this.CurrTextBox.Text = "";
            }
        }

        private void ShowTermDropDownList()
        {
            this.TermDropDownList.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowFixedAssetDropDownList()
        {
            this.FixedAssetDropDownList.DataSource = this._fixedAssetBL.GetListDDLFixedAsset();
            this.FixedAssetDropDownList.DataValueField = "FACode";
            this.FixedAssetDropDownList.DataTextField = "FAName";
            this.FixedAssetDropDownList.DataBind();
            this.FixedAssetDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowPeriodDropDownList()
        {
            this.PeriodDropDownList.DataSource = this._fixedAssetBL.GetPeriod();
            this.PeriodDropDownList.DataValueField = "MonthCode";
            this.PeriodDropDownList.DataTextField = "MonthName";
            this.PeriodDropDownList.DataBind();
            this.PeriodDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowYearDropDownList()
        {
            int _year = DateTime.Now.Year;
            for (int i = 5; i >= 0; i--)
            {
                this.YearDropDownList.Items.Add((_year - i).ToString());
            }

            for (int z = 1; z <= 5; z++)
            {
                this.YearDropDownList.Items.Add((_year + z).ToString());
            }

            this.YearDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.TransDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");

            this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.AddLifeTimeTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");

            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button3"/*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ShowData()
        {
            GLFAServiceHd _glFAServiceHd = this._faServiceBL.GetSingleFAServiceHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_glFAServiceHd.CurrCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.TransNoTextBox.Text = _glFAServiceHd.TransNmbr;
            this.FileNmbrTextBox.Text = _glFAServiceHd.FileNmbr;
            this.TransDateTextBox.Text = DateFormMapper.GetValue(_glFAServiceHd.TransDate);
            this.SupplierDropDownList.SelectedValue = _glFAServiceHd.SuppCode;
            this.AttnTextBox.Text = _glFAServiceHd.Attn;
            this.CurrDropDownList.SelectedValue = _glFAServiceHd.CurrCode;
            if (_glFAServiceHd.CurrCode == _currencyBL.GetCurrDefault())
            {
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
            this.ForexRateTextBox.Text = _glFAServiceHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNRateTextBox.Text = (_glFAServiceHd.PPNRate == 0) ? "0" : Convert.ToDecimal(_glFAServiceHd.PPNRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.TermDropDownList.SelectedValue = _glFAServiceHd.Term;

            if (_glFAServiceHd.DueDate != null)
            {
                this.DueDateTextBox.Text = DateFormMapper.GetValue(_glFAServiceHd.DueDate);
            }

            this.FixedAssetDropDownList.SelectedValue = _glFAServiceHd.FACode;
            this.PeriodDropDownList.SelectedValue = _glFAServiceHd.StartPeriod.ToString();
            this.YearDropDownList.SelectedValue = _glFAServiceHd.StartYear.ToString();
            this.AddLifeTimeTextBox.Text = _glFAServiceHd.AddLife.ToString();
            this.PPNPercentTextBox.Text = (_glFAServiceHd.PPN == 0) ? "0" : _glFAServiceHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            if (_glFAServiceHd.PPN <= 0)
            {
                this.PPNNoTextBox.Text = "";
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNDateTextBox.Text = "";
                this.PPNRateTextBox.Text = "0";
                this.PPNDateLiteral.Text = "<input id='button3' type='button' style='Visibility:hidden' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
            }
            else
            {
                this.PPNNoTextBox.Text = _glFAServiceHd.PPNNo;
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNDateLiteral.Text = "<input id='button3' type='button' style='Visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                if (_glFAServiceHd.PPNDate != null)
                {
                    this.PPNDateTextBox.Text = DateFormMapper.GetValue(_glFAServiceHd.PPNDate);
                }

                if (_glFAServiceHd.PPNRate != null)
                {
                    decimal _ppnRateValue = Convert.ToDecimal((_glFAServiceHd.PPNRate == null) ? 0 : _glFAServiceHd.PPNRate);
                    this.PPNRateTextBox.Text = (_ppnRateValue == 0) ? "0" : _ppnRateValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                }
            }
            this.CurrTextBox.Text = _glFAServiceHd.CurrCode;
            this.AmountBaseTextBox.Text = (_glFAServiceHd.BaseForex == 0) ? "0" : _glFAServiceHd.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_glFAServiceHd.PPNForex == 0) ? "0" : _glFAServiceHd.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_glFAServiceHd.TotalForex == 0) ? "0" : _glFAServiceHd.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _glFAServiceHd.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAServiceHd _glFAServiceHd = this._faServiceBL.GetSingleFAServiceHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _glFAServiceHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFAServiceHd.FACode = this.FixedAssetDropDownList.SelectedValue;
            _glFAServiceHd.SuppCode = this.SupplierDropDownList.SelectedValue;
            _glFAServiceHd.Attn = this.AttnTextBox.Text;
            _glFAServiceHd.Term = this.TermDropDownList.SelectedValue;

            if (this.DueDateTextBox.Text.Trim() != "")
            {
                _glFAServiceHd.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            }
            else
            {
                _glFAServiceHd.DueDate = null;
            }

            _glFAServiceHd.CurrCode = this.CurrDropDownList.SelectedValue;
            _glFAServiceHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _glFAServiceHd.PPNNo = this.PPNNoTextBox.Text;

            if (this.PPNDateTextBox.Text != "")
            {
                _glFAServiceHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _glFAServiceHd.PPNDate = null;
            }

            if (this.PPNRateTextBox.Text != "")
            {
                _glFAServiceHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            }

            _glFAServiceHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _glFAServiceHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _glFAServiceHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _glFAServiceHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _glFAServiceHd.StartYear = Convert.ToInt32(this.YearDropDownList.SelectedValue);
            _glFAServiceHd.StartPeriod = Convert.ToInt32(this.PeriodDropDownList.SelectedValue);
            _glFAServiceHd.AddLife = Convert.ToInt32(this.AddLifeTimeTextBox.Text);
            _glFAServiceHd.Remark = this.RemarkTextBox.Text;

            _glFAServiceHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFAServiceHd.DatePrep = DateTime.Now;

            bool _result = this._faServiceBL.EditFAServiceHd(_glFAServiceHd);

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
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAServiceHd _glFAServiceHd = this._faServiceBL.GetSingleFAServiceHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _glFAServiceHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFAServiceHd.FACode = this.FixedAssetDropDownList.SelectedValue;
            _glFAServiceHd.SuppCode = this.SupplierDropDownList.SelectedValue;
            _glFAServiceHd.Attn = this.AttnTextBox.Text;
            _glFAServiceHd.Term = this.TermDropDownList.SelectedValue;

            if (this.DueDateTextBox.Text.Trim() != "")
            {
                _glFAServiceHd.DueDate = DateFormMapper.GetValue(this.DueDateTextBox.Text);
            }
            else
            {
                _glFAServiceHd.DueDate = null;
            }

            _glFAServiceHd.CurrCode = this.CurrDropDownList.SelectedValue;
            _glFAServiceHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _glFAServiceHd.PPNNo = this.PPNNoTextBox.Text;

            if (this.PPNDateTextBox.Text != "")
            {
                _glFAServiceHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _glFAServiceHd.PPNDate = null;
            }

            if (this.PPNRateTextBox.Text != "")
            {
                _glFAServiceHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            }

            _glFAServiceHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _glFAServiceHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _glFAServiceHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _glFAServiceHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _glFAServiceHd.StartYear = Convert.ToInt32(this.YearDropDownList.SelectedValue);
            _glFAServiceHd.StartPeriod = Convert.ToInt32(this.PeriodDropDownList.SelectedValue);
            _glFAServiceHd.AddLife = Convert.ToInt32(this.AddLifeTimeTextBox.Text);
            _glFAServiceHd.Remark = this.RemarkTextBox.Text;

            _glFAServiceHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFAServiceHd.DatePrep = DateTime.Now;

            bool _result = this._faServiceBL.EditFAServiceHd(_glFAServiceHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }
    }
}
