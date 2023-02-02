using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCRejectInDt
    {
        public STCRejectInDt(string _prmTransNmbr, string _prmProductCode, string _prmLocationCode, decimal _prmQty, string _prmUnit, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.LocationCode = _prmLocationCode;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.Remark = _prmRemark;
        }

        public STCRejectInDt(string _prmTransNmbr, string _prmProductCode, string _prmLocationCode, decimal _prmQty, string _prmUnit, string _prmRemark, decimal? _prmPriceCost, decimal? _prmTotalCost)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.LocationCode = _prmLocationCode;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.Remark = _prmRemark;
            this.PriceCost = _prmPriceCost;
            this.TotalCost = _prmTotalCost;
        }
    }
}