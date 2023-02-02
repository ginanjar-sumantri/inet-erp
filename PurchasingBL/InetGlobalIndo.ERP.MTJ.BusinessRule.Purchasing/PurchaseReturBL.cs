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
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing
{
    public sealed class PurchaseReturBL : Base
    {
        public PurchaseReturBL()
        {
        }

        #region PRCReturHd
        public double RowsCountPRCReturHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _prcReturHd in this.db.PRCReturHds
                            join _msSupplier in this.db.MsSuppliers
                                on _prcReturHd.SuppCode equals _msSupplier.SuppCode
                            where (SqlMethods.Like(_prcReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && _prcReturHd.Status != PurchaseReturDataMapper.GetStatusByte(TransStatus.Deleted)
                            select _prcReturHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<PRCReturHd> GetListPRCReturHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<PRCReturHd> _result = new List<PRCReturHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _prcReturHd in this.db.PRCReturHds
                                join _msSupplier in this.db.MsSuppliers
                                on _prcReturHd.SuppCode equals _msSupplier.SuppCode
                                where (SqlMethods.Like(_prcReturHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _prcReturHd.Status != PurchaseReturDataMapper.GetStatusByte(TransStatus.Deleted)
                                orderby _prcReturHd.EditDate descending
                                select new
                                {
                                    TransNmbr = _prcReturHd.TransNmbr,
                                    FileNmbr = _prcReturHd.FileNmbr,
                                    TransDate = _prcReturHd.TransDate,
                                    Status = _prcReturHd.Status,
                                    SuppCode = _prcReturHd.SuppCode,
                                    SuppName = _msSupplier.SuppName,
                                    RRNo = _prcReturHd.RRNo,
                                    Remark = _prcReturHd.Remark,
                                    CreatedBy = _prcReturHd.CreatedBy
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

                if (_prmOrderBy == "Supplier")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.SuppName)) : (_query.OrderByDescending(a => a.SuppName));

                if (_prmOrderBy == "RR No.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.RRNo)) : (_query.OrderByDescending(a => a.RRNo));

                _query = _query.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new PRCReturHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.SuppCode, _row.SuppName, _row.RRNo, _row.Remark, _row.CreatedBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public PRCReturHd GetSinglePRCReturHd(string _prmCode)
        {
            PRCReturHd _result = null;

            try
            {
                _result = this.db.PRCReturHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<PRCReturHd> GetListPRBySupplierForDDL(string _prmSuppCode)
        {
            List<PRCReturHd> _result = new List<PRCReturHd>();

            try
            {
                var _query = (
                                from _v_PurchaseReturtForRejectOut in this.db.V_PurchaseReturtForRejectOuts
                                where _v_PurchaseReturtForRejectOut.SuppCode.Trim().ToLower() == _prmSuppCode.Trim().ToLower()
                                orderby _v_PurchaseReturtForRejectOut.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _v_PurchaseReturtForRejectOut.TransNmbr,
                                    FileNmbr = _v_PurchaseReturtForRejectOut.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new PRCReturHd(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPRCReturHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    PRCReturHd _PRCReturHd = this.db.PRCReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_PRCReturHd != null)
                    {
                        if ((_PRCReturHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.PRCReturDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.PRCReturDts.DeleteAllOnSubmit(_query);

                            this.db.PRCReturHds.DeleteOnSubmit(_PRCReturHd);

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

        public string AddPRCReturHd(PRCReturHd _prmPRCReturHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmPRCReturHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.PRCReturHds.InsertOnSubmit(_prmPRCReturHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);
                this.db.SubmitChanges();

                _result = _prmPRCReturHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPRCReturHd(PRCReturHd _prmPRCReturHd)
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
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_PRReturGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.PurchaseRetur);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
                else
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
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
                    this.db.S_PRReturApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        PRCReturHd _prcReturHd = this.GetSinglePRCReturHd(_prmTransNmbr);

                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_prcReturHd.TransDate.Year, _prcReturHd.TransDate.Month, AppModule.GetValue(TransactionType.PurchaseRetur), this._companyTag, ""))
                        {
                            _prcReturHd.FileNmbr = _item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.PurchaseRetur);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSinglePRCReturHd(_prmTransNmbr).FileNmbr;
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
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_PRReturPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Posting Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.PurchaseRetur);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
                    _transActivity.FileNmbr = this.GetSinglePRCReturHd(_prmTransNmbr).FileNmbr;
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
                else
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string Unposting(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_PRReturUnPost(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Unposting Success";
                }
                else
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Unposting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public bool GetSinglePRCReturHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    PRCReturHd _PRCReturHd = this.db.PRCReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_PRCReturHd != null)
                    {
                        if (_PRCReturHd.Status != PurchaseReturDataMapper.GetStatusByte(TransStatus.Posted))
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

        public bool DeleteMultiApprovePRCReturHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    PRCReturHd _PRCReturHd = this.db.PRCReturHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_PRCReturHd.Status == PurchaseReturDataMapper.GetStatusByte(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _PRCReturHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _PRCReturHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_PRCReturHd != null)
                    {
                        if ((_PRCReturHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.PRCReturDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.PRCReturDts.DeleteAllOnSubmit(_query);

                            this.db.PRCReturHds.DeleteOnSubmit(_PRCReturHd);

                            _result = true;
                        }
                        else if (_PRCReturHd.FileNmbr != "" && _PRCReturHd.Status == PurchaseReturDataMapper.GetStatusByte(TransStatus.Approved))
                        {
                            _PRCReturHd.Status = PurchaseReturDataMapper.GetStatusByte(TransStatus.Deleted);
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

        #region PRCReturDt
        public int RowsCountPRCReturDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.PRCReturDts.Where(_temp => _temp.TransNmbr == _prmCode).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<PRCReturDt> GetListPRCReturDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<PRCReturDt> _result = new List<PRCReturDt>();

            try
            {
                //var _query = (
                //                from _prcReturDt in this.db.PRCReturDts
                //                where _prcReturDt.TransNmbr == _prmCode
                //                orderby _prcReturDt.ProductCode ascending
                //                select new
                //                {
                //                    TransNmbr = _prcReturDt.TransNmbr,
                //                    ProductCode = _prcReturDt.ProductCode,
                //                    LocationCode = _prcReturDt.LocationCode,
                //                    LocationName = (
                //                                    from _location in this.db.MsWrhsLocations
                //                                    where _location.WLocationCode == _prcReturDt.LocationCode
                //                                    select _location.WLocationName
                //                                   ).FirstOrDefault(),
                //                    Qty = _prcReturDt.Qty,
                //                    Unit = _prcReturDt.Unit,
                //                    Price = _prcReturDt.Price,
                //                    AmountForex = _prcReturDt.AmountForex,
                //                    Remark = _prcReturDt.Remark,
                //                    CreatedBy = _prcReturDt.CreatedBy
                //                }
                //            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                //foreach (var _row in _query)
                //{
                //    _result.Add(new PRCReturDt(_row.TransNmbr, _row.ProductCode, _row.LocationCode, _row.LocationName, _row.Qty, _row.Unit, _row.Price, _row.AmountForex, _row.Remark, _row.CreatedBy));
                //}
                var _query = (
                                from _prcReturDt in this.db.PRCReturDts
                                where _prcReturDt.TransNmbr == _prmCode
                                orderby _prcReturDt.ProductCode ascending
                                select _prcReturDt
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

        public List<PRCReturDt> GetListPRCReturDt(string _prmCode)
        {
            List<PRCReturDt> _result = new List<PRCReturDt>();

            try
            {
                var _query = (
                                from _prcReturDt in this.db.PRCReturDts
                                where _prcReturDt.TransNmbr == _prmCode
                                orderby _prcReturDt.ProductCode ascending
                                select _prcReturDt
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

        //public PRCReturDt GetSinglePRCReturDt(string _prmCode, string _prmProductCode, string _prmLocationCode)
        //{
        //    PRCReturDt _result = null;

        //    try
        //    {
        //        _result = this.db.PRCReturDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode && _temp.LocationCode == _prmLocationCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public PRCReturDt GetSinglePRCReturDt(string _prmCode, string _prmProductCode)
        {
            PRCReturDt _result = null;

            try
            {
                _result = this.db.PRCReturDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public PRCReturDt GetSinglePRCReturDtForStcRejOut(string _prmCode, string _prmProductCode)
        {
            PRCReturDt _result = null;

            try
            {
                _result = this.db.PRCReturDts.FirstOrDefault(_temp => _temp.TransNmbr == _prmCode && _temp.ProductCode == _prmProductCode && _temp.Qty - _temp.QtySJ - _temp.QtyClose > 0);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPRCReturDt(string[] _prmCode, string _prmTransNo)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;            
            decimal _tempPPNForex = 0;
            decimal _tempTotalForex = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    //PRCReturDt _PRCReturDt = this.db.PRCReturDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.LocationCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.TransNmbr == _prmTransNo.Trim().ToLower());
                    PRCReturDt _PRCReturDt = this.db.PRCReturDts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.TransNmbr == _prmTransNo.Trim().ToLower());
                    this.db.PRCReturDts.DeleteOnSubmit(_PRCReturDt);

                    ////
                    PRCReturHd _prcReturHd = this.GetSinglePRCReturHd(_prmTransNo);
                    _tempBaseForex = _prcReturHd.BaseForex -_PRCReturDt.AmountForex;
                    _tempPPNForex = _tempBaseForex * _prcReturHd.PPN;
                    _tempTotalForex = _tempBaseForex + _tempPPNForex;

                    _prcReturHd.BaseForex = _tempBaseForex;
                    _prcReturHd.PPNForex = _tempPPNForex;
                    _prcReturHd.TotalForex = _tempTotalForex; 
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

        public bool AddPRCReturDt(PRCReturDt _prmPRCReturDt, Decimal _prmAmount)
        {
            bool _result = false;
            decimal _tempBaseAmount = 0;            
            decimal _tempDisc = 0;
            decimal _tempTax = 0;           
            decimal _tempTotalAmount = 0;

            try
            {
                PRCReturHd _prcReturHd = this.GetSinglePRCReturHd(_prmPRCReturDt.TransNmbr);

                _tempBaseAmount = _prcReturHd.BaseForex + Convert.ToDecimal(_prmAmount);
                _tempTax = _tempBaseAmount * _prcReturHd.PPN;

                ////_tempDiscAmount = _directSalesHd.DiscAmount;
                //if (_prcReturHd.DiscPercent == 0 && _prcReturHd.BaseForex == 0)
                //{
                //    _tempDisc = 0;
                //    _tempTax = 0;
                //}
                //else
                //{
                //    _tempDisc = Convert.ToDecimal((_directSalesHd.DiscPercent * (_directSalesHd.BaseForex + _prmDirectSalesDt.Amount)) / 100);
                //    _tempTax = Convert.ToDecimal((_directSalesHd.PPNPercent * ((_directSalesHd.BaseForex + _prmDirectSalesDt.Amount) - _tempDisc)) / 100);
                //}

                _tempTotalAmount = (_tempBaseAmount - _tempDisc) + _tempTax;

                _prcReturHd.BaseForex = _tempBaseAmount;                
                _prcReturHd.PPNForex = _tempTax;
                _prcReturHd.TotalForex = _tempTotalAmount;
                _prcReturHd.EditBy = HttpContext.Current.User.Identity.Name;
                _prcReturHd.EditDate = DateTime.Now;

                this.db.PRCReturDts.InsertOnSubmit(_prmPRCReturDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPRCReturDt(PRCReturDt _prmPRCReturDt)
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

        //#region V_STRRForSI
        ////public decimal GetPPNFromVSTRRForSI(string _prmPONo, string _prmProductCode)
        ////{
        ////    decimal _result = 0;

        ////    try
        ////    {
        ////        var _query = (
        ////                        from _vSTRRForSI in this.db.V_STRRForSIs
        ////                        where _vSTRRForSI.PO_No == _prmPONo && _vSTRRForSI.Product_Code == _prmProductCode
        ////                        select new
        ////                        {
        ////                            PPN = _vSTRRForSI.PPN
        ////                        }
        ////                     );
        ////        foreach (var _row in _query)
        ////        {
        ////            _result = _row.PPN;
        ////        }

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ErrorHandler.Record(ex, EventLogEntryType.Error);
        ////    }

        ////    return _result;
        ////}

        //public List<PRCReturDt> GetListDDLRRNoFromVSTRRForSI()
        //{
        //    List<PRCReturDt> _result = new List<PRCReturDt>();

        //    try
        //    {
        //        var _query = (
        //                        from _vSTRRForSI in this.db.V_STRRForSIs
        //                        select new
        //                        {
        //                            RRNo = _vSTRRForSI.RR_No
        //                        }
        //                     ).Distinct();

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new PRCReturDt(_row.RRNo));
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<MsProduct> GetListDDLProductFromVSTRRForSI(string _prmRRNo)
        //{
        //    List<MsProduct> _result = new List<MsProduct>();

        //    try
        //    {
        //        var _query = (
        //                        from _vSTRRForSI in this.db.V_STRRForSIs
        //                        where _vSTRRForSI.RR_No == _prmRRNo
        //                        orderby _vSTRRForSI.Product_Name
        //                        select new
        //                        {
        //                            ProductCode = _vSTRRForSI.Product_Code,
        //                            ProductName = _vSTRRForSI.Product_Name
        //                        }
        //                     );
        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public string GetPONoFromVSTRRForSI(string _prmRRNo)
        //{
        //    string _result = "";

        //    try
        //    {
        //        var _query = (
        //                        from _vSTRRForSI in this.db.V_STRRForSIs
        //                        where _vSTRRForSI.RR_No == _prmRRNo
        //                        select new
        //                        {
        //                            PONo = _vSTRRForSI.PO_No
        //                        }
        //                     );
        //        foreach (var _row in _query)
        //        {
        //            _result = _row.PONo;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public string GetUnitFromVSTRRForSI(string _prmRRNo, string _prmProductCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        var _query = (
        //                        from _vSTRRForSI in this.db.V_STRRForSIs
        //                        where _vSTRRForSI.RR_No == _prmRRNo && _vSTRRForSI.Product_Code == _prmProductCode
        //                        select new
        //                        {
        //                            Unit = _vSTRRForSI.Unit
        //                        }
        //                     );
        //        foreach (var _row in _query)
        //        {
        //            _result = _row.Unit;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public decimal GetQtyFromVSTRRForSI(string _prmRRNo, string _prmProductCode)
        //{
        //    decimal _result = 0;

        //    try
        //    {
        //        var _query = (
        //                        from _vSTRRForSI in this.db.V_STRRForSIs
        //                        where _vSTRRForSI.RR_No == _prmRRNo && _vSTRRForSI.Product_Code == _prmProductCode
        //                        select new
        //                        {
        //                            Qty = _vSTRRForSI.Qty
        //                        }
        //                     );
        //        foreach (var _row in _query)
        //        {
        //            _result = (_row.Qty == null) ? 0 : Convert.ToDecimal(_row.Qty);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public decimal GetPriceFromVSTRRForSI(string _prmRRNo, string _prmProductCode)
        //{
        //    decimal _result = 0;

        //    try
        //    {
        //        var _query = (
        //                        from _vSTRRForSI in this.db.V_STRRForSIs
        //                        where _vSTRRForSI.RR_No == _prmRRNo && _vSTRRForSI.Product_Code == _prmProductCode
        //                        select new
        //                        {
        //                            Price = _vSTRRForSI.Price
        //                        }
        //                     );
        //        foreach (var _row in _query)
        //        {
        //            _result = (_row.Price == null) ? 0 : Convert.ToDecimal(_row.Price);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}
        //#endregion

        ~PurchaseReturBL()
        {
        }
    }
}
