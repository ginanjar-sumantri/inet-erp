using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Settings
{
    public abstract class Base : IDisposable
    {
        //protected UserBL _userBL = null;
        //protected GeneralDataContext dbGeneral = null;
        //protected MTJERPManSysDataContext dbMansys = new MTJERPManSysDataContext(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));
        protected MTJMembershipDataContext db = new MTJMembershipDataContext(ApplicationConfig.MembershipConnString);
        protected MTJMembershipDataContext dbMembership = new MTJMembershipDataContext(ApplicationConfig.MembershipConnString);
        
        protected internal String _currentUser = HttpContext.Current.User.Identity.Name;
        protected internal String _companyTag = "";

        protected internal string _string = "";
        protected internal char _char = ' ';
        protected internal Guid _guid = new Guid();
        protected internal DateTime _datetime = new DateTime();
        protected internal int _int = 0;
        protected internal Decimal _decimal = 0;
        protected internal char? _nullableChar = ' ';
        protected internal byte _byte = 0;
        protected internal Boolean _boolean = false;

        //protected Base()
        //{
            //this._userBL = new UserBL();
            //this.dbGeneral = new GeneralDataContext(this._userBL.ConnectionString(this._currentUser));

            //this._companyTag = this._userBL.GetCompanyTag(this._userBL.GetCompanyId(this._currentUser));
        //}

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
