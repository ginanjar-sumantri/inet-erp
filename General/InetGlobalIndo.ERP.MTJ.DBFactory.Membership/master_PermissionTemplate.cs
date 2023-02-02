using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.Membership
{
    public partial class master_PermissionTemplate
    {
        private string _moduleID = "";
        private string _menuName = "";
        private byte? _indent;

        public master_PermissionTemplate(string _prmModuleID, short _prmMenuID, string _prmMenuName, bool _prmAdd, bool _prmEdit, bool _prmDelete, bool _prmView, bool _prmGetApproval, bool _prmApprove, bool _prmPosting, bool _prmUnposting, bool _prmPrintPreview, bool _prmTaxPreview, bool _prmAccess, bool _prmClose, bool _prmGenerate, bool _prmRevisi, byte? _prmIndent)
        {
            this.ModuleID = _prmModuleID;
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
            this.Indent = _prmIndent;
        }

        public string ModuleID
        {
            get
            {
                return this._moduleID;
            }
            set
            {
                this._moduleID = value;
            }
        }

        public byte? Indent
        {
            get
            {
                return this._indent;
            }
            set
            {
                this._indent = value;
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
