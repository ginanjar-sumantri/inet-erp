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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Finance
{
    public sealed class FINGiroOutBL :Base
    {
        public FINGiroOutBL()
        {
        }

        #region FINGiroOut
        public double RowsCountFINGiroOut(string _prmCategory, string _prmKeyword)
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
                            from _finGiroOut in this.db.FINGiroOuts
                            where (SqlMethods.Like(_finGiroOut.GiroNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            && (SqlMethods.Like((_finGiroOut.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _finGiroOut.GiroNo
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<FINGiroOut> GetListFINGiroOut(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<FINGiroOut> _result = new List<FINGiroOut>();

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
                                from _finGiroOut in this.db.FINGiroOuts
                                where (SqlMethods.Like(_finGiroOut.GiroNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_finGiroOut.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _finGiroOut.UserDate descending
                                select new
                                {
                                    GiroNo = _finGiroOut.GiroNo,
                                    FileNmbr = _finGiroOut.FileNmbr,
                                    Status = _finGiroOut.Status,
                                    PaymentNo = _finGiroOut.PaymentNo,
                                    PaymentDate = _finGiroOut.PaymentDate,
                                    DueDate = _finGiroOut.DueDate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new FINGiroOut(_row.GiroNo, _row.FileNmbr, _row.Status, _row.PaymentNo, _row.PaymentDate, _row.DueDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<FINGiroOut> GetListForDDL(string _prmCode)
        {
            List<FINGiroOut> _result = new List<FINGiroOut>();

            try
            {
                var _query = (
                                from _finGiroOut in this.db.FINGiroOuts
                                where (_finGiroOut.SuppCode == _prmCode || _finGiroOut.CustCode == _prmCode) &&
                                        (_finGiroOut.Status == GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.OnHold) || _finGiroOut.Status == GiroPaymentDataMapper.GetStatus(GiroPaymentStatus.Cancelled))
                                orderby _finGiroOut.GiroNo ascending
                                select new
                                {
                                    GiroNo = _finGiroOut.GiroNo
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new FINGiroOut(_row.GiroNo));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public FINGiroOut GetSingleFINGiroOut(string _prmCode)
        {
            FINGiroOut _result = null;

            try
            {
                _result = this.db.FINGiroOuts.Single(_temp => _temp.GiroNo == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string Drawn(string _prmGiroNo, DateTime _prmDrawnDate, string _prmDrawnRemark, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_FNGiroOutDrawn(_prmGiroNo, _prmDrawnDate, _prmDrawnRemark, _prmuser, ref _result);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
                _result = "Your Failed Drawn Giro";
            }

            return _result;
        }

        public string Cancel(string _prmGiroNo, DateTime _prmTolakDate, string _prmTolakReason, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.S_FNGiroOutCancel(_prmGiroNo, _prmTolakDate, _prmTolakReason, _prmuser, ref _result);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
                _result = "Your Failed Cancel Giro";
            }

            return _result;
        }

        public string Unposting(string _prmGiroNo, int _prmYear, int _prmPeriod, string _prmuser)
        {
            string _result = "";

            try
            {
               this.db.S_FNGiroOutUnPost(_prmGiroNo, _prmYear, _prmPeriod, _prmuser, ref _result);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
                _result = "Your Failed Unposting Giro";
            }

            return _result;
        }

        #endregion

        ~FINGiroOutBL()
        {

        }
    }
}
