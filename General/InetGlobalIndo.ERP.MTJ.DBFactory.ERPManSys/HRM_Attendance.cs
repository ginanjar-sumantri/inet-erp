using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_Attendance
    {
        public string _employeeName = "";
        public string _absenceTypeName = "";

        public HRM_Attendance(Guid _prmAttendanceCode, String _prmAbsenceTypeCode, string _prmAbsenceTypeName, string _prmEmpNumb, string _prmEmpName, DateTime _prmAttendanceDate, String _prmRemark)
        {
            this.AttendanceCode = _prmAttendanceCode;
            this.AbsenceTypeCode = _prmAbsenceTypeCode;
            this.AbsenceTypeName = _prmAbsenceTypeName;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.AttendanceDate = _prmAttendanceDate;
            this.Remark = _prmRemark;
        }

        public string EmpName
        {
            get
            {
                return this._employeeName;
            }
            set
            {
                this._employeeName = value;
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
