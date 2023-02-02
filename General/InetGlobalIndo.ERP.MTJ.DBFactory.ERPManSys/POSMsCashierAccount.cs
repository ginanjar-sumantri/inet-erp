using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class POSMsCashierAccount
    {
        String _accountName = "";
        String _employeeName = "";

        public POSMsCashierAccount(String _prmCashierEmpNmbr, String _prmAccount, String _prmAccountName)
        {
            this.CashierEmpNmbr = _prmCashierEmpNmbr;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
        }

        public POSMsCashierAccount(String _prmEmployeeName, String _prmAccountName)
        {
            this.EmployeeName = _prmEmployeeName;
            this.AccountName = _prmAccountName;
        }

        public String AccountName
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

        public String EmployeeName
        {
            get
            {
                return this._employeeName;
            }

            set
            {
                this._employeeName = value;
            }
        }
        
    }
}
