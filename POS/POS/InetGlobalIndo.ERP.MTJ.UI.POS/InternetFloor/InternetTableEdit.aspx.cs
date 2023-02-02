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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.InternetFloor
{
    public partial class InternetTableEdit : InternetFloorBase
    {
        private InternetFloorBL _internetFloorBL = new InternetFloorBL();
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

                this.TableIDPerRoomTextBox.Attributes.Add("ReadOnly", "True");
                
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
            String [] _FloorAndTableNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._tableAndFloorKey), ApplicationConfig.EncryptionKey).Split('|');
            POSMsInternetTable _posMsInternetTable = this._internetFloorBL.GetSingleInternetTable(_FloorAndTableNmbr[0], _FloorAndTableNmbr[1]);

            this.TableIDPerRoomTextBox.Text = _posMsInternetTable.TableIDPerRoom.ToString();
            this.TableNmbrTextBox.Text = _posMsInternetTable.TableNmbr.ToString();
            this.StatusDropDownList.SelectedValue = _posMsInternetTable.Status.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            String[] _FloorAndTableNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._tableAndFloorKey), ApplicationConfig.EncryptionKey).Split('|');
            POSMsInternetTable _posMsInternetTable = this._internetFloorBL.GetSingleInternetTable(_FloorAndTableNmbr[0], _FloorAndTableNmbr[1]);

            _posMsInternetTable.TableIDPerRoom = Convert.ToInt32 ( this.TableIDPerRoomTextBox.Text );
            _posMsInternetTable.TableNmbr = this.TableNmbrTextBox.Text;
            _posMsInternetTable.Status = Convert.ToByte( this.StatusDropDownList.SelectedValue );

            bool _result = this._internetFloorBL.EditSubmit();

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_FloorAndTableNmbr[0], ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            String[] _FloorAndTableNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._tableAndFloorKey), ApplicationConfig.EncryptionKey).Split('|');
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_FloorAndTableNmbr[0], ApplicationConfig.EncryptionKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}