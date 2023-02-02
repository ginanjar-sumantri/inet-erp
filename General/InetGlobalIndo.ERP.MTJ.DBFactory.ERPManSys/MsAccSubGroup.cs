using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsAccSubGroup
    {
        private string _accGroupName = "";

        public MsAccSubGroup(string _prmAccSubGroupCode, string _prmAccSubGroupName, string _prmAccGroup, string _prmAccGroupName)
        {
            this.AccSubGroupCode = _prmAccSubGroupCode;
            this.AccSubGroupName = _prmAccSubGroupName;
            this.AccGroup = _prmAccGroup;
            this.AccGroupName = _prmAccGroupName;
        }

        public MsAccSubGroup(string _prmAccSubGroupCode, string _prmAccSubGroupName, string _prmAccGroup, string _prmUserID, DateTime _prmUserDate)
        {
            this.AccSubGroupCode = _prmAccSubGroupCode;
            this.AccSubGroupName = _prmAccSubGroupName;
            this.AccGroup = _prmAccGroup;
            this.UserID = _prmUserID;
            this.UserDate = _prmUserDate;
        }

        public MsAccSubGroup(string _prmAccSubGroupCode, string _prmAccSubGroupName)
        {
            this.AccSubGroupCode = _prmAccSubGroupCode;
            this.AccSubGroupName = _prmAccSubGroupName;
        }

        public string AccGroupName
        {
            get
            {
                return this._accGroupName;
            }
            set
            {
                this._accGroupName = value;
            }
        }
    }
}
