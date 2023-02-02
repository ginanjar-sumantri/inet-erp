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
    public sealed class CreditCardBL : Base
    {
        public CreditCardBL()
        {
        }

        #region Member

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
                             from _posMsCreditCard in this.db.POSMsCreditCards
                             where (SqlMethods.Like(_posMsCreditCard.CreditCardCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_posMsCreditCard.CreditCardName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posMsCreditCard
                        ).Count();

            _result = _query;

            return _result;
        }
        
        public List<POSMsCreditCard> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsCreditCard> _result = new List<POSMsCreditCard>();

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
                                from _posMsCreditCard in this.db.POSMsCreditCards
                                where (SqlMethods.Like(_posMsCreditCard.CreditCardCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsCreditCard.CreditCardName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsCreditCard.CreditCardName descending
                                select _posMsCreditCard
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

        public List<POSMsCreditCard> GetList()
        {
            List<POSMsCreditCard> _result = new List<POSMsCreditCard>();

            try
            {
                var _query = (
                                from _posMsCreditCard in this.db.POSMsCreditCards
                                orderby _posMsCreditCard.CreditCardName ascending
                                select _posMsCreditCard
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

        public List<POSMsCreditCard> GetList(String _prmCreditType)
        {
            List<POSMsCreditCard> _result = new List<POSMsCreditCard>();

            try
            {
                var _query = (
                                from _posMsCreditCard in this.db.POSMsCreditCards
                                where _posMsCreditCard.CreditCardTypeCode.Trim().ToLower() == _prmCreditType.Trim().ToLower()
                                orderby _posMsCreditCard.CreditCardName ascending
                                select _posMsCreditCard
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
                    POSMsCreditCard _posMsCreditCard = this.db.POSMsCreditCards.Single(_temp => _temp.CreditCardCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.POSMsCreditCards.DeleteOnSubmit(_posMsCreditCard);
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

        public POSMsCreditCard GetSingle(string _prmCode)
        {
            POSMsCreditCard _result = null;

            try
            {
                _result = this.db.POSMsCreditCards.Single(_temp => _temp.CreditCardCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetMemberNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _posMsCreditCard in this.db.POSMsCreditCards
                                where _posMsCreditCard.CreditCardCode == _prmCode
                                select new
                                {
                                    CreditCardName = _posMsCreditCard.CreditCardName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CreditCardName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(POSMsCreditCard _prmPOSMsCreditCard)
        {
            bool _result = false;

            try
            {
                this.db.POSMsCreditCards.InsertOnSubmit(_prmPOSMsCreditCard);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(POSMsCreditCard _prmPOSMsCreditCard)
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

        public List<POSMsCreditCard> GetCreditCardListDDL(String _prmCreditCardType)
        {
            List<POSMsCreditCard> _result = new List<POSMsCreditCard>();

            try
            {
                var _query = (
                                from _msCreditCard in this.db.POSMsCreditCards
                                where _msCreditCard.CreditCardTypeCode == _prmCreditCardType
                                select new
                                {
                                    CreditCardCode = _msCreditCard.CreditCardCode,
                                    CreditCardName = _msCreditCard.CreditCardName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new POSMsCreditCard(_row.CreditCardCode, _row.CreditCardName));
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String GetCreditCardTypeDDL(String _prmCreditCardCode)
        {
            String _result = "";

            var _query = (
                            from _msCreditCardType in this.db.POSMsCreditCardTypes
                            join _msCreditCard in this.db.POSMsCreditCards
                            on _msCreditCardType.CreditCardTypeCode equals _msCreditCard.CreditCardTypeCode
                            where _msCreditCard.CreditCardCode == _prmCreditCardCode
                            select _msCreditCard.CreditCardTypeCode
                            ).FirstOrDefault();

            _result = _query;
            return _result;
        }

        public String GetCreditCardType(String _prmCreditCardCode)
        {
            String _result = "";
            var _query = (
                                from _msCreditCardType in this.db.POSMsCreditCardTypes
                                where _msCreditCardType.CreditCardTypeCode == _prmCreditCardCode
                                select new
                                {
                                    CreditCardName = _msCreditCardType.CreditCardTypeName
                                }
                              );

            foreach (var _obj in _query)
            {
                _result = _obj.CreditCardName;
            }
            return _result;
        }

        #endregion

        ~CreditCardBL()
        {
        }
    }
}
