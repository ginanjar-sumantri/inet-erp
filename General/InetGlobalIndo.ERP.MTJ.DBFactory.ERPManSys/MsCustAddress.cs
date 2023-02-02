using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsCustAddress
    {
        private string _CountryName = "";

        public MsCustAddress(string _prmCustCode, string _prmDeliveryCode, string _prmDeliveryName, string _prmAddress1, string _prmAddress2, string _prmContry, string _prmZipCode)
        {
            this.CustCode = _prmCustCode;
            this.DeliveryCode = _prmDeliveryCode;
            this.DeliveryName = _prmDeliveryName;
            this.DeliveryAddr1 = _prmAddress1;
            this.DeliveryAddr2 = _prmAddress2;
            this.CountryName = _prmContry;
            this.ZipCode = _prmZipCode;
        }

        public MsCustAddress(string _prmCustCode, string _prmDeliveryCode, string _prmDeliveryName)
        {
            this.CustCode = _prmCustCode;
            this.DeliveryCode = _prmDeliveryCode;
            this.DeliveryName = _prmDeliveryName;
        }

        public string CountryName
        {
            get
            {
                return this._CountryName;
            }
            set
            {
                this._CountryName = value;
            }
        }
    }
}
