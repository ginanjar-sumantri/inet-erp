using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsFAGroupSub
    {
        public MsFAGroupSub(string _prmFAGroupSubCode, string _prmFAGroupSubName, string _prmFaGroupCode, char _prmMoving, char _prmProcess)
        {
            this.FASubGrpCode = _prmFAGroupSubCode;
            this.FASubGrpName = _prmFAGroupSubName;
            this.FAGroup = _prmFaGroupCode;
            this.FgMoving = _prmMoving;
            this.FgProcess = _prmProcess;
        }

        public MsFAGroupSub(string _prmFAGroupSubCode, string _prmFAGroupSubName, string _prmFaGroupCode, char _prmMoving, char _prmProcess, string _prmCodeCounter, int _prmLastCounterNo)
        {
            this.FASubGrpCode = _prmFAGroupSubCode;
            this.FASubGrpName = _prmFAGroupSubName;
            this.FAGroup = _prmFaGroupCode;
            this.FgMoving = _prmMoving;
            this.FgProcess = _prmProcess;
            this.CodeCounter = _prmCodeCounter;
            this.LastCounterNo = _prmLastCounterNo;
        }

        public MsFAGroupSub(string _prmFAGroupSubCode, string _prmFAGroupSubName)
        {
            this.FASubGrpCode = _prmFAGroupSubCode;
            this.FASubGrpName = _prmFAGroupSubName;
        }
    }
}
