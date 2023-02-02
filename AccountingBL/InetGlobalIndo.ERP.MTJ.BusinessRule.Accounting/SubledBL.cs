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
    public sealed class SubledBL : Base
    {
        private char _subledCode = ' ';
        private string _subledName = " ";

        public SubledBL()
        {
        }

        #region Subled

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
                            from _subled in this.db.MsSubleds
                            where (SqlMethods.Like(_subled.SubledCode.ToString(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_subled.SubledName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _subled.SubledCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public bool Edit(MsSubled _prmMsSubled)
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

        public bool Add(MsSubled _prmMsSubled)
        {
            bool _result = false;

            try
            {

                this.db.MsSubleds.InsertOnSubmit(_prmMsSubled);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmSubledCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmSubledCode.Length; i++)
                {
                    MsSubled _msSubled = this.db.MsSubleds.Single(_subled => _subled.SubledCode == Convert.ToChar(_prmSubledCode[i]));

                    this.db.MsSubleds.DeleteOnSubmit(_msSubled);
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

        public MsSubled GetSingle(string _prmSubled)
        {
            MsSubled _result = null;

            try
            {
                _result = this.db.MsSubleds.Single(_subled => _subled.SubledCode == Convert.ToChar(_prmSubled));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetSubledNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msSubled in this.db.V_MsSubleds
                                where _msSubled.SubLed_No == _prmCode
                                select new
                                {
                                    SubledName = _msSubled.SubLed_Name
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.SubledName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsSubled> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsSubled> _result = new List<MsSubled>();

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
                                from _subled in this.db.MsSubleds
                                where (SqlMethods.Like(_subled.SubledCode.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_subled.SubledName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    SubledCode = _subled.SubledCode,
                                    SubledName = _subled.SubledName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { SubledCode = this._subledCode, SubledName = this._subledName });

                    _result.Add(new MsSubled(_row.SubledCode, _row.SubledName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetSubledNameByCodeView(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _subled in this.db.V_MsSubleds
                                where _subled.SubLed_No == _prmCode
                                select new
                                {
                                    SubledName = _subled.SubLed_Name
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.SubledName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<V_MsSubled> GetListSubled(char _prmValue)
        {
            List<V_MsSubled> _result = new List<V_MsSubled>();

            try
            {
                var _query =
                            (
                                from _subled in this.db.V_MsSubleds
                                where _subled.SubledType == _prmValue
                                orderby _subled.SubLed_Name ascending
                                select new
                                {
                                    SubLedNo = _subled.SubLed_No,
                                    SubLedName = _subled.SubLed_Name + " - " + _subled.SubLed_No
                                }
                            );

                foreach (var _obj in _query)
                {
                    //var _row = _obj.Template(new { SubledCode = this._subledCode, SubledName = this._subledName });

                    _result.Add(new V_MsSubled(_obj.SubLedNo, _obj.SubLedName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsSubled> GetList()
        {
            List<MsSubled> _result = new List<MsSubled>();

            try
            {
                var _query =
                            (
                                from _subled in this.db.MsSubleds
                                select new
                                {
                                    SubledCode = _subled.SubledCode,
                                    SubledName = _subled.SubledName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { SubledCode = this._subledCode, SubledName = this._subledName });

                    _result.Add(new MsSubled(_row.SubledCode, _row.SubledName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~SubledBL()
        {
        }

    }
}
