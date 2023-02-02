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
    public sealed class CreditCardTypeBL : Base
    {
        public CreditCardTypeBL()
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
                             from _posMsCreditCardType in this.db.POSMsCreditCardTypes
                             where (SqlMethods.Like(_posMsCreditCardType.CreditCardTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_posMsCreditCardType.CreditCardTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posMsCreditCardType
                        ).Count();

            _result = _query;

            return _result;
        }


        public List<POSMsCreditCardType> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsCreditCardType> _result = new List<POSMsCreditCardType>();

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
                                from _posMsCreditCardType in this.db.POSMsCreditCardTypes
                                where (SqlMethods.Like(_posMsCreditCardType.CreditCardTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsCreditCardType.CreditCardTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsCreditCardType.CreditCardTypeCode descending
                                select _posMsCreditCardType
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

        public List<POSMsCreditCardType> GetList()
        {
            List<POSMsCreditCardType> _result = new List<POSMsCreditCardType>();

            try
            {
                var _query = (
                                from _posMsCreditCardType in this.db.POSMsCreditCardTypes
                                orderby _posMsCreditCardType.CreditCardTypeCode ascending
                                select _posMsCreditCardType 
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
                    POSMsCreditCardType _posMsCreditCardType = this.db.POSMsCreditCardTypes.Single(_temp => _temp.CreditCardTypeCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.POSMsCreditCardTypes.DeleteOnSubmit(_posMsCreditCardType);
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

        public POSMsCreditCardType GetSingle(string _prmCode)
        {
            POSMsCreditCardType _result = null;

            try
            {
                _result = this.db.POSMsCreditCardTypes.Single(_temp => _temp.CreditCardTypeCode == _prmCode);
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
                                from _posMsCreditCardType in this.db.POSMsCreditCardTypes
                                where _posMsCreditCardType.CreditCardTypeCode == _prmCode
                                select new
                                {
                                    CreditCardTypeName = _posMsCreditCardType.CreditCardTypeName 
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CreditCardTypeName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(POSMsCreditCardType _prmPOSMsCreditCardType)
        {
            bool _result = false;

            try
            {
                this.db.POSMsCreditCardTypes.InsertOnSubmit(_prmPOSMsCreditCardType);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(POSMsCreditCardType _prmPOSMsCreditCardType)
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
        
        public List<POSMsCreditCardType> GetCreditCardTypeListDDL()
        {
            List<POSMsCreditCardType> _result = new List<POSMsCreditCardType>();

            try
            {
                var _query = (
                                from _msCreditCardType in this.db.POSMsCreditCardTypes
                                //where _msCustomer.CompanyID == _prmCompanyID
                                select new
                                {
                                    CreditCardTypeCode = _msCreditCardType.CreditCardTypeCode,
                                    CreditCardTypeName = _msCreditCardType.CreditCardTypeName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new POSMsCreditCardType(_row.CreditCardTypeCode, _row.CreditCardTypeName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }


        #endregion

        ~CreditCardTypeBL()
        {
        }
    }
}
