using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PRCTrDirectPurchaseDt
    {
        public string _productName = "";
        public string _locationName = "";
        public string _wrhsName = "";
        public string _unitName = "";

        public PRCTrDirectPurchaseDt(String _prmTransNmbr, String _prmProductCode, String _prmProductName,
            String _prmLocationCode, String _prmLocationName, String _prmWrhsCode, String _prmWrhsName,
            Decimal _prmQty, String _prmUnit, String _prmUnitName, Decimal _prmPrice, Decimal _prmAmount, 
            String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.LocationCode = _prmLocationCode;
            this.LocationName = _prmLocationName;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
            this.Qty = _prmQty;
            this.Unit = _prmUnit;
            this.UnitName = _prmUnitName;
            this.Price = _prmPrice;
            this.Amount = _prmAmount;
            this.Remark = _prmRemark;
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

        public string WrhsName
        {
            get
            {
                return this._wrhsName;
            }
            set
            {
                this._wrhsName = value;
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
