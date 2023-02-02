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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class PaymentBL : Base
    {
        public PaymentBL()
        {

        }

        #region PaymentType

        public double RowsCountPaymentType(string _prmCategory, string _prmKeyword)
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
                            from _msPayType in this.db.MsPayTypes
                            where (SqlMethods.Like(_msPayType.PayCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msPayType.PayName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msPayType.PayCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountPaymentTypeForReport()
        {
            double _result = 0;

            var _query =
                        (
                            from _msPayType in this.db.MsPayTypes
                            where (_msPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _msPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Payment))
                            select _msPayType.PayCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountReceiptTypeForReport()
        {
            double _result = 0;

            var _query =
                        (
                            from _msPayType in this.db.MsPayTypes
                            where (_msPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _msPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Receipt))
                            select _msPayType.PayCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MsPayType> GetListPaymentType(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsPayType> _result = new List<MsPayType>();

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
                                from _msPayType in this.db.MsPayTypes
                                join _msAccount in this.db.MsAccounts
                                    on _msPayType.Account equals _msAccount.Account
                                where (SqlMethods.Like(_msPayType.PayCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msPayType.PayName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msPayType.UserDate descending
                                select new
                                {
                                    PayCode = _msPayType.PayCode,
                                    PayName = _msPayType.PayName,
                                    Account = _msAccount.Account,
                                    AccountName = _msAccount.AccountName,
                                    BankName = (
                                                    from _msBank in this.db.MsBanks
                                                    where _msBank.BankCode == _msPayType.Bank
                                                    select _msBank.BankName
                                                ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { PayCode = this._string, PayName = this._string, Account = this._string, AccountName = this._string, BankName = this._string });

                    _result.Add(new MsPayType(_row.PayCode, _row.PayName, _row.Account, _row.AccountName, _row.BankName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListPaymentTypeForReport(int _prmReqPage, int _prmPageSize)
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _msPayType in this.db.MsPayTypes
                                where (_msPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _msPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Payment))
                                orderby _msPayType.PayName ascending
                                select new
                                {
                                    PayCode = _msPayType.PayCode,
                                    PayName = _msPayType.PayName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.PayCode, _row.PayName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListReceiptTypeForReport(int _prmReqPage, int _prmPageSize)
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _msPayType in this.db.MsPayTypes
                                where (_msPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _msPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Receipt))
                                orderby _msPayType.PayName ascending
                                select new
                                {
                                    PayCode = _msPayType.PayCode,
                                    PayName = _msPayType.PayName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.PayCode, _row.PayName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPaymentType(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsPayType _msPayType = this.db.MsPayTypes.Single(_temp => _temp.PayCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsPayTypes.DeleteOnSubmit(_msPayType);
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

        public MsPayType GetSinglePaymentType(string _prmCode)
        {
            MsPayType _result = null;

            try
            {
                _result = this.db.MsPayTypes.Single(_temp => _temp.PayCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAccountByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msPayType in this.db.MsPayTypes
                                where _msPayType.PayCode == _prmCode
                                select new
                                {
                                    Account = _msPayType.Account
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.Account;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetAccBankChargeNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msPayType in this.db.MsPayTypes
                                where _msPayType.AccBankCharge == _prmCode
                                select new
                                {
                                    AccBankCharge = _msPayType.AccBankCharge
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.AccBankCharge;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetCurrCodeByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msPayType in this.db.V_MsPayTypes
                                where _msPayType.Payment_Code == _prmCode
                                select new
                                {
                                    CurrCode = _msPayType.CurrCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CurrCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetPaymentName(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msPayType in this.db.MsPayTypes
                                where _msPayType.PayCode == _prmCode
                                select new
                                {
                                    PayName = _msPayType.PayName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.PayName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddPaymentType(MsPayType _prmMsPayType)
        {
            bool _result = false;

            try
            {
                this.db.MsPayTypes.InsertOnSubmit(_prmMsPayType);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPaymentType(MsPayType _prmMsPayType)
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

        public List<MsPayType> GetListDDLbyViewMsPayType(string _prmCurrCode)
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Bank) || _vMsPayType.FgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Giro) || _vMsPayType.FgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Kas) || _vMsPayType.FgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Other))
                                && (_vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Payment))
                                && _vMsPayType.CurrCode == _prmCurrCode
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLForSalaryPayment()
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Payment))
                                && (_vMsPayType.FgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Bank) || _vMsPayType.FgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Kas) || _vMsPayType.FgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Other))
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLByViewPayment()
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Payment))
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLPayment()
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Payment))
                                    && (_vMsPayType.FgMode != PaymentDataMapper.GetModePaymentType(ModePaymentType.DP))
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLPaymentWithoutCurrCode(String _prmCurrCode)
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Payment))
                                    && (_vMsPayType.FgMode != PaymentDataMapper.GetModePaymentType(ModePaymentType.DP))
                                    && _vMsPayType.CurrCode != _prmCurrCode
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLBank()
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Bank))
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLReceiptNonTrade(string _prmCurrCode)
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Bank) || _vMsPayType.FgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Giro) || _vMsPayType.FgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Kas) || _vMsPayType.FgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Other))
                                && (_vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Receipt))
                                && _vMsPayType.CurrCode == _prmCurrCode
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLGiroReceipt()
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Bank))
                                && (_vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Receipt))
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLGiroReceiptChangePay()
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgMode != PaymentDataMapper.GetModeReceiptType(ModeReceiptType.DP))
                                && (_vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Receipt))
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLDPCustomer(string _prmCurrCode)
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgMode != PaymentDataMapper.GetModeReceiptType(ModeReceiptType.DP))
                                && (_vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Receipt))
                                && _vMsPayType.CurrCode == _prmCurrCode
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLCustBillAcc(string _prmCurrCode)
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgMode == PaymentDataMapper.GetModeReceiptType(ModeReceiptType.Bank))
                                && (_vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Receipt) || _vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All))
                                && _vMsPayType.CurrCode == _prmCurrCode
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLDPSuppPay(string _prmCurrCode)
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgMode != PaymentDataMapper.GetModeReceiptType(ModeReceiptType.DP))
                                && (_vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Payment))
                                && _vMsPayType.CurrCode == _prmCurrCode
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListReceiptForDDL(int _prmReqPage, int _prmPageSize)
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where (_vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _vMsPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Receipt))
                                orderby _vMsPayType.Payment_Name ascending
                                select new
                                {
                                    Payment_Code = _vMsPayType.Payment_Code,
                                    Payment_Name = _vMsPayType.Payment_Name
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize); ;

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.Payment_Code, _row.Payment_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLbyViewMsBankPayment(string _prmCurrCode)
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _vMsbankPayment in this.db.V_MsBankPayments
                                where _vMsbankPayment.CurrCode == _prmCurrCode
                                orderby _vMsbankPayment.BankName ascending
                                select new
                                {
                                    PayCode = _vMsbankPayment.BankCode,
                                    PayName = _vMsbankPayment.BankName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.PayCode, _row.PayName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsPayType> GetListDDLGiroPayment()
        {
            List<MsPayType> _result = new List<MsPayType>();

            try
            {
                var _query = (
                                from _msPayType in this.db.MsPayTypes
                                where (_msPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.All) || _msPayType.FgType == PaymentDataMapper.GetTypePayment(TypePayment.Payment))
                                    && (_msPayType.FgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Giro) || _msPayType.FgMode == PaymentDataMapper.GetModePaymentType(ModePaymentType.Bank))
                                orderby _msPayType.PayName ascending
                                select new
                                {
                                    PayCode = _msPayType.PayCode,
                                    PayName = _msPayType.PayName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsPayType(_row.PayCode, _row.PayName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public char GetFgMode(string _prmPaymentCode)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _vMsPayType in this.db.V_MsPayTypes
                                where _vMsPayType.Payment_Code == _prmPaymentCode
                                select new
                                {
                                    FgMode = _vMsPayType.FgMode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.FgMode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsExist(string _prmPaymentCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _msPayType in this.db.MsPayTypes
                                where _msPayType.PayCode == _prmPaymentCode
                                select new
                                {
                                    PayCode = _msPayType.PayCode
                                }
                            ).Count();

                if (_query > 0)
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

        ~PaymentBL()
        {

        }
    }
}
