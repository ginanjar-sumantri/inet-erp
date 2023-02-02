using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrSlipDt
    {
        private string _payrollName = "";

        public PAYTrSlipDt(String _prmTransNmbr, String _prmPayrollCode, String _prmPayrollName)
        {
            this.TransNmbr = _prmTransNmbr;
            this.PayrollCode = _prmPayrollCode;
            this.PayrollName = _prmPayrollName;
        }

        public string PayrollName
        {
            get
            {
                return this._payrollName;
            }
            set
            {
                this._payrollName = value;
            }
        }
    }
}
