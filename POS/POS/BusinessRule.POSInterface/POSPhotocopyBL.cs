using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.Common;


namespace BusinessRule.POSInterface
{
    public sealed class POSPhotocopyBL : Base
    {
        public POSPhotocopyBL()
        {
        }

        ~POSPhotocopyBL()
        {
        }

        #region POSPhotocopy

        public POSTrPhotocopyHd GetSinglePOSTrPhotocopyHd(String _prmCode)
        {
            POSTrPhotocopyHd _result = new POSTrPhotocopyHd();
            try
            {
                _result = this.db.POSTrPhotocopyHds.FirstOrDefault(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrPhotocopyDt> GetListPhotocopyDtByTransNmbr(String _prmTransNmbr)
        {
            List<POSTrPhotocopyDt> _result = new List<POSTrPhotocopyDt>();
            try
            {
                var _query = (
                                from _trPhotocopyDt in this.db.POSTrPhotocopyDts
                                where _trPhotocopyDt.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    ProductCode = _trPhotocopyDt.ProductCode,
                                    Remark = (
                                                from _product in this.db.MsProducts
                                                where _product.ProductCode == _trPhotocopyDt.ProductCode
                                                select _product.ProductName
                                              ).FirstOrDefault(),
                                    Qty = _trPhotocopyDt.Qty,
                                    DiscForex = _trPhotocopyDt.DiscForex,
                                    AmountForex = _trPhotocopyDt.AmountForex,
                                    LineTotalForex = _trPhotocopyDt.LineTotalForex
                                }
                            );
                foreach (var _row in _query)
                {
                    _result.Add(new POSTrPhotocopyDt(_row.ProductCode, (_row.Qty == null) ? 0 : Convert.ToInt32(_row.Qty), (_row.AmountForex == null) ? 0 : Convert.ToDecimal(_row.AmountForex), (_row.DiscForex == null) ? 0 : Convert.ToDecimal(_row.DiscForex), (_row.LineTotalForex == null) ? 0 : Convert.ToDecimal(_row.LineTotalForex), _row.Remark));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrPhotocopyHd> GetListPhotocopySendToCashier(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrPhotocopyHd> _result = new List<POSTrPhotocopyHd>();
            try
            {
                var _query = (
                                from _photocopyHd in this.db.POSTrPhotocopyHds
                                where SqlMethods.Like((_photocopyHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_photocopyHd.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && _photocopyHd.Status == POSTrPhotocopyDataMapper.GetStatus(POSTrPhotocopyStatus.SendToCashier)
                                    && _photocopyHd.DoneSettlement == POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet)
                                    && _photocopyHd.IsVOID == false
                                    && (_photocopyHd.DeliveryOrderReff == "" || _photocopyHd.DeliveryOrderReff == null)
                                select _photocopyHd
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

        public List<POSTrPhotocopyHd> GetListPhotocopyPayNotDelivered(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrPhotocopyHd> _result = new List<POSTrPhotocopyHd>();
            try
            {
                var _query = (
                                from _PhotocopyHd in this.db.POSTrPhotocopyHds
                                join _settlementRef in this.db.POSTrSettlementDtRefTransactions
                                    on _PhotocopyHd.TransNmbr equals _settlementRef.ReferenceNmbr
                                join _settlement in this.db.POSTrSettlementHds
                                    on _settlementRef.TransNmbr equals _settlement.TransNmbr
                                join _settlementDtProducy in this.db.POSTrSettlementDtProducts
                                    on _PhotocopyHd.TransNmbr equals _settlementDtProducy.ReffNmbr 
                                    //_settlement.TransNmbr equals _settlementDtProducy.TransNmbr
                                into joined
                                from _settlementDtProducy in joined.DefaultIfEmpty()
                                where SqlMethods.Like((_PhotocopyHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_settlement.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && SqlMethods.Like((_PhotocopyHd.CustName ?? "").Trim().ToLower(), _pattern3.Trim().ToLower())
                                    && ((_settlement.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted) && _settlementDtProducy.FgStock == 'Y') || _PhotocopyHd.DPPaid > 0)
                                    && _settlementRef.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy)
                                    && _PhotocopyHd.IsVOID == false
                                    && ((_PhotocopyHd.DeliveryStatus == null || _PhotocopyHd.DeliveryStatus == false) ? false : true) == POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered)
                                //&& _settlementDtProducy.FgStock == 'Y'
                                select _PhotocopyHd
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

        public List<POSTrPhotocopyHd> GetListPhotocopyHdForDeliveryOrder(String _prmReferenceNo)
        {
            List<POSTrPhotocopyHd> _result = new List<POSTrPhotocopyHd>();

            try
            {
                var _query = (
                                from _PhotocopyHd in this.db.POSTrPhotocopyHds
                                where _PhotocopyHd.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                                orderby _PhotocopyHd.TransDate descending
                                select _PhotocopyHd
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

        public Boolean SetDelivery(String _prmTransNmbr, Boolean _prmDeliverValue)
        {
            Boolean _result = false;

            try
            {
                POSTrPhotocopyHd _PhotocopyHd = this.db.POSTrPhotocopyHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _PhotocopyHd.DeliveryStatus = _prmDeliverValue;

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
                POSTrPhotocopyHd _PhotocopyHd = this.db.POSTrPhotocopyHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _PhotocopyHd.Remark = _reasonBL.GetReasonByCode(Convert.ToInt32(_prmReasonCode));
                _PhotocopyHd.IsVOID = _prmVOIDValue;

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
                POSTrDeliveryOrder _posTrDeliveryOrder = this.db.POSTrDeliveryOrders.Single(_temp => _temp.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower());

                var _query =
                    (
                       from _PhotocopyHd in this.db.POSTrPhotocopyHds
                       where _PhotocopyHd.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                       select _PhotocopyHd
                    );

                foreach (var _row in _query)
                {
                    _row.Remark = _reasonBL.GetReasonByCode(Convert.ToInt32(_prmReasonCode));
                    _row.IsVOID = _prmVOIDValue;
                }

                _posTrDeliveryOrder.IsVoid = _prmVOIDValue;
                _posTrDeliveryOrder.Reason = Convert.ToInt32(_prmReasonCode);

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
                                join _posPhotocopyHD in this.db.POSTrPhotocopyHds
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posPhotocopyHD.TransNmbr
                                where _posPhotocopyHD.TransNmbr == RefNmbr
                                select _posPhotocopyHD.ReferenceNo
                                ).FirstOrDefault();

                _result = _query2;

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
                //                && _posTrSettlementRefTransac.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy)
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
                                && _posTrSettlementRefTransac.TransType.ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy).ToLower()
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

        public List<MsProductGroup> GetListProductGroup(int _prmCurrPage, int _prmPageSize)
        {
            List<MsProductGroup> _result = new List<MsProductGroup>();
            try
            {
                var _qryProductGroup = (
                        from _msProductGroup in this.db.MsProductGroups
                        join _msProductSubGroup in this.db.MsProductSubGroups
                        on _msProductGroup.ProductGrpCode equals _msProductSubGroup.ProductGroup
                        where (
                                from _posMSMenuServiceTypeDt in this.db.POSMsMenuServiceTypeDts
                                where _posMSMenuServiceTypeDt.MenuServiceTypeCode == "PHOTOCOPY"
                                select _posMSMenuServiceTypeDt.ProductSubGroup
                            ).Contains(_msProductSubGroup.ProductSubGrpCode)
                        select _msProductGroup
                    ).Distinct().Skip(_prmPageSize * _prmCurrPage).Take(_prmPageSize);
                foreach (var _row in _qryProductGroup)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<MsProductSubGroup> GetListProductSubGroup(String _prmProductGroupCode, int _prmCurrPage, int _prmPageSize)
        {
            List<MsProductSubGroup> _result = new List<MsProductSubGroup>();
            try
            {
                var _qrySubGroup = (
                        from _msProductSubGroup in this.db.MsProductSubGroups
                        where _msProductSubGroup.ProductGroup == _prmProductGroupCode
                            && (
                                from _posMSMenuServiceTypeDt in this.db.POSMsMenuServiceTypeDts
                                where _posMSMenuServiceTypeDt.MenuServiceTypeCode == "PHOTOCOPY"
                                select _posMSMenuServiceTypeDt.ProductSubGroup
                            ).Contains(_msProductSubGroup.ProductSubGrpCode)
                        select _msProductSubGroup
                    ).Distinct().Skip(_prmPageSize * _prmCurrPage).Take(_prmPageSize);
                foreach (var _row in _qrySubGroup)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<MsProduct> GetListProduct(String _prmProductSubGroup, int _prmCurrPage, int _prmPageSize)
        {
            List<MsProduct> _result = new List<MsProduct>();
            try
            {
                var _qryProduct = (
                        from _msProduct in this.db.MsProducts
                        where _msProduct.ProductSubGroup == _prmProductSubGroup
                        select _msProduct
                    ).Distinct().Skip(_prmCurrPage * _prmPageSize).Take(_prmPageSize);
                foreach (var _row in _qryProduct)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public MsProduct GetSingleProduct(String _prmProductCode)
        {
            MsProduct _result = null;
            try
            {
                var _qry = (from _msProduct in this.db.MsProducts
                            where _msProduct.ProductCode == _prmProductCode
                            select _msProduct);
                if (_qry.Count() > 0)
                    _result = this.db.MsProducts.Single(a => a.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public int GetProductGroupMaxPage(int _sizePerPage)
        {
            int _result = 0;
            try
            {
                int _productGroupCount = (
                    from _msProductGroup in this.db.MsProductGroups
                    join _msProductSubGroup in this.db.MsProductSubGroups
                    on _msProductGroup.ProductGrpCode equals _msProductSubGroup.ProductGroup
                    where (
                            from _posMSMenuServiceTypeDt in this.db.POSMsMenuServiceTypeDts
                            where _posMSMenuServiceTypeDt.MenuServiceTypeCode == "INET"
                            select _posMSMenuServiceTypeDt.ProductSubGroup
                        ).Contains(_msProductSubGroup.ProductSubGrpCode)
                    select _msProductGroup
                ).Distinct().Count();
                _result = _productGroupCount / _sizePerPage;
                if ((_productGroupCount % _sizePerPage) == 0) _result--;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public int GetProductSubGroupMaxPage(int _sizePerPage, String _prmProductGroupCode)
        {
            int _result = 0;
            try
            {
                int _subGroupCount = (
                from _msProductSubGroup in this.db.MsProductSubGroups
                where _msProductSubGroup.ProductGroup == _prmProductGroupCode
                    && (
                        from _posMSMenuServiceTypeDt in this.db.POSMsMenuServiceTypeDts
                        where _posMSMenuServiceTypeDt.MenuServiceTypeCode == "INET"
                        select _posMSMenuServiceTypeDt.ProductSubGroup
                    ).Contains(_msProductSubGroup.ProductSubGrpCode)
                select _msProductSubGroup
                ).Distinct().Count();
                _result = _subGroupCount / _sizePerPage;
                if ((_subGroupCount % _sizePerPage) == 0) _result--;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public int GetProductMaxPage(int _sizePerPage, String _prmProductSubGroup)
        {
            int _result = 0;
            try
            {
                int _productCount = (
                        from _msProduct in this.db.MsProducts
                        where _msProduct.ProductSubGroup == _prmProductSubGroup
                        select _msProduct
                    ).Distinct().Count();
                _result = _productCount / _sizePerPage;
                if ((_productCount % _sizePerPage) == 0) _result--;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public string GetMemberName(string _prmMemberCode)
        {
            if (this.db.MsMembers.Where(a => a.MemberCode == _prmMemberCode).Count() > 0)
                return this.db.MsMembers.Single(a => a.MemberCode == _prmMemberCode).MemberName;
            else return "";
        }

        public String SendToCashier(POSTrPhotocopyHd _prmPOSTrPhotocopyHd, String _prmDetilTrans)
        {
            String _result = "";
            try
            {
                if (_prmPOSTrPhotocopyHd.TransNmbr == "" || _prmPOSTrPhotocopyHd.TransNmbr == null)
                {
                    //using (TransactionScope _scope = new TransactionScope())
                    //{
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmPOSTrPhotocopyHd.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    this.db.POSTrPhotocopyHds.InsertOnSubmit(_prmPOSTrPhotocopyHd);

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

                var _queryDetail = (from _temp1 in this.db.POSTrPhotocopyDts
                                    where _temp1.TransNmbr == _prmPOSTrPhotocopyHd.TransNmbr
                                    select _temp1
                                    );

                this.db.POSTrPhotocopyDts.DeleteAllOnSubmit(_queryDetail);

                String[] _detailTransaksi = _prmDetilTrans.Split('^');
                int _ctrRow = 1;
                foreach (String _dataDetil in _detailTransaksi)
                {
                    String[] _rowData = _dataDetil.Split('|');
                    // prodcode,qty,prodname,sellprice,disc,total,description
                    MsProduct _msProduct = new ProductBL().GetSingleProduct(_rowData[0].ToString());

                    POSTrPhotocopyDt _addTrPhotocopyDt = new POSTrPhotocopyDt();
                    _addTrPhotocopyDt.TransNmbr = _prmPOSTrPhotocopyHd.TransNmbr;
                    _addTrPhotocopyDt.ItemNo = _ctrRow++;
                    _addTrPhotocopyDt.ProductCode = _rowData[0].ToString();
                    _addTrPhotocopyDt.Qty = Convert.ToInt32(_rowData[1]);
                    _addTrPhotocopyDt.Unit = _msProduct.Unit;
                    _addTrPhotocopyDt.UnitPriceForex = 0;
                    _addTrPhotocopyDt.AmountForex = Convert.ToDecimal(_rowData[3].ToString());
                    _addTrPhotocopyDt.DiscPercentage = Convert.ToDecimal(_rowData[4].ToString()) / _addTrPhotocopyDt.AmountForex * 100;
                    _addTrPhotocopyDt.DiscForex = Convert.ToDecimal(_rowData[4].ToString());
                    _addTrPhotocopyDt.LineTotalForex = Convert.ToDecimal(_rowData[5].ToString());
                    _addTrPhotocopyDt.Remark = _rowData[6];

                    this.db.POSTrPhotocopyDts.InsertOnSubmit(_addTrPhotocopyDt);
                }

                this.db.SubmitChanges();

                String _errMessage = "";
                //this.db.spSAL_POSPosting(_prmPOSTrRetailHd.TransNmbr, HttpContext.Current.User.Identity.Name, _errMessage);

                //_scope.Complete();

                _result = _prmPOSTrPhotocopyHd.TransNmbr + "|" + _prmPOSTrPhotocopyHd.FileNmbr + "|" + _errMessage;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String SendToCashierForDeliveryOrder(POSTrPhotocopyHd _prmPOSTrPhotocopyHd, String _prmDetilTrans, POSTrDeliveryOrderRef _prmPOSTrDeliveryOrderRef)
        {
            String _result = "";
            try
            {
                if (_prmPOSTrPhotocopyHd.TransNmbr == "" || _prmPOSTrPhotocopyHd.TransNmbr == null)
                {
                    //using (TransactionScope _scope = new TransactionScope())
                    //{
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmPOSTrPhotocopyHd.TransNmbr = _item.Number;
                        _prmPOSTrDeliveryOrderRef.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    this.db.POSTrPhotocopyHds.InsertOnSubmit(_prmPOSTrPhotocopyHd);
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

                var _queryDetail = (from _temp1 in this.db.POSTrPhotocopyDts
                                    where _temp1.TransNmbr == _prmPOSTrPhotocopyHd.TransNmbr
                                    select _temp1
                                    );

                this.db.POSTrPhotocopyDts.DeleteAllOnSubmit(_queryDetail);

                String[] _detailTransaksi = _prmDetilTrans.Split('^');
                int _ctrRow = 1;
                foreach (String _dataDetil in _detailTransaksi)
                {
                    String[] _rowData = _dataDetil.Split('|');
                    // prodcode,qty,prodname,sellprice,disc,total,description
                    MsProduct _msProduct = new ProductBL().GetSingleProduct(_rowData[0].ToString());

                    POSTrPhotocopyDt _addTrPhotocopyDt = new POSTrPhotocopyDt();
                    _addTrPhotocopyDt.TransNmbr = _prmPOSTrPhotocopyHd.TransNmbr;
                    _addTrPhotocopyDt.ItemNo = _ctrRow++;
                    _addTrPhotocopyDt.ProductCode = _rowData[0].ToString();
                    _addTrPhotocopyDt.Qty = Convert.ToInt32(_rowData[1]);
                    _addTrPhotocopyDt.Unit = _msProduct.Unit;
                    _addTrPhotocopyDt.UnitPriceForex = 0;
                    _addTrPhotocopyDt.AmountForex = Convert.ToDecimal(_rowData[3].ToString());
                    _addTrPhotocopyDt.DiscPercentage = Convert.ToDecimal(_rowData[4].ToString()) / _addTrPhotocopyDt.AmountForex * 100;
                    _addTrPhotocopyDt.DiscForex = Convert.ToDecimal(_rowData[4].ToString());
                    _addTrPhotocopyDt.LineTotalForex = Convert.ToDecimal(_rowData[5].ToString());
                    _addTrPhotocopyDt.Remark = _rowData[6];

                    this.db.POSTrPhotocopyDts.InsertOnSubmit(_addTrPhotocopyDt);
                }

                this.db.SubmitChanges();

                String _errMessage = "";
                //this.db.spSAL_POSPosting(_prmPOSTrRetailHd.TransNmbr, HttpContext.Current.User.Identity.Name, _errMessage);

                //_scope.Complete();

                _result = _prmPOSTrPhotocopyHd.TransNmbr + "|" + _prmPOSTrPhotocopyHd.FileNmbr + "|" + _errMessage;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
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
                                join _posPhotocopyHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posPhotocopyHD.TransNmbr
                                where _posPhotocopyHD.TransNmbr == RefNmbr
                                && _posPhotocopyHD.TransType == _prmTranstype
                                select _posPhotocopyHD.CustName
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
                                join _posPhotocopyHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posPhotocopyHD.TransNmbr
                                where _posPhotocopyHD.TransNmbr == RefNmbr
                                && _posPhotocopyHD.TransType == _prmTranstype
                                select _posPhotocopyHD.DoneSettlement
                                ).FirstOrDefault();

                _result = ((_query2 == null)? 'N':'Y');

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion
    }
}
