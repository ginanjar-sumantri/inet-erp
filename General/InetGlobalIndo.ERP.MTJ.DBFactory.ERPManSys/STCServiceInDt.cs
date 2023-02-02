using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCServiceInDt
    {
        public STCServiceInDt(string _prmTransNmbr, string _prmImeiNo, string _prmProductCode,
            string _prmLocationCode, decimal _prmQty, string _prmUnit, DateTime _prmEstReturnDate,
            bool _prmFgGaransi, bool? _prmFgSegelOK, string _prmRemark, decimal? _prmQtyOut)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ImeiNo = _prmImeiNo;
            this.ProductCode = _prmProductCode;
            this.LocationCode = _prmLocationCode;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.EstReturnDate = _prmEstReturnDate;
            this.FgGaransi = _prmFgGaransi;
            this.FgSegelOK = _prmFgSegelOK;
            this.Remark = _prmRemark;
            this.QtyOut = _prmQtyOut;
        }

        public STCServiceInDt(string _prmTransNmbr, string _prmImeiNo, string _prmProductCode, string _prmProductName,
            string _prmLocationCode, string _prmLocationName, decimal _prmQty, string _prmUnit, DateTime _prmEstReturnDate,
            bool _prmFgGaransi, bool? _prmFgSegelOK, string _prmRemark, decimal? _prmQtyOut)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ImeiNo = _prmImeiNo;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.LocationCode = _prmLocationCode;
            this.LocationName = _prmLocationName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.EstReturnDate = _prmEstReturnDate;
            this.FgGaransi = _prmFgGaransi;
            this.FgSegelOK = _prmFgSegelOK;
            this.Remark = _prmRemark;
            this.QtyOut = _prmQtyOut;
        }

        public String ProductName { get; set; }
        public String LocationName { get; set; }
    }
}
