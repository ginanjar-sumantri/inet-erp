using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;

namespace Windows_App
{
    class SerialPortInterface
    {
        //public SerialPortInterface() { }
        //public SerialPortInterface(Object Caller) { this._smsGatewayForm = (SMSGatewayForm)Caller; }

        //private SMSGatewayForm _smsGatewayForm;
        private const String FailedMessage = "Failed to receive data from phone.";
        private SerialPort _port;
        private AutoResetEvent receiveNow;

        public String OpenPort()
        {
            String _result = "Failed to Open Port.";
            try
            {
                if (_port == null)
                    _port = new SerialPort();
                if (!_port.IsOpen)
                {
                    receiveNow = new AutoResetEvent(false);
                    //_port.PortName = "COM1";
                    _port.PortName = GlobalVar.PortName;                 //COM1
                    _port.BaudRate = 115200;//115200                   //9600
                    _port.DataBits = 8;                   //8
                    _port.StopBits = StopBits.One;                  //1
                    _port.Parity = Parity.None;                     //None
                    _port.ReadTimeout = 3000;             //300
                    _port.WriteTimeout = 1000;           //300
                    _port.Encoding = Encoding.GetEncoding("iso-8859-1");
                    _port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                    _port.Open();
                    _port.DtrEnable = true;
                    _port.RtsEnable = true;
                }
                _result = "Port Open Success.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }
        public void ClosePort()
        {
            try
            {
                if (_port != null)
                    if (_port.IsOpen)
                    {
                        _port.Close();
                        _port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
                        _port = null;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean sendMessage(String _prmPhoneNo, String _prmMessage)
        {
            Boolean _result = false;
            if (_port != null)
            {
                if (!_port.IsOpen)
                    OpenPort();
            }
            else
            {
                OpenPort();
            }
            String recievedData = ExecCommand(_port, "AT", 300, "No phone connected");
            recievedData = ExecCommand(_port, "AT+CMGF=1", 300, "Failed to set message format.");
            //String command = "AT+CMGS=\"+622199287229\"";
            String command = "AT+CMGS=\"" + _prmPhoneNo + "\"";
            //recievedData = ExecCommand(_port, "AT+CMEE=1 ", 300, "enable see error");
            recievedData = ExecCommand(_port, command, 300, "Failed to accept phoneNo");
            //command = "Handy Gendut" + char.ConvertFromUtf32(26) + "\r";
            command = _prmMessage.Replace("\r", "") + char.ConvertFromUtf32(26) + "\r";
            recievedData = ExecCommand(_port, command, 10000, "Failed to send message");
            if (recievedData != FailedMessage) _result = true;

            ClosePort();

            return _result;
        }
        public List<MessagesBlock> ReadMessages()
        {
            List<MessagesBlock> _result = new List<MessagesBlock>();
            try
            {
                if (_port != null)
                {
                    if (!_port.IsOpen)
                        OpenPort();
                }
                else
                {
                    OpenPort();
                }

                ExecCommand(_port, "AT", 300, "No phone connected");
                // Use message format "Text mode"
                ExecCommand(_port, "AT+CMGF=1", 300, "Failed to set message format.");
                // Use character set "PCCP437"
                ExecCommand(_port, "AT+CSCS=\"PCCP437\"", 300, "Failed to set character set.");
                // Select SIM storage
                ExecCommand(_port, "AT+CPMS=\"SM\"", 300, "Failed to select message storage.");
                // Read the messages
                String input = ExecCommand(_port, "AT+CMGL=\"ALL\"", 5000, "Failed to read the messages.");
                if (input != FailedMessage)
                {
                    ClosePort();
                    return digestReadMessages(input);
                }
                else
                {
                    ClosePort();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Boolean deleteAllMessages()
        {
            if (_port != null)
            {
                if (!_port.IsOpen)
                    OpenPort();
            }
            else
            {
                OpenPort();
            }

            String input = ExecCommand(_port, "AT+CMGF=1", 300, "Failed to set message format.");
            input = ExecCommand(_port, "AT+CMGD=1,3", 5000, "Failed to delete messages.");

            ClosePort();

            return (input != FailedMessage) ? true : false;
        }
        public String cekSignal()
        {
            String _result = "";
            try
            {
                if (_port != null)
                {
                    if (!_port.IsOpen)
                        OpenPort();
                }
                else
                {
                    OpenPort();
                }

                String receivedData = ExecCommand(_port, "AT+CSQ", 300, "Failed to cek signal");
                if (receivedData == FailedMessage)
                {
                    _result = "0";
                }
                else if (receivedData.Contains("+CSQ:"))
                {
                    receivedData = receivedData.Substring(receivedData.IndexOf("+CSQ:") + 6);
                    _result = receivedData.Substring(0, receivedData.IndexOf("\r"));
                }
                else if (receivedData.Contains("+CSQ\r\r\n"))
                {
                    _result = "0";
                }

                ClosePort();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }
        public String cekPulsa(String _kodeCekPulsa)
        {
            if (_port != null)
            {
                if (!_port.IsOpen)
                    OpenPort();
            }
            else
            {
                OpenPort();
            }

            //String _result = this.ReadPulsa(_port, _smsGatewayForm._kodeCekPulsa, "Failed to read balance check.");
            String _result = this.ReadPulsa(_port, _kodeCekPulsa, "Failed to read balance check.");
            //_result = _result.Substring ( _result.IndexOf ( "2,\"" ) + 3  ) ;
            if (_result.IndexOf("\",15") == -1)
            {
                ClosePort();
                return "Failed to check balance.";
            }
            else
            {
                if (_result.Length > 12)
                    _result = _result.Substring(12);
                ClosePort();

                return _result.Substring(0, _result.IndexOf("\",15"));
            }
        }


        #region bridging Gateway and DB
        private dbConnector _dbConn;

        public void PortingMessageToDB()
        {
            if (_dbConn == null) _dbConn = new dbConnector();
            List<MessagesBlock> _messagesBlocks = ReadMessages();
            if (_messagesBlocks != null && _messagesBlocks.Count > 0)
            {
                foreach (MessagesBlock _singleMessage in _messagesBlocks)
                {
                    _dbConn.insertSMSGatewayReceive(_singleMessage);
                    String globalReplyMessage = _dbConn.snatchQuery("GlobalReplyMessage", "MsOrganization", "OrganizationID=" + GlobalVar.OrgID.ToString());
                    if (globalReplyMessage == null) globalReplyMessage = "";
                    String replyTo = _singleMessage.Sender.Substring(1, _singleMessage.Sender.Length - 2);
                    if ((replyTo.Length >= 8) && IsNumeric(replyTo.Substring(1)) && (replyTo.Substring(0, 1) == "+") && globalReplyMessage != "")
                        sendMessage(replyTo, globalReplyMessage);
                }
                deleteAllMessages();////////Delete after extracting messages
            }
        }

        public void SendingPushedMessages()
        {
            String MessageToSend = _dbConn.snatchQuery("id,DestinationPhoneNo,Message,OrganizationID,fgMasking", "SMSGatewaySend", "flagSend = 0 AND OrganizationID=" + GlobalVar.OrgID);
            String[] MessageFields = MessageToSend.Split('♀');//♀ = alt +12
            Int16 retry = 3;
            Boolean notSent = true;

            if (Convert.ToBoolean(MessageFields[4]))
            {

            }
            else
            {
                do
                {
                    notSent = !sendMessage(MessageFields[1], MessageFields[2]);
                }
                while ((retry-- > 0) && notSent);
                if (!notSent)
                    _dbConn.executeNonQuery("UPDATE SMSGatewaySend SET flagSend = 1, DateSent='" + DateTime.Now + "' WHERE id=" + MessageFields[0]);
                else
                    _dbConn.executeNonQuery("UPDATE SMSGatewaySend SET flagSend = 2 WHERE id=" + MessageFields[0]);
                //////flagSend = 1 -> Sent ; flagSend = 2 -> Failed to Send ;
            }
        }

        #endregion


        #region Private Methods
        private String ExecCommand(SerialPort port, string command, int responseTimeout, string errorMessage)
        {
            try
            {
                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNow.Reset();
                port.Write(command + "\r");

                String input = ReadResponse(port, responseTimeout);
                //if ((input.Length == 0) || ((!input.EndsWith("\r\n> ")) && (!input.EndsWith("\r\nOK\r\n"))))
                //    throw new ApplicationException("No success message was received.");
                return input;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(errorMessage, ex);
            }
        }
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (e.EventType == SerialData.Chars)
                    receiveNow.Set();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private String ReadResponse(SerialPort port, int timeout)
        {
            string buffer = string.Empty;
            try
            {
                do
                {
                    if (receiveNow.WaitOne(timeout, false))
                    {
                        string t = port.ReadExisting();
                        buffer += t;
                    }
                    else
                    {
                        return FailedMessage;
                    }
                    if (buffer.EndsWith("\r\nERROR\r\n"))
                        return FailedMessage;
                }
                while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\n> ") && !buffer.EndsWith("\r\nERROR\r\n"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return buffer;
        }
        private List<MessagesBlock> digestReadMessages(String _prmResponseMessages)
        {
            List<MessagesBlock> _result = new List<MessagesBlock>();
            try
            {
                String _hederMessage, _isiMessage;
                String[] _splittedHeader;
                while (_prmResponseMessages.IndexOf("+CMGL:") != -1)
                {
                    _prmResponseMessages = _prmResponseMessages.Substring(_prmResponseMessages.IndexOf("+CMGL:") + 6);
                    _hederMessage = _prmResponseMessages.Substring(0, _prmResponseMessages.IndexOf("\r\n"));
                    _prmResponseMessages = _prmResponseMessages.Substring(_prmResponseMessages.IndexOf("\r\n") + 2);
                    _isiMessage = _prmResponseMessages.Substring(0, _prmResponseMessages.IndexOf("\r\n"));

                    _splittedHeader = _hederMessage.Split(',');

                    MessagesBlock _singleMessage = new MessagesBlock();
                    _singleMessage.Index = _splittedHeader[0];
                    _singleMessage.Status = _splittedHeader[1];
                    _singleMessage.Sender = _splittedHeader[2].Replace("\'", "\'\'");
                    _singleMessage.Alphabet = _splittedHeader[3];
                    _singleMessage.Sent = _splittedHeader[4] + " " + _splittedHeader[5].Replace("\'", "\'\'").Substring(0, 8);
                    _singleMessage.Message = _isiMessage.Replace("\'", "\'\'");

                    _result.Add(_singleMessage);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }
        private Boolean IsNumeric(String _prmString)
        {
            Boolean _result = false;
            if (_prmString.Length > 0) _result = true;
            for (int i = 0; i < _prmString.Length; i++)
                if (_prmString[i] < '0' || _prmString[i] > '9') return false;
            return _result;
        }
        private String ReadPulsa(SerialPort port, String nomorCekPulsa, String errorMessage)
        {
            String buffer = String.Empty;
            try
            {
                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNow.Reset();
                port.Write("AT+CUSD=1,\"" + nomorCekPulsa + "\",15\r");

                do
                {
                    if (receiveNow.WaitOne(300, false))
                    {
                        string t = port.ReadExisting();
                        buffer += t;
                    }
                    else
                    {
                        return FailedMessage;
                    }
                    if (buffer.EndsWith("\r\nERROR\r\n"))
                        return FailedMessage;
                }
                while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\nERROR\r\n"));
                buffer = String.Empty;
                do
                {
                    if (receiveNow.WaitOne(6700, false))
                    {
                        string t = port.ReadExisting();
                        buffer += t;
                    }
                    else
                    {
                        return FailedMessage;
                    }
                    if (buffer.EndsWith("\r\nERROR\r\n"))
                        return FailedMessage;
                }
                while (!buffer.EndsWith("\r\n"));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(errorMessage, ex);
            }
            return buffer;
        }
        #endregion


        ~SerialPortInterface() { }
    }
}
