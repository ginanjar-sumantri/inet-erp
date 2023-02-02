using System;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings
{
    public partial class _Default : SettingsBase
    {
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            //this._permAccess = this._permBL.PermissionValidation(this._moduleID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            //if (this._permAccess == false)
            //{
            //    Response.Redirect(this._errorPermissionPage);
            //}
        }
    }
}