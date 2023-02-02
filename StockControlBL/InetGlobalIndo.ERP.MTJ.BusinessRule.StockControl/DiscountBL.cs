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
    public sealed class DiscountBL : Base
    {
        public DiscountBL()
        {

        }

        #region Discount

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
                            from _master_Discount in this.db.Master_Discounts
                            where (SqlMethods.Like(_master_Discount.DiscountID.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_master_Discount.DiscountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _master_Discount
                        ).Count();

            _result = _query;

            return _result;
        }


        public List<Master_Discount> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_Discount> _result = new List<Master_Discount>();

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
                                from _master_Discount in this.db.Master_Discounts
                                where (SqlMethods.Like(_master_Discount.DiscountID.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_master_Discount.DiscountName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _master_Discount.EditDate descending
                                select new
                                {
                                    DiscountCode = _master_Discount.DiscountCode,
                                    DiscountID = _master_Discount.DiscountID,
                                    DiscountName = _master_Discount.DiscountName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Discount(_row.DiscountCode, _row.DiscountID, _row.DiscountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Discount> GetListForDDL()
        {
            List<Master_Discount> _result = new List<Master_Discount>();

            try
            {
                var _query = (
                                from _master_Discount in this.db.Master_Discounts
                                orderby _master_Discount.DiscountName ascending
                                select new
                                {
                                    DiscountCode = _master_Discount.DiscountCode,
                                    DiscountName = _master_Discount.DiscountName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Discount(_row.DiscountCode, _row.DiscountName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetDiscountNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _master_Discount in this.db.Master_Discounts
                                where _master_Discount.DiscountCode == new Guid(_prmCode)
                                select new
                                {
                                    DiscountName = _master_Discount.DiscountName
                                }
                            );

                foreach (var _obj in _query)
                {
                    _result = _obj.DiscountName;
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
                    Master_Discount _master_Discount = this.db.Master_Discounts.Single(_temp => _temp.DiscountCode == new Guid(_prmCode[i]));

                    this.db.Master_Discounts.DeleteOnSubmit(_master_Discount);
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

        public Master_Discount GetSingle(string _prmCode)
        {
            Master_Discount _result = null;

            try
            {
                _result = this.db.Master_Discounts.Single(_temp => _temp.DiscountCode == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        private bool IsDiscountIDExists(string _prmDiscountID, Guid _prmDiscountCode)
        {
            bool _result = false;

            try
            {
                var _query = from _master_Discount in this.db.Master_Discounts
                             where (_master_Discount.DiscountID == _prmDiscountID) && (_master_Discount.DiscountCode != _prmDiscountCode)
                             select new
                             {
                                 _master_Discount.DiscountID
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

        public bool Add(Master_Discount _prmMaster_Discount)
        {
            bool _result = false;

            try
            {
                if (this.IsDiscountIDExists(_prmMaster_Discount.DiscountID, _prmMaster_Discount.DiscountCode) == false)
                {
                    this.db.Master_Discounts.InsertOnSubmit(_prmMaster_Discount);

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

        public bool Edit(Master_Discount _prmMaster_Discount)
        {
            bool _result = false;

            try
            {
                if (this.IsDiscountIDExists(_prmMaster_Discount.DiscountID, _prmMaster_Discount.DiscountCode) == false)
                {
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

        #endregion

        #region DiscountLevel

        public double RowsCountDiscountLevel(string _prmDiscountCode)
        {
            double _result = 0;

            var _query =
                        (
                            from _master_DiscountLevel in this.db.Master_DiscountLevels
                            where _master_DiscountLevel.DiscountCode == new Guid(_prmDiscountCode)
                            select _master_DiscountLevel
                        ).Count();

            _result = _query;

            return _result;
        }


        public List<Master_DiscountLevel> GetListDiscountLevel(int _prmReqPage, int _prmPageSize, string _prmDiscountCode)
        {
            List<Master_DiscountLevel> _result = new List<Master_DiscountLevel>();

            try
            {
                var _query = (
                                from _master_DiscountLevel in this.db.Master_DiscountLevels
                                where _master_DiscountLevel.DiscountCode == new Guid(_prmDiscountCode)
                                orderby _master_DiscountLevel.Level ascending
                                select new
                                {
                                    DiscountCode = _master_DiscountLevel.DiscountCode,
                                    Level = _master_DiscountLevel.Level,
                                    Discount = _master_DiscountLevel.Discount
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_DiscountLevel(_row.DiscountCode, _row.Level, _row.Discount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiDiscountLevel(string[] _prmCode)
        {
            bool _result = false;


            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('&');

                    Master_DiscountLevel _master_DiscountLevel = this.db.Master_DiscountLevels.Single(_temp => _temp.Level == Convert.ToByte(_tempSplit[1]) && _temp.DiscountCode == new Guid(_tempSplit[0]));

                    this.db.Master_DiscountLevels.DeleteOnSubmit(_master_DiscountLevel);
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

        public Master_DiscountLevel GetSingleDiscountLevel(string _prmDiscountCode, string _prmLevel)
        {
            Master_DiscountLevel _result = null;

            try
            {
                _result = this.db.Master_DiscountLevels.Single(_temp => _temp.DiscountCode == new Guid(_prmDiscountCode) && _temp.Level == Convert.ToByte(_prmLevel));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddDiscountLevel(Master_DiscountLevel _prmMaster_DiscountLevel)
        {
            bool _result = false;

            try
            {
                this.db.Master_DiscountLevels.InsertOnSubmit(_prmMaster_DiscountLevel);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditDiscountLevel(Master_DiscountLevel _prmMaster_DiscountLevel)
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

        public List<Byte> GetListDiscLevelForProduct(Guid _prmDiscCode)
        {
            List<Byte> _result = new List<Byte>();

            try
            {
                var _query = (
                                from _master_DiscountLevel in this.db.Master_DiscountLevels
                                where _master_DiscountLevel.DiscountCode == _prmDiscCode
                                select _master_DiscountLevel.Discount
                          );

                foreach (Byte _row in _query)
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

        #endregion

        ~DiscountBL()
        {
        }

    }
}
