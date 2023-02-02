using System;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.TransferEndingBalance
{
    public partial class TransferEndingBalance : TransferEndingBalanceBase
    {
        private TransferEndingBalanceBL _transfer = new TransferEndingBalanceBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void isiDDL()
        {
            int _year = DateTime.Now.Year;

            for (int i = 9; i >= 0; i--)
            {
                this.TransferDDL.Items.Add((_year - i).ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.isiDDL();
                this.TransferDDL.Attributes.Add("OnChange", "TransferDDL_SelectedIndexChanged(" + this.TransferDDL.ClientID + ", " + this.Label1.ClientID + ");");
            }

            this.Label1.Text = "year, to Beginning of " + (Convert.ToInt32(this.TransferDDL.SelectedValue) + 1);
        }

        protected void ProcessButton_Click(object sender, EventArgs e)
        {
            bool _result = _transfer.TransferEndingBalance(Convert.ToInt32(this.TransferDDL.SelectedValue));
            if (_result == true)
            {
                this.WarningLabel.Text = "Success Tranfering Balance";
            }
            else
            {
                this.WarningLabel.Text = "Failed Tranfering Balance";
            }
        }
    }
}