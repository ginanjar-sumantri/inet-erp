using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using System.Transactions;
using System.Web;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using InetGlobalIndo.ERP.MTJ.Common;

namespace BusinessRule.POSInterface
{
    public sealed class CloseShiftBL : Base
    {
        public CloseShiftBL()
        {
        }

        #region CloseShift

        public List<POSTrSettlementDtPaymentType> GetPayment(String _prmCashierID, DateTime _prmOpenShift, DateTime _prmCloseShift)
        {
            List<POSTrSettlementDtPaymentType> _result = new List<POSTrSettlementDtPaymentType>();

            try
            {
                var _query = (from _posTrSettelmentHd in this.db.POSTrSettlementHds
                              join _posTrSettelmentDtPaymentType in this.db.POSTrSettlementDtPaymentTypes
                                on _posTrSettelmentHd.TransNmbr equals _posTrSettelmentDtPaymentType.TransNmbr
                              where _posTrSettelmentHd.CashierID == _prmCashierID
                              && _posTrSettelmentHd.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted)
                              && (_posTrSettelmentDtPaymentType.FgClose ?? false) != true
                              && _posTrSettelmentHd.TransDate >= _prmOpenShift
                              && _posTrSettelmentHd.TransDate <= _prmCloseShift
                              group _posTrSettelmentDtPaymentType by new { PayType = _posTrSettelmentDtPaymentType.PaymentType } into _dtPaymentType
                              select new
                              {
                                  PaymentAmount = _dtPaymentType.Sum(a => a.PaymentAmount),
                                  PaymentType = _dtPaymentType.Key.PayType.ToString()
                              }
                                  );
                foreach (var _row in _query)
                {
                    _result.Add(new POSTrSettlementDtPaymentType(_row.PaymentType.ToString(), Convert.ToDecimal(_row.PaymentAmount)));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;

        }

        public List<POSTrSettlementDtPaymentType> GetListCloseShift(String _prmCashierID, DateTime _prmOpenShift, DateTime _prmCloseShift)
        {
            List<POSTrSettlementDtPaymentType> _result = new List<POSTrSettlementDtPaymentType>();

            try
            {
                var _query = (from _posDtPaymentType in this.db.POSTrSettlementDtPaymentTypes
                              join _posHd in this.db.POSTrSettlementHds
                                  on _posDtPaymentType.TransNmbr equals _posHd.TransNmbr
                              join _posDtRefTrans in this.db.POSTrSettlementDtRefTransactions
                                  on _posHd.TransNmbr equals _posDtRefTrans.TransNmbr
                              where _posHd.CashierID == _prmCashierID
                                && _posHd.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted)
                                && (_posDtPaymentType.FgClose ?? false) != true
                                && _posHd.TransDate >= _prmOpenShift
                                && _posHd.TransDate <= _prmCloseShift
                              select new
                                  {
                                      TransNo = _posDtPaymentType.TransNmbr,
                                      TransDate = _posHd.TransDate,
                                      /*ReferenceNo = ((_posDtRefTrans.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Retail))?
                                                      (from _posTrRetailHd in this.db.POSTrRetailHds
                                                       where _posTrRetailHd.TransNmbr == _posDtRefTrans.ReferenceNmbr
                                                       select _posTrRetailHd.ReferenceNo
                                                       ).FirstOrDefault()
                                                      :(from _posTrinternetHd in this.db.POSTrInternetHds
                                                       where _posTrinternetHd.TransNmbr == _posDtRefTrans.ReferenceNmbr
                                                       select _posTrinternetHd.ReferenceNo
                                                       ).FirstOrDefault()),*/
                                      PaymentType = _posDtPaymentType.PaymentType,
                                      //Divisi      = _posDtRefTrans.TransType,
                                      AmountTransaction = _posDtPaymentType.PaymentAmount,
                                      CardType = _posDtPaymentType.CardType
                                  }
                            ).Distinct();
                foreach (var _row in _query)
                {
                    _result.Add(new POSTrSettlementDtPaymentType(_row.TransNo, Convert.ToDateTime(_row.TransDate), /*_row.ReferenceNo,*/ _row.PaymentType/*, _row.Divisi*/, Convert.ToDecimal(_row.AmountTransaction), _row.CardType));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public List<POSTrSettlementDtPaymentType> GetListCloseShift(String _prmTransNmbr)
        {
            List<POSTrSettlementDtPaymentType> _result = new List<POSTrSettlementDtPaymentType>();

            try
            {
                var _query = (from _posDtPaymentType in this.db.POSTrSettlementDtPaymentTypes
                              join _posHd in this.db.POSTrSettlementHds
                                  on _posDtPaymentType.TransNmbr equals _posHd.TransNmbr
                              where _posDtPaymentType.TransNmbr == _prmTransNmbr
                                && _posHd.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted)
                              select _posDtPaymentType
                            ).Distinct();
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

        public POSTrSettlementDtPaymentType GetSingle(String _prmTransactionNo, String _prmPaymentType, String _prmCardType)
        {
            POSTrSettlementDtPaymentType _result = new POSTrSettlementDtPaymentType();

            try
            {
                _result = this.db.POSTrSettlementDtPaymentTypes.Single(_temp => _temp.TransNmbr == _prmTransactionNo && _temp.PaymentType == _prmPaymentType && _temp.CardType == _prmCardType);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public bool UpDateCloseShift(String _prmShiftLogCode, String _prmApproveBy)
        {
            bool _result = false;

            try
            {
                //using (TransactionScope _scope = new TransactionScope())
                //{
                //    List<POSTrSettlementDtPaymentType> _posTrSettlementDtPaymentType = this.GetListCloseShift(_prmCashierID);

                //    foreach (var _row in _posTrSettlementDtPaymentType)
                //    {
                //        POSTrSettlementDtPaymentType _posTrSettlementDtPaymentTypeSingle = this.GetSingle(_row.TransNmbr, _row.PaymentType, _row.CardType);
                //        _posTrSettlementDtPaymentTypeSingle.FgClose = true;
                //        //this.db.spPOS_CloseShiftCashier(_prmShiftLogCode,_prmApproveBy);

                //    }
                String _errormsg = "";
                this.db.spPOS_CloseShiftCashier(_prmShiftLogCode, _prmApproveBy, ref _errormsg);
                this.db.SubmitChanges();
                _result = true;
                //    _scope.Complete();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public ReportDataSource ReportCloseShift(String _prmCashierEmployee, String _prmCashierAccount, String _prmCash, String _prmVoucher, String _prmCredit, String _prmDebit)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "SpPOS_RptCloseShift";
                _cmd.Parameters.AddWithValue("@CashierID", HttpContext.Current.User.Identity.Name);
                _cmd.Parameters.AddWithValue("@TransType", POSTransTypeDataMapper.GetTransType(POSTransType.Retail));
                _cmd.Parameters.AddWithValue("@Status", POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted));
                _cmd.Parameters.AddWithValue("@FgClose", true);
                _cmd.Parameters.AddWithValue("@CashierEmployee", _prmCashierEmployee);
                _cmd.Parameters.AddWithValue("@CashierAccount", _prmCashierAccount);
                _cmd.Parameters.AddWithValue("@PaymentTypeCredit", POSPaymentTypeMapper.GetStatusText(POSPaymentType.Kredit));
                _cmd.Parameters.AddWithValue("@PaymentTypeDebit", POSPaymentTypeMapper.GetStatusText(POSPaymentType.Debit));
                _cmd.Parameters.AddWithValue("@Cash", Convert.ToDecimal(_prmCash));
                _cmd.Parameters.AddWithValue("@Voucher", Convert.ToDecimal(_prmVoucher));
                _cmd.Parameters.AddWithValue("@Credit", Convert.ToDecimal(_prmCredit));
                _cmd.Parameters.AddWithValue("@Debit", Convert.ToDecimal(_prmDebit));

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);

                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        //public bool add(POSTrShiftLog _prmPOSTrShiftLog)
        //{
        //    bool _result = false;
        //    try
        //    {
        //        Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
        //        foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
        //        {
        //            _prmPOSTrShiftLog.ShiftLogCode = _item.Number;
        //            _transactionNumber.TempTransNmbr = _item.Number;
        //        }

        //        this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
        //        this.db.POSTrShiftLogs.InsertOnSubmit(_prmPOSTrShiftLog);

        //        var _query = (
        //                    from _temp in this.db.Temporary_TransactionNumbers
        //                    where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
        //                    select _temp
        //                  );

        //        this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }
        //    return _result;

        //}
        #endregion


        #region OpenShift

        //public bool CheckUserApprove(String _prmEmployeeID, Guid _prmUserID)
        //{
        //    bool _result = false;

        //    V_UserApprove _vUserApproves = this.db.V_UserApproves.FirstOrDefault(_temp => _temp.UserId == _prmUserID && _temp.EmployeeId == _prmEmployeeID);
        //    if (_vUserApproves != null)
        //        _result = true;

        //    return _result;
        //}

        public POSTrShiftLog GetSinglePOSTrShiftLog(String _prmCashierEmpNmbr, Guid _prmUserID)
        {
            POSTrShiftLog _result = new POSTrShiftLog();

            try
            {
                _result = this.db.POSTrShiftLogs.FirstOrDefault(_temp => (_temp.UserId == _prmUserID || _temp.CashierEmpNmbr == _prmCashierEmpNmbr) && _temp.CloseShift == null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public bool AddShiftLog(POSTrShiftLog _prmPOSTrShiftLog)
        {
            bool _result = false;
            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmPOSTrShiftLog.ShiftLogCode = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.POSTrShiftLogs.InsertOnSubmit(_prmPOSTrShiftLog);

                var _query = (
                            from _temp in this.db.Temporary_TransactionNumbers
                            where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                            select _temp
                          );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;

        }

        public bool EditShiftLog(POSTrShiftLog _prmPOSTrShiftLog)
        {
            bool _result = false;
            try
            {
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }


        #endregion

        ~CloseShiftBL()
        {
        }


    }
}
