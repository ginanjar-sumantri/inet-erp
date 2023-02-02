using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PRCReturHd
    {
        private string _suppName = "";
        
        public PRCReturHd(string _prmTransNmbr,string _prmFileNmbr ,DateTime _prmTransDate, byte _prmStatus,
            string _prmSuppCode, string _prmSuppName, string _prmRRNo, string _prmRemark, string _prmCreatedBy)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
            this.RRNo = _prmRRNo;
            this.Remark = _prmRemark;
            this.CreatedBy = _prmCreatedBy;
        }

        public PRCReturHd(string _prmTransNmbr, string _prmFileNmbr, DateTime _prmTransDate, byte _prmStatus,
            string _prmSuppCode, string _prmSuppName, string _prmRRNo, string _prmRemark, string _prmCreatedBy,
            string _prmCurrCode, decimal _prmBaseForex, decimal _prmForexRate, decimal _prmPPN, decimal _prmPPNRate,
            decimal _prmPPNForex, decimal _prmTotalForex, string _prmAccAP, char? _prmFgAP, string _prmAccPPN, 
            char? _prmFgPPN, char? _prmFgProcess)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _prmTransDate;
            this.Status = _prmStatus;
            this.SuppCode = _prmSuppCode;
            this.SuppName = _prmSuppName;
            this.RRNo = _prmRRNo;
            this.Remark = _prmRemark;
            this.CreatedBy = _prmCreatedBy;
            this.CurrCode = _prmCurrCode;
            this.BaseForex = _prmBaseForex;
            this.ForexRate = _prmForexRate;
            this.PPN = _prmPPN;
            //this.PPNRate = _prmPPNRate;
            this.PPNForex = _prmPPNForex;
            this.TotalForex = _prmTotalForex;
            //this.AccAP = _prmAccAP;
            //this.FgAP = _prmFgAP;
            //this.AccPPN = _prmAccPPN;
            //this.FgPPN = _prmFgPPN;
            //this.FgProcess = _prmFgProcess;
        }

        public PRCReturHd(string _prmFileNmbr, string _prmRRNo)
        {
            this.FileNmbr = _prmFileNmbr;
            this.RRNo = _prmRRNo;            
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
