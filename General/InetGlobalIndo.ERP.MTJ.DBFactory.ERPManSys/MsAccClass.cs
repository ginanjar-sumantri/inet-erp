using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsAccClass : Base
    {
        private string _accSubGroupName = "";

        public MsAccClass(string _prmClassCode, string _prmClassName, string _prmSubGroup, string _prmAccSubGroupName)
        {
            this.AccClassCode = _prmClassCode;
            this.AccClassName = _prmClassName;
            this.AccSubGroup = _prmSubGroup;
            this.AccSubGroupName = _prmAccSubGroupName;
        }

        public MsAccClass(string _prmClassCode, string _prmClassName, string _prmSubGroup, string _prmUserID, DateTime _prmUserDate)
        {
            this.AccClassCode = _prmClassCode;
            this.AccClassName = _prmClassName;
            this.AccSubGroup = _prmSubGroup;
            this.UserID = _prmUserID;
            this.UserDate = _prmUserDate;
        }

        public MsAccClass(string _prmClassCode, string _prmClassName)
        {
            this.AccClassCode = _prmClassCode;
            this.AccClassName = _prmClassName;
        }

        public string AccSubGroupName
        {
            get
            {
                return this._accSubGroupName;
            }
            set
            {
                this._accSubGroupName = value;
            }
        }
    }
}
