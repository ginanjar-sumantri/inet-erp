using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrScheduleShiftHd
    {
        private string _ScheduleShiftTypeName = "";

        public HRMTrScheduleShiftHd(string _prmTransNmbr, String _prmFileNmbr, char _prmStatus, DateTime _prmTransDate, String _prmPeriodCode, DateTime _prmStartDate, DateTime _prmEndDate, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.PeriodCode = _prmPeriodCode;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
            this.Remark = _prmRemark;
        }

        public HRMTrScheduleShiftHd(string _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }
    }
}
