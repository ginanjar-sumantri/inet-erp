using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PRCPOHd
    {
        private string _supplierName = "";

        public PRCPOHd(string _prmTransNmbr, string _prmFileNmbr, int _prmRevisi, DateTime _prmTransDate, char _prmStatus, string _prmCurrCode, string _prmSuppName, decimal _prmTotalForex)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Revisi = _prmRevisi;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.CurrCode = _prmCurrCode;
            this.SupplierName = _prmSuppName;
            this.TotalForex = _prmTotalForex;
        }

        public PRCPOHd(string _prmTransNmbr, string _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }

        public PRCPOHd(int _prmRevisi)
        {
            this.Revisi = _prmRevisi;
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
