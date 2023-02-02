using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrSalaryPaymentDb
    {
        private string _empName = "";
        private string _fileNmbr = "";

        public PAYTrSalaryPaymentDb(String _prmEmpNumb, string _prmEmpName, string _prmCurrCode,
            decimal _prmForexRate, decimal? _prmAmountForex, decimal? _prmBalanceForex, Decimal? _prmPaidForex, string _prmRemark)
        {
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.CurrCode = _prmCurrCode;
            this.ForexRate = _prmForexRate;
            this.AmountForex = _prmAmountForex;
            this.BalanceForex = _prmBalanceForex;
            this.PaidForex = _prmPaidForex;
            this.Remark = _prmRemark;
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
