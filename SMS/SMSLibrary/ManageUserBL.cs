using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;

namespace SMSLibrary
{
    public class ManageUserBL : SMSLibBase
    {
        public ManageUserBL()
        {
        }

        ~ManageUserBL()
        {
        }

        public bool CeknothaveUser(int _prmOrganizationID, String _prmUserID)
        {
            bool _row = false;

            try
            {
                var _query = (from _user in this.db.MsUsers
                              where _user.OrganizationID == _prmOrganizationID
                              && _user.UserID == _prmUserID
                              select _user.UserID
                    ).Count();

                if (_query == 0)
                    _row = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _row;
        }

        public bool CekhaveUser(int _prmOrganizationID, String _prmUserID)
        {
            bool _row = false;

            try
            {
                var _query = (from _user in this.db.MsUsers
                              where _user.OrganizationID == _prmOrganizationID
                              && _user.UserID == _prmUserID
                              select _user.UserID
                    ).Count();

                if (_query != 0)
                    _row = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _row;
        }


        public Double GetOrganizationID(String _prmOrganizationName)
        {
            Double _id = 0;

            try
            {
                var _query = (from _organization in this.db.MsOrganizations
                              where _organization.OrganizationName == _prmOrganizationName
                              select _organization.OrganizationID
                    ).FirstOrDefault();

                _id = _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _id;
        }

        public bool Add(MsUser _prmMsUser)
        {
            bool _result = false;

            try
            {
                this.db.MsUsers.InsertOnSubmit(_prmMsUser);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;

        }

        public int RowsCount(string _prmCategory, string _prmKeyword, Int32 _prmOrganizationID)
        {
            int _result = 0;

            string _pattern1 = "%%";


            if (_prmCategory == "NameListItem")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }


            _result = (
                           from _list in this.db.MsUsers
                           where (SqlMethods.Like((_list.UserID ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && _list.fgAdmin == false
                                && _list.fgWebAdmin == false
                                && _list.OrganizationID == _prmOrganizationID
                           select _list
                       ).Count();
            return _result;
        }

        public List<MsUser> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, Int32 _prmOrganizationID)
        {
            List<MsUser> _result = new List<MsUser>();

            string _pattern1 = "%%";

            if (_prmCategory == "NameListItem")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _list in this.db.MsUsers
                                where (SqlMethods.Like((_list.UserID ?? "").Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && _list.fgAdmin == false
                                    && _list.fgWebAdmin == false
                                    && _list.OrganizationID == _prmOrganizationID
                                select _list
                            ).Skip(_prmPageSize * _prmReqPage).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public bool DeleteMultiUnSub(string[] _prmCode, Int32 _prmOrganizationID)
        {
            bool _result = false;
            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsUser _msUser = this.db.MsUsers.Single(_temp => _temp.UserID.Trim().ToLower() == _prmCode[i].Trim().ToLower() && _temp.OrganizationID == _prmOrganizationID);
                    this.db.MsUsers.DeleteOnSubmit(_msUser);
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

        public MsUser GetSingle(Int32 _prmOrganizationID, String _prmUserID)
        {
            MsUser _result = new MsUser();

            try
            {
                _result = this.db.MsUsers.Single(_temp => _temp.OrganizationID == _prmOrganizationID && _temp.UserID == _prmUserID);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _result;
        }

        public bool Edit(MsUser _prmMsuser)
        {
            bool _result = false;
            try
            {
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public bool CekPassword(String _prmOldPassword, Int32 _prmOrganization, String _prmUserID)
        {
            bool _result = false;

            try
            {
                var _query = (from _msUser in this.db.MsUsers
                              where _msUser.OrganizationID == _prmOrganization
                              && _msUser.UserID == _prmUserID
                              && _msUser.password == _prmOldPassword
                              select _msUser
                    ).Count();

                if (_query == 1)
                    _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean ChangePassword(String _prmOrganization, String _prmUserID, String _prmOldPassword, String _prmNewPassword)
        {
            Boolean _result = false;
            try
            {
                MsUser _changedData = this.db.MsUsers.Single(a => a.OrganizationID == Convert.ToInt32(_prmOrganization) && a.UserID == _prmUserID);
                if (Rijndael.Encrypt(_prmOldPassword, ApplicationConfig.EncryptionKey) != _changedData.password)
                    return _result;
                else
                    _changedData.password = Rijndael.Encrypt(_prmNewPassword, ApplicationConfig.EncryptionKey);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public MSPackage GetSingle(String _prmPackage)
        {
            MSPackage _result = new MSPackage();

            try
            {
                _result = this.db.MSPackages.Single(_temp => _temp.PackageName == _prmPackage);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _result;
        }

        public bool CekCountUserInOrganization(Int32 _prmOrganization)
        {
            bool _result = false;

            try
            {
                var _query = (from _msuser in this.db.MsUsers
                              where _msuser.OrganizationID == _prmOrganization
                              select _msuser.OrganizationID).Count();

                var _query1 = this.db.MsOrganizations.Single(_temp => _temp.OrganizationID == _prmOrganization).UserLimit;

                if ((_query - 1) < _query1)
                    _result = true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return _result;
        }

    }
}
