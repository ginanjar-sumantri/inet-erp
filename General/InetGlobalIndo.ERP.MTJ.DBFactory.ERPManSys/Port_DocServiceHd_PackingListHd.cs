using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Port_DocServiceHd_PackingListHd
    {
        private string _packingListHdTransNmbr = "";
        private string _serviceTypeName = "";

        public Port_DocServiceHd_PackingListHd(Guid _prmDtDocServicePackingListCode, Guid _prmPackingListHdCode, string _prmPackingListHdTransNmbr, string _prmPackingListName, string _prmServiceTypeName, decimal _prmPackingListPrice, decimal _prmQty, decimal _prmTotalPay)
        {
            this.DtDocServicePackingListCode = _prmDtDocServicePackingListCode;
            this.PackingListHdCode = _prmPackingListHdCode;
            this.PackingListHdTransNmbr = _prmPackingListHdTransNmbr;
            this.PackingListName = _prmPackingListName;
            this.ServiceTypeName = _prmServiceTypeName;
            this.PackingListPrice = _prmPackingListPrice;
            this.Qty = _prmQty;
            this.TotalPay = _prmTotalPay;
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

        public string PackingListHdTransNmbr
        {
            get
            {
                return this._packingListHdTransNmbr;
            }
            set
            {
                this._packingListHdTransNmbr = value;
            }
        }
    }
}
