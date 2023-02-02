using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLemburHd
    {
        public HRMTrLemburHd(string _prmTransNmbr, String _prmFileNmbr, char _prmStatus, DateTime _prmTransDate, String _prmDayType, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.DayType = _prmDayType;
            this.Remark = _prmRemark;
        }

        public HRMTrLemburHd(string _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }
    }
}
