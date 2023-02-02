using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PRCTrDirectPurchaseHd
    {
        public string _suppName = "";

        public PRCTrDirectPurchaseHd(String _prmTransNmbr, String _prmFileNmbr, DateTime _prmTransDate, byte _prmStatus,
            String _prmSuppCode, String _prmSuppName, String _prmCurrCode)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
            this.CurrCode = _prmCurrCode;
        }

        public PRCTrDirectPurchaseHd(String _prmTransNmbr, String _prmFileNmbr, DateTime _prmTransDate, byte _prmStatus,
            String _prmSuppCode, String _prmSuppName, String _prmCurrCode, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
            this.CurrCode = _prmCurrCode;
            this.Remark = _prmRemark; 
        }

        public string SuppName
        {
            get
            {
                return this._suppName;
            }
            set
            {
                this._suppName = value;
            }
        }
    }
}
