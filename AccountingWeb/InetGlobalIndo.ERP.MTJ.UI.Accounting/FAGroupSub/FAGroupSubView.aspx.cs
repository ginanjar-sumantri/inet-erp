using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FASubGroup
{
    public partial class FAGroupSubView : FASubGroupBase
    {
        private FixedAssetsBL _fixed = new FixedAssetsBL();
        private PermissionBL _permBL = new PermissionBL();
        
        private int _no = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _widthPopUp = "600";
        private string _heightPopUp = "400";
        private string _resizablePopUp = "true";
        private string _menuBarPopUp = "false";

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

                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.SetAttribute();
                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowData();
                this.ShowDetail();
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

        protected void SetAttribute()
        {
            CompanyConfiguration _compConfig = new CompanyConfig().GetSingle(CompanyConfigure.FACodeAutoNumber);

            if (_compConfig.SetValue == CompanyConfigureDataMapper.GetFACodeAutoNumber(FACodeAutoNumber.True))
            {
                this.EnableCodeCounter.Visible = true;
                this.EnableLastCounter.Visible = true;
            }
            else
            {
                this.EnableCodeCounter.Visible = false;
                this.EnableLastCounter.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowData()
        {
            MsFAGroupSub _msFAGroupSub = this._fixed.GetSingleFAGroupSub(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            FixedAssetsBL _fixAsset = new FixedAssetsBL();

            this.FAGroupSubCodeTextBox.Text = _msFAGroupSub.FASubGrpCode;
            this.FAGroupSubNameTextBox.Text = _msFAGroupSub.FASubGrpName;
            this.FAGroupTextBox.Text = _fixAsset.GetFAGroupNameByCode(_msFAGroupSub.FAGroup);
            this.MovingCheckBox.Checked = FixedAssetsDataMapper.IsChecked(_msFAGroupSub.FgMoving);
            this.ProcessCheckbox.Checked = FixedAssetsDataMapper.IsChecked(_msFAGroupSub.FgProcess);
            this.CodeCounterTextBox.Text = _msFAGroupSub.CodeCounter;
            this.LastCounterTextBox.Text = ((int)_msFAGroupSub.LastCounterNo).ToString().PadLeft(Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.FACodeDigitNumber).SetValue), '0');
        }

        public void ShowDetail()
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.ListRepeater.DataSource = this._fixed.GetListFAGroupSubAcc(this.FAGroupSubCodeTextBox.Text);
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._fixed.DeleteMultiFAGroupSubAcc(_tempSplit, this.FAGroupSubCodeTextBox.Text);

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

            Response.Redirect(this._viewPage + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MsFAGroupSubAcc _temp = (MsFAGroupSubAcc)e.Item.DataItem;

                string _code = _temp.FASubGroup.ToString();
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

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = _viewDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + _currCodeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_currCode, ApplicationConfig.EncryptionKey));
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                _editButton.PostBackUrl = _editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + _currCodeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_currCode, ApplicationConfig.EncryptionKey));
                _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

                Literal _currency = (Literal)e.Item.FindControl("CurrencyLiteral");
                _currency.Text = HttpUtility.HtmlEncode(_temp.CurrCode);

                Literal _accountAR = (Literal)e.Item.FindControl("AccAssetLiteral");
                _accountAR.Text = HttpUtility.HtmlEncode(_temp.AccFA);

                Literal _accountDisc = (Literal)e.Item.FindControl("AccDepriciationLiteral");
                _accountDisc.Text = HttpUtility.HtmlEncode(_temp.AccDepr);

                Literal _accountDP = (Literal)e.Item.FindControl("AccAkumDepriciationLiteral");
                _accountDP.Text = HttpUtility.HtmlEncode(_temp.AccAkumDepr);

                Literal _accountCredit = (Literal)e.Item.FindControl("AccSalesLiteral");
                _accountCredit.Text = HttpUtility.HtmlEncode(_temp.AccSales);

                Literal _accountPPn = (Literal)e.Item.FindControl("AccTenancyLiteral");
                _accountPPn.Text = HttpUtility.HtmlEncode(_temp.AccTenancy);

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
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_addDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}