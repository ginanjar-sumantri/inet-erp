using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PRCPODt
    {
        private string _productName = "";
        private string _unitName = "";

        public PRCPODt(string _prmTransNmbr, int _prmRevisi, string _prmProductCode, string _prmProductName, decimal _prmQty, string _prmUnit, string _prmUnitName, decimal _prmPriceForex, decimal _prmAmountForex, decimal _prmDiscForex, decimal _prmNettoForex, char _prmDoneClosing, decimal? _prmQtyClose, string _prmCreatedBy)
        {
            this.TransNmbr = _prmTransNmbr;
            this.Revisi = _prmRevisi;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.UnitName = _prmUnitName;
            this.PriceForex = _prmPriceForex;
            this.AmountForex = _prmAmountForex;
            this.DiscForex = _prmDiscForex;
            this.NettoForex = _prmNettoForex;
            this.DoneClosing = _prmDoneClosing;            
            this.QtyClose = _prmQtyClose;
            this.CreatedBy = _prmCreatedBy;
        }

        public PRCPODt(string _prmTransNmbr, int _prmRevisi, string _prmProductCode, string _prmProductName, decimal _prmQty, string _prmUnit, string _prmUnitName, decimal _prmPriceForex, decimal _prmAmountForex, decimal _prmDiscForex, decimal _prmNettoForex, char _prmDoneClosing, decimal? _prmQtyRR, decimal? _prmQtyClose, string _prmCreatedBy)
        {
            this.TransNmbr = _prmTransNmbr;
            this.Revisi = _prmRevisi;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.UnitName = _prmUnitName;
            this.PriceForex = _prmPriceForex;
            this.AmountForex = _prmAmountForex;
            this.DiscForex = _prmDiscForex;
            this.NettoForex = _prmNettoForex;
            this.DoneClosing = _prmDoneClosing;
            this.QtyRR = _prmQtyRR;
            this.QtyClose = _prmQtyClose;
            this.CreatedBy = _prmCreatedBy;
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

        public string UnitName
        {
            get
            {
                return this._unitName;
            }
            set
            {
                this._unitName = value;
            }
        }
    }
}
