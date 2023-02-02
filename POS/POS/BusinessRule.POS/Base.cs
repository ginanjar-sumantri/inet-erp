using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace BusinessRule.POS
{
    public abstract class Base : IDisposable
    {
        protected MTJERPManSysDataContext db = new MTJERPManSysDataContext(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));
        protected MTJMembershipDataContext dbMembership = new MTJMembershipDataContext(ApplicationConfig.MembershipConnString);
        //protected GeneralDataContext dbGeneral = new GeneralDataContext(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

        protected internal string _string = "";
        protected internal char _char = ' ';
        protected internal DateTime _datetime = new DateTime();
        protected internal int _int = 0;
        protected internal Decimal _decimal = 0;
        protected internal char? _nullableChar = ' ';
        protected internal Decimal? _nullableDecimal = 0;

        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Check
        public static bool IsNumeric(string _value)
        {
            try
            {
                Convert.ToDecimal(_value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
