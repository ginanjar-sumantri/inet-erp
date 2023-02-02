using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common;

namespace BusinessRule.POS
{
    public sealed class POSReasonBL : Base
    {
        public POSReasonBL()
        {
        }

        #region Reason

        public double RowsCount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern2 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                             from _posMsReason in this.db.POSMsReasons
                             where (SqlMethods.Like(_posMsReason.ReasonName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posMsReason
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<POSMsReason> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsReason> _result = new List<POSMsReason>();

            string _pattern2 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _posMsReason in this.db.POSMsReasons
                                where (SqlMethods.Like(_posMsReason.ReasonName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsReason.ReasonName descending
                                select _posMsReason
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsReason> GetList()
        {
            List<POSMsReason> _result = new List<POSMsReason>();

            try
            {
                var _query = (
                                from _posMsReason in this.db.POSMsReasons
                                orderby _posMsReason.ReasonName ascending
                                select _posMsReason
                            );

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
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
                    POSMsReason _posMsReason = this.db.POSMsReasons.Single(_temp => _temp.ReasonCode == Convert.ToInt32(_prmCode[i]));

                    this.db.POSMsReasons.DeleteOnSubmit(_posMsReason);
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

        public POSMsReason GetSingle(int _prmCode)
        {
            POSMsReason _result = null;

            try
            {
                _result = this.db.POSMsReasons.Single(_temp => _temp.ReasonCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetReasonByCode(int _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _posMsReason in this.db.POSMsReasons
                                where _posMsReason.ReasonCode == _prmCode
                                select new
                                {
                                    ReasonName = _posMsReason.ReasonName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ReasonName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(POSMsReason _prmPOSMsReason)
        {
            bool _result = false;

            try
            {
                this.db.POSMsReasons.InsertOnSubmit(_prmPOSMsReason);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(POSMsReason _prmPOSMsReason)
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

        public int GetReasonLastNo() {
            int _result = 0;

            var _query = (
                            from _reason in this.db.POSMsReasons
                            select _reason.ReasonCode
                         ).Max();

            _result = _query + 1;

            return _result;
        }

        #endregion

        ~POSReasonBL()
        {
        }
    }
}
