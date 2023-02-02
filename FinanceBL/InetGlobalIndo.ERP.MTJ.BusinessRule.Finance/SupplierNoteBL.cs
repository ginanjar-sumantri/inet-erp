using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class SupplierNoteBL : Base
    {
        public SupplierNoteBL()
        {

        }

        #region FINSuppInvHd
        public double RowsCountFINSuppInvHd(string _prmCategory, string _prmKeyword)
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
                            from _finSuppInvHd in this.db.FINSuppInvHds
                            join _msSupp in this.db.MsSuppliers
                                on _finSuppInvHd.SuppCode equals _msSupp.SuppCode
                            where (SqlMethods.Like(_finSuppInvHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_finSuppInvHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _finSuppInvHd.Status != SupplierNoteDataMapper.GetStatus(TransStatus.Deleted)
                            select _finSuppInvHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINSuppInvHd> GetListFINSuppInvHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINSuppInvHd> _result = new List<FINSuppInvHd>();

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
                                from _finSuppInvHd in this.db.FINSuppInvHds
                                join _msSupp in this.db.MsSuppliers
                                    on _finSuppInvHd.SuppCode equals _msSupp.SuppCode
                                where (SqlMethods.Like(_finSuppInvHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finSuppInvHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finSuppInvHd.Status != SupplierNoteDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _finSuppInvHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _finSuppInvHd.TransNmbr,
                                    FileNmbr = _finSuppInvHd.FileNmbr,
                                    TransDate = _finSuppInvHd.TransDate,
                                    CurrCode = _finSuppInvHd.CurrCode,
                                    Status = _finSuppInvHd.Status,
                                    SuppCode = _finSuppInvHd.SuppCode,
                                    SuppName = _msSupp.SuppName,
                                    Term = _finSuppInvHd.Term,
                                    TermName = (
                                                    from _msTerm in this.db.MsTerms
                                                    where _msTerm.TermCode == _finSuppInvHd.Term
                                                    select _msTerm.TermName
                                                ).FirstOrDefault(),
                                    SuppInvoice = _finSuppInvHd.SuppInvoice
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINSuppInvHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CurrCode, _row.Status, _row.SuppCode, _row.SuppName, _row.Term, _row.TermName, _row.SuppInvoice));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINSuppInvHd GetSingleFINSuppInvHd(string _prmCode)
        {
            FINSuppInvHd _result = null;

            try
            {
                _result = this.db.FINSuppInvHds.Single(_temp => _temp.TransNmbr.ToLower() == _prmCode.ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFileNmbrV_STRRForSIByRRNo(string _prmCode)
        {
            string _result = null;

            try
            {
                _result = this.db.V_STRRForSIs.Single(_temp => _temp.RR_No.ToLower() == _prmCode.ToLower()).FileNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINSuppInvHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINSuppInvHd _finSuppInvHd = this.db.FINSuppInvHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finSuppInvHd != null)
                    {
                        if ((_finSuppInvHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINSuppInvDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINSuppInvDts.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINSuppInvDt2s
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINSuppInvDt2s.DeleteAllOnSubmit(_query2);

                            var _query3 = (from _detail in this.db.FINSuppInvRRLists
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINSuppInvRRLists.DeleteAllOnSubmit(_query3);

                            this.db.FINSuppInvHds.DeleteOnSubmit(_finSuppInvHd);

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

        public string AddFINSuppInvHd(FINSuppInvHd _prmFINSuppInvHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINSuppInvHd.TransDate.Year, _prmFINSuppInvHd.TransDate.Month, AppModule.GetValue(TransactionType.SupplierNote), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINSuppInvHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINSuppInvHds.InsertOnSubmit(_prmFINSuppInvHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINSuppInvHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINSuppInvHd(FINSuppInvHd _prmFINSuppInvHd)
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
                int _success = this.db.S_FNSIRRGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.SupplierNote);
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
                    int _success = this.db.S_FNSIRRApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINSuppInvHd _finSuppInvHd = this.GetSingleFINSuppInvHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_finSuppInvHd.TransDate.Year, _finSuppInvHd.TransDate.Month, AppModule.GetValue(TransactionType.SupplierNote), this._companyTag, ""))
                        {
                            _finSuppInvHd.FileNmbr = item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.APRate);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINSuppInvHd(_prmTransNmbr).FileNmbr;
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
                FINSuppInvHd _finSUPInvHd = this.db.FINSuppInvHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finSUPInvHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSIRRPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.SupplierNote);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = GetSingleFINSuppInvHd(_prmTransNmbr).FileNmbr;
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
                FINSuppInvHd _finSUPInvHd = this.db.FINSuppInvHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finSUPInvHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.S_FNSIRRUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.SupplierNote);
                        //_transActivity.TransNmbr = _prmTransNmbr;
                        //_transActivity.FileNmbr = GetSingleFINSuppInvHd(_prmTransNmbr).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINSuppInvHd(_prmTransNmbr).TransDate;
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

        public bool GetSingleFINSuppInvHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINSuppInvHd _finSuppInvHd = this.db.FINSuppInvHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finSuppInvHd != null)
                    {
                        if (_finSuppInvHd.Status != SupplierNoteDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINSuppInvHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINSuppInvHd _finSuppInvHd = this.db.FINSuppInvHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finSuppInvHd.Status == SupplierNoteDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finSuppInvHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finSuppInvHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finSuppInvHd != null)
                    {
                        if ((_finSuppInvHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINSuppInvDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINSuppInvDts.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.FINSuppInvDt2s
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINSuppInvDt2s.DeleteAllOnSubmit(_query2);

                            var _query3 = (from _detail in this.db.FINSuppInvRRLists
                                           where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail);

                            this.db.FINSuppInvRRLists.DeleteAllOnSubmit(_query3);

                            this.db.FINSuppInvHds.DeleteOnSubmit(_finSuppInvHd);

                            _result = true;
                        }
                        else if (_finSuppInvHd.FileNmbr != "" && _finSuppInvHd.Status == SupplierNoteDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finSuppInvHd.Status = SupplierNoteDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINSuppInvDt
        public int RowsCountFINSuppInvDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINSuppInvDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINSuppInvDt> GetListFINSuppInvDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<FINSuppInvDt> _result = new List<FINSuppInvDt>();

            try
            {
                var _query = (
                                from _finSuppInvDt in this.db.FINSuppInvDts
                                where _finSuppInvDt.TransNmbr == _prmCode
                                orderby _finSuppInvDt.RRNo ascending
                                select new
                                {
                                    TransNmbr = _finSuppInvDt.TransNmbr,
                                    RRNo = _finSuppInvDt.RRNo,
                                    FileNmbr = (
                                                    from _receivingPO in this.db.STCReceiveHds
                                                    where _receivingPO.TransNmbr == _finSuppInvDt.RRNo
                                                    select _receivingPO.FileNmbr
                                               ).FirstOrDefault(),
                                    ProductCode = _finSuppInvDt.ProductCode,
                                    PONo = _finSuppInvDt.PONo,
                                    FileNoPO = (
                                                    from _prcPOHd in this.db.PRCPOHds
                                                    where _prcPOHd.TransNmbr == _finSuppInvDt.PONo
                                                    select _prcPOHd.FileNmbr
                                                ).FirstOrDefault(),
                                    Qty = _finSuppInvDt.Qty,
                                    Unit = (
                                                from _msUnit in this.db.MsUnits
                                                where _msUnit.UnitCode == _finSuppInvDt.Unit
                                                select _msUnit.UnitName
                                           ).FirstOrDefault(),
                                    PriceForex = _finSuppInvDt.PriceForex,
                                    AmountForex = _finSuppInvDt.AmountForex,
                                    Remark = _finSuppInvDt.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINSuppInvDt(_row.TransNmbr, _row.RRNo, _row.FileNmbr, _row.ProductCode, _row.PONo, _row.FileNoPO, _row.Qty, _row.Unit, _row.PriceForex, _row.AmountForex, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINSuppInvDt GetSingleFINSuppInvDt(string _prmCode, string _prmRRNo, string _prmProductCode)
        {
            FINSuppInvDt _result = null;

            try
            {
                _result = this.db.FINSuppInvDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.RRNo == _prmRRNo && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINSuppInvDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            FINSuppInvHd _finSuppInvHd = new FINSuppInvHd();

            decimal _total = 0;
            decimal _total2 = 0;

            try
            {

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    FINSuppInvDt _finSuppInvDt = this.db.FINSuppInvDts.Single(_temp => _temp.RRNo.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.ProductCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    this.db.FINSuppInvDts.DeleteOnSubmit(_finSuppInvDt);
                }

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    var _query = (
                                    from _finSuppInvDt2 in this.db.FINSuppInvDts
                                    where !(
                                               from _finSuppInvDt3 in this.db.FINSuppInvDts
                                               where _finSuppInvDt3.RRNo == _tempSplit[0] && _finSuppInvDt3.ProductCode == _tempSplit[1] && _finSuppInvDt3.TransNmbr == _prmTransNo
                                               select new { _finSuppInvDt3.RRNo, _finSuppInvDt3.ProductCode }
                                       ).Contains(new { _finSuppInvDt2.RRNo, _finSuppInvDt2.ProductCode })
                                       && _finSuppInvDt2.TransNmbr == _prmTransNo
                                    group _finSuppInvDt2 by _finSuppInvDt2.TransNmbr into _grp
                                    select new
                                    {
                                        AmountForex = _grp.Sum(a => a.AmountForex)
                                    }
                                  );

                    foreach (var _obj in _query)
                    {
                        _total = (_obj.AmountForex == null) ? 0 : Convert.ToDecimal(_obj.AmountForex);
                    }
                }

                var _query2 = (
                                    from _finSuppInvDtA in this.db.FINSuppInvDt2s
                                    where _finSuppInvDtA.TransNmbr == _prmTransNo
                                    group _finSuppInvDtA by _finSuppInvDtA.TransNmbr into _grp
                                    select new
                                    {
                                        AmountForex2 = _grp.Sum(a => a.AmountForex)
                                    }
                                  );

                foreach (var _obj in _query2)
                {
                    _total2 = (_obj.AmountForex2 == null) ? 0 : Convert.ToDecimal(_obj.AmountForex2);
                }

                _finSuppInvHd = this.db.FINSuppInvHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _finSuppInvHd.BaseForex = _total + _total2;
                _finSuppInvHd.PPNForex = Convert.ToDecimal((_finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPN / 100);
                _finSuppInvHd.PPhForex = Convert.ToDecimal((_finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPh / 100);
                _finSuppInvHd.TotalForex = _finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex + _finSuppInvHd.PPNForex + _finSuppInvHd.PPhForex + _finSuppInvHd.OtherForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINSuppInvDt(FINSuppInvDt _prmFINSuppInvDt)
        {
            bool _result = false;

            FINSuppInvHd _finSuppInvHd = new FINSuppInvHd();

            decimal _total = 0;
            decimal _total2 = 0;

            try
            {
                var _query = (
                               from _finSuppInvDt in this.db.FINSuppInvDts
                               where !(
                                           from _finSuppInvDt2 in this.db.FINSuppInvDts
                                           where _finSuppInvDt2.RRNo == _prmFINSuppInvDt.RRNo && _finSuppInvDt.ProductCode == _prmFINSuppInvDt.ProductCode && _finSuppInvDt2.TransNmbr == _prmFINSuppInvDt.TransNmbr
                                           select new { _finSuppInvDt2.RRNo, _finSuppInvDt2.ProductCode }
                                       ).Contains(new { _finSuppInvDt.RRNo, _finSuppInvDt.ProductCode })
                                       && _finSuppInvDt.TransNmbr == _prmFINSuppInvDt.TransNmbr
                               group _finSuppInvDt by _finSuppInvDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = (_obj.AmountForex == null) ? 0 : Convert.ToDecimal(_obj.AmountForex);
                }

                var _query2 = (
                                    from _finSuppInvDtA in this.db.FINSuppInvDt2s
                                    where _finSuppInvDtA.TransNmbr == _prmFINSuppInvDt.TransNmbr
                                    group _finSuppInvDtA by _finSuppInvDtA.TransNmbr into _grp
                                    select new
                                    {
                                        AmountForex2 = _grp.Sum(a => a.AmountForex)
                                    }
                                  );

                foreach (var _obj in _query2)
                {
                    _total2 = (_obj.AmountForex2 == null) ? 0 : Convert.ToDecimal(_obj.AmountForex2);
                }

                _finSuppInvHd = this.db.FINSuppInvHds.Single(_fa => _fa.TransNmbr == _prmFINSuppInvDt.TransNmbr);

                _finSuppInvHd.BaseForex = _total + _total2 + ((_prmFINSuppInvDt.AmountForex == null) ? 0 : Convert.ToDecimal(_prmFINSuppInvDt.AmountForex));
                _finSuppInvHd.PPNForex = Convert.ToDecimal((_finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPN / 100);
                _finSuppInvHd.PPhForex = Convert.ToDecimal((_finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPh / 100);
                _finSuppInvHd.TotalForex = _finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex + _finSuppInvHd.PPNForex + _finSuppInvHd.PPhForex + _finSuppInvHd.OtherForex;

                this.db.FINSuppInvDts.InsertOnSubmit(_prmFINSuppInvDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINSuppInvDt(FINSuppInvDt _prmFINSuppInvDt)
        {
            bool _result = false;

            FINSuppInvHd _finSuppInvHd = new FINSuppInvHd();

            decimal _total = 0;
            decimal _total2 = 0;

            try
            {
                var _query = (
                               from _finSuppInvDt in this.db.FINSuppInvDts
                               where !(
                                           from _finSuppInvDt2 in this.db.FINSuppInvDts
                                           where _finSuppInvDt2.RRNo == _prmFINSuppInvDt.RRNo && _finSuppInvDt.ProductCode == _prmFINSuppInvDt.ProductCode && _finSuppInvDt2.TransNmbr == _prmFINSuppInvDt.TransNmbr
                                           select new { _finSuppInvDt2.RRNo, _finSuppInvDt2.ProductCode }
                                       ).Contains(new { _finSuppInvDt.RRNo, _finSuppInvDt.ProductCode })
                                       && _finSuppInvDt.TransNmbr == _prmFINSuppInvDt.TransNmbr
                               group _finSuppInvDt by _finSuppInvDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = (_obj.AmountForex == null) ? 0 : Convert.ToDecimal(_obj.AmountForex);
                }

                var _query2 = (
                                    from _finSuppInvDtA in this.db.FINSuppInvDt2s
                                    where _finSuppInvDtA.TransNmbr == _prmFINSuppInvDt.TransNmbr
                                    group _finSuppInvDtA by _finSuppInvDtA.TransNmbr into _grp
                                    select new
                                    {
                                        AmountForex2 = _grp.Sum(a => a.AmountForex)
                                    }
                                  );

                foreach (var _obj in _query2)
                {
                    _total2 = (_obj.AmountForex2 == null) ? 0 : Convert.ToDecimal(_obj.AmountForex2);
                }

                _finSuppInvHd = this.db.FINSuppInvHds.Single(_fa => _fa.TransNmbr == _prmFINSuppInvDt.TransNmbr);

                _finSuppInvHd.BaseForex = _total + _total2 + ((_prmFINSuppInvDt.AmountForex == null) ? 0 : Convert.ToDecimal(_prmFINSuppInvDt.AmountForex));
                _finSuppInvHd.PPNForex = Convert.ToDecimal((_finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPN / 100);
                _finSuppInvHd.PPhForex = Convert.ToDecimal((_finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPh / 100);
                _finSuppInvHd.TotalForex = _finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex + _finSuppInvHd.PPNForex + _finSuppInvHd.PPhForex + _finSuppInvHd.OtherForex;

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

        #region FINSuppInvDt2
        public int RowsCountFINSuppInvDt2(string _prmCode)
        {
            int _result = 0;

            _result = this.db.FINSuppInvDt2s.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public List<FINSuppInvDt2> GetListFINSuppInvDt2(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<FINSuppInvDt2> _result = new List<FINSuppInvDt2>();

            try
            {
                var _query = (
                                from _finSuppInvDt2 in this.db.FINSuppInvDt2s
                                where _finSuppInvDt2.TransNmbr == _prmCode
                                orderby _finSuppInvDt2.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _finSuppInvDt2.TransNmbr,
                                    ProductCode = _finSuppInvDt2.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode == _finSuppInvDt2.ProductCode
                                                    select _msProduct.ProductName
                                               ).FirstOrDefault(),
                                    WrhsCode = _finSuppInvDt2.WrhsCode,
                                    WrhsName = (
                                                    from _msWarehouse in this.db.MsWarehouses
                                                    where _msWarehouse.WrhsCode == _finSuppInvDt2.WrhsCode
                                                    select _msWarehouse.WrhsName
                                                ).FirstOrDefault(),
                                    WrhsSubLed = _finSuppInvDt2.WrhsSubLed,
                                    LocationCode = _finSuppInvDt2.LocationCode,
                                    LocationName = (
                                                    from _msWrhsLocation in this.db.MsWrhsLocations
                                                    where _msWrhsLocation.WLocationCode == _finSuppInvDt2.LocationCode
                                                    select _msWrhsLocation.WLocationName
                                                ).FirstOrDefault(),
                                    Qty = _finSuppInvDt2.Qty,
                                    Unit = (
                                                from _msUnit in this.db.MsUnits
                                                where _msUnit.UnitCode == _finSuppInvDt2.Unit
                                                select _msUnit.UnitName
                                           ).FirstOrDefault(),
                                    PriceForex = _finSuppInvDt2.PriceForex,
                                    AmountForex = _finSuppInvDt2.AmountForex,
                                    Remark = _finSuppInvDt2.Remark
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINSuppInvDt2(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.WrhsCode, _row.WrhsName, _row.WrhsSubLed, _row.LocationCode, _row.LocationName, _row.Qty, _row.Unit, _row.PriceForex, _row.AmountForex, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINSuppInvDt2 GetSingleFINSuppInvDt2(string _prmCode, string _prmWrhsCode, string _prmWrhsSubled, string _prmLocationCode, string _prmProductCode)
        {
            FINSuppInvDt2 _result = null;

            try
            {
                _result = this.db.FINSuppInvDt2s.Single(_temp => _temp.TransNmbr == _prmCode && _temp.WrhsCode == _prmWrhsCode && _temp.ProductCode == _prmProductCode && _temp.WrhsSubLed == _prmWrhsSubled && _temp.LocationCode == _prmLocationCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINSuppInvDt2(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            FINSuppInvHd _finSuppInvHd = new FINSuppInvHd();

            decimal _total = 0;
            decimal _total2 = 0;

            try
            {

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    FINSuppInvDt2 _finSuppInvDt2 = this.db.FINSuppInvDt2s.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.WrhsCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.WrhsSubLed.Trim().ToLower() == _tempSplit[2].Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _tempSplit[3].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    this.db.FINSuppInvDt2s.DeleteOnSubmit(_finSuppInvDt2);
                }

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    var _query = (
                                    from _finSuppInvDt3 in this.db.FINSuppInvDt2s
                                    where !(
                                               from _finSuppInvDt4 in this.db.FINSuppInvDt2s
                                               where _finSuppInvDt4.ProductCode == _tempSplit[0] && _finSuppInvDt4.WrhsCode == _tempSplit[1] && _finSuppInvDt4.WrhsSubLed == _tempSplit[2] && _finSuppInvDt4.LocationCode == _tempSplit[3] && _finSuppInvDt4.TransNmbr == _prmTransNo
                                               select new { _finSuppInvDt4.ProductCode, _finSuppInvDt4.WrhsCode, _finSuppInvDt4.WrhsSubLed, _finSuppInvDt4.LocationCode }
                                       ).Contains(new { _finSuppInvDt3.ProductCode, _finSuppInvDt3.WrhsCode, _finSuppInvDt3.WrhsSubLed, _finSuppInvDt3.LocationCode })
                                       && _finSuppInvDt3.TransNmbr == _prmTransNo
                                    group _finSuppInvDt3 by _finSuppInvDt3.TransNmbr into _grp
                                    select new
                                    {
                                        AmountForex = _grp.Sum(a => a.AmountForex)
                                    }
                                  );

                    foreach (var _obj in _query)
                    {
                        _total = (_obj.AmountForex == null) ? 0 : Convert.ToDecimal(_obj.AmountForex);
                    }
                }

                var _query2 = (
                                    from _finSuppInvDt in this.db.FINSuppInvDts
                                    where _finSuppInvDt.TransNmbr == _prmTransNo
                                    group _finSuppInvDt by _finSuppInvDt.TransNmbr into _grp
                                    select new
                                    {
                                        AmountForex2 = _grp.Sum(a => a.AmountForex)
                                    }
                                  );

                foreach (var _obj in _query2)
                {
                    _total2 = (_obj.AmountForex2 == null) ? 0 : Convert.ToDecimal(_obj.AmountForex2);
                }

                _finSuppInvHd = this.db.FINSuppInvHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _finSuppInvHd.BaseForex = _total + _total2;
                _finSuppInvHd.PPNForex = Convert.ToDecimal((_finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPN / 100);
                _finSuppInvHd.PPhForex = Convert.ToDecimal((_finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPh / 100);
                _finSuppInvHd.TotalForex = _finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex + _finSuppInvHd.PPNForex + _finSuppInvHd.PPhForex + _finSuppInvHd.OtherForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFINSuppInvDt2(FINSuppInvDt2 _prmFINSuppInvDt2)
        {
            bool _result = false;

            FINSuppInvHd _finSuppInvHd = new FINSuppInvHd();

            decimal _total = 0;
            decimal _total2 = 0;

            try
            {
                var _query = (
                               from _finSuppInvDt2 in this.db.FINSuppInvDt2s
                               where !(
                                           from _finSuppInvDt3 in this.db.FINSuppInvDt2s
                                           where _finSuppInvDt3.ProductCode == _prmFINSuppInvDt2.ProductCode && _finSuppInvDt3.WrhsCode == _prmFINSuppInvDt2.WrhsCode && _finSuppInvDt3.WrhsSubLed == _prmFINSuppInvDt2.WrhsSubLed && _finSuppInvDt3.LocationCode == _prmFINSuppInvDt2.LocationCode && _finSuppInvDt3.TransNmbr == _prmFINSuppInvDt2.TransNmbr
                                           select new { _finSuppInvDt3.ProductCode, _finSuppInvDt3.WrhsCode, _finSuppInvDt3.WrhsSubLed, _finSuppInvDt3.LocationCode }
                                       ).Contains(new { _finSuppInvDt2.ProductCode, _finSuppInvDt2.WrhsCode, _finSuppInvDt2.WrhsSubLed, _finSuppInvDt2.LocationCode })
                                       && _finSuppInvDt2.TransNmbr == _prmFINSuppInvDt2.TransNmbr
                               group _finSuppInvDt2 by _finSuppInvDt2.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(_temp => _temp.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = (_obj.AmountForex == null) ? 0 : Convert.ToDecimal(_obj.AmountForex);
                }

                var _query2 = (
                                    from _finSuppInvDt in this.db.FINSuppInvDts
                                    where _finSuppInvDt.TransNmbr == _prmFINSuppInvDt2.TransNmbr
                                    group _finSuppInvDt by _finSuppInvDt.TransNmbr into _grp
                                    select new
                                    {
                                        AmountForex2 = _grp.Sum(a => a.AmountForex)
                                    }
                                  );

                foreach (var _obj in _query2)
                {
                    _total2 = (_obj.AmountForex2 == null) ? 0 : Convert.ToDecimal(_obj.AmountForex2);
                }

                _finSuppInvHd = this.db.FINSuppInvHds.Single(_fa => _fa.TransNmbr == _prmFINSuppInvDt2.TransNmbr);

                _finSuppInvHd.BaseForex = _total + _total2 + ((_prmFINSuppInvDt2.AmountForex == null) ? 0 : Convert.ToDecimal(_prmFINSuppInvDt2.AmountForex));
                _finSuppInvHd.PPNForex = Convert.ToDecimal((_finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPN / 100);
                _finSuppInvHd.PPhForex = Convert.ToDecimal((_finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPh / 100);
                _finSuppInvHd.TotalForex = _finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex + _finSuppInvHd.PPNForex + _finSuppInvHd.PPhForex + _finSuppInvHd.OtherForex;

                this.db.FINSuppInvDt2s.InsertOnSubmit(_prmFINSuppInvDt2);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINSuppInvDt2(FINSuppInvDt2 _prmFINSuppInvDt2)
        {
            bool _result = false;

            //FINSuppInvHd _finSuppInvHd = new FINSuppInvHd();

            decimal _total = 0;
            decimal _total2 = 0;

            try
            {
                var _query = (
                               from _finSuppInvDt in this.db.FINSuppInvDt2s
                               where !(
                                           from _finSuppInvDt3 in this.db.FINSuppInvDt2s
                                           where _finSuppInvDt3.ProductCode == _prmFINSuppInvDt2.ProductCode && _finSuppInvDt.WrhsCode == _prmFINSuppInvDt2.WrhsCode && _finSuppInvDt.WrhsSubLed == _prmFINSuppInvDt2.WrhsSubLed && _finSuppInvDt.LocationCode == _prmFINSuppInvDt2.LocationCode && _finSuppInvDt3.TransNmbr == _prmFINSuppInvDt2.TransNmbr
                                           select new { _finSuppInvDt3.ProductCode, _finSuppInvDt3.WrhsCode, _finSuppInvDt3.WrhsSubLed, _finSuppInvDt3.LocationCode }
                                       ).Contains(new { _finSuppInvDt.ProductCode, _finSuppInvDt.WrhsCode, _finSuppInvDt.WrhsSubLed, _finSuppInvDt.LocationCode })
                                       && _finSuppInvDt.TransNmbr == _prmFINSuppInvDt2.TransNmbr
                               group _finSuppInvDt by _finSuppInvDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                foreach (var _obj in _query)
                {
                    _total = (_obj.AmountForex == null) ? 0 : Convert.ToDecimal(_obj.AmountForex);
                }

                var _query2 = (
                                    from _finSuppInvDt in this.db.FINSuppInvDts
                                    where _finSuppInvDt.TransNmbr == _prmFINSuppInvDt2.TransNmbr
                                    group _finSuppInvDt by _finSuppInvDt.TransNmbr into _grp
                                    select new
                                    {
                                        AmountForex2 = _grp.Sum(a => a.AmountForex)
                                    }
                                  );

                foreach (var _obj in _query2)
                {
                    _total2 = (_obj.AmountForex2 == null) ? 0 : Convert.ToDecimal(_obj.AmountForex2);
                }

                FINSuppInvHd _finSuppInvHd = this.db.FINSuppInvHds.Single(_fa => _fa.TransNmbr == _prmFINSuppInvDt2.TransNmbr);

                decimal _tempBaseForex = _total + _total2 + ((_prmFINSuppInvDt2.AmountForex == null) ? 0 : Convert.ToDecimal(_prmFINSuppInvDt2.AmountForex));
                _finSuppInvHd.BaseForex = _tempBaseForex;

                decimal _tempPPNForex = Convert.ToDecimal((_tempBaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPN / 100);
                _finSuppInvHd.PPNForex = _tempPPNForex;

                decimal _tempPPhForex = Convert.ToDecimal((_tempBaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPh / 100);
                _finSuppInvHd.PPhForex = _tempPPhForex;

                _finSuppInvHd.TotalForex = _tempBaseForex - _finSuppInvHd.DiscForex + _tempPPNForex + _tempPPhForex + _finSuppInvHd.OtherForex;

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

        #region FINSuppInvRRList
        public List<FINSuppInvRRList> GetListFINSuppInvRRList(String _prmTransNo)
        {
            List<FINSuppInvRRList> _result = new List<FINSuppInvRRList>();

            try
            {
                var _query = (
                                from _finSuppInvRRList in this.db.FINSuppInvRRLists
                                join _stcReceiveHd in this.db.STCReceiveHds
                                    on _finSuppInvRRList.RRNo equals _stcReceiveHd.TransNmbr
                                where _finSuppInvRRList.TransNmbr == _prmTransNo
                                orderby _stcReceiveHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _finSuppInvRRList.TransNmbr,
                                    RRNo = _finSuppInvRRList.RRNo,
                                    FileNmbr = _stcReceiveHd.FileNmbr,
                                    Supplier = (
                                                   from _msSupp in this.db.MsSuppliers
                                                   where _msSupp.SuppCode == _stcReceiveHd.SuppCode
                                                   select _msSupp.SuppName
                                               ).FirstOrDefault(),
                                    PONo = (
                                                   from _po in this.db.PRCPOHds
                                                   where _po.TransNmbr == _stcReceiveHd.PONo
                                                   select _po.FileNmbr
                                               ).FirstOrDefault(),
                                    Remark = _finSuppInvRRList.Remark
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new FINSuppInvRRList(_row.TransNmbr, _row.RRNo, _row.FileNmbr, _row.Supplier, _row.PONo, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String AddFINSuppInvRRList(String _prmSuppNoteTransNmbr, FINSuppInvRRList _prmFINSuppInvRRList)
        {
            String _result = "";

            try
            {
                String _currSuppNote = "";
                String _currPO = "";

                _currSuppNote = this.GetSingleFINSuppInvHd(_prmSuppNoteTransNmbr).CurrCode;
                _currPO = (
                             from _rr in this.db.STCReceiveHds
                             join _po in this.db.PRCPOHds
                                 on _rr.PONo equals _po.TransNmbr
                             where _rr.TransNmbr == _prmFINSuppInvRRList.RRNo
                             select _po.CurrCode
                          ).FirstOrDefault();

                if (_currSuppNote == _currPO)
                {
                    this.db.FINSuppInvRRLists.InsertOnSubmit(_prmFINSuppInvRRList);

                    var _query = (
                                    from _finSuppInvDt in this.db.FINSuppInvDts
                                    where _finSuppInvDt.TransNmbr == _prmFINSuppInvRRList.TransNmbr
                                    select _finSuppInvDt
                                 );
                    this.db.FINSuppInvDts.DeleteAllOnSubmit(_query);

                    FINSuppInvHd _finSuppInvHd = this.db.FINSuppInvHds.Single(_temp => _temp.TransNmbr == _prmFINSuppInvRRList.TransNmbr);
                    _finSuppInvHd.BaseForex = 0;
                    _finSuppInvHd.DiscForex = 0;
                    _finSuppInvHd.PPNForex = 0;
                    _finSuppInvHd.PPhForex = 0;
                    _finSuppInvHd.TotalForex = 0;

                    this.db.SubmitChanges();

                    _result = "";
                }
                else
                {
                    _result = "RR Currency is not valid for this Supplier Note";
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //private Boolean CekCurrencyRRForSuppNote(String _prmSuppNoteTransNmbr, String _prmRRNo)
        //{
        //    Boolean _result = false;

        //    try
        //    {
        //        var _query = (
        //                        from _rr in this.db.STCReceiveHds
        //                        join _po in this.db.PRCPOHds
        //                            on _rr.PONo equals _po.TransNmbr                                    
        //                        where _rr.TransNmbr == _prmRRNo
        //                        select _po.CurrCode
        //                     );
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool DeleteMultiFINSuppInvRRList(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    var _queryDt2 = (
                                        from _finSuppInvRRList in this.db.FINSuppInvRRLists
                                        where _finSuppInvRRList.TransNmbr == _tempSplit[0]
                                            && _finSuppInvRRList.RRNo == _tempSplit[1]
                                        select _finSuppInvRRList
                                    );

                    this.db.FINSuppInvRRLists.DeleteAllOnSubmit(_queryDt2);

                    var _queryDt = (
                                        from _finSuppInvDt in this.db.FINSuppInvDts
                                        where _finSuppInvDt.TransNmbr == _tempSplit[0]
                                        select _finSuppInvDt
                                    );

                    this.db.FINSuppInvDts.DeleteAllOnSubmit(_queryDt);

                    FINSuppInvHd _finSuppInvHd = this.db.FINSuppInvHds.Single(_temp => _temp.TransNmbr == _tempSplit[0]);
                    _finSuppInvHd.BaseForex = 0;
                    _finSuppInvHd.DiscForex = 0;
                    _finSuppInvHd.PPNForex = 0;
                    _finSuppInvHd.PPhForex = 0;
                    _finSuppInvHd.TotalForex = 0;
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

        public bool GenerateFINSuppInvDt(string _prmTransNmbr)
        {
            bool _result = false;

            decimal _total = 0;
            decimal _total2 = 0;

            try
            {
                var _delSuppInvDt = (
                                        from _finSuppInvDt in this.db.FINSuppInvDts
                                        where _finSuppInvDt.TransNmbr == _prmTransNmbr
                                        select _finSuppInvDt
                                   );

                this.db.FINSuppInvDts.DeleteAllOnSubmit(_delSuppInvDt);

                var _queryRRList = (
                                from _finSuppInvRRList in this.db.FINSuppInvRRLists
                                join _stcReceiveHd in this.db.STCReceiveHds
                                    on _finSuppInvRRList.RRNo equals _stcReceiveHd.TransNmbr
                                join _stcReceiveDt in this.db.STCReceiveDts
                                    on _stcReceiveHd.TransNmbr equals _stcReceiveDt.TransNmbr
                                where _finSuppInvRRList.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    TransNmbr = _finSuppInvRRList.TransNmbr,
                                    RRNo = _finSuppInvRRList.RRNo,
                                    RRNoFileNmbr = _stcReceiveHd.FileNmbr,
                                    Remark = _finSuppInvRRList.Remark,
                                    PONo = _stcReceiveHd.PONo,
                                    ProductCode = _stcReceiveDt.ProductCode,
                                    Qty = _stcReceiveDt.Qty,
                                    Unit = _stcReceiveDt.Unit,
                                    PriceForex = ((_stcReceiveDt.Qty == 0) ? 0 : (_stcReceiveDt.TotalForex / _stcReceiveDt.Qty)),
                                    AmountForex = _stcReceiveDt.TotalForex,
                                    RemarkDt = _stcReceiveDt.Remark
                                }
                             );

                FINSuppInvHd _finSuppInvHd = new FINSuppInvHd();

                foreach (var _item in _queryRRList)
                {
                    FINSuppInvDt _finSuppInvDtTab = new FINSuppInvDt();

                    _finSuppInvDtTab.TransNmbr = _prmTransNmbr;
                    _finSuppInvDtTab.RRNo = _item.RRNo;
                    _finSuppInvDtTab.PONo = _item.PONo;
                    _finSuppInvDtTab.ProductCode = _item.ProductCode;
                    _finSuppInvDtTab.Qty = _item.Qty;
                    _finSuppInvDtTab.Unit = _item.Unit;
                    _finSuppInvDtTab.PriceForex = _item.PriceForex;
                    _finSuppInvDtTab.AmountForex = _item.AmountForex;
                    _finSuppInvDtTab.Remark = _item.RemarkDt;

                    var _query = (
                               from _finSuppInvDt in this.db.FINSuppInvDts
                               where !(
                                           from _finSuppInvDt2 in this.db.FINSuppInvDts
                                           where _finSuppInvDt2.RRNo == _finSuppInvDtTab.RRNo && _finSuppInvDt.ProductCode == _finSuppInvDtTab.ProductCode && _finSuppInvDt2.TransNmbr == _finSuppInvDtTab.TransNmbr
                                           select new { _finSuppInvDt2.RRNo, _finSuppInvDt2.ProductCode }
                                       ).Contains(new { _finSuppInvDt.RRNo, _finSuppInvDt.ProductCode })
                                       && _finSuppInvDt.TransNmbr == _finSuppInvDtTab.TransNmbr
                               group _finSuppInvDt by _finSuppInvDt.TransNmbr into _grp
                               select new
                               {
                                   AmountForex = _grp.Sum(a => a.AmountForex)
                               }
                             );

                    foreach (var _obj in _query)
                    {
                        _total = (_obj.AmountForex == null) ? 0 : Convert.ToDecimal(_obj.AmountForex);
                    }

                    var _query2 = (
                                        from _finSuppInvDtA in this.db.FINSuppInvDt2s
                                        where _finSuppInvDtA.TransNmbr == _finSuppInvDtTab.TransNmbr
                                        group _finSuppInvDtA by _finSuppInvDtA.TransNmbr into _grp
                                        select new
                                        {
                                            AmountForex2 = _grp.Sum(a => a.AmountForex)
                                        }
                                      );

                    foreach (var _obj in _query2)
                    {
                        _total2 = (_obj.AmountForex2 == null) ? 0 : Convert.ToDecimal(_obj.AmountForex2);
                    }

                    _finSuppInvHd = this.db.FINSuppInvHds.Single(_fa => _fa.TransNmbr == _finSuppInvDtTab.TransNmbr);

                    _finSuppInvHd.BaseForex = _total + _total2 + ((_finSuppInvDtTab.AmountForex == null) ? 0 : Convert.ToDecimal(_finSuppInvDtTab.AmountForex));
                    _finSuppInvHd.PPNForex = Convert.ToDecimal((_finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPN / 100);
                    _finSuppInvHd.PPhForex = Convert.ToDecimal((_finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex) * _finSuppInvHd.PPh / 100);
                    _finSuppInvHd.TotalForex = _finSuppInvHd.BaseForex - _finSuppInvHd.DiscForex + _finSuppInvHd.PPNForex + _finSuppInvHd.PPhForex + _finSuppInvHd.OtherForex;

                    this.db.FINSuppInvDts.InsertOnSubmit(_finSuppInvDtTab);

                    this.db.SubmitChanges();
                }

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~SupplierNoteBL()
        {
        }
    }
}
