using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_PhoneSery
    {
        public string _productName = "";

        public Master_PhoneSery(Guid _prmPhoneSeriesCode, string _prmPhoneSeriesID, string _prmPhoneSeriesDesc, string _prmProductCode, string _prmProductName)
        {
            this.PhoneSeriesCode = _prmPhoneSeriesCode;
            this.PhoneSeriesID = _prmPhoneSeriesID;
            this.PhoneSeriesDesc = _prmPhoneSeriesDesc;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
        }

        public Master_PhoneSery(Guid _prmPhoneSeriesCode, string _prmPhoneSeriesDesc)
        {
            this.PhoneSeriesCode = _prmPhoneSeriesCode;
            this.PhoneSeriesDesc = _prmPhoneSeriesDesc;
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
    }
}
