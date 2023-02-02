using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLoanChangeHd
    {
        private string _empName = "";

        public HRMTrLoanChangeHd(string _prmTransNmbr, String _prmFileNmbr, char _prmStatus, DateTime _prmTransDate, String _prmLoanInNo, String _prmEmpNumb,
             String _prmEmpName, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.LoanInNo = _prmLoanInNo;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.Remark = _prmRemark;
        }

        public HRMTrLoanChangeHd(string _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
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
    }
}
