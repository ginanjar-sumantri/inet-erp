using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINPettyReceiveHd
    {
        private string _currencyName = "";

        public FINPettyReceiveHd(string _prmTransNumber, string _prmFileNmbr, char _prmStatus, DateTime _prmTransDate, string _prmCurrName, decimal _prmForexRate, string _prmPayTo, string _prmRemark, byte _prmFgType)
        {
            this.TransNmbr = _prmTransNumber;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.CurrencyName = _prmCurrName;
            this.ForexRate = _prmForexRate;
            this.PayTo = _prmPayTo;
            this.Remark = _prmRemark;
            this.FgType = _prmFgType;
        }

        public FINPettyReceiveHd(string _prmTransNumber, char _prmStatus, DateTime _prmTransDate, string _prmPayTo, string _prmRemark)
        {
            this.TransNmbr = _prmTransNumber;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.PayTo = _prmPayTo;
            this.Remark = _prmRemark;
        }

        public FINPettyReceiveHd(string _prmTransNumber, string _prmFileNmbr, char _prmStatus, DateTime _prmTransDate, string _prmPayTo, string _prmRemark)
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
