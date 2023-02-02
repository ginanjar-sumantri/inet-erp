using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsTerm
    {
        public MsTerm(string _prmTermCode, string _prmTermName, int _prmXPeriod, string _prmTypeRange, int _prmXRange)
        {
            this.TermCode = _prmTermCode;
            this.TermName = _prmTermName;
            this.XPeriod = _prmXPeriod;
            this.TypeRange = _prmTypeRange;
            this.XRange = _prmXRange;
        }

        public MsTerm(string _prmTermCode, string _prmTermName)
        {
            this.TermCode = _prmTermCode;
            this.TermName = _prmTermName;
        }
    }
}
