using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class SALTrDirectSalesHd
    {
        public string _custName = "";

        public SALTrDirectSalesHd(String _prmTransNmbr, DateTime _prmDate, byte  _prmStatus, String _prmCustCode,
            String _prmFileNo, String _prmCurrCode)
        {
            this.TransNmbr = _prmTransNmbr;
            this.Date = _prmDate;
            this.Status = _prmStatus;
            this.CustCode = _prmCustCode;
            this.FileNo = _prmFileNo;
            this.CurrCode = _prmCurrCode ;
            
        }

        public SALTrDirectSalesHd(String _prmTransNmbr, DateTime _prmDate, byte _prmStatus, String _prmCustCode,
            String _prmFileNo, String _prmCurrCode, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.Date = _prmDate;
            this.Status = _prmStatus;
            this.CustCode = _prmCustCode;
            this.FileNo = _prmFileNo;
            this.CurrCode = _prmCurrCode;
            this.Remark = _prmRemark;
        }

        public SALTrDirectSalesHd(String _prmTransNmbr, String _prmFileNo)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNo = _prmFileNo;
        }

        public string CustName
        {
            get
            {
                return this._custName;
            }
            set
            {
                this._custName = value;
            }
        }
    }
}
