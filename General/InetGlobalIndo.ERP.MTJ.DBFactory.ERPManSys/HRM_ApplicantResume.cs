using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_ApplicantResume
    {

        public HRM_ApplicantResume(Guid _prmResumeCode, string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, string _prmName, byte _prmStatus)
        {
            this.ApplicantResumeCode = _prmResumeCode;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Name = _prmName;
            this.Status = _prmStatus;
        }

        public HRM_ApplicantResume(Guid _prmAppResumeCode, string _prmFileNmbr)
        {
            this.ApplicantResumeCode = _prmAppResumeCode;
            this.FileNmbr = _prmFileNmbr;
        }
    }
}
