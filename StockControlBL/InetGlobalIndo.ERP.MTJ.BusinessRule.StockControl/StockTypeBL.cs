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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public partial class StockTypeBL : Base
    {
        public StockTypeBL()
        {

        }
        #region StockTypeHD
        public double RowsCountStockType(string _prmCategory, string _prmKeyword)
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
                    from _msStockType in this.db.MsStockTypes
                    where (SqlMethods.Like(_msStockType.StockTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msStockType.StockTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msStockType

                ).Count();

            _result = _query;
            return _result;
        }

        public List<MsStockType> GetListStockType(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsStockType> _result = new List<MsStockType>();
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
                                from _msStockType in this.db.MsStockTypes
                                where (SqlMethods.Like(_msStockType.StockTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msStockType.StockTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msStockType.UserDate descending
                                select new
                                {
                                    StockTypeCode = _msStockType.StockTypeCode,
                                    StockTypeName = _msStockType.StockTypeName,
                                    Account = _msStockType.Account,

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { StockTypeCode = this._string, StockTypeName = this._string, Account = this._string });

                    _result.Add(new MsStockType(_row.StockTypeCode, _row.StockTypeName, _row.Account));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiStockType(string[] _prmStockTypeCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmStockTypeCode.Length; i++)
                {
                    MsStockType _msStockType = this.db.MsStockTypes.Single(_temp => _temp.StockTypeCode.Trim().ToLower() == _prmStockTypeCode[i].Trim().ToLower());

                    if (_msStockType != null)
                    {
                        var _query = (from _detail in this.db.MsStockTypeDts
                                      where _detail.StockType.Trim().ToLower() == _prmStockTypeCode[i].Trim().ToLower()
                                      select _detail);

                        this.db.MsStockTypeDts.DeleteAllOnSubmit(_query);

                        this.db.MsStockTypes.DeleteOnSubmit(_msStockType);
                    }
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

        public MsStockType GetSingleStockType(string _prmStockTypeCode)
        {
            MsStockType _result = null;

            try
            {
                _result = this.db.MsStockTypes.Single(_temp => _temp.StockTypeCode == _prmStockTypeCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddStockType(MsStockType _prmMsStockType)
        {
            bool _result = false;

            try
            {
                this.db.MsStockTypes.InsertOnSubmit(_prmMsStockType);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditStockType(MsStockType _prmMsStockType)
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

        public string GetStockTypeNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msStockType in this.db.MsStockTypes
                                where _msStockType.StockTypeCode.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    StockTypeName = _msStockType.StockTypeName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.StockTypeName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsStockType _prmMsStockType)
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

        #region StockTypeDt
        public List<MsStockTypeDt> GetListTransTypeForStockType(int _prmReqPage, int _prmPageSize, string _prmStockType)
        {
            List<MsStockTypeDt> _result = new List<MsStockTypeDt>();

            try
            {
                var _query =
                            (
                                from _stockTypeDt in this.db.MsStockTypeDts
                                where _stockTypeDt.StockType == _prmStockType
                                select new
                                {
                                    StockType = _stockTypeDt.StockType,
                                    TransTypeCode = _stockTypeDt.TransType,
                                    UserId = _stockTypeDt.UserId,
                                    UserDate = _stockTypeDt.UserDate,
                                    TransTypeName = ( 
                                                        from _transType in this.db.MsTransTypes 
                                                        where _transType.TransTypeCode == _stockTypeDt.TransType
                                                        select _transType.TransTypeName
                                                     ).FirstOrDefault(),
                                    FgAccount = (
                                                    from _transType in this.db.MsTransTypes
                                                    where _transType.TransTypeCode == _stockTypeDt.TransType
                                                    select _transType.FgAccount
                                                 ).FirstOrDefault(),
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsStockTypeDt(_row.StockType, _row.TransTypeCode, _row.UserId, _row.UserDate, _row.TransTypeName, _row.FgAccount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public int RowsCountForStockTypeDt(string _prmStockTypeCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.MsStockTypeDts.Where(_temp => _temp.StockType == _prmStockTypeCode).Count();

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddStockTypeDt(string[] _prmTransTypeCode, string _prmStockTypeCode, bool _prmGrabAll)
        {
            bool _result = false;

            List<MsStockTypeDt> _listMSStockTypeDt = new List<MsStockTypeDt>();

            try
            {
                if (_prmGrabAll == false)
                {
                    for (int i = 0; i < _prmTransTypeCode.Length; i++)
                    {
                        MsStockTypeDt _msStockTypeDt = new MsStockTypeDt();

                        _msStockTypeDt.StockType = _prmStockTypeCode;
                        _msStockTypeDt.TransType = _prmTransTypeCode[i];

                        _msStockTypeDt.UserId = this._currentUser;
                        _msStockTypeDt.UserDate = DateTime.Now;

                        _listMSStockTypeDt.Add(_msStockTypeDt);
                    }
                }
                else if (_prmGrabAll == true)
                {
                    var _query = (
                                   from _msTransType in this.db.MsTransTypes
                                   where
                                         !(
                                            from _msStockTypeDt in this.db.MsStockTypeDts
                                            where _msStockTypeDt.StockType.Trim().ToLower() == _prmStockTypeCode.Trim().ToLower()
                                            select _msStockTypeDt.TransType
                                         ).Contains(_msTransType.TransTypeCode)
                                   select new
                                   {
                                       TransTypeCode = _msTransType.TransTypeCode
                                   }
                               );

                    foreach (var _row in _query)
                    {
                        _listMSStockTypeDt.Add(new MsStockTypeDt(_prmStockTypeCode, _row.TransTypeCode, this._currentUser, DateTime.Now));
                    }
                }

                if (_listMSStockTypeDt.Count() > 0)
                {
                    this.db.MsStockTypeDts.InsertAllOnSubmit(_listMSStockTypeDt);

                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsStockType> GetListStockTypeDtForDDL()
        {
            List<MsStockType> _result = new List<MsStockType>();

            try
            {
                var _query = (
                                from _msStockTypeDt in this.db.MsStockTypeDts
                                where _msStockTypeDt.TransType.Trim().ToLower() == AppModule.GetValue(TransactionType.StockAdjustment).Trim().ToLower()
                                select new
                                {
                                    StockTypeCode = _msStockTypeDt.StockType,
                                    StockTypeName = (
                                                        from _msStockType in this.db.MsStockTypes
                                                        where _msStockType.StockTypeCode.Trim().ToLower() == _msStockTypeDt.StockType.Trim().ToLower()
                                                        select _msStockType.StockTypeName
                                    ).FirstOrDefault()
                                }
                              );

                foreach (var _row in _query)
                {
                    _result.Add(new MsStockType(_row.StockTypeCode, _row.StockTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsStockType> GetListBeginFromStockTypeDtForDDL()
        {
            List<MsStockType> _result = new List<MsStockType>();

            try
            {
                var _query = (
                                from _msStockTypeDt in this.db.MsStockTypeDts
                                where _msStockTypeDt.TransType.Trim().ToLower() == AppModule.GetValue(TransactionType.StockBeginning).Trim().ToLower()
                                select new
                                {
                                    StockTypeCode = _msStockTypeDt.StockType,
                                    StockTypeName = (
                                                        from _msStockType in this.db.MsStockTypes
                                                        where _msStockType.StockTypeCode.Trim().ToLower() == _msStockTypeDt.StockType.Trim().ToLower()
                                                        select _msStockType.StockTypeName
                                    ).FirstOrDefault()
                                }
                              );

                foreach (var _row in _query)
                {
                    _result.Add(new MsStockType(_row.StockTypeCode, _row.StockTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsStockType> GetListForDDLStockReceivingOther()
        {
            List<MsStockType> _result = new List<MsStockType>();

            try
            {
                var _query = (
                                from _msStockTypeDt in this.db.MsStockTypeDts
                                where _msStockTypeDt.TransType.Trim().ToLower() == AppModule.GetValue(TransactionType.StockReceivingOther).Trim().ToLower()
                                select new
                                {
                                    StockTypeCode = _msStockTypeDt.StockType,
                                    StockTypeName = (
                                                        from _msStockType in this.db.MsStockTypes
                                                        where _msStockType.StockTypeCode.Trim().ToLower() == _msStockTypeDt.StockType.Trim().ToLower()
                                                        select _msStockType.StockTypeName
                                    ).FirstOrDefault()
                                }
                              );

                foreach (var _row in _query)
                {
                    _result.Add(new MsStockType(_row.StockTypeCode, _row.StockTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsStockType> GetListForDDLStockReceivingCustomer()
        {
            List<MsStockType> _result = new List<MsStockType>();

            try
            {
                var _query = (
                                from _msStockTypeDt in this.db.MsStockTypeDts
                                where _msStockTypeDt.TransType.Trim().ToLower() == AppModule.GetValue(TransactionType.StockReceivingCustomer).Trim().ToLower()
                                select new
                                {
                                    StockTypeCode = _msStockTypeDt.StockType,
                                    StockTypeName = (
                                                        from _msStockType in this.db.MsStockTypes
                                                        where _msStockType.StockTypeCode.Trim().ToLower() == _msStockTypeDt.StockType.Trim().ToLower()
                                                        select _msStockType.StockTypeName
                                    ).FirstOrDefault()
                                }
                              );

                foreach (var _row in _query)
                {
                    _result.Add(new MsStockType(_row.StockTypeCode, _row.StockTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsStockType> GetListForDDLStockReceivingSupplier()
        {
            List<MsStockType> _result = new List<MsStockType>();

            try
            {
                var _query = (
                                from _msStockTypeDt in this.db.MsStockTypeDts
                                where _msStockTypeDt.TransType.Trim().ToLower() == AppModule.GetValue(TransactionType.StockReceivingSupplier).Trim().ToLower()
                                select new
                                {
                                    StockTypeCode = _msStockTypeDt.StockType,
                                    StockTypeName = (
                                                        from _msStockType in this.db.MsStockTypes
                                                        where _msStockType.StockTypeCode.Trim().ToLower() == _msStockTypeDt.StockType.Trim().ToLower()
                                                        select _msStockType.StockTypeName
                                    ).FirstOrDefault()
                                }
                              );

                foreach (var _row in _query)
                {
                    _result.Add(new MsStockType(_row.StockTypeCode, _row.StockTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsStockType> GetListForDDLStockIssueSlip()
        {
            List<MsStockType> _result = new List<MsStockType>();

            try
            {
                var _query = (
                                from _msStockTypeDt in this.db.MsStockTypeDts
                                where _msStockTypeDt.TransType.Trim().ToLower() == AppModule.GetValue(TransactionType.StockIssueSlip).Trim().ToLower()
                                select new
                                {
                                    StockTypeCode = _msStockTypeDt.StockType,
                                    StockTypeName = (
                                                        from _msStockType in this.db.MsStockTypes
                                                        where _msStockType.StockTypeCode.Trim().ToLower() == _msStockTypeDt.StockType.Trim().ToLower()
                                                        select _msStockType.StockTypeName
                                    ).FirstOrDefault()
                                }
                              );

                foreach (var _row in _query)
                {
                    _result.Add(new MsStockType(_row.StockTypeCode, _row.StockTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiStockTypeDt(string[] _prmTransTypeCode, string _prmStockTypeCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmTransTypeCode.Length; i++)
                {
                    MsStockTypeDt _msStockTypeDt = this.db.MsStockTypeDts.Single(_temp => _temp.TransType.Trim().ToLower() == _prmTransTypeCode[i].Trim().ToLower() && _temp.StockType.ToLower().Trim() == _prmStockTypeCode.ToLower().Trim());

                    this.db.MsStockTypeDts.DeleteOnSubmit(_msStockTypeDt);
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

        public int RowsCountTransType(string _prmCode)
        {
            int _result = 0;

            _result = (
                           from _msTransType in this.db.MsTransTypes
                           where
                                 !(
                                    from _msStockTypeDt in this.db.MsStockTypeDts
                                    where _msStockTypeDt.StockType.Trim().ToLower() == _prmCode.Trim().ToLower()
                                    select _msStockTypeDt.TransType
                                 ).Contains(_msTransType.TransTypeCode)
                           select new
                           {
                               TransTypeCode = _msTransType.TransTypeCode
                           }
                       ).Count();

            return _result;
        }

        public List<MsTransType> GetListTransType(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<MsTransType> _result = new List<MsTransType>();

            try
            {
                var _query = (
                                   from _msTransType in this.db.MsTransTypes
                                   where
                                         !(
                                            from _msStockTypeDt in this.db.MsStockTypeDts
                                            where _msStockTypeDt.StockType.Trim().ToLower() == _prmCode.Trim().ToLower()
                                            select _msStockTypeDt.TransType
                                         ).Contains(_msTransType.TransTypeCode)
                                   select new
                                   {
                                       TransTypeCode = _msTransType.TransTypeCode,
                                       TransTypeName = _msTransType.TransTypeName
                                   }
                               ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsTransType(_row.TransTypeCode, _row.TransTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~StockTypeBL()
        {

        }
    }
}
