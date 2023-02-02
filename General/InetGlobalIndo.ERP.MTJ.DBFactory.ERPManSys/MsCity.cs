using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsCity
    {
        private string _regionalName = "";
        private string _countryName = "";

        public MsCity(string _prmCityCode, string _prmCityName, string _prmRegionalName, string _prmCountryName)
        {
            this.CityCode = _prmCityCode;
            this.CityName = _prmCityName;
            this.RegionalName = _prmRegionalName;
            this.CountryName = _prmCountryName;
        }

        public MsCity(string _prmCityCode, string _prmCityName)
        {
            this.CityCode = _prmCityCode;
            this.CityName = _prmCityName;
        }

        public string RegionalName
        {
            get
            {
                return this._regionalName;
            }
            set
            {
                this._regionalName = value;
            }
        }

        public string CountryName
        {
            get
            {
                return this._countryName;
            }
            set
            {
                this._countryName = value;
            }
        }

        
    }
}
