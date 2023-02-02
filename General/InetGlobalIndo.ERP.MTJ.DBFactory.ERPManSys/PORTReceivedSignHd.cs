using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PORTReceivedSignHd
    {
        private string _productSubGroupName = "";

        public PORTReceivedSignHd(Guid _prmHdReceivedSignCode, string _prmFormID, string _prmProductSubGroupCode,
                        string _prmProductSubGroupName, string _prmReceivedFrom, DateTime _prmDate)
        {
            this.HdReceivedSignCode = _prmHdReceivedSignCode;
            this.FormID = _prmFormID;
            this.ProductSubGroupCode = _prmProductSubGroupCode;
            this.ProductSubGroupName = _prmProductSubGroupName;
            this.ReceivedFrom = _prmReceivedFrom;
            this.Date = _prmDate;
        }

        public string ProductSubGroupName
        {
            get
            {
                return this._productSubGroupName;
            }

            set
            {
                this._productSubGroupName = value;
            }
        }
    }
}
