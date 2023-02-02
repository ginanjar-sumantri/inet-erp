using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSMsMenuServiceTypeDt
    {
        String _productSubGroupName = "";

        public POSMsMenuServiceTypeDt(String _prmMenuServiceTypeCode, String _prmProductSubGroup, String _prmProductSubGroupName)
        {
            this.MenuServiceTypeCode = _prmMenuServiceTypeCode;
            this.ProductSubGroup = _prmProductSubGroup;
            this.ProductSubGroupName = _prmProductSubGroupName;
        }

        public String ProductSubGroupName
        {
            get
            {
                return this._productSubGroupName;
            }

            set
            {
                this._productSubGroupName = value;
            }
        }
    }
}
