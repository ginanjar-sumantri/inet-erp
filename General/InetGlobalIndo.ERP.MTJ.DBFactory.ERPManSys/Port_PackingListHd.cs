using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Port_PackingListHd
    {
        private string _shipName = "";

        public Port_PackingListHd(Guid _prmPackingListHdCode, string _prmTransNmbr, DateTime _prmTransDate, byte _prmStatus, string _prmInvoiceNo, string _prmLCNo, string _prmShipName, DateTime? _prmETD, decimal _prmGrandTotalPackage, decimal _prmGrandTotalNetWeight, decimal _prmGrandTotalNetGross)
        {
            this.PackingListHdCode = _prmPackingListHdCode;
            this.TransNmbr = _prmTransNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.InvoiceNo = _prmInvoiceNo;
            this.LCNo = _prmLCNo;
            this.ShipName = _prmShipName;
            this.ETD = _prmETD;
            this.GrandTotalPackage = _prmGrandTotalPackage;
            this.GrandTotalNetWeight = _prmGrandTotalNetWeight;
            this.GrandTotalNetGross = _prmGrandTotalNetGross;
        }

        public Port_PackingListHd(Guid _prmPackingListHdCode, string _prmTransNmbr)
        {
            this.PackingListHdCode = _prmPackingListHdCode;
            this.TransNmbr = _prmTransNmbr;
        }

        public string ShipName
        {
            get
            {
                return this._shipName;
            }
            set
            {
                this._shipName = value;
            }
        }
    }
}
