using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsRack_Customer
    {
        private string _custName    ="";
        private string _rackName    ="";

        public MsRack_Customer(Guid _prmRackCustomerCode, string _prmCustCode, string _prmCustName, string _prmRackCode, string _prmRackName)
        {
            this.RackCustomerCode = _prmRackCustomerCode;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.RackCode = _RackCode;
            this.RackName = _prmRackName;
        }

        public MsRack_Customer(string _prmCustCode, string _prmRackCode, string _prmRackName)
        {
            this.CustCode = _prmCustCode;
            this.RackCode = _prmRackCode;
            this.RackName = _prmRackName;
        }
         public MsRack_Customer(string _prmRackCode, string _prmRackName)
         {
             this.RackCode = _prmRackCode;
             this.RackName = _prmRackName;
         }

         public MsRack_Customer(Guid _prmRackCustomerCode, string _prmCustCode, string _prmRackCode)
         {
             this.RackCustomerCode = _prmRackCustomerCode;
             this.CustCode = _prmCustCode;                     
             this.RackCode = _prmRackCode;             
         }

         public MsRack_Customer(string _prmCustName, string _prmRackName, Guid _prmRackCustomerCode)
         {
             this.RackCustomerCode = _prmRackCustomerCode;
             this.CustName = _prmCustName;
             this.RackName = _prmRackName;
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

        public string RackName
        {
            get
            {
                return this._rackName;
            }
            set
            {
                this._rackName = value;
            }
        }

    }
}


