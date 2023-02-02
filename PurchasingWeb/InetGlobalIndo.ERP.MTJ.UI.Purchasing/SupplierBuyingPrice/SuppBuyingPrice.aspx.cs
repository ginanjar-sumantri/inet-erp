using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Linq;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.SupplierBuyingPrice
{
    public partial class SuppBuyingPrice : SuppBuyingPriceBase
    {
        private SuppBuyingPriceBL _suppBuyingPriceBL = new SuppBuyingPriceBL();
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

        private string _app = "Aplikasi";

        private string _currPageKey = "CurrentPage";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00_DefaultBodyContentPlaceHolder_TempHidden";

        private string _awalAdd = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater1_ctl";
        private string _akhirAdd = "_ListCheckBox1";
        private string _cboxAdd = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox1";
        private string _tempHiddenAdd = "ctl00_DefaultBodyContentPlaceHolder_TempHidden2";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);
            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.ViewState[this._currPageKey] = 0;

                this.btnSearchSuppNo.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findSupplier&configCode=supplier','_popSearch','width=750,height=300,toolbar=0,location=0,status=0,scrollbars=1')";
                String spawnCust = "<script language='JavaScript'>\n";
                ////////////////////DECLARE FUNCTION FOR CATCHING SJ NO SEARCH
                spawnCust += "function findSupplier(x) {\n";
                spawnCust += "dataArray = x.split ('|') ;\n";
                spawnCust += "document.getElementById('" + this.SupplierTextBox.ClientID + "').value = dataArray [0];\n";
                spawnCust += "}\n";

                this.btnSearchProdNo.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findMsProduct&configCode=msproduct','_popSearch','width=750,height=300,toolbar=0,location=0,status=0,scrollbars=1')";
                ////////////////////DECLARE FUNCTION FOR CATCHING SJ NO SEARCH
                spawnCust += "function findMsProduct(x) {\n";
                spawnCust += "dataArray = x.split ('|') ;\n";
                spawnCust += "document.getElementById('" + this.ProductTextBox.ClientID + "').value = dataArray [0];\n";
                spawnCust += "}\n";
                spawnCust += "</script>\n";
                this.javascriptReceiver.Text = spawnCust;

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ViewStokButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SupplierTextBox.Attributes.Add("ReadOnly", "True");
                this.ProductTextBox.Attributes.Add("ReadOnly", "True");
                //this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                //this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                //this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                //this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                //this.AddButton1.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                //this.DeleteButton1.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.ClearLabel();
                this.Panel2.Visible = false;
            
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
            this.SupplierTextBox.Text = "";
            this.ProductTextBox.Text = "";
        }

        protected void ShowData(Int32 _prmCurrentPage)
        {
            this._page = _prmCurrentPage;

            this.ListRepeater.DataSource = this._suppBuyingPriceBL.getSellingPrice(_prmCurrentPage, _maxrow, this.SupplierTextBox.Text, this.ProductTextBox.Text, this.sortField.Value, Convert.ToBoolean(this.ascDesc.Value));
            this.ListRepeater.DataBind();

            this.ShowPage(_prmCurrentPage);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                PRCSuppBuyingPrice _prcSuppBuyingPrice = (PRCSuppBuyingPrice)e.Item.DataItem;

                //string _code = _msCustContact.CustCode.ToString();
                //string _itemCode = _msCustContact.ItemNo.ToString();

                //if (this.TempHidden.Value == "")
                //{
                //    this.TempHidden.Value += _itemCode;
                //}
                //else
                //{
                //    this.TempHidden.Value += "," + _itemCode;
                //}

                Literal _custCodeLiteral = (Literal)e.Item.FindControl("SuppCodeLiteral");
                _custCodeLiteral.Text = Convert.ToString(_prcSuppBuyingPrice.SuppCode);

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                _productCodeLiteral.Text = Convert.ToString(_prcSuppBuyingPrice.ProductCode);

                Literal _transNmbrLiteral = (Literal)e.Item.FindControl("TransNmbrLiteral");
                _transNmbrLiteral.Text = Convert.ToString(_prcSuppBuyingPrice.TransNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("TransDateLiteral");
                _transDateLiteral.Text = DateFormMapper.GetValue(_prcSuppBuyingPrice.TransDate);

                Literal _unitCodeLiteral = (Literal)e.Item.FindControl("UnitCodeLiteral");
                _unitCodeLiteral.Text = HttpUtility.HtmlEncode(_prcSuppBuyingPrice.UnitCode);

                Literal _currCodeLiteral = (Literal)e.Item.FindControl("CurrCodeLiteral");
                _currCodeLiteral.Text = HttpUtility.HtmlEncode(_prcSuppBuyingPrice.CurrCode);

                Literal _amoununtForexLiteral = (Literal)e.Item.FindControl("AmountForexLiteral");
                _amoununtForexLiteral.Text = HttpUtility.HtmlEncode(_prcSuppBuyingPrice.AmountForex.ToString("#,##0.00"));

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                //CheckBox _cb = (CheckBox)e.Item.FindControl("ListCheckBox");
                //_cb.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + "," + _cb.ClientID + ",'" + _itemCode + "', '" + _awal + "' , '" + _akhir + "', '" + _cbox + "' )");

                //ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                //_viewButton.PostBackUrl = _viewDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + _itemCodeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_itemCode, ApplicationConfig.EncryptionKey));
                //_viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                //this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                //if (this._permView == PermissionLevel.NoAccess)
                //{
                //    _viewButton.Visible = false;
                //}

                //ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton2");
                //_editButton.PostBackUrl = _editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + _itemCodeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_itemCode, ApplicationConfig.EncryptionKey));
                //_editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

                //this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                //if (this._permEdit == PermissionLevel.NoAccess)
                //{
                //    _editButton.Visible = false;
                //}

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
            }
        }

        protected void ViewStokButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel2.Visible = true;
            this.ShowData(0);
        }

        protected void ResertButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel2.Visible = false;
            this.ClearLabel();
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

        protected void field1_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Supplier Code")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Supplier Code";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }

        protected void field2_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Product Code")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Product Code";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }

        protected void field3_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Trans Number")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Trans Number";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }

        protected void field4_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Trans Date")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Trans Date";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }

        protected void field5_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Unit Code")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Unit Code";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }

        protected void field6_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Curr Code")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Curr Code";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }

        protected void field7_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Amount Forex")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Amount Forex";
                this.ascDesc.Value = "true";
            }
            this.ShowData(Convert.ToInt32(this.ViewState[this._currPageKey]));
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._suppBuyingPriceBL.RowsCountSellingPrice(this.SupplierTextBox.Text , this.ProductTextBox.Text);
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
            }
            else
            {
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();


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
}
}