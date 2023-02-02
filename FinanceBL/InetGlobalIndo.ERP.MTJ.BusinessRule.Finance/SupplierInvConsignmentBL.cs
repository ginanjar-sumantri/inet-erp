using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class SupplierInvConsignmentBL : Base
    {
        public SupplierInvConsignmentBL()
        {

        }

        #region FINCNSuppHd
        public double RowsCountSuppInvConsignHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            var _query =
                        (
                            from _finSuppInvConsignmentHds in this.db.FINSuppInvConsignmentHds
                            join _msSupp in this.db.MsSuppliers
                                on _finSuppInvConsignmentHds.SuppCode equals _msSupp.SuppCode
                            where (SqlMethods.Like(_finSuppInvConsignmentHds.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_finSuppInvConsignmentHds.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _finSuppInvConsignmentHds.Status != SupplierInvoiceDataMapper.GetStatus(TransStatus.Deleted)
                            select _finSuppInvConsignmentHds.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINSuppInvConsignmentHd> GetListSuppInvConsignHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINSuppInvConsignmentHd> _result = new List<FINSuppInvConsignmentHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _FfinSuppInvConsignmentHd in this.db.FINSuppInvConsignmentHds
                                join _msSupp in this.db.MsSuppliers
                                    on _FfinSuppInvConsignmentHd.SuppCode equals _msSupp.SuppCode
                                where (SqlMethods.Like(_FfinSuppInvConsignmentHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_FfinSuppInvConsignmentHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _FfinSuppInvConsignmentHd.Status != SupplierInvoiceDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _FfinSuppInvConsignmentHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _FfinSuppInvConsignmentHd.TransNmbr,
                                    FileNmbr = _FfinSuppInvConsignmentHd.FileNmbr,
                                    TransDate = _FfinSuppInvConsignmentHd.TransDate,
                                    CurrCode = _FfinSuppInvConsignmentHd.CurrCode,
                                    Status = _FfinSuppInvConsignmentHd.Status,
                                    SuppCode = _FfinSuppInvConsignmentHd.SuppCode,
                                    SuppName = _msSupp.SuppName,
                                    Term = _FfinSuppInvConsignmentHd.Term,
                                    TermName = (
                                                    from _msTerm in this.db.MsTerms
                                                    where _FfinSuppInvConsignmentHd.Term == _msTerm.TermCode
                                                    select _msTerm.TermName
                                                ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINSuppInvConsignmentHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.SuppCode, _row.SuppName, _row.Term, _row.TermName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINSuppInvConsignmentHd GetSingleSuppInvConsignHd(string _prmCode)
        {
            FINSuppInvConsignmentHd _result = null;

            try
            {
                _result = this.db.FINSuppInvConsignmentHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINSuppInvConsignmentHd GetSingleSuppInvConsignHdView(string _prmCode)
        {
            FINSuppInvConsignmentHd _result = new FINSuppInvConsignmentHd();

            try
            {
                var _query = (
                               from _finSuppInvConsignmentHd in this.db.FINSuppInvConsignmentHds
                               join _msSupp in this.db.MsSuppliers
                                    on _finSuppInvConsignmentHd.SuppCode equals _msSupp.SuppCode
                               join _msTerm in this.db.MsTerms
                               on _finSuppInvConsignmentHd.Term equals _msTerm.TermCode
                               orderby _finSuppInvConsignmentHd.DatePrep descending
                               where _finSuppInvConsignmentHd.TransNmbr == _prmCode
                               select new
                               {
                                   TransNmbr = _finSuppInvConsignmentHd.TransNmbr,
                                   FileNmbr = _finSuppInvConsignmentHd.FileNmbr,
                                   TransDate = _finSuppInvConsignmentHd.TransDate,
                                   Status = _finSuppInvConsignmentHd.Status,
                                   SuppCode = _finSuppInvConsignmentHd.SuppCode,
                                   SuppName = _msSupp.SuppName,
                                   CurrCode = _finSuppInvConsignmentHd.CurrCode,
                                   ForexRate = _finSuppInvConsignmentHd.ForexRate,
                                   TotalForex = _finSuppInvConsignmentHd.TotalForex,
                                   Term = _finSuppInvConsignmentHd.Term,
                                   TermName = _msTerm.TermName,
                                   DiscForex = _finSuppInvConsignmentHd.DiscForex,
                                   BaseForex = _finSuppInvConsignmentHd.BaseForex,
                                   //Attn = _finSuppInvConsignmentHd.Attn,
                                   Remark = _finSuppInvConsignmentHd.Remark,
                                   PPN = _finSuppInvConsignmentHd.PPN,
                                   PPNDate = _finSuppInvConsignmentHd.PPNDate,
                                   PPNForex = _finSuppInvConsignmentHd.PPNForex,
                                   PPNNo = _finSuppInvConsignmentHd.PPNNo,
                                   PPNRate = _finSuppInvConsignmentHd.PPNRate
                                   //SuppInvNo = _finSuppInvConsignmentHd.SuppInvNo,
                                   //SuppPONo = _finSuppInvConsignmentHd.SuppPONo
                               }
                           ).Single();

                _result.TransNmbr = _query.TransNmbr;
                _result.FileNmbr = _query.FileNmbr;
                _result.TransDate = _query.TransDate;
                _result.Status = _query.Status;
                _result.SuppCode = _query.SuppCode;
                _result.SuppName = _query.SuppName;
                _result.CurrCode = _query.CurrCode;
                _result.ForexRate = _query.ForexRate;
                _result.TotalForex = _query.TotalForex;
                _result.Term = _query.Term;
                _result.TermName = _query.TermName;
                _result.DiscForex = _query.DiscForex;
                _result.BaseForex = _query.BaseForex;
                //_result.Attn = _query.Attn;
                _result.Remark = _query.Remark;
                _result.PPN = _query.PPN;
                _result.PPNDate = _query.PPNDate;
                _result.PPNForex = _query.PPNForex;
                _result.PPNNo = _query.PPNNo;
                _result.PPNRate = _query.PPNRate;
                //_result.SuppInvNo = _query.SuppInvNo;
                //_result.SuppPONo = _query.SuppPONo;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSuppInvConsignHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINSuppInvConsignmentHd _finSuppInvConsignHd = this.db.FINSuppInvConsignmentHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finSuppInvConsignHd != null)
                    {
                        if ((_finSuppInvConsignHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINSuppInvConsignmentDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINSuppInvConsignmentDts.DeleteAllOnSubmit(_query);

                            this.db.FINSuppInvConsignmentHds.DeleteOnSubmit(_finSuppInvConsignHd);

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

        public string AddSuppInvConsignHd(FINSuppInvConsignmentHd _prmSuppInvConsignHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINCNSuppHd.TransDate.Year, _prmFINCNSuppHd.TransDate.Month, AppModule.GetValue(TransactionType.NotaCreditSupplier), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSuppInvConsignHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINSuppInvConsignmentHds.InsertOnSubmit(_prmSuppInvConsignHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSuppInvConsignHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSuppInvConsignHd(FINSuppInvConsignmentHd _prmSuppInvConsignHd)
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

        public string GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                int _success = this.db.S_FNSIConsignmentGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.SuppInvConsignment);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
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

        public string Approve(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.S_FNSIConsignmentApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINSuppInvConsignmentHd _finSuppInvConsignHd = this.GetSingleSuppInvConsignHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finSuppInvConsignHd.TransDate.Year, _finSuppInvConsignHd.TransDate.Month, AppModule.GetValue(TransactionType.SuppInvConsignment), this._companyTag, ""))
                        {
                            _finSuppInvConsignHd.FileNmbr = item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.SuppInvConsignment);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSuppInvConsignHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

                        _scope.Complete();
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

        public string Posting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINSuppInvConsignmentHd _finSuppInvConsignHd = this.db.FINSuppInvConsignmentHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finSuppInvConsignHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSIConsignmentPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.SuppInvConsignment);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSingleSuppInvConsignHd(_prmTransNmbr).FileNmbr;
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

        public string Unposting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINSuppInvConsignmentHd _finSuppInvConsignHd = this.db.FINSuppInvConsignmentHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finSuppInvConsignHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSIConsignmentUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "Unposting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public bool GetSingleSuppInvConsignHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINSuppInvConsignmentHd _finSuppInvConsignHd = this.db.FINSuppInvConsignmentHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finSuppInvConsignHd != null)
                    {
                        if (_finSuppInvConsignHd.Status != SupplierInvoiceDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveSuppInvConsignHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINSuppInvConsignmentHd _finSuppInvConsignHd = this.db.FINSuppInvConsignmentHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finSuppInvConsignHd.Status == SupplierInvoiceDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finSuppInvConsignHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finSuppInvConsignHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finSuppInvConsignHd != null)
                    {
                        if ((_finSuppInvConsignHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINSuppInvConsignmentDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINSuppInvConsignmentDts.DeleteAllOnSubmit(_query);

                            this.db.FINSuppInvConsignmentHds.DeleteOnSubmit(_finSuppInvConsignHd);

                            _result = true;
                        }
                        else if (_finSuppInvConsignHd.FileNmbr != "" && _finSuppInvConsignHd.Status == SupplierInvoiceDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finSuppInvConsignHd.Status = SupplierInvoiceDataMapper.GetStatus(TransStatus.Deleted);
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

        #endregion

        #region FINCNSuppDt
        public int RowsCountSuppInvConsignDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINSuppInvConsignmentDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINSuppInvConsignmentDt> GetListSuppInvConsignDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<FINSuppInvConsignmentDt> _result = new List<FINSuppInvConsignmentDt>();

            try
            {
                var _query = (
                                from _finSuppInvConsignDt in this.db.FINSuppInvConsignmentDts
                                where _finSuppInvConsignDt.TransNmbr == _prmCode
                                select _finSuppInvConsignDt
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

        public FINSuppInvConsignmentDt GetSingleSuppInvConsignDt(String _prmTransNmbr, String _prmSJType, String _prmSJNo, String _prmProductCode)
        {
            FINSuppInvConsignmentDt _result = null;

            try
            {
                _result = this.db.FINSuppInvConsignmentDts.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.SJType == _prmSJType && _temp.SJNo == _prmSJNo && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public FINSuppInvConsignmentDt GetSingleSuppInvConsignDt(string _prmCode, int _prmItemNo)
        //{
        //    FINSuppInvConsignmentDt _result = null;

        //    try
        //    {
        //        _result = this.db.FINSuppInvConsignmentDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == _prmItemNo);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public FINCNSuppDt GetSingleSuppInvConsignDtView(string _prmCode, int _prmItemNo)
        //{
        //    FINCNSuppDt _result = new FINCNSuppDt();

        //    try
        //    {
        //        var _query = (
        //                       from _finCNSuppDt in this.db.FINCNSuppDts
        //                       join _msAccount in this.db.MsAccounts
        //                            on _finCNSuppDt.Account equals _msAccount.Account
        //                       join _msUnit in this.db.MsUnits
        //                            on _finCNSuppDt.Unit equals _msUnit.UnitCode
        //                       orderby _finCNSuppDt.ItemNo ascending
        //                       where _finCNSuppDt.TransNmbr == _prmCode && _finCNSuppDt.ItemNo == _prmItemNo
        //                       select new
        //                       {
        //                           ItemNo = _finCNSuppDt.ItemNo,
        //                           Account = _finCNSuppDt.Account,
        //                           AccountName = _msAccount.AccountName,
        //                           AmountForex = _finCNSuppDt.AmountForex,
        //                           Subled = _finCNSuppDt.Subled,
        //                           SubledName = (
        //                                            from _viewMsSubled in this.db.V_MsSubleds
        //                                            where _viewMsSubled.SubLed_No == _finCNSuppDt.Subled
        //                                            select _viewMsSubled.SubLed_Name
        //                                         ).FirstOrDefault(),
        //                           PriceForex = _finCNSuppDt.PriceForex,
        //                           Qty = _finCNSuppDt.Qty,
        //                           Unit = _finCNSuppDt.Unit,
        //                           UnitName = _msUnit.UnitName,
        //                           Remark = _finCNSuppDt.Remark
        //                       }
        //                   ).Single();

        //        _result.ItemNo = _query.ItemNo;
        //        _result.Account = _query.Account;
        //        _result.AccountName = _query.AccountName;
        //        _result.AmountForex = _query.AmountForex;
        //        _result.Subled = _query.Subled;
        //        _result.SubledName = _query.SubledName;
        //        _result.PriceForex = _query.PriceForex;
        //        _result.Qty = _query.Qty;
        //        _result.Unit = _query.Unit;
        //        _result.UnitName = _query.UnitName;
        //        _result.Remark = _query.Remark;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool DeleteMultiSuppInvConsignDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            FINSuppInvConsignmentHd _finSuppInvConsignHd = new FINSuppInvConsignmentHd();

            decimal _total = 0;
            Decimal _countAmount = 0;
            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('|');
                    FINSuppInvConsignmentDt _finSuppInvConsignDt = this.db.FINSuppInvConsignmentDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.SJType == _tempSplit[1] && _temp.SJNo == _tempSplit[2] && _temp.ProductCode == _tempSplit[3]);
                    _countAmount = _countAmount + Convert.ToDecimal(_finSuppInvConsignDt.AmountForex);
                    this.db.FINSuppInvConsignmentDts.DeleteOnSubmit(_finSuppInvConsignDt);
                }

                var _query = (
                                from _finSuppInvConsignDt2 in this.db.FINSuppInvConsignmentDts
                                where _finSuppInvConsignDt2.TransNmbr == _prmTransNo
                                group _finSuppInvConsignDt2 by _finSuppInvConsignDt2.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = Convert.ToDecimal(_obj.AmountForex);
                }

                _finSuppInvConsignHd = this.db.FINSuppInvConsignmentHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _finSuppInvConsignHd.BaseForex = _total - _countAmount;
                if (_finSuppInvConsignHd.BaseForex != 0)
                {
                    _finSuppInvConsignHd.PPNForex = (_finSuppInvConsignHd.BaseForex - _finSuppInvConsignHd.DiscForex) * _finSuppInvConsignHd.PPN / 100;
                    _finSuppInvConsignHd.TotalForex = _finSuppInvConsignHd.BaseForex - _finSuppInvConsignHd.DiscForex + _finSuppInvConsignHd.PPNForex;
                }
                else
                {
                    _finSuppInvConsignHd.PPNForex = 0;
                    _finSuppInvConsignHd.TotalForex = 0;
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

        public String SaveDetailSuppInvCon(String _prmSJType, String _prmSJNo, String _prmTransNmbr, String _prmSuppCode)
        {
            String _result = "";

            try
            {
                var _query = (from _finSuppInvConsDt in this.db.FINSuppInvConsignmentDts
                              where _finSuppInvConsDt.TransNmbr == _prmTransNmbr
                              && _finSuppInvConsDt.SJType == _prmSJType
                              && _finSuppInvConsDt.SJNo == _prmSJNo
                              select _finSuppInvConsDt
                                ).Count();

                if (_query == 0)
                {
                    List<V_FNSIConsignmentOustanding> _viewFNSICons = this.GetListViewFINSICon(_prmSJType, _prmSJNo, _prmSuppCode);

                    foreach (var _row in _viewFNSICons)
                    {
                        FINSuppInvConsignmentDt _finSuppInvConDt = new FINSuppInvConsignmentDt();

                        _finSuppInvConDt.TransNmbr = _prmTransNmbr;
                        _finSuppInvConDt.SJType = _row.SJ_Type;
                        _finSuppInvConDt.SJNo = _row.SJ_No;
                        _finSuppInvConDt.ProductCode = _row.Product_Code;
                        _finSuppInvConDt.Qty = Convert.ToDecimal(_row.Qty);
                        _finSuppInvConDt.Unit = _row.Unit;
                        _finSuppInvConDt.PriceForex = 0;
                        _finSuppInvConDt.AmountForex = 0;
                        _finSuppInvConDt.Remark = "";
                        _finSuppInvConDt.Account = "";
                        _finSuppInvConDt.FgSubLed = 'N';

                        this.db.FINSuppInvConsignmentDts.InsertOnSubmit(_finSuppInvConDt);
                    }

                    this.db.SubmitChanges();
                }
                else
                {
                    _result = "There Are Already Existing Data";
                }
            }
            catch (Exception ex1)
            {
                _result = "You Failed Add Data";
            }

            return _result;
        }

        public bool EditSuppInvConsignDt(FINSuppInvConsignmentDt _prmFINSuppInvConsignDt, Decimal _prmAmount)
        {
            bool _result = false;
            decimal _tempBaseAmount = 0;
            decimal _tempDiscAmount = 0;
            decimal _tempDisc = 0;
            //decimal _tempTex = 0;
            decimal _tempPPNAmount = 0;
            decimal _tempTotalAmount = 0;

            try
            {
                FINSuppInvConsignmentHd _finSuppInvConsignHd = this.GetSingleSuppInvConsignHd(_prmFINSuppInvConsignDt.TransNmbr);

                _tempBaseAmount = Convert.ToDecimal(_finSuppInvConsignHd.BaseForex - _prmFINSuppInvConsignDt.AmountForex) + Convert.ToDecimal(_prmAmount);

                //_tempBaseAmount = _directSalesHd.BaseForex + Convert.ToDecimal(_prmAmount);
                _prmFINSuppInvConsignDt.AmountForex = _prmAmount;
                //_tempDiscAmount = _directSalesHd.DiscAmount;
                if (_tempBaseAmount != 0)
                    _tempDisc = (_finSuppInvConsignHd.DiscForex / _tempBaseAmount) * 100;
                //_tempTex = (_finSuppInvConsignHd.PPNForex / (_finSuppInvConsignHd.BaseForex - _finSuppInvConsignHd.DiscForex)) * 100;

                if (_tempDisc == 0)
                {
                    _tempDiscAmount = _finSuppInvConsignHd.DiscForex;
                }
                else
                {
                    _tempDiscAmount = Math.Round((_tempBaseAmount * _tempDisc) / 100, 2);
                }

                if (_finSuppInvConsignHd.PPN == 0)
                {
                    _tempPPNAmount = _finSuppInvConsignHd.PPNForex;
                }
                else
                {
                    if (_tempBaseAmount != 0)
                        _tempPPNAmount = Convert.ToDecimal(Math.Round(((_tempBaseAmount - _tempDiscAmount) * _finSuppInvConsignHd.PPN) / 100, 2));
                    else
                        _tempPPNAmount = 0;
                }
                if (_tempBaseAmount != 0)
                    _tempTotalAmount = _tempBaseAmount - _tempDiscAmount + _tempPPNAmount;

                _finSuppInvConsignHd.BaseForex = _tempBaseAmount;
                _finSuppInvConsignHd.DiscForex = _tempDiscAmount;
                _finSuppInvConsignHd.PPNForex = _tempPPNAmount;
                _finSuppInvConsignHd.TotalForex = _tempTotalAmount;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public bool AddSuppInvConsignDt(FINCNSuppDt _prmFINCNSuppDt)
        //{
        //    bool _result = false;

        //    FINCNSuppHd _finCNSuppHd = new FINCNSuppHd();

        //    decimal _total = 0;

        //    try
        //    {
        //        var _query = (
        //                       from _finCNSuppDt in this.db.FINCNSuppDts
        //                       where !(
        //                                   from _finCNSuppDt2 in this.db.FINCNSuppDts
        //                                   where _finCNSuppDt2.ItemNo == _prmFINCNSuppDt.ItemNo && _finCNSuppDt2.TransNmbr == _prmFINCNSuppDt.TransNmbr
        //                                   select _finCNSuppDt2.ItemNo
        //                               ).Contains(_finCNSuppDt.ItemNo)
        //                               && _finCNSuppDt.TransNmbr == _prmFINCNSuppDt.TransNmbr
        //                       group _finCNSuppDt by _finCNSuppDt.TransNmbr into _grp
        //                       select new
        //                       {
        //                           AmountForex = _grp.Sum(a => a.AmountForex)
        //                       }
        //                     );

        //        foreach (var _obj in _query)
        //        {
        //            _total = _obj.AmountForex;
        //        }


        //        _finCNSuppHd = this.db.FINCNSuppHds.Single(_fa => _fa.TransNmbr == _prmFINCNSuppDt.TransNmbr);

        //        _finCNSuppHd.BaseForex = _total + _prmFINCNSuppDt.AmountForex;
        //        _finCNSuppHd.PPNForex = (_finCNSuppHd.BaseForex - _finCNSuppHd.DiscForex) * _finCNSuppHd.PPN / 100;
        //        _finCNSuppHd.TotalForex = _finCNSuppHd.BaseForex - _finCNSuppHd.DiscForex + _finCNSuppHd.PPNForex;

        //        this.db.FINCNSuppDts.InsertOnSubmit(_prmFINCNSuppDt);

        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool EditSuppInvConsignDt(FINCNSuppDt _prmFINCNSuppDt)
        //{
        //    bool _result = false;

        //    FINCNSuppHd _finCNSuppHd = new FINCNSuppHd();

        //    decimal _total = 0;

        //    try
        //    {
        //        var _query = (
        //                       from _finCNSuppDt in this.db.FINCNSuppDts
        //                       where !(
        //                                   from _finCNSuppDt2 in this.db.FINCNSuppDts
        //                                   where _finCNSuppDt2.ItemNo == _prmFINCNSuppDt.ItemNo && _finCNSuppDt2.TransNmbr == _prmFINCNSuppDt.TransNmbr
        //                                   select _finCNSuppDt2.ItemNo
        //                               ).Contains(_finCNSuppDt.ItemNo)
        //                               && _finCNSuppDt.TransNmbr == _prmFINCNSuppDt.TransNmbr
        //                       group _finCNSuppDt by _finCNSuppDt.TransNmbr into _grp
        //                       select new
        //                       {
        //                           AmountForex = _grp.Sum(a => a.AmountForex)
        //                       }
        //                     );

        //        foreach (var _obj in _query)
        //        {
        //            _total = _obj.AmountForex;
        //        }

        //        _finCNSuppHd = this.db.FINCNSuppHds.Single(_fa => _fa.TransNmbr == _prmFINCNSuppDt.TransNmbr);

        //        _finCNSuppHd.BaseForex = _total + _prmFINCNSuppDt.AmountForex;
        //        _finCNSuppHd.PPNForex = (_finCNSuppHd.BaseForex - _finCNSuppHd.DiscForex) * _finCNSuppHd.PPN / 100;
        //        _finCNSuppHd.TotalForex = _finCNSuppHd.BaseForex - _finCNSuppHd.DiscForex + _finCNSuppHd.PPNForex;

        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public int GetMaxNoItemSuppInvConsignDt(string _prmCode)
        //{
        //    int _result = 0;

        //    try
        //    {
        //        _result = this.db.FINCNSuppDts.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}
        #endregion

        #region View_FNSIConsignmentOustanding

        public List<V_FNSIConsignmentOustanding> GetDDLforSJType(String _prmSupplier)
        {
            List<V_FNSIConsignmentOustanding> _result = new List<V_FNSIConsignmentOustanding>();

            try
            {
                var _query = (from _view in this.db.V_FNSIConsignmentOustandings
                              where _view.Supplier == _prmSupplier
                              select new
                              {
                                  _view.SJ_Type
                              }
                            ).Distinct();
                foreach (var _row in _query)
                {
                    V_FNSIConsignmentOustanding _result1 = new V_FNSIConsignmentOustanding();
                    _result1.SJ_Type = _row.SJ_Type;
                    _result.Add(_result1);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public List<V_FNSIConsignmentOustanding> GetDDLforSJNo(String _prmSupplier, String _prmSJType)
        {
            List<V_FNSIConsignmentOustanding> _result = new List<V_FNSIConsignmentOustanding>();

            try
            {
                var _query = (from _view in this.db.V_FNSIConsignmentOustandings
                              where _view.Supplier == _prmSupplier
                              && _view.SJ_Type == _prmSJType
                              select new
                              {
                                  _view.SJ_Type,
                                  _view.SJ_No,
                                  _view.Supplier
                              }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    V_FNSIConsignmentOustanding _result1 = new V_FNSIConsignmentOustanding();
                    _result1.SJ_Type = _row.SJ_Type;
                    _result1.SJ_No = _row.SJ_No;
                    _result1.Supplier = _row.Supplier;

                    _result.Add(_result1);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public List<V_FNSIConsignmentOustanding> GetListViewFINSICon(String _prmSJType, String _prmSJNo, String _prmSuppCode)
        {
            List<V_FNSIConsignmentOustanding> _result = new List<V_FNSIConsignmentOustanding>();

            try
            {
                var _query = (from _view in this.db.V_FNSIConsignmentOustandings
                              where _view.SJ_Type == _prmSJType
                              && _view.SJ_No == _prmSJNo
                              && _view.Supplier == _prmSuppCode
                              select _view
                            );
                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }


        #endregion

        ~SupplierInvConsignmentBL()
        {
        }
    }
}
