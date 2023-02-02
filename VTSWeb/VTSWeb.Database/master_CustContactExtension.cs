using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace VTSWeb.Database
{
    public partial class master_CustContactExtension
    {
        private string _custName = "";
        private string _contactName = "";

        public master_CustContactExtension(String _prmCustCode, int _prmItemNo, String _prmCustomerPhoto)
        {
            this.CustCode = _prmCustCode;
            this.ItemNo   = _prmItemNo;
            this.CustomerPhoto = _prmCustomerPhoto;

        }
        public master_CustContactExtension(String _prmCustCode, String _prmCustName,int _prmItemNo, String _prmContactName, String _prmCustomerPhoto)
        {
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.ItemNo = _prmItemNo;
            this.ContactName = _prmContactName;
            this.CustomerPhoto = _prmCustomerPhoto;
        }
        public string CustName
        {
            get
            {
                return this._custName;
            }
            set
            {
                this._custName = value;
            }
        }

        public string ContactName
        {
            get
            {
                return this._contactName;
            }
            set
            {
                this._contactName = value;
            }
        }
    }
}