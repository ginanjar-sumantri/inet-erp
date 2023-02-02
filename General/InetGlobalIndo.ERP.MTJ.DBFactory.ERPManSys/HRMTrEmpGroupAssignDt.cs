using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrEmpGroupAssignDt
    {
        private string _empName = "";
        private string _fromEmpGroupName = "";
        private string _toEmpGroupName = "";

        public HRMTrEmpGroupAssignDt(String _prmTransNmbr, String _prmEmpNumb, String _prmEmpName, String _prmFromEmpGroup,
            String _prmFromEmpGroupName, String _prmToEmpGroup, String _prmToEmpGroupName, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.FromEmpGroup = _prmFromEmpGroup;
            this.FromEmpGroupName = _prmFromEmpGroupName;
            this.ToEmpGroup = _prmToEmpGroup;
            this.ToEmpGroupName = _prmToEmpGroupName;
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

        public string FromEmpGroupName
        {
            get
            {
                return this._fromEmpGroupName;
            }
            set
            {
                this._fromEmpGroupName = value;
            }
        }

        public string ToEmpGroupName
        {
            get
            {
                return this._toEmpGroupName;
            }
            set
            {
                this._toEmpGroupName = value;
            }
        }
    }
}
