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
    public sealed class POSInternetBL : Base
    {
        public POSInternetBL()
        {
        }

        ~POSInternetBL()
        {
        }

        #region POSInternet

        public String GetTableNumber(String _prmRoomCode, int _prmTableID)
        {
            String _result = _prmTableID.ToString();
            try
            {
                var _qry = (
                    from _internetTable in this.db.POSMsInternetTables
                    join _internetRoom in this.db.POSMsInternetFloors
                    on _internetTable.FloorNmbr equals _internetRoom.FloorNmbr
                    where _internetRoom.roomCode == _prmRoomCode
                        && _internetTable.TableIDPerRoom == _prmTableID
                        && _internetTable.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                        && _internetRoom.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                    select _internetTable.TableNmbr);
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
                    from _internetTable in this.db.POSMsInternetTables
                    join _internetRoom in this.db.POSMsInternetFloors
                    on _internetTable.FloorNmbr equals _internetRoom.FloorNmbr
                    where _internetRoom.roomCode == _prmRoomCode
                        && _internetTable.TableIDPerRoom == _prmTableID
                        && _internetTable.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                        && _internetRoom.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                    select _internetTable.Status);
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
            var _query = from _msInternetFloor in this.db.POSMsInternetFloors
                         where _msInternetFloor.fgActive == true
                         && _msInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                         select _msInternetFloor;
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
                                where _msInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                                && _msInternetFloor.fgActive == true
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

        public POSTrInternetHd GetSinglePOSTrInternetHd(String _prmCode)
        {
            POSTrInternetHd _result = new POSTrInternetHd();
            try
            {
                _result = this.db.POSTrInternetHds.FirstOrDefault(_temp => _temp.TransNmbr == _prmCode);
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
                                && _msInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
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
                              && _msInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
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
                            && _posMsInternetTable.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                            && _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
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
                            && _posMsInternetTable.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                            && _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
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
                        && _posMsFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                        select _posMsFloor.FloorNmbr
                    ).FirstOrDefault();
                POSMsInternetTable _fromRow = this.db.POSMsInternetTables.FirstOrDefault(a => a.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet) && a.FloorNmbr == _floorNmbr && a.TableIDPerRoom == Convert.ToInt32(_prmFromTableID));
                POSMsInternetTable _toRow = this.db.POSMsInternetTables.FirstOrDefault(a => a.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet) && a.FloorNmbr == _floorNmbr && a.TableIDPerRoom == Convert.ToInt32(_prmToTableID));
                _fromRow.Status = 0;
                _toRow.Status = 1;

                //Boolean _result = _tableHistBL.TransferTable(_floorNmbr, Convert.ToInt32(_prmFromTableID), Convert.ToInt32(_prmToTableID));
                var _query = (
                                from _hist in this.db.POSTableStatusHistories
                                where _hist.FloorNmbr == _floorNmbr && _hist.TableID == Convert.ToInt32(_prmFromTableID)
                                && _hist.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                                orderby _hist.ID descending
                                select _hist.ID
                             );

                if (_query.Count() > 0)
                {
                    int _histIDOld = _query.FirstOrDefault();

                    POSTableStatusHistory _histTableOld = this.db.POSTableStatusHistories.Single(_temp => _temp.ID == _histIDOld);
                    _histTableOld.StillActive = false;

                    POSTableStatusHistory _histTable = new POSTableStatusHistory();
                    _histTable.ID = _prmNewID;
                    _histTable.FloorNmbr = _floorNmbr;
                    _histTable.FloorType = POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet);
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

        public String StopInternet(String _prmFloorName, String _prmTableID)
        {
            String _result = "";

            try
            {
                int _floorNmbr = (
                        from _posMsFloor in this.db.POSMsInternetFloors
                        where _posMsFloor.FloorName == _prmFloorName
                        && _posMsFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                        select _posMsFloor.FloorNmbr
                    ).FirstOrDefault();
                var _query = (
                                from _hist in this.db.POSTableStatusHistories
                                where _hist.FloorNmbr == _floorNmbr && _hist.TableID == Convert.ToInt32(_prmTableID)
                                && _hist.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
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

        public String AddTimeInternet(String _prmFloorName, String _prmTableID, int _prmDuration)
        {
            String _result = "";

            try
            {
                int _floorNmbr = (
                        from _posMsFloor in this.db.POSMsInternetFloors
                        where _posMsFloor.FloorName == _prmFloorName
                        select _posMsFloor.FloorNmbr
                    ).FirstOrDefault();
                var _query = (
                                from _hist in this.db.POSTableStatusHistories
                                where _hist.FloorNmbr == _floorNmbr && _hist.TableID == Convert.ToInt32(_prmTableID)
                                orderby _hist.ID descending
                                select _hist.ID
                             );

                if (_query.Count() > 0)
                {
                    int _histIDOld = _query.FirstOrDefault();

                    POSTableStatusHistory _histTableOld = this.db.POSTableStatusHistories.Single(_temp => _temp.ID == _histIDOld);
                    _histTableOld.EndTime = _histTableOld.EndTime.AddMinutes(_prmDuration);

                    this.db.SubmitChanges();
                }
            }
            catch (Exception Ex)
            {
                _result = "Add Time Failed, " + Ex.Message;
            }

            return _result;
        }

        public List<POSTrInternetDt> GetListInternetDtByTransNmbr(String _prmTransNmbr)
        {
            List<POSTrInternetDt> _result = new List<POSTrInternetDt>();
            try
            {
                var _query = (
                                from _trInternetDt in this.db.POSTrInternetDts
                                where _trInternetDt.TransNmbr == _prmTransNmbr
                                select new
                                {
                                    ProductCode = _trInternetDt.ProductCode,
                                    Remark = (
                                                from _product in this.db.MsProducts
                                                where _product.ProductCode == _trInternetDt.ProductCode
                                                select _product.ProductName
                                              ).FirstOrDefault(),
                                    Qty = _trInternetDt.Qty,
                                    DiscForex = _trInternetDt.DiscForex,
                                    AmountForex = _trInternetDt.AmountForex,
                                    LineTotalForex = _trInternetDt.LineTotalForex
                                }
                            );
                foreach (var _row in _query)
                {
                    _result.Add(new POSTrInternetDt(_row.ProductCode, (_row.Qty == null) ? 0 : Convert.ToInt32(_row.Qty), (_row.AmountForex == null) ? 0 : Convert.ToDecimal(_row.AmountForex), (_row.DiscForex == null) ? 0 : Convert.ToDecimal(_row.DiscForex), (_row.LineTotalForex == null) ? 0 : Convert.ToDecimal(_row.LineTotalForex), _row.Remark));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrInternetHd> GetListInternetSendToCashier(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrInternetHd> _result = new List<POSTrInternetHd>();
            try
            {
                var _query = (
                                from _internetHd in this.db.POSTrInternetHds
                                where SqlMethods.Like((_internetHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_internetHd.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && _internetHd.Status == POSTrInternetDataMapper.GetStatus(POSTrInternetStatus.SendToCashier)
                                    && _internetHd.DoneSettlement == POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet)
                                    && _internetHd.IsVOID == false
                                    && (_internetHd.DeliveryOrderReff == "" || _internetHd.DeliveryOrderReff == null)
                                select _internetHd
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

        public List<POSTrInternetHd> GetListInternetPayNotDelivered(String _prmCustID, String _prmSearchBy, String _prmSearchText)
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

            List<POSTrInternetHd> _result = new List<POSTrInternetHd>();
            try
            {
                var _query = (
                                from _internetHd in this.db.POSTrInternetHds
                                join _settlementRef in this.db.POSTrSettlementDtRefTransactions
                                    on _internetHd.TransNmbr equals _settlementRef.ReferenceNmbr
                                join _settlement in this.db.POSTrSettlementHds
                                    on _settlementRef.TransNmbr equals _settlement.TransNmbr
                                join _settlementDtProducy in this.db.POSTrSettlementDtProducts
                                    on _internetHd.TransNmbr equals _settlementDtProducy.ReffNmbr
                                    //_settlement.TransNmbr equals _settlementDtProducy.TransNmbr
                                into joined
                                from _settlementDtProducy in joined.DefaultIfEmpty()
                                where SqlMethods.Like((_internetHd.ReferenceNo ?? "").Trim().ToLower(), _pattern1.Trim().ToLower())
                                    && SqlMethods.Like((_settlement.TransNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower())
                                    && SqlMethods.Like((_internetHd.CustName ?? "").Trim().ToLower(), _pattern3.Trim().ToLower())
                                    && ((_settlement.Status == POSTrSettlementDataMapper.GetStatus(POSTrSettlementStatus.Posted) && _settlementDtProducy.FgStock == 'Y') || _internetHd.DPPaid > 0)
                                    && _settlementRef.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Internet)
                                    && _internetHd.IsVOID == false
                                    && ((_internetHd.DeliveryStatus == null || _internetHd.DeliveryStatus == false) ? false : true) == POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered)
                                //&& _settlementDtProducy.FgStock == 'Y'
                                select _internetHd
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

        public List<POSTrInternetHd> GetListInternetHdForDeliveryOrder(String _prmReferenceNo)
        {
            List<POSTrInternetHd> _result = new List<POSTrInternetHd>();

            try
            {
                var _query = (
                                from _internetHd in this.db.POSTrInternetHds
                                where _internetHd.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                                orderby _internetHd.TransDate descending
                                select _internetHd
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
                POSTrInternetHd _internetHd = this.db.POSTrInternetHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
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
                POSTrInternetHd _internetHd = this.db.POSTrInternetHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower());
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
                       from _internetHd in this.db.POSTrInternetHds
                       where _internetHd.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                       select _internetHd
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
                                join _posInternetHD in this.db.POSTrInternetHds
                                on _posTrSettlemenRefTrans.ReferenceNmbr equals _posInternetHD.TransNmbr
                                where _posInternetHD.TransNmbr == RefNmbr
                                select _posInternetHD.ReferenceNo
                                ).FirstOrDefault();

                _result = _query2;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetItemDurationPerInternet(String _prmInternetTransNmbr)
        {
            int _result = 0;

            var _query = (
                            from _internetHd in this.db.POSTrInternetHds
                            join _internetDt in this.db.POSTrInternetDts
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

        public POSTrInternetHd GetSinglePOSTableStatusHistory(int _prmFloor, Int16 _prmTableID)
        {
            POSTrInternetHd _result = null;
            try
            {
                var _query = (
                    //from _pOSTrInternetDt in this.db.POSTrInternetDts
                    //join _pOSTrInternetHd in this.db.POSTrInternetHds
                    //on _pOSTrInternetDt.TransNmbr equals _pOSTrInternetHd.TransNmbr
                    //where _pOSTrInternetHd.FloorNmbr == _prmFloor
                    //&& _pOSTrInternetHd.TableNmbr == _prmTableID
                    //&& _pOSTrInternetHd.Status == 2
                    //&& _pOSTrInternetHd.TransType == "INTERNET"
                    //select _pOSTrInternetHd

                                from _pOSTableStatusHistory in this.db.POSTableStatusHistories
                                join _pOSTrInternetHd in this.db.POSTrInternetHds
                                on new { _pOSTableStatusHistory.FloorNmbr, _pOSTableStatusHistory.TableID } equals new { FloorNmbr = (int)_pOSTrInternetHd.FloorNmbr, TableID = (int)_pOSTrInternetHd.TableNmbr }
                                //&& _pOSTableStatusHistory.TableID == _pOSTrInternetHd.TableNmbr
                                //&& _pOSTrInternetHd.TransDate >= _pOSTableStatusHistory.StartTime
                                //&& _pOSTrInternetHd.TransDate <= _pOSTableStatusHistory.EndTime
                                where _pOSTableStatusHistory.FloorNmbr == _prmFloor
                                && _pOSTableStatusHistory.TableID == _prmTableID
                                && _pOSTableStatusHistory.Status == 2
                                && _pOSTableStatusHistory.StillActive == true
                                && _pOSTableStatusHistory.FloorType == "Internet"
                                    //&& _pOSTrInternetHd.TransDate >= _pOSTableStatusHistory.StartTime
                                && _pOSTrInternetHd.TransDate <= _pOSTableStatusHistory.EndTime
                                orderby _pOSTrInternetHd.TransDate descending
                                select _pOSTrInternetHd
                            );
                _result = _query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public POSTrInternetBookingHd GetSinglePOSTrInternetBookingHd(int _prmFloor, Int16 _prmTableID)
        {
            POSTrInternetBookingHd _result = null;
            try
            {
                var _query = (
                                from _pOSTableStatusHistory in this.db.POSTableStatusHistories
                                join _pOSTrInternetBookingDt in this.db.POSTrInternetBookingDts
                                on _pOSTableStatusHistory.ID equals _pOSTrInternetBookingDt.HistoryID
                                join _pOSTrInternetBookingHd in this.db.POSTrInternetBookingHds
                                on _pOSTrInternetBookingDt.InternetBookCode equals _pOSTrInternetBookingHd.InternetBookCode
                                where _pOSTableStatusHistory.FloorNmbr == _prmFloor
                                && _pOSTableStatusHistory.TableID == _prmTableID
                                && _pOSTableStatusHistory.Status == 1
                                && _pOSTableStatusHistory.StillActive == true
                                select _pOSTrInternetBookingHd
                             );
                _result = _query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String GetSinglePOSMsInternetFloor(String _prmFloorType)
        {
            String _result = "";
            try
            {
                var _query = (from _msInternetFloor in this.db.POSMsInternetFloors
                              where _msInternetFloor.FloorType == _prmFloorType
                              orderby _msInternetFloor.FloorNmbr ascending
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

        public String GetTransnumbSettlement(String _transNmbr)
        {
            String _result = "";
            try
            {
                //var _query = (
                //                from _posTrSettlementRefTransac in this.db.POSTrSettlementDtRefTransactions

                //                where _posTrSettlementRefTransac.ReferenceNmbr == _transNmbr
                //                && _posTrSettlementRefTransac.TransType == POSTransTypeDataMapper.GetTransType(POSTransType.Internet)
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
                                && _posTrSettlementRefTransac.TransType.ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).ToLower()
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

        #region POS Internet Choose Table

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
                                where _posMSMenuServiceTypeDt.MenuServiceTypeCode == "INET"
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
                                where _posMSMenuServiceTypeDt.MenuServiceTypeCode == "INET"
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

        public String SendToCashier(POSTrInternetHd _prmPOSTrInternetHd, String _prmDetilTrans)
        {
            String _result = "";
            try
            {
                if (_prmPOSTrInternetHd.TransNmbr == "" || _prmPOSTrInternetHd.TransNmbr == null)
                {
                    //using (TransactionScope _scope = new TransactionScope())
                    //{
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmPOSTrInternetHd.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    this.db.POSTrInternetHds.InsertOnSubmit(_prmPOSTrInternetHd);

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

                var _queryDetail = (from _temp1 in this.db.POSTrInternetDts
                                    where _temp1.TransNmbr == _prmPOSTrInternetHd.TransNmbr
                                    select _temp1
                                    );

                this.db.POSTrInternetDts.DeleteAllOnSubmit(_queryDetail);

                String[] _detailTransaksi = _prmDetilTrans.Split('^');
                int _ctrRow = 1;
                foreach (String _dataDetil in _detailTransaksi)
                {
                    String[] _rowData = _dataDetil.Split('|');
                    // prodcode,qty,prodname,sellprice,disc,total,description
                    MsProduct _msProduct = new ProductBL().GetSingleProduct(_rowData[0].ToString());

                    POSTrInternetDt _addTrInternetDt = new POSTrInternetDt();
                    _addTrInternetDt.TransNmbr = _prmPOSTrInternetHd.TransNmbr;
                    _addTrInternetDt.ItemNo = _ctrRow++;
                    _addTrInternetDt.ProductCode = _rowData[0].ToString();
                    _addTrInternetDt.Qty = Convert.ToDecimal(_rowData[1]);
                    _addTrInternetDt.Unit = _msProduct.Unit;
                    _addTrInternetDt.UnitPriceForex = 0;
                    _addTrInternetDt.AmountForex = Convert.ToDecimal(_rowData[3].ToString());
                    _addTrInternetDt.DiscPercentage = Convert.ToDecimal(_rowData[4].ToString()) / _addTrInternetDt.AmountForex * 100;
                    _addTrInternetDt.DiscForex = Convert.ToDecimal(_rowData[4].ToString());
                    _addTrInternetDt.LineTotalForex = Convert.ToDecimal(_rowData[5].ToString());
                    _addTrInternetDt.Remark = _rowData[6];

                    this.db.POSTrInternetDts.InsertOnSubmit(_addTrInternetDt);
                }

                this.db.SubmitChanges();

                String _errMessage = "";
                //this.db.spSAL_POSPosting(_prmPOSTrRetailHd.TransNmbr, HttpContext.Current.User.Identity.Name, _errMessage);

                //_scope.Complete();

                _result = _prmPOSTrInternetHd.TransNmbr + "|" + _prmPOSTrInternetHd.FileNmbr + "|" + _errMessage;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String SendToCashierForDeliveryOrder(POSTrInternetHd _prmPOSTrInternetHd, String _prmDetilTrans, POSTrDeliveryOrderRef _prmPOSTrDeliveryOrderRef)
        {
            String _result = "";
            try
            {
                if (_prmPOSTrInternetHd.TransNmbr == "" || _prmPOSTrInternetHd.TransNmbr == null)
                {
                    //using (TransactionScope _scope = new TransactionScope())
                    //{
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmPOSTrInternetHd.TransNmbr = _item.Number;
                        _prmPOSTrDeliveryOrderRef.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    this.db.POSTrInternetHds.InsertOnSubmit(_prmPOSTrInternetHd);
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

                var _queryDetail = (from _temp1 in this.db.POSTrInternetDts
                                    where _temp1.TransNmbr == _prmPOSTrInternetHd.TransNmbr
                                    select _temp1
                                    );

                this.db.POSTrInternetDts.DeleteAllOnSubmit(_queryDetail);

                String[] _detailTransaksi = _prmDetilTrans.Split('^');
                int _ctrRow = 1;
                foreach (String _dataDetil in _detailTransaksi)
                {
                    String[] _rowData = _dataDetil.Split('|');
                    // prodcode,qty,prodname,sellprice,disc,total,description
                    MsProduct _msProduct = new ProductBL().GetSingleProduct(_rowData[0].ToString());

                    POSTrInternetDt _addTrInternetDt = new POSTrInternetDt();
                    _addTrInternetDt.TransNmbr = _prmPOSTrInternetHd.TransNmbr;
                    _addTrInternetDt.ItemNo = _ctrRow++;
                    _addTrInternetDt.ProductCode = _rowData[0].ToString();
                    _addTrInternetDt.Qty = Convert.ToDecimal(_rowData[1]);
                    _addTrInternetDt.Unit = _msProduct.Unit;
                    _addTrInternetDt.UnitPriceForex = 0;
                    _addTrInternetDt.AmountForex = Convert.ToDecimal(_rowData[3].ToString());
                    _addTrInternetDt.DiscPercentage = Convert.ToDecimal(_rowData[4].ToString()) / _addTrInternetDt.AmountForex * 100;
                    _addTrInternetDt.DiscForex = Convert.ToDecimal(_rowData[4].ToString());
                    _addTrInternetDt.LineTotalForex = Convert.ToDecimal(_rowData[5].ToString());
                    _addTrInternetDt.Remark = _rowData[6];

                    this.db.POSTrInternetDts.InsertOnSubmit(_addTrInternetDt);
                }

                this.db.SubmitChanges();

                String _errMessage = "";
                //this.db.spSAL_POSPosting(_prmPOSTrRetailHd.TransNmbr, HttpContext.Current.User.Identity.Name, _errMessage);

                //_scope.Complete();

                _result = _prmPOSTrInternetHd.TransNmbr + "|" + _prmPOSTrInternetHd.FileNmbr + "|" + _errMessage;
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

        #endregion
    }
}
