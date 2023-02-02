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
    public sealed class KitchenBL : Base
    {
        public KitchenBL()
        {
        }

        #region Kitchen

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
                             from _msKitchen in this.db.POSMsKitchens
                             where (SqlMethods.Like(_msKitchen.KitchenCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_msKitchen.KitchenName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _msKitchen
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<POSMsKitchen> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsKitchen> _result = new List<POSMsKitchen>();

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
                                from _msKitchen in this.db.POSMsKitchens
                                where (SqlMethods.Like(_msKitchen.KitchenCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msKitchen.KitchenName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msKitchen.KitchenName descending
                                select new
                                {
                                    KitchenCode = _msKitchen.KitchenCode,
                                    KitchenName = _msKitchen.KitchenName,
                                    Chef = _msKitchen.Chef,
                                    Location = _msKitchen.Location,
                                    KitchenPrinterIPAddress = _msKitchen.KitchenPrinterIPAddress,
                                    KitchenPrinterName = _msKitchen.KitchenPrinterName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new POSMsKitchen(_row.KitchenCode, _row.KitchenName, _row.Chef, _row.Location, _row.KitchenPrinterIPAddress, _row.KitchenPrinterName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsKitchen> GetList()
        {
            List<POSMsKitchen> _result = new List<POSMsKitchen>();

            try
            {
                var _query = (
                                from _msKitchen in this.db.POSMsKitchens
                                orderby _msKitchen.KitchenName ascending
                                select new
                                {
                                    KitchenCode = _msKitchen.KitchenCode,
                                    KitchenName = _msKitchen.KitchenName,
                                    Chef = _msKitchen.Chef,
                                    Location = _msKitchen.Location,
                                    KitchenPrinterIPAddress = _msKitchen.KitchenPrinterIPAddress,
                                    KitchenPrinterName = _msKitchen.KitchenPrinterName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new POSMsKitchen(_row.KitchenCode, _row.KitchenName, _row.Chef, _row.Location, _row.KitchenPrinterIPAddress, _row.KitchenPrinterName));
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
                    POSMsKitchen _msKitchen = this.db.POSMsKitchens.Single(_temp => _temp.KitchenCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.POSMsKitchens.DeleteOnSubmit(_msKitchen);
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

        public POSMsKitchen GetSingle(string _prmCode)
        {
            POSMsKitchen _result = null;
            string[] _split = _prmCode.Split('|');
            try
            {
                _result = this.db.POSMsKitchens.Single(_temp => _temp.KitchenCode == _split[0]);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetKitchenNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msKitchen in this.db.POSMsKitchens
                                where _msKitchen.KitchenCode == _prmCode
                                select new
                                {
                                    KitchenName = _msKitchen.KitchenName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.KitchenName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(POSMsKitchen _prmPOSMsKitchen)
        {
            bool _result = false;

            try
            {
                this.db.POSMsKitchens.InsertOnSubmit(_prmPOSMsKitchen);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit()
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

        #region KitchenDt

        public double RowsCountDt(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "KitchenCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "ProductTypeCode")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                             from _msKitchen in this.db.POSMsKitchenDts
                             where (SqlMethods.Like(_msKitchen.KitchenCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_msKitchen.ProductTypeCode.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _msKitchen
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<POSMsKitchenDt> GetListDt(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsKitchenDt> _result = new List<POSMsKitchenDt>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "ProductTypeCode")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _msKitchenDt in this.db.POSMsKitchenDts
                                where (SqlMethods.Like(_msKitchenDt.KitchenCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msKitchenDt.ProductTypeCode.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msKitchenDt.KitchenCode descending
                                select _msKitchenDt
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

        public List<POSMsKitchenDt> GetListDt()
        {
            List<POSMsKitchenDt> _result = new List<POSMsKitchenDt>();

            try
            {
                var _query = (
                                from _msKitchen in this.db.POSMsKitchenDts
                                orderby _msKitchen.KitchenCode ascending
                                select _msKitchen
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

        public bool DeleteMultiDt(string[] _prmCode)
        {
            bool _result = false;
            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _split = _prmCode[i].Split('|');
                    POSMsKitchenDt _msKitchenDt = this.db.POSMsKitchenDts.FirstOrDefault(_temp => _temp.KitchenCode.Trim().ToLower() == _split[0].Trim().ToLower() && _temp.ProductTypeCode.Trim().ToLower() == _split[1].Trim().ToLower());

                    this.db.POSMsKitchenDts.DeleteOnSubmit(_msKitchenDt);
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

        public POSMsKitchenDt GetSingleDt(string _prmCode)
        {
            POSMsKitchenDt _result = null;
            string[] _split = _prmCode.Split('|');
            string _prmKitchenCode = _split[0];
            string _prmProductTypeCode = _split[1];
            try
            {
                _result = this.db.POSMsKitchenDts.FirstOrDefault(_temp => _temp.KitchenCode == _prmKitchenCode && _temp.ProductTypeCode == _prmProductTypeCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddDt(POSMsKitchenDt _prmPOSMsKitchenDt)
        {
            bool _result = false;

            try
            {
                this.db.POSMsKitchenDts.InsertOnSubmit(_prmPOSMsKitchenDt);
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

        ~KitchenBL()
        {
        }
    }
}
