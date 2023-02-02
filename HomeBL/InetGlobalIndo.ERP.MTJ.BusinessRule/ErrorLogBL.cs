using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Diagnostics;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule
{
    public sealed class ErrorLogBL : Base
    {
        #region ErrorLogBL

        public ErrorLogBL()
        {
        }

        public String CreateErrorLog(String _prmErrorMessage, String _prmStackMessage, String _prmUserName, String _prmMethodCall, String _prmTransType)
        {
            String _result = "";

            try
            {
                ErrorLog _errorlog = new ErrorLog();

                _errorlog.Code = Guid.NewGuid();
                _errorlog.Date = DateTime.Now;
                _errorlog.ErrorMessage = _prmErrorMessage;
                _errorlog.StackMessage = _prmStackMessage;
                _errorlog.UserName = _prmUserName;
                _errorlog.MethodCall = _prmMethodCall;
                _errorlog.TransType = _prmTransType;

                this.dbGeneral.ErrorLogs.InsertOnSubmit(_errorlog);

                this.dbGeneral.SubmitChanges();

                _result = _errorlog.Code.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
                //String _ErrorCode  = new ErrorLogBL(

                //String _ErrorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "ErrorLog", "Error");
                _result = ApplicationConfig.Error + " You Failed Add Data. For futher information please contact your system Administrator. Code Case : ";// +_ErrorCode;
            }

            return _result;
        }

        ~ErrorLogBL()
        {
        }

        #endregion
    }
}
