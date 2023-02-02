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
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.MemberType
{
    public partial class MemberTypeEdit : MemberTypeBase
    {
        private MemberTypeBL _memberTypeBL = new MemberTypeBL();
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
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.MemberTypeCodeTextBox.Attributes.Add("ReadOnly", "True");
                
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            MsMemberType _msMemberType = this._memberTypeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.MemberTypeCodeTextBox.Text = _msMemberType.MemberTypeCode;
            this.MemberTypeNameTextBox.Text = _msMemberType.MemberTypeName;
            this.FgActiveCheckBox.Checked = (_msMemberType.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _msMemberType.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsMemberType _msMemberType = this._memberTypeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msMemberType.MemberTypeName = this.MemberTypeNameTextBox.Text;
            _msMemberType.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msMemberType.Remark = this.RemarkTextBox.Text;
            _msMemberType.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _msMemberType.ModifiedDate = DateTime.Now;

            bool _result = this._memberTypeBL.Edit(_msMemberType);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}