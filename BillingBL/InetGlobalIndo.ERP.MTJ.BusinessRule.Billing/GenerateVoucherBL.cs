using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed class GenerateVoucherBL : Base
    {
        private List<String> PINCollection = new List<String>();
        private List<String> SNCollection = new List<String>();

        public GenerateVoucherBL()
        {

        }

        public Double RowCountVoucherGenerator(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "ProductCode")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                           from _bilTrGenerateCard in this.db.BILTrGenerateCards
                           where (SqlMethods.Like(_bilTrGenerateCard.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like((_bilTrGenerateCard.ProductCode ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                           select _bilTrGenerateCard.TransNmbr
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<BILTrGenerateCard> GetListTrGenerateCard(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<BILTrGenerateCard> _result = new List<BILTrGenerateCard>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "ProductCode")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _bilTrGenerateCard in this.db.BILTrGenerateCards
                                where (SqlMethods.Like(_bilTrGenerateCard.TransNmbr.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like((_bilTrGenerateCard.ProductCode ?? "").Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _bilTrGenerateCard.TransDate descending
                                select _bilTrGenerateCard
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<MsProduct_SerialNumber> GetVoucherSerialNumberList(String _prmTransNmbr)
        {
            List<MsProduct_SerialNumber> _result = new List<MsProduct_SerialNumber>();
            try
            {
                var _qry = (
                        from _msproductSerial in this.db.MsProduct_SerialNumbers
                        where _msproductSerial.TransNmbr == _prmTransNmbr
                        orderby _msproductSerial.SerialNumber
                        select _msproductSerial
                    );
                foreach (var _row in _qry) {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public bool DeleteMultiBilTrGenerateCard(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    BILTrGenerateCard _BilTrGenerateCard = this.db.BILTrGenerateCards.Single(_temp => _temp.TransNmbr.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_BilTrGenerateCard != null)
                    {
                        this.db.BILTrGenerateCards.DeleteOnSubmit(_BilTrGenerateCard);
                        _result = true;
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

        public Boolean generateVoucher(String _prmSeries, String _prmProductCode, Boolean _prmUseAuth, DateTime _prmTransDate, DateTime _prmExpireDate, Double _prmCardValue, Int16 _prmProduceQty, String _prmRemark, Int16 _prmPINDigit)
        {
            Boolean _result = false;
            try
            {
                String _generatedTransNmbr = "";
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _transactionNumber.TempTransNmbr = _item.Number;
                    _generatedTransNmbr = _item.Number;
                }
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                var _query = (
                            from _temp in this.db.Temporary_TransactionNumbers
                            where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                            select _temp
                          );

                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                BILTrGenerateCard _bilTrGenerateCard = new BILTrGenerateCard();
                _bilTrGenerateCard.TransNmbr = _generatedTransNmbr;
                _bilTrGenerateCard.Series = _prmSeries;
                _bilTrGenerateCard.ProductCode = _prmProductCode;
                _bilTrGenerateCard.TransDate = _prmTransDate;
                _bilTrGenerateCard.ExpiredDate = _prmExpireDate;
                _bilTrGenerateCard.CardValue = Convert.ToDecimal(_prmCardValue);
                _bilTrGenerateCard.ProduceQty = _prmProduceQty;
                _bilTrGenerateCard.Remark = _prmRemark;

                this.db.BILTrGenerateCards.InsertOnSubmit(_bilTrGenerateCard);

                //this.db.spBIL_GenerateVoucher(_bilTrGenerateCard.TransNmbr, _prmProductCode,
                //    _prmUseAuth, ("000000"+ _prmExpireDate.ToString("ddMMyy")).Substring(("000000"+ _prmExpireDate.ToString("ddMMyy")).Length - 6 ), _prmPINDigit, _prmProduceQty);

                //////////////////////////
                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();                
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandTimeout = 300;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "spBIL_GenerateVoucher";
                _cmd.Parameters.AddWithValue("@TransNmbr", _bilTrGenerateCard.TransNmbr);
                _cmd.Parameters.AddWithValue("@ProductCode", _prmProductCode);
                _cmd.Parameters.AddWithValue("@PINAuthentication", _prmUseAuth);
                _cmd.Parameters.AddWithValue("@ExpirationDate", ("000000" + _prmExpireDate.ToString("ddMMyy")).Substring(("000000" + _prmExpireDate.ToString("ddMMyy")).Length - 6));
                _cmd.Parameters.AddWithValue("@PINDigit", _prmPINDigit);
                _cmd.Parameters.AddWithValue("@VoucherQty", _prmProduceQty);

                _conn.Open();
                _cmd.ExecuteNonQuery();
                _conn.Close();
                //////////////////////////

                //for (int i = 0; i < _prmProduceQty; i++) {
                //    MsProduct_SerialNumber _generatedVoucher = new MsProduct_SerialNumber();
                //    _generatedVoucher.SerialNumber = this.GenerateSerialNumber();
                //    this.SNCollection.Add(_generatedVoucher.SerialNumber);
                //    _generatedVoucher.TransNmbr = _bilTrGenerateCard.TransNmbr;
                //    _generatedVoucher.ProductCode = _prmProductCode;
                //    _generatedVoucher.PIN = this._generateNewPIN(_prmPINDigit);
                //    this.PINCollection.Add(_generatedVoucher.PIN);
                //    if (_prmUseAuth)
                //    {
                //        Random _rndGenerator = new Random();
                //        _generatedVoucher.PINAuthentication = _rndGenerator.Next(0,999999).ToString();
                //    }
                //    _generatedVoucher.ExpirationDate = _prmExpireDate.ToString("ddMMyy");
                //    this.db.MsProduct_SerialNumbers.InsertOnSubmit(_generatedVoucher);
                //}

                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        private String GenerateSerialNumber() {
            String _result = "";
            try
            {
                int _lastNumber = 1;
                while ((
                    (from _msProduct_SerialNumber in this.db.MsProduct_SerialNumbers where _msProduct_SerialNumber.SerialNumber == _lastNumber.ToString("000000") select _msProduct_SerialNumber).Count() > 0) 
                    || this.SNCollection.Exists ( delegate(String x) { return x == _lastNumber.ToString("000000"); } 
                    ))
                {
                    _lastNumber ++ ;
                }
                _result = _lastNumber.ToString("000000");
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        private Boolean CekPINAvailability( String _prmPIN ) {
            Boolean _result = true;
            try
            {
                _result = ( (from _msProduct_serialNumber in this.db.MsProduct_SerialNumbers where _msProduct_serialNumber.PIN == _prmPIN select _msProduct_serialNumber ).Count() > 0 );
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        private String _generateNewPIN(Int16 _prmDigit)
        {
            String _result = "";
            try
            {
                do
                {
                    _result = "";
                    Random _rndGenerator = new Random();
                    for (int i = 0; i < _prmDigit; i++)
                    {
                        _result += _rndGenerator.Next(0, 9).ToString();
                    }
                } while (this.CekPINAvailability(_result) || this.PINCollection.Exists(delegate(String x) {return x == _result ;} ));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        ~GenerateVoucherBL()
        {

        }
    }
}
