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
    public sealed class POSShippingBL : Base
    {
        public POSShippingBL()
        {
        }

        ~POSShippingBL()
        {
        }

        #region POSShipping

        public POSTrShippingHd GetSinglePOSTrShippingHd(String _prmCode)
        {
            POSTrShippingHd _result = new POSTrShippingHd();
            try
            {
                _result = this.db.POSTrShippingHds.FirstOrDefault(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public POSTrShippingDt GetSinglePOSTrShippingDt(String _prmCode, Int32 _prmItemNo)
        {
            POSTrShippingDt _result = new POSTrShippingDt();
            try
            {
                _result = this.db.POSTrShippingDts.FirstOrDefault(_temp => _temp.TransNmbr == _prmCode & _temp.ItemNo == _prmItemNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrShippingHd> GetListShippingSendToCashier(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrShippingHd> _result = new List<POSTrShippingHd>();
            try
            {
                var _query = (
                                from _shippingHd in this.db.POSTrShippingHds
                                where SqlMethods.Like((_shippingHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_shippingHd.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && _shippingHd.Status == POSTrShippingDataMapper.GetStatus(POSTrShippingStatus.SendToCashier)
                                    && _shippingHd.DoneSettlement == POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet)
                                    && _shippingHd.IsVOID == false
                                //&& (_shippingHd.DeliveryOrderReff == "" || _shippingHd.DeliveryOrderReff == null)
                                select _shippingHd
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

        public List<POSTrShippingHd> GetListShippingPayNotDelivered(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrShippingHd> _result = new List<POSTrShippingHd>();
            try
            {
                var _query = (
                                from _shippingHd in this.db.POSTrShippingHds
                                join _settlementRef in this.db.POSTrSettlementDtRefTransactions
                                    on _shippingHd.TransNmbr equals _settlementRef.ReferenceNmbr
                                join _settlement in this.db.POSTrSettlementHds
                                    on _settlementRef.TransNmbr equals _settlement.TransNmbr
                                join _settlementDtProducy in this.db.POSTrSettlementDtProducts
                                    on _shippingHd.TransNmbr equals _settlementDtProducy.ReffNmbr
                                into joined
                                from _settlementDtProducy in joined.DefaultIfEmpty()
                                where SqlMethods.Like((_shippingHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_settlement.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && SqlMethods.Like((_shippingHd.CustName ?? "").Trim().ToLower(), _pattern3.Trim().ToLower())
                                    && ((_settlement.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted)) || _shippingHd.DPPaid > 0) //&& _settlementDtProducy.FgStock == 'Y'
                                    && _settlementRef.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Shipping)
                                    && _shippingHd.IsVOID == false
                                //&& ((_shippingHd.DeliveryStatus == null || _shippingHd.DeliveryStatus == false) ? false : true) == POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered)
                                select _shippingHd
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

        //public List<POSTrShippingHd> GetListShippingHdForDeliveryOrder(String _prmReferenceNo)
        //{
        //    List<POSTrShippingHd> _result = new List<POSTrShippingHd>();

        //    try
        //    {
        //        var _query = (
        //                        from _shippingHd in this.db.POSTrShippingHds
        //                        where _shippingHd.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
        //                        orderby _shippingHd.TransDate descending
        //                        select _shippingHd
        //                        );
        //        if (_query.Count() > 0)
        //            _result.Add(_query.FirstOrDefault());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return _result;
        //}

        public List<POSTrShippingDt> GetListShippingDtByTransNmbr(String _prmTransNmbr)
        {
            List<POSTrShippingDt> _result = null;
            try
            {
                var _query = (
                                from _trShippingDt in this.db.POSTrShippingDts
                                where _trShippingDt.TransNmbr == _prmTransNmbr
                                select _trShippingDt
                            );
                if (_query.Count() > 0)
                    _result = new List<POSTrShippingDt>();
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

        public Boolean SetDelivery(String _prmTransNmbr, Boolean _prmDeliverValue)
        {
            Boolean _result = false;

            try
            {
                POSTrShippingHd _shippingHd = this.db.POSTrShippingHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _shippingHd.DeliveryStatus = _prmDeliverValue;

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
                POSTrShippingHd _shippingHd = this.db.POSTrShippingHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _shippingHd.Remark = _reasonBL.GetReasonByCode(Convert.ToInt32(_prmReasonCode));
                _shippingHd.IsVOID = _prmVOIDValue;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public Boolean DeletePOSTrShippingDt(String _prmTransNmbr, Int32 _prmItemNo)
        {
            Boolean _result = false;

            try
            {
                POSTrShippingDt _posTrShippingDt = this.db.POSTrShippingDts.FirstOrDefault(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() & _temp.ItemNo == _prmItemNo);
                this.db.POSTrShippingDts.DeleteOnSubmit(_posTrShippingDt);
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
                                join _posShippingHD in this.db.POSTrShippingHds
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posShippingHD.TransNmbr
                                where _posShippingHD.TransNmbr == RefNmbr
                                select _posShippingHD.ReferenceNo
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
                //                && _posTrSettlementRefTransac.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Shipping)
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
                                && _posTrSettlementRefTransac.TransType.ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Shipping).ToLower()
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

        public string GetMemberName(string _prmMemberCode)
        {
            if (this.db.MsMembers.Where(a => a.MemberCode == _prmMemberCode).Count() > 0)
                return this.db.MsMembers.Single(a => a.MemberCode == _prmMemberCode).MemberName;
            else return "";
        }

        public Boolean SendToCashier(String _prmTransNmbr)
        {
            Boolean _result = false;
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    POSTrShippingHd _posTrShippingHd = this.GetSinglePOSTrShippingHd(_prmTransNmbr);
                    foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_posTrShippingHd.TransDate).Year, Convert.ToDateTime(_posTrShippingHd.TransDate).Month, POSTransTypeDataMapper.GetTransType(POSTransType.Shipping), this._companyTag, ""))
                    {
                        _posTrShippingHd.FileNmbr = item.Number;
                    }

                    _result = true;

                    this.db.SubmitChanges();

                    _scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        //public String SendToCashierForDeliveryOrder(POSTrShippingHd _prmPOSTrShippingHd, String _prmDetilTrans, POSTrDeliveryOrderRef _prmPOSTrDeliveryOrderRef)
        //{
        //    String _result = "";
        //    try
        //    {
        //        if (_prmPOSTrShippingHd.TransNmbr == "" || _prmPOSTrShippingHd.TransNmbr == null)
        //        {
        //            Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
        //            foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
        //            {
        //                _prmPOSTrShippingHd.TransNmbr = _item.Number;
        //                _prmPOSTrDeliveryOrderRef.TransNmbr = _item.Number;
        //                _transactionNumber.TempTransNmbr = _item.Number;
        //            }
        //            this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

        //            this.db.POSTrShippingHds.InsertOnSubmit(_prmPOSTrShippingHd);
        //            this.db.POSTrDeliveryOrderRefs.InsertOnSubmit(_prmPOSTrDeliveryOrderRef);

        //            var _query = (
        //                        from _temp in this.db.Temporary_TransactionNumbers
        //                        where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
        //                        select _temp
        //                      );

        //            this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);
        //        }

        //        var _queryDetail = (from _temp1 in this.db.POSTrShippingDts
        //                            where _temp1.TransNmbr == _prmPOSTrShippingHd.TransNmbr
        //                            select _temp1
        //                            );

        //        this.db.POSTrShippingDts.DeleteAllOnSubmit(_queryDetail);

        //        String[] _detailTransaksi = _prmDetilTrans.Split('^');
        //        int _ctrRow = 1;
        //        foreach (String _dataDetil in _detailTransaksi)
        //        {
        //            String[] _rowData = _dataDetil.Split('|');
        //            POSTrShippingDt _addTrShippingDt = new POSTrShippingDt();
        //            _addTrShippingDt.TransNmbr = _prmPOSTrShippingHd.TransNmbr;
        //            _addTrShippingDt.ItemNo = _ctrRow++;
        //            _addTrShippingDt.AirwayBill = _rowData[0];
        //            _addTrShippingDt.ProductShape = _rowData[1];
        //            _addTrShippingDt.Weight = Convert.ToDecimal(_rowData[2]);
        //            _addTrShippingDt.Unit = _rowData[3];
        //            _addTrShippingDt.PriceForex = Convert.ToDecimal(_rowData[4]);
        //            _addTrShippingDt.AmountForex = Convert.ToDecimal(_rowData[5]);
        //            _addTrShippingDt.DiscPercentage = Convert.ToDecimal(_rowData[6].ToString()) / _addTrShippingDt.AmountForex * 100;
        //            _addTrShippingDt.DiscForex = Convert.ToDecimal(_rowData[6].ToString());
        //            _addTrShippingDt.PackageAmount = Convert.ToDecimal(_rowData[7]);
        //            _addTrShippingDt.InsuranceAmount = Convert.ToDecimal(_rowData[8]);
        //            _addTrShippingDt.LineTotalForex = Convert.ToDecimal(_rowData[9].ToString());
        //            _addTrShippingDt.Remark = _rowData[10];
        //            _addTrShippingDt.FgConsignment = Convert.ToBoolean(_rowData[11]);
        //            _addTrShippingDt.TRS = Convert.ToDecimal(_rowData[12]);
        //            _addTrShippingDt.TRS2 = Convert.ToDecimal(_rowData[13]);
        //            _addTrShippingDt.TRS3 = Convert.ToDecimal(_rowData[14]);
        //            _addTrShippingDt.OtherSurcharge = Convert.ToDecimal(_rowData[15]);
        //            _addTrShippingDt.OtherSurcharge2 = Convert.ToDecimal(_rowData[16]);
        //            _addTrShippingDt.OtherSurcharge3 = Convert.ToDecimal(_rowData[17]);
        //            this.db.POSTrShippingDts.InsertOnSubmit(_addTrShippingDt);
        //        }

        //        this.db.SubmitChanges();

        //        String _errMessage = "";
        //        _result = _prmPOSTrShippingHd.TransNmbr + "|" + _prmPOSTrShippingHd.FileNmbr + "|" + _errMessage;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return _result;
        //}

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
                                join _posShippingHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posShippingHD.TransNmbr
                                where _posShippingHD.TransNmbr == RefNmbr
                                && _posShippingHD.TransType == _prmTranstype
                                select _posShippingHD.CustName
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
                                join _posShippingHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posShippingHD.TransNmbr
                                where _posShippingHD.TransNmbr == RefNmbr
                                && _posShippingHD.TransType == _prmTranstype
                                select _posShippingHD.DoneSettlement
                                ).FirstOrDefault();

                _result = ((_query2 == null) ? 'N' : 'Y');

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public POSMsShipping GetPOSMsShipping(string _prmVendorCode, string _prmShippingTypeCode, string _prmProductShape, string _prmCityCode)
        {
            POSMsShipping _result = null;
            //var _query = (
            //            from _posMsShipping in db.spPOS_MsShipping(_prmVendorCode, _prmShippingTypeCode, _prmProductShape, _prmCityCode)
            //            select _posMsShipping
            //        );

            //foreach (var _row in _query)
            //{
            //    //_result.Add(new POSMsShipping(_row.VendorCode,_row.ShippingTypeCode,_row.ProductShape,_row.CityCode,_row.VendorName,_row.ShippingTypeName,_row.CityName,_row.Percentage,_row.Price1,_row.Price2,_row.EstimationTime));
            //    _result = new POSMsShipping(_row.VendorCode, _row.ShippingTypeCode, _row.ProductShape, _row.CityCode, _row.VendorName, _row.ShippingTypeName, _row.CityName, _row.Percentage, _row.Price1, _row.Price2, _row.EstimationTime, _row.UnitCode);
            //}
            this.db.spPOS_MsShipping(_prmVendorCode, _prmShippingTypeCode, _prmProductShape, _prmCityCode);
            var _query = (
                            from _temp in db.General_TemporaryTables
                            where _temp.TableName == "spPOS_MsShippingResult"
                            && _temp.StoreProcedure == "spPOS_MsShipping"
                            && _temp.PrimaryKey1 == _prmVendorCode
                            && _temp.PrimaryKey2 == _prmShippingTypeCode
                            && _temp.PrimaryKey3 == _prmProductShape
                            && _temp.PrimaryKey4 == _prmCityCode
                            select _temp);

            foreach (var _row in _query)
            {
                _result = new POSMsShipping(_row.Field1, _row.Field2, _row.Field3, _row.Field4, _row.Field5, _row.Field6, _row.Field7, Convert.ToDecimal(_row.Field8), Convert.ToDecimal(_row.Field9), Convert.ToDecimal(_row.Field10), _row.Field11, _row.Field12);
            }
            return _result;
        }

        public General_TemporaryTable GetPOSMsZone(string _prmZoneCode, string _prmProductShape, double? _prmWeight, string _prmCountryCode)
        {
            General_TemporaryTable _result = null;
            this.db.spPOS_MsShippingZone(_prmZoneCode, _prmProductShape, _prmWeight, _prmCountryCode);

            var _query = (
                            from _temp in db.General_TemporaryTables
                            where _temp.TableName == "spPOS_MsShippingZoneResult"
                            && _temp.StoreProcedure == "spPOS_MsShippingZone"
                            && _temp.PrimaryKey1 == _prmZoneCode
                            && _temp.PrimaryKey2 == _prmProductShape
                            select _temp
                         );
            foreach (var _row in _query)
            {
                _result = _row;
            }
            return _result;
        }

        public List<POSMsShippingType> GetShippingType(string _prmVendorCode)
        {
            List<POSMsShippingType> _result = null;

            var _query = (
                        from _posMsShippingType in db.POSMsShippingTypes
                        join _posMsShippingVendorDt in db.POSMsShippingVendorDts
                        on _posMsShippingType.ShippingTypeCode equals _posMsShippingVendorDt.ShippingZonaTypeCode
                        where _posMsShippingType.FgActive == 'Y'
                        & _posMsShippingVendorDt.FgActive == 'Y'
                        & _posMsShippingVendorDt.VendorCode == _prmVendorCode
                        select _posMsShippingType
                    ).Distinct();

            if (_query.Count() > 0)
                _result = new List<POSMsShippingType>();

            foreach (var _row in _query)
            {
                _result.Add(_row);
            }
            return _result;
        }

        public List<POSMsZone> GetShippingZone(string _prmVendorCode)
        {
            List<POSMsZone> _result = null;

            var _query = (
                        from _posMsZone in db.POSMsZones
                        join _posMsShippingVendorDt in db.POSMsShippingVendorDts
                        on _posMsZone.ZoneCode equals _posMsShippingVendorDt.ShippingZonaTypeCode
                        where _posMsZone.FgActive == 'Y'
                        & _posMsShippingVendorDt.FgActive == 'Y'
                        & _posMsShippingVendorDt.VendorCode == _prmVendorCode
                        select _posMsZone
                    );

            if (_query.Count() > 0)
                _result = new List<POSMsZone>();

            foreach (var _row in _query)
            {
                _result.Add(_row);
            }
            return _result;
        }

        public List<V_POSMsShipping> GetShippingZoneType(string _prmVendorCode, string _prmCountryCode, string _prmCityCode)
        {
            List<V_POSMsShipping> _result = null;
            string _shippingZoneType = "";
            var _query = (
                                from _vPOSMsShipping in this.db.V_POSMsShippings
                                where _vPOSMsShipping.VendorCode == _prmVendorCode
                                && _vPOSMsShipping.CountryCode == _prmCountryCode
                                && _vPOSMsShipping.CityCode == _prmCityCode
                                orderby _vPOSMsShipping.VendorName
                                select _vPOSMsShipping
                            ).Distinct();

            if (_query.Count() > 0)
                _result = new List<V_POSMsShipping>();
            
            foreach (var _row in _query)
            {
                if (_shippingZoneType != _row.ShippingZonaTypeName)
                {
                    _shippingZoneType = _row.ShippingZonaTypeName;
                    _result.Add(_row);
                }
            }

            return _result;
        }

        public bool AddPOSTrShippingHd(POSTrShippingHd _prmPOSTrShippingHd)
        {
            bool _result = false;

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmPOSTrShippingHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                this.db.POSTrShippingHds.InsertOnSubmit(_prmPOSTrShippingHd);

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

        public bool EditPOSTrShippingHd(POSTrShippingHd _prmPOSTrShippingHd)
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

        public bool AddPOSTrShippingDt(POSTrShippingDt _prmPOSTrShippingDt)
        {
            bool _result = false;

            try
            {
                this.db.POSTrShippingDts.InsertOnSubmit(_prmPOSTrShippingDt);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPOSTrShippingDt(POSTrShippingDt _prmPOSTrShippingDt)
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

        public int RowsCountPOSTrShippingDt(string _prmCode)
        {
            int _result = 0;

            _result = this.db.POSTrShippingDts.Where(_row => _row.TransNmbr == _prmCode).Count();

            return _result;
        }

        public Decimal GetMaxWeightDocument(string _prmZoneCode, string _prmProductShape)
        {
            Decimal _result = 0;
            POSMsZonePrice _posMsZonePrice = this.db.POSMsZonePrices.OrderByDescending(_row => _row.Weight).FirstOrDefault(_row => _row.ZoneCode == _prmZoneCode && _row.ProductShape == _prmProductShape && _row.FgActive == 'Y');
            _result = (_posMsZonePrice == null) ? 0 : _posMsZonePrice.Weight;
            return _result;
        }

        public POSMsZone GetSinglePOSMsZone(string _prmCode)
        {
            POSMsZone _result = null;

            try
            {
                _result = this.db.POSMsZones.Single(_temp => _temp.ZoneCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<V_POSMsShipping> GetListByCountryCity(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<V_POSMsShipping> _result = new List<V_POSMsShipping>();

            try
            {
                string[] _split = _prmKeyword.Split(',');
                string _pattern1 = _split[0];
                string _pattern2 = _split[1];
                string _vendorName = "";

                var _query = (
                                from _vPOSMsShipping in this.db.V_POSMsShippings
                                where _vPOSMsShipping.CountryCode == _pattern1
                                && _vPOSMsShipping.CityCode == _pattern2
                                orderby _vPOSMsShipping.VendorName
                                select _vPOSMsShipping
                            ).Distinct().Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
                foreach (var _row in _query)
                {
                    if (_vendorName != _row.VendorName)
                    {
                        _vendorName = _row.VendorName;
                        _result.Add(_row);
                    }
                }

                //if (_prmCategory == "FgHome")
                //{
                //    var _query = (
                //                    from _posMsShippingVendor in this.db.POSMsShippingVendors
                //                    join _posMsShippingVendorDt in db.POSMsShippingVendorDts
                //                        on _posMsShippingVendor.VendorCode equals _posMsShippingVendorDt.VendorCode
                //                    join _posMsShippingTypeDt in db.POSMsShippingTypeDts
                //                        on _posMsShippingVendorDt.ShippingZonaTypeCode equals _posMsShippingTypeDt.ShippingTypeCode
                //                    where (_posMsShippingVendor.FgZone == null || _posMsShippingVendor.FgZone == 'N')
                //                    && _posMsShippingVendor.FgActive == 'Y'
                //                    && _posMsShippingVendorDt.FgActive == 'Y'
                //                    && _posMsShippingTypeDt.FgActive == 'Y'
                //                    orderby _posMsShippingVendor.VendorName
                //                    select _posMsShippingVendor
                //                ).Distinct().Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
                //    foreach (var _row in _query)
                //    {
                //        _result.Add(_row);
                //    }
                //}
                //else
                //{
                //string _pattern1 = "%%";

                //if (_prmCategory == "CountryCode")
                //{
                //    _pattern1 = _prmKeyword.ToUpper();
                //}
                //var _query = (
                //                from _posMsShippingVendor in this.db.POSMsShippingVendors
                //                join _posMsShippingVendorDt in db.POSMsShippingVendorDts
                //                    on _posMsShippingVendor.VendorCode equals _posMsShippingVendorDt.VendorCode
                //                join _posMsZoneCountry in db.POSMsZoneCountries
                //                    on _posMsShippingVendorDt.ShippingZonaTypeCode equals _posMsZoneCountry.ZoneCode
                //                where _posMsShippingVendor.FgZone == 'Y'
                //                && _posMsShippingVendor.FgActive == 'Y'
                //                && _posMsShippingVendorDt.FgActive == 'Y'
                //                && _posMsZoneCountry.FgActive == 'Y'
                //                && _posMsZoneCountry.CountryCode.ToUpper() == _pattern1
                //                orderby _posMsShippingVendor.VendorName
                //                select _posMsShippingVendor
                //            ).Distinct().Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
                //foreach (var _row in _query)
                //{
                //    _result.Add(_row);
                //}
                //}

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public POSMsShippingVendor GetSinglePOSMsShippingVendor(string _prmCode)
        {
            POSMsShippingVendor _result = null;

            try
            {
                _result = this.db.POSMsShippingVendors.Single(_temp => _temp.VendorCode == _prmCode);
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
