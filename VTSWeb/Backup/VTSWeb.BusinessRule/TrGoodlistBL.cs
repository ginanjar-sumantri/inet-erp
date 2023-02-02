using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.Data.Linq.SqlClient;
using VTSWeb.SystemConfig;
using VTSWeb.Database;


namespace VTSWeb.BusinessRule
{
    public sealed class TrGoodListBL : Base
    {

        public TrGoodListBL()
        {
        }
        ~TrGoodListBL()
        {
        }

        #region TrGoodList
        public int RowsCount
        {
            get
            {
                return this.db.TrGoodLists.Count();
            }
        }

        public List<TrGoodList> GetList(int _prmReqPage, int _prmPageSize)
        {
            List<TrGoodList> _result = new List<TrGoodList>();

            try
            {
                var _query = (
                                from _TrGoodList in this.db.TrGoodLists
                                orderby _TrGoodList.CustCode, _TrGoodList.ItemCode  ascending
                                select new
                                {
                                    CustCode = _TrGoodList.CustCode,
                                    ItemCode = _TrGoodList.ItemCode,
                                    ProductName = _TrGoodList.ProductName,
                                    SerialNumber = _TrGoodList.SerialNumber,
                                    Remark = _TrGoodList.Remark,
                                    ElectriCityNumerik = _TrGoodList.ElectriCityNumerik

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new TrGoodList(_row.CustCode, _row.ItemCode, _row.ProductName, _row.SerialNumber, _row.Remark, _row.ElectriCityNumerik));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public List<TrGoodList> GetList(String _prmCustCode, string _prmItemCode)
        {
            List<TrGoodList> _result = new List<TrGoodList>();
            String _pattern = "%%";

            if (_prmItemCode != "")
            {
                _pattern = "%" + _prmItemCode + "%";
            }

            try
            {
                var _query = (
                                from _TrGoodList in this.db.TrGoodLists
                                where
                                _TrGoodList.CustCode == _prmCustCode &&
                                (SqlMethods.Like(_TrGoodList.ItemCode.ToString().Trim().ToLower(), _pattern.Trim().ToLower()))
                                orderby _TrGoodList.CustCode, _TrGoodList.ItemCode ascending
                                select new
                                {
                                    CustCode = _TrGoodList.CustCode,
                                    ItemCode = _TrGoodList.ItemCode,
                                    ProductName = _TrGoodList.ProductName,
                                    SerialNumber = _TrGoodList.SerialNumber,
                                    Remark = _TrGoodList.Remark,
                                    ElectriCityNumerik = _TrGoodList.ElectriCityNumerik

                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new TrGoodList(_row.CustCode, _row.ItemCode, _row.ProductName, _row.SerialNumber, _row.Remark, _row.ElectriCityNumerik));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }
        public TrGoodList GetSingle(string _prmCode)
        {
            TrGoodList _result = null;

            try
            {
                String[] _tempSplit = _prmCode.Split('-');
                _result = this.db.TrGoodLists.Single(_temp => _temp.CustCode.ToLower() == _tempSplit[0].Trim().ToLower() && _temp.ItemCode.ToString() == _tempSplit[1].Trim().ToLower());
            }
            catch (Exception)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        public List<TrGoodList> GetItemCodeDDLForGoodsInOut(String _prmCustCode)
        {
            List<TrGoodList> _result = new List<TrGoodList>();

            try
            {
                var _query = (
                                 from _TrGoodList in this.db.TrGoodLists
                                 where _TrGoodList.CustCode == _prmCustCode
                                 select new
                                 {
                                     CustCode = _TrGoodList.CustCode,
                                     ItemCode = _TrGoodList.ItemCode
                                 }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new TrGoodList(_row.CustCode, _row.ItemCode));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool DeleteMulti(String[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _tempSplit = _prmCode[i].Split('-');
                    TrGoodList _TrGoodList = this.db.TrGoodLists.Single(_temp => _temp.CustCode.ToString() == _tempSplit[0].Trim().ToLower() && _temp.ItemCode.ToString() == _tempSplit[1].Trim().ToLower());

                    this.db.TrGoodLists.DeleteOnSubmit(_TrGoodList);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Add(List<TrGoodList> _prmTrGoodList)
        {
            bool _result = false;

            try
            {
                this.db.TrGoodLists.InsertAllOnSubmit(_prmTrGoodList);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        #endregion
    }
}
