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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class SupplierReturBL : Base
    {
        public SupplierReturBL()
        {

        }

        #region FINSuppReturHd
        public double RowsCountFINSuppReturHd(string _prmCategory, string _prmKeyword)
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
                           from _FINSuppReturHd in this.db.FINSuppReturHds
                           join _msSupp in this.db.MsSuppliers
                               on _FINSuppReturHd.SuppCode equals _msSupp.SuppCode
                           where (SqlMethods.Like(_FINSuppReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                              && (SqlMethods.Like((_FINSuppReturHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                              && _FINSuppReturHd.Status != SupplierReturDataMapper.GetStatus(TransStatus.Deleted)
                           select _FINSuppReturHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINSuppReturHd> GetListFINSuppReturHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINSuppReturHd> _result = new List<FINSuppReturHd>();

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
                                from _FINSuppReturHd in this.db.FINSuppReturHds
                                join _msSupp in this.db.MsSuppliers
                                    on _FINSuppReturHd.SuppCode equals _msSupp.SuppCode
                                where (SqlMethods.Like(_FINSuppReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_FINSuppReturHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _FINSuppReturHd.Status != SupplierReturDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _FINSuppReturHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _FINSuppReturHd.TransNmbr,
                                    FileNmbr = _FINSuppReturHd.FileNmbr,
                                    TransDate = _FINSuppReturHd.TransDate,
                                    CurrCode = _FINSuppReturHd.CurrCode,
                                    Status = _FINSuppReturHd.Status,
                                    SuppCode = _FINSuppReturHd.SuppCode,
                                    SuppName = _msSupp.SuppName,
                                    Term = _FINSuppReturHd.Term,
                                    TermName = (
                                                    from _msTerm in this.db.MsTerms
                                                    where _FINSuppReturHd.Term == _msTerm.TermCode
                                                    select _msTerm.TermName
                                                ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINSuppReturHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.SuppCode, _row.SuppName, _row.Term, _row.TermName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINSuppReturHd GetSingleFINSuppReturHd(string _prmCode)
        {
            FINSuppReturHd _result = null;

            try
            {
                _result = this.db.FINSuppReturHds.Single(_temp => _temp.TransNmbr.ToLower() == _prmCode.ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINSuppReturHd GetSingleFINSuppReturHdView(string _prmCode)
        {
            FINSuppReturHd _result = new FINSuppReturHd();

            try
            {
                var _query = (
                               from _FINSuppReturHd in this.db.FINSuppReturHds
                               join _msSupp in this.db.MsSuppliers
                                    on _FINSuppReturHd.SuppCode equals _msSupp.SuppCode
                               join _msTerm in this.db.MsTerms
                                    on _FINSuppReturHd.Term equals _msTerm.TermCode
                               orderby _FINSuppReturHd.DatePrep descending
                               where _FINSuppReturHd.TransNmbr == _prmCode
                               select new
                               {
                                   TransNmbr = _FINSuppReturHd.TransNmbr,
                                   FileNmbr = _FINSuppReturHd.FileNmbr,
                                   TransDate = _FINSuppReturHd.TransDate,
                                   Status = _FINSuppReturHd.Status,
                                   SuppCode = _FINSuppReturHd.SuppCode,
                                   SuppName = _msSupp.SuppName,
                                   CurrCode = _FINSuppReturHd.CurrCode,
                                   ForexRate = _FINSuppReturHd.ForexRate,
                                   //OtherForex = _FINSuppReturHd.OtherForex,
                                   TotalForex = _FINSuppReturHd.TotalForex,
                                   Term = _FINSuppReturHd.Term,
                                   TermName = _msTerm.TermName,
                                   //DiscForex = _FINSuppReturHd.DiscForex,
                                   BaseForex = _FINSuppReturHd.BaseForex,
                                   Attn = _FINSuppReturHd.Attn,
                                   Remark = _FINSuppReturHd.Remark,
                                   PPN = _FINSuppReturHd.PPN,
                                   PPNDate = _FINSuppReturHd.PPNDate,
                                   PPNForex = _FINSuppReturHd.PPNForex,
                                   PPNNo = _FINSuppReturHd.PPNNo,
                                   PPNRate = _FINSuppReturHd.PPNRate                               }
                           ).Single();

                _result.TransNmbr = _query.TransNmbr;
                _result.FileNmbr = _query.FileNmbr;
                _result.TransDate = _query.TransDate;
                _result.Status = _query.Status;
                _result.SuppCode = _query.SuppCode;
                _result.SuppName = _query.SuppName;
                _result.CurrCode = _query.CurrCode;
                _result.ForexRate = _query.ForexRate;
                //_result.OtherForex = _query.OtherForex;
                _result.TotalForex = _query.TotalForex;
                _result.Term = _query.Term;
                _result.TermName = _query.TermName;
                //_result.DiscForex = _query.DiscForex;
                _result.BaseForex = _query.BaseForex;
                _result.Attn = _query.Attn;
                _result.Remark = _query.Remark;
                _result.PPN = _query.PPN;
                _result.PPNDate = _query.PPNDate;
                _result.PPNForex = _query.PPNForex;
                _result.PPNNo = _query.PPNNo;
                _result.PPNRate = _query.PPNRate;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFileNmbrV_STSJForCIsBySJNo(string _prmCode)
        {
            string _result = null;

            try
            {
                _result = this.db.V_STSJForCIs.Single(_temp => _temp.SJ_No.ToLower() == _prmCode.ToLower()).FileNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINSuppReturHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINSuppReturHd _FINSuppReturHd = this.db.FINSuppReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_FINSuppReturHd != null)
                    {
                        if ((_FINSuppReturHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINSuppReturDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            //var _query1 = (from _detail1 in this.db.FINCustInvSJLists
                            //               where _detail1.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                            //               select _detail1);

                            //this.db.FINCustInvSJLists.DeleteAllOnSubmit(_query1);

                            this.db.FINSuppReturDts.DeleteAllOnSubmit(_query);

                            this.db.FINSuppReturHds.DeleteOnSubmit(_FINSuppReturHd);

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

        public string AddFINSuppReturHd(FINSuppReturHd _prmFINSuppReturHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINSuppReturHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.FINSuppReturHds.InsertOnSubmit(_prmFINSuppReturHd);
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINSuppReturHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINSuppReturHd(FINSuppReturHd _prmFINSuppReturHd)
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
                int _success = this.db.S_FNSuppReturGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.SupplierRetur);
                    _transActivity.TransNmbr = _prmTransNmbr;
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
                    int _success = this.db.S_FNSuppReturApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINSuppReturHd _FINSuppReturHd = this.GetSingleFINSuppReturHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_FINSuppReturHd.TransDate.Year, _FINSuppReturHd.TransDate.Month, AppModule.GetValue(TransactionType.SupplierRetur), this._companyTag, ""))
                        {
                            _FINSuppReturHd.FileNmbr = _item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.SupplierRetur);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINSuppReturHd(_prmTransNmbr).FileNmbr;
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
                FINSuppReturHd _FINSuppReturHd = this.db.FINSuppReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_FINSuppReturHd.TransDate);

                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSuppReturPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.SupplierRetur);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINSuppReturHd(_prmTransNmbr).FileNmbr;
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
                FINSuppReturHd _FINSuppReturHd = this.db.FINSuppReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_FINSuppReturHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSuppReturUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        //_result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.CustomerNote);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINSuppReturHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINSuppReturHd(_prmTransNmbr).TransDate;
                        //_transActivity.Reason = "";

                        //this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        //this.db.SubmitChanges();
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

        public bool GetSingleFINSuppReturHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINSuppReturHd _FINSuppReturHd = this.db.FINSuppReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_FINSuppReturHd != null)
                    {
                        if (_FINSuppReturHd.Status != SupplierReturDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINSuppReturHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINSuppReturHd _FINSuppReturHd = this.db.FINSuppReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_FINSuppReturHd.Status == CustomerNoteDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _FINSuppReturHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _FINSuppReturHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_FINSuppReturHd != null)
                    {
                        if ((_FINSuppReturHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINSuppReturDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            //var _query1 = (from _detail1 in this.db.FINCustInvSJLists
                            //               where _detail1.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                            //               select _detail1);

                            //this.db.FINCustInvSJLists.DeleteAllOnSubmit(_query1);

                            this.db.FINSuppReturDts.DeleteAllOnSubmit(_query);

                            this.db.FINSuppReturHds.DeleteOnSubmit(_FINSuppReturHd);

                            _result = true;
                        }
                        else if (_FINSuppReturHd.FileNmbr != "" && _FINSuppReturHd.Status == SupplierReturDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _FINSuppReturHd.Status = SupplierReturDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINSuppReturDt

        public List<STCReturRRHd> GetListRRnoForDDL()
        {
            List<STCReturRRHd> _result = new List<STCReturRRHd>();
            try
            {
                var _query = (from _stcReturRRHds in this.db.STCReturRRHds
                              join _stcReturRRDts in this.db.STCReturRRDts
                              on _stcReturRRHds.TransNmbr equals _stcReturRRDts.TransNmbr
                              where _stcReturRRHds.FgDeliveryBack != 'Y'
                              && (_stcReturRRDts.Qty - _stcReturRRDts.QtySJ) != 0
                              select new
                              {
                                  RRno = _stcReturRRHds.TransNmbr,
                                  FileNmbr = _stcReturRRHds.FileNmbr
                              }
                              ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new STCReturRRHd(_row.RRno, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public String SaveFINSuppReturDt(String _prmTransNmbr, String _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (from _finSuppReturDts in this.db.FINSuppReturDts
                              where _finSuppReturDts.SJNo == _prmTransNmbr
                              select _finSuppReturDts
                                ).Count();

                if (_query == 0)
                {
                    List<STCRejectOutDt> _stcRejectOutDt = this.GetListSTCRejectOutDt(_prmTransNmbr);
                    FINSuppReturHd _finCustReturHd = this.GetSingleFINSuppReturHd(_prmCode);
                    Decimal _totalForex = 0;

                    foreach (var _row in _stcRejectOutDt)
                    {
                        String _reqReturNo = this.GetReqReturNoSTCRejectOutHd(_prmTransNmbr);
                        //STCReturRRHd _stcReturRRHd = this.db.STCReturRRHds.Single(_temp => _temp.TransNmbr.ToLower() == _prmTransNmbr.ToLower());
                        //MKTReqReturDt _mktReqReturDt = this.db.MKTReqReturDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _reqReturNo.Trim().ToLower() && _temp.ProductCode.Trim().ToLower() == _row.ProductCode.Trim().ToLower());

                        FINSuppReturDt _finSuppReturDt = new FINSuppReturDt();
                        _finSuppReturDt.TransNmbr = _prmCode;
                        _finSuppReturDt.SJNo = _row.TransNmbr;
                        _finSuppReturDt.ProductCode = _row.ProductCode;
                        _finSuppReturDt.Qty = _row.Qty;
                        _finSuppReturDt.Unit = _row.Unit;
                        _finSuppReturDt.PriceForex = Convert.ToDecimal(_row.PriceCost);
                        _finSuppReturDt.AmountForex = _finSuppReturDt.Qty * Convert.ToDecimal(_row.PriceCost);
                        _finSuppReturDt.Remark = _row.Remark;
                        _finSuppReturDt.RequestNo = _reqReturNo;
                        _totalForex += _finSuppReturDt.AmountForex;

                        this.db.FINSuppReturDts.InsertOnSubmit(_finSuppReturDt);
                    }
                    _finCustReturHd.BaseForex = _totalForex + _finCustReturHd.BaseForex;
                    _finCustReturHd.PPNForex = Convert.ToDecimal((_finCustReturHd.BaseForex * _finCustReturHd.PPN / 100));
                    _finCustReturHd.TotalForex = _finCustReturHd.BaseForex + _finCustReturHd.PPNForex;

                    this.db.SubmitChanges();
                }
                else
                {
                    _result = "There Are Already Existing Data";
                }
            }
            catch (Exception ex)
            {
                _result = "You Failed Add Data";
            }

            return _result;
        }

        public List<FINSuppReturDt> GetlistFINSuppReturDt(int _prmReqPage, int _prmPageSize, String _prmTransNmbr)
        {
            List<FINSuppReturDt> _result = new List<FINSuppReturDt>();
            try
            {
                var _query = (from _finSuppReturDts in this.db.FINSuppReturDts
                              where _finSuppReturDts.TransNmbr == _prmTransNmbr
                              //&& (_stcReturRRDt.Qty - _stcReturRRDt.QtySJ) != 0
                              select _finSuppReturDts
                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
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

        public List<STCRejectOutDt> GetListSTCRejectOutDt(string _prmTransNmbr)
        {
            List<STCRejectOutDt> _result = new List<STCRejectOutDt>();

            try
            {
                var _query =
                            (
                                from _stcRejectOutDt in this.db.STCRejectOutDts
                                where _stcRejectOutDt.TransNmbr == _prmTransNmbr
                                select _stcRejectOutDt
                                //select new
                                //{
                                //    TransNmbr = _stcRejectInDt.TransNmbr,
                                //    ProductCode = _stcRejectInDt.ProductCode,
                                //    ProductName = (
                                //                    from _msProduct in this.db.MsProducts
                                //                    where _msProduct.ProductCode == _stcRejectInDt.ProductCode
                                //                    select _msProduct.ProductName
                                //                  ).FirstOrDefault(),
                                //    LocationCode = _stcRejectInDt.LocationCode,
                                //    LocationName = (
                                //                    from _msLocation in this.db.MsWrhsLocations
                                //                    where _msLocation.WLocationCode == _stcRejectInDt.LocationCode
                                //                    select _msLocation.WLocationName
                                //                  ).FirstOrDefault(),
                                //    Unit = _stcRejectInDt.Unit,
                                //    Qty = _stcRejectInDt.Qty
                                //}
                            );

                foreach (var _row in _query)
                {
                    //_result.Add(new STCRejectInDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.LocationCode, _row.LocationName, _row.Qty, _row.Unit));
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetReqReturNoSTCRejectOutHd(string _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                               from _stcRejectOutHd in this.db.STCRejectOutHds
                               where _stcRejectOutHd.TransNmbr == _prmCode
                               select new
                               {
                                   ReqReturNo = _stcRejectOutHd.PurchaseRetur
                               }
                           ).Single();

                _result = _query.ReqReturNo;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCReturRRDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            FINSuppReturHd _finCustReturHd = new FINSuppReturHd();

            decimal _total = 0;
            //Decimal _countAmount = 0;
            try
            {
                //for (int i = 0; i < _prmCode.Length; i++)
                //{
                //    string[] _tempSplit = _prmCode[i].Split('|');
                //    FINSuppReturDt _finSuppReturDt = this.db.FINSuppReturDts.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.SJNo == _tempSplit[1] && _temp.ProductCode == _tempSplit[2]);
                //    _countAmount = _countAmount + Convert.ToDecimal(_finSuppReturDt.AmountForex);
                //    this.db.FINSuppReturDts.DeleteOnSubmit(_finSuppReturDt);
                //}
                var _queryDt = (from _detail in this.db.FINSuppReturDts
                                where _detail.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower()
                              select _detail);

                this.db.FINSuppReturDts.DeleteAllOnSubmit(_queryDt);

                var _query = (
                                from _finSuppReturDt in this.db.FINSuppReturDts
                                where _finSuppReturDt.TransNmbr == _prmTransNo
                                group _finSuppReturDt by _finSuppReturDt.TransNmbr into _grp
                                select new
                                {
                                    AmountForex = _grp.Sum(a => a.AmountForex)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = Convert.ToDecimal(_obj.AmountForex);
                }

                _finCustReturHd = this.db.FINSuppReturHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _finCustReturHd.BaseForex = _finCustReturHd.BaseForex -_total;
                if (_finCustReturHd.BaseForex != 0)
                {
                    _finCustReturHd.PPNForex = (Convert.ToDecimal(_finCustReturHd.BaseForex) * _finCustReturHd.PPN / 100);
                    _finCustReturHd.TotalForex = _finCustReturHd.BaseForex + _finCustReturHd.PPNForex;
                }
                else
                {
                    _finCustReturHd.PPNForex = 0;
                    _finCustReturHd.TotalForex = 0;
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

        public FINSuppReturDt GetSingleFINSuppReturDt(String _prmTransNmbr, String _prmRRno, String _prmProductCode)
        {
            FINSuppReturDt _result = null;

            try
            {
                _result = this.db.FINSuppReturDts.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.SJNo == _prmRRno && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINSuppReturDt(FINSuppReturDt _prmFINSuppReturDt, Decimal _prmAmount)
        {
            bool _result = false;
            decimal _tempBaseAmount = 0;
            //decimal _tempTex = 0;
            decimal _tempPPNAmount = 0;
            decimal _tempTotalAmount = 0;

            try
            {
                FINSuppReturHd _finSuppInvConsignHd = this.GetSingleFINSuppReturHd(_prmFINSuppReturDt.TransNmbr);

                _tempBaseAmount = Convert.ToDecimal(_finSuppInvConsignHd.BaseForex - _prmFINSuppReturDt.AmountForex) + Convert.ToDecimal(_prmAmount);

                //_tempBaseAmount = _directSalesHd.BaseForex + Convert.ToDecimal(_prmAmount);
                _prmFINSuppReturDt.AmountForex = _prmAmount;
                //_tempDiscAmount = _directSalesHd.DiscAmount;
                //if (_tempBaseAmount != 0)
                //    _tempDisc = (_finSuppInvConsignHd.DiscForex / _tempBaseAmount) * 100;
                //_tempTex = (_finSuppInvConsignHd.PPNForex / (_finSuppInvConsignHd.BaseForex - _finSuppInvConsignHd.DiscForex)) * 100;

                //if (_tempDisc == 0)
                //{
                //    _tempDiscAmount = _finSuppInvConsignHd.DiscForex;
                //}
                //else
                //{
                //    _tempDiscAmount = Math.Round((_tempBaseAmount * _tempDisc) / 100, 2);
                //}

                if (_finSuppInvConsignHd.PPN == 0)
                {
                    _tempPPNAmount = _finSuppInvConsignHd.PPNForex;
                }
                else
                {
                    if (_tempBaseAmount != 0)
                        _tempPPNAmount = Convert.ToDecimal(Math.Round((_tempBaseAmount * _finSuppInvConsignHd.PPN) / 100, 2));
                    else
                        _tempPPNAmount = 0;
                }
                if (_tempBaseAmount != 0)
                    _tempTotalAmount = _tempBaseAmount + _tempPPNAmount;

                _finSuppInvConsignHd.BaseForex = _tempBaseAmount;
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

        #endregion

        ~SupplierReturBL()
        {
        }
    }
}
