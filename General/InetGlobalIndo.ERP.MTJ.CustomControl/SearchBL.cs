using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Collections;

namespace InetGlobalIndo.ERP.MTJ.CustomControl
{
    public class SearchBL : Base
    {
        public SearchBL() { }

        public List<String> getFieldList(String _prmSearchCode)
        {
            List<String> _result = new List<String>();
            try
            {
                var _queryFieldList = (
                        from _fieldList in this.db.SearchFields
                        where _fieldList.SearchCode == _prmSearchCode
                        select _fieldList
                    );
                foreach (var _rsFieldList in _queryFieldList)
                    _result.Add(_rsFieldList.FieldName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<SearchField> getFieldList(String _prmSearchCode, Boolean FgWithLabel) {
            List<SearchField> _result = new List<SearchField>();
            try
            {
                var _queryFieldList = (
                        from _fieldList in this.db.SearchFields
                        where _fieldList.SearchCode == _prmSearchCode
                        select _fieldList
                    );
                foreach (var _row in _queryFieldList)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return _result;
        }

        public List<SearchField> getFieldCondition(String _prmSearchCode)
        {
            List<SearchField> _result = new List<SearchField>();
            try
            {
                var _queryField = (
                        from _fieldList in this.db.SearchFields
                        where _fieldList.SearchCode == _prmSearchCode
                        select _fieldList
                    );
                foreach (var _rsField in _queryField)
                    _result.Add(_rsField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<String> getSearchDataResult(String _prmSearchCode, String _prmWhereCondition, String _prmOrderField, Boolean _prmDescending, String _prmParamWhere)
        {
            List<String> _result = new List<String>();
            try
            {
                var _tempQrySelectExpression = (
                        from _selectExpression in this.db.SearchConfigs
                        where _selectExpression.SearchCode == _prmSearchCode
                        select _selectExpression
                    ).FirstOrDefault();
                var _qrySelectExpression = _tempQrySelectExpression.SelectExpression;
                _qrySelectExpression += _prmWhereCondition;
                if (_tempQrySelectExpression.AdditionalWhere != null)
                {
                    if (_prmWhereCondition != "")
                    {
                        _qrySelectExpression += " AND " + _tempQrySelectExpression.AdditionalWhere;
                    }
                    else
                    {
                        _qrySelectExpression += " WHERE " + _tempQrySelectExpression.AdditionalWhere;
                    }
                    if (_prmParamWhere != "")
                        _qrySelectExpression += " AND " + _prmParamWhere;
                }
                else
                {
                    if (_prmWhereCondition != "")
                    {
                        if (_prmParamWhere != "")
                            _qrySelectExpression += " AND " + _prmParamWhere;
                    }
                    else
                    {
                        if (_prmParamWhere != "")
                            _qrySelectExpression += " WHERE " + _prmParamWhere;
                    }
                }

                if (_prmOrderField != "")
                    _qrySelectExpression += " ORDER BY " + _prmOrderField;
                if (_prmDescending)
                    _qrySelectExpression += " DESC";
                var _searchDataResult = (
                        from _dynamicSearch in this.db.dynamicSearch(_qrySelectExpression)
                        select _dynamicSearch.ResultRow
                    );
                foreach (var _rsDataResult in _searchDataResult)
                {
                    String _hasil = (_rsDataResult.Replace("\r\n", " ")).ToString();
                    _result.Add(_hasil);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }


        public String getProductData(String _prmProductCode)
        {
            String _result = "";
            try
            {
                var _query = (
                        from _productData in this.db.MsProducts
                        where _productData.ProductCode == _prmProductCode
                        select _productData
                    );
                if (_query.Count() > 0)
                {
                    var _rs = _query.FirstOrDefault();
                    _result = _rs.ProductName;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }


        ~SearchBL() { }
    }
}
