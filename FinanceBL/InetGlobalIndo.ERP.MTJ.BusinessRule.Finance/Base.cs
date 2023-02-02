using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public abstract class Base : IDisposable
    {
        protected UserBL _userBL = null;
        protected MTJERPManSysDataContext db = null;

        protected internal String _currentUser = HttpContext.Current.User.Identity.Name;
        protected internal String _companyTag = "";

        protected internal string _string = "";
        protected internal char _char = ' ';
        protected internal DateTime _datetime = new DateTime();
        protected internal int _int = 0;
        protected internal Decimal _decimal = 0;
        protected internal char? _nullableChar = ' ';

        protected Base()
        {
            this._userBL = new UserBL();
            this.db = new MTJERPManSysDataContext(this._userBL.ConnectionString(this._currentUser));

            this._companyTag = this._userBL.GetCompanyTag(this._userBL.GetCompanyId(this._currentUser));
        }
        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
