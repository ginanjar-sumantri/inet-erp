using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class HRM_ScreeningProcessComment
    {
        public string _commentStatus = "";
        public string _empName = "";

        public HRM_ScreeningProcessComment(Guid _prmScreeningProcessCommentCode, Guid _prmScreeningScheduleCode,
            string _prmEmpNumb, string _prmEmpName, Guid _prmCommentStatusCode, string _prmCommentStatus, string _prmComment)
        {
            this.ScreeningProcessCommentCode = _prmScreeningProcessCommentCode;
            this.ScreeningScheduleCode = _prmScreeningScheduleCode;
            this.EmpNumb = _prmEmpNumb;
            this.EmpName = _prmEmpName;
            this.CommentStatusCode = _prmCommentStatusCode;
            this.CommentStatus = _prmCommentStatus;
            this.Comment = _prmComment;
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

        public string CommentStatus
        {
            get
            {
                return this._commentStatus;
            }
            set
            {
                this._commentStatus = value;
            }
        }
    }
}
