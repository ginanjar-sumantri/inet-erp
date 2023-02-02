using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_Instructor
    {
        public Master_Instructor(Guid _prmInstructorsCode, string _prmInstructorsName, string _prmInstructorsDescription, string _prmInstructorsAddress1)
        {
            this.InstructorsCode = _prmInstructorsCode;
            this.InstructorsName = _prmInstructorsName;
            this.InstructorsDescription = _prmInstructorsDescription;
            this.InstructorsAddress1 = _prmInstructorsAddress1;
        }

        public Master_Instructor(Guid _prmInstructorsCode, string _prmInstructorsName)
        {
            this.InstructorsCode = _prmInstructorsCode;
            this.InstructorsName = _prmInstructorsName;
        }
    }
}
