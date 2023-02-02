using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_TerminationRequest
    {
        public string _empName = "";
        public string _reasonName = "";

        public HRM_TerminationRequest(Guid _prmTerminationRequestCode, string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, byte _prmStatus, string _prmEmpNumb, string _prmEmpName, String _prmReasonCode, string _prmReasonName, DateTime _prmExitDate, string _prmRemark)
        {
            this.TerminationRequestCode = _prmTerminationRequestCode;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.ReasonCode = _prmReasonCode;
            this.ReasonName = _prmReasonName;
            this.ExitDate = _prmExitDate;
            this.Remark = _prmRemark;
        }

        public HRM_TerminationRequest(Guid _prmTerminationRequestCode, string _prmFileNmbr)
        {
            this.TerminationRequestCode = _prmTerminationRequestCode;
            this.FileNmbr = _prmFileNmbr;
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

        public string ReasonName
        {
            get
            {
                return this._reasonName;
            }
            set
            {
                this._reasonName = value;
            }
        }
    }
}
