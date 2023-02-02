using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFADevaluationDt : Base
    {
        private string _FAName = "";

        public GLFADevaluationDt(string _prmFACode, string _prmFAName, int _prmBalanceLife, decimal _prmBalanceAmount)
        {
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.BalanceLife = _prmBalanceLife;
            this.BalanceAmount = _prmBalanceAmount;
        }

        public GLFADevaluationDt(string _prmTransNmbr, string _prmFACode, string _prmFAName, int _prmBalanceLife, decimal _prmBalanceAmount, int _prmNewLife, decimal _prmNewAmount, int _prmAdjustLife, decimal _prmAdjustAmount)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.BalanceLife = _prmBalanceLife;
            this.BalanceAmount = _prmBalanceAmount;
            this.NewLife = _prmNewLife;
            this.NewAmount = _prmNewAmount;
            this.AdjustLife = _prmAdjustLife;
            this.AdjustAmount = _prmAdjustAmount;
        }

        public string FAName
        {
            get
            {
                return this._FAName;
            }
            set
            {
                this._FAName = value;
            }
        }
    }
}
