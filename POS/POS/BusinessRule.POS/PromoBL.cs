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
    public sealed class PromoBL : Base
    {
        public PromoBL()
        {
        }

        #region Member

        public double RowsCount(string _prmCategory, string _prmKeyword, string _prmStartDate, string _prmEndDate)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Status")
                _pattern2 = "%" + _prmKeyword + "%";

            var _query =
                        (
                             from _posMsPromos in this.db.POSMsPromos
                             where (SqlMethods.Like(_posMsPromos.PromoCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_posMsPromos.Status.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _posMsPromos.StartDate >= Convert.ToDateTime(_prmStartDate)
                                && _posMsPromos.EndDate <= Convert.ToDateTime(_prmEndDate)
                             select _posMsPromos
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<POSMsPromo> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, string _prmStartDate, string _prmEndDate)
        {
            List<POSMsPromo> _result = new List<POSMsPromo>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Status")
                _pattern2 = "%" + _prmKeyword + "%";

            try
            {
                var _query = (
                                from _posMsPromos in this.db.POSMsPromos
                                where (SqlMethods.Like(_posMsPromos.PromoCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsPromos.Status.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && _posMsPromos.StartDate >= Convert.ToDateTime(_prmStartDate)
                                   && _posMsPromos.EndDate <= Convert.ToDateTime(_prmEndDate)
                                orderby _posMsPromos.PromoCode descending
                                select _posMsPromos
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

        public List<POSMsPromo> GetList()
        {
            List<POSMsPromo> _result = new List<POSMsPromo>();

            try
            {
                var _query = (
                                from _posMsPromos in this.db.POSMsPromos
                                orderby _posMsPromos.PromoCode ascending
                                select _posMsPromos
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
                    POSMsPromo _posMsPromos = this.db.POSMsPromos.Single(_temp => _temp.PromoCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.POSMsPromos.DeleteOnSubmit(_posMsPromos);
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

        public POSMsPromo GetSingle(string _prmCode)
        {
            POSMsPromo _result = null;

            try
            {
                _result = this.db.POSMsPromos.Single(_temp => _temp.PromoCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public string GetPOSPromoByCode(string _prmCode)
        //{
        //    string _result = "";

        //    try
        //    {
        //        var _query = (
        //                        from _posMsPromos in this.db.POSMsPromos
        //                        where _posMsPromos.PromoCode == _prmCode
        //                        select new
        //                        {
        //                            MemberTypeName = _posMsPromos.AmountType
        //                        }
        //                      );

        //        foreach (var _obj in _query)
        //        {
        //            _result = _obj.MemberTypeName;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public bool Add(POSMsPromo _prmPOSMsPromo)
        {
            bool _result = false;

            try
            {
                this.db.POSMsPromos.InsertOnSubmit(_prmPOSMsPromo);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(POSMsPromo _prmPOSPromoConfig)
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

        //public bool CekToPOSMsPromoMember(String _prmCode)
        //{
        //    bool _result = false;
        //    try
        //    {
        //        var _query = (from _posMsPromoMember in this.db.POSMsPromoMembers
        //                      where _posMsPromoMember.PromoCode == _prmCode
        //                      select _posMsPromoMember
        //            ).Count();

        //        if (_query == 0)
        //            _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return _result;
        //}

        #endregion

        #region Detail

        public List<POSMsPromoMember> GetListPOSMsPromoMember(string _prmCode)
        {
            List<POSMsPromoMember> _result = new List<POSMsPromoMember>();

            try
            {
                var _query = (
                                from _POSMsPromoMember in this.db.POSMsPromoMembers
                                where _POSMsPromoMember.PromoCode == _prmCode
                                orderby _POSMsPromoMember.MemberType ascending
                                select _POSMsPromoMember
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

        public POSMsPromoMember GetSinglePOSMsPromoMember(string _prmCode, String _prmMemberType)
        {
            POSMsPromoMember _result = null;

            try
            {
                _result = this.db.POSMsPromoMembers.FirstOrDefault(_temp => _temp.PromoCode == _prmCode && _temp.MemberType == _prmMemberType);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPOSMsPromoMember(POSMsPromoMember _prmPOSMsPromoMember)
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

        public bool DeleteMultiPOSMsPromoMember(string[] _prmMemberType, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmMemberType.Length; i++)
                {
                    POSMsPromoMember _POSMsPromoMember = this.db.POSMsPromoMembers.Single(_temp => _temp.MemberType == _prmMemberType[i] && _temp.PromoCode.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.POSMsPromoMembers.DeleteOnSubmit(_POSMsPromoMember);
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

        public bool AddPOSMsPromoMember(POSMsPromoMember _prmPOSMsPromoMember)
        {
            bool _result = false;

            try
            {
                this.db.POSMsPromoMembers.InsertOnSubmit(_prmPOSMsPromoMember);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool CheckAvailable(String _prmPromoCode, String _prmMemberType)
        {
            bool _result = false;

            var _query = (
                            from _posMsPromo in this.db.POSMsPromos
                            where _posMsPromo.PromoCode == _prmPromoCode
                            select new
                            {
                                StartDate = _posMsPromo.StartDate,
                                EndDate = _posMsPromo.EndDate,
                                PaymentType = _posMsPromo.PaymentType
                            }
                            ).FirstOrDefault();

            DateTime Start = Convert.ToDateTime(_query.StartDate);
            DateTime End = Convert.ToDateTime(_query.EndDate);
            Byte Payment = Convert.ToByte(_query.PaymentType);

            var _query2 = (
                            from _posMsPromo in this.db.POSMsPromos
                            join _posMsPromoMember in this.db.POSMsPromoMembers
                                on _posMsPromo.PromoCode equals _posMsPromoMember.PromoCode

                            where _posMsPromo.PromoCode != _prmPromoCode &&
                            _posMsPromo.PaymentType == Payment &&
                            (_posMsPromo.StartDate <= Start ||
                            _posMsPromo.StartDate <= End) &&
                            (_posMsPromo.EndDate >= Start ||
                            _posMsPromo.EndDate >= End) &&
                            _posMsPromoMember.MemberType == _prmMemberType &&
                            _posMsPromo.Status == 3
                            select _posMsPromo
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

        public List<POSMsPromoProduct> GetListPOSMsPromoProduct(string _prmCode)
        {
            List<POSMsPromoProduct> _result = new List<POSMsPromoProduct>();

            try
            {
                var _query = (
                                from _posMsPromoProduct in this.db.POSMsPromoProducts
                                where _posMsPromoProduct.PromoCode == _prmCode
                                orderby _posMsPromoProduct.ProductCode ascending
                                select _posMsPromoProduct
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

        public POSMsPromoProduct GetSinglePOSMsPromoProduct(string _prmCode, String _prmProductCode)
        {
            POSMsPromoProduct _result = null;

            try
            {
                _result = this.db.POSMsPromoProducts.Single(_temp => _temp.PromoCode == _prmCode && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPOSMsPromoProduct(POSMsPromoProduct _prmPOSMsPromoProduct)
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

        public bool DeleteMultiPOSMsPromoProduct(string[] _prmProductCode, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmProductCode.Length; i++)
                {
                    POSMsPromoProduct _posMsPromoProduct = this.db.POSMsPromoProducts.FirstOrDefault(_temp => _temp.ProductCode == _prmProductCode[i] && _temp.PromoCode.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.POSMsPromoProducts.DeleteOnSubmit(_posMsPromoProduct);
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

        public String AddPOSMsPromoProduct(POSMsPromoProduct _prmPOSMsPromoProduct)
        {
            String _result = "";
            
            try
            {
                if (this.IsProductExist(_prmPOSMsPromoProduct.ProductCode, _prmPOSMsPromoProduct.PromoCode))
                {
                    _result = ", Product already exist";
                }
                else
                {
                    this.db.POSMsPromoProducts.InsertOnSubmit(_prmPOSMsPromoProduct);
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

        private Boolean IsProductExist(String _prmProductCode, String _prmPromoCode)
        {
            Boolean _result = false;

            var _query = (
                            from _promoProduct in this.db.POSMsPromoProducts
                            where _promoProduct.ProductCode == _prmProductCode
                            && _promoProduct.PromoCode == _prmPromoCode
                            select _promoProduct.PromoCode
                         ).Count();

            if (_query > 0)
            {
                _result = true;
            }

            return _result;
        }

        #endregion

        #region Detail3

        public List<POSMsPromoFreeProduct> GetListPOSMsPromoFreeProduct(string _prmCode)
        {
            List<POSMsPromoFreeProduct> _result = new List<POSMsPromoFreeProduct>();

            try
            {
                var _query = (
                                from _posMsPromoFreeProduct in this.db.POSMsPromoFreeProducts
                                where _posMsPromoFreeProduct.PromoCode == _prmCode
                                orderby _posMsPromoFreeProduct.FreeProductCode ascending
                                select _posMsPromoFreeProduct
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

        public POSMsPromoFreeProduct GetSinglePOSMsPromoFreeProduct(string _prmCode, String _prmFreeProductCode)
        {
            POSMsPromoFreeProduct _result = null;

            try
            {
                _result = this.db.POSMsPromoFreeProducts.Single(_temp => _temp.PromoCode == _prmCode && _temp.FreeProductCode == _prmFreeProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditPOSMsPromoFreeProduct(POSMsPromoFreeProduct _prmPOSMsPromoFreeProduct)
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

        public bool DeleteMultiPOSMsPromoFreeProduct(string[] _prmFreeProductCode, string _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmFreeProductCode.Length; i++)
                {
                    POSMsPromoFreeProduct _posMsPromoFreeProduct = this.db.POSMsPromoFreeProducts.FirstOrDefault(_temp => _temp.FreeProductCode == _prmFreeProductCode[i] && _temp.PromoCode.Trim().ToLower() == _prmCode.Trim().ToLower());

                    this.db.POSMsPromoFreeProducts.DeleteOnSubmit(_posMsPromoFreeProduct);
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

        public String AddPOSMsPromoFreeProduct(POSMsPromoFreeProduct _prmPOSMsPromoFreeProduct)
        {
            String _result = "";

            try
            {
                if (this.IsFreeProductExist(_prmPOSMsPromoFreeProduct.FreeProductCode, _prmPOSMsPromoFreeProduct.PromoCode))
                {
                    _result = ", FreeProduct already exist";
                }
                else
                {
                    this.db.POSMsPromoFreeProducts.InsertOnSubmit(_prmPOSMsPromoFreeProduct);
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

        private Boolean IsFreeProductExist(String _prmFreeProductCode, String _prmPromoCode)
        {
            Boolean _result = false;

            var _query = (
                            from _promoFreeProduct in this.db.POSMsPromoFreeProducts
                            where _promoFreeProduct.FreeProductCode == _prmFreeProductCode
                            && _promoFreeProduct.PromoCode == _prmPromoCode
                            select _promoFreeProduct.PromoCode
                         ).Count();

            if (_query > 0)
            {
                _result = true;
            }

            return _result;
        }

        #endregion

        public POSMsPromo GetPromoByProductCode(String _prmProductCode)
        {
            POSMsPromo _result = null;

            var _query = (
                            from _promoProduct in this.db.POSMsPromoProducts
                            where _promoProduct.ProductCode == _prmProductCode
                            select _promoProduct.PromoCode
                         );

            if (_query.Count() > 0)
            {
                var _queryPromo = (
                                        from _promo in this.db.POSMsPromos
                                        where _promo.PromoCode == _query.FirstOrDefault()
                                        select _promo
                                     ).FirstOrDefault();

                _result = _queryPromo;
            }

            return _result;
        }

        public List<String> CheckDatePromo(Byte _prmPaymentType, DateTime _prmStartDate, DateTime _prmEndDate)
        {
            List<String> _result = new List<String>();


            var _query = (
                            from _msPromo in this.db.POSMsPromos
                            where _msPromo.PaymentType == _prmPaymentType &&
                            (_msPromo.StartDate <= _prmStartDate ||
                            _msPromo.StartDate <= _prmEndDate) &&
                            (_msPromo.EndDate >= _prmStartDate ||
                            _msPromo.EndDate >= _prmEndDate)
                            select _msPromo.PromoCode
                            );

            foreach (var _row in _query)
            {
                _result.Add(_row);
            }

            return _result;
        }

        ~PromoBL()
        {
        }
    }
}
