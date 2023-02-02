using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using VTSWeb.Database;
using System.Diagnostics;
using System.Data.SqlClient;
using VTSWeb.SystemConfig;
using System.Data;
using System.Data.Linq.SqlClient;

namespace VTSWeb.BusinessRule
{
    public sealed class ClearanceBL : Base
    {
        public ClearanceBL()
        {
        }
        ~ClearanceBL()
        {
        }
        #region ClearanceHd

        public int RowsCount(String _prmCustCode)
        {
            int _result = 0;

            String _pattern = "%%";

            if (_prmCustCode != "")
            {
                _pattern = "%" + _prmCustCode + "%";
            }

            _result = (from _msClearanceHd in this.db.ClearanceHds
                       join _msCust in this.db.MsCustomers
                       on _msClearanceHd.CustomerCode equals _msCust.CustCode
                       where
                      (SqlMethods.Like(_msCust.CustCode.ToString().Trim().ToLower(), _pattern.Trim().ToLower()))

                       select _msClearanceHd.ClearanceCode).Count();
            return _result;
        }

        public List<ClearanceHd> GetList(int _prmReqPage, int _prmPageSize, String _prmCustCode)
        {
            List<ClearanceHd> _result = new List<ClearanceHd>();

            String _pattern = "%%";

            if (_prmCustCode != "")
            {
                _pattern = "%" + _prmCustCode + "%";
            }

            try
            {
                var _query = (
                                from _msClearanceHd in this.db.ClearanceHds
                                join _msCust in this.db.MsCustomers
                                on _msClearanceHd.CustomerCode equals _msCust.CustCode
                                where
                                (SqlMethods.Like(_msCust.CustCode.ToString().Trim().ToLower(), _pattern.Trim().ToLower()))
                                orderby _msClearanceHd.CustomerCode ascending
                                select new
                                {
                                    ClearanceCode = _msClearanceHd.ClearanceCode,
                                    ClearanceDate = _msClearanceHd.ClearanceDate,
                                    CustomerCode = _msClearanceHd.CustomerCode,
                                    CustName = (from _msCustomer in this.db.MsCustomers
                                                where _msClearanceHd.CustomerCode == _msCustomer.CustCode
                                                select _msCustomer.CustName).FirstOrDefault(),

                                    VisitorCode = _msClearanceHd.VisitorCode,
                                    ContactName = (from _msCustomerContact in this.db.MsCustContacts
                                                   where _msClearanceHd.CustomerCode == _msCustomerContact.CustCode
                                                   && _msClearanceHd.VisitorCode == _msCustomerContact.ItemNo
                                                   select _msCustomerContact.ContactName).FirstOrDefault(),
                                    Remark = _msClearanceHd.Remark,
                                    CompleteStatus = _msClearanceHd.CompleteStatus
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new ClearanceHd(_row.ClearanceCode, _row.ClearanceDate, _row.CustomerCode, _row.CustName, _row.VisitorCode, _row.ContactName, _row.Remark, _row.CompleteStatus));
                }
            }
            catch (Exception ex)
            {

            }

            return _result;
        }


        public List<ClearanceHd> GetList()
        {
            List<ClearanceHd> _result = new List<ClearanceHd>();

            try
            {
                var _query = (
                                from _msClearanceHd in this.db.ClearanceHds
                                orderby  _msClearanceHd.CustomerCode ascending
                                select new
                                {
                                    ClearanceCode = _msClearanceHd.ClearanceCode,
                                    ClearanceDate = _msClearanceHd.ClearanceDate,
                                    CustomerCode = _msClearanceHd.CustomerCode,
                                    CustName = (from _msCust in this.db.MsCustomers
                                                where _msClearanceHd.CustomerCode == _msCust.CustCode
                                                select _msCust.CustName).FirstOrDefault(),

                                    VisitorCode = _msClearanceHd.VisitorCode,
                                    ContactName = (from _msCustContact in this.db.MsCustContacts
                                                   where _msClearanceHd.CustomerCode == _msCustContact.CustCode
                                                   && _msClearanceHd.VisitorCode == _msCustContact.ItemNo
                                                   select _msCustContact.ContactName).FirstOrDefault(),
                                    Remark = _msClearanceHd.Remark,
                                    CompleteStatus = _msClearanceHd.CompleteStatus
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new ClearanceHd(_row.ClearanceCode, _row.ClearanceDate, _row.CustomerCode, _row.CustName, _row.VisitorCode,_row.ContactName, _row.Remark, _row.CompleteStatus));
                }
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public List<ClearanceHd> GetListClearanceForSearch(string _prmCustName, string _prmVisitCode, string _prmVisitName)
        {
            List<ClearanceHd> _result = new List<ClearanceHd>();

            try
            {
                var _query = (
                                from _msClearanceHd in this.db.ClearanceHds
                                join _msCustContact in this.db.MsCustContacts
                                    on _msClearanceHd.CustomerCode equals _msCustContact.CustCode
                                join _msCust in this.db.MsCustomers
                                    on _msClearanceHd.CustomerCode equals _msCust.CustCode
                                where (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _prmCustName.ToLower()))
                                    && (SqlMethods.Like(_msCustContact.ItemNo.ToString().ToLower(), _prmVisitCode.ToLower()))
                                    && (SqlMethods.Like(_msCustContact.ContactName.ToString().Trim().ToLower(), _prmVisitName.Trim().ToLower()))
                                    && _msClearanceHd.VisitorCode == _msCustContact.ItemNo
                                orderby _msClearanceHd.ClearanceCode ascending
                                select new
                                {
                                    ClearanceCode = _msClearanceHd.ClearanceCode,
                                    ClearanceDate = _msClearanceHd.ClearanceDate,
                                    CustomerCode = _msClearanceHd.CustomerCode,
                                    CustomerName = _msCust.CustName,
                                    VisitorCode = _msCustContact.ItemNo,
                                    VisitorName = _msCustContact.ContactName,
                                    Remark = _msClearanceHd.Remark,
                                    CompleteStatus = _msClearanceHd.CompleteStatus
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new ClearanceHd(_row.ClearanceCode, _row.ClearanceDate, _row.CustomerCode, _row.CustomerName, _row.VisitorCode, _row.VisitorName, _row.Remark, _row.CompleteStatus));
                }
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public ClearanceHd GetSingleClearance(string _prmCode)
        {
            ClearanceHd _result = null;

            try
            {
                _result = this.db.ClearanceHds.Single(_temp => _temp.ClearanceCode == _prmCode);
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    ClearanceHd _msClearanceHd = this.db.ClearanceHds.Single(_temp => _temp.ClearanceCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.ClearanceHds.DeleteOnSubmit(_msClearanceHd);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(ClearanceHd _prmClearanceHd)
        {
            bool _result = false;

            try
            {
                this.db.ClearanceHds.InsertOnSubmit(_prmClearanceHd);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddTemp(ClearanceHd _prmClearanceHd)
        {
            bool _result = false;

            try
            {
                this.db.ClearanceHds.InsertOnSubmit(_prmClearanceHd);
                //this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(ClearanceHd _prmClearanceHd)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditTemp(ClearanceHd _prmClearanceHd)
        {
            bool _result = false;

            try
            {
                //this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsClearanceCodeExist(string _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _msClearanceHd in this.db.ClearanceHds
                                where _msClearanceHd.ClearanceCode == _prmCode
                                select new
                                {
                                    ClearanceCode = _msClearanceHd.ClearanceCode
                                }
                              ).Count();

                if (_query > 0)
                {
                    _result = true;
                }
                else
                {
                    _result = false;
                }
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        #endregion

        #region ClearanceDt

        public int RowsCountDt(string _prmCode)
        {
            //get
            //{
            return this.db.ClearanceHds.Where(_temp => _temp.ClearanceCode.Trim().ToLower() == _prmCode.Trim().ToLower()).Count();
            //}
        }
        
        public List<ClearanceDt> GetListDt(string _prmCode)
        {
            List<ClearanceDt> _result = new List<ClearanceDt>();

            try
            {
                var _query = (
                                from _msClearanceDt in this.db.ClearanceDts
                                where _msClearanceDt.ClearanceCode.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _msClearanceDt.ClearanceCode,_msClearanceDt.AreaCode,_msClearanceDt.PurposeCode,_msClearanceDt.CheckIn descending
                                select new
                                {
                                    ClearanceCode = _msClearanceDt.ClearanceCode,
                                    AreaCode = _msClearanceDt.AreaCode,
                                    PurposeCode = _msClearanceDt.PurposeCode,
                                    CheckIn = _msClearanceDt.CheckIn,
                                    CheckOut = _msClearanceDt.CheckOut
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new ClearanceDt(_row.ClearanceCode, _row.AreaCode, _row.PurposeCode, _row.CheckIn, _row.CheckOut));
                }
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<ClearanceDt> GetListDtSearch(string _prmCode)
        {
            List<ClearanceDt> _result = new List<ClearanceDt>();

            try
            {
                var _query = (
                                from _msClearanceDt in this.db.ClearanceDts
                                where _msClearanceDt.ClearanceCode.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _msClearanceDt.ClearanceCode descending
                                select new
                                {
                                    ClearanceCode = _msClearanceDt.ClearanceCode,
                                    AreaCode = _msClearanceDt.AreaCode,
                                    AreaName = (from _msArea in this.db.MsAreas
                                                where _msArea.AreaCode == _msClearanceDt.AreaCode
                                                select _msArea.AreaName
                                                ).FirstOrDefault(),
                                    PurposeCode = _msClearanceDt.PurposeCode,
                                    PurposeName = (from _msPurpose in this.db.MsPurposes
                                                   where _msPurpose.PurposeCode == _msClearanceDt.PurposeCode
                                                   select _msPurpose.PurposeName
                                                ).FirstOrDefault(),
                                    DateIn = _msClearanceDt.CheckIn,
                                    TimeIn = _msClearanceDt.CheckIn.TimeOfDay,
                                    DateOut = _msClearanceDt.CheckOut,
                                    TimeOut = _msClearanceDt.CheckOut.TimeOfDay
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new ClearanceDt(_row.ClearanceCode, _row.AreaCode, _row.AreaName, _row.PurposeCode, _row.PurposeName, _row.DateIn, _row.TimeIn, _row.DateOut, _row.TimeOut));
                }
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        //String _prmCheckIn
        public ClearanceDt GetSingleDt(String _prmCode, String _prmArea, String _prmPurpost, String _prmCheckIn)
        {
            ClearanceDt _result = null;

            try
            {
                _result = this.db.ClearanceDts.Single(_temp => _temp.ClearanceCode.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.AreaCode.Trim().ToLower() == _prmArea.Trim().ToLower() && _temp.PurposeCode.Trim().ToLower() == _prmPurpost.Trim().ToLower() && _temp.CheckIn == Convert.ToDateTime(_prmCheckIn));
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiDt(String[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {

                    string[] _tempSplit = _prmCode[i].Split('-');
                    ClearanceDt _msClearance = this.db.ClearanceDts.Single(_temp => _temp.ClearanceCode.ToString() == _tempSplit[0].Trim().ToLower() && _temp.AreaCode.ToString() == _tempSplit[1].Trim().ToLower() && _temp.PurposeCode.ToString() == _tempSplit[2].Trim().ToLower());

                    this.db.ClearanceDts.DeleteOnSubmit(_msClearance);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }


        //public bool AddListDt(string _prmClearanceCode, DataGridView _prmDataGrid)
        //{
        //    bool _result = false;

        //    List<ClearanceDt> _listResult = new List<ClearanceDt>();

        //    try
        //    {
        //        for (int i = 0; i < _prmDataGrid.Rows.Count - 1; i++)
        //        {
        //            ClearanceDt _clearanceDt = new ClearanceDt();

        //            _clearanceDt.ClearanceCode = _prmDataGrid.Rows[i].Cells["ClearanceCode"].Value.ToString();
        //            _clearanceDt.AreaCode = _prmDataGrid.Rows[i].Cells["AreaCode"].Value.ToString();
        //            _clearanceDt.PurposeCode = _prmDataGrid.Rows[i].Cells["PurposeCode"].Value.ToString();

        //            DateTime _dateIn = Convert.ToDateTime(_prmDataGrid.Rows[i].Cells["DateIn"].Value.ToString());
        //            int _yearIn = _dateIn.Year;
        //            int _monthIn = _dateIn.Month;
        //            int _dayIn = _dateIn.Day;
        //            DateTime _timeIn = Convert.ToDateTime(_prmDataGrid.Rows[i].Cells["TimeIn"].Value.ToString());
        //            int _hourIn = _timeIn.Hour;
        //            int _minuteIn = _timeIn.Minute;
        //            int _secondIn = _timeIn.Second;

        //            DateTime _checkIn = new DateTime(_yearIn, _monthIn, _dayIn, _hourIn, _minuteIn, _secondIn);
        //            _clearanceDt.CheckIn = _checkIn;

        //            DateTime _dateOut = Convert.ToDateTime(_prmDataGrid.Rows[i].Cells["DateOut"].Value.ToString());
        //            int _yearOut = _dateOut.Year;
        //            int _monthOut = _dateOut.Month;
        //            int _dayOut = _dateOut.Day;
        //            DateTime _timeOut = Convert.ToDateTime(_prmDataGrid.Rows[i].Cells["TimeOut"].Value.ToString());
        //            int _hourOut = _timeOut.Hour;
        //            int _minuteOut = _timeOut.Minute;
        //            int _secondOut = _timeOut.Second;

        //            DateTime _checkOut = new DateTime(_yearOut, _monthOut, _dayOut, _hourOut, _minuteOut, _secondOut);
        //            _clearanceDt.CheckOut = _checkOut;

        //            _listResult.Add(_clearanceDt);
        //        }

        //        for (int i = 0; i < _prmDataGrid.Rows.Count - 1; i++)
        //        {
        //            ClearanceDt _clearanceDt = new ClearanceDt();

        //            _clearanceDt.ClearanceCode = _prmClearanceCode;

        //            foreach (var item in _listResult)
        //            {
        //                if (_clearanceDt.ClearanceCode == item.ClearanceCode)
        //                {
        //                    this.db.ClearanceDts.InsertOnSubmit(item);

        //                    _result = true;
        //                }
        //            }

        //        }
        //        this.db.SubmitChanges();
        //    }
        //    catch (Exception Ex)
        //    {
        //    }

        //    return _result;
        //}

        public bool AddDt(ClearanceDt _prmClearanceDt)
        {
            bool _result = false;

            try
            {
                this.db.ClearanceDts.InsertOnSubmit(_prmClearanceDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditDt(ClearanceDt _prmClearanceDt)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool SubmitChanges()
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsDetailExist(string _prmCode, string _prmAreaCode, string _prmPurposeCode, DateTime _prmCheckIn)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _msClearanceDt in this.db.ClearanceDts
                                where _msClearanceDt.ClearanceCode.Trim().ToLower() == _prmCode.Trim().ToLower()
                                    && _msClearanceDt.AreaCode.Trim().ToLower() == _prmAreaCode.Trim().ToLower()
                                    && _msClearanceDt.PurposeCode.Trim().ToLower() == _prmPurposeCode.Trim().ToLower()
                                    && _msClearanceDt.CheckIn == _prmCheckIn
                                select new
                                {
                                    ClearanceCode = _msClearanceDt.ClearanceCode
                                }
                              ).Count();

                if (_query > 0)
                {
                    _result = true;
                }
                else
                {
                    _result = false;
                }
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        #endregion


        
    }

}
