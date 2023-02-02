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
    public sealed class CashierBL : Base
    {
        public CashierBL()
        {
        }

        private CustomerDOBL _customerDOBL = new CustomerDOBL();
        private MenuServiceTypeBL _menuServiceTypeBL = new MenuServiceTypeBL();

        #region CashierBL

        //old//public String SettlementPosting(POSTrSettlementHd _prmPOSTrSettlementHd, String _prmPOSTrSettleRefTransact, String _prmCreditCard, String _prmDebitCard, String _prmVoucher)
        public String SettlementPosting(POSTrSettlementHd _prmPOSTrSettlementHd, String _prmPOSTrSettleRefTransact, String _prmCreditCard, String _prmDebitCard, String _prmVoucher, List<POSTrSettlementDtRefTransaction> _prmListPOSSettlementRefTrans, List<POSTableStatusHistory> _prmListPOSTableHist, String _prmAccount, List<String> _prmListHistoryUpdate, String _newSettleType, bool _prmDO, String _typeRef)
        {
            String _result = "";
            String _errMessage = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmPOSTrSettlementHd.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(((DateTime)_prmPOSTrSettlementHd.TransDate).Year, ((DateTime)_prmPOSTrSettlementHd.TransDate).Month, AppModule.GetValue(TransactionType.POSSettlement), this._companyTag, ""))
                    {
                        _prmPOSTrSettlementHd.FileNmbr = item.Number;
                    }

                    this.db.POSTrSettlementHds.InsertOnSubmit(_prmPOSTrSettlementHd);

                    var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                    this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                    if (_prmPOSTrSettlementHd.TotalCashPayment > 0)
                    {
                        POSTrSettlementDtPaymentType _settlePayType = new POSTrSettlementDtPaymentType();
                        _settlePayType.TransNmbr = _prmPOSTrSettlementHd.TransNmbr;
                        _settlePayType.PaymentType = POSPaymentTypeMapper.GetStatusText(POSPaymentType.Cash);
                        _settlePayType.CardType = "";
                        //if (_prmPOSTrSettlementHd.TotalCashPayment > _prmPOSTrSettlementHd.TotalForex)
                        //{
                        //    _settlePayType.PaymentAmount = _prmPOSTrSettlementHd.TotalCashPayment - (_prmPOSTrSettlementHd.TotalVoucherPayment + _prmPOSTrSettlementHd.TotalDebitCardPayment + _prmPOSTrSettlementHd.TotalCreditCardPayment + _prmPOSTrSettlementHd.ChangeAmountForex);
                        //}
                        //else
                        //{
                        if (_newSettleType == POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.DP))
                        {
                            _settlePayType.PaymentAmount = _prmPOSTrSettlementHd.TotalCashPayment;
                        }
                        else
                        {
                            _settlePayType.PaymentAmount = _prmPOSTrSettlementHd.TotalCashPayment - _prmPOSTrSettlementHd.ChangeAmountForex;
                        }
                        //}
                        _settlePayType.EDCReference = "";
                        _settlePayType.CardNumber = "";

                        //String _account = this.GetAccountCashierByCashierID(_prmPOSTrSettlementHd.CashierID);
                        //if (_account != "")
                        //{
                        //    _settlePayType.Account = _account;
                        //}
                        //else
                        //{
                        //    _settlePayType.Account = new SetupBL().GetSingle("Cash").SetValue;
                        //}
                        _settlePayType.Account = _prmAccount;

                        _settlePayType.FgSubLed = 'N';

                        this.db.POSTrSettlementDtPaymentTypes.InsertOnSubmit(_settlePayType);
                    }
                    if (_prmCreditCard != "")
                    {
                        String[] _creditCard = _prmCreditCard.Split(',');

                        POSTrSettlementDtPaymentType _settlePayType = new POSTrSettlementDtPaymentType();
                        _settlePayType.TransNmbr = _prmPOSTrSettlementHd.TransNmbr;
                        _settlePayType.PaymentType = POSPaymentTypeMapper.GetStatusText(POSPaymentType.Kredit);
                        _settlePayType.CardType = _creditCard[0];
                        _settlePayType.PaymentAmount = _prmPOSTrSettlementHd.TotalCreditCardPayment;
                        _settlePayType.EDCReference = "";
                        _settlePayType.Account = _creditCard[1];
                        _settlePayType.CardNumber = _creditCard[2];
                        _settlePayType.FgSubLed = Convert.ToChar(_creditCard[3]);

                        this.db.POSTrSettlementDtPaymentTypes.InsertOnSubmit(_settlePayType);
                    }
                    if (_prmDebitCard != "")
                    {
                        String[] _debitCard = _prmDebitCard.Split(',');

                        POSTrSettlementDtPaymentType _settlePayType = new POSTrSettlementDtPaymentType();
                        _settlePayType.TransNmbr = _prmPOSTrSettlementHd.TransNmbr;
                        _settlePayType.PaymentType = POSPaymentTypeMapper.GetStatusText(POSPaymentType.Debit);
                        _settlePayType.CardType = _debitCard[0];
                        _settlePayType.PaymentAmount = _prmPOSTrSettlementHd.TotalDebitCardPayment;
                        _settlePayType.EDCReference = "";
                        _settlePayType.CardNumber = _debitCard[3];
                        _settlePayType.Account = _debitCard[1];
                        _settlePayType.FgSubLed = Convert.ToChar(_debitCard[2]);

                        this.db.POSTrSettlementDtPaymentTypes.InsertOnSubmit(_settlePayType);
                    }
                    if (_prmVoucher != "")
                    {
                        POSTrSettlementDtPaymentType _settlePayType = new POSTrSettlementDtPaymentType();
                        _settlePayType.TransNmbr = _prmPOSTrSettlementHd.TransNmbr;
                        _settlePayType.PaymentType = POSPaymentTypeMapper.GetStatusText(POSPaymentType.Voucher);
                        _settlePayType.CardType = "";
                        if (_prmPOSTrSettlementHd.TotalVoucherPayment > _prmPOSTrSettlementHd.TotalForex)
                        {
                            _settlePayType.PaymentAmount = _prmPOSTrSettlementHd.TotalForex;
                        }
                        else
                        {
                            _settlePayType.PaymentAmount = _prmPOSTrSettlementHd.TotalVoucherPayment;
                        }
                        _settlePayType.EDCReference = _prmVoucher;
                        //_settlePayType.CardNumber = _prmDebitCard;
                        _settlePayType.CardNumber = _prmVoucher;
                        _settlePayType.Account = "";
                        _settlePayType.FgSubLed = 'N';

                        this.db.POSTrSettlementDtPaymentTypes.InsertOnSubmit(_settlePayType);
                    }

                    // untuk isi transnmbr settlement (yg dari autonmbr) di list settlement trans ref
                    int _index = 0;
                    foreach (var _item in _prmListPOSSettlementRefTrans)
                    {
                        _prmListPOSSettlementRefTrans[_index].TransNmbr = _prmPOSTrSettlementHd.TransNmbr;
                        _index += 1;
                    }

                    this.db.POSTrSettlementDtRefTransactions.InsertAllOnSubmit(_prmListPOSSettlementRefTrans);
                    //this.db.POSTableStatusHistories.InsertAllOnSubmit(_prmListPOSTableHist);

                    //if (_prmListHistoryUpdate.Count > 0)
                    //{
                    //    foreach (var _row in _prmListHistoryUpdate)
                    //    {
                    //        String[] _dataField = _row.Split(',');

                    //        POSTableStatusHistory _histTable = this.db.POSTableStatusHistories.Single(_temp => _temp.ID == Convert.ToInt32(_dataField[0]));
                    //        _histTable.EndTime = _histTable.EndTime.AddMinutes(Convert.ToInt32(_dataField[1]));
                    //    }
                    //}

                    ////update POSTrPromoItem
                    //String[] _split = _prmPOSTrSettleRefTransact.Split(',');
                    //POSTrPromoItem _checkPOSTrPromoItem = this.db.POSTrPromoItems.FirstOrDefault(_item => _item.ReffNmbr == _split[0]);
                    //if (_checkPOSTrPromoItem != null)
                    //{
                    //    String _tempTransNmbr = "%" + _checkPOSTrPromoItem.TransNmbr + "%";
                    //    if (_tempTransNmbr != "%%")
                    //    {
                    //        var _queryPromo = (
                    //             from _posTrPromoItem in this.db.POSTrPromoItems
                    //             where (SqlMethods.Like(_posTrPromoItem.TransNmbr.Trim().ToLower(), _tempTransNmbr.Trim().ToLower()))
                    //             select _posTrPromoItem
                    //            );
                    //        if (_queryPromo.Count() > 0)
                    //        {
                    //            foreach (var _row in _queryPromo)
                    //            {
                    //                POSTrPromoItem _newPOSTrPromoItem = new POSTrPromoItem();
                    //                _newPOSTrPromoItem.TransNmbr = _prmPOSTrSettlementHd.TransNmbr;
                    //                _newPOSTrPromoItem.TransType = _row.TransType;
                    //                _newPOSTrPromoItem.ReffNmbr = _row.ReffNmbr;
                    //                _newPOSTrPromoItem.ProductCode = _row.ProductCode;
                    //                _newPOSTrPromoItem.FreeProductCode = _row.FreeProductCode;
                    //                _newPOSTrPromoItem.FreeQty = _row.FreeQty;
                    //                _newPOSTrPromoItem.Disc = _row.Disc;
                    //                _newPOSTrPromoItem.WrhsCode = _row.WrhsCode;
                    //                _newPOSTrPromoItem.LocationCode = _row.LocationCode;
                    //                _newPOSTrPromoItem.Unit = _row.Unit;
                    //                _newPOSTrPromoItem.Remark = _row.Remark;
                    //                _newPOSTrPromoItem.FgActive = _row.FgActive;
                    //                _newPOSTrPromoItem.CreatedBy = HttpContext.Current.User.Identity.Name;
                    //                _newPOSTrPromoItem.CreatedDate = _row.CreatedDate;
                    //                _newPOSTrPromoItem.ModifiedBy = HttpContext.Current.User.Identity.Name;
                    //                _newPOSTrPromoItem.ModifiedDate = DateTime.Now;

                    //                this.db.POSTrPromoItems.DeleteOnSubmit(_row);
                    //                this.db.POSTrPromoItems.InsertOnSubmit(_newPOSTrPromoItem);
                    //            }
                    //        }
                    //    }
                    //}

                    ////update discount
                    ////if (_queryPromo.Count() == 0)
                    //this.db.spPOS_UpdateDisconPOSTr();
                    //if (_prmDO == true)
                    //{
                    //    //POSTrDeliveryOrder _posTrDeliveryOrder = this._customerDOBL.GetSingleTrDeliveryOrder(_typeRef.Trim());
                    //    POSTrDeliveryOrder _posTrDeliveryOrder = this.db.POSTrDeliveryOrders.FirstOrDefault(_temp => _temp.ReferenceNo.Trim().ToLower() == _typeRef.Trim().ToLower());
                    //    _posTrDeliveryOrder.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Paid);
                    //    _posTrDeliveryOrder.TransDate = DateTime.Now;

                    //    POSTrDeliveryOrderLog _pOSTrDeliveryOrderLog = new POSTrDeliveryOrderLog();
                    //    _pOSTrDeliveryOrderLog.ReferenceNo = _typeRef;
                    //    _pOSTrDeliveryOrderLog.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Paid);
                    //    _pOSTrDeliveryOrderLog.TransDate = DateTime.Now;
                    //    _pOSTrDeliveryOrderLog.UserName = HttpContext.Current.User.Identity.Name;
                    //    this.db.POSTrDeliveryOrderLogs.InsertOnSubmit(_pOSTrDeliveryOrderLog);
                    //}

                    this.db.SubmitChanges();

                    //String _errMessage = "";
                    //this.db.spPOS_SettlementPost(_prmPOSTrSettlementHd.TransNmbr, HttpContext.Current.User.Identity.Name, ref _errMessage);

                    //if (_errMessage != "") _result = _errMessage;

                    _scope.Complete();
                    //_result = _prmPOSTrSettlementHd.TransNmbr + "|" + _prmPOSTrSettlementHd.FileNmbr + "|" + _errMessage;
                }

                this.db.spPOS_SettlementPost(_prmPOSTrSettlementHd.TransNmbr, HttpContext.Current.User.Identity.Name, ref _errMessage);
                if (_errMessage == "")
                {
                    using (TransactionScope _scope2 = new TransactionScope())
                    {
                        this.db.POSTableStatusHistories.InsertAllOnSubmit(_prmListPOSTableHist);

                        if (_prmListHistoryUpdate.Count > 0)
                        {
                            foreach (var _row in _prmListHistoryUpdate)
                            {
                                String[] _dataField = _row.Split(',');

                                POSTableStatusHistory _histTable = this.db.POSTableStatusHistories.Single(_temp => _temp.ID == Convert.ToInt32(_dataField[0]));
                                _histTable.EndTime = _histTable.EndTime.AddMinutes(Convert.ToInt32(_dataField[1]));
                            }
                        }

                        //update POSTrPromoItem
                        String[] _split = _prmPOSTrSettleRefTransact.Split(',');
                        POSTrPromoItem _checkPOSTrPromoItem = this.db.POSTrPromoItems.FirstOrDefault(_item => _item.ReffNmbr == _split[0]);
                        if (_checkPOSTrPromoItem != null)
                        {
                            String _tempTransNmbr = "%" + _checkPOSTrPromoItem.TransNmbr + "%";
                            if (_tempTransNmbr != "%%")
                            {
                                var _queryPromo = (
                                     from _posTrPromoItem in this.db.POSTrPromoItems
                                     where (SqlMethods.Like(_posTrPromoItem.TransNmbr.Trim().ToLower(), _tempTransNmbr.Trim().ToLower()))
                                     select _posTrPromoItem
                                    );
                                if (_queryPromo.Count() > 0)
                                {
                                    foreach (var _row in _queryPromo)
                                    {
                                        POSTrPromoItem _newPOSTrPromoItem = new POSTrPromoItem();
                                        _newPOSTrPromoItem.TransNmbr = _prmPOSTrSettlementHd.TransNmbr;
                                        _newPOSTrPromoItem.TransType = _row.TransType;
                                        _newPOSTrPromoItem.ReffNmbr = _row.ReffNmbr;
                                        _newPOSTrPromoItem.ProductCode = _row.ProductCode;
                                        _newPOSTrPromoItem.FreeProductCode = _row.FreeProductCode;
                                        _newPOSTrPromoItem.FreeQty = _row.FreeQty;
                                        _newPOSTrPromoItem.Disc = _row.Disc;
                                        _newPOSTrPromoItem.WrhsCode = _row.WrhsCode;
                                        _newPOSTrPromoItem.LocationCode = _row.LocationCode;
                                        _newPOSTrPromoItem.Unit = _row.Unit;
                                        _newPOSTrPromoItem.Remark = _row.Remark;
                                        _newPOSTrPromoItem.FgActive = _row.FgActive;
                                        _newPOSTrPromoItem.CreatedBy = HttpContext.Current.User.Identity.Name;
                                        _newPOSTrPromoItem.CreatedDate = _row.CreatedDate;
                                        _newPOSTrPromoItem.ModifiedBy = HttpContext.Current.User.Identity.Name;
                                        _newPOSTrPromoItem.ModifiedDate = DateTime.Now;

                                        this.db.POSTrPromoItems.DeleteOnSubmit(_row);
                                        this.db.POSTrPromoItems.InsertOnSubmit(_newPOSTrPromoItem);
                                    }
                                }
                            }
                        }

                        //update discount
                        //if (_queryPromo.Count() == 0)
                        this.db.spPOS_UpdateDisconPOSTr();
                        if (_prmDO == true)
                        {
                            //POSTrDeliveryOrder _posTrDeliveryOrder = this._customerDOBL.GetSingleTrDeliveryOrder(_typeRef.Trim());
                            POSTrDeliveryOrder _posTrDeliveryOrder = this.db.POSTrDeliveryOrders.FirstOrDefault(_temp => _temp.ReferenceNo.Trim().ToLower() == _typeRef.Trim().ToLower());
                            _posTrDeliveryOrder.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Paid);
                            _posTrDeliveryOrder.TransDate = DateTime.Now;

                            POSTrDeliveryOrderLog _pOSTrDeliveryOrderLog = new POSTrDeliveryOrderLog();
                            _pOSTrDeliveryOrderLog.ReferenceNo = _typeRef;
                            _pOSTrDeliveryOrderLog.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Paid);
                            _pOSTrDeliveryOrderLog.TransDate = DateTime.Now;
                            _pOSTrDeliveryOrderLog.UserName = HttpContext.Current.User.Identity.Name;
                            this.db.POSTrDeliveryOrderLogs.InsertOnSubmit(_pOSTrDeliveryOrderLog);
                        }

                        this.db.SubmitChanges();
                        _scope2.Complete();
                    }
                }
                else
                    _result = _errMessage;

                // dipindah ke atas
                //String _errMessage = "";
                //this.db.spPOS_SettlementPost(_prmPOSTrSettlementHd.TransNmbr, HttpContext.Current.User.Identity.Name, ref _errMessage);

                //if (_errMessage != "") _result = _errMessage;
            }
            catch (Exception ex)
            {
                if (_errMessage != "")
                    _result = _errMessage;
                else
                    _result = ex.Message;
            }
            return _result;
        }

        public List<V_POSReferenceNotYetPayList> GetListReferenceNotPay(String _prmRefNmbr)
        {
            String _pattern1 = "%%";

            if (_prmRefNmbr != "")
            {
                _pattern1 = "%" + _prmRefNmbr.Trim() + "%";
            }

            List<V_POSReferenceNotYetPayList> _result = new List<V_POSReferenceNotYetPayList>();
            try
            {
                var _query = (
                            from _refList in this.db.V_POSReferenceNotYetPayLists
                            where (SqlMethods.Like((_refList.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                            //&& _refList.DoneSettlement != 'Y'
                            select _refList
                         );

                foreach (var _row in _query)
                {
                    _result.Add(new V_POSReferenceNotYetPayList(_row.TransNmbr, _row.TransType, _row.ReferenceNo, Convert.ToChar(_row.DoneSettlement), _row.MemberID, _row.CustName, _row.CustPhone));
                }
            }
            catch (Exception Ex)
            {
                _result = null;
            }

            return _result;
        }

        public String GetAccountCashierByCashierID(String _prmCashierName)
        {
            String _result = "";
            String _empNumb = "";

            Guid _empID = this.dbMembership.aspnet_Users.Single(_user => _user.UserName == _prmCashierName).UserId;
            var _queryUserEmployee = (
                            from _userEmp in this.dbMembership.User_Employees
                            where _userEmp.UserId.ToString().Trim().ToLower() == _empID.ToString().Trim().ToLower()
                            select _userEmp.EmployeeId
                         );
            if (_queryUserEmployee.Count() > 0)
            {
                _empNumb = _queryUserEmployee.FirstOrDefault();

                var _queryCashierAccount = (
                                from _cashierAccount in new MTJERPManSysDataContext(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name)).POSMsCashierAccounts
                                where _cashierAccount.CashierEmpNmbr.Trim().ToLower() == _empNumb.Trim().ToLower()
                                select _cashierAccount.Account
                              );

                if (_queryCashierAccount.Count() > 0)
                {
                    _result = _queryCashierAccount.FirstOrDefault();
                }
            }

            return _result;
        }

        public V_POSReferenceNotYetPayList GetSingleReferenceNotPay(String _prmTransNmbr)
        {
            V_POSReferenceNotYetPayList _result = new V_POSReferenceNotYetPayList();

            try
            {
                _result = (
                            from _list in this.db.V_POSReferenceNotYetPayLists
                            where _list.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()
                            select _list
                         ).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                _result = null;
            }

            return _result;
        }

        public V_POSReferenceNotYetPayListAll GetSingleReferenceNotPayAll(String _prmTransNmbr, String _prmTransType)
        {
            V_POSReferenceNotYetPayListAll _result = new V_POSReferenceNotYetPayListAll();

            try
            {
                _result = (
                            from _list in this.db.V_POSReferenceNotYetPayListAlls
                            where _list.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()
                            && _list.TransType.Trim().ToLower() == _prmTransType.Trim().ToLower()
                            select _list
                         ).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                _result = null;
            }

            return _result;
        }

        public V_POSReferenceNotYetPayListAll GetFirstReferenceNotPayAll(String _prmTransNmbr)
        {
            V_POSReferenceNotYetPayListAll _result = new V_POSReferenceNotYetPayListAll();

            try
            {
                _result = (
                            from _list in this.db.V_POSReferenceNotYetPayListAlls
                            where _list.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()
                            select _list
                         ).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                _result = null;
            }

            return _result;
        }

        public bool CekFgSendToKitchen(String _prmTransNmbr)
        {
            bool _result = false;
            Int32 _count = 0;
            try
            {
                var _query = (from _posSettlement in this.db.POSTrSettlementHds
                              join _refTrans in this.db.POSTrSettlementDtRefTransactions
                                    on _posSettlement.TransNmbr equals _refTrans.TransNmbr
                              where _posSettlement.TransNmbr == _prmTransNmbr
                              select new
                                  {
                                      _refTrans.ReferenceNmbr,
                                      _refTrans.TransType
                                  }
                              );

                foreach (var _row in _query)
                {
                    if (_row.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Internet))
                    {
                        var _query1 = (from _posInternetDt in this.db.POSTrInternetDts
                                       join _msProduct in this.db.MsProducts
                                            on _posInternetDt.ProductCode equals _msProduct.ProductCode
                                       join _productType in this.db.MsProductTypes
                                            on _msProduct.ProductType equals _productType.ProductTypeCode
                                       where _posInternetDt.TransNmbr == _row.ReferenceNmbr
                                       && _productType.fgSendToKitchen == true
                                       select _productType.ProductTypeCode
                                        ).Count();

                        _count += _query1;
                    }
                    else if (_row.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Retail))
                    {
                        var _query2 = (from _posRetailDt in this.db.POSTrRetailDts
                                       join _msProduct in this.db.MsProducts
                                            on _posRetailDt.ProductCode equals _msProduct.ProductCode
                                       join _productType in this.db.MsProductTypes
                                            on _msProduct.ProductType equals _productType.ProductTypeCode
                                       where _posRetailDt.TransNmbr == _row.ReferenceNmbr
                                       && _productType.fgSendToKitchen == true
                                       select _productType.ProductTypeCode
                                        ).Count();

                        _count += _query2;
                    }
                    else if (_row.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe))
                    {
                        var _query2 = (from _posCafeDt in this.db.POSTrCafeDts
                                       join _msProduct in this.db.MsProducts
                                            on _posCafeDt.ProductCode equals _msProduct.ProductCode
                                       join _productType in this.db.MsProductTypes
                                            on _msProduct.ProductType equals _productType.ProductTypeCode
                                       where _posCafeDt.TransNmbr == _row.ReferenceNmbr
                                       && _productType.fgSendToKitchen == true
                                       select _productType.ProductTypeCode
                                        ).Count();

                        _count += _query2;
                    }
                    else if (_row.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Printing))
                    {
                        var _query2 = (from _posPrintingDt in this.db.POSTrPrintingDts
                                       join _msProduct in this.db.MsProducts
                                            on _posPrintingDt.ProductCode equals _msProduct.ProductCode
                                       join _productType in this.db.MsProductTypes
                                            on _msProduct.ProductType equals _productType.ProductTypeCode
                                       where _posPrintingDt.TransNmbr == _row.ReferenceNmbr
                                       && _productType.fgSendToKitchen == true
                                       select _productType.ProductTypeCode
                                        ).Count();

                        _count += _query2;
                    }
                    else if (_row.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy))
                    {
                        var _query2 = (from _posPhotocopyDt in this.db.POSTrPhotocopyDts
                                       join _msProduct in this.db.MsProducts
                                            on _posPhotocopyDt.ProductCode equals _msProduct.ProductCode
                                       join _productType in this.db.MsProductTypes
                                            on _msProduct.ProductType equals _productType.ProductTypeCode
                                       where _posPhotocopyDt.TransNmbr == _row.ReferenceNmbr
                                       && _productType.fgSendToKitchen == true
                                       select _productType.ProductTypeCode
                                        ).Count();

                        _count += _query2;
                    }
                    else if (_row.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic))
                    {
                        var _query2 = (from _posGraphicDt in this.db.POSTrGraphicDts
                                       join _msProduct in this.db.MsProducts
                                            on _posGraphicDt.ProductCode equals _msProduct.ProductCode
                                       join _productType in this.db.MsProductTypes
                                            on _msProduct.ProductType equals _productType.ProductTypeCode
                                       where _posGraphicDt.TransNmbr == _row.ReferenceNmbr
                                       && _productType.fgSendToKitchen == true
                                       select _productType.ProductTypeCode
                                        ).Count();

                        _count += _query2;
                    }
                }
                //var _query = (from _productType in this.db.MsProductTypes 
                //              where _productType.fgSendToKitchen == true
                //              &&  (from _posSettlement in this.db.POSTrSettlementHds 
                //                  join _refTrans in this.db.POSTrSettlementDtRefTransactions 
                //                        on _posSettlement.TransNmbr equals _refTrans.TransNmbr 
                //                  join _posInternetDt in this.db.POSTrInternetDts 
                //                        on _refTrans.ReferenceNmbr equals _posInternetDt.TransNmbr
                //                  join _msProduct in this.db.MsProducts 
                //                        on _posInternetDt.ProductCode equals _msProduct.ProductCode
                //                   where _posSettlement.TransNmbr == _prmTransNmbr
                //                  select _msProduct.ProductType
                //                  ).Contains(_productType.ProductTypeCode)
                //              select _productType.ProductTypeCode
                //              ).Count();

                if (_count > 0)
                    _result = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public bool CekFgSendToKitchenDO(String _prmReferenceNo)
        {
            bool _result = false;
            Int32 _count = 0;
            try
            {
                var _query = (from _pOSTrDeliveryOrderRef in this.db.POSTrDeliveryOrderRefs
                              where _pOSTrDeliveryOrderRef.ReferenceNo == _prmReferenceNo
                              select new
                              {
                                  _pOSTrDeliveryOrderRef.TransNmbr,
                                  _pOSTrDeliveryOrderRef.TransType
                              }
                              );

                foreach (var _row in _query)
                {
                    if (_row.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Internet))
                    {
                        var _query1 = (from _posInternetDt in this.db.POSTrInternetDts
                                       join _msProduct in this.db.MsProducts
                                            on _posInternetDt.ProductCode equals _msProduct.ProductCode
                                       join _productType in this.db.MsProductTypes
                                            on _msProduct.ProductType equals _productType.ProductTypeCode
                                       where _posInternetDt.TransNmbr == _row.TransNmbr
                                       && _productType.fgSendToKitchen == true
                                       select _productType.ProductTypeCode
                                        ).Count();

                        _count += _query1;
                    }
                    else if (_row.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe))
                    {
                        var _query2 = (from _posCafeDt in this.db.POSTrCafeDts
                                       join _msProduct in this.db.MsProducts
                                            on _posCafeDt.ProductCode equals _msProduct.ProductCode
                                       join _productType in this.db.MsProductTypes
                                            on _msProduct.ProductType equals _productType.ProductTypeCode
                                       where _posCafeDt.TransNmbr == _row.TransNmbr
                                       && _productType.fgSendToKitchen == true
                                       select _productType.ProductTypeCode
                                        ).Count();

                        _count += _query2;
                    }
                    else if (_row.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Retail))
                    {
                        var _query3 = (from _posRetailDt in this.db.POSTrRetailDts
                                       join _msProduct in this.db.MsProducts
                                            on _posRetailDt.ProductCode equals _msProduct.ProductCode
                                       join _productType in this.db.MsProductTypes
                                            on _msProduct.ProductType equals _productType.ProductTypeCode
                                       where _posRetailDt.TransNmbr == _row.TransNmbr
                                       && _productType.fgSendToKitchen == true
                                       select _productType.ProductTypeCode
                                        ).Count();

                        _count += _query3;
                    }
                    else if (_row.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Printing))
                    {
                        var _query3 = (from _posPrintingDt in this.db.POSTrPrintingDts
                                       join _msProduct in this.db.MsProducts
                                            on _posPrintingDt.ProductCode equals _msProduct.ProductCode
                                       join _productType in this.db.MsProductTypes
                                            on _msProduct.ProductType equals _productType.ProductTypeCode
                                       where _posPrintingDt.TransNmbr == _row.TransNmbr
                                       && _productType.fgSendToKitchen == true
                                       select _productType.ProductTypeCode
                                        ).Count();

                        _count += _query3;
                    }
                    else if (_row.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy))
                    {
                        var _query3 = (from _posPhotocopyDt in this.db.POSTrPhotocopyDts
                                       join _msProduct in this.db.MsProducts
                                            on _posPhotocopyDt.ProductCode equals _msProduct.ProductCode
                                       join _productType in this.db.MsProductTypes
                                            on _msProduct.ProductType equals _productType.ProductTypeCode
                                       where _posPhotocopyDt.TransNmbr == _row.TransNmbr
                                       && _productType.fgSendToKitchen == true
                                       select _productType.ProductTypeCode
                                        ).Count();

                        _count += _query3;
                    }
                    else if (_row.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic))
                    {
                        var _query3 = (from _posGraphicDt in this.db.POSTrGraphicDts
                                       join _msProduct in this.db.MsProducts
                                            on _posGraphicDt.ProductCode equals _msProduct.ProductCode
                                       join _productType in this.db.MsProductTypes
                                            on _msProduct.ProductType equals _productType.ProductTypeCode
                                       where _posGraphicDt.TransNmbr == _row.TransNmbr
                                       && _productType.fgSendToKitchen == true
                                       select _productType.ProductTypeCode
                                        ).Count();

                        _count += _query3;
                    }
                }
                if (_count > 0)
                    _result = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public List<POSTrSettlementDtRefTransaction> GetRefTrans(String _prmTransNmbr)
        {
            List<POSTrSettlementDtRefTransaction> _result = new List<POSTrSettlementDtRefTransaction>();

            try
            {
                var _query = (from _refTrans in this.db.POSTrSettlementDtRefTransactions
                              where _refTrans.TransNmbr == _prmTransNmbr
                              select new
                                  {
                                      _refTrans.TransType,
                                      _refTrans.ReferenceNmbr
                                  }
                            );

                foreach (var _row in _query)
                {
                    POSTrSettlementDtRefTransaction _posRefTrans = new POSTrSettlementDtRefTransaction();
                    _posRefTrans.TransNmbr = _prmTransNmbr;
                    _posRefTrans.TransType = _row.TransType;
                    _posRefTrans.ReferenceNmbr = _row.ReferenceNmbr;

                    _result.Add(_posRefTrans);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public ReportDataSource ReportSendToKitchen(String _prmTransNmbr, String _prmKitchenCode)
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
                _cmd.CommandText = "SpPOS_RptSendToKitchen";
                _cmd.Parameters.AddWithValue("@TransNmbr", _prmTransNmbr);
                _cmd.Parameters.AddWithValue("@kitchenCode", _prmKitchenCode);

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

        public ReportDataSource ReportSendToKitchenDO(String _prmReferenceNo, String _prmKitchenCode)
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
                _cmd.CommandText = "SpPOS_RptSendToKitchenDO";
                _cmd.Parameters.AddWithValue("@ReferenceNo", _prmReferenceNo);
                _cmd.Parameters.AddWithValue("@kitchenCode", _prmKitchenCode);

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

        public ReportDataSource ReportSendToCustomer(String _prmTransNmbr, String _prmCompanyAddress)
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
                _cmd.CommandText = "SpPOS_RptSendToCustomer";
                _cmd.Parameters.AddWithValue("@TransNmbr", _prmTransNmbr);
                _cmd.Parameters.AddWithValue("@CompanyAddress", _prmCompanyAddress);

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

        public ReportDataSource ReportSendToCustomerDO(String _prmReferenceNo, String _prmCompanyAddress)
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
                _cmd.CommandText = "SpPOS_RptSendToCustomerDO";
                _cmd.Parameters.AddWithValue("@ReferenceNo", _prmReferenceNo);
                _cmd.Parameters.AddWithValue("@CompanyAddress", _prmCompanyAddress);
                //_cmd.Parameters.AddWithValue("@TransNmbr", _prmTransNmbr);
                //_cmd.Parameters.AddWithValue("@TransType", _prmTransType);

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

        public bool CancelTransaction()
        {
            bool result = false;
            DataTable _dataTable = new DataTable();
            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));
                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "POSCancelTransaction";

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public List<POSTrAllDiscon> GetDiscount(String _prmTransNmbr, DateTime _prmDate, int _prmTypePayment, String _prmDebitCreditCode, Decimal _total, String _prmTransType, String _prmMemberID)
        {
            List<POSTrAllDiscon> result = new List<POSTrAllDiscon>();
            try
            {
                if (_prmMemberID == "")
                {
                    this.db.spPOS_GetAllDiscountNonMember(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total, _prmTransType);
                    var _query = (
                            from _posTrAllDiscon in db.POSTrAllDiscons
                            where _posTrAllDiscon.TransNmbr == _prmTransNmbr
                            && _posTrAllDiscon.TransType == _prmTransType
                            select _posTrAllDiscon
                         );
                    foreach (var _item in _query)
                    {
                        result.Add(_item);
                    }
                    //foreach (spPOS_GetAllDiscountNonMemberResult _item in this.db.spPOS_GetAllDiscountNonMember(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total, _prmTransType))
                    //{
                    //    result.Add(new POSTrAllDiscon(_item.TransNmbr, Convert.ToDateTime(_item.TransDate), _item.TransType, _item.ProductCode, _item.TotalDisconItem, _item.TotalDisconSubtotal, _item.TypeDiscon));
                    //}
                }
                else
                {
                    this.db.spPOS_GetAllDiscountMember(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total, _prmTransType);
                    var _query = (
                            from _posTrAllDiscon in db.POSTrAllDiscons
                            where _posTrAllDiscon.TransNmbr == _prmTransNmbr
                            && _posTrAllDiscon.TransType == _prmTransType
                            select _posTrAllDiscon
                         );
                    foreach (var _item in _query)
                    {
                        result.Add(_item);
                    }
                    //foreach (spPOS_GetAllDiscountMemberResult _item in this.db.spPOS_GetAllDiscountMember(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total, _prmTransType))
                    //{
                    //    result.Add(new POSTrAllDiscon(_item.TransNmbr, Convert.ToDateTime(_item.TransDate), _item.TransType, _item.ProductCode, _item.TotalDisconItem, _item.TotalDisconSubtotal, _item.TypeDiscon));
                    //}
                    if (result.Count() == 0)
                    {
                        this.db.spPOS_GetAllDiscountNonMember(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total, _prmTransType);
                        _query = (
                                from _posTrAllDiscon in db.POSTrAllDiscons
                                where _posTrAllDiscon.TransNmbr == _prmTransNmbr
                                && _posTrAllDiscon.TransType == _prmTransType
                                select _posTrAllDiscon
                             );
                        foreach (var _item in _query)
                        {
                            result.Add(_item);
                        }
                        //foreach (spPOS_GetAllDiscountNonMemberResult _item in this.db.spPOS_GetAllDiscountNonMember(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total, _prmTransType))
                        //{
                        //    result.Add(new POSTrAllDiscon(_item.TransNmbr, Convert.ToDateTime(_item.TransDate), _item.TransType, _item.ProductCode, _item.TotalDisconItem, _item.TotalDisconSubtotal, _item.TypeDiscon));
                        //}
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public List<POSTrPromoItem> GetPromo(String _prmTransNmbr, DateTime _prmDate, int _prmTypePayment, String _prmDebitCreditCode, Decimal _total, String _prmTransType, String _prmMemberID)
        {
            List<POSTrPromoItem> _result = new List<POSTrPromoItem>();

            String _pattern1 = "%" + _prmTransNmbr + "%";
            String _pattern2 = "%" + _prmTransType + "%";

            try
            {
                if (_prmMemberID == "")
                {
                    this.db.spPOS_GetAllPromoNonMember(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total, _prmTransType);

                    var _query = (
                     from _posTrPromoItem in this.db.POSTrPromoItems
                     where (SqlMethods.Like(_posTrPromoItem.ReffNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                     & (SqlMethods.Like(_posTrPromoItem.TransType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                     select _posTrPromoItem
                    );

                    foreach (var _row in _query)
                    {
                        POSTrPromoItem _trPromoItem = _row;
                        _result.Add(_trPromoItem);
                    }
                }
                else
                {
                    this.db.spPOS_GetAllPromoMember(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total, _prmTransType);
                    var _query = (
                         from _posTrPromoItem in this.db.POSTrPromoItems
                         where (SqlMethods.Like(_posTrPromoItem.ReffNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                         & (SqlMethods.Like(_posTrPromoItem.TransType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                         select _posTrPromoItem
                        );

                    if (_query.Count() == 0)
                    {
                        this.db.spPOS_GetAllPromoNonMember(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total, _prmTransType);

                        _query = (
                        from _posTrPromoItem in this.db.POSTrPromoItems
                        where (SqlMethods.Like(_posTrPromoItem.ReffNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        & (SqlMethods.Like(_posTrPromoItem.TransType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                        select _posTrPromoItem
                       );
                    }

                    foreach (var _row in _query)
                    {
                        POSTrPromoItem _trPromoItem = _row;
                        _result.Add(_trPromoItem);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return _result;

        }

        public List<POSTrPromoItem> GetAllPromo(String _prmTransNmbr, DateTime _prmDate, int _prmTypePayment, String _prmDebitCreditCode, Decimal _total)
        {
            List<POSTrPromoItem> _result = new List<POSTrPromoItem>();

            try
            {
                String _tempTransNmbr = "%%";
                //Boolean _cekdisc = false;
                string[] _temp = _prmTransNmbr.Split(',');
                this.db.spPOS_GetAllPromoMemberNew(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total);
                POSTrPromoItem _checkPOSTrPromoItem = this.db.POSTrPromoItems.FirstOrDefault(_item => _item.ReffNmbr == _temp[0]);
                if (_checkPOSTrPromoItem != null)
                {
                    _tempTransNmbr = "%" + _checkPOSTrPromoItem.TransNmbr + "%";
                    if (_tempTransNmbr != "%%")
                    {
                        var _query = (
                             from _posTrPromoItem in this.db.POSTrPromoItems
                             where (SqlMethods.Like(_posTrPromoItem.TransNmbr.Trim().ToLower(), _tempTransNmbr.Trim().ToLower()))
                             && _posTrPromoItem.ReffNmbr == ""
                             && _posTrPromoItem.TransType == ""
                             select _posTrPromoItem
                            );
                        //if (_query.Count() > 0)
                        //    _cekdisc = true;

                        //if (_cekdisc == false)
                        //{
                        //    this.db.spPOS_GetAllPromoNonMemberNew(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total);

                        //    _query = (
                        //           from _posTrPromoItem in this.db.POSTrPromoItems
                        //           where (SqlMethods.Like(_posTrPromoItem.TransNmbr.Trim().ToLower(), _tempTransNmbr.Trim().ToLower()))
                        //           && _posTrPromoItem.ReffNmbr == ""
                        //           && _posTrPromoItem.TransType == ""
                        //           select _posTrPromoItem
                        //          );
                        //}
                        foreach (var _row in _query)
                        {
                            POSTrPromoItem _trPromoItem = _row;
                            _result.Add(_trPromoItem);
                        }
                    }
                }
                else
                {
                    this.db.spPOS_GetAllPromoNonMemberNew(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total);
                    _checkPOSTrPromoItem = this.db.POSTrPromoItems.FirstOrDefault(_item => _item.ReffNmbr == _temp[0]);
                    if (_checkPOSTrPromoItem != null)
                    {
                        _tempTransNmbr = "%" + _checkPOSTrPromoItem.TransNmbr + "%";
                        if (_tempTransNmbr != "%%")
                        {
                            var _query = (
                                   from _posTrPromoItem in this.db.POSTrPromoItems
                                   where (SqlMethods.Like(_posTrPromoItem.TransNmbr.Trim().ToLower(), _tempTransNmbr.Trim().ToLower()))
                                   && _posTrPromoItem.ReffNmbr == ""
                                   && _posTrPromoItem.TransType == ""
                                   select _posTrPromoItem
                                  );
                            foreach (var _row in _query)
                            {
                                POSTrPromoItem _trPromoItem = _row;
                                _result.Add(_trPromoItem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return _result;

        }

        public List<POSTrPromoItem> GetPromoByFgPayment(String _prmTransNmbr, DateTime _prmDate, int _prmTypePayment, String _prmDebitCreditCode, Decimal _total, String _prmTransType, String _prmMemberID)
        {
            List<POSTrPromoItem> _result = new List<POSTrPromoItem>();

            String _pattern1 = "%" + _prmTransNmbr + "%";
            String _pattern2 = "%" + _prmTransType + "%";
            String _pattern3 = "%" + "Promo By FgPayment" + "%";

            try
            {
                if (_prmMemberID == "")
                {
                    this.db.spPOS_GetAllPromoNonMemberByFgPayment(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total, _prmTransType);

                    var _query = (
                     from _posTrPromoItem in this.db.POSTrPromoItems
                     where (SqlMethods.Like(_posTrPromoItem.ReffNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                     & (SqlMethods.Like(_posTrPromoItem.TransType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                     & (SqlMethods.Like(_posTrPromoItem.Remark.Trim().ToLower(), _pattern3.Trim().ToLower()))
                     select _posTrPromoItem
                    );

                    foreach (var _row in _query)
                    {
                        POSTrPromoItem _trPromoItem = _row;
                        _result.Add(_trPromoItem);
                    }
                }
                else
                {
                    this.db.spPOS_GetAllPromoMemberByFgPayment(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total, _prmTransType);
                    var _query = (
                         from _posTrPromoItem in this.db.POSTrPromoItems
                         where (SqlMethods.Like(_posTrPromoItem.ReffNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                         & (SqlMethods.Like(_posTrPromoItem.TransType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                         & (SqlMethods.Like(_posTrPromoItem.Remark.Trim().ToLower(), _pattern3.Trim().ToLower()))
                         select _posTrPromoItem
                        );

                    if (_query.Count() == 0)
                    {
                        this.db.spPOS_GetAllPromoNonMemberByFgPayment(_prmTransNmbr, _prmDate, _prmTypePayment, _prmDebitCreditCode, _total, _prmTransType);

                        _query = (
                        from _posTrPromoItem in this.db.POSTrPromoItems
                        where (SqlMethods.Like(_posTrPromoItem.ReffNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        & (SqlMethods.Like(_posTrPromoItem.TransType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                        & (SqlMethods.Like(_posTrPromoItem.Remark.Trim().ToLower(), _pattern3.Trim().ToLower()))
                        select _posTrPromoItem
                       );
                    }

                    foreach (var _row in _query)
                    {
                        POSTrPromoItem _trPromoItem = _row;
                        _result.Add(_trPromoItem);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return _result;

        }

        public List<V_POSReferenceNotYetPayList> GetListPOSReferenceNotYetPayList(String _prmDate)
        {
            String _pattern1 = "%%";

            if (_prmDate != "")
            {
                _pattern1 = "%" + _prmDate.Trim() + "%";
            }

            List<V_POSReferenceNotYetPayList> _result = new List<V_POSReferenceNotYetPayList>();
            try
            {
                var _query = (
                            from _refList in this.db.V_POSReferenceNotYetPayLists
                            where (SqlMethods.Like((_refList.date ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                            select _refList
                         );

                foreach (var _row in _query)
                    _result.Add(_row);
            }
            catch (Exception Ex)
            {
                _result = null;
            }

            return _result;
        }

        public List<V_POSReferenceNotYetPayList> GetPOSReferenceNotYetPayList(String _prmSearchBy, String _prmSearchText)
        {
            String _pattern1 = "%";
            DateTime _pattern2a = new DateTime();
            DateTime _pattern2b = new DateTime();
            String _pattern3 = "%";
            String _pattern4 = "%";
            String _pattern5 = "%";
            String _pattern6 = "%";

            if (_prmSearchBy.Trim() == "TransNmbr")
                _pattern1 = "%" + _prmSearchText.Trim().ToLower() + "%";
            else if (_prmSearchBy.Trim() == "TransDate")
            {
                string[] _tempSplit = _prmSearchText.Split('|');
                DateTime _startDate = Convert.ToDateTime(_tempSplit[0]);
                DateTime _endDate = Convert.ToDateTime(_tempSplit[1]);
                _pattern2a = new DateTime(_startDate.Year, _startDate.Month, _startDate.Day, 0, 0, 0);
                _pattern2b = new DateTime(_endDate.Year, _endDate.Month, _endDate.Day, 23, 59, 59);
            }
            else if (_prmSearchBy.Trim() == "TransType")
                _pattern3 = "%" + _prmSearchText.Trim().ToLower() + "%";
            else if (_prmSearchBy.Trim() == "ReferenceNo")
                _pattern4 = "%" + _prmSearchText.Trim().ToLower() + "%";
            else if (_prmSearchBy.Trim() == "MemberID")
                _pattern5 = "%" + _prmSearchText.Trim().ToLower() + "%";
            else if (_prmSearchBy.Trim() == "CustName")
                _pattern6 = "%" + _prmSearchText.Trim().ToLower() + "%";

            List<V_POSReferenceNotYetPayList> _result = new List<V_POSReferenceNotYetPayList>();
            try
            {
                if (_prmSearchBy == "TransDate")
                {
                    var _query = (
                                from _vPOSReferenceNotYetPayList in this.db.V_POSReferenceNotYetPayLists
                                where Convert.ToDateTime(_vPOSReferenceNotYetPayList.date) >= _pattern2a
                                && Convert.ToDateTime(_vPOSReferenceNotYetPayList.date) <= _pattern2b
                                select _vPOSReferenceNotYetPayList
                            );
                    foreach (var _row in _query)
                        _result.Add(_row);
                }
                else
                {
                    var _query = (
                                    from _vPOSReferenceNotYetPayList in this.db.V_POSReferenceNotYetPayLists
                                    where SqlMethods.Like((_vPOSReferenceNotYetPayList.TransNmbr ?? "").Trim().ToLower(), _pattern1)
                                        && SqlMethods.Like((_vPOSReferenceNotYetPayList.TransType ?? "").Trim().ToLower(), _pattern3)
                                        && SqlMethods.Like((_vPOSReferenceNotYetPayList.ReferenceNo ?? "").Trim().ToLower(), _pattern4)
                                        && SqlMethods.Like((_vPOSReferenceNotYetPayList.MemberID ?? "").Trim().ToLower(), _pattern5)
                                        && SqlMethods.Like((_vPOSReferenceNotYetPayList.CustName ?? "").Trim().ToLower(), _pattern6)
                                    select _vPOSReferenceNotYetPayList
                                );
                    foreach (var _row in _query)
                        _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrSettlementHd> GetListPOSTrSettlementHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSTrSettlementHd> _result = new List<POSTrSettlementHd>();

            string _pattern1 = "%%";


            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _trSettlemetHD in this.db.POSTrSettlementHds
                                where (SqlMethods.Like(_trSettlemetHD.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _trSettlemetHD.TransDate descending
                                select _trSettlemetHD
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    POSTrSettlementHd _posTrSettlementHd = _row;
                    _result.Add(_posTrSettlementHd);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSTrSettlementHd> GetListPaidPOSTrSettlementHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSTrSettlementHd> _result = new List<POSTrSettlementHd>();

            string _pattern1 = "%%";


            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _trSettlemetHD in this.db.POSTrSettlementHds
                                where (SqlMethods.Like(_trSettlemetHD.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && _trSettlemetHD.Status == 2
                                orderby _trSettlemetHD.TransDate descending
                                select _trSettlemetHD
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    POSTrSettlementHd _posTrSettlementHd = _row;
                    _result.Add(_posTrSettlementHd);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        
        public List<POSTrSettlementDtProduct> GetListPOSTrSettlementDtProduct(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSTrSettlementDtProduct> _result = new List<POSTrSettlementDtProduct>();

            string _pattern1 = "%%";


            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _trSettlemetDt in this.db.POSTrSettlementDtProducts
                                where (SqlMethods.Like(_trSettlemetDt.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                select _trSettlemetDt
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    POSTrSettlementDtProduct _POSTrSettlementDtProduct = _row;
                    _result.Add(_POSTrSettlementDtProduct);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public ReportDataSource CashierPrintPreview(DateTime _prmStartDate, DateTime _prmEndDate)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();
                SqlCommand _cmd2 = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spPOS_RptSendToCashier";
                _cmd.Parameters.AddWithValue("@BeginDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);

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

        public ReportDataSource CheckStatusPrintPreview(DateTime _prmStartDate, DateTime _prmEndDate, int _prmFgActive)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();
                SqlCommand _cmd2 = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spPOS_RptCheckStatus";
                _cmd.Parameters.AddWithValue("@BeginDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@EndDate", _prmEndDate);
                _cmd.Parameters.AddWithValue("@FgReport ", _prmFgActive);

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

        public Decimal? GetProductQty(String _prmTransNmbr, String _prmProductCode)
        {
            Decimal? _result = 0;
            try
            {
                var _query = (
                               from _vPOSTrDetailAll in this.db.V_POSTrDetailAlls
                               where (SqlMethods.Like(_vPOSTrDetailAll.TransNmbr.Trim().ToLower(), _prmTransNmbr.Trim().ToLower()))
                               && (SqlMethods.Like(_vPOSTrDetailAll.ProductCode.Trim().ToLower(), _prmProductCode.Trim().ToLower()))
                               select _vPOSTrDetailAll
                           );

                foreach (var _row in _query)
                {
                    _result += _row.Qty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Decimal GetTotalDO(String _prmReferenceNo)
        {
            Decimal _result = 0;
            try
            {
                var _query = (
                                from _vPOSTrDetailAll in this.db.V_POSTrDetailAlls
                                where (SqlMethods.Like(_vPOSTrDetailAll.ReferenceNo.Trim().ToLower(), _prmReferenceNo.Trim().ToLower()))
                                group _vPOSTrDetailAll by _vPOSTrDetailAll.ReferenceNo into g
                                select new
                                {
                                    TotalForex = g.Sum(p => p.TotalForex)
                                }
                            );
                foreach (var _row in _query)
                {
                    _result = Convert.ToDecimal(_row.TotalForex);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSMsKitchen> GetPrinterKitchen(String _prmTransNmbr)
        {
            List<POSMsKitchen> _result = null;
            try
            {
                var _query = (
                                from _posTrSettlementDtProduct in this.db.POSTrSettlementDtProducts
                                join _msProduct in this.db.MsProducts
                                    on _posTrSettlementDtProduct.ProductCode equals _msProduct.ProductCode
                                join _posMsKitchenDt in this.db.POSMsKitchenDts
                                    on _msProduct.ProductType equals _posMsKitchenDt.ProductTypeCode
                                join _posMsKitchen in this.db.POSMsKitchens
                                      on _posMsKitchenDt.KitchenCode equals _posMsKitchen.KitchenCode
                                where (SqlMethods.Like(_posTrSettlementDtProduct.TransNmbr.Trim().ToLower(), _prmTransNmbr.Trim().ToLower()))
                                && _msProduct.FgActive == 'Y'
                                && _posMsKitchenDt.FgActive == 'Y'
                                select _posMsKitchen
                            ).Distinct();

                if (_query.Count() > 0)
                    _result = new List<POSMsKitchen>();
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

        public List<POSMsKitchen> GetPrinterKitchenDO(String _prmReferenceNo)
        {
            List<POSMsKitchen> _result = null;
            try
            {
                var _query = (
                                from _vPOSTrDetailAll in this.db.V_POSTrDetailAlls
                                join _msProduct in this.db.MsProducts
                                    on _vPOSTrDetailAll.ProductCode equals _msProduct.ProductCode
                                join _posMsKitchenDt in this.db.POSMsKitchenDts
                                    on _msProduct.ProductType equals _posMsKitchenDt.ProductTypeCode
                                join _posMsKitchen in this.db.POSMsKitchens
                                      on _posMsKitchenDt.KitchenCode equals _posMsKitchen.KitchenCode
                                where (SqlMethods.Like(_vPOSTrDetailAll.ReferenceNo.Trim().ToLower(), _prmReferenceNo.Trim().ToLower()))
                                && _msProduct.FgActive == 'Y'
                                && _posMsKitchenDt.FgActive == 'Y'
                                select _posMsKitchen
                            ).Distinct();

                if (_query.Count() > 0)
                    _result = new List<POSMsKitchen>();

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

        public POSMsCashierPrinter GetDefaultPrinter(String _prmHostIP)
        {
            POSMsCashierPrinter _result = null;
            try
            {
                //var _query = (
                //                from _posMsCashierPrinter in this.db.POSMsCashierPrinters
                //                where _posMsCashierPrinter.HostName == _prmHostName
                //                select _posMsCashierPrinter
                //              );

                //foreach (var _item in _query)
                //{
                //    _result = _item;
                //}
                _result = this.db.POSMsCashierPrinters.FirstOrDefault(_temp => _temp.IPAddress == _prmHostIP);
                if (_result == null)
                    _result = this.db.POSMsCashierPrinters.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region CloseShift

        public List<POSTrSettlementDtPaymentType> GetPayment(String _prmCashierID)
        {
            List<POSTrSettlementDtPaymentType> _result = new List<POSTrSettlementDtPaymentType>();

            try
            {
                var _query = (from _posTrSettelmentHd in this.db.POSTrSettlementHds
                              join _posTrSettelmentDtPaymentType in this.db.POSTrSettlementDtPaymentTypes
                                on _posTrSettelmentHd.TransNmbr equals _posTrSettelmentDtPaymentType.TransNmbr
                              where _posTrSettelmentHd.CashierID == _prmCashierID
                              && _posTrSettelmentHd.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted)
                              group _posTrSettelmentDtPaymentType by _posTrSettelmentDtPaymentType.PaymentType into _dtPaymentType
                              select new
                              {
                                  PaymentType = _dtPaymentType.GroupBy(a => a.PaymentType),
                                  PaymentAmount = _dtPaymentType.Sum(a => a.PaymentAmount)
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

        #endregion

        ~CashierBL()
        {
        }
    }
}
