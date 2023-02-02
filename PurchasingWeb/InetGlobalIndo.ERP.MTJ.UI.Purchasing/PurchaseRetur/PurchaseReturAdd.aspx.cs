using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRetur
{
    public partial class PurchaseReturAdd : PurchaseReturBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowSupplier();
                //this.ShowRRNo();
                this.ShowCurrency();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNTextBox.Text = "0";
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowSupplier()
        {
            this.SupplierDropDownList.Items.Clear();
            this.SupplierDropDownList.DataTextField = "SuppName";
            this.SupplierDropDownList.DataValueField = "SuppCode";
            this.SupplierDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.SupplierDropDownList.DataBind();
            this.SupplierDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
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

        //public void ShowRRNo()
        //{
        //    this.RRNoDropDownList.Items.Clear();
        //    this.RRNoDropDownList.DataTextField = "FileNmbr";
        //    this.RRNoDropDownList.DataValueField = "TransNmbr";
        //    this.RRNoDropDownList.DataSource = this._receivingPOBL.GetListDDLRRQtyRemainForReturBySuppCode(this.SupplierDropDownList.SelectedValue);
        //    this.RRNoDropDownList.DataBind();
        //    this.RRNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();
            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.RemarkTextBox.Text = "";
            this.SupplierDropDownList.SelectedValue = "null";
            //this.RRNoDropDownList.SelectedValue = "null";
            this.ReferenceNoTextBox.Text = "";
        }

        //protected void SupplierDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.ShowRRNo();
        //}

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            PRCReturHd _prcReturHd = new PRCReturHd();

            //_prcReturHd.RRNo = this.RRNoDropDownList.SelectedValue;
            //string _poNmbr = _receivingPOBL.GetPONoSTCReceiveHd(this.RRNoDropDownList.SelectedValue);
            //PRCPOHd _po = this._purchaseOrderBL.GetSinglePRCPOHd(_poNmbr);
            //_prcReturHd.CurrCode = _po.CurrCode;
            //_prcReturHd.ForexRate = _po.ForexRate;
            //_prcReturHd.PPN = _po.PPN;
            //_prcReturHd.PPNRate = _po.PPNForex;
            //_prcReturHd.PPNRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            //string _suppGroup = _suppBL.GetSuppGroupByCode(this.SupplierDropDownList.SelectedValue);
            //MsSuppGroupAcc _msSuppGroupAcc = _suppBL.GetSingleSuppGroupAcc(_suppGroup, _prcReturHd.CurrCode);
            //_prcReturHd.AccAP = _msSuppGroupAcc.AccAP;
            //_prcReturHd.FgAP = _accountBL.GetAccountSubLed(_msSuppGroupAcc.AccAP);
            //_prcReturHd.AccPPN = _msSuppGroupAcc.AccPPn;
            //_prcReturHd.FgPPN = _accountBL.GetAccountSubLed(_msSuppGroupAcc.AccPPn);
            //_prcReturHd.EditBy = HttpContext.Current.User.Identity.Name;
            //_prcReturHd.EditDate = DateTime.Now;
            _prcReturHd.Status = PurchaseReturDataMapper.GetStatusByte(TransStatus.OnHold);
            _prcReturHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _prcReturHd.SuppCode = this.SupplierDropDownList.SelectedValue;
            _prcReturHd.FgDeliveryBack = (this.DeliveryBackRBL.SelectedValue == "Y") ? true : false;
            _prcReturHd.UseReference = (this.UseReferenceRBL.SelectedValue == "Y") ? 'Y' : 'N';
            _prcReturHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            if (this.UseReferenceRBL.SelectedValue == "Y")
                _prcReturHd.RRNo = this.ReferenceNoTextBox.Text;
            _prcReturHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            //_prcReturHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            //_prcReturHd.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            //_prcReturHd.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
            //_prcReturHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            //_prcReturHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            //_prcReturHd.ForexRate = 0;
            _prcReturHd.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
            _prcReturHd.BaseForex = 0;
            _prcReturHd.PPNForex = 0;
            _prcReturHd.TotalForex = 0;
            _prcReturHd.Remark = this.RemarkTextBox.Text;
            _prcReturHd.CreatedBy = HttpContext.Current.User.Identity.Name;
            _prcReturHd.CreatedDate = DateTime.Now;
            _prcReturHd.EditBy = "";
            _prcReturHd.EditDate = DateTime.Now;

            string _result = this._purchaseReturBL.AddPRCReturHd(_prcReturHd);

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
            this.PPNForexTextBox.Text = (Convert.ToDecimal(this.BaseForexTextBox.Text) * (Convert.ToDecimal(this.PPNTextBox.Text) / 100) * Convert.ToDecimal(this.CurrRateTextBox.Text)).ToString("#,#.##");

            if (this.PPNForexTextBox.Text == "")
                this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = ((Convert.ToDecimal(this.BaseForexTextBox.Text) * Convert.ToDecimal(this.CurrRateTextBox.Text)) + Convert.ToDecimal(this.PPNForexTextBox.Text)).ToString("#,#.##");
        }

        protected void PPNTextBox_TextChanged(object sender, EventArgs e)
        {
            this.BaseForexTextBox_TextChanged(null, null);
        }
    }
}