using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINPettyPrintHistory
    {
        public FINPettyPrintHistory(Guid _prmFINPettyPrintHistoryId, string _prmTransNmbr, DateTime _prmDateTime, string _prmRemark, string _prmUserName)
        {
            this.FINPettyPrintHistoryId = _prmFINPettyPrintHistoryId;
            this.TransNmbr = _prmTransNmbr;
            this.DateTime = _prmDateTime;
            this.Remark = _prmRemark;
            this.UserName = _prmUserName;
        }
    }
}
