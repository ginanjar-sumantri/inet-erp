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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Promo
{
    public partial class PromoMemberEdit : PromoBase
    {
        private PromoBL _promoBL = new PromoBL();
        private PermissionBL _permBL = new PermissionBL();
        private MemberTypeBL _memberTypeBL = new MemberTypeBL();

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
                this.PageTitleLiteral.Text = this._pageTitleDetailLiteral;
                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.SetButtonPermission();
                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowData()
        {
            POSMsPromoMember _posMsPromoMember = this._promoBL.GetSinglePOSMsPromoMember(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            this.PromoCodeTextBox.Text = _posMsPromoMember.PromoCode;
            this.MemberTypeTextBox.Text = _posMsPromoMember.MemberType;
            this.AmountTypeRBL.SelectedValue = _posMsPromoMember.AmountType.ToString();
            this.AmountTextBox.Text = Convert.ToDecimal(_posMsPromoMember.Amount).ToString("#");
            this.FgGenderCheckBox.Checked = (_posMsPromoMember.FgGender == 'Y') ? true : false;
            if (_posMsPromoMember.FgGender == 'Y')
                this.GenderRBL.SelectedValue = _posMsPromoMember.Gender.ToString();
            this.FgActiveCheckBox.Checked = (_posMsPromoMember.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _posMsPromoMember.Remark;

            String _status = this._promoBL.GetSingle(this.PromoCodeTextBox.Text).Status.ToString();
            if (_status.Trim().ToLower() == POSPromoDataMapper.GetStatus(POSPromoStatus.Posting).ToString().ToLower())
            {
                this.EditButton.Visible = false;
            }
            else
            {
                this.EditButton.Visible = true;
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.MemberTypeTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}