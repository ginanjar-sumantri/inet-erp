using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Transaction_Activity
    {
        public Transaction_Activity(Guid _prmActivitiesCode, String _prmTransType, String _prmTransNmbr,
            Byte _prmActivitiesID, String _prmUsername, DateTime _prmActivitiesDate)
        {
            this.ActivitiesCode = _prmActivitiesCode;
            this.TransType = _prmTransType;
            this.TransNmbr = _prmTransNmbr;
            this.ActivitiesID = _prmActivitiesID;
            this.Username = _prmUsername;
            this.ActivitiesDate = _prmActivitiesDate;
        }

        public Transaction_Activity(Guid _prmActivitiesCode, String _prmTransType,
            String _prmTransNmbr, DateTime _prmActivitiesDate, String _prmUsername)
        {
            this.ActivitiesCode = _prmActivitiesCode;
            this.TransType = _prmTransType;
            this.TransNmbr = _prmTransNmbr;
            this.ActivitiesDate = _prmActivitiesDate;
            this.Username = _prmUsername;
        }
    }
}