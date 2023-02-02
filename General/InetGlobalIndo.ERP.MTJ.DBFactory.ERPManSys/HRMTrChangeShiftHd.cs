using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrChangeShiftHd
    {
        private string _ChangeShiftTypeName = "";

        public HRMTrChangeShiftHd(string _prmTransNmbr, String _prmFileNmbr, char _prmStatus, DateTime _prmTransDate, String _prmChangeType, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.ChangeType = _prmChangeType;
            this.Remark = _prmRemark;
        }

        public HRMTrChangeShiftHd(string _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }
    }
}
