using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsCurrRate
    {
        public MsCurrRate(DateTime _prmCurrDate, string _prmCurrCode, decimal _prmCurrRate)
        {
            this.CurrDate = _prmCurrDate;
            this.CurrCode = _prmCurrCode;
            this.CurrRate = _prmCurrRate;
        }

        public MsCurrRate(decimal _prmCurrRate)
        {
            this.CurrRate = _prmCurrRate;
        }
    }
}
