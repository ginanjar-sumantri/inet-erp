using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTMsServiceTypeDt
    {
        private string _accARServiceName = "";
        private string _accDiscServiceName = "";
        private string _accDPServiceName = "";
        private string _accPPNServiceName = "";
        private string _accServRevenueName = "";
        private string _accFreightOutServiceName = "";
        private string _accInsuranceServiceName = "";

        public PORTMsServiceTypeDt(Guid _prmServiceTypeCode, string _prmCurrCode, string _prmAccARServiceName, string _prmAccDiscServiceName, string _prmAccDPServiceName, string _prmAccPPNServiceName, string _prmAccServRevenueName, string _prmAccFreightOutServiceName, string _prmAccInsuranceServiceName)
        {
            this.ServiceTypeCode = _prmServiceTypeCode;
            this.CurrCode = _prmCurrCode;
            this.AccARServiceName = _prmAccARServiceName;
            this.AccDiscServiceName = _prmAccDiscServiceName;
            this.AccDPServiceName = _prmAccDPServiceName;
            this.AccPPNServiceName = _prmAccPPNServiceName;
            this.AccServRevenueName = _prmAccServRevenueName;
            this.AccFreightOutServiceName = _prmAccFreightOutServiceName;
            this.AccInsuranceServiceName = _prmAccInsuranceServiceName;
        }

        public string AccARServiceName
        {
            get
            {
                return this._accARServiceName;
            }
            set
            {
                this._accARServiceName = value;
            }
        }

        public string AccDiscServiceName
        {
            get
            {
                return this._accDiscServiceName;
            }
            set
            {
                this._accDiscServiceName = value;
            }
        }

        public string AccDPServiceName
        {
            get
            {
                return this._accDPServiceName;
            }
            set
            {
                this._accDPServiceName = value;
            }
        }

        public string AccPPNServiceName
        {
            get
            {
                return this._accPPNServiceName;
            }
            set
            {
                this._accPPNServiceName = value;
            }
        }

        public string AccServRevenueName
        {
            get
            {
                return this._accServRevenueName;
            }
            set
            {
                this._accServRevenueName = value;
            }
        }

        public string AccFreightOutServiceName
        {
            get
            {
                return this._accFreightOutServiceName;
            }
            set
            {
                this._accFreightOutServiceName = value;
            }
        }

        public string AccInsuranceServiceName
        {
            get
            {
                return this._accInsuranceServiceName;
            }
            set
            {
                this._accInsuranceServiceName = value;
            }
        }
    }
}
