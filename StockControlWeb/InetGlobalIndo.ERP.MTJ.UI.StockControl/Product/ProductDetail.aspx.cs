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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Product
{
    public partial class ProductDetail : ProductBase
    {
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currBL = new CurrencyBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private DiscountBL _discountBL = new DiscountBL();
        private CompanyConfig _compConfig = new CompanyConfig();
        private SupplierBL _suppBL = new SupplierBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int?[] _navMark2 = { null, null, null, null };
        private bool _flag2 = true;
        private bool _nextFlag2 = false;
        private bool _lastFlag2 = false;

        private int?[] _navMark3 = { null, null, null, null };
        private bool _flag3 = true;
        private bool _nextFlag3 = false;
        private bool _lastFlag3 = false;

        private string _currPageKey = "CurrentPage";
        private string _currPageKey2 = "CurrentPage2";
        private string _currPageKey3 = "CurrentPage3";

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private int _page2;
        private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no2 = 0;
        private int _nomor2 = 0;

        private int _page3;
        private int _maxrow3 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength3 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no3 = 0;
        private int _nomor3 = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater2_ctl";
        private string _akhir2 = "_ListCheckBox2";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        private string _awal3 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater3_ctl";
        private string _akhir3 = "_ListCheckBox2";
        private string _cbox3 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox3";
        private string _tempHidden3 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden3";

        private string _app = "Aplikasi";

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
            this.DataPagerButton2.Attributes.Add("Style", "visibility: hidden;");
            this.DataPagerButton3.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ViewState[this._currPageKey] = 0;
                this.ViewState[this._currPageKey2] = 0;
                this.ViewState[this._currPageKey3] = 0;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.AddButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.AddButton3.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton3.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";

                this.ClearLabel();
                this.ShowData(0);
                this.ShowDataDetail2(0);
                this.ShowDataDetail3(0);
                this.SetButtonPermission();

                String _haveProductItemDuration = this._compConfig.GetSingle(CompanyConfigure.HaveProductItemDuration).SetValue;
                if (_haveProductItemDuration == "0")
                {
                    this.ItemDurationRow.Visible = false;
                }
                else
                {
                    this.ItemDurationRow.Visible = true;
                }
            }
        }

        private void SetButtonPermission()
        {
            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.AddButton.Visible = false;
                this.AddButton2.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
                this.DeleteButton2.Visible = false;
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
            this.Label2.Text = "";
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._productBL.RowsCountProductConvert(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private double RowCount2()
        {
            double _result = 0;

            _result = this._productBL.RowsCountProductSalesPrice(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
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

        private void ShowPage2(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount2();

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
                this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater2.DataBind();

                _flag2 = true;
                _nextFlag2 = false;
                _lastFlag2 = false;
                _navMark2 = _navMarkBackup;
            }
            else
            {
                this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater2.DataBind();
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

        private Boolean IsCheckedAll2()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.ListRepeater2.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox2");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.ListRepeater2.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        public void ShowData(Int32 _prmCurrentPage)
        {
            MsProduct _msProduct = this._productBL.GetSingleProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = this._currBL.GetDecimalPlace(_msProduct.PurchaseCurr);
            string _pgYear = this._compConfig.GetSingle(CompanyConfigure.ActivePGYear).SetValue;

            this.ProductCodeTextBox.Text = _msProduct.ProductCode;
            this.ProductNameTextBox.Text = _msProduct.ProductName;
            this.ProductSubGroupTextBox.Text = _productBL.GetProductSubGroupNameByCode(_msProduct.ProductSubGroup);
            this.ProductTypeTextBox.Text = _productBL.GetProductTypeNameByCode(_msProduct.ProductType);
            this.Spec1TextBox.Text = _msProduct.Specification1;
            this.Spec2TextBox.Text = _msProduct.Specification2;
            this.Spec3TextBox.Text = _msProduct.Specification3;
            this.Spec4TextBox.Text = _msProduct.Specification4;
            this.ProductPhoto.Attributes.Add("src", "" + ApplicationConfig.ProductPhotoVirDirPath + _msProduct.Photo + "?t=" + System.DateTime.Now.ToString());

            Boolean _isUsingPG = this._productBL.GetSingleProductType(_msProduct.ProductType).IsUsingPG;
            if (_isUsingPG == true)
            {
                this.PriceGroupTR.Visible = true;
                if (_msProduct.PriceGroupCode != null)
                {
                    this.PGTextBox.Text = this._priceGroupBL.GetSingle(_msProduct.PriceGroupCode, Convert.ToInt32(_pgYear.Trim())).PriceGroupCode;
                }
            }
            else
            {
                this.PriceGroupTR.Visible = false;
            }
            this.BuyingPriceTextBox.Text = (_msProduct.BuyingPrice == 0) ? "0" : _msProduct.BuyingPrice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.CurrencyTextBox.Text = _msProduct.PurchaseCurr;
            this.MinQtyTextBox.Text = (_msProduct.MinQty == 0) ? "0" : _msProduct.MinQty.ToString("#,##0.##");
            this.MaxQtyTextBox.Text = (_msProduct.MaxQty == 0) ? "0" : _msProduct.MaxQty.ToString("#,##0.##");
            this.UnitDropTextBox.Text = _unitBL.GetUnitNameByCode(_msProduct.Unit);
            this.UnitOrderTextBox.Text = _unitBL.GetUnitNameByCode(_msProduct.UnitOrder);
            this.LengthTextBox.Text = (_msProduct.Length == 0) ? "0" : _msProduct.Length.ToString("#,##0.##");
            this.WeightTextBox.Text = (_msProduct.Weight == 0) ? "0" : _msProduct.Weight.ToString("#,##0.##");
            this.HeightTextBox.Text = (_msProduct.Height == 0) ? "0" : _msProduct.Height.ToString("#,##0.##");
            this.WidthTextBox.Text = (_msProduct.Width == 0) ? "0" : _msProduct.Width.ToString("#,##0.##");
            this.VolumeTextBox.Text = (_msProduct.Volume == 0) ? "0" : _msProduct.Volume.ToString("#,##0.##");
            //this.ActiveTextBox.Text = _msProduct.FgActive.ToString();
            this.BarcodeTextBox.Text = _msProduct.Barcode;
            this.FgConsignmentCheckBox.Checked = Convert.ToBoolean(_msProduct.FgConsignment);
            this.FgAssemblyCheckBox.Checked = Convert.ToBoolean(_msProduct.fgAssembly);
            this.FgPackageCheckBox.Checked = Convert.ToBoolean(_msProduct.fgPackage);
            this.SuppNmbrTextBox.Text = _msProduct.ConsignmentSuppCode;
            this.SupplierNameTextBox.Text = _suppBL.GetSuppNameByCode(_msProduct.ConsignmentSuppCode) + " - " + _msProduct.ConsignmentSuppCode;
            //this.ProductValTypeTextBox.Text = ProductDataMapper.ProductValTypeMapper(_msProduct.ProductValType);
            //this.DiscountTextBox.Text = _discountBL.GetDiscountNameByCode(_msProduct.DiscountCode.ToString());
            this.SellingPriceTextBox.Text = (_msProduct.SellingPrice == null) ? "0" : Convert.ToDecimal(_msProduct.SellingPrice).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            //this.DiscAmountTextBox.Text = (_msProduct.DiscAmount == null) ? "0" : Convert.ToDecimal(_msProduct.DiscAmount).ToString("#,##0.##");
            //this.TotalTextBox.Text = (_msProduct.SellingPrice - _msProduct.DiscAmount == null) ? "0" : Convert.ToDecimal(_msProduct.SellingPrice - _msProduct.DiscAmount).ToString("#,##0.##");
            this.ItemDurationTextBox.Text = (_msProduct.ItemDuration == null) ? "0" : _msProduct.ItemDuration.ToString();
            this.FgActiveCheckBox.Checked = (_msProduct.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _msProduct.Remark;

            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._productBL.GetListProductConvert(_prmCurrentPage, _maxrow, this.ProductCodeTextBox.Text);
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "" && _nvcExtractor.GetValue(_app) == "Convert")
            {
                this.Label1.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
            }
        }

        protected void ShowDataDetail2(Int32 _prmCurrentPage)
        {
            this.TempHidden2.Value = "";

            this._page2 = _prmCurrentPage;

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + _tempHidden2 + "' );");
            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");

            this.ListRepeater2.DataSource = this._productBL.GetListProductSalesPrice(_prmCurrentPage, _maxrow, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this.ListRepeater2.DataBind();

            this.AllCheckBox2.Checked = this.IsCheckedAll2();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "" && _nvcExtractor.GetValue(_app) == "Price")
            {
                this.Label2.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowPage2(_prmCurrentPage);
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

        private Boolean IsChecked2(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHidden2.Value.Split(',');

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
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void AddButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addSalesPricePage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._productBL.DeleteMultiProductConvert(_tempSplit, this.ProductCodeTextBox.Text);

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

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error + "&" + _app + "=" + "Convert");
        }

        protected void DeleteButton2_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden2.Value;
            string[] _tempSplit = _temp.Split(',');
            string _error = "";
            string _page = "0";

            this.ClearLabel();

            bool _result = this._productBL.DeleteMultiProductSalesPrice(_tempSplit, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }

            this.CheckHidden2.Value = "";
            this.AllCheckBox2.Checked = false;

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error + "&" + _app + "=" + "Price");
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MsProductConvert _temp = (MsProductConvert)e.Item.DataItem;
                string _unitBLCode = _temp.UnitCode.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _unitBLCode;
                }
                else
                {
                    this.TempHidden.Value += "," + _unitBLCode;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _unitBLCode + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_unitBLCode);

                //if (this._isCheckedAll == true && _listCheckbox.Checked == false)
                //{
                //    this._isCheckedAll = false;
                //}

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                _editButton.PostBackUrl = this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ProductCodeTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + _unitBLKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_unitBLCode, ApplicationConfig.EncryptionKey));
                _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton.Visible = false;
                }

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

                Literal _unitBL = (Literal)e.Item.FindControl("UnitLiteral");
                _unitBL.Text = HttpUtility.HtmlEncode(_temp.UnitConvertName);

                Literal _unitBLConvert = (Literal)e.Item.FindControl("UnitConvertLiteral");
                _unitBLConvert.Text = HttpUtility.HtmlEncode(_temp.UnitCodeName);

                Literal _rate = (Literal)e.Item.FindControl("RateLiteral");
                _rate.Text = HttpUtility.HtmlEncode((_temp.Rate == 0) ? "0" : _temp.Rate.ToString("#,##0.##"));
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

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Master_ProductSalesPrice _temp = (Master_ProductSalesPrice)e.Item.DataItem;

                string _code = _temp.CurrCode.ToString();

                if (this.TempHidden2.Value == "")
                {
                    this.TempHidden2.Value = _code;
                }
                else
                {
                    this.TempHidden2.Value += "," + _code;
                }

                Literal _noLiteral2 = (Literal)e.Item.FindControl("NoLiteral2");
                _no2 = _page2 * _maxrow3;
                _no2 += 1;
                _no2 = _nomor2 + _no2;
                _noLiteral2.Text = _no2.ToString();
                _nomor2 += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox2");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "')");
                _listCheckbox.Checked = this.IsChecked2(_code);

                ImageButton _editButton2 = (ImageButton)e.Item.FindControl("EditButton2");
                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton2.Visible = false;
                }
                else
                {
                    _editButton2.Visible = true;
                    _editButton2.PostBackUrl = this._editSalesPricePage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._currKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                    _editButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                }

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate2");
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

                Literal _currCode = (Literal)e.Item.FindControl("CurrCodeLiteral");
                _currCode.Text = HttpUtility.HtmlEncode(_temp.CurrCode);

                byte _decimalPlace = _currBL.GetDecimalPlace(_temp.CurrCode);

                Literal _salesPrice = (Literal)e.Item.FindControl("SalesPriceLiteral");
                _salesPrice.Text = HttpUtility.HtmlEncode(_temp.SalesPrice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));

                Literal _unit = (Literal)e.Item.FindControl("UnitLiteral");
                _unit.Text = HttpUtility.HtmlEncode(_temp.UnitCode);
            }
        }

        protected void DataPagerTopRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager2")
            {
                this.ViewState[this._currPageKey2] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataDetail2(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey2]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox2");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton2");
                    _pageNumberLinkButton.CommandName = "DataPager2";
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

        protected void DataPagerButton2_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater2.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount2())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount2().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount2()) - 1;
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

            this.ShowDataDetail2(_reqPage);
        }

        #region Alternatif

        private double RowCount3()
        {
            double _result = 0;

            _result = this._productBL.RowsCountProductAlternatif(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            _result = System.Math.Ceiling(_result / (double)_maxrow3);

            return _result;
        }

        private void ShowPage3(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount3();

            if (_prmCurrentPage - _maxlength3 > 0)
            {
                min = _prmCurrentPage - _maxlength3;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength3 < q)
            {
                max = _prmCurrentPage + _maxlength3 + 1;
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
                    this._navMark3[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark3[0]);
                    _pageNumberElement++;

                    this._navMark3[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark3[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark3[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark3[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag3 = true;
                    }

                    this._navMark3[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark3[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark3.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater3.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater3.DataBind();

                _flag3 = true;
                _nextFlag3 = false;
                _lastFlag3 = false;
                _navMark3 = _navMarkBackup;
            }
            else
            {
                this.DataPagerTopRepeater3.DataSource = from _query in _pageNumber
                                                        select _query;
                this.DataPagerTopRepeater3.DataBind();
            }
        }

        private Boolean IsCheckedAll3()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.ListRepeater3.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox3");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.ListRepeater3.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        protected void ShowDataDetail3(Int32 _prmCurrentPage)
        {
            this.TempHidden3.Value = "";

            this._page3 = _prmCurrentPage;

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.AllCheckBox3.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox3.ClientID + ", " + this.CheckHidden3.ClientID + ", '" + _awal3 + "', '" + _akhir3 + "', '" + _tempHidden3 + "' );");
            this.DeleteButton3.Attributes.Add("OnClick", "return AskYouFirst();");

            this.ListRepeater3.DataSource = this._productBL.GetListProductAlternatif(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _prmCurrentPage, _maxrow3);
            this.ListRepeater3.DataBind();

            this.AllCheckBox3.Checked = this.IsCheckedAll3();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "" && _nvcExtractor.GetValue(_app) == "Price")
            {
                this.Label3.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowPage3(_prmCurrentPage);
        }

        private Boolean IsChecked3(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHidden3.Value.Split(',');

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

        protected void AddButton3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addProductAlternatifPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton3_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden3.Value;
            string[] _tempSplit = _temp.Split(',');
            string _error = "";
            string _page = "0";

            this.ClearLabel();

            bool _result = this._productBL.DeleteMultiProductAlternatif(_tempSplit, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }

            this.CheckHidden3.Value = "";
            this.AllCheckBox3.Checked = false;

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error + "&" + _app + "=" + "Price");
        }

        protected void ListRepeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MsProduct_Alternatif _temp = (MsProduct_Alternatif)e.Item.DataItem;

                string _code = _temp.ProductCode.ToString() + "|" + _temp.AlternatifCode.ToString();
                string _alternatifCode = _temp.AlternatifCode.ToString();

                if (this.TempHidden3.Value == "")
                {
                    this.TempHidden3.Value = _code;
                }
                else
                {
                    this.TempHidden3.Value += "," + _code;
                }

                Literal _noLiteral3 = (Literal)e.Item.FindControl("NoLiteral3");
                _no3 = _page3 * _maxrow3;
                _no3 += 1;
                _no3 = _nomor3 + _no3;
                _noLiteral3.Text = _no3.ToString();
                _nomor3 += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox3");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden3.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal3 + "', '" + _akhir3 + "', '" + _cbox3 + "')");
                _listCheckbox.Checked = this.IsChecked3(_code);

                ImageButton _editButton3 = (ImageButton)e.Item.FindControl("EditButton3");
                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton3.Visible = false;
                }
                else
                {
                    _editButton3.Visible = true;
                    _editButton3.PostBackUrl = this._editProductAlternatifPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeKeyAlternatif + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_alternatifCode, ApplicationConfig.EncryptionKey));
                    _editButton3.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                }

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate3");
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

                Literal _currCode = (Literal)e.Item.FindControl("AlternatifCodeLiteral");
                _currCode.Text = HttpUtility.HtmlEncode(_temp.AlternatifCode);

            }
        }

        protected void DataPagerTopRepeater3_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager3")
            {
                this.ViewState[this._currPageKey3] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataDetail3(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey3]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox3");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton3");
                    _pageNumberLinkButton.CommandName = "DataPager3";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark3[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark3[0] = null;
                    }
                    else if (_pageNumber == this._navMark3[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark3[1] = null;
                    }
                    else if (_pageNumber == this._navMark3[2] && this._flag3 == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark3[2] = null;
                        this._nextFlag3 = true;
                        this._flag3 = true;
                    }
                    else if (_pageNumber == this._navMark3[3] && this._flag3 == true && this._nextFlag3 == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark3[3] = null;
                        this._lastFlag3 = true;
                    }
                    else
                    {
                        if (this._lastFlag3 == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark3[2] && this._flag3 == true)
                            this._flag3 = false;
                    }
                }
            }
        }

        protected void DataPagerButton3_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater3.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount3())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount3().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount3()) - 1;
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

            this.ViewState[this._currPageKey3] = _reqPage;

            this.ShowDataDetail3(_reqPage);
        }

        #endregion
    }
}