using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptTrade
{
    public partial class ReceiptTradeDetail : ReceiptTradeBase
    {
        private FINReceiptTradeBL _finReceiptTradeBL = new FINReceiptTradeBL();
        private ReportFinanceBL _reportFinanceBL = new ReportFinanceBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private PermissionBL _permBL = new PermissionBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();
        private ReportTourBL _reportTourBL = new ReportTourBL();

        private string _reportPath1 = "ReceiptTrade/ReceiptTradePrintPreview.rdlc";
        private string _reportPath2 = "ReceiptNonTrade/JournalEntryPrintPreview.rdlc";
        private string _reportPath3 = "ReceiptNonTrade/JournalEntryPrintPreviewHomeCurr.rdlc";

        private int _no = 0;
        private int _no2 = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater2_ctl";
        private string _akhir2 = "_ListCheckBox2";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgPreview = "preview.jpg";
        private string _imgCreateJurnal = "view_journal.jpg";

        private decimal _totalReceipt = 0;
        private decimal _totalBankCharges = 0;
        private decimal _totalCustCharges = 0;
        private decimal _totalInvoice = 0;
        private byte _decimalPlaceDb = 0;
        private byte _decimalPlaceCr = 0;
        private byte _decimalPlaceHome = 0;

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
                this.AddButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.ClearLabel();
                this.ShowData();

                this.SetButtonPermission();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
            this.Label.Text = "";
            this.WarningLabel2.Text = "";
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.AddButton.Visible = false;
                this.AddButton2.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
                this.DeleteButton2.Visible = false;
            }
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
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
            this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._permPrintPreview == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
            }
        }

        public void ShowTotal()
        {
            this.TotalInvoiceLabel.Text = _totalInvoice == 0 ? "0" : _totalInvoice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            this.TotalReceiptLabel.Text = _totalReceipt == 0 ? "0" : _totalReceipt.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            this.TotalBankChargesLabel.Text = _totalBankCharges == 0 ? "0" : _totalBankCharges.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceDb));
            this.TotalCustChargesLabel.Text = _totalCustCharges == 0 ? "0" : _totalCustCharges.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceDb));
        }

        public void ShowData()
        {
            _finReceiptTradeBL = new FINReceiptTradeBL();
            FINReceiptTradeHd _finReceiptTradeHd = this._finReceiptTradeBL.GetSingleFINReceiptTradeHdView(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _finReceiptTradeHd.TransNmbr;
            this.FileNmbrTextBox.Text = _finReceiptTradeHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finReceiptTradeHd.TransDate);
            this.CustTextBox.Text = _finReceiptTradeHd.CustomerName;

            this.RemarkTextBox.Text = _finReceiptTradeHd.Remark;
            this.StatusLabel.Text = ReceiptTradeDataMapper.GetStatusText(_finReceiptTradeHd.Status);
            this.StatusHiddenField.Value = _finReceiptTradeHd.Status.ToString();

            if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
                this.AddButton2.Visible = false;
                this.DeleteButton2.Visible = false;
                this.EditButton.Visible = false;
            }
            else
            {
                this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
                this.AddButton2.Visible = true;
                this.DeleteButton2.Visible = true;
                this.EditButton.Visible = true;
            }

            this.ShowDataDetail1();
            this.ShowDataDetail2();
            this.ShowTotal();

            this.ShowActionButton();
            this.ShowPreviewButton();

            this.ShowCreateJurnalButton();
            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
            this.Panel3.Visible = false;
            this.Panel4.Visible = false;
        }
        // -- untuk mencari credit
        public void ShowDataDetail2()
        {
            this.TempHidden2.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater2.DataSource = null;
            }
            else
            {
                this.ListRepeater2.DataSource = this._finReceiptTradeBL.GetListFINReceiptTradeCr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater2.DataBind();

            this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + _tempHidden2 + "' );");
            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FINReceiptTradeCr _temp = (FINReceiptTradeCr)e.Item.DataItem;

                _decimalPlaceCr = this._currencyBL.GetDecimalPlace(_temp.CurrCode);

                string _code = _temp.TransNmbr.ToString();
                string _invoice = _temp.InvoiceNo.ToString();

                if (this.TempHidden2.Value == "")
                {
                    this.TempHidden2.Value = _invoice;
                }
                else
                {
                    this.TempHidden2.Value += "," + _invoice;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral2");
                _no2 += 1;
                _noLiteral.Text = _no2.ToString();

                CheckBox _listCheckbox2;
                _listCheckbox2 = (CheckBox)e.Item.FindControl("ListCheckBox2");
                _listCheckbox2.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _listCheckbox2.ClientID + ", '" + _invoice + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "')");

                ImageButton _viewButton2 = (ImageButton)e.Item.FindControl("ViewButton2");
                _viewButton2.PostBackUrl = this._viewDetailPage2 + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_invoice, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _viewButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                if (this._permView == PermissionLevel.NoAccess)
                {
                    _viewButton2.Visible = false;
                }

                ImageButton _editButton2 = (ImageButton)e.Item.FindControl("EditButton2");
                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton2.Visible = false;
                }
                else
                {
                    if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                    {
                        _editButton2.Visible = false;
                    }
                    else
                    {
                        _editButton2.PostBackUrl = this._editDetailPage2 + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_invoice, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                        _editButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                    }
                }

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

                Literal _invoiceNo = (Literal)e.Item.FindControl("InvoiceLiteral");
                _invoiceNo.Text = HttpUtility.HtmlEncode(_temp.FileNmbr);


                Decimal _fgValue = (_temp.FgValue == null) ? 0 : Convert.ToDecimal(_temp.FgValue);
                Decimal _amountForexValue = _temp.AmountForex * _fgValue;
                Literal _amountForex = (Literal)e.Item.FindControl("AmountForexLiteral");
                _amountForex.Text = (_temp.AmountForex == 0) ? "0" : HttpUtility.HtmlEncode(_amountForexValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceCr)));

                Decimal _amountInvoiceValue = _temp.AmountInvoice * _fgValue;
                Literal _amountInvoice = (Literal)e.Item.FindControl("AmountInvoiceLiteral");
                _amountInvoice.Text = (_temp.AmountInvoice == 0) ? "0" : HttpUtility.HtmlEncode(_amountInvoiceValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceCr)));

                Decimal _amountBalanceValue = _temp.AmountBalance * _fgValue;
                Literal _amountBalance = (Literal)e.Item.FindControl("AmountBalanceLiteral");
                _amountBalance.Text = (_temp.AmountBalance == 0) ? "0" : HttpUtility.HtmlEncode(_amountBalanceValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceCr)));

                Decimal _amountHomeValue = (_temp.AmountHome == null) ? 0 : Convert.ToDecimal(_temp.AmountHome);
                _amountHomeValue = _amountHomeValue * _fgValue;
                _totalInvoice = _totalInvoice + Convert.ToDecimal(_amountHomeValue);
            }
        }

        // -- untuk mencari debit
        public void ShowDataDetail1()
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
                this.ListRepeater.DataSource = this._finReceiptTradeBL.GetListFINReceiptTradeDb(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");
            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            FINReceiptTradeDb _temp = (FINReceiptTradeDb)e.Item.DataItem;

            _decimalPlaceDb = this._currencyBL.GetDecimalPlace(_temp.CurrCode);

            _decimalPlaceHome = _currencyBL.GetDecimalPlace(_currencyBL.GetCurrDefault());
            string _code = _temp.ItemNo.ToString();

            if (this.TempHidden.Value == "")
            {
                this.TempHidden.Value = _code;
            }
            else
            {
                this.TempHidden.Value += "," + _code;
            }

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
            _no += 1;
            _noLiteral.Text = _no.ToString();

            CheckBox _listCheckbox;
            _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
            _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");

            ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
            _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
            _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                _viewButton.Visible = false;
            }

            ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                _editButton.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    _editButton.Visible = false;
                }
                else
                {
                    _editButton.PostBackUrl = this._editDetailPage + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
                    _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                }
            }

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

            Literal _payName = (Literal)e.Item.FindControl("PayNameLiteral");
            _payName.Text = HttpUtility.HtmlEncode(_temp.PayName);

            Literal _documentNo = (Literal)e.Item.FindControl("DocumentLiteral");
            _documentNo.Text = HttpUtility.HtmlEncode(_temp.DocumentNo);

            Literal _currency = (Literal)e.Item.FindControl("CurrencyLiteral");
            _currency.Text = HttpUtility.HtmlEncode(_temp.CurrCode);

            Literal _forexRate = (Literal)e.Item.FindControl("ForexRateLiteral");
            _forexRate.Text = (_temp.ForexRate == 0) ? "0" : HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.ForexRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceDb)));

            Literal _amountForex = (Literal)e.Item.FindControl("AmountForexLiteral");
            _amountForex.Text = (_temp.AmountForex == 0) ? "0" : HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.AmountForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceDb)));

            Literal _bankCharge = (Literal)e.Item.FindControl("BankChargeLiteral");
            _bankCharge.Text = (_temp.BankExpense == 0 || _temp.BankExpense == null) ? "0" : HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.BankExpense).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceDb)));

            Literal _custCharge = (Literal)e.Item.FindControl("CustChargeLiteral");
            _custCharge.Text = (_temp.CustRevenue == 0 || _temp.CustRevenue == null) ? "0" : HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.CustRevenue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceDb)));

            Literal _receiptForex = (Literal)e.Item.FindControl("ReceiptForexLiteral");
            _receiptForex.Text = (_temp.ReceiptForex == 0 || _temp.ReceiptForex == null) ? "0" : HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.ReceiptForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceDb)));

            Literal _receiptHome = (Literal)e.Item.FindControl("ReceiptHomeLiteral");
            _receiptHome.Text = (_temp.ReceiptHome == 0 || _temp.ReceiptHome == null) ? "0" : HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.ReceiptHome).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome)));

            _totalReceipt = _totalReceipt + Convert.ToDecimal((_temp.ReceiptHome == null) ? 0 : _temp.ReceiptHome);
            _totalBankCharges = _totalBankCharges + Convert.ToDecimal(((_temp.BankExpense * _temp.ForexRate) == null) ? 0 : _temp.BankExpense);
            _totalCustCharges = _totalCustCharges + Convert.ToDecimal(((_temp.CustRevenue * _temp.ForexRate) == null) ? 0 : _temp.CustRevenue);
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            //string[] _date = this.DateTextBox.Text.Split('-');
            //int _year = Convert.ToInt32(_date[0]);
            //int _period = Convert.ToInt32(_date[1]);
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                string _result2 = this._finReceiptTradeBL.GetAppr(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result2 = this._finReceiptTradeBL.Approve(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                string _result2 = this._finReceiptTradeBL.Posting(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == ReceiptTradeDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result2 = this._finReceiptTradeBL.Unposting(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                //this.Label.Text = _result2;
            }

            this.ShowData();
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.TransNoTextBox.Text;
            string[] _tempSplit = _temp.Split(',');

            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._reportFinanceBL.ReceiptTradeReceivePrintPreview(_temp, HttpContext.Current.User.Identity.Name);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[3];
            _reportParam[0] = new ReportParameter("TransNmbr", _temp, true);
            _reportParam[1] = new ReportParameter("CurrHome", _currencyBL.GetCurrDefault(), true);
            _reportParam[2] = new ReportParameter("UserName", HttpContext.Current.User.Identity.Name, true);
            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._finReceiptTradeBL.DeleteMultiFINReceiptTradeDb(_tempSplit, this.TransNoTextBox.Text);

            if (_result == true)
            {
                this.WarningLabel.Text = "Delete Success";
            }
            else
            {
                this.WarningLabel.Text = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            this.ShowData();
        }

        protected void AddButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton2_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden2.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._finReceiptTradeBL.DeleteMultiFINReceiptTradeCr(_tempSplit, this.TransNoTextBox.Text);

            if (_result == true)
            {
                this.WarningLabel2.Text = "Delete Success";
            }
            else
            {
                this.WarningLabel2.Text = "Delete Failed";
            }

            this.CheckHidden2.Value = "";
            this.AllCheckBox2.Checked = false;

            this.ShowData();
        }


        public void ShowCreateJurnalButton()
        {
            this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

            if (this._permPosting == PermissionLevel.NoAccess)
            {
                this.CreateJurnalImageButton.Visible = false;
            }
            else
            {
                this.CreateJurnalImageButton.Visible = false;
                this.CreateJurnalImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgCreateJurnal;

                if (this.StatusHiddenField.Value.Trim().ToLower() == POSTicketingDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    this.CreateJurnalImageButton.Visible = true;
                }
            }
        }

        protected void CreateJurnalImageButton_Click(object sender, ImageClickEventArgs e)
        {

            if (this.CreateJurnalDDL.SelectedValue == "1")
            {
                this.Panel1.Visible = false;
                this.Panel2.Visible = false;
                this.Panel4.Visible = false;
                this.Panel3.Visible = true;

                this.ReportViewer2.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource1 = this._reportTourBL.JournalTicketingPrintPreview(this.TransNoTextBox.Text);

                this.ReportViewer2.LocalReport.DataSources.Clear();
                this.ReportViewer2.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer2.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
                this.ReportViewer2.DataBind();
                ReportParameter[] _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                this.ReportViewer2.LocalReport.SetParameters(_reportParam);
                this.ReportViewer2.LocalReport.Refresh();
            }
            else if (this.CreateJurnalDDL.SelectedValue == "2")
            {
                this.Panel1.Visible = false;
                this.Panel2.Visible = false;
                this.Panel4.Visible = true;
                this.Panel3.Visible = false;

                this.ReportViewer3.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource2 = this._reportTourBL.JournalTicketingPrintPreview(this.TransNoTextBox.Text);

                this.ReportViewer3.LocalReport.DataSources.Clear();
                this.ReportViewer3.LocalReport.DataSources.Add(_reportDataSource2);
                this.ReportViewer3.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath3;
                this.ReportViewer3.DataBind();
                ReportParameter[] _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                this.ReportViewer3.LocalReport.SetParameters(_reportParam);
                this.ReportViewer3.LocalReport.Refresh();
            }
        }

        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string _result2 = this._finReceiptTradeBL.Unposting(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result2 == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.ReceiptTrade), this.TransNoTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
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
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }
    }
}