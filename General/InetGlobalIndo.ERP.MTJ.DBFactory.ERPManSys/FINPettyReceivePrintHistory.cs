using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINPettyReceivePrintHistory
    {
        public FINPettyReceivePrintHistory(Guid _prmFINPettyReceivePrintHistoryId, string _prmTransNmbr, DateTime _prmDateTime, string _prmRemark, string _prmUserName)
        {
            this.FINPettyReceivePrintHistoryId = _prmFINPettyReceivePrintHistoryId;
            this.TransNmbr = _prmTransNmbr;
            this.DateTime = _prmDateTime;
            this.Remark = _prmRemark;
            this.UserName = _prmUserName;
        }
    }
}
