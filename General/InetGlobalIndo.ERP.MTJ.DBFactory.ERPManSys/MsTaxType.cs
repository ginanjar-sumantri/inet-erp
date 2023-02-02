using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsTaxType
    {
        public MsTaxType(string _prmTaxTypeCode, string _prmTaxTypeName)
        {
            this.TaxTypeCode = _prmTaxTypeCode;
            this.TaxTypeName = _prmTaxTypeName;
        }
    }
}
