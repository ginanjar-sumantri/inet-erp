using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Diagnostics;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed class SalesConfirmationBL : Base
    {
        public SalesConfirmationBL()
        {
        }

        #region BILTrSalesConfirmation

        public int RowsCountSalesConfirmation(string _prmCategory, string _prmKeyword)
        {
            int _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            //string _pattern3 = "%%";

            if (_prmCategory == "TransNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                //_pattern3 = "%%";
            }
            if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                //_pattern3 = "%%";
            }
            //else if (_prmCategory == "CustName")
            //{
            //    _pattern3 = "%" + _prmKeyword + "%";
            //    _pattern1 = "%%";
            //    _pattern2 = "%%";
            //}

            try
            {
                _result =
                           (
                               from _salesConfirmation in this.db.BILTrSalesConfirmations
                               //join _msCustomer in this.db.MsCustomers
                               //     on _salesConfirmation.CustCode equals _msCustomer.CustCode
                               where
                                    (SqlMethods.Like(_salesConfirmation.TransNmbr.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_salesConfirmation.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                               //&& (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                               select _salesConfirmation.TransNmbr
                           ).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<BILTrSalesConfirmation> GetListSalesConfirmation(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<BILTrSalesConfirmation> _result = new List<BILTrSalesConfirmation>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            //string _pattern3 = "%%";

            if (_prmCategory == "TransNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                //_pattern3 = "%%";
            }
            if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                //_pattern3 = "%%";
            }
            //else if (_prmCategory == "CustName")
            //{
            //    _pattern3 = "%" + _prmKeyword + "%";
            //    _pattern1 = "%%";
            //    _pattern2 = "%%";
            //}

            try
            {
                var _query = (
                                from _salesConfirmation in this.db.BILTrSalesConfirmations
                                //join _msCustomer in this.db.MsCustomers
                                //     on _salesConfirmation.CustCode equals _msCustomer.CustCode
                                where
                                    (SqlMethods.Like(_salesConfirmation.TransNmbr.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_salesConfirmation.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                //&& (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                orderby _salesConfirmation.TransDate descending
                                select new
                                {
                                    TransNmbr = _salesConfirmation.TransNmbr,
                                    FileNmbr = _salesConfirmation.FileNmbr,
                                    TransDate = _salesConfirmation.TransDate,
                                    Status = _salesConfirmation.Status,
                                    FormulirID = _salesConfirmation.FormulirID,
                                    CustCode = _salesConfirmation.CustCode,
                                    CustName = (
                                                    from _msCustomer in this.db.MsCustomers
                                                    where _salesConfirmation.CustCode == _msCustomer.CustCode
                                                    select _msCustomer.CustName
                                                    ).FirstOrDefault(),
                                    CompanyName = _salesConfirmation.CompanyName,
                                    CustBillAccount = _salesConfirmation.CustBillAccount
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new BILTrSalesConfirmation(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.FormulirID, _row.CustCode, _row.CustName, _row.CompanyName, _row.CustBillAccount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public BILTrSalesConfirmation GetSingleSalesConfirmation(String _prmTransNmbr)
        {
            BILTrSalesConfirmation _result = null;

            try
            {
                _result = this.db.BILTrSalesConfirmations.Single(_temp => _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String AddSalesConfirmation(BILTrSalesConfirmation _prmSalesConfirmation)
        {
            String _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSalesConfirmation.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                this.db.BILTrSalesConfirmations.InsertOnSubmit(_prmSalesConfirmation);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSalesConfirmation.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSalesConfirmation(BILTrSalesConfirmation _prmSalesConfirmation)
        {
            bool _result = false;

            try
            {
                if (_prmSalesConfirmation.CustCode.Trim() == "")
                {
                    foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(DateTime.Now.Year, DateTime.Now.Month, AppModule.GetValue(TransactionType.Customer), this._companyTag, ""))
                    {
                        if (_prmSalesConfirmation.CustCode == "")
                        {
                            _prmSalesConfirmation.CustCode = _item.Number;
                        }
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

        public bool DeleteMultiSalesConfirmation(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    BILTrSalesConfirmation _prmSalesConfirmation = this.db.BILTrSalesConfirmations.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if (_prmSalesConfirmation != null)
                    {
                        if ((_prmSalesConfirmation.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.BILTrSalesConfirmationDts
                                          where _detail.TransNmbr == _prmCode[i]
                                          select _detail);

                            this.db.BILTrSalesConfirmationDts.DeleteAllOnSubmit(_query);

                            this.db.BILTrSalesConfirmations.DeleteOnSubmit(_prmSalesConfirmation);

                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            break;
                        }
                    }
                }

                if (_result == true)
                    this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetApproval(String _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spBIL_SalesConfirmationGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

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

        public string Approve(String _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.spBIL_SalesConfirmationApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        BILTrSalesConfirmation _salesConfirmation = this.GetSingleSalesConfirmation(_prmCode);

                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_salesConfirmation.TransDate.Year, _salesConfirmation.TransDate.Month, AppModule.GetValue(TransactionType.SalesConfirmation), this._companyTag, ""))
                        {
                            _salesConfirmation.FileNmbr = item.Number;
                        }
                        _salesConfirmation.ApprovedDate = DateTime.Now;

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

        public string Posting(String _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();
                BILTrSalesConfirmation _salesConfirmation = this.db.BILTrSalesConfirmations.Single(_temp => _temp.TransNmbr == _prmCode);

                //using (TransactionScope _scope = new TransactionScope())
                //{
                String _locked = _transCloseBL.IsExistAndLocked(_salesConfirmation.TransDate);

                if (_locked == "")
                {
                    this.db.spBIL_SalesConfirmationPost(_prmCode, _salesConfirmation.TransDate.Year, _salesConfirmation.TransDate.Month, _prmuser, this._companyTag, ref _result);

                    if (_result == "")
                    {
                        //int _month = _salesConfirmation.TransDate.AddDays(Convert.ToInt32(_salesConfirmation.TargetInstallationDay)).Month + 1;
                        //int _year = _salesConfirmation.TransDate.AddDays(Convert.ToInt32(_salesConfirmation.TargetInstallationDay)).Year;

                        //String _paymentStatus = new RegistrationConfigBL().GetSingleRegistrationConfig(_salesConfirmation.RegCode).PaymentStatus;

                        //if (_salesConfirmation.FgGenerateBillAccount == true)
                        //{
                        //    if (_paymentStatus == PaymentStatusDataMapper.GetPaymentStatusText(PaymentStatus.Before))
                        //    {
                        //        this.db.S_BillingGenerateInvoice(_month, _year, "", "", _salesConfirmation.CustCode, "", ' ', "", AppModule.GetValue(TransactionType.SalesConfirmation), this._companyTag, this._currentUser, ref _result);

                        //        if (_result == "")
                        //        {
                        //            _scope.Complete();

                        _result = "Posting Success";
                        //    }
                        //    }
                        //}
                    }
                }
                else
                {
                    _result = _locked;
                }
                //}
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string UnPosting(String _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                BILTrSalesConfirmation _salesConfirmation = this.db.BILTrSalesConfirmations.Single(_temp => _temp.TransNmbr == _prmCode);
                String _locked = _transCloseBL.IsExistAndLocked(_salesConfirmation.TransDate);
                if (_locked == "")
                {
                    //this.db.spBIL_SalesConfirmationUnPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public List<BILTrSalesConfirmation> GetListForDDLSalesConfirmation()
        {
            List<BILTrSalesConfirmation> _result = new List<BILTrSalesConfirmation>();

            try
            {
                var _query = (
                                from _bilTrSalesConfirmation in this.db.BILTrSalesConfirmations
                                where _bilTrSalesConfirmation.Status == SalesConfirmationDataMapper.GetStatus(TransStatus.Posted)
                                orderby _bilTrSalesConfirmation.TransNmbr
                                select new
                                {
                                    TransNmbr = _bilTrSalesConfirmation.TransNmbr,
                                    FileNmbr = _bilTrSalesConfirmation.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new BILTrSalesConfirmation(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<BILTrSalesConfirmation> GetListForDDLSalesConfirmationByCustAndProduct(String _prmCustCode, String _prmProductCode)
        {
            List<BILTrSalesConfirmation> _result = new List<BILTrSalesConfirmation>();

            try
            {
                var _query = (
                                from _bilTrSalesConfirmation in this.db.BILTrSalesConfirmations
                                join _biltrSalesConfirmationDt in this.db.BILTrSalesConfirmationDts
                                on _bilTrSalesConfirmation.TransNmbr equals _biltrSalesConfirmationDt.TransNmbr
                                join _bilTrContract in this.db.BILTrContracts
                                on _bilTrSalesConfirmation.TransNmbr equals _bilTrContract.SalesConfirmationNoRef
                                join _bilTrBeritaAcara in this.db.BILTrBeritaAcaras
                                on _bilTrSalesConfirmation.TransNmbr equals _bilTrBeritaAcara.SalesConfirmationNoRef
                                where _bilTrSalesConfirmation.CustCode == _prmCustCode
                                    && _biltrSalesConfirmationDt.ProductCode == _prmProductCode
                                    && _bilTrSalesConfirmation.Status == SalesConfirmationDataMapper.GetStatusByte(TransStatus.Posted)
                                    && _bilTrContract.Status == ContractDataMapper.GetStatusByte(TransStatus.Approved)
                                    && _bilTrBeritaAcara.Status == BeritaAcaraDataMapper.GetStatusByte(TransStatus.Posted)
                                orderby _bilTrSalesConfirmation.TransNmbr
                                select new
                                {
                                    TransNmbr = _bilTrSalesConfirmation.TransNmbr,
                                    FileNmbr = _bilTrSalesConfirmation.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new BILTrSalesConfirmation(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean GetFgSoftBlockExec(String _prmCode)
        {
            Boolean _result = false;

            try
            {
                _result = (
                              from _sc in this.db.BILTrSalesConfirmations
                              where _sc.TransNmbr == _prmCode
                              select _sc.FgSoftBlockExec
                          ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean UpdateFgNotifiedBA(String _prmCode, Boolean _prmUpdateValue)
        {
            Boolean _result = false;

            try
            {
                BILTrSalesConfirmation _sc = this.db.BILTrSalesConfirmations.Single(_temp => _temp.TransNmbr == _prmCode);
                _sc.FgNotifiedBA = _prmUpdateValue;

                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        public Boolean UpdateFgNotifiedCL(String _prmCode, Boolean _prmUpdateValue)
        {
            Boolean _result = false;

            try
            {
                BILTrSalesConfirmation _sc = this.db.BILTrSalesConfirmations.Single(_temp => _temp.TransNmbr == _prmCode);
                _sc.FgNotifiedCL = !_sc.FgNotifiedCL;

                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        public Boolean UpdateFgPending(String _prmCode, Boolean _prmUpdateValue, String _prmPendingReason)
        {
            Boolean _result = false;

            try
            {
                BILTrSalesConfirmation _sc = this.db.BILTrSalesConfirmations.Single(_temp => _temp.TransNmbr == _prmCode);
                _sc.FgPending = _prmUpdateValue;
                _sc.PendingReason = _prmPendingReason;

                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        public Boolean UpdateFgNotifiedSoftBlock(String _prmCode, Boolean _prmUpdateValue)
        {
            Boolean _result = false;

            try
            {
                BILTrSalesConfirmation _sc = this.db.BILTrSalesConfirmations.Single(_temp => _temp.TransNmbr == _prmCode);
                _sc.FgNotifiedSoftBlock = _prmUpdateValue;

                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        public Boolean UpdateFgSoftBlockExec(String _prmCode, Boolean _prmUpdateValue)
        {
            Boolean _result = false;

            try
            {
                BILTrSalesConfirmation _sc = this.db.BILTrSalesConfirmations.Single(_temp => _temp.TransNmbr == _prmCode);
                _sc.FgSoftBlockExec = _prmUpdateValue;

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

        #region BILTrSalesConfirmationDt

        public int RowsCountSalesConfirmationDt(String _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.BILTrSalesConfirmationDts.Where(_row => _row.TransNmbr == _prmCode).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<BILTrSalesConfirmationDt> GetListSalesConfirmationDt(int _prmReqPage, int _prmPageSize, String _prmCode)
        {
            List<BILTrSalesConfirmationDt> _result = new List<BILTrSalesConfirmationDt>();

            try
            {
                var _query = (
                                from _salesConfirmationDt in this.db.BILTrSalesConfirmationDts
                                where _salesConfirmationDt.TransNmbr == _prmCode
                                orderby _salesConfirmationDt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _salesConfirmationDt.TransNmbr,
                                    ProductCode = _salesConfirmationDt.ProductCode,
                                    ProductName = (
                                                        from _msProduct in this.db.MsProducts
                                                        where _salesConfirmationDt.ProductCode == _msProduct.ProductCode
                                                        select _msProduct.ProductName
                                                   ).FirstOrDefault(),
                                    ProductSpecification = _salesConfirmationDt.ProductSpecification,
                                    CurrCode = _salesConfirmationDt.CurrCode,
                                    AmountForex = _salesConfirmationDt.AmountForex
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new BILTrSalesConfirmationDt(_row.TransNmbr, _row.ProductCode, _row.ProductName, _row.ProductSpecification, _row.CurrCode, _row.AmountForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Decimal GetTotalAmountSalesConfirmationDt(String _prmCode)
        {
            Decimal _result = 0;

            try
            {
                var _query = (
                                from _salesConfirmationDt in this.db.BILTrSalesConfirmationDts
                                where _salesConfirmationDt.TransNmbr == _prmCode
                                orderby _salesConfirmationDt.ProductCode ascending
                                select _salesConfirmationDt.AmountForex
                            ).Sum();

                _result = Convert.ToDecimal(_query);

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public BILTrSalesConfirmationDt GetSingleSalesConfirmationDt(String _prmTransNmbr, String _prmProductCode)
        {
            BILTrSalesConfirmationDt _result = null;

            try
            {
                _result = this.db.BILTrSalesConfirmationDts.Single(_temp => _temp.TransNmbr == _prmTransNmbr && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSalesConfirmationDt(string[] _prmProductCode, String _prmTransNmbr)
        {
            bool _result = false;
            decimal _amount = 0;

            try
            {
                for (int i = 0; i < _prmProductCode.Length; i++)
                {
                    BILTrSalesConfirmationDt _salesConfirmationDt = this.db.BILTrSalesConfirmationDts.Single(_temp => _temp.ProductCode == _prmProductCode[i] && _temp.TransNmbr == _prmTransNmbr);

                    _amount = _amount + Convert.ToDecimal(_salesConfirmationDt.AmountForex);

                    this.db.BILTrSalesConfirmationDts.DeleteOnSubmit(_salesConfirmationDt);
                }

                BILTrSalesConfirmation _bilTrSalesConfirmation = this.GetSingleSalesConfirmation(_prmTransNmbr);

                _bilTrSalesConfirmation.PPNForex = (this.GetTotalAmountSalesConfirmationDt(_prmTransNmbr) - _amount) * (_bilTrSalesConfirmation.PPNPercentage / 100);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddSalesConfirmationDt(BILTrSalesConfirmationDt _prmSalesConfirmationDt)
        {
            bool _result = false;

            //decimal _total = 0;

            try
            {
                BILTrSalesConfirmation _bilTrSalesConfirmation = this.GetSingleSalesConfirmation(_prmSalesConfirmationDt.TransNmbr);

                _bilTrSalesConfirmation.PPNForex = (this.GetTotalAmountSalesConfirmationDt(_prmSalesConfirmationDt.TransNmbr) + _prmSalesConfirmationDt.AmountForex) * (_bilTrSalesConfirmation.PPNPercentage / 100);

                this.db.BILTrSalesConfirmationDts.InsertOnSubmit(_prmSalesConfirmationDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSalesConfirmationDt(BILTrSalesConfirmationDt _prmSalesConfirmationDt, Decimal _prmAmountForex)
        {
            bool _result = false;

            try
            {
                BILTrSalesConfirmation _bilTrSalesConfirmation = this.GetSingleSalesConfirmation(_prmSalesConfirmationDt.TransNmbr);

                _bilTrSalesConfirmation.PPNForex = (this.GetTotalAmountSalesConfirmationDt(_prmSalesConfirmationDt.TransNmbr) - _prmAmountForex + _prmSalesConfirmationDt.AmountForex) * (_bilTrSalesConfirmation.PPNPercentage / 100);

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

        ~SalesConfirmationBL()
        {
        }
    }
}
