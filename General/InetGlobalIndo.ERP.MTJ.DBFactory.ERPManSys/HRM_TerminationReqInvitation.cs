using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_TerminationReqCommentInvitation
    {
        private string _empName = "";

        public HRM_TerminationReqCommentInvitation(Guid _prmTerminationReqCommentInvitationCode, Guid _prmTerminationRequestCode, string _prmEmpNumb, string _prmEmpName)
        {
            this.TerminationReqCommentInvitationCode = _prmTerminationReqCommentInvitationCode;
            this.TerminationRequestCode = _prmTerminationRequestCode;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
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
