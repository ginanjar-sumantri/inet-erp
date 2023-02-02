using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SMSLibrary;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;

namespace SMSLibrary
{
    public class dbConnector
    {
        public dbConnector()
        {
            _con = new SqlConnection();
            _command = new SqlCommand();
        }

        private SqlConnection _con;
        private SqlCommand _command;

        #region basic connection

        public Boolean OpenConnection()
        {
            Boolean _result = false;
            try
            {
                if (_con.State == ConnectionState.Open)
                    _con.Close();
                _con.ConnectionString = ApplicationConfig.SMSPortalConnectionString;
                _con.Open();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }
        public Boolean OpenConnection(String _prmConnectionString)
        {
            Boolean _result = false;
            try
            {
                if (_con.State == ConnectionState.Open)
                    _con.Close();
                _con.ConnectionString = _prmConnectionString;
                _con.Open();
                _result = true;
            }
            catch (Exception ex)
            {
                return _result;
                //throw ex;
            }
            return _result;
        }

        public List<String> executeQuery(String _prmQuery)
        {
            List<String> _result = new List<string>();
            try
            {
                if (_con.State != ConnectionState.Open)
                    OpenConnection(GlobalVar.ConnString);
                this._command = new SqlCommand(_prmQuery, this._con);
                SqlDataReader _dataReader = this._command.ExecuteReader();
                while (_dataReader.Read())
                {
                    String _implodedRow = "";
                    for (int i = 0; i < _dataReader.FieldCount; i++)
                        _implodedRow += "♀" + ((_dataReader.GetValue(i).ToString() == "") ? "" : _dataReader.GetValue(i).ToString());
                    _result.Add(_implodedRow.Substring(1));
                }//♀ = alt +12
                _dataReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }
        public List<String> executeQuery(String _prmQuery,String _prmConnectionString)
        {
            List<String> _result = new List<string>();
            try
            {
                OpenConnection(_prmConnectionString);

                this._command = new SqlCommand(_prmQuery, this._con);
                SqlDataReader _dataReader = this._command.ExecuteReader();
                while (_dataReader.Read())
                {
                    String _implodedRow = "";
                    for (int i = 0; i < _dataReader.FieldCount; i++)
                        _implodedRow += "♀" + ((_dataReader.GetValue(i).ToString() == "") ? "" : _dataReader.GetValue(i).ToString());
                    _result.Add(_implodedRow.Substring(1));
                }//♀ = alt +12
                _dataReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public void executeNonQuery(String _prmQuery)
        {
            try
            {
                if (_con.State != ConnectionState.Open)
                    OpenConnection(GlobalVar.ConnString);
                this._command = new SqlCommand(_prmQuery, this._con);
                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void executeNonQuery(String _prmQuery,String _prmConnectionString)
        {
            try
            {
                OpenConnection(_prmConnectionString);
                this._command = new SqlCommand(_prmQuery, this._con);
                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean cekExistingData(String _prmTableName, String _prmFieldName, String _prmValue)
        {
            Boolean _result = false;
            try
            {
                if (_con.State != ConnectionState.Open)
                    OpenConnection(GlobalVar.ConnString);
                this._command = new SqlCommand("SELECT COUNT(*) AS dataCount FROM " + _prmTableName + " WHERE " + _prmFieldName + " = " + _prmValue, this._con);
                SqlDataReader _dataReader = _command.ExecuteReader();
                _dataReader.Read();
                _result = (_dataReader.GetInt32(0) > 0);
                _dataReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String snatchQuery(String _prmFields, String _prmTableName)
        {
            String _result = "";
            try
            {
                if (_con.State != ConnectionState.Open)
                    OpenConnection(GlobalVar.ConnString);
                this._command = new SqlCommand("SELECT TOP 1 " + _prmFields + " FROM " + _prmTableName, this._con);
                String _implodedRow = "";
                SqlDataReader _dataReader = this._command.ExecuteReader();
                if (_dataReader.Read())
                    for (int i = 0; i < _dataReader.FieldCount; i++)
                        _implodedRow += "♀" + ((_dataReader.GetValue(i).ToString() == "") ? "" : _dataReader.GetValue(i).ToString());
                if (_implodedRow.Length > 1)
                    _result = _implodedRow.Substring(1);
                _dataReader.Close();
            }//♀ = alt +12
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }
        public String snatchQuery(String _prmFields, String _prmTableName, String _prmCondition)
        {
            String _result = "";
            try
            {
                if (_con.State != ConnectionState.Open)
                    OpenConnection(GlobalVar.ConnString);
                if (_con.State == ConnectionState.Open)
                {
                    this._command = new SqlCommand("SELECT TOP 1 " + _prmFields + " FROM " + _prmTableName + " WHERE " + _prmCondition, this._con);
                    String _implodedRow = "";
                    SqlDataReader _dataReader = this._command.ExecuteReader();
                    if (_dataReader.Read())
                        for (int i = 0; i < _dataReader.FieldCount; i++)
                            _implodedRow += "♀" + ((_dataReader.GetValue(i).ToString() == "") ? "" : _dataReader.GetValue(i).ToString());
                    if (_implodedRow.Length > 1)
                        _result = _implodedRow.Substring(1);
                    _dataReader.Close();
                }
            }//♀ = alt +12
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }


        public void CloseConnection() { if (_con.State == ConnectionState.Open) _con.Close(); }

        #endregion

        #region SMSGateway DB bridge

        public void insertSMSGatewayReceive(MessagesBlock _prmMessage)
        {
            if (_con.State != ConnectionState.Open)
                OpenConnection(GlobalVar.ConnString);
            //String _prefixMessage = "";
            //if (_prmMessage.Message.IndexOf('#') > 0 && _prmMessage.Message.IndexOf('#') < 30)
            //    _prefixMessage = _prmMessage.Message.Substring(0, _prmMessage.Message.IndexOf('#'));
            String _senderPhoneNo = _prmMessage.Sender;
            String _message = _prmMessage.Message;
            String _category = "";            
            if (Convert.ToInt16(snatchQuery("count(*)", "TrAutoReply", "KeyWord like '" + _message + "' AND PhoneNumber='" + (_senderPhoneNo.Substring(1, _senderPhoneNo.Length - 2)) + "'")) > 0)
            {
                _category = _message;
                String _autoReplyData = snatchQuery("ReplyMessage", "TrAutoReply", "RTRIM(LTRIM(UPPER(KeyWord))) like '" + _message.ToUpper().Trim() + "' AND PhoneNumber='" + (_senderPhoneNo.Substring(1, _senderPhoneNo.Length - 2)) + "'");
                executeNonQuery("INSERT INTO SMSGatewaySend ( Category, DestinationPhoneNo, Message, flagSend,userID, OrganizationID, fgMasking ) VALUES ('AutoReply','" + (_senderPhoneNo.Substring(1, _senderPhoneNo.Length - 2)) + "','" + _autoReplyData + "',0,'" + GlobalVar.UserID + "'," + GlobalVar.OrgID + ", false)");
            }
            if (!cekExistingData("SMSGatewayReceive", "Message", "'" + _prmMessage.Message + "'"))
                executeNonQuery("INSERT INTO SMSGatewayReceive ( Category, SenderPhoneNo, Message, flagRead,userID, dateReceived,OrganizationID ) VALUES ('" + _category + "','" + (_senderPhoneNo.Substring(1, _senderPhoneNo.Length - 2)) + "','" + _message + "',0,'" + GlobalVar.UserID + "', '" + DateTime.Now.ToString("MM/dd/yyyy") + "'," + GlobalVar.OrgID + ")");
        }

        #endregion

        ~dbConnector() { }

        internal String isConnected()
        {
            return ""; //command baris ini kalau mau pakai cek TCP CLient ( terakhir dicoba masih actively refused by Server ).
            try
            {
                IPHostEntry IPHost = new IPHostEntry();
                IPHost = Dns.GetHostEntry(ApplicationConfig.ServerIP);

                IPAddress IPAddr = IPHost.AddressList[0];
                
                TcpClient TcpCli = new TcpClient();
                TcpCli.Connect(IPAddr, 1433);
                TcpCli.Close();

                /////////////////////////////
                //PingReply pingReply;
                //using (var ping = new Ping())
                //    pingReply = ping.Send(ApplicationConfig.ServerIP,1000);
                //var available = pingReply.Status == IPStatus.Success;
                //if (available)
                //{
                /////////////////////////////
                    if (_con.State == ConnectionState.Closed)
                        this.OpenConnection();
                    return "";
                /////////////////////////////
                //}
                //else 
                //{
                //    return false;
                //}
                /////////////////////////////
            }
            catch ( Exception ex)
            {
                return ex.Message + ApplicationConfig.ServerIP ;
            }
        }
    }
}
