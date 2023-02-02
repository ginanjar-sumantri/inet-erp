using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLBudgetAcc
    {
        string _accountName = "";

        public GLBudgetAcc(Guid _prmBudgetDetailCode, Guid _prmBudgetCode, string _prmAccount, string _prmAccountName, decimal _prmAmountBudgetRate, decimal _prmAmountBudgetForex, decimal _prmAmountBudgetHome, decimal _prmAmountActual)
        {
            this.BudgetDetailCode = _prmBudgetDetailCode;
            this.BudgetCode = _prmBudgetCode;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
            this.AmountBudgetRate = _prmAmountBudgetRate;
            this.AmountBudgetForex = _prmAmountBudgetForex;
            this.AmountBudgetHome = _prmAmountBudgetHome;
            this.AmountActual = _prmAmountActual;
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
