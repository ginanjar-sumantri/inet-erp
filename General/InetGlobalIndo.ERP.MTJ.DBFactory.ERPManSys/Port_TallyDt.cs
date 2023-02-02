using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Port_TallyDt
    {
        private string _wrhsName = "";
        private string _locationName = "";

        public Port_TallyDt(Guid _prmTallyDtCode, Guid _prmTallyHdCode, string _prmWrhsCode, string _prmWrhsName, string _prmLocationCode, string _prmLocationName, string _prmRemark)
        {
            this.TallyDtCode =  _prmTallyDtCode;
            this.TallyHdCode = _prmTallyHdCode;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
            this.LocationCode = _prmLocationCode;
            this.LocationName = _prmLocationName;
            this.Remark = _prmRemark;
        }

        public Port_TallyDt(Guid _prmTallyDtCode, string _prmWrhsName)
        {
            this.TallyDtCode = _prmTallyDtCode;
            this.WrhsName = _prmWrhsName;
        }

        public string WrhsName
        {
            get
            {
                return this._wrhsName;
            }
            set
            {
                this._wrhsName = value;
            }
        }

        public string LocationName
        {
            get
            {
                return this._locationName;
            }
            set
            {
                this._locationName = value;
            }
        }
    }
}
