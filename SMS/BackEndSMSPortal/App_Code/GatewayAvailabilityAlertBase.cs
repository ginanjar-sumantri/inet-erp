using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMS;
using SMSLibrary;

namespace SMS.BackEndSMSPortal
{
    public abstract class GatewayAvailabilityAlertBase : AdminSMSWebBase
    {
        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;
        protected String Leverage = "";

        public GatewayAvailabilityAlertBase()
        {   
        }
        ~GatewayAvailabilityAlertBase()
        {
        }
    }
}
