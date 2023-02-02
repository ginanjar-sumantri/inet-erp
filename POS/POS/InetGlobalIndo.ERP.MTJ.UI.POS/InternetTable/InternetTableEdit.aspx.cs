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
    public partial class InternetTableEdit : InternetTableBase
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.TableNmbrTextBox.Attributes.Add("ReadOnly", "True");
                
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            POSMsInternetTable _posMsInternetTable = this._internetTableBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            
            this.TableNmbrTextBox.Text = _posMsInternetTable.TableNmbr.ToString();
            this.FloorNmbrTextBox.Text = _posMsInternetTable.FloorNmbr.ToString();
            this.StatusDropDownList.SelectedValue = _posMsInternetTable.Status.ToString();
            this.fgActiveCheckBox.Checked = _posMsInternetTable.fgActive;
            //this.positionXTextBox.Text = _posMsInternetTable.positionX.ToString();
            //this.positionYTextBox.Text = _posMsInternetTable.positionY.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsInternetTable _posMsInternetTable = this._internetTableBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _posMsInternetTable.FloorType = this.FloorTypeDropDownList.Text;
            _posMsInternetTable.FloorNmbr = Convert.ToInt32(this.FloorNmbrTextBox.Text);
            _posMsInternetTable.Status = Convert.ToByte(this.StatusDropDownList.SelectedValue);
            _posMsInternetTable.fgActive = this.fgActiveCheckBox.Checked;
            //_posMsInternetTable.positionX = Convert.ToInt32(this.positionXTextBox.Text);
            //_posMsInternetTable.positionY = Convert.ToInt32(this.positionYTextBox.Text);

            bool _result = this._internetTableBL.Edit(_posMsInternetTable);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}