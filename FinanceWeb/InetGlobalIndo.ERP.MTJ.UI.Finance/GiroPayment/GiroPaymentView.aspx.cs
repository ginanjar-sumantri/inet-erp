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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroPayment
{
    public partial class GiroPaymentView : GiroPaymentBase
    {
        private FINGiroOutBL _finGiroOutBL = new FINGiroOutBL();
        private SupplierBL _suppBL = new SupplierBL();
        private CustomerBL _custBL = new CustomerBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _year = DateTime.Now.Year;
        private int _period = DateTime.Now.Month;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.DrawnDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DrawnDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.CancelDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.CancelDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.DrawnSaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.DrawnCancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.CancelSaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelCancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.UnpostingSaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.UnpostingCancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

                this.SetAttribute();
                this.ClearPanel();
                this.ClearLabel();
                this.ShowData();

            }
        }

        protected void SetAttribute()
        {
            this.DrawnRemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.DrawnRemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
            this.CancelReasonTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.CancelReasonTextBox.ClientID + "," + this.CounterTextBox2.ClientID + ",500" + ");");
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.DrawnSaveButton.Visible = false;
                this.CancelSaveButton.Visible = false;
                this.UnpostingSaveButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearPanel()
        {
            this.CancelPanel.Visible = false;
            this.DrawnPanel.Visible = false;
            this.UnpostingPanel.Visible = false;
        }

        public void SetPanel()
        {
            if (this.StatusHiddenField.Value == GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.OnHold).ToString())
            {
                this.DrawnPanel.Visible = false;
                this.CancelPanel.Visible = false;
                this.UnpostingPanel.Visible = false;
            }
            else if (this.StatusHiddenField.Value == GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.Drawn).ToString())
            {
                this.DrawnPanel.Visible = true;
                this.CancelPanel.Visible = false;
                this.UnpostingPanel.Visible = false;
            }
            else if (this.StatusHiddenField.Value == GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.Cancelled).ToString())
            {
                this.DrawnPanel.Visible = false;
                this.CancelPanel.Visible = true;
                this.UnpostingPanel.Visible = false;
            }
        }

        public void ShowData()
        {
            FINGiroOut _finGiroOut = this._finGiroOutBL.GetSingleFINGiroOut(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.GiroNoTextBox.Text = _finGiroOut.GiroNo;
            this.PaymentNoTextBox.Text = _finGiroOut.FileNmbr;
            this.PaymentDateTextBox.Text = DateFormMapper.GetValue(_finGiroOut.PaymentDate);
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_finGiroOut.DueDate);

            if (_finGiroOut.SuppCode != null && _finGiroOut.SuppCode.Trim() != "")
            {
                this.PayToTextBox.Text = _finGiroOut.SuppCode;
                this.PayToNameTextBox.Text = _suppBL.GetSuppNameByCode(_finGiroOut.SuppCode);
            }
            else if (_finGiroOut.CustCode != null && _finGiroOut.CustCode.Trim() != "")
            {
                this.PayToTextBox.Text = _finGiroOut.CustCode;
                this.PayToNameTextBox.Text = _custBL.GetNameByCode(_finGiroOut.CustCode);
            }

            this.BankPaymentTextBox.Text = _paymentBL.GetPaymentName(_finGiroOut.BankPayment);
            this.CurrencyTextBox.Text = _finGiroOut.CurrCode;
            this.CurrencyRateTextBox.Text = _finGiroOut.ForexRate.ToString("#,###.##");
            this.AmountTextBox.Text = _finGiroOut.AmountForex.ToString("#,###.##");
            this.RemarkTextBox.Text = _finGiroOut.Remark;
            this.StatusLabel.Text = GiroPaymentDataMapper.GetStatusText(_finGiroOut.Status);
            this.StatusHiddenField.Value = _finGiroOut.Status.ToString();

            if (this.StatusHiddenField.Value == GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.OnHold).ToString())
            {
                this.ActionDropDownList.Items.Clear();
                this.ActionDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.ActionDropDownList.Items.Insert(1, new ListItem(GiroPaymentDataMapper.GetStatusText(GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.Drawn)), GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.Drawn).ToString()));
                this.ActionDropDownList.Items.Insert(2, new ListItem(GiroPaymentDataMapper.GetStatusText(GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.Cancelled)), GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.Cancelled).ToString()));

                this.DrawnDateTextBox.Attributes.Add("ReadOnly", "True");
                this.DrawnDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
                this.DrawnBankPaymentTextBox.Text = _paymentBL.GetPaymentName(_finGiroOut.BankPayment);
                this.DrawnRemarkTextBox.Text = "";
                this.DrawnRemarkTextBox.Attributes.Remove("Style");
                this.DrawnRemarkTextBox.Attributes.Remove("ReadOnly");

                this.CancelDateTextBox.Attributes.Add("ReadOnly", "True");
                this.CancelDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
                this.CancelReasonTextBox.Text = "";
                this.CancelReasonTextBox.Attributes.Remove("Style");
                this.CancelReasonTextBox.Attributes.Remove("ReadOnly");
            }
            else if (this.StatusHiddenField.Value == GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.Drawn).ToString())
            {
                this.ActionDropDownList.Items.Clear();
                this.ActionDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.ActionDropDownList.Items.Insert(1, new ListItem("Unposting", GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.OnHold).ToString()));

                this.DrawnDateTextBox.Attributes.Add("ReadOnly", "True");
                this.DrawnDateTextBox.Text = DateFormMapper.GetValue(_finGiroOut.DateDrawn);
                this.DrawnBankPaymentTextBox.Text = _paymentBL.GetPaymentName(_finGiroOut.BankPayment);
                this.DrawnRemarkTextBox.Text = _finGiroOut.DrawnRemark;

                this.DrawnRemarkTextBox.Attributes.Add("Style", "background-color:#cccccc");
                this.DrawnRemarkTextBox.Attributes.Add("ReadOnly", "true");
            }
            else if (this.StatusHiddenField.Value == GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.Cancelled).ToString())
            {
                this.ActionDropDownList.Items.Clear();
                this.ActionDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.ActionDropDownList.Items.Insert(1, new ListItem("Unposting", GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.OnHold).ToString()));

                this.CancelDateTextBox.Attributes.Add("ReadOnly", "True");
                this.CancelDateTextBox.Text = DateFormMapper.GetValue(_finGiroOut.TolakDate);
                this.CancelReasonTextBox.Text = _finGiroOut.TolakReason;

                this.CancelReasonTextBox.Attributes.Add("Style", "background-color:#cccccc");
                this.CancelReasonTextBox.Attributes.Add("ReadOnly", "true");
            }
            else
            {
                this.StatusLabel.Text = "Change";
                this.ActionDropDownList.Visible = false;
            }
            this.SetPanel();
        }

        protected void ActionDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearLabel();

            if (this.ActionDropDownList.SelectedValue == GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.OnHold).ToString())
            {
                this.DrawnPanel.Visible = false;
                this.CancelPanel.Visible = false;
                this.UnpostingPanel.Visible = true;
            }
            else if (this.ActionDropDownList.SelectedValue == GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.Drawn).ToString())
            {
                this.DrawnPanel.Visible = true;
                this.CancelPanel.Visible = false;
                this.UnpostingPanel.Visible = false;
            }
            else if (this.ActionDropDownList.SelectedValue == GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.Cancelled).ToString())
            {
                this.DrawnPanel.Visible = false;
                this.CancelPanel.Visible = true;
                this.UnpostingPanel.Visible = false;
            }
            else
            {
                this.DrawnPanel.Visible = false;
                this.CancelPanel.Visible = false;
                this.UnpostingPanel.Visible = false;
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void DrawnSaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _result = this._finGiroOutBL.Drawn(this.GiroNoTextBox.Text, DateFormMapper.GetValue(this.DrawnDateTextBox.Text), this.DrawnRemarkTextBox.Text, HttpContext.Current.User.Identity.Name);

            if (_result == "")
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Success Drawn Data";
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = _result;
            }
            this.ShowData();
            this.ClearPanel();
        }

        protected void CancelSaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _result = this._finGiroOutBL.Cancel(this.GiroNoTextBox.Text, DateFormMapper.GetValue(this.CancelDateTextBox.Text), this.CancelReasonTextBox.Text, HttpContext.Current.User.Identity.Name);

            if (_result == "")
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Success Cancel Data";
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = _result;
            }
            this.ShowData();
            this.ClearPanel();
        }

        protected void UnpostingSaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _result = this._finGiroOutBL.Unposting(this.GiroNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

            if (_result == "")
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Success Unposting Data";
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = _result;
            }
            this.ShowData();
            this.ClearPanel();
        }
    }
}