using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_TerminationReqComment
    {
        private string _orgName = "";

        public HRM_TerminationReqComment(Guid _prmTerminationReqCommentInvitationCode, Guid _prmTerminationRequestCode,
            string _prmOrgUnit, string _prmOrgName, string _prmComment, bool _prmIsClose, string _prmCommentClose)
        {
            this.TerminationReqCommentCode = _prmTerminationReqCommentInvitationCode;
            this.TerminationReqCode = _prmTerminationRequestCode;
            this.OrgUnit = _prmOrgUnit;
            this.OrgName = _prmOrgName;
            this.Comment = _prmComment;
            this.IsClose = _prmIsClose;
            this.CommentClose = _prmCommentClose;
        }

        public string OrgName
        {
            get
            {
                return this._orgName;
            }
            set
            {
                this._orgName = value;
            }
        }
    }
}
