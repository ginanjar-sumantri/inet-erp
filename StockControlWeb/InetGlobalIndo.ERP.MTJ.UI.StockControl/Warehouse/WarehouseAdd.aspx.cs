using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Warehouse
{
    public partial class WarehouseAdd : WarehouseBase
    {
        private WarehouseBL _warehouse = new WarehouseBL();
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

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowWarehouseGroupName();
                this.ShowWarehouseAreaName();

                this.ClearData();
            }
        }

        public void ClearData()
        {
            this.WarningLabel.Text = "";
            this.WarehouseCodeTextBox.Text = "";
            this.WarehouseNameTextBox.Text = "";
            this.WarehouseGroupNameDropDownList.SelectedValue = "null";
            this.WarehouseAreaDropDownList.SelectedValue = "null";
            this.IsActiveCheckBox.Checked = true;
        }

        public void ShowWarehouseGroupName()
        {
            this.WarehouseGroupNameDropDownList.DataSource = this._warehouse.GetListWrhsGroupForDDL();
            this.WarehouseGroupNameDropDownList.DataValueField = "WrhsGroupCode";
            this.WarehouseGroupNameDropDownList.DataTextField = "WrhsGroupName";
            this.WarehouseGroupNameDropDownList.DataBind();
            this.WarehouseGroupNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowWarehouseAreaName()
        {
            this.WarehouseAreaDropDownList.DataSource = this._warehouse.GetListWrhsAreaForDDL();
            this.WarehouseAreaDropDownList.DataValueField = "WrhsAreaCode";
            this.WarehouseAreaDropDownList.DataTextField = "WrhsAreaName";
            this.WarehouseAreaDropDownList.DataBind();
            this.WarehouseAreaDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void WarehouseTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.WarehouseTypeDropDownList.SelectedValue != "0")
            {
                this.SubledRadioButtonList.Enabled = true;
            }
            else
            {
                this.SubledRadioButtonList.Enabled = false;
                this.SubledRadioButtonList.SelectedValue = (WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled)).ToString();
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            MsWarehouse _msWarehouse = new MsWarehouse();

            _msWarehouse.WrhsCode = this.WarehouseCodeTextBox.Text;
            _msWarehouse.WrhsName = this.WarehouseNameTextBox.Text;
            _msWarehouse.WrhsGroup = this.WarehouseGroupNameDropDownList.SelectedValue;
            _msWarehouse.WrhsArea = this.WarehouseAreaDropDownList.SelectedValue;
            _msWarehouse.WrhsType = Convert.ToByte(this.WarehouseTypeDropDownList.SelectedValue);
            _msWarehouse.FgSubLed = Convert.ToChar(this.SubledRadioButtonList.SelectedValue);
            _msWarehouse.FgActive = WarehouseDataMapper.IsActive(this.IsActiveCheckBox.Checked);
            _msWarehouse.UserID = HttpContext.Current.User.Identity.Name;
            _msWarehouse.UserDate = DateTime.Now;

            bool _result = this._warehouse.Add(_msWarehouse);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.WarehouseCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
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