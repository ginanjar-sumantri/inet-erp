using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_ApplicantFinishing_Resume
    {
        private string _applicantResumeScreeningFileNmbr = "";

        public HRM_ApplicantFinishing_Resume(Guid _prmApplicantFinishingResumeCode, Guid _prmApplicantFinishingCode, String _prmApplicantResumeScreeningFileNmbr, String _prmEmpNumb, String _prmRemark)
        {
            this.ApplicantFinishingResumeCode = _prmApplicantFinishingResumeCode;
            this.ApplicantFinishingCode = _prmApplicantFinishingCode;
            this.ApplicantResumeScreeningFileNmbr = _prmApplicantResumeScreeningFileNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.Remark = _prmRemark;
        }

        public string ApplicantResumeScreeningFileNmbr
        {
            get
            {
                return this._applicantResumeScreeningFileNmbr;
            }
            set
            {
                this._applicantResumeScreeningFileNmbr = value;
            }
        }
    }
}
