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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class FINGiroInBL : Base
    {
        public FINGiroInBL()
        {
        }


        #region FINGiroIn
        public double RowsCountFINGiroIn(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                            from _finGiroIn in this.db.FINGiroIns
                            where (SqlMethods.Like(_finGiroIn.GiroNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_finGiroIn.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _finGiroIn.GiroNo
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINGiroIn> GetListFINGiroIn(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINGiroIn> _result = new List<FINGiroIn>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "FileNo")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _finGiroIn in this.db.FINGiroIns
                                where (SqlMethods.Like(_finGiroIn.GiroNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_finGiroIn.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _finGiroIn.UserDate descending
                                select new
                                {
                                    GiroNo = _finGiroIn.GiroNo,
                                    FileNmbr = _finGiroIn.FileNmbr,
                                    Status = _finGiroIn.Status,
                                    ReceiptNo = _finGiroIn.ReceiptNo,
                                    ReceiptDate = _finGiroIn.ReceiptDate,
                                    DueDate = _finGiroIn.DueDate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINGiroIn(_row.GiroNo, _row.FileNmbr, _row.Status, _row.ReceiptNo, _row.ReceiptDate, _row.DueDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINGiroIn> GetListForDDL(string _prmCode)
        {
            List<FINGiroIn> _result = new List<FINGiroIn>();

            try
            {
                var _query = (
                                from _finGiroIn in this.db.FINGiroIns
                                where (_finGiroIn.SuppCode == _prmCode || _finGiroIn.CustCode == _prmCode) &&
                                        (_finGiroIn.Status == GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.OnHold) || _finGiroIn.Status == GiroReceiptDataMapper.GetStatus(GiroReceiptStatus.Cancelled))
                                orderby _finGiroIn.GiroNo ascending
                                select new
                                {
                                    GiroNo = _finGiroIn.GiroNo
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINGiroIn(_row.GiroNo));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINGiroIn GetSingleFINGiroIn(string _prmCode)
        {
            FINGiroIn _result = null;

            try
            {
                _result = this.db.FINGiroIns.Single(_temp => _temp.GiroNo == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiFINGiroIn(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    FINGiroIn _finGiroIn = this.db.FINGiroIns.Single(_temp => _temp.GiroNo.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.FINGiroIns.DeleteOnSubmit(_finGiroIn);
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

        public bool AddFINGiroIn(FINGiroIn _prmFINGiroIn)
        {
            bool _result = false;

            try
            {
                this.db.FINGiroIns.InsertOnSubmit(_prmFINGiroIn);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditFINGiroIn(FINGiroIn _prmFINGiroIn)
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

        public bool Setor(string _prmGiroNo, DateTime _prmDateSetor, string _prmReceiptType, string _prmBankSetor, decimal _prmBankExpense, int _prmPeriod, string _prmuser)
        {
            bool _result = false;
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_FNGiroInSetor(_prmGiroNo, _prmDateSetor, _prmReceiptType, _prmBankSetor, _prmBankExpense, _prmuser, ref _errorMsg);

                if (_success == 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public bool Drawn(string _prmGiroNo, DateTime _prmDrawnDate, string _prmDrawnRemark, string _prmReceiptType, decimal _prmBankExpense, string _prmuser)
        {
            bool _result = false;
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_FNGiroInDrawn(_prmGiroNo, _prmDrawnDate, _prmDrawnRemark, _prmReceiptType, _prmBankExpense, _prmuser, ref _errorMsg);

                if (_success == 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public bool Cancel(string _prmGiroNo, DateTime _prmTolakDate, string _prmTolakReason, string _prmuser)
        {
            bool _result = false;
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_FNGiroInCancel(_prmGiroNo, _prmTolakDate, _prmTolakReason, _prmuser, ref _errorMsg);

                if (_success == 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }

        public bool Unposting(string _prmGiroNo, int _prmYear, int _prmPeriod, string _prmuser)
        {
            bool _result = false;
            string _errorMsg = "";

            try
            {
                int _success = this.db.S_FNGiroInUnPost(_prmGiroNo, _prmYear, _prmPeriod, _prmuser, ref _errorMsg);

                if (_success == 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error, _errorMsg);
            }

            return _result;
        }
        #endregion

        ~FINGiroInBL()
        {
        }
    }
}
