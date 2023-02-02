using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.CustomControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.BankRecon
{
    public partial class BankReconDetail : BankReconBase
    {
        private BankReconBL _bankReconBL = new BankReconBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private AccountBL _accountBL = new AccountBL();
        private PaymentBL _payTypeBL = new PaymentBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();

        private int _no = 0;
        private int _nomor = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";

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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.ClearLabel();
                this.ShowData();
                this.ShowDataDetail();

                this.SetButtonPermission();
            }
        }

        private void SetButtonPermission()
        {
            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.AddButton.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
            this.Label.Text = "";
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == BankReconDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BankReconDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BankReconDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BankReconDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
        }

        public void ShowData()
        {
            _bankReconBL = new BankReconBL();
            Finance_BankRecon _bankRecon = this._bankReconBL.GetSingleFinance_BankRecon(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            this.TransNoTextBox.Text = _bankRecon.TransNmbr;
            this.FileNmbrTextBox.Text = _bankRecon.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_bankRecon.TransDate);
            this.PayTypeTextBox.Text = _payTypeBL.GetPaymentName(_bankRecon.PayCode);
            string _account = _payTypeBL.GetAccountByCode(_bankRecon.PayCode);
            string _currCode = _accountBL.GetCurrByAccCode(_account);
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
            this.SumValueForexTextBox.Text = (_bankRecon.SumValueForex == 0) ? "0" : _bankRecon.SumValueForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DiffValueForexTextBox.Text = (_bankRecon.DiffValueForex == 0) ? "0" : _bankRecon.DiffValueForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.BankValueForexTextBox.Text = (_bankRecon.BankValueForex == 0) ? "0" : _bankRecon.BankValueForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _bankRecon.Remark;
            this.StatusLabel.Text = BankReconDataMapper.GetStatusText(_bankRecon.Status);
            this.StatusHiddenField.Value = _bankRecon.Status.ToString();
            if (this.StatusHiddenField.Value.Trim().ToLower() == BankReconDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                this.EditButton.Visible = false;
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
            }
            else
            {
                this.EditButton.Visible = true;
                this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
            }

            this.ShowActionButton();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.Label.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }
        }

        public void ShowDataDetail()
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._bankReconBL.GetListFinance_BankReconAccount(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Finance_BankReconAccount _temp = (Finance_BankReconAccount)e.Item.DataItem;

            string _accountCode = _temp.BankReconAccountCode.ToString();

            if (this.TempHidden.Value == "")
            {
                this.TempHidden.Value = _accountCode;
            }
            else
            {
                this.TempHidden.Value += "," + _accountCode;
            }

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
            _no += 1;
            _noLiteral.Text = _no.ToString();

            CheckBox _listCheckbox;
            _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
            _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _accountCode + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");

            ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                _viewButton.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == BankReconDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
                {
                    _viewButton.Visible = false;
                }
                else
                {
                    _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeItem + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_accountCode, ApplicationConfig.EncryptionKey));
                    _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";
                }
            }

            ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton2");
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                _editButton.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == BankReconDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
                {
                    _editButton.Visible = false;
                }
                else
                {
                    _editButton.PostBackUrl = this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeItem + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_accountCode, ApplicationConfig.EncryptionKey));
                    _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                }
            }

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this._accountBL.GetCurrByAccCode(_temp.Account));
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(this._currencyBL.GetCurrDefault());

            HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
            _tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");
            if (e.Item.ItemType == ListItemType.Item)
            {
                _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
                _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
            }
            if (e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
                _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
            }

            Literal _account = (Literal)e.Item.FindControl("AccountLiteral");
            _account.Text = HttpUtility.HtmlEncode(_temp.Account);

            Literal _accountName = (Literal)e.Item.FindControl("AccountNameLiteral");
            _accountName.Text = HttpUtility.HtmlEncode(_temp.AccountName);

            Literal _forexRate = (Literal)e.Item.FindControl("ForexRateLiteral");
            _forexRate.Text = (_temp.ForexRate == 0 ? "0" : HttpUtility.HtmlEncode(_temp.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace))));

            Literal _fgValue = (Literal)e.Item.FindControl("FgValueLiteral");
            _fgValue.Text = HttpUtility.HtmlEncode(BankReconDataMapper.GetFgValue(_temp.FgValue));

            Literal _amountForex = (Literal)e.Item.FindControl("AmountForexLiteral");
            _amountForex.Text = (_temp.AmountForex == 0 ? "0" : HttpUtility.HtmlEncode(_temp.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace))));
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_addDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _error = "";
            string _page = "0";

            this.ClearLabel();

            bool _result = this._bankReconBL.DeleteMultiFinance_BankReconAccount(_tempSplit, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)));
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            string _code = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            if (this.StatusHiddenField.Value.Trim().ToLower() == BankReconDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
            {
                string _result2 = this._bankReconBL.GetApproval(_code, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BankReconDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result2 = this._bankReconBL.Approve(_code, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BankReconDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
            {
                string _result2 = this._bankReconBL.Posting(_code, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BankReconDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result2 = this._bankReconBL.Unposting(_code, HttpContext.Current.User.Identity.Name);

                //this.Label.Text = _result2;
            }

            this.ShowData();
            this.ShowDataDetail();
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            string _code = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string _result2 = this._bankReconBL.Unposting(_code, HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result2 == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.BankRecon), this.TransNoTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
                if (_result1 == true)
                this.Label.Text = _result2;
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
            }
            else
            {
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
                this.Label.Text = _result2;
            }

            this.ShowData();
            this.ShowDataDetail();
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }

    }
}