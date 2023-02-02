using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Transactions;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;
using System.Collections;
using System.Data.Linq.SqlClient;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Sales
{
    public class CustSellingPriceBL : Base
    {
        public CustSellingPriceBL() { }

        public double RowsCountSellingPrice(String _prmCustomerCode, String _prmProductCode)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            _pattern1 = "%" + _prmCustomerCode + "%";
            _pattern2 = "%" + _prmProductCode + "%";

            var _query =
                        (
                            from _salSellingPrice in this.db.SALCustSellingPrices
                            where (SqlMethods.Like(_salSellingPrice.CustCode.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                               && (SqlMethods.Like(_salSellingPrice.ProductCode.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                            select _salSellingPrice.CustCode
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<SALCustSellingPrice> getSellingPrice(int _prmReqPage, int _prmPageSize, String _prmCustomerCode, String _prmProductCode, String _prmOrderBy, Boolean _prmAscDesc)
        {
            List<SALCustSellingPrice> _result = new List<SALCustSellingPrice>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            _pattern1 = "%" + _prmCustomerCode + "%";
            _pattern2 = "%" + _prmProductCode + "%";

            try
            {
                var _query1 = (
                        from _salSellingPrice in this.db.SALCustSellingPrices

                        where (SqlMethods.Like(_salSellingPrice.CustCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        && (SqlMethods.Like(_salSellingPrice.ProductCode.Trim().ToLower(), _pattern2.Trim().ToLower()))
                        select new {
                            CustCode = _salSellingPrice.CustCode ,
                            ProductCode = _salSellingPrice.ProductCode ,
                            TransNmbr = _salSellingPrice.TransNmbr ,
                            TransDate = _salSellingPrice.TransDate ,
                            UnitCode = _salSellingPrice.UnitCode ,
                            CurrCode = _salSellingPrice.CurrCode,
                            AmountForex = _salSellingPrice.AmountForex 
                        }
                    );

                if (_prmOrderBy == "Customer Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.CustCode)) : (_query1.OrderByDescending(a => a.CustCode));
                if (_prmOrderBy == "Product Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.ProductCode)) : (_query1.OrderByDescending(a => a.ProductCode));
                if (_prmOrderBy == "Trans Number")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TransNmbr)) : (_query1.OrderByDescending(a => a.TransNmbr));
                if (_prmOrderBy == "Trans Date")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.TransDate)) : (_query1.OrderByDescending(a => a.TransDate));
                if (_prmOrderBy == "Unit Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.UnitCode)) : (_query1.OrderByDescending(a => a.UnitCode));
                if (_prmOrderBy == "Curr Code")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.CurrCode)) : (_query1.OrderByDescending(a => a.CurrCode));
                if (_prmOrderBy == "Amount Forex")
                    _query1 = _prmAscDesc ? (_query1.OrderBy(a => a.AmountForex)) : (_query1.OrderByDescending(a => a.AmountForex));

                var _query = _query1.Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);


                foreach (var _row in _query)
                {
                    _result.Add(new SALCustSellingPrice(_row.CustCode, _row.ProductCode , _row.TransNmbr ,_row.TransDate, _row.UnitCode , _row.CurrCode , _row.AmountForex));
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        ~CustSellingPriceBL() { }
    }
}
