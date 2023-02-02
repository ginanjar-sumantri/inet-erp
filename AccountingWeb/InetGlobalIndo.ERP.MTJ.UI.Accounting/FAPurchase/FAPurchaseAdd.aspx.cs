using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAPurchase
{
    public partial class FAPurchaseAdd : FAPurchaseBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.TransDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.TransDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowSupplierDropdownlist();
                this.ShowCurrencyDropdownlist();
                this.ShowTermDropdownlist();

                this.ClearLabel();
                this.ClearData();
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

        private void ShowSupplierDropdownlist()
        {
            this.SupplierDropDownList.Items.Clear();
            this.SupplierDropDownList.DataSource = this._supplierBL.GetListDDLSupp();
            this.SupplierDropDownList.DataValueField = "SuppCode";
            this.SupplierDropDownList.DataTextField = "SuppName";
            this.SupplierDropDownList.DataBind();
            this.SupplierDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCurrencyDropdownlist()
        {
            this.CurrencyDropDownList.Items.Clear();
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowFAPO()
        {
            this.FAPONoDropDownList.Items.Clear();
            this.FAPONoDropDownList.DataSource = this._faPOBL.GetListDDLFAPOBySupplier(this.SupplierDropDownList.SelectedValue);
            this.FAPONoDropDownList.DataValueField = "TransNmbr";
            this.FAPONoDropDownList.DataTextField = "FileNmbr";
            this.FAPONoDropDownList.DataBind();
            this.FAPONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void SetAttribute()
        {
            this.TransDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");

            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2"/*this.ppn_date_start.ClientID*/ + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ClearData()
        {
            this.WarningLabel.Text = "";
            this.TransDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.SupplierDropDownList.SelectedValue = "null";
            this.AttnTextBox.Text = "";
            this.SuppInvoiceNoTextBox.Text = "";
            this.FAPONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.FAPONoDropDownList.SelectedValue = "null";
            this.CurrencyDropDownList.SelectedValue = "null";
            this.ForexRateTextBox.Text = "";
            this.TermDropDownList.SelectedValue = "null";
            this.PPNPercentTextBox.Text = "0";
            this.PPNDateTextBox.Text = "";
            this.PPNRateTextBox.Text = "";
            this.CurrTextBox.Text = "";
            this.AmountBaseTextBox.Text = "0";
            this.DiscForexTextBox.Text = "0";
            this.PPNRateTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
            this.DecimalPlaceHiddenField.Value = "";
            this.QtySeparateCheckBox.Checked = false;
        }

        protected void SupplierDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SupplierDropDownList.SelectedValue != "null")
            {
                this.AttnTextBox.Text = this._supplierBL.GetSuppContact(this.SupplierDropDownList.SelectedValue);
                this.TermDropDownList.SelectedValue = this._supplierBL.GetTerm(this.SupplierDropDownList.SelectedValue);

                string _currCode = _supplierBL.GetCurr(this.SupplierDropDownList.SelectedValue);
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_currCode);
                this.CurrencyDropDownList.SelectedValue = _currCode;
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.PPNRateTextBox.Text = this.ForexRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrencyDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
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
                this.ShowFAPO();
            }
            else
            {
                this.AttnTextBox.Text = "";
                this.TermDropDownList.SelectedValue = "null";
                this.ForexRateTextBox.Text = "0";
                this.PPNRateTextBox.Text = "0";
                this.CurrencyDropDownList.SelectedValue = "null";
                this.CurrTextBox.Text = "";
                this.FAPONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.FAPONoDropDownList.SelectedValue = "null";
            }
        }

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

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAPurchaseHd _glFAPurchaseHd = new GLFAPurchaseHd();

            _glFAPurchaseHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _glFAPurchaseHd.Status = FAPurchaseDataMapper.GetStatus(TransStatus.OnHold);
            _glFAPurchaseHd.SuppCode = this.SupplierDropDownList.SelectedValue;
            _glFAPurchaseHd.Attn = this.AttnTextBox.Text;
            _glFAPurchaseHd.SuppInvoice = this.SuppInvoiceNoTextBox.Text;
            _glFAPurchaseHd.FAPONo = this.FAPONoDropDownList.SelectedValue;
            _glFAPurchaseHd.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _glFAPurchaseHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
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
            _glFAPurchaseHd.FgQtySeparate = this.QtySeparateCheckBox.Checked;
            _glFAPurchaseHd.Remark = this.RemarkTextBox.Text;

            _glFAPurchaseHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFAPurchaseHd.DatePrep = DateTime.Now;

            List<GLFAPurchaseDt> _listDetail = new List<GLFAPurchaseDt>();

            List<PRCFAPODt> _prcFAPOdtList = _faPOBL.GetListPRCFAPODt(_glFAPurchaseHd.FAPONo);

            int _item = 1;

            foreach (PRCFAPODt _detail in _prcFAPOdtList)
            {
                PRCFAPODt _PRCFAPODt = this._faPOBL.GetSinglePRCFAPODt(_detail.TransNmbr, _detail.FANAme);

                decimal _qtyRR = (_PRCFAPODt.QtyRR == null) ? 0 : Convert.ToDecimal(_PRCFAPODt.QtyRR);

                if (_detail.Qty - _qtyRR > 0)
                {
                    GLFAPurchaseDt _glFAPurchaseDt = new GLFAPurchaseDt();

                    _glFAPurchaseDt.FAName = _detail.FANAme;
                    _glFAPurchaseDt.ItemNo = _item;
                    _glFAPurchaseDt.LifeMonth = 0;
                    _glFAPurchaseDt.FgFA = 'F';
                    _glFAPurchaseDt.Qty = _detail.Qty - _qtyRR;
                    _glFAPurchaseDt.PriceForex = _detail.PriceForex;
                    _glFAPurchaseDt.AmountForex = _glFAPurchaseDt.Qty * _detail.PriceForex;

                    _item = _item + Convert.ToInt32(_detail.Qty);

                    _listDetail.Add(_glFAPurchaseDt);
                }
            }

            string _result = this._faPurchaseBL.AddFAPurchaseHd(_glFAPurchaseHd, _listDetail);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

        protected void FAPONoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FAPONoDropDownList.SelectedValue != "null")
            {
                PRCFAPOHd _PRCFAPOHd = this._faPOBL.GetSinglePRCFAPOHdForFAPONoDDL(this.FAPONoDropDownList.SelectedValue);

                this.PPNPercentTextBox.Text = _PRCFAPOHd.PPN.ToString("#,##0");
                if (_PRCFAPOHd.PPN != 0)
                {
                    this.PPNNoTextBox.Attributes.Remove("ReadOnly");
                    this.PPNNoTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
                else
                {
                    this.PPNNoTextBox.Attributes.Add("ReadOnly","True");
                    this.PPNNoTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                }
                this.AmountBaseTextBox.Text = _PRCFAPOHd.BaseForex.ToString("###0.##");
                this.PPNForexTextBox.Text = _PRCFAPOHd.PPNForex.ToString("#,##0.##");
                this.DiscForexTextBox.Text = _PRCFAPOHd.DiscForex.ToString("#,##0.##");
                this.TotalForexTextBox.Text = _PRCFAPOHd.TotalForex.ToString("####0.##");

            }
            else
            {
                this.DiscForexTextBox.Text = "0";
            }
        }
    }
}