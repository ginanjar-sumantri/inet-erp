using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_EmployeeAuthorization
    {
        private string _empName = "";
        private string _accountName = "";
        private string _transTypeName = "";

        public Master_EmployeeAuthorization(string _prmEmpNumb, string _prmEmpName)
        {
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
        }

        public Master_EmployeeAuthorization(string _prmTransTypeCode, string _prmTransTypeName, string _prmAccount, string _prmAccountName)
        {
            this.TransTypeCode = _prmTransTypeCode;
            this.TransTypeName = _prmTransTypeName;
            this.Account = _prmAccount;
            this.AccountName = _prmAccountName;
        }

        public string EmpName
        {
            get
            {
                return this._empName;
            }
            set
            {
                this._empName = value;
            }
        }

        public string TransTypeName
        {
            get
            {
                return this._transTypeName;
            }
            set
            {
                this._transTypeName = value;
            }
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
