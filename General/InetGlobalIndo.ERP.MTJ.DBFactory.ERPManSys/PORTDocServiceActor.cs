using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTDocServiceActor
    {
        public PORTDocServiceActor(Guid _prmDocServiceActorCode, Guid _prmHdDocServiceCode, byte _prmStatus, DateTime _prmDate, string _prmUserID, string _prmRemark)
        {
            this.DocServiceActorCode = _prmDocServiceActorCode;
            this.HdDocServiceCode = _prmHdDocServiceCode;
            this.Status = _prmStatus;
            this.Date = _prmDate;
            this.UserID = _prmUserID;
            this.Remark = _prmRemark;
        }
    }
}
