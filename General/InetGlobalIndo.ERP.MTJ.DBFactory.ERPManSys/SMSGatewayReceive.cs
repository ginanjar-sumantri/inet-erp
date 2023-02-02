using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class SMSGatewayReceive
    {
        string _prefixMessageText = "";
        string _custID = "";
        string _custName = "";

        public SMSGatewayReceive(String _prmPrefixMessage, String _prmPrefixMessageText)
        {
            this.PrefixMessage = _prmPrefixMessage;
            this.PrefixMessageText = _prmPrefixMessageText;
        }

        public SMSGatewayReceive(long _prmID, String _prmPrefixMessage, String _prmSenderPhoneNo, String _prmMessage,
            Byte _prmFlagRead, String _prmUserName, long _prmReplyId)
        {
            this.id = _prmID;
            this.PrefixMessage = _prmPrefixMessage;
            this.SenderPhoneNo = _prmSenderPhoneNo;
            this.Message = _prmMessage;
            this.flagRead = _prmFlagRead;
            this.userName = _prmUserName;
            this.ReplyId = _prmReplyId;
        }

        public SMSGatewayReceive(long _prmID, String _prmPrefixMessage, String _prmSenderPhoneNo, String _prmMessage,
            Byte _prmFlagRead, String _prmUserName, long _prmReplyId, String _prmCustID, String _prmCustName, DateTime _prmDateReceived)
        {
            this.id = _prmID;
            this.PrefixMessage = _prmPrefixMessage;
            this.SenderPhoneNo = _prmSenderPhoneNo;
            this.Message = _prmMessage;
            this.flagRead = _prmFlagRead;
            this.userName = _prmUserName;
            this.ReplyId = _prmReplyId;
            this.CustID = _prmCustID;
            this.CustName = _prmCustName;
            this.DateReceived = _prmDateReceived;
        }

        public String PrefixMessageText
        {
            get
            {
                return this._prefixMessageText;
            }
            set
            {
                this._prefixMessageText = value;
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
