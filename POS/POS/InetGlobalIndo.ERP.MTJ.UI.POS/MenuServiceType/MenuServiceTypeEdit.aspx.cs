using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using BusinessRule.POS;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.MenuServiceType
{
    public partial class MenuServiceTypeEdit : MenuServiceTypeBase
    {
        //private MemberBL _memberBL = new MemberBL();
        //private CityBL _cityBL = new CityBL();
        //private MemberTypeBL _membertypeBL = new MemberTypeBL();
        //private ReligionBL _religionBL = new ReligionBL();
        private MenuServiceTypeBL _menuServiceTypeBL = new MenuServiceTypeBL();
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
                
                this.ShowWarehouse();
                this.ShowWarehouseLocation();
                this.ShowWarehouseDepositIn();
                this.ShowLocationDeposit();                 

                this.ClearLabel();
                this.ShowData();

                this.MenuServiceTypeCodeTextbox.Attributes.Add("ReadOnly", "True");
                this.MenuServiceTypeNameTextBox.Attributes.Add("ReadOnly", "True");
            }
        }              

        protected void ShowWarehouse()
        {
            this.WarehouseDropDownList.Items.Clear();
            this.WarehouseDropDownList.DataTextField = "WrhsName";
            this.WarehouseDropDownList.DataValueField = "WrhsCode";
            this.WarehouseDropDownList.DataSource = this._menuServiceTypeBL.GetListDDLWareHouse();
            this.WarehouseDropDownList.DataBind();
            this.WarehouseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowWarehouseLocation()
        {
            this.WarehouseLocationDropDownList.Items.Clear();
            this.WarehouseLocationDropDownList.DataTextField = "WLocationName";
            this.WarehouseLocationDropDownList.DataValueField = "WLocationCode";
            this.WarehouseLocationDropDownList.DataSource = this._menuServiceTypeBL.GetListDDLWareHouseLocation(this.WarehouseDropDownList.SelectedValue);
            this.WarehouseLocationDropDownList.DataBind();
            this.WarehouseLocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //if (this.JobTitleListBox.DataSource != null)
            //{
            //    this.JobTitleListBox.Items[0].Selected = true;
            //}
        }

        protected void ShowWarehouseDepositIn()
        {
            this.WarehouseDepositinDropDownList.Items.Clear();
            this.WarehouseDepositinDropDownList.DataTextField = "WrhsName";
            this.WarehouseDepositinDropDownList.DataValueField = "WrhsCode";
            this.WarehouseDepositinDropDownList.DataSource = this._menuServiceTypeBL.GetListDDLWareHouseDepositIn();
            this.WarehouseDepositinDropDownList.DataBind();
            this.WarehouseDepositinDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //if (this.JobLevelListBox.DataSource != null)
            //{
            //    this.JobLevelListBox.Items[0].Selected = true;
            //}
        }

        protected void ShowLocationDeposit()
        {
            this.DepositLocationDropDownList.Items.Clear();
            this.DepositLocationDropDownList.DataTextField = "WLocationName";
            this.DepositLocationDropDownList.DataValueField = "WLocationCode";
            this.DepositLocationDropDownList.DataSource = this._menuServiceTypeBL.GetListDDLDepositLocation(this.WarehouseDepositinDropDownList.SelectedValue);
            this.DepositLocationDropDownList.DataBind();
            this.DepositLocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowData()
        {
            POSMsMenuServiceType _msMenuServiceType = this._menuServiceTypeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.MenuServiceTypeCodeTextbox.Text = _msMenuServiceType.MenuServiceTypeCode;           
            this.MenuServiceTypeNameTextBox.Text = _msMenuServiceType.MenuServiceTypeName;
            this.WarehouseDropDownList.SelectedValue = _msMenuServiceType.WrhsCode;
            this.ShowWarehouseLocation();
            this.WarehouseLocationDropDownList.SelectedValue = _msMenuServiceType.LocationCode;
            this.WarehouseDepositinDropDownList.SelectedValue = _msMenuServiceType.WrhsDepositInCode;
            this.ShowLocationDeposit();
            this.DepositLocationDropDownList.SelectedValue = _msMenuServiceType.LocationDepositInCode;
        }

        public void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsMenuServiceType _msMenuServiceType = this._menuServiceTypeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msMenuServiceType.MenuServiceTypeCode = this.MenuServiceTypeCodeTextbox.Text;
            _msMenuServiceType.MenuServiceTypeName = this.MenuServiceTypeNameTextBox.Text;
            _msMenuServiceType.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _msMenuServiceType.LocationCode = this.WarehouseLocationDropDownList.SelectedValue;
            _msMenuServiceType.WrhsDepositInCode = this.WarehouseDepositinDropDownList.SelectedValue;
            _msMenuServiceType.LocationDepositInCode = this.DepositLocationDropDownList.SelectedValue;

            bool _result = this._menuServiceTypeBL.EditPOSMsMenuServiceType(_msMenuServiceType);

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

        protected void WarehouseDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowWarehouseLocation();
        }
}
}