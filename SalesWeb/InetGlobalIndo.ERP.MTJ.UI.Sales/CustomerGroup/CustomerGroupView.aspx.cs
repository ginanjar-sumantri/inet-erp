using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.CustomerGroup
{
    public partial class CustomerGroupView : CustomerGroupBase
    {
        private CustomerBL _customerBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _no = 0;

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
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowData();
                this.ShowDetail();
            }
        }

        protected void ClearLabel()
        {
            this.Label1.Text = "";
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
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
            }
        }

        public void ShowData()
        {
            MsCustGroup _msCustGroup = this._customerBL.GetSingleCustGroup(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CustGroupCodeTextBox.Text = _msCustGroup.CustGroupCode;
            this.CustGroupNameTextBox.Text = _msCustGroup.CustGroupName;
            this.TypeDropDownList.SelectedValue = _msCustGroup.CustGroupType;
            this.FgPKPCheckBox.Checked = CustomerDataMapper.IsChecked(_msCustGroup.FgPKP);
            this.FgPPhCheckBox.Checked = CustomerDataMapper.IsChecked(_msCustGroup.FgPPh);
            this.PPhTextBox.Text = (_msCustGroup.PPH == 0) ? "0" : _msCustGroup.PPH.ToString("#,###.##");
        }

        public void ShowDetail()
        {
            string _groupCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._customerBL.GetListCustGroupAcc(_groupCode);
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.Label1.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MsCustGroupAcc _temp = (MsCustGroupAcc)e.Item.DataItem;

                string _code = _temp.CustGroup.ToString();
                string _currCode = _temp.CurrCode.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _currCode;
                }
                else
                {
                    this.TempHidden.Value += "," + _currCode;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no = _page * _maxrow;
                _no += 1;
                //_no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                //_nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _currCode + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");

                string _urlViewDetail = _viewDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + _currCodeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_currCode, ApplicationConfig.EncryptionKey));
                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                //_viewButton.Attributes.Add("OnClick", "ShowFormAddDetail('" + _urlViewDetail + "','" + _widthPopUp + "', '" + _heightPopUp + "', '" + _resizablePopUp + "', '" + _menuBarPopUp + "'); return false;");
                _viewButton.PostBackUrl = _urlViewDetail;
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                if (this._permView == PermissionLevel.NoAccess)
                {
                    _viewButton.Visible = false;
                }

                string _urlEditDetail = _editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + _currCodeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_currCode, ApplicationConfig.EncryptionKey));
                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                //_editButton.Attributes.Add("OnClick", "ShowFormAddDetail('" + _urlEditDetail + "','" + _widthPopUp + "', '" + _heightPopUp + "', '" + _resizablePopUp + "', '" + _menuBarPopUp + "'); return false;");
                _editButton.PostBackUrl = _urlEditDetail;
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

                Literal _currency = (Literal)e.Item.FindControl("CurrencyLiteral");
                _currency.Text = HttpUtility.HtmlEncode(_temp.CurrCode);

                Literal _accountAR = (Literal)e.Item.FindControl("AccountARLiteral");
                _accountAR.Text = HttpUtility.HtmlEncode(_temp.AccARName);

                Literal _accountDisc = (Literal)e.Item.FindControl("AccountDiscountLiteral");
                _accountDisc.Text = HttpUtility.HtmlEncode(_temp.AccDiscName);

                Literal _accountDP = (Literal)e.Item.FindControl("AccountDPLiteral");
                _accountDP.Text = HttpUtility.HtmlEncode(_temp.AccDPName);

                Literal _accountCredit = (Literal)e.Item.FindControl("AccountCreditLiteral");
                _accountCredit.Text = HttpUtility.HtmlEncode(_temp.AccCreditName);

                Literal _accountPPn = (Literal)e.Item.FindControl("AccountPPnLiteral");
                _accountPPn.Text = HttpUtility.HtmlEncode(_temp.AccPPnName);

                Literal _accountOther = (Literal)e.Item.FindControl("AccountOtherLiteral");
                _accountOther.Text = HttpUtility.HtmlEncode(_temp.AccOtherName);
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_addDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _groupCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _error = "";

            this.ClearLabel();

            bool _result = this._customerBL.DeleteMultiCustGroupAcc(_tempSplit, _groupCode);

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

            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }
    }
}