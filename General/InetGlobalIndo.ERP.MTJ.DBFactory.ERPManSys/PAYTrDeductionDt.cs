using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrDeductionDt
    {
        private string _empName = "";
        private string _DeductionName = "";

        public PAYTrDeductionDt(String _prmTransNmbr, int _prmItemNo, String _prmEmpNumb, String _prmEmpName, String _prmDeduction, String _prmDeductionName,
            String _prmCurrCode, Decimal? _prmForexRate, Decimal? _prmAmountForex, Decimal? _prmAmountHome)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ItemNo = _prmItemNo;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.Deduction = _prmDeduction;
            this.DeductionName = _prmDeductionName;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.AmountForex = _prmAmountForex;
            this.AmountHome = _prmAmountHome;
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

        public string DeductionName
        {
            get
            {
                return this._DeductionName;
            }
            set
            {
                this._DeductionName = value;
            }
        }
    }
}
