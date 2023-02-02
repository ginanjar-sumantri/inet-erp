using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Data.Linq.SqlClient;
using System.Diagnostics;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Transactions;
using BusinessRule.POS;


namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Tour
{
    public sealed class TicketingBL : Base
    {
        public TicketingBL()
        {
        }

        #region POSTrTicketingHd

        public double RowsCountPOSTrTicketingHd(string _prmCategory, string _prmKeyword)
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
                                from _posTrTicketingHd in this.db.POSTrTicketingHds
                                where (SqlMethods.Like(_posTrTicketingHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posTrTicketingHd.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_posTrTicketingHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _posTrTicketingHd.Status != POSTicketingDataMapper.GetStatus(TransStatus.Deleted)
                                select _posTrTicketingHd.TransNmbr
                            ).Count();

                _result = _query;

            }
            catch (Exception)
            {

                throw;
            }

            return _result;
        }

        public List<POSTrTicketingHd> GetListPOSTrTicketingHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSTrTicketingHd> _result = new List<POSTrTicketingHd>();

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
                                from _posTrTicketingHd in this.db.POSTrTicketingHds
                                where (SqlMethods.Like(_posTrTicketingHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posTrTicketingHd.CustName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_posTrTicketingHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _posTrTicketingHd.Status != POSTicketingDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _posTrTicketingHd.DatePrep descending
                                select _posTrTicketingHd
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

        public POSTrTicketingHd GetSinglePOSTrTicketingHd(string _prmCode)
        {
            POSTrTicketingHd _result = null;

            try
            {
                _result = this.db.POSTrTicketingHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPOSTrTicketingHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    POSTrTicketingHd _posTrTicketingHd = this.db.POSTrTicketingHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower());

                    if (_posTrTicketingHd != null)
                    {
                        if ((_posTrTicketingHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.POSTrTicketingDts
                                          where _detail.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower()
                                          select _detail);

                            this.db.POSTrTicketingDts.DeleteAllOnSubmit(_query);

                            this.db.POSTrTicketingHds.DeleteOnSubmit(_posTrTicketingHd);

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

        public bool GetSinglePOSTrTicketingHdForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    POSTrTicketingHd _posTrTicketingHd = this.db.POSTrTicketingHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_posTrTicketingHd != null)
                    {
                        if (_posTrTicketingHd.Status != POSTicketingDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiApprovePOSTrTicketingHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    POSTrTicketingHd _posTrTicketingHd = this.db.POSTrTicketingHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_posTrTicketingHd.Status == POSTicketingDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _posTrTicketingHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _posTrTicketingHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_posTrTicketingHd != null)
                    {
                        if ((_posTrTicketingHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.POSTrTicketingDts
                                          where _detail.TransNmbr == _prmCode[i]
                                          select _detail);

                            this.db.POSTrTicketingDts.DeleteAllOnSubmit(_query);

                            this.db.POSTrTicketingHds.DeleteOnSubmit(_posTrTicketingHd);

                            _result = true;
                        }
                        else if (_posTrTicketingHd.FileNmbr != "" && _posTrTicketingHd.Status == POSTicketingDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _posTrTicketingHd.Status = POSTicketingDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddPOSTrTicketingHd(POSTrTicketingHd _prmPOSTrTicketingHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINSuppInvHd.TransDate.Year, _prmFINSuppInvHd.TransDate.Month, AppModule.GetValue(TransactionType.SupplierNote), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmPOSTrTicketingHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                this.db.POSTrTicketingHds.InsertOnSubmit(_prmPOSTrTicketingHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmPOSTrTicketingHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddPOSTrTicketingHdForDO(POSTrTicketingHd _prmPOSTrTicketingHd, POSTrDeliveryOrderRef _prmPOSTrDeliveryOrderRef)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmFINSuppInvHd.TransDate.Year, _prmFINSuppInvHd.TransDate.Month, AppModule.GetValue(TransactionType.SupplierNote), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmPOSTrTicketingHd.TransNmbr = _item.Number;
                    _prmPOSTrDeliveryOrderRef.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                this.db.POSTrTicketingHds.InsertOnSubmit(_prmPOSTrTicketingHd);
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

        public bool EditPOSTrTicketingHd(POSTrTicketingHd _prmPOSTrTicketingHd)
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
                int _success = this.db.spPOS_TicketingGetAppr(_prmTransNmbr, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.Ticketing);
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
                    int _success = this.db.spPOS_TicketingApprove(_prmTransNmbr, _prmuser, ref _result);

                    if (_result == "")
                    {
                        POSTrTicketingHd _posTrTicketingHd = this.GetSinglePOSTrTicketingHd(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_posTrTicketingHd.TransDate).Year, Convert.ToDateTime(_posTrTicketingHd.TransDate).Month, AppModule.GetValue(TransactionType.Ticketing), this._companyTag, ""))
                        {
                            _posTrTicketingHd.FileNmbr = item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.APRate);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = _posTrTicketingHd.FileNmbr;
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
                int _success = this.db.spPOS_TicketingPosting(_prmTransNmbr, _prmuser, ref _result);

                POSTrTicketingHd _posTrTicketingHd = this.db.POSTrTicketingHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());

                if (_result == "")
                {
                    _result = "Posting Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.SupplierNote);
                    _transActivity.TransNmbr = _prmTransNmbr;
                    _transActivity.FileNmbr = _posTrTicketingHd.FileNmbr;
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
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
                POSTrTicketingHd _posTrTicketingHd = this.db.POSTrTicketingHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_posTrTicketingHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 16 feb 09*/
                {
                    int _success = this.db.spPOS_TicketingUnPosting(_prmTransNmbr, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.Ticketing);
                        _transActivity.TransNmbr = _prmTransNmbr;
                        _transActivity.FileNmbr = _posTrTicketingHd.FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        _transActivity.ActivitiesDate = _posTrTicketingHd.TransDate;
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
                _result = "Unposting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Revised(string _prmTransNmbr, int _prmItemNo, decimal _prmBuyingPrice, decimal _prmSellingPrice, string _prmUser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spPOS_TicketingRevisePrice(_prmTransNmbr, _prmItemNo, _prmBuyingPrice, _prmSellingPrice, _prmUser, ref _result);


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

        public List<POSTrTicketingHd> GetListTicketingForDeliveryOrder(String _prmReferenceNo)
        {
            List<POSTrTicketingHd> _result = new List<POSTrTicketingHd>();

            try
            {
                var _query = (
                                from _ticketingHd in this.db.POSTrTicketingHds
                                where _ticketingHd.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                                orderby _ticketingHd.TransDate descending
                                select _ticketingHd
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

        public string GetValueForReport(string _prmReportID, string _prmReportParameter)
        {
            string _result = "";

            try
            {
                var _query = (from _companyConfig in this.db.CompanyConfigReportParameters
                              where _companyConfig.ReportID == _prmReportID
                              && _companyConfig.ReportParameter == _prmReportParameter
                              select new
                              {
                                  Value = _companyConfig.Value
                              }
                              );
                foreach (var _obj in _query)
                {
                    _result = _obj.Value;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<POSTrTicketingHd> GetListTicketingHdSendToCashier(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrTicketingHd> _result = new List<POSTrTicketingHd>();
            try
            {
                var _query = (
                                from _ticketingHd in this.db.POSTrTicketingHds
                                where SqlMethods.Like((_ticketingHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_ticketingHd.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && _ticketingHd.Status == POSHotelDataMapper.GetStatus(TransStatus.Approved)
                                    && _ticketingHd.DoneSettlement == POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet)
                                    && _ticketingHd.IsVOID == false
                                    && _ticketingHd.DeliveryOrderReff == ""
                                select _ticketingHd
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
                POSTrTicketingHd _internetHd = this.db.POSTrTicketingHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
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

        public List<POSTrTicketingHd> GetListTicketingPayNotDelivered(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrTicketingHd> _result = new List<POSTrTicketingHd>();
            try
            {
                var _query = (
                                from _ticketingHd in this.db.POSTrTicketingHds
                                join _settlementRef in this.db.POSTrSettlementDtRefTransactions
                                    on _ticketingHd.TransNmbr equals _settlementRef.ReferenceNmbr
                                join _settlement in this.db.POSTrSettlementHds
                                    on _settlementRef.TransNmbr equals _settlement.TransNmbr
                                join _settlementDtProducy in this.db.POSTrSettlementDtProducts
                                    //on _settlement.TransNmbr equals _settlementDtProducy.TransNmbr
                                    on _ticketingHd.TransNmbr equals _settlementDtProducy.ReffNmbr
                                into joined
                                from _settlementDtProducy in joined.DefaultIfEmpty()
                                where SqlMethods.Like((_ticketingHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_settlement.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && SqlMethods.Like((_ticketingHd.CustName ?? "").Trim().ToLower(), _pattern3.Trim().ToLower())
                                    && ((_settlement.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted) || _ticketingHd.DPPaid > 0))
                                    && _settlementRef.TransType == AppModule.GetValue(TransactionType.Ticketing)
                                    && _ticketingHd.IsVOID == false
                                    //&& ((_ticketingHd.DeliveryStatus == null || _ticketingHd.DeliveryStatus == false) ? false : true) == POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered)
                                //&& _settlementDtProducy.FgStock == 'Y'
                                select _ticketingHd
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
                POSTrTicketingHd _ticketingHd = this.db.POSTrTicketingHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _ticketingHd.DeliveryStatus = _prmDeliverValue;

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
                                join _posTicketingHD in this.db.POSTrTicketingHds
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posTicketingHD.TransNmbr
                                where _posTicketingHD.TransNmbr == RefNmbr
                                select _posTicketingHD.ReferenceNo
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
                                join _posTicketingHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posTicketingHD.TransNmbr
                                where _posTicketingHD.TransNmbr == RefNmbr
                                && _posTicketingHD.TransType == _prmTranstype
                                select _posTicketingHD.CustName
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
                                join _posTicketingHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posTicketingHD.TransNmbr
                                where _posTicketingHD.TransNmbr == RefNmbr
                                && _posTicketingHD.TransType == _prmTranstype
                                select _posTicketingHD.DoneSettlement
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
                //                && _posTrSettlementRefTransac.TransType == AppModule.GetValue(TransactionType.Ticketing)
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
                                && _posTrSettlementRefTransac.TransType.ToLower() == AppModule.GetValue(TransactionType.Ticketing).ToLower()
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

        public Boolean SendToCashierPOSTrTicketing(string _prmTransNmbr)
        {
            bool _result = false;

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    POSTrTicketingHd _posTrTicketingHd = this.GetSinglePOSTrTicketingHd(_prmTransNmbr);
                    foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_posTrTicketingHd.TransDate).Year, Convert.ToDateTime(_posTrTicketingHd.TransDate).Month, AppModule.GetValue(TransactionType.Ticketing), this._companyTag, ""))
                    {
                        _posTrTicketingHd.FileNmbr = item.Number;
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

        #region POSTrTicketingDt

        public double RowsCountPOSTrTicketingDt(string _prmCode)
        {
            double _result = 0;

            var _query =
                        (
                            from _posTrTicketingDts in this.db.POSTrTicketingDts
                            where _posTrTicketingDts.TransNmbr == _prmCode
                            orderby _posTrTicketingDts.ItemNo descending
                            select _posTrTicketingDts.ItemNo
                        ).Count();

            _result = _query;

            return _result;

        }

        public List<POSTrTicketingDt> GetListPOSTrTicketingDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<POSTrTicketingDt> _result = new List<POSTrTicketingDt>();

            try
            {
                var _query = (
                                from _posTrTicketingDt in this.db.POSTrTicketingDts
                                where _posTrTicketingDt.TransNmbr == _prmCode
                                orderby _posTrTicketingDt.ItemNo descending
                                select _posTrTicketingDt
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

        public POSTrTicketingDt GetSinglePOSTrTicketingDt(string _prmCode, Int32 _prmItemNo)
        {
            POSTrTicketingDt _result = null;

            try
            {
                _result = this.db.POSTrTicketingDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public bool DeleteMultiPOSTrTicketingDt(string[] _prmCode, string _prmTransNo)
        //{
        //    bool _result = false;

        //    POSTrTicketingHd _posTrTicketingHd = new POSTrTicketingHd();
        //    POSTrTicketingDt _prmPOSTrTicketingDt = new POSTrTicketingDt();

        //    decimal _total = 0;

        //    try
        //    {

        //        for (int i = 0; i < _prmCode.Length; i++)
        //        {
        //            string[] _tempSplit = _prmCode[i].Split('-');

        //            POSTrTicketingDt _posTrTicketingDt = this.db.POSTrTicketingDts.Single(_temp => _temp.ItemNo.ToString() == _tempSplit[0] && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

        //            this.db.POSTrTicketingDts.DeleteOnSubmit(_posTrTicketingDt);
        //        }

        //        for (int i = 0; i < _prmCode.Length; i++)////////
        //        {
        //            string[] _tempSplit = _prmCode[i].Split('-');

        //            var _query = (
        //                            from _posTrTicketingDts2 in this.db.POSTrTicketingDts
        //                            where !(
        //                                       from _posTrTicketingDts3 in this.db.POSTrTicketingDts
        //                                       where _posTrTicketingDts3.ItemNo.ToString() == _tempSplit[0] && _posTrTicketingDts3.TransNmbr == _prmTransNo
        //                                       select new { _posTrTicketingDts3.ItemNo }
        //                               ).Contains(new { _posTrTicketingDts2.ItemNo })
        //                               && 
        //                            _posTrTicketingDts2.TransNmbr == _prmTransNo
        //                            group _posTrTicketingDts2 by _posTrTicketingDts2.TransNmbr into _grp
        //                            select new
        //                            {
        //                                SellingPrice = _grp.Sum(a => a.SellingPrice)
        //                            }
        //                          );

        //            foreach (var _obj in _query)
        //            {
        //                _total = (_obj.SellingPrice == null) ? 0 : Convert.ToDecimal(_obj.SellingPrice);
        //            }
        //        }

        //        _posTrTicketingHd = this.db.POSTrTicketingHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

        //        _posTrTicketingHd.SubTotalForex = _total;
        //        //_posTrTicketingHd.SubTotalForex = _prmPOSTrTicketingDt.SellingPrice + ((_posTrTicketingHd.SubTotalForex == null) ? 0 : Convert.ToDecimal(_posTrTicketingHd.SubTotalForex));
        //        _posTrTicketingHd.DiscForex = Convert.ToDecimal(_posTrTicketingHd.SubTotalForex * _posTrTicketingHd.DiscPercentage) / 100;
        //        _posTrTicketingHd.PPNForex = Convert.ToDecimal((_posTrTicketingHd.SubTotalForex - _posTrTicketingHd.DiscForex) * _posTrTicketingHd.PPNPercentage / 100);
        //        _posTrTicketingHd.TotalForex = _posTrTicketingHd.SubTotalForex - _posTrTicketingHd.DiscForex + _posTrTicketingHd.PPNForex + _posTrTicketingHd.OtherForex;

        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool DeleteMultiPOSTrTicketingDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;

            POSTrTicketingHd _posTrTicketingHd = new POSTrTicketingHd();

            decimal _total = 0;

            try
            {

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    POSTrTicketingDt _posTrTicketingDt = this.db.POSTrTicketingDts.Single(_temp => _temp.ItemNo.ToString() == _tempSplit[0] && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                    this.db.POSTrTicketingDts.DeleteOnSubmit(_posTrTicketingDt);
                }

                for (int i = 0; i < _prmCode.Length; i++)////////
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    var _query = (
                                    from _posTrTicketingDts2 in this.db.POSTrTicketingDts
                                    where _posTrTicketingDts2.ItemNo.ToString() == _tempSplit[0]
                                        // !(
                                        //        from _posTrTicketingDts3 in this.db.POSTrTicketingDts
                                        //        where _posTrTicketingDts3.ItemNo.ToString() == _tempSplit[0] 
                                        //        && _posTrTicketingDts3.TransNmbr == _prmTransNo
                                        //        select new { _posTrTicketingDts3.ItemNo }
                                        //).Contains(new { _posTrTicketingDts2.ItemNo })
                                       && _posTrTicketingDts2.TransNmbr == _prmTransNo
                                    //group _posTrTicketingDts2 by _posTrTicketingDts2.TransNmbr into _grp
                                    select new
                                    {
                                        SellingPrice = _posTrTicketingDts2.SellingPrice
                                    }
                                  );

                    foreach (var _obj in _query)
                    {
                        _total = _total + ((_obj.SellingPrice == null) ? 0 : Convert.ToDecimal(_obj.SellingPrice));
                    }
                }

                _posTrTicketingHd = this.db.POSTrTicketingHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _posTrTicketingHd.SubTotalForex = _posTrTicketingHd.SubTotalForex - _total;
                _posTrTicketingHd.DiscForex = Convert.ToDecimal(_posTrTicketingHd.SubTotalForex * _posTrTicketingHd.DiscPercentage) / 100;
                _posTrTicketingHd.PPNForex = Convert.ToDecimal((_posTrTicketingHd.SubTotalForex - _posTrTicketingHd.DiscForex) * _posTrTicketingHd.PPNPercentage / 100);
                _posTrTicketingHd.TotalForex = _posTrTicketingHd.SubTotalForex - _posTrTicketingHd.DiscForex + _posTrTicketingHd.PPNForex + _posTrTicketingHd.OtherForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeletePOSTrTicketingDt(string _prmTransNo, string _prmCode)
        {
            bool _result = false;
            POSTrTicketingHd _posTrTicketingHd = new POSTrTicketingHd();
            decimal _total = 0;

            try
            {
                POSTrTicketingDt _posTrTicketingDt = this.db.POSTrTicketingDts.Single(_temp => _temp.ItemNo.ToString() == _prmCode && _temp.TransNmbr.Trim().ToLower() == _prmTransNo.Trim().ToLower());

                this.db.POSTrTicketingDts.DeleteOnSubmit(_posTrTicketingDt);

                var _query = (
                                from _posTrTicketingDts2 in this.db.POSTrTicketingDts
                                where _posTrTicketingDts2.ItemNo.ToString() == _prmCode
                                   && _posTrTicketingDts2.TransNmbr == _prmTransNo
                                select new
                                {
                                    SellingPrice = _posTrTicketingDts2.SellingPrice
                                }
                              );

                foreach (var _obj in _query)
                {
                    _total = _total + ((_obj.SellingPrice == null) ? 0 : Convert.ToDecimal(_obj.SellingPrice));
                }

                _posTrTicketingHd = this.db.POSTrTicketingHds.Single(_fa => _fa.TransNmbr == _prmTransNo);

                _posTrTicketingHd.SubTotalForex = _posTrTicketingHd.SubTotalForex - _total;
                _posTrTicketingHd.DiscForex = Convert.ToDecimal(_posTrTicketingHd.SubTotalForex * _posTrTicketingHd.DiscPercentage) / 100;
                _posTrTicketingHd.PPNForex = Convert.ToDecimal((_posTrTicketingHd.SubTotalForex - _posTrTicketingHd.DiscForex) * _posTrTicketingHd.PPNPercentage / 100);
                _posTrTicketingHd.TotalForex = _posTrTicketingHd.SubTotalForex - _posTrTicketingHd.DiscForex + _posTrTicketingHd.PPNForex + _posTrTicketingHd.OtherForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddPOSTrTicketingDt(POSTrTicketingDt _prmPOSTrTicketingDt)
        {
            bool _result = false;

            POSTrTicketingHd _posTrTicketingHd = new POSTrTicketingHd();

            //decimal _total = 0;

            try
            {
                //var _query = (
                //               from _posTrTicketingDt in this.db.POSTrTicketingDts
                //               where !(
                //                           from _posTrTicketingDt2 in this.db.POSTrTicketingDts
                //                           where _posTrTicketingDt2.ItemNo == _prmPOSTrTicketingDt.ItemNo && _posTrTicketingDt2.TransNmbr == _prmPOSTrTicketingDt.TransNmbr
                //                           select new { _posTrTicketingDt2.ItemNo }
                //                       ).Contains(new { _posTrTicketingDt.ItemNo })
                //                       && _posTrTicketingDt.TransNmbr == _prmPOSTrTicketingDt.TransNmbr
                //               group _prmPOSTrTicketingDt by _prmPOSTrTicketingDt.TransNmbr into _grp
                //               select new
                //               {
                //                   SellingPrice = _grp.Sum(a => a.SellingPrice)
                //               }
                //             );

                //foreach (var _obj in _query)
                //{
                //    _total = (_obj.SellingPrice == null) ? 0 : Convert.ToDecimal(_obj.SellingPrice);
                //}

                _posTrTicketingHd = this.db.POSTrTicketingHds.Single(_fa => _fa.TransNmbr == _prmPOSTrTicketingDt.TransNmbr);

                //_posTrTicketingHd.SubTotalForex = _total + ((_prmPOSTrTicketingDt.SellingPrice == null) ? 0 : Convert.ToDecimal(_prmPOSTrTicketingDt.SellingPrice));
                _posTrTicketingHd.SubTotalForex = Convert.ToDecimal(_prmPOSTrTicketingDt.SellingPrice + _posTrTicketingHd.SubTotalForex);
                _posTrTicketingHd.DiscForex = Convert.ToDecimal(_posTrTicketingHd.SubTotalForex * _posTrTicketingHd.DiscPercentage) / 100;
                _posTrTicketingHd.PPNForex = Convert.ToDecimal((_posTrTicketingHd.SubTotalForex - _posTrTicketingHd.DiscForex) * _posTrTicketingHd.PPNPercentage / 100);

                _posTrTicketingHd.TotalForex = _posTrTicketingHd.SubTotalForex - _posTrTicketingHd.DiscForex + _posTrTicketingHd.PPNForex + _posTrTicketingHd.OtherForex;

                this.db.POSTrTicketingDts.InsertOnSubmit(_prmPOSTrTicketingDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPOSTrTicketingDt(POSTrTicketingDt _prmPOSTrTicketingDt, decimal _prmSubTotalForex, string _prmSellingPrice)
        {
            bool _result = false;

            POSTrTicketingHd _posTrTicketingHd = new POSTrTicketingHd();

            decimal _total = 0;

            try
            {
                //var _query = (
                //               from _posTrTicketingDt in this.db.POSTrTicketingDts
                //               where _posTrTicketingDt.ItemNo == _prmPOSTrTicketingDt.ItemNo 
                //               && _posTrTicketingDt.TransNmbr == _prmPOSTrTicketingDt.TransNmbr                                   //!(
                //               //            from _posTrTicketingDt2 in this.db.POSTrTicketingDts
                //               //            where _posTrTicketingDt2.ItemNo == _prmPOSTrTicketingDt.ItemNo && _posTrTicketingDt2.TransNmbr == _prmPOSTrTicketingDt.TransNmbr
                //               //            select new { _posTrTicketingDt2.ItemNo }
                //               //        ).Contains(new { _posTrTicketingDt.ItemNo })
                //               //        && _posTrTicketingDt.TransNmbr == _prmPOSTrTicketingDt.TransNmbr
                //               //group _prmPOSTrTicketingDt by _prmPOSTrTicketingDt.TransNmbr into _grp
                //               select new
                //               {
                //                   SellingPrice = _posTrTicketingDt.SellingPrice
                //               }
                //             );

                //foreach (var _obj in _query)
                //{
                //    _total = (_obj.SellingPrice == null) ? 0 : Convert.ToDecimal(_obj.SellingPrice);
                //}

                _posTrTicketingHd = this.db.POSTrTicketingHds.Single(_fa => _fa.TransNmbr == _prmPOSTrTicketingDt.TransNmbr);

                //_posTrTicketingHd.SubTotalForex = _total + ((_prmPOSTrTicketingDt.SellingPrice == null) ? 0 : Convert.ToDecimal(_prmPOSTrTicketingDt.SellingPrice));
                //_posTrTicketingHd.SubTotalForex = _prmPOSTrTicketingDt.SellingPrice + ((_posTrTicketingHd.SubTotalForex == null) ? 0 : Convert.ToDecimal(_posTrTicketingHd.SubTotalForex));
                _posTrTicketingHd.SubTotalForex = Convert.ToDecimal(_prmSellingPrice) + _prmSubTotalForex;
                _posTrTicketingHd.DiscForex = Convert.ToDecimal(_posTrTicketingHd.SubTotalForex * _posTrTicketingHd.DiscPercentage) / 100;
                _posTrTicketingHd.PPNForex = Convert.ToDecimal((_posTrTicketingHd.SubTotalForex - _posTrTicketingHd.DiscForex) * _posTrTicketingHd.PPNPercentage / 100);

                _posTrTicketingHd.TotalForex = _posTrTicketingHd.SubTotalForex - _posTrTicketingHd.DiscForex + _posTrTicketingHd.PPNForex + _posTrTicketingHd.OtherForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSTrTicketingDt> GetListPOSTrTicketingDt(string _prmCode)
        {
            List<POSTrTicketingDt> _result = new List<POSTrTicketingDt>();

            try
            {
                var _query = (
                                from _posTrTicketingDt in this.db.POSTrTicketingDts
                                where _posTrTicketingDt.TransNmbr == _prmCode
                                select _posTrTicketingDt
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


        #endregion

        ~TicketingBL()
        {
        }
    }
}
