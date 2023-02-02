using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_ScreeningProcessAttachment
    {
        public string _screeningSchedule = "";
        
        public HRM_ScreeningProcessAttachment(Guid _prmProcessAttachmentCode, Guid _prmScreeningScheduleCode, string _prmScreeningSchedule, string _prmFile, string _prmRemark)
        {
            this.ProcessAttachmentCode = _prmProcessAttachmentCode;
            this.ScreeningScheduleCode = _prmScreeningScheduleCode;
            this.ScreeningSchedule = _prmScreeningSchedule;
            this.File = _prmFile;
            this.Remark = _prmRemark;
        }

        public string ScreeningSchedule
        {
            get
            {
                return this._screeningSchedule;
            }
            set
            {
                this._screeningSchedule = value;
            }
        }
    }
}
