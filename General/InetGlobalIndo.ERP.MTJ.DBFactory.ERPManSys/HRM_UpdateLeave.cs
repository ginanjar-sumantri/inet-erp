using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_UpdateLeave
    {
        private string _empName = "";

        public HRM_UpdateLeave(Guid _prmAbsenceRequestCode, string _prmTransNmbr, string _prmFileNmbr,
            DateTime _prmTransDate, byte _prmStatus, String _prmEmpNumb, String _prmEmpName,
            byte _prmLeaveDayRemain, byte _prmLeaveCurrent, DateTime? _prmExpiredDateLeaveCurrent,
            DateTime? _prmExpiredDateLeaveRemain, string _prmRemark)
        {
            this.UpdateLeaveCode = _prmAbsenceRequestCode;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.LeaveDayRemain = _prmLeaveDayRemain;
            this.LeaveCurrent = _prmLeaveCurrent;
            this.ExpiredDateLeaveCurrent = _prmExpiredDateLeaveCurrent;
            this.ExpiredDateLeaveRemain = _prmExpiredDateLeaveRemain;
            this.Remark = _prmRemark;
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
