using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCSJDtReference
    {
        private string _productCode = "";
        private Int32 _qtyOrder = 0;
        private String _unitOrder = "";

        public STCSJDtReference(String _prmTransNmbr, String _prmReferenceNmbr, String _prmProductCode, Int32 _prmQtyOrder, String _prmUnitOrder)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ReferenceNmbr = _prmReferenceNmbr;
            this.ProductCode = _prmProductCode;
            this.QtyOrder = _prmQtyOrder;
            this.UnitOrder = _prmUnitOrder;
        }

        public string ProductCode
        {
            get
            {
                return this._productCode;
            }
            set
            {
                this._productCode = value;
            }
        }

        public Int32 QtyOrder
        {
            get
            {
                return this._qtyOrder;
            }
            set
            {
                this._qtyOrder = value;
            }
        }

        public String UnitOrder
        {
            get
            {
                return this._unitOrder;
            }
            set
            {
                this._unitOrder = value;
            }
        }

    }
}
