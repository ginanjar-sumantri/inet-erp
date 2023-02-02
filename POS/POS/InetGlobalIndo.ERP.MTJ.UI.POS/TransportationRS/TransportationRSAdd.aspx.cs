using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.TransportationRS
{
    public partial class TransportationRSAdd : TransportationRSBase
    {
        private TransportationRSBL _transportationRSBL = new TransportationRSBL();
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

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.CodeTextBox.Text = "";
            this.NameTextBox.Text = "";
        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();

            POSMsShippingTR _posMsShippingTypeTRS = this._transportationRSBL.GetSingle(this.CodeTextBox.Text);
            if (_posMsShippingTypeTRS != null)
                _errorMsg = _errorMsg + "Code = " + this.CodeTextBox.Text + " Already Exist.";

            this.WarningLabel.Text = _errorMsg.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsShippingTR _posMsShippingTRS = new POSMsShippingTR();

                _posMsShippingTRS.TRSCode = this.CodeTextBox.Text;
                _posMsShippingTRS.TRSName = this.NameTextBox.Text;
                if (this.FgActiveCheckBox.Checked == true)
                    _posMsShippingTRS.FgActive = 'Y';
                else
                    _posMsShippingTRS.FgActive = 'N';
                _posMsShippingTRS.Remark = this.RemarkTextBox.Text;
                _posMsShippingTRS.CreatedBy = HttpContext.Current.User.Identity.Name;
                _posMsShippingTRS.CreatedDate = DateTime.Now;
                _posMsShippingTRS.ModifiedBy = "";
                _posMsShippingTRS.ModifiedDate = this._defaultdate;

                bool _result = this._transportationRSBL.Add(_posMsShippingTRS);

                if (_result == true)
                {
                    Response.Redirect(this._homePage);
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Add Data";
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}