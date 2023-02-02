using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrScheduleShiftDt
    {
        private string _empGroupName = "";
        private string _shiftName = "";
        private string _toEmpGroupName = "";

        public HRMTrScheduleShiftDt(String _prmTransNmbr, String _prmEmpGroupCode, String _prmEmpGroupName, DateTime _prmEffectiveDate,
            String _prmShiftCode, String _prmShiftName, int? _prmNoDay)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpGroupCode = _prmEmpGroupCode;
            this.EmpGroupName = _prmEmpGroupName;
            this.EffectiveDate = _prmEffectiveDate;
            this.ShiftCode = _prmShiftCode;
            this.ShiftName = _prmShiftName;
            this.NoDay = _prmNoDay;
        }

        public string EmpGroupName
        {
            get
            {
                return this._empGroupName;
            }
            set
            {
                this._empGroupName = value;
            }
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
