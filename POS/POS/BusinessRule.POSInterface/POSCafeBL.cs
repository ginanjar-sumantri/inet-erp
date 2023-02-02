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
    public sealed class POSCafeBL : Base
    {
        public POSCafeBL()
        {
        }

        ~POSCafeBL()
        {
        }

        #region POSCafeBL

        public String GetTableNumber(String _prmRoomCode, int _prmTableID)
        {
            String _result = _prmTableID.ToString();
            try
            {
                var _qry = (
                    from _cafeTable in this.db.POSMsInternetTables
                    join _cafeRoom in this.db.POSMsInternetFloors
                    on _cafeTable.FloorNmbr equals _cafeRoom.FloorNmbr
                    where _cafeRoom.roomCode == _prmRoomCode
                        && _cafeTable.TableIDPerRoom == _prmTableID
                        && _cafeTable.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                        && _cafeRoom.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                    select _cafeTable.TableNmbr);
                if (_qry.Count() > 0)
                    _result = _qry.FirstOrDefault().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public int GetTableStatus(String _prmRoomCode, int _prmTableID)
        {
            int _result = 0;
            try
            {
                var _qry = (
                    from _cafeTable in this.db.POSMsInternetTables
                    join _cafeRoom in this.db.POSMsInternetFloors
                    on _cafeTable.FloorNmbr equals _cafeRoom.FloorNmbr
                    where _cafeRoom.roomCode == _prmRoomCode
                        && _cafeTable.TableIDPerRoom == _prmTableID
                        && _cafeTable.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                        && _cafeRoom.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                    select _cafeTable.Status);
                if (_qry.Count() > 0)
                    _result = _qry.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public int GetLastPageFloorButton(int _prmPageSize)
        {
            var _query = from _msCafeFloor in this.db.POSMsInternetFloors
                         where _msCafeFloor.fgActive == true
                         && _msCafeFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                         select _msCafeFloor;
            if (_query.Count() > 0)
            {
                if (_query.Count() % _prmPageSize == 0)
                    return Convert.ToInt16(_query.Count() / _prmPageSize) - 1;
                else
                    return Convert.ToInt16(_query.Count() / _prmPageSize);
            }
            else
                return 0;
        }

        public List<POSMsInternetFloor> GetList(int _prmReqPage, int _prmPageSize)
        {
            List<POSMsInternetFloor> _result = new List<POSMsInternetFloor>();
            try
            {
                var _query = (
                                from _msInternetFloor in this.db.POSMsInternetFloors
                                where _msInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                                select _msInternetFloor
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

        public POSTrCafeHd GetSinglePOSTrCafeHd(String _prmCode)
        {
            POSTrCafeHd _result = new POSTrCafeHd();
            try
            {
                _result = this.db.POSTrCafeHds.FirstOrDefault(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public POSMsInternetFloor GetSingle(int _prmCode)
        {
            POSMsInternetFloor _result = null;
            try
            {
                _result = this.db.POSMsInternetFloors.Single(_temp => _temp.FloorNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public string GetFloorNameByNmbr(int _prmCode)
        {
            string _result = "";
            try
            {
                var _query = (
                                from _msInternetFloor in this.db.POSMsInternetFloors
                                where _msInternetFloor.FloorNmbr == _prmCode
                                && _msInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                                select _msInternetFloor.FloorName
                              );
                if (_query.Count() > 0)
                    _result = _query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public int GetFloorNmbrByName(String _prmFloorName)
        {
            int _result = 0;
            if (this.db.POSMsInternetFloors.Where(a => a.roomCode == _prmFloorName).Count() > 0)
                _result = this.db.POSMsInternetFloors.Where(a => a.roomCode == _prmFloorName).FirstOrDefault().FloorNmbr;
            return _result;
        }

        public string GetRoomCode(int _prmCode)
        {
            string _result = "";
            try
            {
                var _query = (from _msInternetFloor in this.db.POSMsInternetFloors
                              where _msInternetFloor.FloorNmbr == _prmCode
                              & _msInternetFloor.FloorType == "Cafe"
                              select _msInternetFloor.roomCode);
                if (_query.Count() > 0)
                    _result = _query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSMsInternetTable> GetListOccupiedTable(String _prmRoomCode)
        {
            List<POSMsInternetTable> _result = new List<POSMsInternetTable>();
            try
            {
                var _qry = (
                        from _posMsInternetTable in this.db.POSMsInternetTables
                        join _posMsInternetFloor in this.db.POSMsInternetFloors
                        on _posMsInternetTable.FloorNmbr equals _posMsInternetFloor.FloorNmbr
                        where _posMsInternetFloor.roomCode == _prmRoomCode
                            && _posMsInternetTable.Status == 2
                            && _posMsInternetTable.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                            && _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                        select _posMsInternetTable
                    );
                foreach (var _row in _qry)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSMsInternetTable> GetListAvailableTable(String _prmRoomCode)
        {
            List<POSMsInternetTable> _result = new List<POSMsInternetTable>();
            try
            {
                var _qry = (
                        from _posMsInternetTable in this.db.POSMsInternetTables
                        join _posMsInternetFloor in this.db.POSMsInternetFloors
                        on _posMsInternetTable.FloorNmbr equals _posMsInternetFloor.FloorNmbr
                        where _posMsInternetFloor.roomCode == _prmRoomCode
                            && _posMsInternetTable.Status == 0
                            && _posMsInternetTable.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                            && _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                        select _posMsInternetTable
                    );
                foreach (var _row in _qry)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean TableTransfer(string _prmFromTableID, string _prmToTableID, string _prmFloorName, int _prmNewID)
        {
            Boolean _result = false;
            try
            {
                int _floorNmbr = (
                        from _posMsFloor in this.db.POSMsInternetFloors
                        where _posMsFloor.FloorName == _prmFloorName
                        && _posMsFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                        select _posMsFloor.FloorNmbr
                    ).FirstOrDefault();
                POSMsInternetTable _fromRow = this.db.POSMsInternetTables.FirstOrDefault(a => a.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe) && a.FloorNmbr == _floorNmbr && a.TableIDPerRoom == Convert.ToInt32(_prmFromTableID));
                POSMsInternetTable _toRow = this.db.POSMsInternetTables.FirstOrDefault(a => a.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe) && a.FloorNmbr == _floorNmbr && a.TableIDPerRoom == Convert.ToInt32(_prmToTableID));
                _fromRow.Status = 0;
                _toRow.Status = 1;

                //Boolean _result = _tableHistBL.TransferTable(_floorNmbr, Convert.ToInt32(_prmFromTableID), Convert.ToInt32(_prmToTableID));
                var _query = (
                                from _hist in this.db.POSTableStatusHistories
                                where _hist.FloorNmbr == _floorNmbr && _hist.TableID == Convert.ToInt32(_prmFromTableID)
                                && _hist.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                                orderby _hist.ID descending
                                select _hist.ID
                             );

                if (_query.Count() > 0)
                {
                    int _histIDOld = _query.FirstOrDefault();

                    POSTableStatusHistory _histTableOld = this.db.POSTableStatusHistories.FirstOrDefault(_temp => _temp.ID == _histIDOld);
                    _histTableOld.StillActive = false;

                    POSTableStatusHistory _histTable = new POSTableStatusHistory();
                    _histTable.ID = _prmNewID;
                    _histTable.FloorNmbr = _floorNmbr;
                    _histTable.FloorType = POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe);
                    _histTable.TableID = Convert.ToInt32(_prmToTableID);
                    _histTable.StartTime = _histTableOld.StartTime;
                    _histTable.EndTime = _histTableOld.EndTime;
                    _histTable.Status = _histTableOld.Status;
                    _histTable.StillActive = true;
                    _result = true;
                    this.db.POSTableStatusHistories.InsertOnSubmit(_histTable);
                }

                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {

            }
            return _result;
        }

        //public String StopInternet(String _prmFloorName, String _prmTableID)
        //{
        //    String _result = "";

        //    try
        //    {
        //        int _floorNmbr = (
        //                from _posMsFloor in this.db.POSMsInternetFloors
        //                where _posMsFloor.FloorName == _prmFloorName
        //                select _posMsFloor.FloorNmbr
        //            ).FirstOrDefault();
        //        var _query = (
        //                        from _hist in this.db.POSTableStatusHistories
        //                        where _hist.FloorNmbr == _floorNmbr && _hist.TableID == Convert.ToInt32(_prmTableID)
        //                        orderby _hist.ID descending
        //                        select _hist.ID
        //                     );

        //        if (_query.Count() > 0)
        //        {
        //            int _histIDOld = _query.FirstOrDefault();

        //            POSTableStatusHistory _histTableOld = this.db.POSTableStatusHistories.Single(_temp => _temp.ID == _histIDOld);
        //            _histTableOld.StillActive = false;

        //            this.db.SubmitChanges();
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        _result = "Stop Time Failed, " + Ex.Message;
        //    }

        //    return _result;
        //}

        //public String AddTimeInternet(String _prmFloorName, String _prmTableID, int _prmDuration)
        //{
        //    String _result = "";

        //    try
        //    {
        //        int _floorNmbr = (
        //                from _posMsFloor in this.db.POSMsInternetFloors
        //                where _posMsFloor.FloorName == _prmFloorName
        //                select _posMsFloor.FloorNmbr
        //            ).FirstOrDefault();
        //        var _query = (
        //                        from _hist in this.db.POSTableStatusHistories
        //                        where _hist.FloorNmbr == _floorNmbr && _hist.TableID == Convert.ToInt32(_prmTableID)
        //                        orderby _hist.ID descending
        //                        select _hist.ID
        //                     );

        //        if (_query.Count() > 0)
        //        {
        //            int _histIDOld = _query.FirstOrDefault();

        //            POSTableStatusHistory _histTableOld = this.db.POSTableStatusHistories.Single(_temp => _temp.ID == _histIDOld);
        //            _histTableOld.EndTime = _histTableOld.EndTime.AddMinutes(_prmDuration);

        //            this.db.SubmitChanges();
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        _result = "Add Time Failed, " + Ex.Message;
        //    }

        //    return _result;
        //}

        public List<POSTrCafeDt> GetListCafeDtByTransNmbr(String _prmTransNmbr)
        {
            List<POSTrCafeDt> _result = new List<POSTrCafeDt>();
            try
            {
                var _query = (
                                from _trCafeDt in this.db.POSTrCafeDts
                                where _trCafeDt.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    ProductCode = _trCafeDt.ProductCode,
                                    Remark = (
                                                from _product in this.db.MsProducts
                                                where _product.ProductCode == _trCafeDt.ProductCode
                                                select _product.ProductName
                                              ).FirstOrDefault(),
                                    Qty = _trCafeDt.Qty,
                                    DiscForex = _trCafeDt.DiscForex,
                                    AmountForex = _trCafeDt.AmountForex,
                                    LineTotalForex = _trCafeDt.LineTotalForex
                                }
                            );
                foreach (var _row in _query)
                {
                    _result.Add(new POSTrCafeDt(_row.ProductCode, (_row.Qty == null) ? 0 : Convert.ToInt32(_row.Qty), (_row.AmountForex == null) ? 0 : Convert.ToDecimal(_row.AmountForex), (_row.DiscForex == null) ? 0 : Convert.ToDecimal(_row.DiscForex), (_row.LineTotalForex == null) ? 0 : Convert.ToDecimal(_row.LineTotalForex), _row.Remark));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrCafeHd> GetListCafeHdSendToCashier(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrCafeHd> _result = new List<POSTrCafeHd>();
            try
            {
                var _query = (
                                from _cafeHd in this.db.POSTrCafeHds
                                where SqlMethods.Like((_cafeHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_cafeHd.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && _cafeHd.Status == POSTrInternetDataMapper.GetStatus(POSTrInternetStatus.SendToCashier)
                                    && _cafeHd.DoneSettlement == POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet)
                                    && _cafeHd.IsVOID == false
                                    && (_cafeHd.DeliveryOrderReff == "" || _cafeHd.DeliveryOrderReff == null)
                                select _cafeHd
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

        public List<POSTrCafeHd> GetListCafePayNotDelivered(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrCafeHd> _result = new List<POSTrCafeHd>();
            try
            {
                var _query = (
                                from _cafeHd in this.db.POSTrCafeHds
                                join _settlementRef in this.db.POSTrSettlementDtRefTransactions
                                    on _cafeHd.TransNmbr equals _settlementRef.ReferenceNmbr
                                join _settlement in this.db.POSTrSettlementHds
                                    on _settlementRef.TransNmbr equals _settlement.TransNmbr
                                join _settlementDtProducy in this.db.POSTrSettlementDtProducts
                                    //on _settlement.TransNmbr equals _settlementDtProducy.TransNmbr
                                    on _cafeHd.TransNmbr equals _settlementDtProducy.ReffNmbr
                                into joined
                                from _settlementDtProducy in joined.DefaultIfEmpty()
                                where SqlMethods.Like((_cafeHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_settlement.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && SqlMethods.Like((_cafeHd.CustName ?? "").Trim().ToLower(), _pattern3.Trim().ToLower())
                                    && ((_settlement.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted) && _settlementDtProducy.FgStock == 'Y') || _cafeHd.DPPaid > 0)
                                    && _settlementRef.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe)
                                    && _cafeHd.IsVOID == false
                                    && ((_cafeHd.DeliveryStatus == null || _cafeHd.DeliveryStatus == false) ? false : true) == POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered)
                                //&& _settlementDtProducy.FgStock == 'Y'
                                select _cafeHd
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

        public List<POSTrCafeHd> GetListRetailHdForDeliveryOrder(String _prmReferenceNo)
        {
            List<POSTrCafeHd> _result = new List<POSTrCafeHd>();

            try
            {
                var _query = (
                                from _cafeHd in this.db.POSTrCafeHds
                                where _cafeHd.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                                orderby _cafeHd.TransDate descending
                                select _cafeHd
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
                POSTrCafeHd _internetHd = this.db.POSTrCafeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
                _internetHd.DeliveryStatus = _prmDeliverValue;

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
                POSTrCafeHd _internetHd = this.db.POSTrCafeHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
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

        public Boolean SetVOIDForDeliveryOrder(String _prmReferenceNo, String _prmReasonCode, Boolean _prmVOIDValue)
        {
            Boolean _result = false;
            POSReasonBL _reasonBL = new POSReasonBL();

            try
            {
                POSTrDeliveryOrder _posTrdeliveryOrder = this.db.POSTrDeliveryOrders.Single(_temp => _temp.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower());

                var _query =
                    (
                       from _cafeHd in this.db.POSTrCafeHds
                       where _cafeHd.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                       select _cafeHd
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
                                join _posCafeHD in this.db.POSTrCafeHds
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posCafeHD.TransNmbr
                                where _posCafeHD.TransNmbr == RefNmbr
                                select _posCafeHD.ReferenceNo
                                ).FirstOrDefault();

                _result = _query2;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetItemDurationPerCafe(String _prmInternetTransNmbr)
        {
            int _result = 0;

            var _query = (
                            from _internetHd in this.db.POSTrCafeHds
                            join _internetDt in this.db.POSTrCafeDts
                                on _internetHd.TransNmbr equals _internetDt.TransNmbr
                            join _product in this.db.MsProducts
                                on _internetDt.ProductCode equals _product.ProductCode
                            where _internetHd.TransNmbr.Trim().ToLower() == _prmInternetTransNmbr.Trim().ToLower()
                                && (_product.ItemDuration ?? 0) > 0
                            select (int)_product.ItemDuration
                         );

            if (_query.Count() > 0)
            {
                foreach (var _itemDuration in _query)
                {
                    _result += Convert.ToInt32(_itemDuration);
                }
            }

            return _result;
        }

        public String StopCafe(String _prmFloorName, String _prmTableID)
        {
            String _result = "";

            try
            {
                int _floorNmbr = (
                        from _posMsFloor in this.db.POSMsInternetFloors
                        where _posMsFloor.FloorName == _prmFloorName
                        && _posMsFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                        select _posMsFloor.FloorNmbr
                    ).FirstOrDefault();
                var _query = (
                                from _hist in this.db.POSTableStatusHistories
                                where _hist.FloorNmbr == _floorNmbr && _hist.TableID == Convert.ToInt32(_prmTableID)
                                && _hist.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                                && _hist.StillActive == true
                                orderby _hist.ID descending
                                select _hist.ID
                             );

                if (_query.Count() > 0)
                {
                    int _histIDOld = _query.FirstOrDefault();

                    POSTableStatusHistory _histTableOld = this.db.POSTableStatusHistories.Single(_temp => _temp.ID == _histIDOld);
                    _histTableOld.StillActive = false;

                    this.db.SubmitChanges();
                }
            }
            catch (Exception Ex)
            {
                _result = "Stop Time Failed, " + Ex.Message;
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
                                join _posInternetHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posInternetHD.TransNmbr
                                where _posInternetHD.TransNmbr == RefNmbr
                                && _posInternetHD.TransType == _prmTranstype
                                select _posInternetHD.DoneSettlement
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
                //                && _posTrSettlementRefTransac.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe)
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
                                && _posTrSettlementRefTransac.TransType.ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).ToLower()
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

        #region POS Cafe Choose Table

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
                                where _posMSMenuServiceTypeDt.MenuServiceTypeCode == "CAFE"
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
                                where _posMSMenuServiceTypeDt.MenuServiceTypeCode == "CAFE"
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

        public String SendToCashier(POSTrCafeHd _prmPOSTrCafeHd, String _prmDetilTrans)
        {
            String _result = "";
            try
            {
                if (_prmPOSTrCafeHd.TransNmbr == "" || _prmPOSTrCafeHd.TransNmbr == null)
                {
                    //using (TransactionScope _scope = new TransactionScope())
                    //{
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmPOSTrCafeHd.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    this.db.POSTrCafeHds.InsertOnSubmit(_prmPOSTrCafeHd);

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

                var _queryDetail = (from _temp1 in this.db.POSTrCafeDts
                                    where _temp1.TransNmbr == _prmPOSTrCafeHd.TransNmbr
                                    select _temp1
                                    );

                this.db.POSTrCafeDts.DeleteAllOnSubmit(_queryDetail);

                String[] _detailTransaksi = _prmDetilTrans.Split('^');
                int _ctrRow = 1;
                foreach (String _dataDetil in _detailTransaksi)
                {
                    String[] _rowData = _dataDetil.Split('|');
                    // prodcode,qty,prodname,sellprice,disc,total,description
                    MsProduct _msProduct = new ProductBL().GetSingleProduct(_rowData[0].ToString());

                    POSTrCafeDt _addTrCafeDt = new POSTrCafeDt();
                    _addTrCafeDt.TransNmbr = _prmPOSTrCafeHd.TransNmbr;
                    _addTrCafeDt.ItemNo = _ctrRow++;
                    _addTrCafeDt.ProductCode = _rowData[0].ToString();
                    _addTrCafeDt.Qty = Convert.ToDecimal(_rowData[1]);
                    _addTrCafeDt.Unit = _msProduct.Unit;
                    _addTrCafeDt.UnitPriceForex = 0;
                    _addTrCafeDt.AmountForex = Convert.ToDecimal(_rowData[3].ToString());
                    _addTrCafeDt.DiscPercentage = Convert.ToDecimal(_rowData[4].ToString()) / _addTrCafeDt.AmountForex * 100;
                    _addTrCafeDt.DiscForex = Convert.ToDecimal(_rowData[4].ToString());
                    _addTrCafeDt.LineTotalForex = Convert.ToDecimal(_rowData[5].ToString());
                    _addTrCafeDt.Remark = _rowData[6];
                    this.db.POSTrCafeDts.InsertOnSubmit(_addTrCafeDt);
                }

                this.db.SubmitChanges();

                String _errMessage = "";
                //this.db.spSAL_POSPosting(_prmPOSTrRetailHd.TransNmbr, HttpContext.Current.User.Identity.Name, _errMessage);

                //_scope.Complete();

                _result = _prmPOSTrCafeHd.TransNmbr + "|" + _prmPOSTrCafeHd.FileNmbr + "|" + _errMessage;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String SendToCashierForDeliveryOrder(POSTrCafeHd _prmPOSTrCafeHd, String _prmDetilTrans, POSTrDeliveryOrderRef _prmPOSTrDeliveryOrderRef)
        {
            String _result = "";
            try
            {
                if (_prmPOSTrCafeHd.TransNmbr == "" || _prmPOSTrCafeHd.TransNmbr == null)
                {
                    //using (TransactionScope _scope = new TransactionScope())
                    //{
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmPOSTrCafeHd.TransNmbr = _item.Number;
                        _prmPOSTrDeliveryOrderRef.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    this.db.POSTrCafeHds.InsertOnSubmit(_prmPOSTrCafeHd);
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

                var _queryDetail = (from _temp1 in this.db.POSTrCafeDts
                                    where _temp1.TransNmbr == _prmPOSTrCafeHd.TransNmbr
                                    select _temp1
                                    );

                this.db.POSTrCafeDts.DeleteAllOnSubmit(_queryDetail);

                String[] _detailTransaksi = _prmDetilTrans.Split('^');
                int _ctrRow = 1;
                foreach (String _dataDetil in _detailTransaksi)
                {
                    String[] _rowData = _dataDetil.Split('|');
                    // prodcode,qty,prodname,sellprice,disc,total,description
                    MsProduct _msProduct = new ProductBL().GetSingleProduct(_rowData[0].ToString());

                    POSTrCafeDt _addTrCafeDt = new POSTrCafeDt();
                    _addTrCafeDt.TransNmbr = _prmPOSTrCafeHd.TransNmbr;
                    _addTrCafeDt.ItemNo = _ctrRow++;
                    _addTrCafeDt.ProductCode = _rowData[0].ToString();
                    _addTrCafeDt.Qty = Convert.ToDecimal(_rowData[1]);
                    _addTrCafeDt.Unit = _msProduct.Unit;
                    _addTrCafeDt.UnitPriceForex = 0;
                    _addTrCafeDt.AmountForex = Convert.ToDecimal(_rowData[3].ToString());
                    _addTrCafeDt.DiscPercentage = Convert.ToDecimal(_rowData[4].ToString()) / _addTrCafeDt.AmountForex * 100;
                    _addTrCafeDt.DiscForex = Convert.ToDecimal(_rowData[4].ToString());
                    _addTrCafeDt.LineTotalForex = Convert.ToDecimal(_rowData[5].ToString());
                    _addTrCafeDt.Remark = _rowData[6];

                    this.db.POSTrCafeDts.InsertOnSubmit(_addTrCafeDt);
                }

                this.db.SubmitChanges();

                String _errMessage = "";
                //this.db.spSAL_POSPosting(_prmPOSTrRetailHd.TransNmbr, HttpContext.Current.User.Identity.Name, _errMessage);

                //_scope.Complete();

                _result = _prmPOSTrCafeHd.TransNmbr + "|" + _prmPOSTrCafeHd.FileNmbr + "|" + _errMessage;
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
                                join _posInternetHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posInternetHD.TransNmbr
                                where _posInternetHD.TransNmbr == RefNmbr
                                && _posInternetHD.TransType == _prmTranstype
                                select _posInternetHD.CustName
                                ).FirstOrDefault();

                _result = _query2;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetTelephoneByTransType(string _prmCode, string _prmTranstype)
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
                                join _posCafeHD in this.db.V_POSReferencePayedLists
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posCafeHD.TransNmbr
                                where _posCafeHD.TransNmbr == RefNmbr
                                && _posCafeHD.TransType == _prmTranstype
                                select _posCafeHD.CustPhone
                                ).FirstOrDefault();

                _result = _query2;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public POSTrCafeBookingHd GetSinglePOSTrCafeBookingHd(int _prmFloor, Int16 _prmTableID)
        {
            POSTrCafeBookingHd _result = null;
            try
            {
                var _query = (
                                from _pOSTableStatusHistory in this.db.POSTableStatusHistories
                                join _pOSTrCafeBookingDt in this.db.POSTrCafeBookingDts
                                on _pOSTableStatusHistory.ID equals _pOSTrCafeBookingDt.HistoryID
                                join _pOSTrCafeBookingHd in this.db.POSTrCafeBookingHds
                                on _pOSTrCafeBookingDt.CafeBookCode equals _pOSTrCafeBookingHd.CafeBookCode
                                where _pOSTableStatusHistory.FloorNmbr == _prmFloor
                                && _pOSTableStatusHistory.TableID == _prmTableID
                                && _pOSTableStatusHistory.Status == 1
                                && _pOSTableStatusHistory.StillActive == true
                                select _pOSTrCafeBookingHd
                             );
                _result = _query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public POSTrCafeHd GetSinglePOSTableStatusHistory(int _prmFloor, Int16 _prmTableID)
        {
            POSTrCafeHd _result = null;
            try
            {
                var _query = (
                                from _pOSTableStatusHistory in this.db.POSTableStatusHistories
                                join _pOSTrCafeHd in this.db.POSTrCafeHds
                                    on _pOSTableStatusHistory.FloorNmbr equals _pOSTrCafeHd.FloorNmbr
                                where _pOSTableStatusHistory.FloorNmbr == _prmFloor
                                && _pOSTableStatusHistory.TableID == _prmTableID
                                && _pOSTableStatusHistory.Status == 2
                                && _pOSTableStatusHistory.StillActive == true
                                && _pOSTableStatusHistory.FloorType == "Cafe"
                                && new DateTime(Convert.ToDateTime(_pOSTrCafeHd.TransDate).Year, Convert.ToDateTime(_pOSTrCafeHd.TransDate).Month, Convert.ToDateTime(_pOSTrCafeHd.TransDate).Day) == new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                                orderby _pOSTrCafeHd.TransDate descending
                                select _pOSTrCafeHd
                            );

                List<POSTrCafeHd> _posCafeHD = new List<POSTrCafeHd>();

                foreach (var _item in _query)
                {
                    String[] _tableExtSplit = _item.TableExtension.Split(',');
                    if (_tableExtSplit.Contains(_prmTableID.ToString()))
                    {
                        _posCafeHD.Add(_item);
                        break;
                    }
                }

                _result = _posCafeHD.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        #endregion
    }
}


