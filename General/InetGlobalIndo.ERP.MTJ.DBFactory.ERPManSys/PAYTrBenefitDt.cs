using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrBenefitDt
    {
        private string _empName = "";
        private string _benefitName = "";

        public PAYTrBenefitDt(String _prmTransNmbr, int _prmItemNo, String _prmEmpNumb, String _prmEmpName, String _prmBenefit, String _prmBenefitName,
            String _prmCurrCode, Decimal? _prmForexRate, Decimal? _prmAmountForex, Decimal? _prmAmountHome)
        {
            this.TransNmbr = _prmTransNmbr;
            this.ItemNo = _prmItemNo;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.Benefit = _prmBenefit;
            this.BenefitName = _prmBenefitName;
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

        public string BenefitName
        {
            get
            {
                return this._benefitName;
            }
            set
            {
                this._benefitName = value;
            }
        }
    }
}
