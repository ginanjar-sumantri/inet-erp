using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Port_PackingListDt
    {
        public Port_PackingListDt(Guid _prmPackingListDtCode, Guid _prmPackingListHdCode, string _prmProductCode, decimal _prmPackage, decimal _prmNetWeight, decimal _prmGrossWeight)
        {
            this.PackingListDtCode = _prmPackingListDtCode;
            this.PackingListHdCode = _prmPackingListHdCode;
            this.ProductCode = _prmProductCode;
            this.Package = _prmPackage;
            this.NetWeight = _prmNetWeight;
            this.GrossWeight = _prmGrossWeight;
        }
    }
}
