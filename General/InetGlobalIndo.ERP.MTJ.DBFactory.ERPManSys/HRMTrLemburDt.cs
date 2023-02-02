using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLemburDt
    {
        private string _empName = "";

        public HRMTrLemburDt(String _prmTransNmbr, String _prmEmpNumb, String _prmEmpName, String _prmStartHours, String _prmEndHours, Decimal _prmHours,
            Char _prmFgFullDays, Char _prmFgMakan, Char _prmFgShift, char? _prmStatusProcess)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.StartHours = _prmStartHours;
            this.EndHours = _prmEndHours;
            this.Hours = _prmHours;
            this.FgFullDays = _prmFgFullDays;
            this.FgMakan = _prmFgMakan;
            this.FgShift = _prmFgShift;
            this.StatusProcess = _prmStatusProcess;
        }

        public string EmpName
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
