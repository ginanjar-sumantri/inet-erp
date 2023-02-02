using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class BILTrSalesConfirmationDt
    {
        private string _productName = "";

        public BILTrSalesConfirmationDt(String _prmTransNmbr, String _prmProductCode, String _prmProductName, String _prmProductSpecification,
            String _prmCurrCode, Decimal? _prmAmountForex)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.ProductSpecification = _prmProductSpecification;
            this.CurrCode = _prmCurrCode;
            this.AmountForex = _prmAmountForex;
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
