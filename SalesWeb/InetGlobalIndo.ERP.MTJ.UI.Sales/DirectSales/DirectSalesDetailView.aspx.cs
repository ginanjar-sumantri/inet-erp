using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using System.Web.UI.HtmlControls;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.DirectSales
{
    public partial class DirectSalesDetailView : DirectSalesBase
    {
        private DirectSalesBL _directSalesBL = new DirectSalesBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _customerBL = new CustomerBL();

        private byte _decimalPlace = 0;

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
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                SALTrDirectSalesHd _directSalesHd = this._directSalesBL.GetSingleDirectSalesHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                if (_directSalesHd.Status != DirectSalesDataMapper.GetStatusByte(TransStatus.Posted))
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                    this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                    this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                    this.ImportButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/import_excel.jpg";
                    this.GetFormatExcelLiteral.Text = "<a href='" + ApplicationConfig.StockControlFormatVirDirPath + "DirectSales.xls'>" + "<img src='" + ApplicationConfig.HomeWebAppURL + "images/download_excel.jpg" + "' border='0'>" + "</a>";
                }
                else
                {
                    this.EditButton.Visible = false;
                    this.AddButton.Visible = false;
                    this.DeleteButton.Visible = false;
                    this.ImportButton.Visible = false;
                    this.GetFormatExcelLiteral.Text = "";
                }

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Edit2.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Back.jpg";
                //this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ShowData(0);
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");
            this.DataPagerButton.Visible = false;
            this.QtyOrderTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyOrderTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "BlurAmount(" + QtyOrderTextBox.ClientID + "," + PriceTextBox.ClientID + "," + AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void ShowData(Int32 _prmCurrentPage)
        {
            String _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            String _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            String _warehouseCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._Warehouse), ApplicationConfig.EncryptionKey);
            String _LocationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._Location), ApplicationConfig.EncryptionKey);

            SALTrDirectSalesDt _directSalesDt = _directSalesBL.GetSingleDirectSalesDt(_transNmbr, _productCode, _warehouseCode, _LocationCode);
            MsProduct _msProduct = _productBL.GetSingleProduct(_productCode);

            MsWarehouse _msWarehouse = _warehouseBL.GetSingle(_directSalesDt.WrhsCode);
            MsCustomer _msCustomer = _customerBL.GetSingleCust(_directSalesDt.WrhsSubLed);
            MsWrhsLocation _msWrhsLocation = _warehouseBL.GetSingleWrhsLocation(_directSalesDt.WLocationCode);

            char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(_directSalesDt.WrhsCode);
            if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.WarehouseSubledTextBox.Text = "";
            }
            else
            {
                if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.WarehouseSubledTextBox.Text = _customerBL.GetNameByCode(_directSalesDt.WrhsSubLed);
                }
                else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.WarehouseSubledTextBox.Text = _supplierBL.GetSuppNameByCode(_directSalesDt.WrhsSubLed);
                }
            }

            this.WarehouseCodeTextBox.Text = _msWarehouse.WrhsName;
            this.WarehouseLocationTextBox.Text = _msWrhsLocation.WLocationName;
            this.TransNmbrHiddenField.Value = _directSalesDt.TransNmbr;
            this.ProductCodeTextBox.Text = _directSalesDt.ProductCode;
            this.ProductNameTextBox.Text = _msProduct.ProductName;
            this.QtyOrderTextBox.Text = Convert.ToDecimal(_directSalesDt.QtyOrder).ToString("0");
            this.UnitOrderTextBox.Text = _directSalesDt.UnitOrder;
            this.PriceTextBox.Text = _directSalesDt.Price.ToString("#,##0.00");
            this.AmountTextBox.Text = _directSalesDt.Amount.ToString("#,##0.00");

            if (_directSalesBL.GetSingleProductType(_msProduct.ProductType).IsUsingUniqueID == true)
            {
                this.Panel2.Visible = true;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.ImportButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/import_excel.jpg";

                this.TempHidden.Value = "";

                this._page = _prmCurrentPage;

                this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                if (this._permView == PermissionLevel.NoAccess)
                {
                    this.ListRepeater.DataSource = null;
                }
                else
                {
                    this.ListRepeater.DataSource = this._directSalesBL.GetListSerialNmbr(_prmCurrentPage, _maxrow, _transNmbr, _productCode, _LocationCode, _warehouseCode);
                    if (this._directSalesBL.RowsCountSALTrDirectSalesSerialNumberByProduct(_transNmbr, _productCode, _LocationCode, _warehouseCode) >= Convert.ToDouble(this.QtyOrderTextBox.Text))
                    {
                        this.ImportButton.Visible = false;
                        this.AddButton.Visible = false;
                    }
                }
                this.ListRepeater.DataBind();

                this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

                this.AllCheckBox.Checked = this.IsCheckedAll();

                this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

                if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
                {
                    this.Label1.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
                }

                if (this._permView != PermissionLevel.NoAccess)
                {
                    this.ShowPage(_prmCurrentPage);
                }
            }
            else
            {
                this.Panel2.Visible = false;
            }


        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._Warehouse + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._Warehouse)) + "&" + this._Location + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._Location)));

            //if (this.ProductCodeTextBox.Text != "" && this.ProductNameTextBox.Text != "" && this.QtyOrderTextBox.Text != "")
            //{
            //    string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            //    String _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            //    String _warehouseCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._Warehouse), ApplicationConfig.EncryptionKey);
            //    String _LocationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._Location), ApplicationConfig.EncryptionKey);

            //    SALTrDirectSalesDt _directSalesDt = _directSalesBL.GetSingleDirectSalesDt(_transNo, _productCode, _warehouseCode, _LocationCode);

            //    //MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductCodeTextBox.Text);

            //    _directSalesDt.Qty = Convert.ToDecimal(this.QtyOrderTextBox.Text);
            //    _directSalesDt.Price = Convert.ToDecimal(this.PriceTextBox.Text);

            //    bool _result = this._directSalesBL.EditDirectSalesDtAndEditDirectSalesHd(_directSalesDt, Convert.ToDecimal(this.AmountTextBox.Text));

            //    if (_result == true)
            //    {
            //        Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            //    }
            //    else
            //    {
            //        this.WarningLabel.Text = "Your Failed Add Data";
            //    }
            //}
            //else
            //{
            //    this.WarningLabel.Text = "Please Insert Product Code, Product Name & Qty";
            //}
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }



        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.ImportButton.Visible = false;
                this.AddButton.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
            }
        }

        private double RowCount()
        {
            double _result = 0;

            _result = _directSalesBL.RowsCountSALTrDirectSalesSerialNumberByProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._Location), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._Warehouse), ApplicationConfig.EncryptionKey));
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

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SALTrDirectSalesDt_SerialNmbr _temp = (SALTrDirectSalesDt_SerialNmbr)e.Item.DataItem;

                string _code = _temp.SerialNmbr.ToString();

                string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
                string _WarehouseCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._Warehouse), ApplicationConfig.EncryptionKey);

                //Transaction_NCPImport _trans = _ncpBL.GetSingleTransactionNCPImport(_transNo);

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_code);

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

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

                Literal _serialNmbr = (Literal)e.Item.FindControl("SerialNumberLiteral");
                _serialNmbr.Text = HttpUtility.HtmlEncode(_temp.SerialNmbr);

                //ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                //if (_directSalesBL.GetSingleDirectSalesHd(_transNo).Status != DirectSalesDataMapper.GetStatusByte(TransStatus.Posting))
                //{
                //    _editButton.PostBackUrl = this._editDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._serialNumber + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                //    _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                //}
                //else
                //{
                //    _editButton.Visible = false;
                //}

                //this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                //if (this._permEdit == PermissionLevel.NoAccess)
                //{
                //    _editButton.Visible = false;
                //}
            }
        }

        protected void ImportButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._importPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._Warehouse + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._Warehouse)) + "&" + this._Location + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._Location)));
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._Warehouse + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._Warehouse)) + "&" + this._Location + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._Location)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._directSalesBL.DeleteMultiSALTrDirectSalesSerialNumber(_tempSplit);

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

            Response.Redirect(this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
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

    }
}