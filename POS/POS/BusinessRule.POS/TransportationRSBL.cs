using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;

namespace BusinessRule.POS
{
    public sealed class TransportationRSBL : Base
    {
        public TransportationRSBL()
        {
        }

        #region POSMsShippingTRS

        public double RowsCount(string _prmCategory, string _prmKeyword)
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

            var _query =
                        (
                             from _posMsShippingTRS in this.db.POSMsShippingTRs
                             where (SqlMethods.Like(_posMsShippingTRS.TRSCode.ToString(), _pattern1))
                                && (SqlMethods.Like(_posMsShippingTRS.TRSName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posMsShippingTRS
                        ).Count();

            _result = _query;

            return _result;
        }

        public POSMsShippingTR GetSingle(string _prmCode)
        {
            POSMsShippingTR _result = null;

            try
            {
                _result = this.db.POSMsShippingTRs.Single(_temp => _temp.TRSCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        
        public List<POSMsShippingTR> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsShippingTR> _result = new List<POSMsShippingTR>();

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
                                from _posMsShippingTRS in this.db.POSMsShippingTRs
                                where (SqlMethods.Like(_posMsShippingTRS.TRSCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsShippingTRS.TRSName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsShippingTRS.TRSName descending
                                select _posMsShippingTRS
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

        public bool Add(POSMsShippingTR _prmPOSMsShippingTRS)
        {
            bool _result = false;

            try
            {
                this.db.POSMsShippingTRs.InsertOnSubmit(_prmPOSMsShippingTRS);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSubmit()
        {
            bool _result = false;
            try
            {
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
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
                    string[] _tempSplit = _prmCode[i].Split('-');
                    var _query = (from _posMsShippingTRS in this.db.POSMsShippingTRs
                                  where _posMsShippingTRS.TRSCode == _tempSplit[0]
                                  select _posMsShippingTRS);

                    this.db.POSMsShippingTRs.DeleteAllOnSubmit(_query);
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

        #endregion

        ~TransportationRSBL()
        {
        }
    }
}
