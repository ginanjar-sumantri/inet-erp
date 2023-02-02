using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class TransLogActivitiesBL : Base
    {
        public TransLogActivitiesBL()
        {

        }

        #region TransLogActivitiesBL
        public double RowsCount(string _prmTransType, DateTime _prmFrom, DateTime _prmTo, byte _prmActivitiesMode, string _prmUsername)
        {
            double _result = 0;            

            var _query =
                        (
                            from _trUnpostingActivitie in this.db.Transaction_UnpostingActivities
                            where (SqlMethods.Like(_trUnpostingActivitie.TransType.Trim().ToLower(), ("%" + _prmTransType + "%").ToString().Trim().ToLower()))
                            && (_trUnpostingActivitie.ActivitiesDate >= _prmFrom
                            || _trUnpostingActivitie.ActivitiesDate <= _prmTo)
                            && _trUnpostingActivitie.ActivitiesID == _prmActivitiesMode
                            && (SqlMethods.Like(_trUnpostingActivitie.Username.Trim().ToLower(), ("%" + _prmUsername + "%").Trim().ToLower()))
                            select _trUnpostingActivitie
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<Transaction_UnpostingActivity> GetList(int _prmReqPage, int _prmPageSize, string _prmTransType, DateTime _prmFrom, DateTime _prmTo, byte _prmActivitiesMode, string _prmUsername)
        {
            List<Transaction_UnpostingActivity> _result = new List<Transaction_UnpostingActivity>();

            
            try
            {
                var _query =
                            (
                                from _trUnpostingActivitie in this.db.Transaction_UnpostingActivities
                                where (SqlMethods.Like(_trUnpostingActivitie.TransType.Trim().ToLower(), ("%" + _prmTransType + "%").ToString().Trim().ToLower()))
                                        && (_trUnpostingActivitie.ActivitiesDate >= _prmFrom
                                        || _trUnpostingActivitie.ActivitiesDate <= _prmTo)
                                        && _trUnpostingActivitie.ActivitiesID == _prmActivitiesMode
                                        && (SqlMethods.Like(_trUnpostingActivitie.Username.Trim().ToLower(), ("%" + _prmUsername + "%").Trim().ToLower()))
                                            orderby _trUnpostingActivitie.ActivitiesCode ascending
                                select _trUnpostingActivitie
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

        public List<Transaction_UnpostingActivity> GetList()
        {
            List<Transaction_UnpostingActivity> _result = new List<Transaction_UnpostingActivity>();

            try
            {
                var _query =
                            (
                                from _trUnpostingActivitie in this.db.Transaction_UnpostingActivities
                                orderby _trUnpostingActivitie.ActivitiesCode ascending
                                select _trUnpostingActivitie
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

        public List<Transaction_UnpostingActivity> GetListForDDLTransLogActivitie()
        {
            List<Transaction_UnpostingActivity> _result = new List<Transaction_UnpostingActivity>();

            try
            {
                var _query = (
                                from _trUnpostingActivitie in this.db.Transaction_UnpostingActivities
                                orderby _trUnpostingActivitie.ActivitiesCode descending
                                select new
                                {
                                    TransType = AppModule.GetValueName(_trUnpostingActivitie.TransType),
                                    TransTypeCode = _trUnpostingActivitie.TransType
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    Transaction_UnpostingActivity _transLog = new Transaction_UnpostingActivity();
                    _transLog.TransType = _row.TransType;
                    _transLog.FileNmbr = _row.TransTypeCode;
                    _result.Add(_transLog);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~TransLogActivitiesBL()
        {

        }

    }
}
