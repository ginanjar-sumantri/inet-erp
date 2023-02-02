using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class V_RRForSupplierRetur
    {
        public V_RRForSupplierRetur(string _prmSuppCode, string _prmPurchaseRetur)
        {
            this.SuppCode = _prmSuppCode;
            this.PurchaseRetur = _prmPurchaseRetur;
        }

        ~V_RRForSupplierRetur()
        {
        }
    }
}