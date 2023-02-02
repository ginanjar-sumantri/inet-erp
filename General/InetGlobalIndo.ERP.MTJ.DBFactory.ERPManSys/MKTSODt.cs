using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MKTSODt
    {
        private string _productName = "";

        public MKTSODt(string _prmTransNmbr, int _prmRevisi, string _prmProductCode, string _prmProductName, decimal _prmQtyOrder, string _prmUnitOrder,
            decimal? _prmQty, string _prmUnit, char? _prmDoneClosing, decimal? _prmQtyClose, Decimal? _prmPrice, Decimal? _prmAmount, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.Revisi = _prmRevisi;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.QtyOrder = _prmQtyOrder;
            this.UnitOrder = _prmUnitOrder;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.DoneClosing = _prmDoneClosing;
            this.QtyClose = _prmQtyClose;
            this.Price = _prmPrice;
            this.Amount = _prmAmount;
            this.Remark = _prmRemark;
        }

        public MKTSODt(string _prmTransNmbr, int _prmRevisi, string _prmProductCode, decimal _prmQtyOrder, string _prmUnitOrder,
            Decimal? _prmQty, string _prmUnit, Decimal? _prmPrice, Decimal? _prmAmount, String _prmRemark, char? _prmDoneClosing, Decimal _prmQtyDO)
        {
            this.TransNmbr = _prmTransNmbr;
            this.Revisi = _prmRevisi;
            this.ProductCode = _prmProductCode;
            this.QtyOrder = _prmQtyOrder;
            this.UnitOrder = _prmUnitOrder;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.Price = _prmPrice;
            this.Amount = _prmAmount;
            this.Remark = _prmRemark;
            this.DoneClosing = _prmDoneClosing;
            this.QtyDO = _prmQtyDO;
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
