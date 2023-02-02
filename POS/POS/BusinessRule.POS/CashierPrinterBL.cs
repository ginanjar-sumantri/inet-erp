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
    public sealed class CashierPrinterBL : Base
    {
        public CashierPrinterBL()
        {
        }

        #region CashierPrinter

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
                             from _msCashierPrinter in this.db.POSMsCashierPrinters
                             where (SqlMethods.Like(_msCashierPrinter.CashierPrinterCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_msCashierPrinter.CashierPrinterName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _msCashierPrinter
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<POSMsCashierPrinter> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsCashierPrinter> _result = new List<POSMsCashierPrinter>();

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
                                from _msCashierPrinter in this.db.POSMsCashierPrinters
                                where (SqlMethods.Like(_msCashierPrinter.CashierPrinterCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCashierPrinter.CashierPrinterName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msCashierPrinter.CashierPrinterName descending
                                select _msCashierPrinter
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

        public List<POSMsCashierPrinter> GetList()
        {
            List<POSMsCashierPrinter> _result = new List<POSMsCashierPrinter>();

            try
            {
                var _query = (
                                from _msCashierPrinter in this.db.POSMsCashierPrinters
                                orderby _msCashierPrinter.CashierPrinterName ascending
                                select _msCashierPrinter
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
                    POSMsCashierPrinter _msCashierPrinter = this.db.POSMsCashierPrinters.Single(_temp => _temp.CashierPrinterCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.POSMsCashierPrinters.DeleteOnSubmit(_msCashierPrinter);
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

        public POSMsCashierPrinter GetSingle(string _prmCode)
        {
            POSMsCashierPrinter _result = null;
            string[] _split = _prmCode.Split('|');
            try
            {
                _result = this.db.POSMsCashierPrinters.FirstOrDefault(_temp => _temp.CashierPrinterCode == _split[0]);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCashierPrinterNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msCashierPrinter in this.db.POSMsCashierPrinters
                                where _msCashierPrinter.CashierPrinterCode == _prmCode
                                select new
                                {
                                    CashierPrinterName = _msCashierPrinter.CashierPrinterName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CashierPrinterName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(POSMsCashierPrinter _prmPOSMsCashierPrinter)
        {
            bool _result = false;

            try
            {
                this.db.POSMsCashierPrinters.InsertOnSubmit(_prmPOSMsCashierPrinter);
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

        public V_POSMsCashierPrinter GetDefaultPrinter()
        {
            V_POSMsCashierPrinter _result = null;

            try
            {
                var _query = (
                                from _vPOSMsCashierPrinter in this.db.V_POSMsCashierPrinters
                                select _vPOSMsCashierPrinter
                              );

                foreach (var _item in _query)
                {
                    _result = _item;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~CashierPrinterBL()
        {
        }
    }
}
