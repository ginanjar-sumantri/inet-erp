using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrPayrollSettingDt
    {
        private string _groupName = "";
        private string _formulaName = "";

        public PAYTrPayrollSettingDt(String _prmTransNmbr, String _prmGroup, String _prmGroupName, Decimal _prmAmountForex, String _prmFormula, String _prmFormulaName, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.GroupCode = _prmGroup;
            this.GroupName = _prmGroupName;
            this.AmountForex = _prmAmountForex;
            this.Formula = _prmFormula;
            this.FormulaName = _prmFormulaName;
            this.Remark = _prmRemark;
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
