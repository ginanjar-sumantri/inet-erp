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

namespace BusinessRule.POSInterface
{
    public sealed class POSTrInternetBookingBL : Base
    {
        public POSTrInternetBookingBL()
        {
        }

        #region InternetBooking

        public List<POSTrInternetBookingDt> GetList()
        {
            List<POSTrInternetBookingDt> _result = new List<POSTrInternetBookingDt>();

            try
            {
                String _before = new CompanyConfig().GetSingle(CompanyConfigure.POSBookingTimeLimitBefore).SetValue;
                String _after = new CompanyConfig().GetSingle(CompanyConfigure.POSBookingTimeLimitAfter).SetValue;
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
                    //from _trInternetBookingHd in this.db.POSTrInternetBookingHds
                                from _trInternetBookingDt in this.db.POSTrInternetBookingDts
                                //on _trInternetBookingHd.InternetBookCode equals _trInternetBookingDt.InternetBookCode

                                //where (new DateTime(_trInternetBookingDt.BookingDate.Year, _trInternetBookingDt.BookingDate.Month, _trInternetBookingDt.BookingDate.Day, _trInternetBookingDt.StartTimeHour, _trInternetBookingDt.StartTimeMinute + _timeLimitAfter, 0)) >= _now
                                //  && _trInternetBookingDt.FgActive == true
                                where (new DateTime(_trInternetBookingDt.BookingDate.Year, _trInternetBookingDt.BookingDate.Month, _trInternetBookingDt.BookingDate.Day, _trInternetBookingDt.StartTimeHour, _trInternetBookingDt.StartTimeMinute, 0)) >= (new DateTime(_now.Year, _now.Month, _now.Day, _timeHourBooking, _timeMinuteBooking, 0))
                                  && _trInternetBookingDt.FgActive == true
                                
                                //&& !(
                                //      from _trBooking in this.db.POSTrInternetBookings
                                //      where (new DateTime(_trInternetBooking.BookingDate.Year, _trInternetBooking.BookingDate.Month, _trInternetBooking.BookingDate.Day, _trInternetBooking.StartTimeHour, _trInternetBooking.StartTimeMinute + _timeLimitAfter, 0)) <= _now
                                //      select _trBooking.HistoryID
                                //  ).Contains(_trInternetBooking.HistoryID)

                                orderby (new DateTime(_trInternetBookingDt.BookingDate.Year, _trInternetBookingDt.BookingDate.Month, _trInternetBookingDt.BookingDate.Day, _trInternetBookingDt.StartTimeHour, _trInternetBookingDt.StartTimeMinute, 0))
                                select _trInternetBookingDt
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

        public List<POSTrInternetBookingDt> GetListForDetail(string _prmCode)
        {
            List<POSTrInternetBookingDt> _result = new List<POSTrInternetBookingDt>();

            try
            {
                var _query = (
                                from _internetBookingTableDt in this.db.POSTrInternetBookingDts
                                where _internetBookingTableDt.InternetBookCode == _prmCode
                                select _internetBookingTableDt
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
                var _qry = (
                        from _internetTable in this.db.POSMsInternetTables
                        where
                            _internetTable.Status != 2
                            &&
                            _internetTable.FloorNmbr == _prmFloorNmbr
                            &&
                            _internetTable.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                            &&
                            !(from _internetBookList in this.db.POSTrInternetBookingDts
                              where _internetBookList.FloorNmbr == _prmFloorNmbr
                                && _internetBookList.FgActive == true
                                && (
                                    (_startTime >= new DateTime(_internetBookList.BookingDate.Year, _internetBookList.BookingDate.Month, _internetBookList.BookingDate.Day, _internetBookList.StartTimeHour, _internetBookList.StartTimeMinute, 0)
                                    && _startTime <= new DateTime(_internetBookList.BookingDate.Year, _internetBookList.BookingDate.Month, _internetBookList.BookingDate.Day, _internetBookList.EndTimeHour, _internetBookList.EndTimeMinute, 0))
                                    ||
                                    (_endTime >= new DateTime(_internetBookList.BookingDate.Year, _internetBookList.BookingDate.Month, _internetBookList.BookingDate.Day, _internetBookList.StartTimeHour, _internetBookList.StartTimeMinute, 0)
                                    && _endTime <= new DateTime(_internetBookList.BookingDate.Year, _internetBookList.BookingDate.Month, _internetBookList.BookingDate.Day, _internetBookList.EndTimeHour, _internetBookList.EndTimeMinute, 0))
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
                              select _internetBookList.TableIDPerRoom
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

        public List<POSTrInternetBookingDt> GetListReservedTable(DateTime _prmBookingDate, bool _prmFgActive, int _prmFloorNmbr, int _prmStartTimeHour, int _prmStartTimeMinutes, int _prmEndTimeHour, int _prmEndTimeMinutes)
        {
            List<POSTrInternetBookingDt> _result = new List<POSTrInternetBookingDt>();
            try
            {
                String _newSearch = _prmBookingDate.Year + "-" + _prmBookingDate.Month + "-" + _prmBookingDate.Day;
                DateTime _startTime = new DateTime(_prmBookingDate.Year, _prmBookingDate.Month, _prmBookingDate.Day, 0, 0, 0);
                //DateTime _endTime = new DateTime(_prmBookingDate.Year, _prmBookingDate.Month, _prmBookingDate.Day, _prmEndTimeHour, _prmEndTimeMinutes, 00);

                var _qry = (from _internetBookList in this.db.POSTrInternetBookingDts
                            where _internetBookList.FloorNmbr == _prmFloorNmbr
                              && _internetBookList.BookingDate == _startTime
                              && _internetBookList.FgActive == _prmFgActive
                            //&& _internetBookList.StartTimeHour >= _prmStartTimeHour
                            //&& _internetBookList.EndTimeHour <= _prmEndTimeHour

                            //(
                            //    (
                            //      (_prmStartTimeHour * 60 + _prmStartTimeMinutes) >= (_internetBookList.StartTimeHour * 60 + _internetBookList.StartTimeMinute) && (_prmStartTimeHour * 60 + _prmStartTimeMinutes) <= (_internetBookList.EndTimeHour * 60 + _internetBookList.EndTimeMinute)
                            //    )
                            //    ||
                            //    (
                            //      (_prmEndTimeHour * 60 + _prmEndTimeMinutes) >= (_internetBookList.StartTimeHour * 60 + _internetBookList.StartTimeMinute) && (_prmEndTimeHour * 60 + _prmEndTimeMinutes) <= (_internetBookList.EndTimeHour * 60 + _internetBookList.EndTimeMinute)
                            //    )
                            //)
                            //(

                            //(
                            //   ((_prmEndTimeHour >= _internetBookList.StartTimeHour
                            //   && _prmEndTimeMinutes >= _internetBookList.StartTimeMinute)
                            //   && (_internetBookList.StartTimeHour >= _prmStartTimeHour
                            //   && _internetBookList.StartTimeMinute >= _prmStartTimeMinutes))
                            //|| ((_prmStartTimeHour <= _internetBookList.EndTimeHour
                            //   && _prmStartTimeMinutes <= _internetBookList.EndTimeMinute)
                            //   && (_internetBookList.EndTimeHour <= _prmEndTimeHour
                            //   && _internetBookList.EndTimeMinute <= _prmEndTimeMinutes))
                            //   )
                            //(
                            //    _startTime <= new DateTime(_internetBookList.BookingDate.Year, _internetBookList.BookingDate.Month, _internetBookList.BookingDate.Day, _internetBookList.StartTimeHour, _internetBookList.StartTimeMinute, 00)
                            //&&
                            //    _endTime >= new DateTime(_internetBookList.BookingDate.Year, _internetBookList.BookingDate.Month, _internetBookList.BookingDate.Day, _internetBookList.StartTimeHour, _internetBookList.StartTimeMinute, 00)
                            //)
                            //||
                            //(
                            //    _startTime <= new DateTime(_internetBookList.BookingDate.Year, _internetBookList.BookingDate.Month, _internetBookList.BookingDate.Day, _internetBookList.EndTimeHour, _internetBookList.EndTimeMinute, 00)
                            //&&
                            //    _endTime >= new DateTime(_internetBookList.BookingDate.Year, _internetBookList.BookingDate.Month, _internetBookList.BookingDate.Day, _internetBookList.EndTimeHour, _internetBookList.EndTimeMinute, 00)
                            //)
                            //)

                            orderby _internetBookList.BookingDate descending, _internetBookList.StartTimeHour descending, _internetBookList.StartTimeMinute descending
                            select _internetBookList);
                foreach (var _row in _qry)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<POSTrInternetBookingDt> GetListBooking(String _prmMemberCode, String _prmMembername, String _prmPhone1, bool _prmFgActive)
        {
            List<POSTrInternetBookingDt> _result = new List<POSTrInternetBookingDt>();

            try
            {
                var _query = (
                                from _internetBookingDt in this.db.POSTrInternetBookingDts
                                join _internetBookingHd in this.db.POSTrInternetBookingHds

                               on _internetBookingDt.InternetBookCode equals _internetBookingHd.InternetBookCode
                                where _internetBookingHd.MemberCode == _prmMemberCode
                                    && _internetBookingHd.CustName == _prmMembername
                                    && _internetBookingHd.PhoneNumber1 == _prmPhone1
                                    && _internetBookingDt.FgActive == _prmFgActive
                                orderby _internetBookingDt.BookingDate descending
                                select _internetBookingDt);

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
                            where _internetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                            select _internetFloor);
                foreach (var _row in _qry)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public POSTrInternetBookingDt GetSingleBookingDt(String _prmCode)
        {
            POSTrInternetBookingDt _result = null;
            string[] _tempSplit = _prmCode.Split('-');
            try
            {
                _result = this.db.POSTrInternetBookingDts.FirstOrDefault(_temp => _temp.InternetBookCode == _tempSplit[0] && _temp.ItemNo == Convert.ToInt32(_tempSplit[1]));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public POSTrInternetBookingHd GetSingleBookingHd(String _prmCode)
        {
            POSTrInternetBookingHd _result = null;
            string[] _tempSplit = _prmCode.Split('-');
            try
            {
                _result = this.db.POSTrInternetBookingHds.FirstOrDefault(_temp => _temp.InternetBookCode == _tempSplit[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public POSTrInternetBookingDt GetSingleForStatus(String _prmCode)
        {
            POSTrInternetBookingDt _result = null;
            string[] _splitTemp = _prmCode.Split('-');
            try
            {
                _result = this.db.POSTrInternetBookingDts.FirstOrDefault(_temp => _temp.HistoryID == Convert.ToInt32(_splitTemp[0]) && _temp.InternetBookCode.Trim().ToLower() == _splitTemp[1].Trim().ToLower() && _temp.ItemNo == Convert.ToInt32(_splitTemp[2]));
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
                                from _posTrInternetHd in this.db.POSTrInternetBookingHds
                                where _posTrInternetHd.InternetBookCode == _prmCode
                                //&& _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                                select new
                                {
                                    MemberCode = _posTrInternetHd.MemberCode
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
                                from _posTrInternetHd in this.db.POSTrInternetBookingHds
                                where _posTrInternetHd.InternetBookCode == _prmCode
                                //&& _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                                select new
                                {
                                    CustName = _posTrInternetHd.CustName
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
                                from _posTrInternetHd in this.db.POSTrInternetBookingHds
                                where _posTrInternetHd.InternetBookCode == _prmCode
                                //&& _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                                select new
                                {
                                    PhoneNumber1 = _posTrInternetHd.PhoneNumber1
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
                                from _posTrInternetHd in this.db.POSTrInternetBookingHds
                                where _posTrInternetHd.InternetBookCode == _prmCode
                                //&& _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                                select new
                                {
                                    OperatorName = _posTrInternetHd.OperatorName
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

        public String GetTableNumberLabel(int _prmFloorNmbr, int? _prmIDTablePerRoom)
        {
            String _result = "";
            try
            {
                _result = this.db.POSMsInternetTables.Single(a => a.FloorNmbr == _prmFloorNmbr && a.TableIDPerRoom == _prmIDTablePerRoom && a.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)).TableNmbr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public double RowsCountAllHistoryID(string _prmcode)
        {
            double _result = 0;

            var _query =
                        (
                            from _InternetBooking in this.db.POSTrInternetBookingDts
                            orderby _InternetBooking.InternetBookCode descending
                            select _InternetBooking.HistoryID
                        ).Count();

            _result = _query;

            return _result;
        }

        //public int GetMax()
        //{
        //    int _result = 0;

        //    var _query =
        //                (
        //                    from _statusHistory in this.db.POSTrInternetBookingDts
        //                    where _statusHistory.InternetBookCode.Trim().ToLower() == 
        //                    select _statusHistory.ItemNo
        //                ).Count();

        //    _result = _query;

        //    return _result;
        //}

        public Boolean AddTrBooking(POSTrInternetBookingHd _prmNewDataHd, List<POSTrInternetBookingDt> _prmNewDataDt, List<POSTableStatusHistory> _prmHistory)
        {
            Boolean _result = false;

            try
            {
                if (_prmNewDataHd.InternetBookCode == "" || _prmNewDataHd.InternetBookCode == null)
                {
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmNewDataHd.InternetBookCode = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    foreach (var _row in _prmNewDataDt)
                    {
                        _row.InternetBookCode = _prmNewDataHd.InternetBookCode;
                    }

                    this.db.POSTrInternetBookingHds.InsertOnSubmit(_prmNewDataHd);
                    this.db.POSTrInternetBookingDts.InsertAllOnSubmit(_prmNewDataDt);
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
            this.db.POSTrInternetBookingDts.DeleteOnSubmit(this.db.POSTrInternetBookingDts.Single(a => a.InternetBookCode == _prmBookingCode));
            this.db.SubmitChanges();
        }

        public void CancelAll(DateTime _prmBookingDate, int _prmFloorNmbr, int _prmStartTimeHour, int _prmStartTimeMinutes, int _prmEndTimeHour, int _prmEndTimeMinutes)
        {
            try
            {
                var _qry = (from _internetBookList in this.db.POSTrInternetBookingDts
                            where _internetBookList.FloorNmbr == _prmFloorNmbr
                              && _internetBookList.BookingDate == _prmBookingDate
                              && (
                                  (
                                  (_prmStartTimeHour * 60 + _prmStartTimeMinutes) >= (_internetBookList.StartTimeHour * 60 + _internetBookList.StartTimeMinute) && (_prmStartTimeHour * 60 + _prmStartTimeMinutes) <= (_internetBookList.EndTimeHour * 60 + _internetBookList.EndTimeMinute)
                                  )
                                  ||
                                  (
                                  (_prmEndTimeHour * 60 + _prmEndTimeMinutes) >= (_internetBookList.StartTimeHour * 60 + _internetBookList.StartTimeMinute) && (_prmEndTimeHour * 60 + _prmEndTimeMinutes) <= (_internetBookList.EndTimeHour * 60 + _internetBookList.EndTimeMinute)
                                  )
                              )
                            select _internetBookList);
                if (_qry.Count() > 0)
                {
                    foreach (var _row in _qry)
                    {
                        POSTrInternetBookingDt _deleteData = this.db.POSTrInternetBookingDts.FirstOrDefault(a => a.InternetBookCode == _row.InternetBookCode);
                        this.db.POSTrInternetBookingDts.DeleteOnSubmit(_deleteData);
                    }
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public bool Edit(POSTrInternetBookingDt _prmStatusInternetBooking)
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

        ~POSTrInternetBookingBL()
        {
        }
    }
}
