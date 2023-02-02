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
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.IO;
using System.Drawing;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl
{
    public partial class ProductBL : Base
    {
        public ProductBL()
        {

        }

        #region Product

        public double RowsCountProduct(string _prmCategory, string _prmKeyword)
        {

            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                (
                    from _msProduct in this.db.MsProducts
                    where (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msProduct

                ).Count();

            _result = _query;
            return _result;
        }

        public double RowsCountProductForReport(string _prmProductGroup, string _prmProductSubGroup, string _prmProductType)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmProductGroup != "")
            {
                _pattern1 = "%" + _prmProductGroup + "%";
            }
            if (_prmProductSubGroup != "")
            {
                _pattern2 = "%" + _prmProductSubGroup + "%";
            }
            if (_prmProductType != "")
            {
                _pattern3 = "%" + _prmProductType + "%";
            }

            var _query =
                (
                    from _msProduct in this.db.MsProducts
                    join _msProductSubGroup in this.db.MsProductSubGroups
                        on _msProduct.ProductSubGroup equals _msProductSubGroup.ProductSubGrpCode
                    where (SqlMethods.Like(_msProductSubGroup.ProductGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && (SqlMethods.Like(_msProduct.ProductType.Trim().ToLower(), _pattern3.Trim().ToLower()))
                    orderby _msProduct.ProductName descending
                    select _msProduct
                ).Count();

            _result = _query;
            return _result;
        }        

        public double RowsCountProductForReport(string _prmProductGroup, string _prmProductSubGroup, string _prmProductType, string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";
            string _pattern5 = "%%";

            if (_prmProductGroup != "")
            {
                _pattern1 = "%" + _prmProductGroup + "%";
            }
            if (_prmProductSubGroup != "")
            {
                _pattern2 = "%" + _prmProductSubGroup + "%";
            }
            if (_prmProductType != "")
            {
                _pattern3 = "%" + _prmProductType + "%";
            }
            if (_prmCategory == "Code")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern5 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern5 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";
            }

            var _query =
                (
                    from _msProduct in this.db.MsProducts
                    join _msProductSubGroup in this.db.MsProductSubGroups
                        on _msProduct.ProductSubGroup equals _msProductSubGroup.ProductSubGrpCode
                    where (SqlMethods.Like(_msProductSubGroup.ProductGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && (SqlMethods.Like(_msProduct.ProductType.Trim().ToLower(), _pattern3.Trim().ToLower()))
                        && (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern4.Trim().ToLower()))
                        && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern5.Trim().ToLower()))
                    orderby _msProduct.ProductName descending
                    select _msProduct
                ).Count();

            _result = _query;
            return _result;
        }

        public double RowsCountProductForReportPriceList(string _prmProductSubGroup, string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";           

            
            if (_prmProductSubGroup != "")
            {
                _pattern2 = "%" + _prmProductSubGroup + "%";
            }            
            if (_prmCategory == "Code")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }

            var _query =
                (
                    from _msProduct in this.db.MsProducts
                    join _msProductSubGroup in this.db.MsProductSubGroups
                        on _msProduct.ProductSubGroup equals _msProductSubGroup.ProductSubGrpCode
                    where (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))                        
                        && (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern2.Trim().ToLower()))
                        && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                    orderby _msProduct.ProductName descending
                    select _msProduct
                ).Count();

            _result = _query;
            return _result;
        }

        public double RowsCountProductUniqueIDForReport()
        {
            double _result = 0;

            var _query =
                (
                    from _msProduct in this.db.MsProducts
                    join _msProductSubGroup in this.db.MsProductSubGroups
                        on _msProduct.ProductSubGroup equals _msProductSubGroup.ProductSubGrpCode
                    join _msProductType in this.db.MsProductTypes
                        on _msProduct.ProductType equals _msProductType.ProductTypeCode
                    where _msProductType.IsUsingUniqueID == true
                    orderby _msProduct.ProductName descending
                    select _msProduct
                ).Count();

            _result = _query;
            return _result;
        }

        public double RowsCountProductInformation(String _prmProductCode, DateTime _prmDateFrom, DateTime _prmDateTo)
        {
            double _result = 0;

            try
            {
                var _query = (
                                from _viewProductInformation in this.db.V_ProductInformations
                                where _viewProductInformation.ProductCode == _prmProductCode
                                && _viewProductInformation.TransDate >= _prmDateFrom
                                && _viewProductInformation.TransDate <= _prmDateTo
                                orderby _viewProductInformation.TransDate ascending
                                select new
                                {
                                    TransNmbr = _viewProductInformation.TransNmbr
                                }
                            ).Count();

                _result = _query;
                return _result;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;

        }

        public List<V_ProductInformation> GetListProductInformation(int _prmReqPage, int _prmPageSize, String _prmProductCode, DateTime _prmDateFrom, DateTime _prmDateTo)
        {
            List<V_ProductInformation> _result = new List<V_ProductInformation>();

            try
            {
                var _query = (
                                from _viewProductInformation in this.db.V_ProductInformations
                                where _viewProductInformation.ProductCode == _prmProductCode
                                && _viewProductInformation.TransDate >= _prmDateFrom
                                && _viewProductInformation.TransDate <= _prmDateTo
                                orderby _viewProductInformation.TransDate ascending
                                select new
                                {
                                    TransDate = _viewProductInformation.TransDate,
                                    TransNmbr = _viewProductInformation.TransNmbr,
                                    TransType = _viewProductInformation.TransType,
                                    FileNmbr = _viewProductInformation.FileNmbr,
                                    ReffInsName = _viewProductInformation.ReffInsName,
                                    WrhsCode = _viewProductInformation.WrhsCode,
                                    QtyIn = _viewProductInformation.QtyIn,
                                    QtyOut = _viewProductInformation.QtyOut,
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new V_ProductInformation(Convert.ToDateTime(_row.TransDate.ToString()), _row.TransNmbr, _row.TransType, _row.FileNmbr, _row.ReffInsName, _row.WrhsCode, _row.QtyIn, _row.QtyOut));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;

        }

        public List<MsProduct> GetListProduct(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsProduct> _result = new List<MsProduct>();
            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                where (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msProduct.ProductCode, _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName,
                                    ProductSubGrpCode = _msProduct.ProductSubGroup,
                                    ProductSubGrpName = (
                                                            from _msProductSubGroup in this.db.MsProductSubGroups
                                                            where _msProduct.ProductSubGroup == _msProductSubGroup.ProductSubGrpCode
                                                            select _msProductSubGroup.ProductSubGrpName
                                                         ).FirstOrDefault(),
                                    ProductTypeCode = _msProduct.ProductType,
                                    ProductTypeName = (
                                                            from _msProductType in this.db.MsProductTypes
                                                            where _msProduct.ProductType == _msProductType.ProductTypeCode
                                                            select _msProductType.ProductTypeName
                                                      ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string, ProductSubGrpCode = this._string, ProductSubGrpName = this._string, ProductTypeCode = this._string, ProductTypeName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName, _row.ProductSubGrpCode, _row.ProductSubGrpName, _row.ProductTypeCode, _row.ProductTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsProductCodeExists(String _prmProductCode)
        {
            bool _result = false;

            try
            {
                var _query = from _msProduct in this.db.MsProducts
                             where _msProduct.ProductCode == _prmProductCode
                             select new
                             {
                                 _msProduct.ProductCode
                             };

                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListForDDLProduct()
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                where _msProduct.FgActive == 'Y'
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListForDDLProductByCustomer(string _prmCust)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _bilTrSalesConfirmation in this.db.BILTrSalesConfirmations
                                join _bilTrSalesConfirmationDt in this.db.BILTrSalesConfirmationDts
                                on _bilTrSalesConfirmation.TransNmbr equals _bilTrSalesConfirmationDt.TransNmbr
                                join _msProduct in this.db.MsProducts
                                on _bilTrSalesConfirmationDt.ProductCode equals _msProduct.ProductCode
                                where _bilTrSalesConfirmation.CustCode == _prmCust
                                && _msProduct.FgActive == 'Y'
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListForDDLGimmick()
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                where _msProduct.SellingPrice == 0
                                && _msProduct.FgActive == 'Y'
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetListStringProduct()
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result += _row.ProductCode + ",";
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListForDDLProductUniqueID(int _prmReqPage, int _prmPageSize)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msProductSubGroup in this.db.MsProductSubGroups
                                    on _msProduct.ProductSubGroup equals _msProductSubGroup.ProductSubGrpCode
                                join _msProductType in this.db.MsProductTypes
                                    on _msProduct.ProductType equals _msProductType.ProductTypeCode
                                where _msProductType.IsUsingUniqueID == true
                                && _msProduct.FgActive == 'Y'
                                orderby _msProduct.ProductName descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListForDDLProduct(string _prmProductGroup, string _prmProductSubGroup, string _prmProductType, int _prmReqPage, int _prmPageSize)
        {
            List<MsProduct> _result = new List<MsProduct>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmProductGroup != "")
            {
                _pattern1 = "%" + _prmProductGroup + "%";
            }
            if (_prmProductSubGroup != "")
            {
                _pattern2 = "%" + _prmProductSubGroup + "%";
            }
            if (_prmProductType != "")
            {
                _pattern3 = "%" + _prmProductType + "%";
            }

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msProductSubGroup in this.db.MsProductSubGroups
                                    on _msProduct.ProductSubGroup equals _msProductSubGroup.ProductSubGrpCode
                                where (SqlMethods.Like(_msProductSubGroup.ProductGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductType.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && _msProduct.FgActive == 'Y'
                                orderby _msProduct.ProductName descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListForDDLProduct(string _prmProductGroup, string _prmProductSubGroup, string _prmProductType, string _prmCategory, string _prmKeyword, int _prmReqPage, int _prmPageSize)
        {
            List<MsProduct> _result = new List<MsProduct>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";
            string _pattern5 = "%%";

            if (_prmProductGroup != "")
            {
                _pattern1 = "%" + _prmProductGroup + "%";
            }
            if (_prmProductSubGroup != "")
            {
                _pattern2 = "%" + _prmProductSubGroup + "%";
            }
            if (_prmProductType != "")
            {
                _pattern3 = "%" + _prmProductType + "%";
            }
            if (_prmCategory == "Code")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern5 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern5 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";
            }

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msProductSubGroup in this.db.MsProductSubGroups
                                    on _msProduct.ProductSubGroup equals _msProductSubGroup.ProductSubGrpCode
                                where (SqlMethods.Like(_msProductSubGroup.ProductGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductType.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern5.Trim().ToLower()))
                                    && _msProduct.FgActive == 'Y'
                                orderby _msProduct.ProductName descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListForDDLProductForReportPriceList(string _prmProductSubGroup, string _prmCategory, string _prmKeyword, int _prmReqPage, int _prmPageSize)
        {
            List<MsProduct> _result = new List<MsProduct>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";            
                        
            if (_prmProductSubGroup != "")
            {
                _pattern1 = "%" + _prmProductSubGroup + "%";
            }            
            if (_prmCategory == "Code")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }
            else if (_prmCategory == "Name")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msProductSubGroup in this.db.MsProductSubGroups
                                    on _msProduct.ProductSubGroup equals _msProductSubGroup.ProductSubGrpCode
                                where (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && _msProduct.FgActive == 'Y'
                                orderby _msProduct.ProductName descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListForDDLProductForReportPriceList(string _prmProductGroup)
        {
            List<MsProduct> _result = new List<MsProduct>();

            string _pattern1 = "%%";            

            if (_prmProductGroup != "")
            {
                _pattern1 = "%" + _prmProductGroup + "%";
            }            

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msProductSubGroup in this.db.MsProductSubGroups
                                    on _msProduct.ProductSubGroup equals _msProductSubGroup.ProductSubGrpCode
                                where (SqlMethods.Like(_msProductSubGroup.ProductGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))                                    
                                    && _msProduct.FgActive == 'Y'
                                orderby _msProduct.ProductName descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListForDDLProduct(string _prmProductGroup, string _prmProductSubGroup, string _prmProductType)
        {
            List<MsProduct> _result = new List<MsProduct>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmProductGroup != "")
            {
                _pattern1 = "%" + _prmProductGroup + "%";
            }
            if (_prmProductSubGroup != "")
            {
                _pattern2 = "%" + _prmProductSubGroup + "%";
            }
            if (_prmProductType != "")
            {
                _pattern3 = "%" + _prmProductType + "%";
            }

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msProductSubGroup in this.db.MsProductSubGroups
                                    on _msProduct.ProductSubGroup equals _msProductSubGroup.ProductSubGrpCode
                                where (SqlMethods.Like(_msProductSubGroup.ProductGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductType.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && _msProduct.FgActive == 'Y'
                                orderby _msProduct.ProductName descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListForDDLProductUniqueID()
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msProductSubGroup in this.db.MsProductSubGroups
                                    on _msProduct.ProductSubGroup equals _msProductSubGroup.ProductSubGrpCode
                                join _msProductType in this.db.MsProductTypes
                                    on _msProduct.ProductType equals _msProductType.ProductTypeCode
                                where _msProductType.IsUsingUniqueID == true
                                && _msProduct.FgActive == 'Y'
                                orderby _msProduct.ProductName descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListForDDLProductByType(string _prmType)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                where _msProduct.ProductType == _prmType
                                && _msProduct.FgActive == 'Y'
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListForDDLProductNonStock()
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _prodType in this.db.MsProductTypes
                                    on _msProduct.ProductType equals _prodType.ProductTypeCode
                                where _msProduct.FgActive.ToString().ToLower() == ProductDataMapper.IsActive(YesNo.Yes).ToString().ToLower()
                                    && _prodType.FgStock.ToString().ToLower() == ProductTypeDataMapper.IsActive(YesNo.No).ToString().ToLower()
                                    && _msProduct.FgActive == 'Y'
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListViewProductForDDL()
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.V_MsProducts
                                where _msProduct.FgActive == 'Y'
                                orderby _msProduct.Product_Name descending
                                select new
                                {
                                    ProductCode = _msProduct.Product_Code,
                                    ProductName = _msProduct.Product_Name
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductByProductSubGroupForDDL(string _prmProductSubGrpCode)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                where _msProduct.ProductSubGroup == _prmProductSubGrpCode
                                && _msProduct.FgActive == 'Y'
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductActiveForDDL()
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                where _msProduct.FgActive == ProductDataMapper.IsActive(YesNo.Yes)
                                orderby _msProduct.ModifiedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductForDDLActiveAndStock(string _prmSJNo)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msProductType in this.db.MsProductTypes
                                    on _msProduct.ProductType equals _msProductType.ProductTypeCode
                                join _stcSJDt in this.db.STCSJDts
                                    on _msProduct.ProductCode equals _stcSJDt.ProductCode
                                where _msProduct.FgActive == ProductDataMapper.IsActive(YesNo.Yes) &&
                                _msProductType.FgStock == ProductTypeDataMapper.IsActive(YesNo.Yes) &&
                                _stcSJDt.TransNmbr == _prmSJNo
                                && _msProduct.FgActive == 'Y'
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductForDDLActiveAndStock()
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msProductType in this.db.MsProductTypes
                                    on _msProduct.ProductType equals _msProductType.ProductTypeCode
                                where _msProduct.FgActive == ProductDataMapper.IsActive(YesNo.Yes) &&
                                _msProductType.FgStock == ProductTypeDataMapper.IsActive(YesNo.Yes)
                                && _msProduct.FgActive == 'Y'
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductForDDLStockByWrhsAndWrhsSub(string _prmWarehouse, string _prmWrhsSubled)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _stcStockValues in this.db.STCStockValues
                                join _msProduct in this.db.MsProducts
                                    on _stcStockValues.ProductCode equals _msProduct.ProductCode
                                where _stcStockValues.WrhsCode.Trim().ToLower() == _prmWarehouse.Trim().ToLower()
                                && _stcStockValues.WrhsSubLed.Trim().ToLower() == _prmWrhsSubled.Trim().ToLower()
                                && _msProduct.FgActive == 'Y'
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _stcStockValues.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductFromVSTTransferReqForSJ(string _prmRequestNo)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _vSTTransferReqSJ in this.db.V_STTransferReqForSJs
                                where _vSTTransferReqSJ.Request_No == _prmRequestNo
                                select new
                                {
                                    ProductCode = _vSTTransferReqSJ.Product_Code,
                                    ProductName = _vSTTransferReqSJ.Product_Name
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductFromV_STRejectOutForRR(string _prmRejectNo)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _vSTRejectOutForRR in this.db.V_STRejectOutForRRs
                                where _vSTRejectOutForRR.Reject_No == _prmRejectNo
                                select new
                                {
                                    ProductCode = _vSTRejectOutForRR.ProductCode,
                                    ProductName = _vSTRejectOutForRR.ProductName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductForDDLStock()
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msProductType in this.db.MsProductTypes
                                    on _msProduct.ProductType equals _msProductType.ProductTypeCode
                                where _msProductType.FgStock == ProductTypeDataMapper.IsActive(YesNo.Yes)
                                && _msProduct.FgActive == 'Y'
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            ).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductForDDLStockByCurr(string _currCode)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msProductType in this.db.MsProductTypes
                                    on _msProduct.ProductType equals _msProductType.ProductTypeCode
                                where _msProductType.FgStock == ProductTypeDataMapper.IsActive(YesNo.Yes)
                                && _msProduct.PurchaseCurr == _currCode
                                && _msProduct.FgActive == 'Y'
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            ).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, ProductName = this._string });

                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductForPhoneSeries(String _prmProductTypeCode)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msProductType in this.db.MsProductTypes
                                    on _msProduct.ProductType equals _msProductType.ProductTypeCode
                                where _msProductType.IsUsingUniqueID == true
                                    && _msProductType.ProductTypeCode == _prmProductTypeCode
                                && _msProduct.FgActive == 'Y'
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListProductUniqueIDForDDL()
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msProductType in this.db.MsProductTypes
                                    on _msProduct.ProductType equals _msProductType.ProductTypeCode
                                where _msProductType.IsUsingUniqueID == true
                                && _msProduct.FgActive == 'Y'
                                orderby _msProduct.CreatedDate descending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiProduct(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsProduct _msProduct = this.db.MsProducts.Single(_temp => _temp.ProductCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_msProduct != null)
                    {
                        var _query = (from _detail in this.db.MsProductConverts
                                      where _detail.ProductCode.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                      select _detail);

                        this.db.MsProductConverts.DeleteAllOnSubmit(_query);

                        var _query2 = (from _detail in this.db.Master_ProductSalesPrices
                                       where _detail.ProductCode.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                       select _detail);

                        this.db.Master_ProductSalesPrices.DeleteAllOnSubmit(_query2);

                        this.db.MsProducts.DeleteOnSubmit(_msProduct);
                    }
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

        public MsProduct GetSingleProduct(string _prmCode)
        {
            MsProduct _result = null;

            try
            {
                _result = this.db.MsProducts.Single(_temp => _temp.ProductCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MsProduct GetSingleProductByBarcode(string _prmBarcode)
        {
            MsProduct _result = null;

            try
            {
                var _productCode = (
                                    from _msProduct in this.db.MsProducts
                                    where _msProduct.Barcode == _prmBarcode
                                    select _msProduct.ProductCode
                                   ).FirstOrDefault();

                _result = this.db.MsProducts.Single(_temp => _temp.ProductCode == _productCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetProductNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                where _msProduct.ProductCode == _prmCode
                                select new
                                {
                                    ProductName = _msProduct.ProductName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ProductName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddProduct(MsProduct _prmMsProduct)
        {
            bool _result = false;

            try
            {
                if (IsUsingPG(_prmMsProduct) == true)
                {
                    MsProductConvert _msProductConvert = new MsProductConvert();

                    _msProductConvert.ProductCode = _prmMsProduct.ProductCode;
                    _msProductConvert.UnitCode = _prmMsProduct.Unit;
                    _msProductConvert.UnitConvert = _prmMsProduct.Unit;
                    _msProductConvert.Rate = 1;

                    this.db.MsProductConverts.InsertOnSubmit(_msProductConvert);
                    this.db.MsProducts.InsertOnSubmit(_prmMsProduct);
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddProductList(List<MsProduct> _prmMsProductList)
        {
            bool _result = false;

            try
            {
                foreach (MsProduct _row in _prmMsProductList)
                {
                    MsProduct _msProduct = new MsProduct();

                    _msProduct.ProductCode = _row.ProductCode;
                    _msProduct.ProductName = _row.ProductName;
                    _msProduct.ProductSubGroup = _row.ProductSubGroup;
                    _msProduct.ProductType = _row.ProductType;
                    _msProduct.Specification1 = _row.Specification1;
                    _msProduct.Specification2 = _row.Specification2;
                    _msProduct.Specification3 = _row.Specification3;
                    _msProduct.Specification4 = _row.Specification4;
                    _msProduct.MinQty = _row.MinQty;
                    _msProduct.MaxQty = _row.MaxQty;
                    _msProduct.Unit = _row.Unit;
                    _msProduct.UnitOrder = _row.UnitOrder;
                    _msProduct.Length = _row.Length;
                    _msProduct.Width = _row.Width;
                    _msProduct.Height = _row.Height;
                    _msProduct.Volume = _row.Volume;
                    _msProduct.Weight = _row.Weight;
                    _msProduct.PurchaseCurr = _row.PurchaseCurr;
                    _msProduct.PriceGroupCode = _row.PriceGroupCode;
                    _msProduct.BuyingPrice = _row.BuyingPrice;
                    _msProduct.SellingPrice = _row.SellingPrice;
                    _msProduct.FgActive = _row.FgActive;
                    _msProduct.CreatedBy = _row.CreatedBy;
                    _msProduct.CreatedDate = _row.CreatedDate;
                    _msProduct.Barcode = _row.Barcode;
                    _msProduct.FgConsignment = _row.FgConsignment;
                    _msProduct.ConsignmentSuppCode = _row.ConsignmentSuppCode;

                    MsProductConvert _msProductConvert = new MsProductConvert();

                    _msProductConvert.ProductCode = _row.ProductCode;
                    _msProductConvert.UnitCode = _row.Unit;
                    _msProductConvert.UnitConvert = _row.Unit;
                    _msProductConvert.Rate = 1;

                    this.db.MsProductConverts.InsertOnSubmit(_msProductConvert);

                    this.db.MsProducts.InsertOnSubmit(_msProduct);
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

        private bool IsUsingPG(MsProduct _prmProduct)
        {
            bool _result = false;
            bool _pg = GetSingleProductType(_prmProduct.ProductType).IsUsingPG;

            if (_pg == true && _prmProduct.PriceGroupCode != null)
            {
                _result = true;
            }
            else if (_pg == false && _prmProduct.PriceGroupCode == null)
            {
                _result = true;
            }
            else
            {
                _result = false;
            }

            return _result;
        }

        public bool EditProduct(MsProduct _prmMsProduct)
        {
            bool _result = false;

            try
            {
                if (IsUsingPG(_prmMsProduct) == true)
                {
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetUnitNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msUnit in this.db.MsUnits
                                    on _msProduct.Unit equals _msUnit.UnitCode
                                where _msProduct.ProductCode == _prmCode
                                select new
                                {
                                    UnitName = _msUnit.UnitName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.UnitName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public decimal GetSystemQty(string _prmProductCode, string _prmLocCode, string _prmWrhs, string _prmSubled)
        {
            decimal _result = 0;

            try
            {
                var _query = (

                                from _stcStockValues in this.db.STCStockValues
                                where _stcStockValues.ProductCode == _prmProductCode
                                    && _stcStockValues.LocationCode == _prmLocCode
                                    && _stcStockValues.WrhsCode == _prmWrhs
                                    && _stcStockValues.WrhsSubLed == _prmSubled
                                select new
                                {
                                    Qty = _stcStockValues.Qty
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.Qty;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetUnitCodeByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                join _msUnit in this.db.MsUnits
                                    on _msProduct.Unit equals _msUnit.UnitCode
                                where _msProduct.ProductCode.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    UnitCode = _msUnit.UnitCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.UnitCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public double RowsCountProductReport(string _prmProductSubGroup, string _prmProductType)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmProductSubGroup != "")
            {
                _pattern1 = "%" + _prmProductSubGroup + "%";
                //_pattern2 = "%%";
            }

            if (_prmProductType != "")
            {
                _pattern2 = "%" + _prmProductType + "%";
                //_pattern1 = "%%";
            }

            var _query =
                        (
                            from _msProduct in this.db.MsProducts
                            where _msProduct.FgActive == ProductDataMapper.IsActive(YesNo.Yes)
                               && (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msProduct.ProductType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _msProduct.ProductCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountDDLProductForReport(string _prmProductSubGroup, string _prmProductType, string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmProductSubGroup != "null")
            {
                _pattern1 = "%" + _prmProductSubGroup + "%";
            }

            if (_prmProductType != "null")
            {
                _pattern2 = "%" + _prmProductType + "%";
            }

            if (_prmCategory == "Code")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }

            var _query =
                        (
                            from _msProduct in this.db.MsProducts
                            where _msProduct.FgActive == ProductDataMapper.IsActive(YesNo.Yes)
                               && (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_msProduct.ProductType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               && (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                            select _msProduct.ProductCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public double RowsCountDDLProductForReport(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            //string _pattern1 = "%%";
            //string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            //if (_prmProductSubGroup != "null")
            //{
            //    _pattern1 = "%" + _prmProductSubGroup + "%";
            //}

            //if (_prmProductType != "null")
            //{
            //    _pattern2 = "%" + _prmProductType + "%";
            //}

            if (_prmCategory == "Code")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }

            var _query =
                        (
                            from _msProduct in this.db.MsProducts
                            where _msProduct.FgActive == ProductDataMapper.IsActive(YesNo.Yes)
                               && (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                               && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                            select _msProduct.ProductCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<MsProduct> GetListDDLProductForReport(string _prmProductSubGroup, string _prmProductType, int _prmReqPage, int _prmPageSize)
        {
            List<MsProduct> _result = new List<MsProduct>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmProductSubGroup != "")
            {
                _pattern1 = "%" + _prmProductSubGroup + "%";
            }

            if (_prmProductType != "")
            {
                _pattern2 = "%" + _prmProductType + "%";
            }

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                where _msProduct.FgActive == ProductDataMapper.IsActive(YesNo.Yes)
                                    && (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msProduct.ProductName ascending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName + " - " + _msProduct.ProductCode
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListDDLProductForReport(string _prmProductSubGroup, string _prmProductType)
        {
            List<MsProduct> _result = new List<MsProduct>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmProductSubGroup != "")
            {
                _pattern1 = "%" + _prmProductSubGroup + "%";
            }

            if (_prmProductType != "")
            {
                _pattern2 = "%" + _prmProductType + "%";
            }

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                where _msProduct.FgActive == ProductDataMapper.IsActive(YesNo.Yes)
                                    && (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msProduct.ProductName ascending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName + " - " + _msProduct.ProductCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListDDLProductForReport(string _prmProductSubGroup, string _prmProductType, string _prmCategory, string _prmKeyword)
        {
            List<MsProduct> _result = new List<MsProduct>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmProductSubGroup != "null")
            {
                _pattern1 = "%" + _prmProductSubGroup + "%";
            }

            if (_prmProductType != "null")
            {
                _pattern2 = "%" + _prmProductType + "%";
            }

            if (_prmCategory == "Code")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                where _msProduct.FgActive == ProductDataMapper.IsActive(YesNo.Yes)
                                    && (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                orderby _msProduct.ProductName ascending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName + " - " + _msProduct.ProductCode
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListDDLProductForReport(int _prmReqPage, int _prmPageSize, string _prmProductSubGroup, string _prmProductType, string _prmCategory, string _prmKeyword)
        {
            List<MsProduct> _result = new List<MsProduct>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmProductSubGroup != "null")
            {
                _pattern1 = "%" + _prmProductSubGroup + "%";
            }

            if (_prmProductType != "null")
            {
                _pattern2 = "%" + _prmProductType + "%";
            }

            if (_prmCategory == "Code")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                where _msProduct.FgActive == ProductDataMapper.IsActive(YesNo.Yes)
                                    && (SqlMethods.Like(_msProduct.ProductSubGroup.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                orderby _msProduct.ProductName ascending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName + " - " + _msProduct.ProductCode
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProduct> GetListDDLProductForReport(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsProduct> _result = new List<MsProduct>();

            //string _pattern1 = "%%";
            //string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            //if (_prmProductSubGroup != "null")
            //{
            //    _pattern1 = "%" + _prmProductSubGroup + "%";
            //}

            //if (_prmProductType != "null")
            //{
            //    _pattern2 = "%" + _prmProductType + "%";
            //}

            if (_prmCategory == "Code")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern4 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern3 = "%%";
            }

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                where _msProduct.FgActive == ProductDataMapper.IsActive(YesNo.Yes)
                                    && (SqlMethods.Like(_msProduct.ProductCode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                orderby _msProduct.ProductName ascending
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductCode + " - " + _msProduct.ProductName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String ChangeProductPicture(String _prmProductCode, FileUpload _prmFileUpload)
        {
            String _result = "";

            String _path = _prmFileUpload.PostedFile.FileName;
            FileInfo _file = new FileInfo(_path);
            String _imagepath = ApplicationConfig.ProductPhotoPath + _prmProductCode + _file.Extension;

            if (_path == "")
            {
                _result = "Invalid filename supplied";
            }
            if (_prmFileUpload.PostedFile.ContentLength == 0)
            {
                _result = "Invalid file content";
            }
            if (_result == "")
            {
                if (PictureHandler.IsPictureFile(_path, ApplicationConfig.ImageExtension) == true)
                {
                    System.Drawing.Image _uploadedImage = System.Drawing.Image.FromStream(_prmFileUpload.PostedFile.InputStream);

                    Decimal _width = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Width);
                    Decimal _height = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Height);

                    if (_width > Convert.ToDecimal(ApplicationConfig.ImageHeight) || _height > Convert.ToDecimal(ApplicationConfig.ImageHeight))
                    {
                        _result = "This image is too big - please resize it!";
                    }
                    else
                    {
                        if (_prmFileUpload.PostedFile.ContentLength <= Convert.ToDecimal(ApplicationConfig.ImageMaxSize))
                        {
                            MsProduct _product = this.GetSingleProduct(_prmProductCode);

                            if (_product.Photo != ApplicationConfig.ProductImageDefault)
                            {
                                if (File.Exists(ApplicationConfig.ProductPhotoPath + _product.Photo) == true)
                                {
                                    File.Delete(ApplicationConfig.ProductPhotoPath + _product.Photo);
                                }
                            }

                            //_file.CopyTo(_imagepath, true);
                            _prmFileUpload.PostedFile.SaveAs(_imagepath);

                            _product.Photo = _prmProductCode + _file.Extension;
                            this.db.SubmitChanges();

                            _file.Refresh();

                            _result = "File uploaded successfully";
                        }
                        else
                        {
                            _result = "Unable to upload, file exceeds maximum limit";
                        }
                    }
                }
                else
                {
                    _result = "File type not supported";
                }
            }

            return _result;
        }

        public bool GetSingleProductByBarcodeForPOS(string _prmBarcode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _product in this.db.MsProducts
                                where _product.Barcode.Trim().ToLower() == _prmBarcode.Trim().ToLower()
                                select _product
                             );


                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public bool GetSingleProductForPOS(string _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _product in this.db.MsProducts
                                where _product.ProductCode.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select _product
                             );


                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        #endregion

        #region ProductGroup

        public double RowsCount(string _prmCategory, string _prmKeyword)
        {

            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                (
                    from _msProductGroup in this.db.MsProductGroups
                    where (SqlMethods.Like(_msProductGroup.ProductGrpCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msProductGroup.ProductGrpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msProductGroup

                ).Count();

            _result = _query;
            return _result;
        }

        public List<MsProductGroup> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsProductGroup> _result = new List<MsProductGroup>();
            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            try
            {
                var _query = (
                                from _msProductGroup in this.db.MsProductGroups
                                orderby _msProductGroup.CreatedDate descending
                                where (SqlMethods.Like(_msProductGroup.ProductGrpCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msProductGroup.ProductGrpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    ProductGrpCode = _msProductGroup.ProductGrpCode,
                                    ProductGrpName = _msProductGroup.ProductGrpName,
                                    UserId = _msProductGroup.CreatedBy,
                                    UserDate = _msProductGroup.CreatedDate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductGrpCode = this._string, ProductGrpName = this._string, UserId = this._string, UserDate = this._nullableDateTime });

                    _result.Add(new MsProductGroup(_row.ProductGrpCode, _row.ProductGrpName, _row.UserId, _row.UserDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProductGroup> GetListForDDL()
        {
            List<MsProductGroup> _result = new List<MsProductGroup>();

            try
            {
                var _query = (
                                from _msProductGroup in this.db.MsProductGroups
                                where _msProductGroup.FgActive == 'Y'
                                orderby _msProductGroup.CreatedDate descending
                                select new
                                {
                                    ProductGrpCode = _msProductGroup.ProductGrpCode,
                                    ProductGrpName = _msProductGroup.ProductGrpName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductGrpCode = this._string, ProductGrpName = this._string });

                    _result.Add(new MsProductGroup(_row.ProductGrpCode, _row.ProductGrpName));
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
                    MsProductGroup _msProductGroup = this.db.MsProductGroups.Single(_temp => _temp.ProductGrpCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsProductGroups.DeleteOnSubmit(_msProductGroup);
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

        public MsProductGroup GetSingle(string _prmCode)
        {
            MsProductGroup _result = null;

            try
            {
                _result = this.db.MsProductGroups.Single(_temp => _temp.ProductGrpCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetProductByCodeAndCurr(String _prmProductCode, String _prmCurrCode)
        {
            String _result = "||";
            try
            {
                var _query = (
                                from _productData in this.db.MsProducts
                                where _productData.ProductCode == _prmProductCode
                                    && _productData.PurchaseCurr == _prmCurrCode
                                select new
                                {
                                    ProductName = _productData.ProductName,
                                    Unit = _productData.Unit,
                                    SellingPrice = _productData.BuyingPrice
                                }
                            );
                if (_query.Count() > 0)
                {
                    var _rs = _query.FirstOrDefault();
                    _result = _rs.ProductName + "|" + _rs.Unit + "|" + _rs.SellingPrice;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public string GetProductGroupNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msProductGroup in this.db.MsProductGroups
                                where _msProductGroup.ProductGrpCode == _prmCode
                                select new
                                {
                                    ProductGroupName = _msProductGroup.ProductGrpName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ProductGroupName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(MsProductGroup _prmMsProductGroup)
        {
            bool _result = false;

            try
            {
                this.db.MsProductGroups.InsertOnSubmit(_prmMsProductGroup);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsProductGroup _prmMsProductGroup)
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

        #region ProductSubGroup

        public double RowsCountProductSubGroup(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                (
                    from _msProdSubGroup in this.db.MsProductSubGroups
                    where (SqlMethods.Like(_msProdSubGroup.ProductSubGrpCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msProdSubGroup.ProductSubGrpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msProdSubGroup

                ).Count();

            _result = _query;
            return _result;
        }

        public List<MsProductSubGroup> GetListProductSubGroup(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsProductSubGroup> _result = new List<MsProductSubGroup>();
            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            try
            {
                var _query = (
                                from _msProductSubGroup in this.db.MsProductSubGroups
                                where (SqlMethods.Like(_msProductSubGroup.ProductSubGrpCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msProductSubGroup.ProductSubGrpName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msProductSubGroup.CreatedDate descending
                                select new
                                {
                                    ProductSubGrpCode = _msProductSubGroup.ProductSubGrpCode,
                                    ProductSubGrpName = _msProductSubGroup.ProductSubGrpName,
                                    ProductGroup = _msProductSubGroup.ProductGroup,
                                    UserId = _msProductSubGroup.CreatedBy,
                                    UserDate = _msProductSubGroup.CreatedDate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductSubGrpCode = this._string, ProductSubGrpName = this._string, ProductGroup = this._string, UserId = this._string, UserDate = this._nullableDateTime });

                    _result.Add(new MsProductSubGroup(_row.ProductSubGrpCode, _row.ProductSubGrpName, _row.ProductGroup, _row.UserId, _row.UserDate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProductSubGroup> GetListProductSubGroupForDDL()
        {
            List<MsProductSubGroup> _result = new List<MsProductSubGroup>();

            try
            {
                var _query = (
                                from _msProductSubGroup in this.db.MsProductSubGroups
                                where _msProductSubGroup.FgActive == 'Y'
                                orderby _msProductSubGroup.CreatedDate descending
                                select new
                                {
                                    ProductSubGrpCode = _msProductSubGroup.ProductSubGrpCode,
                                    ProductSubGrpName = _msProductSubGroup.ProductSubGrpName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductSubGrpCode = this._string, ProductSubGrpName = this._string });

                    _result.Add(new MsProductSubGroup(_row.ProductSubGrpCode, _row.ProductSubGrpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProductSubGroup> GetListProductSubGroupForDDLbyGroupCode(String _prmProductGroup)
        {
            List<MsProductSubGroup> _result = new List<MsProductSubGroup>();

            try
            {
                var _query = (
                                from _msProductSubGroup in this.db.MsProductSubGroups
                                where _msProductSubGroup.ProductGroup == _prmProductGroup
                                && _msProductSubGroup.FgActive == 'Y'
                                orderby _msProductSubGroup.CreatedDate descending
                                select new
                                {
                                    ProductSubGrpCode = _msProductSubGroup.ProductSubGrpCode,
                                    ProductSubGrpName = _msProductSubGroup.ProductSubGrpName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsProductSubGroup(_row.ProductSubGrpCode, _row.ProductSubGrpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProductSubGroup> GetListProductSubGroupForDDL(string _prmCategory, string _prmKeyword, int _prmReqPage, int _prmPageSize)
        {
            List<MsProductSubGroup> _result = new List<MsProductSubGroup>();

            try
            {
                string _pattern3 = "%%";
                string _pattern4 = "%%";

                if (_prmCategory == "Code")
                {
                    _pattern3 = "%" + _prmKeyword + "%";
                    _pattern4 = "%%";
                }
                else if (_prmCategory == "Name")
                {
                    _pattern4 = "%" + _prmKeyword + "%";
                    _pattern3 = "%%";
                }

                var _query = (
                                from _msProductSubGroup in this.db.MsProductSubGroups
                                where (SqlMethods.Like(_msProductSubGroup.ProductSubGrpCode.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProductSubGroup.ProductSubGrpName.Trim().ToLower(), _pattern4.Trim().ToLower()))
                                && _msProductSubGroup.FgActive == 'Y'
                                orderby _msProductSubGroup.CreatedDate descending
                                select new
                                {
                                    ProductSubGrpCode = _msProductSubGroup.ProductSubGrpCode,
                                    ProductSubGrpName = _msProductSubGroup.ProductSubGrpName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductSubGrpCode = this._string, ProductSubGrpName = this._string });

                    _result.Add(new MsProductSubGroup(_row.ProductSubGrpCode, _row.ProductSubGrpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProductSubGroup> GetListProductSubGroupForDDL(string _prmProductGroupCode)
        {
            List<MsProductSubGroup> _result = new List<MsProductSubGroup>();

            string _pattern1 = "%%";

            if (_prmProductGroupCode != "")
            {
                _pattern1 = "%" + _prmProductGroupCode + "%";
            }

            try
            {
                var _query = (
                                from _msProductSubGroup in this.db.MsProductSubGroups
                                where SqlMethods.Like(_msProductSubGroup.ProductGroup.Trim().ToLower(), _pattern1.Trim().ToLower())
                                && _msProductSubGroup.FgActive == 'Y'
                                orderby _msProductSubGroup.CreatedDate descending
                                select new
                                {
                                    ProductSubGrpCode = _msProductSubGroup.ProductSubGrpCode,
                                    ProductSubGrpName = _msProductSubGroup.ProductSubGrpName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductSubGrpCode = this._string, ProductSubGrpName = this._string });

                    _result.Add(new MsProductSubGroup(_row.ProductSubGrpCode, _row.ProductSubGrpName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetProductSubGroupNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msProductSubGroup in this.db.MsProductSubGroups
                                where _msProductSubGroup.ProductSubGrpCode == _prmCode
                                select new
                                {
                                    ProductSubGrpName = _msProductSubGroup.ProductSubGrpName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ProductSubGrpName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiProductSubGroup(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsProductSubGroup _msProductSubGroup = this.db.MsProductSubGroups.Single(_temp => _temp.ProductSubGrpCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsProductSubGroups.DeleteOnSubmit(_msProductSubGroup);
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

        public MsProductSubGroup GetSingleProductSubGroup(string _prmCode)
        {
            MsProductSubGroup _result = null;

            try
            {
                _result = this.db.MsProductSubGroups.Single(_temp => _temp.ProductSubGrpCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddProductSubGroup(MsProductSubGroup _prmMsProductSubGroup)
        {
            bool _result = false;

            try
            {
                this.db.MsProductSubGroups.InsertOnSubmit(_prmMsProductSubGroup);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditProductSubGroup(MsProductSubGroup _prmMsProductSubGroup)
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

        public bool IsProductSubGroupExists(String _prmProductSubGroup)
        {
            bool _result = false;

            try
            {
                var _query = from _msProductSubGroup in this.db.MsProductSubGroups
                             where _msProductSubGroup.ProductSubGrpCode == _prmProductSubGroup
                             select new
                             {
                                 _msProductSubGroup.ProductSubGrpCode
                             };

                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region ProductType
        public double RowsCountProductType(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                (
                    from _msProductType in this.db.MsProductTypes
                    where (SqlMethods.Like(_msProductType.ProductTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msProductType.ProductTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msProductType

                ).Count();

            _result = _query;
            return _result;
        }

        public List<MsProductType> GetListProductType(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsProductType> _result = new List<MsProductType>();
            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }
            try
            {
                var _query = (
                                from _msProductType in this.db.MsProductTypes
                                where (SqlMethods.Like(_msProductType.ProductTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_msProductType.ProductTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msProductType.ModifiedDate descending,_msProductType.CreatedDate descending
                                select new
                                {
                                    ProductTypeCode = _msProductType.ProductTypeCode,
                                    ProductTypeName = _msProductType.ProductTypeName,
                                    ProductCategory = _msProductType.ProductCategory,
                                    IsUsingPG = _msProductType.IsUsingPG
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsProductType(_row.ProductTypeCode, _row.ProductTypeName, _row.ProductCategory, _row.IsUsingPG));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProductType> GetListProductTypeForDDL()
        {
            List<MsProductType> _result = new List<MsProductType>();

            try
            {
                var _query = (
                                from _msProductType in this.db.MsProductTypes
                                where _msProductType.FgActive == 'Y'
                orderby _msProductType.ProductTypeName
                                select new
                                {
                                    ProductTypeCode = _msProductType.ProductTypeCode,
                                    ProductTypeName = _msProductType.ProductTypeName
                                }
                            ).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductTypeCode = this._string, ProductTypeName = this._string });

                    _result.Add(new MsProductType(_row.ProductTypeCode, _row.ProductTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }        

        public List<MsProductType> GetListProductTypeForDDL(string _prmCategory, string _prmKeyword)
        {
            List<MsProductType> _result = new List<MsProductType>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _msProductType in this.db.MsProductTypes
                                where (SqlMethods.Like(_msProductType.ProductTypeCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProductType.ProductTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && _msProductType.FgActive == 'Y'
                                orderby _msProductType.ProductTypeName
                                select new
                                {
                                    ProductTypeCode = _msProductType.ProductTypeCode,
                                    ProductTypeName = _msProductType.ProductTypeName
                                }
                            ).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductTypeCode = this._string, ProductTypeName = this._string });

                    _result.Add(new MsProductType(_row.ProductTypeCode, _row.ProductTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProductType> GetListProductTypeForPhoneSeries()
        {
            List<MsProductType> _result = new List<MsProductType>();

            try
            {
                var _query = (
                                from _msProductType in this.db.MsProductTypes
                                where _msProductType.IsUsingUniqueID == true
                                && _msProductType.FgActive == 'Y'
                                orderby _msProductType.ProductTypeName
                                select new
                                {
                                    ProductTypeCode = _msProductType.ProductTypeCode,
                                    ProductTypeName = _msProductType.ProductTypeName
                                }
                            ).Distinct();

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductTypeCode = this._string, ProductTypeName = this._string });

                    _result.Add(new MsProductType(_row.ProductTypeCode, _row.ProductTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiProductType(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsProductType _msProductType = this.db.MsProductTypes.Single(_temp => _temp.ProductTypeCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    if (_msProductType != null)
                    {
                        var _query = (from _detail in this.db.MsProductTypeDts
                                      where _detail.ProductTypeCode.Trim().ToLower() == _prmCode[i].Trim().ToLower()
                                      select _detail);

                        this.db.MsProductTypeDts.DeleteAllOnSubmit(_query);

                        this.db.MsProductTypes.DeleteOnSubmit(_msProductType);
                    }
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

        public MsProductType GetSingleProductType(string _prmCode)
        {
            MsProductType _result = null;

            try
            {
                _result = this.db.MsProductTypes.Single(_temp => _temp.ProductTypeCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetProductTypeNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msProductType in this.db.MsProductTypes
                                where _msProductType.ProductTypeCode == _prmCode
                                select new
                                {
                                    ProductTypeName = _msProductType.ProductTypeName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ProductTypeName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetProductTypeCodeByProductCode(string _prmProductCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msProductType in this.db.MsProductTypes
                                join _msProduct in this.db.MsProducts
                                    on _msProductType.ProductTypeCode equals _msProduct.ProductType
                                where _msProduct.ProductCode == _prmProductCode
                                select new
                                {
                                    ProductTypeCode = _msProductType.ProductTypeCode
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ProductTypeCode;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetFgStockByCode(string _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _msProductType in this.db.MsProductTypes
                                where _msProductType.ProductTypeCode == _prmCode
                                select new
                                {
                                    FgStock = _msProductType.FgStock
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = ProductTypeDataMapper.IsTrue(_obj.FgStock);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddProductType(MsProductType _prmMsProductType)
        {
            bool _result = false;

            try
            {
                this.db.MsProductTypes.InsertOnSubmit(_prmMsProductType);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditProductType(MsProductType _prmMsProductType)
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

        public bool IsProductTypeExists(String _prmProductType)
        {
            bool _result = false;

            try
            {
                var _query = from _msProductType in this.db.MsProductTypes
                             where _msProductType.ProductTypeCode == _prmProductType
                             select new
                             {
                                 _msProductType.ProductTypeCode
                             };

                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region ProductConvert

        public int RowsCountProductConvert(string _prmTransNo)
        {
            int _result = 0;

            try
            {
                _result = this.db.MsProductConverts.Where(_temp => _temp.ProductCode == _prmTransNo).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<MsProductConvert> GetListProductConvert(int _prmReqPage, int _prmPageSize, string _prmProductCode)
        {
            List<MsProductConvert> _result = new List<MsProductConvert>();

            try
            {
                var _query = (
                                from _msProductConvert in this.db.MsProductConverts
                                where _msProductConvert.ProductCode == _prmProductCode
                                select new
                                {
                                    ProductCode = _msProductConvert.ProductCode,
                                    UnitCode = _msProductConvert.UnitCode,
                                    UnitName = (
                                                    from _msUnit in this.db.MsUnits
                                                    where _msProductConvert.UnitCode == _msUnit.UnitCode
                                                    select _msUnit.UnitName
                                                ).FirstOrDefault(),
                                    UnitConvertCode = _msProductConvert.UnitConvert,
                                    UnitConvertName = (
                                                            from _msUnit2 in this.db.MsUnits
                                                            where _msProductConvert.UnitConvert == _msUnit2.UnitCode
                                                            select _msUnit2.UnitName
                                                        ).FirstOrDefault(),
                                    Rate = _msProductConvert.Rate
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ProductCode = this._string, UnitCode = this._string, UnitName = this._string, UnitConvertCode = this._string, UnitConvertName = this._string, Rate = this._decimal });

                    _result.Add(new MsProductConvert(_row.ProductCode, _row.UnitCode, _row.UnitName, _row.UnitConvertCode, _row.UnitConvertName, _row.Rate));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiProductConvert(string[] _prmUnitCode, string _prmProductCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmUnitCode.Length; i++)
                {
                    MsProductConvert _msProductConvert = this.db.MsProductConverts.Single(_temp => _temp.UnitCode.Trim().ToLower() == _prmUnitCode[i].Trim().ToLower() && _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower());

                    this.db.MsProductConverts.DeleteOnSubmit(_msProductConvert);
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

        public MsProductConvert GetSingleProductConvert(string _prmUnitCode, string _prmProductCode)
        {
            MsProductConvert _result = null;

            try
            {
                _result = this.db.MsProductConverts.Single(_temp => _temp.UnitCode == _prmUnitCode && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddProductConvert(MsProductConvert _prmMsProductConvert)
        {
            bool _result = false;

            try
            {
                this.db.MsProductConverts.InsertOnSubmit(_prmMsProductConvert);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditProductConvert(MsProductConvert _prmMsProductConvert)
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

        public List<MsProductConvert> GetListMsProductConvertForDDL(String _prmProductCode) 
        {
            List<MsProductConvert> _result = new List<MsProductConvert>();

            try
            {
                var _query = (from _msProductConvert in this.db.MsProductConverts
                              where _msProductConvert.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower()
                              select new
                                  {
                                      UnitCode = _msProductConvert.UnitCode,
                                      UnitConvert = _msProductConvert.UnitConvert
                                  }
                              );

                foreach (var _row in _query)
                {
                    _result.Add(new MsProductConvert(_row.UnitCode, _row.UnitConvert));
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }


        #endregion

        #region ProductTypeDt
        public int RowsCountMsProductTypeDt(string _prmCode)
        {
            int _result = 0;

            try
            {
                var _query = (
                                 from _msProductTypeDt in this.db.MsProductTypeDts
                                 where _msProductTypeDt.ProductTypeCode == _prmCode
                                 select _msProductTypeDt.ProductTypeCode
                             ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public List<MsProductTypeDt> GetListMsProductTypeDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<MsProductTypeDt> _result = new List<MsProductTypeDt>();

            try
            {
                var _query = (
                                from _msProductTypeDt in this.db.MsProductTypeDts
                                where _msProductTypeDt.ProductTypeCode == _prmCode
                                orderby _msProductTypeDt.ProductTypeCode ascending
                                select new
                                {
                                    ProductTypeCode = _msProductTypeDt.ProductTypeCode,
                                    WrhsType = _msProductTypeDt.WrhsType,
                                    AccInvent = (
                                                    from _msAccount in this.db.MsAccounts
                                                    where _msProductTypeDt.AccInvent == _msAccount.Account
                                                    select _msAccount.AccountName
                                                 ).FirstOrDefault(),
                                    AccSales = (
                                                    from _msAccount2 in this.db.MsAccounts
                                                    where _msProductTypeDt.AccSales == _msAccount2.Account
                                                    select _msAccount2.AccountName
                                                 ).FirstOrDefault(),
                                    AccTransitSJ = (
                                                    from _msAccount3 in this.db.MsAccounts
                                                    where _msProductTypeDt.AccTransitSJ == _msAccount3.Account
                                                    select _msAccount3.AccountName
                                                 ).FirstOrDefault(),
                                    AccWIP = (
                                                    from _msAccount4 in this.db.MsAccounts
                                                    where _msProductTypeDt.AccWIP == _msAccount4.Account
                                                    select _msAccount4.AccountName
                                                 ).FirstOrDefault(),
                                    AccTransitWrhs = (
                                                    from _msAccount5 in this.db.MsAccounts
                                                    where _msProductTypeDt.AccTransitWrhs == _msAccount5.Account
                                                    select _msAccount5.AccountName
                                                 ).FirstOrDefault(),
                                    AccCOGS = (
                                                    from _msAccount6 in this.db.MsAccounts
                                                    where _msProductTypeDt.AccCOGS == _msAccount6.Account
                                                    select _msAccount6.AccountName
                                                 ).FirstOrDefault(),
                                    AccSRetur = (
                                                   from _msAccount7 in this.db.MsAccounts
                                                   where _msProductTypeDt.AccSRetur == _msAccount7.Account
                                                   select _msAccount7.AccountName
                                                ).FirstOrDefault(),
                                    AccPRetur = (
                                                   from _msAccount8 in this.db.MsAccounts
                                                   where _msProductTypeDt.AccPRetur == _msAccount8.Account
                                                   select _msAccount8.AccountName
                                                ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsProductTypeDt(_row.ProductTypeCode, _row.WrhsType, _row.AccInvent, _row.AccSales, _row.AccTransitSJ, _row.AccWIP, _row.AccTransitWrhs, _row.AccCOGS, _row.AccSRetur, _row.AccPRetur));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsProductTypeDt> GetListMsProductTypeDtSecond(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<MsProductTypeDt> _result = new List<MsProductTypeDt>();

            try
            {
                var _query = (
                                from _msProductTypeDt in this.db.MsProductTypeDts
                                where _msProductTypeDt.ProductTypeCode == _prmCode
                                orderby _msProductTypeDt.ProductTypeCode ascending
                                select new
                                {
                                    ProductTypeCode = _msProductTypeDt.ProductTypeCode,
                                    WrhsType = _msProductTypeDt.WrhsType,
                                    AccInvent = _msProductTypeDt.AccInvent,
                                    AccSales = _msProductTypeDt.AccSales,
                                    AccTransitSJ = _msProductTypeDt.AccTransitSJ,
                                    AccWIP = _msProductTypeDt.AccWIP,
                                    AccTransitWrhs = _msProductTypeDt.AccTransitWrhs,
                                    AccCOGS = _msProductTypeDt.AccCOGS,
                                    AccSRetur = _msProductTypeDt.AccSRetur,
                                    AccPRetur = _msProductTypeDt.AccPRetur,
                                    AccTransitReject = _msProductTypeDt.AccTransitReject
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsProductTypeDt(_row.ProductTypeCode, _row.WrhsType, _row.AccInvent, _row.AccSales, _row.AccTransitSJ, _row.AccWIP, _row.AccTransitWrhs, _row.AccCOGS, _row.AccSRetur, _row.AccPRetur, _row.AccTransitReject));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MsProductTypeDt GetSingleMsProductTypeDt(string _prmCode, byte _prmWrhsType)
        {
            MsProductTypeDt _result = null;

            try
            {
                _result = this.db.MsProductTypeDts.Single(_temp => _temp.ProductTypeCode == _prmCode && _temp.WrhsType == _prmWrhsType);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiMsProductTypeDt(string[] _prmCode, string _prmProdTypeCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsProductTypeDt _msProductTypeDt = this.db.MsProductTypeDts.Single(_temp => _temp.WrhsType == Convert.ToByte(_prmCode[i]) && _temp.ProductTypeCode == _prmProdTypeCode);

                    this.db.MsProductTypeDts.DeleteOnSubmit(_msProductTypeDt);
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

        public bool AddMsProductTypeDt(MsProductTypeDt _prmMsProductTypeDt)
        {
            bool _result = false;

            try
            {
                this.db.MsProductTypeDts.InsertOnSubmit(_prmMsProductTypeDt);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditMsProductTypeDt(MsProductTypeDt _prmMsProductTypeDt)
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

        #region ProductSalesPrice

        public int RowsCountProductSalesPrice(string _prmProductCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.Master_ProductSalesPrices.Where(_temp => _temp.ProductCode == _prmProductCode).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<Master_ProductSalesPrice> GetListProductSalesPrice(int _prmReqPage, int _prmPageSize, string _prmProductCode)
        {
            List<Master_ProductSalesPrice> _result = new List<Master_ProductSalesPrice>();

            try
            {
                var _query = (
                                from _master_ProductSalesPrice in this.db.Master_ProductSalesPrices
                                where _master_ProductSalesPrice.ProductCode == _prmProductCode
                                select new
                                {
                                    ProductCode = _master_ProductSalesPrice.ProductCode,
                                    CurrCode = _master_ProductSalesPrice.CurrCode,
                                    SalesPrice = _master_ProductSalesPrice.SalesPrice,
                                    UnitCode = _master_ProductSalesPrice.UnitCode
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_ProductSalesPrice(_row.ProductCode, _row.CurrCode, _row.UnitCode, _row.SalesPrice));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiProductSalesPrice(string[] _prmCurrCode, string _prmProductCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCurrCode.Length; i++)
                {
                    Master_ProductSalesPrice _master_ProductSalesPrice = this.db.Master_ProductSalesPrices.Single(_temp => _temp.CurrCode.Trim().ToLower() == _prmCurrCode[i].Trim().ToLower() && _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower());

                    this.db.Master_ProductSalesPrices.DeleteOnSubmit(_master_ProductSalesPrice);
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

        public Master_ProductSalesPrice GetSingleProductSalesPrice(string _prmCurrCode, string _prmProductCode)
        {

            Master_ProductSalesPrice _result = null;

            try
            {
                _result = this.db.Master_ProductSalesPrices.Single(_temp => _temp.CurrCode == _prmCurrCode && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddProductSalesPrice(Master_ProductSalesPrice _prmMaster_ProductSalesPrice)
        {
            bool _result = false;

            try
            {
                this.db.Master_ProductSalesPrices.InsertOnSubmit(_prmMaster_ProductSalesPrice);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditProductSalesPrice(Master_ProductSalesPrice _prmMaster_ProductSalesPrice)
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

        public decimal GetProductSalesPrice(string _prmCurrCode, string _prmProductCode)
        {

            decimal _result = 0;

            try
            {
                _result = this.db.MsProducts.Single(_temp => _temp.PurchaseCurr == _prmCurrCode && _temp.ProductCode == _prmProductCode).SellingPrice;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public decimal GetProductSalesPriceForPOS(string _prmProductCode, string _prmCurrCode, string _prmUnitCode)
        {

            decimal _result = 0;

            try
            {
                _result = this.db.Master_ProductSalesPrices.Single(_temp => _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower() && _temp.CurrCode.Trim().ToLower() == _prmCurrCode.Trim().ToLower() && _temp.UnitCode.Trim().ToLower() == _prmUnitCode.Trim().ToLower()).SalesPrice;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddProductSalesPriceList(List<Master_ProductSalesPrice> _prmMsProductSalesPriceList)
        {
            bool _result = false;

            try
            {
                //foreach (Master_ProductSalesPrice _row in _prmMsProductSalesPriceList)
                //{
                //    Master_ProductSalesPrice _msProductSalesPrice = new Master_ProductSalesPrice();

                //    _msProductSalesPrice.ProductCode = _row.ProductCode;
                //    _msProductSalesPrice.CurrCode = _row.CurrCode;
                //    _msProductSalesPrice.SalesPrice = _row.SalesPrice;
                //    _msProductSalesPrice.InsertBy = _row.InsertBy;
                //    _msProductSalesPrice.InsertDate = _row.InsertDate;


                //    this.db.Master_ProductSalesPrices.InsertOnSubmit(_msProductSalesPrice);
                //}
                this.db.Master_ProductSalesPrices.InsertAllOnSubmit(_prmMsProductSalesPriceList);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool GetCheckProductCodeAndCurrCode(string _prmProductCode, string _prmCurrcode)
        {
            bool _result = false;
            try
            {
                var _query = (
                                from _msProductPriceSales in this.db.Master_ProductSalesPrices
                                where _msProductPriceSales.ProductCode == _prmProductCode &&
                                _msProductPriceSales.CurrCode == _prmCurrcode
                                select _msProductPriceSales
                ).Count();
                if (_query == 0)
                {
                    return _result = true;
                }
            }
            catch (Exception ex)
            {                
            }
            return _result;
        }

        #endregion

        #region DefaultProductVal

        public List<Master_DefaultProductValType> GetListDefaultProductVal()
        {
            List<Master_DefaultProductValType> _result = new List<Master_DefaultProductValType>();

            try
            {
                var _query = (
                                from _defaulProdVal in this.db.Master_DefaultProductValTypes
                                select new
                                {
                                    ProductDefaultValNo = _defaulProdVal.ProductDefaultValNo,
                                    ProductValType = _defaulProdVal.ProductValType

                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_DefaultProductValType(_row.ProductDefaultValNo, _row.ProductValType));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditDefaultProductVal(Master_DefaultProductValType _prmMaster_ProductDefaultVal)
        {
            bool _result = false;

            try
            {
                if (this.IsCogsExists() == false)
                {
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsCogsExists()
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _cogs in this.db.StockControl_COGs
                                select new
                                {
                                    Code = _cogs.Code

                                }).Count();
                if (_query > 0)
                {
                    _result = true;
                }
                else
                {
                    _result = false;
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Master_DefaultProductValType GetSingleProductDefaultVal(string _prmCode)
        {
            Master_DefaultProductValType _result = null;

            try
            {
                _result = this.db.Master_DefaultProductValTypes.Single(_temp => _temp.ProductDefaultValNo.ToString() == _prmCode.Trim());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public byte GetProductDefaultValue()
        {
            byte _result = 0;

            try
            {
                var _query = (
                               from _defaulProdVal in this.db.Master_DefaultProductValTypes
                               select _defaulProdVal.ProductValType
                           ).Take(1).FirstOrDefault();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region ProductPhoneType

        public int RowsCountProductPhoneType(string _prmProductCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.MsProduct_PhoneTypes.Where(_temp => _temp.ProductCode == _prmProductCode).Count();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<MsProduct_PhoneType> GetListProductPhoneType(int _prmReqPage, int _prmPageSize, string _prmProductCode)
        {
            List<MsProduct_PhoneType> _result = new List<MsProduct_PhoneType>();

            try
            {
                var _query = (
                                from _msProductPhoneType in this.db.MsProduct_PhoneTypes
                                where _msProductPhoneType.ProductCode == _prmProductCode
                                select new
                                {
                                    ProductCode = _msProductPhoneType.ProductCode,
                                    PhoneTypeCode = _msProductPhoneType.PhoneTypeCode,
                                    PhoneTypeName = (
                                                        from _msPhoneType in this.db.Master_PhoneTypes
                                                        where _msProductPhoneType.PhoneTypeCode == _msPhoneType.PhoneTypeCode
                                                        select _msPhoneType.PhoneTypeDesc
                                                     ).FirstOrDefault()
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct_PhoneType(_row.ProductCode, _row.PhoneTypeCode, _row.PhoneTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiProductPhoneType(string[] _prmPhoneTypeCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmPhoneTypeCode.Length; i++)
                {
                    String[] _tempSplit = _prmPhoneTypeCode[i].Split('&');

                    MsProduct_PhoneType _msProductPhoneType = this.db.MsProduct_PhoneTypes.Single(_temp => _temp.PhoneTypeCode == new Guid(_tempSplit[1]) && _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower());

                    this.db.MsProduct_PhoneTypes.DeleteOnSubmit(_msProductPhoneType);
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

        public MsProduct_PhoneType GetSingleProductPhoneType(string _prmPhoneTypeCode, string _prmProductCode)
        {

            MsProduct_PhoneType _result = null;

            try
            {
                _result = this.db.MsProduct_PhoneTypes.Single(_temp => _temp.PhoneTypeCode == new Guid(_prmPhoneTypeCode) && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddProductPhoneType(MsProduct_PhoneType _prmMsProductPhoneType)
        {
            bool _result = false;

            try
            {
                this.db.MsProduct_PhoneTypes.InsertOnSubmit(_prmMsProductPhoneType);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditProductSalesPrice(MsProduct_PhoneType _prmMsProductPhoneType)
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

        #region COGS

        public Decimal GetStockCOGS(string _prmGimmickCode)
        {
            Decimal _result = 0;

            try
            {
                var _query = (
                                from _cogs in this.db.STCCOGs
                                where _cogs.ProductCode == _prmGimmickCode
                                group _cogs by _cogs.ProductCode into _grp
                                select new
                                {
                                    QtyIn = _grp.Sum(a => a.QtyIn),
                                    QtyOut = _grp.Sum(a => a.QtyOut)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = Convert.ToDecimal((_obj.QtyIn - _obj.QtyOut).ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Decimal GetStockValues(string _prmProductCode)
        {
            Decimal _result = 0;

            try
            {
                var _query = (
                                from _stock in this.db.STCStockValues
                                where _stock.ProductCode == _prmProductCode
                                group _stock by _stock.ProductCode into _grp
                                select new
                                {
                                    Qty = _grp.Sum(a => a.Qty)
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = Convert.ToDecimal((_obj.Qty).ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        #endregion

        #region ProductFormula

        public String GetProductData(String _prmProductCode)
        {
            String _result = "||";
            try
            {
                var _query = (
                        from _product in this.db.MsProducts
                        where _product.ProductCode == _prmProductCode
                        select _product
                    );

                if (_query.Count() > 0)
                {
                    var _rs = _query.FirstOrDefault();
                    _result = _rs.ProductName + "|" + _rs.Unit;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Double RowsCountProductFormula(string _prmCategory, string _prmKeyword)
        {
            Double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query =
                (
                    from _msProductFormula in this.db.STCMsProductFormulas
                    join _msProduct in this.db.MsProducts
                        on _msProductFormula.ProductCodeAssembly equals _msProduct.ProductCode
                    where (SqlMethods.Like(_msProductFormula.ProductCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msProductFormula.ProductCodeAssembly
                ).Distinct().Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<STCMsProductFormula> GetListProductFormula(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<STCMsProductFormula> _result = new List<STCMsProductFormula>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _msProductFormula in this.db.STCMsProductFormulas
                                join _msProduct in this.db.MsProducts
                                    on _msProductFormula.ProductCodeAssembly equals _msProduct.ProductCode
                                where (SqlMethods.Like(_msProductFormula.ProductCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msProduct.ProductName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                select new
                                {
                                    ProductCodeAssembly = _msProductFormula.ProductCodeAssembly,
                                    ProductAssemblyName = _msProduct.ProductName
                                }
                            ).Distinct().Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new STCMsProductFormula(_row.ProductCodeAssembly, _row.ProductAssemblyName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<STCMsProductFormula> GetListProductFormulaDtByProductAssembly(String _prmProductCodeHd)
        {
            List<STCMsProductFormula> _result = new List<STCMsProductFormula>();

            
            try
            {
                var _query = (
                                from _msProductFormula in this.db.STCMsProductFormulas
                                join _msProduct in this.db.MsProducts
                                    on _msProductFormula.ProductCodeAssembly equals _msProduct.ProductCode
                                    where _msProductFormula.ProductCodeAssembly == _prmProductCodeHd
                                
                                select new
                                {
                                    ProductCodeAssembly = _msProductFormula.ProductCodeAssembly,
                                    ProductCode = _msProductFormula.ProductCode,
                                    ProductAssemblyName = _msProduct.ProductName,
                                    Qty = _msProductFormula.Qty,
                                    ProductUnit = _msProductFormula.Unit,
                                    fgDisassembly = _msProductFormula.fgDisassembly,
                                    fgMainProduct = _msProductFormula.fgMainProduct
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new STCMsProductFormula(_row.ProductCodeAssembly, _row.ProductCode, _row.ProductAssemblyName, _row.Qty, _row.ProductUnit, Convert.ToBoolean(_row.fgDisassembly), Convert.ToBoolean(_row.fgMainProduct)));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiProductFormula(string[] _prmProductAssemblyCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmProductAssemblyCode.Length; i++)
                {
                    var _query = (
                                    from _productFormula in this.db.STCMsProductFormulas
                                    where _productFormula.ProductCodeAssembly.Trim().ToLower() == _prmProductAssemblyCode[i].Trim().ToLower()
                                    select _productFormula
                                 );

                    this.db.STCMsProductFormulas.DeleteAllOnSubmit(_query);
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

        public Double RowsCountProductFormulaDt(string _prmProductCode)
        {
            Double _result = 0;

            try
            {
                var _query =
                (
                    from _msProductFormula in this.db.STCMsProductFormulas
                    where _msProductFormula.ProductCode == _prmProductCode
                    select _msProductFormula.ProductCode
                ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<STCMsProductFormula> GetListProductFormulaDt(string _prmProductCode)
        {
            List<STCMsProductFormula> _result = new List<STCMsProductFormula>();

            try
            {
                var _query = (
                                from _msProductFormula in this.db.STCMsProductFormulas
                                where _msProductFormula.ProductCodeAssembly == _prmProductCode
                                select new
                                {
                                    ProductCode = _msProductFormula.ProductCode,
                                    ProductName = (
                                                       from _msProduct in this.db.MsProducts
                                                       where _msProductFormula.ProductCode == _msProduct.ProductCode
                                                       select _msProduct.ProductName
                                                   ).FirstOrDefault(),
                                    ProductCodeAssembly = _msProductFormula.ProductCodeAssembly,
                                    ProductAssemblyName = (
                                                               from _msProduct2 in this.db.MsProducts
                                                               where _msProductFormula.ProductCodeAssembly == _msProduct2.ProductCode
                                                               select _msProduct2.ProductName
                                                           ).FirstOrDefault(),
                                    Qty = _msProductFormula.Qty,
                                    UnitCode = _msProductFormula.Unit,
                                    UnitName = (
                                                    from _msUnit in this.db.MsUnits
                                                    where _msProductFormula.Unit == _msUnit.UnitCode
                                                    select _msUnit.UnitName
                                                ).FirstOrDefault(),
                                    fgDisassembly = _msProductFormula.fgDisassembly,
                                    fgMainProduct = _msProductFormula.fgMainProduct


                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new STCMsProductFormula(_row.ProductCode, _row.ProductName, _row.ProductCodeAssembly, _row.ProductAssemblyName, _row.Qty, _row.UnitCode, _row.UnitName, Convert.ToBoolean(_row.fgDisassembly), Convert.ToBoolean(_row.fgMainProduct)));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiProductFormulaDt(string _prmProductCode, string _prmProductAssemblyCode)
        {
            bool _result = false;

            try
            {
                //for (int i = 0; i < _prmProductCode.Length; i++)
                //{
                STCMsProductFormula _msProductFormula = this.db.STCMsProductFormulas.Single(_temp => _temp.ProductCode.Trim().ToLower() == _prmProductCode.Trim().ToLower() && _temp.ProductCodeAssembly.Trim().ToLower() == _prmProductAssemblyCode.Trim().ToLower());

                this.db.STCMsProductFormulas.DeleteOnSubmit(_msProductFormula);
                //}

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public STCMsProductFormula GetSingleProductFormulaDt(string _prmProductAssemblyCode, string _prmProductCode)
        {
            STCMsProductFormula _result = null;

            try
            {
                _result = this.db.STCMsProductFormulas.Single(_temp => _temp.ProductCodeAssembly == _prmProductAssemblyCode && _temp.ProductCode == _prmProductCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public bool AddProductFormulaDt(STCMsProductFormula _prmMsProductFormula)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        if (this.GetSingleProductFormulaDt(_prmMsProductFormula.ProductCodeAssembly, _prmMsProductFormula.ProductCode) == null)
        //        {
        //            this.db.STCMsProductFormulas.InsertOnSubmit(_prmMsProductFormula);
        //        }
        //        this.db.SubmitChanges();

        //        _result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }

        //    return _result;
        //}

        public Boolean AddProductFormulaDt(String _prmProductCodeAssembly, String _prmDetilTrans)
        {
            Boolean _result = false;

            try
            {
                String[] _detailTransaksi = _prmDetilTrans.Split('^');

                foreach (String _dataDetil in _detailTransaksi)
                {
                    String[] _rowData = _dataDetil.Split('|');

                    if (this.GetSingleProductFormulaDt(_prmProductCodeAssembly, _rowData[1].Split('-')[0].Trim()) == null)
                    {
                        STCMsProductFormula _stcMsProductFormula = new STCMsProductFormula();

                        _stcMsProductFormula.ProductCodeAssembly = _prmProductCodeAssembly;
                        _stcMsProductFormula.ProductCode = _rowData[1].Split('-')[0].Trim();
                        _stcMsProductFormula.Qty = Convert.ToDecimal(_rowData[2]);
                        _stcMsProductFormula.Unit = _rowData[3];
                        _stcMsProductFormula.CreatedBy = HttpContext.Current.User.Identity.Name;
                        _stcMsProductFormula.CreatedDate = DateTime.Now;
                        _stcMsProductFormula.EditBy = HttpContext.Current.User.Identity.Name;
                        _stcMsProductFormula.EditDate = DateTime.Now;

                        this.db.STCMsProductFormulas.InsertOnSubmit(_stcMsProductFormula);
                    }
                }

                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public bool AddProductFormulaDetail(STCMsProductFormula _prmSTCMsProductFormula)
        {
            bool _result = false;

            try
            {
                this.db.STCMsProductFormulas.InsertOnSubmit(_prmSTCMsProductFormula);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditProductFormulaDt(STCMsProductFormula _prmMsProductFormula)
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

        #region Product Price Group

        public String GetPriceGroupCodeByProductCode(String _prmProductCode)
        {
            String _result = "";
            try
            {
                MsProduct _product = this.db.MsProducts.Single(a => a.ProductCode == _prmProductCode);
                _result = _product.PriceGroupCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        #endregion

        #region Alternatif

        public double RowsCountProductAlternatif(string _prmProductCode)
        {

            double _result = 0;

            //string _pattern1 = "%%";
            //string _pattern2 = "%%";

            //if (_prmCategory == "Code")
            //{
            //    _pattern1 = "%" + _prmKeyword + "%";
            //    _pattern2 = "%%";

            //}
            //else if (_prmCategory == "Name")
            //{
            //    _pattern2 = "%" + _prmKeyword + "%";
            //    _pattern1 = "%%";
            //}

            var _query =
                (
                    from _msProductAlternatif in this.db.MsProduct_Alternatifs
                    where _msProductAlternatif.ProductCode == _prmProductCode
                    //&& (SqlMethods.Like(_msProductAlternatif.ProductCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    //&& (SqlMethods.Like(_msProductAlternatif.AlternatifCode.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    orderby _msProductAlternatif.AlternatifCode ascending
                    select _msProductAlternatif

                ).Count();

            _result = _query;
            return _result;
        }

        public List<MsProduct_Alternatif> GetListProductAlternatif(String _prmProductCode, int _prmReqPage, int _prmPageSize)
        {
            List<MsProduct_Alternatif> _result = new List<MsProduct_Alternatif>();
            //string _pattern1 = "%%";
            //string _pattern2 = "%%";

            //if (_prmCategory == "Code")
            //{
            //    _pattern1 = "%" + _prmKeyword + "%";
            //    _pattern2 = "%%";

            //}
            //else if (_prmCategory == "Name")
            //{
            //    _pattern2 = "%" + _prmKeyword + "%";
            //    _pattern1 = "%%";
            //}
            try
            {
                var _query = (
                                from _msProductAlternatif in this.db.MsProduct_Alternatifs
                                where _msProductAlternatif.ProductCode == _prmProductCode
                                //&& (SqlMethods.Like(_msProductAlternatif.ProductCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                //&& (SqlMethods.Like(_msProductAlternatif.AlternatifCode.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msProductAlternatif.AlternatifCode ascending
                                select _msProductAlternatif
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

        public bool DeleteMultiProductAlternatif(string[] _prmCurrCode, string _prmProductCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCurrCode.Length; i++)
                {
                    string[] _tempSplit = _prmCurrCode[i].Split('|');

                    MsProduct_Alternatif _msProductAlternatif = this.db.MsProduct_Alternatifs.Single(_temp => _temp.ProductCode.Trim().ToLower() == _tempSplit[0].Trim().ToLower() && _temp.AlternatifCode.Trim().ToLower() == _tempSplit[1].Trim().ToLower());

                    this.db.MsProduct_Alternatifs.DeleteOnSubmit(_msProductAlternatif);
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

        public List<MsProduct> GetListProductAlternatifForDDL(String _prmProductCode)
        {
            List<MsProduct> _result = new List<MsProduct>();

            try
            {
                var _query = (
                                from _msProduct in this.db.MsProducts
                                where _msProduct.ProductCode != _prmProductCode
                                && !(from _msProductAlternatif in this.db.MsProduct_Alternatifs
                                     where _msProductAlternatif.ProductCode == _prmProductCode
                                     select _msProductAlternatif.AlternatifCode
                                    ).Contains(_msProduct.ProductCode)
                                select new
                                {
                                    ProductCode = _msProduct.ProductCode,
                                    ProductName = _msProduct.ProductName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsProduct(_row.ProductCode, _row.ProductName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddProductAlternatif(MsProduct_Alternatif _prmMsProduct_Alternatif)
        {
            bool _result = false;

            try
            {
                this.db.MsProduct_Alternatifs.InsertOnSubmit(_prmMsProduct_Alternatif);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public MsProduct_Alternatif GetSingleProductAlternatif(string _prmProductCode, String _prmProductAlternatifCode)
        {
            MsProduct_Alternatif _result = null;

            try
            {
                _result = this.db.MsProduct_Alternatifs.Single(_temp => _temp.ProductCode == _prmProductCode && _temp.AlternatifCode == _prmProductAlternatifCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditProductAlternatif(MsProduct_Alternatif _prmMsProductAlternatif)
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

        public String GetProductName(String _prmAlternatifCode)
        {
            String _result = "";

            try
            {
                _result = this.db.MsProducts.Single(_temp => _temp.ProductCode == _prmAlternatifCode).ProductName;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        public List<V_StockTransType> GetListViewStockTransType(String _prmCategory,String _prmKeyword) 
        {
            List<V_StockTransType> _result = new List<V_StockTransType>();

            string _pattern1 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
           
            try
            {
                var _query = (from _viewStockTransType in this.db.V_StockTransTypes
                              where (SqlMethods.Like(_viewStockTransType.TransTypeName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                              select new 
                              { 
                                    TransType = _viewStockTransType.TransType,
                                    TransTypeName = _viewStockTransType.TransTypeName
                              }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new V_StockTransType(_row.TransType, _row.TransTypeName));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public List<MsTaxType> GetTaxDDL()
        {
            List<MsTaxType> _result = new List<MsTaxType>();

            try
            {
                var _query = (
                                from _msTaxType in this.db.MsTaxTypes
                                where _msTaxType.fgActive == true
                                select new
                                {
                                    TaxTypeCode = _msTaxType.TaxTypeCode,
                                    TaxTypeName = _msTaxType.TaxTypeName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsTaxType(_row.TaxTypeCode, _row.TaxTypeName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Decimal GetSingleTaxPercent(String _prmTax)
        {
            Decimal _result = 0;

            try
            {
                var _query = (from _taxType in this.db.MsTaxTypes 
                              where _taxType.TaxTypeCode.Trim().ToLower() == _prmTax 
                              select _taxType.DefaultValue
                    ).FirstOrDefault();

                _result = Convert.ToDecimal(_query);

            }
            catch (Exception ex)
            {   
                throw ex;
            }

            return _result;
        }

        public String GetSingleTaxTypeName(String _prmTaxTypeCode)
        {
            String _result = "";

            try
            {
                var _query = (from _taxType in this.db.MsTaxTypes
                              where _taxType.TaxTypeCode.Trim().ToLower() == _prmTaxTypeCode
                              && _taxType.fgActive == true
                              select _taxType.TaxTypeName
                    ).FirstOrDefault();

                _result = _query;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        ~ProductBL()
        {
        }
    }
}
