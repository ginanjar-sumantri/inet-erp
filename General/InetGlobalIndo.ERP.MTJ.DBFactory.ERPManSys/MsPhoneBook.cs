using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsPhoneBook
    {
        public MsPhoneBook(String _prmHP, String _prmCustID, String _prmCustName)
        {
            this.HP = _prmHP;
            this.CustID = _prmCustID;
            this.CustName = _prmCustName;
        }        
    }
}
