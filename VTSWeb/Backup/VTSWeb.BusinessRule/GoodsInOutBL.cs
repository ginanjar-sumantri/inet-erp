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
    public sealed class GoodsInOutBL : Base
    {
        public GoodsInOutBL()
        {
        }
        ~GoodsInOutBL()
        {
        }
        #region GoodsInOutHd

        public int RowsCount(String _prmGoods, String _prmKeyword)
        {
            int _result = 0;

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmGoods == "CustCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmGoods == "TransType")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result = (from _msGoodsInOutHds in this.db.GoodsInOutHds
                       join _msCust in this.db.MsCustomers
                       on _msGoodsInOutHds.CustCode equals _msCust.CustCode
                       where (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msGoodsInOutHds.TransType.Trim().ToLower(), _pattern2.Trim().ToLower()))

                       select _msGoodsInOutHds.TransNmbr).Count();
            return _result;
        }



        public List<GoodsInOutHd> GetListHd()
        {
            List<GoodsInOutHd> _result = new List<GoodsInOutHd>();

            try
            {
                var _query = (
                                from _msGoodsInOutHd in this.db.GoodsInOutHds
                                orderby _msGoodsInOutHd.TransNmbr, _msGoodsInOutHd.CustCode, _msGoodsInOutHd.TransType ascending
                                select new
                                {
                                    TransNmbr = _msGoodsInOutHd.TransNmbr,
                                    FileNmbr = _msGoodsInOutHd.FileNmbr,
                                    TransType = _msGoodsInOutHd.TransType,
                                    CustCode = _msGoodsInOutHd.CustCode,
                                    TransDate = _msGoodsInOutHd.TransDate,
                                    Remark = _msGoodsInOutHd.Remark,
                                    Status = _msGoodsInOutHd.Status,
                                    CarryBy = _msGoodsInOutHd.CarryBy,
                                    RequestedBy = _msGoodsInOutHd.RequestedBy,
                                    ApprovedBy = _msGoodsInOutHd.ApprovedBy,
                                    PostedBy = _msGoodsInOutHd.PostedBy,
                                    EntryDate = _msGoodsInOutHd.EntryDate,
                                    EntryUserName = _msGoodsInOutHd.EntryUserName,
                                    EditDate = _msGoodsInOutHd.EditDate,
                                    EditUserName = _msGoodsInOutHd.EntryUserName

                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new GoodsInOutHd(_row.TransNmbr, _row.FileNmbr, _row.TransType, _row.CustCode, _row.TransDate, _row.Remark,
                        _row.Status, _row.CarryBy, _row.RequestedBy, _row.ApprovedBy, _row.PostedBy, _row.EntryDate, _row.EntryUserName, _row.EditDate, _row.EditUserName));
                }
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<GoodsInOutHd> GetListHd(int _prmReqPage, int _prmPageSize, String _prmGoods, String _prmKeyword)
        {
            List<GoodsInOutHd> _result = new List<GoodsInOutHd>();
            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmGoods == "CustCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmGoods == "TransType")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }


            try
            {
                var _query = (
                                from _msGoodsInOutHd in this.db.GoodsInOutHds
                                join _msCust in this.db.MsCustomers
                                on _msGoodsInOutHd.CustCode equals _msCust.CustCode
                                where (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msGoodsInOutHd.TransType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msGoodsInOutHd.TransNmbr,_msGoodsInOutHd.CustCode,_msGoodsInOutHd.TransType  ascending
                                select new
                                {
                                    TransNmbr = _msGoodsInOutHd.TransNmbr,
                                    FileNmbr = _msGoodsInOutHd.FileNmbr,
                                    TransType = _msGoodsInOutHd.TransType,
                                    CustCode  = _msGoodsInOutHd.CustCode,
                                    TransDate = _msGoodsInOutHd.TransDate,
                                    Remark = _msGoodsInOutHd.Remark,
                                    Status = _msGoodsInOutHd.Status,
                                    CarryBy = _msGoodsInOutHd.CarryBy,
                                    RequestedBy = _msGoodsInOutHd.RequestedBy,
                                    ApprovedBy = _msGoodsInOutHd.ApprovedBy,
                                    PostedBy = _msGoodsInOutHd.PostedBy,
                                    EntryDate = _msGoodsInOutHd.EntryDate,
                                    EntryUserName = _msGoodsInOutHd.EntryUserName,
                                    EditDate = _msGoodsInOutHd.EditDate,
                                    EditUserName = _msGoodsInOutHd.EditUserName
                                   
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new GoodsInOutHd(_row.TransNmbr, _row.FileNmbr, _row.TransType, _row.CustCode, _row.TransDate, _row.Remark,
                        _row.Status, _row.CarryBy, _row.RequestedBy, _row.ApprovedBy, _row.PostedBy, _row.EntryDate, _row.EntryUserName, _row.EditDate, _row.EditUserName));
                }
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public List<GoodsInOutHd> GetListGoodsInOutForSearch(string _prmCustName, string _prmVisitCode, string _prmVisitName)
        //{
        //    List<GoodsInOutHd> _result = new List<GoodsInOutHd>();

        //    try
        //    {
        //        var _query = (
        //                        from _msGoodsInOutHd in this.db.GoodsInOutHds
        //                        join _msCustContact in this.db.MsCustContacts
        //                            on _msGoodsInOutHd.CustomerCode equals _msCustContact.CustCode
        //                        join _msCust in this.db.MsCustomers
        //                            on _msGoodsInOutHd.CustomerCode equals _msCust.CustCode
        //                        where (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _prmCustName.ToLower()))
        //                            && (SqlMethods.Like(_msCustContact.ItemNo.ToString().ToLower(), _prmVisitCode.ToLower()))
        //                            && (SqlMethods.Like(_msCustContact.ContactName.ToString().Trim().ToLower(), _prmVisitName.Trim().ToLower()))
        //                            && _msGoodsInOutHd.VisitorCode == _msCustContact.ItemNo
        //                        orderby _msGoodsInOutHd.GoodsInOutCode ascending
        //                        select new
        //                        {
        //                            GoodsInOutCode = _msGoodsInOutHd.GoodsInOutCode,
        //                            GoodsInOutDate = _msGoodsInOutHd.GoodsInOutDate,
        //                            CustomerCode = _msGoodsInOutHd.CustomerCode,
        //                            CustomerName = _msCust.CustName,
        //                            VisitorCode = _msCustContact.ItemNo,
        //                            VisitorName = _msCustContact.ContactName,
        //                            Remark = _msGoodsInOutHd.Remark,
        //                            CompleteStatus = _msGoodsInOutHd.CompleteStatus
        //                        }
        //                    ).Distinct();

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new GoodsInOutHd(_row.GoodsInOutCode, _row.GoodsInOutDate, _row.CustomerCode, _row.CustomerName, _row.VisitorCode, _row.VisitorName, _row.Remark, _row.CompleteStatus));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}
        public double RowsCountAllTransNumber()
        {
            double _result = 0;

            var _query =
                        (
                            from _GoodsInOutHds in this.db.GoodsInOutHds
                            orderby _GoodsInOutHds.TransDate descending
                            select _GoodsInOutHds.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public GoodsInOutHd GetSingleHd(string _prmCode)
        {
            GoodsInOutHd _result = null;

            try
            {
                _result = this.db.GoodsInOutHds.Single(_temp => _temp.TransNmbr == _prmCode);
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GoodsInOutHd _msGoodsInOutHd = this.db.GoodsInOutHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.GoodsInOutHds.DeleteOnSubmit(_msGoodsInOutHd);
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

        public bool Add(GoodsInOutHd _prmGoodsInOutHd)
        {
            bool _result = false;

            try
            {
                double _max = this.RowsCountAllTransNumber();

                _prmGoodsInOutHd.TransNmbr = (_max + 1).ToString().PadLeft(8, '0');

                this.db.GoodsInOutHds.InsertOnSubmit(_prmGoodsInOutHd);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddTemp(GoodsInOutHd _prmGoodsInOutHd)
        {
            bool _result = false;

            try
            {
                this.db.GoodsInOutHds.InsertOnSubmit(_prmGoodsInOutHd);
                //this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(GoodsInOutHd _prmGoodsInOutHd)
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

        public bool EditTemp(GoodsInOutHd _prmGoodsInOutHd)
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

        public bool IsGoodsInOutCodeExist(string _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _msGoodsInOutHd in this.db.GoodsInOutHds
                                where _msGoodsInOutHd.TransNmbr == _prmCode
                                select new
                                {
                                    GoodsInOutCode = _msGoodsInOutHd.TransNmbr
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

        #region GoodsInOutDt

        public int RowsCountDt(string _prmCode)
        {
            //get
            //{
            return this.db.GoodsInOutHds.Where(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()).Count();
            //}
        }

        public List<GoodsInOutDt> GetListDt(string _prmCode)
        {
            List<GoodsInOutDt> _result = new List<GoodsInOutDt>();

            try
            {
                var _query = (
                                from _msGoodsInOutDt in this.db.GoodsInOutDts
                                where _msGoodsInOutDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _msGoodsInOutDt.TransNmbr, _msGoodsInOutDt.ItemNo ascending
                                select new
                                {
                                    TransNumb = _msGoodsInOutDt.TransNmbr,
                                    ItemNo   = _msGoodsInOutDt.ItemNo,
                                    ContactName = (from _msContactName in this.db.MsCustContacts
                                                   where _msContactName.ItemNo == _msGoodsInOutDt.ItemNo
                                                   && _msContactName.CustCode == _msGoodsInOutDt.CustCode
                                                   select _msContactName.ContactName
                                                   ).FirstOrDefault(),
                                    ItemCode = _msGoodsInOutDt.ItemCode,
                                    CustCode = _msGoodsInOutDt.CustCode,
                                    CustName = (from _msCustName in this.db.MsCustomers
                                                where _msCustName.CustCode == _msGoodsInOutDt.CustCode
                                                select _msCustName.CustName
                                                   ).FirstOrDefault(),
                                    ProductName = _msGoodsInOutDt.ProductName,
                                    SerialNumber = _msGoodsInOutDt.SerialNumber,
                                    Remark = _msGoodsInOutDt.Remark,
                                    ElectriCity = _msGoodsInOutDt.ElectriCityNumerik
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new GoodsInOutDt(_row.TransNumb, _row.ItemNo, _row.ContactName, _row.ItemCode, _row.CustCode, _row.CustName, _row.ProductName, _row.SerialNumber, _row.Remark, _row.ElectriCity));
                }
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        
        public List<GoodsInOutDt> GetListDtbyCust(string _prmCustCode)
        {
            List<GoodsInOutDt> _result = new List<GoodsInOutDt>();
            String _pattern = "%%";

            if (_prmCustCode != "")
            {
                _pattern = "%" + _prmCustCode + "%";
            }

            try
            {
                var _query = (
                                from _msGoodsInOutDt in this.db.GoodsInOutDts
                                join _msCust in this.db.MsCustomers
                                on _msGoodsInOutDt.CustCode equals _msCust.CustCode
                                where
                               (SqlMethods.Like(_msCust.CustCode.ToString().Trim().ToLower(), _pattern.Trim().ToLower()))
                                orderby _msGoodsInOutDt.TransNmbr, _msGoodsInOutDt.ItemNo ascending
                                select new
                                {
                                    TransNumb = _msGoodsInOutDt.TransNmbr,
                                    ItemNo = _msGoodsInOutDt.ItemNo,
                                    ContactName = (from _msContactName in this.db.MsCustContacts
                                                   where _msContactName.ItemNo == _msGoodsInOutDt.ItemNo
                                                   && _msContactName.CustCode == _msGoodsInOutDt.CustCode
                                                   select _msContactName.ContactName
                                                   ).FirstOrDefault(),
                                    ItemCode = _msGoodsInOutDt.ItemCode,
                                    CustCode = _msGoodsInOutDt.CustCode,
                                    CustName = (from _msCustName in this.db.MsCustomers
                                                where _msCustName.CustCode == _msGoodsInOutDt.CustCode
                                                select _msCustName.CustName
                                                   ).FirstOrDefault(),
                                    ProductName = _msGoodsInOutDt.ProductName,
                                    SerialNumber = _msGoodsInOutDt.SerialNumber,
                                    Remark = _msGoodsInOutDt.Remark,
                                    ElectriCity = _msGoodsInOutDt.ElectriCityNumerik
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new GoodsInOutDt(_row.TransNumb, _row.ItemNo, _row.ContactName, _row.ItemCode, _row.CustCode, _row.CustName, _row.ProductName, _row.SerialNumber, _row.Remark, _row.ElectriCity));
                }
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxItemNoByCode(string _prmTransNumb)
        {
            int _result = 0;

            try
            {
                _result = this.db.GoodsInOutDts.Where(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNumb.Trim().ToLower()).Max(_max => _max.ItemNo);
            }
            catch (Exception)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        public double RowsCountAllTransNumberDt()
        {
            double _result = 0;

            var _query =
                        (
                            from _GoodsInOutDt in this.db.GoodsInOutDts
                            orderby _GoodsInOutDt.ItemNo descending
                            select _GoodsInOutDt.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }
        public GoodsInOutDt GetSingleDt(String _prmCode)
        {
            GoodsInOutDt _result = null;

            try
            {
                String[] _tempSplit = _prmCode.Split('-');
                _result = this.db.GoodsInOutDts.Single(_temp => _temp.TransNmbr.ToLower() == _tempSplit[0].Trim().ToLower() && _temp.ItemNo.ToString() == _tempSplit[1].Trim().ToLower());
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
                    GoodsInOutDt _msGoodsInOut = this.db.GoodsInOutDts.Single(_temp => _temp.TransNmbr.ToString() == _tempSplit[0].Trim().ToLower() && _temp.ItemNo.ToString() == _tempSplit[1].Trim().ToLower());

                    this.db.GoodsInOutDts.DeleteOnSubmit(_msGoodsInOut);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }


        //public bool AddListDt(string _prmGoodsInOutCode, DataGridView _prmDataGrid)
        //{
        //    bool _result = false;

        //    List<GoodsInOutDt> _listResult = new List<GoodsInOutDt>();

        //    try
        //    {
        //        for (int i = 0; i < _prmDataGrid.Rows.Count - 1; i++)
        //        {
        //            GoodsInOutDt _GoodsInOutDt = new GoodsInOutDt();

        //            _GoodsInOutDt.GoodsInOutCode = _prmDataGrid.Rows[i].Cells["GoodsInOutCode"].Value.ToString();
        //            _GoodsInOutDt.AreaCode = _prmDataGrid.Rows[i].Cells["AreaCode"].Value.ToString();
        //            _GoodsInOutDt.PurposeCode = _prmDataGrid.Rows[i].Cells["PurposeCode"].Value.ToString();

        //            DateTime _dateIn = Convert.ToDateTime(_prmDataGrid.Rows[i].Cells["DateIn"].Value.ToString());
        //            int _yearIn = _dateIn.Year;
        //            int _monthIn = _dateIn.Month;
        //            int _dayIn = _dateIn.Day;
        //            DateTime _timeIn = Convert.ToDateTime(_prmDataGrid.Rows[i].Cells["TimeIn"].Value.ToString());
        //            int _hourIn = _timeIn.Hour;
        //            int _minuteIn = _timeIn.Minute;
        //            int _secondIn = _timeIn.Second;

        //            DateTime _checkIn = new DateTime(_yearIn, _monthIn, _dayIn, _hourIn, _minuteIn, _secondIn);
        //            _GoodsInOutDt.CheckIn = _checkIn;

        //            DateTime _dateOut = Convert.ToDateTime(_prmDataGrid.Rows[i].Cells["DateOut"].Value.ToString());
        //            int _yearOut = _dateOut.Year;
        //            int _monthOut = _dateOut.Month;
        //            int _dayOut = _dateOut.Day;
        //            DateTime _timeOut = Convert.ToDateTime(_prmDataGrid.Rows[i].Cells["TimeOut"].Value.ToString());
        //            int _hourOut = _timeOut.Hour;
        //            int _minuteOut = _timeOut.Minute;
        //            int _secondOut = _timeOut.Second;

        //            DateTime _checkOut = new DateTime(_yearOut, _monthOut, _dayOut, _hourOut, _minuteOut, _secondOut);
        //            _GoodsInOutDt.CheckOut = _checkOut;

        //            _listResult.Add(_GoodsInOutDt);
        //        }

        //        for (int i = 0; i < _prmDataGrid.Rows.Count - 1; i++)
        //        {
        //            GoodsInOutDt _GoodsInOutDt = new GoodsInOutDt();

        //            _GoodsInOutDt.GoodsInOutCode = _prmGoodsInOutCode;

        //            foreach (var item in _listResult)
        //            {
        //                if (_GoodsInOutDt.GoodsInOutCode == item.GoodsInOutCode)
        //                {
        //                    this.db.GoodsInOutDts.InsertOnSubmit(item);

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

        public bool AddDt(GoodsInOutDt _prmGoodsInOutDt)
        {
            bool _result = false;

            try
            {
                //double _max = this.RowsCountAllTransNumberDt();
                //_prmGoodsInOutDt.ItemNo   = Convert.ToInt32((_max + 1).ToString().PadLeft(2, '0'));
                this.db.GoodsInOutDts.InsertOnSubmit(_prmGoodsInOutDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditDt(GoodsInOutDt _prmGoodsInOutDt)
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

        //public bool IsDetailExist(string _prmCode, string _prmAreaCode, string _prmPurposeCode, DateTime _prmCheckIn)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        var _query = (
        //                        from _msGoodsInOutDt in this.db.GoodsInOutDts
        //                        where _msGoodsInOutDt.GoodsInOutCode.Trim().ToLower() == _prmCode.Trim().ToLower()
        //                            && _msGoodsInOutDt.AreaCode.Trim().ToLower() == _prmAreaCode.Trim().ToLower()
        //                            && _msGoodsInOutDt.PurposeCode.Trim().ToLower() == _prmPurposeCode.Trim().ToLower()
        //                            && _msGoodsInOutDt.CheckIn == _prmCheckIn
        //                        select new
        //                        {
        //                            GoodsInOutCode = _msGoodsInOutDt.GoodsInOutCode
        //                        }
        //                      ).Count();

        //        if (_query > 0)
        //        {
        //            _result = true;
        //        }
        //        else
        //        {
        //            _result = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return _result;
        //}

        #endregion



    }

}
