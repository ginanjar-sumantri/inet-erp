using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MKTDOHd
    {

        public MKTDOHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmCustCode, string _prmSONo, string _prmPOCustNo)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.CustCode = _prmCustCode;
            this.SONo = _prmSONo;
            this.POCustNo = _prmPOCustNo;
        }

        public MKTDOHd(string _prmTransNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
        }

        public MKTDOHd(string _prmTransNmbr, string _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }
    }
}
