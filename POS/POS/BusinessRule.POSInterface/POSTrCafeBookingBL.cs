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
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using BusinessRule.POS;

namespace BusinessRule.POSInterface
{
    public sealed class POSTrCafeBookingBL : Base
    {
        public POSTrCafeBookingBL()
        {
        }

        protected POSConfigurationBL _pOSConfigurationBL = new POSConfigurationBL();
    
        #region Cafe Booking

        public List<POSTrCafeBookingDt> GetList()
        {
            List<POSTrCafeBookingDt> _result = new List<POSTrCafeBookingDt>();
            try
            {

                //String _before = new CompanyConfig().GetSingle(CompanyConfigure.POSBookingTimeLimitBefore).SetValue;
                //String _after = new CompanyConfig().GetSingle(CompanyConfigure.POSBookingTimeLimitAfter).SetValue;
                CompanyConfiguration _setValue = null;
                _setValue =  this._pOSConfigurationBL.GetSingle("POSBookingCafeTimeLimitBefore");
                String _before = _setValue.SetValue;
                
                _setValue = this._pOSConfigurationBL.GetSingle("POSBookingCafeTimeLimitAfter");
                String _after = _setValue.SetValue;
                
                int _timeLimitBefore = (_before == "") ? 0 : Convert.ToInt32(_before);
                int _timeLimitAfter = (_after == "") ? 0 : Convert.ToInt32(_after);
                DateTime _now = DateTime.Now;

                int _timeMinuteBooking = 0;
                int _timeHourBooking = _now.Hour;
                if (_now.Minute - _timeLimitAfter >= 0)
                {
                    _timeMinuteBooking = _now.Minute - _timeLimitAfter;
                }
                else
                {
                    _timeMinuteBooking = 60 + (_now.Minute - _timeLimitAfter);
                    _timeHourBooking = _now.Hour - 1;
                }

                var _query = (
                    //from _trInternetBookingHd in this.db.POSTrCafeBookingHds
                                from _trCafeBookingDt in this.db.POSTrCafeBookingDts
                                //on _trInternetBookingHd.CafeBookCode equals _trCafeBookingDt.CafeBookCode
                                where (new DateTime(_trCafeBookingDt.BookingDate.Year, _trCafeBookingDt.BookingDate.Month, _trCafeBookingDt.BookingDate.Day, _trCafeBookingDt.StartTimeHour, _trCafeBookingDt.StartTimeMinute, 0)) >= (new DateTime(_now.Year, _now.Month, _now.Day, _timeHourBooking, _timeMinuteBooking, 0))
                                  && _trCafeBookingDt.FgActive == true
                                //&& !(
                                //      from _trBooking in this.db.POSTrInternetBookings
                                //      where (new DateTime(_trInternetBooking.BookingDate.Year, _trInternetBooking.BookingDate.Month, _trInternetBooking.BookingDate.Day, _trInternetBooking.StartTimeHour, _trInternetBooking.StartTimeMinute + _timeLimitAfter, 0)) <= _now
                                //      select _trBooking.HistoryID
                                //  ).Contains(_trInternetBooking.HistoryID)
                                orderby (new DateTime(_trCafeBookingDt.BookingDate.Year, _trCafeBookingDt.BookingDate.Month, _trCafeBookingDt.BookingDate.Day, _trCafeBookingDt.StartTimeHour, _trCafeBookingDt.StartTimeMinute, 0))
                                select _trCafeBookingDt
                            );

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<POSTrCafeBookingDt> GetListForDetail(string _prmCode)
        {
            List<POSTrCafeBookingDt> _result = new List<POSTrCafeBookingDt>();

            try
            {
                var _query = (
                                from _cafeBookingTableDt in this.db.POSTrCafeBookingDts
                                where _cafeBookingTableDt.CafeBookCode == _prmCode
                                select _cafeBookingTableDt
                                ).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSMsInternetTable> GetListUnReservedTable(DateTime _prmBookingDate, int _prmFloorNmbr, int _prmStartTimeHour, int _prmStartTimeMinutes, int _prmEndTimeHour, int _prmEndTimeMinutes)
        {
            List<POSMsInternetTable> _result = new List<POSMsInternetTable>();
            DateTime _startTime = new DateTime(_prmBookingDate.Year, _prmBookingDate.Month, _prmBookingDate.Day, _prmStartTimeHour, _prmStartTimeMinutes, 0);
            DateTime _endTime = new DateTime(_prmBookingDate.Year, _prmBookingDate.Month, _prmBookingDate.Day, _prmEndTimeHour, _prmEndTimeMinutes, 0);
            try
            {
                //var _qry = (
                //        from _internetTable in this.db.POSMsInternetTables
                //        where
                //            _internetTable.Status != 2
                //            &&
                //            _internetTable.FloorNmbr == _prmFloorNmbr
                //            &&
                //            !(from _cafeBookList in this.db.POSTrCafeBookingDts
                //              where _cafeBookList.FloorNmbr == _prmFloorNmbr
                //                && _cafeBookList.FgActive == true
                //                && _cafeBookList.BookingDate == _prmBookingDate
                //                && (
                //                    (
                //                    (_prmStartTimeHour * 60 + _prmStartTimeMinutes) >= (_cafeBookList.StartTimeHour * 60 + _cafeBookList.StartTimeMinute) && (_prmStartTimeHour * 60 + _prmStartTimeMinutes) <= (_cafeBookList.EndTimeHour * 60 + _cafeBookList.EndTimeMinute)
                //                    )
                //                    ||
                //                    (
                //                    (_prmEndTimeHour * 60 + _prmEndTimeMinutes) >= (_cafeBookList.StartTimeHour * 60 + _cafeBookList.StartTimeMinute) && (_prmEndTimeHour * 60 + _prmEndTimeMinutes) <= (_cafeBookList.EndTimeHour * 60 + _cafeBookList.EndTimeMinute)
                //                    )
                //                )
                //              select _cafeBookList.TableIDPerRoom
                //            ).Contains(_internetTable.TableIDPerRoom)
                //        select _internetTable
                //    );
                var _qry = (
                        from _internetTable in this.db.POSMsInternetTables
                        where
                            _internetTable.Status != 2
                            &&
                            _internetTable.FloorNmbr == _prmFloorNmbr
                            &&
                            _internetTable.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                            &&
                            !(from _cafeBookList in this.db.POSTrCafeBookingDts
                              where _cafeBookList.FloorNmbr == _prmFloorNmbr
                                && _cafeBookList.FgActive == true
                                && (
                                    (_startTime >= new DateTime(_cafeBookList.BookingDate.Year, _cafeBookList.BookingDate.Month, _cafeBookList.BookingDate.Day, _cafeBookList.StartTimeHour, _cafeBookList.StartTimeMinute, 0)
                                    && _startTime <= new DateTime(_cafeBookList.BookingDate.Year, _cafeBookList.BookingDate.Month, _cafeBookList.BookingDate.Day, _cafeBookList.EndTimeHour, _cafeBookList.EndTimeMinute, 0))
                                    ||
                                    (_endTime >= new DateTime(_cafeBookList.BookingDate.Year, _cafeBookList.BookingDate.Month, _cafeBookList.BookingDate.Day, _cafeBookList.StartTimeHour, _cafeBookList.StartTimeMinute, 0)
                                    && _endTime <= new DateTime(_cafeBookList.BookingDate.Year, _cafeBookList.BookingDate.Month, _cafeBookList.BookingDate.Day, _cafeBookList.EndTimeHour, _cafeBookList.EndTimeMinute, 0))
                                    )
                              //&& _internetBookList.StartTimeHour >= _prmStartTimeHour
                              //&& _internetBookList.StartTimeMinute >= _prmStartTimeMinutes
                              //&& _internetBookList.EndTimeHour <= _prmEndTimeHour
                              //&& _internetBookList.EndTimeMinute <= _prmEndTimeMinutes

                              //&& _internetBookList.BookingDate == _prmBookingDate
                              //&& (
                              //    (
                              //    (_prmStartTimeHour * 60 + _prmStartTimeMinutes) >= (_internetBookList.StartTimeHour * 60 + _internetBookList.StartTimeMinute) && (_prmStartTimeHour * 60 + _prmStartTimeMinutes) <= (_internetBookList.EndTimeHour * 60 + _internetBookList.EndTimeMinute)
                              //    )
                              //    ||
                              //    (
                              //    (_prmEndTimeHour * 60 + _prmEndTimeMinutes) >= (_internetBookList.StartTimeHour * 60 + _internetBookList.StartTimeMinute) && (_prmEndTimeHour * 60 + _prmEndTimeMinutes) <= (_internetBookList.EndTimeHour * 60 + _internetBookList.EndTimeMinute)
                              //    )
                              //)
                              select _cafeBookList.TableIDPerRoom
                            ).Contains(_internetTable.TableIDPerRoom)
                        select _internetTable
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

        public List<POSMsInternetTable> GetListUnReservedTableJoinTable(DateTime _prmBookingDate, int _prmFloorNmbr, int _prmStartTimeHour, int _prmStartTimeMinutes, int _prmEndTimeHour, int _prmEndTimeMinutes)
        {
            List<POSMsInternetTable> _result = new List<POSMsInternetTable>();
            DateTime _startTime = new DateTime(_prmBookingDate.Year, _prmBookingDate.Month, _prmBookingDate.Day, _prmStartTimeHour, _prmStartTimeMinutes, 0);
            DateTime _endTime = new DateTime(_prmBookingDate.Year, _prmBookingDate.Month, _prmBookingDate.Day, _prmEndTimeHour, _prmEndTimeMinutes, 0);
            try
            {
                var _qry = (
                        from _internetTable in this.db.POSMsInternetTables
                        where
                            _internetTable.Status != 2
                            &&
                            _internetTable.FloorNmbr == _prmFloorNmbr
                            &&
                            _internetTable.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                            &&
                            //!(from _cafeBookList in this.db.POSTrCafeBookingDts
                            //  where _cafeBookList.FloorNmbr == _prmFloorNmbr
                            //    && _cafeBookList.FgActive == true
                            //    && _cafeBookList.BookingDate == _prmBookingDate
                            //    && (
                            //        (
                            //        (_prmStartTimeHour * 60 + _prmStartTimeMinutes) >= (_cafeBookList.StartTimeHour * 60 + _cafeBookList.StartTimeMinute) && (_prmStartTimeHour * 60 + _prmStartTimeMinutes) <= (_cafeBookList.EndTimeHour * 60 + _cafeBookList.EndTimeMinute)
                            //        )
                            //        ||
                            //        (
                            //        (_prmEndTimeHour * 60 + _prmEndTimeMinutes) >= (_cafeBookList.StartTimeHour * 60 + _cafeBookList.StartTimeMinute) && (_prmEndTimeHour * 60 + _prmEndTimeMinutes) <= (_cafeBookList.EndTimeHour * 60 + _cafeBookList.EndTimeMinute)
                            //        )
                            //      )
                            !(from _cafeBookList in this.db.POSTrCafeBookingDts
                              where _cafeBookList.FloorNmbr == _prmFloorNmbr
                                && _cafeBookList.FgActive == true
                                && (
                                    (_startTime >= new DateTime(_cafeBookList.BookingDate.Year, _cafeBookList.BookingDate.Month, _cafeBookList.BookingDate.Day, _cafeBookList.StartTimeHour, _cafeBookList.StartTimeMinute, 0)
                                    && _startTime <= new DateTime(_cafeBookList.BookingDate.Year, _cafeBookList.BookingDate.Month, _cafeBookList.BookingDate.Day, _cafeBookList.EndTimeHour, _cafeBookList.EndTimeMinute, 0))
                                    ||
                                    (_endTime >= new DateTime(_cafeBookList.BookingDate.Year, _cafeBookList.BookingDate.Month, _cafeBookList.BookingDate.Day, _cafeBookList.StartTimeHour, _cafeBookList.StartTimeMinute, 0)
                                    && _endTime <= new DateTime(_cafeBookList.BookingDate.Year, _cafeBookList.BookingDate.Month, _cafeBookList.BookingDate.Day, _cafeBookList.EndTimeHour, _cafeBookList.EndTimeMinute, 0))
                                    )
                              select _cafeBookList.TableIDPerRoom
                            ).Contains(_internetTable.TableIDPerRoom)
                        select _internetTable
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

        public List<POSTrCafeBookingDt> GetListReservedTable(DateTime _prmBookingDate, bool _prmFgActive, int _prmFloorNmbr, int _prmStartTimeHour, int _prmStartTimeMinutes, int _prmEndTimeHour, int _prmEndTimeMinutes)
        {
            List<POSTrCafeBookingDt> _result = new List<POSTrCafeBookingDt>();
            try
            {
                String _newSearch = _prmBookingDate.Year + "-" + _prmBookingDate.Month + "-" + _prmBookingDate.Day;
                DateTime _startTime = new DateTime(_prmBookingDate.Year, _prmBookingDate.Month, _prmBookingDate.Day, 0, 0, 0);
                //DateTime _endTime = new DateTime(_prmBookingDate.Year, _prmBookingDate.Month, _prmBookingDate.Day, _prmEndTimeHour, _prmEndTimeMinutes, 00);

                var _qry = (from _cafeBookList in this.db.POSTrCafeBookingDts
                            where _cafeBookList.FloorNmbr == _prmFloorNmbr
                              && _cafeBookList.BookingDate == _prmBookingDate
                              && _cafeBookList.FgActive == _prmFgActive
                            //&&
                            //(
                            //    (
                            //      (_prmStartTimeHour * 60 + _prmStartTimeMinutes) >= (_cafeBookList.StartTimeHour * 60 + _cafeBookList.StartTimeMinute) && (_prmStartTimeHour * 60 + _prmStartTimeMinutes) <= (_cafeBookList.EndTimeHour * 60 + _cafeBookList.EndTimeMinute)
                            //    )
                            //    ||
                            //    (
                            //      (_prmEndTimeHour * 60 + _prmEndTimeMinutes) >= (_cafeBookList.StartTimeHour * 60 + _cafeBookList.StartTimeMinute) && (_prmEndTimeHour * 60 + _prmEndTimeMinutes) <= (_cafeBookList.EndTimeHour * 60 + _cafeBookList.EndTimeMinute)
                            //    )
                            //)
                            //(

                            //(
                            //   ((_prmEndTimeHour >= _cafeBookList.StartTimeHour
                            //   && _prmEndTimeMinutes >= _cafeBookList.StartTimeMinute)
                            //   && (_cafeBookList.StartTimeHour >= _prmStartTimeHour
                            //   && _cafeBookList.StartTimeMinute >= _prmStartTimeMinutes))
                            //|| ((_prmStartTimeHour <= _cafeBookList.EndTimeHour
                            //   && _prmStartTimeMinutes <= _cafeBookList.EndTimeMinute)
                            //   && (_cafeBookList.EndTimeHour <= _prmEndTimeHour
                            //   && _cafeBookList.EndTimeMinute <= _prmEndTimeMinutes))
                            //   )
                            //(
                            //    _startTime <= new DateTime(_cafeBookList.BookingDate.Year, _cafeBookList.BookingDate.Month, _cafeBookList.BookingDate.Day, _cafeBookList.StartTimeHour, _cafeBookList.StartTimeMinute, 00)
                            //&&
                            //    _endTime >= new DateTime(_cafeBookList.BookingDate.Year, _cafeBookList.BookingDate.Month, _cafeBookList.BookingDate.Day, _cafeBookList.StartTimeHour, _cafeBookList.StartTimeMinute, 00)
                            //)
                            //||
                            //(
                            //    _startTime <= new DateTime(_cafeBookList.BookingDate.Year, _cafeBookList.BookingDate.Month, _cafeBookList.BookingDate.Day, _cafeBookList.EndTimeHour, _cafeBookList.EndTimeMinute, 00)
                            //&&
                            //    _endTime >= new DateTime(_cafeBookList.BookingDate.Year, _cafeBookList.BookingDate.Month, _cafeBookList.BookingDate.Day, _cafeBookList.EndTimeHour, _cafeBookList.EndTimeMinute, 00)
                            //)
                            //
                            //)
                            //orderby _cafeBookList.BookingDate descending
                            orderby _cafeBookList.BookingDate descending, _cafeBookList.StartTimeHour descending, _cafeBookList.StartTimeMinute descending
                            select _cafeBookList);
                foreach (var _row in _qry)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrCafeBookingDt> GetListBooking(String _prmMemberCode, String _prmMembername, String _prmPhone1, bool _prmFgActive)
        {
            List<POSTrCafeBookingDt> _result = new List<POSTrCafeBookingDt>();

            try
            {
                var _query = (
                                from _CafeBookingDt in this.db.POSTrCafeBookingDts
                                join _cafeBookingHd in this.db.POSTrCafeBookingHds

                               on _CafeBookingDt.CafeBookCode equals _cafeBookingHd.CafeBookCode
                                where _cafeBookingHd.MemberCode == _prmMemberCode
                                    && _cafeBookingHd.CustName == _prmMembername
                                    && _cafeBookingHd.PhoneNumber1 == _prmPhone1
                                    && _CafeBookingDt.FgActive == _prmFgActive
                                orderby _CafeBookingDt.BookingDate descending
                                select _CafeBookingDt);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }

            }
            catch (Exception)
            {
            }

            return _result;
        }

        public List<POSMsInternetFloor> GetListInternetFloor()
        {
            List<POSMsInternetFloor> _result = new List<POSMsInternetFloor>();
            try
            {
                var _qry = (from _internetFloor in this.db.POSMsInternetFloors
                            where _internetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                            select _internetFloor
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

        public POSTrCafeBookingDt GetSingleBookingDt(String _prmCode)
        {
            POSTrCafeBookingDt _result = null;
            string[] _tempSplit = _prmCode.Split('-');
            try
            {
                _result = this.db.POSTrCafeBookingDts.Single(_temp => _temp.CafeBookCode == _tempSplit[0] && _temp.ItemNo == Convert.ToInt32(_tempSplit[1]));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public POSTrCafeBookingHd GetSingleBookingHd(String _prmCode)
        {
            POSTrCafeBookingHd _result = null;
            try
            {
                _result = this.db.POSTrCafeBookingHds.Single(_temp => _temp.CafeBookCode == _prmCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public POSTrCafeBookingDt GetSingleForStatus(String _prmCode)
        {
            POSTrCafeBookingDt _result = null;
            string[] _splitTemp = _prmCode.Split('-');
            try
            {
                _result = this.db.POSTrCafeBookingDts.Single(_temp => _temp.HistoryID == Convert.ToInt32(_splitTemp[0]) && _temp.CafeBookCode.Trim().ToLower() == _splitTemp[1].Trim().ToLower() && _temp.ItemNo == Convert.ToInt32(_splitTemp[2]));
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return _result;
        }

        public String GetMemberCode(String _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _posTrCafeHd in this.db.POSTrCafeBookingHds
                                where _posTrCafeHd.CafeBookCode == _prmCode
                                //&& _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                                select new
                                {
                                    MemberCode = _posTrCafeHd.MemberCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.MemberCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetCustName(String _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _posTrCafeHd in this.db.POSTrCafeBookingHds
                                where _posTrCafeHd.CafeBookCode == _prmCode
                                //&& _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                                select new
                                {
                                    CustName = _posTrCafeHd.CustName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CustName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetPhoneNmbr(String _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _posTrCafeHd in this.db.POSTrCafeBookingHds
                                where _posTrCafeHd.CafeBookCode == _prmCode
                                //&& _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                                select new
                                {
                                    PhoneNumber1 = _posTrCafeHd.PhoneNumber1
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.PhoneNumber1;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetOperatorName(String _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _posTrCafeHd in this.db.POSTrCafeBookingHds
                                where _posTrCafeHd.CafeBookCode == _prmCode
                                //&& _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                                select new
                                {
                                    OperatorName = _posTrCafeHd.OperatorName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.OperatorName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public String GetTableNumberLabel(int _prmFloorNmbr, int? _prmIDTablePerRoom, string _prmFloorType)
        public String GetTableNumberLabel(int _prmFloorNmbr, int? _prmIDTablePerRoom)
        {
            String _result = "";
            try
            {
                //_result = this.db.POSMsInternetTables.Single(a => a.FloorNmbr == _prmFloorNmbr && a.TableIDPerRoom == _prmIDTablePerRoom && a.FloorType == _prmFloorType.Trim().ToLower()).TableNmbr;
                _result = this.db.POSMsInternetTables.Single(a => a.FloorNmbr == _prmFloorNmbr && a.TableIDPerRoom == _prmIDTablePerRoom && a.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)).TableNmbr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public double RowsCountAllHistoryID()
        {
            double _result = 0;

            var _query =
                        (
                            from _cafeBooking in this.db.POSTrCafeBookingDts
                            orderby _cafeBooking.CafeBookCode descending
                            select _cafeBooking.HistoryID
                        ).Count();

            _result = _query;

            return _result;
        }

        public int GetMax()
        {
            int _result = 0;

            var _query =
                        (
                            from _statusHistory in this.db.POSTrCafeBookingDts
                            orderby _statusHistory.ItemNo descending
                            select _statusHistory.ItemNo
                        ).Count();

            _result = _query;

            return _result;
        }

        public Boolean AddTrBooking(POSTrCafeBookingHd _prmNewDataHd, List<POSTrCafeBookingDt> _prmNewDataDt, List<POSTableStatusHistory> _prmHistory)
        {
            Boolean _result = false;

            try
            {
                if (_prmNewDataHd.CafeBookCode == "" || _prmNewDataHd.CafeBookCode == null)
                {
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmNewDataHd.CafeBookCode = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    foreach (var _row in _prmNewDataDt)
                    {
                        _row.CafeBookCode = _prmNewDataHd.CafeBookCode;
                    }

                    this.db.POSTrCafeBookingHds.InsertOnSubmit(_prmNewDataHd);
                    this.db.POSTrCafeBookingDts.InsertAllOnSubmit(_prmNewDataDt);
                    this.db.POSTableStatusHistories.InsertAllOnSubmit(_prmHistory);

                    var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                    this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);
                }

                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public void Cancel(String _prmBookingCode)
        {
            this.db.POSTrCafeBookingDts.DeleteOnSubmit(this.db.POSTrCafeBookingDts.Single(a => a.CafeBookCode == _prmBookingCode));
            this.db.SubmitChanges();
        }

        public void CancelAll(DateTime _prmBookingDate, int _prmFloorNmbr, int _prmStartTimeHour, int _prmStartTimeMinutes, int _prmEndTimeHour, int _prmEndTimeMinutes)
        {
            try
            {
                var _qry = (from _cafeBookList in this.db.POSTrCafeBookingDts
                            where _cafeBookList.FloorNmbr == _prmFloorNmbr
                              && _cafeBookList.BookingDate == _prmBookingDate
                              && (
                                  (
                                  (_prmStartTimeHour * 60 + _prmStartTimeMinutes) >= (_cafeBookList.StartTimeHour * 60 + _cafeBookList.StartTimeMinute) && (_prmStartTimeHour * 60 + _prmStartTimeMinutes) <= (_cafeBookList.EndTimeHour * 60 + _cafeBookList.EndTimeMinute)
                                  )
                                  ||
                                  (
                                  (_prmEndTimeHour * 60 + _prmEndTimeMinutes) >= (_cafeBookList.StartTimeHour * 60 + _cafeBookList.StartTimeMinute) && (_prmEndTimeHour * 60 + _prmEndTimeMinutes) <= (_cafeBookList.EndTimeHour * 60 + _cafeBookList.EndTimeMinute)
                                  )
                              )
                            select _cafeBookList);
                if (_qry.Count() > 0)
                {
                    foreach (var _row in _qry)
                    {
                        POSTrCafeBookingDt _deleteData = this.db.POSTrCafeBookingDts.FirstOrDefault(a => a.CafeBookCode == _row.CafeBookCode);
                        this.db.POSTrCafeBookingDts.DeleteOnSubmit(_deleteData);
                    }
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        public bool Edit(POSTrCafeBookingDt _prmStatusInternetBooking)
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
        #endregion

        ~POSTrCafeBookingBL()
        {
        }
    }
}
