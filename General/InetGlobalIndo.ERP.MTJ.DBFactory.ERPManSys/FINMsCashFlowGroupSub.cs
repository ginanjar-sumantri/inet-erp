using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINMsCashFlowGroupSub
    {
        public FINMsCashFlowGroupSub(string _prmCFtypeCode, string _prmCFGroupCode, string _prmCFGroupSubCode, string _prmCFGroupSubName, string _prmTypeCode, char _prmOperator)
        {
            this.CashFlowType = _prmCFtypeCode;
            this.CashFlowGroupCode = _prmCFGroupCode;
            this.CashFlowGroupSubCode = _prmCFGroupSubCode;
            this.CashFlowGroupSubName = _prmCFGroupSubName;
            this.TypeCode = _prmTypeCode;
            this.Operator = _prmOperator;
        }


    }
}
