using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_ApplicantScreening_Resume
    {
        public HRM_ApplicantScreening_Resume(Guid _prmApplicantResumeScreeningCode, Guid _prmApplicantResumeCode, DateTime? _prmExpectedReturn, string _prmRemark, bool _prmIsClose)
        {
            this.ApplicantResumeScreeningCode = _prmApplicantResumeScreeningCode;
            this.ApplicantResumeCode = _prmApplicantResumeCode;
            this.ExpectedReturn = _prmExpectedReturn;
            this.Remark = _prmRemark;
            this.IsClose = _prmIsClose;
        }
    }
}
