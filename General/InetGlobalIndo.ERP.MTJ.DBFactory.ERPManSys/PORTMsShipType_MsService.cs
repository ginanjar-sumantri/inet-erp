using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTMsShipType_MsService
    {
        private string _serviceName = "";

        public PORTMsShipType_MsService(Guid _prmShipTypeCode, Guid _prmServiceCode, string _prmServiceName)
        {
            this.ShipTypeCode = _prmShipTypeCode;
            this.ServiceCode = _prmServiceCode;
            this.ServiceName = _prmServiceName;
        }

        public string ServiceName
        {
            get
            {
                return this._serviceName;
            }
            set
            {
                this._serviceName = value;
            }
        }
    }
}
