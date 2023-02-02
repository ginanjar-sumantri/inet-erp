using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLBudgetYearDt
    {
        public GLBudgetYearDt(int _prmYear, string _prmDepartment, int _prmRevisi, int _prmItemNo, int _prmDays)
        {
            this.Year = _prmYear;
            //this.Department = _prmDepartment;
            this.Revisi = _prmRevisi;
            this.ItemNo = _prmItemNo;
            this.Days = _prmDays;
        }

    }
}