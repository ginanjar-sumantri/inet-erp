using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_ProductSalesPrice
    {
        public Master_ProductSalesPrice(string _prmProductCode, string _prmCurrCode, string _prmUnitCode, decimal _prmSalesPrice)
        {
            this.ProductCode = _prmProductCode;
            this.CurrCode = _prmCurrCode;
            this.UnitCode = _prmUnitCode;
            this.SalesPrice = _prmSalesPrice;
        }

        public Master_ProductSalesPrice(string _prmUnitCode, decimal _prmSalesPrice)
        {
            this.UnitCode = _prmUnitCode;
            this.SalesPrice = _prmSalesPrice;
        }

        public Master_ProductSalesPrice(string _prmProductCode, string _prmCurrCode, string _prmUnitCode, decimal _prmSalesPrice, string _prmInsertBy, DateTime _prmInsertDate, string _prmEditBy, DateTime _prmEditDate)
        {
            this.ProductCode = _prmProductCode;
            this.CurrCode = _prmCurrCode;
            this.UnitCode = _prmUnitCode;
            this.SalesPrice = _prmSalesPrice;
            this.CreatedBy = _prmInsertBy;
            this.CreatedDate = _prmInsertDate;
            this.ModifiedBy = _prmEditBy;
            this.ModifiedDate = _prmEditDate;
        }
    }
}
