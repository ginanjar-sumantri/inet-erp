using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrAttendanceDaily
    {
        private string _empName = "";
        private string _shiftName = "";
        private string _absenceTypeName = "";

        public HRMTrAttendanceDaily(string _prmEmpNumb, String _prmEmpName, DateTime _prmTransDate, Char _prmStatus, String _prmShiftCode,
            String _prmShiftName, String _prmAbsenceTypeCode, String _prmAbsenceTypeName, String _prmClockIn, String _prmClockOut)
        {
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.ShiftCode = _prmShiftCode;
            this.ShiftName = _prmShiftName;
            this.AbsenceTypeCode = _prmAbsenceTypeCode;
            this.AbsenceTypeName = _prmAbsenceTypeName;
            this.ClockIn = _prmClockIn;
            this.ClockOut = _prmClockOut;
        }

        public HRMTrAttendanceDaily(string _prmEmpNumb, String _prmEmpName, DateTime _prmTransDate, Char _prmStatus, string _prmClockIn, string _prmClockOut)
        {
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.ClockIn = _prmClockIn;
            this.ClockOut = _prmClockOut;
        }

        public string EmpName
        {
            get
            {
                return this._empName;
            }
            set
            {
                this._empName = value;
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

        public string AbsenceTypeName
        {
            get
            {
                return this._absenceTypeName;
            }
            set
            {
                this._absenceTypeName = value;
            }
        }
    }
}
