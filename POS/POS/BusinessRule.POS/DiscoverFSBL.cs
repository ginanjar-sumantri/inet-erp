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
    public sealed class DiscoverFSBL : Base
    {
        public DiscoverFSBL()
        {
        }

        #region POSMsShippingDFS

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
                             from _posMsShippingDFS in this.db.POSMsShippingDFs
                             where (SqlMethods.Like(_posMsShippingDFS.DFSCode.ToString(), _pattern1))
                                && (SqlMethods.Like(_posMsShippingDFS.DFSName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posMsShippingDFS
                        ).Count();

            _result = _query;

            return _result;
        }

        public POSMsShippingDF GetSingle(string _prmCode)
        {
            POSMsShippingDF _result = null;

            try
            {
                _result = this.db.POSMsShippingDFs.Single(_temp => _temp.DFSCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsShippingDF> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsShippingDF> _result = new List<POSMsShippingDF>();

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
                                from _posMsShippingDFS in this.db.POSMsShippingDFs
                                where (SqlMethods.Like(_posMsShippingDFS.DFSCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsShippingDFS.DFSName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsShippingDFS.DFSName descending
                                select _posMsShippingDFS
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

        public bool Add(POSMsShippingDF _prmPOSMsShippingDFS)
        {
            bool _result = false;

            try
            {
                this.db.POSMsShippingDFs.InsertOnSubmit(_prmPOSMsShippingDFS);
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
                    var _query = (from _posMsShippingDFS in this.db.POSMsShippingDFs
                                  where _posMsShippingDFS.DFSCode == _tempSplit[0]
                                  select _posMsShippingDFS);

                    this.db.POSMsShippingDFs.DeleteAllOnSubmit(_query);
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

        ~DiscoverFSBL()
        {
        }
    }
}
