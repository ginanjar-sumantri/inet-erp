using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
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
    public partial class ProductTypeEdit : ProductTypeBase
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
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ClearLabel();
                this.ShowTaxDDL();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowData()
        {
            MsProductType _msProductType = this._prodTypeBL.GetSingleProductType(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.ProductTypeCodeTextBox.Text = _msProductType.ProductTypeCode;
            this.ProductTypeNameTextBox.Text = _msProductType.ProductTypeName;
            this.CategoryDropDownList.SelectedValue = _msProductType.ProductCategory;
            this.StockLabel.Text = ProductTypeDataMapper.GetActiveText(_msProductType.FgStock);
            this.IsUsingPGCheckBox.Checked = _msProductType.IsUsingPG;
            this.IsUsingUniqueIDCheckBox.Checked = _msProductType.IsUsingUniqueID;
            this.StockHiddenField.Value = _msProductType.FgStock.ToString();

            this.SendToKitchenCheckBox.Checked = Convert.ToBoolean(_msProductType.fgSendToKitchen);
            this.WithTaxCheckBox.Checked = Convert.ToBoolean(_msProductType.fgTax);
            if (Convert.ToBoolean(_msProductType.fgTax))
            {
                this.TaxDiv.Visible = true;
                this.TaxTypeDropDownList.SelectedValue = _msProductType.TaxTypeCode;
                this.TaxPercentageTextBox.Text = Convert.ToDecimal(_msProductType.TaxValue).ToString("##0.##");
                this.ServiceChargerTextBox.Text = Convert.ToDecimal(_msProductType.ServiceCharges).ToString("##0.##");
                this.ServiceChargesCalculateCheckBox.Checked = Convert.ToBoolean(_msProductType.fgServiceChargesCalculate);
            }
            this.FgActiveCheckBox.Checked = (_msProductType.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _msProductType.Remark;
        }

        protected void CategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CategoryDropDownList.SelectedValue != "null")
            {
                if (this.CategoryDropDownList.SelectedValue == "Subkon" || this.CategoryDropDownList.SelectedValue == "Other")
                {
                    this.StockLabel.Text = "No";
                    this.StockHiddenField.Value = ProductTypeDataMapper.IsActive(YesNo.No).ToString();
                }
                else
                {
                    this.StockLabel.Text = "Yes";
                    this.StockHiddenField.Value = ProductTypeDataMapper.IsActive(YesNo.Yes).ToString();
                }
            }
            else
            {
                this.StockLabel.Text = "";
                this.StockHiddenField.Value = "";
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsProductType _msProductType = this._prodTypeBL.GetSingleProductType(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msProductType.ProductTypeName = this.ProductTypeNameTextBox.Text;
            _msProductType.ProductCategory = this.CategoryDropDownList.SelectedValue;
            _msProductType.FgStock = Convert.ToChar(this.StockHiddenField.Value);
            _msProductType.IsUsingPG = this.IsUsingPGCheckBox.Checked;
            _msProductType.IsUsingUniqueID = this.IsUsingUniqueIDCheckBox.Checked;
            _msProductType.fgSendToKitchen = this.SendToKitchenCheckBox.Checked;
            _msProductType.fgTax = this.WithTaxCheckBox.Checked;
            _msProductType.TaxTypeCode = this.TaxTypeDropDownList.SelectedValue;
            _msProductType.TaxValue = ((this.TaxPercentageTextBox.Text == "") ? 0 : Convert.ToDecimal(this.TaxPercentageTextBox.Text));
            _msProductType.ServiceCharges = ((this.ServiceChargerTextBox.Text == "") ? 0 : Convert.ToDecimal(this.ServiceChargerTextBox.Text));
            _msProductType.fgServiceChargesCalculate = this.ServiceChargesCalculateCheckBox.Checked;
            _msProductType.FgActive = (this.FgActiveCheckBox.Checked == true) ? 'Y' : 'N';
            _msProductType.Remark = this.RemarkTextBox.Text;
            _msProductType.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _msProductType.ModifiedDate = DateTime.Now;

            bool _result = this._prodTypeBL.EditProductType(_msProductType);

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
            Response.Redirect(_homePage);
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
            MsProductType _msProductType = this._prodTypeBL.GetSingleProductType(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

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
            _msProductType.ModifiedBy = HttpContext.Current.User.Identity.Name;
            _msProductType.ModifiedDate = DateTime.Now;

            bool _result = this._prodTypeBL.EditProductType(_msProductType);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
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