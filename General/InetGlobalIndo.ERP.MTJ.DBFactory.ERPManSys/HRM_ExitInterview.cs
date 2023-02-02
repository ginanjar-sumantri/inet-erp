using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_ExitInterview
    {
        private string _empName = "";

        public HRM_ExitInterview(Guid _prmTerminationRequestCode, string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, byte _prmStatus, string _prmEmpNumb, string _prmEmpName, string _prmRemark)
        {
            this.TerminationRequestCode = _prmTerminationRequestCode;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
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
