using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSMsCreditCardType
    {
        public POSMsCreditCardType(String _prmCreditCardTypeCode, String _prmCreditCardTypeName)
        {
            this.CreditCardTypeCode = _prmCreditCardTypeCode;
            this.CreditCardTypeName = _prmCreditCardTypeName;
        }
    }
}
