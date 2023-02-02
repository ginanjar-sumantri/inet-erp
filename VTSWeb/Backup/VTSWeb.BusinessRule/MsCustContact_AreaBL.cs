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
    public sealed class MsCustContact_AreaBL : Base
    {
        public MsCustContact_AreaBL()
        {
        }
        ~MsCustContact_AreaBL()
        {
        }

        #region CustContact_Area
        public int RowsCount(String _prmCustCode)
        {
            int _result = 0;

            String _pattern = "%%";

            if (_prmCustCode != "")
            {
                _pattern = "%" + _prmCustCode + "%";
            }

            _result = (from _msCustContactArea in this.db.MsCustContact_MsAreas
                       join _msCust in this.db.MsCustomers
                       on _msCustContactArea.CustCode equals _msCust.CustCode
                       where
                      (SqlMethods.Like(_msCust.CustCode.ToString().Trim().ToLower(), _pattern.Trim().ToLower()))

                       select _msCustContactArea.CustCode).Count();
            return _result;
        }

        public List<MsCustContact_MsArea> GetList(int _prmReqPage, int _prmPageSize, String _prmCustCode)
        {
            List<MsCustContact_MsArea> _result = new List<MsCustContact_MsArea>();
            
            String _pattern = "%%";

            if (_prmCustCode != "")
            {
                _pattern = "%" + _prmCustCode + "%";
            }
            try
            {
                var _query = (
                                from _msCustContactArea in this.db.MsCustContact_MsAreas
                                join _msCustomer in this.db.MsCustomers
                                on _msCustContactArea.CustCode equals _msCustomer.CustCode
                                where (SqlMethods.Like(_msCustomer.CustCode.ToString().Trim().ToLower(), _pattern.Trim().ToLower()))

                                orderby _msCustomer.CustName, _msCustContactArea.ItemNo ascending
                                select new
                                {
                                    ContactName = (from _msCustContact in this.db.MsCustContacts
                                              where _msCustContactArea.CustCode == _msCustContact.CustCode
                                              && _msCustContactArea.ItemNo == _msCustContact.ItemNo
                                              select _msCustContact.ContactName).FirstOrDefault(),

                                    CustName = (from _msCust in this.db.MsCustomers
                                                where _msCustContactArea.CustCode == _msCust.CustCode
                                                select _msCust.CustName).FirstOrDefault(),

                                    AreaName = (from _msArea in this.db.MsAreas
                                                where _msCustContactArea.AreaCode == _msArea.AreaCode
                                                select _msArea.AreaName).FirstOrDefault(),

                                    VisitorAreaCode = _msCustContactArea.VisitorAreaCode

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustContact_MsArea(_row.ContactName, _row.CustName, _row.AreaName, _row.VisitorAreaCode));
                }
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public List<MsCustContact_MsArea> GetListAreaForClearance(string _prmAreaName, string _prmCustCode, int _prmItemCode)
        {
            string _patternAreaName = "%" + _prmAreaName + "%";

            List<MsCustContact_MsArea> _result = new List<MsCustContact_MsArea>();

            try
            {
                var _query = (
                                from _msCustContactArea in this.db.MsCustContact_MsAreas
                                join _msArea in this.db.MsAreas
                                    on _msCustContactArea.AreaCode equals _msArea.AreaCode
                                where (SqlMethods.Like(_msArea.AreaName.Trim().ToLower(), _patternAreaName))
                                   && _msCustContactArea.CustCode == _prmCustCode
                                   && _msCustContactArea.ItemNo == _prmItemCode
                                //orderby _msCustContactArea.AreaCode, _msCustContactArea.AreaName ascending
                                select new
                                {
                                    CustCode = _msCustContactArea.CustCode,
                                    ItemNo = _msCustContactArea.ItemNo,
                                    AreaCode = _msCustContactArea.AreaCode,
                                    AreaName = _msArea.AreaName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustContact_MsArea(_row.CustCode, _row.ItemNo, _row.AreaCode, _row.AreaName));
                }
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        public List<MsCustContact_MsArea> GetAreaDDLForClearance(String _prmCustCode, String _prmCustName)
        {
            List<MsCustContact_MsArea> _result = new List<MsCustContact_MsArea>();

            try
            {
                var _query = (
                                 from _msCustContactArea in this.db.MsCustContact_MsAreas
                                 join _msArea in this.db.MsAreas
                                 on _msCustContactArea.AreaCode equals _msArea.AreaCode
                                 where _msCustContactArea.CustCode == _prmCustCode
                                    && _msCustContactArea.ItemNo == Convert.ToInt32(_prmCustName)
                                 select new
                                {
                                    AreaCode = _msCustContactArea.AreaCode,
                                    AreaName = _msArea.AreaName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustContact_MsArea(_row.AreaCode, _row.AreaName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        public List<MsCustContact_MsArea> GetAreaDDLForClearanceHd(String _prmCustCode)
        {
            List<MsCustContact_MsArea> _result = new List<MsCustContact_MsArea>();

            try
            {
                var _query = (
                                 from _msCustContactArea in this.db.MsCustContact_MsAreas
                                 join _msArea in this.db.MsAreas
                                 on _msCustContactArea.AreaCode equals _msArea.AreaCode
                                 where _msCustContactArea.CustCode == _prmCustCode
                                 select new
                                 {
                                     AreaCode = _msCustContactArea.AreaCode,
                                     AreaName = _msArea.AreaName
                                 }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustContact_MsArea(_row.AreaCode, _row.AreaName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public bool AddCustContactArea(MsCustContact_MsArea _prmMsCustContact_MsArea)
        {
            bool _result = false;

            try
            {
                this.db.MsCustContact_MsAreas.InsertOnSubmit(_prmMsCustContact_MsArea);
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
                    MsCustContact_MsArea _msCustContactArea = this.db.MsCustContact_MsAreas.Single(_temp => _temp.VisitorAreaCode.ToString().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsCustContact_MsAreas.DeleteOnSubmit(_msCustContactArea);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }
        public bool Edit(MsCustContact_MsArea _msCustContactArea)
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
        public bool DeleteCustContactArea(string _prmVisitorAreaCode)
        {
            bool _result = false;

            try
            {
                MsCustContact_MsArea _msCustContactArea = this.db.MsCustContact_MsAreas.Single(_temp => _temp.VisitorAreaCode.ToString().Trim().ToLower() == _prmVisitorAreaCode.Trim().ToLower());

                this.db.MsCustContact_MsAreas.DeleteOnSubmit(_msCustContactArea);

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool IsCustContactAreaExist(string _prmCustCode, int _prmVisitorCode, string _prmAreaCode)
        {
            bool _result = false;

            try
            {
                var _query = (
                                from _custContactArea in this.db.MsCustContact_MsAreas
                                where _custContactArea.CustCode == _prmCustCode
                                   && _custContactArea.ItemNo == _prmVisitorCode
                                   && _custContactArea.AreaCode == _prmAreaCode
                                select new
                                {
                                    VisitorAreaCode = _custContactArea.VisitorAreaCode
                                }
                              ).Count();

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
            }

            return _result;
        }

        #endregion

    }
}


