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
    public sealed class VendorBL : Base
    {
        public VendorBL()
        {
        }

        #region POSMsShippingVendor

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
                             from _posMsShippingVendor in this.db.POSMsShippingVendors
                             where (SqlMethods.Like(_posMsShippingVendor.VendorCode.ToString(), _pattern1))
                                && (SqlMethods.Like(_posMsShippingVendor.VendorName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posMsShippingVendor
                        ).Count();

            _result = _query;

            return _result;
        }

        public POSMsShippingVendor GetSingle(string _prmCode)
        {
            POSMsShippingVendor _result = null;

            try
            {
                _result = this.db.POSMsShippingVendors.Single(_temp => _temp.VendorCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsShippingVendor> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsShippingVendor> _result = new List<POSMsShippingVendor>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }

            try
            {

                var _query = (
                                from _posMsShippingVendor in this.db.POSMsShippingVendors
                                where (SqlMethods.Like(_posMsShippingVendor.VendorCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsShippingVendor.VendorName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsShippingVendor.VendorName descending
                                select _posMsShippingVendor
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

        public bool Add(POSMsShippingVendor _prmPOSMsShippingVendor)
        {
            bool _result = false;

            try
            {
                this.db.POSMsShippingVendors.InsertOnSubmit(_prmPOSMsShippingVendor);
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
                    var _query = (from _posMsShippingVendor in this.db.POSMsShippingVendors
                                  where _posMsShippingVendor.VendorCode == _tempSplit[0]
                                  select _posMsShippingVendor);

                    this.db.POSMsShippingVendors.DeleteAllOnSubmit(_query);
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

        #region POSMsShippingVendorDt

        public double RowsCountDt(string _prmCategory, string _prmKeyword)
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
                             from _posMsShippingTypeVendorDt in this.db.POSMsShippingVendorDts
                             join _posMsShippingTypeVendor in this.db.POSMsShippingVendors
                             on _posMsShippingTypeVendorDt.VendorCode equals _posMsShippingTypeVendor.VendorCode
                             where (SqlMethods.Like(_posMsShippingTypeVendorDt.VendorCode.ToString(), _pattern1))
                             && (SqlMethods.Like(_posMsShippingTypeVendor.VendorName.ToString(), _pattern2))
                             select _posMsShippingTypeVendorDt
                        ).Count();

            _result = _query;

            return _result;
        }

        public POSMsShippingVendorDt GetSingleDt(string _prmCode, string _prmShippingTypeCode)
        {
            POSMsShippingVendorDt _result = null;

            try
            {
                _result = this.db.POSMsShippingVendorDts.Single(_temp => _temp.VendorCode == _prmCode && _temp.ShippingZonaTypeCode == _prmShippingTypeCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsShippingVendorDt> GetListDt(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsShippingVendorDt> _result = new List<POSMsShippingVendorDt>();

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
                                from _posMsShippingVendorDt in this.db.POSMsShippingVendorDts
                                where (SqlMethods.Like(_posMsShippingVendorDt.VendorCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsShippingVendorDt.ShippingZonaTypeCode.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsShippingVendorDt.VendorCode descending
                                select _posMsShippingVendorDt
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

        public bool AddDt(POSMsShippingVendorDt _prmPOSMsShippingVendorDt)
        {
            bool _result = false;

            try
            {
                this.db.POSMsShippingVendorDts.InsertOnSubmit(_prmPOSMsShippingVendorDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('|');
                    var _query = (from _posMsShippingVendorDt in this.db.POSMsShippingVendorDts
                                  where _posMsShippingVendorDt.VendorCode == _tempSplit[0]
                                    && _posMsShippingVendorDt.ShippingZonaTypeCode == _tempSplit[1]
                                  select _posMsShippingVendorDt);

                    this.db.POSMsShippingVendorDts.DeleteAllOnSubmit(_query);
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

        #region POSMsShippingTypeDt

        

        #endregion

        ~VendorBL()
        {
        }
    }
}
