using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINDPSuppReturHd
    {
        private string _suppName = "";

        public FINDPSuppReturHd(string _prmTransNmbr,string _prmFileNmbr ,DateTime _TransDate, string _prmSuppName, string _prmRemark, char _prmStatus)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.TransDate = _TransDate;
            this.SuppName = _prmSuppName;
            this.Remark = _prmRemark;
            this.Status = _prmStatus;
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
