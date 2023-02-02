using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;

namespace BusinessRule.POS
{
    public sealed class POSDiscountBL : Base
    {
        public POSDiscountBL()
        {
        }

        #region Member

        public double RowsCount(string _prmCategory, string _prmKeyword, string _prmStartDate, string _prmEndDate)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Code")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "AmountType")
                _pattern2 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Status")
                _pattern3 = "%" + _prmKeyword + "%";

            var _query =
                        (
                             from _posMsDiscountConfigs in this.db.POSMsDiscountConfigs
                             where (SqlMethods.Like(_posMsDiscountConfigs.DiscountConfigCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_posMsDiscountConfigs.AmountType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && (SqlMethods.Like(_posMsDiscountConfigs.Status.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                && _posMsDiscountConfigs.StartDate >= Convert.ToDateTime(_prmStartDate)
                                && _posMsDiscountConfigs.EndDate <= Convert.ToDateTime(_prmEndDate)
                             select _posMsDiscountConfigs
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<POSMsDiscountConfig> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, string _prmStartDate, string _prmEndDate)
        {
            List<POSMsDiscountConfig> _result = new List<POSMsDiscountConfig>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Code")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "AmountType")
                _pattern2 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Status")
                _pattern3 = "%" + _prmKeyword + "%";

            try
            {
                var _query = (
                                from _posMsDiscountConfigs in this.db.POSMsDiscountConfigs
                                where (SqlMethods.Like(_posMsDiscountConfigs.DiscountConfigCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsDiscountConfigs.AmountType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsDiscountConfigs.Status.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                   && _posMsDiscountConfigs.StartDate >= Convert.ToDateTime(_prmStartDate)
                                   && _posMsDiscountConfigs.EndDate <= Convert.ToDateTime(_prmEndDate)
                                orderby _posMsDiscountConfigs.DiscountConfigCode descending
                                select _posMsDiscountConfigs
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

        public List<POSMsDiscountConfig> GetList()
        {
            List<POSMsDiscountConfig> _result = new List<POSMsDiscountConfig>();

            try
            {
                var _query = (
                                from _posMsDiscountConfigs in this.db.POSMsDiscountConfigs
                                orderby _posMsDiscountConfigs.DiscountConfigCode ascending
                                select _posMsDiscountConfigs
                            );

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

        public bool DeleteMulti(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    POSMsDiscountConfig _posMsDiscountConfigs = this.db.POSMsDiscountConfigs.Single(_temp => _temp.DiscountConfigCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.POSMsDiscountConfigs.DeleteOnSubmit(_posMsDiscountConfigs);
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

        public POSMsDiscountConfig GetSingle(string _prmCode)
        {
            POSMsDiscountConfig _result = null;

            try
            {
                _result = this.db.POSMsDiscountConfigs.Single(_temp => _temp.DiscountConfigCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetPOSDiscountConfigByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _posMsDiscountConfigs in this.db.POSMsDiscountConfigs
                                where _posMsDiscountConfigs.DiscountConfigCode == _prmCode
                                select new
                                {
                                    MemberTypeName = _posMsDiscountConfigs.AmountType
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.MemberTypeName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(POSMsDiscountConfig _prmPOSMsDiscountConfig)
        {
            bool _result = false;

            try
            {
                this.db.POSMsDiscountConfigs.InsertOnSubmit(_prmPOSMsDiscountConfig);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(POSMsDiscountConfig _prmPOSDiscountConfig)
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

        public bool CekToPOSMSDiscountConfigMember(String _prmCode)
        {
            bool _result = false;
            try
            {
                var _query = (from _posMSDiscountConfigMember in this.db.POSMsDiscountConfigMembers
                              where _posMSDiscountConfigMember.DiscountConfigCode == _prmCode
                              select _posMSDiscountConfigMember
                    ).Count();

                if (_query == 0)
                    _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        #endregion

        #region Detail

        public List<POSMsDiscountConfigMember> GetListPOSMsDiscountConfigMember(string _prmCode)
        {
            List<POSMsDiscountConfigMember> _result = new List<POSMsDiscountConfigMember>();

            try
            {
                var _query = (
                                from _posMsDiscountConfigMember in this.db.POSMsDiscountConfigMembers
                                where _posMsDiscountConfigMember.DiscountConfigCode == _prmCode
                                orderby _posMsDiscountConfigMember.MemberType ascending
                                select _posMsDiscountConfigMember
                            );

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

        public POSMsDiscountConfigMember GetSinglePOSMsDiscountConfigMember(string _prmCode, String _prmMemberType)
        {
            POSMsDiscountConfigMember _result = null;

            try
            {
                _result = this.db.POSMsDiscountConfigMembers.Single(_temp => _temp.DiscountConfigCode == _prmCode && _temp.MemberType == _prmMemberType);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPOSMsDiscountConfigMember(POSMsDiscountConfigMember _prmPOSMsDiscountConfigMember)
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

        public bool DeleteMultiPOSMsDiscountConfigMember(string[] _prmMemberType, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmMemberType.Length; i++)
                {
                    POSMsDiscountConfigMember _posMsDiscountConfigMember = this.db.POSMsDiscountConfigMembers.Single(_temp => _temp.MemberType == _prmMemberType[i] && _temp.DiscountConfigCode.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.POSMsDiscountConfigMembers.DeleteOnSubmit(_posMsDiscountConfigMember);
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

        public bool AddPOSMsDiscountConfigMember(POSMsDiscountConfigMember _prmPOSMsDiscountConfigMember)
        {
            bool _result = false;

            try
            {
                this.db.POSMsDiscountConfigMembers.InsertOnSubmit(_prmPOSMsDiscountConfigMember);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool CheckhAvailableDisc(String _prmDiscCode, String _prmMemberType)
        {
            bool _result = false;

            var _query = (
                            from _posMsDiscConfig in this.db.POSMsDiscountConfigs
                            where _posMsDiscConfig.DiscountConfigCode == _prmDiscCode
                            select new
                            {
                                StartDate = _posMsDiscConfig.StartDate,
                                EndDate = _posMsDiscConfig.EndDate,
                                PaymentType = _posMsDiscConfig.PaymentType
                            }
                            ).FirstOrDefault();

            DateTime Start = Convert.ToDateTime(_query.StartDate);
            DateTime End = Convert.ToDateTime(_query.EndDate);
            Byte Payment = Convert.ToByte(_query.PaymentType);

            var _query2 = (
                            from _posMsDiscConfig in this.db.POSMsDiscountConfigs

                            join _posMsDiscConfigMember in this.db.POSMsDiscountConfigMembers
                                on _posMsDiscConfig.DiscountConfigCode equals _posMsDiscConfigMember.DiscountConfigCode

                            where _posMsDiscConfig.DiscountConfigCode != _prmDiscCode &&
                            _posMsDiscConfig.PaymentType == Payment &&
                            (_posMsDiscConfig.StartDate <= Start ||
                            _posMsDiscConfig.StartDate <= End) &&
                            (_posMsDiscConfig.EndDate >= Start ||
                            _posMsDiscConfig.EndDate >= End) &&
                            _posMsDiscConfigMember.MemberType == _prmMemberType
                            select _posMsDiscConfig
                            ).Count();
            if (_query2 == 0)
            {
                _result = true;
            }
            else
            {
                _result = false;
            }
            return _result;
        }

        #endregion

        #region Detail2

        public List<POSMsDiscountProduct> GetListPOSMsDiscountProduct(string _prmCode)
        {
            List<POSMsDiscountProduct> _result = new List<POSMsDiscountProduct>();

            try
            {
                var _query = (
                                from _posMsDiscountProduct in this.db.POSMsDiscountProducts
                                where _posMsDiscountProduct.DiscountConfigCode == _prmCode
                                orderby _posMsDiscountProduct.ProductCode ascending
                                select _posMsDiscountProduct
                            );

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

        public POSMsDiscountProduct GetSinglePOSMsDiscountProduct(string _prmCode, String _prmProductCode)
        {
            POSMsDiscountProduct _result = null;

            try
            {
                _result = this.db.POSMsDiscountProducts.Single(_temp => _temp.DiscountConfigCode == _prmCode && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPOSMsDiscountProduct(POSMsDiscountProduct _prmPOSMsDiscountProduct)
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

        public bool DeleteMultiPOSMsDiscountProduct(string[] _prmProductCode, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmProductCode.Length; i++)
                {
                    POSMsDiscountProduct _posMsDiscountProduct = this.db.POSMsDiscountProducts.Single(_temp => _temp.ProductCode == _prmProductCode[i] && _temp.DiscountConfigCode.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.POSMsDiscountProducts.DeleteOnSubmit(_posMsDiscountProduct);
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

        public String AddPOSMsDiscountProduct(POSMsDiscountProduct _prmPOSMsDiscountProduct)
        {
            String _result = "";
            //String _discountConfigCode = "";

            try
            {
                //if (this.IsProductExist(_prmPOSMsDiscountProduct.ProductCode, ref _discountConfigCode))
                if (this.IsProductExist(_prmPOSMsDiscountProduct.ProductCode, _prmPOSMsDiscountProduct.DiscountConfigCode))
                {
                    //_result = ", Product already exist in discount " + _discountConfigCode;
                    _result = ", Product already exist";
                }
                else
                {
                    this.db.POSMsDiscountProducts.InsertOnSubmit(_prmPOSMsDiscountProduct);
                    this.db.SubmitChanges();

                    _result = "";
                }

            }
            catch (Exception ex)
            {
                _result = ", " + ex.Message;
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //private Boolean IsProductExist(String _prmProductCode, ref String _discountConfigCode)
        private Boolean IsProductExist(String _prmProductCode, String _prmDiscountConfigCode)
        {
            Boolean _result = false;

            var _query = (
                            from _discProduct in this.db.POSMsDiscountProducts
                            where _discProduct.ProductCode == _prmProductCode
                            && _discProduct.DiscountConfigCode == _prmDiscountConfigCode
                            select _discProduct.DiscountConfigCode
                         ).Count();

            if (_query > 0)
            {
                _result = true;

                //var _query2 = (
                //                from _discProduct2 in this.db.POSMsDiscountProducts
                //                where _discProduct2.ProductCode == _prmProductCode
                //                select _discProduct2.DiscountConfigCode
                //              ).FirstOrDefault();

                //_discountConfigCode = _query2;
            }

            return _result;
        }

        #endregion

        public POSMsDiscountConfig GetDiscountByProductCode(String _prmProductCode)
        {
            POSMsDiscountConfig _result = null;

            var _query = (
                            from _discProduct in this.db.POSMsDiscountProducts
                            where _discProduct.ProductCode == _prmProductCode
                            select _discProduct.DiscountConfigCode
                         );

            if (_query.Count() > 0)
            {
                var _queryDiscount = (
                                        from _disc in this.db.POSMsDiscountConfigs
                                        where _disc.DiscountConfigCode == _query.FirstOrDefault()
                                        select _disc
                                     ).FirstOrDefault();

                _result = _queryDiscount;
            }

            return _result;
        }

        public List<String> CheckDateDisc(Byte _prmPaymentType, DateTime _prmStartDate, DateTime _prmEndDate)
        {
            List<String> _result = new List<String>();


            var _query = (
                            from _msDisc in this.db.POSMsDiscountConfigs
                            where _msDisc.PaymentType == _prmPaymentType &&
                            (_msDisc.StartDate >= _prmStartDate ||
                            _msDisc.StartDate <= _prmEndDate) &&
                            (_msDisc.EndDate >= _prmStartDate ||
                            _msDisc.EndDate >= _prmEndDate)
                            select _msDisc.DiscountConfigCode
                            );

            foreach (var _row in _query)
            {
                _result.Add(_row);
            }

            return _result;
        }

        ~POSDiscountBL()
        {
        }
    }
}
