using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsProductType
    {
        public MsProductType(string _prmProdTypeCode, string _prmProdTypeName, string _prmProdCat)
        {
            this.ProductTypeCode = _prmProdTypeCode;
            this.ProductTypeName = _prmProdTypeName;
            this.ProductCategory = _prmProdCat;
        }

        public MsProductType(string _prmProdTypeCode, string _prmProdTypeName, string _prmProdCat, Boolean _prmIsUsingPG)
        {
            this.ProductTypeCode = _prmProdTypeCode;
            this.ProductTypeName = _prmProdTypeName;
            this.ProductCategory = _prmProdCat;
            this.IsUsingPG = _prmIsUsingPG;
        }

        public MsProductType(string _prmProdTypeCode, string _prmProdTypeName)
        {
            this.ProductTypeCode = _prmProdTypeCode;
            this.ProductTypeName = _prmProdTypeName;
        }
    }
}
