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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using Microsoft.Reporting.WebForms;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.AdjustDiffRate
{
    public partial class AdjustDiffRateDetail : AdjustDiffRateBase
    {
        private GLAdjustDiffRateBL _adjDiffRateBL = new GLAdjustDiffRateBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private PermissionBL _permBL = new PermissionBL();
        private ReportBL _reportBL = new ReportBL();

        private string _reportPath1 = "AdjustDiffRate/JournalEntryPrintPreview.rdlc";
        private string _reportPath2 = "AdjustDiffRate/JournalEntryPrintPreviewHomeCurr.rdlc";

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private int _page2;
        private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no2 = 0;
        private int _nomor2 = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater2_ctl";
        private string _akhir2 = "_ListCheckBox2";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgCreateJurnal = "create_journal.jpg";
        
        private byte _decimalPlace = 0;

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
                //this.AddButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.ProcessButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/process.jpg";

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
                //this.AddButton2.Visible = false;
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
            this.ActionButton.Visible = true;

            if (this._adjDiffRateBL.RowsCountGLAdjustDiffRateDt2(this.TransNoTextBox.Text) > 0)
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == AdjustDiffRateDataMapper.GetStatus(AdjustDiffRateStatus.OnHold).ToString().ToLower())
                {
                    this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                    this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                    if (this._permPosting == PermissionLevel.NoAccess)
                    {
                        this.ActionButton.Visible = false;
                    }
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == AdjustDiffRateDataMapper.GetStatus(AdjustDiffRateStatus.Posting).ToString().ToLower())
                {
                    this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                    this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                    if (this._permUnposting == PermissionLevel.NoAccess)
                    {
                        this.ActionButton.Visible = false;
                    }
                }
            }
            else
            {
                this.ActionButton.Visible = false;
            }
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

                if (this.StatusHiddenField.Value.Trim().ToLower() == AdjustDiffRateDataMapper.GetStatus(AdjustDiffRateStatus.Posting).ToString().ToLower())
                {
                    this.CreateJurnalImageButton.Visible = true;
                }
            }
        }


        public void ShowData()
        {
            _adjDiffRateBL = new GLAdjustDiffRateBL();
            GLAdjustDiffRateHd _adjDiffRateHd = this._adjDiffRateBL.GetSingleGLAdjustDiffRateHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _adjDiffRateHd.TransNmbr;
            this.FileNmbrTextBox.Text = _adjDiffRateHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_adjDiffRateHd.TransDate);

            this.RemarkTextBox.Text = _adjDiffRateHd.Remark;
            this.StatusLabel.Text = AdjustDiffRateDataMapper.GetStatusText(_adjDiffRateHd.Status);
            this.StatusHiddenField.Value = _adjDiffRateHd.Status.ToString();

            if (this.StatusHiddenField.Value.Trim().ToLower() == AdjustDiffRateDataMapper.GetStatus(AdjustDiffRateStatus.Posting).ToString().ToLower())
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
                //this.AddButton2.Visible = false;
                this.DeleteButton2.Visible = false;
                this.EditButton.Visible = false;
            }
            else
            {
                this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
                //this.AddButton2.Visible = true;
                this.DeleteButton2.Visible = true;
                this.EditButton.Visible = true;
            }

            this.ShowDataDetail1();
            this.ShowDataDetail2();
            //this.ShowTotal();

            this.ShowActionButton();
            this.ShowCreateJurnalButton();
            //this.ShowPreviewButton();

            this.Panel1.Visible = true;
            //this.Panel2.Visible = false;
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
                this.ListRepeater.DataSource = this._adjDiffRateBL.GetListGLAdjustDiffRateDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");
            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            GLAdjustDiffRateDt _temp = (GLAdjustDiffRateDt)e.Item.DataItem;

            string _code = _temp.CurrCode.ToString();

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

            Literal _currency = (Literal)e.Item.FindControl("CurrencyLiteral");
            _currency.Text = HttpUtility.HtmlEncode(_temp.CurrCode);
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
                this.ListRepeater2.DataSource = this._adjDiffRateBL.GetListGLAdjustDiffRateDt2(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater2.DataBind();

            //this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + _tempHidden2 + "' );");
            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GLAdjustDiffRateDt2 _temp = (GLAdjustDiffRateDt2)e.Item.DataItem;

                _decimalPlace = this._currencyBL.GetDecimalPlace(_temp.CurrCode);

                String _code = _temp.TransNmbr.ToString();
                String _account = _temp.Account.ToString();
                String _currCode = _temp.CurrCode.ToString();
                String _all = _account + "-" + _currCode;

                if (this.TempHidden2.Value == "")
                {
                    this.TempHidden2.Value = _all;
                }
                else
                {
                    this.TempHidden2.Value += "," + _all;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral2");
                _no2 += 1;
                _noLiteral.Text = _no2.ToString();

                //CheckBox _listCheckbox2;
                //_listCheckbox2 = (CheckBox)e.Item.FindControl("ListCheckBox2");
                //_listCheckbox2.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _listCheckbox2.ClientID + ", '" + _all + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "')");

                //ImageButton _viewButton2 = (ImageButton)e.Item.FindControl("ViewButton2");
                //_viewButton2.PostBackUrl = this._viewDetailPage2 + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_all, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                //_viewButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                //this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                //if (this._permView == PermissionLevel.NoAccess)
                //{
                //    _viewButton2.Visible = false;
                //}

                //ImageButton _editButton2 = (ImageButton)e.Item.FindControl("EditButton2");
                //this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                //if (this._permEdit == PermissionLevel.NoAccess)
                //{
                //    _editButton2.Visible = false;
                //}
                //else
                //{
                //    if (this.StatusHiddenField.Value.Trim().ToLower() == AdjustDiffRateDataMapper.GetStatus(AdjustDiffRateStatus.Posting).ToString().ToLower())
                //    {
                //        _editButton2.Visible = false;
                //    }
                //    else
                //    {
                //        _editButton2.PostBackUrl = this._editDetailPage2 + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_all, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                //        _editButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                //    }
                //}

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

                Literal _accountLiteral = (Literal)e.Item.FindControl("AccountLiteral");
                _accountLiteral.Text = HttpUtility.HtmlEncode(_temp.Account);

                Literal _currLiteral = (Literal)e.Item.FindControl("CurrencyLiteral");
                _currLiteral.Text = HttpUtility.HtmlEncode(_temp.CurrCode);

                Literal _totalForexLiteral = (Literal)e.Item.FindControl("TotalForexLiteral");
                _totalForexLiteral.Text = (_temp.TotalForex == 0) ? "0" : HttpUtility.HtmlEncode(_temp.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));

                Literal _amountInvoice = (Literal)e.Item.FindControl("TotalNewHomeLiteral");
                _amountInvoice.Text = (_temp.TotalNewHome == 0) ? "0" : HttpUtility.HtmlEncode(_temp.TotalNewHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));

                Literal _totalOldHome = (Literal)e.Item.FindControl("TotalOldHomeLiteral");
                _totalOldHome.Text = (_temp.TotalOldHome == 0) ? "0" : HttpUtility.HtmlEncode(_temp.TotalOldHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));

                Literal _totalAdjustHome = (Literal)e.Item.FindControl("TotalAdjustHomeLiteral");
                _totalAdjustHome.Text = (_temp.TotalAdjustHome == 0) ? "0" : HttpUtility.HtmlEncode(_temp.TotalAdjustHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));

            }
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
            this.ClearLabel();

            string _result = "";

            if (this.StatusHiddenField.Value.Trim().ToLower() == AdjustDiffRateDataMapper.GetStatus(AdjustDiffRateStatus.OnHold).ToString().ToLower())
            {
                if (this._adjDiffRateBL.RowsCountGLAdjustDiffRateDt2(this.TransNoTextBox.Text) > 0)
                {
                    _result = this._adjDiffRateBL.Posting(this.TransNoTextBox.Text, HttpContext.Current.User.Identity.Name);
                }
                else
                {
                    _result = "Transaction must be processed first";
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == AdjustDiffRateDataMapper.GetStatus(AdjustDiffRateStatus.Posting).ToString().ToLower())
            {
                _result = this._adjDiffRateBL.UnPosting(this.TransNoTextBox.Text, HttpContext.Current.User.Identity.Name);
            }

            this.Label.Text = _result;

            this.ShowData();
            this.ShowActionButton();
        }

        protected void ProcessButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();

            string _result = this._adjDiffRateBL.Process(this.TransNoTextBox.Text);
            this.Label.Text = _result;

            this.ShowData();
            this.ShowActionButton();
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

            bool _result = this._adjDiffRateBL.DeleteMultiGLAdjustDiffRateDt(_tempSplit, this.TransNoTextBox.Text);

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

            bool _result = this._adjDiffRateBL.DeleteMultiGLAdjustDiffRateDt2(this.TransNoTextBox.Text);

            if (_result == true)
            {
                this.WarningLabel2.Text = "Delete Success";
            }
            else
            {
                this.WarningLabel2.Text = "Delete Failed";
            }

            this.CheckHidden2.Value = "";
            //this.AllCheckBox2.Checked = false;

            this.ShowData();
        }

        protected void CreateJurnalImageButton_Click(object sender, ImageClickEventArgs e)
        {

            if (this.CreateJurnalDDL.SelectedValue == "1")
            {
                this.Panel1.Visible = false;
                this.Panel2.Visible = true;
                this.Panel3.Visible = false;                

                this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource1 = this._reportBL.JournalTicketingPrintPreview(this.TransNoTextBox.Text);

                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
                this.ReportViewer1.DataBind();
                ReportParameter[] _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
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

                ReportDataSource _reportDataSource2 = this._reportBL.JournalTicketingPrintPreview(this.TransNoTextBox.Text);

                this.ReportViewer2.LocalReport.DataSources.Clear();
                this.ReportViewer2.LocalReport.DataSources.Add(_reportDataSource2);
                this.ReportViewer2.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
                this.ReportViewer2.DataBind();
                ReportParameter[] _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                this.ReportViewer2.LocalReport.SetParameters(_reportParam);
                this.ReportViewer2.LocalReport.Refresh();
            }
        }

    }
}