using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLBudgetYearHd
    {
        public GLBudgetYearHd(int _prmYear, string _prmDepartment, int _prmRevisi, char? _prmStatus)
        {
            this.Year = _prmYear;
            //this.Department = _prmDepartment;
            this.Revisi = _prmRevisi;
            this.Status = _prmStatus;
        }

    }
}