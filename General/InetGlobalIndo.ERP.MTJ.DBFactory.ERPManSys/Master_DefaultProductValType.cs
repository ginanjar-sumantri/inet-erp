using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_DefaultProductValType
    {
        public Master_DefaultProductValType(Guid _productDefaultValNo, byte _prmDefaultValue)
        {
            this.ProductDefaultValNo = _productDefaultValNo;
            this.ProductValType = _prmDefaultValue;
        }

        public Master_DefaultProductValType(byte _prmDefaultValue)
        {
            this.ProductValType = _prmDefaultValue;
        }

    }
}
