using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class master_RolePermission
    {
        private string _roleName = "";
        private string _menuName = "";

        public master_RolePermission(Guid _prmRoleID, string _prmRoleName, short _prmMenuID, string _prmMenuName, byte _prmAdd, byte _prmEdit, byte _prmDelete, byte _prmView, byte _prmGetApproval, byte _prmApprove, byte _prmPosting, byte _prmUnposting, byte _prmPrintPreview, byte _prmTaxPreview, byte _prmAccess, byte _prmClose, byte _prmGenerate, byte _prmRevisi)
        {
            this.RoleID = _prmRoleID;
            this.RoleName = _prmRoleName;
            this.MenuID = _prmMenuID;
            this.MenuName = _prmMenuName;
            this.Add = _prmAdd;
            this.Edit = _prmEdit;
            this.Delete = _prmDelete;
            this.View = _prmView;
            this.GetApproval = _prmGetApproval;
            this.Approve = _prmApprove;
            this.Posting = _prmPosting;
            this.Unposting = _prmUnposting;
            this.PrintPreview = _prmPrintPreview;
            this.TaxPreview = _prmTaxPreview;
            this.Access = _prmAccess;
            this.Close = _prmClose;
            this.Generate = _prmGenerate;
            this.Revisi = _prmRevisi;
        }

        public master_RolePermission(Guid _prmRoleID, string _prmRoleName)
        {
            this.RoleID = _prmRoleID;
            this.RoleName = _prmRoleName;
        }

        public string RoleName
        {
            get
            {
                return this._roleName;
            }
            set
            {
                this._roleName = value;
            }
        }

        public string MenuName
        {
            get
            {
                return this._menuName;
            }
            set
            {
                this._menuName = value;
            }
        }
    }
}
