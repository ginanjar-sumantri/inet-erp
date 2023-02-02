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
    public sealed class POSGraphicBL : Base
    {
        public POSGraphicBL()
        {
        }

        ~POSGraphicBL()
        {
        }

        #region POSGraphic

        public POSTrGraphicHd GetSinglePOSTrGraphicHd(String _prmCode)
        {
            POSTrGraphicHd _result = new POSTrGraphicHd();
            try
            {
                _result = this.db.POSTrGraphicHds.FirstOrDefault(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrGraphicDt> GetListGraphicDtByTransNmbr(String _prmTransNmbr)
        {
            List<POSTrGraphicDt> _result = new List<POSTrGraphicDt>();
            try
            {
                var _query = (
                                from _trGraphicDt in this.db.POSTrGraphicDts
                                where _trGraphicDt.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    ProductCode = _trGraphicDt.ProductCode,
                                    Remark = (
                                                from _product in this.db.MsProducts
                                                where _product.ProductCode == _trGraphicDt.ProductCode
                                                select _product.ProductName
                                              ).FirstOrDefault(),
                                    Qty = _trGraphicDt.Qty,
                                    DiscForex = _trGraphicDt.DiscForex,
                                    AmountForex = _trGraphicDt.AmountForex,
                                    LineTotalForex = _trGraphicDt.LineTotalForex
                                }
                            );
                foreach (var _row in _query)
                {
                    _result.Add(new POSTrGraphicDt(_row.ProductCode, (_row.Qty == null) ? 0 : Convert.ToInt32(_row.Qty), (_row.AmountForex == null) ? 0 : Convert.ToDecimal(_row.AmountForex), (_row.DiscForex == null) ? 0 : Convert.ToDecimal(_row.DiscForex), (_row.LineTotalForex == null) ? 0 : Convert.ToDecimal(_row.LineTotalForex), _row.Remark));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrGraphicHd> GetListGraphicSendToCashier(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrGraphicHd> _result = new List<POSTrGraphicHd>();
            try
            {
                var _query = (
                                from _graphicHd in this.db.POSTrGraphicHds
                                where SqlMethods.Like((_graphicHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_graphicHd.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && _graphicHd.Status == POSTrGraphicDataMapper.GetStatus(POSTrGraphicStatus.SendToCashier)
                                    && _graphicHd.DoneSettlement == POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet)
                                    && _graphicHd.IsVOID == false
                                    && (_graphicHd.DeliveryOrderReff == "" || _graphicHd.DeliveryOrderReff == null)
                                select _graphicHd
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

        public List<POSTrGraphicHd> GetListGraphicPayNotDelivered(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrGraphicHd> _result = new List<POSTrGraphicHd>();
            try
            {
                var _query = (
                                from _GraphicHd in this.db.POSTrGraphicHds
                                join _settlementRef in this.db.POSTrSettlementDtRefTransactions
                                    on _GraphicHd.TransNmbr equals _settlementRef.ReferenceNmbr
                                join _settlement in this.db.POSTrSettlementHds
                                    on _settlementRef.TransNmbr equals _settlement.TransNmbr
                                join _settlementDtProducy in this.db.POSTrSettlementDtProducts
                                    on _GraphicHd.TransNmbr equals _settlementDtProducy.ReffNmbr 
                                    //_settlement.TransNmbr equals _settlementDtProducy.TransNmbr
                                into joined
                                from _settlementDtProducy in joined.DefaultIfEmpty()
                                where SqlMethods.Like((_GraphicHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_settlement.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && SqlMethods.Like((_GraphicHd.CustName ?? "").Trim().ToLower(), _pattern3.Trim().ToLower())
                                    && ((_settlement.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted) && _settlementDtProducy.FgStock == 'Y') || _GraphicHd.DPPaid > 0)
                                    && _settlementRef.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic)
                                    && _GraphicHd.IsVOID == false
                                    && ((_GraphicHd.DeliveryStatus == null || _GraphicHd.DeliveryStatus == false) ? false : true) == POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered)
                                //&& _settlementDtProducy.FgStock == 'Y'
                                select _GraphicHd
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

        public List<POSTrGraphicHd> GetListGraphicHdForDeliveryOrder(String _prmReferenceNo)
        {
            List<POSTrGraphicHd> _result = new List<POSTrGraphicHd>();

            try
            {
                var _query = (
                                from _GraphicHd in this.db.POSTrGraphicHds
                                where _GraphicHd.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                                orderby _GraphicHd.TransDate descending
                                select _GraphicHd
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
                POSTrGraphicHd _GraphicHd = this.db.POSTrGraphicHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _GraphicHd.DeliveryStatus = _prmDeliverValue;

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
                POSTrGraphicHd _GraphicHd = this.db.POSTrGraphicHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _GraphicHd.Remark = _reasonBL.GetReasonByCode(Convert.ToInt32(_prmReasonCode));
                _GraphicHd.IsVOID = _prmVOIDValue;

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
                       from _GraphicHd in this.db.POSTrGraphicHds
                       where _GraphicHd.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                       select _GraphicHd
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
                                join _posGraphicHD in this.db.POSTrGraphicHds
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posGraphicHD.TransNmbr
                                where _posGraphicHD.TransNmbr == RefNmbr
                                select _posGraphicHD.ReferenceNo
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
                //                && _posTrSettlementRefTransac.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic)
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
                                && _posTrSettlementRefTransac.TransType.ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic).ToLower()
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
                                where _posMSMenuServiceTypeDt.MenuServiceTypeCode == "GRAPHIC"
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
                                where _posMSMenuServiceTypeDt.MenuServiceTypeCode == "GRAPHIC"
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

        public String SendToCashier(POSTrGraphicHd _prmPOSTrGraphicHd, String _prmDetilTrans)
        {
            String _result = "";
            try
            {
                if (_prmPOSTrGraphicHd.TransNmbr == "" || _prmPOSTrGraphicHd.TransNmbr == null)
                {
                    //using (TransactionScope _scope = new TransactionScope())
                    //{
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmPOSTrGraphicHd.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    this.db.POSTrGraphicHds.InsertOnSubmit(_prmPOSTrGraphicHd);

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

                var _queryDetail = (from _temp1 in this.db.POSTrGraphicDts
                                    where _temp1.TransNmbr == _prmPOSTrGraphicHd.TransNmbr
                                    select _temp1
                                    );

                this.db.POSTrGraphicDts.DeleteAllOnSubmit(_queryDetail);

                String[] _detailTransaksi = _prmDetilTrans.Split('^');
                int _ctrRow = 1;
                foreach (String _dataDetil in _detailTransaksi)
                {
                    String[] _rowData = _dataDetil.Split('|');
                    // prodcode,qty,prodname,sellprice,disc,total,description
                    MsProduct _msProduct = new ProductBL().GetSingleProduct(_rowData[0].ToString());

                    POSTrGraphicDt _addTrGraphicDt = new POSTrGraphicDt();
                    _addTrGraphicDt.TransNmbr = _prmPOSTrGraphicHd.TransNmbr;
                    _addTrGraphicDt.ItemNo = _ctrRow++;
                    _addTrGraphicDt.ProductCode = _rowData[0].ToString();
                    _addTrGraphicDt.Qty = Convert.ToInt32(_rowData[1]);
                    _addTrGraphicDt.Unit = _msProduct.Unit;
                    _addTrGraphicDt.UnitPriceForex = 0;
                    _addTrGraphicDt.AmountForex = Convert.ToDecimal(_rowData[3].ToString());
                    _addTrGraphicDt.DiscPercentage = Convert.ToDecimal(_rowData[4].ToString()) / _addTrGraphicDt.AmountForex * 100;
                    _addTrGraphicDt.DiscForex = Convert.ToDecimal(_rowData[4].ToString());
                    _addTrGraphicDt.LineTotalForex = Convert.ToDecimal(_rowData[5].ToString());
                    _addTrGraphicDt.Remark = _rowData[6];

                    this.db.POSTrGraphicDts.InsertOnSubmit(_addTrGraphicDt);
                }

                this.db.SubmitChanges();

                String _errMessage = "";
                //this.db.spSAL_POSPosting(_prmPOSTrRetailHd.TransNmbr, HttpContext.Current.User.Identity.Name, _errMessage);

                //_scope.Complete();

                _result = _prmPOSTrGraphicHd.TransNmbr + "|" + _prmPOSTrGraphicHd.FileNmbr + "|" + _errMessage;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String SendToCashierForDeliveryOrder(POSTrGraphicHd _prmPOSTrGraphicHd, String _prmDetilTrans, POSTrDeliveryOrderRef _prmPOSTrDeliveryOrderRef)
        {
            String _result = "";
            try
            {
                if (_prmPOSTrGraphicHd.TransNmbr == "" || _prmPOSTrGraphicHd.TransNmbr == null)
                {
                    //using (TransactionScope _scope = new TransactionScope())
                    //{
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmPOSTrGraphicHd.TransNmbr = _item.Number;
                        _prmPOSTrDeliveryOrderRef.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    this.db.POSTrGraphicHds.InsertOnSubmit(_prmPOSTrGraphicHd);
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

                var _queryDetail = (from _temp1 in this.db.POSTrGraphicDts
                                    where _temp1.TransNmbr == _prmPOSTrGraphicHd.TransNmbr
                                    select _temp1
                                    );

                this.db.POSTrGraphicDts.DeleteAllOnSubmit(_queryDetail);

                String[] _detailTransaksi = _prmDetilTrans.Split('^');
                int _ctrRow = 1;
                foreach (String _dataDetil in _detailTransaksi)
                {
                    String[] _rowData = _dataDetil.Split('|');
                    // prodcode,qty,prodname,sellprice,disc,total,description
                    MsProduct _msProduct = new ProductBL().GetSingleProduct(_rowData[0].ToString());

                    POSTrGraphicDt _addTrGraphicDt = new POSTrGraphicDt();
                    _addTrGraphicDt.TransNmbr = _prmPOSTrGraphicHd.TransNmbr;
                    _addTrGraphicDt.ItemNo = _ctrRow++;
                    _addTrGraphicDt.ProductCode = _rowData[0].ToString();
                    _addTrGraphicDt.Qty = Convert.ToInt32(_rowData[1]);
                    _addTrGraphicDt.Unit = _msProduct.Unit;
                    _addTrGraphicDt.UnitPriceForex = 0;
                    _addTrGraphicDt.AmountForex = Convert.ToDecimal(_rowData[3].ToString());
                    _addTrGraphicDt.DiscPercentage = Convert.ToDecimal(_rowData[4].ToString()) / _addTrGraphicDt.AmountForex * 100;
                    _addTrGraphicDt.DiscForex = Convert.ToDecimal(_rowData[4].ToString());
                    _addTrGraphicDt.LineTotalForex = Convert.ToDecimal(_rowData[5].ToString());
                    _addTrGraphicDt.Remark = _rowData[6];

                    this.db.POSTrGraphicDts.InsertOnSubmit(_addTrGraphicDt);
                }

                this.db.SubmitChanges();

                String _errMessage = "";
                //this.db.spSAL_POSPosting(_prmPOSTrRetailHd.TransNmbr, HttpContext.Current.User.Identity.Name, _errMessage);

                //_scope.Complete();

                _result = _prmPOSTrGraphicHd.TransNmbr + "|" + _prmPOSTrGraphicHd.FileNmbr + "|" + _errMessage;
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
                                join _posGraphicHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posGraphicHD.TransNmbr
                                where _posGraphicHD.TransNmbr == RefNmbr
                                && _posGraphicHD.TransType == _prmTranstype
                                select _posGraphicHD.CustName
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
                                join _posGraphicHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posGraphicHD.TransNmbr
                                where _posGraphicHD.TransNmbr == RefNmbr
                                && _posGraphicHD.TransType == _prmTranstype
                                select _posGraphicHD.DoneSettlement
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
