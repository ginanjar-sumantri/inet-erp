using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Data.Linq.SqlClient;
using System.Diagnostics;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using BusinessRule.POS;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Tour
{
    public sealed class HotelBL : Base
    {
        public HotelBL()
        {
        }

        #region POSTrHotelHd

        public double RowsCountPOSTrHotelHd(string _prmCategory, string _prmKeyword)
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
            else if (_prmCategory == "CustName")
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
                var _query =
                            (
                                from _posTrHotelHd in this.db.POSTrHotelHds
                                where (SqlMethods.Like(_posTrHotelHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posTrHotelHd.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_posTrHotelHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _posTrHotelHd.Status != POSHotelDataMapper.GetStatus(TransStatus.Deleted)
                                select _posTrHotelHd.TransNmbr
                            ).Count();

                _result = _query;

            }
            catch (Exception)
            {

                throw;
            }

            return _result;
        }

        public List<POSTrHotelHd> GetListPOSTrHotelHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSTrHotelHd> _result = new List<POSTrHotelHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "CustName")
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
                                from _posTrHotelHd in this.db.POSTrHotelHds
                                where (SqlMethods.Like(_posTrHotelHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posTrHotelHd.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_posTrHotelHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _posTrHotelHd.Status != POSHotelDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _posTrHotelHd.DatePrep descending
                                select _posTrHotelHd
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

        public POSTrHotelHd GetSinglePOSTrHotelHd(string _prmCode)
        {
            POSTrHotelHd _result = null;

            try
            {
                _result = this.db.POSTrHotelHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPOSTrHotelHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    POSTrHotelHd _posTrHotelHd = this.db.POSTrHotelHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower());

                    if (_posTrHotelHd != null)
                    {
                        if ((_posTrHotelHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.POSTrHotelDts
                                          where _detail.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower()
                                          select _detail);

                            this.db.POSTrHotelDts.DeleteAllOnSubmit(_query);

                            this.db.POSTrHotelHds.DeleteOnSubmit(_posTrHotelHd);

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

        public bool GetSinglePOSTrHotelHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    POSTrHotelHd _posTrHotelHd = this.db.POSTrHotelHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_posTrHotelHd != null)
                    {
                        if (_posTrHotelHd.Status != POSHotelDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApprovePOSTrHotelHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    POSTrHotelHd _posTrHotelHd = this.db.POSTrHotelHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_posTrHotelHd.Status == POSHotelDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _posTrHotelHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _posTrHotelHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_posTrHotelHd != null)
                    {
                        if ((_posTrHotelHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.POSTrHotelDts
                                          where _detail.TransNmbr == _prmCode[i]
                                          select _detail);

                            this.db.POSTrHotelDts.DeleteAllOnSubmit(_query);

                            this.db.POSTrHotelHds.DeleteOnSubmit(_posTrHotelHd);

                            _result = true;
                        }
                        else if (_posTrHotelHd.FileNmbr != "" && _posTrHotelHd.Status == POSHotelDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _posTrHotelHd.Status = POSHotelDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddPOSTrHotelHd(POSTrHotelHd _prmPOSTrHotelHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINSuppInvHd.TransDate.Year, _prmFINSuppInvHd.TransDate.Month, AppModule.GetValue(TransactionType.SupplierNote), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmPOSTrHotelHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.POSTrHotelHds.InsertOnSubmit(_prmPOSTrHotelHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmPOSTrHotelHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddPOSTrHotelHdForDO(POSTrHotelHd _prmPOSTrHotelHd, POSTrDeliveryOrderRef _prmPOSTrDeliveryOrderRef)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINSuppInvHd.TransDate.Year, _prmFINSuppInvHd.TransDate.Month, AppModule.GetValue(TransactionType.SupplierNote), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmPOSTrHotelHd.TransNmbr = _item.Number;
                    _prmPOSTrDeliveryOrderRef.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                this.db.POSTrHotelHds.InsertOnSubmit(_prmPOSTrHotelHd);
                this.db.POSTrDeliveryOrderRefs.InsertOnSubmit(_prmPOSTrDeliveryOrderRef);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmPOSTrDeliveryOrderRef.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPOSTrHotelHd(POSTrHotelHd _prmPOSTrHotelHd)
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
                int _success = this.db.spPOS_HotelGetAppr(_prmTransNmbr, _prmuser, ref _result);

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.Hotel);
                    _transActivity.TransNmbr = _prmTransNmbr;
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);

                    _result = "Get Approval Success";
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
                    int _success = this.db.spPOS_HotelApprove(_prmTransNmbr, _prmuser, ref _result);

                    if (_result == "")
                    {
                        POSTrHotelHd _posTrHotelHd = this.GetSinglePOSTrHotelHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_posTrHotelHd.TransDate).Year, Convert.ToDateTime(_posTrHotelHd.TransDate).Month, AppModule.GetValue(TransactionType.Hotel), this._companyTag, ""))
                        {
                            _posTrHotelHd.FileNmbr = item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.APRate);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = _posTrHotelHd.FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

                        _result = "Approve Success";
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
                int _success = this.db.spPOS_HotelPosting(_prmTransNmbr, _prmuser, ref _result);
                POSTrHotelHd _posTrHotelHd = this.db.POSTrHotelHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());

                if (_result == "")
                {
                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.SupplierNote);
                    _transActivity.TransNmbr = _prmTransNmbr;
                    _transActivity.FileNmbr = _posTrHotelHd.FileNmbr;
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);

                    _result = "Posting Success";
                    this.db.SubmitChanges();
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
            TransactionCloseBL _transCloseBL = new TransactionCloseBL();
            string _result = "";

            try
            {
                POSTrHotelHd _posTrHotelHd = this.db.POSTrHotelHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_posTrHotelHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.spPOS_HotelUnPosting(_prmTransNmbr, _prmuser, ref _result);

                    if (_result == "")
                    {
                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.Hotel);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = _posTrHotelHd.FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        _transActivity.ActivitiesDate = _posTrHotelHd.TransDate;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);

                        _result = "Unposting Success";
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
                _result = "Unposting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public List<POSTrHotelHd> GetListHotelHdForDeliveryOrder(String _prmReferenceNo)
        {
            List<POSTrHotelHd> _result = new List<POSTrHotelHd>();

            try
            {
                var _query = (
                                from _hotelHd in this.db.POSTrHotelHds
                                where _hotelHd.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                                orderby _hotelHd.TransDate descending
                                select _hotelHd
                                );
                if (_query.Count() > 0)
                    _result.Add(_query.FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrHotelHd> GetListHotelHdSendToCashier(String _prmCustID, String _prmSearchBy, String _prmSearchText)
        {
            String _pattern1 = "%";
            String _pattern2 = "%";

            if (_prmCustID.Trim() != "")
            {
                _pattern1 = "%" + _prmCustID + "%";
            }

            if (_prmSearchBy.Trim() == "JobOrder")
            {
                _pattern2 = "%" + _prmSearchText + "%";
            }

            List<POSTrHotelHd> _result = new List<POSTrHotelHd>();
            try
            {
                var _query = (
                                from _hotelHd in this.db.POSTrHotelHds
                                where SqlMethods.Like((_hotelHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_hotelHd.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && _hotelHd.Status == POSHotelDataMapper.GetStatus(TransStatus.Approved)
                                    && _hotelHd.DoneSettlement == POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet)
                                    && _hotelHd.IsVOID == false
                                    && _hotelHd.DeliveryOrderReff == ""
                                select _hotelHd
                            );
                foreach (var _row in _query)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean SetVOID(String _prmTransNmbr, String _prmReasonCode, Boolean _prmVOIDValue)
        {
            Boolean _result = false;
            POSReasonBL _reasonBL = new POSReasonBL();

            try
            {
                POSTrHotelHd _internetHd = this.db.POSTrHotelHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _internetHd.Remark = _reasonBL.GetReasonByCode(Convert.ToInt32(_prmReasonCode));
                _internetHd.IsVOID = _prmVOIDValue;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public List<POSTrHotelHd> GetListHotelPayNotDelivered(String _prmCustID, String _prmSearchBy, String _prmSearchText)
        {
            String _pattern1 = "%";
            String _pattern2 = "%";
            String _pattern3 = "%";

            if (_prmCustID.Trim() != "")
            {
                _pattern1 = "%" + _prmCustID + "%";
            }

            if (_prmSearchBy.Trim() == "JobOrder")
            {
                _pattern2 = "%" + _prmSearchText + "%";
            }
            else if (_prmSearchBy.Trim() == "CustName")
            {
                _pattern3 = "%" + _prmSearchText + "%";
            }

            List<POSTrHotelHd> _result = new List<POSTrHotelHd>();
            try
            {
                var _query = (
                                from _hotelHd in this.db.POSTrHotelHds
                                join _settlementRef in this.db.POSTrSettlementDtRefTransactions
                                    on _hotelHd.TransNmbr equals _settlementRef.ReferenceNmbr
                                join _settlement in this.db.POSTrSettlementHds
                                    on _settlementRef.TransNmbr equals _settlement.TransNmbr
                                join _settlementDtProducy in this.db.POSTrSettlementDtProducts
                                    on _settlement.TransNmbr equals _settlementDtProducy.TransNmbr
                                into joined
                                from _settlementDtProducy in joined.DefaultIfEmpty()
                                where SqlMethods.Like((_hotelHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_settlement.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && SqlMethods.Like((_hotelHd.CustName ?? "").Trim().ToLower(), _pattern3.Trim().ToLower())
                                    && ((_settlement.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted) || _hotelHd.DPPaid > 0))
                                    && _settlementRef.TransType == AppModule.GetValue(TransactionType.Hotel)
                                    && _hotelHd.IsVOID == false
                                //&& ((_hotelHd.DeliveryStatus == null || _hotelHd.DeliveryStatus == false) ? false : true) == POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered)
                                //&& _settlementDtProducy.FgStock == 'Y'
                                select _hotelHd
                            ).Distinct();
                foreach (var _row in _query)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean SetDelivery(String _prmTransNmbr, Boolean _prmDeliverValue)
        {
            Boolean _result = false;

            try
            {
                POSTrHotelHd _hotelHd = this.db.POSTrHotelHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _hotelHd.DeliveryStatus = _prmDeliverValue;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public string GetRefNmbrInterByTransType(string _prmCode, string _prmTranstype)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions

                                where _posTrSettlementRefTransac.TransNmbr == _prmCode
                                && _posTrSettlementRefTransac.TransType == _prmTranstype
                                select _posTrSettlementRefTransac.ReferenceNmbr
                              ).FirstOrDefault();


                String RefNmbr = _query;

                var _query2 = (
                                from _posTrSettlemenRefTrans in this.db.POSTrSettlementDtRefTransactions
                                join _posHoteHD in this.db.POSTrHotelHds
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posHoteHD.TransNmbr
                                where _posHoteHD.TransNmbr == RefNmbr
                                select _posHoteHD.ReferenceNo
                                ).FirstOrDefault();

                _result = _query2;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetMemberNameByTransType(string _prmCode, string _prmTranstype)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions

                                where _posTrSettlementRefTransac.TransNmbr == _prmCode
                                && _posTrSettlementRefTransac.TransType == _prmTranstype
                                select _posTrSettlementRefTransac.ReferenceNmbr
                              ).FirstOrDefault();


                String RefNmbr = _query;

                var _query2 = (
                                from _posTrSettlemenRefTrans in this.db.POSTrSettlementDtRefTransactions
                                join _posHotelHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posHotelHD.TransNmbr
                                where _posHotelHD.TransNmbr == RefNmbr
                                && _posHotelHD.TransType == _prmTranstype
                                select _posHotelHD.CustName
                                ).FirstOrDefault();

                _result = _query2;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Char GetDonePayByTransType(string _prmCode, string _prmTranstype)
        {
            Char _result = 'N';

            try
            {
                var _query = (
                                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions

                                where _posTrSettlementRefTransac.TransNmbr == _prmCode
                                && _posTrSettlementRefTransac.TransType == _prmTranstype
                                select _posTrSettlementRefTransac.ReferenceNmbr
                              ).FirstOrDefault();


                String RefNmbr = _query;

                var _query2 = (
                                from _posTrSettlemenRefTrans in this.db.POSTrSettlementDtRefTransactions
                                join _posHotelHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posHotelHD.TransNmbr
                                where _posHotelHD.TransNmbr == RefNmbr
                                && _posHotelHD.TransType == _prmTranstype
                                select _posHotelHD.DoneSettlement
                                ).FirstOrDefault();

                _result = ((_query2 == null) ? 'N' : 'Y');

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetTransnumbSettlement(String _transNmbr)
        {
            String _result = "";
            try
            {
                //var _query = (
                //                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions

                //                where _posTrSettlementRefTransac.ReferenceNmbr == _transNmbr
                //                && _posTrSettlementRefTransac.TransType == AppModule.GetValue(TransactionType.Hotel)
                //                select _posTrSettlementRefTransac.TransNmbr
                //              ).FirstOrDefault();

                //String _transNmbrSettlementRef = _query;
                //var _query2 = (
                //                from _posTrSettlementHds in this.db.POSTrSettlementHds
                //                where _posTrSettlementHds.TransNmbr == _transNmbrSettlementRef
                //                select _posTrSettlementHds.TransNmbr
                //              ).FirstOrDefault();

                //_result = _query2;
                var _query = (
                                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions
                                join _posTrSettlementHd in this.db.POSTrSettlementHds
                                on _posTrSettlementRefTransac.TransNmbr equals _posTrSettlementHd.TransNmbr
                                where _posTrSettlementRefTransac.ReferenceNmbr == _transNmbr
                                && _posTrSettlementRefTransac.TransType.ToLower() == AppModule.GetValue(TransactionType.Hotel).ToLower()
                                select _posTrSettlementHd.FileNmbr
                              ).FirstOrDefault();

                _result = _query;
            }

            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean SendToCashierPOSTrHotel(string _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    POSTrHotelHd _posTrHotelHd = this.GetSinglePOSTrHotelHd(_prmTransNmbr);
                    foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_posTrHotelHd.TransDate).Year, Convert.ToDateTime(_posTrHotelHd.TransDate).Month, AppModule.GetValue(TransactionType.Hotel), this._companyTag, ""))
                    {
                        _posTrHotelHd.FileNmbr = item.Number;
                    }

                    _result = true;

                    this.db.SubmitChanges();

                    _scope.Complete();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        #endregion

        #region POSTrHotelDt

        public Double RowsCountPOSTrHotelDt(string _prmCode)
        {
            double _result = 0;

            var _query =
                        (
                            from _posTrHotelDts in this.db.POSTrHotelDts
                            where _posTrHotelDts.TransNmbr == _prmCode
                            orderby _posTrHotelDts.ItemNo descending
                            select _posTrHotelDts.ItemNo
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<POSTrHotelDt> GetListPOSTrHotelDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<POSTrHotelDt> _result = new List<POSTrHotelDt>();

            try
            {
                var _query = (
                                from _posTrHotelDt in this.db.POSTrHotelDts
                                where _posTrHotelDt.TransNmbr == _prmCode
                                orderby _posTrHotelDt.ItemNo descending
                                select _posTrHotelDt
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
        public List<POSTrHotelDt> GetListPOSTrHotelDt(string _prmCode)
        {
            List<POSTrHotelDt> _result = new List<POSTrHotelDt>();

            try
            {
                var _query = (
                                from _posTrHotelDt in this.db.POSTrHotelDts
                                where _posTrHotelDt.TransNmbr == _prmCode
                                select _posTrHotelDt
                            );

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

        public POSTrHotelDt GetSinglePOSTrHotelDt(string _prmCode, Int32 _prmItemNo)
        {
            POSTrHotelDt _result = null;

            try
            {
                _result = this.db.POSTrHotelDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPOSTrHotelDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            POSTrHotelHd _posTrHotelHd = new POSTrHotelHd();

            decimal _total = 0;

            try
            {

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    POSTrHotelDt _posTrHotelDt = this.db.POSTrHotelDts.Single(_temp => _temp.ItemNo.ToString() == _tempSplit[0] && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    this.db.POSTrHotelDts.DeleteOnSubmit(_posTrHotelDt);
                }

                for (int i = 0; i < _prmCode.Length; i++)////////
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    var _query = (
                                    from _posTrHotelDts2 in this.db.POSTrHotelDts
                                    where _posTrHotelDts2.ItemNo.ToString() == _tempSplit[0] && _posTrHotelDts2.TransNmbr == _prmTransNo.Trim().ToLower()
                                    select new
                                    {
                                        SellingPrice = _posTrHotelDts2.SellingPrice
                                    }
                                  );

                    foreach (var _obj in _query)
                    {
                        _total = _total + ((_obj.SellingPrice == null) ? 0 : Convert.ToDecimal(_obj.SellingPrice));
                    }
                }

                _posTrHotelHd = this.db.POSTrHotelHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _posTrHotelHd.SubTotalForex = _posTrHotelHd.SubTotalForex - _total;
                _posTrHotelHd.DiscForex = Convert.ToDecimal(_posTrHotelHd.SubTotalForex * _posTrHotelHd.DiscPercentage) / 100;
                _posTrHotelHd.PPNForex = Convert.ToDecimal((_posTrHotelHd.SubTotalForex - _posTrHotelHd.DiscForex) * _posTrHotelHd.PPNPercentage / 100);
                _posTrHotelHd.TotalForex = _posTrHotelHd.SubTotalForex - _posTrHotelHd.DiscForex + _posTrHotelHd.PPNForex + _posTrHotelHd.OtherForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeletePOSTrHotelDt(string _prmTransNo, string _prmCode)
        {
            bool _result = false;
            POSTrHotelHd _posTrHotelHd = new POSTrHotelHd();
            decimal _total = 0;

            try
            {
                POSTrHotelDt _posTrHotelDt = this.db.POSTrHotelDts.Single(_temp => _temp.ItemNo.ToString() == _prmCode && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                this.db.POSTrHotelDts.DeleteOnSubmit(_posTrHotelDt);

                var _query = (
                                from _posTrHotelDts2 in this.db.POSTrHotelDts
                                where _posTrHotelDts2.ItemNo.ToString() == _prmCode
                                   && _posTrHotelDts2.TransNmbr == _prmTransNo
                                select new
                                {
                                    SellingPrice = _posTrHotelDts2.SellingPrice
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = _total + ((_obj.SellingPrice == null) ? 0 : Convert.ToDecimal(_obj.SellingPrice));
                }

                _posTrHotelHd = this.db.POSTrHotelHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _posTrHotelHd.SubTotalForex = _posTrHotelHd.SubTotalForex - _total;
                _posTrHotelHd.DiscForex = Convert.ToDecimal(_posTrHotelHd.SubTotalForex * _posTrHotelHd.DiscPercentage) / 100;
                _posTrHotelHd.PPNForex = Convert.ToDecimal((_posTrHotelHd.SubTotalForex - _posTrHotelHd.DiscForex) * _posTrHotelHd.PPNPercentage / 100);
                _posTrHotelHd.TotalForex = _posTrHotelHd.SubTotalForex - _posTrHotelHd.DiscForex + _posTrHotelHd.PPNForex + _posTrHotelHd.OtherForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public bool AddPOSTrHotelDt(POSTrHotelDt _prmPOSTrHotelDt)
        {
            bool _result = false;

            POSTrHotelHd _posTrHotelHd = new POSTrHotelHd();

            //decimal _total = 0;

            try
            {
                //var _query = (
                //               from _posTrHotelDt in this.db.POSTrHotelDts
                //               where !(
                //                           from _posTrHotelDt2 in this.db.POSTrHotelDts
                //                           where _posTrHotelDt2.ItemNo == _prmPOSTrHotelDt.ItemNo && _posTrHotelDt2.TransNmbr == _prmPOSTrHotelDt.TransNmbr
                //                           select new { _posTrHotelDt2.ItemNo }
                //                       ).Contains(new { _posTrHotelDt.ItemNo })
                //                       && _posTrHotelDt.TransNmbr == _prmPOSTrHotelDt.TransNmbr
                //               group _prmPOSTrHotelDt by _prmPOSTrHotelDt.TransNmbr into _grp
                //               select new
                //               {
                //                   SellingPrice = _grp.Sum(a => a.SellingPrice)
                //               }
                //             );

                //foreach (var _obj in _query)
                //{
                //    _total = (_obj.SellingPrice == null) ? 0 : Convert.ToDecimal(_obj.SellingPrice);
                //}

                _posTrHotelHd = this.db.POSTrHotelHds.Single(_fa => _fa.TransNmbr == _prmPOSTrHotelDt.TransNmbr);

                //_posTrHotelHd.SubTotalForex = _total + ((_prmPOSTrHotelDt.SellingPrice == null) ? 0 : Convert.ToDecimal(_prmPOSTrHotelDt.SellingPrice));
                _posTrHotelHd.SubTotalForex = _prmPOSTrHotelDt.SellingPrice + ((_posTrHotelHd.SubTotalForex == null) ? 0 : Convert.ToDecimal(_posTrHotelHd.SubTotalForex));
                _posTrHotelHd.DiscForex = Convert.ToDecimal(_posTrHotelHd.SubTotalForex * _posTrHotelHd.DiscPercentage) / 100;
                _posTrHotelHd.PPNForex = Convert.ToDecimal((_posTrHotelHd.SubTotalForex - _posTrHotelHd.DiscForex) * _posTrHotelHd.PPNPercentage / 100);

                _posTrHotelHd.TotalForex = _posTrHotelHd.SubTotalForex - _posTrHotelHd.DiscForex + _posTrHotelHd.PPNForex + _posTrHotelHd.OtherForex;

                this.db.POSTrHotelDts.InsertOnSubmit(_prmPOSTrHotelDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPOSTrHotelDt(POSTrHotelDt _prmPOSTrHotelDt, decimal _subTotalForex, string _sellingPrice)
        {
            bool _result = false;

            POSTrHotelHd _posTrHotelHd = new POSTrHotelHd();

            decimal _total = 0;

            try
            {
                //var _query = (
                //               from _posTrHotelDt in this.db.POSTrHotelDts
                //               where !(
                //                           from _posTrHotelDt2 in this.db.POSTrHotelDts
                //                           where _posTrHotelDt2.ItemNo == _prmPOSTrHotelDt.ItemNo && _posTrHotelDt2.TransNmbr == _prmPOSTrHotelDt.TransNmbr
                //                           select new { _posTrHotelDt2.ItemNo }
                //                       ).Contains(new { _posTrHotelDt.ItemNo })
                //                       && _posTrHotelDt.TransNmbr == _prmPOSTrHotelDt.TransNmbr
                //               group _prmPOSTrHotelDt by _prmPOSTrHotelDt.TransNmbr into _grp
                //               select new
                //               {
                //                   SellingPrice = _grp.Sum(a => a.SellingPrice)
                //               }
                //             );

                //foreach (var _obj in _query)
                //{
                //    _total = (_obj.SellingPrice == null) ? 0 : Convert.ToDecimal(_obj.SellingPrice);
                //}

                _posTrHotelHd = this.db.POSTrHotelHds.Single(_fa => _fa.TransNmbr == _prmPOSTrHotelDt.TransNmbr);

                _posTrHotelHd.SubTotalForex = _subTotalForex + Convert.ToDecimal(_sellingPrice);
                _posTrHotelHd.DiscForex = Convert.ToDecimal(_posTrHotelHd.SubTotalForex * _posTrHotelHd.DiscPercentage) / 100;
                _posTrHotelHd.PPNForex = Convert.ToDecimal((_posTrHotelHd.SubTotalForex - _posTrHotelHd.DiscForex) * _posTrHotelHd.PPNPercentage / 100);

                _posTrHotelHd.TotalForex = _posTrHotelHd.SubTotalForex - _posTrHotelHd.DiscForex + _posTrHotelHd.PPNForex + _posTrHotelHd.OtherForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string Resived(string _prmTransNmbr, int _itemNo, decimal _prmBuyingPrice, decimal _prmSellingPrice, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spPOS_HotelVoucherRevisePrice(_prmTransNmbr, _itemNo, _prmBuyingPrice, _prmSellingPrice, _prmuser, ref _result);

                    _scope.Complete();

                }
            }
            catch (Exception ex)
            {
                _result = "Revised Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }


        #endregion

        #region POSMsHotel

        public double RowsCountPOSMsHotel(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "HotelCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "HotelName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }


            try
            {
                var _query =
                            (
                                from _POSMsHotel in this.db.POSMsHotels
                                where (SqlMethods.Like(_POSMsHotel.HotelCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_POSMsHotel.HotelName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _POSMsHotel.HotelCode
                            ).Count();

                _result = _query;

            }
            catch (Exception)
            {

                throw;
            }

            return _result;
        }

        public List<POSMsHotel> GetListPOSMsHotel(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsHotel> _result = new List<POSMsHotel>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "HotelCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "HotelName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _POSMsHotel in this.db.POSMsHotels
                                where (SqlMethods.Like(_POSMsHotel.HotelCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_POSMsHotel.HotelName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _POSMsHotel.HotelName descending
                                select _POSMsHotel
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

        public POSMsHotel GetSinglePOSMsHotel(string _prmCode)
        {
            POSMsHotel _result = null;

            try
            {
                _result = this.db.POSMsHotels.Single(_temp => _temp.HotelCode.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    POSMsHotel _POSMsHotel = this.db.POSMsHotels.Single(_temp => _temp.HotelCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.POSMsHotels.DeleteOnSubmit(_POSMsHotel);
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

        public bool AddPOSMsHotel(POSMsHotel _prmPOSMsHotel)
        {
            bool _result = false;

            try
            {
                this.db.POSMsHotels.InsertOnSubmit(_prmPOSMsHotel);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPOSMsHotel(POSMsHotel _prmPOSMsHotel)
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

        public String GetHotelNameByCode(string _prmHotelCode)
        {
            String _result = "";

            try
            {
                _result = this.db.POSMsHotels.Single(_temp => _temp.HotelCode.Trim().ToLower() == _prmHotelCode.Trim().ToLower()).HotelName;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsHotel> GetListSupplierPOSMsHotel()
        {
            List<POSMsHotel> _result = new List<POSMsHotel>();
            try
            {
                var _query = (
                                from _POSMsHotel in this.db.POSMsHotels
                                select new
                                {
                                    SupplierCode = _POSMsHotel.SuppCode,
                                    SupplierName = (from _msSupplier in this.db.MsSuppliers
                                                    where _msSupplier.SuppCode == _POSMsHotel.SuppCode
                                                    select _msSupplier.SuppName).FirstOrDefault()
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new POSMsHotel(_row.SupplierCode, _row.SupplierName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsHotel> GetListHotelBySuppCode(String _prmSuppCode)
        {
            List<POSMsHotel> _result = new List<POSMsHotel>();
            try
            {
                var _query = (
                                from _POSMsHotel in this.db.POSMsHotels
                                where _POSMsHotel.SuppCode.Trim().ToLower() == _prmSuppCode.Trim().ToLower()
                                select new
                                {
                                    HotelCode = _POSMsHotel.HotelCode,
                                    HotelName = _POSMsHotel.HotelName,
                                    SuppCode = _POSMsHotel.SuppCode
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new POSMsHotel(_row.HotelCode, _row.HotelName, _row.SuppCode));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~HotelBL()
        {
        }
    }
}
