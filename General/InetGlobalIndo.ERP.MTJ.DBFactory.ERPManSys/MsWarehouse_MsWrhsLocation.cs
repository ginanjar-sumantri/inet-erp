using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class MsWarehouse_MsWrhsLocation
    {
        private string _wLocName = "";

        public MsWarehouse_MsWrhsLocation(string _prmWrhsCode, string _prmWLocationCode, string _prmWLocationName)
        {
            this.WrhsCode = _prmWrhsCode;
            this.WLocation = _prmWLocationCode;
            this.WLocationName = _prmWLocationName;
        }

        public string WLocationName
        {
            get
            {
                return this._wLocName;
            }
            set
            {
                this._wLocName = value;
            }
        }
    }
}
