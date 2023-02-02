using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windows_App
{
    static class GlobalVar
    {
        private static String _connString, _userID,_portName;
        private static Int32 _orgID;
        private static Boolean _fgAdmin;

        public static String ConnString {
            get {return _connString;}
            set {_connString = value ;}
        }
        public static String UserID {
            get { return _userID; }
            set { _userID = value; }
        }
        public static String PortName {
            get { return _portName; }
            set { _portName = value; }
        }
        public static Int32 OrgID {
            get { return _orgID; }
            set { _orgID = value; }
        }
        public static Boolean FgAdmin {
            get { return _fgAdmin; }
            set { _fgAdmin = value; }
        }
    }
}
