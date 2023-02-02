using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;
using BusinessRule.POS;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Promo
{
    public partial class PromoMemberAdd : PromoBase
    {
        private PromoBL _promoBL = new PromoBL();
        private MemberTypeBL _memberTypeBL = new MemberTypeBL();
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleDetailLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.AmountTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                //this.AmountTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.ShowMemberType();

                this.ClearData();
            }
        }

        protected void ShowMemberType()
        {
            this.MemberTypeDropDownList.DataSource = this._memberTypeBL.GetList();
            this.MemberTypeDropDownList.DataValueField = "MemberTypeCode";
            this.MemberTypeDropDownList.DataTextField = "MemberTypeName";
            this.MemberTypeDropDownList.DataBind();
            this.MemberTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            String _code = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            this.PromoCodeTextBox.Text = _code;
            this.MemberTypeDropDownList.SelectedValue = "null";
            this.AmountTypeRBL.SelectedIndex = 0;
            this.AmountTextBox.Text = "";
            this.FgGenderCheckBox.Checked = false;
            this.GenderRBL.SelectedIndex = 0;
            this.GenderRBL.Enabled = false;
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {

            POSMsPromoMember _posMsPromoMember = new POSMsPromoMember();

            _posMsPromoMember.PromoCode = this.PromoCodeTextBox.Text;
            _posMsPromoMember.MemberType = this.MemberTypeDropDownList.SelectedValue;
            _posMsPromoMember.AmountType = Convert.ToChar(this.AmountTypeRBL.SelectedValue);
            _posMsPromoMember.Amount = Convert.ToInt64(this.AmountTextBox.Text);
            _posMsPromoMember.FgGender = (this.FgGenderCheckBox.Checked == true) ? 'Y' : 'N';
            if (this.FgGenderCheckBox.Checked == true)
            {
                _posMsPromoMember.Gender = (this.GenderRBL.SelectedValue == "Male") ? Convert.ToByte(1) : Convert.ToByte(0);
            }
            _posMsPromoMember.FgActive = (this.FgActiveCheckBox.Checked == false) ? 'N' : 'Y';
            _posMsPromoMember.Remark = this.RemarkTextBox.Text;
            _posMsPromoMember.CreatedBy = HttpContext.Current.User.Identity.Name;
            _posMsPromoMember.CreatedDate = DateTime.Now; 

            bool _que = this._promoBL.CheckAvailable(this.PromoCodeTextBox.Text, this.MemberTypeDropDownList.SelectedValue);
            if (_que == true)
            {
                bool _result = this._promoBL.AddPOSMsPromoMember(_posMsPromoMember);

                if (_result == true)
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Add Data";
                }
            }
            else
            {
                this.WarningLabel.Text = "This Promo can't be given to this member";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
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