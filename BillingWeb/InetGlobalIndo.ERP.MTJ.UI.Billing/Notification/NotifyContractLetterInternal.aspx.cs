using System;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web.UI;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Notification
{
    public partial class NotifyContractLetterInternal : NotifyBase
    {
        private NotificationBL _notificationBL = new NotificationBL();
        private SalesConfirmationBL _scBL = new SalesConfirmationBL();
        private PermissionBL _permBL = new PermissionBL();

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

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDCLInternal, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleNotifyContractLetterInternalLiteral;

                this.ViewState[this._currPageKey] = 0;

                this.SearchImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/searchBtn.jpg";
                this.SendEmailImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/send_email.jpg";

                DateTime _now = DateTime.Now;
                this.DateTextBox.Text = DateFormMapper.GetValue(_now);

                this.ClearLabel();
                this.ShowData(0);
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._notificationBL.RowsCountCLNotYetApproved(DateFormMapper.GetValue(this.DateTextBox.Text), this.PendingStatusDropDownList.SelectedValue, EmailNotificationID.ContractLetterNotYetApprovedInternal);
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

                //this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                //                                          select _query;
                //this.DataPagerBottomRepeater.DataBind();
            }
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

        private void ShowData(Int32 _prmCurrentPage)
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuIDCLInternal, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._notificationBL.GetListCLNotYetApproved(_prmCurrentPage, _maxrow, DateFormMapper.GetValue(this.DateTextBox.Text), this.PendingStatusDropDownList.SelectedValue, EmailNotificationID.ContractLetterNotYetApprovedInternal);
            }
            this.ListRepeater.DataBind();

            this.AllHidden.Value = this._notificationBL.GetListCLNotYetApproved(DateFormMapper.GetValue(this.DateTextBox.Text), this.PendingStatusDropDownList.SelectedValue, EmailNotificationID.ContractLetterNotYetApprovedInternal);

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                BILTrSalesConfirmation _temp = (BILTrSalesConfirmation)e.Item.DataItem;
                string _code = _temp.TransNmbr.ToString();

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

                ImageButton _pendingButton = (ImageButton)e.Item.FindControl("PendingImageButton");
                _pendingButton.CommandArgument = _code;
                if (_temp.FgPending)
                {
                    _pendingButton.CommandName = "Resume";
                    _pendingButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/resumeButton.jpg";
                }
                else
                {
                    _pendingButton.CommandName = "Pending";
                    _pendingButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/pendingButton.jpg";
                }

                Literal _scNoLiteral = (Literal)e.Item.FindControl("SCNoLiteral");
                _scNoLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _dateLiteral = (Literal)e.Item.FindControl("DateLiteral");
                _dateLiteral.Text = HttpUtility.HtmlEncode(DateFormMapper.GetValue(_temp.TransDate));

                Literal _baNoLiteral = (Literal)e.Item.FindControl("BALiteral");
                _baNoLiteral.Text = HttpUtility.HtmlEncode(_temp.BAFileNmbr);

                Literal _baDateLiteral = (Literal)e.Item.FindControl("BADateLiteral");
                _baDateLiteral.Text = HttpUtility.HtmlEncode(DateFormMapper.GetValue(_temp.BATransDate));

                Literal _custLiteral = (Literal)e.Item.FindControl("CustomerLiteral");
                _custLiteral.Text = HttpUtility.HtmlEncode(_temp.CompanyName);

                Literal _statusLiteral = (Literal)e.Item.FindControl("StatusLiteral");
                _statusLiteral.Text = HttpUtility.HtmlEncode(SalesConfirmationDataMapper.GetTrueFalse(_temp.FgPending));
            }
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Resume")
            {
                this.ClearLabel();

                Boolean _result = this._scBL.UpdateFgPending(e.CommandArgument.ToString(), false, "");

                if (_result == true)
                {
                    this.WarningLabel.Text = e.CommandArgument.ToString() + " resumed";
                }
                else
                {
                    this.WarningLabel.Text = e.CommandArgument.ToString() + " failed to resume";
                }

                this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
            }
            else if (e.CommandName == "Pending")
            {
                this.ClearLabel();

                Boolean _result = this._scBL.UpdateFgPending(e.CommandArgument.ToString(), true, "");

                if (_result == true)
                {
                    this.WarningLabel.Text = e.CommandArgument.ToString() + " changed to Pending";
                }
                else
                {
                    this.WarningLabel.Text = e.CommandArgument.ToString() + " failed to pending";
                }

                this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
            }
        }

        protected void SearchImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.ShowData(0);
        }

        protected void PendingStatusDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowData(0);
        }

        protected void SendEmailImageButton_Click(object sender, ImageClickEventArgs e)
        {
            String _result = "";

            if (this.GrapAllCheckBox.Checked == false)
            {
                _result = _notificationBL.SendEmail(this.CheckHidden.Value, EmailNotificationID.BANotYetApproved);
            }
            else
            {
                _result = _notificationBL.SendEmail(this.AllHidden.Value, EmailNotificationID.BANotYetApproved);
            }

            if (_result != "")
            {
                this.WarningLabel.Text = _result;
            }
            else
            {
                this.CheckHidden.Value = "";
                this.WarningLabel.Text = "You succesfully sent email(s)";
            }

            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }
    }
}