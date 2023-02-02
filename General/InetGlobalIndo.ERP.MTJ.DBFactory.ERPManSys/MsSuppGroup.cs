using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsSuppGroup
    {
        private string _suppGroup = "";

        public MsSuppGroup(string _prmSuppGroupCode, string _prmSuppGroupName, string _prmSuppGroup)
        {
            this.SuppGroupCode = _prmSuppGroupCode;
            this.SuppGroupName = _prmSuppGroupName;
            this.SubGroup = _prmSuppGroup;
        }

        public MsSuppGroup(string _prmSuppGroupCode, string _prmSuppGroupName)
        {
            this.SuppGroupCode = _prmSuppGroupCode;
            this.SuppGroupName = _prmSuppGroupName;
        }

        public string SubGroup
        {
            get
            {
                return this._suppGroup;
            }
            set
            {
                this._suppGroup = value;
            }
        }
    }
}
