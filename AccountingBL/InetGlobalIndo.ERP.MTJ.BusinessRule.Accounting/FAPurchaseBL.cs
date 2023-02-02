using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Transactions;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting
{
    public sealed class FAPurchaseBL : Base
    {
        public FAPurchaseBL()
        {

        }

        #region FAPurchase
        public double RowsCountFAPurchase(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "PurchaseNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";

            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern1 = "%%";

            }
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }

            var _query =
                        (
                            from _glFAPurchaseHd in this.db.GLFAPurchaseHds
                            join _msSupplier in this.db.MsSuppliers
                                   on _glFAPurchaseHd.SuppCode equals _msSupplier.SuppCode
                            where (SqlMethods.Like(_glFAPurchaseHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like((_glFAPurchaseHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && (_glFAPurchaseHd.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted))
                            select _glFAPurchaseHd.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<GLFAPurchaseHd> GetListFAPurchaseHd(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<GLFAPurchaseHd> _result = new List<GLFAPurchaseHd>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "PurchaseNo")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";

            }
            else if (_prmCategory == "FileNo")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern1 = "%%";

            }
            else if (_prmCategory == "SuppName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
            }

            try
            {
                var _query =
                            (
                                from _glFAPurchaseHd in this.db.GLFAPurchaseHds
                                join _msSupplier in this.db.MsSuppliers
                                    on _glFAPurchaseHd.SuppCode equals _msSupplier.SuppCode
                                where (SqlMethods.Like(_glFAPurchaseHd.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msSupplier.SuppName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like((_glFAPurchaseHd.FileNmbr ?? "").Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && (_glFAPurchaseHd.Status != TransactionDataMapper.GetStatus(TransStatus.Deleted))
                                orderby _glFAPurchaseHd.DatePrep descending
                                select new
                                {
                                    TransNmbr = _glFAPurchaseHd.TransNmbr,
                                    FileNmbr = _glFAPurchaseHd.FileNmbr,
                                    TransDate = _glFAPurchaseHd.TransDate,
                                    Status = _glFAPurchaseHd.Status,
                                    SuppCode = _glFAPurchaseHd.SuppCode,
                                    CurrCode = _glFAPurchaseHd.CurrCode
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new GLFAPurchaseHd(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.Status, _row.SuppCode, _row.CurrCode));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFAPurchaseHd GetSingleFAPurchaseHd(string _prmCode)
        {
            GLFAPurchaseHd _result = null;

            try
            {
                _result = this.db.GLFAPurchaseHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public char GetStatusFAPurchaseHd(string _prmCode)
        {
            char _result = ' ';

            try
            {
                var _query = (
                                from _glFAPurchaseHd in this.db.GLFAPurchaseHds
                                where _glFAPurchaseHd.TransNmbr == _prmCode
                                select new
                                {
                                    Status = _glFAPurchaseHd.Status
                                }
                            );

                foreach (var _row in _query)
                {
                    _result = _row.Status;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetSingleFAPurchaseHdpprove(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAPurchaseHd _glFAPurchaseHd = this.db.GLFAPurchaseHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFAPurchaseHd != null)
                    {
                        if (_glFAPurchaseHd.Status != TransactionDataMapper.GetStatus(TransStatus.Posted))
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

        public bool DeleteMultiFAPurchaseHd(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAPurchaseHd _glFAPurchaseHd = this.db.GLFAPurchaseHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFAPurchaseHd != null)
                    {
                        if ((_glFAPurchaseHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLFAPurchaseDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.GLFAPurchaseDts.DeleteAllOnSubmit(_query);

                            this.db.GLFAPurchaseHds.DeleteOnSubmit(_glFAPurchaseHd);

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

        public bool DeleteMultiApproveFAPurchaseHd(string[] _prmCode, string _prmTransType, string _prmUsername, byte _prmActivitiesID, string _prmReason)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    GLFAPurchaseHd _glFAPurchaseHd = this.db.GLFAPurchaseHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_glFAPurchaseHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                    {
                        Transaction_UnpostingActivity _unpostingActivity = new Transaction_UnpostingActivity();

                        _unpostingActivity.ActivitiesCode = Guid.NewGuid();
                        _unpostingActivity.TransType = _prmTransType;
                        _unpostingActivity.TransNmbr = _glFAPurchaseHd.TransNmbr;
                        _unpostingActivity.FileNmbr = _glFAPurchaseHd.FileNmbr;
                        _unpostingActivity.Username = _prmUsername;
                        _unpostingActivity.ActivitiesDate = DateTime.Now;
                        _unpostingActivity.ActivitiesID = _prmActivitiesID;
                        _unpostingActivity.Reason = _prmReason;

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_unpostingActivity);
                    }

                    if (_glFAPurchaseHd != null)
                    {
                        if ((_glFAPurchaseHd.FileNmbr ?? "").Trim() == "")
                        {
                            var _query = (from _detail in this.db.GLFAPurchaseDts
                                          where _detail.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                          select _detail);

                            this.db.GLFAPurchaseDts.DeleteAllOnSubmit(_query);

                            this.db.GLFAPurchaseHds.DeleteOnSubmit(_glFAPurchaseHd);

                            _result = true;
                        }
                        else if (_glFAPurchaseHd.FileNmbr != "" && _glFAPurchaseHd.Status == TransactionDataMapper.GetStatus(TransStatus.Approved))
                        {
                            _glFAPurchaseHd.Status = TransactionDataMapper.GetStatus(TransStatus.Deleted);
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

        public string AddFAPurchaseHd(GLFAPurchaseHd _prmGLFAPurchaseHd, List<GLFAPurchaseDt> _prmListGLFAPurchaseDt)
        {
            string _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                //foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_prmGLFAPurchaseHd.TransDate.Year, _prmGLFAPurchaseHd.TransDate.Month, AppModule.GetValue(TransactionType.FAPurchase), this._companyTag, ""))
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmGLFAPurchaseHd.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                }


                decimal _baseForex = 0;

                foreach (GLFAPurchaseDt _detail in _prmListGLFAPurchaseDt)
                {
                    _detail.TransNmbr = _prmGLFAPurchaseHd.TransNmbr;
                    _baseForex = _baseForex + Convert.ToDecimal(_detail.AmountForex);
                }

                //_prmGLFAPurchaseHd.BaseForex = _baseForex;
                //_prmGLFAPurchaseHd.PPNForex = _prmGLFAPurchaseHd.PPN * _baseForex / 100;
                //_prmGLFAPurchaseHd.TotalForex = _baseForex - _prmGLFAPurchaseHd.DiscForex + _prmGLFAPurchaseHd.PPNForex;

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.GLFAPurchaseHds.InsertOnSubmit(_prmGLFAPurchaseHd);

                var _query = (
                                from _temp in this.db.Temporary_TransactionNumbers
                                where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                                select _temp
                              );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.GLFAPurchaseDts.InsertAllOnSubmit(_prmListGLFAPurchaseDt);

                this.db.SubmitChanges();

                _result = _prmGLFAPurchaseHd.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFAPurchaseHd(GLFAPurchaseHd _prmGLFAPurchaseHd)
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

        public string GetApproval(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_GLFAPurchaseGetAppr(_prmCode, 0, 0, _prmuser, ref _result);

                if (_result == "")
                {
                    _result = "Get Approval Success";

                    Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                    _transActivity.ActivitiesCode = Guid.NewGuid();
                    _transActivity.TransType = AppModule.GetValue(TransactionType.FAPurchase);
                    _transActivity.TransNmbr = _prmCode;
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
                _result = "Get Approval Failed ";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string Approve(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    this.db.S_GLFAPurchaseApprove(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        GLFAPurchaseHd _glFAPurchaseHd = this.GetSingleFAPurchaseHd(_prmCode);
                        foreach (S_SAAutoNmbrResult _item in this.db.S_SAAutoNmbr(_glFAPurchaseHd.TransDate.Year, _glFAPurchaseHd.TransDate.Month, AppModule.GetValue(TransactionType.FAPurchase), this._companyTag, ""))
                        {
                            _glFAPurchaseHd.FileNmbr = _item.Number;
                        }

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FAPurchase);
                        _transActivity.TransNmbr = _prmCode;
                        _transActivity.FileNmbr = this.GetSingleFAPurchaseHd(_prmCode).FileNmbr;
                        _transActivity.Username = _prmuser;
                        _transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.Approve);
                        _transActivity.ActivitiesDate = DateTime.Now;
                        _transActivity.Reason = "";

                        this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        this.db.SubmitChanges();

                        _result = "Approve Success";
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

        public string Posting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                GLFAPurchaseHd _glFAPurchaseHd = this.db.GLFAPurchaseHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFAPurchaseHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFAPurchasePost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "Posting Success";

                        Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        _transActivity.ActivitiesCode = Guid.NewGuid();
                        _transActivity.TransType = AppModule.GetValue(TransactionType.FAPurchase);
                        _transActivity.TransNmbr = _prmCode;
                        _transActivity.FileNmbr = this.GetSingleFAPurchaseHd(_prmCode).FileNmbr;
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

        public string UnPosting(string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                TransactionCloseBL _transCloseBL = new TransactionCloseBL();

                GLFAPurchaseHd _glFAPurchaseHd = this.db.GLFAPurchaseHds.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());
                String _locked = _transCloseBL.IsExistAndLocked(_glFAPurchaseHd.TransDate);
                if (_locked == "") /*ini saya tambahkan untuk cek Close vidy 13 feb 09*/
                {
                    this.db.S_GLFAPurchaseUnPost(_prmCode, 0, 0, _prmuser, ref _result);

                    if (_result == "")
                    {
                        _result = "UnPosting Success";

                        //Transaction_UnpostingActivity _transActivity = new Transaction_UnpostingActivity();
                        //_transActivity.ActivitiesCode = Guid.NewGuid();
                        //_transActivity.TransType = AppModule.GetValue(TransactionType.FAPurchase);
                        //_transActivity.TransNmbr = _prmCode;
                        //_transActivity.FileNmbr = this.GetSingleFAPurchaseHd(_prmCode).FileNmbr;
                        //_transActivity.Username = _prmuser;
                        //_transActivity.ActivitiesID = ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting);
                        //_transActivity.ActivitiesDate = this.GetSingleFAPurchaseHd(_prmCode).TransDate;
                        //_transActivity.Reason = "";

                        //this.db.Transaction_UnpostingActivities.InsertOnSubmit(_transActivity);
                        //this.db.SubmitChanges();
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
        #endregion

        #region FAPurchaseDt

        public int RowsCountFAPurchaseDt(string _prmTransNmbr)
        {
            int _result = 0;

            _result = this.db.GLFAPurchaseDts.Where(_row => _row.TransNmbr.Trim().ToLower() == _prmTransNmbr.Trim().ToLower()).Count();

            return _result;
        }

        public List<GLFAPurchaseDt> GetListFAPurchaseDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<GLFAPurchaseDt> _result = new List<GLFAPurchaseDt>();

            try
            {
                var _query =
                            (
                                from _faPurchaseDt in this.db.GLFAPurchaseDts
                                where _faPurchaseDt.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower()
                                orderby _faPurchaseDt.ItemNo ascending
                                select new
                                {
                                    TransNmbr = _faPurchaseDt.TransNmbr,
                                    ItemNo = _faPurchaseDt.ItemNo,
                                    FAStatus = _faPurchaseDt.FAStatus,
                                    FAOwner = _faPurchaseDt.FAOwner,
                                    FASubGroup = _faPurchaseDt.FASubGroup,
                                    FACode = _faPurchaseDt.FACode,
                                    FAName = _faPurchaseDt.FAName,
                                    FALocationType = _faPurchaseDt.FALocationType,
                                    AmountForex = _faPurchaseDt.AmountForex,
                                    Qty = _faPurchaseDt.Qty,
                                    PriceForex = _faPurchaseDt.PriceForex
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new GLFAPurchaseDt(_row.TransNmbr, _row.ItemNo, _row.FAStatus, _row.FAOwner, _row.FASubGroup, _row.FACode, _row.FAName, _row.FALocationType, _row.AmountForex, _row.Qty, _row.PriceForex));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public GLFAPurchaseDt GetSingleFAPurchaseDt(string _prmCode, string _prmItemNo)
        {
            GLFAPurchaseDt _result = null;

            try
            {
                _result = this.db.GLFAPurchaseDts.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower() && _temp.ItemNo == Convert.ToInt32(_prmItemNo));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFAPurchaseDt(string[] _prmItemCode, string _prmCode)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;
            decimal _tempPPNForex = 0;
            int _tempDisconPercent = 0;
            Decimal _tempDisconForex = 0;

            try
            {
                for (int i = 0; i < _prmItemCode.Length; i++)
                {
                    GLFAPurchaseDt _glFAPurchaseDt = this.db.GLFAPurchaseDts.Single(_temp => _temp.ItemNo == Convert.ToInt32(_prmItemCode[i]) && _temp.TransNmbr.Trim().ToLower() == _prmCode.Trim().ToLower());

                    GLFAPurchaseHd _glFAPurchaseHd = this.GetSingleFAPurchaseHd(_prmCode);
                    _tempDisconPercent = Convert.ToInt32((Convert.ToDecimal(_glFAPurchaseHd.DiscForex) / Convert.ToDecimal(_glFAPurchaseHd.BaseForex)) * 100); //percent discon awal
                    _tempBaseForex = _glFAPurchaseHd.BaseForex = _glFAPurchaseHd.BaseForex - Convert.ToDecimal(_glFAPurchaseDt.AmountForex);
                    _tempDisconForex = _glFAPurchaseHd.DiscForex = (_tempBaseForex * _tempDisconPercent)/100;
                    _tempPPNForex = _glFAPurchaseHd.PPNForex = (_tempBaseForex - _tempDisconForex) * _glFAPurchaseHd.PPN / 100;
                    _glFAPurchaseHd.TotalForex = _tempBaseForex - _tempDisconForex + _tempPPNForex;

                    this.db.GLFAPurchaseDts.DeleteOnSubmit(_glFAPurchaseDt);
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

        public int GetMaxNoItemFAPurchaseDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.GLFAPurchaseDts.Where(_a => _a.TransNmbr == _prmCode).Max(_max => _max.ItemNo);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddFAPurchaseDt(GLFAPurchaseDt _prmGLFAPurchaseDt)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;
            decimal _tempPPNForex = 0;
            try
            {
                CompanyConfiguration _compConfigAuto = new CompanyConfig().GetSingle(CompanyConfigure.FACodeAutoNumber);

                if (_compConfigAuto.SetValue == CompanyConfigureDataMapper.GetFACodeAutoNumber(FACodeAutoNumber.True))
                {
                    MsFAGroupSub _msFASubGroup = this.db.MsFAGroupSubs.Single(temp => temp.FASubGrpCode == _prmGLFAPurchaseDt.FASubGroup);
                    CompanyConfiguration _compConfigDigit = new CompanyConfig().GetSingle(CompanyConfigure.FACodeDigitNumber);

                    if (_prmGLFAPurchaseDt.FACode.Trim() == "")
                    {
                        _prmGLFAPurchaseDt.FACode = _msFASubGroup.CodeCounter + ((int)_msFASubGroup.LastCounterNo + 1).ToString().PadLeft(Convert.ToInt16(_compConfigDigit.SetValue), '0');

                        _msFASubGroup.LastCounterNo = (int)_msFASubGroup.LastCounterNo + 1;
                    }
                }

                GLFAPurchaseHd _glFAPurchaseHd = this.GetSingleFAPurchaseHd(_prmGLFAPurchaseDt.TransNmbr);
                _tempBaseForex = _glFAPurchaseHd.BaseForex = _glFAPurchaseHd.BaseForex + Convert.ToDecimal(_prmGLFAPurchaseDt.AmountForex);
                _tempPPNForex = _glFAPurchaseHd.PPNForex = (_tempBaseForex - _glFAPurchaseHd.DiscForex) * _glFAPurchaseHd.PPN / 100;
                _glFAPurchaseHd.TotalForex = _tempBaseForex - _glFAPurchaseHd.DiscForex + _tempPPNForex;

                this.db.GLFAPurchaseDts.InsertOnSubmit(_prmGLFAPurchaseDt);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFAPurchaseDt(GLFAPurchaseDt _prmGLFAPurchaseDt, decimal _prmAmountForexOriginal)
        {
            bool _result = false;
            decimal _tempBaseForex = 0;
            decimal _tempPPNForex = 0;
            decimal _tempDisconPercent = 0;
            Decimal _tempDisconForex = 0;

            try
            {
                GLFAPurchaseHd _glFAPurchaseHd = this.GetSingleFAPurchaseHd(_prmGLFAPurchaseDt.TransNmbr);

                _tempDisconPercent = Convert.ToDecimal((Convert.ToDecimal(_glFAPurchaseHd.DiscForex) / Convert.ToDecimal(_glFAPurchaseHd.BaseForex)) * 100); //percent discon awal

                _tempBaseForex = _glFAPurchaseHd.BaseForex - _prmAmountForexOriginal;

                _tempBaseForex = _glFAPurchaseHd.BaseForex = _tempBaseForex + Convert.ToDecimal(_prmGLFAPurchaseDt.AmountForex);

                _tempDisconForex = _glFAPurchaseHd.DiscForex = (_tempBaseForex * _tempDisconPercent) / 100;
                _tempPPNForex = _glFAPurchaseHd.PPNForex = (_tempBaseForex - _tempDisconForex) * _glFAPurchaseHd.PPN / 100;
                _glFAPurchaseHd.TotalForex = _tempBaseForex - _tempDisconForex + _tempPPNForex;

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

        ~FAPurchaseBL()
        {

        }
    }
}
