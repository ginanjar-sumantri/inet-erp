using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FASubGroup
{
    public partial class FAGroupSubEdit : FASubGroupBase
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.SetAttribute();

                this.ClearLabel();
                this.ShowData();
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
            MsFAGroupSub _msFAGroupSub = this._fixedAssetBL.GetSingleFAGroupSub(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.FAGroupSubCodeTextBox.Text = _msFAGroupSub.FASubGrpCode;
            this.FAGroupSubNameTextBox.Text = _msFAGroupSub.FASubGrpName;
            this.ShowDropdownlist();
            this.FAGroupDDL.SelectedValue = _msFAGroupSub.FAGroup;
            this.MovingCheckBox.Checked = FixedAssetsDataMapper.IsChecked(_msFAGroupSub.FgMoving);
            this.ProcessCheckbox.Checked = FixedAssetsDataMapper.IsChecked(_msFAGroupSub.FgProcess);
            this.CodeCounterTextBox.Text = _msFAGroupSub.CodeCounter;
            if ((int)_msFAGroupSub.LastCounterNo != 0)
            {
                this.CodeCounterTextBox.Attributes.Add("ReadOnly", "True");
                this.CodeCounterTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
            }
            else
            {
                this.CodeCounterTextBox.Attributes.Remove("ReadOnly");
                this.CodeCounterTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
            }
            this.LastCounterTextBox.Text = ((int)_msFAGroupSub.LastCounterNo).ToString().PadLeft(Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.FACodeDigitNumber).SetValue), '0');
            this.LastCounterTextBox.Attributes.Add("ReadOnly", "True");
            this.LastCounterTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            MsFAGroupSub _msFAGroupSub = this._fixedAssetBL.GetSingleFAGroupSub(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msFAGroupSub.FASubGrpCode = this.FAGroupSubCodeTextBox.Text;
            _msFAGroupSub.FASubGrpName = this.FAGroupSubNameTextBox.Text;
            _msFAGroupSub.FAGroup = this.FAGroupDDL.SelectedValue;
            _msFAGroupSub.FgMoving = FixedAssetsDataMapper.IsChecked(this.MovingCheckBox.Checked);
            _msFAGroupSub.FgProcess = FixedAssetsDataMapper.IsChecked(this.ProcessCheckbox.Checked);
            _msFAGroupSub.CodeCounter = (this.CodeCounterTextBox.Text.Trim() == "") ? "" : this.CodeCounterTextBox.Text;

            _msFAGroupSub.UserId = HttpContext.Current.User.Identity.Name;
            _msFAGroupSub.UserDate = DateTime.Now;

            bool _result = this._fixedAssetBL.EditFAGroupSub(_msFAGroupSub);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
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

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            MsFAGroupSub _msFAGroupSub = this._fixedAssetBL.GetSingleFAGroupSub(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msFAGroupSub.FASubGrpCode = this.FAGroupSubCodeTextBox.Text;
            _msFAGroupSub.FASubGrpName = this.FAGroupSubNameTextBox.Text;
            _msFAGroupSub.FAGroup = this.FAGroupDDL.SelectedValue;
            _msFAGroupSub.FgMoving = FixedAssetsDataMapper.IsChecked(this.MovingCheckBox.Checked);
            _msFAGroupSub.FgProcess = FixedAssetsDataMapper.IsChecked(this.ProcessCheckbox.Checked);
            _msFAGroupSub.UserId = HttpContext.Current.User.Identity.Name;
            _msFAGroupSub.UserDate = DateTime.Now;

            bool _result = this._fixedAssetBL.EditFAGroupSub(_msFAGroupSub);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }
    }
}
