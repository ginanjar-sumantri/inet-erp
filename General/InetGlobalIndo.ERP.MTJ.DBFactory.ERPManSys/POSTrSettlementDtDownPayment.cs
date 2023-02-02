using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSTrSettlementDtDownPayment
    {
        public POSTrSettlementDtDownPayment(String _prmTransNmbr, String _prmTransType, String _prmReffNmbr,
            String _prmProductCode, String _prmWrhsCode, String _prmLocationCode, Decimal _prmQty,
            String _prmUnit, Decimal _prmTotalForex, Char _prmFgStock, String _prmAccInvent, Char _prmFgInvent,
            String _prmAccSales, Char _prmFgSales, String _prmAccCOGS, Char _prmFgCOGS)
        {
            this.TransNmbr = _prmTransNmbr;
            this.TransType = _prmTransType;
            this.ReffNmbr = _prmReffNmbr;
            this.ProductCode = _prmProductCode;
            this.WrhsCode = _prmWrhsCode;
            this.LocationCode = _prmLocationCode;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.TotalForex = _prmTotalForex;
            this.FgStock = _prmFgStock;
            this.AccInvent = _prmAccInvent;
            this.FgInvent = _prmFgInvent;
            this.AccSales = _prmAccSales;
            this.FgSales = _prmFgSales;
            this.AccCOGS = _prmAccCOGS;
            this.FgCOGS = _prmFgCOGS;
        }
    }
}