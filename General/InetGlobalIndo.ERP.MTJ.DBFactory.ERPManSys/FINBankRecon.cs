using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Finance_BankRecon : Base
    {
        private string _payName = "";

        public Finance_BankRecon(Guid _prmBankReconCode, string _prmTransNmbr, string _prmFileNmbr, 
            DateTime _prmTransDate, string _prmPayCode, string _prmPayName, string _prmAccPay,
            char _prmFgPay, decimal _prmSumValurForex, decimal _prmDiffValueForex, decimal _prmBankValueForex, 
            byte _prmStatus, string _prmRemark)
        {
            this.BankReconCode = _prmBankReconCode;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.PayCode = _prmPayCode;
            this.PayName = _prmPayName;
            this.AccPay = _prmAccPay;
            this.FgPay = _prmFgPay;
            this.SumValueForex = _prmSumValurForex;
            this.DiffValueForex = _prmDiffValueForex;
            this.BankValueForex = _prmBankValueForex;
            this.Status = _prmStatus;
            this.Remark = _prmRemark;
        }

        public string PayName
        {
            get
            {
                return this._payName;
            }
            set
            {
                this._payName = value;
            }
        }
    }
}
