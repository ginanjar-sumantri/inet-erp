using System;
using System.Collections.Generic;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Diagnostics;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule
{
    public sealed class Transaction_ActivityBL : Base
    {
        public Transaction_ActivityBL()
        {

        }

        public double RowsCount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "Transtype")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                           from _transActivity in this.db.Transaction_Activities
                           where (SqlMethods.Like(_transActivity.TransType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           select _transActivity.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<Transaction_Activity> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Transaction_Activity> _result = new List<Transaction_Activity>();

            string _pattern1 = "%%";

            if (_prmCategory == "Transtype")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (from _transActivity in this.db.Transaction_Activities
                              where (SqlMethods.Like(_transActivity.TransType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              && _transActivity.ActivitiesID == DataMapping.ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)
                              orderby _transActivity.TransNmbr descending
                              select new
                              {
                                  ActivitiesCode = _transActivity.ActivitiesCode,
                                  TransType = _transActivity.TransType,
                                  TransNmbr = _transActivity.TransNmbr,
                                  ActivitiesDate = _transActivity.ActivitiesDate,
                                  Username = _transActivity.Username
                              }).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ActivitiesCode = this._guid, TransType = this._string, TransNmbr = this._string, ActivitiesDate = this._datetime, Username = this._string });

                    _result.Add(new Transaction_Activity(_row.ActivitiesCode, _row.TransType, _row.TransNmbr, Convert.ToDateTime(_row.ActivitiesDate), _row.Username));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCountAdvancedSearch(string _prmTransType, string _prmTransNmbr, DateTime _prmDateFrom, DateTime _prmDateTo, String _prmUnpostingBy)
        {
            double _result = 0;

            string _pattern1 = "%" + _prmTransType + "%";
            string _pattern3 = "%" + _prmTransNmbr + "%";
            string _pattern4 = "%" + _prmUnpostingBy + "%";

            var _query =
                        (
                            from _transActivity in this.db.Transaction_Activities
                            where (SqlMethods.Like(_transActivity.TransType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && _transActivity.ActivitiesDate >= _prmDateFrom && _transActivity.ActivitiesDate <= _prmDateTo
                                && (SqlMethods.Like(_transActivity.TransNmbr.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                && (SqlMethods.Like(_transActivity.Username.Trim().ToLower(), _pattern4.Trim().ToLower()))
                            select _transActivity.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<Transaction_Activity> GetListAdvancedSearch(int _prmReqPage, int _prmPageSize, string _prmTransType, string _prmTransNmbr, DateTime _prmDateFrom, DateTime _prmDateTo, String _prmUnpostingBy)
        {
            List<Transaction_Activity> _result = new List<Transaction_Activity>();

            string _pattern1 = "%" + _prmTransType + "%";
            string _pattern3 = "%" + _prmTransNmbr + "%";
            string _pattern4 = "%" + _prmUnpostingBy + "%";

            try
            {
                var _query = (
                               from _transActivity in this.db.Transaction_Activities
                               where (SqlMethods.Like(_transActivity.TransType.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && _transActivity.ActivitiesDate >= _prmDateFrom && _transActivity.ActivitiesDate <= _prmDateTo
                                 && (SqlMethods.Like(_transActivity.TransNmbr.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                 && (SqlMethods.Like(_transActivity.Username.Trim().ToLower(), _pattern4.Trim().ToLower()))
                               orderby _transActivity.TransNmbr descending
                               select new
                               {
                                   ActivitiesCode = _transActivity.ActivitiesCode,
                                   TransType = _transActivity.TransType,
                                   TransNmbr = _transActivity.TransNmbr,
                                   ActivitiesDate = _transActivity.ActivitiesDate,
                                   Username = _transActivity.Username
                               }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {

                    _result.Add(new Transaction_Activity(_row.ActivitiesCode, _row.TransType, _row.TransNmbr, Convert.ToDateTime(_row.ActivitiesDate), _row.Username));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

    }
}
