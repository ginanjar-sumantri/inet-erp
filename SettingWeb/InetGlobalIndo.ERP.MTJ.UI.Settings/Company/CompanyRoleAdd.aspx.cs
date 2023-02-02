using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.Company
{
    public partial class CompanyRoleAdd : CompanyBase
    {
        private CompanyBL _companyBL = new CompanyBL();
        private RoleBL _roleBL = new RoleBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral4;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowRole();
                
                this.ClearData();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();
            this.RoleCheckBoxList.ClearSelection();
        }

        private void ShowRole()
        {
            this.RoleCheckBoxList.DataTextField = "RoleName";
            this.RoleCheckBoxList.DataValueField = "RoleId";
            this.RoleCheckBoxList.DataSource = this._roleBL.GetListRole();
            this.RoleCheckBoxList.DataBind();
        }

        public void ShowData()
        {
            string _tempRole;

            List<master_Company_aspnet_Role> _masterCompanyRole = new List<master_Company_aspnet_Role>();
            _masterCompanyRole = _companyBL.GetListCompanyRole(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey)));

            int i = 0;
            string[] _role = new String[_masterCompanyRole.Count];

            foreach (var _item in _masterCompanyRole)
            {
                _tempRole = _item.RoleId.ToString();

                _role[i] = _tempRole;

                i++;
            }

            for (int k = 0; k < this.RoleCheckBoxList.Items.Count; k++)
            {
                for (int j = 0; j < _role.Length; j++)
                {
                    if (this.RoleCheckBoxList.Items[k].Value == _role[j])
                    {
                        this.RoleCheckBoxList.Items[k].Selected = true;
                    }
                }
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            master_Company_aspnet_Role _master_Company_aspnet_Role = new master_Company_aspnet_Role();

            _master_Company_aspnet_Role.CompanyID = new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._compKey), ApplicationConfig.EncryptionKey));
            
            String _tempRole = "";
            for (int i = 0; i < this.RoleCheckBoxList.Items.Count; i++)
            {
                if (this.RoleCheckBoxList.Items[i].Selected == true)
                {
                    if (_tempRole == "")
                    {
                        _tempRole = this.RoleCheckBoxList.Items[i].Value;
                    }
                    else
                    {
                        _tempRole = _tempRole + "," + this.RoleCheckBoxList.Items[i].Value;
                    }
                }
            }

            bool _result = _companyBL.AddRole(_master_Company_aspnet_Role, _tempRole);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._compKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._compKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
            this.ShowData();
        }
    }
}