using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class PAYTrDeductionHd
    {
        private string _orgUnitName = "";

        public PAYTrDeductionHd(string _prmTransNmbr, String _prmFileNmbr, char _prmStatus, DateTime _prmTransDate,
            String _prmOrgUnit, String _prmOrgUnitName, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.Status = _prmStatus;
            this.TransDate = _prmTransDate;
            this.OrgUnit = _prmOrgUnit;
            this.OrgUnitName = _prmOrgUnitName;
            this.Remark = _prmRemark;
        }

        public PAYTrDeductionHd(string _prmTransNmbr, String _prmFileNmbr)
        {
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
        }

        public string OrgUnitName
        {
            get
            {
                return this._orgUnitName;
            }
            set
            {
                this._orgUnitName = value;
            }
        }
    }
}
