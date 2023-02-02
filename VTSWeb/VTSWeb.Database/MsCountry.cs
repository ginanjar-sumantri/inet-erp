using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsCountry
    {
        public MsCountry(String _prmCountryCode, String _prmCountryName)
        {
            this.CountryCode = _prmCountryCode;
            this.CountryName = _prmCountryName;
     
        }
    }
    public partial class MsCountry
    {
        public MsCountry( String _prmCountryName)
        {
            this.CountryName = _prmCountryName;

        }
    }
}