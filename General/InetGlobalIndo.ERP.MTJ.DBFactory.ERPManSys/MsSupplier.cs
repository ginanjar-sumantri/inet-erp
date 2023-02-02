using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsSupplier
    {
        private string _suppTypeName = "";
        private string _suppGroupName = "";

        public MsSupplier(string _prmSuppCode, string _prmSuppName, string _prmSuppType, string _prmSuppTypeName, string _prmSuppGroup, string _prmSuppGroupName, char _prmFgActive)
        {
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
            this.SuppType = _prmSuppType;
            this.SuppTypeName = _prmSuppTypeName;
            this.SuppGroup = _prmSuppGroup;
            this.SuppGroupName = _prmSuppGroupName;
            this.FgActive = _prmFgActive;
        }

        public MsSupplier(String _prmSuppCode, String _prmSuppName, String _prmSuppType, String _prmSuppGroup, 
            String _prmAddress1, String _prmAddress2, String _prmCity, String _prmPostCode,String _prmTelephone, 
            String _prmFax, String _prmEmail, String _prmCurrCode, String _prmTerm, String _prmBank, 
            String _prmRekeningNo, String _prmNpwp, Char _prmFgPPN, String _prmNppkp,
            String _prmContactPerson, String _prmContactTitle, String _prmContactHP, Char _prmFgActive, 
            String _prmRemark, String _prmUserid, DateTime _prmUserDate)
        {
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
            this.SuppType = _prmSuppType;
            this.SuppGroup = _prmSuppGroup;
            this.Address1 = _prmAddress1;
            this.Address2 = _prmAddress2;
            this.City = _prmCity;
            this.PostCode = _prmPostCode;
            this.Telephone = _prmTelephone;
            this.Fax = _prmFax;
            this.Email = _prmEmail;
            this.CurrCode = _prmCurrCode;
            this.Term = _prmTerm;
            this.Bank = _prmBank;
            this.RekeningNo = _prmRekeningNo;
            this.NPWP = _prmNpwp;
            this.FgPPN = _prmFgPPN;
            this.NPPKP = _prmNppkp;
            this.ContactPerson = _prmContactPerson;
            this.ContactTitle = _prmContactTitle;
            this.ContactHP = _prmContactHP;
            this.FgActive = _prmFgActive;
            this.Remark = _prmRemark;
            this.CreatedBy = _prmUserid;
            this.CreatedDate = _prmUserDate;
        }

        public MsSupplier(string _prmSuppCode, string _prmSuppName)
        {
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
        }

        public string SuppTypeName
        {
            get
            {
                return this._suppTypeName;
            }
            set
            {
                this._suppTypeName = value;
            }
        }

        public string SuppGroupName
        {
            get
            {
                return this._suppGroupName;
            }
            set
            {
                this._suppGroupName = value;
            }
        }
    }
}
