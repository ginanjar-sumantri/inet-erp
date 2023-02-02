using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.UI.Billing.BillingInvoice;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.ProductConfiguration
{
    public partial class ProductConfigurationAdd : ProductConfigurationBase
    {
        private ProductConfigurationBL _productConfigurationBL = new ProductConfigurationBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();

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

                //this.ShowProductDrowDownList();
                this.SetAttribute();

                this.ClearLabel();
                this.ClearData();
            }
        }

        private void SetAttribute()
        {
            this.ContractMonthTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.ContractMonthTextBox.ClientID + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ClearData()
        {
            //this.ProductDropDownList.SelectedValue = "null";
            this.ProductPicker1.ProductCode = "";
            this.ProductPicker1.ProductName = "";
            this.IsPostponeCheckbox.Checked = false;
        }

        //private void ShowProductDrowDownList()
        //{
        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataSource = this._productBL.GetListForDDLProductNonStock();
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Master_ProductExtension _masterProductExtension = new Master_ProductExtension();

            _masterProductExtension.ProductCode = this.ProductPicker1.ProductCode;
            _masterProductExtension.IsPostponeAllowed = this.IsPostponeCheckbox.Checked;
            _masterProductExtension.IsChangePriceAllowed = this.ChangePriceCheckBox.Checked;
            _masterProductExtension.IsMRC = this.MRCCheckBox.Checked;
            _masterProductExtension.MinContractMonth = Convert.ToInt32(this.ContractMonthTextBox.Text);

            _masterProductExtension.InsertBy = HttpContext.Current.User.Identity.Name;
            _masterProductExtension.InsertDate = DateTime.Now;
            _masterProductExtension.EditBy = HttpContext.Current.User.Identity.Name;
            _masterProductExtension.EditDate = DateTime.Now;

            bool _result = this._productConfigurationBL.AddProductExtension(_masterProductExtension);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
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
