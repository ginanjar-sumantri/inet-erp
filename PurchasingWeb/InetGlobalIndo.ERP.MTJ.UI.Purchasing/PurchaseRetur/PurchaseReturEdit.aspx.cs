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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRetur
{
    public partial class PurchaseReturEdit : PurchaseReturBase
    {
        private PurchaseReturBL _purchaseReturBL = new PurchaseReturBL();
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
        private ReceivingPOBL _receivingPOBL = new ReceivingPOBL();
        private SupplierBL _suppBL = new SupplierBL();
        private AccountBL _accountBL = new AccountBL();
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.SetAttribute();
                this.ClearLabel();
                this.ShowCurrency();
                this.ShowData();
            }
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            PRCReturHd _prcReturHd = this._purchaseReturBL.GetSinglePRCReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            //this.RRNoTextBox.Text = (_prcReturHd.RRNo == "null") ? "" : _receivingPOBL.GetFileNmbrSTCReceiveHd(_prcReturHd.RRNo);

            this.TransNoTextBox.Text = _prcReturHd.TransNmbr;
            this.FileNoTextBox.Text = _prcReturHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_prcReturHd.TransDate);
            this.SupplierTextBox.Text = _suppBL.GetSuppNameByCode(_prcReturHd.SuppCode);
            this.DeliveryBackRBL.SelectedValue = (_prcReturHd.FgDeliveryBack == true) ? "Y" : "N";
            this.UseReferenceRBL.SelectedValue = (_prcReturHd.UseReference == 'Y') ? "Y" : "N";
            this.ReferenceNoTextBox.Text = (_prcReturHd.RRNo == "null") ? "" : _prcReturHd.RRNo;
            this.CurrCodeDropDownList.SelectedValue = (_prcReturHd.CurrCode == "null") ? "IDR" : _prcReturHd.CurrCode;
            this.CurrCodeDropDownList_SelectedIndexChanged(null, null);
            this.CurrRateTextBox.Text = _prcReturHd.ForexRate.ToString("#,#.##");
            this.PPNTextBox.Text = _prcReturHd.PPN.ToString("#,#.##");
            this.BaseForexTextBox.Text = _prcReturHd.BaseForex.ToString("#,#.##");
            this.PPNForexTextBox.Text = _prcReturHd.PPNForex.ToString("#,#.##");
            this.TotalForexTextBox.Text = _prcReturHd.TotalForex.ToString("#,#.##");
            this.RemarkTextBox.Text = _prcReturHd.Remark;
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            PRCReturHd _prcReturHd = this._purchaseReturBL.GetSinglePRCReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _prcReturHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _prcReturHd.FgDeliveryBack = (this.DeliveryBackRBL.SelectedValue == "Y") ? true : false;
            _prcReturHd.UseReference = (this.UseReferenceRBL.SelectedValue == "Y") ? 'Y' : 'N';
            if (this.UseReferenceRBL.SelectedValue == "Y")
                _prcReturHd.RRNo = this.ReferenceNoTextBox.Text;
            _prcReturHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _prcReturHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _prcReturHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _prcReturHd.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
            _prcReturHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _prcReturHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _prcReturHd.Remark = this.RemarkTextBox.Text;

            _prcReturHd.EditBy = HttpContext.Current.User.Identity.Name;
            _prcReturHd.EditDate = DateTime.Now;

            bool _result = this._purchaseReturBL.EditPRCReturHd(_prcReturHd);

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
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            PRCReturHd _prcReturHd = this._purchaseReturBL.GetSinglePRCReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _prcReturHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _prcReturHd.FgDeliveryBack = (this.DeliveryBackRBL.SelectedValue == "Y") ? true : false;
            _prcReturHd.UseReference = (this.UseReferenceRBL.SelectedValue == "Y") ? 'Y' : 'N';
            if (this.UseReferenceRBL.SelectedValue == "Y")
                _prcReturHd.RRNo = this.ReferenceNoTextBox.Text;
            _prcReturHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _prcReturHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _prcReturHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _prcReturHd.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
            _prcReturHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _prcReturHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _prcReturHd.Remark = this.RemarkTextBox.Text;

            _prcReturHd.EditBy = HttpContext.Current.User.Identity.Name;
            _prcReturHd.EditDate = DateTime.Now;

            bool _result = this._purchaseReturBL.EditPRCReturHd(_prcReturHd);

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

        protected void UseReferenceRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.UseReferenceRBL.SelectedValue == "Y")
            {
                this.ReferenceNoTextBox.Enabled = true;
                this.ReferenceNoTextBox.Attributes.Add("BackColor", "#CCCCCC");
            }
            else
            {
                this.ReferenceNoTextBox.Enabled = false;
                this.ReferenceNoTextBox.Attributes.Remove("BackColor");
            }
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();
                byte _decimalPlace = _currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.CurrRateTextBox.Text = _currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                if (this.CurrCodeDropDownList.SelectedValue == _currCodeHome)
                {
                    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.CurrRateTextBox.Text = "1";
                }
                else
                {
                    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                }
            }
            else
            {
                this.CurrRateTextBox.Text = "0";
                this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        protected void BaseForexTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.CurrCodeDropDownList.SelectedValue == "null")
            {
                this.CurrCodeDropDownList.SelectedValue = "IDR";
                this.CurrCodeDropDownList_SelectedIndexChanged(null, null);
            }
            if (this.BaseForexTextBox.Text == "")
                this.BaseForexTextBox.Text = "0";
            if (this.PPNTextBox.Text == "")
                this.PPNTextBox.Text = "0";
            //this.PPNForexTextBox.Text = (Convert.ToDecimal(this.BaseForexTextBox.Text) * (Convert.ToDecimal(this.PPNTextBox.Text) / 100) * Convert.ToDecimal(this.CurrRateTextBox.Text)).ToString("#,#.##");
            this.PPNForexTextBox.Text = ((Convert.ToDecimal(this.BaseForexTextBox.Text) * Convert.ToDecimal(this.PPNTextBox.Text) / 100)).ToString("#,#.##");

            if (this.PPNForexTextBox.Text == "")
                this.PPNForexTextBox.Text = "0";
            //this.TotalForexTextBox.Text = ((Convert.ToDecimal(this.BaseForexTextBox.Text) * Convert.ToDecimal(this.CurrRateTextBox.Text)) + Convert.ToDecimal(this.PPNForexTextBox.Text)).ToString("#,#.##");
            this.TotalForexTextBox.Text = (Convert.ToDecimal(this.BaseForexTextBox.Text) + Convert.ToDecimal(this.PPNForexTextBox.Text)).ToString("#,#.##");
        }

        protected void PPNTextBox_TextChanged(object sender, EventArgs e)
        {
            this.BaseForexTextBox_TextChanged(null, null);
        }

    }
}