using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl
{
    public partial class ProductTypeAdd : ProductTypeBase
    {
        private ProductBL _prodTypeBL = new ProductBL();
        private AccountBL _accBL = new AccountBL();
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
                this.PageTitleLiteral.Text = "Product Type";

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ProductTypeCodeTextBox.Text = "";
            this.ProductTypeNameTextBox.Text = "";
            this.CategoryDropDownList.SelectedValue = "null";
            this.StockLabel.Text = "";
            this.StockHiddenField.Value = "";
            this.IsUsingPGCheckBox.Checked = false;
            this.IsUsingUniqueIDCheckBox.Checked = false;
            this.FgActiveCheckBox.Checked = false;
            this.RemarkTextBox.Text = "";
        }

        protected void CategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CategoryDropDownList.SelectedValue != "null")
            {
                if (this.CategoryDropDownList.SelectedValue == "Subkon" || this.CategoryDropDownList.SelectedValue == "Other")
                {
                    this.StockLabel.Text = "No";
                    this.StockHiddenField.Value = (ProductTypeDataMapper.IsActive(YesNo.No)).ToString();
                }
                else
                {
                    this.StockLabel.Text = "Yes";
                    this.StockHiddenField.Value = (ProductTypeDataMapper.IsActive(YesNo.Yes)).ToString();
                }
            }
            else
            {
                this.StockLabel.Text = "";
                this.StockHiddenField.Value = "";
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            MsProductType _msProductType = new MsProductType();

            _msProductType.ProductTypeCode = this.ProductTypeCodeTextBox.Text;
            _msProductType.ProductTypeName = this.ProductTypeNameTextBox.Text;
            _msProductType.ProductCategory = this.CategoryDropDownList.SelectedValue;
            _msProductType.FgStock = Convert.ToChar(this.StockHiddenField.Value);
            _msProductType.IsUsingPG = this.IsUsingPGCheckBox.Checked;
            _msProductType.IsUsingUniqueID = this.IsUsingUniqueIDCheckBox.Checked;
            _msProductType.fgSendToKitchen = this.SendToKitchenCheckBox.Checked;
            _msProductType.fgTax = this.WithTaxCheckBox.Checked;
            if (this.WithTaxCheckBox.Checked)
            {
                _msProductType.TaxTypeCode = this.TaxTypeDropDownList.SelectedValue;
                _msProductType.TaxValue = ((this.TaxPercentageTextBox.Text == "") ? 1 : Convert.ToDecimal(this.TaxPercentageTextBox.Text));
            }
            else
            {
                _msProductType.TaxTypeCode = "";
                _msProductType.TaxValue = 1;
            }
            _msProductType.ServiceCharges = ((this.ServiceChargerTextBox.Text == "") ? 0 : Convert.ToDecimal(this.ServiceChargerTextBox.Text));
            _msProductType.fgServiceChargesCalculate = this.ServiceChargesCalculateCheckBox.Checked;
            _msProductType.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msProductType.Remark = this.RemarkTextBox.Text;
            _msProductType.CreatedBy = HttpContext.Current.User.Identity.Name;
            _msProductType.CreatedDate = DateTime.Now;

            bool _result = this._prodTypeBL.AddProductType(_msProductType);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ProductTypeCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
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

        private void ShowTaxDDL()
        {
            this.TaxTypeDropDownList.Items.Clear();
            this.TaxTypeDropDownList.DataTextField = "TaxTypeName";
            this.TaxTypeDropDownList.DataValueField = "TaxTypeCode";
            this.TaxTypeDropDownList.DataSource = this._prodTypeBL.GetTaxDDL();
            this.TaxTypeDropDownList.DataBind();
            //this.TaxTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void WithTaxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.WithTaxCheckBox.Checked == true)
            {
                this.TaxDiv.Visible = true;
                this.ShowTaxDDL();
                this.TaxPercentageTextBox.Text = this._prodTypeBL.GetSingleTaxPercent(this.TaxTypeDropDownList.SelectedValue).ToString("##0.##");
            }
            else
            {
                this.TaxDiv.Visible = false;
            }
        }

        protected void TaxTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TaxPercentageTextBox.Text = this._prodTypeBL.GetSingleTaxPercent(this.TaxTypeDropDownList.SelectedValue).ToString("##0.##");
        }
    }
}