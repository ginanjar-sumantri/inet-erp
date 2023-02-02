using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_Education
    {
        public Master_Education(Guid _prmEducationCode, string _prmEducationName, string _prmEducationDescription)
        {
            this.EducationCode = _prmEducationCode;
            this.EducationName = _prmEducationName;
            this.EducationDescription = _prmEducationDescription;
        }

        public Master_Education(Guid _prmEducationCode, string _prmEducationName)
        {
            this.EducationCode = _prmEducationCode;
            this.EducationName = _prmEducationName;
        }
    }
}
