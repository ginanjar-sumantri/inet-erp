using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PRCFAPOHd
    {
        private string _supplierName = "";

        public PRCFAPOHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, char _prmStatus, string _prmCurrCode, string _prmSuppName, decimal _prmTotalForex)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.CurrCode = _prmCurrCode;
            this.SupplierName = _prmSuppName;
            this.TotalForex = _prmTotalForex;
        }

        public PRCFAPOHd(string _prmTransNmbr, string _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }

        public string SupplierName
        {
            get
            {
                return this._supplierName;
            }
            set
            {
                this._supplierName = value;
            }
        }
    }
}
