using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrTermHd
    {
        private string _termTypeName = "";

        public HRMTrTermHd(string _prmTransNmbr, String _prmFileNmbr, char _prmStatus, DateTime _prmTransDate, String _prmTermType, String _prmTermTypeName, DateTime _prmEffectiveDate, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.TermType = _prmTermType;
            this.TermTypeName = _prmTermTypeName;
            this.EffectiveDate = _prmEffectiveDate;
            this.Remark = _prmRemark;
        }

        public HRMTrTermHd(string _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }

        public string TermTypeName
        {
            get
            {
                return this._termTypeName;
            }
            set
            {
                this._termTypeName = value;
            }
        }
    }
}
