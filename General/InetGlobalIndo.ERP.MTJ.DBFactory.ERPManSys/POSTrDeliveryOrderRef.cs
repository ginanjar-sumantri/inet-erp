using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSTrDeliveryOrderRef
    {
        public POSTrDeliveryOrderRef(String _prmReferenceNo, String _prmTransType, String _prmTransNmbr)
        {
            this.ReferenceNo = _prmReferenceNo;
            this.TransType = _prmTransType;
            this.TransNmbr = _prmTransNmbr;
        }
    }
}
