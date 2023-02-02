using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Finance_MoneyChanger
    {
        public Finance_MoneyChanger(Guid _prmMoneyChangerCode, string _prmTransNumber, string _prmFileNmbr, byte _prmStatus, DateTime _prmTransDate, String _prmCurrCode, String _prmCurrExchange, decimal _prmForexRateExchange, decimal _prmAmountExchange, decimal _prmAmount, byte _prmFgType, byte _prmFgTypeExchange)
        {
            this.MoneyChangerCode = _prmMoneyChangerCode;
            this.TransNmbr = _prmTransNumber;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.CurrCode = _prmCurrCode;
            this.CurrExchange = _prmCurrExchange;
            this.ForexRateExchange = _prmForexRateExchange;
            this.AmountExchange = _prmAmountExchange;
            this.Amount = _prmAmount;
            this.FgType = _prmFgType;
            this.FgTypeExchange = _prmFgTypeExchange;
        }
    }
}
