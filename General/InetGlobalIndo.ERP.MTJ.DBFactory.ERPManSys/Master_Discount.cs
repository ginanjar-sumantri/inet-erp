using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_Discount
    {
        public Master_Discount(Guid _prmDiscountCode, String _prmDiscountID, String _prmDiscountName)
        {
            this.DiscountCode = _prmDiscountCode;
            this.DiscountID = _prmDiscountID;
            this.DiscountName = _prmDiscountName;
        }

        public Master_Discount(Guid _prmDiscountCode, String _prmDiscountName)
        {
            this.DiscountCode = _prmDiscountCode;
            this.DiscountName = _prmDiscountName;
        }
    }
}
