using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsAccGroup
    {
        private string _accTypeName = "";

        public MsAccGroup(string _prmAccGroupCode, string _prmAccGroupName, char _prmAccType, string _prmAccTypeName)
        {
            this.AccGroupCode = _prmAccGroupCode;
            this.AccGroupName = _prmAccGroupName;
            this.AccType = _prmAccType;
            this.AccTypeName = _prmAccTypeName;
        }

        public MsAccGroup(string _prmAccGroupCode, string _prmAccGroupName)
        {
            this.AccGroupCode = _prmAccGroupCode;
            this.AccGroupName = _prmAccGroupName;
        }

        public string AccTypeName
        {
            get
            {
                return this._accTypeName;
            }

            set
            {
                this._accTypeName = value;
            }
        }
    }
}
