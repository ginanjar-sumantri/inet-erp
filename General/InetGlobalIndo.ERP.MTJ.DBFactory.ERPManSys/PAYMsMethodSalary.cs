using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYMsMethodSalary
    {
        public PAYMsMethodSalary(string _prmMethodCode, string _prmMethodName, string _prmMethodRange, int _prmRangeDay)
        {
            this.MethodCode = _prmMethodCode;
            this.MethodName = _prmMethodName;
            this.MethodRange = _prmMethodRange;
            this.RangeDay = _prmRangeDay;
        }

        public PAYMsMethodSalary(string _prmMethodCode, string _prmMethodName)
        {
            this.MethodCode = _prmMethodCode;
            this.MethodName = _prmMethodName;
        }
    }
}
