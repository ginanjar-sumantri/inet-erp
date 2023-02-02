using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.Customer
{
    public partial class CustomerView : CustomerBase
    {
        private CustomerBL _customerBL = new CustomerBL();
        private TermBL _termBL = new TermBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CityBL _cityBL = new CityBL();
        private RegionalBL _regionalBL = new RegionalBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _no = 0;
        private int _nomor = 0;

        private int _page2;
        private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _no2 = 0;
        private int _nomor2 = 0;

        private string _app = "Aplikasi";

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

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.AddButton1.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton1.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowData();
            }
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
                this.AddButton.Visible = false;
                this.AddButton1.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
                this.DeleteButton1.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowData()
        {
            MsCustomer _msCustomer = this._customerBL.GetSingleCust(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            MsCustCollect _msCustCollect = this._customerBL.GetSingleCustCollect(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            Master_CustomerExtension _msCustExtension = this._customerBL.GetSingleCustExtension(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CustCodeTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            this.CustNameTextBox.Text = _msCustomer.CustName;
            this.CustGroupTextBox.Text = _customerBL.GetCustGroupNameByCode(_msCustomer.CustGroup);
            this.CustTypeTextBox.Text = _customerBL.GetCustTypeNameByCode(_msCustomer.CustType);
            this.VerificationCodeTextBox.Text = _msCustomer.CustVerificationCode;
            this.Addr1TextBox.Text = _msCustomer.Address1;
            this.Addr2TextBox.Text = _msCustomer.Address2;
            this.CityTextBox.Text = _cityBL.GetCityNameByCode(_msCustomer.City);
            this.ZipCodeTextBox.Text = _msCustomer.ZipCode;
            this.PhoneTextBox.Text = _msCustomer.Phone;
            this.FaxTextBox.Text = _msCustomer.Fax;
            this.EmailTextBox.Text = _msCustomer.Email;
            this.CurrencyTextBox.Text = _msCustomer.CurrCode;
            this.TermTextBox.Text = _termBL.GetTermNameByCode(_msCustomer.Term);
            this.NPWPTextBox.Text = _msCustomer.NPWP;
            //this.NPPKPTextBox.Text = _msCustomer.NPPKP;
            //this.NPWPAddressTextBox.Text = _msCustomer.NPWPAddress;
            this.IsActiveCheckBox.Checked = CustomerDataMapper.IsActive(_msCustomer.FgActive);
            this.ContactNameTextBox.Text = _msCustomer.ContactName;
            this.ContactTitleTextBox.Text = _msCustomer.ContactTitle;
            this.ContactEmailTextBox.Text = _msCustomer.ContactEmail;
            this.ContactHPTextBox.Text = _msCustomer.ContactHP;
            this.FgLimitCheckBox.Checked = CustomerDataMapper.IsLimit(_msCustomer.FgLimit);
            this.CustLimitTextBox.Text = (_msCustomer.CustLimit == 0) ? "0" : Convert.ToDecimal(_msCustomer.CustLimit).ToString("#,###.##");
            this.UseLimitTextBox.Text = (_msCustomer.UseLimit == 0) ? "0" : Convert.ToDecimal(_msCustomer.UseLimit).ToString("#,###.##");
            this.GracePeriodTextBox.Text = Convert.ToString(_msCustomer.GracePeriod);
            this.PrintFPCheckBox.Checked = Convert.ToBoolean(_msCustomer.PrintFP);
            this.StampCheckBox.Checked = Convert.ToBoolean(_msCustomer.FgStamp);
            this.FgPPNCheckBox.Checked = CustomerDataMapper.IsPPN(_msCustomer.FgPPN);
            this.DueDateCycleTextBox.Text = _msCustomer.DueDateCycle.ToString();
            this.RemarkTextBox.Text = _msCustomer.Remark;

            if (_msCustCollect != null)
            {
                this.BillTextBox.Text = _customerBL.GetCustCollectNameByCode(_msCustCollect.CustCollect);
            }

            if (_msCustExtension != null)
            {
                this.SalesPersonTextBox.Text = _employeeBL.GetEmpNameByCode(_msCustExtension.EmpNumb);
            }
            else
            {
                this.SalesPersonTextBox.Text = "";
            }

            this.TempHidden.Value = "";
            this.TempHidden2.Value = "";

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._customerBL.GetListCustContact(_msCustomer.CustCode);
            }
            this.ListRepeater.DataBind();
            
            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater1.DataSource = null;
            }
            else
            {
                this.ListRepeater1.DataSource = this._customerBL.GetListCustAddressByCustCode(_msCustomer.CustCode);
            }
            this.ListRepeater1.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "'  ,'" + _akhir + "', '" + _tempHidden + "' );");
            this.AllCheckBox1.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox1.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awalAdd + "'  ,'" + _akhirAdd + "', '" + _tempHiddenAdd + "' );");

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
            this.DeleteButton1.Attributes.Add("OnClick", "return AskYouFirst();");

            if (this._nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "" && _nvcExtractor.GetValue(_app) == "Contact")
            {
                this.WarningLabel.Text = this._nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            if (this._nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "" && _nvcExtractor.GetValue(_app) == "Address")
            {
                this.WarningLabel1.Text = this._nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }
        }

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
            Response.Redirect(this._addDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error1 = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempsplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._customerBL.DeleteMultiCustContact(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _tempsplit);

            if (_result == true)
            {
                _error1 = "Delete Success";
            }
            else
            {
                _error1 = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error1 + "&" + _app + "=" + "Contact");
        }

        protected void AddButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._AddAddressPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton1_Click(object sender, ImageClickEventArgs e)
        {
            string _error2 = "";
            string _temp = this.CheckHidden2.Value;
            string[] _tempsplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._customerBL.DeleteMultiCustAddress(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _tempsplit);

            if (_result == true)
            {
                _error2 = "Delete Success";
            }
            else
            {
                _error2 = "Delete Failed";
            }

            this.CheckHidden2.Value = "";
            this.AllCheckBox1.Checked = false;

            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error2 + "&" + _app + "=" + "Address");
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                MsCustContact _msCustContact = (MsCustContact)e.Item.DataItem;

                string _code = _msCustContact.CustCode.ToString();
                string _itemCode = _msCustContact.ItemNo.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value += _itemCode;
                }
                else
                {
                    this.TempHidden.Value += "," + _itemCode;
                }

                Literal _contactNameLiteral = (Literal)e.Item.FindControl("ContactNameLiteral");
                _contactNameLiteral.Text = Convert.ToString(_msCustContact.ContactName);

                Literal _contactTitleLiteral = (Literal)e.Item.FindControl("ContactTitleLiteral");
                _contactTitleLiteral.Text = Convert.ToString(_msCustContact.ContactTitle);

                Literal _address1Literal = (Literal)e.Item.FindControl("Address1Literal");
                _address1Literal.Text = Convert.ToString(_msCustContact.Address1);

                Literal _countryLiteral = (Literal)e.Item.FindControl("CountryLiteral");
                _countryLiteral.Text = Convert.ToString(_msCustContact.CountryName);

                Literal _phoneLiteral = (Literal)e.Item.FindControl("PhoneLiteral");
                _phoneLiteral.Text = HttpUtility.HtmlEncode(_msCustContact.Phone);

                Literal _emailLiteral = (Literal)e.Item.FindControl("EmailLiteral");
                _emailLiteral.Text = HttpUtility.HtmlEncode(_msCustContact.Email);

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _cb = (CheckBox)e.Item.FindControl("ListCheckBox");
                _cb.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + "," + _cb.ClientID + ",'" + _itemCode + "', '" + _awal + "' , '" + _akhir + "', '" + _cbox + "' )");

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = _viewDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + _itemCodeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_itemCode, ApplicationConfig.EncryptionKey));
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                if (this._permView == PermissionLevel.NoAccess)
                {
                    _viewButton.Visible = false;
                }

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton2");
                _editButton.PostBackUrl = _editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + _itemCodeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_itemCode, ApplicationConfig.EncryptionKey));
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
            }
        }

        protected void ListRepeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                MsCustAddress _msCustAddress = (MsCustAddress)e.Item.DataItem;

                string _code = _msCustAddress.CustCode.ToString();
                string _deliveryCode = _msCustAddress.DeliveryCode.ToString();

                if (this.TempHidden2.Value == "")
                {
                    this.TempHidden2.Value += _deliveryCode;
                }
                else
                {
                    this.TempHidden2.Value += "," + _deliveryCode;
                }

                Literal _deliveryCodeLiteral = (Literal)e.Item.FindControl("DeliveryCodeLiteral");
                _deliveryCodeLiteral.Text = _msCustAddress.DeliveryCode;

                Literal _deliveryNameLiteral = (Literal)e.Item.FindControl("DeliveryNameLiteral");
                _deliveryNameLiteral.Text = _msCustAddress.DeliveryName;

                Literal _countryLiteral = (Literal)e.Item.FindControl("Address1Literal");
                _countryLiteral.Text = _msCustAddress.DeliveryAddr1;

                Literal _address2Literal = (Literal)e.Item.FindControl("Address2Literal");
                _address2Literal.Text = _msCustAddress.DeliveryAddr1;

                Literal _country1Literal = (Literal)e.Item.FindControl("Country2Literal");
                _country1Literal.Text = _msCustAddress.CountryName;

                Literal _zipCodeLiteral = (Literal)e.Item.FindControl("ZipCodeLiteral");
                _zipCodeLiteral.Text = _msCustAddress.ZipCode;

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral1");
                _no2 = _page2 * _maxrow2;
                _no2 += 1;
                _no2 = _nomor2 + _no2;
                _noLiteral.Text = _no2.ToString();
                _nomor2 += 1;

                CheckBox _cb = (CheckBox)e.Item.FindControl("ListCheckBox1");
                _cb.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + "," + _cb.ClientID + ",'" + _deliveryCode + "', '" + _awalAdd + "' , '" + _akhirAdd + "', '" + _cboxAdd + "' )");

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton3");
                _editButton.PostBackUrl = this._editAddressPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + _deliveryCodeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_deliveryCode, ApplicationConfig.EncryptionKey));
                _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton.Visible = false;
                }

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterListTemplate1");
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
    }
}