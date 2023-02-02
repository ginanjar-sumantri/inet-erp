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
    public sealed class MsCustContactBL : Base
    {
        public MsCustContactBL()
        {
        }
        ~MsCustContactBL()
        {
        }

        #region CustContact
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
            if (_prmCompany == "ContactName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            _result = (from _msCustContact in this.db.MsCustContacts
                       join _msCust in this.db.MsCustomers
                       on _msCustContact.CustCode equals _msCust.CustCode
                       where (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           &&(SqlMethods.Like(_msCustContact.ContactName.Trim().ToLower(), _pattern2.Trim().ToLower()))

                       select _msCustContact.CustCode).Count();
            return _result;
        }


        public List<MsCustContact> GetList(int _prmReqPage, int _prmPageSize, String _prmCompany, String _prmKeyword)
        {
            List<MsCustContact> _result = new List<MsCustContact>();

            String _pattern1 = "%%";
            String _pattern2 = "%%";


            if (_prmCompany == "CustCode")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
            }
            if (_prmCompany == "ContactName")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _msCustContact in this.db.MsCustContacts
                                join _msCust in this.db.MsCustomers
                                on _msCustContact.CustCode equals _msCust.CustCode
                                where (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    &&(SqlMethods.Like(_msCustContact.ContactName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                join _msReligion in this.db.MsReligions
                                on _msCustContact.Religion equals _msReligion.ReligionCode
                                join _msCountry in this.db.MsCountries
                                on _msCustContact.Country equals _msCountry.CountryCode
                                orderby _msCust.CustName ascending
                                select new
                                {
                                    CustCode = _msCust.CustCode,
                                    CustName = _msCust.CustName,
                                    ItemNo = _msCustContact.ItemNo,
                                    ContactType = _msCustContact.ContactType,
                                    ContactName = _msCustContact.ContactName,
                                    ContactTitle = _msCustContact.ContactTitle,
                                    Birthday = _msCustContact.Birthday,
                                    Religion = _msReligion.ReligionName,
                                    Address1 = _msCustContact.Address1,
                                    Address2 = _msCustContact.Address2,
                                    Country = _msCountry.CountryName,
                                    ZipCode = _msCustContact.ZipCode,
                                    Phone = _msCustContact.Phone,
                                    Fax = _msCustContact.Fax,
                                    Email = _msCustContact.Email,
                                    Remark = _msCustContact.Remark,
                                    FgAccess = _msCustContact.FgAccess,
                                    FgGoodsIn = _msCustContact.FgGoodsIn,
                                    FgGoodsOut = _msCustContact.FgGoodsOut,
                                    FgAdditionalVisitor = _msCustContact.FgAdditionalVisitor,
                                    FgAuthorizationContact = _msCustContact.FgAuthorizationContact,
                                    CardID = _msCustContact.CardID
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustContact(_row.CustCode, _row.CustName, _row.ItemNo, _row.ContactType, _row.ContactName, _row.ContactTitle, _row.Birthday,
                        _row.Religion, _row.Address1, _row.Address2, _row.Country, _row.ZipCode, _row.Phone, _row.Fax, _row.Email, _row.Remark, Convert.ToChar(_row.FgAccess), Convert.ToChar(_row.FgGoodsIn), Convert.ToChar(_row.FgGoodsOut), Convert.ToChar(_row.FgAdditionalVisitor), Convert.ToChar(_row.FgAuthorizationContact), _row.CardID));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public List<MsCustContact> GetList(String _prmCustCode)
        {
            List<MsCustContact> _result = new List<MsCustContact>();

            String _pattern = "%%";

            if (_prmCustCode != "")
            {
                _pattern = "%" + _prmCustCode + "%";
            }

            try
            {
                var _query = (
                                from _msCustContact in this.db.MsCustContacts
                                join _msCust in this.db.MsCustomers
                                on _msCustContact.CustCode equals _msCust.CustCode
                                where
                               (SqlMethods.Like(_msCust.CustCode.ToString().Trim().ToLower(), _pattern.Trim().ToLower()))
                                join _msReligion in this.db.MsReligions
                                on _msCustContact.Religion equals _msReligion.ReligionCode
                                join _msCountry in this.db.MsCountries
                                on _msCustContact.Country equals _msCountry.CountryCode
                                orderby _msCust.CustName, _msCustContact.ContactName, _msCustContact.ItemNo ascending
                                select new
                                {
                                    CustCode = _msCust.CustCode,
                                    CustName = _msCust.CustName,
                                    ItemNo = _msCustContact.ItemNo,
                                    ContactType = _msCustContact.ContactType,
                                    ContactName = _msCustContact.ContactName,
                                    ContactTitle = _msCustContact.ContactTitle,
                                    Birthday = _msCustContact.Birthday,
                                    Religion = _msReligion.ReligionName,
                                    Address1 = _msCustContact.Address1,
                                    Address2 = _msCustContact.Address2,
                                    Country = _msCountry.CountryName,
                                    ZipCode = _msCustContact.ZipCode,
                                    Phone = _msCustContact.Phone,
                                    Fax = _msCustContact.Fax,
                                    Email = _msCustContact.Email,
                                    Remark = _msCustContact.Remark,

                                    FgAccess = _msCustContact.FgAccess,
                                    FgGoodsIn = _msCustContact.FgGoodsIn,
                                    FgGoodsOut = _msCustContact.FgGoodsOut,
                                    FgAdditionalVisitor = _msCustContact.FgAdditionalVisitor,
                                    FgAuthorizationContact = _msCustContact.FgAuthorizationContact,
                                    CardID = _msCustContact.CardID

                                }
                            );

                foreach (var _row in _query)
                {

                    _result.Add(new MsCustContact(_row.CustCode, _row.CustName, _row.ItemNo, _row.ContactType, _row.ContactName, _row.ContactTitle, _row.Birthday,
                        _row.Religion, _row.Address1, _row.Address2, _row.Country, _row.ZipCode, _row.Phone, _row.Fax, _row.Email, _row.Remark, Convert.ToChar(_row.FgAccess), Convert.ToChar(_row.FgGoodsIn), Convert.ToChar(_row.FgGoodsOut), Convert.ToChar(_row.FgAdditionalVisitor), Convert.ToChar(_row.FgAuthorizationContact), _row.CardID));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public List<MsCustContact> GetListForTrGoods(String _prmCustCode, String _prmTypeIn, String _prmTypeOut)
        {
            List<MsCustContact> _result = new List<MsCustContact>();
            String _pattern1 = "%%";
            String _pattern2 = "%%";
            String _pattern3 = "%%";

            if (_prmCustCode != "")
            {
                _pattern1 = "%" + _prmCustCode + "%";
            }
            if (_prmTypeIn != "")
            {
                _pattern2 = "%" + _prmTypeIn + "%";
            }
            if (_prmTypeOut != "")
            {
                _pattern3 = "%" + _prmTypeOut + "%";
            }


            try
            {
                var _query = (
                                from _msCustContact in this.db.MsCustContacts
                                join _msCust in this.db.MsCustomers
                                on _msCustContact.CustCode equals _msCust.CustCode
                                where (SqlMethods.Like(_msCust.CustCode.ToString().Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCustContact.FgGoodsIn.ToString().Trim().ToLower(), _pattern2.Trim().ToLower()))
                                   && (SqlMethods.Like(_msCustContact.FgGoodsOut.ToString().Trim().ToLower(), _pattern3.Trim().ToLower()))
                                orderby _msCustContact.ContactName ascending
                                select new
                                {
                                    CustCode = _msCustContact.CustCode,
                                    ContactName = _msCustContact.ContactName,
                                    FgGoodsIn = _msCustContact.FgGoodsIn,
                                    FgGoodsOut = _msCustContact.FgGoodsOut,
                                    CardID = _msCustContact.CardID

                                }
                            );

                foreach (var _row in _query)
                {

                    _result.Add(new MsCustContact(_row.CustCode, _row.ContactName, Convert.ToChar(_row.FgGoodsIn), Convert.ToChar(_row.FgGoodsOut), _row.CardID));
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }




        public MsCustContact GetSingle(string _prmCode)
        {
            MsCustContact _result = null;

            try
            {
                String[] _tempSplit = _prmCode.Split('-');
                _result = this.db.MsCustContacts.Single(_temp => _temp.CustCode.ToLower() == _tempSplit[0].Trim().ToLower() && _temp.ItemNo.ToString() == _tempSplit[1].Trim().ToLower());
            }
            catch (Exception)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        public MsCustContact GetSingleContactName(string _prmCode)
        {
            MsCustContact _result = null;

            try
            {
                String[] _tempSplit = _prmCode.Split('-');
                _result = this.db.MsCustContacts.Single(_temp => _temp.CustCode.ToLower() == _tempSplit[0].Trim().ToLower() && _temp.ItemNo.ToString() == _tempSplit[1].Trim().ToLower());
            }
            catch (Exception)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        public MsCustContact GetSingleForClearance(string _prmCustCode, int _prmItemNo)
        {
            MsCustContact _result = null;

            try
            {
                _result = this.db.MsCustContacts.Single(_temp => _temp.CustCode.ToLower() == _prmCustCode.Trim().ToLower() && _temp.ItemNo == _prmItemNo);
            }
            catch (Exception)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }


        public String GetCustomerPhotoByCode(String _prmCust, int _prmItemNo)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msCustContact in this.db.MsCustContacts
                                where _msCustContact.CustCode == _prmCust
                                && _msCustContact.ItemNo == _prmItemNo
                                select new
                                {
                                    CustPhoto = (from _msCustContactExtensions in this.db.master_CustContactExtensions
                                                 where _msCustContact.CustCode == _msCustContactExtensions.CustCode
                                                 && _msCustContact.ItemNo == _msCustContactExtensions.ItemNo
                                                 select _msCustContactExtensions.CustomerPhoto).FirstOrDefault(),
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.CustPhoto;
                }
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public string GetNameCustContactByCode(string _prmCustCode, int _prmItemNo)
        {
            string _result = "";

            try
            {
                _result = this.db.MsCustContacts.Single(_temp => _temp.CustCode.Trim().ToLower() == _prmCustCode.Trim().ToLower() && _temp.ItemNo == _prmItemNo).ContactName;
            }
            catch (Exception)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        public List<MsCustContact> GetContactNameForDDL(String _prmCustCode)
        {
            List<MsCustContact> _result = new List<MsCustContact>();

            try
            {
                var _query = (
                                from _mscustContact in this.db.MsCustContacts
                                where _mscustContact.CustCode == _prmCustCode
                                select new
                                {
                                    CustCode = _mscustContact.ItemNo,
                                    ContactName = _mscustContact.ContactName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustContact(_row.CustCode, _row.ContactName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        public List<MsCustContact> GetContactNameForClearance(String _prmCustCode)
        {
            List<MsCustContact> _result = new List<MsCustContact>();

            try
            {
                var _query = (
                                from _mscustContact in this.db.MsCustContacts
                                where _mscustContact.CustCode == _prmCustCode
                                select new
                                {
                                    ItemNo = _mscustContact.ItemNo,
                                    ContactName = _mscustContact.ContactName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustContact(_row.ItemNo, _row.ContactName));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        public List<MsCustContact> GetCustomerNameForClearance()
        {
            List<MsCustContact> _result = new List<MsCustContact>();

            try
            {
                var _query = (
                                from _mscustContact in this.db.MsCustContacts
                                join _msCust in this.db.MsCustomers
                                on _mscustContact.CustCode equals _msCust.CustCode
                                select new
                                {
                                    CustCode = _mscustContact.CustCode,
                                    CustName = _mscustContact.CustName
                                }
                            ).Distinct();

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustContact(_row.CustName, _row.CustName));
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
                    MsCustContact _msCustContact = this.db.MsCustContacts.Single(_temp => _temp.CustCode.ToString() == _tempSplit[0].Trim().ToLower() && _temp.ItemNo.ToString() == _tempSplit[1].Trim().ToLower());

                    this.db.MsCustContacts.DeleteOnSubmit(_msCustContact);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Add(MsCustContact _prmMsCustContact)
        {
            bool _result = false;

            try
            {
                this.db.MsCustContacts.InsertOnSubmit(_prmMsCustContact);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        public bool Edit(MsCustContact _prmMsCustContact)
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




        public int GetMaxItemNoByCode(string _prmCustCode)
        {
            int _result = 0;

            try
            {
                _result = this.db.MsCustContacts.Where(_temp => _temp.CustCode.Trim().ToLower() == _prmCustCode.Trim().ToLower()).Max(_max => _max.ItemNo);
            }
            catch (Exception)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        // copas



        public bool Add(MsCustomer _newData)
        {
            bool _result = false;

            try
            {
                this.db.MsCustomers.InsertOnSubmit(_newData);
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
