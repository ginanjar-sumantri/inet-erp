using System;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.BudgetEntry
{
    public partial class GLBudgetDetail : GLBudgetBase
    {
        private GLBudgetBL _glBudgetBL = new GLBudgetBL();
        private ReportBL _reportBL = new ReportBL();
        private PermissionBL _permBL = new PermissionBL();
        private OrganizationStructureBL _orgUnit = new OrganizationStructureBL();
        private CurrencyBL _curr = new CurrencyBL();
        private AccountBL _accountBL = new AccountBL();

        private string _reportPath1 = "GLBudget/GLBudgetPrintPreview.rdlc";

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _currPageKey = "CurrentPage";

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;
        //private Boolean _isCheckedAll = true;

        private decimal _amount = 0;

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgPreview = "preview.jpg";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

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

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ViewState[this._currPageKey] = 0;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowData();
                this.ShowDataDetail(0);
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

        private double RowCount()
        {
            double _result = 0;

            _result = this._glBudgetBL.RowsCountGLBudgetAcc(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private Boolean IsCheckedAll()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.ListRepeater.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.ListRepeater.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        private void ShowPage(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount();

            if (_prmCurrentPage - _maxlength > 0)
            {
                min = _prmCurrentPage - _maxlength;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength < q)
            {
                max = _prmCurrentPage + _maxlength + 1;
            }
            else
            {
                max = Convert.ToDecimal(q);
            }

            if (_prmCurrentPage > 0)
                _addElement += 2;

            if (_prmCurrentPage < q - 1)
                _addElement += 2;

            i = Convert.ToInt32(min);
            _pageNumber = new Int32[Convert.ToInt32(max - min) + _addElement];
            if (_pageNumber.Length != 0)
            {
                //NB: Prev Or First
                if (_prmCurrentPage > 0)
                {
                    this._navMark[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[0]);
                    _pageNumberElement++;

                    this._navMark[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag = true;
                    }

                    this._navMark[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();

                _flag = true;
                _nextFlag = false;
                _lastFlag = false;
                _navMark = _navMarkBackup;
            }
        }

        public void ShowDataDetail(Int32 _prmCurrentPage)
        {
            this.TempHidden.Value = "";

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._glBudgetBL.GetListDt(_prmCurrentPage, _maxrow, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + this._awal + "', '" + this._akhir + "', '" + this._tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
            }
        }

        private Boolean IsChecked(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHidden.Value.Split(',');

            for (int i = 0; i < _value.Length; i++)
            {
                if (_prmValue == _value[i])
                {
                    _result = true;
                    break;
                }
            }

            return _result;
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

            if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.OnHold).ToString().Trim().ToLower())
            {
                String _result = this._glBudgetBL.GetAppr(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.WaitingForApproval).ToString().Trim().ToLower())
            {
                String _result = this._glBudgetBL.Approve(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

                this.Label.Text = _result;
            }

            this.ShowData();
            this.ShowDataDetail(0);
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailAdd + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._glBudgetBL.DeleteMultiGLBudgetAcc(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), _tempSplit);

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

            Response.Redirect(this._viewPage + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GLBudgetAcc _temp = (GLBudgetAcc)e.Item.DataItem;

                string _code = _temp.BudgetDetailCode.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_code);

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._detailView + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeDt + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
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
                    if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.Approved).ToString().ToLower())
                    {
                        _editButton.Visible = false;
                    }
                    else
                    {
                        _editButton.PostBackUrl = this._detailEdit + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeDt + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
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

                Literal _accountLiteral = (Literal)e.Item.FindControl("AccountLiteral");
                _accountLiteral.Text = HttpUtility.HtmlEncode(_temp.AccountName);

                byte _decimalPlace = _curr.GetDecimalPlace(_accountBL.GetCurrByAccCode(_temp.Account));

                Literal _amountBudgetRateLiteral = (Literal)e.Item.FindControl("AmountBudgetRateLiteral");
                _amountBudgetRateLiteral.Text = HttpUtility.HtmlEncode((_temp.AmountBudgetRate == 0) ? "0" : _temp.AmountBudgetRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));

                Literal _amountBudgetForexLiteral = (Literal)e.Item.FindControl("AmountBudgetForexLiteral");
                _amountBudgetForexLiteral.Text = HttpUtility.HtmlEncode((_temp.AmountBudgetForex == 0) ? "0" : _temp.AmountBudgetForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));

                byte _decimalPlaceHome = _curr.GetDecimalPlace(_curr.GetCurrDefault());

                Literal _amountBudgetHomeLiteral = (Literal)e.Item.FindControl("AmountBudgetHomeLiteral");
                _amountBudgetHomeLiteral.Text = HttpUtility.HtmlEncode((_temp.AmountBudgetHome == 0) ? "0" : _temp.AmountBudgetHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome)));

                Literal _amountActualLiteral = (Literal)e.Item.FindControl("AmountActualLiteral");
                _amountActualLiteral.Text = HttpUtility.HtmlEncode((_temp.AmountActual == 0) ? "0" : _temp.AmountActual.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome)));
            }
        }

        public void ShowData()
        {
            _glBudgetBL = new GLBudgetBL();
            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = Convert.ToInt32((_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey) == "") ? "0" : Rijndael.Decrypt(_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey), ApplicationConfig.EncryptionKey));

            _glBudgetBL = new GLBudgetBL();
            GLBudget _glBudget = this._glBudgetBL.GetSingleHd(new Guid(Rijndael.Decrypt(_nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            this.StartDateTextBox.Text = DateFormMapper.GetValue(_glBudget.StartDate);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(_glBudget.EndDate);
            this.OrgUnitTextBox.Text = _orgUnit.GetDescriptionByCode(_glBudget.OrgUnit);
            this.StatusLabel.Text = GLBudgetDataMapper.GetStatusText(_glBudget.Status);
            this.StatusHiddenField.Value = _glBudget.Status.ToString();
            this.RemarkTextBox.Text = _glBudget.Remark.ToString();

            if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.Approved).ToString().ToLower())
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
                this.EditButton.Visible = false;
            }
            else
            {
                this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
                this.EditButton.Visible = true;
            }

            this.ShowActionButton();
            //this.ShowPreviewButton();
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.OnHold).ToString().Trim().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.WaitingForApproval).ToString().Trim().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.Approved).ToString().Trim().ToLower())
            {
                this.ActionButton.Visible = false;
            }
        }

        //public void ShowPreviewButton()
        //{
        //    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;

        //    this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

        //    if (this._permPrintPreview == PermissionLevel.NoAccess)
        //    {
        //        this.PreviewButton.Visible = false;
        //    }
        //    else
        //    {
        //        if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.OnHold).ToString().Trim().ToLower())
        //        {
        //            this.PreviewButton.Visible = false;
        //        }
        //        else if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.WaitingForApproval).ToString().Trim().ToLower())
        //        {
        //            this.PreviewButton.Visible = true;
        //        }
        //        else if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.Approved).ToString().Trim().ToLower())
        //        {
        //            this.PreviewButton.Visible = true;
        //        }

        //    }
        //}

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataDetail(Convert.ToInt32(e.CommandArgument));
            }
        }

        //protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    this.Panel1.Visible = false;
        //    this.Panel2.Visible = true;

        //    this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

        //    ReportDataSource _reportDataSource1 = this._reportBL.BudgetEntryPrintPreview(Convert.ToInt32(this.YearTextBox.Text), Convert.ToInt32(_periodBL.GetPeriodByDesc(this.PeriodTextBox.Text)));

        //    this.ReportViewer1.LocalReport.DataSources.Clear();
        //    this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
        //    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
        //    this.ReportViewer1.DataBind();

        //    ReportParameter[] _reportParam = new ReportParameter[3];
        //    _reportParam[0] = new ReportParameter("Year", this.YearTextBox.Text, true);
        //    _reportParam[1] = new ReportParameter("Period", _periodBL.GetPeriodByDesc(this.PeriodTextBox.Text).ToString(), true);
        //    _reportParam[2] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
        //    this.ReportViewer1.LocalReport.SetParameters(_reportParam);
        //    this.ReportViewer1.LocalReport.Refresh();
        //}

        protected void DataPagerTopRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton");
                    _pageNumberLinkButton.CommandName = "DataPager";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark[0] = null;
                    }
                    else if (_pageNumber == this._navMark[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark[1] = null;
                    }
                    else if (_pageNumber == this._navMark[2] && this._flag == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark[2] = null;
                        this._nextFlag = true;
                        this._flag = true;
                    }
                    else if (_pageNumber == this._navMark[3] && this._flag == true && this._nextFlag == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark[3] = null;
                        this._lastFlag = true;
                    }
                    else
                    {
                        if (this._lastFlag == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark[2] && this._flag == true)
                            this._flag = false;
                    }
                }
            }
        }

        protected void DataPagerButton_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount()) - 1;
                        break;
                    }
                    else if (_reqPage < 0)
                    {
                        ((TextBox)_item.Controls[3]).Text = "1";
                        _reqPage = 0;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            this.ViewState[this._currPageKey] = _reqPage;

            this.ShowDataDetail(_reqPage);
        }
    }
}