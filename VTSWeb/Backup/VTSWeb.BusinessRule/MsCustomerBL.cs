using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.Linq.SqlClient;
using VTSWeb.Database;
using VTSWeb.SystemConfig;

namespace VTSWeb.BusinessRule
{
    public sealed class MsCustomerBL : Base
    {

        public MsCustomerBL()
        {
        }
        ~MsCustomerBL()
        {
        }

        #region Customer
        public int RowsCount(String _prmCompany, String _prmKeyword)
        {
            int _result = 0;

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmCompany == "CustCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmCompany == "CustTypeName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result = (from _msCustomer in this.db.MsCustomers
                       join _msCustType in this.db.MsCustTypes
                       on _msCustomer.CustType equals _msCustType.CustTypeCode
                       where  (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msCustType.CustTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))

                       select _msCustomer.CustCode).Count();
            return _result;
        }

        public List<MsCustomer> GetList(int _prmReqPage, int _prmPageSize, String _prmCompany, String _prmKeyword)
        {
            List<MsCustomer> _result = new List<MsCustomer>();

            String _pattern1 = "%%";
            String _pattern2 = "%%";

            if (_prmCompany == "CustCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmCompany == "CustTypeName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                               from _msCustomer in this.db.MsCustomers
                               join _msCustType in this.db.MsCustTypes
                               on _msCustomer.CustType equals _msCustType.CustTypeCode
                               where (SqlMethods.Like(_msCustomer.CustName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                  && (SqlMethods.Like(_msCustType.CustTypeName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                               orderby _msCustomer.CustName ascending
                               select new
                                {
                                    CustCode = _msCustomer.CustCode,
                                    CustName = _msCustomer.CustName,
                                    CustTypeName = _msCustType.CustTypeName,
                                    Address1 = _msCustomer.Address1,
                                    Address2 = _msCustomer.Address2,
                                    ZipCode = _msCustomer.ZipCode,
                                    Phone = _msCustomer.Phone,
                                    Fax = _msCustomer.Fax,
                                    Email = _msCustomer.Email,
                                    FgActive = _msCustomer.FgActive,
                                    ContactName = _msCustomer.ContactName,
                                    ContactTitle = _msCustomer.ContactTitle,
                                    ContactHp = _msCustomer.ContactHP,
                                    ContactEmail = _msCustomer.ContactEmail,

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustomer(_row.CustCode, _row.CustName, _row.CustTypeName, _row.Address1, _row.Address2, _row.ZipCode,
                        _row.Phone, _row.Fax, _row.Email, _row.FgActive, _row.ContactName, _row.ContactTitle, _row.ContactHp, _row.ContactEmail));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public List<MsCustomer> GetList()
        {
            List<MsCustomer> _result = new List<MsCustomer>();

            try
            {
                var _query = (
                                from _msCustomer in this.db.MsCustomers
                                join _msCustType in this.db.MsCustTypes
                                on _msCustomer.CustType equals _msCustType.CustTypeCode
                                orderby _msCustomer.CustName ascending
                                select new
                                {
                                    CustCode = _msCustomer.CustCode,
                                    CustName = _msCustomer.CustName,
                                    CustTypeName = _msCustType.CustTypeName,
                                    Address1 = _msCustomer.Address1,
                                    Address2 = _msCustomer.Address2,
                                    ZipCode = _msCustomer.ZipCode,
                                    Phone = _msCustomer.Phone,
                                    Fax = _msCustomer.Fax,
                                    Email = _msCustomer.Email,
                                    FgActive = _msCustomer.FgActive,
                                    ContactName = _msCustomer.ContactName,
                                    ContactTitle = _msCustomer.ContactTitle,
                                    ContactHp = _msCustomer.ContactHP,
                                    ContactEmail = _msCustomer.ContactEmail

                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustomer(_row.CustCode, _row.CustName, _row.CustTypeName, _row.Address1, _row.Address2, _row.ZipCode,
                        _row.Phone, _row.Fax, _row.Email, Convert.ToChar(_row.FgActive), _row.ContactName, _row.ContactTitle, _row.ContactHp, _row.ContactEmail));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public MsCustomer GetSingle(String _prmCode)
        {
            MsCustomer _result = null;

            try
            {
                _result = this.db.MsCustomers.Single(_temp => _temp.CustCode == _prmCode);
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public String GetCustomerNameByCode(String _prmCode)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msCustomer in this.db.MsCustomers
                                where _msCustomer.CustCode == _prmCode
                                select new
                                {
                                    CustomerName = _msCustomer.CustName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CustomerName;
                }
            }
            catch (Exception)
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
                    MsCustomer _msCustomer = this.db.MsCustomers.Single(_temp => _temp.CustCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsCustomers.DeleteOnSubmit(_msCustomer);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Add(MsCustomer _prmMsCustomer)
        {
            bool _result = false;

            try
            {
                this.db.MsCustomers.InsertOnSubmit(_prmMsCustomer);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Edit(MsCustomer _prmMsArea)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }
        public List<MsCustomer> GetCustomerForDDL()
        {
            List<MsCustomer> _result = new List<MsCustomer>();

            try
            {
                var _query = (
                                from _msCustomer in this.db.MsCustomers
                                //where _msRegional.RegionalCode == _prmCode
                                select new
                                {
                                    CustCode = _msCustomer.CustCode,
                                    CustName = _msCustomer.CustName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustomer(_row.CustCode, _row.CustName));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }
        #endregion



    }
}
