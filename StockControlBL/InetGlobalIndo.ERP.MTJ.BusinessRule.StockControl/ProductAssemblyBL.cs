using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Diagnostics;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Web;
using System.Transactions;
using System.Data.Linq;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public sealed class ProductAssemblyBL : Base
    {
        public ProductAssemblyBL()
        {

        }

        #region Product Assembly
        public double RowsCountSTCTrProductAssembly(string _prmCategory, string _prmKeyword)
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
            else if (_prmCategory == "ProductName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            var _query =
                        (
                            from _stcTrProductAssembly in this.db.STCTrProductAssemblies
                            join _msProduct in this.db.MsProducts
                                on _stcTrProductAssembly.ProductCode equals _msProduct.ProductCode
                            where (SqlMethods.Like(_stcTrProductAssembly.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                 && (SqlMethods.Like((_stcTrProductAssembly.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                 && _stcTrProductAssembly.fgAssembly == true
                            select _stcTrProductAssembly.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }
        
        public List<STCTrProductAssembly> GetListSTCTrProductAssembly(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCTrProductAssembly> _result = new List<STCTrProductAssembly>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "ProductName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _stcTrProductAssembly in this.db.STCTrProductAssemblies
                                join _msProduct in this.db.MsProducts
                                    on _stcTrProductAssembly.ProductCode equals _msProduct.ProductCode
                                where (SqlMethods.Like(_stcTrProductAssembly.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                     && (SqlMethods.Like((_stcTrProductAssembly.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                     && _stcTrProductAssembly.fgAssembly == true
                                orderby _stcTrProductAssembly.TransDate descending
                                select new
                                {
                                    TransNmbr = _stcTrProductAssembly.TransNmbr,
                                    FileNmbr = _stcTrProductAssembly.FileNmbr,
                                    TransDate = _stcTrProductAssembly.TransDate,
                                    ProductCode = _stcTrProductAssembly.ProductCode,
                                    ProductName = _msProduct.ProductName,
                                    Status = _stcTrProductAssembly.Status,
                                    Reference = _stcTrProductAssembly.Reference,
                                    Qty = _stcTrProductAssembly.Qty,
                                    Remark = _stcTrProductAssembly.Remark
                                }
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCTrProductAssembly(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.ProductCode, _row.ProductName, _row.Status, _row.Reference, _row.Qty, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCTrProductAssembly> GetListSTCTrProductAssemblyForDDL()
        {
            List<STCTrProductAssembly> _result = new List<STCTrProductAssembly>();

            try
            {
                var _query = (
                                from _stcTrProductAssembly in this.db.STCTrProductAssemblies
                                orderby _stcTrProductAssembly.TransNmbr ascending
                                select new
                                {
                                    TransNmbr = _stcTrProductAssembly.TransNmbr,
                                    FileNmbr = _stcTrProductAssembly.FileNmbr
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new STCTrProductAssembly(_row.TransNmbr, _row.FileNmbr));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCTrProductAssembly GetSingleSTCTrProductAssembly(string _prmCode)
        {
            STCTrProductAssembly _result = null;

            try
            {
                _result = this.db.STCTrProductAssemblies.Single(_temp => _temp.TransNmbr.ToLower() == _prmCode.ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiSTCTrProductAssembly(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTrProductAssembly _stcTrProductAssembly = this.db.STCTrProductAssemblies.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_stcTrProductAssembly != null)
                    {
                        if ((_stcTrProductAssembly.FileNmbr ?? "").Trim() == "")
                        {
                            this.db.STCTrProductAssemblies.DeleteOnSubmit(_stcTrProductAssembly);

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

        public string AddSTCTrProductAssembly(STCTrProductAssembly _prmSTCTrProductAssembly)
        {
            string _result = "";

            try
            {

                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmSTCTrProductAssembly.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.STCTrProductAssemblies.InsertOnSubmit(_prmSTCTrProductAssembly);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmSTCTrProductAssembly.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditSTCTrProductAssembly(STCTrProductAssembly _prmSTCTrProductAssembly)
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

        public string GetAppr(string _prmTransNmbr, String _prmUserName)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.spSTC_ProductAssemblyGetAppr(_prmTransNmbr, _prmUserName, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ProductAssembly);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
                    _transActivity.FileNmbr = "";
                    _transActivity.Username = _prmUserName;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.GetApproval);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
                else
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Get Approval Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string Approve(string _prmTransNmbr, String _prmUserName)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    int _success = this.db.spSTC_ProductAssemblyApprove(_prmTransNmbr, _prmUserName, ref _errorMsg);

                    if (_errorMsg == "")
                    {
                        STCTrProductAssembly _stcTrProductAssembly = this.GetSingleSTCTrProductAssembly(_prmTransNmbr);

                        if ((_stcTrProductAssembly.FileNmbr ?? "").Trim() == "")
                        {
                            foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_stcTrProductAssembly.TransDate.Year, _stcTrProductAssembly.TransDate.Month, AppModule.GetValue(TransactionType.ProductAssembly), this._companyTag, ""))
                            {
                                _stcTrProductAssembly.FileNmbr = item.Number;
                            }

                            Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                            _transActivity.ActivitiesCode = Guid.NewGuid();
                            _transActivity.TransType = AppModule.GetValue(TransactionType.ProductAssembly);
                            _transActivity.TransNmbr = _prmTransNmbr.ToString();
                            _transActivity.FileNmbr = this.GetSingleSTCTrProductAssembly(_prmTransNmbr).FileNmbr;
                            _transActivity.Username = _prmUserName;
                            _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                            _transActivity.ActivitiesDate = DateTime.Now;
                            _transActivity.Reason = "";

                            this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                            this.db.SubmitChanges();

                            _result = "Approve Success";
                            _scope.Complete();
                        }
                    }
                    else
                    {
                        _result = _errorMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                _result = "Approve Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string Posting(string _prmTransNmbr, String _prmUserName)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                int _success = this.db.spSTC_ProductAssemblyPost(_prmTransNmbr, _prmUserName, ref _errorMsg);

                if (_errorMsg == "")
                {
                    _result = "Posting Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.ProductAssembly);
                    _transActivity.TransNmbr = _prmTransNmbr.ToString();
                    _transActivity.FileNmbr = this.GetSingleSTCTrProductAssembly(_prmTransNmbr).FileNmbr;
                    _transActivity.Username = _prmUserName;
                    _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Posting);
                    _transActivity.ActivitiesDate = DateTime.Now;
                    _transActivity.Reason = "";

                    this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    this.db.SubmitChanges();
                }
                else
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public string Unposting(string _prmTransNmbr, String _prmUserName)
        {
            string _result = "";
            string _errorMsg = "";

            try
            {
                //int _success = this.db.spSTC_ProductAssemblyUnPost(_prmTransNmbr, _prmUserName, ref _errorMsg);
                ISingleResult<spSTC_ProductAssemblyUnPostResult> _execute = this.db.spSTC_ProductAssemblyUnPost(_prmTransNmbr, _prmUserName, ref _errorMsg);
                if (_errorMsg == "")
                {
                    _result = "UnPosting Success";

                    //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    //_transActivity.ActivitiesCode = Guid.NewGuid();
                    //_transActivity.TransType = AppModule.GetValue(TransactionType.ProductAssembly);
                    //_transActivity.TransNmbr = _prmTransNmbr.ToString();
                    //_transActivity.FileNmbr = this.GetSingleSTCTrProductAssembly(_prmTransNmbr).FileNmbr;
                    //_transActivity.Username = _prmUserName;
                    //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                    //_transActivity.ActivitiesDate = this.GetSingleSTCTrProductAssembly(_prmTransNmbr).TransDate;
                    //_transActivity.Reason = "";

                    //this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                    //this.db.SubmitChanges();
                }
                else
                {
                    _result = _errorMsg;
                }
            }
            catch (Exception ex)
            {
                _result = "UnPosting Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }
        
        public string GetFileNmbrSTCTrProductAssembly(string _prmTransNmbr)
        {
            string _result = "";

            try
            {
                _result = (this.db.STCTrProductAssemblies.Single(_temp => _temp.TransNmbr == _prmTransNmbr).FileNmbr ?? "").Trim();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSTCTrProductAssemblyForStatus(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTrProductAssembly _STCTrProductAssembly = this.db.STCTrProductAssemblies.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_STCTrProductAssembly != null)
                    {
                        if (_STCTrProductAssembly.Status != ProductAssemblyDataMapper.GetStatusByte(TransStatus.Posted))
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

        public bool DeleteMultiApproveSTCTrProductAssembly(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    STCTrProductAssembly _STCTrProductAssembly = this.db.STCTrProductAssemblies.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_STCTrProductAssembly.Status == ProductAssemblyDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _STCTrProductAssembly.TransNmbr;
                        _unpostingActivity.FileNmbr = _STCTrProductAssembly.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_STCTrProductAssembly != null)
                    {
                        if ((_STCTrProductAssembly.FileNmbr ?? "").Trim() == "")
                        {
                            this.db.STCTrProductAssemblies.DeleteOnSubmit(_STCTrProductAssembly);

                            _result = true;
                        }
                        else if (_STCTrProductAssembly.FileNmbr != "" && _STCTrProductAssembly.Status == ProductAssemblyDataMapper.GetStatusByte(TransStatus.Approved))
                        {
                            _STCTrProductAssembly.Status = ProductAssemblyDataMapper.GetStatusByte(TransStatus.Deleted);
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

        #endregion

        #region Product Assembly

        public double RowsCountSTCTrProductDisAssembly(string _prmCategory, string _prmKeyword)
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
            else if (_prmCategory == "ProductName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            var _query =
                        (
                            from _stcTrProductAssembly in this.db.STCTrProductAssemblies
                            join _msProduct in this.db.MsProducts
                                on _stcTrProductAssembly.ProductCode equals _msProduct.ProductCode
                            where (SqlMethods.Like(_stcTrProductAssembly.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                 && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                 && (SqlMethods.Like((_stcTrProductAssembly.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                 && _stcTrProductAssembly.fgAssembly == false
                            select _stcTrProductAssembly.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<STCTrProductAssembly> GetListSTCTrProductDisAssembly(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCTrProductAssembly> _result = new List<STCTrProductAssembly>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "ProductName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _stcTrProductAssembly in this.db.STCTrProductAssemblies
                                join _msProduct in this.db.MsProducts
                                    on _stcTrProductAssembly.ProductCode equals _msProduct.ProductCode
                                where (SqlMethods.Like(_stcTrProductAssembly.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                     && (SqlMethods.Like((_stcTrProductAssembly.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                     && _stcTrProductAssembly.fgAssembly == false
                                orderby _stcTrProductAssembly.TransDate descending
                                select new
                                {
                                    TransNmbr = _stcTrProductAssembly.TransNmbr,
                                    FileNmbr = _stcTrProductAssembly.FileNmbr,
                                    TransDate = _stcTrProductAssembly.TransDate,
                                    ProductCode = _stcTrProductAssembly.ProductCode,
                                    ProductName = _msProduct.ProductName,
                                    Status = _stcTrProductAssembly.Status,
                                    Reference = _stcTrProductAssembly.Reference,
                                    Qty = _stcTrProductAssembly.Qty,
                                    Remark = _stcTrProductAssembly.Remark
                                }
                             ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCTrProductAssembly(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.ProductCode, _row.ProductName, _row.Status, _row.Reference, _row.Qty, _row.Remark));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        #endregion

        ~ProductAssemblyBL()
        {

        }
    }
}
