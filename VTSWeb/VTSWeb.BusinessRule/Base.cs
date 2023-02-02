using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTSWeb.Database;
using VTSWeb.SystemConfig;

namespace VTSWeb.BusinessRule
{
    public abstract class Base : IDisposable
    {
        protected VTSDatabaseDataContext db = new VTSDatabaseDataContext(ApplicationConfig.ConnString);
        //protected SimpleTaskListDatabaseDataContext db = new SimpleTaskListDatabaseDataContext(GetConString.GetConnString(GetConString.ConnString));

        protected internal string _string = "";
        protected internal char _char = ' ';
        protected internal DateTime _datetime = new DateTime();
        protected internal int _int = 0;
        protected internal Decimal _decimal = 0;

        #region IDisposable Members
        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
