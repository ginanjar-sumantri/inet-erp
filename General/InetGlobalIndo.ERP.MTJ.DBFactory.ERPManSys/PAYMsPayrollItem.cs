using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYMsPayrollItem
    {
        private string _formulaName = "";
        private string _accountName = "";

        public PAYMsPayrollItem(string _prmPayrollCode, string _prmPayrollName, char _prmPayrollPref, String _prmPayrollNo,
            String _prmPayrollType, String _prmFormulaCode, String _prmFormulaName, String _prmGroupBy, String _prmPrioritas,
            Boolean? _prmFgPPh, int? _prmFgValue, String _prmAccount, String _prmAccountName)
        {
            this.PayrollCode = _prmPayrollCode;
            this.PayrollName = _prmPayrollName;
            this.PayrollPref = _prmPayrollPref;
            this.PayrollNo = _prmPayrollNo;
            this.PayrollType = _prmPayrollType;
            this.FormulaCode = _prmFormulaCode;
            this.FormulaName = _prmFormulaName;
            this.GroupBy = _prmGroupBy;
            this.Prioritas = _prmPrioritas;
            this.FgPPh = _prmFgPPh;
            this.FgValue = _prmFgValue;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
        }

        public PAYMsPayrollItem(string _prmPayrollCode, string _prmPayrollName)
        {
            this.PayrollCode = _prmPayrollCode;
            this.PayrollName = _prmPayrollName;
        }

        public string FormulaName
        {
            get
            {
                return this._formulaName;
            }
            set
            {
                this._formulaName = value;
            }
        }

        public string AccountName
        {
            get
            {
                return this._accountName;
            }
            set
            {
                this._accountName = value;
            }
        }
    }
}
