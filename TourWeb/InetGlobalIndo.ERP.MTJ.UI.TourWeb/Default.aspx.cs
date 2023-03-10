using System;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.UI.Tour;

namespace InetGlobalIndo.ERP.MTJ.UI.Tour
{
    public partial class _Default : TourBase
    {
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._moduleID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }
        }
    }
}