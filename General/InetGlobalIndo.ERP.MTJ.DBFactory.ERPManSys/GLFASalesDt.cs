using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class GLFASalesDt
    {
        string _faName = "";

        public GLFASalesDt(string _prmFACode, string _prmFAName, decimal _prmAmountForex, decimal _prmAmountHome, decimal _prmAmountCurrent)
        {
            this.FACode = _prmFACode;
            this.FAName = _prmFAName;
            this.AmountForex = _prmAmountForex;
            this.AmountHome = _prmAmountHome;
            this.AmountCurrent = _prmAmountCurrent;
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
