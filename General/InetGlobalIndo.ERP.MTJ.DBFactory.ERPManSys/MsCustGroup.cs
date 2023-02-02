using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsCustGroup
    {
        public MsCustGroup(string _prmCustGroupCode, string _prmCustGroupName, string _prmCustGroupType, char _prmFgPKP, char _prmFgPPh, decimal _prmPPH)
        {
            this.CustGroupCode = _prmCustGroupCode;
            this.CustGroupName = _prmCustGroupName;
            this.CustGroupType = _prmCustGroupType;
            this.FgPKP = _prmFgPKP;
            this.FgPPh = _prmFgPPh;
            this.PPH = _prmPPH;
        }

        public MsCustGroup(string _prmCustGroupCode, string _prmCustGroupName)
        {
            this.CustGroupCode = _prmCustGroupCode;
            this.CustGroupName = _prmCustGroupName;
        }
    }
}
