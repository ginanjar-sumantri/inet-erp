using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsProduct_PhoneType
    {
        private string _phoneTypeName = "";

        public MsProduct_PhoneType(String _prmProductCode, Guid _prmPhoneTypeCode, String _prmPhoneTypeName)
        {
            this.ProductCode = _prmProductCode;
            this.PhoneTypeCode = _prmPhoneTypeCode;
            this.PhoneTypeName = _prmPhoneTypeName;
        }

        public string PhoneTypeName
        {
            get
            {
                return this._phoneTypeName;
            }
            set
            {
                this._phoneTypeName = value;
            }
        }

    }
}
