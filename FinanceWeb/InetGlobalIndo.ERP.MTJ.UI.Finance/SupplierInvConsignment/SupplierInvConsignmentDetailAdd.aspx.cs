using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.UI.Finance.NotaCreditCustomer;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierInvConsignment
{
    public partial class SupplierInvConsignmentDetailAdd : SupplierInvConsignmentBase
    {
        //private NotaCreditSupplierBL _notaCreditSupplierBL = new NotaCreditSupplierBL();
        //private AccountBL _accountBL = new AccountBL();
        //private UnitBL _unitBL = new UnitBL();
        //private SubledBL _subledBL = new SubledBL();
        //private EmployeeBL _empBL = new EmployeeBL();
        //private User_EmployeeBL _userEmpBL = new User_EmployeeBL();
        private PermissionBL _permBL = new PermissionBL();
        //private CurrencyBL _currencyBL = new CurrencyBL();
        private SupplierInvConsignmentBL _suppInvConBL = new SupplierInvConsignmentBL();
        private UnitBL _unitBL = new UnitBL();

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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ClearData();
                this.SetAttribute();

                this.SJTypeDDL();
            }
        }

        protected void SetAttribute()
        {
            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");

            this.GoImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/go.jpg";
            //this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
            //this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
            //this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
            this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

            this.TransNmbrHiddenField.Value = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            this.SuppCodeHiddenField.Value = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._suppCode), ApplicationConfig.EncryptionKey);

            this.SearchButton.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=productStock','_popSearch','width=750,height=300,toolbar=0,location=0,status=0,scrollbars=1')";
            String spawnJS = "<script language='JavaScript'>\n";
            spawnJS += "function findProduct(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.ProductCodeTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "}\n";
            spawnJS += "</script>\n";
            this.javascriptReceiver.Text = spawnJS;
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();
            //this.AccountTextBox.Text = "";
            //this.AccountDropDownList.SelectedValue = "null";
            //this.FgSubledHiddenField.Value = "";
            this.SJTypeDropDownList.Items.Clear();
            this.SJTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //this.SJNoDropDownList.Items.Clear();
            //this.SJNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //this.RemarkTextBox.Text = "";
            //this.PriceTextBox.Text = "0";
            //this.AmountTextBox.Text = "0";
            //this.QtyTextBox.Text = "1";
            //this.UnitDropDownList.SelectedValue = "null";
            //this.DecimalPlaceHiddenField.Value = "";

            //string _currHeader = this._notaCreditSupplierBL.GetSingleFINCNSuppHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).CurrCode;
            //byte _decimalPlace = _currencyBL.GetDecimalPlace(_currHeader);
            //this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }

        //public void SJNoDDL()
        //{
        //    this.SJNoDropDownList.Items.Clear();
        //    this.SJNoDropDownList.DataTextField = "SJ_No";
        //    this.SJNoDropDownList.DataValueField = "SJ_No";
        //    this.SJNoDropDownList.DataSource = this._suppInvConBL.GetDDLforSJNo(this.SuppCodeHiddenField.Value, this.SJTypeDropDownList.SelectedValue);
        //    this.SJNoDropDownList.DataBind();
        //    this.SJNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void SJTypeDDL()
        {
            this.SJTypeDropDownList.Items.Clear();
            this.SJTypeDropDownList.DataTextField = "SJ_Type";
            this.SJTypeDropDownList.DataValueField = "SJ_Type";
            this.SJTypeDropDownList.DataSource = this._suppInvConBL.GetDDLforSJType(this.SuppCodeHiddenField.Value);
            this.SJTypeDropDownList.DataBind();
            this.SJTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SJTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowDataDetail(0);
            //if (this.SJTypeDropDownList.SelectedValue != "null")
            //{
            //    //this.SJNoDDL();
            //    this.ListRepeater.DataSource = this._suppInvConBL.GetListVFNSIConsignmentOustanding(this.SuppCodeHiddenField.Value, this.SJTypeDropDownList.SelectedValue,"");
            //    this.ListRepeater.DataBind();
            //}
            //else
            //{
            //    this.SJNoDropDownList.Items.Clear();
            //    this.SJNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //}
            //this.GetSubled();
        }

        //protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    String _retun = _suppInvConBL.SaveDetailSuppInvCon(this.SJTypeDropDownList.SelectedValue, this.SJNoDropDownList.SelectedValue, this.TransNmbrHiddenField.Value, this.SuppCodeHiddenField.Value);

        //    if (_retun == "")
        //    {
        //        Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNmbrHiddenField.Value, ApplicationConfig.EncryptionKey)));
        //    }
        //    else
        //    {
        //        this.WarningLabel.Text = _retun;
        //    }
        //}

        //protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNmbrHiddenField.Value, ApplicationConfig.EncryptionKey)));
        //}

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNmbrHiddenField.Value, ApplicationConfig.EncryptionKey)));
        }

        //protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    this.ClearData();
        //}

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                V_FNSIConsignmentOustanding _temp = (V_FNSIConsignmentOustanding)e.Item.DataItem;

                String _code = _temp.SJ_No.ToString() + "|" + _temp.Product_Code.ToString();

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

                ImageButton _addButton = (ImageButton)e.Item.FindControl("AddButton");
                this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);
                _addButton.CommandName = "Add";
                _addButton.CommandArgument = _code;
                _addButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";

                if (this._permAdd == PermissionLevel.NoAccess)
                {
                    _addButton.Visible = false;
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

                Literal _sjNo = (Literal)e.Item.FindControl("SJNoLiteral");
                _sjNo.Text = HttpUtility.HtmlEncode(_temp.SJ_No);

                Literal _sjDate = (Literal)e.Item.FindControl("SJDateLiteral");
                _sjDate.Text = HttpUtility.HtmlEncode(DateFormMapper.GetValue(_temp.TransDate));
                
                Literal _fileNmbr = (Literal)e.Item.FindControl("FileNmbrLiteral");
                _fileNmbr.Text = HttpUtility.HtmlEncode(_temp.FileNmbr);

                Literal _productCode = (Literal)e.Item.FindControl("ProductCodeLiteral");
                _productCode.Text = HttpUtility.HtmlEncode(_temp.Product_Code);
                
                Literal _productName = (Literal)e.Item.FindControl("ProductNameLiteral");
                _productName.Text = HttpUtility.HtmlEncode(_temp.Product_Name);

                Literal _qty = (Literal)e.Item.FindControl("QtyLiteral");
                _qty.Text = HttpUtility.HtmlEncode((_temp.Qty == 0) ? "0" : Convert.ToDecimal(_temp.Qty).ToString("#,###0.##"));

                Literal _unit = (Literal)e.Item.FindControl("UnitLiteral");
                _unit.Text = HttpUtility.HtmlEncode(_unitBL.GetUnitNameByCode(_temp.Unit));
            }
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                string[] _temp = e.CommandArgument.ToString().Split('|');
                String _result = _suppInvConBL.SaveDetailFINSuppInvConsignmentDt(this.SJTypeDropDownList.SelectedValue, _temp[0], this.TransNmbrHiddenField.Value, _temp[1], this.SuppCodeHiddenField.Value);
                if (_result == "")
                {
                    ImageButton _addButton = (ImageButton)e.Item.FindControl("AddButton");
                    _addButton.Visible = false;
                    this.WarningLabel.Text = "Data have been added.";
                }
                else
                {
                    this.WarningLabel.Text = _result;
                }
            }
        }

        protected void ProductCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            this.ShowDataDetail(0);
        }

        protected void ShowDataDetail(Int32 _prmCurrentPage)
        {
            if (this.SJTypeDropDownList.SelectedValue != "null")
            {
                this._page = _prmCurrentPage;
                String _code = this.SuppCodeHiddenField.Value + "|" + this.SJTypeDropDownList.SelectedValue + "|" + this.ProductCodeTextBox.Text;
                this.ListRepeater.DataSource = this._suppInvConBL.GetListVFNSIConsignmentOustanding(_prmCurrentPage, _maxrow, "Code", _code);
                this.ListRepeater.DataBind();
                this.ShowPage(_prmCurrentPage);
            }
        }

        private double RowCount()
        {
            double _result = 0;
            String _code = this.SuppCodeHiddenField.Value + "|" + this.SJTypeDropDownList.SelectedValue + "|" + this.ProductCodeTextBox.Text;
            _result = this._suppInvConBL.RowsCountVFNSIConsignmentOustanding("Code", _code);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
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

        protected void GoImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowDataDetail(0);
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
    }
}