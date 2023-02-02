using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSLibrary
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
            Byte _prmFlagSend, String _prmuserID, DateTime? _prmDateSent)
        {
            this.id = _prmID;
            this.Category = _prmCategory;
            this.DestinationPhoneNo = _prmDestinationPhoneNo;
            this.Message = _prmMessage;
            this.flagSend = _prmFlagSend;
            this.userID = _prmuserID;
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
