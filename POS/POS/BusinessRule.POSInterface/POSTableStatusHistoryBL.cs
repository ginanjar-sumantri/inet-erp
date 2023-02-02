using System;
using System.Diagnostics;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

namespace BusinessRule.POSInterface
{
    public sealed class POSTableStatusHistoryBL : Base
    {
        public POSTableStatusHistoryBL()
        {
        }

        public int GetNewID()
        {
            int _result = 0;

            try
            {
                var _query = (
                                from _tableStatus in this.db.POSTableStatusHistories
                                //where _tableStatus.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                                select _tableStatus.ID
                             );

                if (_query.Count() > 0)
                {
                    _result = _query.Max() + 1;
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public int GetNewIDCafe()
        {
            int _result = 0;

            try
            {
                var _query = (
                                from _tableStatus in this.db.POSTableStatusHistories
                                //where _tableStatus.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                                select _tableStatus.ID
                             );

                if (_query.Count() > 0)
                {
                    _result = _query.Max() + 1;
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public Boolean Is15MinsLeft(String _prmRoomCode, int _prmTableID)
        {
            Boolean _result = false;

            var _query = (
                            from _hist in this.db.POSTableStatusHistories
                            join _floor in this.db.POSMsInternetFloors
                                on _hist.FloorNmbr equals _floor.FloorNmbr
                            where _floor.roomCode == _prmRoomCode
                                && _hist.TableID == _prmTableID
                                && _hist.Status == POSTrInternetDataMapper.GetStatusTable(POSMsInternetTableStatus.NotAvailable)
                                && _floor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                            orderby _hist.ID descending
                            select new
                            {
                                _hist.ID,
                                _hist.EndTime
                            }
                         );
            if (_query.Count() > 0)
            {
                TimeSpan _time = new TimeSpan(_query.FirstOrDefault().EndTime.Ticks);
                TimeSpan _timeNow = new TimeSpan(DateTime.Now.Ticks);

                double _difference = _time.Subtract(_timeNow).TotalMinutes;
                int _timeLimit = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSInternetTimeLimitAfter).SetValue);

                //if (_difference <= 15 && _difference >= 0)
                if (_difference <= _timeLimit && _difference >= 0)
                {
                    _result = true;
                }
            }

            return _result;
        }

        public Boolean Is15MinsLeftCafe(String _prmRoomCode, int _prmTableID)
        {
            Boolean _result = false;

            var _query = (
                            from _hist in this.db.POSTableStatusHistories
                            join _floor in this.db.POSMsInternetFloors
                                on _hist.FloorNmbr equals _floor.FloorNmbr
                            where _floor.roomCode == _prmRoomCode
                                && _hist.TableID == _prmTableID
                                && _hist.Status == POSTrInternetDataMapper.GetStatusTable(POSMsInternetTableStatus.NotAvailable)
                                && _floor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                            orderby _hist.ID descending
                            select new
                            {
                                _hist.ID,
                                _hist.EndTime
                            }
                         );
            if (_query.Count() > 0)
            {
                TimeSpan _time = new TimeSpan(_query.FirstOrDefault().EndTime.Ticks);
                TimeSpan _timeNow = new TimeSpan(DateTime.Now.Ticks);

                double _difference = _time.Subtract(_timeNow).TotalMinutes;
                int _timeLimit = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSCafeTimeLimitAfter).SetValue);

                //if (_difference <= 15 && _difference >= 0)
                if (_difference <= _timeLimit && _difference >= 0)
                {
                    _result = true;
                }
            }

            return _result;
        }

        public Boolean TransferTable(int _prmFloorNmbr, int _prmTableNmbrFrom, int _prmTableNmbrTo)
        {
            Boolean _result = false;

            try
            {

            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool AddTrBookingHistory(POSTableStatusHistory _newHistoryBookingData)
        {
            bool _result = false;

            try
            {
                this.db.POSTableStatusHistories.InsertOnSubmit(_newHistoryBookingData);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public POSTableStatusHistory GetSingleForInternet(String _prmCode)
        {
            POSTableStatusHistory _result = null;
            string[] _splitTemp = _prmCode.Split('-');
            try
            {
                _result = this.db.POSTableStatusHistories.Single(_temp => _temp.ID == Convert.ToInt32(_splitTemp[0]) && _temp.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet));
            }
            catch (Exception)
            {
            }

            return _result;
        }
        public POSTableStatusHistory GetSingleForCafe(String _prmCode)
        {
            POSTableStatusHistory _result = null;
            string[] _splitTemp = _prmCode.Split('-');
            try
            {
                _result = this.db.POSTableStatusHistories.Single(_temp => _temp.ID == Convert.ToInt32(_splitTemp[0]) && _temp.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe));
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public Int32 GetID(Int32 _prmFloorNmbr, Int32 _prmTableID, String _prmFloorType)
        {
            Int32 _result = 0;
            try
            {
                _result = this.db.POSTableStatusHistories.OrderByDescending(_temp => _temp.ID).FirstOrDefault(_temp => _temp.FloorNmbr == _prmFloorNmbr && _temp.TableID == _prmTableID && _temp.FloorType == _prmFloorType).ID;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public Boolean IsTableHistoryExist(String _prmFloorType, int _prmFloorNmbr, int _prmTableID, DateTime _prmNow, ref String _prmID)
        {
            Boolean _result = false;

            try
            {
                var _query = (
                                from _history in this.db.POSTableStatusHistories
                                where _history.FloorNmbr == _prmFloorNmbr
                                    && _history.TableID == _prmTableID
                                    && (_history.StartTime <= _prmNow && _history.EndTime >= _prmNow)
                                    && _history.FloorType == _prmFloorType
                                    && _history.StillActive == true
                                orderby _history.StartTime descending
                                select _history
                             );

                if (_query.Count() > 0)
                {
                    _prmID = _query.FirstOrDefault().ID.ToString();
                    _result = true;
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        //public Boolean IsTableHistoryExistCafe(int _prmFloorNmbr, int _prmTableID, DateTime _prmNow, ref String _prmID)
        //{
        //    Boolean _result = false;

        //    try
        //    {
        //        var _query = (
        //                        from _history in this.db.POSTableStatusHistories
        //                        where _history.FloorNmbr == _prmFloorNmbr
        //                            && _history.TableID == _prmTableID
        //                            && (_history.StartTime <= _prmNow && _history.EndTime >= _prmNow)
        //                            && _history.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
        //                        orderby _history.StartTime descending
        //                        select _history
        //                     );

        //        if (_query.Count() > 0)
        //        {
        //            _prmID = _query.FirstOrDefault().ID.ToString();
        //            _result = true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }

        //    return _result;
        //}

        public bool Edit(POSTableStatusHistory _prmStatusHistory)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {

            }

            return _result;
        }



        //public int RowsCountAllHistoryID()
        //{
        //    int _result = 0;

        //    var _query =
        //                (
        //                    from _statusHistory in this.db.POSTableStatusHistories
        //                    where _statusHistory.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
        //                    orderby _statusHistory.ID descending
        //                    select _statusHistory.ID
        //                ).Count();

        //    _result = _query;

        //    return _result;
        //}

        public int GetMaxIDStatusHistory()
        {
            int _result = 0;
            try
            {
                var _query = (from _statusHistory in this.db.POSTableStatusHistories
                              //where _statusHistory.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                              //orderby _statusHistory.ID descending
                              select _statusHistory.ID
                            ).Count();
                _result = _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        ~POSTableStatusHistoryBL()
        {
        }
    }
}
