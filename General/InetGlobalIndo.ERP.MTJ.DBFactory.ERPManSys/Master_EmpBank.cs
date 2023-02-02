using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_EmpBank
    {
        string _bankName = "";

        public Master_EmpBank(Guid _prmEmpBankCode, string _prmEmpNmbr, string _prmBankCode, string _prmBankName, string _prmBankAddr, string _prmBankAccount, string _prmBankAccountName)
        {
            this.EmpBankCode = _prmEmpBankCode;
            this.EmpNmbr = _prmEmpNmbr;
            this.BankCode = _prmBankCode;
            this.BankName = _prmBankName;
            this.BankAddr = _prmBankAddr;
            this.BankAccount = _prmBankAccount;
            this.BankAccountName = _prmBankAccountName;
        }

        public string BankName
        {
            get
            {
                return this._bankName;
            }
            set
            {
                this._bankName = value;
            }
        }
    }
}
