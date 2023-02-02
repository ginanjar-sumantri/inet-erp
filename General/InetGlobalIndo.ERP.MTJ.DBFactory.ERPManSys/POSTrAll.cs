using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSTrAll
    {
        public POSTrAll(String _prmTransType, String _prmTransNmbr, String _prmProductCode, String _prmProductName, int _prmQty)
        {
            this.TransType = _prmTransType;
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.Qty = _prmQty;
        }

        public String TransType { get; set; }
        public String TransName { get; set; }
        public String TransNmbr { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public int Qty { get; set; }
    }
}
