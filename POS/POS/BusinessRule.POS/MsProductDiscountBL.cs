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
    public sealed class MsProductDiscountBL : Base
    {
        public MsProductDiscountBL()
        {
        }

        #region DiscountConfigMemberBL

        public double RowsCount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Discount")
                _pattern2 = "%" + _prmKeyword + "%";

            var _query =
                        (
                             from _POSMsDiscountProducts in this.db.POSMsDiscountProducts
                             where (SqlMethods.Like(_POSMsDiscountProducts.ProductCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_POSMsDiscountProducts.DiscountConfigCode.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _POSMsDiscountProducts
                        ).Count();
            _result = _query;
            return _result;
        }


        public List<POSMsDiscountProduct> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsDiscountProduct> _result = new List<POSMsDiscountProduct>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
                _pattern1 = "%" + _prmKeyword + "%";
            else if (_prmCategory == "Discount")
                _pattern2 = "%" + _prmKeyword + "%";

            try
            {
                var _query = (
                                from _POSMsDiscountProducts in this.db.POSMsDiscountProducts
                                where (SqlMethods.Like(_POSMsDiscountProducts.ProductCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_POSMsDiscountProducts.DiscountConfigCode.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select _POSMsDiscountProducts
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);
                foreach (var _row in _query)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<POSMsDiscountProduct> GetList()
        {
            List<POSMsDiscountProduct> _result = new List<POSMsDiscountProduct>();
            try
            {
                var _query = (
                                from _POSMsDiscountProducts in this.db.POSMsDiscountProducts
                                select _POSMsDiscountProducts
                            );
                foreach (var _row in _query)
                    _result.Add(_row);
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
                    POSMsDiscountProduct _deleteData = this.db.POSMsDiscountProducts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());
                    this.db.POSMsDiscountProducts.DeleteOnSubmit(_deleteData);
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

        public POSMsDiscountProduct GetSingle(string _prmCode)
        {
            POSMsDiscountProduct _result = null;
            try
            {
                _result = this.db.POSMsDiscountProducts.Single(_temp => _temp.ProductCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public bool Add(POSMsDiscountProduct _prmNewMsProductDiscountConfig)
        {
            bool _result = false;
            try
            {
                var _qry = (from _msProductDiscount in this.db.POSMsDiscountProducts 
                            where _msProductDiscount.ProductCode == _prmNewMsProductDiscountConfig.ProductCode 
                            select _msProductDiscount);
                if (_qry.Count() == 0)
                    this.db.POSMsDiscountProducts.InsertOnSubmit(_prmNewMsProductDiscountConfig);
                else {
                    POSMsDiscountProduct _editData = this.db.POSMsDiscountProducts.Single(a => a.ProductCode == _prmNewMsProductDiscountConfig.ProductCode);
                    _editData.DiscountConfigCode = _prmNewMsProductDiscountConfig.DiscountConfigCode;
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

        public bool SubmitEdit()
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

        #endregion

        ~MsProductDiscountBL()
        {
        }


        public String GetProductName(String _prmProductCode)
        {
            String _result = "" ;
            var _qry = this.db.MsProducts.Where(a => a.ProductCode == _prmProductCode);
            if (_qry.Count() > 0)
                _result = _qry.FirstOrDefault().ProductName;
            return _result;
        }

        public List<POSMsDiscountConfig> GetListDiscountConfigCode()
        {
            List<POSMsDiscountConfig> _result = new List<POSMsDiscountConfig>();
            var _qry = (
                    from _msDiscountConfig in this.db.POSMsDiscountConfigs
                    where _msDiscountConfig.Status >= 2
                    select _msDiscountConfig
                );
            foreach (var _row in _qry)
                _result.Add(_row);
            return _result;
        }
    }
}
