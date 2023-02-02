using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrAttendanceClock
    {
        private string _empName = "";

        public HRMTrAttendanceClock(string _prmEmpNumb, String _prmEmpName, DateTime _prmTransDate, Char? _prmOperation, Char? _prmTransStatus)
        {
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.TransDate = _prmTransDate;
            this.Operation = _prmOperation;
            this.TransStatus = _prmTransStatus;
        }

        public HRMTrAttendanceClock(string _prmEmpNumb, DateTime _prmTransDate)
        {
            this.EmpNumb = _prmEmpNumb;
            this.TransDate = _prmTransDate;
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
    }
}
