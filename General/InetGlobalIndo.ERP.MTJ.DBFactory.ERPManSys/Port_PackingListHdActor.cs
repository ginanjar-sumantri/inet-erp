using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Port_PackingListHdActor
    {
        public Port_PackingListHdActor(Guid _prmPackingListHdActorCode, Guid _prmPackingListHdCode, byte _prmStatus, DateTime _prmDate, string _prmUserID, string _prmRemark)
        {
            this.PackingListHdActorCode = _prmPackingListHdActorCode;
            this.PackingListHdCode = _prmPackingListHdCode;
            this.Status = _prmStatus;
            this.Date = _prmDate;
            this.UserID = _prmUserID;
            this.Remark = _prmRemark;
        }
    }
}
