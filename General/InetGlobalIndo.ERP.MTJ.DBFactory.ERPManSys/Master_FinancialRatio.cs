using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_FinancialRatio
    {
        public Master_FinancialRatio(string _prmFinancialRatioCode, string _prmFinancialRatioName, byte _prmGroupLevel, string _prmDescription, string _prmAccount)
        {
            this.FinancialRatioCode = _prmFinancialRatioCode;
            this.FinancialRatioName = _prmFinancialRatioName;
            this.GroupLevel = _prmGroupLevel;
            this.Description = _prmDescription;
            this.Account = _prmAccount;
        }

        public Master_FinancialRatio(string _prmFinancialRatioCode, string _prmEducationName)
        {
            this.FinancialRatioCode = _prmFinancialRatioCode;
            this.FinancialRatioName = _prmEducationName;
        }
    }
}
