using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTDocServiceDt
    {
        private string _unitName = "";
        private string _serviceName = "";
        private string _serviceTypeName = "";

        public PORTDocServiceDt(Guid _prmDtDocServiceCode, Guid _prmHdDocServiceCode, Guid _prmServiceCode, string _prmServiceName, string _prmServiceTypeName, decimal? _prmPrice, decimal? _prmQty, decimal? _prmTotalPay, string _prmUnitCode, string _prmUnitName)
        {
            this.DtDocServiceCode = _prmDtDocServiceCode;
            this.HdDocServiceCode = _prmHdDocServiceCode;
            this.ServiceCode = _prmServiceCode;
            this.ServiceName = _prmServiceName;
            this.ServiceTypeName = _prmServiceTypeName;
            this.Price = _prmPrice;
            this.Qty = _prmQty;
            this.TotalPay = _prmTotalPay;
            this.UnitCode = _prmUnitCode;
            this.UnitName = _prmUnitName;
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

        public string UnitName
        {
            get
            {
                return this._unitName;
            }
            set
            {
                this._unitName = value;
            }
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
