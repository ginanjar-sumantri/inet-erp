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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.OtherSurcharge
{
    public partial class OtherSurchargeEdit : OtherSurchargeBase
    {
        private OtherSurchargeBL _otherSurchargeBL = new OtherSurchargeBL();
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
                this.CodeTextBox.Attributes.Add("ReadOnly", "True");
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
            POSMsShippingO _posMsShippingOS = this._otherSurchargeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CodeTextBox.Text = _posMsShippingOS.OtherSurchargeCode.ToString();
            this.NameTextBox.Text = _posMsShippingOS.OtherSurchargeName.ToString();
            if (_posMsShippingOS.FgActive == 'Y')
                this.FgActiveCheckBox.Checked = true;
            else
                this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = _posMsShippingOS.Remark.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsShippingO _posMsShippingOS = this._otherSurchargeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            _posMsShippingOS.OtherSurchargeName = this.NameTextBox.Text;
            if (this.FgActiveCheckBox.Checked == true)
                _posMsShippingOS.FgActive = 'Y';
            else
                _posMsShippingOS.FgActive = 'N';
            _posMsShippingOS.Remark = this.RemarkTextBox.Text;
            _posMsShippingOS.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _posMsShippingOS.ModifiedDate = DateTime.Now;

            bool _result = this._otherSurchargeBL.EditSubmit();

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