using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSTrSettlementDtRefTransaction
    {
        public POSTrSettlementDtRefTransaction(String _prmTransNmbr, String _prmTransType, String _prmReferenceNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.TransType = _prmTransType;
            this.ReferenceNmbr = _prmReferenceNmbr;
        }
    }
}
