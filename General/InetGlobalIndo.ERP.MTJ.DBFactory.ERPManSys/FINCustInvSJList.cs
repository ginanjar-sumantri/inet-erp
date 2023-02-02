using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINCustInvSJList
    {
        private string _SJFileNmbr = "";
        private string _custName = "";
        private string _soNo = "";

        public FINCustInvSJList(string _prmTransNmbr, String _prmSJNo, String _prmSJNoFileNmbr, String _prmCustName, String _prmSONo, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.SJNo = _prmSJNo;
            this.SJNoFileNmbr = _prmSJNoFileNmbr;
            this.CustName = _prmCustName;
            this.SONo = _prmSONo;
            this.Remark = _prmRemark;
        }

        public string SJNoFileNmbr
        {
            get
            {
                return this._SJFileNmbr;
            }
            set
            {
                this._SJFileNmbr = value;
            }
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

        public string SONo
        {
            get
            {
                return this._soNo;
            }
            set
            {
                this._soNo = value;
            }
        }
    }
}
