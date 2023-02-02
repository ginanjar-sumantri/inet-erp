using System;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Transactions;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule
{
    public sealed class CompanyConfig : Base
    {
        public CompanyConfig()
        {
        }

        public CompanyConfiguration GetSingle(CompanyConfigure _prmCodeConfig)
        {
            CompanyConfiguration _result = new CompanyConfiguration();

            try
            {
                _result = this.db.CompanyConfigurations.Single(_temp => _temp.ConfigCode == CompanyConfigureDataMapper.GetCompanyConfigure(_prmCodeConfig));
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public CompanyConfiguration GetSingle(String _prmCodeConfig)
        {
            CompanyConfiguration _result = new CompanyConfiguration();

            try
            {
                _result = this.db.CompanyConfigurations.Single(_temp => _temp.ConfigCode.Trim().ToLower() == _prmCodeConfig.Trim().ToLower());
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public Boolean Edit(CompanyConfiguration _prmCodeConfig)
        {
            Boolean _result = false;

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

        public string OpenEncryption(string _prmKeyID, string _prmCode, string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spPAY_OpenEncryption(_prmCode, _prmKeyID, _prmuser, ref _result);
            }
            catch (Exception ex)
            {
                _result = "Open Encryption Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string CloseEncryption(string _prmuser)
        {
            string _result = "";

            try
            {
                this.db.spPAY_CloseEncryption("", _prmuser, ref _result);
            }
            catch (Exception ex)
            {
                _result = "Close Encryption Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public string ChangeKey(String _prmOldKeyID, string _prmOldKeyIDEncryption, string _prmNewKeyID, string _prmNewKeyIDEncryption)
        {
            string _result = "";

            try
            {
                this.db.spPAY_ChangeKeyEncryption(_prmOldKeyID, _prmOldKeyIDEncryption, _prmNewKeyID, _prmNewKeyIDEncryption, ref _result);
            }
            catch (Exception ex)
            {
                _result = "Close Encryption Failed";
                ErrorHandler.Record(ex, EventLogEntryType.Error, _result);
            }

            return _result;
        }

        public String GetThemeComponent(ThemeComponent _themeComponentCode, String _prmThemeCode)
        {
            String _result = "";
            switch (_themeComponentCode)
            {
                case ThemeComponent.ThemeName:
                    _result = this.dbMembership.master_themes.Single(a => a.ThemeCode == _prmThemeCode).ThemeName;
                    break;
                case ThemeComponent.BackgroundColorBody:
                    _result = this.dbMembership.master_themes.Single(a => a.ThemeCode == _prmThemeCode).BackgroundColorBody;
                    break;
                case ThemeComponent.BackgroundImage:
                    _result = this.dbMembership.master_themes.Single(a => a.ThemeCode == _prmThemeCode).BackgroundImage;
                    break;
                case ThemeComponent.BackgroundImageBawah:
                    _result = this.dbMembership.master_themes.Single(a => a.ThemeCode == _prmThemeCode).BackgroundImageBawah;
                    break;
                case ThemeComponent.RowColor:
                    _result = this.dbMembership.master_themes.Single(a => a.ThemeCode == _prmThemeCode).RowColor;
                    break;
                case ThemeComponent.RowColorAlternate:
                    _result = this.dbMembership.master_themes.Single(a => a.ThemeCode == _prmThemeCode).RowColorAlternate;
                    break;
                case ThemeComponent.RowColorHover:
                    _result = this.dbMembership.master_themes.Single(a => a.ThemeCode == _prmThemeCode).RowColorHover;
                    break;
                case ThemeComponent.WelcomeTextColor:
                    _result = this.dbMembership.master_themes.Single(a => a.ThemeCode == _prmThemeCode).WelcomeTextColor;
                    break;
                case ThemeComponent.BackgroundColorLogin:
                    _result = this.dbMembership.master_themes.Single(a => a.ThemeCode == _prmThemeCode).BackgroundColorLogin;
                    break;
                case ThemeComponent.BackgroundimageLogin:
                    _result = this.dbMembership.master_themes.Single(a => a.ThemeCode == _prmThemeCode).BackgroundImageLogin;
                    break;
                case ThemeComponent.BackgroundImagePanelLogin:
                    _result = this.dbMembership.master_themes.Single(a => a.ThemeCode == _prmThemeCode).BackgroundImagePanelLogin;
                    break;
                case ThemeComponent.PanelLoginWidth:
                    _result = this.dbMembership.master_themes.Single(a => a.ThemeCode == _prmThemeCode).PanelLoginWidth;
                    break;
                case ThemeComponent.PanelLoginHeight:
                    _result = this.dbMembership.master_themes.Single(a => a.ThemeCode == _prmThemeCode).PanelLoginHeight;
                    break;
            }
            return _result;
        }

        /// <summary>
        /// //////// untuk di setting web
        /// </summary>

        public CompanyConfiguration GetSingleCompanyConfiguration(String _prmConfigCode)
        {
            CompanyConfiguration _result = new CompanyConfiguration();

            try
            {
                var _query1 = (from _comfig in this.db.CompanyConfigurations
                               where _comfig.ConfigCode.Trim().ToLower() == _prmConfigCode.Trim().ToLower()
                               select _comfig
                                ).Count();

                if (_query1 > 0)
                {
                    var _query = (from _companyConfiguration in this.db.CompanyConfigurations
                                  where _companyConfiguration.ConfigCode.Trim().ToLower() == _prmConfigCode.Trim().ToLower()
                                  select _companyConfiguration
                                    ).FirstOrDefault();

                    _result = _query;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _result;
        }

        public bool ToInsertCompanyConfiguration(CompanyConfiguration _prmCompanyConfig)
        {
            bool _result = false;

            try
            {
                this.db.CompanyConfigurations.InsertOnSubmit(_prmCompanyConfig);
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public bool SubmitCompanyConfiguration(CompanyConfiguration _prmCompanyConfig)
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

        public bool ToInsertAllCompanyConfiguration(List<CompanyConfiguration> _prmCompanyConfig, List<CompanyConfiguration> _prmCompanyConfigEdit)
        {
            bool _result = false;

            try
            {
                this.db.CompanyConfigurations.InsertAllOnSubmit(_prmCompanyConfig);

                foreach (var _row in _prmCompanyConfigEdit)
                {
                    CompanyConfiguration _comFig = this.GetSingleCompanyConfiguration(_row.ConfigCode);
                    _comFig.SetValue = _row.SetValue;
                    _comFig.ConfigDescription = _row.ConfigDescription;
                    _comFig.GroupConfig = _row.GroupConfig;
                    _comFig.AlowEdit = _row.AlowEdit;
                    _comFig.CreateBy = _row.CreateBy;
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

        public bool CekCompanyConfig()
        {
            bool _result = false;

            try
            {
                var _query = (from _comfig in this.db.CompanyConfigurations
                              select _comfig
                              ).Count();

                if (_query > 0)
                    _result = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public List<CompanyConfiguration> GetListCompanyConfig()
        {
            List<CompanyConfiguration> _result = new List<CompanyConfiguration>();

            try
            {
                var _query = (from _companyConfig in this.db.CompanyConfigurations
                              select _companyConfig
                              );

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

        public List<CompanyConfiguration> GetListCompanyConfig(String _prmGroupConfig)
        {
            List<CompanyConfiguration> _result = new List<CompanyConfiguration>();

            try
            {
                var _query = (
                                from _companyConfig in this.db.CompanyConfigurations
                                where _companyConfig.GroupConfig.Trim().ToLower() == _prmGroupConfig.Trim().ToLower()
                                    && _companyConfig.Visible == true
                                orderby _companyConfig.SortNo
                                select _companyConfig
                             );

                foreach (var _row in _query)
                {
                    String _expr = "";
                    if (CompConfigDataTypeMapper.GetCompConfigDataType(_row.ValueType) == CompConfigDataType.SQLQuery)
                    {
                        var _searchDataResult = (
                            from _dynamicSearch in this.db.dynamicSearch(_row.SQLExpr)
                            select _dynamicSearch.ResultRow
                        );

                        foreach (var _rsDataResult in _searchDataResult)
                        {
                            String _hasil = (_rsDataResult.Replace("\r\n", " ")).ToString();
                            if (_expr == "")
                            {
                                _expr = _hasil;
                            }
                            else
                            {
                                _expr += "," + _hasil;
                            }
                        }
                        _result.Add(new CompanyConfiguration(_row.ConfigCode, _row.SetValue, _row.ConfigDescription, _row.CreateBy, _row.GroupConfig, _row.AlowEdit, _row.SortNo, _row.Visible, _row.ValueType, _expr));
                    }
                    else
                    {
                        _result.Add(new CompanyConfiguration(_row.ConfigCode, _row.SetValue, _row.ConfigDescription, _row.CreateBy, _row.GroupConfig, _row.AlowEdit, _row.SortNo, _row.Visible, _row.ValueType, _row.SQLExpr));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<String> GetListCompanyConfigGroup()
        {
            List<String> _result = new List<String>();

            try
            {
                var _query = (
                                from _companyConfig in this.db.CompanyConfigurations
                                orderby _companyConfig.GroupConfig
                                select _companyConfig.GroupConfig
                             ).Distinct();

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

        public List<String> GetSQLExprByCode(String _prmSQLExpr)
        {
            List<String> _result = new List<String>();

            var _searchDataResult = (
                            from _dynamicSearch in this.db.dynamicSearch(_prmSQLExpr)
                            select _dynamicSearch.ResultRow
                        );
            foreach (var _rsDataResult in _searchDataResult)
            {
                String _hasil = (_rsDataResult.Replace("\r\n", " ")).ToString();
                _result.Add(_hasil);
            }

            return _result;
        }

        public bool CekGroupConfig(String _prmGroupConfig)
        {
            bool _result = false;
            try
            {
                var _query = (from _companyConfig in this.db.CompanyConfigurations
                              where _companyConfig.GroupConfig.Trim().ToLower() == _prmGroupConfig.Trim().ToLower()
                              select _companyConfig
                            ).Count();

                if (_query > 0)
                    _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        ~CompanyConfig()
        {
        }
    }
}