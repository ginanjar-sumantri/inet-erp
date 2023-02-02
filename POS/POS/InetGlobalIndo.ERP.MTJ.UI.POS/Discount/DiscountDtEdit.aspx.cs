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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Discount
{
    public partial class DiscountDtEdit : DiscountBase
    {
        private POSDiscountBL _discountBL = new POSDiscountBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.DiscountConfigCodeTextBox.Attributes.Add("ReadOnly", "True");
                this.MemberTypeTextBox.Attributes.Add("ReadOnly", "True");
                this.showAmountType();
                //this.ReferenceTextBox.Attributes.Add("ReadOnly", "True");

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }
        public void showAmountType()
        {
            POSMsDiscountConfig _posMsDiscountConfig = this._discountBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            String _amountType = _posMsDiscountConfig.AmountType;
            if (_amountType == "P")
            {
                this.DiscountAmountRangeValidator.Visible = true;
            }
            else
            {
                this.DiscountAmountRangeValidator.Visible = false;
            }
        }
        public void ShowData()
        {
            POSMsDiscountConfigMember _posMsDiscountConfigMember = this._discountBL.GetSinglePOSMsDiscountConfigMember(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            this.DiscountConfigCodeTextBox.Text = _posMsDiscountConfigMember.DiscountConfigCode;
            this.MemberTypeTextBox.Text = _posMsDiscountConfigMember.MemberType;
            this.DiscountAmountTextBox.Text = Convert.ToDecimal(_posMsDiscountConfigMember.DiscountAmount).ToString("#");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsDiscountConfigMember _posMsDiscountConfigMember = this._discountBL.GetSinglePOSMsDiscountConfigMember(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            _posMsDiscountConfigMember.DiscountAmount = Convert.ToInt64(this.DiscountAmountTextBox.Text);

            bool _result = this._discountBL.EditPOSMsDiscountConfigMember(_posMsDiscountConfigMember);

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
    }
}