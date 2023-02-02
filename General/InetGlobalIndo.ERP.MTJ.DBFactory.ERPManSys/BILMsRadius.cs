using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class BILMsRadius
    {
        string _custName = "";

        public BILMsRadius(String _prmRadiusCode, String _prmRadiusName, String _prmCustCode, String _prmCustName, 
            String _prmRadiusIP, String _prmRadiusUserName, String _prmRadiusPwd, String _prmRadiusDbName)
        {
            this.RadiusCode = _prmRadiusCode;
            this.RadiusName = _prmRadiusName;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.RadiusIP = _prmRadiusIP;
            this.RadiusUserName = _prmRadiusUserName;
            this.RadiusPwd = _prmRadiusPwd;
            this.RadiusDbName = _prmRadiusDbName;
        }

        public BILMsRadius(String _prmRadiusCode, String _prmRadiusName)
        {
            this.RadiusCode = _prmRadiusCode;
            this.RadiusName = _prmRadiusName;
        }

        public string CustName
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
