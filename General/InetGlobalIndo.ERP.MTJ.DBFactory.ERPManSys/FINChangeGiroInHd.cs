using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINChangeGiroInHd
    {
        public FINChangeGiroInHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, string _prmSuppCode, string _prmCustCode, char _prmStatus)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.SuppCode = _prmSuppCode;
            this.CustCode = _prmCustCode;
            this.Status = _prmStatus;
        }
    }
}
