using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public abstract class Base : IDisposable
    {
        protected UserBL _userBL = null;
        protected MTJERPManSysDataContext db = null;

        //protected MTJERPManSysDataContext db = new MTJERPManSysDataContext(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

        //protected internal string _companyTag = this._u 
        //Guid _companyId = new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name);
        //string _companyTag = this._user.GetCompanyTag(this._u);

        protected internal String _currentUser = HttpContext.Current.User.Identity.Name;
        protected internal String _companyTag = "";

        protected internal string _string = "";
        protected internal char _char = ' ';
        protected internal DateTime _datetime = new DateTime();
        protected internal DateTime? _nullableDateTime = new DateTime?();
        protected internal int _int = 0;
        protected internal int? _nullableInt = 0;
        protected internal Decimal _decimal = 0;
        protected internal Decimal? _nullableDecimal = 0;
        protected internal char? _nullableChar = ' ';
        protected internal Guid _guid = new Guid();

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

        ~Base()
        {
        }
    }
}