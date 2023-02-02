using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsTermType
    {
        public HRMMsTermType(String _prmTermType, string _prmTermDescription, string _prmRemark)
        {
            this.TermType = _prmTermType;
            this.TermDescription = _prmTermDescription;
            this.Remark = _prmRemark;
        }

        public HRMMsTermType(String _prmTermType, string _prmTermDescription)
        {
            this.TermType = _prmTermType;
            this.TermDescription = _prmTermDescription;
        }
    }
}
