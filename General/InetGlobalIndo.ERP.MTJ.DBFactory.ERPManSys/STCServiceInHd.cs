using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class STCServiceInHd
    {
        public STCServiceInHd(string _prmTransNmbr, string _prmFileNmbr, char _prmStatus, DateTime _prmTransDate,
            string _prmCustCode, string _prmWrhsCode, char? _prmWrhsFgSubLed, string _prmWrhsSubLed,
            string _prmReceivedBy, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.CustCode = _prmCustCode;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsFgSubLed = _prmWrhsFgSubLed;
            this.WrhsSubLed = _prmWrhsSubLed;
            this.ReceivedBy = _prmReceivedBy;
            this.Remark = _prmRemark;
        }

        public STCServiceInHd(string _prmTransNmbr, string _prmFileNmbr, char _prmStatus, DateTime _prmTransDate,
            string _prmCustCode, string _prmCustName, string _prmWrhsCode, string _prmWrhsName, 
            char? _prmWrhsFgSubLed, string _prmWrhsSubLed, string _prmReceivedBy, string _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.CustCode = _prmCustCode;
            this.CustName = _prmCustName;
            this.WrhsCode = _prmWrhsCode;
            this.WrhsName = _prmWrhsName;
            this.WrhsFgSubLed = _prmWrhsFgSubLed;
            this.WrhsSubLed = _prmWrhsSubLed;
            this.ReceivedBy = _prmReceivedBy;
            this.Remark = _prmRemark;
        }

        public STCServiceInHd(string _prmTransNmbr, string _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }

        public String CustName { get; set; }
        public String WrhsName { get; set; }
    }
}
