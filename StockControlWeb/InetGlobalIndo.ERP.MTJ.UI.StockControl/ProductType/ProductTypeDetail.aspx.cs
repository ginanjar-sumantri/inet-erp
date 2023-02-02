using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl
{
    public partial class ProductTypeDetail : ProductTypeBase
    {
        private ProductBL _prodType = new ProductBL();
        //private CurrencyBL _currencyBL = new CurrencyBL();
        private PermissionBL _permBL = new PermissionBL();
        //private PriceGroupBL _priceGroupBL = new PriceGroupBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        //private int?[] _navMark2 = { null, null, null, null };
        //private bool _flag2 = true;
        //private bool _nextFlag2 = false;
        //private bool _lastFlag2 = false;

        private string _currPageKey = "CurrentPage";
        //private string _currPageKey2 = "CurrentPage";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        //private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater2_ctl";
        //private string _akhir2 = "_ListCheckBox2";
        //private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        //private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        //private int _page2;
        //private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        //private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        //private int _no2 = 0;
        //private int _nomor2 = 0;
        //private Boolean _isCheckedAll = true;

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
            //this.DataPagerButton2.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ViewState[this._currPageKey] = 0;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                //this.AddButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                //this.DeleteButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";

                this.ClearLabel();
                this.ShowData(0);

                this.SetButtonPermission();
            }
        }

        private void SetButtonPermission()
        {
            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.AddButton.Visible = false;
                //this.AddButton2.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
                //this.DeleteButton2.Visible = false;
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.Label1.Text = "";
            //this.Label2.Text = "";
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

        //private Boolean IsCheckedAll2()
        //{
        //    Boolean _result = true;

        //    foreach (RepeaterItem _row in this.ListRepeater2.Items)
        //    {
        //        CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox2");

        //        if (_chk.Checked == false)
        //        {
        //            _result = false;
        //            break;
        //        }
        //    }

        //    if (this.ListRepeater2.Items.Count == 0)
        //    {
        //        _result = false;
        //    }

        //    return _result;
        //}

        private void ShowData(Int32 _prmCurrentPage)
        {
            MsProductType _msProductType = this._prodType.GetSingleProductType(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.ProductTypeCodeTextBox.Text = _msProductType.ProductTypeCode;
            this.ProductTypeNameTextBox.Text = _msProductType.ProductTypeName;
            this.CategoryName.Text = _msProductType.ProductCategory;
            this.IsUsingPGCheckBox.Checked = _msProductType.IsUsingPG;
            this.IsUsingUniqueIDCheckBox.Checked = _msProductType.IsUsingUniqueID;
            this.StockLabel.Text = ProductTypeDataMapper.GetActiveText(_msProductType.FgStock);
            this.SendToKitchenLabel.Text = ((_msProductType.fgSendToKitchen == false || _msProductType.fgSendToKitchen == null)? "No": (_msProductType.fgSendToKitchen == true)? "Yes": "No");
            this.WithTaxLabel.Text = ((_msProductType.fgTax == false || _msProductType.fgTax == null) ?  "No": (_msProductType.fgTax == true)? "Yes": "No");
            if (Convert.ToBoolean(_msProductType.fgTax))
            {
                this.TaxDiv.Visible = true;
                this.TaxTypeTextBox.Text = _prodType.GetSingleTaxTypeName(_msProductType.TaxTypeCode);
                this.TaxPercentageTextBox.Text = Convert.ToDecimal(_msProductType.TaxValue).ToString("#,##0.##");
                this.ServiceChargerTextBox.Text = Convert.ToDecimal(_msProductType.ServiceCharges).ToString("#,##0.##");
                this.ServiceChargesCalculateLabel.Text = ((_msProductType.fgServiceChargesCalculate == false || _msProductType.fgServiceChargesCalculate ==null)? "No": (_msProductType.fgServiceChargesCalculate == true)? "Yes": "No");
            }
            this.FgActiveCheckBox.Checked = (_msProductType.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _msProductType.Remark;
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");
            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._prodType.GetListMsProductTypeDtSecond(_prmCurrentPage, _maxrow, this.ProductTypeCodeTextBox.Text);
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Checked = this.IsCheckedAll();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.Label1.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
            }

            //if (this.IsUsingPGCheckBox.Checked == true)
            //{
            //    this.ShowData2(0);
            //}
            //else
            //{
            //    this.panelPG.Visible = false;
            //}
        }

        //private void ShowData2(Int32 _prmCurrentPage)
        //{
        //    this.TempHidden.Value = "";

        //    NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

        //    this._page2 = _prmCurrentPage;

        //    this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + _tempHidden2 + "' );");
        //    this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");

        //    this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

        //    if (this._permView == PermissionLevel.NoAccess)
        //    {
        //        this.ListRepeater2.DataSource = null;
        //    }
        //    else
        //    {
        //        this.ListRepeater2.DataSource = this._priceGroupBL.GetListProdType_PG(_prmCurrentPage, _maxrow2, this.ProductTypeCodeTextBox.Text);
        //    }
        //    this.ListRepeater2.DataBind();

        //    this.AllCheckBox2.Checked = this.IsCheckedAll2();

        //    //if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
        //    //{
        //    //    this.Label2.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
        //    //}

        //    if (this._permView != PermissionLevel.NoAccess)
        //    {
        //        this.ShowPage2(_prmCurrentPage);
        //    }
        //}

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

        //private Boolean IsChecked2(String _prmValue)
        //{
        //    Boolean _result = false;
        //    String[] _value = this.CheckHidden2.Value.Split(',');

        //    for (int i = 0; i < _value.Length; i++)
        //    {
        //        if (_prmValue == _value[i])
        //        {
        //            _result = true;
        //            break;
        //        }
        //    }

        //    return _result;
        //}

        private double RowCount()
        {
            double _result = 0;

            _result = this._prodType.RowsCountMsProductTypeDt(this.ProductTypeCodeTextBox.Text);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        //private double RowCount2()
        //{
        //    double _result = 0;

        //    _result = this._priceGroupBL.RowsCountProdType_PG(this.ProductTypeCodeTextBox.Text);
        //    _result = System.Math.Ceiling(_result / (double)_maxrow2);

        //    return _result;
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

        //private void ShowPage2(Int32 _prmCurrentPage)
        //{
        //    Int32[] _pageNumber;
        //    byte _addElement = 0;
        //    Int32 _pageNumberElement = 0;

        //    int i = 0;
        //    decimal min = 0, max = 0;
        //    double q = this.RowCount2();

        //    if (_prmCurrentPage - _maxlength2 > 0)
        //    {
        //        min = _prmCurrentPage - _maxlength2;
        //    }
        //    else
        //    {
        //        min = 0;
        //    }

        //    if (_prmCurrentPage + _maxlength2 < q)
        //    {
        //        max = _prmCurrentPage + _maxlength2 + 1;
        //    }
        //    else
        //    {
        //        max = Convert.ToDecimal(q);
        //    }

        //    if (_prmCurrentPage > 0)
        //        _addElement += 2;

        //    if (_prmCurrentPage < q - 1)
        //        _addElement += 2;

        //    i = Convert.ToInt32(min);
        //    _pageNumber = new Int32[Convert.ToInt32(max - min) + _addElement];
        //    if (_pageNumber.Length != 0)
        //    {
        //        //NB: Prev Or First
        //        if (_prmCurrentPage > 0)
        //        {
        //            this._navMark2[0] = 0;

        //            _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[0]);
        //            _pageNumberElement++;

        //            this._navMark2[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

        //            _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[1]);
        //            _pageNumberElement++;
        //        }

        //        for (; i < max; i++)
        //        {
        //            _pageNumber[_pageNumberElement] = i;
        //            _pageNumberElement++;
        //        }

        //        if (_prmCurrentPage < q - 1)
        //        {
        //            this._navMark2[2] = Convert.ToInt32(_prmCurrentPage + 1);

        //            _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[2]);
        //            _pageNumberElement++;

        //            if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
        //            {
        //                this._flag2 = true;
        //            }

        //            this._navMark2[3] = Convert.ToInt32(q - 1);

        //            _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark2[3]);
        //            _pageNumberElement++;
        //        }

        //        int?[] _navMarkBackup = new int?[4];
        //        this._navMark.CopyTo(_navMarkBackup, 0);
        //        this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
        //                                                select _query;
        //        this.DataPagerTopRepeater2.DataBind();

        //        _flag2 = true;
        //        _nextFlag2 = false;
        //        _lastFlag2 = false;
        //        _navMark2 = _navMarkBackup;

        //    }
        //    else
        //    {
        //        this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
        //                                                select _query;
        //        this.DataPagerTopRepeater2.DataBind();
        //    }
        //}

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._prodType.DeleteMultiMsProductTypeDt(_tempSplit, this.ProductTypeCodeTextBox.Text);

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

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MsProductTypeDt _temp = (MsProductTypeDt)e.Item.DataItem;
                string _code = _temp.WrhsType.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _noLiteral.Text = _no.ToString();

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_code);

                //if (this._isCheckedAll == true && _listCheckbox.Checked == false)
                //{
                //    this._isCheckedAll = false;
                //}

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ProductTypeCodeTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._wrhsKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                if (this._permView == PermissionLevel.NoAccess)
                {
                    _viewButton.Visible = false;
                }

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                _editButton.PostBackUrl = this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ProductTypeCodeTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._wrhsKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton.Visible = false;
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

                Literal _wrhsType = (Literal)e.Item.FindControl("WrhsTypeLiteral");
                _wrhsType.Text = HttpUtility.HtmlEncode(WarehouseDataMapper.GetWrhsType(_temp.WrhsType));

                Literal _accInvent = (Literal)e.Item.FindControl("AccInventLiteral");
                _accInvent.Text = HttpUtility.HtmlEncode(_temp.AccInvent);

                Literal _accCOGS = (Literal)e.Item.FindControl("AccCOGSLiteral");
                _accCOGS.Text = HttpUtility.HtmlEncode((_temp.AccCOGS == "null") ? "" : _temp.AccCOGS);

                Literal _accSales = (Literal)e.Item.FindControl("AccSalesLiteral");
                _accSales.Text = HttpUtility.HtmlEncode((_temp.AccSales == "null") ? "" : _temp.AccSales);

                Literal _accWIP = (Literal)e.Item.FindControl("AccWIPLiteral");
                _accWIP.Text = HttpUtility.HtmlEncode((_temp.AccWIP == "null") ? "" : _temp.AccWIP);

                Literal _accTransitSJ = (Literal)e.Item.FindControl("AccTransitSJLiteral");
                _accTransitSJ.Text = HttpUtility.HtmlEncode((_temp.AccTransitSJ == "null") ? "" : _temp.AccTransitSJ);

                Literal _accTransitWrhs = (Literal)e.Item.FindControl("AccTransitWrhsLiteral");
                _accTransitWrhs.Text = HttpUtility.HtmlEncode((_temp.AccTransitWrhs == "null") ? "" : _temp.AccTransitWrhs);

                Literal _accTransitReject = (Literal)e.Item.FindControl("AccTransitRejectLiteral");
                _accTransitReject.Text = HttpUtility.HtmlEncode((_temp.AccTransitReject == "null") ? "" : _temp.AccTransitReject);
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

        //protected void AddButton2_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect(this._addDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        //}

        //protected void DeleteButton2_Click(object sender, ImageClickEventArgs e)
        //{
        //    string _error = "";
        //    string _temp = this.CheckHidden2.Value;
        //    string[] _tempSplit = _temp.Split(',');
        //    string _page = "0";

        //    this.ClearLabel();

        //    bool _result = this._priceGroupBL.DeleteMultiProdType_PG(_tempSplit);

        //    if (_result == true)
        //    {
        //        _error = "Delete Success";
        //    }
        //    else
        //    {
        //        _error = "Delete Failed";
        //    }

        //    this.CheckHidden2.Value = "";
        //    this.AllCheckBox2.Checked = false;

        //    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        //}

        //protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        Master_ProductType_PriceGroup _temp = (Master_ProductType_PriceGroup)e.Item.DataItem;
        //        string _code = _temp.ProductType_PriceGroupCode.ToString();

        //        if (this.TempHidden2.Value == "")
        //        {
        //            this.TempHidden2.Value = _code;
        //        }
        //        else
        //        {
        //            this.TempHidden2.Value += "," + _code;
        //        }

        //        Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
        //        _no += 1;
        //        _noLiteral.Text = _no.ToString();

        //        CheckBox _listCheckbox;
        //        _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox2");
        //        _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "')");
        //        _listCheckbox.Checked = this.IsChecked2(_code);

        //        HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate2");
        //        _tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
        //        _tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

        //        Literal _pgCode = (Literal)e.Item.FindControl("PGCodeLiteral");
        //        _pgCode.Text = HttpUtility.HtmlEncode(this._priceGroupBL.GetSingle(_temp.PriceGroupUniqueCode.ToString()).PriceGroupCode);

        //        Literal _year = (Literal)e.Item.FindControl("YearLiteral");
        //        _year.Text = HttpUtility.HtmlEncode(this._priceGroupBL.GetSingle(_temp.PriceGroupUniqueCode.ToString()).Year.ToString());

        //        Literal _currency = (Literal)e.Item.FindControl("CurrencyLiteral");
        //        _currency.Text = HttpUtility.HtmlEncode(this._priceGroupBL.GetSingle(_temp.PriceGroupUniqueCode.ToString()).CurrCode);

        //        byte _decimalPlaceForex = this._currencyBL.GetDecimalPlace(this._priceGroupBL.GetSingle(_temp.PriceGroupUniqueCode.ToString()).CurrCode.Trim());
        //        decimal _tempAmtForex = this._priceGroupBL.GetSingle(_temp.PriceGroupUniqueCode.ToString()).AmountForex;
        //        Literal _amountForex = (Literal)e.Item.FindControl("AmountForexLiteral");
        //        _amountForex.Text = (_tempAmtForex == 0 ? "0" : HttpUtility.HtmlEncode(_tempAmtForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceForex))));

        //        byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(this._currencyBL.GetCurrDefault());
        //        decimal _tempAmtHome = this._priceGroupBL.GetSingle(_temp.PriceGroupUniqueCode.ToString()).AmountHome;
        //        Literal _amountHome = (Literal)e.Item.FindControl("AmountLiteralLiteral");
        //        _amountHome.Text = (_tempAmtHome == 0 ? "0" : HttpUtility.HtmlEncode(_tempAmtHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome))));

        //    }
        //}

        //protected void DataPagerButton2_Click(object sender, EventArgs e)
        //{
        //    Int32 _reqPage = 0;

        //    foreach (Control _item in this.DataPagerTopRepeater2.Controls)
        //    {
        //        if (((TextBox)_item.Controls[3]).Text != "")
        //        {
        //            _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

        //            if (_reqPage > this.RowCount2())
        //            {
        //                ((TextBox)_item.Controls[3]).Text = this.RowCount2().ToString();
        //                _reqPage = Convert.ToInt32(this.RowCount2()) - 1;
        //                break;
        //            }
        //            else if (_reqPage < 0)
        //            {
        //                ((TextBox)_item.Controls[3]).Text = "1";
        //                _reqPage = 0;
        //                break;
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }
        //    }

        //    this.ViewState[this._currPageKey2] = _reqPage;

        //    this.ShowData2(_reqPage);
        //}

        //protected void DataPagerTopRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "DataPager")
        //    {
        //        this.ViewState[this._currPageKey2] = Convert.ToInt32(e.CommandArgument);

        //        this.ShowData2(Convert.ToInt32(e.CommandArgument));
        //    }
        //}

        //protected void DataPagerTopRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        //    {
        //        int _pageNumber = (int)e.Item.DataItem;

        //        NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

        //        if (Convert.ToInt32(this.ViewState[this._currPageKey2]) == _pageNumber)
        //        {
        //            TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox2");

        //            _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
        //            _pageNumberTextbox.MaxLength = 9;
        //            _pageNumberTextbox.Visible = true;

        //            _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
        //            _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
        //        }
        //        else
        //        {
        //            LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton2");
        //            _pageNumberLinkButton.CommandName = "DataPager";
        //            _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

        //            if (_pageNumber == this._navMark2[0])
        //            {
        //                _pageNumberLinkButton.Text = "First";
        //                this._navMark2[0] = null;
        //            }
        //            else if (_pageNumber == this._navMark2[1])
        //            {
        //                _pageNumberLinkButton.Text = "Prev";
        //                this._navMark2[1] = null;
        //            }
        //            else if (_pageNumber == this._navMark2[2] && this._flag2 == false)
        //            {
        //                _pageNumberLinkButton.Text = "Next";
        //                this._navMark2[2] = null;
        //                this._nextFlag2 = true;
        //                this._flag2 = true;
        //            }
        //            else if (_pageNumber == this._navMark2[3] && this._flag2 == true && this._nextFlag2 == true)
        //            {
        //                _pageNumberLinkButton.Text = "Last";
        //                this._navMark2[3] = null;
        //                this._lastFlag2 = true;
        //            }
        //            else
        //            {
        //                if (this._lastFlag2 == false)
        //                {
        //                    _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
        //                }

        //                if (_pageNumber == this._navMark2[2] && this._flag2 == true)
        //                    this._flag2 = false;
        //            }
        //        }
        //    }
        //}
    }
}