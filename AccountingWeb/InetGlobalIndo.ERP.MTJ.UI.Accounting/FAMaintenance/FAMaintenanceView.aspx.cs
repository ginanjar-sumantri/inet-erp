using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAMaintanance
{
    public partial class FAMaintenanceView : FAMaintananceBase
    {
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _page;
        private int _maxrow = 5;
        private int _no = 0;
        private int _nomor = 0;

        private string awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string akhir = "_ListCheckBox";
        private string cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00_DefaultBodyContentPlaceHolder_TempHidden";

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
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowDataDetail();
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
            MsFAMaintenance _msFaMaintenance = this._fixedAssetBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.FixAssetsCodeTextBox.Text = _msFaMaintenance.FAMaintenanceCode;
            this.FixAssetsNameTextBox.Text = _msFaMaintenance.FAMaintenanceName;
            this.FgAddValueCheckBox.Checked = FixedAssetsDataMapper.IsFGAddValue(_msFaMaintenance.FgAddValue);

            if (this.FgAddValueCheckBox.Checked == false)
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
            }
        }

        private void ShowDataDetail()
        {
            this.TempHidden.Value = "";
            //cari data url jika kosong diisi dengan nol
            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = Convert.ToInt32((_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey) == "") ? "0" : Rijndael.Decrypt(_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey), ApplicationConfig.EncryptionKey));

            this.ListRepeater.DataSource = this._fixedAssetBL.GetListAccByCode(_page, _maxrow, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + awal + "'  ,'" + akhir + "', '" + _tempHidden + "' );");

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _error = "";
            string _page = "0";

            bool _result = this._fixedAssetBL.DeleteMultiAcc(_tempSplit, this.FixAssetsCodeTextBox.Text);

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
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                MsFAMaintenanceAcc _faMaintenanceAcc = (MsFAMaintenanceAcc)e.Item.DataItem;

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value += _faMaintenanceAcc.CurrCode;
                }
                else
                {
                    this.TempHidden.Value += "," + _faMaintenanceAcc.CurrCode;
                }

                Literal _Currency = (Literal)e.Item.FindControl("CurrencyLiteral");
                _Currency.Text = HttpUtility.HtmlEncode(_faMaintenanceAcc.CurrCode);

                Literal _Account = (Literal)e.Item.FindControl("AccountLiteral");
                _Account.Text = HttpUtility.HtmlEncode(_faMaintenanceAcc.AccountName);

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _cb = (CheckBox)e.Item.FindControl("ListCheckBox");
                //_cb.Attributes.Add("OnClick", "CheckBox_Click(" + this.FixAssetsHidden.ClientID + "," + _cb.ClientID + ",'" + _faMaintenanceAcc.CurrCode + "', '" + awal + "' , '" + akhir + "', '" + cbox + "','" + _tempHidden + "' )");
                _cb.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + "," + _cb.ClientID + ",'" + _faMaintenanceAcc.CurrCode + "', '" + awal + "' , '" + akhir + "', '" + cbox + "' );");

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
            Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.FixAssetsCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }
    }
}