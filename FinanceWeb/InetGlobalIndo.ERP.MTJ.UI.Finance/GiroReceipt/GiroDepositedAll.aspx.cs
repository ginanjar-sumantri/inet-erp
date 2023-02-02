using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroReceipt
{
    public partial class GiroDepositedAll : GiroReceiptBase
    {
        private FINGiroInBL _finGiroInBL = new FINGiroInBL();
        private PermissionBL _permBL = new PermissionBL();
        private BankBL _bankBL = new BankBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private CompanyConfig _companyConfigBL = new CompanyConfig();

        private string _reportPath1 = "PettyCash/PettyCashPrintPreview.rdlc";

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        //private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl00_ListCheckBox";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _currPageKey = "CurrentPage";

        private string _width = "600";
        private string _height = "500";
        private string _resizable = "1";
        private string _menuBar = "0";

        private string _confirmTitle = "Warning : Your request for print will be recorded.";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.BeginDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.BeginDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.DateTextBoxt.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ViewState[this._currPageKey] = 0;

                this.GoButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";
                this.SetorAllImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/setor_all.jpg";
                this.SetorImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/setor.jpg";

                this.SetorPanel.Visible = false;
                this.ButtonDepositedPanel.Visible = false;
                this.SetAttribute();
                this.ShowSetorBankReceipt();
                this.ShowBankSetor();
                this.ClearData();
            }
        }

        public void ShowSetorBankReceipt()
        {
            this.BankReceiptDDL.Items.Clear();
            this.BankReceiptDDL.DataTextField = "PayName";
            this.BankReceiptDDL.DataValueField = "PayCode";
            this.BankReceiptDDL.DataSource = this._paymentBL.GetListDDLGiroReceipt();
            this.BankReceiptDDL.DataBind();
            this.BankReceiptDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowBankSetor()
        {
            this.BankSetorDDL.Items.Clear();
            this.BankSetorDDL.DataTextField = "BankName";
            this.BankSetorDDL.DataValueField = "BankCode";
            this.BankSetorDDL.DataSource = this._bankBL.GetList();
            this.BankSetorDDL.DataBind();
            this.BankSetorDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SetAttribute()
        {
            DateTime _now = DateTime.Now;

            this.BeginDateTextBox.Text = DateFormMapper.GetValue(_now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(_now);
            this.DateTextBoxt.Text = DateFormMapper.GetValue(_now);

            this.BeginDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
            this.DateTextBoxt.Attributes.Add("ReadOnly", "True");

            //this.PreviewButton.Attributes.Add("OnClick", "return ConfirmFillDescription('" + this.DescriptionHiddenField.ClientID + "', '" + _confirmTitle + "');");
            //this.PreviewButton2.Attributes.Add("OnClick", "return ConfirmFillDescription('" + this.DescriptionHiddenField.ClientID + "', '" + _confirmTitle + "');");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();

            DateTime _now = DateTime.Now;

            this.BeginDateTextBox.Text = DateFormMapper.GetValue(_now);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(_now);
            this.DescriptionHiddenField.Value = "";
        }

        private void ShowPage(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            Int32 _pageNumberElement = 0;
            decimal min = 0, max = 0;

            Double q = this._finGiroInBL.RowsCountFINGiroInForDepositedAll(DateFormMapper.GetValue(this.BeginDateTextBox.Text), DateFormMapper.GetValue(this.EndDateTextBox.Text));
            q = System.Math.Ceiling(q / (double)_maxrow);

            if (_prmCurrentPage - _maxlength > 0)
                min = _prmCurrentPage - _maxlength;
            else
                min = 0;

            if (_prmCurrentPage + _maxlength < q)
                max = _prmCurrentPage + _maxlength + 1;

            else
                max = Convert.ToDecimal(q);

            _pageNumber = new Int32[Convert.ToInt32(max - min)];

            decimal i = min;
            //_min = min;
            //_max = max;

            if (min > 0)
            {
                i++;
                _pageNumber[0] = Convert.ToInt32(i - 1);
                _pageNumberElement++;
            }

            for (; i < max; i++)
            {
                _pageNumber[_pageNumberElement] = Convert.ToInt32(i);
                _pageNumberElement++;
            }

            //if (max < (decimal)q)
            //    _pageNumber[_pageNumberElement] = Convert.ToInt32(i);

            this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                                                      select _query;
            this.DataPagerBottomRepeater.DataBind();

            this.DataPagerTopRepeater.DataSource = this.DataPagerBottomRepeater.DataSource;
            this.DataPagerTopRepeater.DataBind();
        }

        protected void DataPagerRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey]) == _pageNumber)
                {
                    Label _pageNumberLabel = (Label)e.Item.FindControl("PageNumberLabel");
                    _pageNumberLabel.Text = "[<b>" + (_pageNumber + 1).ToString() + "</b>]";
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton");
                    _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                    _pageNumberLinkButton.CommandName = "DataPager";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();
                }
            }
        }

        protected void DataPagerRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowData(Convert.ToInt32(e.CommandArgument));
            }
        }

        public void ShowData(Int32 _prmReqPage)
        {
            this.TempHidden.Value = "";

            _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            //this._page = Convert.ToInt32((_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey) == "") ? "0" : Rijndael.Decrypt(_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey), ApplicationConfig.EncryptionKey));

            this.ListRepeater.DataSource = this._finGiroInBL.GetListFINGiroInForDepositedAll(_prmReqPage, _maxrow, DateFormMapper.GetValue(this.BeginDateTextBox.Text), DateFormMapper.GetValue(this.EndDateTextBox.Text));
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowPage(_prmReqPage);
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
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FINGiroIn _temp = (FINGiroIn)e.Item.DataItem;
                string _code = _temp.GiroNo.ToString();

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
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "', 'false')");
                _listCheckbox.Checked = this.IsChecked(_code);

                string _tempURL = _depositedAllViewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.Attributes.Add("OnClick", "ShowPopUp('" + _tempURL + "','" + _width + "','" + _height + "','" + _resizable + "','" + _menuBar + "'); return false;");
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                if (this._permView == PermissionLevel.NoAccess)
                {
                    _viewButton.Visible = false;
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

                Literal _giroNo = (Literal)e.Item.FindControl("GiroNoLiteral");
                _giroNo.Text = HttpUtility.HtmlEncode(_temp.GiroNo);

                Literal _fileNo = (Literal)e.Item.FindControl("FileNoLiteral");
                _fileNo.Text = HttpUtility.HtmlEncode(_temp.FileNmbr);

                Literal _receiptDate = (Literal)e.Item.FindControl("ReceiptDateLiteral");
                _receiptDate.Text = DateFormMapper.GetValue(_temp.ReceiptDate);

                Literal _receiptNo = (Literal)e.Item.FindControl("ReceiptNoLiteral");
                _receiptNo.Text = HttpUtility.HtmlEncode(_temp.FileNmbr);

                Literal _status = (Literal)e.Item.FindControl("StatusLiteral");
                _status.Text = HttpUtility.HtmlEncode(GiroReceiptDataMapper.GetStatusText(_temp.Status));

                Literal _dueDate = (Literal)e.Item.FindControl("DueDateLiteral");
                _dueDate.Text = DateFormMapper.GetValue(_temp.DueDate);
            }
        }

        protected void GoButton_Click(object sender, ImageClickEventArgs e)
        {
            this.SetorPanel.Visible = true;
            this.ButtonDepositedPanel.Visible = true;
            this.CheckHidden.Value = "";

            this.ShowData(0);
        }

        protected void SetorImageButton_Click(object sender, ImageClickEventArgs e)
        {
            string _result = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _user = HttpContext.Current.User.Identity.Name;

            _result = this._finGiroInBL.DepositedAllFINGiroIn(_tempSplit, DateFormMapper.GetValue(this.DateTextBoxt.Text), this.BankReceiptDDL.SelectedValue, this.BankSetorDDL.SelectedValue, Convert.ToDecimal(this.BankChargeTextBox.Text), _user);

            if (_result == "")
            {
                Response.Redirect(_homePage);
            }
            else
            {
                this.ShowData(0);
                this.WarningLabel.Text = _result;
            }

        }

        protected void SetorAllImageButton_Click(object sender, ImageClickEventArgs e)
        {
            string _result = "";
            List<FINGiroIn> _finGiroIn = this._finGiroInBL.GetListForDepositedAll();
            string _user = HttpContext.Current.User.Identity.Name;

            _result = this._finGiroInBL.DepositedAllFINGiroIn(_finGiroIn, DateFormMapper.GetValue(this.DateTextBoxt.Text), this.BankReceiptDDL.SelectedValue, this.BankSetorDDL.SelectedValue, Convert.ToDecimal(this.BankChargeTextBox.Text), _user);

            if (_result == "")
            {
                Response.Redirect(_homePage);
            }
            else
            {
                this.ShowData(0);
                this.WarningLabel.Text = _result;
            }
        }
    }
}