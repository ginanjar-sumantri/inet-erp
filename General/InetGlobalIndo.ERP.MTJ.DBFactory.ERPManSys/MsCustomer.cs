using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsCustomer
    {
        private string _custBillAccount = "";
        private string _custBillDescription = "";

        public MsCustomer(string _prmCustCode, string _prmCustName, string _prmCustGroup, string _prmCustType, char? _prmFgActive, string _prmAddress1)
        {
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.CustGroup = _prmCustGroup;
            this.CustType = _prmCustType;
            this.FgActive = _prmFgActive;
            this.Address1 = _prmAddress1;
        }

        public MsCustomer(string _prmCustCode, string _prmCustName, string _prmCustGroup, string _prmCustType, char? _prmFgActive, string _prmAddress1, String _prmCustVerificationCode)
        {
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.CustGroup = _prmCustGroup;
            this.CustType = _prmCustType;
            this.FgActive = _prmFgActive;
            this.Address1 = _prmAddress1;
            this.CustVerificationCode = _prmCustVerificationCode;
        }

        public MsCustomer(String _prmCustCode, String _prmCustName, String _prmCustType, String _prmCustBillAccount,
            String _prmContactName, String _prmCustomerEmail, String _prmCustBillDescription)
        {
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.CustType = _prmCustType;
            this.CustBillAccount = _prmCustBillAccount;
            this.ContactName = _prmContactName;
            this.Email = _prmCustomerEmail;
            this.CustBillDescription = _prmCustBillDescription;            
        }

        public MsCustomer(String _prmCustCode, String _prmCustName, String _prmCustType, String _prmCustBillAccount,
            String _prmContactName, String _prmCustomerEmail, String _prmCustBillDescription, String _prmCustVerificationCode)
        {
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.CustType = _prmCustType;
            this.CustBillAccount = _prmCustBillAccount;
            this.ContactName = _prmContactName;
            this.Email = _prmCustomerEmail;
            this.CustBillDescription = _prmCustBillDescription;
            this.CustVerificationCode = _prmCustVerificationCode;
        }

        public MsCustomer(string _prmCustCode, string _prmCustName)
        {
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
        }

        public String CustBillAccount
        {
            get
            {
                return this._custBillAccount;
            }
            set
            {
                this._custBillAccount = value;
            }
        }

        public String CustBillDescription
        {
            get
            {
                return this._custBillDescription;
            }
            set
            {
                this._custBillDescription = value;
            }
        }
    }
}
