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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroReceipt
{
    public partial class GiroReceiptView : GiroReceiptBase
    {
        private SupplierBL _suppBL = new SupplierBL();
        private CustomerBL _custBL = new CustomerBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private BankBL _bankBL = new BankBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private FINGiroInBL _finGiroInBL = new FINGiroInBL();
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
                this.SetorDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.SetorDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DrawnDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.DrawnDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.CancelDateLiteral.Text = "<input id='button3' type='button' onclick='displayCalendar(" + this.CancelDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveCancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.SaveDrawnButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.SaveSetorButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.SaveUnpostButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelCancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.CancelDrawnButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.CancelSetorButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.CancelUnpostButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

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
            this.ReasonTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.ReasonTextBox.ClientID + "," + this.CounterTextBox2.ClientID + ",500" + ");");
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.SaveCancelButton.Visible = false;
                this.SaveDrawnButton.Visible = false;
                this.SaveSetorButton.Visible = false;
                this.SaveUnpostButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowSetorBankReceipt()
        {
            this.SetorBankReceiptDropDownList.Items.Clear();
            this.SetorBankReceiptDropDownList.DataTextField = "PayName";
            this.SetorBankReceiptDropDownList.DataValueField = "PayCode";
            this.SetorBankReceiptDropDownList.DataSource = this._paymentBL.GetListDDLGiroReceipt();
            this.SetorBankReceiptDropDownList.DataBind();
            this.SetorBankReceiptDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowBankSetor()
        {
            this.BankSetorDropDownList.Items.Clear();
            this.BankSetorDropDownList.DataTextField = "BankName";
            this.BankSetorDropDownList.DataValueField = "BankCode";
            this.BankSetorDropDownList.DataSource = this._bankBL.GetList();
            this.BankSetorDropDownList.DataBind();
            this.BankSetorDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowDrawnBankReceipt()
        {
            this.DrawnBankReceiptDropDownList.Items.Clear();
            this.DrawnBankReceiptDropDownList.DataTextField = "PayName";
            this.DrawnBankReceiptDropDownList.DataValueField = "PayCode";
            this.DrawnBankReceiptDropDownList.DataSource = this._paymentBL.GetListDDLGiroReceipt();
            this.DrawnBankReceiptDropDownList.DataBind();
            this.DrawnBankReceiptDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ClearPanel()
        {
            this.SetorPanel.Visible = false;
            this.CancelPanel.Visible = false;
            this.DrawnPanel.Visible = false;
            this.UnpostPanel.Visible = false;
        }

        public void ShowData()
        {
            FINGiroIn _finGiroIn = this._finGiroInBL.GetSingleFINGiroIn(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.GiroNoTextBox.Text = _finGiroIn.GiroNo;
            this.ReceiptNoTextBox.Text = _finGiroIn.FileNmbr;
            this.ReceiptDateTextBox.Text = DateFormMapper.GetValue(_finGiroIn.ReceiptDate);
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_finGiroIn.DueDate);
            if (_finGiroIn.SuppCode != null)
            {
                this.ReceiptCodeTextBox.Text = _finGiroIn.SuppCode;
                this.ReceiptNameTextBox.Text = _suppBL.GetSuppNameByCode(_finGiroIn.SuppCode);
            }
            else if (_finGiroIn.CustCode != null)
            {
                this.ReceiptCodeTextBox.Text = _finGiroIn.CustCode;
                this.ReceiptNameTextBox.Text = _custBL.GetNameByCode(_finGiroIn.CustCode);
            }
            this.BankGiroTextBox.Text = _bankBL.GetBankNameByCode(_finGiroIn.BankGiro);
            this.CurrCodeTextBox.Text = _finGiroIn.CurrCode;
            this.RateTextBox.Text = _finGiroIn.ForexRate.ToString("#,###.##");
            this.AmountTextBox.Text = _finGiroIn.AmountForex.ToString("#,###.##");
            this.RemarkTextBox.Text = _finGiroIn.Remark;
            this.StatusLabel.Text = GiroReceiptDataMapper.GetStatusText(_finGiroIn.Status);
            this.StatusHiddenField.Value = _finGiroIn.Status.ToString();

            if (this.StatusHiddenField.Value == GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.OnHold).ToString())
            {
                this.ActionDropDownList.Items.Clear();
                this.ActionDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.ActionDropDownList.Items.Insert(1, new ListItem(GiroReceiptDataMapper.GetStatusText(GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Deposit)), GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Deposit).ToString()));
                this.ActionDropDownList.Items.Insert(2, new ListItem(GiroReceiptDataMapper.GetStatusText(GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Cancelled)), GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Cancelled).ToString()));
            }
            else if (this.StatusHiddenField.Value == GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Deposit).ToString())
            {
                this.ActionDropDownList.Items.Clear();
                this.ActionDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.ActionDropDownList.Items.Insert(1, new ListItem(GiroReceiptDataMapper.GetStatusText(GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Drawn)), GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Drawn).ToString()));
                this.ActionDropDownList.Items.Insert(2, new ListItem(GiroReceiptDataMapper.GetStatusText(GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Cancelled)), GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Cancelled).ToString()));
                this.ActionDropDownList.Items.Insert(3, new ListItem("Unposting", GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.OnHold).ToString()));
            }
            else if (this.StatusHiddenField.Value == GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Cancelled).ToString())
            {
                this.ActionDropDownList.Items.Clear();
                this.ActionDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.ActionDropDownList.Items.Insert(1, new ListItem(GiroReceiptDataMapper.GetStatusText(GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Deposit)), GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Deposit).ToString()));
                this.ActionDropDownList.Items.Insert(2, new ListItem("Unposting", GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.OnHold).ToString()));
            }
            else if (this.StatusHiddenField.Value == GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Drawn).ToString())
            {
                this.ActionDropDownList.Items.Clear();
                this.ActionDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.ActionDropDownList.Items.Insert(1, new ListItem("Unposting", GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.OnHold).ToString()));
            }
            else
            {
                this.StatusLabel.Text = "Change";
                this.ActionDropDownList.Visible = false;
            }
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ActionDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearLabel();

            if (ActionDropDownList.SelectedValue == GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Deposit).ToString())
            {
                this.SetorPanel.Visible = true;
                this.ShowSetorBankReceipt();
                this.ShowBankSetor();
                this.SetorDateTextBox.Attributes.Add("ReadOnly", "True");
                this.SetorDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
                this.SetorBankChangeTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
                this.SetorBankChangeTextBox.Text = "0";
                this.SetorBankChangeTextBox.Attributes.Add("OnBlur", "AmountForex_OnBlur(" + this.SetorBankChangeTextBox.ClientID + ");");

                this.DrawnPanel.Visible = false;
                this.CancelPanel.Visible = false;
                this.UnpostPanel.Visible = false;
            }
            else if (ActionDropDownList.SelectedValue == GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Drawn).ToString())
            {
                this.SetorPanel.Visible = false;
                this.DrawnPanel.Visible = true;
                this.ShowDrawnBankReceipt();
                this.DrawnDateTextBox.Attributes.Add("ReadOnly", "True");
                this.DrawnDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
                this.DrawnBankChangeTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
                this.DrawnBankChangeTextBox.Text = "0";
                this.DrawnBankChangeTextBox.Attributes.Add("OnBlur", "AmountForex_OnBlur(" + this.DrawnBankChangeTextBox.ClientID + ");");

                this.CancelPanel.Visible = false;
                this.UnpostPanel.Visible = false;
            }
            else if (ActionDropDownList.SelectedValue == GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Cancelled).ToString())
            {
                this.SetorPanel.Visible = false;
                this.DrawnPanel.Visible = false;
                this.CancelPanel.Visible = true;
                this.CancelDateTextBox.Attributes.Add("ReadOnly", "True");
                this.CancelDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
                this.UnpostPanel.Visible = false;
            }
            else if (ActionDropDownList.SelectedValue == GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.OnHold).ToString())
            {
                this.SetorPanel.Visible = false;
                this.DrawnPanel.Visible = false;
                this.CancelPanel.Visible = false;
                this.UnpostPanel.Visible = true;
            }
            else
            {
                this.SetorPanel.Visible = false;
                this.DrawnPanel.Visible = false;
                this.CancelPanel.Visible = false;
                this.UnpostPanel.Visible = false;
            }
        }

        protected void SaveSetorButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _result = this._finGiroInBL.Setor(this.GiroNoTextBox.Text, DateFormMapper.GetValue(this.SetorDateTextBox.Text), this.SetorBankReceiptDropDownList.SelectedValue, this.BankSetorDropDownList.SelectedValue, Convert.ToDecimal(this.SetorBankChangeTextBox.Text), _period, HttpContext.Current.User.Identity.Name);

            this.ClearLabel();

            if (_result == true)
            {
                this.WarningLabel.Text = "Your Success Setor Data";
                //Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Setor Data";
            }
            this.ShowData();
            this.ClearPanel();
        }

        protected void CancelSetorButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveDrawnButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _result = this._finGiroInBL.Drawn(this.GiroNoTextBox.Text, DateFormMapper.GetValue(this.DrawnDateTextBox.Text), this.DrawnRemarkTextBox.Text, this.DrawnBankReceiptDropDownList.SelectedValue, Convert.ToDecimal(this.DrawnBankChangeTextBox.Text), HttpContext.Current.User.Identity.Name);

            this.ClearLabel();

            if (_result == true)
            {
                this.WarningLabel.Text = "Your Success Drawn Data";
                //Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Drawn Data";
            }
            this.ShowData();
            this.ClearPanel();
        }

        protected void CancelDrawnButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveCancelButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _result = this._finGiroInBL.Cancel(this.GiroNoTextBox.Text, DateFormMapper.GetValue(this.CancelDateTextBox.Text), this.ReasonTextBox.Text, HttpContext.Current.User.Identity.Name);

            this.ClearLabel();

            if (_result == true)
            {
                this.WarningLabel.Text = "Your Success Cancel Data";
                //Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Cancel Data";
            }
            this.ShowData();
            this.ClearPanel();
        }

        protected void CancelCancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveUnpostButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _result = this._finGiroInBL.Unposting(this.GiroNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

            this.ClearLabel();

            if (_result == true)
            {
                this.WarningLabel.Text = "Your Success Unpost Data";
                //Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Unpost Data";
            }
            this.ShowData();
            this.ClearPanel();
        }

        protected void CancelUnpostButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}