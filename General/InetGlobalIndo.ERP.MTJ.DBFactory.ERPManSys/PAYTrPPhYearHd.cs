using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrPPhYearHd
    {
        public PAYTrPPhYearHd(int _prmYear, Char _prmStatus)
        {
            this.Year = _prmYear;
            this.Status = _prmStatus;
        }
    }
}
