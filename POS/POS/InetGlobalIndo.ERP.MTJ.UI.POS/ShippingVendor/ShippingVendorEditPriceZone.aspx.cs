using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingVendor
{
    public partial class ShippingVendorEditPriceZone : ShippingVendorBase
    {
        private VendorBL _vendorBL = new VendorBL();
        private PermissionBL _permBL = new PermissionBL();
        private CityBL _cityBL = new CityBL();
        private BankBL _bankBL = new BankBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private TermBL _termBL = new TermBL();
        private ShippingBL _shippingBL = new ShippingBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _currPageKey = "CurrentPage";

        private int _page;
        private int _maxrow = 15;
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

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {

                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.SinglePriceButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/price.jpg";
                this.MultiPriceButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/multiple_price.jpg";
                this.CodeTextBox.Attributes.Add("ReadOnly", "True");
                this.NameTextBox.Attributes.Add("ReadOnly", "True");

                //this.SetButtonPermission();
                this.ShowData();
                this.SinglePriceButton.Visible = false;
                this.MultiplePricePanel.Visible = false;
            }
        }

        private void ClearLabel()
        {
            //this.WarningLabel.Text = "";
            this.WarningLabel2.Text = "";
        }

        public void ShowData()
        {
            Int32 _reqPage = 0;
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            POSMsShippingVendor _posMsShippingVendor = this._vendorBL.GetSingle(_tempSplit[0]);
            this.CodeTextBox.Text = _posMsShippingVendor.VendorCode.ToString();
            this.NameTextBox.Text = _posMsShippingVendor.VendorName.ToString();

            if (_posMsShippingVendor.FgZone == 'Y')
                this.FgZoneCheckBox.Checked = true;
            else
                this.FgZoneCheckBox.Checked = false;

            this.ShowDataDetail(_reqPage);
            //this.ShowDataDetail2(_reqPage);
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void MultiPriceButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowDataDetail2(0);
            this.PriceSinglePanel.Visible = false;
            this.MultiplePricePanel.Visible = true;
            this.MultiPriceButton.Visible = false;
            this.SinglePriceButton.Visible = true;
        }

        protected void SinglePriceButton_Click(object sender, ImageClickEventArgs e)
        {
            this.PriceSinglePanel.Visible = true;
            this.MultiplePricePanel.Visible = false;
            this.MultiPriceButton.Visible = true;
            this.SinglePriceButton.Visible = false;
        }

        #region Detail

        private double RowCount()
        {
            double _result = 0;
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            _result = this._shippingBL.RowsCountPOSMsZonePriceForVendor(_tempSplit[0]);
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

        public void ShowDataDetail(Int32 _prmCurrentPage)
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._shippingBL.GetZoneByVendor(_prmCurrentPage, _maxrow, _tempSplit[0], false);
            }
            this.ListRepeater.DataBind();

            //this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            //this.AllCheckBox.Checked = this.IsCheckedAll();

            //this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            General_TemporaryTable _temp = (General_TemporaryTable)e.Item.DataItem;
            //Primary 1 = VendorCode, Primary 2 = ProductShape, Primary 3 = Weight
            string _code = _temp.PrimaryKey1.ToString() + "|" + _temp.PrimaryKey2.ToString() + "|" + _temp.PrimaryKey3;

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

            ImageButton _saveButton = (ImageButton)e.Item.FindControl("SaveButton");
            _saveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
            _saveButton.CommandName = "Save";
            _saveButton.CommandArgument = _code;
            _saveButton.Visible = false;

            ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
            _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
            _editButton.CommandName = "Edit";
            _editButton.CommandArgument = _code;

            Literal _productShapeLiteral = (Literal)e.Item.FindControl("ProductShapeLiteral");
            if (_temp.PrimaryKey2 == "0")
            {
                _productShapeLiteral.Text = "Document";
            }
            else if (_temp.PrimaryKey2 == "1")
            {
                _productShapeLiteral.Text = "Non Document";
            }
            else if (_temp.PrimaryKey2 == "2")
            {
                _productShapeLiteral.Text = "International Priority";
            }

            Literal _weightLiteral = (Literal)e.Item.FindControl("WeightLiteral");
            _weightLiteral.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.PrimaryKey3).ToString("#0.#"));

            String[] _tempSplit = _temp.FieldDescription.Split(',');
            int _max = _tempSplit.Count() - 1;
            int _count = 0;

            if (_count <= _max & _temp.Field1 != null)
            {
                this.Field1Literal.Text = _tempSplit[_count];

                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field1 == null) ? "0" : Convert.ToDecimal(_temp.Field1).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");

                //Panel _fieldPanel = new Panel();
                //_fieldPanel = (Panel)(Page.FindControl("Field1Panel"));
                //_fieldPanel.Visible = false;
            }
            else
            {
                this.Field1Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field2 != null)
            {
                this.Field2Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field2 == null) ? "0" : Convert.ToDecimal(_temp.Field2).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field2Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field3 != null)
            {
                this.Field3Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field3 == null) ? "0" : Convert.ToDecimal(_temp.Field3).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field3Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field4 != null)
            {
                this.Field4Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field4 == null) ? "0" : Convert.ToDecimal(_temp.Field4).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field4Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field5 != null)
            {
                this.Field5Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field5 == null) ? "0" : Convert.ToDecimal(_temp.Field5).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field5Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field6 != null)
            {
                this.Field6Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field6 == null) ? "0" : Convert.ToDecimal(_temp.Field6).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field6Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field7 != null)
            {
                this.Field7Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field7 == null) ? "0" : Convert.ToDecimal(_temp.Field7).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field7Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field8 != null)
            {
                this.Field8Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field8 == null) ? "0" : Convert.ToDecimal(_temp.Field8).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field8Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field9 != null)
            {
                this.Field9Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field9 == null) ? "0" : Convert.ToDecimal(_temp.Field9).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field9Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field10 != null)
            {
                this.Field10Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field10 == null) ? "0" : Convert.ToDecimal(_temp.Field10).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field10Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field11 != null)
            {
                this.Field11Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field11 == null) ? "0" : Convert.ToDecimal(_temp.Field11).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field11Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field12 != null)
            {
                this.Field12Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field12 == null) ? "0" : Convert.ToDecimal(_temp.Field12).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field12Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field13 != null)
            {
                this.Field13Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field13 == null) ? "0" : Convert.ToDecimal(_temp.Field13).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field13Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field14 != null)
            {
                this.Field14Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field14 == null) ? "0" : Convert.ToDecimal(_temp.Field14).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field14Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field15 != null)
            {
                this.Field15Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field15 == null) ? "0" : Convert.ToDecimal(_temp.Field15).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field15Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field16 != null)
            {
                this.Field16Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field16 == null) ? "0" : Convert.ToDecimal(_temp.Field16).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field16Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field17 != null)
            {
                this.Field17Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field17 == null) ? "0" : Convert.ToDecimal(_temp.Field17).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field17Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field18 != null)
            {
                this.Field18Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field18 == null) ? "0" : Convert.ToDecimal(_temp.Field18).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field18Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field19 != null)
            {
                this.Field19Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field19 == null) ? "0" : Convert.ToDecimal(_temp.Field19).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field19Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field20 != null)
            {
                this.Field20Literal.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field20 == null) ? "0" : Convert.ToDecimal(_temp.Field20).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.Field20Panel.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

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
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            String[] _tempSplit = e.CommandArgument.ToString().Split('|');
            General_TemporaryTable _temp = this._shippingBL.GetSingleZoneByVendor(_tempSplit[0], false);
            if (_temp != null)
            {
                String[] _tempSplit2 = _temp.FieldDescription.Split(',');
                int _max = _tempSplit2.Count();

                if (e.CommandName == "Edit")
                {
                    for (int i = 1; i <= _max; i++)
                    {
                        TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + i + "TextBox");
                        _valueTextBox.Attributes.Remove("ReadOnly");
                        _valueTextBox.Attributes.Remove("Style");
                    }
                    ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                    _editButton.Visible = false;
                    ImageButton _saveButton = (ImageButton)e.Item.FindControl("SaveButton");
                    _saveButton.Visible = true;
                }
                else if (e.CommandName == "Save")
                {
                    bool _result = false;
                    for (int i = 1; i <= _max; i++)
                    {
                        TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + i + "TextBox");
                        POSMsZonePrice _posMsZonePrice = this._shippingBL.GetSinglePOSMsZonePriceForEdit(_tempSplit2[i - 1], _tempSplit[1], Convert.ToDecimal(_tempSplit[2]));
                        if (_posMsZonePrice != null)
                        {
                            if ((((_posMsZonePrice.Price == null) ? 0 : _posMsZonePrice.Price)) != ((_valueTextBox.Text == "") ? 0 : Convert.ToDecimal(_valueTextBox.Text)))
                            {
                                _posMsZonePrice.Price = (_valueTextBox.Text == "") ? 0 : Convert.ToDecimal(_valueTextBox.Text);
                                _result = this._shippingBL.EditSubmit();
                            }
                        }
                    }

                    if (_result)
                    {
                        for (int i = 1; i <= _max; i++)
                        {
                            TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + i + "TextBox");
                            _valueTextBox.Attributes.Add("ReadOnly", "True");
                            _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
                        }
                        ImageButton _saveButton = (ImageButton)e.Item.FindControl("SaveButton");
                        _saveButton.Visible = false;
                        ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                        _editButton.Visible = true;
                    }
                    else
                    {
                        WarningLabel.Text = "You Failed Edit Price";
                    }
                }

            }
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataDetail(Convert.ToInt32(e.CommandArgument));
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

            this.ShowDataDetail(_reqPage);
        }

        #endregion

        #region Detail Multiple Price

        private double RowCount2()
        {
            double _result = 0;
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            _result = this._shippingBL.RowsCountPOSMsZoneMultiplePriceForVendor(_tempSplit[0]);
            //_result = this._shippingBL.RowsCountPOSMsZoneMultiplePrice("Code" , _tempSplit[0]);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private void ShowPage2(Int32 _prmCurrentPage2)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount2();

            if (_prmCurrentPage2 - _maxlength > 0)
            {
                min = _prmCurrentPage2 - _maxlength;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage2 + _maxlength < q)
            {
                max = _prmCurrentPage2 + _maxlength + 1;
            }
            else
            {
                max = Convert.ToDecimal(q);
            }

            if (_prmCurrentPage2 > 0)
                _addElement += 2;

            if (_prmCurrentPage2 < q - 1)
                _addElement += 2;

            i = Convert.ToInt32(min);
            _pageNumber = new Int32[Convert.ToInt32(max - min) + _addElement];
            if (_pageNumber.Length != 0)
            {
                //NB: Prev Or First
                if (_prmCurrentPage2 > 0)
                {
                    this._navMark[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[0]);
                    _pageNumberElement++;

                    this._navMark[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage2 - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage2 < q - 1)
                {
                    this._navMark[2] = Convert.ToInt32(_prmCurrentPage2 + 1);

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
                this.DataPagerTop2Repeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTop2Repeater.DataBind();

                _flag = true;
                _nextFlag = false;
                _lastFlag = false;
                _navMark = _navMarkBackup;
            }
            else
            {
                this.DataPagerTop2Repeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTop2Repeater.DataBind();
            }
        }

        public void ShowDataDetail2(Int32 _prmCurrentPage2)
        {
            this.TempHidden2.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage2;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            if (this._permView == PermissionLevel.NoAccess)
            {
                this.MultipleListRepeater.DataSource = null;
            }
            else
            {
                this.MultipleListRepeater.DataSource = this._shippingBL.GetZoneByVendor(_prmCurrentPage2, _maxrow, _tempSplit[0], true);
            }
            this.MultipleListRepeater.DataBind();
            
            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage2(_prmCurrentPage2);
            }
        }

        protected void DataPagerTop2Repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataDetail2(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTop2Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumber2TextBox");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLink2Button");
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

        protected void DataPager2Button_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTop2Repeater.Controls)
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

            this.ViewState[this._currPageKey] = _reqPage;

            this.ShowDataDetail2(_reqPage);
        }

        protected void MultipleListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            String[] _tempSplit = e.CommandArgument.ToString().Split('|');
            General_TemporaryTable _temp = this._shippingBL.GetSingleZoneByVendor(_tempSplit[0], true);
            if (_temp != null)
            {
                String[] _tempSplit2 = _temp.FieldDescription.Split(',');
                int _max = _tempSplit2.Count();

                if (e.CommandName == "Edit")
                {
                    for (int i = 1; i <= _max; i++)
                    {
                        TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + i + "TextBox");
                        _valueTextBox.Attributes.Remove("ReadOnly");
                        _valueTextBox.Attributes.Remove("Style");
                    }
                    ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                    _editButton.Visible = false;
                    ImageButton _saveButton = (ImageButton)e.Item.FindControl("SaveButton");
                    _saveButton.Visible = true;
                }
                else if (e.CommandName == "Save")
                {
                    bool _result = false;
                    for (int i = 1; i <= _max; i++)
                    {
                        TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + i + "TextBox");
                        POSMsZoneMultiplePrice _posMsZonePrice = this._shippingBL.GetSinglePOSMsZoneMultiplePriceForEdit(_tempSplit2[i - 1], _tempSplit[1], Convert.ToDecimal(_tempSplit[2]), Convert.ToDecimal(_tempSplit[3]));
                        if (_posMsZonePrice != null)
                        {
                            if ((((_posMsZonePrice.Price == null) ? 0 : _posMsZonePrice.Price)) != ((_valueTextBox.Text == "") ? 0 : Convert.ToDecimal(_valueTextBox.Text)))
                            {
                                _posMsZonePrice.Price = (_valueTextBox.Text == "") ? 0 : Convert.ToDecimal(_valueTextBox.Text);
                                _result = this._shippingBL.EditSubmit();
                            }
                        }
                    }

                    if (_result)
                    {
                        for (int i = 1; i <= _max; i++)
                        {
                            TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + i + "TextBox");
                            _valueTextBox.Attributes.Add("ReadOnly", "True");
                            _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
                        }
                        ImageButton _saveButton = (ImageButton)e.Item.FindControl("SaveButton");
                        _saveButton.Visible = false;
                        ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                        _editButton.Visible = true;
                    }
                    else
                    {
                        WarningLabel2.Text = "You Failed Edit Price";
                    }
                }

            }
        }

        protected void MultipleListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            General_TemporaryTable _temp = (General_TemporaryTable)e.Item.DataItem;
            //Primary 1 = VendorCode, Primary 2 = ProductShape, Primary 3 = Weight1, Primary 4 = Weight4
            string _code = _temp.PrimaryKey1.ToString() + "|" + _temp.PrimaryKey2.ToString() + "|" + _temp.PrimaryKey3 + "|" + _temp.PrimaryKey4;

            if (this.TempHidden2.Value == "")
            {
                this.TempHidden2.Value = _code;
            }
            else
            {
                this.TempHidden2.Value += "," + _code;
            }

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
            _no = _page * _maxrow;
            _no += 1;
            _no = _nomor + _no;
            _noLiteral.Text = _no.ToString();
            _nomor += 1;

            ImageButton _saveButton = (ImageButton)e.Item.FindControl("SaveButton");
            _saveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
            _saveButton.CommandName = "Save";
            _saveButton.CommandArgument = _code;
            _saveButton.Visible = false;

            ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
            _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
            _editButton.CommandName = "Edit";
            _editButton.CommandArgument = _code;

            Literal _productShapeLiteral = (Literal)e.Item.FindControl("ProductShapeLiteral");
            if (_temp.PrimaryKey2 == "0")
            {
                _productShapeLiteral.Text = "Document";
            }
            else if (_temp.PrimaryKey2 == "1")
            {
                _productShapeLiteral.Text = "Non Document";
            }
            else if (_temp.PrimaryKey2 == "2")
            {
                _productShapeLiteral.Text = "International Priority";
            }

            Literal _weight1Literal = (Literal)e.Item.FindControl("Weight1Literal");
            _weight1Literal.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.PrimaryKey3).ToString("#0.#"));

            Literal _weight2Literal = (Literal)e.Item.FindControl("Weight2Literal");
            _weight2Literal.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.PrimaryKey4).ToString("#0.#"));

            String[] _tempSplit = _temp.FieldDescription.Split(',');
            int _max = _tempSplit.Count() - 1;
            int _count = 0;

            this.FieldPanel1.Visible = true;
            this.FieldPanel2.Visible = true;
            this.FieldPanel3.Visible = true;
            this.FieldPanel4.Visible = true;
            this.FieldPanel5.Visible = true;
            this.FieldPanel6.Visible = true;
            this.FieldPanel7.Visible = true;
            this.FieldPanel8.Visible = true;
            this.FieldPanel9.Visible = true;
            this.FieldPanel10.Visible = true;
            this.FieldPanel11.Visible = true;
            this.FieldPanel12.Visible = true;
            this.FieldPanel13.Visible = true;
            this.FieldPanel14.Visible = true;
            this.FieldPanel15.Visible = true;
            this.FieldPanel16.Visible = true;
            this.FieldPanel17.Visible = true;
            this.FieldPanel18.Visible = true;
            this.FieldPanel19.Visible = true;
            this.FieldPanel20.Visible = true;

            if (_count <= _max & _temp.Field1 != null)
            {
                this.FieldLiteral1.Text = _tempSplit[_count];

                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field1 == null) ? "0" : Convert.ToDecimal(_temp.Field1).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel1.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field2 != null)
            {
                this.FieldLiteral2.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field2 == null) ? "0" : Convert.ToDecimal(_temp.Field2).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel2.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field3 != null)
            {
                this.FieldLiteral3.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field3 == null) ? "0" : Convert.ToDecimal(_temp.Field3).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel3.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field4 != null)
            {
                this.FieldLiteral4.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field4 == null) ? "0" : Convert.ToDecimal(_temp.Field4).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel4.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field5 != null)
            {
                this.FieldLiteral5.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field5 == null) ? "0" : Convert.ToDecimal(_temp.Field5).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel5.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field6 != null)
            {
                this.FieldLiteral6.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field6 == null) ? "0" : Convert.ToDecimal(_temp.Field6).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel6.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field7 != null)
            {
                this.FieldLiteral7.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field7 == null) ? "0" : Convert.ToDecimal(_temp.Field7).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel7.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field8 != null)
            {
                this.FieldLiteral8.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field8 == null) ? "0" : Convert.ToDecimal(_temp.Field8).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel8.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field9 != null)
            {
                this.FieldLiteral9.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field9 == null) ? "0" : Convert.ToDecimal(_temp.Field9).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel9.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field10 != null)
            {
                this.FieldLiteral10.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field10 == null) ? "0" : Convert.ToDecimal(_temp.Field10).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel10.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field11 != null)
            {
                this.FieldLiteral11.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field11 == null) ? "0" : Convert.ToDecimal(_temp.Field11).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel11.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field12 != null)
            {
                this.FieldLiteral12.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field12 == null) ? "0" : Convert.ToDecimal(_temp.Field12).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel12.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field13 != null)
            {
                this.FieldLiteral13.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field13 == null) ? "0" : Convert.ToDecimal(_temp.Field13).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel13.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field14 != null)
            {
                this.FieldLiteral14.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field14 == null) ? "0" : Convert.ToDecimal(_temp.Field14).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel14.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field15 != null)
            {
                this.FieldLiteral15.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field15 == null) ? "0" : Convert.ToDecimal(_temp.Field15).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel15.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field16 != null)
            {
                this.FieldLiteral16.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field16 == null) ? "0" : Convert.ToDecimal(_temp.Field16).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel16.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field17 != null)
            {
                this.FieldLiteral17.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field17 == null) ? "0" : Convert.ToDecimal(_temp.Field17).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel17.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field18 != null)
            {
                this.FieldLiteral18.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field18 == null) ? "0" : Convert.ToDecimal(_temp.Field18).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel18.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field19 != null)
            {
                this.FieldLiteral19.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field19 == null) ? "0" : Convert.ToDecimal(_temp.Field19).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel19.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

            if (_count <= _max & _temp.Field20 != null)
            {
                this.FieldLiteral20.Text = _tempSplit[_count];
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Text = (_temp.Field20 == null) ? "0" : Convert.ToDecimal(_temp.Field20).ToString("#0.#");
                _valueTextBox.Attributes.Add("ReadOnly", "True");
                _valueTextBox.Attributes.Add("Style", "Background : #CCCCCC");
            }
            else
            {
                this.FieldPanel20.Visible = false;
                TextBox _valueTextBox = (TextBox)e.Item.FindControl("Value" + Convert.ToString(_count + 1) + "TextBox");
                _valueTextBox.Visible = false;
            }
            _count += 1;

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
        }

        #endregion

       
}

}