using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsProduct_SerialNumber
    {
        public MsProduct_SerialNumber(string _prmSerialNumber, string _prmPIN, string _prmExpirationDate, Boolean _prmIsSold, String _prmProductCode, String _prmTransNmbr, String _prmManufactureID, Byte _prmCounter)
        {
            this.SerialNumber = _prmSerialNumber;
            this.PIN = _prmPIN;
            this.ExpirationDate = _prmExpirationDate;
            //this.Country = _prmCountry;
            //this.BulkMode = _prmBulkMode;
            this.IsSold = _prmIsSold;
            this.ProductCode = _prmProductCode;
            this.TransNmbr = _prmTransNmbr;
            this.ManufactureID = _prmManufactureID;
            this.Counter = _prmCounter;
        }

        public MsProduct_SerialNumber(string _prmSerialNumber)
        {
            this.SerialNumber = _prmSerialNumber;
        }

        public MsProduct_SerialNumber(string _prmSerialNumber, string _prmPIN)
        {
            this.SerialNumber = _prmSerialNumber;
            this.PIN = _prmPIN;
        }       
    }
}
