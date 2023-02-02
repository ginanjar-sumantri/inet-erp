using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCReturRRDt
    {
        public STCReturRRDt(string _prmTransNmbr, string _prmProductCode, string _prmProductName,
            string _prmLocationCode, string _prmLocationName, decimal _prmQty, string _prmUnit)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.LocationCode = _prmLocationCode;
            this.LocationName = _prmLocationName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
        }

        public String ProductName { get; set; }
        public String LocationName { get; set; }
    }
}
