using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace BusinessRule.POSInterface
{
    public abstract class Base : IDisposable
    {
        protected UserBL _userBL = null; 
        
        protected MTJERPManSysDataContext db = new MTJERPManSysDataContext(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));
        protected MTJMembershipDataContext dbMembership = new MTJMembershipDataContext(ApplicationConfig.MembershipConnString);
        //protected GeneralDataContext dbGeneral = new GeneralDataContext(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

        protected internal String _currentUser = HttpContext.Current.User.Identity.Name;
        protected internal String _companyTag = "";
        
        protected internal string _string = "";
        protected internal char _char = ' ';
        protected internal DateTime _datetime = new DateTime();
        protected internal int _int = 0;
        protected internal Decimal _decimal = 0;
        protected internal char? _nullableChar = ' ';
        protected internal Decimal? _nullableDecimal = 0;

        protected Base()
        {
            this._userBL = new UserBL();
            this._companyTag = this._userBL.GetCompanyTag(this._userBL.GetCompanyId(this._currentUser));
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
