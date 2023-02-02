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

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.PromoCodeTextBox.Attributes.Add("ReadOnly", "True");
                this.MemberTypeTextBox.Attributes.Add("ReadOnly", "True");
                this.AmountTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                
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
            POSMsPromoMember _posMsPromoMember = this._promoBL.GetSinglePOSMsPromoMember(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            this.PromoCodeTextBox.Text = _posMsPromoMember.PromoCode;
            this.MemberTypeTextBox.Text = _posMsPromoMember.MemberType;
            this.AmountTypeRBL.SelectedValue = _posMsPromoMember.AmountType.ToString();
            this.AmountTextBox.Text = Convert.ToDecimal(_posMsPromoMember.Amount).ToString("#");
            this.FgGenderCheckBox.Checked = (_posMsPromoMember.FgGender == 'Y') ? true : false;
            if (_posMsPromoMember.FgGender == 'Y')
                this.GenderRBL.SelectedValue = _posMsPromoMember.Gender.ToString();
            else
                this.GenderRBL.Enabled = false;
            this.FgActiveCheckBox.Checked = (_posMsPromoMember.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _posMsPromoMember.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsPromoMember _posMsPromoMember = this._promoBL.GetSinglePOSMsPromoMember(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            _posMsPromoMember.AmountType = Convert.ToChar(this.AmountTypeRBL.SelectedValue);
            _posMsPromoMember.Amount = Convert.ToInt64(this.AmountTextBox.Text);
            _posMsPromoMember.FgGender = (this.FgGenderCheckBox.Checked == true) ? 'Y' : 'N';
            if (this.FgGenderCheckBox.Checked == true)
            {
                _posMsPromoMember.Gender = (this.GenderRBL.SelectedValue == "Male") ? Convert.ToByte(0) : Convert.ToByte(1);
            }
            _posMsPromoMember.FgActive = (this.FgActiveCheckBox.Checked == false) ? 'N' : 'Y';
            _posMsPromoMember.Remark = this.RemarkTextBox.Text;
            _posMsPromoMember.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _posMsPromoMember.ModifiedDate = DateTime.Now;

            bool _result = this._promoBL.EditPOSMsPromoMember(_posMsPromoMember);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void FgGenderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.FgGenderCheckBox.Checked == true)
                this.GenderRBL.Enabled = true;
            else
                this.GenderRBL.Enabled = false;
        }
    }
}