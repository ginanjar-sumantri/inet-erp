using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.EmployeeAuthor
{
    public partial class EmployeeAuthorView : EmployeeAuthorBase
    {
        private AccountBL _accountBL = new AccountBL();
        private EmployeeBL _empBL = new EmployeeBL();
        private TransTypeBL _transTypeBL = new TransTypeBL();
        private PermissionBL _permBL = new PermissionBL();

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

        //private string awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        //private string akhir = "_ListCheckBox";
        //private string cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        //private string _temphidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

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
            this.DataPagerBottomButton.Attributes.Add("Style", "visibility: hidden;");

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.ViewState[this._currPageKey] = 0;

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SetDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/set_detail.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowTransTypeDropdownlist();
                this.ShowData(0);
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.SetDetailButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._accountBL.RowsCountEmployeeAuthorizationView(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.TransTypeDropDownList.SelectedValue);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        //private void ShowPage()
        //{
        //    double q = this._empBL.RowsCountEmployeeAuthorization(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), "", "");

        //    decimal min = 0, max = 0;
        //    q = System.Math.Ceiling(q / (double)_maxrow);

        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //    if (_page - _maxlength > 0)
        //    {
        //        min = _page - _maxlength;
        //    }
        //    else
        //    {
        //        min = 0;
        //    }

        //    if (_page + _maxlength < q)
        //    {
        //        max = _page + _maxlength + 1;
        //    }
        //    else
        //    {
        //        max = Convert.ToDecimal(q);
        //    }

        //    decimal i = min;

        //    if (min > 0)
        //    {
        //        sb.Append("<a href='" + this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt((i - 1).ToString(), ApplicationConfig.EncryptionKey)) + "'>" + ("<<< ") + "</a>&nbsp;");
        //    }

        //    for (; i < max; i++)
        //    {
        //        if (i == _page)
        //        {
        //            sb.Append("[<b>" + (i + 1) + "</b>]&nbsp;");
        //        }
        //        else
        //        {
        //            sb.Append("<a href='" + this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(i.ToString(), ApplicationConfig.EncryptionKey)) + "'>" + (i + 1) + "</a>&nbsp;");
        //        }
        //    }

        //    if (max < (decimal)q)
        //    {
        //        sb.Append("<a href='" + this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(i.ToString(), ApplicationConfig.EncryptionKey)) + "'>" + (" >>>") + "</a>&nbsp;");
        //    }

        //    this.PageLabel.Text = sb.ToString();
        //    this.PageLabel2.Text = sb.ToString();
        //}

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

                this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                                                          select _query;
                this.DataPagerBottomRepeater.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater.DataSource = null;
                this.DataPagerTopRepeater.DataBind();

                this.DataPagerBottomRepeater.DataSource = null;
                this.DataPagerBottomRepeater.DataBind();
            }
        }

        protected void ShowTransTypeDropdownlist()
        {
            this.TransTypeDropDownList.Items.Clear();
            this.TransTypeDropDownList.DataSource = this._transTypeBL.GetList();
            this.TransTypeDropDownList.DataValueField = "TransTypeCode";
            this.TransTypeDropDownList.DataTextField = "TransTypeName";
            this.TransTypeDropDownList.DataBind();
            this.TransTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", ""));
        }

        protected void ShowData(Int32 _prmCurrentPage)
        {
            this.EmployeeTextBox.Text = _empBL.GetEmpNameByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            //this._page = Convert.ToInt32((_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey) == "") ? "0" : Rijndael.Decrypt(_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey), ApplicationConfig.EncryptionKey));
            this._page = _prmCurrentPage;

            //this.ListRepeater.DataSource = this._empBL.GetListTransTypeAccount(this._page, _maxrow, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.TransTypeDropDownList.SelectedValue);
            this.ListRepeater.DataSource = this._accountBL.GetListEmpAuthTransTypeAccount(_prmCurrentPage, _maxrow, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.TransTypeDropDownList.SelectedValue);
            this.ListRepeater.DataBind();

            this.ShowPage(_prmCurrentPage);
        }

        protected void SetDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = "0";

            this.ClearLabel();
            this.ShowData(0);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Master_EmployeeAuthorization _temp = (Master_EmployeeAuthorization)e.Item.DataItem;

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterListTemplate");
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

                Literal _transTypeLiteral = (Literal)e.Item.FindControl("TransTypeLiteral");
                _transTypeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransTypeCode);

                Literal _transTypeNameLiteral = (Literal)e.Item.FindControl("TransTypeNameLiteral");
                _transTypeNameLiteral.Text = HttpUtility.HtmlEncode(_temp.TransTypeName);

                Literal _accountLiteral = (Literal)e.Item.FindControl("AccountLiteral");
                _accountLiteral.Text = HttpUtility.HtmlEncode(_temp.Account);

                Literal _accountNameLiteral = (Literal)e.Item.FindControl("AccountNameLiteral");
                _accountNameLiteral.Text = HttpUtility.HtmlEncode(_temp.AccountName);
            }
        }

        protected void TransTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState[this._currPageKey] = "0";

            this.ClearLabel();
            this.ShowData(0);
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

        protected void DataPagerBottomButton_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerBottomRepeater.Controls)
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
    }
}
