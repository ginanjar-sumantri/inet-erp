using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_Master_Gender
    {
        public HRM_Master_Gender(string _prmGenderCode, string _prmGenderName)
        {
            this.GenderCode = _prmGenderCode;
            this.GenderName = _prmGenderName;
        }
    }
}
