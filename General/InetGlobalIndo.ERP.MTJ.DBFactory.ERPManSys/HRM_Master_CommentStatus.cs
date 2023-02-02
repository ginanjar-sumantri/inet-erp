using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_Master_CommentStatus
    {
        public HRM_Master_CommentStatus(Guid _prmCommentStatusCode, string _prmCommentStatusName, string _prmDescription)
        {
            this.CommentStatusCode = _prmCommentStatusCode;
            this.CommentStatusName = _prmCommentStatusName;
            this.Description = _prmDescription;
        }

        public HRM_Master_CommentStatus(Guid _prmCommentStatusCode, string _prmCommentStatusName)
        {
            this.CommentStatusCode = _prmCommentStatusCode;
            this.CommentStatusName = _prmCommentStatusName;
        }
    }
}
