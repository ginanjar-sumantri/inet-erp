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
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.CustomControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.BudgetYear
{
    public partial class BudgetYearDetail : GLBudgetYearBase
    {
        private GLBudgetBL _glBudgetBL = new GLBudgetBL();
        private PermissionBL _permBL = new PermissionBL();
        //private ReportBL _reportBL = new ReportBL();

        //private string _reportPath1 = "GLBudget/GLBudgetPrintPreview.rdlc";
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();

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
                this.SetAttribute();
                this.SetButtonPermission();
                this.ClearLabel();
                this.ClearData();
                this.ShowData();
                this.ShowDataDetail(0);
            }
        }

        protected void ClearData()
        {
            this.AccountTextBox.Text = "";
            this.DayTextBox.Text = "0";
            this.Amount01TextBox.Text = "0";
            this.Amount02TextBox.Text = "0";
            this.Amount03TextBox.Text = "0";
            this.Amount04TextBox.Text = "0";
            this.Amount05TextBox.Text = "0";
            this.Amount06TextBox.Text = "0";
            this.Amount07TextBox.Text = "0";
            this.Amount08TextBox.Text = "0";
            this.Amount09TextBox.Text = "0";
            this.Amount10TextBox.Text = "0";
            this.Amount11TextBox.Text = "0";
            this.Amount12TextBox.Text = "0";
            this.RemarkAddTextBox.Text = "";
        }

        protected void SetAttribute()
        {
            this.DayTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.DayTextBox.ClientID + "," + this.DayTextBox.ClientID + ",500" + ");");
            this.AccountTextBox.Attributes.Add("ReadOnly", "True");
            this.AccountTextBox.Attributes.Add("Style", "Background : #CCCCCC;");

            this.SearchAccountButton.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findAccount&configCode=account','_popSearch','maximize=1,toolbar=0,location=0,status=0,scrollbars=1')";

            String spawnJS = "<script language='JavaScript'>\n";
            spawnJS += "function findAccount(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.AccountTextBox.ClientID + "').value = dataArray[0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            spawnJS += "</script>\n";
            this.javascriptReceiver.Text = spawnJS;

            this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
            this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
            this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
            this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
            this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
            this.RevisiButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/revision.jpg";
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

            string[] _yearRevisi = Rijndael.Decrypt(_nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey).Split('|');
            _result = this._glBudgetBL.RowsCountGLBudgetYearDt(Convert.ToInt32(_yearRevisi[0]), Convert.ToInt32(_yearRevisi[1]));
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
                string _yearRevisi = Rijndael.Decrypt(_nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                this.ListRepeater.DataSource = this._glBudgetBL.GetListGLBudgetYearDt(_prmCurrentPage, _maxrow, "YearRevisi", _yearRevisi);
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

            string[] _yearRevisi = Rijndael.Decrypt(_nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey).Split('|');
            if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.OnHold).ToString().Trim().ToLower())
            {
                String _result = this._glBudgetBL.GetApprGLBudgetYearHd(Convert.ToInt32(_yearRevisi[0]), Convert.ToInt32(_yearRevisi[1]), HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.WaitingForApproval).ToString().Trim().ToLower())
            {
                String _result = this._glBudgetBL.ApproveGLBudgetYearHd(Convert.ToInt32(_yearRevisi[0]), Convert.ToInt32(_yearRevisi[1]), HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.Approved).ToString().Trim().ToLower())
            {
                String _result = this._glBudgetBL.PostingGLBudgetYearHd(Convert.ToInt32(_yearRevisi[0]), Convert.ToInt32(_yearRevisi[1]), HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.Posted).ToString().Trim().ToLower())
            {
                this.ReasonPanel.Visible = true;
            }

            this.ShowData();
            this.ShowDataDetail(0);
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            //Response.Redirect(this._detailAdd + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            this.AddPanel.Visible = true;
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._glBudgetBL.DeleteMultiGLBudgetYearDt(_tempSplit);

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
                GLBudgetYearDt _temp = (GLBudgetYearDt)e.Item.DataItem;

                string _code = _temp.Year.ToString() + "|" + _temp.Revisi.ToString() + "|" + _temp.ItemNo.ToString();

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

                //ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                //_viewButton.PostBackUrl = this._detailView + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeDt + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                //_viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                //this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                //if (this._permView == PermissionLevel.NoAccess)
                //{
                //    _viewButton.Visible = false;
                //}

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                _editButton.CommandName = "Edit";
                _editButton.CommandArgument = _code;

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
                        //_editButton.PostBackUrl = this._detailEdit + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeDt + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                        _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                    }
                }

                if (this.StatusLabel.Text == "Posted")
                    _editButton.Visible = false;

                ImageButton _saveButton = (ImageButton)e.Item.FindControl("SaveButton");
                _saveButton.CommandName = "Save";
                _saveButton.CommandArgument = _code;
                _saveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                _saveButton.Visible = false;

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

                TextBox _dayTextBox = (TextBox)e.Item.FindControl("DayTextBox");
                _dayTextBox.Text = HttpUtility.HtmlEncode(_temp.Days.ToString());
                _dayTextBox.Attributes.Add("ReadOnly", "True");
                _dayTextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 30px;");

                TextBox _amount01TextBox = (TextBox)e.Item.FindControl("Amount01TextBox");
                _amount01TextBox.Text = HttpUtility.HtmlEncode(_temp.Amount01.ToString("#,#0.#"));
                _amount01TextBox.Attributes.Add("ReadOnly", "True");
                _amount01TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                TextBox _amount02TextBox = (TextBox)e.Item.FindControl("Amount02TextBox");
                _amount02TextBox.Text = HttpUtility.HtmlEncode(_temp.Amount02.ToString("#,#0.#"));
                _amount02TextBox.Attributes.Add("ReadOnly", "True");
                _amount02TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                TextBox _amount03TextBox = (TextBox)e.Item.FindControl("Amount03TextBox");
                _amount03TextBox.Text = HttpUtility.HtmlEncode(_temp.Amount03.ToString("#,#0.#"));
                _amount03TextBox.Attributes.Add("ReadOnly", "True");
                _amount03TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                TextBox _amount04TextBox = (TextBox)e.Item.FindControl("Amount04TextBox");
                _amount04TextBox.Text = HttpUtility.HtmlEncode(_temp.Amount04.ToString("#,#0.#"));
                _amount04TextBox.Attributes.Add("ReadOnly", "True");
                _amount04TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                TextBox _amount05TextBox = (TextBox)e.Item.FindControl("Amount05TextBox");
                _amount05TextBox.Text = HttpUtility.HtmlEncode(_temp.Amount05.ToString("#,#0.#"));
                _amount05TextBox.Attributes.Add("ReadOnly", "True");
                _amount05TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                TextBox _amount06TextBox = (TextBox)e.Item.FindControl("Amount06TextBox");
                _amount06TextBox.Text = HttpUtility.HtmlEncode(_temp.Amount06.ToString("#,#0.#"));
                _amount06TextBox.Attributes.Add("ReadOnly", "True");
                _amount06TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                TextBox _amount07TextBox = (TextBox)e.Item.FindControl("Amount07TextBox");
                _amount07TextBox.Text = HttpUtility.HtmlEncode(_temp.Amount07.ToString("#,#0.#"));
                _amount07TextBox.Attributes.Add("ReadOnly", "True");
                _amount07TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                TextBox _amount08TextBox = (TextBox)e.Item.FindControl("Amount08TextBox");
                _amount08TextBox.Text = HttpUtility.HtmlEncode(_temp.Amount08.ToString("#,#0.#"));
                _amount08TextBox.Attributes.Add("ReadOnly", "True");
                _amount08TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                TextBox _amount09TextBox = (TextBox)e.Item.FindControl("Amount09TextBox");
                _amount09TextBox.Text = HttpUtility.HtmlEncode(_temp.Amount09.ToString("#,#0.#"));
                _amount09TextBox.Attributes.Add("ReadOnly", "True");
                _amount09TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                TextBox _amount10TextBox = (TextBox)e.Item.FindControl("Amount10TextBox");
                _amount10TextBox.Text = HttpUtility.HtmlEncode(_temp.Amount10.ToString("#,#0.#"));
                _amount10TextBox.Attributes.Add("ReadOnly", "True");
                _amount10TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                TextBox _amount11TextBox = (TextBox)e.Item.FindControl("Amount11TextBox");
                _amount11TextBox.Text = HttpUtility.HtmlEncode(_temp.Amount11.ToString("#,#0.#"));
                _amount11TextBox.Attributes.Add("ReadOnly", "True");
                _amount11TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                TextBox _amount12TextBox = (TextBox)e.Item.FindControl("Amount12TextBox");
                _amount12TextBox.Text = HttpUtility.HtmlEncode(_temp.Amount12.ToString("#,#0.#"));
                _amount12TextBox.Attributes.Add("ReadOnly", "True");
                _amount12TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                TextBox _remarkTextBox = (TextBox)e.Item.FindControl("RemarkTextBox");
                _remarkTextBox.Text = HttpUtility.HtmlEncode(_temp.Remark);
                _remarkTextBox.Attributes.Add("ReadOnly", "True");
                _remarkTextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 200px;");
            }
        }

        public void ShowData()
        {
            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = Convert.ToInt32((_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey) == "") ? "0" : Rijndael.Decrypt(_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey), ApplicationConfig.EncryptionKey));

            string[] _yearRevisi = Rijndael.Decrypt(_nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey).Split('|');
            GLBudgetYearHd _glBudgetYearHd = this._glBudgetBL.GetSingleGLBudgetYearHd(Convert.ToInt32(_yearRevisi[0]), Convert.ToInt32(_yearRevisi[1]));
            this.YearTextBox.Text = _glBudgetYearHd.Year.ToString();
            this.RevisiTextBox.Text = _glBudgetYearHd.Revisi.ToString();
            this.RemarkTextBox.Text = _glBudgetYearHd.Remark.ToString();
            this.StatusLabel.Text = GLBudgetDataMapper.GetStatusText(Convert.ToByte(_glBudgetYearHd.Status.ToString()));
            this.StatusHiddenField.Value = _glBudgetYearHd.Status.ToString();
            this.FgActiveCheckBox.Checked = _glBudgetYearHd.FgActive == 'Y' ? true : false;

            if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.Posted).ToString().ToLower())
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
                this.EditButton.Visible = false;
                this.RevisiButton.Visible = true;
            }
            else
            {
                this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
                this.EditButton.Visible = true;
                this.RevisiButton.Visible = false;
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
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == GLBudgetDataMapper.GetStatus(GLBudgetStatus.Posted).ToString().Trim().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
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

        protected void CheckValidData()
        {
            this.ClearLabel();
            if (this.AccountTextBox.Text == "")
                this.WarningLabel.Text = "Account must be filled. ";
            if (IsNumeric(this.Amount01TextBox.Text) == false | IsNumeric(this.Amount02TextBox.Text) == false | IsNumeric(this.Amount03TextBox.Text) == false | IsNumeric(this.Amount04TextBox.Text) == false | IsNumeric(this.Amount05TextBox.Text) == false | IsNumeric(this.Amount06TextBox.Text) == false | IsNumeric(this.Amount07TextBox.Text) == false | IsNumeric(this.Amount08TextBox.Text) == false | IsNumeric(this.Amount09TextBox.Text) == false | IsNumeric(this.Amount10TextBox.Text) == false | IsNumeric(this.Amount11TextBox.Text) == false | IsNumeric(this.Amount12TextBox.Text) == false)
                this.WarningLabel.Text += "Amount 01 - 12 must be Numeric Input.";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                string[] _yearRevisi = Rijndael.Decrypt(_nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey).Split('|');
                string _code = Rijndael.Decrypt(_nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

                GLBudgetYearDt _glBudgetYearDt = new GLBudgetYearDt();
                _glBudgetYearDt.Year = Convert.ToInt32(this.YearTextBox.Text);
                _glBudgetYearDt.Revisi = Convert.ToInt32(_yearRevisi[1]);

                Int32 _itemNo = 1;
                List<GLBudgetYearDt> _listGLBudgetYearDt = this._glBudgetBL.GetListGLBudgetYearDt(0, 1000, "", _code);
                foreach (var _item in _listGLBudgetYearDt)
                {
                    if (_itemNo < _item.ItemNo)
                        _itemNo = _item.ItemNo + 1;
                }
                _glBudgetYearDt.ItemNo = _itemNo;

                _glBudgetYearDt.Account = this.AccountTextBox.Text;
                _glBudgetYearDt.Days = Convert.ToInt32(this.DayTextBox.Text);
                _glBudgetYearDt.Amount01 = Convert.ToDecimal(this.Amount01TextBox.Text);
                _glBudgetYearDt.Amount02 = Convert.ToDecimal(this.Amount02TextBox.Text);
                _glBudgetYearDt.Amount03 = Convert.ToDecimal(this.Amount03TextBox.Text);
                _glBudgetYearDt.Amount04 = Convert.ToDecimal(this.Amount04TextBox.Text);
                _glBudgetYearDt.Amount05 = Convert.ToDecimal(this.Amount05TextBox.Text);
                _glBudgetYearDt.Amount06 = Convert.ToDecimal(this.Amount06TextBox.Text);
                _glBudgetYearDt.Amount07 = Convert.ToDecimal(this.Amount07TextBox.Text);
                _glBudgetYearDt.Amount08 = Convert.ToDecimal(this.Amount08TextBox.Text);
                _glBudgetYearDt.Amount09 = Convert.ToDecimal(this.Amount09TextBox.Text);
                _glBudgetYearDt.Amount10 = Convert.ToDecimal(this.Amount10TextBox.Text);
                _glBudgetYearDt.Amount11 = Convert.ToDecimal(this.Amount11TextBox.Text);
                _glBudgetYearDt.Amount12 = Convert.ToDecimal(this.Amount12TextBox.Text);
                _glBudgetYearDt.Remark = this.RemarkAddTextBox.Text;

                bool _result = this._glBudgetBL.AddGLBudgetYearDt(_glBudgetYearDt);
                if (_result == true)
                {
                    //this.WarningLabel.Text = "Success Add Data.";
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)));
                }
                else
                {
                    this.WarningLabel.Text = this.WarningLabel.Text + " Failed Add Data.";
                }
            }
        }

        #region Check
        public static bool IsNumeric(string _value)
        {
            try
            {
                Convert.ToDecimal(_value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            String[] _tempSplit = e.CommandArgument.ToString().Split('|');
            GLBudgetYearDt _temp = this._glBudgetBL.GetSingleGLBudgetYearDt(Convert.ToInt32(_tempSplit[0]), Convert.ToInt32(_tempSplit[1]), Convert.ToInt32(_tempSplit[2]));
            if (_temp != null)
            {
                TextBox _dayTextBox = (TextBox)e.Item.FindControl("DayTextBox");
                TextBox _amount01TextBox = (TextBox)e.Item.FindControl("Amount01TextBox");
                TextBox _amount02TextBox = (TextBox)e.Item.FindControl("Amount02TextBox");
                TextBox _amount03TextBox = (TextBox)e.Item.FindControl("Amount03TextBox");
                TextBox _amount04TextBox = (TextBox)e.Item.FindControl("Amount04TextBox");
                TextBox _amount05TextBox = (TextBox)e.Item.FindControl("Amount05TextBox");
                TextBox _amount06TextBox = (TextBox)e.Item.FindControl("Amount06TextBox");
                TextBox _amount07TextBox = (TextBox)e.Item.FindControl("Amount07TextBox");
                TextBox _amount08TextBox = (TextBox)e.Item.FindControl("Amount08TextBox");
                TextBox _amount09TextBox = (TextBox)e.Item.FindControl("Amount09TextBox");
                TextBox _amount10TextBox = (TextBox)e.Item.FindControl("Amount10TextBox");
                TextBox _amount11TextBox = (TextBox)e.Item.FindControl("Amount11TextBox");
                TextBox _amount12TextBox = (TextBox)e.Item.FindControl("Amount12TextBox");
                TextBox _remarkTextBox = (TextBox)e.Item.FindControl("RemarkTextBox");

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                ImageButton _saveButton = (ImageButton)e.Item.FindControl("SaveButton");

                if (e.CommandName == "Edit")
                {
                    _dayTextBox.Attributes.Remove("ReadOnly");
                    _dayTextBox.Attributes.Remove("Style");
                    _dayTextBox.Attributes.Add("Style", "Width : 30px;");

                    _amount01TextBox.Attributes.Remove("ReadOnly");
                    _amount01TextBox.Attributes.Remove("Style");
                    _amount01TextBox.Attributes.Add("Style", "Width : 70px;");

                    _amount02TextBox.Attributes.Remove("ReadOnly");
                    _amount02TextBox.Attributes.Remove("Style");
                    _amount02TextBox.Attributes.Add("Style", "Width : 70px;");

                    _amount03TextBox.Attributes.Remove("ReadOnly");
                    _amount03TextBox.Attributes.Remove("Style");
                    _amount03TextBox.Attributes.Add("Style", "Width : 70px;");

                    _amount04TextBox.Attributes.Remove("ReadOnly");
                    _amount04TextBox.Attributes.Remove("Style");
                    _amount04TextBox.Attributes.Add("Style", "Width : 70px;");

                    _amount05TextBox.Attributes.Remove("ReadOnly");
                    _amount05TextBox.Attributes.Remove("Style");
                    _amount05TextBox.Attributes.Add("Style", "Width : 70px;");

                    _amount06TextBox.Attributes.Remove("ReadOnly");
                    _amount06TextBox.Attributes.Remove("Style");
                    _amount06TextBox.Attributes.Add("Style", "Width : 70px;");

                    _amount07TextBox.Attributes.Remove("ReadOnly");
                    _amount07TextBox.Attributes.Remove("Style");
                    _amount07TextBox.Attributes.Add("Style", "Width : 70px;");

                    _amount08TextBox.Attributes.Remove("ReadOnly");
                    _amount08TextBox.Attributes.Remove("Style");
                    _amount08TextBox.Attributes.Add("Style", "Width : 70px;");

                    _amount09TextBox.Attributes.Remove("ReadOnly");
                    _amount09TextBox.Attributes.Remove("Style");
                    _amount09TextBox.Attributes.Add("Style", "Width : 70px;");

                    _amount10TextBox.Attributes.Remove("ReadOnly");
                    _amount10TextBox.Attributes.Remove("Style");
                    _amount10TextBox.Attributes.Add("Style", "Width : 70px;");

                    _amount11TextBox.Attributes.Remove("ReadOnly");
                    _amount11TextBox.Attributes.Remove("Style");
                    _amount11TextBox.Attributes.Add("Style", "Width : 70px;");

                    _amount12TextBox.Attributes.Remove("ReadOnly");
                    _amount12TextBox.Attributes.Remove("Style");
                    _amount12TextBox.Attributes.Add("Style", "Width : 70px;");

                    _remarkTextBox.Attributes.Remove("ReadOnly");
                    _remarkTextBox.Attributes.Remove("Style");
                    _remarkTextBox.Attributes.Add("Style", "Width : 200px;");

                    _editButton.Visible = false;
                    _saveButton.Visible = true;
                }
                else if (e.CommandName == "Save")
                {
                    bool _result = false;
                    if (IsNumeric(_dayTextBox.Text) & IsNumeric(_amount01TextBox.Text) & IsNumeric(_amount02TextBox.Text) & IsNumeric(_amount03TextBox.Text) & IsNumeric(_amount04TextBox.Text) & IsNumeric(_amount05TextBox.Text) & IsNumeric(_amount06TextBox.Text) & IsNumeric(_amount07TextBox.Text) & IsNumeric(_amount08TextBox.Text) & IsNumeric(_amount09TextBox.Text) & IsNumeric(_amount10TextBox.Text) & IsNumeric(_amount11TextBox.Text) & IsNumeric(_amount12TextBox.Text))
                    {
                        _temp.Days = Convert.ToInt32(_dayTextBox.Text);
                        _temp.Amount01 = Convert.ToDecimal(_amount01TextBox.Text);
                        _temp.Amount02 = Convert.ToDecimal(_amount02TextBox.Text);
                        _temp.Amount03 = Convert.ToDecimal(_amount03TextBox.Text);
                        _temp.Amount04 = Convert.ToDecimal(_amount04TextBox.Text);
                        _temp.Amount05 = Convert.ToDecimal(_amount05TextBox.Text);
                        _temp.Amount06 = Convert.ToDecimal(_amount06TextBox.Text);
                        _temp.Amount07 = Convert.ToDecimal(_amount07TextBox.Text);
                        _temp.Amount08 = Convert.ToDecimal(_amount08TextBox.Text);
                        _temp.Amount09 = Convert.ToDecimal(_amount09TextBox.Text);
                        _temp.Amount10 = Convert.ToDecimal(_amount10TextBox.Text);
                        _temp.Amount11 = Convert.ToDecimal(_amount11TextBox.Text);
                        _temp.Amount12 = Convert.ToDecimal(_amount12TextBox.Text);
                        _temp.Remark = _remarkTextBox.Text;
                        _result = this._glBudgetBL.EditGLBudgetYearDt(_temp);

                        if (_result)
                        {
                            _dayTextBox.Attributes.Add("ReadOnly", "True");
                            _dayTextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 30px;");

                            _amount01TextBox.Attributes.Add("ReadOnly", "True");
                            _amount01TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                            _amount02TextBox.Attributes.Add("ReadOnly", "True");
                            _amount02TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                            _amount03TextBox.Attributes.Add("ReadOnly", "True");
                            _amount03TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                            _amount04TextBox.Attributes.Add("ReadOnly", "True");
                            _amount04TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                            _amount05TextBox.Attributes.Add("ReadOnly", "True");
                            _amount05TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                            _amount06TextBox.Attributes.Add("ReadOnly", "True");
                            _amount06TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                            _amount07TextBox.Attributes.Add("ReadOnly", "True");
                            _amount07TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                            _amount08TextBox.Attributes.Add("ReadOnly", "True");
                            _amount08TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                            _amount09TextBox.Attributes.Add("ReadOnly", "True");
                            _amount09TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                            _amount10TextBox.Attributes.Add("ReadOnly", "True");
                            _amount10TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                            _amount11TextBox.Attributes.Add("ReadOnly", "True");
                            _amount11TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                            _amount12TextBox.Attributes.Add("ReadOnly", "True");
                            _amount12TextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 70px;");

                            _remarkTextBox.Attributes.Add("ReadOnly", "True");
                            _remarkTextBox.Attributes.Add("Style", "Background : #CCCCCC; Width : 200px;");

                            _saveButton.Visible = false;
                            _editButton.Visible = true;
                        }
                        else
                        {
                            WarningLabel.Text = "You Failed Edit Data.";
                        }
                    }
                    else
                    {
                        WarningLabel.Text = "Day and Amount 0-12 must be Numeric Input.";
                    }
                }

            }
        }

        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            DateTime _transdate = DateTime.Now;
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string[] _yearRevisi = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey).Split('|');
            string _result = this._glBudgetBL.UnpostGLBudgetYearHd(Convert.ToInt32(_yearRevisi[0]), Convert.ToInt32(_yearRevisi[1]), HttpContext.Current.User.Identity.Name);

            if (_result == "Unposting Success")
            {
                //bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.GLBudget), this.TransNoTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
                //if (_result1 == true)
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

            this.ShowData();
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }

        protected void RevisiButton_Click(object sender, ImageClickEventArgs e)
        {
            String _result = this._glBudgetBL.GLBudgetYearCreateRevisi(Convert.ToInt32(this.YearTextBox.Text), HttpContext.Current.User.Identity.Name);
            if (_result != "")
                this.WarningLabel.Text = _result;
            else
                Response.Redirect(_homePage);
        }
    }
}