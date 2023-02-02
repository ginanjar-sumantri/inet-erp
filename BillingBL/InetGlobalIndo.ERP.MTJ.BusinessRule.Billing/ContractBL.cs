using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed class ContractBL : Base
    {
        public ContractBL()
        {
        }

        #region Contract

        public int RowsCountContract(string _prmCategory, string _prmKeyword)
        {
            int _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "CompanyName")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
            }

            _result = (
                           from _bilTrContracts in this.db.BILTrContracts
                           where
                                (SqlMethods.Like((_bilTrContracts.TransNmbr ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_bilTrContracts.FileNmbr.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && (SqlMethods.Like(_bilTrContracts.CompanyName.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                           select new
                           {
                               InvoiceHd = _bilTrContracts.TransNmbr
                           }
                       ).Count();

            return _result;
        }

        public List<BILTrContract> GetListContract(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<BILTrContract> _result = new List<BILTrContract>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "CompanyName")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _contract in this.db.BILTrContracts
                                where
                                    (SqlMethods.Like((_contract.TransNmbr ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_contract.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_contract.CompanyName.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _contract.TransDate descending
                                select new
                                {
                                    TransNmbr = _contract.TransNmbr,
                                    FileNmbr = _contract.FileNmbr,
                                    TransDate = _contract.TransDate,
                                    SalesConfirmationNoRef = _contract.SalesConfirmationNoRef,
                                    CompanyName = _contract.CompanyName,
                                    ResponsibleName = _contract.ResponsibleName,
                                    TitleName = _contract.TitleName,
                                    LetterProviderInformation = _contract.LetterProviderInformation,
                                    LetterCustomerInformation = _contract.LetterCustomerInformation,
                                    FinanceCustomerPIC = _contract.FinanceCustomerPIC,
                                    FinanceCustomerPhone = _contract.FinanceCustomerPhone,
                                    FinanceCustomerFax = _contract.FinanceCustomerFax,
                                    FinanceCustomerEmail = _contract.FinanceCustomerEmail,
                                    Status = _contract.Status
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new BILTrContract(_row.TransNmbr, _row.FileNmbr, Convert.ToDateTime(_row.TransDate.ToString()), _row.SalesConfirmationNoRef, _row.CompanyName, _row.ResponsibleName, _row.TitleName, _row.LetterProviderInformation, _row.LetterCustomerInformation, _row.FinanceCustomerPIC, _row.FinanceCustomerPhone, _row.FinanceCustomerFax, _row.FinanceCustomerEmail, Convert.ToByte(_row.Status)));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiContract(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    BILTrContract _bilTrContract = this.db.BILTrContracts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.BILTrContracts.DeleteOnSubmit(_bilTrContract);
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

        public bool EditContract(BILTrContract _prmBILTrContract)
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

        public BILTrContract GetSingleContract(String _prmTransNmbr)
        {
            BILTrContract _result = null;

            try
            {
                _result = this.db.BILTrContracts.Single(_temp => _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public BILTrContract GetContractBySCCode(String _prmSCCode)
        {
            BILTrContract _result = null;

            try
            {
                _result = (
                               from _contract in this.db.BILTrContracts
                               where _contract.SalesConfirmationNoRef == _prmSCCode
                               orderby _contract.TransDate descending
                               select  _contract
                          ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String AddtoBILTrContract(BILTrContract _prmBILTrContract)
        {
            String _result = "";
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                    foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                    {
                        _prmBILTrContract.TransNmbr = _item.Number;
                        _transactionNumber.TempTransNmbr = _item.Number;
                    }
                    this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                    this.db.BILTrContracts.InsertOnSubmit(_prmBILTrContract);

                    var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                    this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                    this.db.SubmitChanges();


                    _scope.Complete();

                    _result = _prmBILTrContract.TransNmbr;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public string GetAppr(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                int _success = this.db.spBIL_ContractGetAppr(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(string _prmTransNmbr, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spBIL_ContarctApprove(_prmTransNmbr, _prmYear, _prmPeriod, _prmuser, ref _result);

                    if (_result == "")
                    {
                        BILTrContract _bilTrContract = this.GetSingleContract(_prmTransNmbr);
                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_prmYear, _prmPeriod, AppModule.GetValue(TransactionType.Contract), this._companyTag, ""))
                        {
                            _bilTrContract.FileNmbr = item.Number;
                        }
                        _bilTrContract.ApprovedDate = DateTime.Now;

                        this.db.SubmitChanges();

                        _scope.Complete();

                        _result = "Approve Success";
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        #endregion
        //#region CustBillAccount

        //public int RowsCountCustBillAccount(string _prmCategory, string _prmKeyword)
        //{
        //    int _result = 0;

        //    string _pattern1 = "%%";
        //    string _pattern2 = "%%";

        //    if (_prmCategory == "CustomerBillingAccount")
        //    {
        //        _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
        //        _pattern2 = "%%";
        //    }
        //    else if (_prmCategory == "CustomerName")
        //    {
        //        _pattern1 = "%%";
        //        _pattern2 = "%" + _prmKeyword.Trim().ToLower() + "%";
        //    }

        //    _result =
        //               (
        //                   from _custBillAccount in this.db.Master_CustBillAccounts
        //                   join _msCustomer in this.db.MsCustomers
        //                        on _custBillAccount.CustCode equals _msCustomer.CustCode
        //                   where
        //                        (SqlMethods.Like(_custBillAccount.CustBillAccount.Trim().ToLower(), _pattern1))
        //                        && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2))
        //                   select new
        //                   {
        //                       CustBillCode = _custBillAccount.CustBillCode
        //                   }
        //               ).Count();

        //    return _result;
        //}

        //public List<Master_CustBillAccount> GetListCustBillAccount(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        //{
        //    List<Master_CustBillAccount> _result = new List<Master_CustBillAccount>();

        //    string _pattern1 = "%%";
        //    string _pattern2 = "%%";

        //    if (_prmCategory == "CustomerBillingAccount")
        //    {
        //        _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
        //        _pattern2 = "%%";
        //    }
        //    else if (_prmCategory == "CustomerName")
        //    {
        //        _pattern1 = "%%";
        //        _pattern2 = "%" + _prmKeyword.Trim().ToLower() + "%";
        //    }

        //    try
        //    {
        //        var _query =
        //                    (
        //                        from _custBillAccount in this.db.Master_CustBillAccounts
        //                        join _msCustomer in this.db.MsCustomers
        //                            on _custBillAccount.CustCode equals _msCustomer.CustCode
        //                        where
        //                            (SqlMethods.Like(_custBillAccount.CustBillAccount.Trim().ToLower(), _pattern1))
        //                            && (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern2))
        //                        orderby _custBillAccount.EditDate descending
        //                        select new
        //                        {
        //                            CustBillCode = _custBillAccount.CustBillCode,
        //                            CustBillAccount = _custBillAccount.CustBillAccount,
        //                            CustCode = _custBillAccount.CustCode,
        //                            CustName = _msCustomer.CustName,
        //                            ProductCode = _custBillAccount.ProductCode,
        //                            ProductName = (
        //                                            from _msProduct in this.db.MsProducts
        //                                            where _msProduct.ProductCode == _custBillAccount.ProductCode
        //                                            select _msProduct.ProductName
        //                                          ).FirstOrDefault(),
        //                            CustBillDescription = _custBillAccount.CustBillDescription,
        //                            CurrCode = _custBillAccount.CurrCode,
        //                            AmountForex = _custBillAccount.AmountForex,
        //                            fgActive = _custBillAccount.fgActive
        //                        }
        //                    ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new Master_CustBillAccount(_row.CustBillCode, _row.CustBillAccount, _row.CustCode, _row.CustName, _row.ProductCode, _row.ProductName, _row.CustBillDescription, _row.CurrCode, _row.AmountForex, _row.fgActive));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool EditCustBillAccount(Master_CustBillAccount _prmCustBillAccount)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        if (this.IsCustBillAccountExists(_prmCustBillAccount.CustBillCode, _prmCustBillAccount.CustBillAccount) == false)
        //        {
        //            this.db.SubmitChanges();

        //            _result = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public string AddCustBillAccount(Master_CustBillAccount _prmCustBillAccount)
        //{
        //    string _result = "";

        //    try
        //    {
        //        if (this.IsCustBillAccountExists(_prmCustBillAccount.CustBillCode, _prmCustBillAccount.CustBillAccount) == false)
        //        {
        //            this.db.S_SAAutoNmbrAlphabet(DateTime.Now.Year, DateTime.Now.Month, AppModule.GetValue(TransactionType.CustomerBillingAccount), ref _result, _prmCustBillAccount.CustCode);

        //            if (_result != "")
        //            {
        //                _prmCustBillAccount.CustBillAccount = _result;
        //                this.db.Master_CustBillAccounts.InsertOnSubmit(_prmCustBillAccount);
        //                this.db.SubmitChanges();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public bool DeleteMultiCustBillAccount(string[] _prmCustBillCode)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        for (int i = 0; i < _prmCustBillCode.Length; i++)
        //        {
        //            Master_CustBillAccount _msCustBillAccount = this.db.Master_CustBillAccounts.Single(_temp => _temp.CustBillCode == new Guid(_prmCustBillCode[i]));

        //            this.db.Master_CustBillAccounts.DeleteOnSubmit(_msCustBillAccount);
        //        }

        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public Master_CustBillAccount GetSingleCustBillAccount(Guid _prmCustBillCode)
        //{
        //    Master_CustBillAccount _result = null;

        //    try
        //    {
        //        _result = this.db.Master_CustBillAccounts.Single(_temp => _temp.CustBillCode == _prmCustBillCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //private bool IsCustBillAccountExists(Guid _prmCustBillCode, string _prmCustBillAccount)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        var _query = from _custBillAccount in this.db.Master_CustBillAccounts
        //                     where (_custBillAccount.CustBillAccount == _prmCustBillAccount) && (_custBillAccount.CustBillCode != _prmCustBillCode)
        //                     select new
        //                     {
        //                         _custBillAccount.CustBillAccount
        //                     };

        //        if (_query.Count() > 0)
        //        {
        //            _result = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<Master_CustBillAccount> GetListDDLCustBillAccount(string _prmCustCode, string _prmCurrCode)
        //{
        //    List<Master_CustBillAccount> _result = new List<Master_CustBillAccount>();

        //    try
        //    {
        //        var _query =
        //                    (
        //                        from _custBillAccount in this.db.Master_CustBillAccounts
        //                        where _custBillAccount.CustCode.Trim().ToLower() == _prmCustCode.Trim().ToLower() && _custBillAccount.CurrCode.Trim().ToLower() == _prmCurrCode.Trim().ToLower()
        //                        && _custBillAccount.fgActive == true
        //                        orderby _custBillAccount.CustBillAccount ascending
        //                        select new
        //                        {
        //                            CustBillCode = _custBillAccount.CustBillCode,
        //                            CustBillAccount = _custBillAccount.CustBillAccount
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new Master_CustBillAccount(_row.CustBillCode, _row.CustBillAccount));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public List<Master_CustBillAccount> GetListDDLCustBillAccountPostpone(string _prmCustCode, string _prmCurrCode)
        //{
        //    List<Master_CustBillAccount> _result = new List<Master_CustBillAccount>();

        //    try
        //    {
        //        var _query =
        //                    (
        //                        from _custBillAccount in this.db.Master_CustBillAccounts
        //                        join _masterProductExtension in this.db.Master_ProductExtensions
        //                            on _custBillAccount.ProductCode equals _masterProductExtension.ProductCode
        //                        where _custBillAccount.CustCode.Trim().ToLower() == _prmCustCode.Trim().ToLower() && _custBillAccount.CurrCode.Trim().ToLower() == _prmCurrCode.Trim().ToLower()
        //                            && _masterProductExtension.IsPostponeAllowed == true
        //                        orderby _custBillAccount.CustBillAccount ascending
        //                        select new
        //                        {
        //                            CustBillCode = _custBillAccount.CustBillCode,
        //                            CustBillAccount = _custBillAccount.CustBillAccount
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new Master_CustBillAccount(_row.CustBillCode, _row.CustBillAccount));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public string GetCustBillAccount(Guid _prmCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        var _query = (
        //                        from _custBillAccount in this.db.Master_CustBillAccounts
        //                        where _custBillAccount.CustBillCode == _prmCode
        //                        select new
        //                        {
        //                            CustBillAccount = _custBillAccount.CustBillAccount
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result = _row.CustBillAccount;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //#endregion

        ~ContractBL()
        {
        }
    }
}
