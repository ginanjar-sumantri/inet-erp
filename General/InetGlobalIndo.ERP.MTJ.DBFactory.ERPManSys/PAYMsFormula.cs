using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYMsFormula
    {
        public PAYMsFormula(string _prmFormulaCode, string _prmFormulaName, String _prmFormulaProcess, Decimal? _prmFormulaValues, Boolean? _prmFgUpdate)
        {
            this.FormulaCode = _prmFormulaCode;
            this.FormulaName = _prmFormulaName;
            this.FormulaProcess = _prmFormulaProcess;
            this.FormulaValues = _prmFormulaValues;
            this.FgUpdate = _prmFgUpdate;
        }

        public PAYMsFormula(string _prmFormulaCode, string _prmFormulaName)
        {
            this.FormulaCode = _prmFormulaCode;
            this.FormulaName = _prmFormulaName;
        }
    }
}
