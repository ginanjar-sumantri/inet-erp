using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PRCRequestDt
    {
        string _productName = "";

        public PRCRequestDt(string _prmTransNmbr, string _prmProductCode, string _prmProductName, string _prmSpecification,
            decimal _prmQty, string _prmUnit, DateTime _prmRequireDate, string _prmRemark, char _prmDoneClosing,
            decimal? _prmQtyPO, decimal? _prmQtyClose, decimal _prmEstPrice, string _prmCreateBy)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.Specification = _prmSpecification;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.RequireDate = _prmRequireDate;
            this.Remark = _prmRemark;
            this.DoneClosing = _prmDoneClosing;
            this.QtyPO = _prmQtyPO;
            this.QtyClose = _prmQtyClose;
            this.EstPrice = _prmEstPrice;
            this.CreatedBy = _prmCreateBy;
        }

        public PRCRequestDt(String _prmTransNmbr, String _prmProductCode, String _prmSpecification,
           Decimal _prmQty, String _prmUnit, DateTime _prmRequireDate, String _prmRemark, Char _prmDoneClosing,
           Decimal _prmEstPrice, String _prmCreateBy, DateTime _prmCreateDate, String _prmEditBy, DateTime _prmEditDate)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.Specification = _prmSpecification;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.RequireDate = _prmRequireDate;
            this.Remark = _prmRemark;
            this.DoneClosing = _prmDoneClosing;
            this.EstPrice = _prmEstPrice;
            this.CreatedBy = _prmCreateBy;
            this.CreatedDate = _prmCreateDate;
            this.EditBy = _prmEditBy;
            this.EditDate = _prmEditDate;
        }

        public string ProductName
        {
            get
            {
                return this._productName;
            }
            set
            {
                this._productName = value;
            }
        }
    }
}
