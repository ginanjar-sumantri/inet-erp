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

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCashReceive
{
    public partial class PettyCashReceivePrintPreview : PettyCashReceiveBase
    {
        private PettyBL _pettyBL = new PettyBL();
        private PermissionBL _permBL = new PermissionBL();

        private ReportFinanceBL _reportFinanceBL = new ReportFinanceBL();
        private string _reportPath1 = "PettyCashReceive/PettyCashPrintPreview.rdlc";

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
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

            this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._permPrintPreview == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.BeginDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.BeginDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.EndDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.EndDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ViewState[this._currPageKey] = 0;

                this.GoButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";
                this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/preview.jpg";
                this.PreviewButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/preview.jpg";

                this.SetButtonPermission();

                this.SetAttribute();
                this.ClearData();
            }
        }

        private void SetButtonPermission()
        {
            this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._permPrintPreview == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
                this.PreviewButton2.Visible = false;
            }
        }

        protected void SetAttribute()
        {
            this.BeginDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");

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

            Double q = this._pettyBL.RowsCountPettyCashReceiveHdPrintPreview(DateFormMapper.GetValue(this.BeginDateTextBox.Text), DateFormMapper.GetValue(this.EndDateTextBox.Text));
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

            this.ListRepeater.DataSource = this._pettyBL.GetListPettyCashReceiveHdPrintPreview(_prmReqPage, _maxrow, DateFormMapper.GetValue(this.BeginDateTextBox.Text), DateFormMapper.GetValue(this.EndDateTextBox.Text));
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowPage(_prmReqPage);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FINPettyReceiveHd _temp = (FINPettyReceiveHd)e.Item.DataItem;

                string _code = _temp.TransNmbr;

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

                string _tempURL = _printPreviewPageDetail + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.Attributes.Add("OnClick", "ShowPopUp('" + _tempURL + "','" + _width + "','" + _height + "','" + _resizable + "','" + _menuBar + "'); return false;");
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

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

                Literal _transactionNoLiteral = (Literal)e.Item.FindControl("TransactionNoLiteral");
                _transactionNoLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transactionDateLiteral = (Literal)e.Item.FindControl("TransactionDateLiteral");
                _transactionDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate);

                Literal _statusLiteral = (Literal)e.Item.FindControl("StatusLiteral");
                _statusLiteral.Text = PettyCashDataMapper.GetStatusText(_temp.Status);

                Literal _payTo = (Literal)e.Item.FindControl("PayToLiteral");
                _payTo.Text = HttpUtility.HtmlEncode(_temp.PayTo);

                Literal _reqPrint = (Literal)e.Item.FindControl("ReqPrintLiteral");
                int _tempReqPrint = this._pettyBL.GetReqPrintReceipt(_temp.TransNmbr);
                _reqPrint.Text = (_tempReqPrint == 0) ? "0" : _tempReqPrint.ToString();
            }
        }

        protected void GoButton_Click(object sender, ImageClickEventArgs e)
        {
            this.MenuPanel.Visible = true;
            this.ReportViewer1.Visible = false;
            this.CheckHidden.Value = "";

            this.ShowData(0);
        }
        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            bool _result = this._pettyBL.AddMultiFINPettyReceivePrintHistory(_tempSplit, this.DescriptionHiddenField.Value, HttpContext.Current.User.Identity.Name);

            if (_result == true)
            {
                this.MenuPanel.Visible = false;
                this.ReportViewer1.Visible = true;

                this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource1 = this._reportFinanceBL.PettyCashReceivePrintPreview(this.CheckHidden.Value, this.DescriptionHiddenField.Value, HttpContext.Current.User.Identity.Name);

                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
                this.ReportViewer1.DataBind();

                ReportParameter[] _reportParam = new ReportParameter[4];
                _reportParam[0] = new ReportParameter("TransNmbr", this.CheckHidden.Value, true);
                _reportParam[1] = new ReportParameter("Remark", this.DescriptionHiddenField.Value, true);
                _reportParam[2] = new ReportParameter("User", HttpContext.Current.User.Identity.Name, true);
                _reportParam[3] = new ReportParameter("Type", PettyDataMapper.GetType(Common.Enum.PettyType.Receipt).ToString(), true);
                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                this.ReportViewer1.LocalReport.Refresh();
            }
            else
            {
                this.WarningLabel.Text = "You Failed Preview";
            }
        }
    }
}