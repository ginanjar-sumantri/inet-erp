using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Port_TallyDtProduct
    {
        private string _productName = "";

        public Port_TallyDtProduct(Guid _prmTallyDtProductCode, Guid _prmTallyDtCode, string _prmProductCode, string _prmProductName,
            decimal _prmQtyColly, string _prmUnitColly, decimal _prmQtyNetWeight, string _prmUnitNetWeight, string _prmRemark,
            string _prmAccInvent, char? _prmFgInvent, decimal _prmHPPperUnit, decimal _prmHPPperProduct)
        {
            this.TallyDtProductCode = _prmTallyDtProductCode;
            this.TallyDtCode = _prmTallyDtCode;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.QtyColly = _prmQtyColly;
            this.UnitColly = _prmUnitColly;
            this.QtyNetWeight = _prmQtyNetWeight;
            this.UnitNetWeight = _prmUnitNetWeight;
            this.Remark = _prmRemark;
            this.AccInvent = _prmAccInvent;
            this.FgInvent = _prmFgInvent;
            this.HPPperUnit = _prmHPPperUnit;
            this.HPPperProduct = _prmHPPperProduct;
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
