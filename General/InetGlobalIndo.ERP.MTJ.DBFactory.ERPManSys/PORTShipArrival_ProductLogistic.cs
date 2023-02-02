using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTShipArrival_ProductLogistic
    {

         private string _productName = "";
        private string _unitName = "";

        public PORTShipArrival_ProductLogistic(Guid _prmDtShipCode, Guid _prmHdShipCode, string _prmProductCode, string _prmProductName,
            string _prmUnitCode, string _prmUnitName, Decimal? _prmVolume, DateTime? _prmDateFill)
        {
            this.DtShipArrivalItemLogCode = _prmDtShipCode;
            this.HdShipArrivalCode = _prmHdShipCode;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.UnitCode = _prmUnitCode;
            this.UnitName = _prmUnitName;
            this.Volume = _prmVolume;
            this.DateFill = _prmDateFill;
        }

        public string ProductName
        {
            get
            {
                return this._productName;
            }
            set
            {
                this._productName = value;
            }
        }

        public string UnitName
        {
            get
            {
                return this._unitName;
            }
            set
            {
                this._unitName = value;
            }
        }
    }
}
