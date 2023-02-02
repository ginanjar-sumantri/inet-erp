using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using VTSWeb.Database;
using System.Diagnostics;
using System.Data.SqlClient;
using VTSWeb.SystemConfig;
using System.Data;
using System.Data.Linq.SqlClient;

namespace VTSWeb.BusinessRule
{
    public sealed class RackCustomerBL : Base
    {
        public RackCustomerBL()
        {
        }
        ~RackCustomerBL()
        {
        }

        #region RackCustomer
        public int RowsCount(String _prmCustCode)
        {
            int _result = 0;

            String _pattern = "%%";

            if (_prmCustCode != "")
            {
                _pattern = "%" + _prmCustCode + "%";
            }

            _result = (from _msRackCustomer in this.db.MsRack_Customers
                       join _msCust in this.db.MsCustomers
                       on _msRackCustomer.CustCode equals _msCust.CustCode
                       where
                      (SqlMethods.Like(_msCust.CustCode.ToString().Trim().ToLower(), _pattern.Trim().ToLower()))

                       select _msRackCustomer.CustCode).Count();
            return _result;
        }

        public List<MsRack_Customer> GetList(int _prmReqPage, int _prmPageSize, String _prmCustCode)
        {
            List<MsRack_Customer> _result = new List<MsRack_Customer>();

            String _pattern = "%%";

            if (_prmCustCode != "")
            {
                _pattern = "%" + _prmCustCode + "%";
            }
            try
            {
                var _query = (
                                from _msRackCustomers in this.db.MsRack_Customers
                                join _msCustomer in this.db.MsCustomers
                                on _msRackCustomers.CustCode equals _msCustomer.CustCode
                                where (SqlMethods.Like(_msCustomer.CustCode.ToString().Trim().ToLower(), _pattern.Trim().ToLower()))
                                orderby _msCustomer.CustName
                                select new
                                {
                                    CustName = (from _msCust in this.db.MsCustomers
                                                where _msRackCustomers.CustCode == _msCust.CustCode
                                                select _msCust.CustName).FirstOrDefault(),

                                    RackServerName = (from _msRackServer in this.db.MsRackServers
                                                      where _msRackCustomers.RackCode == _msRackServer.RackCode
                                                select _msRackServer.RackName).FirstOrDefault(),

                                    RackCustomerCode = _msRackCustomers.RackCustomerCode

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsRack_Customer(_row.CustName, _row.RackServerName, _row.RackCustomerCode));
                }
            }
            catch (Exception ex)
            {

            }

            return _result;
        }
        public MsRack_Customer GetSingle(String _prmCode)
        {
            MsRack_Customer _result = null;

            try
            {
                _result = this.db.MsRack_Customers.Single(_temp => _temp.RackCustomerCode.ToString() == _prmCode);
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool AddRackCustomer(MsRack_Customer _prmMsRack_Customer)
        {
            bool _result = false;

            try
            {
                this.db.MsRack_Customers.InsertOnSubmit(_prmMsRack_Customer);
                this.db.SubmitChanges();

                _result = true;
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
                    MsRack_Customer _msRackServer = this.db.MsRack_Customers.Single(_temp => _temp.RackCustomerCode.ToString().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsRack_Customers.DeleteOnSubmit(_msRackServer);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }
        public bool Edit(MsRack_Customer _msRackServer)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        public bool DeleteCustContactRackServer(string _prmVisitorRackServerCode)
        {
            bool _result = false;

            try
            {
                MsRack_Customer _msRackServer = this.db.MsRack_Customers.Single(_temp => _temp.RackCustomerCode.ToString().Trim().ToLower() == _prmVisitorRackServerCode.Trim().ToLower());

                this.db.MsRack_Customers.DeleteOnSubmit(_msRackServer);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }



        public List<MsRack_Customer> GetListRackServerForClearance(string _prmCustCode)
        {
            //string _patternRackServerName = "%" + _prmRackServerName + "%";

            List<MsRack_Customer> _result = new List<MsRack_Customer>();

            try
            {
                var _query = (
                                from _msRackCustomer in this.db.MsRack_Customers
                                join _msRackServer in this.db.MsRackServers
                                    on _msRackCustomer.RackCode equals _msRackServer.RackCode
                                where _msRackCustomer.CustCode == _prmCustCode
                                //orderby _msRackServer.RackServerCode, _msRackServer.RackServerName ascending
                                select new
                                {
                                    CustCode = _msRackCustomer.CustCode,
                                    RackCode = _msRackCustomer.RackCode,
                                    RackName = _msRackServer.RackName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsRack_Customer(_row.CustCode, _row.RackCode, _row.RackName));
                }
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public List<MsRack_Customer> GetRackServerDDLForClearance(String _prmCustCode, String _prmCustName)
        //{
        //    List<MsRack_Customer> _result = new List<MsRack_Customer>();

        //    try
        //    {
        //        var _query = (
        //                         from _msRackCustomer in this.db.MsRack_Customers
        //                         join _msRackServer in this.db.MsRackServers
        //                         on _msRackCustomer.RackCode equals _msRackServer.RackCode
        //                         where _msRackServer.CustCode == _prmCustCode
        //                            && _msRackServer.ItemNo == Convert.ToInt32(_prmCustName)
        //                         select new
        //                         {
        //                             RackServerCode = _msRackServer.RackServerCode,
        //                             RackServerName = _msRackServer.RackServerName
        //                         }
        //                    ).Distinct();

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsRack_Customer(_row.RackServerCode, _row.RackServerName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return _result;
        //}
        //public List<MsRack_Customer> GetRackServerDDLForClearanceHd(String _prmCustCode)
        //{
        //    List<MsRack_Customer> _result = new List<MsRack_Customer>();

        //    try
        //    {
        //        var _query = (
        //                         from _msRackServer in this.db.MsRack_Customers
        //                         join _msRackServer in this.db.MsRackServers
        //                         on _msRackServer.RackServerCode equals _msRackServer.RackServerCode
        //                         where _msRackServer.CustCode == _prmCustCode
        //                         select new
        //                         {
        //                             RackServerCode = _msRackServer.RackServerCode,
        //                             RackServerName = _msRackServer.RackServerName
        //                         }
        //                    ).Distinct();

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new MsRack_Customer(_row.RackServerCode, _row.RackServerName));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return _result;
        //}
        //public bool IsCustContactRackServerExist(string _prmCustCode, int _prmVisitorCode, string _prmRackServerCode)
        //{
        //    bool _result = false;

        //    try
        //    {
        //        var _query = (
        //                        from _custContactRackServer in this.db.MsRack_Customers
        //                        where _custContactRackServer.CustCode == _prmCustCode
        //                           && _custContactRackServer.ItemNo == _prmVisitorCode
        //                           && _custContactRackServer.RackServerCode == _prmRackServerCode
        //                        select new
        //                        {
        //                            VisitorRackServerCode = _custContactRackServer.VisitorRackServerCode
        //                        }
        //                      ).Count();

        //        if (_query > 0)
        //        {
        //            _result = true;
        //        }
        //        else
        //        {
        //            _result = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return _result;
        //}

        #endregion

    }
}
