using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace VTSWeb.Common
{
    public static class GetConString
    {
        public static string GetConnString(String _prmDatabase)
        {
            //string _instanceName = "";
            string _dbName = "";
            string _serverName = "";
            String _result = "";
            string _userID = "";
            string _password = "";
            RegistryKey rk = Registry.LocalMachine;

            if (_prmDatabase != "")
            {
                //ambil data dari registry development
                RegistryKey sk1 = rk.OpenSubKey(@"SOFTWARE\SimpleTaskList\" + _prmDatabase);
                //_instanceName = sk1.GetValue("InstanceName").ToString();
                _dbName = sk1.GetValue("dbName").ToString();
                _serverName = sk1.GetValue("ServerName").ToString();
                _userID = sk1.GetValue("UserID").ToString();
                _password = sk1.GetValue("UserPwd").ToString();
                _result = "Database=" + _dbName + "; Server=" + _serverName + "; UID=" + _userID + "; PWD=" + _password + ";";
            }

            return _result;

        }

        private static String _connString = "";

        public static String ConnString
        {
            set
            {
                _connString = value;
            }
            get
            {
                return _connString;
            }
        }
    }
}
