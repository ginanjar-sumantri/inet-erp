using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCServiceOutDt
    {
        public STCServiceOutDt(string _prmTransNmbr, string _prmImeiNo, string _prmProductCode, string _prmLocationCode,
            decimal _prmQty, string _prmUnit, DateTime _prmEstReturnDate, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ImeiNo = _prmImeiNo;
            this.ProductCode = _prmProductCode;
            this.LocationCode = _prmLocationCode;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.Remark = _prmRemark;
        }

        public STCServiceOutDt(string _prmTransNmbr, string _prmImeiNo, string _prmProductCode, string _prmProductName,
            string _prmLocationCode, string _prmLocationName, decimal _prmQty, string _prmUnit, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ImeiNo = _prmImeiNo;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.LocationCode = _prmLocationCode;
            this.LocationName = _prmLocationName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.Remark = _prmRemark;
        }

        public String ProductName { get; set; }
        public String LocationName { get; set; }
    }
}
