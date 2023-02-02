using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_ScreeningProcess
    {
        public string _screeningSchedule = "";
        
        public HRM_ScreeningProcess(Guid _prmScreeningScheduleCode, string _prmScreeningSchedule, string _prmTransNmbr, 
            string _prmFileNmbr, DateTime _prmTransDateBegin, DateTime _prmTransDateEnd, byte _prmStatus,
            string _prmInstitution, string _prmRemark)
        {
            this.ScreeningScheduleCode = _prmScreeningScheduleCode;
            this.ScreeningSchedule = _prmScreeningSchedule;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDateBegin = _prmTransDateBegin;
            this.TransDateEnd = _prmTransDateEnd;
            this.Status = _prmStatus;
            this.Institution = _prmInstitution;
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
