using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsReprimand
    {
        public HRMMsReprimand(string _prmReprimandCode, string _prmReprimandName, string _prmReprimandStatus, int? _prmReprimandLevel,
            char? _prmFgMaxTaken, int? _prmMaxTaken, int? _prmRangeEffective)
        {
            this.ReprimandCode = _prmReprimandCode;
            this.ReprimandName = _prmReprimandName;
            this.ReprimandStatus = _prmReprimandStatus;
            this.ReprimandLevel = _prmReprimandLevel;
            this.FgMaxTaken = _prmFgMaxTaken;
            this.MaxTaken = _prmMaxTaken;
            this.RangeEffective = _prmRangeEffective;
        }

        public HRMMsReprimand(string _prmReprimandCode, string _prmReprimandName)
        {
            this.ReprimandCode = _prmReprimandCode;
            this.ReprimandName = _prmReprimandName;
        }
    }
}
