using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_ClassroomGroup
    {

        public Master_ClassroomGroup(Guid _prmClassGroupCode, string _prmClassGroupName, string _prmClassGroupDesc)
        {
            this.ClassroomGroupCode = _prmClassGroupCode;
            this.ClassroomGroupName = _prmClassGroupName;
            this.ClassroomGroupDescription = _prmClassGroupDesc;
        }

        public Master_ClassroomGroup(Guid _prmClassGroupCode, string _prmClassGroupName)
        {
            this.ClassroomGroupCode = _prmClassGroupCode;
            this.ClassroomGroupName = _prmClassGroupName;
        }
    }
}
