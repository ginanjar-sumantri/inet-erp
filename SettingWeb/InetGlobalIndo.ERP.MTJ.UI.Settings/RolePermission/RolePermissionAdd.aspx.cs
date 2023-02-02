using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.RolePermission
{
    public partial class RolePermissionAdd : RolePermissionBase
    {
        private PermissionBL _permissionBL = new PermissionBL();
        private RoleBL _roleBL = new RoleBL();
        private MenuBL _menuBL = new MenuBL();
        private Guid _companyId = new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name);

        private int _no = 0;

        //private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.SaveAndEditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_edit.jpg";

                this.ShowRoleDropdownlist();
                this.ShowModuleDropdownlist();

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowRoleDropdownlist()
        {
            this.RoleDropDownList.Items.Clear();
            this.RoleDropDownList.DataSource = this._roleBL.GetListRoleForDDLPermission(this._companyId);
            this.RoleDropDownList.DataValueField = "RoleId";
            this.RoleDropDownList.DataTextField = "RoleName";
            this.RoleDropDownList.DataBind();
            this.RoleDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowModuleDropdownlist()
        {
            this.ModuleDropDownList.Items.Clear();
            if (this.RoleDropDownList.SelectedValue == "null")
            {
                this.ModuleDropDownList.DataSource = null;
            }
            else
            {
                this.ModuleDropDownList.DataSource = this._menuBL.GetListModuleForDDL(_companyId, new Guid(this.RoleDropDownList.SelectedValue));
            }
            this.ModuleDropDownList.DataValueField = "ModuleId";
            this.ModuleDropDownList.DataTextField = "Text";
            this.ModuleDropDownList.DataBind();
            this.ModuleDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowData()
        {
            this.TempHidden.Value = "";
            this.CheckHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.ListRepeater.DataSource = this._permissionBL.GetList(this.ModuleDropDownList.SelectedValue);
            this.ListRepeater.DataBind();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            List<master_PermissionTemplate> _listTemplate = this._permissionBL.GetList(this.ModuleDropDownList.SelectedValue);
            foreach (var _item in _listTemplate)
            {
                if (this.CheckHidden.Value == "")
                {
                    this.CheckHidden.Value = _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                }
                else
                {
                    this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                }

                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.TaxPreview) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Close) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Generate) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Revisi) + "-" + PermissionDataMapper.GetStatusLevel(PermissionLevel.NoAccess).ToString();
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                master_PermissionTemplate _temp = (master_PermissionTemplate)e.Item.DataItem;
                string _code = _temp.MenuID.ToString();

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

                Literal _moduleID = (Literal)e.Item.FindControl("ModuleLiteral");
                _moduleID.Text = HttpUtility.HtmlEncode(AppModule.GetText(_temp.ModuleID));

                byte _indent = Convert.ToByte((_temp.Indent == null) ? 0 : _temp.Indent);
                string _td = "";
                for (int i = 0; i < _indent; i++)
                {
                    _td += "<td>&nbsp;&nbsp;&nbsp;</td>";
                }
                Literal _menuName = (Literal)e.Item.FindControl("MenuLiteral");
                _menuName.Text = _td + "<td>" + HttpUtility.HtmlEncode(_temp.MenuName) + "</td>";

                byte _permission = 0;

                CheckBox _accessImage = (CheckBox)e.Item.FindControl("AccessPermission");
                _accessImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _accessImage.Visible = _temp.Access;

                CheckBox _addImage = (CheckBox)e.Item.FindControl("AddPermission");
                _addImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _addImage.Visible = _temp.Add;

                CheckBox _editImage = (CheckBox)e.Item.FindControl("EditPermission");
                _editImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _editImage.Visible = _temp.Edit;

                CheckBox _deleteImage = (CheckBox)e.Item.FindControl("DeletePermission");
                _deleteImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _deleteImage.Visible = _temp.Delete;

                CheckBox _viewImage = (CheckBox)e.Item.FindControl("ViewPermission");
                _viewImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _viewImage.Visible = _temp.View;

                CheckBox _getApprovalImage = (CheckBox)e.Item.FindControl("GetApprovalPermission");
                _getApprovalImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _getApprovalImage.Visible = _temp.GetApproval;

                CheckBox _approveImage = (CheckBox)e.Item.FindControl("ApprovePermission");
                _approveImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _approveImage.Visible = _temp.Approve;

                CheckBox _postingImage = (CheckBox)e.Item.FindControl("PostingPermission");
                _postingImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _postingImage.Visible = _temp.Posting;

                CheckBox _unPostingImage = (CheckBox)e.Item.FindControl("UnpostingPermission");
                _unPostingImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _unPostingImage.Visible = _temp.Unposting;

                CheckBox _printPreviewImage = (CheckBox)e.Item.FindControl("PrintPreviewPermission");
                _printPreviewImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _printPreviewImage.Visible = _temp.PrintPreview;

                CheckBox _taxPreviewImage = (CheckBox)e.Item.FindControl("TaxPreviewPermission");
                _taxPreviewImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _taxPreviewImage.Visible = _temp.TaxPreview;

                CheckBox _closeImage = (CheckBox)e.Item.FindControl("ClosePermission");
                _closeImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _closeImage.Visible = _temp.Close;

                CheckBox _generateImage = (CheckBox)e.Item.FindControl("GeneratePermission");
                _generateImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _generateImage.Visible = _temp.Generate;

                CheckBox _revisiImage = (CheckBox)e.Item.FindControl("RevisiPermission");
                _revisiImage.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessImage.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _revisiImage.Visible = _temp.Revisi;

                //Image _horizontalImage = (Image)e.Item.FindControl("HorizontalPermission");
                //_horizontalImage.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/permission_level/permission" + _permission.ToString() + ".gif";
                //_horizontalImage.Attributes.Add("OnClick", "HorizontalChangePermission(" + this.CheckHidden.ClientID + ", '" + ApplicationConfig.HomeWebAppURL + "', " + _code + ", " +
                //    _horizontalImage.ClientID + ", " + _accessImage.ClientID + ", " + _addImage.ClientID + //", " + _editImage.ClientID + ", " + _deleteImage.ClientID + ", " + _viewImage.ClientID + ", " + _getApprovalImage.ClientID + ", " + _approveImage.ClientID + ", " +
                //    //_postingImage.ClientID + ", " + _unPostingImage.ClientID + ", " + _printPreviewImage.ClientID + ", " + _taxPreviewImage.ClientID + ", " + _closeImage.ClientID + ", " + _generateImage.ClientID + ", " + _revisiImage.ClientID + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.Access) + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.Add) + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.Edit) + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.Delete) + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.View) + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.GetApproval) + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.Approve) + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.Posting) + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.Unposting) + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.PrintPreview) + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.TaxPreview) + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.Close) + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.Generate) + 
                //    //", " + PermissionDataMapper.GetText(Common.Enum.Action.Revisi) + 
                //    ")");
            }
        }

        protected void ModuleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowData();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _result = _permissionBL.AddRolePermission(new Guid(this.RoleDropDownList.SelectedValue), this.CheckHidden.Value);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void SaveAndEditButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _result = _permissionBL.AddRolePermission(new Guid(this.RoleDropDownList.SelectedValue), this.CheckHidden.Value);

            if (_result == true)
            {
                Response.Redirect(this._editPage + "?" + this._userKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.RoleDropDownList.SelectedValue, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void RoleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowModuleDropdownlist();
            this.ClearLabel();
            this.ShowData();
        }
    }
}