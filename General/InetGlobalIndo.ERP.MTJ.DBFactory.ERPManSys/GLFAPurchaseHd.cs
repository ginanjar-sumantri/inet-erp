using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFAPurchaseHd
    {
        public GLFAPurchaseHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmSuppCode, string _prmCurrCode)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.SuppCode = _prmSuppCode;
            this.CurrCode = _prmCurrCode;
        }
    }
}
