using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCSJDt
    {
        private string _productName = "";
        private string _locationName = "";
        private string _unitName = "";
        private string _soNo = "";

        public STCSJDt(string _prmTransNmbr, string _prmProductCode, string _prmProductName, string _prmLocationCode, string _prmLocationName, string _prmDONo, decimal _prmQty, string _prmUnit, string _prmUnitName)
        {
            TransNmbr = _prmTransNmbr;
            DONo = _prmDONo;
            ProductCode = _prmProductCode;
            ProductName = _prmProductName;
            LocationCode = _prmLocationCode;
            LocationName = _prmLocationName;
            Qty = _prmQty;
            Unit = _prmUnit;
            UnitName = _prmUnitName;
        }

        public STCSJDt(string _prmTransNmbr, string _prmProductCode, string _prmProductName, String _prmWrhsCode, String _prmWrhsSubLed, string _prmLocationCode, string _prmLocationName, string _prmDONo, decimal _prmQty, string _prmUnit, string _prmUnitName)
        {
            TransNmbr = _prmTransNmbr;
            DONo = _prmDONo;
            ProductCode = _prmProductCode;
            ProductName = _prmProductName;
            WrhsCode = _prmWrhsCode;
            WrhsSubLed = _prmWrhsSubLed;
            LocationCode = _prmLocationCode;
            LocationName = _prmLocationName;
            Qty = _prmQty;
            Unit = _prmUnit;
            UnitName = _prmUnitName;
        }

        public STCSJDt(string _prmTransNmbr, string _prmProductCode, string _prmProductName, Int32 _prmItemID, String _prmWrhsCode, String _prmWrhsSubLed, string _prmLocationCode, string _prmLocationName, string _prmDONo, decimal _prmQty, string _prmUnit, string _prmUnitName)
        {
            TransNmbr = _prmTransNmbr;
            DONo = _prmDONo;
            ProductCode = _prmProductCode;
            ProductName = _prmProductName;
            ItemID = _prmItemID;
            WrhsCode = _prmWrhsCode;
            WrhsSubLed = _prmWrhsSubLed;
            LocationCode = _prmLocationCode;
            LocationName = _prmLocationName;
            Qty = _prmQty;
            Unit = _prmUnit;
            UnitName = _prmUnitName;
        }

        public STCSJDt(string _prmTransNmbr, string _prmProductCode, string _prmLocationCode, string _prmDONo, decimal _prmQty, string _prmUnit, String _prmRemark, Decimal _prmQtyLoss, Decimal? _prmQtyRetur, Decimal _prmQtyReceive)
        {
            TransNmbr = _prmTransNmbr;
            DONo = _prmDONo;
            ProductCode = _prmProductCode;
            LocationCode = _prmLocationCode;
            Qty = _prmQty;
            Unit = _prmUnit;
            Remark = _prmRemark;
            QtyLoss = _prmQtyLoss;
            QtyReceive = _prmQtyReceive;
            QtyRetur = _prmQtyRetur;
        }

        public STCSJDt(string _prmTransNmbr, string _prmProductCode, string _prmLocationCode, String _prmWrhsCode, char _prmWrhsFgSubLed, String _prmWrhsSubLed, string _prmDONo, decimal _prmQty, string _prmUnit, String _prmRemark, Decimal _prmQtyLoss, Decimal? _prmQtyRetur, Decimal _prmQtyReceive)
        {
            TransNmbr = _prmTransNmbr;
            DONo = _prmDONo;
            ProductCode = _prmProductCode;
            WrhsCode = _prmWrhsCode;
            WrhsFgSubLed = _prmWrhsFgSubLed;
            WrhsSubLed = _prmWrhsSubLed;
            LocationCode = _prmLocationCode;
            Qty = _prmQty;
            Unit = _prmUnit;
            Remark = _prmRemark;
            QtyLoss = _prmQtyLoss;
            QtyReceive = _prmQtyReceive;
            QtyRetur = _prmQtyRetur;
        }

        public STCSJDt(string _prmSONo, decimal _prmQty, string _prmUnit)
        {
            SONo = _prmSONo;
            Qty = _prmQty;
            Unit = _prmUnit;
        }

        public STCSJDt(String _prmTransNmbr, String _prmReferenceNmbr, String _prmProductCode,String _prmLocationCode ,Int32 _prmQtyOrder, String _prmUnitOrder)
        {
            TransNmbr = _prmTransNmbr;
            DONo = _prmReferenceNmbr;
            ProductCode = _prmProductCode;
            LocationCode = _prmLocationCode;
            Qty = _prmQtyOrder;
            Unit = _prmUnitOrder;
        }

        public STCSJDt(String _prmTransNmbr, String _prmReferenceNmbr, String _prmProductCode, String _prmWrhsCode, Char _prmWrhsFgSubLed, String _prmWrhsSubLed, String _prmLocationCode, Int32 _prmQtyOrder, String _prmUnitOrder)
        {
            TransNmbr = _prmTransNmbr;
            DONo = _prmReferenceNmbr;
            ProductCode = _prmProductCode;
            WrhsCode = _prmWrhsCode;
            WrhsFgSubLed = _prmWrhsFgSubLed;
            WrhsSubLed = _prmWrhsSubLed;
            LocationCode = _prmLocationCode;
            Qty = _prmQtyOrder;
            Unit = _prmUnitOrder;
        }

        public STCSJDt(String _prmTransNmbr, String _prmReferenceNmbr, String _prmProductCode, Int32 _prmItemID, String _prmWrhsCode, Char _prmWrhsFgSubLed, String _prmWrhsSubLed, String _prmLocationCode, Int32 _prmQtyOrder, String _prmUnitOrder)
        {
            TransNmbr = _prmTransNmbr;
            DONo = _prmReferenceNmbr;
            ProductCode = _prmProductCode;
            ItemID = _prmItemID;
            WrhsCode = _prmWrhsCode;
            WrhsFgSubLed = _prmWrhsFgSubLed;
            WrhsSubLed = _prmWrhsSubLed;
            LocationCode = _prmLocationCode;
            Qty = _prmQtyOrder;
            Unit = _prmUnitOrder;
        }

        public string SONo
        {
            get
            {
                return this._soNo;
            }
            set
            {
                this._soNo = value;
            }
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

        public string LocationName
        {
            get
            {
                return this._locationName;
            }
            set
            {
                this._locationName = value;
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
