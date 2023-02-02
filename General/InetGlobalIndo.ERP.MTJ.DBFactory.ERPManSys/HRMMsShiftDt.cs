using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsShiftDt
    {
        private string _dayName = "";

        public HRMMsShiftDt(string _prmShiftCode, int _prmDayCode, string _prmDayName, String _prmShiftIn, String _prmShiftOut, String _prmBreakIn, String _prmBreakOut, int? _prmBreakMinute)
        {
            this.ShiftCode = _prmShiftCode;
            this.DayCode = _prmDayCode;
            this.DayName = _prmDayName;
            this.ShiftIn = _prmShiftIn;
            this.ShiftOut = _prmShiftOut;
            this.BreakIn = _prmBreakIn;
            this.BreakOut = _prmBreakOut;
            this.BreakMinute = _prmBreakMinute;
        }

        public string DayName
        {
            get
            {
                return this._dayName;
            }
            set
            {
                this._dayName = value;
            }
        }
    }
}
