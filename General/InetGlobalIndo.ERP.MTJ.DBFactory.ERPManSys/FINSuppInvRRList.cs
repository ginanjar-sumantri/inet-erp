using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class FINSuppInvRRList
    {
        private string _RRFileNmbr = "";
        private string _suppName = "";
        private string _poNo = "";

        public FINSuppInvRRList(string _prmTransNmbr, String _prmRRNo, String _prmRRNoFileNmbr, String _prmSuppName, String _prmPONo, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.RRNo = _prmRRNo;
            this.RRNoFileNmbr = _prmRRNoFileNmbr;
            this.SuppName = _prmSuppName;
            this.PONo = _prmPONo;
            this.Remark = _prmRemark;
        }

        public string RRNoFileNmbr
        {
            get
            {
                return this._RRFileNmbr;
            }
            set
            {
                this._RRFileNmbr = value;
            }
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

        public string PONo
        {
            get
            {
                return this._poNo;
            }
            set
            {
                this._poNo = value;
            }
        }
    }
}
