using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_DiscountLevel
    {
        public Master_DiscountLevel(Guid _prmDiscountCode, Byte _prmLevel, Byte _prmDiscount)
        {
            this.DiscountCode = _prmDiscountCode;
            this.Level = _prmLevel;
            this.Discount = _prmDiscount;
        }
    }
}
