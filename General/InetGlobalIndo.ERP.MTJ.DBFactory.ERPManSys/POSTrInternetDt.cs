using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSTrInternetDt
    {
        public POSTrInternetDt(String _prmTransNmbr, int _prmItemNo, String _prmProductCode,
            Decimal _prmQty, String _prmUnit, Decimal _prmUnitPriceForex, Decimal _prmAmountForex,
            Decimal _prmDiscPercentage, Decimal _prmDiscForex, Decimal _prmLineTotalForex, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ItemNo = _prmItemNo;
            this.ProductCode = _prmProductCode;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.UnitPriceForex = _prmUnitPriceForex;
            this.AmountForex = _prmAmountForex;
            this.DiscPercentage = _prmDiscPercentage;
            this.DiscForex = _prmDiscForex;
            this.LineTotalForex = _prmLineTotalForex;
            this.Remark = _prmRemark;
        }

        public POSTrInternetDt(String _prmProductCode, Decimal _prmQty, Decimal _prmAmountForex, 
            Decimal _prmDiscForex, Decimal _prmLineTotalForex, String _prmRemark)
        {
            this.ProductCode = _prmProductCode;
            this.Qty = _prmQty;
            this.DiscForex = _prmDiscForex;
            this.AmountForex = _prmAmountForex;
            this.LineTotalForex = _prmLineTotalForex;
            this.Remark = _prmRemark;            
        }
    }
}
