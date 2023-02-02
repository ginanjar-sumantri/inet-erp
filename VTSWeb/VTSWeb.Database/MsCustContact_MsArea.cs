using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Database
{
    public partial class MsCustContact_MsArea
    {
        private string _custName    ="";
        private string _contactName ="";
        private string _areaName    ="";

        public MsCustContact_MsArea(Guid _prmVisitorAreaCode, string _prmCustCode, string _prmCustName, int _prmItemNo, string _prmContactName, string _prmAreaCode, string _prmAreaName)
        {
            this.VisitorAreaCode = _prmVisitorAreaCode;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.ItemNo = _prmItemNo;
            this.ContactName = _prmContactName;
            this.AreaCode = _prmAreaCode;
            this.AreaName = _prmAreaName;
        }

        public MsCustContact_MsArea(string _prmCustCode, int _prmItemNo, string _prmAreaCode, string _prmAreaName)
        {
            this.CustCode = _prmCustCode;
            this.ItemNo = _prmItemNo;
            this.AreaCode = _prmAreaCode;
            this.AreaName = _prmAreaName;
        }
         public MsCustContact_MsArea(string _prmAreaCode, string _prmAreaName)
         {
             this.AreaCode = _prmAreaCode;
             this.AreaName = _prmAreaName;
         }

         public MsCustContact_MsArea(Guid _prmVisitorAreaCode, string _prmCustCode, int _prmItemNo, string _prmAreaCode)
         {
             this.VisitorAreaCode = _prmVisitorAreaCode;
             this.CustCode = _prmCustCode;             
             this.ItemNo = _prmItemNo;            
             this.AreaCode = _prmAreaCode;             
         }

         public MsCustContact_MsArea(String _prmContactName, string _prmCustName, string _prmAreaName, Guid _prmVisitorAreaCode)
         {
             this.VisitorAreaCode = _prmVisitorAreaCode;
             this.CustName = _prmCustName;
             this.ContactName = _prmContactName;
             this.AreaName = _prmAreaName;
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

        public string AreaName
        {
            get
            {
                return this._areaName;
            }
            set
            {
                this._areaName = value;
            }
        }

    }
}


