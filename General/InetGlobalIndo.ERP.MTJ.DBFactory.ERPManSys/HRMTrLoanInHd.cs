using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLoanInHd
    {
        private string _loanName = "";

        public HRMTrLoanInHd(string _prmTransNmbr, String _prmFileNmbr, char _prmStatus, DateTime _prmTransDate, String _prmLoanCode, String _prmLoanName,
            String _prmPayrollCode, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.LoanCode = _prmLoanCode;
            this.LoanName = _prmLoanName;
            this.PayrollCode = _prmPayrollCode;
            this.Remark = _prmRemark;
        }

        public HRMTrLoanInHd(string _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }

        public string LoanName
        {
            get
            {
                return this._loanName;
            }
            set
            {
                this._loanName = value;
            }
        }
    }
}
