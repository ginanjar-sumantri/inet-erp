using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsProductTypeDt
    {
        public MsProductTypeDt(string _prmProdTypeCode, byte _prmWrhsType, string _prmAccInvent, string _prmAccSales, 
            string _prmAccTransitSJ, string _prmAccWIP, string _prmAccTransitWrhs, string _prmAccCOGS,
            string _prmAccSRetur, string _prmAccPRetur)
        {
            this.ProductTypeCode = _prmProdTypeCode;
            this.WrhsType = _prmWrhsType;
            this.AccInvent = _prmAccInvent;
            this.AccSales = _prmAccSales;
            this.AccTransitSJ = _prmAccTransitSJ;
            this.AccWIP = _prmAccWIP;
            this.AccTransitWrhs = _prmAccTransitWrhs;
            this.AccCOGS = _prmAccCOGS;
            this.AccSRetur = _prmAccSRetur;
            this.AccPRetur = _prmAccPRetur;
        }

        public MsProductTypeDt(string _prmProdTypeCode, byte _prmWrhsType, string _prmAccInvent, string _prmAccSales,
            string _prmAccTransitSJ, string _prmAccWIP, string _prmAccTransitWrhs, string _prmAccCOGS,
            string _prmAccSRetur, string _prmAccPRetur, String _prmAccTransitReject)
        {
            this.ProductTypeCode = _prmProdTypeCode;
            this.WrhsType = _prmWrhsType;
            this.AccInvent = _prmAccInvent;
            this.AccSales = _prmAccSales;
            this.AccTransitSJ = _prmAccTransitSJ;
            this.AccWIP = _prmAccWIP;
            this.AccTransitWrhs = _prmAccTransitWrhs;
            this.AccCOGS = _prmAccCOGS;
            this.AccSRetur = _prmAccSRetur;
            this.AccPRetur = _prmAccPRetur;
            this.AccTransitReject = _prmAccTransitReject;
        }
    }
}
