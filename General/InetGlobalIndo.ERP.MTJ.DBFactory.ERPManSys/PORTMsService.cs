using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTMsService
    {
        private string _serviceTypeName = "";

        public PORTMsService(Guid _prmServiceCode, string _prmServiceTypeName, string _prmServiceName, int _prmPriority)
        {
            this.ServiceCode = _prmServiceCode;
            this.ServiceTypeName = _prmServiceTypeName;
            this.ServiceName = _prmServiceName;
            this.Priority = _prmPriority;
        }

        public PORTMsService(Guid _prmServiceCode, string _prmServiceName)
        {
            this.ServiceCode = _prmServiceCode;
            this.ServiceName = _prmServiceName;
        }

        public string ServiceTypeName
        {
            get
            {
                return this._serviceTypeName;
            }
            set
            {
                this._serviceTypeName = value;
            }
        }
    }
}
