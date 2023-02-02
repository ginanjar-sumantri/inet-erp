using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FASubGroup
{
    public partial class FAGroupSubAdd : FASubGroupBase
    {
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
        private PermissionBL _permBL = new PermissionBL();

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

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ShowDropdownlist();

                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            CompanyConfiguration _compConfig = new CompanyConfig().GetSingle(CompanyConfigure.FACodeAutoNumber);

            if (_compConfig.SetValue == CompanyConfigureDataMapper.GetFACodeAutoNumber(FACodeAutoNumber.True))
            {
                this.EnableCodeCounter.Visible = true;
            }
            else
            {
                this.EnableCodeCounter.Visible = false;
            }
        }

        protected void ClearData()
        {
            this.FAGroupDDL.SelectedValue = "null";
            this.FAGroupSubCodeTextBox.Text = "";
            this.FAGroupSubNameTextBox.Text = "";
            this.MovingCheckBox.Checked = false;
            this.ProcessCheckbox.Checked = false;
            this.CodeCounterTextBox.Text = "";
        }

        protected void NextButton_Click(object sender, EventArgs e)
        {
            MsFAGroupSub _msFAGroupSub = new MsFAGroupSub();

            _msFAGroupSub.FASubGrpCode = this.FAGroupSubCodeTextBox.Text;
            _msFAGroupSub.FASubGrpName = this.FAGroupSubNameTextBox.Text;
            _msFAGroupSub.FAGroup = this.FAGroupDDL.SelectedValue;
            _msFAGroupSub.FgMoving = FixedAssetsDataMapper.IsChecked(this.MovingCheckBox.Checked);
            _msFAGroupSub.FgProcess = FixedAssetsDataMapper.IsChecked(this.ProcessCheckbox.Checked);
            _msFAGroupSub.CodeCounter = (this.CodeCounterTextBox.Text.Trim() == "") ? "" : this.CodeCounterTextBox.Text;
            _msFAGroupSub.LastCounterNo = 0;

            _msFAGroupSub.UserId = HttpContext.Current.User.Identity.Name;
            _msFAGroupSub.UserDate = DateTime.Now;

            bool _result = this._fixedAssetBL.AddFAGroupSub(_msFAGroupSub);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.FAGroupSubCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        private void ShowDropdownlist()
        {
            var hasil = this._fixedAssetBL.GetListFAGroup();

            this.FAGroupDDL.DataSource = hasil;
            this.FAGroupDDL.DataValueField = "FAGroupCode";
            this.FAGroupDDL.DataTextField = "FAGroupName";
            this.FAGroupDDL.DataBind();
            this.FAGroupDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ResetButton_Click1(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}
