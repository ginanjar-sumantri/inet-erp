using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrSKSalaryDt
    {
        private string _payrollName = "";
        private string _groupName = "";
        private string _formulaName = "";

        public PAYTrSKSalaryDt(String _prmTransNmbr, String _prmPayrollCode, String _prmPayrollName, String _prmGroupCode, String _prmGroupName,
            String _prmGroupBy, Decimal _prmAmountCurrent, String _prmModeType, Decimal _prmValueAdjust, Decimal _prmAmountNew, String _prmFormula,
            String _prmFormulaName, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.PayrollCode = _prmPayrollCode;
            this.PayrollName = _prmPayrollName;
            this.GroupCode = _prmGroupCode;
            this.GroupName = _prmGroupName;
            this.GroupBy = _prmGroupBy;
            this.AmountCurrent = _prmAmountCurrent;
            this.ModeType = _prmModeType;
            this.ValueAdjust = _prmValueAdjust;
            this.AmountNew = _prmAmountNew;
            this.Formula = _prmFormula;
            this.FormulaName = _prmFormulaName;
            this.Remark = _prmRemark;
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

        public string GroupName
        {
            get
            {
                return this._groupName;
            }
            set
            {
                this._groupName = value;
            }
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
    }
}
