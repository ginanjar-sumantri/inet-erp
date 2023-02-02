using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINMsCashFlowGroup
    {
        public FINMsCashFlowGroup(string _prmCashflowType, string _prmCashFlowGroupCode, string _prmCashFlowGroupName)
        {
            this.CashFlowType = _prmCashflowType;
            this.CashFlowGroupCode = _prmCashFlowGroupCode;
            this.CashFlowGroupName = _prmCashFlowGroupName;
        }

        public FINMsCashFlowGroup(string _prmCashFlowGroupCode, string _prmCashFlowGroupName)
        {
            this.CashFlowGroupCode = _prmCashFlowGroupCode;
            this.CashFlowGroupName = _prmCashFlowGroupName;
        }
    }
}
