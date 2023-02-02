using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_RecruitmentRequest
    {
        string _orgUnitName = "";

        public HRM_RecruitmentRequest(Guid _prmRecruitmentRequestCode, string _prmTransNmbr, string _prmFileNmbr, DateTime _prmStartDate, byte _prmStatus, DateTime? _prmCloseDate, DateTime? _prmExpectedDate, int _prmQty, int _prmQtyDone, string _prmOrgUnit, string _prmOrgUnitName, string _prmInsertBy)
        {
            this.RecruitmentRequestCode = _prmRecruitmentRequestCode;
            this.TransNmbr = _prmTransNmbr;
            this.FileNmbr = _prmFileNmbr;
            this.StartDate = _prmStartDate;
            this.Status = _prmStatus;
            this.CloseDate = _prmCloseDate;
            this.ExpectedDate = _prmExpectedDate;
            this.Qty = _prmQty;
            this.QtyDone = _prmQtyDone;
            this.OrgUnit = _prmOrgUnit;
            this.OrgUnitName = _prmOrgUnitName;
            this.InsertBy = _prmInsertBy;
        }

        public HRM_RecruitmentRequest(Guid _prmRecruitmentRequestCode, string _prmFileNmbr)
        {
            this.RecruitmentRequestCode = _prmRecruitmentRequestCode;
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
