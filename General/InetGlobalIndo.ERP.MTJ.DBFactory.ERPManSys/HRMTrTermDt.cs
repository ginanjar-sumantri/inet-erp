using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrTermDt
    {
        private string _empName = "";

        public HRMTrTermDt(String _prmTransNmbr, String _prmEmpNumb, String _prmEmpName, String _prmOldJobTitle, 
            String _prmOldJobLevel,  String _prmOldEmpType, DateTime? _prmOldEndDate, String _prmOldWorkPlace, String _prmOldMethod,
            String _prmNewJobTitle, String _prmNewJobLevel, String _prmNewEmpType, DateTime? _prmNewEndDate, String _prmNewWorkPlace, 
            String _prmNewMethod, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.OldJobTitle = _prmOldJobTitle;
            this.OldJobLevel = _prmOldJobLevel;
            this.OldEmpType = _prmOldEmpType;
            this.OldEndDate = _prmOldEndDate;
            this.OldWorkPlace = _prmOldWorkPlace;
            this.OldMethod = _prmOldMethod;
            this.NewJobTitle = _prmNewJobTitle;
            this.NewJobLevel = _prmNewJobLevel;
            this.NewEmpType = _prmNewEmpType;
            this.NewEndDate = _prmNewEndDate;
            this.NewWorkPlace = _prmNewWorkPlace;
            this.NewMethod = _prmNewMethod;
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
    }
}
