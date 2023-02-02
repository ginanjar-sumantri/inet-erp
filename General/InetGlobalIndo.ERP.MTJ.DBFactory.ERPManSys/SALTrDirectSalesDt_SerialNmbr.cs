using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class SALTrDirectSalesDt_SerialNmbr
    {
        public SALTrDirectSalesDt_SerialNmbr(String _prmSerialNmbr)
        {
            this.SerialNmbr = _prmSerialNmbr;
        }

        public SALTrDirectSalesDt_SerialNmbr(String _prmTransNmbr, String _prmProductCode ,String _prmSerialNmbr, String _prmLocationCode, String _prmWrhsCode)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.SerialNmbr = _prmSerialNmbr;
            this.WLocationCode = _prmLocationCode;
            this.WrhsCode = _prmWrhsCode;
        }
    }
}
