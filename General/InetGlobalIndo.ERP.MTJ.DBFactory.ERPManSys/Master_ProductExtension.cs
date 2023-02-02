using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_ProductExtension
    {
        private string _productName = "";

        public Master_ProductExtension(string _prmProductCode, string _prmProductName, bool _prmIsPostponeAllowed)
        {
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.IsPostponeAllowed = _prmIsPostponeAllowed;
        }

        public Master_ProductExtension(string _prmProductCode, string _prmProductName, bool _prmIsPostponeAllowed, bool? _prmChangePrice, bool? _prmMRC, int? _contractMonth)
        {
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.IsPostponeAllowed = _prmIsPostponeAllowed;
            this.IsChangePriceAllowed = _prmChangePrice;
            this.IsMRC = _prmMRC;
            this.MinContractMonth = _contractMonth;
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
