using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSMsShipping
    {
        public POSMsShipping(String _prmVendorCode, String _prmShippingTypeCode, String _prmProductShape, String _prmCityCode, String _prmVendorName, String _prmShippingTypeName, String _prmCityName, Decimal _prmPercentage, Decimal _prmPrice1, Decimal _prmPrice2, String _prmEstimationTime, String _prmUnitCode)
        {
            this.VendorCode = _prmVendorCode;
            this.ShippingTypeCode = _prmShippingTypeCode;
            this.ProductShape = _prmProductShape;
            this.CityCode = _prmCityCode;
            this.VendorName = _prmVendorName;
            this.ShippingTypeName = _prmShippingTypeName;
            this.CityName = _prmCityName;
            this.Percentage = _prmPercentage;
            this.Price1 = _prmPrice1;
            this.Price2 = _prmPrice2;
            this.EstimationTime = _prmEstimationTime;
            this.UnitCode = _prmUnitCode;
        }

        public String VendorCode { get; set; }
        public String ShippingTypeCode { get; set; }
        public String ProductShape { get; set; }
        public String CityCode { get; set; }
        public String VendorName { get; set; }
        public String ShippingTypeName { get; set; }
        public String CityName { get; set; }
        public Decimal Percentage { get; set; }
        public Decimal Price1 { get; set; }
        public Decimal Price2 { get; set; }
        public String EstimationTime { get; set; }
        public String UnitCode { get; set; }
    }
}
