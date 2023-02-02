using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Warehouse
{
    public partial class WarehouseEdit : WarehouseBase
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
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowWarehouseGroupName();
                this.ShowWarehouseAreaName();

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
            MsWarehouse _msWarehouse = this._warehouse.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.WarehouseCodeTextBox.Text = _msWarehouse.WrhsCode;
            this.WarehouseNameTextBox.Text = _msWarehouse.WrhsName;
            this.WarehouseGroupNameDropDownList.SelectedValue = _msWarehouse.WrhsGroup;
            this.WarehouseAreaDropDownList.SelectedValue = _msWarehouse.WrhsArea;
            this.WarehouseTypeDropDownList.SelectedValue = _msWarehouse.WrhsType.ToString();
            if (_msWarehouse.WrhsType == 0)
            {
                this.SubledRadioButtonList.Enabled = false;
            }
            else
            {
                this.SubledRadioButtonList.Enabled = true;
            }
            this.SubledRadioButtonList.SelectedValue = _msWarehouse.FgSubLed.ToString();
            this.IsActiveCheckBox.Checked = WarehouseDataMapper.IsActive(_msWarehouse.FgActive);
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsWarehouse _msWarehouse = this._warehouse.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msWarehouse.WrhsName = this.WarehouseNameTextBox.Text;
            _msWarehouse.WrhsGroup = this.WarehouseGroupNameDropDownList.SelectedValue;
            _msWarehouse.WrhsArea = this.WarehouseAreaDropDownList.SelectedValue;
            _msWarehouse.WrhsType = Convert.ToByte(this.WarehouseTypeDropDownList.SelectedValue);
            _msWarehouse.FgSubLed = Convert.ToChar(this.SubledRadioButtonList.SelectedValue);
            _msWarehouse.FgActive = WarehouseDataMapper.IsActive(this.IsActiveCheckBox.Checked);
            _msWarehouse.UserID = HttpContext.Current.User.Identity.Name;
            _msWarehouse.UserDate = DateTime.Now;

            bool _result = this._warehouse.Edit(_msWarehouse);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
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

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            MsWarehouse _msWarehouse = this._warehouse.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msWarehouse.WrhsName = this.WarehouseNameTextBox.Text;
            _msWarehouse.WrhsGroup = this.WarehouseGroupNameDropDownList.SelectedValue;
            _msWarehouse.WrhsArea = this.WarehouseAreaDropDownList.SelectedValue;
            _msWarehouse.FgActive = WarehouseDataMapper.IsActive(this.IsActiveCheckBox.Checked);
            _msWarehouse.UserID = HttpContext.Current.User.Identity.Name;
            _msWarehouse.UserDate = DateTime.Now;

            bool _result = this._warehouse.Edit(_msWarehouse);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }
    }
}