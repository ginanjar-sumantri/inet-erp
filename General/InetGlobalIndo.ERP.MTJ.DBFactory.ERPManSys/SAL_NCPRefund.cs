using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class SAL_NCPRefund
    {
        public SAL_NCPRefund(String _prmTransNmbr, String _prmFileNmbr, DateTime _prmTransDate, String _prmNCPSalesNmbr, String _prmSerialNumber, Byte _prmStatus, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.NCPSalesNmbr = _prmNCPSalesNmbr;
            this.SerialNumber = _prmSerialNumber;
            this.Status = _prmStatus;
            this.Remark = _prmRemark;
        }
    }
}
