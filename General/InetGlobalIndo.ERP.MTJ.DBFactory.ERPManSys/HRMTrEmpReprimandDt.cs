using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrEmpReprimandDt
    {
        private string _empName = "";
        private string _jobTitleName = "";
        private string _reprimandName = "";

        public HRMTrEmpReprimandDt(String _prmTransNmbr, String _prmEmployeeId, String _prmEmpName, String _prmJobTitle,
            String _prmJobTitleName, String _prmReprimand, String _prmReprimandName, String _prmReprimandNote, DateTime _prmStartEffective,
            DateTime _prmEndEffective, String _prmRefferedTo, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpNumb = _prmEmployeeId;
            this.EmpName = _prmEmpName;
            this.JobTitle = _prmJobTitle;
            this.JobTitleName = _prmJobTitleName;
            this.Reprimand = _prmReprimand;
            this.ReprimandName = _prmReprimandName;
            this.ReprimandNote = _prmReprimandNote;
            this.StartEffective = _prmStartEffective;
            this.EndEffective = _prmEndEffective;
            this.RefferedTo = _prmRefferedTo;
            this.Remark = _prmRemark;
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

        public string ReprimandName
        {
            get
            {
                return this._reprimandName;
            }
            set
            {
                this._reprimandName = value;
            }
        }
    }
}
