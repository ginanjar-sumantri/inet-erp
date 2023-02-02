using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.InternetTable
{
    public partial class InternetTableView : InternetTableBase
    {
        private InternetTableBL _internetTableBL = new InternetTableBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.FloorTypeTextBox.Attributes.Add("ReadOnly", "True");
                this.TableNmbrTextBox.Attributes.Add("ReadOnly", "True");
                this.FloorNmbrTextBox.Attributes.Add("ReadOnly", "True");
                this.StatusTextBox.Attributes.Add("ReadOnly", "True");
                this.positionXTextBox.Attributes.Add("ReadOnly", "True");
                this.positionYTextBox.Attributes.Add("ReadOnly", "True");

                this.SetButtonPermission();

                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowData()
        {
            POSMsInternetTable _posMsInternetTable = this._internetTableBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.FloorTypeTextBox.Text = _posMsInternetTable.FloorType;
            this.TableNmbrTextBox.Text = _posMsInternetTable.TableNmbr.ToString();
            this.FloorNmbrTextBox.Text = _posMsInternetTable.FloorNmbr.ToString();
            this.StatusTextBox.Text = POSTrInternetDataMapper.GetStatusTableText(_posMsInternetTable.Status);
            this.fgActiveCheckBox.Checked = _posMsInternetTable.fgActive;
            //this.positionXTextBox.Text = _posMsInternetTable.positionX.ToString();
            //this.positionYTextBox.Text = _posMsInternetTable.positionY.ToString();
            
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}