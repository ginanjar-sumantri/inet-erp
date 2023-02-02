using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;
using System.IO;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class TerminationRequestBL : Base
    {
        public TerminationRequestBL()
        {

        }

        #region HRM_TerminationRequest
        public double RowsCountHRMTerminationRequest(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                           from _terminationRequest in this.db.HRM_TerminationRequests
                           where (SqlMethods.Like(_terminationRequest.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like((_terminationRequest.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                           select _terminationRequest.TerminationRequestCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_TerminationRequest> GetListHRMTerminationRequest(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRM_TerminationRequest> _result = new List<HRM_TerminationRequest>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _terminationRequest in this.db.HRM_TerminationRequests
                                where (SqlMethods.Like(_terminationRequest.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like((_terminationRequest.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _terminationRequest.EditDate descending
                                select new
                                {
                                    TerminationRequestCode = _terminationRequest.TerminationRequestCode,
                                    TransNmbr = _terminationRequest.TransNmbr,
                                    FileNmbr = _terminationRequest.FileNmbr,
                                    TransDate = _terminationRequest.TransDate,
                                    Status = _terminationRequest.Status,
                                    EmpNumb = _terminationRequest.EmpNumb,
                                    EmpName = (
                                                    from _msEmployee in this.db.MsEmployees
                                                    where _msEmployee.EmpNumb == _terminationRequest.EmpNumb
                                                    select _msEmployee.EmpName
                                                ).FirstOrDefault(),
                                    ReasonCode = _terminationRequest.ReasonCode,
                                    ReasonName = (
                                                    from _msReason in this.db.HRMMsReasons
                                                    where _msReason.ReasonCode == _terminationRequest.ReasonCode
                                                    select _msReason.ReasonName
                                                ).FirstOrDefault(),
                                    ExitDate = _terminationRequest.ExitDate,
                                    Remark = _terminationRequest.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_TerminationRequest(_row.TerminationRequestCode, _row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.EmpNumb, _row.EmpName, _row.ReasonCode, _row.ReasonName, _row.ExitDate, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRM_TerminationRequest> GetListHRMTerminationRequestForDDL()
        {
            List<HRM_TerminationRequest> _result = new List<HRM_TerminationRequest>();

            try
            {
                var _query = (
                                from _terminationRequest in this.db.HRM_TerminationRequests
                                where !(
                                            from _terminationFinishing in this.db.HRM_TerminationFinishings
                                            where _terminationFinishing.TerminationRequestCode == _terminationRequest.TerminationRequestCode
                                            select _terminationFinishing.TerminationRequestCode
                                        ).Contains(_terminationRequest.TerminationRequestCode)
                                orderby _terminationRequest.TransNmbr ascending
                                select new
                                {
                                    TerminationRequestCode = _terminationRequest.TerminationRequestCode,
                                    FileNmbr = _terminationRequest.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_TerminationRequest(_row.TerminationRequestCode, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_TerminationRequest GetSingleHRMTerminationRequest(Guid _prmCode)
        {
            HRM_TerminationRequest _result = null;

            try
            {
                _result = this.db.HRM_TerminationRequests.Single(_temp => _temp.TerminationRequestCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRMTerminationRequest(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                List<string> _fileName = new List<string>();

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_TerminationRequest _terminationRequest = this.db.HRM_TerminationRequests.Single(_temp => _temp.TerminationRequestCode == new Guid(_prmCode[i]));

                    if (_terminationRequest != null)
                    {
                        if ((_terminationRequest.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (
                                              from _detail in this.db.HRM_TerminationReqAttachments
                                              where _detail.TerminationRequestCode == new Guid(_prmCode[i])
                                              select _detail);

                            foreach (HRM_TerminationReqAttachment _row in _query)
                            {
                                _fileName.Add(_row.File);
                            }

                            var _query2 = (
                                              from _detail2 in this.db.HRM_TerminationHandOvers
                                              where _detail2.TerminationRequestCode == new Guid(_prmCode[i])
                                              select _detail2);

                            this.db.HRM_TerminationReqAttachments.DeleteAllOnSubmit(_query);
                            this.db.HRM_TerminationHandOvers.DeleteAllOnSubmit(_query2);

                            this.db.HRM_TerminationRequests.DeleteOnSubmit(_terminationRequest);

                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            break;
                        }
                    }
                }

                if (_result == true)
                {
                    this.db.SubmitChanges();

                    foreach (String _item in _fileName)
                    {
                        if (File.Exists(ApplicationConfig.TerminationReqAttachmentPath + _item.Trim()) == true)
                        {
                            File.Delete(ApplicationConfig.TerminationReqAttachmentPath + _item.Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _result = false;
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddHRMTerminationRequest(HRM_TerminationRequest _prmHRM_TerminationRequest)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmHRM_TerminationRequest.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.HRM_TerminationRequests.InsertOnSubmit(_prmHRM_TerminationRequest);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmHRM_TerminationRequest.TerminationRequestCode.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRMTerminationRequest(HRM_TerminationRequest _prmHRM_TerminationRequest)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(Guid _prmTerminationRequestCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TerminationRequestGetAppr(_prmTerminationRequestCode, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.HRMTerminationRequest);
                    _transActivity.TransNmbr = _prmTerminationRequestCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = HttpContext.Current.User.Identity.Name;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();

                    _result = "Waiting For Approval Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Waiting For Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(Guid _prmTerminationRequestCode)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spHRM_TerminationRequestApprove(_prmTerminationRequestCode, ref _result);

                    if (_result == "")
                    {
                        HRM_TerminationRequest _TerminationRequest = this.GetSingleHRMTerminationRequest(_prmTerminationRequestCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_TerminationRequest.TransDate.Year, _TerminationRequest.TransDate.Month, AppModule.GetValue(TransactionType.HRMTerminationRequest), this._companyTag, ""))
                        {
                            _TerminationRequest.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.HRMTerminationRequest);
                        _transActivity.TransNmbr = _prmTerminationRequestCode.ToString();
                        _transActivity.FileNmbr = this.GetSingleHRMTerminationRequest(_prmTerminationRequestCode).FileNmbr;
                        _transActivity.Username = HttpContext.Current.User.Identity.Name;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

                        this.db.SubmitChanges();

                        _scope.Complete();

                        _result = "Approve Success";
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string UnPosting(Guid _prmTerminationRequestCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TerminationRequestUnPost(_prmTerminationRequestCode, ref _result);
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Close(Guid _prmCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TerminationRequestPosting(_prmCode, ref _result);
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string ClosingComment(string _prmCode, string _prmRemark, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spHRM_TerminationReqCommentClosing(new Guid(_prmCode), _prmRemark, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Closing Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Closing Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Cancel(Guid _prmTerminationRequestCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TerminationRequestCancel(_prmTerminationRequestCode, ref _result);

                if (_result == "")
                {
                    _result = "Cancel Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Cancel Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Reject(Guid _prmTerminationRequestCode)
        {
            string _result = "";

            try
            {
                int _success = this.db.spHRM_TerminationRequestReject(_prmTerminationRequestCode, ref _result);

                if (_result == "")
                {
                    _result = "Reject Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Reject Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        #endregion

        #region HRM_TerminationReqComment
        public double RowsCountHRM_TerminationReqComment(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _termReqComments in this.db.HRM_TerminationReqComments
                where _termReqComments.TerminationReqCode == new Guid(_prmCode)
                select _termReqComments.TerminationReqCommentCode
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_TerminationReqComment> GetListHRM_TerminationReqComment(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRM_TerminationReqComment> _result = new List<HRM_TerminationReqComment>();

            try
            {
                var _query = (
                                from _terminationReqComment in this.db.HRM_TerminationReqComments
                                where _terminationReqComment.TerminationReqCode == new Guid(_prmCode)
                                orderby _terminationReqComment.EditDate descending
                                select new
                                {
                                    TerminationReqCommentCode = _terminationReqComment.TerminationReqCommentCode,
                                    TerminationRequestCode = _terminationReqComment.TerminationReqCode,
                                    OrgUnit = _terminationReqComment.OrgUnit,
                                    OrgName = (
                                                from _orgUnit in this.db.Master_OrganizationUnits
                                                where _orgUnit.OrgUnit == _terminationReqComment.OrgUnit
                                                select _orgUnit.Description
                                              ).FirstOrDefault(),
                                    Comment = _terminationReqComment.Comment,
                                    IsClose = _terminationReqComment.IsClose,
                                    CommentClose = _terminationReqComment.CommentClose
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_TerminationReqComment(_row.TerminationReqCommentCode, _row.TerminationRequestCode, _row.OrgUnit, _row.OrgName, _row.Comment, _row.IsClose, _row.CommentClose));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_TerminationReqComment GetSingleHRM_TerminationReqComment(string _prmCode)
        {
            HRM_TerminationReqComment _result = null;

            try
            {
                _result = this.db.HRM_TerminationReqComments.Single(_temp => _temp.TerminationReqCommentCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRM_TerminationReqComment(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_TerminationReqComment _TerminationReqAttachment = this.db.HRM_TerminationReqComments.Single(_temp => _temp.TerminationReqCommentCode == new Guid(_prmCode[i]));

                    if (_TerminationReqAttachment.IsClose == TerminationRequestDataMapper.GetIsCommentClose(YesNo.Yes))
                    {
                        return _result;
                    }

                    this.db.HRM_TerminationReqComments.DeleteOnSubmit(_TerminationReqAttachment);
                }

                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddHRM_TerminationReqComment(HRM_TerminationReqComment _prmHRM_TerminationReqComment)
        {
            bool _result = false;

            try
            {
                this.db.HRM_TerminationReqComments.InsertOnSubmit(_prmHRM_TerminationReqComment);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRM_TerminationReqComment(HRM_TerminationReqComment _prmHRM_TerminationReqComment)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool CheckUserCommentPermission(String _prmCode, String _prmUserName)
        {
            bool _result = false;
            MTJMembershipDataContext db2 = new MTJMembershipDataContext(ApplicationConfig.MembershipConnString);

            String _empNumb = new User_EmployeeBL().GetEmployeeIDByUserName(_prmUserName);

            var _query = (
                            from _termComment in this.db.HRM_TerminationReqCommentInvitations
                            where _termComment.TerminationRequestCode == new Guid(_prmCode)
                            select new
                            {
                                EmpNumb = _termComment.EmpNumb
                            }
                         );

            foreach (var _item in _query)
            {
                if (_item.EmpNumb == _empNumb)
                {
                    _result = true;
                    break;
                }
            }

            return _result;
        }
        #endregion

        #region HRM_TerminationReqCommentInvitation
        public double RowsCountHRM_termReqCommInvitation(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _termReqCommentInvitation in this.db.HRM_TerminationReqCommentInvitations
                where _termReqCommentInvitation.TerminationRequestCode == new Guid(_prmCode)
                select _termReqCommentInvitation.TerminationReqCommentInvitationCode
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_TerminationReqCommentInvitation> GetListHRM_termReqCommInvitation(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRM_TerminationReqCommentInvitation> _result = new List<HRM_TerminationReqCommentInvitation>();

            try
            {
                var _query = (
                                from _termReqCommInvitation in this.db.HRM_TerminationReqCommentInvitations
                                where _termReqCommInvitation.TerminationRequestCode == new Guid(_prmCode)
                                orderby _termReqCommInvitation.EditDate descending
                                select new
                                {
                                    TerminationReqCommentInvitationCode = _termReqCommInvitation.TerminationReqCommentInvitationCode,
                                    TerminationRequestCode = _termReqCommInvitation.TerminationRequestCode,
                                    EmpNumb = _termReqCommInvitation.EmpNumb,
                                    EmpName = (
                                                from _emp in this.db.MsEmployees
                                                where _emp.EmpNumb == _termReqCommInvitation.EmpNumb
                                                select _emp.EmpName
                                              ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_TerminationReqCommentInvitation(_row.TerminationReqCommentInvitationCode, _row.TerminationRequestCode, _row.EmpNumb, _row.EmpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_TerminationReqCommentInvitation GetSingleHRM_TerminationReqCommentInvitation(string _prmCode)
        {
            HRM_TerminationReqCommentInvitation _result = null;

            try
            {
                _result = this.db.HRM_TerminationReqCommentInvitations.Single(_temp => _temp.TerminationReqCommentInvitationCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRM_TerminationReqCommentInvitation(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_TerminationReqCommentInvitation _termReqCommentInvitation = this.db.HRM_TerminationReqCommentInvitations.Single(_temp => _temp.TerminationReqCommentInvitationCode == new Guid(_prmCode[i]));

                    this.db.HRM_TerminationReqCommentInvitations.DeleteOnSubmit(_termReqCommentInvitation);
                }

                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddHRM_TerminationReqCommentInvitation(HRM_TerminationReqCommentInvitation _prmHRM_TerminationReqCommentInvitation)
        {
            bool _result = false;

            try
            {
                this.db.HRM_TerminationReqCommentInvitations.InsertOnSubmit(_prmHRM_TerminationReqCommentInvitation);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRM_TerminationReqCommentInvitation(HRM_TerminationReqCommentInvitation _prmHRM_TerminationReqCommentInvitation)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region HRM_TerminationReqAttachment
        public double RowsCountHRM_TerminationReqAttachment(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _terminationReqAttachment in this.db.HRM_TerminationReqAttachments
                where _terminationReqAttachment.TerminationRequestCode == new Guid(_prmCode)
                select _terminationReqAttachment.TerminationReqAttachCode
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_TerminationReqAttachment> GetListHRM_TerminationReqAttachment(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRM_TerminationReqAttachment> _result = new List<HRM_TerminationReqAttachment>();

            try
            {
                var _query = (
                                from _terminationReqAttachment in this.db.HRM_TerminationReqAttachments
                                where _terminationReqAttachment.TerminationRequestCode == new Guid(_prmCode)
                                orderby _terminationReqAttachment.EditDate descending
                                select new
                                {
                                    TerminationReqAttachCode = _terminationReqAttachment.TerminationReqAttachCode,
                                    TerminationRequestCode = _terminationReqAttachment.TerminationRequestCode,
                                    TerminationRequestFileNmbr = (
                                                                    from _terminationReq in this.db.HRM_TerminationRequests
                                                                    where _terminationReq.TerminationRequestCode == _terminationReqAttachment.TerminationRequestCode
                                                                    select _terminationReq.FileNmbr
                                                                ).FirstOrDefault(),
                                    File = _terminationReqAttachment.File,
                                    Remark = _terminationReqAttachment.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_TerminationReqAttachment(_row.TerminationReqAttachCode, _row.TerminationRequestCode, _row.TerminationRequestFileNmbr, _row.File, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_TerminationReqAttachment GetSingleHRM_TerminationReqAttachment(string _prmCode)
        {
            HRM_TerminationReqAttachment _result = null;

            try
            {
                _result = this.db.HRM_TerminationReqAttachments.Single(_temp => _temp.TerminationReqAttachCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRM_TerminationReqAttachment(string[] _prmCode)
        {
            bool _result = false;
            string[] _fileName = new string[_prmCode.Length];

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRM_TerminationReqAttachment _terminationReqAttachment = this.db.HRM_TerminationReqAttachments.Single(_temp => _temp.TerminationReqAttachCode == new Guid(_prmCode[i]));

                    _fileName[i] = _terminationReqAttachment.File;

                    this.db.HRM_TerminationReqAttachments.DeleteOnSubmit(_terminationReqAttachment);
                }

                this.db.SubmitChanges();

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    if (File.Exists(ApplicationConfig.TerminationReqAttachmentPath + _fileName[i]) == true)
                    {
                        File.Delete(ApplicationConfig.TerminationReqAttachmentPath + _fileName[i]);
                    }
                }

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String AddHRM_TerminationReqAttachment(HRM_TerminationReqAttachment _prmHRM_TerminationReqAttachment, FileUpload _prmFileUpload)
        {
            String _result = "";

            try
            {
                _result = this.UploadTerminationAttachmentFile(_prmHRM_TerminationReqAttachment.TerminationReqAttachCode.ToString(), _prmFileUpload);

                if (_result == "")
                {
                    String _path = _prmFileUpload.PostedFile.FileName;
                    FileInfo _file = new FileInfo(_path);
                    _prmHRM_TerminationReqAttachment.File = _prmHRM_TerminationReqAttachment.TerminationReqAttachCode.ToString() + _file.Extension;

                    this.db.HRM_TerminationReqAttachments.InsertOnSubmit(_prmHRM_TerminationReqAttachment);
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRM_TerminationReqAttachment(HRM_TerminationReqAttachment _prmHRM_TerminationReqAttachment)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String UploadTerminationAttachmentFile(string _prmAttachCode, FileUpload _prmFileUpload)
        {
            String _result = "";

            String _path = _prmFileUpload.PostedFile.FileName;
            FileInfo _file = new FileInfo(_path);
            String _imagepath = ApplicationConfig.TerminationReqAttachmentPath + _prmAttachCode + _file.Extension;

            if (_path == "")
            {
                _result = "Invalid filename supplied";
            }
            if (_prmFileUpload.PostedFile.ContentLength == 0)
            {
                _result = "Invalid file content";
            }
            if (_result == "")
            {
                if (DocumentHandler.IsDocumentFile(_path, ApplicationConfig.AttachmentExtension) == true)
                {
                    if (_prmFileUpload.PostedFile.ContentLength <= Convert.ToDecimal(ApplicationConfig.AttachmentMaxSize))
                    {
                        if (File.Exists(ApplicationConfig.TerminationReqAttachmentPath + _prmAttachCode + _file.Extension) == true)
                        {
                            File.Delete(ApplicationConfig.TerminationReqAttachmentPath + _prmAttachCode + _file.Extension);
                        }

                        _file.CopyTo(_imagepath, true);
                        _prmFileUpload.PostedFile.SaveAs(_imagepath);

                        _file.Refresh();
                    }
                    else
                    {
                        _result = "Unable to upload, file exceeds maximum limit";
                    }
                }
            }

            return _result;
        }

        #endregion HRM_TerminationReqAttachment

        #region HRM_TerminationHandOver
        public double RowsCountHRM_TerminationHandOver(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _terminationHandOver in this.db.HRM_TerminationHandOvers
                where _terminationHandOver.TerminationRequestCode == new Guid(_prmCode)
                select _terminationHandOver.EmpNumb
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRM_TerminationHandOver> GetListHRM_TerminationHandOver(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRM_TerminationHandOver> _result = new List<HRM_TerminationHandOver>();

            try
            {
                var _query = (
                                from _terminationHandOver in this.db.HRM_TerminationHandOvers
                                where _terminationHandOver.TerminationRequestCode == new Guid(_prmCode)
                                orderby _terminationHandOver.EditDate descending
                                select new
                                {
                                    TerminationRequestCode = _terminationHandOver.TerminationRequestCode,
                                    EmpNumb = _terminationHandOver.EmpNumb,
                                    EmpName = (
                                                from _msEmployee in this.db.MsEmployees
                                                where _msEmployee.EmpNumb == _terminationHandOver.EmpNumb
                                                select _msEmployee.EmpName
                                               ).FirstOrDefault(),
                                    Status = _terminationHandOver.Status,
                                    StartDate = _terminationHandOver.StartDate,
                                    EndDate = _terminationHandOver.EndDate,
                                    Remark = _terminationHandOver.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRM_TerminationHandOver(_row.TerminationRequestCode, _row.EmpNumb, _row.EmpName, _row.Status, _row.StartDate, _row.EndDate, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRM_TerminationHandOver GetSingleHRM_TerminationHandOver(string _prmCode, string _prmEmpNumb)
        {
            HRM_TerminationHandOver _result = null;

            try
            {
                _result = this.db.HRM_TerminationHandOvers.Single(_temp => _temp.TerminationRequestCode == new Guid(_prmCode) && _temp.EmpNumb == _prmEmpNumb);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHRM_TerminationHandOver(string[] _prmEmpNumb, string _prmTerminationRequest)
        {
            bool _result = false;
            string[] _fileName = new string[_prmEmpNumb.Length];

            try
            {
                for (int i = 0; i < _prmEmpNumb.Length; i++)
                {
                    HRM_TerminationHandOver _terminationHandOver = this.db.HRM_TerminationHandOvers.Single(_temp => _temp.TerminationRequestCode == new Guid(_prmTerminationRequest) && _temp.EmpNumb == _prmEmpNumb[i]);

                    this.db.HRM_TerminationHandOvers.DeleteOnSubmit(_terminationHandOver);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean AddHRM_TerminationHandOver(HRM_TerminationHandOver _prmHRM_TerminationHandOver)
        {
            Boolean _result = false;

            try
            {
                this.db.HRM_TerminationHandOvers.InsertOnSubmit(_prmHRM_TerminationHandOver);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditHRM_TerminationHandOver(HRM_TerminationHandOver _prmHRM_TerminationHandOver)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion HRM_TerminationHandOver

        ~TerminationRequestBL()
        {
        }
    }
}
