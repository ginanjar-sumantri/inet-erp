using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.Authentication;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Settings
{
    public sealed class PermissionBL : Base
    {
        public PermissionBL()
        {
        }

        #region master_PermissionTemplate
        public double RowsCount()
        {
            double _result = 0;

            var _query =
                       (
                          from _permissionTemplate in this.db.master_PermissionTemplates
                          select _permissionTemplate.MenuID
                       ).Count();

            _result = _query;

            return _result;
        }

        public List<master_PermissionTemplate> GetList(string _prmModule)
        {
            List<master_PermissionTemplate> _result = new List<master_PermissionTemplate>();

            try
            {
                var _query = (
                                from _permissionTemplate in this.db.master_PermissionTemplates
                                join _master_menu in this.db.master_Menus
                                    on _permissionTemplate.MenuID equals _master_menu.MenuID
                                where _master_menu.ModuleID == _prmModule
                                orderby _master_menu.Priority
                                select new
                                {
                                    ModuleID = _master_menu.ModuleID,
                                    MenuID = _permissionTemplate.MenuID,
                                    MenuName = _master_menu.Text,
                                    Add = _permissionTemplate.Add,
                                    Edit = _permissionTemplate.Edit,
                                    Delete = _permissionTemplate.Delete,
                                    View = _permissionTemplate.View,
                                    GetApproval = _permissionTemplate.GetApproval,
                                    Approve = _permissionTemplate.Approve,
                                    Posting = _permissionTemplate.Posting,
                                    Unposting = _permissionTemplate.Unposting,
                                    PrintPreview = _permissionTemplate.PrintPreview,
                                    TaxPreview = _permissionTemplate.TaxPreview,
                                    Access = _permissionTemplate.Access,
                                    Close = _permissionTemplate.Close,
                                    Generate = _permissionTemplate.Generate,
                                    Revisi = _permissionTemplate.Revisi,
                                    Indent = _master_menu.Indent
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new master_PermissionTemplate(_row.ModuleID, _row.MenuID, _row.MenuName, _row.Add, _row.Edit, _row.Delete, _row.View, _row.GetApproval, _row.Approve, _row.Posting, _row.Unposting, _row.PrintPreview, _row.TaxPreview, _row.Access, _row.Close, _row.Generate, _row.Revisi, _row.Indent));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public master_PermissionTemplate GetSingle(short _prmCode)
        {
            master_PermissionTemplate _result = null;

            try
            {
                _result = this.db.master_PermissionTemplates.Single(_temp => _temp.MenuID == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region master_RolePermission
        public double RowsCountRolePermission(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                            from _permission in this.db.master_RolePermissions
                            join _role in this.db.aspnet_Roles
                                on _permission.RoleID equals _role.RoleId
                            where (SqlMethods.Like(_role.RoleName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                            select _permission.RoleID
                        ).Distinct().Count();

            _result = _query;

            return _result;
        }

        public List<master_RolePermission> GetListRolePermission(int _prmReqPage, int _prmPageSize, Guid _prmRoleID, string _prmModule)
        {
            List<master_RolePermission> _result = new List<master_RolePermission>();

            try
            {
                var _query = (
                                from _permission in this.db.master_RolePermissions
                                join _menu in this.db.master_Menus
                                    on _permission.MenuID equals _menu.MenuID
                                join _user in this.db.aspnet_Users
                                    on _permission.RoleID.ToString().ToLower() equals _user.UserId.ToString().ToLower()
                                where _menu.ModuleID == _prmModule && _permission.RoleID.ToString().ToLower() == _prmRoleID.ToString().ToLower()
                                select new
                                {
                                    RoleID = _permission.RoleID,
                                    UserName = _user.UserName,
                                    MenuID = _permission.MenuID,
                                    MenuName = _menu.Text,
                                    Add = _permission.Add,
                                    Edit = _permission.Edit,
                                    Delete = _permission.Delete,
                                    View = _permission.View,
                                    GetApproval = _permission.GetApproval,
                                    Approve = _permission.Approve,
                                    Posting = _permission.Posting,
                                    Unposting = _permission.Unposting,
                                    PrintPreview = _permission.PrintPreview,
                                    TaxPreview = _permission.TaxPreview,
                                    Access = _permission.Access,
                                    Close = _permission.Close,
                                    Generate = _permission.Generate,
                                    Revisi = _permission.Revisi
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new master_RolePermission(_row.RoleID, _row.UserName, _row.MenuID, _row.MenuName, _row.Add, _row.Edit, _row.Delete, _row.View, _row.GetApproval, _row.Approve, _row.Posting, _row.Unposting, _row.PrintPreview, _row.TaxPreview, _row.Access, _row.Close, _row.Generate, _row.Revisi));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<master_RolePermission> GetListRolePermission(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<master_RolePermission> _result = new List<master_RolePermission>();

            string _pattern1 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _permission in this.db.master_RolePermissions
                                join _role in this.db.aspnet_Roles
                                    on _permission.RoleID equals _role.RoleId
                                where (SqlMethods.Like(_role.RoleName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _role.RoleName ascending
                                select new
                                {
                                    RoleID = _permission.RoleID,
                                    RoleName = _role.RoleName
                                }
                            ).Distinct().Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new master_RolePermission(_row.RoleID, _row.RoleName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            _result.Sort(new Comparison<master_RolePermission>(this.SortByRoleName));

            return _result;
        }

        private int SortByRoleName(master_RolePermission _prmRoleName1, master_RolePermission _prmRoleName2)
        {
            return _prmRoleName1.RoleName.CompareTo(_prmRoleName2.RoleName);
        }

        public master_RolePermission GetSingleRolePermission(short _prmMenuID, Guid _prmCode)
        {
            master_RolePermission _result = null;

            try
            {
                _result = this.db.master_RolePermissions.Single(_temp => _temp.MenuID == _prmMenuID && _temp.RoleID == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiRolePermission(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    //string[] _tempSplit = _prmCode[i].Split('&');

                    var _query = (
                                    from _masterRolePermission in this.db.master_RolePermissions
                                    where _masterRolePermission.RoleID == new Guid(_prmCode[i])
                                    select _masterRolePermission
                                 );

                    this.db.master_RolePermissions.DeleteAllOnSubmit(_query);
                }

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddRolePermission(Guid _prmRoleID, string _prmCheckHidden)
        {
            bool _result = false;

            try
            {
                List<master_RolePermission> _permission = new List<master_RolePermission>();

                string[] _tempsplit = _prmCheckHidden.Split(',');

                for (int i = 0; i < _tempsplit.Count(); i++)
                {
                    bool _editFlag = false;

                    string[] _tempsplit2 = _tempsplit[i].Split('-');

                    foreach (master_RolePermission _obj in _permission)
                    {
                        if (_obj.MenuID == Convert.ToInt16(_tempsplit2[0]))
                        {
                            if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Add)) _obj.Add = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Edit)) _obj.Edit = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.View)) _obj.View = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Delete)) _obj.Delete = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.GetApproval)) _obj.GetApproval = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Approve)) _obj.Approve = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Posting)) _obj.Posting = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Unposting)) _obj.Unposting = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.PrintPreview)) _obj.PrintPreview = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.TaxPreview)) _obj.TaxPreview = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Close)) _obj.Close = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Access)) _obj.Access = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Generate)) _obj.Generate = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Revisi)) _obj.Revisi = Convert.ToByte(_tempsplit2[2]);

                            _editFlag = true;

                            break;
                        }
                    }

                    if (_editFlag == false)
                    {
                        master_RolePermission _masterPermission = new master_RolePermission();

                        _masterPermission.RoleID = _prmRoleID;
                        _masterPermission.MenuID = Convert.ToInt16(_tempsplit2[0]);
                        if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Add))
                            _masterPermission.Add = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Edit))
                            _masterPermission.Edit = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.View))
                            _masterPermission.View = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Delete))
                            _masterPermission.Delete = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.GetApproval))
                            _masterPermission.GetApproval = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Approve))
                            _masterPermission.Approve = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Posting))
                            _masterPermission.Posting = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Unposting))
                            _masterPermission.Unposting = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.PrintPreview))
                            _masterPermission.PrintPreview = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.TaxPreview))
                            _masterPermission.TaxPreview = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Close))
                            _masterPermission.Close = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Access))
                            _masterPermission.Access = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Generate))
                            _masterPermission.Generate = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Revisi))
                            _masterPermission.Revisi = Convert.ToByte(_tempsplit2[2]);

                        _permission.Add(_masterPermission);
                    }
                }

                //foreach (master_RolePermission _item in _permission)
                //{
                this.db.master_RolePermissions.InsertAllOnSubmit(_permission);
                //}

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditRolePermission(Guid _prmRoleID, string _prmCheckHidden, string _prmModule)
        {
            bool _result = false;

            try
            {
                List<master_RolePermission> _permission = new List<master_RolePermission>();

                string[] _tempsplit = _prmCheckHidden.Split(',');

                var _query = (
                                    from _userPermission in this.db.master_RolePermissions
                                    join _master_menu in this.db.master_Menus
                                        on _userPermission.MenuID equals _master_menu.MenuID
                                    where _userPermission.RoleID == _prmRoleID
                                        && _master_menu.ModuleID == _prmModule
                                    select _userPermission
                                 );

                this.db.master_RolePermissions.DeleteAllOnSubmit(_query);

                for (int i = 0; i < _tempsplit.Count(); i++)
                {
                    bool _editFlag = false;

                    string[] _tempsplit2 = _tempsplit[i].Split('-');

                    foreach (master_RolePermission _obj in _permission)
                    {
                        if (_obj.MenuID == Convert.ToInt16(_tempsplit2[0]))
                        {
                            if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Add)) _obj.Add = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Edit)) _obj.Edit = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.View)) _obj.View = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Delete)) _obj.Delete = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.GetApproval)) _obj.GetApproval = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Approve)) _obj.Approve = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Posting)) _obj.Posting = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Unposting)) _obj.Unposting = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.PrintPreview)) _obj.PrintPreview = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.TaxPreview)) _obj.TaxPreview = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Close)) _obj.Close = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Access)) _obj.Access = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Generate)) _obj.Generate = Convert.ToByte(_tempsplit2[2]);
                            else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Revisi)) _obj.Revisi = Convert.ToByte(_tempsplit2[2]);

                            _editFlag = true;

                            break;
                        }
                    }

                    if (_editFlag == false)
                    {
                        master_RolePermission _masterPermission = new master_RolePermission();

                        _masterPermission.RoleID = _prmRoleID;
                        _masterPermission.MenuID = Convert.ToInt16(_tempsplit2[0]);
                        if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Add))
                            _masterPermission.Add = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Edit))
                            _masterPermission.Edit = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.View))
                            _masterPermission.View = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Delete))
                            _masterPermission.Delete = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.GetApproval))
                            _masterPermission.GetApproval = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Approve))
                            _masterPermission.Approve = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Posting))
                            _masterPermission.Posting = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Unposting))
                            _masterPermission.Unposting = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.PrintPreview))
                            _masterPermission.PrintPreview = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.TaxPreview))
                            _masterPermission.TaxPreview = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Close))
                            _masterPermission.Close = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Access))
                            _masterPermission.Access = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Generate))
                            _masterPermission.Generate = Convert.ToByte(_tempsplit2[2]);
                        else if (_tempsplit2[1] == PermissionDataMapper.GetText(Common.Enum.Action.Revisi))
                            _masterPermission.Revisi = Convert.ToByte(_tempsplit2[2]);

                        _permission.Add(_masterPermission);
                    }
                }

                //foreach (master_RolePermission _obj in _permission)
                //{
                this.db.master_RolePermissions.InsertAllOnSubmit(_permission);
                //}

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetMenuIDByRoleID(string _prmRoleID)
        {
            string _result = "";

            var query =
                        (
                             from _msRolePermission in this.db.master_RolePermissions
                             where _msRolePermission.RoleID == new Guid(_prmRoleID)
                             select new
                             {
                                 RoleID = _msRolePermission.RoleID,
                                 MenuID = _msRolePermission.MenuID,
                                 Add = _msRolePermission.Add,
                                 Edit = _msRolePermission.Edit,
                                 View = _msRolePermission.View,
                                 Delete = _msRolePermission.Delete,
                                 GetApproval = _msRolePermission.GetApproval,
                                 Approve = _msRolePermission.Approve,
                                 Posting = _msRolePermission.Posting,
                                 Unposting = _msRolePermission.Unposting,
                                 PrintPreview = _msRolePermission.PrintPreview,
                                 TaxPreview = _msRolePermission.TaxPreview,
                                 Close = _msRolePermission.Close,
                                 Access = _msRolePermission.Access,
                                 Generate = _msRolePermission.Generate,
                                 Revisi = _msRolePermission.Revisi
                             }
                        );

            foreach (var _var in query)
            {
                if (_result == "")
                {
                    if (_var.Add == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Add);
                    if (_var.Edit == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Edit);
                    if (_var.Delete == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Delete);
                    if (_var.View == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.View);
                    if (_var.GetApproval == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.GetApproval);
                    if (_var.Approve == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Approve);
                    if (_var.Posting == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Posting);
                    if (_var.Unposting == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Unposting);
                    if (_var.PrintPreview == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.PrintPreview);
                    if (_var.TaxPreview == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.TaxPreview);
                    if (_var.Close == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Close);
                    if (_var.Access == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access);
                }
                else if (_result != "")
                {
                    if (_var.Add == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Add);
                    if (_var.Edit == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Edit);
                    if (_var.Delete == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Delete);
                    if (_var.View == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.View);
                    if (_var.GetApproval == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.GetApproval);
                    if (_var.Approve == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Approve);
                    if (_var.Posting == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Posting);
                    if (_var.Unposting == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Unposting);
                    if (_var.PrintPreview == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.PrintPreview);
                    if (_var.TaxPreview == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.TaxPreview);
                    if (_var.Close == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Close);
                    if (_var.Access == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Access);
                    if (_var.Generate == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Generate);
                    if (_var.Revisi == PermissionDataMapper.GetStatusLevel(PermissionLevel.EntireOU))
                        _result = _result + "," + _var.MenuID + "-" + PermissionDataMapper.GetText(Common.Enum.Action.Revisi);
                }
            }

            return _result;
        }

        public String GrantDenyAllPermissionByRoleAndModule(String _prmRoleID, String _prmModuleID, Byte _prmAction)
        {
            String _result = "";
            
            //_prmAction  4:GrantAll 0: DenyAll
            int _resultSP = this.db.GrantOrDenyAllPermByRoleAndModule(_prmAction, _prmModuleID, _prmRoleID);
            
            return _result;
        }
        #endregion

        public PermissionLevel PermissionValidation1(short _prmMenuID, string _prmUserName, Common.Enum.Action _prmAction)
        {
            Guid _companyID = new UserBL().GetCompanyId(_prmUserName);
            string[] _roleIds = new RoleBL().GetRolesIDByUserName(_prmUserName);

            PermissionLevel _result = PermissionLevel.NoAccess;

            try
            {
                _result = PermissionDataMapper.GetStatusLevel(PermissionValidationByRoles(_prmMenuID, _roleIds, _prmAction, _companyID));
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public bool PermissionValidation(short _prmMenuID, string _prmUserName, Common.Enum.Action _prmAction)
        {
            Guid _companyID = new UserBL().GetCompanyId(_prmUserName);
            string[] _roleIds = new RoleBL().GetRolesIDByUserName(_prmUserName);

            bool _result = false;

            try
            {
                int _result1 = PermissionValidationByRoles(_prmMenuID, _roleIds, _prmAction, _companyID);

                if (_result1 > 4 || _result1 < 0)
                {
                    _result = false;
                }
                if (_result1 > 0) _result = true;
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte PermissionValidationByRoles(short _prmMenuID, string[] _prmRoleIds, Common.Enum.Action _prmAction, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                if (_prmAction == Common.Enum.Action.Access)
                {
                    _result = this.CekAccess(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
                else if (_prmAction == Common.Enum.Action.Add)
                {
                    _result = this.CekAdd(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
                else if (_prmAction == Common.Enum.Action.Edit)
                {
                    _result = this.CekEdit(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
                else if (_prmAction == Common.Enum.Action.View)
                {
                    _result = this.CekView(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
                else if (_prmAction == Common.Enum.Action.Delete)
                {
                    _result = this.CekDelete(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
                else if (_prmAction == Common.Enum.Action.Close)
                {
                    _result = this.CekClose(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
                else if (_prmAction == Common.Enum.Action.GetApproval)
                {
                    _result = this.CekGetApproval(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
                else if (_prmAction == Common.Enum.Action.Approve)
                {
                    _result = this.CekApprove(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
                else if (_prmAction == Common.Enum.Action.Posting)
                {
                    _result = this.CekPosting(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
                else if (_prmAction == Common.Enum.Action.Unposting)
                {
                    _result = this.CekUnposting(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
                else if (_prmAction == Common.Enum.Action.PrintPreview)
                {
                    _result = this.CekPrintPreview(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
                else if (_prmAction == Common.Enum.Action.TaxPreview)
                {
                    _result = this.CekTaxPreview(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
                else if (_prmAction == Common.Enum.Action.Generate)
                {
                    _result = this.CekGenerate(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
                else if (_prmAction == Common.Enum.Action.Revisi)
                {
                    _result = this.CekRevisi(_prmMenuID, _prmRoleIds, _prmCompanyID);
                }
            }

            catch (Exception Ex)
            {

            }

            return _result;
        }

        #region CekRolePermission
        private byte CekAccess(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.Access
                         ).Max();

            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte CekAdd(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.Add
                         ).Max();
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte CekEdit(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {

                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.Edit
                         ).Max();

            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte CekView(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.View
                         ).Max();

            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte CekDelete(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.Delete
                         ).Max();

            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte CekClose(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.Close
                         ).Max();

            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte CekGetApproval(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.GetApproval
                         ).Max();

            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte CekApprove(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.Approve
                         ).Max();

            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte CekPosting(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.Posting
                         ).Max();

            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte CekUnposting(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.Unposting
                         ).Max();

            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte CekPrintPreview(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.PrintPreview
                         ).Max();

            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte CekTaxPreview(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.TaxPreview
                         ).Max();

            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte CekGenerate(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.Generate
                         ).Max();

            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        private byte CekRevisi(short _prmMenuID, string[] _prmRoleIds, Guid _prmCompanyID)
        {
            byte _result = 0;

            try
            {
                _result = (
                            from _rolePermission in this.db.master_RolePermissions
                            join _masterRole in this.db.aspnet_Roles
                                on _rolePermission.RoleID equals _masterRole.RoleId
                            join _companyRole in this.db.master_Company_aspnet_Roles
                                on _masterRole.RoleId equals _companyRole.RoleId
                            where _prmRoleIds.Contains(_rolePermission.RoleID.ToString())
                                && _companyRole.CompanyID == _prmCompanyID
                                && _rolePermission.MenuID == _prmMenuID
                            select _rolePermission.Revisi
                         ).Max();

            }
            catch (Exception Ex)
            {
            }

            return _result;
        }
        #endregion

        ~PermissionBL()
        {
        }
    }
}
