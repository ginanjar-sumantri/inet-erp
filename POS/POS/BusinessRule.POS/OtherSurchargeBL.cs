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
    public sealed class OtherSurchargeBL : Base
    {
        public OtherSurchargeBL()
        {
        }

        #region POSMsShippingOS

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
                             from _posMsShippingOS in this.db.POSMsShippingOs
                             where (SqlMethods.Like(_posMsShippingOS.OtherSurchargeCode.ToString(), _pattern1))
                                && (SqlMethods.Like(_posMsShippingOS.OtherSurchargeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posMsShippingOS
                        ).Count();

            _result = _query;

            return _result;
        }

        public POSMsShippingO GetSingle(string _prmCode)
        {
            POSMsShippingO _result = null;

            try
            {
                _result = this.db.POSMsShippingOs.Single(_temp => _temp.OtherSurchargeCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsShippingO> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsShippingO> _result = new List<POSMsShippingO>();

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
                                from _posMsShippingOS in this.db.POSMsShippingOs
                                where (SqlMethods.Like(_posMsShippingOS.OtherSurchargeCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsShippingOS.OtherSurchargeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsShippingOS.OtherSurchargeName descending
                                select _posMsShippingOS
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

        public bool Add(POSMsShippingO _prmPOSMsShippingOS)
        {
            bool _result = false;

            try
            {
                this.db.POSMsShippingOs.InsertOnSubmit(_prmPOSMsShippingOS);
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
                    var _query = (from _posMsShippingOS in this.db.POSMsShippingOs
                                  where _posMsShippingOS.OtherSurchargeCode == _tempSplit[0]
                                  select _posMsShippingOS);

                    this.db.POSMsShippingOs.DeleteAllOnSubmit(_query);
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

        ~OtherSurchargeBL()
        {
        }
    }
}
