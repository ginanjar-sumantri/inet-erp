using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.CustomControl
{
    public abstract class Base : Control, IDisposable
    {
        protected UserBL _userBL = null;
        protected MTJERPManSysDataContext db = null;
        protected MTJMembershipDataContext dbMembership = new MTJMembershipDataContext(ApplicationConfig.MembershipConnString);

        protected internal String _currentUser = HttpContext.Current.User.Identity.Name;
        protected internal String _companyTag = "";

        protected Base()
        {
            this._userBL = new UserBL();
            this.db = new MTJERPManSysDataContext(this._userBL.ConnectionString(this._currentUser));

            this._companyTag = this._userBL.GetCompanyTag(this._userBL.GetCompanyId(this._currentUser));
        }
        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}