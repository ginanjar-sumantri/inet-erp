using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    [Serializable]

    public class Base : IDisposable
    {
        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
