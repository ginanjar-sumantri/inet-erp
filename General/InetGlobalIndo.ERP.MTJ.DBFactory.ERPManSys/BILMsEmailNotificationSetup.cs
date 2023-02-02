using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class BILMsEmailNotificationSetup
    {
        public BILMsEmailNotificationSetup(Byte _prmID, Byte _prmNotificationType, String _prmEmailFrom, 
            String _prmEmailTo, String _prmSubject, String _prmBodyMessage)
        {
            this.ID = _prmID;
            this.NotificationType = _prmNotificationType;
            this.EmailFrom = _prmEmailFrom;
            this.EmailTo = _prmEmailTo;
            this.Subject = _prmSubject;
            this.BodyMessage = _prmBodyMessage;
        }
    }
}
