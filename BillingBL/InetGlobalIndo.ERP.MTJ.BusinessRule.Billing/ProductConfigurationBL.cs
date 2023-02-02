using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed class ProductConfigurationBL : Base
    {
        public ProductConfigurationBL()
        {
        }

        #region ProductConfiguration

        public int RowsCountProductExtension(string _prmCategory, string _prmKeyword)
        {
            int _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "ProductCode")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "ProductName")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword.Trim().ToLower() + "%";
            }

            _result =
                       (
                           from _masterProductExtension in this.db.Master_ProductExtensions
                           join _msProduct in this.db.MsProducts
                                on _masterProductExtension.ProductCode equals _msProduct.ProductCode
                           where
                                (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern1))
                                && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern2))
                           select new
                           {
                               ProductCode = _masterProductExtension.ProductCode
                           }
                       ).Count();

            return _result;
        }

        public List<Master_ProductExtension> GetListProductExtension(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_ProductExtension> _result = new List<Master_ProductExtension>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "ProductCode")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "ProductName")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword.Trim().ToLower() + "%";
            }

            try
            {
                var _query =
                            (
                                from _masterProductExtension in this.db.Master_ProductExtensions
                                join _msProduct in this.db.MsProducts
                                     on _masterProductExtension.ProductCode equals _msProduct.ProductCode
                                where
                                     (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern1))
                                     && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern2))
                                orderby _masterProductExtension.ProductCode ascending
                                select new
                                {
                                    ProductCode = _masterProductExtension.ProductCode,
                                    ProductName = _msProduct.ProductName,
                                    IsPostponeAllowed = _masterProductExtension.IsPostponeAllowed,
                                    IsChangePriceAllowed = _masterProductExtension.IsChangePriceAllowed,
                                    IsMRC = _masterProductExtension.IsMRC,
                                    MinContractMonth = _masterProductExtension.MinContractMonth
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_ProductExtension(_row.ProductCode, _row.ProductName, _row.IsPostponeAllowed, _row.IsChangePriceAllowed, _row.IsMRC, _row.MinContractMonth));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Master_ProductExtension GetSingleProductExtension(string _prmProductCode)
        {
            Master_ProductExtension _result = null;

            try
            {
                _result = this.db.Master_ProductExtensions.Single(_temp => _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiProductExtension(string[] _prmProductCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmProductCode.Length; i++)
                {
                    Master_ProductExtension _masterProductExtension = this.db.Master_ProductExtensions.Single(_temp => _temp.ProductCode.Trim().ToLower() == _prmProductCode[i].Trim().ToLower());

                    this.db.Master_ProductExtensions.DeleteOnSubmit(_masterProductExtension);
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

        public bool AddProductExtension(Master_ProductExtension _prmMasterProductExtension)
        {
            bool _result = false;

            try
            {
                this.db.Master_ProductExtensions.InsertOnSubmit(_prmMasterProductExtension);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditProductExtension(Master_ProductExtension _prmMasterProductExtension)
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



        #endregion ProductConfiguration

        ~ProductConfigurationBL()
        {
        }
    }
}
