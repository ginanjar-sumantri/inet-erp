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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.InternetTable
{
    public partial class InternetTableAdd : InternetTableBase
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

                this.TableNmbrTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.TableNmbrTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.FloorNmbrTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.FloorNmbrTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.positionXTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.positionXTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.positionYTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.positionYTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

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

            this.TableNmbrTextBox.Text = "";
            this.FloorNmbrTextBox.Text = "";
            this.StatusDropDownList.SelectedValue = "0";
            this.fgActiveCheckBox.Checked = false;
            this.positionXTextBox.Text = "";
            this.positionYTextBox.Text = "";
        }
        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsInternetTable _posMsInternetTable = new POSMsInternetTable();

            _posMsInternetTable.FloorType = this.FloorTypeDropDownList.SelectedValue;
            _posMsInternetTable.TableNmbr = this.TableNmbrTextBox.Text;
            _posMsInternetTable.FloorNmbr = Convert.ToInt32(this.FloorNmbrTextBox.Text);
            _posMsInternetTable.Status = Convert.ToByte(this.StatusDropDownList.SelectedValue);
            _posMsInternetTable.fgActive = this.fgActiveCheckBox.Checked;
            //_posMsInternetTable.positionX = Convert.ToInt32(this.positionXTextBox.Text);
            //_posMsInternetTable.positionY = Convert.ToInt32(this.positionYTextBox.Text);

            bool _result = this._internetTableBL.Add(_posMsInternetTable);

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