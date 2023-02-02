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
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.MenuServiceType
{
    public partial class MenuServiceTypeView : MenuServiceTypeBase
    {
        private MenuServiceTypeBL _menuServiceTypeBL = new MenuServiceTypeBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _no = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        //private string _imgGetApproval = "get_approval.jpg";
        //private string _imgApprove = "approve.jpg";
        //private string _imgPosting = "posting.jpg";
        //private string _imgUnPosting = "unposting.jpg";

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

                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Delete.jpg";

                //this.SetButtonPermission();

                this.MenuServiceTypeCodeTextBox.Attributes.Add("ReadOnly", "True");
                this.MenuServiceTypeNameTextBox.Attributes.Add("ReadOnly", "True");
                this.WarehouseTextBox.Attributes.Add("ReadOnly", "True");
                this.WarehouseLocationTextBox.Attributes.Add("ReadOnly", "True");
                this.WarehouseDepositinTextBox.Attributes.Add("ReadOnly", "True");
                this.DepositLocationTextBox.Attributes.Add("ReadOnly", "True");

                this.ShowData();
            }
        }

        private void ClearLabel()
        {
            this.Label.Text = "";
        }

        public void ShowData()
        {
            POSMsMenuServiceType _posMsMenuServiceType = this._menuServiceTypeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.MenuServiceTypeCodeTextBox.Text = _posMsMenuServiceType.MenuServiceTypeCode;
            this.MenuServiceTypeNameTextBox.Text = _posMsMenuServiceType.MenuServiceTypeName;
            this.WarehouseTextBox.Text = _menuServiceTypeBL.GetWarehouseNamebyCode(_posMsMenuServiceType.WrhsCode);
            this.WarehouseLocationTextBox.Text = this._menuServiceTypeBL.GetWLocationNameByWLocationCode(_posMsMenuServiceType.LocationCode);
            this.WarehouseDepositinTextBox.Text = _menuServiceTypeBL.GetWarehouseNamebyCode(_posMsMenuServiceType.WrhsDepositInCode);
            this.DepositLocationTextBox.Text = this._menuServiceTypeBL.GetWLocationNameByWLocationCode(_posMsMenuServiceType.LocationDepositInCode);

            ShowDataDetail1();

        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        #region Detail1

        public void ShowDataDetail1()
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._menuServiceTypeBL.GetListPOSMsMenuServiceTypeDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");
            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSMsMenuServiceTypeDt _temp = (POSMsMenuServiceTypeDt)e.Item.DataItem;

                string _code = _temp.ProductSubGroup;

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _noLiteral.Text = _no.ToString();

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");

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

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                _editButton.PostBackUrl = this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey2 + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.MenuServiceTypeCodeTextBox.Text, ApplicationConfig.EncryptionKey));
                _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton.Visible = false;
                }

                Literal _menuServiceTypeCode = (Literal)e.Item.FindControl("MenuServiceTypeCodeLiteral");
                _menuServiceTypeCode.Text = HttpUtility.HtmlEncode(_temp.MenuServiceTypeCode);

                Literal _productSubGroup = (Literal)e.Item.FindControl("ProductSubGroupLiteral");
                _productSubGroup.Text = HttpUtility.HtmlEncode(this._menuServiceTypeBL.GetNameSubProductbyCode(_temp.ProductSubGroup));             
            }
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Label.Text = "";
            this.Label1.Text = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._menuServiceTypeBL.DeleteMultiPOSMsMenuServiceTypeDt(_tempSplit, this.MenuServiceTypeCodeTextBox.Text);

            if (_result == true)
            {
                this.Label.Text = "Delete Success";
            }
            else
            {
                this.Label.Text = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            this.ShowData();
        }

        #endregion
    }
}