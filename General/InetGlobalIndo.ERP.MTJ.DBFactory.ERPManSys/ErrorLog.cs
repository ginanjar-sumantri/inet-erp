using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class ErrorLog
    {
        public ErrorLog(Guid _prmCode, DateTime _prmDate, String _prmErrorMessage, String _prmStackMessage, String _prmUserName, String _prmMethodCall, String _prmTransType)
        {
            this.Code = _prmCode;
            this.Date = _prmDate;
            this.ErrorMessage = _prmErrorMessage;
            this.StackMessage = _prmStackMessage;
            this.UserName = _prmUserName;
            this.MethodCall = _prmMethodCall;
            this.TransType = _prmTransType;
        }
    }
}
