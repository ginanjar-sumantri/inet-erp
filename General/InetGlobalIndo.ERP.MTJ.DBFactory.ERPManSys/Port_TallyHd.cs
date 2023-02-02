using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Port_TallyHd
    {
        private string _formArriveID = "";

        public Port_TallyHd(Guid _prmTallyHdCode, string _prmTransNmbr, string _prmFileNmbr, byte _prmStatus, DateTime _prmTransDateStart, DateTime? _prmTransDateEnd, int _prmTimeZone, Guid _prmHdShipArrivalCode, string _prmFormArriveID, decimal _prmTotalColly, decimal _prmTotalNetWeight)
        {
            this.TallyHdCode = _prmTallyHdCode;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDateStart = _prmTransDateStart;
            this.TransDateEnd = _prmTransDateEnd;
            this.TimeZone = _prmTimeZone;
            this.HdShipArrivalCode = _prmHdShipArrivalCode;
            this.FormArriveID = _prmFormArriveID;
            this.TotalColly = _prmTotalColly;
            this.TotalNetWeight = _prmTotalNetWeight;
        }

        public Port_TallyHd(Guid _prmTallyHdCode, string _prmFileNmbr)
        {
            this.TallyHdCode = _prmTallyHdCode;
            this.FileNmbr = _prmFileNmbr;
        }

        public string FormArriveID
        {
            get
            {
                return this._formArriveID;
            }
            set
            {
                this._formArriveID = value;
            }
        }
    }
}
