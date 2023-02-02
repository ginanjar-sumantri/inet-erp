using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCMsProductFormula
    {
        string _prodAssemblyName = "";
        string _prodName = "";
        string _unitName = "";
        Int32 _no = 0;

        public STCMsProductFormula(string _prmProductCode, string _prmProductName, string _prmProductCodeAssembly, string _prmProductAssemblyName, Decimal _prmQty, string _prmUnit, String _prmUnitName)
        {
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.ProductCodeAssembly = _prmProductCodeAssembly;
            this.ProductAssemblyName = _prmProductAssemblyName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.UnitName = _prmUnitName;
        }

        public STCMsProductFormula(string _prmProductCode, string _prmProductName, string _prmProductCodeAssembly, string _prmProductAssemblyName, Decimal _prmQty, string _prmUnit, String _prmUnitName, Boolean _prmfgDisassembly, Boolean _prmfgMainProduct)
        {
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.ProductCodeAssembly = _prmProductCodeAssembly;
            this.ProductAssemblyName = _prmProductAssemblyName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.UnitName = _prmUnitName;
            this.fgDisassembly = _prmfgDisassembly;
            this.fgMainProduct = _prmfgMainProduct;
        }

        public STCMsProductFormula(string _prmProductCodeAssembly, string _prmProductAssemblyName)
        {
            this.ProductCodeAssembly = _prmProductCodeAssembly;
            this.ProductAssemblyName = _prmProductAssemblyName;
        }

        public STCMsProductFormula(Int32 _prmNo, String _prmProductCode, String _prmProductName, Decimal _prmQty, String _prmUnitName)
        {
            this.No = _prmNo;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.Qty = _prmQty;
            this.UnitName = _prmUnitName;
        }

        public STCMsProductFormula(String _prmProductCodeAssembly, String _prmProductCode, String _prmProductName, Decimal _prmQty, String _prmUnitName, Boolean _prmfgDisassembly, Boolean _prmfgMainProduct)
        {
            this.ProductCodeAssembly = _prmProductCodeAssembly;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.Qty = _prmQty;
            this.Unit = _prmUnitName;
            this.fgDisassembly = _prmfgDisassembly;
            this.fgMainProduct = _prmfgMainProduct;
        }

        public string ProductName
        {
            get
            {
                return this._prodName;
            }
            set
            {
                this._prodName = value;
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

        public string ProductAssemblyName
        {
            get
            {
                return this._prodAssemblyName;
            }
            set
            {
                this._prodAssemblyName = value;
            }
        }

        public Int32 No
        {
            get
            {
                return this._no;
            }
            set
            {
                this._no = value;
            }
        }
    }
}
