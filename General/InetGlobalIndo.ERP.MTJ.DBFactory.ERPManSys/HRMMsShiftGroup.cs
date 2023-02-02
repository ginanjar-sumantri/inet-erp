using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsShiftGroup
    {
        private string _shiftName = "";

        public HRMMsShiftGroup(string _prmShiftGroup, string _prmShiftCode, string _prmShiftName)
        {
            this.ShiftGroup = _prmShiftGroup;
            this.ShiftCode = _prmShiftCode;
            this.ShiftName = _prmShiftName;
        }

        public HRMMsShiftGroup(string _prmShiftGroup)
        {
            this.ShiftGroup = _prmShiftGroup;
        }

        public string ShiftName
        {
            get
            {
                return this._shiftName;
            }
            set
            {
                this._shiftName = value;
            }
        }
    }
}
