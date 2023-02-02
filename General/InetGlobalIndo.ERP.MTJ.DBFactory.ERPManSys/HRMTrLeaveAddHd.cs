using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLeaveAddHd
    {
        public HRMTrLeaveAddHd(String _prmTransNmbr, String _prmFileNmbr, byte? _prmStatus, DateTime? _prmTransDate, DateTime? _prmStartEffectiveDate, DateTime? _prmEndEffectiveDate)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.StartEffectiveDate = _prmStartEffectiveDate;
            this.EndEffectiveDate = _prmEndEffectiveDate;
        }

        public HRMTrLeaveAddHd(String _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }
    }
}
