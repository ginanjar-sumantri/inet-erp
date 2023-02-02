using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCOpnameDt
    {
        public STCOpnameDt(string _prmTransNmbr, string _prmProductCode, string _prmLocationCode, decimal _prmQtySystem, decimal _prmQtyActual, decimal _prmQtyOpname, string _prmUnit, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.LocationCode = _prmLocationCode;
            this.QtySystem = _prmQtySystem;
            this.QtyActual = _prmQtyActual;
            this.QtyOpname = _prmQtyOpname;
            this.Unit = _prmUnit;
            this.Remark = _prmRemark;
        }

        public STCOpnameDt(string _prmLocationCode)
        {
            this.LocationCode = _prmLocationCode;
        }
    }
}
