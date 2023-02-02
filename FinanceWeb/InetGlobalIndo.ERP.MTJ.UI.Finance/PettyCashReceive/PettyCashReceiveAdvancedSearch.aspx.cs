using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCashReceive
{
    public partial class PettyCashReceiveAdvancedSearch : PettyCashReceiveBase
    {
        private PettyBL _pettyBL = new PettyBL();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerBottomButton.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.DateFromLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateFromTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DateToLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.DateToTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = "Petty Cash In : Advanced Search";

                this.GoImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";

                this.ViewState[this._currPageKey] = 0;

                this.ShowStatus();
                //this.ShowPetty();

                this.ClearData();
                this.ShowData(0);
            }
        }

        protected void ClearData()
        {
            this.DateFromTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.DateToTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.DateFromTextBox.Attributes.Add("ReadOnly", "True");
            this.DateToTextBox.Attributes.Add("ReadOnly", "True");
            this.TransactionNoTextBox.Text = "";
            this.PayToText.Text = "";
            this.FileNoTextBox.Text = "";
        }

        protected void ShowStatus()
        {
            this.StatusRadioButtonList.Items.Add(new ListItem("All", ""));
            this.StatusRadioButtonList.Items.Add(new ListItem(PettyCashDataMapper.GetStatusText(PettyCashDataMapper.GetStatus(TransStatus.OnHold)), PettyCashDataMapper.GetStatus(TransStatus.OnHold).ToString()));
            this.StatusRadioButtonList.Items.Add(new ListItem(PettyCashDataMapper.GetStatusText(PettyCashDataMapper.GetStatus(TransStatus.WaitingForApproval)), PettyCashDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString()));
            this.StatusRadioButtonList.Items.Add(new ListItem(PettyCashDataMapper.GetStatusText(PettyCashDataMapper.GetStatus(TransStatus.Posted)), PettyCashDataMapper.GetStatus(TransStatus.Posted).ToString()));
            this.StatusRadioButtonList.Items[0].Selected = true;
        }

        public void ShowPetty()
        {
            //this.PettyDDL.DataTextField = "PettyName";
            //this.PettyDDL.DataValueField = "PettyCode";
            //this.PettyDDL.DataSource = this._pettyBL.GetList();
            //this.PettyDDL.DataBind();
            //this.PettyDDL.Items.Insert(0, new ListItem("[Choose One]", ""));
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._pettyBL.RowsCountReceiveAdvancedSearch(this.TransactionNoTextBox.Text, this.FileNoTextBox.Text, DateFormMapper.GetValue(this.DateFromTextBox.Text), DateFormMapper.GetValue(this.DateToTextBox.Text), this.TypeDDL.SelectedValue, this.PayToText.Text, this.StatusRadioButtonList.SelectedValue);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

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

                this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                                                          select _query;
                this.DataPagerBottomRepeater.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();

                this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                                                          select _query;
                this.DataPagerBottomRepeater.DataBind();
            }
        }

        private void ShowData(Int32 _prmCurrentPage)
        {
            this._page = _prmCurrentPage;

            this.ListRepeater.DataSource = this._pettyBL.GetListReceiveAdvancedSearch(_prmCurrentPage, _maxrow, this.TransactionNoTextBox.Text, this.FileNoTextBox.Text, DateFormMapper.GetValue(this.DateFromTextBox.Text), DateFormMapper.GetValue(this.DateToTextBox.Text), this.TypeDDL.SelectedValue, this.PayToText.Text, this.StatusRadioButtonList.SelectedValue);
            this.ListRepeater.DataBind();

            this.ShowPage(_prmCurrentPage);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FINPettyReceiveHd _temp = (FINPettyReceiveHd)e.Item.DataItem;

                string _code = _temp.TransNmbr;

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                if (_temp.Status.ToString().Trim().ToLower() == PettyCashDataMapper.GetStatus(TransStatus.Posted).ToString().Trim().ToLower())
                {
                    _editButton.Visible = false;
                }
                else
                {
                    _editButton.PostBackUrl = this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                    _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
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

                Literal _transaction = (Literal)e.Item.FindControl("TransactionNoLiteral");
                _transaction.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _fileNo = (Literal)e.Item.FindControl("FileNoLiteral");
                _fileNo.Text = HttpUtility.HtmlEncode(_temp.FileNmbr);

                Literal _transactionDate = (Literal)e.Item.FindControl("TransactionDateLiteral");
                DateTime _tempDate = _temp.TransDate;
                _transactionDate.Text = _tempDate.Day + "/" + _tempDate.Month + "/" + _tempDate.Year;

                Literal _type = (Literal)e.Item.FindControl("TypeLiteral");
                _type.Text = PettyCashDataMapper.GetTypeText(PettyCashDataMapper.GetType(_temp.FgType));

                Literal _status = (Literal)e.Item.FindControl("StatusLiteral");
                _status.Text = HttpUtility.HtmlEncode((_temp.Status).ToString());

                Literal _currency = (Literal)e.Item.FindControl("CurrencyLiteral");
                _currency.Text = HttpUtility.HtmlEncode(_temp.CurrencyName);

                Literal _forex = (Literal)e.Item.FindControl("ForexRateLiteral");
                _forex.Text = HttpUtility.HtmlEncode((_temp.ForexRate).ToString("#.###,##"));

                Literal _payTo = (Literal)e.Item.FindControl("PayToLiteral");
                _payTo.Text = HttpUtility.HtmlEncode(_temp.PayTo);
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

        protected void GoImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ViewState[this._currPageKey] = 0;

            this.ShowData(0);
        }
    }
}