using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_ApplicantFinishing
    {
        private string _recruitmentRequestFileNmbr = "";

        public HRM_ApplicantFinishing(Guid _prmApplicantFinishingCode, string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, string _prmRecruitmentRequestFileNmbr, byte _prmStatus)
        {
            this.ApplicantFinishingCode = _prmApplicantFinishingCode;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.RecruitmentRequestFileNmbr = _prmRecruitmentRequestFileNmbr;
            this.Status = _prmStatus;
        }

        public string RecruitmentRequestFileNmbr
        {
            get
            {
                return this._recruitmentRequestFileNmbr;
            }
            set
            {
                this._recruitmentRequestFileNmbr = value;
            }
        }
    }
}
