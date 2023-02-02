using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.GenerateVoucher
{
    public partial class GenerateVoucherAdd : GenerateVoucherBase
    {
        private GenerateVoucherBL _generateVoucherBL = new GenerateVoucherBL();
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

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.TransDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.ExpireDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttribute()
        {
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearData()
        {
            this.ClearLabel();
            this.SeriesTextBox.Text = "";
            this.ProductPicker1.ProductCode = "";
            this.ProductPicker1.ProductName = "";
            this.CardValueTextBox.Text = "";
            this.ProduceQtyTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            if ( this._generateVoucherBL.generateVoucher ( this.SeriesTextBox.Text,this.ProductPicker1.ProductCode,
                this.GeneratePINAuth.Checked , Convert.ToDateTime(this.TransDateTextBox.Text),
                Convert.ToDateTime(this.ExpireDate.Text), Convert.ToDouble( this.CardValueTextBox.Text),
                Convert.ToInt16 ( this.ProduceQtyTextBox.Text ), this.RemarkTextBox.Text , 16) )
                Response.Redirect(this._homePage);
            else
                this.WarningLabel.Text = "Your Failed Generate Voucher";
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