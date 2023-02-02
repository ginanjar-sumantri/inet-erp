using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Data;

namespace BusinessRule.POSInterface
{
    public sealed class POSRetailBL : Base
    {
        public POSRetailBL()
        {
        }

        #region POSRetail

        public int GetLastPageFloorButton(int _prmPageSize)
        {
            var _query = from _retailHd in this.db.POSTrRetailHds
                         //where _retailHd.fgActive == true
                         select _retailHd;
            if (_query.Count() > 0)
                return Convert.ToInt16(_query.Count() / _prmPageSize);
            else
                return 0;
        }

        public List<POSTrRetailHd> GetList(int _prmReqPage, int _prmPageSize)
        {
            List<POSTrRetailHd> _result = new List<POSTrRetailHd>();
            try
            {
                var _query = (
                                from _retailHd in this.db.POSTrRetailHds
                                select _retailHd
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
                foreach (var _row in _query)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrRetailHd> GetListRetailOnHold(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrRetailHd> _result = new List<POSTrRetailHd>();
            try
            {
                var _query = (
                                from _retailHd in this.db.POSTrRetailHds
                                where SqlMethods.Like((_retailHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_retailHd.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && _retailHd.Status == POSTrRetailDataMapper.GetStatus(POSTrRetailStatus.OnHold)
                                    && _retailHd.IsVOID == false
                                select _retailHd
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

        public List<POSTrRetailHd> GetListRetailSendToCashier(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrRetailHd> _result = new List<POSTrRetailHd>();
            try
            {
                var _query = (
                                from _retailHd in this.db.POSTrRetailHds
                                where SqlMethods.Like((_retailHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_retailHd.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && _retailHd.Status == POSTrRetailDataMapper.GetStatus(POSTrRetailStatus.SendToCashier)
                                    && _retailHd.DoneSettlement == POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet)
                                    && _retailHd.IsVOID == false
                                    && (_retailHd.DeliveryOrderReff == "" || _retailHd.DeliveryOrderReff == null)
                                select _retailHd
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

        public List<POSTrRetailHd> GetListRetailPayNotDelivered(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrRetailHd> _result = new List<POSTrRetailHd>();
            try
            {
                var _query = (
                                from _retailHd in this.db.POSTrRetailHds
                                join _settlementRef in this.db.POSTrSettlementDtRefTransactions
                                    on _retailHd.TransNmbr equals _settlementRef.ReferenceNmbr
                                join _settlement in this.db.POSTrSettlementHds
                                    on _settlementRef.TransNmbr equals _settlement.TransNmbr
                                join _settlementDtProducy in this.db.POSTrSettlementDtProducts
                                    on _retailHd.TransNmbr equals _settlementDtProducy.ReffNmbr
                                    //on _settlement.TransNmbr equals _settlementDtProducy.TransNmbr
                                    into joined
                                from _settlementDtProducy in joined.DefaultIfEmpty()
                                where SqlMethods.Like((_retailHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_settlement.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && SqlMethods.Like((_retailHd.CustName ?? "").Trim().ToLower(), _pattern3.Trim().ToLower())
                                    && ((_settlement.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted) && _settlementDtProducy.FgStock == 'Y') || _retailHd.DPPaid > 0)
                                    && _settlementRef.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Retail)
                                    && _retailHd.IsVOID == false
                                    && ((_retailHd.DeliveryStatus == null || _retailHd.DeliveryStatus == false) ? false : true) == POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered)
                                //&& _settlementDtProducy.FgStock == 'Y'
                                select _retailHd
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

        //public List<V_POSCheckStatusMonitoring> GetMonitoringNotDelivered(String _prmCustID, String _prmSearchBy, String _prmSearchText)
        public List<V_POSCheckStatusMonitoring> GetMonitoringNotDelivered(String _prmSearchBy, String _prmSearchText)
        {
            String _pattern1 = "%";
            String _pattern2 = "%";
            String _pattern3 = "%";
            String _pattern4 = "%";
            String _pattern5 = "%";
            String _pattern6 = "%";
            DateTime _pattern7a = new DateTime();
            DateTime _pattern7b = new DateTime();
            String _pattern8 = "%";
            //if (_prmCustID.Trim() != "")
            //{
            //    _pattern1 = "%" + _prmCustID + "%";
            //}
            //if (_prmSearchBy.Trim() == "JobOrder")
            //{
            //    _pattern2 = "%" + _prmSearchText + "%";
            //}
            //else if (_prmSearchBy.Trim() == "CustName")
            //{
            //    _pattern3 = "%" + _prmSearchText + "%";
            //}

            if (_prmSearchBy.Trim() == "ReferenceNo")
            {
                _pattern1 = "%" + _prmSearchText.Trim().ToLower() + "%";
            }
            else if (_prmSearchBy.Trim() == "SettlementNo")
            {
                _pattern2 = "%" + _prmSearchText.Trim().ToLower() + "%";
            }
            else if (_prmSearchBy.Trim() == "TransNmbr")
            {
                _pattern3 = "%" + _prmSearchText.Trim().ToLower() + "%";
            }
            else if (_prmSearchBy.Trim() == "TransType")
            {
                _pattern4 = "%" + _prmSearchText.Trim().ToLower() + "%";
            }
            else if (_prmSearchBy.Trim() == "DoneSettlement")
            {
                _pattern5 = "%" + _prmSearchText.Trim().ToLower() + "%";
            }
            else if (_prmSearchBy.Trim() == "CustName")
            {
                _pattern6 = "%" + _prmSearchText.Trim().ToLower() + "%";
            }
            else if (_prmSearchBy.Trim() == "TransDate")
            {
                string[] _tempSplit = _prmSearchText.Trim().Split('|');
                DateTime _startDate = Convert.ToDateTime(_tempSplit[0]);
                DateTime _endDate = Convert.ToDateTime(_tempSplit[1]);
                //_pattern7a = DateFormMapper.GetValue(_tempSplit[0]);
                //_pattern7b = DateFormMapper.GetValue(_tempSplit[1]);
                _pattern7a = new DateTime(_startDate.Year, _startDate.Month, _startDate.Day, 0, 0, 0);
                _pattern7b = new DateTime(_endDate.Year, _endDate.Month, _endDate.Day, 23, 59, 59);
            }
            else if (_prmSearchBy.Trim() == "DPPaid")
            {
                _pattern8 = "%" + _prmSearchText.Trim() + "%";
            }

            List<V_POSCheckStatusMonitoring> _result = new List<V_POSCheckStatusMonitoring>();
            try
            {
                if (_prmSearchBy.Trim() == "TransDate")
                {
                    var _query = (
                                    from _posCheckStatusMonitoring in this.db.V_POSCheckStatusMonitorings
                                    where _posCheckStatusMonitoring.TransDate >= _pattern7a
                                        && _posCheckStatusMonitoring.TransDate <= _pattern7b
                                    select _posCheckStatusMonitoring
                                );
                    foreach (var _row in _query)
                        _result.Add(_row);
                }
                else
                {
                    var _query = (
                                    from _posCheckStatusMonitoring in this.db.V_POSCheckStatusMonitorings
                                    where SqlMethods.Like((_posCheckStatusMonitoring.ReferenceNo ?? "").Trim().ToLower(), _pattern1)
                                        && SqlMethods.Like((_posCheckStatusMonitoring.SettlementNo ?? "").Trim().ToLower(), _pattern2)
                                        && SqlMethods.Like((_posCheckStatusMonitoring.TransNmbr ?? "").Trim().ToLower(), _pattern3)
                                        && SqlMethods.Like((_posCheckStatusMonitoring.TransType ?? "").Trim().ToLower(), _pattern4)
                                        && SqlMethods.Like((Convert.ToString(_posCheckStatusMonitoring.DoneSettlement) ?? "").ToLower(), _pattern5)
                                        && SqlMethods.Like((_posCheckStatusMonitoring.CustName ?? "").Trim().ToLower(), _pattern6)
                                        //&& SqlMethods.Like((_posCheckStatusMonitoring.TransDate ?? "").Trim().ToLower(), _pattern7.ToLower())
                                        && SqlMethods.Like((Convert.ToString(_posCheckStatusMonitoring.DPPaid) ?? "").Trim(), _pattern8)
                                    select _posCheckStatusMonitoring
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

        public List<POSTrRetailHd> GetListMonitoring(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrRetailHd> _result = new List<POSTrRetailHd>();
            List<POSTrInternetHd> _result2 = new List<POSTrInternetHd>();
            try
            {
                var _query = (
                                from _retailHd in this.db.POSTrRetailHds
                                join _settlementRef in this.db.POSTrSettlementDtRefTransactions
                                    on _retailHd.TransNmbr equals _settlementRef.ReferenceNmbr
                                join _settlement in this.db.POSTrSettlementHds
                                    on _settlementRef.TransNmbr equals _settlement.TransNmbr
                                where SqlMethods.Like((_retailHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_retailHd.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && _settlement.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted)
                                    && _settlementRef.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Retail)
                                    && _retailHd.IsVOID == false
                                    && ((_retailHd.DeliveryStatus == null || _retailHd.DeliveryStatus == false) ? false : true) == POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered)
                                select _retailHd
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

        public POSTrRetailHd GetSingle(String _prmCode)
        {
            POSTrRetailHd _result = null;
            try
            {
                _result = this.db.POSTrRetailHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String SaveAndHold(POSTrRetailHd _prmPOSTrRetailHd, String _prmDetilTrans)
        {
            String _result = "";
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmPOSTrRetailHd.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    this.db.POSTrRetailHds.InsertOnSubmit(_prmPOSTrRetailHd);

                    var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                    this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                    //this.itemCount.Value + "|" + this.productCode.Text + "|";
                    //this.boughtItems.Value += this.productName.Value + "|" + this.qty.Text + "|" + this.uom.Value + "|";
                    //this.boughtItems.Value += this.price.Value + "|" + this.lineTotal.Value;

                    var _queryRetailDt = (
                                            from _trRetailDt in this.db.POSTrRetailDts
                                            where _trRetailDt.TransNmbr == _prmPOSTrRetailHd.TransNmbr
                                                && _trRetailDt.IsVoid == false
                                            select _trRetailDt
                                         );
                    this.db.POSTrRetailDts.DeleteAllOnSubmit(_queryRetailDt);

                    String[] _detailTransaksi = _prmDetilTrans.Split('^');
                    foreach (String _dataDetil in _detailTransaksi)
                    {
                        String[] _rowData = _dataDetil.Split('|');

                        ProductBL _productBL = new ProductBL();
                        MsProduct _msProduct = _productBL.GetSingleProduct(_rowData[1].ToString());
                        POSTrRetailDt _addTrRetailDt = new POSTrRetailDt();
                        _addTrRetailDt.TransNmbr = _prmPOSTrRetailHd.TransNmbr;
                        _addTrRetailDt.ItemNo = Convert.ToInt32(_rowData[0]);
                        _addTrRetailDt.ProductCode = _rowData[1].ToString();
                        _addTrRetailDt.Qty = Convert.ToInt32(_rowData[3].ToString());
                        _addTrRetailDt.Unit = _msProduct.Unit;
                        _addTrRetailDt.UnitPriceForex = Convert.ToDecimal(_rowData[5].ToString());
                        _addTrRetailDt.AmountForex = Convert.ToDecimal(_rowData[5].ToString());
                        _addTrRetailDt.DiscPercentage = 0;
                        _addTrRetailDt.DiscForex = Convert.ToDecimal(_rowData[4].ToString());
                        _addTrRetailDt.LineTotalForex = Convert.ToDecimal(_rowData[6].ToString());
                        _addTrRetailDt.IsVoid = false;
                        //_addTrRetailDt.Remark = _rowData[3];
                        _addTrRetailDt.CancelQty = 0;

                        this.db.POSTrRetailDts.InsertOnSubmit(_addTrRetailDt);
                    }

                    this.db.SubmitChanges();

                    _result = "";
                    //String _errMessage = "";
                    //this.db.spSAL_POSPosting(_prmPOSTrRetailHd.TransNmbr, HttpContext.Current.User.Identity.Name, _errMessage);

                    _scope.Complete();

                    //_result = _prmPOSTrRetailHd.TransNmbr + "|" + _prmPOSTrRetailHd.FileNmbr + "|" + _errMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String SendToCashier(POSTrRetailHd _prmPOSTrRetailHd, String _prmDetilTrans)
        {
            String _result = "";
            try
            {
                //using (TransactionScope _scope = new TransactionScope())
                //{
                if (_prmPOSTrRetailHd.TransNmbr == "" || _prmPOSTrRetailHd.TransNmbr == null)
                {
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmPOSTrRetailHd.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(((DateTime)_prmPOSTrRetailHd.TransDate).Year, ((DateTime)_prmPOSTrRetailHd.TransDate).Month, AppModule.GetValue(TransactionType.POSRetail), this._companyTag, ""))
                    {
                        _prmPOSTrRetailHd.FileNmbr = item.Number;
                    }

                    this.db.POSTrRetailHds.InsertOnSubmit(_prmPOSTrRetailHd);

                    var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                    this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);
                }

                //this.itemCount.Value + "|" + this.productCode.Text + "|";
                //this.boughtItems.Value += this.productName.Value + "|" + this.qty.Text + "|" + this.uom.Value + "|";
                //this.boughtItems.Value += this.price.Value + "|" + this.lineTotal.Value;

                var _queryDetail = (
                                    from _detalRetail in this.db.POSTrRetailDts
                                    where _detalRetail.TransNmbr.Trim().ToLower() == _prmPOSTrRetailHd.TransNmbr.Trim().ToLower()
                                    select _detalRetail
                                   );

                this.db.POSTrRetailDts.DeleteAllOnSubmit(_queryDetail);

                String[] _detailTransaksi = _prmDetilTrans.Split('^');
                foreach (String _dataDetil in _detailTransaksi)
                {
                    String[] _rowData = _dataDetil.Split('|');

                    MsProduct _msProduct = new ProductBL().GetSingleProduct(_rowData[1].ToString());

                    POSTrRetailDt _addTrRetailDt = new POSTrRetailDt();
                    _addTrRetailDt.TransNmbr = _prmPOSTrRetailHd.TransNmbr;
                    _addTrRetailDt.ItemNo = Convert.ToInt32(_rowData[0]);
                    _addTrRetailDt.ProductCode = _rowData[1].ToString();
                    _addTrRetailDt.Qty = Convert.ToInt32(_rowData[3].ToString());
                    _addTrRetailDt.Unit = _msProduct.Unit;
                    _addTrRetailDt.UnitPriceForex = Convert.ToDecimal(_rowData[5].ToString());
                    _addTrRetailDt.AmountForex = Convert.ToDecimal(_rowData[5].ToString());
                    _addTrRetailDt.DiscPercentage = 0;
                    _addTrRetailDt.DiscForex = Convert.ToDecimal(_rowData[4].ToString());
                    _addTrRetailDt.LineTotalForex = Convert.ToDecimal(_rowData[6].ToString());
                    //_addTrRetailDt.Remark = _rowData[3];
                    _addTrRetailDt.CancelQty = 0;

                    this.db.POSTrRetailDts.InsertOnSubmit(_addTrRetailDt);
                }

                this.db.SubmitChanges();

                //_scope.Complete();
            }

                //String _errMessage = "";
            //this.db.spSAL_POSPosting(_prmPOSTrRetailHd.TransNmbr, HttpContext.Current.User.Identity.Name, _errMessage);
            //_result = _prmPOSTrRetailHd.TransNmbr + "|" + _prmPOSTrRetailHd.FileNmbr + "|" + _errMessage;

            catch (Exception ex)
            {
                _result = ex.Message;
            }
            return _result;
        }

        public String SendToCashierForDeliveryOrder(POSTrRetailHd _prmPOSTrRetailHd, String _prmDetilTrans, POSTrDeliveryOrderRef _prmPOSTrDeliveryOrderRef)
        {
            String _result = "";
            try
            {
                //using (TransactionScope _scope = new TransactionScope())
                //{
                if (_prmPOSTrRetailHd.TransNmbr == "" || _prmPOSTrRetailHd.TransNmbr == null)
                {
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmPOSTrRetailHd.TransNmbr = _item.Number;
                        _prmPOSTrDeliveryOrderRef.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(((DateTime)_prmPOSTrRetailHd.TransDate).Year, ((DateTime)_prmPOSTrRetailHd.TransDate).Month, AppModule.GetValue(TransactionType.POSRetail), this._companyTag, ""))
                    {
                        _prmPOSTrRetailHd.FileNmbr = item.Number;
                    }

                    this.db.POSTrRetailHds.InsertOnSubmit(_prmPOSTrRetailHd);
                    this.db.POSTrDeliveryOrderRefs.InsertOnSubmit(_prmPOSTrDeliveryOrderRef);

                    var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                    this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);
                }

                //this.itemCount.Value + "|" + this.productCode.Text + "|";
                //this.boughtItems.Value += this.productName.Value + "|" + this.qty.Text + "|" + this.uom.Value + "|";
                //this.boughtItems.Value += this.price.Value + "|" + this.lineTotal.Value;

                var _queryDetail = (
                                    from _detalRetail in this.db.POSTrRetailDts
                                    where _detalRetail.TransNmbr.Trim().ToLower() == _prmPOSTrRetailHd.TransNmbr.Trim().ToLower()
                                    select _detalRetail
                                   );

                this.db.POSTrRetailDts.DeleteAllOnSubmit(_queryDetail);

                String[] _detailTransaksi = _prmDetilTrans.Split('^');
                foreach (String _dataDetil in _detailTransaksi)
                {
                    String[] _rowData = _dataDetil.Split('|');

                    MsProduct _msProduct = new ProductBL().GetSingleProduct(_rowData[1].ToString());

                    POSTrRetailDt _addTrRetailDt = new POSTrRetailDt();
                    _addTrRetailDt.TransNmbr = _prmPOSTrRetailHd.TransNmbr;
                    _addTrRetailDt.ItemNo = Convert.ToInt32(_rowData[0]);
                    _addTrRetailDt.ProductCode = _rowData[1].ToString();
                    _addTrRetailDt.Qty = Convert.ToInt32(_rowData[3].ToString());
                    _addTrRetailDt.Unit = _msProduct.Unit;
                    _addTrRetailDt.UnitPriceForex = Convert.ToDecimal(_rowData[5].ToString());
                    _addTrRetailDt.AmountForex = Convert.ToDecimal(_rowData[5].ToString());
                    _addTrRetailDt.DiscPercentage = 0;
                    _addTrRetailDt.DiscForex = Convert.ToDecimal(_rowData[4].ToString());
                    _addTrRetailDt.LineTotalForex = Convert.ToDecimal(_rowData[6].ToString());
                    //_addTrRetailDt.Remark = _rowData[3];
                    _addTrRetailDt.CancelQty = 0;

                    this.db.POSTrRetailDts.InsertOnSubmit(_addTrRetailDt);
                }

                this.db.SubmitChanges();

                //    _scope.Complete();
                //}

                //String _errMessage = "";
                //this.db.spSAL_POSPosting(_prmPOSTrRetailHd.TransNmbr, HttpContext.Current.User.Identity.Name, _errMessage);
                //_result = _prmPOSTrRetailHd.TransNmbr + "|" + _prmPOSTrRetailHd.FileNmbr + "|" + _errMessage;
            }
            catch (Exception ex)
            {
                _result = ex.Message;
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
                //                && _posTrSettlementRefTransac.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Retail)
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
                                && _posTrSettlementRefTransac.TransType.ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).ToLower()
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


        #endregion

        public POSTrRetailDt GetSingleRetailDt(String _prmCode, int _prmItemNo)
        {
            POSTrRetailDt _result = null;
            try
            {
                _result = this.db.POSTrRetailDts.Single(_temp => _temp.TransNmbr == _prmCode && _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrRetailDt> GetListRetailDtByTransNmbr(String _prmTransNmbr)
        {
            List<POSTrRetailDt> _result = new List<POSTrRetailDt>();
            try
            {
                var _query = (
                                from _listRetailDt in this.db.POSTrRetailDts
                                join _msProduct in this.db.MsProducts
                                    on _listRetailDt.ProductCode equals _msProduct.ProductCode
                                where _listRetailDt.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    ItemNo = _listRetailDt.ItemNo,
                                    ProductCode = _listRetailDt.ProductCode,
                                    Remark = (
                                                from _product in this.db.MsProducts
                                                where _product.ProductCode == _listRetailDt.ProductCode
                                                select _product.ProductName
                                              ).FirstOrDefault(),
                                    Qty = _listRetailDt.Qty,
                                    DiscForex = _listRetailDt.DiscForex,
                                    AmountForex = _listRetailDt.AmountForex,
                                    LineTotalForex = _listRetailDt.LineTotalForex
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new POSTrRetailDt(_row.ItemNo, _row.ProductCode, (_row.Qty == null) ? 0 : Convert.ToInt32(_row.Qty), (_row.AmountForex == null) ? 0 : Convert.ToDecimal(_row.AmountForex), (_row.DiscForex == null) ? 0 : Convert.ToDecimal(_row.DiscForex), (_row.LineTotalForex == null) ? 0 : Convert.ToDecimal(_row.LineTotalForex), _row.Remark));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrRetailHd> ListPOSRetailHold(String _prmMemberID, String _prmReferenceNo)
        {
            String _pattern1 = "%%";
            String _pattern2 = "%%";

            if (_prmMemberID.Trim() != "")
            {
                _pattern1 = "%" + _prmMemberID.Trim() + "%";
            }
            if (_prmReferenceNo.Trim() != "")
            {
                _pattern2 = "%" + _prmReferenceNo.Trim() + "%";
            }

            List<POSTrRetailHd> _result = new List<POSTrRetailHd>();

            try
            {
                var _query = (
                            from _posTrRetail in this.db.POSTrRetailHds
                            where SqlMethods.Like((_posTrRetail.MemberID ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                && SqlMethods.Like((_posTrRetail.ReferenceNo ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                            select _posTrRetail
                        );

                foreach (var _row in _query)
                {
                    Char _doneSettlement = ((_row.DoneSettlement == null) ? 'N' : Convert.ToChar(_row.DoneSettlement));
                    _result.Add(new POSTrRetailHd(_row.TransNmbr, _row.FileNmbr, _row.TransType, Convert.ToDateTime(_row.TransDate), _row.ReferenceNo, _row.MemberID, _row.Status, _doneSettlement, _row.Remark, (_row.DeliveryStatus == null) ? false : true));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<POSTrRetailHd> GetListRetailHdForDeliveryOrder(String _prmReferenceNo)
        {
            List<POSTrRetailHd> _result = new List<POSTrRetailHd>();

            try
            {
                var _query = (
                                from _retailHd in this.db.POSTrRetailHds
                                where _retailHd.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                                orderby _retailHd.TransDate descending
                                select _retailHd
                                );
                //foreach (var _row in _query)
                //{
                //    _result.Add(_row);
                //}
                if (_query.Count() > 0)
                    _result.Add(_query.FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean SetVOID(String _prmTransNmbr, Boolean _prmVOIDValue)
        {
            Boolean _result = false;
            POSReasonBL _reasonBL = new POSReasonBL();

            try
            {
                POSTrRetailHd _retailHd = this.db.POSTrRetailHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _retailHd.IsVOID = _prmVOIDValue;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public Boolean SetVOID(String _prmTransNmbr, String _prmReasonCode, Boolean _prmVOIDValue)
        {
            Boolean _result = false;
            POSReasonBL _reasonBL = new POSReasonBL();

            try
            {
                POSTrRetailHd _retailHd = this.db.POSTrRetailHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _retailHd.Remark = _reasonBL.GetReasonByCode(Convert.ToInt32(_prmReasonCode));
                _retailHd.IsVOID = _prmVOIDValue;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public Boolean SetVOIDForDeliveryOrder(String _prmReferenceNo, String _prmReasonCode, Boolean _prmVOIDValue)
        {
            Boolean _result = false;
            POSReasonBL _reasonBL = new POSReasonBL();

            try
            {
                POSTrDeliveryOrder _posTrdeliveryOrder = this.db.POSTrDeliveryOrders.Single(_temp => _temp.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower());

                var _query =
                    (
                       from _retailHd in this.db.POSTrRetailHds
                       where _retailHd.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                       select _retailHd
                    );

                foreach (var _row in _query)
                {
                    _row.Remark = _reasonBL.GetReasonByCode(Convert.ToInt32(_prmReasonCode));
                    _row.IsVOID = _prmVOIDValue;
                }
                _posTrdeliveryOrder.IsVoid = _prmVOIDValue;
                _posTrdeliveryOrder.Reason = Convert.ToInt32(_prmReasonCode);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public Boolean SetDelivery(String _prmTransNmbr, Boolean _prmDeliverValue)
        {
            Boolean _result = false;

            try
            {
                POSTrRetailHd _retailHd = this.db.POSTrRetailHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _retailHd.DeliveryStatus = _prmDeliverValue;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public string GetTransTypeByTransNmbr(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions

                                join _posSettlementHd in this.db.POSTrSettlementHds
                                on _posTrSettlementRefTransac.TransNmbr equals _posSettlementHd.TransNmbr

                                where _posSettlementHd.TransNmbr == _prmCode
                                select _posTrSettlementRefTransac.TransType
                              ).FirstOrDefault();



                _result = _query;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetRefNmbrRetailByTransType(string _prmCode, string _prmTranstype)
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
                                join _posRetailHD in this.db.POSTrRetailHds
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posRetailHD.TransNmbr
                                where _posRetailHD.TransNmbr == RefNmbr
                                select _posRetailHD.ReferenceNo
                                ).FirstOrDefault();

                _result = _query2;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetDeliveredStatusByTransType(string _prmCode, string _prmTranstype)
        {
            Boolean _result = false;

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
                                join _posRefNotYetPay in this.db.V_POSReferenceNotYetPayLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posRefNotYetPay.TransNmbr
                                where _posRefNotYetPay.TransNmbr == RefNmbr
                                && _posRefNotYetPay.TransType == _prmTranstype
                                select _posRefNotYetPay.DeliveryStatus
                                ).FirstOrDefault();

                _result = Convert.ToBoolean(_query2);

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetDivisiByTransType(string _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions

                                where _posTrSettlementRefTransac.TransNmbr == _prmCode
                                select _posTrSettlementRefTransac.TransType
                              ).FirstOrDefault();

                _result = _query;

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
                                join _posRetailHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posRetailHD.TransNmbr
                                where _posRetailHD.TransNmbr == RefNmbr
                                && _posRetailHD.TransType == _prmTranstype
                                select _posRetailHD.CustName
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
            Char _result = ' ';

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
                                join _posRetailHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posRetailHD.TransNmbr
                                where _posRetailHD.TransNmbr == RefNmbr
                                && _posRetailHD.TransType == _prmTranstype
                                select _posRetailHD.DoneSettlement
                                ).FirstOrDefault();

                _result = (_query2 == null) ? 'N' : 'Y';

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetRefNumberTrSettlementRefTransac(string _prmCode, string _prmTranstype)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions

                                where _posTrSettlementRefTransac.TransNmbr == _prmCode
                                && _posTrSettlementRefTransac.TransType == _prmTranstype
                                select _posTrSettlementRefTransac.ReferenceNmbr
                              ).FirstOrDefault();


                _result = _query;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        ~POSRetailBL()
        {
        }
    }
}
