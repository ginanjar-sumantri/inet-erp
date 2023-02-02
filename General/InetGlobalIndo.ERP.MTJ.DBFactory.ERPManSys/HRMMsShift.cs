using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsShift
    {
        public HRMMsShift(string _prmShiftCode, string _prmShiftName, String _prmShiftIn, String _prmShiftOut, String _prmBreakIn, String _prmBreakOut,
            int? _prmBreakMinute, Boolean? _prmFgActive)
        {
            this.ShiftCode = _prmShiftCode;
            this.ShiftName = _prmShiftName;
            this.ShiftIn = _prmShiftIn;
            this.ShiftOut = _prmShiftOut;
            this.BreakIn = _prmBreakIn;
            this.BreakOut = _prmBreakOut;
            this.BreakMinute = _prmBreakMinute;
            this.FgActive = _prmFgActive;
        }

        public HRMMsShift(string _prmShiftCode, string _prmShiftName)
        {
            this.ShiftCode = _prmShiftCode;
            this.ShiftName = _prmShiftName;
        }
    }
}
