using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrPayrollSettingHd
    {
        private string _payrollName = "";

        public PAYTrPayrollSettingHd(string _prmTransNmbr, String _prmFileNmbr, char _prmStatus, DateTime _prmTransDate,
             String _prmPayroll, String _prmPayrollName, DateTime _prmStartEffectiveDate, DateTime? _prmEndEffectiveDate, String _prmGroupBy,
             String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.Payroll = _prmPayroll;
            this.PayrollName = _prmPayrollName;
            this.StartEffectiveDate = _prmStartEffectiveDate;
            this.EndEffectiveDate = _prmEndEffectiveDate;
            this.GroupBy = _prmGroupBy;
            this.Remark = _prmRemark;
        }

        public PAYTrPayrollSettingHd(string _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
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
