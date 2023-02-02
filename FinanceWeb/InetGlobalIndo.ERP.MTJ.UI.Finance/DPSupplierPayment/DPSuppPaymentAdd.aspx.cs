using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierPayment
{
    public partial class DPSuppPaymentAdd : DPSupplierPaymentBase
    {
        private SupplierBL _suppBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private FINDPSuppPayBL _finDPSuppBL = new FINDPSuppPayBL();
        private PurchaseOrderBL _poBL = new PurchaseOrderBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNDateLiteral.Text = "<input id='button2' type='button' style ='visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowSupp();
                this.ShowCurrency();
                this.ShowPODDL();
                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.BaseForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        public void ShowSupp()
        {
            this.SuppDropDownList.Items.Clear();
            this.SuppDropDownList.DataTextField = "SuppName";
            this.SuppDropDownList.DataValueField = "SuppCode";
            this.SuppDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.SuppDropDownList.DataBind();
            this.SuppDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCurrency()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowPODDL()
        {
            this.PONoDropDownList.DataTextField = "FileNmbr";
            this.PONoDropDownList.DataValueField = "TransNmbr";
            this.PONoDropDownList.DataSource = this._poBL.GetListDDLPOBySupplier(this.SuppDropDownList.SelectedValue);
            this.PONoDropDownList.DataBind();
            this.PONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();
            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.SuppDropDownList.SelectedValue = "null";
            this.AttnTextBox.Text = "";
            this.SuppInvoiceTextBox.Text = "";
            this.PONoDropDownList.SelectedValue = "null";
            this.CurrCodeDropDownList.SelectedValue = "null";
            this.CurrRateTextBox.Text = "";
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.PPNNoTextBox.Text = "";
            this.PPNDateTextBox.Text = "";
            this.PPNRateTextBox.Text = "";
            this.CurrTextBox.Text = "";
            this.BaseForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
            this.PPNTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
        }

        protected void SuppDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SuppDropDownList.SelectedValue != "null")
            {
                this.AttnTextBox.Text = _suppBL.GetSuppContact(this.SuppDropDownList.SelectedValue);

                this.ShowPODDL();
            }
            else
            {
                this.PONoDropDownList.Items.Clear();
                this.PONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

                this.AttnTextBox.Text = "";
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDPSuppHd _finDPSuppHd = new FINDPSuppHd();

            _finDPSuppHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finDPSuppHd.Status = DPSuppPayDataMapper.GetStatus(TransStatus.OnHold);
            _finDPSuppHd.SuppCode = this.SuppDropDownList.SelectedValue;
            _finDPSuppHd.Attn = this.AttnTextBox.Text;
            _finDPSuppHd.SuppInvoice = this.SuppInvoiceTextBox.Text;
            _finDPSuppHd.PONo = this.PONoDropDownList.SelectedValue;
            _finDPSuppHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _finDPSuppHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finDPSuppHd.Remark = this.RemarkTextBox.Text;
            _finDPSuppHd.PPNNo = this.PPNNoTextBox.Text;
            if (this.PPNDateTextBox.Text != "")
            {
                _finDPSuppHd.PPNDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
            }
            else
            {
                _finDPSuppHd.PPNDate = null;
            }
            _finDPSuppHd.PPNRate = Convert.ToDecimal((this.PPNRateTextBox.Text.Trim() == "") ? "0" : this.PPNRateTextBox.Text.Trim());
            _finDPSuppHd.BaseForex = Convert.ToDecimal((this.BaseForexTextBox.Text.Trim() == "") ? "0" : this.BaseForexTextBox.Text.Trim());
            _finDPSuppHd.PPN = Convert.ToDecimal((this.PPNTextBox.Text.Trim() == "") ? "0" : this.PPNTextBox.Text.Trim());
            _finDPSuppHd.PPNForex = Convert.ToDecimal((this.PPNForexTextBox.Text.Trim() == "") ? "0" : this.PPNForexTextBox.Text.Trim());
            _finDPSuppHd.TotalForex = Convert.ToDecimal((this.TotalForexTextBox.Text.Trim() == "") ? "0" : this.TotalForexTextBox.Text.Trim());
            _finDPSuppHd.Balance = 0;
            _finDPSuppHd.BalancePPn = 0;
            _finDPSuppHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finDPSuppHd.DatePrep = DateTime.Now;

            string _result = this._finDPSuppBL.AddFINDPSuppHd(_finDPSuppHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearDataNumeric();

            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                this.SetCurrRate();
            }

        }

        protected void PONoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PONoDropDownList.SelectedValue != "null")
            {
                PRCPOHd _poHd = _poBL.GetSinglePRCPOHd(this.PONoDropDownList.SelectedValue, _poBL.GetLastRevisiPRCPOHd(this.PONoDropDownList.SelectedValue));

                this.CurrCodeDropDownList.SelectedValue = _poHd.CurrCode;
                this.CurrCodeDropDownList.Enabled = false;
                this.CurrTextBox.Text = _poHd.CurrCode;
                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_poHd.CurrCode);
                this.CurrRateTextBox.Text = _poHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                this.CurrRateTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
                this.PPNRateTextBox.Text = _poHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                decimal _dpForex = Convert.ToDecimal((_poHd.DPForex == null) ? 0 : _poHd.DPForex);
                this.TotalForexTextBox.Text = _dpForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNTextBox.Text = _poHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (_poHd.PPN != 0)
                {
                    this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                    this.PPNNoTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
                    //this.ppn_date_start.Attributes.Add("Style", "visibility: visible");
                    this.PPNDateLiteral.Text = "<input id='button2' type='button' style ='visibility:visible' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                }
                else
                {
                    this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
                    this.PPNNoTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
                    //this.ppn_date_start.Attributes.Add("Style", "visibility: hidden");
                    this.PPNDateLiteral.Text = "<input id='button2' type='button' style ='visibility: hidden' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                }
                this.PPNForexTextBox.Text = (Math.Round(_dpForex / (100 + _poHd.PPN) * (_poHd.PPN), _decimalPlace)).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.BaseForexTextBox.Text = (Math.Round(_dpForex / ((100 + _poHd.PPN) / 100), _decimalPlace)).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.CurrCodeDropDownList.SelectedValue = "null";
                this.CurrCodeDropDownList.Enabled = true;
                this.CurrTextBox.Text = "";
                this.CurrRateTextBox.Text = "";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
                this.PPNRateTextBox.Text = "";

                this.PPNTextBox.Text = "";
                this.TotalForexTextBox.Text = "";

                this.PPNForexTextBox.Text = "";
                this.BaseForexTextBox.Text = "";
            }
        }

        private void ClearDataNumeric()
        {
            this.CurrRateTextBox.Text = "0";
            this.DecimalPlaceHiddenField.Value = "";
            this.PPNTextBox.Text = "0";
            this.PPNRateTextBox.Text = "0";
            this.CurrTextBox.Text = "";
            this.BaseForexTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
        }

        private void DisableRate()
        {
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.CurrRateTextBox.Text = "1";

            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.PPNRateTextBox.Text = "1";
        }

        private void EnableRate()
        {
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            this.PPNRateTextBox.Attributes.Remove("ReadOnly");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");

        }

        private void SetCurrRate()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
            this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNRateTextBox.Text = this.CurrRateTextBox.Text;
            this.CurrTextBox.Text = this.CurrCodeDropDownList.SelectedValue;
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (this.CurrCodeDropDownList.SelectedValue.Trim().ToLower() == _currencyBL.GetCurrDefault().Trim().ToLower())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
            }
        }
    }
}