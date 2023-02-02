using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrClaimIssueHd
    {
        private string _empName = "";

        public HRMTrClaimIssueHd(string _prmTransNmbr, String _prmFileNmbr, char _prmStatus, DateTime _prmTransDate, String _prmEmpNumb, String _prmEmpName,
            String _prmCurrCode, decimal? _prmForexRate, DateTime? _prmAccidentDate, String _prmClaimFor, String _prmClaimFamilyInfo, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.AccidentDate = _prmAccidentDate;
            this.ClaimFor = _prmClaimFor;
            this.ClaimFamilyInfo = _prmClaimFamilyInfo;
            this.Remark = _prmRemark;
        }

        public HRMTrClaimIssueHd(string _prmTransNmbr, String _prmFileNmbr)
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
