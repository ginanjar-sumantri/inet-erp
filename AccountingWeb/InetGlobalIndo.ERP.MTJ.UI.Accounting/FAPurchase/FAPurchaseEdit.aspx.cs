using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAPurchase
{
    public partial class FAPurchaseEdit : FAPurchaseBase
    {
        private CurrencyBL _currencyBL = new CurrencyBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private TermBL _termBL = new TermBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private FAPurchaseBL _faPurchaseBL = new FAPurchaseBL();
        private PermissionBL _permBL = new PermissionBL();
        private FixedAssetPurchaseOrderBL _faPOBL = new FixedAssetPurchaseOrderBL();

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
                this.PPNDateLiteral.Text = "<input id='button2' type='button' style ='visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                //this.ShowSupplierDropdownlist();
                this.ShowCurrencyDropdownlist();
                this.ShowTermDropdownlist();

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowTermDropdownlist()
        {
            this.TermDropDownList.Items.Clear();
            this.TermDropDownList.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //private void ShowSupplierDropdownlist()
        //{
        //    this.SupplierDropDownList.Items.Clear();
        //    this.SupplierDropDownList.DataSource = this._supplierBL.GetListDDLSupp();
        //    this.SupplierDropDownList.DataValueField = "SuppCode";
        //    this.SupplierDropDownList.DataTextField = "SuppName";
        //    this.SupplierDropDownList.DataBind();
        //    this.SupplierDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowCurrencyDropdownlist()
        {
            this.CurrencyDropDownList.Items.Clear();
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //private void ShowFAPO()
        //{
        //    this.FAPONoDropDownList.Items.Clear();
        //    this.FAPONoDropDownList.DataSource = this._faPOBL.GetListDDLFAPOBySupplier(this.SupplierDropDownList.SelectedValue);
        //    this.FAPONoDropDownList.DataValueField = "TransNmbr";
        //    this.FAPONoDropDownList.DataTextField = "FileNmbr";
        //    this.FAPONoDropDownList.DataBind();
        //    this.FAPONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void SetAttribute()
        {
            this.TransDateTextBox.Attributes.Add("ReadOnly", "True");
            this.FileNmbrTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");

            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID */+ "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" /*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            GLFAPurchaseHd _glFAPurchaseHd = this._faPurchaseBL.GetSingleFAPurchaseHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_glFAPurchaseHd.CurrCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.TransNoTextBox.Text = _glFAPurchaseHd.TransNmbr;
            this.FileNmbrTextBox.Text = _glFAPurchaseHd.FileNmbr;
            this.TransDateTextBox.Text = DateFormMapper.GetValue(_glFAPurchaseHd.TransDate);
            this.SupplierTextBox.Text = _glFAPurchaseHd.SuppCode;
            //this.SupplierDropDownList.SelectedValue = _glFAPurchaseHd.SuppCode;
            this.AttnTextBox.Text = _glFAPurchaseHd.Attn;
            this.SuppInvoiceNoTextBox.Text = _glFAPurchaseHd.SuppInvoice;
            //this.ShowFAPO();
            this.FAPONoTextBox.Text = _glFAPurchaseHd.FAPONo;
            //this.FAPONoDropDownList.SelectedValue = _glFAPurchaseHd.FAPONo;
            this.CurrencyDropDownList.SelectedValue = _glFAPurchaseHd.CurrCode;
            string _currCodeHome = _currencyBL.GetCurrDefault();
            if (this.CurrencyDropDownList.SelectedValue == _currCodeHome)
            {
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.ForexRateTextBox.Attributes.Remove("OnBlur");

                this.ForexRateTextBox.Text = "1";

                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.PPNRateTextBox.Attributes.Remove("OnBlur");

                this.PPNRateTextBox.Text = "1";
            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

                this.ForexRateTextBox.Text = _glFAPurchaseHd.Forexrate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

                this.PPNRateTextBox.Text = "1";

                this.PPNRateTextBox.Text = (_glFAPurchaseHd.PPNRate == null) ? "" : Convert.ToDecimal(_glFAPurchaseHd.PPNRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }

            this.TermDropDownList.SelectedValue = _glFAPurchaseHd.Term;

            this.PPNPercentTextBox.Text = (_glFAPurchaseHd.PPN == 0) ? "0" : _glFAPurchaseHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            if (_glFAPurchaseHd.PPN > 0)
            {
                this.PPNNoTextBox.Text = _glFAPurchaseHd.PPNNo;
                if (_glFAPurchaseHd.PPNDate != null)
                {
                    this.PPNDateTextBox.Text = DateFormMapper.GetValue(_glFAPurchaseHd.PPNDate);
                    //this.ppn_date_start.Attributes.Add("Style", "visibility:visible");
                    this.PPNDateLiteral.Text = "<input id='button2' type='button' style ='visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                }
                else
                {
                    this.PPNDateTextBox.Text = "";
                }

                if (_glFAPurchaseHd.PPN != 0)
                {
                    this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                    this.PPNNoTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
                else
                {
                    this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                    this.PPNNoTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                }
            }
            else
            {
                this.PPNDateLiteral.Text = "<input id='button2' type='button' style ='visibility:hidden' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                //this.ppn_date_start.Attributes.Add("Style", "visibility:hidden");
                this.PPNDateTextBox.Text = "";
                this.PPNNoTextBox.Text = "";
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNNoTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.PPNRateTextBox.Text = "";
            }
            this.QtySeparateCheckBox.Checked = Convert.ToBoolean(_glFAPurchaseHd.FgQtySeparate);
            this.CurrTextBox.Text = _glFAPurchaseHd.CurrCode;
            this.AmountBaseTextBox.Text = (_glFAPurchaseHd.BaseForex == 0) ? "0" : _glFAPurchaseHd.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DiscForexTextBox.Text = (_glFAPurchaseHd.DiscForex == 0) ? "0" : _glFAPurchaseHd.DiscForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_glFAPurchaseHd.PPNForex == 0) ? "0" : _glFAPurchaseHd.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_glFAPurchaseHd.TotalForex == 0) ? "0" : _glFAPurchaseHd.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _glFAPurchaseHd.Remark;
        }

        //protected void SupplierDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (SupplierDropDownList.SelectedValue != "null")
        //    {
        //        this.AttnTextBox.Text = this._supplierBL.GetSuppContact(this.SupplierDropDownList.SelectedValue);
        //        this.TermDropDownList.SelectedValue = this._supplierBL.GetTerm(this.SupplierDropDownList.SelectedValue);

        //        string _currCode = _supplierBL.GetCurr(this.SupplierDropDownList.SelectedValue);
        //        string _currCodeHome = _currencyBL.GetCurrDefault();
        //        byte _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);

        //        this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        //        this.PPNRateTextBox.Text = this.ForexRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrencyDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        //        if (this.CurrencyDropDownList.SelectedValue == _currCodeHome)
        //        {
        //            this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
        //            this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
        //            this.ForexRateTextBox.Attributes.Remove("OnBlur");

        //            this.ForexRateTextBox.Text = "1";

        //            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
        //            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
        //            this.PPNRateTextBox.Attributes.Remove("OnBlur");

        //            this.PPNRateTextBox.Text = "1";
        //        }
        //        else
        //        {
        //            this.ForexRateTextBox.Attributes.Remove("ReadOnly");
        //            this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        //            this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

        //            this.PPNRateTextBox.Attributes.Remove("ReadOnly");
        //            this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        //            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        //        }

        //        this.CurrTextBox.Text = CurrencyDropDownList.SelectedValue;
        //        this.ShowFAPO();
        //    }
        //    else
        //    {
        //        this.AttnTextBox.Text = "";
        //        this.TermDropDownList.SelectedValue = "null";
        //        this.ForexRateTextBox.Text = "0";
        //        this.PPNRateTextBox.Text = "0";
        //        this.CurrencyDropDownList.SelectedValue = "null";
        //        this.CurrTextBox.Text = "";
        //        //this.FAPONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //        //this.FAPONoDropDownList.SelectedValue = "null";
        //    }
        //}

        protected void CurrencyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrencyDropDownList.SelectedValue != "null")
            {

                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrencyDropDownList.SelectedValue);

                this.PPNRateTextBox.Text = this.ForexRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrencyDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                if (this.CurrencyDropDownList.SelectedValue == _currCodeHome)
                {
                    this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.ForexRateTextBox.Attributes.Remove("OnBlur");

                    this.ForexRateTextBox.Text = "1";

                    this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.PPNRateTextBox.Attributes.Remove("OnBlur");

                    this.PPNRateTextBox.Text = "1";
                }
                else
                {
                    this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                    this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

                    this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                    this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
                }

                this.CurrTextBox.Text = CurrencyDropDownList.SelectedValue;
            }
            else
            {
                this.ForexRateTextBox.Text = "0";
                this.PPNRateTextBox.Text = "0";
                this.CurrencyDropDownList.SelectedValue = "null";
                this.CurrTextBox.Text = "";
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAPurchaseHd _glFAPurchaseHd = this._faPurchaseBL.GetSingleFAPurchaseHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _glFAPurchaseHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFAPurchaseHd.SuppCode = this.SupplierTextBox.Text; //this.SupplierDropDownList.SelectedValue;
            _glFAPurchaseHd.Attn = this.AttnTextBox.Text;
            _glFAPurchaseHd.SuppInvoice = this.SuppInvoiceNoTextBox.Text;
            _glFAPurchaseHd.FAPONo = this.FAPONoTextBox.Text; //this.FAPONoDropDownList.SelectedValue;
            _glFAPurchaseHd.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _glFAPurchaseHd.Forexrate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _glFAPurchaseHd.Term = this.TermDropDownList.SelectedValue;
            _glFAPurchaseHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _glFAPurchaseHd.PPNNo = this.PPNNoTextBox.Text;
            if (this.PPNDateTextBox.Text != "")
            {
                _glFAPurchaseHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _glFAPurchaseHd.PPNDate = null;
            }

            if (this.PPNRateTextBox.Text != "")
            {
                _glFAPurchaseHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            }
            else
            {
                _glFAPurchaseHd.PPNRate = null;
            }

            _glFAPurchaseHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _glFAPurchaseHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
            _glFAPurchaseHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _glFAPurchaseHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _glFAPurchaseHd.Remark = this.RemarkTextBox.Text;
            _glFAPurchaseHd.FgQtySeparate = this.QtySeparateCheckBox.Checked;

            _glFAPurchaseHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFAPurchaseHd.DatePrep = DateTime.Now;

            bool _result = this._faPurchaseBL.EditFAPurchaseHd(_glFAPurchaseHd);

            if (_result == true)
            {
                Response.Redirect(_homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
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
            GLFAPurchaseHd _glFAPurchaseHd = this._faPurchaseBL.GetSingleFAPurchaseHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _glFAPurchaseHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFAPurchaseHd.SuppCode = this.SupplierTextBox.Text; //this.SupplierDropDownList.SelectedValue;
            _glFAPurchaseHd.Attn = this.AttnTextBox.Text;
            _glFAPurchaseHd.SuppInvoice = this.SuppInvoiceNoTextBox.Text;
            _glFAPurchaseHd.FAPONo = this.FAPONoTextBox.Text; //this.FAPONoDropDownList.SelectedValue;
            _glFAPurchaseHd.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _glFAPurchaseHd.Forexrate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _glFAPurchaseHd.Term = this.TermDropDownList.SelectedValue;
            _glFAPurchaseHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _glFAPurchaseHd.PPNNo = this.PPNNoTextBox.Text;
            if (this.PPNDateTextBox.Text != "")
            {
                _glFAPurchaseHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _glFAPurchaseHd.PPNDate = null;
            }

            if (this.PPNRateTextBox.Text != "")
            {
                _glFAPurchaseHd.PPNRate = Convert.ToDecimal(this.PPNRateTextBox.Text);
            }
            else
            {
                _glFAPurchaseHd.PPNRate = null;
            }

            _glFAPurchaseHd.BaseForex = Convert.ToDecimal(this.AmountBaseTextBox.Text);
            _glFAPurchaseHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
            _glFAPurchaseHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _glFAPurchaseHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _glFAPurchaseHd.Remark = this.RemarkTextBox.Text;
            _glFAPurchaseHd.FgQtySeparate = this.QtySeparateCheckBox.Checked;

            _glFAPurchaseHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFAPurchaseHd.DatePrep = DateTime.Now;

            bool _result = this._faPurchaseBL.EditFAPurchaseHd(_glFAPurchaseHd);

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