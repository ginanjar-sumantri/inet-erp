using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.Supplier
{
    public partial class SupplierView : SupplierBase
    {
        private SupplierBL _supplierBL = new SupplierBL();
        private CityBL _cityBL = new CityBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private TermBL _termBL = new TermBL();
        private BankBL _bankBL = new BankBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _no = 0;
        private int _nomor = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00_DefaultBodyContentPlaceHolder_TempHidden";

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

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.AddButton.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowData()
        {
            MsSupplier _msSupplier = this._supplierBL.GetSingleSupp(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.SuppCodeTextBox.Text = _msSupplier.SuppCode;
            this.SuppNameTextBox.Text = _msSupplier.SuppName;
            //this.SupplierTypeDropDownList.SelectedValue = _msSupplier.SuppType;
            this.SupplierTypeTextBox.Text = _supplierBL.GetSuppTypeNameByCode(_msSupplier.SuppType);
            //this.SupplierGroupDropDownList.SelectedValue = _msSupplier.SuppGroup;
            this.SupplierGroupTextBox.Text = _supplierBL.GetSuppGroupNameByCode(_msSupplier.SuppGroup);
            this.AddressTextBox.Text = _msSupplier.Address1;
            this.AddressTextBox2.Text = _msSupplier.Address2;
            //this.CityDropDownList.SelectedValue = _msSupplier.City;
            this.CityTextBox.Text = _cityBL.GetCityNameByCode(_msSupplier.City);
            this.ZipCodeTextBox.Text = _msSupplier.PostCode;
            this.TelephoneTextBox.Text = _msSupplier.Telephone;
            this.FaxTextBox.Text = _msSupplier.Fax;
            this.EmailTextBox.Text = _msSupplier.Email;
            //this.CurrencyDropDownList.SelectedValue = _msSupplier.CurrCode;
            this.CurrencyTextBox.Text = _msSupplier.CurrCode;
            //this.TermDropDownList.SelectedValue = _msSupplier.Term;
            this.TermTextBox.Text = _termBL.GetTermNameByCode(_msSupplier.Term);
            //this.BankDropDownList.SelectedValue = _msSupplier.Bank;
            this.BankTextBox.Text = _bankBL.GetBankNameByCode(_msSupplier.Bank);
            this.NPWPTextBox.Text = _msSupplier.NPWP;
            this.NPPKPTextBox.Text = _msSupplier.NPPKP;
            this.ContactPersonTextBox.Text = _msSupplier.ContactPerson;
            this.ContactTitleTextBox.Text = _msSupplier.ContactTitle;
            this.ContactHPTextBox.Text = _msSupplier.ContactHP;
            this.RemarkTextBox.Text = _msSupplier.Remark;
            this.FgPPNCheckBox.Checked = SupplierDataMapper.IsPPN(_msSupplier.FgPPN);
            this.FgActiveCheckBox.Checked = SupplierDataMapper.IsActive(_msSupplier.FgActive);

            this.TempHidden.Value = "";
            this.ListRepeater.DataSource = this._supplierBL.GetListSuppContact(this.SuppCodeTextBox.Text);
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "'  ,'" + _akhir + "', '" + _tempHidden + "' );");
            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "" && _nvcExtractor.GetValue(_app) == "Contact")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                MsSuppContact _msSuppContact = (MsSuppContact)e.Item.DataItem;

                string _code = _msSuppContact.SuppCode.ToString();
                string _itemCode = _msSuppContact.ItemNo.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value += _itemCode;
                }
                else
                {
                    this.TempHidden.Value += "," + _itemCode;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                Literal _itemNoLiteral = (Literal)e.Item.FindControl("ContactNameLiteral");
                _itemNoLiteral.Text = Convert.ToString(_msSuppContact.ContactName);

                Literal _phoneLiteral = (Literal)e.Item.FindControl("PhoneLiteral");
                _phoneLiteral.Text = _msSuppContact.Telephone;

                Literal _emailLiteral = (Literal)e.Item.FindControl("EmailLiteral");
                _emailLiteral.Text = _msSuppContact.Email;

                CheckBox _cb = (CheckBox)e.Item.FindControl("ListCheckBox");
                _cb.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + "," + _cb.ClientID + ",'" + _itemCode + "', '" + _awal + "' , '" + _akhir + "', '" + _cbox + "' )");

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = _viewDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + _itemCodeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_itemCode, ApplicationConfig.EncryptionKey));
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton2");
                _editButton.PostBackUrl = _editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + _itemCodeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_itemCode, ApplicationConfig.EncryptionKey));
                _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

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

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempsplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._supplierBL.DeleteMultiSuppContact(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _tempsplit);

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

            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error + "&" + _app + "=" + "Contact");
        }
    }
}