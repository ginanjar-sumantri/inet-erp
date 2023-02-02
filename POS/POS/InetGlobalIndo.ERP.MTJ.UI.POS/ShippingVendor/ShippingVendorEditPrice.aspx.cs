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
    public partial class ShippingVendorEditPrice : ShippingVendorBase
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
                this.CodeTextBox.Attributes.Add("ReadOnly", "True");
                this.NameTextBox.Attributes.Add("ReadOnly", "True");

                //this.SetButtonPermission();
                this.ShowData();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        //private void SetButtonPermission()
        //{
        //    this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

        //    if (this._permEdit == PermissionLevel.NoAccess)
        //    {
        //        this.EditButton.Visible = false;
        //    }

        //    this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

        //    if (this._permAdd == PermissionLevel.NoAccess)
        //    {
        //        this.AddButton.Visible = false;
        //    }

        //    this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

        //    if (this._permDelete == PermissionLevel.NoAccess)
        //    {
        //        this.DeleteButton.Visible = false;
        //    }
        //}

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
        }

        //protected void EditButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        //}

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        #region Detail

        private double RowCount()
        {
            double _result = 0;
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            _result = this._shippingBL.RowsCountShippingTypeDtForVendor(_tempSplit[0], Convert.ToChar(_tempSplit[1]));
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
                this.ListRepeater.DataSource = this._shippingBL.GetListShippingTypeDtForVendor(_prmCurrentPage, _maxrow, _tempSplit[0], Convert.ToChar(_tempSplit[1]));
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
            V_POSMsShipping _temp = (V_POSMsShipping)e.Item.DataItem;
            string _code = _temp.ShippingZonaTypeCode.ToString() + "|" + _temp.CityCode.ToString() + "|" + _temp.ProductShape;

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


            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                _editButton.Visible = false;
            }

            Literal _shippingTypeNameLiteral = (Literal)e.Item.FindControl("ShippingTypeNameLiteral");
            _shippingTypeNameLiteral.Text = HttpUtility.HtmlEncode(_temp.ShippingZonaTypeName);

            Literal _productShapeLiteral = (Literal)e.Item.FindControl("ProductShapeLiteral");
            if (_temp.ProductShape == "0")
            {
                _productShapeLiteral.Text = "Document";
            }
            else if (_temp.ProductShape == "1")
            {
                _productShapeLiteral.Text = "Non Document";
            }

            Literal _cityLiteral = (Literal)e.Item.FindControl("CityLiteral");
            _cityLiteral.Text = HttpUtility.HtmlEncode(_temp.CityName);

            Literal _price1Literal = (Literal)e.Item.FindControl("Price1Literal");
            _price1Literal.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.Price1).ToString("#,#"));

            Literal _price2Literal = (Literal)e.Item.FindControl("Price2Literal");
            _price2Literal.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.Price2).ToString("#,#"));

            TextBox _price1TextBox = (TextBox)e.Item.FindControl("Price1TextBox");
            //_price1Literal.Text
            _price1TextBox.Visible = false;

            TextBox _price2TextBox = (TextBox)e.Item.FindControl("Price2TextBox");
            //_price1Literal.Text
            _price2TextBox.Visible = false;

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
            if (e.CommandName == "Edit")
            {

                String[] _tempSplit = e.CommandArgument.ToString().Split('|');
                POSMsShippingTypeDt _posMsShippingTypeDt = this._shippingBL.GetSinglePOSMsShippingTypeDt(_tempSplit[0], _tempSplit[1], _tempSplit[2]);

                Literal _price1Literal = (Literal)e.Item.FindControl("Price1Literal");
                _price1Literal.Visible = false;

                Literal _price2Literal = (Literal)e.Item.FindControl("Price2Literal");
                _price2Literal.Visible = false;

                TextBox _price1TextBox = (TextBox)e.Item.FindControl("Price1TextBox");
                _price1TextBox.Visible = true;
                _price1TextBox.Text = Convert.ToDecimal(_posMsShippingTypeDt.Price1).ToString("#,#");

                TextBox _price2TextBox = (TextBox)e.Item.FindControl("Price2TextBox");
                _price2TextBox.Visible = true;
                _price2TextBox.Text = Convert.ToDecimal(_posMsShippingTypeDt.Price2).ToString("#,#");

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                _editButton.Visible = false;
                ImageButton _saveButton = (ImageButton)e.Item.FindControl("SaveButton");
                _saveButton.Visible = true;
            }
            else if (e.CommandName == "Save")
            {
                String[] _tempSplit = e.CommandArgument.ToString().Split('|');
                POSMsShippingTypeDt _posMsShippingTypeDt = this._shippingBL.GetSinglePOSMsShippingTypeDt(_tempSplit[0], _tempSplit[1], _tempSplit[2]);

                TextBox _price1TextBox = (TextBox)e.Item.FindControl("Price1TextBox");
                _posMsShippingTypeDt.Price1 = Convert.ToDecimal(_price1TextBox.Text);


                TextBox _price2TextBox = (TextBox)e.Item.FindControl("Price2TextBox");
                _posMsShippingTypeDt.Price2 = Convert.ToDecimal(_price2TextBox.Text);


                bool _result = this._shippingBL.EditSubmit();
                if (_result)
                {
                    Literal _price1Literal = (Literal)e.Item.FindControl("Price1Literal");
                    _price1Literal.Text = Convert.ToDecimal(_price1TextBox.Text).ToString("#,#");
                    _price1TextBox.Visible = false;
                    _price1Literal.Visible = true;

                    Literal _price2Literal = (Literal)e.Item.FindControl("Price2Literal");
                    _price2Literal.Text = Convert.ToDecimal(_price2TextBox.Text).ToString("#,#");
                    _price2TextBox.Visible = false;
                    _price2Literal.Visible = true;

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

        //protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    string _error = "";
        //    string _temp = this.CheckHidden.Value;
        //    string[] _tempSplit = _temp.Split(',');

        //    this.ClearLabel();

        //    bool _result = this._vendorBL.DeleteMultiDt(_tempSplit);

        //    if (_result == true)
        //    {
        //        _error = "Delete Success";
        //    }
        //    else
        //    {
        //        _error = "Delete Failed";
        //    }
        //    this.WarningLabel.Text = _error;
        //    this.CheckHidden.Value = "";
        //    this.AllCheckBox.Checked = false;
        //    this.ShowDataDetail(0);
        //}

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


    }
}