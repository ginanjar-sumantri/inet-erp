using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Port_PPHTHdActor
    {
        public Port_PPHTHdActor(Guid _prmPPHTActorCode, Guid _prmPPHTCode, byte _prmStatus, DateTime _prmDate, string _prmUserID, string _prmRemark)
        {
            this.PPHTActorCode = _prmPPHTActorCode;
            this.PPHTCode = _prmPPHTCode;
            this.Status = _prmStatus;
            this.Date = _prmDate;
            this.UserID = _prmUserID;
            this.Remark = _prmRemark;
        }
    }
}
