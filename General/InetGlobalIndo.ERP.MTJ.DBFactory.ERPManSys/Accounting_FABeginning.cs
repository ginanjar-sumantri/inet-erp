using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Accounting_FABeginning
    {
        public Accounting_FABeginning(Guid _prmFABeginningCode, string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, byte _prmStatus, string _prmRemark)
        {
            this.FABeginningCode = _prmFABeginningCode;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.Remark = _prmRemark;
        }
    }
}
