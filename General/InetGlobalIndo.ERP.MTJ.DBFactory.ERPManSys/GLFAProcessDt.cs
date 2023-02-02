using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFAProcessDt
    {
        string _faName = "";

        public GLFAProcessDt(string _prmFACode, string _prmFAName, decimal _prmAmountDepr, decimal _prmAdjustDepr, decimal _prmTotalDepr, decimal? _prmBalanceAmount, int? _prmBalanceLife)
        {
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.AmountDepr = _prmAmountDepr;
            this.AdjustDepr = _prmAdjustDepr;
            this.TotalDepr = _prmTotalDepr;
            this.BalanceAmount = _prmBalanceAmount;
            this.BalanceLife = _prmBalanceLife;
        }

        public string FAName
        {
            get
            {
                return this._faName;
            }
            set
            {
                this._faName = value;
            }
        }
    }
}
