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
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing
{
    public sealed class PurchaseOrderBL : Base
    {
        public PurchaseOrderBL()
        {

        }

        private UnitBL _unitBL = new UnitBL();
        private StockIssueRequestFABL _stcIssueRequestFABL = new StockIssueRequestFABL();
        private GLBudgetBL _glBudgetBL = new GLBudgetBL();

        #region PRCPOHd
        public double RowsCountPRCPOHd(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }

            var _query =
                        (
                            from _prcPOHds in this.db.PRCPOHds
                            join _msSupplier in this.db.MsSuppliers
                                on _prcPOHds.SuppCode equals _msSupplier.SuppCode
                            where _prcPOHds.Revisi == this.db.PRCPOHds.Where(_temp => _prcPOHds.TransNmbr.Trim().ToLower() == _temp.TransNmbr.Trim().ToLower()).Max(_max => _max.Revisi)
                               && (SqlMethods.Like(_prcPOHds.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_prcPOHds.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && _prcPOHds.Status != PurchaseOrderDataMapper.GetStatus(TransStatus.Deleted)
                            select _prcPOHds.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<PRCPOHd> GetListPRCPOHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<PRCPOHd> _result = new List<PRCPOHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }

            try
            {
                var _query = (
                                from _prcPOHd in this.db.PRCPOHds
                                join _msSupplier in this.db.MsSuppliers
                                    on _prcPOHd.SuppCode equals _msSupplier.SuppCode
                                where _prcPOHd.Revisi == this.db.PRCPOHds.Where(_temp => _prcPOHd.TransNmbr.Trim().ToLower() == _temp.TransNmbr.Trim().ToLower()).Max(_max => _max.Revisi)
                                  && (SqlMethods.Like(_prcPOHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && (SqlMethods.Like((_prcPOHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                  && _prcPOHd.Status != PurchaseOrderDataMapper.GetStatus(TransStatus.Deleted)
                                orderby _prcPOHd.TransDate descending
                                select new
                                {
                                    TransNmbr = _prcPOHd.TransNmbr,
                                    FileNmbr = _prcPOHd.FileNmbr,
                                    Revisi = _prcPOHd.Revisi,
                                    TransDate = _prcPOHd.TransDate,
                                    Status = _prcPOHd.Status,
                                    CurrCode = _prcPOHd.CurrCode,
                                    SuppName = _msSupplier.SuppName,
                                    TotalForex = _prcPOHd.TotalForex
                                }
                            );

                if (_prmOrderBy == "Trans No.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.TransNmbr)) : (_query.OrderByDescending(a => a.TransNmbr));

                if (_prmOrderBy == "File No.")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.FileNmbr)) : (_query.OrderByDescending(a => a.FileNmbr));

                if (_prmOrderBy == "Trans Date")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.TransDate)) : (_query.OrderByDescending(a => a.TransDate));

                if (_prmOrderBy == "Status")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.Status)) : (_query.OrderByDescending(a => a.Status));

                if (_prmOrderBy == "Currency")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.CurrCode)) : (_query.OrderByDescending(a => a.CurrCode));

                if (_prmOrderBy == "Supplier")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.SuppName)) : (_query.OrderByDescending(a => a.SuppName));

                if (_prmOrderBy == "Total Forex")
                    _query = _prmAscDesc ? (_query.OrderBy(a => a.TotalForex)) : (_query.OrderByDescending(a => a.TotalForex));

                _query = _query.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new PRCPOHd(_row.TransNmbr, _row.FileNmbr, _row.Revisi, _row.TransDate, _row.Status, _row.CurrCode, _row.SuppName, _row.TotalForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public PRCPOHd GetSinglePRCPOHd(string _prmCode, int _prmRevisi)
        {
            PRCPOHd _result = null;

            try
            {
                _result = this.db.PRCPOHds.Single(_temp => (_temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()) && (_temp.Revisi == _prmRevisi));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public PRCPOHd GetSinglePRCPOHd(string _prmCode)
        {
            PRCPOHd _result = null;

            try
            {
                _result = this.db.PRCPOHds.Single(_temp => (_temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSinglePRCPOHdApprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    PRCPOHd _prcRequestHd = this.db.PRCPOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower());

                    if (_prcRequestHd != null)
                    {
                        if (_prcRequestHd.Status != PurchaseOrderDataMapper.GetStatus(TransStatus.Posted))
                        {
                            _result = true;
                        }
                        else
                        {
                            _result = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetFileNmbrPRCPOHd(string _prmCode, int _prmRevisi)
        {
            string _result = null;

            try
            {
                _result = (this.db.PRCPOHds.Single(_temp => (_temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()) && (_temp.Revisi == _prmRevisi)).FileNmbr ?? "").Trim();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string AddPRCPOHd(PRCPOHd _prmPRCPOHd)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmPRCPOHd.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.PRCPOHds.InsertOnSubmit(_prmPRCPOHd);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmPRCPOHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPRCPOHd(PRCPOHd _prmPRCPOHd)
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

        public bool DeleteMultiPRCPOHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    PRCPOHd _prcPOHd = this.db.PRCPOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.Revisi.ToString().Trim().ToLower() == _tempSplit[1].Trim().ToLower());

                    if (_prcPOHd != null)
                    {
                        if ((_prcPOHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.PRCPODt2s
                                          where _detail.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower()
                                          select _detail);

                            this.db.PRCPODt2s.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.PRCPODts
                                           where _detail.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower()
                                           select _detail);

                            this.db.PRCPODts.DeleteAllOnSubmit(_query2);

                            this.db.PRCPOHds.DeleteOnSubmit(_prcPOHd);

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

        public bool DeleteMultiApprovePRCPOHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    PRCPOHd _prcPOHd = this.db.PRCPOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.Revisi.ToString().Trim().ToLower() == _tempSplit[1].Trim().ToLower());

                    if (_prcPOHd.Status == PurchaseOrderDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _prcPOHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _prcPOHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_prcPOHd != null)
                    {
                        if ((_prcPOHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.PRCPODt2s
                                          where _detail.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower()
                                          select _detail);

                            this.db.PRCPODt2s.DeleteAllOnSubmit(_query);

                            var _query2 = (from _detail in this.db.PRCPODts
                                           where _detail.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower()
                                           select _detail);

                            this.db.PRCPODts.DeleteAllOnSubmit(_query2);

                            this.db.PRCPOHds.DeleteOnSubmit(_prcPOHd);

                            _result = true;
                        }
                        else if (_prcPOHd.FileNmbr != "" && _prcPOHd.Status == PurchaseOrderDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _prcPOHd.Status = PurchaseOrderDataMapper.GetStatus(TransStatus.Deleted);
                            _result = true;
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

        public List<PRCPOHd> GetListDDLRevisi(string _prmTransNmbr)
        {
            List<PRCPOHd> _result = new List<PRCPOHd>();

            try
            {
                var _query = (
                                from _prcPOHd in this.db.PRCPOHds
                                where _prcPOHd.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()
                                orderby _prcPOHd.Revisi ascending
                                select new
                                {
                                    Revisi = _prcPOHd.Revisi
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new PRCPOHd(_row.Revisi));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<PRCPOHd> GetListDDLPOBySupplier(string _prmSuppCode)
        {
            List<PRCPOHd> _result = new List<PRCPOHd>();

            try
            {
                var _query = (
                                from _prcPOHd in this.db.PRCPOHds
                                where _prcPOHd.SuppCode.Trim().ToLower() == _prmSuppCode.Trim().ToLower()
                                orderby _prcPOHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _prcPOHd.TransNmbr,
                                    FileNmbr = _prcPOHd.FileNmbr
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new PRCPOHd(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }        

        public string Revisi(string _prmTransNmbr, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_PRPOCreateRevisi(_prmTransNmbr, _prmuser, ref _result);
            }
            catch (Exception ex)
            {
                _result = "Revisi Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Closing(string _prmTransNmbr, int _prmRevisi, string _prmProduct, string _prmRemark, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_PRPOClosing(_prmTransNmbr, _prmRevisi, _prmProduct, _prmRemark, _prmuser, ref _result);
            }
            catch (Exception ex)
            {
                _result = "Closing Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string GetApproval(string _prmTransNmbr, int _prmRevisi, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_PRPOGetAppr(_prmTransNmbr, _prmRevisi, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.PurchaseOrder);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmuser;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        //public Boolean IsBudgetNotExist(String _prmTransNmbr)
        //{
        //    Boolean _result = false;

        //    List<String> _requestNo = new List<String>();
        //    List<String> _account = new List<String>();
        //    List<Guid> _budgetDetailCode = new List<Guid>();
        //    DateTime _date = this.GetSinglePRCPOHd(_prmTransNmbr, this.GetLastRevisiPRCPOHd(_prmTransNmbr)).TransDate;
        //    List<String> _orgUnit = new List<String>();
        //    List<String> _budgetCode = new List<String>();
        //    List<String> _productCode = new List<String>();

        //    _requestNo.Add((
        //                       from _prcPODt2 in this.db.PRCPODt2s
        //                       where _prcPODt2.TransNmbr == _prmTransNmbr && _prcPODt2.Revisi == this.GetLastRevisiPRCPOHd(_prmTransNmbr)
        //                       select _prcPODt2.RequestNo
        //                   ).FirstOrDefault());

        //    foreach (String _row in _requestNo)
        //    {
        //        _orgUnit.Add((
        //                            from _prcRequestHd in this.db.PRCRequestHds
        //                            where _prcRequestHd.TransNmbr == _row
        //                            select _prcRequestHd.OrgUnit
        //                        ).FirstOrDefault());
        //    }

        //    foreach (String _row2 in _orgUnit)
        //    {
        //        _budgetCode.Add(_glBudgetBL.GetBudgetCodeByOrgUnitAndDate(_row2, _date));
        //    }

        //    if (_budgetCode.Count == 0)
        //    {
        //        _result = true;
        //    }
        //    else
        //    {
        //        _productCode.Add((
        //                            from _prcPODt in this.db.PRCPODts
        //                            where _prcPODt.TransNmbr == _prmTransNmbr && _prcPODt.Revisi == this.GetLastRevisiPRCPOHd(_prmTransNmbr)
        //                            select _prcPODt.ProductCode
        //                        ).FirstOrDefault());


        //        foreach (String _row3 in _productCode)
        //        {
        //            _account.Add((
        //                            from _msProduct in this.db.MsProducts
        //                            join _msProductType in this.db.MsProductTypes
        //                                on _msProduct.ProductType equals _msProductType.ProductTypeCode
        //                            join _msProductTypeDt in this.db.MsProductTypeDts
        //                                on _msProductType.ProductTypeCode equals _msProductTypeDt.ProductTypeCode
        //                            where _msProduct.ProductCode == _row3
        //                            select _msProductTypeDt.AccInvent
        //                        ).FirstOrDefault());
        //        }

        //        foreach (String _row5 in _budgetCode)
        //        {
        //            foreach (String _row4 in _account)
        //            {
        //                if (_glBudgetBL.GetBudgetDetailCodeByAccountAndBudgetCode(new Guid(_row5), _row4) != Guid.NewGuid())
        //                {
        //                    _budgetDetailCode.Add(_glBudgetBL.GetBudgetDetailCodeByAccountAndBudgetCode(new Guid(_row5), _row4));
        //                }
        //            }
        //        }

        //        if (_budgetDetailCode.Count == 0)
        //        {
        //            _result = true;
        //        }
        //    }

        //    return _result;
        //}

        //public Boolean CheckAmountBudget(String _prmTransNmbr)
        //{
        //    Boolean _result = false;

        //    List<String> _requestNo = new List<String>();
        //    List<String> _account = new List<String>();
        //    List<Guid> _budgetDetailCode = new List<Guid>();
        //    DateTime _date = this.GetSinglePRCPOHd(_prmTransNmbr, this.GetLastRevisiPRCPOHd(_prmTransNmbr)).TransDate;
        //    List<String> _orgUnit = new List<String>();
        //    List<String> _budgetCode = new List<String>();
        //    List<String> _productCode = new List<String>();
        //    List<Decimal> _amount = new List<Decimal>();
        //    decimal _rate = this.GetSinglePRCPOHd(_prmTransNmbr, this.GetLastRevisiPRCPOHd(_prmTransNmbr)).ForexRate;

        //    _requestNo.Add((
        //                       from _prcPODt2 in this.db.PRCPODt2s
        //                       where _prcPODt2.TransNmbr == _prmTransNmbr && _prcPODt2.Revisi == this.GetLastRevisiPRCPOHd(_prmTransNmbr)
        //                       select _prcPODt2.RequestNo
        //                   ).FirstOrDefault());

        //    foreach (String _row in _requestNo)
        //    {
        //        _orgUnit.Add((
        //                            from _prcRequestHd in this.db.PRCRequestHds
        //                            where _prcRequestHd.TransNmbr == _row
        //                            select _prcRequestHd.OrgUnit
        //                        ).FirstOrDefault());
        //    }

        //    foreach (String _row2 in _orgUnit)
        //    {
        //        _budgetCode.Add(_glBudgetBL.GetBudgetCodeByOrgUnitAndDate(_row2, _date));
        //    }

        //    _productCode.Add((
        //                            from _prcPODt in this.db.PRCPODts
        //                            where _prcPODt.TransNmbr == _prmTransNmbr && _prcPODt.Revisi == this.GetLastRevisiPRCPOHd(_prmTransNmbr)
        //                            select _prcPODt.ProductCode
        //                        ).FirstOrDefault());

        //    foreach (String _row3 in _productCode)
        //    {
        //        _account.Add((
        //                        from _msProduct in this.db.MsProducts
        //                        join _msProductType in this.db.MsProductTypes
        //                            on _msProduct.ProductType equals _msProductType.ProductTypeCode
        //                        join _msProductTypeDt in this.db.MsProductTypeDts
        //                            on _msProductType.ProductTypeCode equals _msProductTypeDt.ProductTypeCode
        //                        where _msProduct.ProductCode == _row3
        //                        select _msProductTypeDt.AccInvent
        //                    ).FirstOrDefault());

        //        _amount.Add((
        //                        from _prcPODt in this.db.PRCPODts
        //                        where _prcPODt.TransNmbr == _prmTransNmbr && _prcPODt.ProductCode == _row3 && _prcPODt.Revisi == this.GetLastRevisiPRCPOHd(_prmTransNmbr)
        //                        select _prcPODt.NettoForex
        //                    ).FirstOrDefault());
        //    }

        //    foreach (String _row5 in _budgetCode)
        //    {
        //        foreach (String _row4 in _account)
        //        {
        //            _budgetDetailCode.Add(_glBudgetBL.GetBudgetDetailCodeByAccountAndBudgetCode(new Guid(_row5), _row4));
        //        }
        //    }

        //    decimal _amountBudget = 0;
        //    decimal _amountActual = 0;

        //    foreach (String _row8 in _budgetCode)
        //    {
        //        foreach (Guid _row6 in _budgetDetailCode)
        //        {
        //            _amountBudget = _glBudgetBL.GetSingleGLBudgetAcc(new Guid(_row8), _row6).AmountBudgetHome;
        //            _amountActual = _glBudgetBL.GetSingleGLBudgetAcc(new Guid(_row8), _row6).AmountActual;

        //            foreach (Decimal _row7 in _amount)
        //            {
        //                if (_amountBudget - _amountActual - (_row7 * _rate) >= 0)
        //                {
        //                    _result = true;
        //                }
        //                else
        //                {
        //                    _result = false;
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    return _result;
        //}

        public string Approve(string _prmTransNmbr, int _prmRevisi, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_PRPOApprove(_prmTransNmbr, _prmRevisi, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        PRCPOHd _prcPOHd = this.GetSinglePRCPOHd(_prmTransNmbr, _prmRevisi);

                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_prcPOHd.TransDate.Year, _prcPOHd.TransDate.Month, AppModule.GetValue(TransactionType.PurchaseOrder), this._companyTag, ""))
                        {
                            _prcPOHd.FileNmbr = _item.Number;
                        }

                        _result = "Approve Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.PurchaseOrder);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSinglePRCPOHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();
                        _scope.Complete();
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

        public string Posting(string _prmTransNmbr, int _prmRevisi, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                PRCPOHd _prcPOHd = this.db.PRCPOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _temp.Revisi == _prmRevisi);
                String _locked = _transCloseBL.IsExistAndLocked(_prcPOHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_PRPOPost(_prmTransNmbr, _prmRevisi, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.PurchaseRetur);
                        _transActivity.TransNmbr = _prmTransNmbr.ToString();
                        _transActivity.FileNmbr = this.GetSinglePRCPOHd(_prmTransNmbr).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Unposting(string _prmTransNmbr, int _prmRevisi, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                PRCPOHd _prcPOHd = this.db.PRCPOHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _temp.Revisi == _prmRevisi);
                String _locked = _transCloseBL.IsExistAndLocked(_prcPOHd.TransDate);
                if (_locked == "")
                {
                    this.db.S_PRPOUnPost(_prmTransNmbr, _prmRevisi, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Unposting Success";
                    }
                }
                else
                {
                    _result = _locked;
                }
            }
            catch (Exception ex)
            {
                _result = "Unposting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public int GetNewRevisiByCode(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                from _prcPOHd in this.db.PRCPOHds
                                where _prcPOHd.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    Revisi = _prcPOHd.Revisi
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.Revisi;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetLastRevisiPRCPOHd(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.PRCPOHds.Where(_temp => _temp.TransNmbr.ToLower() == _prmCode.ToLower() && _temp.Status == PurchaseOrderDataMapper.GetStatus(TransStatus.Posted)).Max(_temp => _temp.Revisi);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region PRCPODt

        public List<PRCPODt> GetListPRCPODt(string _prmCode, int _prmRevisi)
        {
            List<PRCPODt> _result = new List<PRCPODt>();

            try
            {
                var _query = (
                                from _prcPODt in this.db.PRCPODts
                                where _prcPODt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _prcPODt.Revisi == _prmRevisi
                                orderby _prcPODt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _prcPODt.TransNmbr,
                                    Revisi = _prcPODt.Revisi,
                                    ProductCode = _prcPODt.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode.Trim().ToLower() == _prcPODt.ProductCode.Trim().ToLower()
                                                    select _msProduct.ProductName
                                                   ).FirstOrDefault(),
                                    Qty = _prcPODt.Qty,
                                    Unit = _prcPODt.Unit,
                                    UnitName = (
                                            from _msUnit in this.db.MsUnits
                                            where _msUnit.UnitCode.Trim().ToLower() == _prcPODt.Unit.Trim().ToLower()
                                            select _msUnit.UnitName
                                           ).FirstOrDefault(),
                                    PriceForex = _prcPODt.PriceForex,
                                    AmountForex = _prcPODt.AmountForex,
                                    DiscForex = _prcPODt.DiscForex,
                                    NettoForex = _prcPODt.NettoForex,
                                    DoneClosing = _prcPODt.DoneClosing,
                                    QtyRR = _prcPODt.QtyRR,
                                    QtyClose = _prcPODt.QtyClose,
                                    CreatedBy = _prcPODt.CreatedBy
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new PRCPODt(_row.TransNmbr, _row.Revisi, _row.ProductCode, _row.ProductName, _row.Qty, _row.Unit, _row.UnitName, _row.PriceForex, _row.AmountForex, _row.DiscForex, _row.NettoForex, _row.DoneClosing, _row.QtyRR, _row.QtyClose, _row.CreatedBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<PRCPODt> GetListPRCPODtForStcRecvPO(string _prmCode, int _prmRevisi)
        {
            List<PRCPODt> _result = new List<PRCPODt>();

            try
            {
                var _query = (
                                from _prcPODt in this.db.PRCPODts
                                where _prcPODt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _prcPODt.Revisi == _prmRevisi
                                && _prcPODt.Qty - _prcPODt.QtyRR - _prcPODt.QtyClose != 0
                                orderby _prcPODt.ProductCode ascending
                                select new
                                {
                                    TransNmbr = _prcPODt.TransNmbr,
                                    Revisi = _prcPODt.Revisi,
                                    ProductCode = _prcPODt.ProductCode,
                                    ProductName = (
                                                    from _msProduct in this.db.MsProducts
                                                    where _msProduct.ProductCode.Trim().ToLower() == _prcPODt.ProductCode.Trim().ToLower()
                                                    select _msProduct.ProductName
                                                   ).FirstOrDefault(),
                                    Qty = _prcPODt.Qty,
                                    Unit = _prcPODt.Unit,
                                    UnitName = (
                                            from _msUnit in this.db.MsUnits
                                            where _msUnit.UnitCode.Trim().ToLower() == _prcPODt.Unit.Trim().ToLower()
                                            select _msUnit.UnitName
                                           ).FirstOrDefault(),
                                    PriceForex = _prcPODt.PriceForex,
                                    AmountForex = _prcPODt.AmountForex,
                                    DiscForex = _prcPODt.DiscForex,
                                    NettoForex = _prcPODt.NettoForex,
                                    DoneClosing = _prcPODt.DoneClosing,
                                    QtyClose = _prcPODt.QtyClose,
                                    CreatedBy = _prcPODt.CreatedBy
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new PRCPODt(_row.TransNmbr, _row.Revisi, _row.ProductCode, _row.ProductName, _row.Qty, _row.Unit, _row.UnitName, _row.PriceForex, _row.AmountForex, _row.DiscForex, _row.NettoForex, _row.DoneClosing, _row.QtyClose, _row.CreatedBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public PRCPODt GetSinglePRCPODt(string _prmCode, int _prmRevisi, string _prmProductCode)
        {
            PRCPODt _result = null;

            try
            {
                _result = this.db.PRCPODts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.Revisi == _prmRevisi && _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public PRCPODt GetSinglePRCPODtForStcReceivingPO(string _prmCode, int _prmRevisi, string _prmProductCode)
        {
            PRCPODt _result = null;

            try
            {
                _result = this.db.PRCPODts.FirstOrDefault(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.Revisi == _prmRevisi && _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower() && _temp.Qty - _temp.QtyRR - _temp.QtyClose > 0);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiPRCPODt(string[] _prmCode)
        {
            bool _result = false;

            decimal _tempBaseForex = 0;
            decimal _tempDiscForex = 0;
            decimal _tempDPForex = 0;
            decimal _tempPPHForex = 0;
            decimal _tempPPNForex = 0;
            decimal _tempTotalForex = 0;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    PRCPODt _prcPODt = this.db.PRCPODts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.Revisi.ToString().Trim().ToLower() == _tempSplit[1].Trim().ToLower() && _temp.ProductCode.ToString().Trim().ToLower() == _tempSplit[2].Trim().ToLower());

                    this.db.PRCPODts.DeleteOnSubmit(_prcPODt);

                    ////////

                    PRCPOHd _prcPOHd = this.GetSinglePRCPOHd(_tempSplit[0], Convert.ToInt32(_tempSplit[1]));

                    decimal _nettoForex = Convert.ToDecimal((_prcPODt.NettoForex == null) ? 0 : _prcPODt.NettoForex);

                    _tempBaseForex = _prcPOHd.BaseForex - _nettoForex;
                    _tempDiscForex = _tempBaseForex * (_prcPOHd.Disc / 100);
                    _tempDPForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.DP / 100);
                    _tempPPHForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.PPH / 100);
                    _tempPPNForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.PPN / 100);
                    _tempTotalForex = _tempBaseForex - _tempDiscForex + _tempPPNForex + _tempPPHForex;

                    _prcPOHd.BaseForex = _tempBaseForex;
                    _prcPOHd.DiscForex = _tempDiscForex;
                    _prcPOHd.DPForex = _tempDPForex;
                    _prcPOHd.PPHForex = _tempPPHForex;
                    _prcPOHd.PPNForex = _tempPPNForex;
                    _prcPOHd.TotalForex = _tempTotalForex;
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

        public bool AddPRCPODt(PRCPODt _prmPRCPODt)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;
            decimal _tempDiscForex = 0;
            decimal _tempDPForex = 0;
            decimal _tempPPHForex = 0;
            decimal _tempPPNForex = 0;
            decimal _tempTotalForex = 0;

            try
            {
                PRCPOHd _prcPOHd = this.GetSinglePRCPOHd(_prmPRCPODt.TransNmbr, _prmPRCPODt.Revisi);

                decimal _nettoForex = Convert.ToDecimal((_prmPRCPODt.NettoForex == null) ? 0 : _prmPRCPODt.NettoForex);

                _tempBaseForex = _prcPOHd.BaseForex + _nettoForex;
                _tempDiscForex = _tempBaseForex * (_prcPOHd.Disc / 100);
                _tempDPForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.DP / 100);
                _tempPPHForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.PPH / 100);
                _tempPPNForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.PPN / 100);
                _tempTotalForex = _tempBaseForex - _tempDiscForex + _tempPPNForex + _tempPPHForex;

                _prcPOHd.BaseForex = _tempBaseForex;
                _prcPOHd.DiscForex = _tempDiscForex;
                _prcPOHd.DPForex = _tempDPForex;
                _prcPOHd.PPHForex = _tempPPHForex;
                _prcPOHd.PPNForex = _tempPPNForex;
                _prcPOHd.TotalForex = _tempTotalForex;

                this.db.PRCPODts.InsertOnSubmit(_prmPRCPODt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPRCPODt(PRCPODt _prmPRCPODt, decimal _prmNettoOriginal)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;
            decimal _tempDiscForex = 0;
            decimal _tempDPForex = 0;
            decimal _tempPPHForex = 0;
            decimal _tempPPNForex = 0;
            decimal _tempTotalForex = 0;


            try
            {
                PRCPOHd _prcPOHd = this.GetSinglePRCPOHd(_prmPRCPODt.TransNmbr, _prmPRCPODt.Revisi);

                decimal _nettoForex = Convert.ToDecimal((_prmPRCPODt.NettoForex == null) ? 0 : _prmPRCPODt.NettoForex);

                _tempBaseForex = _prcPOHd.BaseForex - _prmNettoOriginal;
                _tempBaseForex = _tempBaseForex + _nettoForex;
                _tempDiscForex = _tempBaseForex * (_prcPOHd.Disc / 100);
                _tempDPForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.DP / 100);
                _tempPPHForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.PPH / 100);
                _tempPPNForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.PPN / 100);
                _tempTotalForex = _tempBaseForex - _tempDiscForex + _tempPPNForex + _tempPPHForex;

                _prcPOHd.BaseForex = _tempBaseForex;
                _prcPOHd.DiscForex = _tempDiscForex;
                _prcPOHd.DPForex = _tempDPForex;
                _prcPOHd.PPHForex = _tempPPHForex;
                _prcPOHd.PPNForex = _tempPPNForex;
                _prcPOHd.TotalForex = _tempTotalForex;

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

        #region PRCPODt2

        public List<PRCPODt2> GetListPRCPODt2(string _prmCode, int _prmRevisi)
        {
            List<PRCPODt2> _result = new List<PRCPODt2>();

            try
            {
                var _query = (
                                from _prcPODt2 in this.db.PRCPODt2s
                                where _prcPODt2.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _prcPODt2.Revisi == _prmRevisi
                                orderby _prcPODt2.RequestNoFileNmbr ascending
                                select new
                                {
                                    TransNmbr = _prcPODt2.TransNmbr,
                                    Revisi = _prcPODt2.Revisi,
                                    RequestNo = _prcPODt2.RequestNo,
                                    RequestNoFileNmbr = new PurchaseRequestBL().GetFileNmbrPRCRequestHd(_prcPODt2.RequestNo),
                                    CreatedBy = _prcPODt2.CreatedBy
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new PRCPODt2(_row.TransNmbr, _row.Revisi, _row.RequestNo, _row.RequestNoFileNmbr, _row.CreatedBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public PRCPODt2 GetSinglePRCPODt2(string _prmCode, int _prmRevisi, string _prmRequestNo)
        {
            PRCPODt2 _result = null;

            try
            {
                //_result = this.db.PRCPODt2s.Single(_temp => (_temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()) && (_temp.Revisi == _prmRevisi) && (_temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower()) && (_temp.RequestNo.Trim().ToLower() == _prmRequestNo.Trim().ToLower()));
                _result = this.db.PRCPODt2s.Single(_temp => (_temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()) && (_temp.Revisi == _prmRevisi) && (_temp.RequestNo.Trim().ToLower() == _prmRequestNo.Trim().ToLower()));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddPRCPODt2(PRCPODt2 _prmPRCPODt2)
        {
            bool _result = false;

            try
            {
                this.db.PRCPODt2s.InsertOnSubmit(_prmPRCPODt2);

                var _query = (
                                from _prcPODt in this.db.PRCPODts
                                where _prcPODt.TransNmbr == _prmPRCPODt2.TransNmbr
                                    && _prcPODt.Revisi == _prmPRCPODt2.Revisi
                                select _prcPODt
                             );
                this.db.PRCPODts.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPRCPODt2(string _prmTransNmbr, int _prmRevisi, string _prmProductCode, string _prmReqNo, decimal _prmQty, string _prmUserName, DateTime _prmDate)
        {
            bool _result = false;
            //decimal _tempBaseForex = 0;
            //decimal _tempDiscForex = 0;
            //decimal _tempDPForex = 0;
            //decimal _tempPPHForex = 0;
            //decimal _tempPPNForex = 0;
            //decimal _tempTotalForex = 0;
            //decimal _nettoForex = 0;
            //decimal _tempDisc = 0;

            try
            {
                //PRCPODt _prcPODt = this.GetSinglePRCPODt(_prmTransNmbr, _prmRevisi, _prmProductCode);

                //if (_prcPODt != null)
                //{
                //    PRCPODt2 _prcPODt2 = this.GetSinglePRCPODt2(_prmTransNmbr, _prmRevisi, _prmProductCode, _prmReqNo);

                //    _nettoForex = _prcPODt.NettoForex;
                //    _tempDisc = _prcPODt.Disc;

                //    decimal _allQty = _unitBL.FindConvertionUnit(_prmProductCode, _prcPODt.UnitWrhs, _prcPODt.Unit) * _prcPODt.QtyWrhsPO;
                //    decimal _existQty = _unitBL.FindConvertionUnit(_prmProductCode, _prcPODt.UnitWrhs, _prcPODt.Unit) * _prcPODt2.Qty;
                //    decimal _currentQty = _unitBL.FindConvertionUnit(_prmProductCode, _prcPODt.UnitWrhs, _prcPODt.Unit) * _prmQty;

                //    _prcPODt.Qty = _allQty - _existQty + _currentQty;
                //    _prcPODt.PriceForex = _prcPODt.PriceForex;
                //    _prcPODt.AmountForex = _prcPODt.Qty * _prcPODt.PriceForex;

                //    if (_tempDisc == 0)
                //    {
                //        _tempDiscForex = _prcPODt.DiscForex;
                //    }
                //    else
                //    {
                //        _tempDiscForex = _tempBaseForex * _tempDisc / 100;
                //    }

                //    _prcPODt.NettoForex = _prcPODt.AmountForex - _prcPODt.DiscForex;
                //    _prcPODt.QtyWrhsPO = _prcPODt.QtyWrhsPO - _prcPODt2.Qty + _prmQty;
                //    _prcPODt.QtyWrhsTotal = _prcPODt.QtyWrhsPO + _prcPODt.QtyWrhsFree;

                //    _prcPODt.EditBy = _prmUserName;
                //    _prcPODt.EditDate = _prmDate;

                //    _prcPODt2.Qty = _prmQty;

                //    _prcPODt2.EditBy = _prmUserName;
                //    _prcPODt2.EditDate = _prmDate;
                //}

                //PRCPOHd _prcPOHd = this.GetSinglePRCPOHd(_prmTransNmbr, _prmRevisi);

                //_tempBaseForex = _prcPOHd.BaseForex + _prcPODt.NettoForex - _nettoForex;
                //_tempDiscForex = _tempBaseForex * (_prcPOHd.Disc / 100);
                //_tempDPForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.DP / 100);
                //_tempPPHForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.PPH / 100);
                //_tempPPNForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.PPN / 100);
                //_tempTotalForex = _tempBaseForex - _tempDiscForex + _tempPPNForex + _tempPPHForex;

                //_prcPOHd.BaseForex = _tempBaseForex;
                //_prcPOHd.DiscForex = _tempDiscForex;
                //_prcPOHd.DPForex = _tempDPForex;
                //_prcPOHd.PPHForex = _tempPPHForex;
                //_prcPOHd.PPNForex = _tempPPNForex;
                //_prcPOHd.TotalForex = _tempTotalForex;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GeneratePODt(string _prmTransNmbr, int _prmRevisi)
        {
            bool _result = false;

            try
            {
                var _delPODt = (
                                from _prcPODt in this.db.PRCPODts
                                where _prcPODt.TransNmbr == _prmTransNmbr
                                    && _prcPODt.Revisi == _prmRevisi
                                select _prcPODt
                               );

                this.db.PRCPODts.DeleteAllOnSubmit(_delPODt);
                this.db.SubmitChanges();

                var _query = (
                                from _prcPODt2 in this.db.PRCPODt2s
                                join _vPRforPO in this.db.V_PRRequestForPOs
                                    on _prcPODt2.RequestNo equals _vPRforPO.PR_No
                                where _prcPODt2.TransNmbr == _prmTransNmbr
                                    && _prcPODt2.Revisi == _prmRevisi
                                select new
                                {
                                    RequestNo = _prcPODt2.RequestNo,
                                    ProductCode = _vPRforPO.Product_Code,
                                    Qty = (int)_vPRforPO.Qty,
                                    Unit = _vPRforPO.Unit,
                                    RequestBy = _vPRforPO.RequestBy,
                                    CreatedBy = _prcPODt2.CreatedBy,
                                    CreatedDate = _prcPODt2.CreatedDate
                                }
                             );

                foreach (var _vPRForPO in _query)
                {
                    decimal _tempBaseForex = 0;
                    decimal _tempDiscForex = 0;
                    decimal _tempDPForex = 0;
                    decimal _tempPPHForex = 0;
                    decimal _tempPPNForex = 0;
                    decimal _tempTotalForex = 0;
                    decimal _nettoForex = 0;
                    decimal _tempDisc = 0;

                    PRCPODt _prcPODt = this.GetSinglePRCPODt(_prmTransNmbr, _prmRevisi, _vPRForPO.ProductCode);

                    //decimal _totalQty = this.GetQtyFromPRCPODt2(_transNmbr[0], Convert.ToInt32(_revisi[0]), _productCode[0]) - _qty;
                    //decimal _convertQty = _unitBL.FindConvertionUnit(_productCode[0], _prcPODt.Unit, _prcPODt.UnitWrhs) * _totalQty;

                    if (_prcPODt != null)
                    {
                        _nettoForex = _prcPODt.NettoForex;
                        _tempDisc = _prcPODt.Disc;

                        decimal _allQty = _unitBL.FindConvertionUnit(_vPRForPO.ProductCode, _prcPODt.UnitWrhs, _prcPODt.Unit) * _prcPODt.Qty;
                        decimal _currentQty = _unitBL.FindConvertionUnit(_vPRForPO.ProductCode, _prcPODt.UnitWrhs, _prcPODt.Unit) * _vPRForPO.Qty;

                        _prcPODt.Qty = _allQty + _currentQty;
                        //_prcPODt.Qty = _vPRForPO.Qty + GetQtyFromPRCPODt2(_prmTransNmbr, _vPRForPO.Revisi, _vPRForPO.ProductCode);
                        _prcPODt.AmountForex = _prcPODt.Qty * _prcPODt.PriceForex;

                        if (_tempDisc == 0)
                        {
                            _tempDiscForex = _prcPODt.DiscForex;
                        }
                        else
                        {
                            _tempDiscForex = _tempBaseForex * _tempDisc / 100;
                        }

                        _prcPODt.NettoForex = _prcPODt.AmountForex - _prcPODt.DiscForex;
                        //_prcPODt.QtyWrhsPO = this.GetQtyFromPRCPODt2(_prmTransNmbr, _prmRevisi, _vPRForPO.ProductCode) + _vPRForPO.Qty;
                        _prcPODt.QtyWrhsPO = this.GetQtyFromVPRRequestForPOByRequest(_vPRForPO.RequestNo, _vPRForPO.ProductCode) + _prcPODt.QtyWrhsPO;
                        _prcPODt.QtyWrhsTotal = _prcPODt.QtyWrhsPO + _prcPODt.QtyWrhsFree;
                    }
                    else
                    {
                        PriceGroupBL _priceGroupBL = new PriceGroupBL();
                        ProductBL _productBL = new ProductBL();
                        CompanyConfig _compConfig = new CompanyConfig();

                        string _pgYear = _compConfig.GetSingle(CompanyConfigure.ActivePGYear).SetValue;
                        bool _isUsingPG = _productBL.GetSingleProductType(_productBL.GetSingleProduct(_vPRForPO.ProductCode).ProductType).IsUsingPG;

                        _prcPODt = new PRCPODt();

                        _prcPODt.TransNmbr = _prmTransNmbr;
                        _prcPODt.Revisi = _prmRevisi;
                        _prcPODt.ProductCode = _vPRForPO.ProductCode;
                        _prcPODt.Unit = _vPRForPO.Unit;
                        _prcPODt.UnitWrhs = _prcPODt.Unit;
                        _prcPODt.DoneClosing = YesNoDataMapper.GetYesNo(YesNo.No);
                        //_prcPODt.Qty = _vPRForPO.Qty + this.GetQtyFromVPRRequestForPOByRequest(_vPRForPO.RequestNo, _vPRForPO.ProductCode);
                        _prcPODt.Qty = _vPRForPO.Qty;
                        if (_isUsingPG == true)
                        {
                            string _priceGroupCode = _productBL.GetSingleProduct(_vPRForPO.ProductCode).PriceGroupCode;
                            decimal _price = _priceGroupBL.GetSingle(_priceGroupCode, Convert.ToInt32(_pgYear.Trim())).AmountForex;

                            _prcPODt.PriceForex = _price; /*/ _forexRate;*/
                            _prcPODt.AmountForex = _prcPODt.PriceForex * _prcPODt.Qty;
                            _prcPODt.NettoForex = _prcPODt.AmountForex;
                        }
                        else
                        {
                            _prcPODt.PriceForex = 0;
                            _prcPODt.AmountForex = 0;
                            _prcPODt.NettoForex = 0;
                        }
                        _prcPODt.Disc = 0;

                        _prcPODt.QtyWrhsPO = _prcPODt.Qty;
                        _prcPODt.QtyWrhsFree = 0;
                        _prcPODt.QtyWrhsTotal = _prcPODt.QtyWrhsPO + _prcPODt.QtyWrhsFree;

                        _prcPODt.CreatedBy = _vPRForPO.CreatedBy;
                        _prcPODt.CreatedDate = _vPRForPO.CreatedDate;
                        _prcPODt.EditBy = _vPRForPO.CreatedBy;
                        _prcPODt.EditDate = _vPRForPO.CreatedDate;

                        this.db.PRCPODts.InsertOnSubmit(_prcPODt);
                    }

                    PRCPOHd _prcPOHd = this.GetSinglePRCPOHd(_prmTransNmbr, _prmRevisi);

                    _tempBaseForex = _prcPOHd.BaseForex - _nettoForex + _prcPODt.NettoForex;
                    _tempDiscForex = _tempBaseForex * (_prcPOHd.Disc / 100);
                    _tempDPForex = (_tempBaseForex - _tempDiscForex) * _prcPOHd.DP / 100;
                    _tempPPHForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.PPH / 100);
                    _tempPPNForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.PPN / 100);
                    _tempTotalForex = _tempBaseForex - _tempDiscForex + _tempPPNForex + _tempPPHForex;

                    _prcPOHd.BaseForex = _tempBaseForex;
                    _prcPOHd.DiscForex = _tempDiscForex;
                    _prcPOHd.DPForex = _tempDPForex;
                    _prcPOHd.PPHForex = _tempPPHForex;
                    _prcPOHd.PPNForex = _tempPPNForex;
                    _prcPOHd.TotalForex = _tempTotalForex;

                    this.db.SubmitChanges();
                }

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //NB: _prmArray harus tersusun berdasarkan :
        //      Trim(TransNmbr)-Trim(Revisi)-Trim(ProductCode)-Trim(RequestNo)
        private List<String[]> ExtractArray(string[] _prmArray)
        {
            List<String[]> _result = new List<String[]>();
            string[] _transNmbr = new string[_prmArray.Length];
            string[] _revisi = new string[_prmArray.Length];
            //string[] _productCode = new string[_prmArray.Length];
            string[] _reqNo = new string[_prmArray.Length];

            for (int i = 0; i < _prmArray.Count(); i++)
            {
                string[] _temp = _prmArray[i].Split('-');

                _transNmbr[i] = _temp[0];
                _revisi[i] = _temp[1];
                //_productCode[i] = _temp[2];
                _reqNo[i] = _temp[2];
            }

            _result.Add(_transNmbr);
            _result.Add(_revisi);
            //_result.Add(_productCode);
            _result.Add(_reqNo);

            return _result;
        }

        //NB: _prmCode harus tersusun berdasarkan :
        //      Trim(TransNmbr)-Trim(Revisi)-Trim(ProductCode)-Trim(RequestNo)
        public bool DeleteMultiPRCPODt2(string[] _prmCode)
        {
            bool _result = false;
            //decimal _tempBaseForex = 0;
            //decimal _tempDiscForex = 0;
            //decimal _tempDPForex = 0;
            //decimal _tempPPHForex = 0;
            //decimal _tempPPNForex = 0;
            //decimal _tempTotalForex = 0;
            //decimal _nettoForex = 0;

            try
            {
                //PRCPOHd _prcPOHd = null;

                //List<String[]> _product = this.ExtractArray(_prmCode);

                //string[] _productArray = _product[2];

                //var _query1 = (
                //                from _productArray2 in _productArray
                //                select _productArray2
                //             ).Distinct();

                //foreach (var _row1 in _query1)
                //{
                //    var _query2 = (
                //                        from _code in _prmCode
                //                        where _code.Contains(_row1)
                //                        select _code
                //                  );

                //    String[] _code2 = new String[_query2.Count()];

                //    int i = 0;
                //    foreach (var _row2 in _query2)
                //    {
                //        _code2[i] = _row2;
                //        i++;
                //    }

                //    List<string[]> _product2 = this.ExtractArray(_code2);

                //    string[] _transNmbr = _product2[0];
                //    string[] _revisi = _product2[1];
                //    string[] _productCode = _product2[2];
                //    string[] _reqNo = _product2[3];

                //    var _query3 = from _prcPODt2 in this.db.PRCPODt2s
                //                  where _prcPODt2.TransNmbr == _transNmbr[0]
                //                    && _prcPODt2.Revisi == Convert.ToInt32(_revisi[0])
                //                    && _prcPODt2.ProductCode == _productCode[0]
                //                    &&
                //                    (
                //                        _reqNo
                //                    ).Contains(_prcPODt2.RequestNo)
                //                  group _prcPODt2 by _prcPODt2.ProductCode into _grp
                //                  select new
                //                  {
                //                      Qty = _grp.Sum(a => a.Qty)
                //                  };

                //    decimal _qty = 0;

                //    foreach (var _row3 in _query3)
                //    {
                //        _qty = _row3.Qty;
                //    }

                //    //update detail 1
                //    //PRCPODt2 _PRCPODt2 = this.GetSinglePRCPODt2(_transNmbr[0], Convert.ToInt32(_revisi[0]), _productCode[0], _reqNo[0]);
                //    PRCPODt _prcPODt = this.GetSinglePRCPODt(_transNmbr[0], Convert.ToInt32(_revisi[0]), _productCode[0]);

                //    _nettoForex = _nettoForex + _prcPODt.NettoForex;

                //    decimal _totalQty = this.GetQtyFromPRCPODt2(_transNmbr[0], Convert.ToInt32(_revisi[0]), _productCode[0]) - _qty;

                //    if (_totalQty > 0)
                //    {
                //        decimal _convertQty = _unitBL.FindConvertionUnit(_productCode[0], _prcPODt.UnitWrhs, _prcPODt.Unit) * _totalQty;

                //        _prcPODt.Qty = _convertQty;
                //        _prcPODt.AmountForex = _prcPODt.Qty * _prcPODt.PriceForex;
                //        _prcPODt.DiscForex = _prcPODt.Disc * _prcPODt.AmountForex / 100;
                //        _prcPODt.NettoForex = _prcPODt.AmountForex - _prcPODt.DiscForex;
                //        _prcPODt.QtyWrhsPO = _totalQty;
                //        _prcPODt.QtyWrhsTotal = _prcPODt.QtyWrhsPO + _prcPODt.QtyWrhsFree;
                //        _nettoForex = _nettoForex - _prcPODt.NettoForex;
                //    }
                //    else
                //    {
                //        this.db.PRCPODts.DeleteOnSubmit(_prcPODt);
                //    }

                //    _prcPOHd = this.GetSinglePRCPOHd(_transNmbr[0], Convert.ToInt32(_revisi[0]));

                //    var _query4 = from _prcPODt2_1 in this.db.PRCPODt2s
                //                  where (
                //                            _reqNo
                //                        ).Contains(_prcPODt2_1.RequestNo)
                //                        && _prcPODt2_1.ProductCode == _productCode[0]
                //                        && _prcPODt2_1.TransNmbr == _transNmbr[0]
                //                        && _prcPODt2_1.Revisi == Convert.ToInt32(_revisi[0])
                //                  select _prcPODt2_1;

                //    this.db.PRCPODt2s.DeleteAllOnSubmit(_query4);
                //}

                //_tempBaseForex = _prcPOHd.BaseForex - _nettoForex;
                //_tempDiscForex = _tempBaseForex * (_prcPOHd.Disc / 100);
                //_tempDPForex = (_tempBaseForex - _tempDiscForex) * _prcPOHd.DP / 100;
                //_tempPPHForex = (_tempBaseForex - _tempDiscForex) * _prcPOHd.PPH / 100;
                //_tempPPNForex = (_tempBaseForex - _tempDiscForex) * (_prcPOHd.PPN / 100);
                //_tempTotalForex = _tempBaseForex - _tempDiscForex + _tempPPNForex + _tempPPHForex;

                //_prcPOHd.BaseForex = _tempBaseForex;
                //_prcPOHd.DiscForex = _tempDiscForex;
                //_prcPOHd.DPForex = _tempDPForex;
                //_prcPOHd.PPHForex = _tempPPHForex;
                //_prcPOHd.PPNForex = _tempPPNForex;
                //_prcPOHd.TotalForex = _tempTotalForex;

                //this.db.SubmitChanges();

                //_result = true;

                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');

                    var _queryDt2 = (
                                    from _prcPODt2 in this.db.PRCPODt2s
                                    where _prcPODt2.TransNmbr == _tempSplit[0]
                                        && _prcPODt2.Revisi == Convert.ToInt32(_tempSplit[1])
                                        && _prcPODt2.RequestNo == _tempSplit[2]
                                    select _prcPODt2
                                 );

                    this.db.PRCPODt2s.DeleteAllOnSubmit(_queryDt2);

                    var _queryDt = (
                                    from _prcPODt in this.db.PRCPODts
                                    where _prcPODt.TransNmbr == _tempSplit[0]
                                        && _prcPODt.Revisi == Convert.ToInt32(_tempSplit[1])
                                    select _prcPODt
                                 );

                    this.db.PRCPODts.DeleteAllOnSubmit(_queryDt);

                    PRCPOHd _prcPOHd = this.db.PRCPOHds.Single(_temp => _temp.TransNmbr == _tempSplit[0] && _temp.Revisi == Convert.ToInt32(_tempSplit[1]));
                    _prcPOHd.BaseForex = 0;
                    _prcPOHd.DiscForex = 0;
                    _prcPOHd.DPForex = 0;
                    _prcPOHd.PPHForex = 0;
                    _prcPOHd.PPNForex = 0;
                    _prcPOHd.TotalForex = 0;
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

        //public List<MsProduct> GetListDDLProductFromPRCPODt2(string _prmCode, int _prmRevisi)
        //{
        //    List<MsProduct> _result = new List<MsProduct>();

        //    try
        //    {
        //        var _query = (
        //                        from _prcPODt2 in this.db.PRCPODt2s
        //                        join _msProduct in this.db.MsProducts
        //                            on _prcPODt2.ProductCode equals _msProduct.ProductCode
        //                        where _prcPODt2.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _prcPODt2.Revisi == _prmRevisi
        //                        orderby _msProduct.ProductName ascending
        //                        select new
        //                        {
        //                            ProductCode = _prcPODt2.ProductCode,
        //                            ProductName = _msProduct.ProductName
        //                        }
        //                    ).Distinct();

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public decimal GetQtyFromPRCPODt2(string _prmTransNmbr, int _prmRevisi, string _prmProductCode)
        //{
        //    decimal _result = 0;
        //    try
        //    {
        //        var _query = (
        //                        from _prcPODt2 in this.db.PRCPODt2s
        //                        where _prcPODt2.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _prcPODt2.Revisi == _prmRevisi && _prcPODt2.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower()
        //                        group _prcPODt2 by _prcPODt2.ProductCode into _grp
        //                        select new
        //                        {
        //                            Qty = _grp.Sum(a => a.Qty)
        //                        }
        //                    );

        //        foreach (var _row in _query)
        //        {
        //            _result = _row.Qty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        //public decimal GetQtyFromPRCPODt2_2(string _prmTransNmbr, int _prmRevisi, string _prmProductCode)
        //{
        //    decimal _result = 0;
        //    string[] aaa = new string[9];

        //    try
        //    {
        //        //delete
        //        var _query = from _prcPODt2 in this.db.PRCPODt2s
        //                     where (_prcPODt2.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower())
        //                            && (_prcPODt2.Revisi == _prmRevisi)
        //                            && _prcPODt2.ProductCode == _prmProductCode
        //                            && !(
        //                                    from _aaa in this.db.PRCPODt2s
        //                                    where (
        //                                            aaa
        //                                            ).Contains(_aaa.ProductCode)
        //                                            && _aaa.TransNmbr == _prmTransNmbr
        //                                            && _aaa.Revisi == _prmRevisi
        //                                    select new
        //                                    {
        //                                        _aaa.ProductCode
        //                                    }
        //                                ).Contains(_prcPODt2.ProductCode)
        //                     group _prcPODt2 by _prcPODt2.ProductCode into _grp
        //                     select new
        //                     {
        //                         Qty = _grp.Sum(a => a.Qty)
        //                     };


        //        //var _qqqq -> aaa 
        //        //this.db.PRCPODt2s.DeleteAllOnSubmit(_qqqq)

        //        //var _query = (
        //        //                from _prcPODt2 in this.db.PRCPODt2s
        //        //                where !(
        //        //                            from _prcPODt in this.db.PRCPODts
        //        //                            where _prcPODt2.TransNmbr == _prmTransNmbr && _prcPODt2.Revisi == _prmRevisi
        //        //                            select _prcPODt.ProductCode
        //        //                        ).Contains(_prcPODt2.ProductCode)
        //        //                    && _prcPODt2.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower() && _prcPODt2.Revisi == _prmRevisi
        //        //                group _prcPODt2 by _prcPODt2.ProductCode into _grp
        //        //                select new
        //        //                {
        //        //                    Qty = _grp.Sum(a => a.Qty)
        //        //                }
        //        //            );

        //        foreach (var _row in _query)
        //        {
        //            _result = _row.Qty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}
        #endregion

        #region V_PRPOForRR
        public List<PRCPOHd> GetPONoFromVPRPOForRR(string _prmSuppCode)
        {
            List<PRCPOHd> _result = new List<PRCPOHd>();

            try
            {
                var _query = (
                                from _vPRPOForRR in this.db.V_PRPOForRRs
                                where _vPRPOForRR.Supp_Code == _prmSuppCode
                                    && (_vPRPOForRR.FileNmbr ?? "").Trim() == _vPRPOForRR.FileNmbr.Trim()
                                select new
                                {
                                    PONo = _vPRPOForRR.PO_No,
                                    FileNmbr = _vPRPOForRR.FileNmbr
                                }
                             ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new PRCPOHd(_row.PONo, _row.FileNmbr));
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductForDDLPODt(string _prmPONo)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _vPRPOForRR in this.db.V_PRPOForRRs
                                where _vPRPOForRR.PO_No.Trim().ToLower() == _prmPONo.Trim().ToLower()
                                orderby _vPRPOForRR.Product_Name
                                select new
                                {
                                    Product_Code = _vPRPOForRR.Product_Code,
                                    Product_Name = _vPRPOForRR.Product_Name
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.Product_Code, _row.Product_Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public decimal GetQtyFromVPRPOForRR(string _prmPONo, string _prmProductCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _vPRPOForRR in this.db.V_PRPOForRRs
                                where _vPRPOForRR.PO_No == _prmPONo && _vPRPOForRR.Product_Code == _prmProductCode
                                select new
                                {
                                    Qty = (_vPRPOForRR.Qty == null) ? 0 : Convert.ToDecimal(_vPRPOForRR.Qty)
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = _row.Qty;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region V_PRRequestForPO

        //public List<MsProduct> GetListDDLProductVPRRequestForPO()
        //{
        //    List<MsProduct> _result = new List<MsProduct>();

        //    try
        //    {
        //        var _query = (
        //                        from _vPRRequestForPO in this.db.V_PRRequestForPOs
        //                        orderby _vPRRequestForPO.Product_Name ascending
        //                        select new
        //                        {
        //                            Product_Code = _vPRRequestForPO.Product_Code,
        //                            Product_Name = _vPRRequestForPO.Product_Name
        //                        }
        //                    ).Distinct();

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsProduct(_row.Product_Code, _row.Product_Name));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public List<PRCRequestHd> GetListDDLPRNoVPRRequestForPO()
        {
            List<PRCRequestHd> _result = new List<PRCRequestHd>();

            try
            {
                var _query = (
                                from _vPRRequestForPO in this.db.V_PRRequestForPOs
                                //where _vPRRequestForPO.Product_Code.Trim().ToLower() == _prmProductCode.Trim().ToLower() && ((_vPRRequestForPO.FileNmbr ?? "") == _vPRRequestForPO.FileNmbr)
                                where ((_vPRRequestForPO.FileNmbr ?? "") == _vPRRequestForPO.FileNmbr)
                                orderby _vPRRequestForPO.FileNmbr ascending
                                select new
                                {
                                    PR_No = _vPRRequestForPO.PR_No,
                                    FileNmbr = _vPRRequestForPO.FileNmbr,
                                    RequestBy = _vPRRequestForPO.RequestBy
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new PRCRequestHd(_row.PR_No, _row.FileNmbr, _row.RequestBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<V_PRRequestForPO> GetListDDLPRNoVPRRequestForPO(String _prmTransNo, int _prmRevisi)
        {
            List<V_PRRequestForPO> _result = new List<V_PRRequestForPO>();

            try
            {
                var _query = (
                                from _prcPODt2 in this.db.PRCPODt2s
                                join _prcReqHd in this.db.PRCRequestHds
                                    on _prcPODt2.RequestNo equals _prcReqHd.TransNmbr
                                where _prcPODt2.TransNmbr == _prmTransNo
                                    && _prcPODt2.Revisi == _prmRevisi
                                    && ((_prcReqHd.FileNmbr ?? "") == _prcReqHd.FileNmbr)
                                orderby _prcReqHd.FileNmbr ascending
                                select new
                                {
                                    TransNmbr = _prcPODt2.TransNmbr,
                                    Revisi = _prcPODt2.Revisi,
                                    PR_No = _prcReqHd.TransNmbr,
                                    FileNmbr = _prcReqHd.FileNmbr,
                                    RequestBy = _prcReqHd.RequestBy
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new V_PRRequestForPO(_row.TransNmbr, _row.Revisi, _row.PR_No, _row.FileNmbr, _row.RequestBy));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public decimal GetQtyFromVPRRequestForPOByRequest(string _prmPRNo)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _vPRRequestForPO in this.db.V_PRRequestForPOs
                                where _vPRRequestForPO.PR_No.Trim().ToLower() == _prmPRNo.Trim().ToLower()
                                group _vPRRequestForPO by _vPRRequestForPO.PR_No into _grp
                                select new
                                {
                                    Qty = _grp.Sum(a => a.Qty)
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = (_row.Qty == null) ? 0 : (decimal)_row.Qty;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public decimal GetQtyFromVPRRequestForPOByRequest(String _prmPRNo, String _prmProductCode)
        {
            decimal _result = 0;

            try
            {
                var _query = (
                                from _vPRRequestForPO in this.db.V_PRRequestForPOs
                                where _vPRRequestForPO.PR_No.Trim().ToLower() == _prmPRNo.Trim().ToLower()
                                    && _vPRRequestForPO.Product_Code.Trim().ToLower() == _prmProductCode.Trim().ToLower()
                                group _vPRRequestForPO by _vPRRequestForPO.PR_No into _grp
                                select new
                                {
                                    Qty = _grp.Sum(a => a.Qty)
                                }
                             );
                foreach (var _row in _query)
                {
                    _result = (_row.Qty == null) ? 0 : (decimal)_row.Qty;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~PurchaseOrderBL()
        {

        }
    }
}
