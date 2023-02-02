using System;
using System.Web;
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
using MySql.Data.MySqlClient;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed class RadiusUpdateVoucherBL : Base
    {
        public RadiusUpdateVoucherBL()
        {
        }

        #region BILTrRadiusUpdateVoucher

        public int RowsCountRadiusUpdateVoucher(string _prmCategory, string _prmKeyword)
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
                               from _radUpdateVoucher in this.db.BILTrRadiusUpdateVouchers
                               where (SqlMethods.Like(_radUpdateVoucher.TransNmbr.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_radUpdateVoucher.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                               select _radUpdateVoucher.TransNmbr
                           ).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<BILTrRadiusUpdateVoucher> GetListRadiusUpdateVoucher(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<BILTrRadiusUpdateVoucher> _result = new List<BILTrRadiusUpdateVoucher>();

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
                                from _radUpdateVoucher in this.db.BILTrRadiusUpdateVouchers
                                where (SqlMethods.Like(_radUpdateVoucher.TransNmbr.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like((_radUpdateVoucher.FileNmbr ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _radUpdateVoucher.TransDate descending
                                select new
                                {
                                    TransNmbr = _radUpdateVoucher.TransNmbr,
                                    FileNmbr = _radUpdateVoucher.FileNmbr,
                                    Status = _radUpdateVoucher.Status,
                                    TransDate = _radUpdateVoucher.TransDate,
                                    RadiusCode = _radUpdateVoucher.RadiusCode,
                                    ExpiredDate = (_radUpdateVoucher.ExpiredDate == null) ? "" : _radUpdateVoucher.ExpiredDate.ToString(),
                                    Series = _radUpdateVoucher.Series,
                                    SerialNoFrom = _radUpdateVoucher.SerialNoFrom,
                                    SerialNoTo = _radUpdateVoucher.SerialNoTo,
                                    SellingAmount = (_radUpdateVoucher.SellingAmount == null) ? 0 : Convert.ToInt32(_radUpdateVoucher.SellingAmount),
                                    AssociatedService = _radUpdateVoucher.AssociatedService,
                                    ExpireTime = (_radUpdateVoucher.ExpireTime == null) ? 0 : Convert.ToInt32(_radUpdateVoucher.ExpireTime),
                                    ExpireTimeUnit = (_radUpdateVoucher.ExpireTimeUnit == null) ? 0 : Convert.ToInt32(_radUpdateVoucher.ExpireTimeUnit)
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new BILTrRadiusUpdateVoucher(_row.TransNmbr, _row.FileNmbr, _row.Status, _row.TransDate, _row.RadiusCode, _row.ExpiredDate, _row.Series, _row.SerialNoFrom, _row.SerialNoTo, _row.SellingAmount, _row.AssociatedService, _row.ExpireTime, _row.ExpireTimeUnit));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public BILTrRadiusUpdateVoucher GetSingleRadiusUpdateVoucher(String _prmTransNmbr)
        {
            BILTrRadiusUpdateVoucher _result = null;

            try
            {
                _result = this.db.BILTrRadiusUpdateVouchers.Single(_temp => _temp.TransNmbr == _prmTransNmbr);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String AddRadiusUpdateVoucher(BILTrRadiusUpdateVoucher _prmRadiusUpdateVoucher)
        {
            String _result = "";

            try
            {
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _prmRadiusUpdateVoucher.TransNmbr = item.Number;
                    _transactionNumber.TempTransNmbr = item.Number;
                }

                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);
                this.db.BILTrRadiusUpdateVouchers.InsertOnSubmit(_prmRadiusUpdateVoucher);

                var _query = (
                               from _temp in this.db.Temporary_TransactionNumbers
                               where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                               select _temp
                             );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                this.db.SubmitChanges();

                _result = _prmRadiusUpdateVoucher.TransNmbr;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditRadiusUpdateVoucher(BILTrRadiusUpdateVoucher _prmRadiusUpdateVoucher)
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

        public bool DeleteMultiRadiusUpdateVoucher(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    BILTrRadiusUpdateVoucher _prmRadiusUpdateVoucher = this.db.BILTrRadiusUpdateVouchers.Single(_temp => _temp.TransNmbr == _prmCode[i]);

                    if (_prmRadiusUpdateVoucher != null)
                    {
                        if ((_prmRadiusUpdateVoucher.FileNmbr ?? "").Trim() == "")
                        {
                            this.db.BILTrRadiusUpdateVouchers.DeleteOnSubmit(_prmRadiusUpdateVoucher);

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
                this.db.spBIL_RadiusUpdateVoucherGetAppr(_prmCode, _prmuser, ref _result);

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
                    this.db.spBIL_RadiusUpdateVoucherApprove(_prmCode, _prmuser, ref _result);

                    if (_result == "")
                    {
                        BILTrRadiusUpdateVoucher _radUpdateVoucher = this.GetSingleRadiusUpdateVoucher(_prmCode);

                        foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(_radUpdateVoucher.TransDate.Year, _radUpdateVoucher.TransDate.Month, AppModule.GetValue(TransactionType.RadiusUpdateVoucher), this._companyTag, ""))
                        {
                            _radUpdateVoucher.FileNmbr = item.Number;
                        }

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
                BILTrRadiusUpdateVoucher _radUpdateVoucher = this.db.BILTrRadiusUpdateVouchers.Single(_temp => _temp.TransNmbr == _prmCode);
                MySqlConnection conn = new MySqlConnection(this.GetDatabaseConnString(_radUpdateVoucher.RadiusCode));

                String _serialNo = "";
                String _serialNoStart = "";
                int _countSerial = 0;

                if (Convert.ToInt32(_radUpdateVoucher.SerialNoTo) >= Convert.ToInt32(_radUpdateVoucher.SerialNoFrom))
                {
                    _countSerial = Convert.ToInt32(_radUpdateVoucher.SerialNoTo) - Convert.ToInt32(_radUpdateVoucher.SerialNoFrom) + 1;
                    _serialNo = _radUpdateVoucher.SerialNoFrom;
                    _serialNoStart = _radUpdateVoucher.SerialNoFrom;
                }
                else
                {
                    _countSerial = Convert.ToInt32(_radUpdateVoucher.SerialNoFrom) - Convert.ToInt32(_radUpdateVoucher.SerialNoTo) + 1;
                    _serialNo = _radUpdateVoucher.SerialNoTo;
                    _serialNoStart = _radUpdateVoucher.SerialNoTo;
                }

                int _serialLen = _serialNo.Length;

                ProductSerialNumberBL _prodSNBL = new ProductSerialNumberBL();
                for (int i = 0; i < _countSerial; i++)
                {
                    MsProduct_SerialNumber _msProdSerialNo = new ProductSerialNumberBL().GetSingleProductSerialNumber(_serialNo);

                    if (_msProdSerialNo.IsSold == false)
                    {
                        _result = this.UpdateVoucher(conn, Convert.ToInt32(_serialNo), _msProdSerialNo.PIN, Convert.ToInt32(_radUpdateVoucher.AssociatedService), (_radUpdateVoucher.SellingAmount == null) ? 0 : Convert.ToDecimal(_radUpdateVoucher.SellingAmount), Convert.ToInt32(_radUpdateVoucher.ExpireTime), Convert.ToInt32(_radUpdateVoucher.ExpireTimeUnit), (DateTime)_radUpdateVoucher.ExpiredDate, _radUpdateVoucher.Series);
                    }
                    int _nextSerial = Convert.ToInt32(_serialNo) + 1;
                    _serialNo = _nextSerial.ToString().PadLeft(_serialLen, '0');

                    if (_result != "") break;
                }

                if (_result == "")
                {
                    this.db.spBIL_RadiusUpdateVoucherPosting(_prmCode, _prmuser, _serialNoStart, _countSerial, ref _result);
                    //for (int i = 0; i <= _countSerial; i++)
                    //{
                    //    MsProduct_SerialNumber _msProductSerialNumber = _prodSNBL.GetSingleProductSerialNumber(_serialNo);
                    //    _msProductSerialNumber.IsSold = true;
                    //    int _nextSerial = Convert.ToInt32(_serialNo) + 1;
                    //    _serialNo = _nextSerial.ToString().PadLeft(_serialLen, '0');
                    //}
                    this.db.SubmitChanges();
                }

                if (_result == "")
                {
                    _result = "Posting Success";
                }
            }
            catch (Exception ex)
            {
                _result = "Posting Failed" + " ," + _result;
                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "Posting RadiusUpdateVoucherBL", "Billing");
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

                BILTrRadiusUpdateVoucher _radUpdateVoucher = this.db.BILTrRadiusUpdateVouchers.Single(_temp => _temp.TransNmbr == _prmCode);
                String _locked = _transCloseBL.IsExistAndLocked(_radUpdateVoucher.TransDate);
                if (_locked == "")
                {
                    //this.db.spBIL_RadiusUpdateVoucherUnPost(_prmCode, 0, 0, _prmuser, ref _result);

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

        public String GetDatabaseConnString(String _prmRadiusCode)
        {
            String _result = "";

            var _query = (
                            from _radius in this.db.BILMsRadius
                            where _radius.RadiusCode == _prmRadiusCode
                            select _radius
                         ).FirstOrDefault();

            _result = "Server=" + _query.RadiusIP + ";Port=3306;" + "Database=" + _query.RadiusDbName + "; Uid=" + _query.RadiusUserName + "; Pwd=" + Rijndael.Decrypt(_query.RadiusPwd, ApplicationConfig.PasswordEncryptionKey) + ";";

            return _result;
        }

        public String UpdateVoucher(MySqlConnection _prmConn, int _prmID, String _prmPIN, int _prmSrvID, Decimal _prmValue, int _prmExpireTime, int _prmExpireTimeUnit, DateTime _prmExpiredDate, String _prmSeries)
        {
            String _result = "";
            MySqlCommand cmd = _prmConn.CreateCommand();
            DateTime _now = DateTime.Now;

            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 3000;
                cmd.CommandText = "updatevoucher";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("_id", MySqlDbType.Int64);
                cmd.Parameters["_id"].Value = _prmID;
                cmd.Parameters.Add("_pin", MySqlDbType.VarChar, 20);
                cmd.Parameters["_pin"].Value = _prmPIN;
                cmd.Parameters.Add("_srvid", MySqlDbType.Int32);
                cmd.Parameters["_srvid"].Value = _prmSrvID;
                cmd.Parameters.Add("_value", MySqlDbType.Decimal, 20);
                cmd.Parameters["_value"].Value = _prmValue;
                cmd.Parameters.Add("_expiretime", MySqlDbType.Int32);
                cmd.Parameters["_expiretime"].Value = _prmExpireTime;
                cmd.Parameters.Add("_expiretimeunit", MySqlDbType.Int32);
                cmd.Parameters["_expiretimeunit"].Value = _prmExpireTimeUnit;
                cmd.Parameters.Add("_expiredDate", MySqlDbType.DateTime);
                cmd.Parameters["_expiredDate"].Value = _prmExpiredDate;
                cmd.Parameters.Add("_series", MySqlDbType.VarChar, 16);
                cmd.Parameters["_series"].Value = _prmSeries;
                cmd.Parameters.Add("_dateNow", MySqlDbType.DateTime);
                cmd.Parameters["_dateNow"].Value = _now;
                cmd.Parameters.Add("_userName", MySqlDbType.VarChar, 64);
                cmd.Parameters["_userName"].Value = HttpContext.Current.User.Identity.Name;

                if (_prmConn.State == System.Data.ConnectionState.Open)
                {
                    _prmConn.Close();
                }

                if (_prmConn.State != System.Data.ConnectionState.Open)
                {
                    _prmConn.Open();
                }

                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "UpdateVoucher", "Billing");
                _result = "Connection To Radius Failed.";
            }

            return _result;
        }

        //public List<BILTrRadiusUpdateVoucher> GetListForDDLRadiusUpdateVoucherByCustAndProduct(String _prmCustCode, String _prmProductCode)
        //{
        //    List<BILTrRadiusUpdateVoucher> _result = new List<BILTrRadiusUpdateVoucher>();

        //    try
        //    {
        //        var _query = (
        //                        from _bilTrRadiusUpdateVoucher in this.db.BILTrRadiusUpdateVouchers
        //                        join _biltrRadiusUpdateVoucherDt in this.db.BILTrRadiusUpdateVoucherDts
        //                        on _bilTrRadiusUpdateVoucher.TransNmbr equals _biltrRadiusUpdateVoucherDt.TransNmbr
        //                        join _bilTrContract in this.db.BILTrContracts
        //                        on _bilTrRadiusUpdateVoucher.TransNmbr equals _bilTrContract.RadiusUpdateVoucherNoRef
        //                        join _bilTrBeritaAcara in this.db.BILTrBeritaAcaras
        //                        on _bilTrRadiusUpdateVoucher.TransNmbr equals _bilTrBeritaAcara.RadiusUpdateVoucherNoRef
        //                        where _bilTrRadiusUpdateVoucher.CustCode == _prmCustCode
        //                            && _biltrRadiusUpdateVoucherDt.ProductCode == _prmProductCode
        //                            && _bilTrRadiusUpdateVoucher.Status == RadiusUpdateVoucherDataMapper.GetStatus(RadiusUpdateVoucherStatus.Posting)
        //                            && _bilTrContract.Status == ContractDataMapper.GetStatus(TransStatus.Approved)
        //                            && _bilTrBeritaAcara.Status == BeritaAcaraDataMapper.GetStatus(TransStatus.Posted)
        //                        orderby _bilTrRadiusUpdateVoucher.TransNmbr
        //                        select new
        //                        {
        //                            TransNmbr = _bilTrRadiusUpdateVoucher.TransNmbr,
        //                            FileNmbr = _bilTrRadiusUpdateVoucher.FileNmbr
        //                        }
        //                    ).Distinct();

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new BILTrRadiusUpdateVoucher(_row.TransNmbr, _row.FileNmbr));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        #endregion

        ~RadiusUpdateVoucherBL()
        {
        }
    }
}
