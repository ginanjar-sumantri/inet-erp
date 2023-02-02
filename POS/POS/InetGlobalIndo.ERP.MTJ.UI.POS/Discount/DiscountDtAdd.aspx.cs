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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Discount
{
    public partial class DiscountDtAdd : DiscountBase
    {
        private POSDiscountBL _discountBL = new POSDiscountBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.DiscountAmountTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.DiscountAmountTextBox.Attributes.Add("OnChange", "HarusAngka(this);");
                this.showAmountType();

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

            this.DiscountConfigCodeTextBox.Text = _code;
            this.MemberTypeDropDownList.SelectedValue = "null";
            this.DiscountAmountTextBox.Text = "";
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {

            POSMsDiscountConfigMember _posMsDiscountConfigMember = new POSMsDiscountConfigMember();

            _posMsDiscountConfigMember.DiscountConfigCode = this.DiscountConfigCodeTextBox.Text;
            _posMsDiscountConfigMember.MemberType = this.MemberTypeDropDownList.SelectedValue;
            _posMsDiscountConfigMember.DiscountAmount = Convert.ToInt64(this.DiscountAmountTextBox.Text);

            bool _que = this._discountBL.CheckhAvailableDisc(this.DiscountConfigCodeTextBox.Text, this.MemberTypeDropDownList.SelectedValue);
                if(_que == true)
                {
                    bool _result = this._discountBL.AddPOSMsDiscountConfigMember(_posMsDiscountConfigMember);

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
                    this.WarningLabel.Text = "This discount can't be given to this member";
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
    }
}