using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSLibrary
{
    public partial class SMSGatewayReceive
    {
        string _CategoryText = "";
        string _custID = "";
        string _custName = "";

        public SMSGatewayReceive(String _prmCategory, String _prmCategoryText)
        {
            this.Category = _prmCategory;
            this.CategoryText = _prmCategoryText;
        }

        public SMSGatewayReceive(long _prmID, String _prmCategory, String _prmSenderPhoneNo, String _prmMessage,
            Byte _prmFlagRead, String _prmuserID, long _prmReplyId)
        {
            this.id = _prmID;
            this.Category = _prmCategory;
            this.SenderPhoneNo = _prmSenderPhoneNo;
            this.Message = _prmMessage;
            this.flagRead = _prmFlagRead;
            this.userID = _prmuserID;
            this.ReplyId = _prmReplyId;
        }

        public SMSGatewayReceive(long _prmID, String _prmCategory, String _prmSenderPhoneNo, String _prmMessage,
            Byte _prmFlagRead, String _prmuserID, long _prmReplyId, String _prmCustID, String _prmCustName, DateTime _prmDateReceived)
        {
            this.id = _prmID;
            this.Category = _prmCategory;
            this.SenderPhoneNo = _prmSenderPhoneNo;
            this.Message = _prmMessage;
            this.flagRead = _prmFlagRead;
            this.userID = _prmuserID;
            this.ReplyId = _prmReplyId;
            this.CustID = _prmCustID;
            this.CustName = _prmCustName;
            this.DateReceived = _prmDateReceived;
        }

        public String CategoryText
        {
            get
            {
                return this._CategoryText;
            }
            set
            {
                this._CategoryText = value;
            }
        }

        public String CustID
        {
            get
            {
                return this._custID;
            }
            set
            {
                this._custID = value;
            }
        }

        public String CustName
        {
            get
            {
                return this._custName;
            }
            set
            {
                this._custName = value;
            }
        }
    }

}
