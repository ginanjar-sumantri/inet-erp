using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Web.UI.HtmlControls;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Report
{
    public partial class ProductListing : ReportBase
    {
        private ReportStockControlBL _report = new ReportStockControlBL();
        private ProductBL _productBL = new ProductBL();
        private StockBalanceBL _stcBalanceBL = new StockBalanceBL();
        private PermissionBL _permBL = new PermissionBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _currPageKey = "CurrentPage";

        private int _page;
        private int _maxrow = 20;
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private int?[] _navMark2 = { null, null, null, null };
        private bool _flag2 = true;
        private bool _nextFlag2 = false;
        private bool _lastFlag2 = false;

        private string _currPageKey2 = "CurrentPage";

        private int _page2;
        private int _maxrow2 = 20;
        private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no2 = 0;
        private int _nomor2 = 0;

        private string _reportPath0 = "Report/ProductListingSummaryPerAllProduct.rdlc";
        private string _reportPath1 = "Report/ProductListingSummaryPerProductGroup.rdlc";
        private string _reportPath2 = "Report/ProductListingSummaryPerProductSubGroup.rdlc";
        private string _reportPath3 = "Report/ProductListingSummaryPerProductType.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDProductListing, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerButton1.Attributes.Add("Style", "visibility: hidden;");

            string _userAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");

            if (_userAgent.Contains("MSIE 7.0"))
                this.StokBalancePanel.Attributes.Add("style", "margin-bottom: 30px;");

            if (!Page.IsPostBack == true)
            {
                this.DateFromLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateFromTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DateToLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.DateToTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.btnSearchPINo.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProductInformation&configCode=product_information','_popSearch','width=1200,height=1000,toolbar=0,location=0,status=0,scrollbars=1')";
                String spawnJS = "<script language='JavaScript'>\n";
                ////////////////////DECLARE FUNCTION FOR CATCHING SJ NO SEARCH
                spawnJS += "function findProductInformation(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.ProductCodeTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.getElementById('" + this.ProductNameTextBox.ClientID + "').value = dataArray [1];\n";
                spawnJS += "document.getElementById('" + this.ProductSpesificationTextBox.ClientID + "').value = dataArray [2];\n";
                spawnJS += "document.getElementById('" + this.ProductTypeTextBox.ClientID + "').value = dataArray [3];\n";
                spawnJS += "document.getElementById('" + this.ProductSubGroupTextBox.ClientID + "').value = dataArray [4];\n";
                spawnJS += "document.getElementById('" + this.PriceGroupTextBox.ClientID + "').value = dataArray [5];\n";
                spawnJS += "document.getElementById('" + this.SellingPriceTextBox.ClientID + "').value = dataArray [6];\n";
                spawnJS += "}\n";
                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = "Product Listing Summary Report";

                this.ViewStokButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view2.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.MenuPanel.Visible = true;
                this.StokBalancePanel.Visible = false;
                this.StokBalancePanel1.Visible = false;
                this.DateTr.Visible = false;

                this.SetAttribute();

                this.ClearData();
            }
        }

        public void SetAttribute()
        {
            this.ProductCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.ProductNameTextBox.Attributes.Add("ReadOnly", "True");
            this.ProductSpesificationTextBox.Attributes.Add("ReadOnly", "True");
            this.ProductTypeTextBox.Attributes.Add("ReadOnly", "True");
            this.ProductSubGroupTextBox.Attributes.Add("ReadOnly", "True");
            this.PriceGroupTextBox.Attributes.Add("ReadOnly", "True");
            this.SellingPriceTextBox.Attributes.Add("ReadOnly", "True");
            this.DateFromTextBox.Attributes.Add("ReadOnly", "True");
            this.DateToTextBox.Attributes.Add("ReadOnly", "True");
            this.DateFromTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.DateToTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
        }

        public void ClearData()
        {
            this.ProductCodeTextBox.Text = "";
            this.ProductNameTextBox.Text = "";
            this.ProductSpesificationTextBox.Text = "";
            this.ProductTypeTextBox.Text = "";
            this.ProductSubGroupTextBox.Text = "";
            this.PriceGroupTextBox.Text = "";
            this.SellingPriceTextBox.Text = "";
        }

        protected void ViewStokButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ViewByDDL.SelectedValue == "0")
            {
                if (this.ProductCodeTextBox.Text != "")
                {
                    this.MenuPanel.Visible = false;
                    this.StokBalancePanel.Visible = true;
                    this.StokBalancePanel1.Visible = false;

                    this.StokBalanceRepeater.DataSource = this._stcBalanceBL.GetListStokBalance(0, _maxrow, this.ProductCodeTextBox.Text);
                    this.StokBalanceRepeater.DataBind();

                    this.ShowPageStockBalance(0);
                }
            }
            else if (this.ViewByDDL.SelectedValue == "1")
            {
                if (this.DateFromTextBox.Text != "" || this.DateToTextBox.Text != "")
                {
                    if (this.ProductCodeTextBox.Text != "")
                    {
                        DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.DateFromTextBox.Text).Year, DateFormMapper.GetValue(this.DateFromTextBox.Text).Month, DateFormMapper.GetValue(this.DateFromTextBox.Text).Day, 0, 0, 0);
                        DateTime _endDateTime = new DateTime(DateFormMapper.GetValue(this.DateToTextBox.Text).Year, DateFormMapper.GetValue(this.DateToTextBox.Text).Month, DateFormMapper.GetValue(this.DateToTextBox.Text).Day, 23, 59, 59);

                        this.MenuPanel.Visible = false;
                        this.StokBalancePanel.Visible = false;
                        this.StokBalancePanel1.Visible = true;

                        this.ProductInformationRepeater.DataSource = this._productBL.GetListProductInformation(0, _maxrow2, this.ProductCodeTextBox.Text, _startDateTime, _endDateTime);
                        this.ProductInformationRepeater.DataBind();
                        this.ProductLabel.Text = this.ProductCodeTextBox.Text + " - " + this.ProductNameTextBox.Text;

                        this.ShowPageProductInformation(0);
                    }
                }
                else
                {
                    this.WarningLabel.Text = "Please Insert Table Date";
                }
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void StokBalanceRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                STCStockValue _temp = (STCStockValue)e.Item.DataItem;

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterStokBalance");
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

                Literal _productName = (Literal)e.Item.FindControl("ProductNameLiteral");
                _productName.Text = HttpUtility.HtmlEncode(_temp.ProductName);

                Literal _locationName = (Literal)e.Item.FindControl("LocationNameLiteral");
                _locationName.Text = HttpUtility.HtmlEncode(_temp.LocationName);

                Literal _wrhsCode = (Literal)e.Item.FindControl("WrhsCodeLiteral");
                _wrhsCode.Text = HttpUtility.HtmlEncode(_temp.WrhsCode);

                Literal _wrhsName = (Literal)e.Item.FindControl("WrhsNameLiteral");
                _wrhsName.Text = HttpUtility.HtmlEncode(_temp.WrhsName);

                Literal _qty = (Literal)e.Item.FindControl("QtyLiteral");
                _qty.Text = HttpUtility.HtmlEncode(_temp.Qty.ToString("#,##0.00"));

                Literal _unitLiteral = (Literal)e.Item.FindControl("UnitLiteral");
                _unitLiteral.Text = HttpUtility.HtmlEncode(_temp.UnitName);
            }
        }

        protected void ProductInformationRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                V_ProductInformation _temp = (V_ProductInformation)e.Item.DataItem;

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterProductInformation");
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

                Literal _transDate = (Literal)e.Item.FindControl("TransDateLiteral");
                _transDate.Text = HttpUtility.HtmlEncode(Convert.ToDateTime(_temp.TransDate).ToString("dd-MM-yyyy"));

                Literal _transNmbr = (Literal)e.Item.FindControl("TransNmbrLiteral");
                _transNmbr.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transType = (Literal)e.Item.FindControl("TransTypeLiteral");
                _transType.Text = HttpUtility.HtmlEncode(_temp.TransType);

                Literal _fileNo = (Literal)e.Item.FindControl("FileNoLiteral");
                _fileNo.Text = HttpUtility.HtmlEncode(_temp.FileNmbr);

                Literal _reffInsName = (Literal)e.Item.FindControl("ReffInsNameLiteral");
                _reffInsName.Text = HttpUtility.HtmlEncode((_temp.ReffInsName == null) ? ("N/A") : (_temp.ReffInsName));

                Literal _wrhsCode = (Literal)e.Item.FindControl("WrhsCodeLiteral");
                _wrhsCode.Text = HttpUtility.HtmlEncode(_temp.WrhsCode);

                Literal _qtyIn = (Literal)e.Item.FindControl("QtyInLiteral");
                _qtyIn.Text = HttpUtility.HtmlEncode(_temp.QtyIn.ToString("#,##0.00"));

                Literal _qtyOut = (Literal)e.Item.FindControl("QtyOutLiteral");
                Decimal _qtyOutDecimal = (_temp.QtyOut == null) ? 0 : Convert.ToDecimal(_temp.QtyOut);
                _qtyOut.Text = HttpUtility.HtmlEncode(_qtyOutDecimal.ToString("#,##0.00"));
            }
        }

        protected void BackButton1_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ProductListingSummary1.aspx");
        }

        protected void BackButton_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ProductListingSummary1.aspx");
        }

        protected void ViewByDDL_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.ViewByDDL.SelectedValue == "0")
            {
                this.DateTr.Visible = false;
            }
            else if (this.ViewByDDL.SelectedValue == "1")
            {
                this.DateTr.Visible = true;
            }
            else
            {
                this.DateTr.Visible = false;
            }

        }

        private double RowCountStockBalance()
        {
            double _result = 0;

            _result = this._stcBalanceBL.RowsCountStokBalance(this.ProductCodeTextBox.Text);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private double RowCountProductInformation()
        {
            double _result = 0;
            DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.DateFromTextBox.Text).Year, DateFormMapper.GetValue(this.DateFromTextBox.Text).Month, DateFormMapper.GetValue(this.DateFromTextBox.Text).Day, 0, 0, 0);
            DateTime _endDateTime = new DateTime(DateFormMapper.GetValue(this.DateToTextBox.Text).Year, DateFormMapper.GetValue(this.DateToTextBox.Text).Month, DateFormMapper.GetValue(this.DateToTextBox.Text).Day, 23, 59, 59);

            _result = this._productBL.RowsCountProductInformation(this.ProductCodeTextBox.Text, _startDateTime, _endDateTime);
            _result = System.Math.Ceiling(_result / (double)_maxrow2);

            return _result;
        }

        private void ShowPageStockBalance(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCountStockBalance();

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

        private void ShowPageProductInformation(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCountProductInformation();

            if (_prmCurrentPage - _maxlength2 > 0)
            {
                min = _prmCurrentPage - _maxlength2;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength2 < q)
            {
                max = _prmCurrentPage + _maxlength2 + 1;
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
                    this._navMark2[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[0]);
                    _pageNumberElement++;

                    this._navMark2[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark2[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag2 = true;
                    }

                    this._navMark2[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark2.CopyTo(_navMarkBackup, 0);
                this.DataPagerTop1Repeater.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTop1Repeater.DataBind();

                _flag2 = true;
                _nextFlag2 = false;
                _lastFlag2 = false;
                _navMark2 = _navMarkBackup;

            }
            else
            {
                this.DataPagerTop1Repeater.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTop1Repeater.DataBind();
            }
        }

        private void ShowDataStokBalance(Int32 _prmCurrentPage)
        {
            this._page = _prmCurrentPage;

            this.StokBalanceRepeater.DataSource = this._stcBalanceBL.GetListStokBalance(_prmCurrentPage, _maxrow, this.ProductCodeTextBox.Text);
            this.StokBalanceRepeater.DataBind();

            this.ShowPageStockBalance(_prmCurrentPage);
        }

        private void ShowDataProductInformation(Int32 _prmCurrentPage)
        {
            this._page2 = _prmCurrentPage;
            DateTime _startDateTime = new DateTime(DateFormMapper.GetValue(this.DateFromTextBox.Text).Year, DateFormMapper.GetValue(this.DateFromTextBox.Text).Month, DateFormMapper.GetValue(this.DateFromTextBox.Text).Day, 0, 0, 0);
            DateTime _endDateTime = new DateTime(DateFormMapper.GetValue(this.DateToTextBox.Text).Year, DateFormMapper.GetValue(this.DateToTextBox.Text).Month, DateFormMapper.GetValue(this.DateToTextBox.Text).Day, 23, 59, 59);

            this.ProductInformationRepeater.DataSource = this._productBL.GetListProductInformation(_prmCurrentPage, _maxrow, this.ProductCodeTextBox.Text, _startDateTime, _endDateTime);
            this.ProductInformationRepeater.DataBind();

            this.ShowPageProductInformation(_prmCurrentPage);
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataStokBalance(Convert.ToInt32(e.CommandArgument));
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

                    if (_reqPage > this.RowCountStockBalance())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountStockBalance().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountStockBalance()) - 1;
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

            this.ShowDataStokBalance(_reqPage);
        }

        protected void DataPagerTop1Repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey2] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataProductInformation(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTop1Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey2]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox1");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton1");
                    _pageNumberLinkButton.CommandName = "DataPager";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark2[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark2[0] = null;
                    }
                    else if (_pageNumber == this._navMark2[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark2[1] = null;
                    }
                    else if (_pageNumber == this._navMark2[2] && this._flag2 == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark2[2] = null;
                        this._nextFlag2 = true;
                        this._flag2 = true;
                    }
                    else if (_pageNumber == this._navMark2[3] && this._flag2 == true && this._nextFlag2 == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark2[3] = null;
                        this._lastFlag2 = true;
                    }
                    else
                    {
                        if (this._lastFlag2 == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark2[2] && this._flag2 == true)
                            this._flag2 = false;
                    }
                }
            }
        }

        protected void DataPagerButton1_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTop1Repeater.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCountProductInformation())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCountProductInformation().ToString();
                        _reqPage = Convert.ToInt32(this.RowCountProductInformation()) - 1;
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

            this.ViewState[this._currPageKey2] = _reqPage;

            this.ShowDataProductInformation(_reqPage);
        }
    }
}