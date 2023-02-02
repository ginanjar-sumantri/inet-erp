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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule
{
    public sealed class BankBL : Base
    {
        public BankBL()
        {

        }

        #region Bank

        public int RowsCount
        {
            get
            {
                return this.db.MsBanks.Count();
            }
        }

        public List<MsBank> GetList(int _prmReqPage, int _prmPageSize)
        {
            List<MsBank> _result = new List<MsBank>();

            try
            {
                var _query = (
                                from _msBank in this.db.MsBanks
                                orderby _msBank.UserDate descending
                                select new
                                {
                                    BankCode = _msBank.BankCode,
                                    BankName = _msBank.BankName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { BankCode = this._string, BankName = this._string });

                    _result.Add(new MsBank(_row.BankCode, _row.BankName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsBank> GetList()
        {
            List<MsBank> _result = new List<MsBank>();

            try
            {
                var _query = (
                                from _msBank in this.db.MsBanks
                                orderby _msBank.UserDate descending
                                select new
                                {
                                    BankCode = _msBank.BankCode,
                                    BankName = _msBank.BankName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { BankCode = this._string, BankName = this._string });

                    _result.Add(new MsBank(_row.BankCode, _row.BankName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MsBank GetSingle(string _prmCode)
        {
            MsBank _result = null;

            try
            {
                _result = this.db.MsBanks.Single(_temp => _temp.BankCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetBankNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msBank in this.db.MsBanks
                                where _msBank.BankCode == _prmCode
                                select new
                                {
                                    BankName = _msBank.BankName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.BankName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetBankPaymentNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _vMsBankPayment in this.db.V_MsBankPayments
                                where _vMsBankPayment.BankCode == _prmCode
                                select new
                                {
                                    BankName = _vMsBankPayment.BankName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.BankName;
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
                    MsBank _msBank = this.db.MsBanks.Single(_temp => _temp.BankCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsBanks.DeleteOnSubmit(_msBank);
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

        public bool Add(MsBank _prmMsBank)
        {
            bool _result = false;

            try
            {
                this.db.MsBanks.InsertOnSubmit(_prmMsBank);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsBank _prmMsBank)
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

        public bool IsBankExists(String _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _bank in this.db.MsBanks
                                where _bank.BankCode == _prmCode
                                select _bank
                             );

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

        #endregion


        ~BankBL()
        {
        }
    }
}
