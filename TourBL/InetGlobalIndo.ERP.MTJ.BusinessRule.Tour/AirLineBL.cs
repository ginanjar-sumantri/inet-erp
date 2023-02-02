using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Data.Linq.SqlClient;
using System.Diagnostics;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;


namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Tour
{
    public sealed class AirLineBL : Base
    {
        public AirLineBL()
        {
        }

        #region AirLine

        public double RowsCountAirLine(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";            

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            

            try
            {
                var _query =
                            (
                                from _msAirline in this.db.MsAirlines
                                where (SqlMethods.Like(_msAirline.AirlineCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msAirline.AirlineName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _msAirline.AirlineCode
                            ).Count();

                _result = _query;

            }
            catch (Exception)
            {

                throw;
            }

            return _result;
        }

        public List<MsAirline> GetListAirLine(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsAirline> _result = new List<MsAirline>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
           
            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";                
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }            

            try
            {
                var _query = (
                                from _msAirline in this.db.MsAirlines
                                where (SqlMethods.Like(_msAirline.AirlineCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msAirline.AirlineName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msAirline.AirlineCode descending
                                select _msAirline
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

        public MsAirline GetSingleAirLine(string _prmCode)
        {
            MsAirline _result = null;

            try
            {
                _result = this.db.MsAirlines.Single(_temp => _temp.AirlineCode.Trim().ToLower() == _prmCode.Trim().ToLower());
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
                    MsAirline _msAirline = this.db.MsAirlines.Single(_temp => _temp.AirlineCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsAirlines.DeleteOnSubmit(_msAirline);
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

        public bool AddAirLine(MsAirline _prmMsAirLine)
        {
            bool _result = false;

            try
            {
                this.db.MsAirlines.InsertOnSubmit(_prmMsAirLine);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditAirLine(MsAirline _prmMsAirLine)
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

        #endregion

        ~AirLineBL()
        {
        }
    }
}
