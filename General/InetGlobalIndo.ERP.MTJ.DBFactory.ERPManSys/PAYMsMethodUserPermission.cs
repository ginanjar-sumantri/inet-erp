using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYMsMethodUserPermission
    {
        private string _methodName = "";

        public PAYMsMethodUserPermission(string _prmMethodCode, string _prmMethodName, string _prmUsername)
        {
            this.MethodCode = _prmMethodCode;
            this.MethodName = _prmMethodName;
            this.Username = _prmUsername;
        }

        public string MethodName
        {
            get
            {
                return this._methodName;
            }
            set
            {
                this._methodName = value;
            }
        }
    }
}
