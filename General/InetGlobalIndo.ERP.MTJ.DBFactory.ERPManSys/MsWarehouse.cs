using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsWarehouse
    {
        public MsWarehouse(string _prmWrhsCode, string _prmWrhsName, string _prmWrhsGroup, string _prmWrhsArea, char _prmFgActive)
        {
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
            this.WrhsGroup = _prmWrhsGroup;
            this.WrhsArea = _prmWrhsArea;
            this.FgActive = _prmFgActive;
        }

        public MsWarehouse(string _prmWrhsCode, string _prmWrhsName)
        {
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
        }
    }
}
