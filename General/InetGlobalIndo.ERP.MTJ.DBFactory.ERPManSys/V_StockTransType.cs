using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class V_StockTransType
    {
        public V_StockTransType(string _prmTransType, string _prmTransTypeName)
        {
            this.TransType = _prmTransType;
            this.TransTypeName = _prmTransTypeName;
        }

        ~V_StockTransType()
        {
        }
    }
}