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
    public sealed class DebitCardBL : Base
    {
        public DebitCardBL()
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
                             from _posMsDebitCards in this.db.POSMsDebitCards
                             where (SqlMethods.Like(_posMsDebitCards.DebitCardCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_posMsDebitCards.DebitCardName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posMsDebitCards
                        ).Count();

            _result = _query;

            return _result;
        }


        public List<POSMsDebitCard> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsDebitCard> _result = new List<POSMsDebitCard>();

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
                                from _posMsDebitCard in this.db.POSMsDebitCards
                                where (SqlMethods.Like(_posMsDebitCard.DebitCardCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsDebitCard.DebitCardName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsDebitCard.DebitCardName descending
                                select _posMsDebitCard
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

        public List<POSMsDebitCard> GetList()
        {
            List<POSMsDebitCard> _result = new List<POSMsDebitCard>();

            try
            {
                var _query = (
                                from _posMsDebitCard in this.db.POSMsDebitCards
                                orderby _posMsDebitCard.DebitCardName ascending
                                select _posMsDebitCard
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
                    POSMsDebitCard _posMsDebitCard = this.db.POSMsDebitCards.Single(_temp => _temp.DebitCardCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.POSMsDebitCards.DeleteOnSubmit(_posMsDebitCard);
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

        public POSMsDebitCard GetSingle(string _prmCode)
        {
            POSMsDebitCard _result = null;

            try
            {
                _result = this.db.POSMsDebitCards.Single(_temp => _temp.DebitCardCode == _prmCode);
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
                                from _posMsDebitCards in this.db.POSMsDebitCards
                                where _posMsDebitCards.DebitCardCode == _prmCode
                                select new
                                {
                                    DebitCardName = _posMsDebitCards.DebitCardName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.DebitCardName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(POSMsDebitCard _prmPOSMsDebitCard)
        {
            bool _result = false;

            try
            {
                this.db.POSMsDebitCards.InsertOnSubmit(_prmPOSMsDebitCard);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(POSMsDebitCard _prmPOSMsDebitCard)
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

        public List<POSMsDebitCard> GetDebitCardListDDL()
        {
            List<POSMsDebitCard> _result = new List<POSMsDebitCard>();

            try
            {
                var _query = (
                                from _msDebitCard in this.db.POSMsDebitCards
                                //where _msCustomer.CompanyID == _prmCompanyID
                                select new
                                {
                                    DebitCardCode = _msDebitCard.DebitCardCode,
                                    DebitCardName = _msDebitCard.DebitCardName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new POSMsDebitCard(_row.DebitCardCode, _row.DebitCardName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        #endregion

        ~DebitCardBL()
        {
        }
    }
}
