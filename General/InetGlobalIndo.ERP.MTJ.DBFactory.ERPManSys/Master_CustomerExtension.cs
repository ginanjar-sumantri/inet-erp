using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_CustomerExtension
    {
        public Master_CustomerExtension(string _prmCustCode, string _prmEmpNumb)
        {
            this.CustCode = _prmCustCode;
            this.EmpNumb = _prmEmpNumb;
        }
    }
}
