using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    [Serializable()]
    public partial class MsAccount : Base
    {

        private string _CurrencyName = "";
        private string _accClassName = "";

        public MsAccount(string _accountCode, string _accountName,
            string _accClass, string _accClassName, string _detail, char _fgSubled,
            char _fgNormal, string _CurrCode, char _fgActive)
        {
            this.Account = _accountCode;
            this.AccountName = _accountName;
            this.AccClass = _accClass;
            this.AccClassName = _accClassName;
            this.Detail = _detail;
            this.FgSubLed = _fgSubled;
            this.FgNormal = _fgNormal;
            this.CurrCode = _CurrCode;
            this.FgActive = _fgActive;

        }

        public MsAccount(string _accountCode, string _accountName, Guid _branchAccCode, string _accClass, string _detail, char _fgSubled,
            char _fgNormal, string _currCode, char _fgActive, string _userID, DateTime _userDate, string _userClose, DateTime _closeDate)
        {
            this.Account = _accountCode;
            this.AccountName = _accountName;
            this.BranchAccCode = _branchAccCode;
            this.AccClass = _accClass;
            this.Detail = _detail;
            this.FgSubLed = _fgSubled;
            this.FgNormal = _fgNormal;
            this.CurrCode = _currCode;
            this.FgActive = _fgActive;
            this.UserID = _userID;
            this.UserDate = _userDate;
            this.UserClose = _userClose;
            this.CloseDate = _closeDate;
        }

        public MsAccount(string _accountCode, string _accountName, char _fgSubled, char _fgNormal, string _CurrCode)
        {
            this.Account = _accountCode;
            this.AccountName = _accountName;
            this.FgSubLed = _fgSubled;
            this.FgNormal = _fgNormal;
            this.CurrCode = _CurrCode;
        }

        public MsAccount(string _accountCode, string _accountName)
        {
            this.Account = _accountCode;
            this.AccountName = _accountName;
        }

        public MsAccount(string _accountCode)
        {
            this.Account = _accountCode;
        }

        public string AccClassName
        {
            get
            {
                return this._accClassName;
            }
            set
            {
                this._accClassName = value;
            }
        }

        public string CurrencyName
        {
            get
            {
                return this._CurrencyName;
            }
            set
            {
                this._CurrencyName = value;
            }
        }

    }
}
