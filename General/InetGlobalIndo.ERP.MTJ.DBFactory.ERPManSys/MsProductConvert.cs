using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsProductConvert
    {
        string _unitConvertName = "";
        string _unitCodeName = "";

        public MsProductConvert(string _prmProductCode, string _prmUnitCode, string _prmUnitCodeName, string _prmUnitConvert, string _prmUnitConvertName, decimal _prmRate)
        {
            this.ProductCode = _prmProductCode;
            this.UnitCode = _prmUnitCode;
            this.UnitCodeName = _prmUnitCodeName;
            this.UnitConvert = _prmUnitConvert;
            this.UnitConvertName = _prmUnitConvertName;
            this.Rate = _prmRate;
        }

        public MsProductConvert(string _prmUnitCode,string _prmUnitConvert)
        {
            this.UnitCode = _prmUnitCode;
            this.UnitConvert = _prmUnitConvert;
        }

        public string UnitCodeName
        {
            get
            {
                return this._unitCodeName;
            }
            set
            {
                this._unitCodeName = value;
            }
        }

        public string UnitConvertName
        {
            get
            {
                return this._unitConvertName;
            }
            set
            {
                this._unitConvertName = value;
            }
        }
    }
}
