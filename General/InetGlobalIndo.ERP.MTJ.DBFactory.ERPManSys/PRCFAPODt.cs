using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PRCFAPODt
    {
        private string _productName = "";
        private string _unitName = "";

        public PRCFAPODt(string _prmTransNmbr, string _prmFAName, decimal _prmQty, string _prmUnit, string _prmUnitName, decimal _prmPriceForex, decimal _prmAmountForex, char _prmDoneClosing, decimal? _prmQtyClose, string _prmCreatedBy)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FANAme = _prmFAName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.UnitName = _prmUnitName;
            this.PriceForex = _prmPriceForex;
            this.AmountForex = _prmAmountForex;
            this.DoneClosing = _prmDoneClosing;
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
