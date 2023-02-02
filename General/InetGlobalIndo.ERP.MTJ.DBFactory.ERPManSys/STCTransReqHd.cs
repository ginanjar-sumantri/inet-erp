using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCTransReqHd
    {
        public STCTransReqHd(string _prmTransNmbr,string _prmFileNmbr ,DateTime _prmTransDate, char _prmStatus, string _prmWrhsAreaSrc, string _prmWrhsAreaDest, string _prmRequestBy)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.WrhsAreaSrc = _prmWrhsAreaSrc;
            this.WrhsAreaDest = _prmWrhsAreaDest;
            this.RequestBy = _prmRequestBy;
        }

        public STCTransReqHd(string _prmTransNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
        }

        public STCTransReqHd(string _prmTransNmbr,string _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }
    }
}
