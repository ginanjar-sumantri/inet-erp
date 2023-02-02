using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Port_PPHTHd
    {
        private string _empName = "";

        public Port_PPHTHd(Guid _prmPPHTCode, string _prmTransNmbr, DateTime _prmTransDate, byte _prmStatus, int _prmPeriod, int _prmYear, string _prmEmpName)
        {
            this.PPHTCode = _prmPPHTCode;
            this.TransNmbr = _prmTransNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.Period = _prmPeriod;
            this.Year = _prmYear;
            this.EmpName = _prmEmpName;
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
