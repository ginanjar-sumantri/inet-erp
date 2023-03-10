using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCReturSJHd
    {
        public STCReturSJHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus,
            string _prmCustName, string _prmRRReturNo, string _prmWrhsName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.CustName = _prmCustName;
            this.RRReturNo = _prmRRReturNo;
            this.WrhsName = _prmWrhsName;
        }

        public STCReturSJHd(string _prmTransNmbr, string _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }

        public String CustName { get; set; }
        public String WrhsName { get; set; }
    }
}
