using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSLibrary
{
    public static class ApplicationConfig
    {
        private static String _smsPortalConnectionString = Properties.Settings.Default.SMSPortalConnectionString ;
        public static String SMSPortalConnectionString {get { return _smsPortalConnectionString; }}

        private static String _serverIP = Properties.Settings.Default.ServerIP;
        public static String ServerIP { get { return _serverIP; } }

        private static String _encryptionKey = Properties.Settings.Default.EncryptionKey;
        public static String EncryptionKey { get { return _encryptionKey; }}

        private static String _listPageSize = Properties.Settings.Default.ListPageSize;
        public static String ListPageSize { get { return _listPageSize; } }

        private static String _dataPagerRange = Properties.Settings.Default.DataPagerRange;
        public static String DataPagerRange { get { return _dataPagerRange; } }

        private static String _actionResult = Properties.Settings.Default.ActionResult;
        public static String ActionResult { get { return _actionResult; } }

        private static String _dateForm = Properties.Settings.Default.DateForm;
        public static String DateForm { get { return _dateForm; } }

        private static String _requestPageKey = Properties.Settings.Default.RequestPageKey;
        public static String RequestPageKey { get { return _requestPageKey; } }
    }
}
