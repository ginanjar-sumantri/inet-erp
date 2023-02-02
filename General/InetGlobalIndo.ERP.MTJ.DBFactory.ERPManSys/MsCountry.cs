using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsCountry
    {
        public MsCountry(string _prmCountryCode, string _prmCountryName)
        {
            this.CountryCode = _prmCountryCode;
            this.CountryName = _prmCountryName;
        }
    }
}
