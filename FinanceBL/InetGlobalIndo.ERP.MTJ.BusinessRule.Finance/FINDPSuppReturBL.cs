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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class FINDPSuppReturBL : Base
    {
        public FINDPSuppReturBL()
        {
        }

        #region FINDPSuppReturHd
        public double RowsCountFINDPSuppReturHd(string _prmCategory, string _prmKeyword)
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
                            from _finDPSuppReturHd in this.db.FINDPSuppReturHds
                            join _msSupp in this.db.MsSuppliers
                                on _finDPSuppReturHd.SuppCode equals _msSupp.SuppCode
                            where (SqlMethods.Like(_finDPSuppReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSupp.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_finDPSuppReturHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _finDPSuppReturHd.Status != DPSupplierReturDataMapper.GetStatus(TransStatus.Deleted)
                            select _finDPSuppReturHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINDPSuppReturHd> GetListFINDPSuppReturHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINDPSuppReturHd> _result = new List<FINDPSuppReturHd>();

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
                                from _finDPSuppReturHd in this.db.FINDPSuppReturHds
                                join _msSupplier in this.db.MsSuppliers
                                    on _finDPSuppReturHd.SuppCode equals _msSupplier.SuppCode
                                where (SqlMethods.Like(_finDPSuppReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_finDPSuppReturHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _finDPSuppReturHd.Status != DPSupplierReturDataMapper.GetStatus(TransStatus.Deleted)
                                select new
                                {
                                    TransNmbr = _finDPSuppReturHd.TransNmbr,
                                    FileNmbr = _finDPSuppReturHd.FileNmbr,
                                    TransDate = _finDPSuppReturHd.TransDate,
                                    SuppName = _msSupplier.SuppName,
                                    Remark = _finDPSuppReturHd.Remark,
                                    Status = _finDPSuppReturHd.Status
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPSuppReturHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.SuppName, _row.Remark, _row.Status));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDPSuppReturHd GetSingleFINDPSuppReturHd(string _prmCode)
        {
            FINDPSuppReturHd _result = null;

            try
            {
                _result = this.db.FINDPSuppReturHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINDPSuppReturHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPSuppReturHd _finDPSuppReturHd = this.db.FINDPSuppReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPSuppReturHd != null)
                    {
                        if ((_finDPSuppReturHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINDPSuppReturDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINDPSuppReturDts.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail2 in this.db.FINDPSuppReturPays
                                           where _detail2.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail2);

                            this.db.FINDPSuppReturPays.DeleteAllOnSubmit(_query2);

                            this.db.FINDPSuppReturHds.DeleteOnSubmit(_finDPSuppReturHd);

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

        public string AddFINDPSuppReturHd(FINDPSuppReturHd _prmFINDPSuppReturHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINDPSuppReturHd.TransDate.Year, _prmFINDPSuppReturHd.TransDate.Month, AppModule.GetValue(TransactionType.FINDPSuppRetur), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmFINDPSuppReturHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.FINDPSuppReturHds.InsertOnSubmit(_prmFINDPSuppReturHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmFINDPSuppReturHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDPSuppReturHd(FINDPSuppReturHd _prmFINDPSuppReturHd)
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

        public string GetSuppCodeHeader(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _finDPSuppReturHd in this.db.FINDPSuppReturHds
                                where _finDPSuppReturHd.TransNmbr == _prmCode
                                select new
                                {
                                    SuppCode = _finDPSuppReturHd.SuppCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.SuppCode;
                }
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
                this.db.S_FNDPSuppReturGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.FINDPSuppRetur);
                    _transActivity.TransNmbr = _prmCode;
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

        public string Approve(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_FNDPSuppReturApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        FINDPSuppReturHd _finDPSuppReturHd = this.GetSingleFINDPSuppReturHd(_prmCode);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_finDPSuppReturHd.TransDate.Year, _finDPSuppReturHd.TransDate.Month, AppModule.GetValue(TransactionType.FINDPSuppRetur), this._companyTag, ""))
                        {
                            _finDPSuppReturHd.FileNmbr = _item.Number;
                        }
                        
                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FINDPSuppRetur);
                        _transActivity.TransNmbr = _prmCode;
                        _transActivity.FileNmbr = GetSingleFINDPSuppReturHd(_prmCode).FileNmbr;
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

        public string Posting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINDPSuppReturHd _finDPSuppReturHd = this.db.FINDPSuppReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDPSuppReturHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNDPSuppReturPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FINDPSuppRetur);
                        _transActivity.TransNmbr = _prmCode;
                        _transActivity.FileNmbr = GetSingleFINDPSuppReturHd(_prmCode).FileNmbr;
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

        public string UnPosting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                FINDPSuppReturHd _finDPSuppReturHd = this.db.FINDPSuppReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_finDPSuppReturHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    this.db.S_FNDPSuppReturUnPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.FINDPSuppRetur);
                        //_transActivity.TransNmbr = _prmCode;
                        //_transActivity.FileNmbr = GetSingleFINDPSuppReturHd(_prmCode).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = GetSingleFINDPSuppReturHd(_prmCode).TransDate;
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
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public char GetStatusFINDPSuppReturHd(string _prmCode)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _finDPSuppReturHd in this.db.FINDPSuppReturHds
                                where _finDPSuppReturHd.TransNmbr == _prmCode
                                select new
                                {
                                    Status = _finDPSuppReturHd.Status
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.Status;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleFINDPSuppReturHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPSuppReturHd _finDPSuppReturHd = this.db.FINDPSuppReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPSuppReturHd != null)
                    {
                        if (_finDPSuppReturHd.Status != DPSupplierReturDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApproveFINDPSuppReturHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINDPSuppReturHd _finDPSuppReturHd = this.db.FINDPSuppReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_finDPSuppReturHd.Status == DPSupplierReturDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _finDPSuppReturHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _finDPSuppReturHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_finDPSuppReturHd != null)
                    {
                        if ((_finDPSuppReturHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.FINDPSuppReturDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.FINDPSuppReturDts.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail2 in this.db.FINDPSuppReturPays
                                           where _detail2.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                           select _detail2);

                            this.db.FINDPSuppReturPays.DeleteAllOnSubmit(_query2);

                            this.db.FINDPSuppReturHds.DeleteOnSubmit(_finDPSuppReturHd);

                            _result = true;
                        }
                        else if (_finDPSuppReturHd.FileNmbr != "" && _finDPSuppReturHd.Status == DPSupplierReturDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _finDPSuppReturHd.Status = DPSupplierReturDataMapper.GetStatus(TransStatus.Deleted);
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

        #region FINDPSuppReturDt
        public List<FINDPSuppReturDt> GetListFINDPSuppReturDt(string _prmTransNumber)
        {
            List<FINDPSuppReturDt> _result = new List<FINDPSuppReturDt>();

            try
            {
                var _query = (
                                from _finDPSuppReturDt in this.db.FINDPSuppReturDts
                                where _finDPSuppReturDt.TransNmbr == _prmTransNumber
                                orderby _finDPSuppReturDt.DPNo ascending
                                select new
                                {
                                    TransNmbr = _finDPSuppReturDt.TransNmbr,
                                    DPNo = _finDPSuppReturDt.DPNo,
                                    CurrCode = _finDPSuppReturDt.CurrCode,
                                    ForexRate = _finDPSuppReturDt.ForexRate,
                                    BaseForex = _finDPSuppReturDt.BaseForex,
                                    PPN = _finDPSuppReturDt.PPN,
                                    PPNForex = _finDPSuppReturDt.PPNForex,
                                    TotalForex = _finDPSuppReturDt.TotalForex,
                                    Remark = _finDPSuppReturDt.Remark
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPSuppReturDt(_row.TransNmbr, _row.DPNo, _row.CurrCode, _row.ForexRate, _row.BaseForex, _row.PPN, _row.PPNForex, _row.TotalForex, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDPSuppReturDt GetSingleFINDPSuppReturDt(string _prmCode, string _prmDPNo)
        {
            FINDPSuppReturDt _result = null;

            try
            {
                _result = this.db.FINDPSuppReturDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.DPNo.Trim().ToLower() == _prmDPNo.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINDPSuppReturDt(string[] _prmItemCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmItemCode.Length; i++)
                {
                    FINDPSuppReturDt _finDPSuppReturDt = this.db.FINDPSuppReturDts.Single(_temp => _temp.DPNo.Trim().ToLower() == _prmItemCode[i].Trim().ToLower() && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    this.db.FINDPSuppReturDts.DeleteOnSubmit(_finDPSuppReturDt);
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

        public bool AddFINDPSuppReturDt(FINDPSuppReturDt _prmFINDPSuppReturDt)
        {
            bool _result = false;

            try
            {
                this.db.FINDPSuppReturDts.InsertOnSubmit(_prmFINDPSuppReturDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDPSuppReturDt(FINDPSuppReturDt _prmFINDPSuppReturDt)
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

        #region FINDPSuppReturPay

        public List<FINDPSuppReturPay> GetListFINDPSuppReturPay(string _prmTransNumber)
        {
            List<FINDPSuppReturPay> _result = new List<FINDPSuppReturPay>();

            try
            {
                var _query = (
                                from _finDPSuppReturPay in this.db.FINDPSuppReturPays
                                where _finDPSuppReturPay.TransNmbr == _prmTransNumber
                                orderby _finDPSuppReturPay.ItemNo descending
                                select new
                                {
                                    TransNmbr = _finDPSuppReturPay.TransNmbr,
                                    ItemNo = _finDPSuppReturPay.ItemNo,
                                    ReceiptType = _finDPSuppReturPay.ReceiptType,
                                    DocumentNo = _finDPSuppReturPay.DocumentNo,
                                    AmountForex = _finDPSuppReturPay.AmountForex,
                                    CurrCode = _finDPSuppReturPay.CurrCode,
                                    ForexRate = _finDPSuppReturPay.ForexRate,
                                    Remark = _finDPSuppReturPay.Remark,
                                    BankGiro = _finDPSuppReturPay.BankGiro,
                                    DueDate = _finDPSuppReturPay.DueDate,
                                    BankExpense = _finDPSuppReturPay.BankExpense
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINDPSuppReturPay(_row.TransNmbr, _row.ItemNo, _row.ReceiptType, _row.DocumentNo, _row.AmountForex, _row.CurrCode, _row.ForexRate, _row.Remark, _row.BankGiro, _row.DueDate, _row.BankExpense));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxNoItemFINDPSuppReturPay(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.FINDPSuppReturPays.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINDPSuppReturPay GetSingleFINDPSuppReturPay(string _prmCode, string _prmItemNo)
        {
            FINDPSuppReturPay _result = null;

            try
            {
                _result = this.db.FINDPSuppReturPays.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.ItemNo == Convert.ToInt32(_prmItemNo));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINDPSuppReturPay(string[] _prmItemCode, string _prmTransNo)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmItemCode.Length; i++)
                {
                    FINDPSuppReturPay _finDPSuppReturPay = this.db.FINDPSuppReturPays.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmItemCode[i]) && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    this.db.FINDPSuppReturPays.DeleteOnSubmit(_finDPSuppReturPay);
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

        public bool AddFINDPSuppReturPay(FINDPSuppReturPay _prmFINDPSuppReturPay)
        {
            bool _result = false;

            try
            {
                this.db.FINDPSuppReturPays.InsertOnSubmit(_prmFINDPSuppReturPay);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINDPSuppReturPay(FINDPSuppReturPay _prmFINDPSuppReturPay)
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

        ~FINDPSuppReturBL()
        {

        }
    }
}
