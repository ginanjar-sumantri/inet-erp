using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.IO;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Xml;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing
{
    public sealed class PurchaseRequestBL : Base
    {
        public PurchaseRequestBL()
        {

        }

        #region PRCRequestHd

        public double RowsCountPRCRequestHd(string _prmCategory, string _prmKeyword)
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
                           from _prcRequestHd in this.db.PRCRequestHds
                           where (SqlMethods.Like(_prcRequestHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like((_prcRequestHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                           && _prcRequestHd.Status != PurchaseRequestDataMapper.GetStatus(TransStatus.Deleted)
                           select _prcRequestHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<PRCRequestHd> GetListPRCRequestHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<PRCRequestHd> _result = new List<PRCRequestHd>();

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
                                from _prcRequestHd in this.db.PRCRequestHds
                                where (SqlMethods.Like(_prcRequestHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_prcRequestHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _prcRequestHd.Status != PurchaseRequestDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _prcRequestHd.TransDate descending
                                select new
                                {
                                    TransNmbr = _prcRequestHd.TransNmbr,
                                    FileNmbr = _prcRequestHd.FileNmbr,
                                    TransDate = _prcRequestHd.TransDate,
                                    Status = _prcRequestHd.Status,
                                    OrgUnit = _prcRequestHd.OrgUnit,
                                    RequestBy = _prcRequestHd.RequestBy,
                                    Remark = _prcRequestHd.Remark,
                                    CurrCode = _prcRequestHd.CurrCode,
                                    CreatedBy = _prcRequestHd.CreatedBy
                                }
                            );

                if (_prmOrderBy == "Trans No.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.TransNmbr)) : (_query.OrderByDescending(a => a.TransNmbr));

                if (_prmOrderBy == "File No.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.FileNmbr)) : (_query.OrderByDescending(a => a.FileNmbr));

                if (_prmOrderBy == "Trans Date")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.TransDate)) : (_query.OrderByDescending(a => a.TransDate));

                if (_prmOrderBy == "Status")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.Status)) : (_query.OrderByDescending(a => a.Status));

                if (_prmOrderBy == "Organization Unit")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.OrgUnit)) : (_query.OrderByDescending(a => a.OrgUnit));

                if (_prmOrderBy == "Currency")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.CurrCode)) : (_query.OrderByDescending(a => a.CurrCode));

                if (_prmOrderBy == "Request By.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.RequestBy)) : (_query.OrderByDescending(a => a.RequestBy));

                if (_prmOrderBy == "Remark")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.Remark)) : (_query.OrderByDescending(a => a.Remark));

                _query = _query.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new PRCRequestHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.OrgUnit, _row.RequestBy, _row.Remark, _row.CurrCode, _row.CreatedBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<PRCRequestHd> GetListPRCRequestHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<PRCRequestHd> _result = new List<PRCRequestHd>();

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
                                from _prcRequestHd in this.db.PRCRequestHds
                                where (SqlMethods.Like(_prcRequestHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_prcRequestHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _prcRequestHd.TransDate descending
                                select new
                                {
                                    TransNmbr = _prcRequestHd.TransNmbr,
                                    FileNmbr = _prcRequestHd.FileNmbr,
                                    TransDate = _prcRequestHd.TransDate,
                                    Status = _prcRequestHd.Status,
                                    OrgUnit = _prcRequestHd.OrgUnit,
                                    RequestBy = _prcRequestHd.RequestBy,
                                    Remark = _prcRequestHd.Remark,
                                    CurrCode = _prcRequestHd.CurrCode,
                                    CreatedBy = _prcRequestHd.CreatedBy
                                }
                            );

                _query = _query.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new PRCRequestHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.OrgUnit, _row.RequestBy, _row.Remark, _row.CurrCode, _row.CreatedBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public PRCRequestHd GetSinglePRCRequestHd(string _prmCode)
        {
            PRCRequestHd _result = null;

            try
            {
                _result = this.db.PRCRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSinglePRCRequestHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    PRCRequestHd _prcRequestHd = this.db.PRCRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_prcRequestHd != null)
                    {
                        if (_prcRequestHd.Status != PurchaseRequestDataMapper.GetStatus(TransStatus.Posted))
                        {
                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public PRCPRCompilerHd GetSinglePRCPRCompileHd(string _prmCode)
        {
            PRCPRCompilerHd _result = null;

            try
            {
                _result = this.db.PRCPRCompilerHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddPRCRequestHd(PRCRequestHd _prmPRCRequestHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmPRCRequestHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.PRCRequestHds.InsertOnSubmit(_prmPRCRequestHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmPRCRequestHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddPRCCompileHd(PRCPRCompilerHd _prmPRCPRCompilerHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmPRCPRCompilerHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.PRCPRCompilerHds.InsertOnSubmit(_prmPRCPRCompilerHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmPRCPRCompilerHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPRCRequestHd(PRCRequestHd _prmPRCRequestHd)
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

        public bool DeleteMultiPRCRequestHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    PRCRequestHd _prcRequestHd = this.db.PRCRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_prcRequestHd != null)
                    {
                        if ((_prcRequestHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.PRCRequestDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.PRCRequestDts.DeleteAllOnSubmit(_query);

                            this.db.PRCRequestHds.DeleteOnSubmit(_prcRequestHd);

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
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiApprovePRCRequestHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    PRCRequestHd _prcRequestHd = this.db.PRCRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_prcRequestHd.Status == PurchaseRequestDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _prcRequestHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _prcRequestHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_prcRequestHd != null)
                    {
                        if ((_prcRequestHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.PRCRequestDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.PRCRequestDts.DeleteAllOnSubmit(_query);

                            this.db.PRCRequestHds.DeleteOnSubmit(_prcRequestHd);

                            _result = true;
                        }
                        else if (_prcRequestHd.FileNmbr != "" && _prcRequestHd.Status == PurchaseRequestDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _prcRequestHd.Status = PurchaseRequestDataMapper.GetStatus(TransStatus.Deleted);
                            _result = true;
                        }
                    }
                }

                if (_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_PRMRGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.PurchaseRequest);
                    _transActivity.TransNmbr = _prmCode.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string GetApprovalCompile(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                PRCPRCompilerHd _approveData = this.db.PRCPRCompilerHds.Single(a => a.TransNmbr == _prmCode);
                _approveData.Status = 1;
                this.db.SubmitChanges();
                _result = "Get Approval Success";
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_PRMRApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        PRCRequestHd _prcRequestHd = this.GetSinglePRCRequestHd(_prmCode);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prcRequestHd.TransDate.Year, _prcRequestHd.TransDate.Month, AppModule.GetValue(TransactionType.PurchaseRequest), this._companyTag, ""))
                        {
                            _prcRequestHd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.PurchaseRequest);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSinglePRCRequestHd(_prmCode).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        
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

        public string ApproveCompile(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                PRCPRCompilerHd _approveData = this.db.PRCPRCompilerHds.Single(a => a.TransNmbr == _prmCode);
                _approveData.Status = 2;
                this.db.SubmitChanges();
                _result = "Approve Success";
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Posting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                PRCRequestHd _prcRequestHd = this.db.PRCRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_prcRequestHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_PRMRPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.PurchaseRequest);
                        _transActivity.TransNmbr = _prmCode.ToString();
                        _transActivity.FileNmbr = this.GetSinglePRCPRCompileHd(_prmCode).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string PostingCompile(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                PRCPRCompilerHd _approveData = this.db.PRCPRCompilerHds.Single(a => a.TransNmbr == _prmCode);
                _approveData.Status = 3;
                this.db.SubmitChanges();
                _result = "Posting Success";
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string UnPosting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                PRCRequestHd _prcRequestHd = this.db.PRCRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_prcRequestHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_PRMRUnPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string UnPostingCompile(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                PRCPRCompilerHd _approveData = this.db.PRCPRCompilerHds.Single(a => a.TransNmbr == _prmCode);
                _approveData.Status = 2;
                this.db.SubmitChanges();
                _result = "UnPosting Success";
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Close(string _prmCode, string _prmProductCode, string _prmRemark, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                PRCRequestHd _prcRequestHd = this.db.PRCRequestHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_prcRequestHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_PRMRClosing(_prmCode, _prmProductCode, _prmRemark, _prmuser, ref _result);
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "You Failed Close";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string GetFileNmbrPRCRequestHd(string _prmTransNmbr)
        {
            string _result = "";

            try
            {
                _result = (this.db.PRCRequestHds.Single(_temp => _temp.TransNmbr == _prmTransNmbr).FileNmbr ?? "").Trim();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region PRCRequestDt

        public int RowsCountPRCRequestDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.PRCRequestDts.Where(_row => _row.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()).Count();

            return _result;
        }

        public int RowsCountPRCPRCompileDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.PRCPRCompilerDts.Where(_row => _row.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()).Count();

            return _result;
        }

        public List<PRCRequestDt> GetListPRCRequestDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<PRCRequestDt> _result = new List<PRCRequestDt>();

            try
            {
                var _query =
                            (
                                from _prcRequestDt in this.db.PRCRequestDts
                                where _prcRequestDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _prcRequestDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _prcRequestDt.TransNmbr,
                                    ProductCode = _prcRequestDt.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode == _prcRequestDt.ProductCode
                                                    select _msProduct.ProductName
                                                  ).FirstOrDefault(),
                                    Specification = _prcRequestDt.Specification,
                                    Qty = _prcRequestDt.Qty,
                                    Unit = _prcRequestDt.Unit,
                                    RequireDate = _prcRequestDt.RequireDate,
                                    Remark = _prcRequestDt.Remark,
                                    DoneClosing = _prcRequestDt.DoneClosing,
                                    QtyPO = _prcRequestDt.QtyPO,
                                    QtyClose = _prcRequestDt.QtyClose,
                                    EstPrice = _prcRequestDt.EstPrice,
                                    CreatedBy = _prcRequestDt.CreatedBy
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new PRCRequestDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.Specification, _row.Qty, _row.Unit, _row.RequireDate, _row.Remark, _row.DoneClosing, _row.QtyPO, _row.QtyClose, _row.EstPrice, _row.CreatedBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<String> GetListProductPRCRequestDt(string _prmTransNmbr)
        {
            List<String> _result = new List<String>();

            try
            {
                var _query =
                            (
                                from _prcRequestDt in this.db.PRCRequestDts
                                where _prcRequestDt.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()
                                orderby _prcRequestDt.ProductCode ascending
                                select new
                                {
                                    ProductCode = _prcRequestDt.ProductCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(_row.ProductCode);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<PRCPRCompilerDt> GetListPRCPRCompileDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<PRCPRCompilerDt> _result = new List<PRCPRCompilerDt>();

            try
            {
                var _query =
                            (
                                from _prcPRCompileDt in this.db.PRCPRCompilerDts
                                where _prcPRCompileDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select _prcPRCompileDt
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public PRCRequestDt GetSinglePRCRequestDt(string _prmCode, string _prmProductCode)
        {
            PRCRequestDt _result = null;

            try
            {
                _result = this.db.PRCRequestDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPRCRequestDt(string[] _prmProductCode, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmProductCode.Length; i++)
                {
                    PRCRequestDt _prcRequestDt = this.db.PRCRequestDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _prmProductCode[i].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.PRCRequestDts.DeleteOnSubmit(_prcRequestDt);
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

        public bool DeleteMultiPRCPRCompileDt(string[] _prmProductCode, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmProductCode.Length; i++)
                {
                    PRCPRCompilerDt _prcPRCompileDt = this.db.PRCPRCompilerDts.Single(_temp => _temp.XMLFileName.Trim().ToLower() == _prmProductCode[i].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.PRCPRCompilerDts.DeleteOnSubmit(_prcPRCompileDt);
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

        public bool AddPRCRequestDt(PRCRequestDt _prmPRCRequestDt)
        {
            bool _result = false;
            try
            {
                this.db.PRCRequestDts.InsertOnSubmit(_prmPRCRequestDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddPRCRequestDtList(List<PRCRequestDt> _prmPRCRequestList)
        {
            bool _result = false;

            try
            {
                foreach (PRCRequestDt _row in _prmPRCRequestList)
                {
                    PRCRequestDt _prcRequestDt = new PRCRequestDt();

                    _prcRequestDt.TransNmbr = _row.TransNmbr;
                    _prcRequestDt.ProductCode = _row.ProductCode;
                    _prcRequestDt.Specification = _row.Specification;
                    _prcRequestDt.Qty = _row.Qty;
                    _prcRequestDt.EstPrice = _row.EstPrice;
                    _prcRequestDt.Unit = _row.Unit;
                    _prcRequestDt.RequireDate = _row.RequireDate;
                    _prcRequestDt.Remark = _row.Remark;
                    _prcRequestDt.DoneClosing = _row.DoneClosing;
                    _prcRequestDt.CreatedBy = _row.CreatedBy;
                    _prcRequestDt.CreatedDate = _row.CreatedDate;
                    _prcRequestDt.EditBy = _row.EditBy;
                    _prcRequestDt.EditDate = _row.EditDate;

                    this.db.PRCRequestDts.InsertOnSubmit(_prcRequestDt);
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

        public bool EditPRCRequestDt(PRCRequestDt _prmPRCRequestDt)
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

        public List<PRCRequestDt> GetPRCRequestDtListOfHeader(String _prmTransNmbr)
        {
            List<PRCRequestDt> _result = new List<PRCRequestDt>();
            try
            {
                var _qry = (
                        from _prcRequestDt in this.db.PRCRequestDts
                        where _prcRequestDt.TransNmbr == _prmTransNmbr
                        select _prcRequestDt
                    );
                foreach (var _rs in _qry)
                {
                    _result.Add(_rs);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        #endregion


        #region PurchaseRequestXML

        public Boolean IsXMLFileExist(String _prmXMLFileName)
        {
            Boolean _result = false;
            try
            {
                if ((from _prcXmlList in this.db.PRCPRxmlLists where _prcXmlList.XMLFileName == _prmXMLFileName select _prcXmlList).Count() > 0)
                    _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public bool DeleteMultiPRCXMLApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    PRCPRxmlList _prcPrXMLList = this.db.PRCPRxmlLists.Single(_temp => _temp.XMLFileName.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_prcPrXMLList != null)
                    {
                        this.db.PRCPRxmlLists.DeleteOnSubmit(_prcPrXMLList);
                        File.Delete(ApplicationConfig.UploadXMLPurchaseRequestPath + _prmCode[i]);
                        _result = true;
                    }
                }
                if (_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPRCXMLCompile(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    PRCPRxmlList _prcPrXMLCompile = this.db.PRCPRxmlLists.Single(_temp => _temp.XMLFileName.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_prcPrXMLCompile != null)
                    {
                        this.db.PRCPRxmlLists.DeleteOnSubmit(_prcPrXMLCompile);
                        File.Delete(ApplicationConfig.UploadXMLPurchaseRequestHQPath + _prcPrXMLCompile.XMLFileName);
                        _result = true;
                    }
                }
                if (_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCountPRCXMLApprove(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "SenderCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "XMLFileName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result =
                        (
                           from _prcXmlList in this.db.PRCPRxmlLists
                           where (SqlMethods.Like(_prcXmlList.SenderCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like((_prcXmlList.XMLFileName ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                           select _prcXmlList
                        ).Count();

            return _result;
        }

        public double RowsCountPRCXMLtoCompile(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "SenderCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "XMLFileName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result =
                        (
                           from _prcXmlList in this.db.PRCPRxmlLists
                           where (SqlMethods.Like(_prcXmlList.SenderCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like((_prcXmlList.XMLFileName ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                           && _prcXmlList.Status == 2
                           select _prcXmlList
                        ).Count();

            return _result;
        }

        public double RowsCountPRCXMLCompile(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNmbr")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result =
                        (
                           from _prcPrCompileHd in this.db.PRCPRCompilerHds
                           where (SqlMethods.Like(_prcPrCompileHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like((_prcPrCompileHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                           select _prcPrCompileHd
                        ).Count();

            return _result;
        }

        public List<PRCPRxmlList> GetListPRCXMLApprove(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<PRCPRxmlList> _result = new List<PRCPRxmlList>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "SenderCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "XMLFileName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _prcXMLList in this.db.PRCPRxmlLists
                                where (SqlMethods.Like(_prcXMLList.SenderCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_prcXMLList.XMLFileName ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _prcXMLList.Status == 0
                                select _prcXMLList
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<PRCPRxmlList> GetListPRCXMLtoCompile(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<PRCPRxmlList> _result = new List<PRCPRxmlList>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "SenderCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "XMLFileName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _prcXMLList in this.db.PRCPRxmlLists
                                where (SqlMethods.Like(_prcXMLList.SenderCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_prcXMLList.XMLFileName ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _prcXMLList.Status == 2
                                select _prcXMLList
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<PRCPRCompilerHd> GetListPRCXMLCompile(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<PRCPRCompilerHd> _result = new List<PRCPRCompilerHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNmbr")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _prcPrCompilerHd in this.db.PRCPRCompilerHds
                                where (SqlMethods.Like(_prcPrCompilerHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_prcPrCompilerHd.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _prcPrCompilerHd
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool ApproveXML(String _prmXMLFileName)
        {
            bool _result = false;
            try
            {
                PRCPRxmlList _approveData = this.db.PRCPRxmlLists.Single(a => a.XMLFileName == _prmXMLFileName);
                _approveData.Status = 1;
                this.db.SubmitChanges();
                File.Move(ApplicationConfig.UploadXMLPurchaseRequestPath + _approveData.XMLFileName, ApplicationConfig.UploadXMLPurchaseRequestHQPath + _approveData.XMLFileName);
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public bool AddXMLListHQ(PRCPRxmlList _prmXmlList)
        {
            bool _result = false;
            try
            {
                if ((from _prcXmlList in this.db.PRCPRxmlLists where _prcXmlList.XMLFileName == _prmXmlList.XMLFileName select _prmXmlList).Count() > 0)
                {
                    PRCPRxmlList _updateData = this.db.PRCPRxmlLists.Single(a => a.XMLFileName == _prmXmlList.XMLFileName);
                    _updateData.Status = 2;
                    _updateData.Remark = _prmXmlList.Remark;
                    this.db.SubmitChanges();
                }
                else
                {
                    this.db.PRCPRxmlLists.InsertOnSubmit(_prmXmlList);
                    this.db.SubmitChanges();
                }
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<String> getXmlProductList(String _prmListXMLFile)
        {
            List<String> _result = new List<string>();
            try
            {
                String[] _xmlFileList = _prmListXMLFile.Split(',');
                foreach (String _singleXmlFile in _xmlFileList)
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(ApplicationConfig.UploadXMLPurchaseRequestHQPath + _singleXmlFile);
                    XmlNodeList ProductCode = xDoc.GetElementsByTagName("ProductCode");
                    for (int i = 0; i < ProductCode.Count; i++)
                    {
                        if (_result.IndexOf(ProductCode[i].InnerText) < 0)
                            _result.Add(ProductCode[i].InnerText);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<String> getSenderCodeList(String _prmListXMLFile) {
            List<String> _result = new List<String>();
            try
            {
                String[] _xmlFileList = _prmListXMLFile.Split(',');
                foreach (String _singleXmlFile in _xmlFileList) {
                    String _currentFileSenderCode = (
                            from _prcPrXmlFileList in this.db.PRCPRxmlLists 
                            where _prcPrXmlFileList.XMLFileName == _singleXmlFile 
                            select _prcPrXmlFileList.SenderCode 
                        ).FirstOrDefault() ;
                    if (_result.IndexOf(_currentFileSenderCode) < 0)
                        _result.Add(_currentFileSenderCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Double getQtyOfProductFromSenderCode(String _prmListXMLFile, String _prmSenderCode, String _prmProductCode) {
            Double _result = 0;
            try
            {
                String[] _xmlFileList = _prmListXMLFile.Split(',');
                foreach (String _singleXmlFile in _xmlFileList) {
                    if ( (from _prcPrXmlFileList in this.db.PRCPRxmlLists where _prcPrXmlFileList.XMLFileName == _singleXmlFile && _prcPrXmlFileList.SenderCode == _prmSenderCode select _prcPrXmlFileList ).Count() > 0 ) {
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.Load(ApplicationConfig.UploadXMLPurchaseRequestHQPath + _singleXmlFile);
                        XmlNodeList ProductCode = xDoc.GetElementsByTagName("ProductCode");
                        XmlNodeList Qty = xDoc.GetElementsByTagName("Qty");
                        for (int i = 0; i < ProductCode.Count; i++)
                        {
                            if ( ProductCode[i].InnerText == _prmProductCode  )
                                _result += Convert.ToDouble(Qty[i].InnerText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public Boolean isXMLFileHQUsed(String _prmXmlFileName) {
            Boolean _result = false;
            try
            {
                if ( (from _prcPrXmlList in this.db.PRCPRxmlLists where _prcPrXmlList.XMLFileName == _prmXmlFileName && _prcPrXmlList.Status == 2 select _prcPrXmlList ).Count() > 0 ) {
                    _result = true ;
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        #endregion

        public String GetCustomerCode()
        {
            String _result = "";
            try
            {
                _result = this.db.CompanyConfigurations.Single(a => a.ConfigCode == "CustomerCode").SetValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean InsertPRCPRxmlList(String _prmSenderCode, String _prmXmlName)
        {
            Boolean _result = false;
            try
            {
                if ((from _xmlFileList in this.db.PRCPRxmlLists where _xmlFileList.SenderCode == _prmSenderCode && _xmlFileList.XMLFileName == _prmXmlName select _xmlFileList).Count() == 0)
                {
                    PRCPRxmlList _addData = new PRCPRxmlList();
                    _addData.SenderCode = _prmSenderCode;
                    _addData.XMLFileName = _prmXmlName;
                    _addData.Status = 0;
                    this.db.PRCPRxmlLists.InsertOnSubmit(_addData);
                    this.db.SubmitChanges();
                }
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        ~PurchaseRequestBL()
        {

        }
    }
}
