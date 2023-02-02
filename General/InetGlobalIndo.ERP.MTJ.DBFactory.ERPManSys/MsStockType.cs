using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{

    public partial class MsStockType
    {
        public MsStockType(string _prmStockTypeCode, string _prmStockTypeName)
        {
            this.StockTypeCode = _prmStockTypeCode;
            this.StockTypeName = _prmStockTypeName;
        }

        public MsStockType(string _prmStockTypeCode, string _prmStockTypeName, string _prmAccount)
        {
            this.StockTypeCode = _prmStockTypeCode;
            this.StockTypeName = _prmStockTypeName;
            this.Account = _prmAccount;
        }

    }


}
