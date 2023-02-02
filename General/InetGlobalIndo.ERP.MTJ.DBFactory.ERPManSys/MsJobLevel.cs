using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsJobLevel
    {
        public MsJobLevel(string _prmJobLevelCode, string _prmJobLevelName)
        {
            this.JobLvlCode = _prmJobLevelCode;
            this.JobLvlName = _prmJobLevelName;
        }
    }
}
