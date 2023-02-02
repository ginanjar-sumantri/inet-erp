using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTMsServiceDt
    {
        public PORTMsServiceDt(Guid _prmServiceCode, string _prmCurrCode, decimal _prmPrice, string _prmUnitCode)
        {
            this.ServiceCode = _prmServiceCode;
            this.CurrCode = _prmCurrCode;
            this.Price = _prmPrice;
            this.UnitCode = _prmUnitCode;
        }
    }
}
