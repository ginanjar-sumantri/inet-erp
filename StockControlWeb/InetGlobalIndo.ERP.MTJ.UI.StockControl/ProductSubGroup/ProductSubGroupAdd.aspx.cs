using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductSubGroup
{
    public partial class ProductSubGroupAdd : ProductSubGroupBase
    {
        private ProductBL _productSubGroupBL = new ProductBL();
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
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowProductGroupName();

                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ProductSubGroupCodeTextBox.Text = "";
            this.ProductSubGroupNameTextBox.Text = "";
            this.ProductGroupNameDropDownList.SelectedValue = "null";
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        public void ShowProductGroupName()
        {
            this.ProductGroupNameDropDownList.DataSource = this._productSubGroupBL.GetListForDDL();
            this.ProductGroupNameDropDownList.DataValueField = "ProductGrpCode";
            this.ProductGroupNameDropDownList.DataTextField = "ProductGrpName";
            this.ProductGroupNameDropDownList.DataBind();
            this.ProductGroupNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsProductSubGroup _msProductSubGroup = new MsProductSubGroup();

            _msProductSubGroup.ProductSubGrpCode = this.ProductSubGroupCodeTextBox.Text;
            _msProductSubGroup.ProductSubGrpName = this.ProductSubGroupNameTextBox.Text;
            _msProductSubGroup.ProductGroup = this.ProductGroupNameDropDownList.SelectedValue;
            _msProductSubGroup.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msProductSubGroup.Remark = this.RemarkTextBox.Text;
            _msProductSubGroup.CreatedBy = HttpContext.Current.User.Identity.Name;
            _msProductSubGroup.CreatedDate = DateTime.Now;

            bool _result = this._productSubGroupBL.AddProductSubGroup(_msProductSubGroup);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }
    }
}