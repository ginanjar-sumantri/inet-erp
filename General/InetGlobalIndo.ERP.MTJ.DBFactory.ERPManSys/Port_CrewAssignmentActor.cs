using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Port_CrewAssignmentActor
    {
        public Port_CrewAssignmentActor(Guid _prmCrewAssignmentActorCode, Guid _prmCrewAssignmentCode, byte _prmStatus, DateTime _prmDate, string _prmUserID, string _prmRemark)
        {
            this.CrewAssignmentActorCode = _prmCrewAssignmentActorCode;
            this.CrewAssignmentCode = _prmCrewAssignmentCode;
            this.Status = _prmStatus;
            this.Date = _prmDate;
            this.UserID = _prmUserID;
            this.Remark = _prmRemark;
        }
    }
}
