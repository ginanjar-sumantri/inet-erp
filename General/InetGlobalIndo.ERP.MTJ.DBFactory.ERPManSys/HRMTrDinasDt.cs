using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrDinasDt
    {
        private String _empName = "";

        public HRMTrDinasDt(String _prmTransNmbr, String _prmEmpNumb, String _prmEmpName, DateTime _prmStartDate, DateTime _prmEndDate, String _prmPlace, String _prmPurpose, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
            this.Place = _prmPlace;
            this.Purpose = _prmPurpose;
            this.Remark = _prmRemark;
        }

        public String EmpName
        {
            get
            {
                return this._empName;
            }

            set
            {
                this._empName = value;
            }
        }
    }
}
