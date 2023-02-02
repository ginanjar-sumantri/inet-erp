using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFAProcessHd
    {
        public GLFAProcessHd(int _prmYear,int _prmPeriod, char _prmStatus, string _prmRemark)
        {
            this.Year = _prmYear;
            this.Period = _prmPeriod;
            this.Status = _prmStatus;
            this.Remark = _prmRemark;
        }
    }
}
