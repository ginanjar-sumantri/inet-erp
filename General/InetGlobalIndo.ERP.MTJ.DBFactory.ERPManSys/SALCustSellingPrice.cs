using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class SALCustSellingPrice
    {
        public SALCustSellingPrice(String _prmCustCode, String _prmProductCode, String _prmTransNmbr, DateTime _prmTransDate, String _prmUnitCode,
            String _prmCurrCode, Decimal _prmAmoutForex)
        {
            this.CustCode = _prmCustCode;
            this.ProductCode = _prmProductCode;
            this.TransNmbr = _prmTransNmbr;
            this.TransDate = _prmTransDate;
            this.UnitCode = _prmUnitCode;
            this.CurrCode = _prmCurrCode;
            this.AmountForex = _prmAmoutForex;
        }
    }
}
