using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMMsAbsGroup
    {
        public HRMMsAbsGroup(String _prmAbsenceGroupCode, string _prmAbsenceGroupName, string _prmAbsenceGroupRemark)
        {
            this.AbsenceGroup = _prmAbsenceGroupCode;
            this.AbsenceGroupName = _prmAbsenceGroupName;
            this.AbsenceGroupRemark = _prmAbsenceGroupRemark;
        }

        public HRMMsAbsGroup(String _prmAbsenceGroupCode, string _prmAbsenceGroupName)
        {
            this.AbsenceGroup = _prmAbsenceGroupCode;
            this.AbsenceGroupName = _prmAbsenceGroupName;
        }
    }
}
