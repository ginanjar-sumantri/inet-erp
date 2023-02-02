using System;
using System.Linq;
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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.CustomControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FABeginning
{
    public partial class FABeginningDetail : FABeginningBase
    {
        private FABeginningBL _faBeginningBL = new FABeginningBL();
        private CurrencyBL _currBL = new CurrencyBL();
        private PermissionBL _permBL = new PermissionBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();
        private ReportBL _reportBL = new ReportBL();

        private string _reportPath1 = "FABeginning/JournalEntryPrintPreview.rdlc";
        private string _reportPath2 = "FABeginning/JournalEntryPrintPreviewHomeCurr.rdlc";

        //private string _reportPath1 = "FABeginning/FABeginningPrintPreview.rdlc";

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private string _currPageKey = "CurrentPage";

        decimal totalAmountHome = 0;
        decimal totalAmountDepr = 0;
        decimal totalAmountCurr = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgPreview = "preview.jpg";
        private string _imgCreateJurnal = "view_journal.jpg";

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

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.ViewState[this._currPageKey] = 0;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.DeleteAllButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete_all.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowData(0);
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
                this.DeleteAllButton.Visible = false;
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._faBeginningBL.RowsCountFABeginningList(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        protected void ClearLabel()
        {
            this.Label.Text = "";
            this.WarningLabel.Text = "";
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

                //this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                //                                          select _query;
                //this.DataPagerBottomRepeater.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();
            }
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetBeginningDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetBeginningDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetBeginningDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetBeginningDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
        }

        private void ShowData(Int32 _prmCurrentPage)
        {
            _faBeginningBL = new FABeginningBL();
            Accounting_FABeginning _accFABeginning = this._faBeginningBL.GetSingleFABeginning(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            this.TransNoTextBox.Text = _accFABeginning.TransNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_accFABeginning.TransDate);
            this.FileNmbrTextBox.Text = _accFABeginning.FileNmbr;
            this.RemarkTextBox.Text = _accFABeginning.Remark;
            this.StatusLabel.Text = FixedAssetBeginningDataMapper.GetStatusText(_accFABeginning.Status);
            this.StatusHiddenField.Value = _accFABeginning.Status.ToString();

            if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetBeginningDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
                this.DeleteAllButton.Visible = false;
                this.EditButton.Visible = false;
            }
            else
            {
                this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
                this.DeleteAllButton.Visible = true;
                this.EditButton.Visible = true;
            }

            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._faBeginningBL.GetListFABeginningList(_prmCurrentPage, _maxrow, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), this.sortField.Value, Convert.ToBoolean(this.ascDesc.Value));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "'  ,'" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
            this.DeleteAllButton.Attributes.Add("OnClick", "return AskYouFirst();");

            this.ShowActionButton();
            this.ShowCreateJurnalButton();

            this.Panel1.Visible = true;
            this.Panel2.Visible = false;

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
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

                if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetBeginningDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
                {
                    this.CreateJurnalImageButton.Visible = true;
                }   
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

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string _currCodeHome = _currBL.GetCurrDefault();
            byte _decimalPlaceHome = _currBL.GetDecimalPlace(_currCodeHome);

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Accounting_FABeginningList _temp = (Accounting_FABeginningList)e.Item.DataItem;

                string _code = _temp.FABeginningListCode.ToString();

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

                CheckBox _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_code);

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton2");
                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton.Visible = false;
                }
                else
                {
                    if (this.StatusHiddenField.Value.Trim().ToLower() != FixedAssetBeginningDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
                    {
                        _editButton.PostBackUrl = this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeDt + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                        _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                    }
                    else
                    {
                        _editButton.Visible = false;
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

                Literal _faCodeLiteral = (Literal)e.Item.FindControl("FACodeLiteral");
                _faCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.FACode);

                Literal _faName = (Literal)e.Item.FindControl("FANameLiteral");
                _faName.Text = HttpUtility.HtmlEncode(_temp.FAName);

                Literal _totalLife = (Literal)e.Item.FindControl("LifeTotalLiteral");
                _totalLife.Text = HttpUtility.HtmlEncode(_temp.TotalLife.ToString());

                Literal _totalDepr = (Literal)e.Item.FindControl("LifeDeprLiteral");
                _totalDepr.Text = HttpUtility.HtmlEncode(_temp.TotalDepr.ToString());

                Literal _amountHome = (Literal)e.Item.FindControl("AmountHomeLiteral");
                _amountHome.Text = (_temp.AmountHome == 0) ? "0" : HttpUtility.HtmlEncode(_temp.AmountHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome)));

                Literal _amountDepr = (Literal)e.Item.FindControl("AmountDeprLiteral");
                _amountDepr.Text = (_temp.AmountDepr == 0) ? "0" : HttpUtility.HtmlEncode(_temp.AmountDepr.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome)));

                Literal _amountCurrent = (Literal)e.Item.FindControl("AmountCurrentLiteral");
                _amountCurrent.Text = (_temp.AmountCurrent == 0) ? "0" : HttpUtility.HtmlEncode(_temp.AmountCurrent.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome)));

                totalAmountHome += Convert.ToDecimal(_amountHome.Text);
                totalAmountDepr += Convert.ToDecimal(_amountDepr.Text);
                totalAmountCurr += Convert.ToDecimal(_amountCurrent.Text);
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Literal _amountHome = (Literal)e.Item.FindControl("AmountHomeLiteral");
                _amountHome.Text = (totalAmountHome == 0) ? "0" : totalAmountHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));

                Literal _amountDepr = (Literal)e.Item.FindControl("AmountDeprLiteral");
                _amountDepr.Text = (totalAmountDepr == 0) ? "0" : totalAmountDepr.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));

                Literal _amountCurrent = (Literal)e.Item.FindControl("AmountCurrentLiteral");
                _amountCurrent.Text = (totalAmountCurr == 0) ? "0" : totalAmountCurr.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempsplit = _temp.Split(',');
            //string _page = "0";

            this.ClearLabel();

            bool _result = this._faBeginningBL.DeleteMultiAccounting_FABeginningList(_tempsplit);

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

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }

        protected void DeleteAllButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempsplit = _temp.Split(',');
            //string _page = "0";

            this.ClearLabel();

            bool _result = this._faBeginningBL.DeleteAllFABeginningList(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

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

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetBeginningDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
            {
                string _result2 = this._faBeginningBL.GetAppr(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetBeginningDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result2 = this._faBeginningBL.Approve(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetBeginningDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
            {
                string _result2 = this._faBeginningBL.Posting(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), DateFormMapper.GetValue(this.DateTextBox.Text), HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetBeginningDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result2 = this._faBeginningBL.Unposting(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), HttpContext.Current.User.Identity.Name);

                //this.Label.Text = _result2;
            }

            this.ShowData(0);
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowData(Convert.ToInt32(e.CommandArgument));
            }
        }

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

            this.ShowData(_reqPage);
        }

        protected void field1_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Fixed Asset Code")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Fixed Asset Code";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }
        protected void field2_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Fixed Asset Name")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Fixed Asset Name";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }
        protected void field3_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Life Total")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Life Total";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }
        protected void field4_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Life Depr")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Life Depr";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }
        protected void field5_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Amount Home")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Amount Home";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }
        protected void field6_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Amount Depr")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Amount Depr";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }
        protected void field7_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Amount Current")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Amount Current";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }

        protected void CreateJurnalImageButton_Click(object sender, ImageClickEventArgs e)
        {

            if (this.CreateJurnalDDL.SelectedValue == "1")
            {
                this.Panel1.Visible = false;
                this.Panel4.Visible = false;
                this.Panel3.Visible = true;

                this.ReportViewer2.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource1 = this._reportBL.JournalTicketingPrintPreview(this.TransNoTextBox.Text);

                this.ReportViewer2.LocalReport.DataSources.Clear();
                this.ReportViewer2.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer2.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
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
                this.Panel3.Visible = false;
                this.Panel4.Visible = true;

                this.ReportViewer3.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource2 = this._reportBL.JournalTicketingPrintPreview(this.TransNoTextBox.Text);

                this.ReportViewer3.LocalReport.DataSources.Clear();
                this.ReportViewer3.LocalReport.DataSources.Add(_reportDataSource2);
                this.ReportViewer3.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
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

            string _result = this._faBeginningBL.Unposting(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.FABeginning), this.TransNoTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
                if (_result1 == true)
                    this.WarningLabel.Text = _result;
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
            }
            else
            {
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
                this.WarningLabel.Text = _result;
            }

            this.ShowData(0);
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }
    }
}