using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Port_TallyHdActor
    {
        public Port_TallyHdActor(Guid _prmTallyActorCode, Guid _prmTallyHdCode, byte _prmStatus, DateTime _prmDate, string _prmUserID, string _prmRemark)
        {
            this.TallyActorCode = _prmTallyActorCode;
            this.TallyHdCode = _prmTallyHdCode;
            this.Status = _prmStatus;
            this.Date = _prmDate;
            this.UserID = _prmUserID;
            this.Remark = _prmRemark;
        }
    }
}
