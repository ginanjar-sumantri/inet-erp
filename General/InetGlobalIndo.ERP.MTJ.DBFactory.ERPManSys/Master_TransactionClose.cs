using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_TransactionClose
    {
        public Master_TransactionClose(Guid _prmTransCode, DateTime _prmStarDate, DateTime _prmEndDate, int _prmStatus, String _prmDescription)
        {
            this.TransCloseCode = _prmTransCode;
            this.StartDate = _prmStarDate;
            this.EndDate = _prmEndDate;
            this.Status = _prmStatus;
            this.Description = _prmDescription;
        }
    }
}
