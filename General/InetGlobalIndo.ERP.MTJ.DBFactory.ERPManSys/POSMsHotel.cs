using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSMsHotel
    {
        String _supplierName = "";
        public POSMsHotel(String _prmSupplierCode, String _prmSupplierName)
        {
            this.SuppCode= _prmSupplierCode;
            this.SupplierName = _prmSupplierName;
        }
        public POSMsHotel(String _prmHotelCode, String _prmHotelName, String _prmSuppCode)
        {
            this.HotelCode = _prmHotelCode;
            this.HotelName = _prmHotelName;
            this.SuppCode = _prmSuppCode;
        }

        public String SupplierName
        {
            get
            {
                return this._supplierName;
            }
            set
            {
                this._supplierName = value;
            }
        }
    }
}
