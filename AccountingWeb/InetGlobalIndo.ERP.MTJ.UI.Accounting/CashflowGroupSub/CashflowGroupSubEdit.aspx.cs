using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.CashflowGroupSub
{
    public partial class CashflowGroupSubEdit : CashflowGroupSubBase
    {
        private CashflowgroupBL _cashflowGroupBL = new CashflowgroupBL();
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
                this.PageTitleLiteral.Text = this._transPageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.SaveButton.Visible = false;
                this.SaveAndViewDetailButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {

        }

        private void ShowData()
        {
            FINMsCashFlowGroupSub _finMsCFGroupSub = this._cashflowGroupBL.GetSingleCFGroupSub(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey2), ApplicationConfig.EncryptionKey));

            this.CodeTextBox.Text = _finMsCFGroupSub.CashFlowGroupSubCode;
            this.NameTextBox.Text = _finMsCFGroupSub.CashFlowGroupSubName;
            this.SumTypeDropDownList.SelectedValue = _finMsCFGroupSub.TypeCode.Trim();
            this.OperatorDropDownList.SelectedValue = _finMsCFGroupSub.Operator.ToString().Trim();
            this.CFTypeTextBox.Text = _finMsCFGroupSub.CashFlowType;

        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINMsCashFlowGroupSub _finMsCFGroupSub = this._cashflowGroupBL.GetSingleCFGroupSub(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey2), ApplicationConfig.EncryptionKey));

            _finMsCFGroupSub.CashFlowGroupSubName = this.NameTextBox.Text;
            _finMsCFGroupSub.Operator = Convert.ToChar(this.OperatorDropDownList.SelectedValue);
            _finMsCFGroupSub.TypeCode = this.SumTypeDropDownList.SelectedValue;

            _finMsCFGroupSub.EditBy = HttpContext.Current.User.Identity.Name;
            _finMsCFGroupSub.EditDate = DateTime.Now;

            bool _result = this._cashflowGroupBL.EditCFGroupSub(_finMsCFGroupSub);

            if (_result == true)
            {
                Response.Redirect(this._transHomePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_transHomePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, EventArgs e)
        {
            //Response.Redirect(this._homePage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.CodeTextBox.Text, ApplicationConfig.EncryptionKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, EventArgs e)
        {
            //bool _result = simpen();
            //if (_result == true)
            //{
            //    Response.Redirect(this._homePage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.CodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            //}
            //else
            //{
            //    this.WarningLabel.Text = "You Failed Edit Data";
            //}
        }
    }
}