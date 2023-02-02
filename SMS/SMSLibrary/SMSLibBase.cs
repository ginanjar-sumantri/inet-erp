using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMSLibrary.Properties;

namespace SMSLibrary
{
    public class SMSLibBase : IDisposable
    {
        protected SMSDBDataContext db = new SMSDBDataContext(Settings.Default.SMSPortalConnectionString);

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
