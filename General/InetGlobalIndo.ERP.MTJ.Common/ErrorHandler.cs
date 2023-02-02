using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace InetGlobalIndo.ERP.MTJ.Common
{
    public static class ErrorHandler
    {
        public static void Record(Exception _prmException, EventLogEntryType _prmEntryType)
        {
            //EventLog.WriteEntry(_prmException.Source, _prmException.Message, _prmEntryType);
        }

        public static void Record(Exception _prmException, EventLogEntryType _prmEntryType, string _prmErrMessage)
        {
            //EventLog.WriteEntry(_prmException.Source, _prmException.Message + "\nStored Procedure Message : " + _prmErrMessage, _prmEntryType);
        }
    }
}