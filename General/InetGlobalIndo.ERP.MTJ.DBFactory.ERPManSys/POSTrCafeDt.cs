using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSTrCafeDt
    {
        public POSTrCafeDt(String _prmProductCode, Decimal _prmQty, Decimal _prmAmountForex, Decimal _prmDiscForex, Decimal _prmLineTotalForex, String _prmRemark)
        {
            this.ProductCode = _prmProductCode;
            this.Qty = _prmQty;
            this.AmountForex = _prmAmountForex;
            this.DiscForex = _prmDiscForex;
            this.LineTotalForex = _prmLineTotalForex;
            this.Remark = _prmRemark;
        }        
    }
}
