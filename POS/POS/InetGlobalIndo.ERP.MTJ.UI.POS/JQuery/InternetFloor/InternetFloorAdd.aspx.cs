using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.InternetFloor
{
    public partial class InternetFloorAdd : InternetFloorBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.FloorNmbrTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.FloorNmbrTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.RoomCodeDropDownList.DataSource = _internetFloorBL.GetListRoomCode();
                this.RoomCodeDropDownList.DataValueField = this.RoomCodeDropDownList.DataTextField = "roomCode";
                this.RoomCodeDropDownList.DataBind();

                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.FloorNmbrTextBox.Text = "";
            this.FloorNameTextBox.Text = "";
            this.DescriptionTextBox.Text = "";

        }

        protected void CheckValidData()
        {
            this.ClearLabel();
            String _cekFloorType = this._internetFloorBL.GetSingle(this.FloorTypeDropDownList.SelectedValue.Trim() + "-" + this.FloorNmbrTextBox.Text.Trim()).FloorType;
            if (_cekFloorType != "")
                this.WarningLabel.Text = this.WarningLabel.Text + " FloorNmbr : " + this.FloorNmbrTextBox.Text.Trim() + " for FloorType = " + this.FloorTypeDropDownList.SelectedValue.Trim() + " Already Exist.";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsInternetFloor _posMsInternetFloor = new POSMsInternetFloor();

                _posMsInternetFloor.FloorType = this.FloorTypeDropDownList.SelectedValue;
                _posMsInternetFloor.FloorNmbr = Convert.ToInt32(this.FloorNmbrTextBox.Text);
                _posMsInternetFloor.FloorName = this.FloorNameTextBox.Text;
                _posMsInternetFloor.roomCode = this.RoomCodeDropDownList.SelectedValue;
                _posMsInternetFloor.fgActive = true;
                _posMsInternetFloor.Description = this.DescriptionTextBox.Text;

                bool _result = this._internetFloorBL.Add(_posMsInternetFloor);

                if (_result == true)
                {
                    Response.Redirect(this._homePage);
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Add Data";
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}