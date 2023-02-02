using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_AbsenceRequest
    {
        private string _absenceTypeName = "";
        private string _empName = "";

        public HRM_AbsenceRequest(Guid _prmAbsenceRequestCode, string _prmTransNmbr, string _prmFileNmbr,
            DateTime _prmTransDate, byte _prmStatus, string _prmEmpNumb, string _prmEmpName,
            DateTime _prmStartDate, DateTime _prmEndDate, String _prmAbsenceTypeCode,
            string _prmAbsenceTypeName, int _prmDays, string _prmRemark)
        {
            this.AbsenceRequestCode = _prmAbsenceRequestCode;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
            this.AbsenceTypeCode = _prmAbsenceTypeCode;
            this.AbsenceTypeName = _prmAbsenceTypeName;
            this.Days= _prmDays;
            this.Remark = _prmRemark;
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
