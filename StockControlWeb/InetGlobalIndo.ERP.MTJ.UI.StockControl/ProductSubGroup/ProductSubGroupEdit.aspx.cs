using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductSubGroup
{
    public partial class ProductSubGroupEdit : ProductSubGroupBase
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowProductGroupName();

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
            MsProductSubGroup _msProductSubGroup = this._productSubGroupBL.GetSingleProductSubGroup(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.ProductSubGroupCodeTextBox.Text = _msProductSubGroup.ProductSubGrpCode;
            this.ProductSubGroupNameTextBox.Text = _msProductSubGroup.ProductSubGrpName;
            this.ProductGroupNameDropDownList.SelectedValue = _msProductSubGroup.ProductGroup;
            this.FgActiveCheckBox.Checked = (_msProductSubGroup.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _msProductSubGroup.Remark;
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
            MsProductSubGroup _msProductSubGroup = this._productSubGroupBL.GetSingleProductSubGroup(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msProductSubGroup.ProductSubGrpName = this.ProductSubGroupNameTextBox.Text;
            _msProductSubGroup.ProductGroup = this.ProductGroupNameDropDownList.SelectedValue;
            _msProductSubGroup.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msProductSubGroup.Remark = this.RemarkTextBox.Text;
            _msProductSubGroup.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _msProductSubGroup.ModifiedDate = DateTime.Now;

            bool _result = this._productSubGroupBL.EditProductSubGroup(_msProductSubGroup);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
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
    }
}