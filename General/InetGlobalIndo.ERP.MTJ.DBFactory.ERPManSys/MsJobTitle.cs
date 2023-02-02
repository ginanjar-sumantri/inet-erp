using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsJobTitle
    {
        public MsJobTitle(string _prmJobTitleCode, string _prmJobTitleName)
        {
            this.JobTtlCode = _prmJobTitleCode;
            this.JobTtlName = _prmJobTitleName;
        }

        public MsJobTitle(string _prmJobTitleCode, string _prmJobTitleName, bool? _prmIsGetOT)
        {
            this.JobTtlCode = _prmJobTitleCode;
            this.JobTtlName = _prmJobTitleName;
            this.IsGetOT = _prmIsGetOT;
        }
    }
}
