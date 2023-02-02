using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINPettyHd
    {
        private string _currencyName = "";

        public FINPettyHd(string _prmTransNumber, string _prmFileNmbr, char _prmStatus, DateTime _prmTransDate, string _prmPetty, string _prmCurrName, decimal _prmForexRate,
                           string _prmPayTo, string _prmRemark)
        {
            this.TransNmbr = _prmTransNumber;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.Petty = _prmPetty;
            this.CurrencyName = _prmCurrName;
            this.ForexRate = _prmForexRate;
            this.PayTo = _prmPayTo;
            this.Remark = _prmRemark;
        }

        public FINPettyHd(string _prmTransNumber, char _prmStatus, DateTime _prmTransDate, string _prmPayTo, string _prmRemark)
        {
            this.TransNmbr = _prmTransNumber;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.PayTo = _prmPayTo;
            this.Remark = _prmRemark;
        }

        public FINPettyHd(string _prmTransNumber,string _prmFileNmbr ,char _prmStatus, DateTime _prmTransDate, string _prmPayTo, string _prmRemark)
        {
            this.TransNmbr = _prmTransNumber;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.PayTo = _prmPayTo;
            this.Remark = _prmRemark;
        }
        
        public string CurrencyName
        {
            get
            {
                return this._currencyName;
            }
            set
            {
                this._currencyName = value;
            }
        }
    }
}
