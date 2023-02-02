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
    public partial class RolePermissionEdit : RolePermissionBase
    {
        private PermissionBL _permissionBL = new PermissionBL();
        private RoleBL _roleBL = new RoleBL();
        private MenuBL _menuBL = new MenuBL();

        private int _no = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.SaveAndEditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_edit.jpg";
                this.GrantAllPermissionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/grantAllButton.jpg";
                this.DenyAllPermissionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/denyAllButton.jpg";

                this.ShowModuleDropdownlist();

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowModuleDropdownlist()
        {
            Guid _companyId = new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name);
            Guid _roleId = new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._userKey), ApplicationConfig.EncryptionKey));
            this.ModuleDropDownList.Items.Clear();
            this.ModuleDropDownList.DataSource = this._menuBL.GetListModuleForDDL(_companyId, _roleId);
            this.ModuleDropDownList.DataValueField = "ModuleId";
            this.ModuleDropDownList.DataTextField = "Text";
            this.ModuleDropDownList.DataBind();
            this.ModuleDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowData()
        {
            this.RoleNameTextBox.Text = _roleBL.GetRoleNameByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._userKey), ApplicationConfig.EncryptionKey));

            this.TempHidden.Value = "";
            this.CheckHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.ListRepeater.DataSource = this._permissionBL.GetList(this.ModuleDropDownList.SelectedValue);
            this.ListRepeater.DataBind();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.SetHidden();
        }

        protected void SetHidden()
        {
            List<master_PermissionTemplate> _listTemplate = this._permissionBL.GetList(this.ModuleDropDownList.SelectedValue);
            foreach (var _item in _listTemplate)
            {
                master_RolePermission _rolePermission = this._permissionBL.GetSingleRolePermission(_item.MenuID, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._userKey), ApplicationConfig.EncryptionKey)));
                if (this.CheckHidden.Value == "")
                {
                    this.CheckHidden.Value = _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.Access);
                }
                else
                {
                    this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.Access);
                }

                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.Add);
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.Edit);
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.Delete);
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.View);
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.GetApproval);
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.Approve);
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.Posting);
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.Unposting);
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.PrintPreview);
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.TaxPreview) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.TaxPreview);
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Close) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.Close);
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Generate) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.Generate);
                this.CheckHidden.Value += "," + _item.MenuID + "-" + PermissionDataMapper.GetText(InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Revisi) + "-" + ((_rolePermission == null) ? 0 : _rolePermission.Revisi);
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                master_PermissionTemplate _temp = (master_PermissionTemplate)e.Item.DataItem;
                master_RolePermission _rolePermission = _permissionBL.GetSingleRolePermission(_temp.MenuID, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._userKey), ApplicationConfig.EncryptionKey)));

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

                byte _indent = Convert.ToByte((_temp.Indent == null) ? 0 : _temp.Indent);
                string _td = "";
                for (int i = 0; i < _indent; i++)
                {
                    _td += "<td>&nbsp;&nbsp;&nbsp;</td>";
                }
                Literal _menuName = (Literal)e.Item.FindControl("MenuLiteral");
                _menuName.Text = _td + "<td>" + HttpUtility.HtmlEncode(_temp.MenuName) + "</td>";

                CheckBox _accessCheckBox = (CheckBox)e.Item.FindControl("AccessPermission");
                //_accessCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _accessCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _accessCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access) + "')");
                _accessCheckBox.Visible = _temp.Access;
                _accessCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.Access == 4) ? true : false;

                CheckBox _addCheckBox = (CheckBox)e.Item.FindControl("AddPermission");
                //_addCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _addCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Add) + "')");
                _addCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _addCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Add) + "')");
                _addCheckBox.Visible = _temp.Add;
                _addCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.Add == 4) ? true : false;

                CheckBox _editCheckBox = (CheckBox)e.Item.FindControl("EditPermission");
                _editCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _editCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Edit) + "')");
                _editCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _editCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Edit) + "')");
                _editCheckBox.Visible = _temp.Edit;
                _editCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.Edit == 4) ? true : false;

                CheckBox _deleteCheckBox = (CheckBox)e.Item.FindControl("DeletePermission");
                _deleteCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _deleteCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Delete) + "')");
                _deleteCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _deleteCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Delete) + "')");
                _deleteCheckBox.Visible = _temp.Delete;
                _deleteCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.Delete == 4) ? true : false;

                CheckBox _viewCheckBox = (CheckBox)e.Item.FindControl("ViewPermission");
                _viewCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _viewCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.View) + "')");
                _viewCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _viewCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.View) + "')");
                _viewCheckBox.Visible = _temp.View;
                _viewCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.View == 4) ? true : false;

                CheckBox _getApprovalCheckBox = (CheckBox)e.Item.FindControl("GetApprovalPermission");
                _getApprovalCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _getApprovalCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.GetApproval) + "')");
                _getApprovalCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _getApprovalCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.GetApproval) + "')");
                _getApprovalCheckBox.Visible = _temp.GetApproval;
                _getApprovalCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.GetApproval == 4) ? true : false;

                CheckBox _approveCheckBox = (CheckBox)e.Item.FindControl("ApprovePermission");
                _approveCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _approveCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Approve) + "')");
                _approveCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _approveCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Approve) + "')");
                _approveCheckBox.Visible = _temp.Approve;
                _approveCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.Approve == 4) ? true : false;

                CheckBox _postingCheckBox = (CheckBox)e.Item.FindControl("PostingPermission");
                _postingCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _postingCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Posting) + "')");
                _postingCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _postingCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Posting) + "')");
                _postingCheckBox.Visible = _temp.Posting;
                _postingCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.Posting == 4) ? true : false;

                CheckBox _unPostingCheckBox = (CheckBox)e.Item.FindControl("UnpostingPermission");
                _unPostingCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _unPostingCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Unposting) + "')");
                _unPostingCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _unPostingCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Unposting) + "')");
                _unPostingCheckBox.Visible = _temp.Unposting;
                _unPostingCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.Unposting == 4) ? true : false;

                CheckBox _printPreviewCheckBox = (CheckBox)e.Item.FindControl("PrintPreviewPermission");
                _printPreviewCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _printPreviewCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.PrintPreview) + "')");
                _printPreviewCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _printPreviewCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.PrintPreview) + "')");
                _printPreviewCheckBox.Visible = _temp.PrintPreview;
                _printPreviewCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.PrintPreview == 4) ? true : false;

                CheckBox _taxPreviewCheckBox = (CheckBox)e.Item.FindControl("TaxPreviewPermission");
                _taxPreviewCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _taxPreviewCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.TaxPreview) + "')");
                _taxPreviewCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _taxPreviewCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.TaxPreview) + "')");
                _taxPreviewCheckBox.Visible = _temp.TaxPreview;
                _taxPreviewCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.TaxPreview == 4) ? true : false;

                CheckBox _closeCheckBox = (CheckBox)e.Item.FindControl("ClosePermission");
                _closeCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _closeCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Close) + "')");
                _closeCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _closeCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Close) + "')");
                _closeCheckBox.Visible = _temp.Close;
                _closeCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.Close == 4) ? true : false;

                CheckBox _generateCheckBox = (CheckBox)e.Item.FindControl("GeneratePermission");
                _generateCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _generateCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Generate) + "')");
                _generateCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _generateCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Generate) + "')");
                _generateCheckBox.Visible = _temp.Generate;
                _generateCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.Generate == 4) ? true : false;

                CheckBox _revisiCheckBox = (CheckBox)e.Item.FindControl("RevisiPermission");
                _revisiCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _revisiCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Revisi) + "')");
                _revisiCheckBox.Attributes.Add("OnClick", "CheckBoxChangePermission(" + this.CheckHidden.ClientID + ", " + _revisiCheckBox.ClientID + ", '" + _code + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Revisi) + "')");
                _revisiCheckBox.Visible = _temp.Revisi;
                _revisiCheckBox.Checked = (_rolePermission == null) ? false : (_rolePermission.Revisi == 4) ? true : false;
            }
        }

        protected void ModuleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _result = _permissionBL.EditRolePermission(_roleBL.GetRoleIDByName(this.RoleNameTextBox.Text), this.CheckHidden.Value, this.ModuleDropDownList.SelectedValue);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void SaveAndEditButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _result = _permissionBL.EditRolePermission(this._roleBL.GetRoleIDByName(this.RoleNameTextBox.Text), this.CheckHidden.Value, this.ModuleDropDownList.SelectedValue);

            if (_result == true)
            {
                this.ClearLabel();
                this.ShowData();

                this.WarningLabel.Text = "Your Succesfully Edit Data";
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void GrantAllPermissionButton_Click(object sender, ImageClickEventArgs e)
        {
            String _result = this._permissionBL.GrantDenyAllPermissionByRoleAndModule(this._roleBL.GetRoleIDByName(this.RoleNameTextBox.Text).ToString(), this.ModuleDropDownList.SelectedValue, 4);

            if (_result == "")
            {
                this.ClearLabel();
                this.ShowData();

                this.WarningLabel.Text = "Your Succesfully Edit Permission";
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Permission";
            }
        }
        protected void DenyAllPermissionButton_Click(object sender, ImageClickEventArgs e)
        {
            String _result = this._permissionBL.GrantDenyAllPermissionByRoleAndModule(this._roleBL.GetRoleIDByName(this.RoleNameTextBox.Text).ToString(), this.ModuleDropDownList.SelectedValue, 0);

            if (_result == "")
            {
                this.ClearLabel();
                this.ShowData();

                this.WarningLabel.Text = "Your Succesfully Edit Permission";
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Permission";
            }
        }
    }
}