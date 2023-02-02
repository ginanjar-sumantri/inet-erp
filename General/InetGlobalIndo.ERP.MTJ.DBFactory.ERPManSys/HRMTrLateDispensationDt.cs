using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRMTrLateDispensationDt
    {
        private String _empName = "";
        private String _description = "";

        public HRMTrLateDispensationDt(String _prmTransNmbr, String _prmEmpNumb, String _prmEmpName, String _prmReasonCode, String _prmDescription, String _prmRemark)
        {
            this.TransNmbr = _prmTransNmbr;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.ReasonCode = _prmReasonCode;
            this.Description = _prmDescription;
            this.Remark = _prmRemark;
        }

        public String EmpName
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

        public String Description
        {
            get
            {
                return this._description;
            }

            set
            {
                this._description = value;
            }
        }
    }
}
