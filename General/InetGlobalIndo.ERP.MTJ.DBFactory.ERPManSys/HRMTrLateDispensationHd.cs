using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLateDispensationHd
    {
        public HRMTrLateDispensationHd(String _prmTransNmbr, String _prmFileNmbr, DateTime _prmTransDate, Char _prmStatus, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.Remark = _prmRemark;
        }
    }
}
