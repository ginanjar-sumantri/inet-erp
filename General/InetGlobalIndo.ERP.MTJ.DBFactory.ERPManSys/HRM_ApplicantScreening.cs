using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_ApplicantScreening
    {
        private string _recruitmentRequestFileNmbr = "";

        public HRM_ApplicantScreening(Guid _prmResumeCode, string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, string _prmRecruitmentRequestFileNmbr, byte _prmStatus)
        {
            this.ApplicantScreeningCode = _prmResumeCode;
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
