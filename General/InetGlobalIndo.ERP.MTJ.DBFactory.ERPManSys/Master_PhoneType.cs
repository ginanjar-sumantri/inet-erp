using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_PhoneType
    {
        public string _phoneSeriesDesc = "";

        public Master_PhoneType(Guid _prmPhoneTypeCode, string _prmPhoneTypeID, string _prmPhoneTypeDesc, Guid _prmPhoneSeriesCode, string _prmPhoneSeriesDesc)
        {
            this.PhoneTypeCode = _prmPhoneTypeCode;
            this.PhoneTypeID = _prmPhoneTypeID;
            this.PhoneTypeDesc = _prmPhoneTypeDesc;
            this.PhoneSeriesCode = _prmPhoneSeriesCode;
            this.PhoneSeriesDesc = _prmPhoneSeriesDesc;
        }

        public Master_PhoneType(Guid _prmPhoneTypeCode, string _prmPhoneTypeDesc)
        {
            this.PhoneTypeCode = _prmPhoneTypeCode;
            this.PhoneTypeDesc = _prmPhoneTypeDesc;
        }

        public string PhoneSeriesDesc
        {
            get
            {
                return this._phoneSeriesDesc;
            }
            set
            {
                this._phoneSeriesDesc = value;
            }
        }
    }
}
