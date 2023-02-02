using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTPlanShipArrival_ProductLogistic
    {
        private string _productName = "";
        private string _unitName = "";

        public PORTPlanShipArrival_ProductLogistic(Guid _prmHdShipCode, Guid _prmDtShipCode, string _prmProductCode, string _prmProductName,
            string _prmUnitName, Decimal? _prmVolume, DateTime? _prmDateFill)
        {
            this.PlanShipArrivalCode = _prmHdShipCode;
            this.PlanShipArrivalItemLogisticCode = _prmDtShipCode;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.UnitName = _prmUnitName; ;
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
