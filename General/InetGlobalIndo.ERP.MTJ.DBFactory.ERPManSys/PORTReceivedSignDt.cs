using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTReceivedSignDt
    {
        private string _productName = "";

        public PORTReceivedSignDt(Guid _prmHdReceivedSignCode, Guid _prmDtReceivedSignCode, string _prmProductCode, string _prmProductName,
                        string _prmRemark, decimal _prmAmount, string _prmUnitCode)
        {
            this.HdReceivedSignCode = _prmHdReceivedSignCode;
            this.DtReceivedSignCode = _prmDtReceivedSignCode;
            this.ProductCode = _prmProductCode;
            this.ProductName = _prmProductName;
            this.Remark = _prmRemark;
            this.Amount = _prmAmount;
            this.UnitCode = _prmUnitCode;
        }

        public string ProductName
        {
            get
            {
                return this._productName;
            }

            set
            {
                this._productName = value;
            }
        }
    }
}
