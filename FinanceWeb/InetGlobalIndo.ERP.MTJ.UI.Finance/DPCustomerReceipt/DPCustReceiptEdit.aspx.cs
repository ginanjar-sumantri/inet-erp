using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerReceipt
{
    public partial class DPCustReceiptEdit : DPCustomerReceiptBase
    {
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private FINDPCustomerBL _finDPCustBL = new FINDPCustomerBL();
        private FINDPCustomerListBL _finDPCustListBL = new FINDPCustomerListBL();
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
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

            _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNDateLiteral.Text = "<input id='button2' type='button' Style = 'visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                //this.ShowDPList();

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.PPNTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.BaseForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "  );");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + " );");
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");

            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.SONoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        //public void ShowDPList()
        //{
        //    FINDPCustHd _finDPCustHd = this._finDPCustBL.GetSingleFINDPCustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.DPListNoDropDownList.Items.Clear();
        //    this.DPListNoDropDownList.DataTextField = "FileNmbr";
        //    this.DPListNoDropDownList.DataValueField = "TransNmbr";
        //    this.DPListNoDropDownList.DataSource = this._finDPCustListBL.GetListDPCustListForDDL(_finDPCustHd.CustCode);
        //    this.DPListNoDropDownList.DataBind();
        //    this.DPListNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void ShowData()
        {
            FINDPCustHd _finDPCustHd = this._finDPCustBL.GetSingleFINDPCustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finDPCustHd.CurrCode);

            string _currCodeHome = _currencyBL.GetCurrDefault();

            this.TransNoTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            this.FileNmbrTextBox.Text = _finDPCustHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finDPCustHd.TransDate);
            //this.StatusLabel.Text = DPCustomerDataMapper.GetStatusText(_finDPCustHd.Status);
            this.CustTextBox.Text = _custBL.GetNameByCode(_finDPCustHd.CustCode);
            this.AttnTextBox.Text = _finDPCustHd.Attn;
            this.DPListNoTextBox.Text = this._finDPCustListBL.GetFileNmbrFINDPCustList(_finDPCustHd.DPListNo);
            this.SONoTextBox.Text = this._salesOrderBL.GetFileNmbrMKTSOHd(_finDPCustHd.SONo);
            this.SoNoHiddenField.Value = _finDPCustHd.SONo;
            this.CurrCodeTextBox.Text = _finDPCustHd.CurrCode;
            this.CurrTextBox.Text = _finDPCustHd.CurrCode;
            if (this.CurrCodeTextBox.Text == _currCodeHome)
            {
                this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            }
            else
            {
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.PPNRateTextBox.Attributes.Remove("ReadOnly");
                this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }

            if (_finDPCustHd.PPN == 0)
            {
                this.PPNDateLiteral.Text = "<input id='button2' type='button' Style = 'visibility:hidden' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                //this.ppn_date_start.Attributes.Add("Style", "visibility:hidden");
                this.PPNNoTextBox.Text = "";
                this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.PPNDateTextBox.Text = "";
                this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNDateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
            }
            else
            {
                this.PPNDateLiteral.Text = "<input id='button2' type='button' Style = 'visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                //this.ppn_date_start.Attributes.Add("Style", "visibility:visible");
                this.PPNNoTextBox.Text = _finDPCustHd.PPNNo;
                this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                this.PPNNoTextBox.Attributes.Add("Style", "background-color:#FFFFFF");
                this.PPNDateTextBox.Text = (_finDPCustHd.PPNDate == null) ? "" : DateFormMapper.GetValue(_finDPCustHd.PPNDate);
                this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNDateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
            }

            this.CurrRateTextBox.Text = (_finDPCustHd.ForexRate == 0) ? "0" : _finDPCustHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finDPCustHd.Remark;
            this.PPNNoTextBox.Text = _finDPCustHd.PPNNo;
            this.PPNDateTextBox.Text = (_finDPCustHd.PPNDate == null) ? "" : DateFormMapper.GetValue(_finDPCustHd.PPNDate);
            this.PPNRateTextBox.Text = (_finDPCustHd.PPNRate == null) ? "0" : Convert.ToDecimal(_finDPCustHd.PPNRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.BaseForexTextBox.Text = (_finDPCustHd.BaseForex == 0) ? "0" : _finDPCustHd.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNTextBox.Text = (_finDPCustHd.PPN == 0) ? "0" : _finDPCustHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_finDPCustHd.PPNForex == 0) ? "0" : _finDPCustHd.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_finDPCustHd.TotalForex == 0) ? "0" : _finDPCustHd.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDPCustHd _finDPCustHd = this._finDPCustBL.GetSingleFINDPCustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finDPCustHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finDPCustHd.Attn = this.AttnTextBox.Text;
            _finDPCustHd.SONo = this.SoNoHiddenField.Value;
            _finDPCustHd.CurrCode = this.CurrCodeTextBox.Text;
            _finDPCustHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finDPCustHd.Remark = this.RemarkTextBox.Text;
            _finDPCustHd.PPNNo = this.PPNNoTextBox.Text;
            if (this.PPNDateTextBox.Text != "")
            {
                _finDPCustHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _finDPCustHd.PPNDate = null;
            }
            _finDPCustHd.PPNRate = (this.PPNRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finDPCustHd.BaseForex = (this.BaseForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.BaseForexTextBox.Text);
            _finDPCustHd.PPN = (this.PPNTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNTextBox.Text);
            _finDPCustHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finDPCustHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finDPCustHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finDPCustHd.DatePrep = DateTime.Now;

            bool _result = this._finDPCustBL.EditFINDPCustHd(_finDPCustHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
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
            FINDPCustHd _finDPCustHd = this._finDPCustBL.GetSingleFINDPCustHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finDPCustHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finDPCustHd.Attn = this.AttnTextBox.Text;
            _finDPCustHd.SONo = this.SoNoHiddenField.Value;
            _finDPCustHd.CurrCode = this.CurrCodeTextBox.Text;
            _finDPCustHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finDPCustHd.Remark = this.RemarkTextBox.Text;
            _finDPCustHd.PPNNo = this.PPNNoTextBox.Text;
            if (this.PPNDateTextBox.Text != "")
            {
                _finDPCustHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _finDPCustHd.PPNDate = null;
            }
            _finDPCustHd.PPNRate = (this.PPNRateTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text);
            _finDPCustHd.BaseForex = (this.BaseForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.BaseForexTextBox.Text);
            _finDPCustHd.PPN = (this.PPNTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNTextBox.Text);
            _finDPCustHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finDPCustHd.TotalForex = (this.TotalForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finDPCustHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finDPCustHd.DatePrep = DateTime.Now;

            bool _result = this._finDPCustBL.EditFINDPCustHd(_finDPCustHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        //protected void DPListNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string _currCodeHome = _currencyBL.GetCurrDefault();

        //    if (DPListNoDropDownList.SelectedValue != "null")
        //    {
        //        FINDPCustList _finDPCustList = this._finDPCustListBL.GetSingleFINDPCustList(this.DPListNoDropDownList.SelectedValue);

        //        this.SoNoHiddenField.Value = _finDPCustList.SONo;
        //        this.SONoTextBox.Text = _salesOrderBL.GetFileNmbrMKTSOHd(_finDPCustList.SONo);
        //        this.CurrCodeTextBox.Text = _finDPCustList.CurrCode;
        //        this.CurrTextBox.Text = _finDPCustList.CurrCode;
        //        this.CurrRateTextBox.Text = _finDPCustList.ForexRate.ToString("#,###.##");
        //        //this.PPNRateTextBox.Text = _finDPCustList.ForexRate.ToString("#,###.##");
        //        //this.PPNTextBox.Text = (_finDPCustList.PPn == 0) ? "0" : _finDPCustList.PPn.ToString("#,###.##");
        //        //this.PPNForexTextBox.Text = (_finDPCustList.PPNForex == 0) ? "0" : _finDPCustList.PPNForex.ToString("#,###.##");
        //        //this.BaseForexTextBox.Text = (_finDPCustList.BaseForex == 0) ? "0" : _finDPCustList.BaseForex.ToString("#,###.##");
        //        //this.TotalForexTextBox.Text = (_finDPCustList.TotalForex == 0) ? "0" : _finDPCustList.TotalForex.ToString("#,###.##");
        //    }
        //    else
        //    {
        //        this.SoNoHiddenField.Value = "";
        //        this.SONoTextBox.Text = "";
        //        this.CurrCodeTextBox.Text = "";
        //        this.CurrTextBox.Text = "";
        //        this.CurrRateTextBox.Text = "";
        //        //this.PPNRateTextBox.Text = "";
        //        //this.PPNTextBox.Text = "0";
        //        //this.PPNForexTextBox.Text = "0";
        //        //this.BaseForexTextBox.Text = "0";
        //        //this.TotalForexTextBox.Text = "0";
        //    }
        //    if (this.CurrCodeTextBox.Text == _currCodeHome)
        //    {
        //        this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
        //        this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
        //        this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
        //        this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
        //    }
        //    else
        //    {
        //        this.CurrRateTextBox.Attributes.Remove("ReadOnly");
        //        this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        //        this.PPNRateTextBox.Attributes.Remove("ReadOnly");
        //        this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        //    }
        // }
    }
}