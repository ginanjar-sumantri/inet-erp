using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class SMSGatewaySend
    {
        string _prmCategoryText = "";

        public SMSGatewaySend(String _prmCategory, String _prmCategoryText)
        {
            this.Category = _prmCategory;
            this.CategoryText = _prmCategoryText;
        }

        public SMSGatewaySend(long _prmID, String _prmCategory, String _prmDestinationPhoneNo, String _prmMessage,
            Byte _prmFlagSend, String _prmUserName, DateTime _prmDateSent)
        {
            this.id = _prmID;
            this.Category = _prmCategory;
            this.DestinationPhoneNo = _prmDestinationPhoneNo;
            this.Message = _prmMessage;
            this.flagSend = _prmFlagSend;
            this.userName = _prmUserName;
            this.DateSent = _prmDateSent;
        }

        public String CategoryText
        {
            get
            {
                return this._prmCategoryText;
            }
            set
            {
                this._prmCategoryText = value;
            }
        }
    }
}
