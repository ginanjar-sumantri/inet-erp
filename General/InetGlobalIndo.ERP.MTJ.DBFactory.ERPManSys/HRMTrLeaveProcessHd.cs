using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLeaveProcessHd
    {
        public HRMTrLeaveProcessHd(int _prmProcessYear, int _prmProcessPeriod, char? _prmStatus, String _prmRemark)
        {
            this.ProcessYear = _prmProcessYear;
            this.ProcessPeriod = _prmProcessPeriod;
            this.Status = _prmStatus;
            this.Remark = _prmRemark;
        }
    }
}
