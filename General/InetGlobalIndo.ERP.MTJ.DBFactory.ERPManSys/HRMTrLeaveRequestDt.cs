using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLeaveRequestDt
    {
        private string _empName = "";
        private string _jobTitleName = "";
        private string _leavesName = "";

        public HRMTrLeaveRequestDt(String _prmTransNmbr, String _prmEmpNumb, String _prmEmpName, String _prmJobTitle, String _prmJobTitleName,
            String _prmLeavesCode, String _prmLeavesName, char _prmIsLess1Day, DateTime _prmStartDate, DateTime _prmEndDate, int _prmDays,
            int _prmHoliday, int _prmDispensation, int? _prmBalance, int _prmTaken, String _prmContactAddress, String _prmContactPhone, String _prmReasonLeave)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.JobTitle = _prmJobTitle;
            this.JobTitleName = _prmJobTitleName;
            this.LeavesCode = _prmLeavesCode;
            this.LeavesName = _prmLeavesName;
            this.IsLess1Day = _prmIsLess1Day;
            this.StartDate = _prmStartDate;
            this.EndDate = _prmEndDate;
            this.Days = _prmDays;
            this.Holiday = _prmHoliday;
            this.Dispensation = _prmDispensation;
            this.Balance = _prmBalance;
            this.Taken = _prmTaken;
            this.ContactAddress = _prmContactAddress;
            this.ContactPhone = _prmContactPhone;
            this.ReasonLeave = _prmReasonLeave;
        }

        public string EmpName
        {
            get
            {
                return this._empName;
            }
            set
            {
                this._empName = value;
            }
        }

        public string JobTitleName
        {
            get
            {
                return this._jobTitleName;
            }
            set
            {
                this._jobTitleName = value;
            }
        }

        public string LeavesName
        {
            get
            {
                return this._leavesName;
            }
            set
            {
                this._leavesName = value;
            }
        }
    }
}
