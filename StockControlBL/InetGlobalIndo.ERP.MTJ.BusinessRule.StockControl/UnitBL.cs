using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class UnitBL : Base
    {
        public UnitBL()
        {

        }

        #region Unit

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
                            from _msUnit in this.db.MsUnits
                            where (SqlMethods.Like(_msUnit.UnitCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msUnit.UnitName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msUnit
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MsUnit> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsUnit> _result = new List<MsUnit>();

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
                                from _msUnit in this.db.MsUnits
                                where (SqlMethods.Like(_msUnit.UnitCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msUnit.UnitName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msUnit.CreatedDate descending
                                select new
                                {
                                    UnitCode = _msUnit.UnitCode,
                                    UnitName = _msUnit.UnitName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { UnitCode = this._string, UnitName = this._string });

                    _result.Add(new MsUnit(_row.UnitCode, _row.UnitName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsUnit> GetListForDDL()
        {
            List<MsUnit> _result = new List<MsUnit>();

            try
            {
                var _query = (
                                from _msUnit in this.db.MsUnits
                                orderby _msUnit.CreatedDate descending
                                select new
                                {
                                    UnitCode = _msUnit.UnitCode,
                                    UnitName = _msUnit.UnitName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { UnitCode = this._string, UnitName = this._string });

                    _result.Add(new MsUnit(_row.UnitCode, _row.UnitName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsUnit> GetListUnitConvertFromForDDL(string _prmUnit)
        {
            List<MsUnit> _result = new List<MsUnit>();

            try
            {
                var _query = (
                    //from _msUnit in this.db.MsUnits
                                from _msConvert in this.db.MsConvertions
                                //join _msConvert in this.db.MsConvertions
                                //    on _msUnit.UnitCode equals _msConvert.UnitCode
                                where _msConvert.UnitCode == _prmUnit
                                orderby _msConvert.UserDate descending
                                select new
                                {
                                    UnitCode = _msConvert.UnitConvert,
                                    UnitName = (
                                                    from _msUnit in this.db.MsUnits
                                                    where _msUnit.UnitCode == _msConvert.UnitConvert
                                                    select _msUnit.UnitName
                                               ).FirstOrDefault()
                                }
                            );

                foreach (var _row in _query)
                {
                    //var _row = _obj.Template(new { UnitCode = this._string, UnitName = this._string });

                    _result.Add(new MsUnit(_row.UnitCode, _row.UnitName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetUnitNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msUnit in this.db.MsUnits
                                where _msUnit.UnitCode == _prmCode
                                select new
                                {
                                    UnitName = _msUnit.UnitName
                                }
                            );

                foreach (var _obj in _query)
                {
                    _result = _obj.UnitName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetUnitCodeByName(string _prmName)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msUnit in this.db.MsUnits
                                where _msUnit.UnitName == _prmName
                                select new
                                {
                                    UnitCode = _msUnit.UnitCode
                                }
                            );

                foreach (var _obj in _query)
                {
                    _result = _obj.UnitCode;
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
                    MsUnit _msUnit = this.db.MsUnits.Single(_temp => _temp.UnitCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsUnits.DeleteOnSubmit(_msUnit);
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

        public MsUnit GetSingle(string _prmCode)
        {
            MsUnit _result = null;

            try
            {
                _result = this.db.MsUnits.Single(_temp => _temp.UnitCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(MsUnit _prmMsUnit)
        {
            bool _result = false;

            try
            {
                this.db.MsUnits.InsertOnSubmit(_prmMsUnit);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsUnit _prmMsUnit)
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

        public bool IsUnitExists(String _prmUnit)
        {
            bool _result = false;

            try
            {
                var _query = from _msUnit in this.db.MsUnits
                             where _msUnit.UnitCode == _prmUnit
                             select new
                             {
                                 _msUnit.UnitCode
                             };

                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProductConvert> GetListForDDLProductConvert(String _prmProductCode)
        {
            List<MsProductConvert> _result = new List<MsProductConvert>();

            try
            {
                var _query = (
                                from _msUnit in this.db.MsProductConverts
                                where _msUnit.ProductCode == _prmProductCode
                                orderby _msUnit.ProductCode descending
                                select new
                                {
                                    UnitCode = _msUnit.UnitCode,
                                    UnitConvert = _msUnit.UnitConvert
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { UnitCode = this._string, UnitConvert = this._string });

                    _result.Add(new MsProductConvert(_row.UnitCode, _row.UnitConvert));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region Convertion

        public double RowsCountConvertion(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "UnitName")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "UnitConvert")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _msConvertion in this.db.MsConvertions
                            join _msUnit in this.db.MsUnits
                            on _msConvertion.UnitCode equals _msUnit.UnitCode
                            join _msUnit2 in this.db.MsUnits
                            on _msConvertion.UnitConvert equals _msUnit2.UnitCode
                            where (SqlMethods.Like(_msUnit.UnitName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msUnit2.UnitName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msConvertion
                        ).Count();

            _result = _query;

            return _result;
        }


        public List<MsConvertion> GetListConvertion(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsConvertion> _result = new List<MsConvertion>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "UnitName")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "UnitConvert")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _msConvertion in this.db.MsConvertions
                                join _msUnit in this.db.MsUnits
                                on _msConvertion.UnitCode equals _msUnit.UnitCode
                                join _msUnit2 in this.db.MsUnits
                                on _msConvertion.UnitConvert equals _msUnit2.UnitCode
                                where (SqlMethods.Like(_msUnit.UnitName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msUnit2.UnitName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msConvertion.UserDate descending
                                select new
                                {
                                    UnitCode = _msConvertion.UnitCode,
                                    UnitCodeName = _msUnit.UnitName,
                                    UnitConvert = _msConvertion.UnitConvert,
                                    UnitConvertName = _msUnit2.UnitName,
                                    Rate = _msConvertion.Rate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { UnitCode = this._string, UnitCodeName = this._string, UnitConvert = this._string, UnitConvertName = this._string, Rate = this._decimal });

                    _result.Add(new MsConvertion(_row.UnitCode, _row.UnitCodeName, _row.UnitConvert, _row.UnitConvertName, _row.Rate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiConvertion(string[] _prmCode)
        {
            bool _result = false;


            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('&');

                    MsConvertion _msConvertion = this.db.MsConvertions.Single(_temp => _temp.UnitConvert.Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.UnitCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower());

                    this.db.MsConvertions.DeleteOnSubmit(_msConvertion);
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

        public MsConvertion GetSingleConvertion(string _prmUnitCode, string _prmUnitConvert)
        {
            MsConvertion _result = null;

            try
            {
                _result = this.db.MsConvertions.Single(_temp => _temp.UnitCode.Trim().ToLower() == _prmUnitCode.Trim().ToLower() && _temp.UnitConvert.Trim().ToLower() == _prmUnitConvert.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddConvertion(MsConvertion _prmMsConvertion)
        {
            bool _result = false;

            try
            {
                this.db.MsConvertions.InsertOnSubmit(_prmMsConvertion);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditConvertion(MsConvertion _prmMsConvertion)
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

        public decimal FindConvertionUnit(string _prmProductCode, string _prmUnitFrom, string _prmUnitTo)
        {
            decimal _result = 0;
            string _errorMsg = "";

            try
            {
                var _rate = this.db.S_FindConvertion(_prmProductCode, _prmUnitFrom, _prmUnitTo);

                foreach (var _row in _rate)
                {
                    _row.Rate = (_row.Rate == null) ? 0 : _row.Rate;
                    _result = Convert.ToDecimal(_row.Rate);
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        #endregion

        ~UnitBL()
        {
        }

    }
}
