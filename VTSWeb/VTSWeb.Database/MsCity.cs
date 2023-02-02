using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsCity
    {
        public MsCity(String _prmCityCode, String _prmCityName, String _prmRegional, String _prmCountry)
        {
            this.CityCode = _prmCityCode;
            this.CityName = _prmCityName;
            this.Regional = _prmRegional;
            this.Country  = _prmCountry;
        }
   
        public MsCity(String _prmCityCode, String _prmCityName)
        {
            this.CityCode = _prmCityCode;
            this.CityName = _prmCityName;
        }

        

    }
}
