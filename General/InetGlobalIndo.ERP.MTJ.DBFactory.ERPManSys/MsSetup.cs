using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsSetup : Base
    {
        string _accountName = "";

        public MsSetup(string _prmSetGroup, string _prmSetCode, string _prmSetValue, string _prmAccountName, string _prmSetDescription)
        {
            this.SetGroup = _prmSetGroup;
            this.SetCode = _prmSetCode;
            this.SetValue = _prmSetValue;
            this.AccountName = _prmAccountName;
            this.SetDescription = _prmSetDescription;
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
