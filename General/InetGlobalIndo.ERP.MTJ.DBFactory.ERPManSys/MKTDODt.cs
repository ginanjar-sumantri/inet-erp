using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MKTDODt
    {
        public MKTDODt(string _prmTransNmbr, string _prmProductCode,int _prmItemID, decimal _prmQty, string _prmUnit, string _prmRemark, char? _prmDoneClosing, decimal _prmQtyClose)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ItemID = _prmItemID;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.Remark = _prmRemark;
            this.DoneClosing = _prmDoneClosing;
            this.QtyClose = _prmQtyClose;
        }

        public MKTDODt(string _prmTransNmbr, string _prmProductCode,int _prmItemID, decimal _prmQty, string _prmUnit, string _prmRemark, 
            char? _prmDoneClosing, decimal _prmQtyClose, decimal _prmQtySJ)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ItemID = _prmItemID;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.Remark = _prmRemark;
            this.DoneClosing = _prmDoneClosing;
            this.QtyClose = _prmQtyClose;
            this.QtySJ = _prmQtySJ;
        }
    }
}
