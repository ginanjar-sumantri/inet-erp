using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.JournalEntry
{
    public partial class JournalEntryDetail : JournalEntryBase
    {
        private JournalEntryBL _journalEntryBL = new JournalEntryBL();
        private ReportBL _reportBL = new ReportBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

        private string _reportPath1 = "JournalEntry/JournalEntryPrintPreview.rdlc";
        private string _reportPath2 = "JournalEntry/JournalEntryPrintPreviewHomeCurr.rdlc";

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
        private string _imgPreview = "preview.jpg";

        private decimal _totalDebit = 0;
        private decimal _totalCredit = 0;

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

                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowData();
                this.ShowDataDetail();
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
            if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
        }

        public void ShowPreviewButton()
        {
            this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;

            this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._permPrintPreview == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.OnHold).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.Approved).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                }
            }
        }

        public void ShowData()
        {
            _journalEntryBL = new JournalEntryBL();
            GLJournalHd _glJournalHd = this._journalEntryBL.GetSingleGLJournalHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeTransClass), ApplicationConfig.EncryptionKey));

            this.ReferenceNoTextBox.Text = _glJournalHd.Reference;
            this.FileNmbrTextBox.Text = _glJournalHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_glJournalHd.TransDate);
            this.RemarkTextBox.Text = _glJournalHd.Remark;
            this.StatusLabel.Text = JournalEntryStatus.GetStatusText(_glJournalHd.Status);
            this.StatusHiddenField.Value = _glJournalHd.Status.ToString();
            this.TransClassHiddenField.Value = _glJournalHd.TransClass;
            if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.Posted).ToString().ToLower())
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
            this.ShowPreviewButton();

            this.Panel1.Visible = true;
            this.Panel2.Visible = false;

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
                this.ListRepeater.DataSource = this._journalEntryBL.GetListGLJournalDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.sortField.Value, Convert.ToBoolean(this.ascDesc.Value));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(this._currencyBL.GetCurrDefault());

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GLJournalDt _temp = (GLJournalDt)e.Item.DataItem;

                string _itemNo = _temp.ItemNo.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _itemNo;
                }
                else
                {
                    this.TempHidden.Value += "," + _itemNo;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no = 0;
                _no += 1;
                //_no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                //_nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _itemNo + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                if (this._permView == PermissionLevel.NoAccess)
                {
                    _viewButton.Visible = false;
                }
                else
                {
                    if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.Posted).ToString().ToLower())
                    {
                        _viewButton.Visible = false;
                    }
                    else
                    {
                        _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_itemNo, ApplicationConfig.EncryptionKey)) + "&" + this._codeReference + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._codeTransClass + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransClassHiddenField.Value, ApplicationConfig.EncryptionKey));
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
                    if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.Posted).ToString().ToLower())
                    {
                        _editButton.Visible = false;
                    }
                    else
                    {
                        _editButton.PostBackUrl = this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_itemNo, ApplicationConfig.EncryptionKey)) + "&" + this._codeReference + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._codeTransClass + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransClassHiddenField.Value, ApplicationConfig.EncryptionKey));
                        _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                    }
                }

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_temp.CurrCode);

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

                Literal _subLedger = (Literal)e.Item.FindControl("SubLedgerLiteral");
                _subLedger.Text = HttpUtility.HtmlEncode(_temp.SubLed);

                Literal _subLedgerName = (Literal)e.Item.FindControl("SubLedNameLiteral");
                _subLedgerName.Text = HttpUtility.HtmlEncode(_temp.SubLed_Name);

                Literal _currency = (Literal)e.Item.FindControl("CurrencyLiteral");
                _currency.Text = HttpUtility.HtmlEncode(_temp.CurrCode);

                Literal _forexRate = (Literal)e.Item.FindControl("ForexRateLiteral");
                _forexRate.Text = (_temp.ForexRate == 0 ? "0" : HttpUtility.HtmlEncode(_temp.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace))));

                Literal _debitForex = (Literal)e.Item.FindControl("DebitForexLiteral");
                _debitForex.Text = (_temp.DebitForex == 0 ? "0" : HttpUtility.HtmlEncode(_temp.DebitForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace))));

                Literal _creditForex = (Literal)e.Item.FindControl("CreditForexLiteral");
                _creditForex.Text = (_temp.CreditForex == 0 ? "0" : HttpUtility.HtmlEncode(_temp.CreditForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace))));

                Literal _debitHome = (Literal)e.Item.FindControl("DebitLiteral");
                _debitHome.Text = (_temp.DebitHome == 0 ? "0" : HttpUtility.HtmlEncode(_temp.DebitHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome))));
                _totalDebit = _totalDebit + _temp.DebitHome;

                Literal _creditHome = (Literal)e.Item.FindControl("CreditLiteral");
                _creditHome.Text = (_temp.CreditHome == 0 ? "0" : HttpUtility.HtmlEncode(_temp.CreditHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome))));
                _totalCredit = _totalCredit + _temp.CreditHome;
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Label _totalDebitLabel = (Label)e.Item.FindControl("TotalDebitLabel");
                _totalDebitLabel.Text = (_totalDebit == 0 ? "0" : HttpUtility.HtmlEncode(_totalDebit.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome))));

                Label _totalCreditLabel = (Label)e.Item.FindControl("TotalCreditLabel");
                _totalCreditLabel.Text = (_totalCredit == 0 ? "0" : HttpUtility.HtmlEncode(_totalCredit.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome))));
            }
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.CreateJurnalDDL.SelectedValue == "1")
            {
                this.Panel1.Visible = false;
                this.Panel2.Visible = true;
                this.Panel3.Visible = false;

                this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource1 = this._reportBL.JournalEntryPrintPreview(this.ReferenceNoTextBox.Text);

                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
                this.ReportViewer1.DataBind();

                ReportParameter[] _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("Nmbr", this.ReferenceNoTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                this.ReportViewer1.LocalReport.Refresh();
            }
            else if (this.CreateJurnalDDL.SelectedValue == "2")
            {
                this.Panel1.Visible = false;
                this.Panel2.Visible = false;
                this.Panel3.Visible = true;

                this.ReportViewer2.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource2 = this._reportBL.JournalEntryPrintPreview(this.ReferenceNoTextBox.Text);

                this.ReportViewer2.LocalReport.DataSources.Clear();
                this.ReportViewer2.LocalReport.DataSources.Add(_reportDataSource2);
                this.ReportViewer2.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
                this.ReportViewer2.DataBind();

                ReportParameter[] _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("Nmbr", this.ReferenceNoTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                this.ReportViewer2.LocalReport.SetParameters(_reportParam);
                this.ReportViewer2.LocalReport.Refresh();
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + _codeTransClass + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransClassHiddenField.Value, ApplicationConfig.EncryptionKey)));
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_addDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + _codeTransClass + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransClassHiddenField.Value, ApplicationConfig.EncryptionKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _error = "";
            string _page = "0";

            this.ClearLabel();

            bool _result = this._journalEntryBL.DeleteMultiGLJournalDt(_tempSplit, this.ReferenceNoTextBox.Text, this.TransClassHiddenField.Value);

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

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeTransClass + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeTransClass)) + "&" + ApplicationConfig.ActionResult + "=" + _error + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)));
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
            bool _result = this._journalEntryBL.CheckBalance(this.ReferenceNoTextBox.Text);

            if (_result == true)
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.OnHold).ToString().ToLower())
                {
                    string _result2 = this._journalEntryBL.GetApproval(this.ReferenceNoTextBox.Text, HttpContext.Current.User.Identity.Name);

                    this.Label.Text = _result2;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
                {
                    string _result2 = this._journalEntryBL.Approve(this.ReferenceNoTextBox.Text, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeTransClass), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                    this.Label.Text = _result2;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.Approved).ToString().ToLower())
                {
                    string _result2 = this._journalEntryBL.Posting(this.ReferenceNoTextBox.Text, HttpContext.Current.User.Identity.Name);

                    this.Label.Text = _result2;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == JournalEntryStatus.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    string _result2 = this._journalEntryBL.Unposting(this.ReferenceNoTextBox.Text, HttpContext.Current.User.Identity.Name);

                    this.Label.Text = _result2;
                }
            }
            else
            {
                this.Label.Text = "Not Balanced";
            }

            this.ShowData();
            this.ShowDataDetail();
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void field1_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Account")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Account";
                this.ascDesc.Value = "true";
            }
            this.ShowDataDetail();
        }

        protected void field2_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Account Name")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Account Name";
                this.ascDesc.Value = "true";
            }
            this.ShowDataDetail();
        }

        protected void field3_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Sub Ledger")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Sub Ledger";
                this.ascDesc.Value = "true";
            }
            this.ShowDataDetail();
        }

        protected void field4_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Sub Ledger Name")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Sub Ledger Name";
                this.ascDesc.Value = "true";
            }
            this.ShowDataDetail();
        }

        protected void field5_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Currency")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Currency";
                this.ascDesc.Value = "true";
            }
            this.ShowDataDetail();
        }

        protected void field6_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Forex Rate")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Forex Rate";
                this.ascDesc.Value = "true";
            }
            this.ShowDataDetail();
        }

        protected void field7_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Debit Forex")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Debit Forex";
                this.ascDesc.Value = "true";
            }
            this.ShowDataDetail();
        }

        protected void field8_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Credit Forex")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Credit Forex";
                this.ascDesc.Value = "true";
            }
            this.ShowDataDetail();
        }

        protected void field9_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Debit")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Debit";
                this.ascDesc.Value = "true";
            }
            this.ShowDataDetail();
        }

        protected void field10_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Credit")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Credit";
                this.ascDesc.Value = "true";
            }
            this.ShowDataDetail();
        }
    }
}