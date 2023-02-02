using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_ScreeningSchedule
    {
        public string _processTypeName = "";

        public HRM_ScreeningSchedule(Guid _prmScreeningScheduleCode, string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, byte _prmStatus, Guid _prmApplicantResumeScreeningCode, Guid _prmProcessTypeCode, string _prmProcessTypeName, DateTime _prmContactingDate, DateTime _prmMeetingDate)
        {
            this.ScreeningScheduleCode = _prmScreeningScheduleCode;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.ApplicantResumeScreeningCode = _prmApplicantResumeScreeningCode;
            this.ProcessTypeCode = _prmProcessTypeCode;
            this.ProcessTypeName = _prmProcessTypeName;
            this.ContactingDate = _prmContactingDate;
            this.MeetingDate = _prmMeetingDate;
        }

        public HRM_ScreeningSchedule(Guid _prmScreeningScheduleCode, string _prmFileNmbr)
        {
            this.ScreeningScheduleCode = _prmScreeningScheduleCode;
            this.FileNmbr = _prmFileNmbr;
        }

        public string ProcessTypeName
        {
            get
            {
                return this._processTypeName;
            }
            set
            {
                this._processTypeName = value;
            }
        }
    }
}
