using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLPeriod
    {
        public GLPeriod(int _prmPeriod, string _prmDescription)
        {
            this.Period = _prmPeriod;
            this.Description = _prmDescription;
        }
    }
}
