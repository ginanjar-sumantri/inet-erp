using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;

namespace SMSLibrary
{
    public class BackEndBL : SMSLibBase
    {
        public bool CekLoginAdmin(String _prmUserAdmin, String _prmPasswordAdmin)
        {
            bool _result = false;

            try
            {
                var _query = (from _msBackEndSetting in this.db.MsBackEndSettings
                              where _msBackEndSetting.Description == "UserAdmin"
                              select _msBackEndSetting.Value).FirstOrDefault();
                if (_query == _prmUserAdmin)
                {
                    var _query1 = (from _msBackEndSetting in this.db.MsBackEndSettings
                                   where _msBackEndSetting.Description == "PasswordAdmin"
                                   select _msBackEndSetting).FirstOrDefault();

                    if (_query1.Value == _prmPasswordAdmin)
                        _result = true;
                    else
                        _result = false;
                }

                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MSPackage> GetListPackageForDDL()
        {
            List<MSPackage> _result = new List<MSPackage>();

            try
            {
                var _query = (
                                from _msPackage in this.db.MSPackages
                                orderby _msPackage.PackageName ascending
                                select new
                                {
                                    PackageName = _msPackage.PackageName,
                                    SMSPerDay = _msPackage.SMSPerDay
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new MSPackage(_row.PackageName, _row.SMSPerDay));
                }
            }
            catch (Exception ex)
            {
                //ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool CekOrganization(String _prmOrganizationName)
        {
            bool _result = false;

            try
            {
                var _query = (from _Organization in this.db.MsOrganizations
                              where _Organization.OrganizationName == _prmOrganizationName
                              select _Organization.OrganizationName
                                ).Count();
                if (_query == 0)
                    _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public bool AddUser(MsUser _prmUser)
        {
            bool _result = false;
            try
            {
                this.db.MsUsers.InsertOnSubmit(_prmUser);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public bool EditUser(MsUser _prmMsUser)
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

        public bool EditOrganization(MsOrganization _prmMsOrganization)
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

        public List<MsUser> GetListMsUser(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsUser> _result = new List<MsUser>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "OrganizationName")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "UserID")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "PackageName")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "Email")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }

            try
            {
                var _query = (from _msUser in this.db.MsUsers
                              join _msOrganization in this.db.MsOrganizations
                              on _msUser.OrganizationID equals _msOrganization.OrganizationID
                              where (SqlMethods.Like(_msOrganization.OrganizationName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_msUser.UserID.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                     && (SqlMethods.Like(_msUser.PackageName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                     && (SqlMethods.Like((_msUser.Email.Trim() ?? "").ToLower(), _pattern4.Trim().ToLower()))
                              orderby _msOrganization.OrganizationName, _msUser.UserID ascending
                              select new { 
                                            OrganizationName = _msOrganization.OrganizationName,
                                            UserName = _msUser.UserID,
                                            FgAdmin = _msUser.fgAdmin,
                                            PackageName = _msUser.PackageName,
                                            Email = _msUser.Email
                                          }
                              ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsUser(_row.OrganizationName, _row.UserName, _row.FgAdmin, _row.PackageName, _row.Email));
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return _result;
        }

        public double RowsCountMsUser(String _prmCategory, String _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";
            string _pattern4 = "%%";

            if (_prmCategory == "OrganizationName")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "UserID")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern3 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "PackageName")
            {
                _pattern3 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern4 = "%%";
            }
            else if (_prmCategory == "Email")
            {
                _pattern4 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
                _pattern2 = "%%";
                _pattern3 = "%%";
            }

            try
            {
                var _query =
                               (
                              from _msUser in this.db.MsUsers
                              join _msOrganization in this.db.MsOrganizations
                              on _msUser.OrganizationID equals _msOrganization.OrganizationID
                              where (SqlMethods.Like(_msOrganization.OrganizationName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                     && (SqlMethods.Like(_msUser.UserID.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                     && (SqlMethods.Like(_msUser.PackageName.Trim().ToLower(), _pattern3.Trim().ToLower()))
                                     && (SqlMethods.Like((_msUser.Email.Trim() ?? "").ToLower(), _pattern4.Trim().ToLower()))
                              orderby _msOrganization.OrganizationName, _msUser.UserID ascending
                              select _msUser.UserID
                               ).Count();

                _result = _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public MsOrganization getSingleMsorganization(String _prmOrganization)
        {
            MsOrganization _result = new MsOrganization();

            try
            {
                var _query = (from _msOrganization in this.db.MsOrganizations
                              where _msOrganization.OrganizationName == _prmOrganization
                              select _msOrganization
                                ).FirstOrDefault();
                _result = _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public MsOrganization getSingleMsorganizationByID(String _prmOrganizationID)
        {
            MsOrganization _result = new MsOrganization();

            try
            {
                var _query = (from _msOrganization in this.db.MsOrganizations
                              where _msOrganization.OrganizationID == Convert.ToInt32( _prmOrganizationID )
                              select _msOrganization
                                ).FirstOrDefault();
                _result = _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public MsUser getSingleMsUser(String _prmOrganizationID, String _prmUserID)
        {
            MsUser _result = new MsUser();

            try
            {
                var _query = (from _msUser in this.db.MsUsers
                              where _msUser.OrganizationID == Convert.ToInt32(_prmOrganizationID)
                              && _msUser.UserID == _prmUserID
                              select _msUser
                                ).FirstOrDefault();
                _result = _query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean EditSubmit() {
            try
            {
                this.db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditBalance(String _prmOrgID, Decimal _prmAmountChange, Boolean _prmFgIncrease, String _prmDescription)
        {
            try
            {
                TrBalance _newData = new TrBalance();
                _newData.OrganizationID = Convert.ToInt32(_prmOrgID);
                _newData.Amount = _prmAmountChange;
                _newData.fgIncrease = _prmFgIncrease;
                _newData.TransDate = DateTime.Now;
                _newData.Description = _prmDescription;

                this.db.TrBalances.InsertOnSubmit(_newData);

                MsOrganization _editData = this.db.MsOrganizations.Single(a => a.OrganizationID == Convert.ToInt32(_prmOrgID));
                _editData.MaskingBalanceAccount = (_prmFgIncrease) ? (_editData.MaskingBalanceAccount + _prmAmountChange) : (_editData.MaskingBalanceAccount - _prmAmountChange);

                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public String getSettingValue(int _prmIndexSettingVariable)
        {
            String _result = "";
            switch (_prmIndexSettingVariable) {
                case 1: _result = this.db.MsBackEndSettings.Single(a => a.Description == "MaskingCID").Value; break;
                case 2: _result = this.db.MsBackEndSettings.Single(a => a.Description == "MaskingPrice").Value; break;
                case 3: _result = this.db.MsBackEndSettings.Single(a => a.Description == "MaskingPWD").Value; break;
                case 4: _result = this.db.MsBackEndSettings.Single(a => a.Description == "MaskingURL").Value; break;
                case 5: _result = this.db.MsBackEndSettings.Single(a => a.Description == "PasswordAdmin").Value; break;
                case 6: _result = this.db.MsBackEndSettings.Single(a => a.Description == "UserAdmin").Value; break;
                case 7: _result = this.db.MsBackEndSettings.Single(a => a.Description == "webDomainName").Value; break;
            }
            return _result;
        }

        public void ChangePassword(String _prmUser, String _prmPassword)
        {
            try
            {
                MsBackEndSetting _editUser = this.db.MsBackEndSettings.Single(a => a.Description == "UserAdmin");
                MsBackEndSetting _editPassword = this.db.MsBackEndSettings.Single(a => a.Description == "PasswordAdmin");
                _editUser.Value = _prmUser;
                _editPassword.Value = Rijndael.Encrypt(_prmPassword, ApplicationConfig.EncryptionKey);
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public void updateBackEndSetting(String _prmMaskingCID, String _prmMaskingPrice,
            String _prmMaskingPWD, String _prmMaskingURL, String _prmWebDomainName)
        {
            MsBackEndSetting _editMaskingCID = this.db.MsBackEndSettings.Single(a => a.Description == "MaskingCID");
            MsBackEndSetting _editMaskingPrice = this.db.MsBackEndSettings.Single(a => a.Description == "MaskingPrice");
            MsBackEndSetting _editMaskingPWD = this.db.MsBackEndSettings.Single(a => a.Description == "MaskingPWD");
            MsBackEndSetting _editMaskingURL = this.db.MsBackEndSettings.Single(a => a.Description == "MaskingURL");
            MsBackEndSetting _editWebDomainName = this.db.MsBackEndSettings.Single(a => a.Description == "webDomainName");
            _editMaskingCID.Value = _prmMaskingCID;
            _editMaskingPrice.Value = _prmMaskingPrice;
            _editMaskingPWD.Value = _prmMaskingPWD;
            _editMaskingURL.Value = _prmMaskingURL;
            _editWebDomainName.Value = _prmWebDomainName;
            this.db.SubmitChanges();
        }
    }
}
