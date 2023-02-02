using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTMsDocument
    {
        public PORTMsDocument(Guid _prmDocListCode, string _prmDocListID, string _prmDocNameList)
        {
            this.DocListCode = _prmDocListCode;
            this.DocListID = _prmDocListID;
            this.DocNameList = _prmDocNameList;
        }
    }
}
