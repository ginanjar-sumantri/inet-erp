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
    public partial class InternetFloorEdit : InternetFloorBase
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

                //this.FloorNmbrTextBox.Attributes.Add("ReadOnly", "True");

                this.RoomCodeDropDownList.DataSource = _internetFloorBL.GetListRoomCode();
                this.RoomCodeDropDownList.DataValueField = this.RoomCodeDropDownList.DataTextField = "roomCode" ;
                this.RoomCodeDropDownList.DataBind();

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
            POSMsInternetFloor _posMsInternetFloor = this._internetFloorBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            //this.FloorNmbrTextBox.Text = _posMsInternetFloor.FloorNmbr.ToString();
            this.FloorTypeDropDownList.SelectedValue = _posMsInternetFloor.FloorType;
            this.FloorNameTextBox.Text = _posMsInternetFloor.FloorName;
            this.RoomCodeDropDownList.SelectedValue = _posMsInternetFloor.roomCode;
            this.DescriptionTextBox.Text = _posMsInternetFloor.Description;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsInternetFloor _posMsInternetFloor = this._internetFloorBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _posMsInternetFloor.FloorType = this.FloorTypeDropDownList.SelectedValue;
            _posMsInternetFloor.FloorName = this.FloorNameTextBox.Text;
            _posMsInternetFloor.roomCode = this.RoomCodeDropDownList.SelectedValue;
            _posMsInternetFloor.Description = this.DescriptionTextBox.Text;

            bool _result = this._internetFloorBL.Edit(_posMsInternetFloor);

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