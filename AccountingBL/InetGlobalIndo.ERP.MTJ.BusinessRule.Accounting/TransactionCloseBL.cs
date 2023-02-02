using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class TransactionCloseBL : Base
    {

        public TransactionCloseBL()
        {

        }

        public double RowsCount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            //string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                //_pattern2 = "%%";
            }

            var _query =
                        (
                            from _msTransClose in this.db.Master_TransactionCloses
                            where (SqlMethods.Like(_msTransClose.Description.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            select _msTransClose
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<Master_TransactionClose> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_TransactionClose> _result = new List<Master_TransactionClose>();

            string _pattern1 = "%%";
            //string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                //_pattern2 = "%%";

            }

            try
            {
                var _query = (
                                from _msTransClose in db.Master_TransactionCloses
                                where (SqlMethods.Like(_msTransClose.Description.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _msTransClose.StartDate ascending
                                select new
                                {
                                    TransCloseCode = _msTransClose.TransCloseCode,
                                    StartDate = _msTransClose.StartDate,
                                    EndDate = _msTransClose.EndDate,
                                    Status = _msTransClose.Status,
                                    Description = _msTransClose.Description
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_TransactionClose(_row.TransCloseCode, _row.StartDate, _row.EndDate, _row.Status, _row.Description));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Master_TransactionClose GetSingle(string _prmTransCloseCode)
        {
            Master_TransactionClose _result = null;

            try
            {
                _result = this.db.Master_TransactionCloses.Single(_temp => _temp.TransCloseCode == new Guid(_prmTransCloseCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string Add(Master_TransactionClose _prmMsTransClose)
        {
            string _result = "";

            try
            {
                String _exist = IsExist(_prmMsTransClose.StartDate, _prmMsTransClose.EndDate);
                if (_exist == "")
                {
                    this.db.Master_TransactionCloses.InsertOnSubmit(_prmMsTransClose);
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(Master_TransactionClose _prmMsTransClose)
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

        public bool DeleteMulti(string[] _prmTransCloseCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTransCloseCode.Length; i++)
                {
                    Master_TransactionClose _msTransClose = this.db.Master_TransactionCloses.Single(_transClose => _transClose.TransCloseCode == new Guid(_prmTransCloseCode[i]));

                    this.db.Master_TransactionCloses.DeleteOnSubmit(_msTransClose);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string IsExistAndLocked(DateTime _prmDate)/*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
        {
            string _result = "";

            try
            {
                var _query = (from _msTransClose in this.db.Master_TransactionCloses
                              where (_msTransClose.Status == TransactionCloseDataMapper.GetTransCloseStatusForTrans(TransCloseStatus.Locked))
                              select new
                              {
                                  StartDate = _msTransClose.StartDate,
                                  EndDate = _msTransClose.EndDate
                              }
                              );

                foreach (var _item in _query)
                {
                    if (_prmDate >= _item.StartDate && _prmDate <= _item.EndDate)
                    {
                        _result = "Transaction Period Already Closed";
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "You Failed Add Data";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string IsExistAndLocked(DateTime _prmStartDate, DateTime _prmEndDate)/*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
        {
            string _result = "";

            try
            {
                var _query = (from _msTransClose in this.db.Master_TransactionCloses
                              where (_msTransClose.Status == TransactionCloseDataMapper.GetTransCloseStatusForTrans(TransCloseStatus.Locked))
                              select new
                              {
                                  StartDate = _msTransClose.StartDate,
                                  EndDate = _msTransClose.EndDate
                              }
                              );

                foreach (var _item in _query)
                {
                    if (_prmStartDate >= _item.StartDate && _prmStartDate <= _item.EndDate)
                    {
                        _result = "Transaction Period Already Closed";
                    }
                    if (_prmEndDate >= _item.StartDate && _prmEndDate <= _item.EndDate)
                    {
                        _result = "Transaction Period Already Closed";
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "You Failed Add Data";
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string IsExist(DateTime _prmStartDate, DateTime _prmEndDate) //this is for transaction close add validation
        {
            string _result = "";

            try
            {
                var _query = (from _msTransClose in this.db.Master_TransactionCloses
                              select new
                              {
                                  StartDate = _msTransClose.StartDate,
                                  EndDate = _msTransClose.EndDate
                              }
                              );

                foreach (var _item in _query)
                {
                    if (_prmStartDate >= _item.StartDate && _prmStartDate <= _item.EndDate)
                    {
                        _result = "Invalid Date Range";
                        break;
                    }
                    if (_prmEndDate >= _item.StartDate && _prmEndDate <= _item.EndDate)
                    {
                        _result = "Invalid Date Range";
                        break;
                    }
                    if (_prmStartDate <= _item.StartDate && _prmEndDate >= _item.EndDate)
                    {
                        _result = "Invalid Date Range";
                        break;
                    }
                    if (_prmStartDate > _prmEndDate)
                    {
                        _result = "Invalid Date Range";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        ~TransactionCloseBL()
        {

        }
    }
}
