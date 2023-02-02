using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.DirectBillOfLading
{
    public partial class DirectBillOfLadingDetailEdit : DirectBillOfLadingBase
    {
        private DirectBillOfLadingBL _directBillOfLadingBL = new DirectBillOfLadingBL();
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();
        private BillOfLadingBL _billOfLadingBL = new BillOfLadingBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();

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

                this.ClearLabel();
                this.SetAttribute();

                this.showLocation();

                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + QtyTextBox.ClientID + ");");
        }

        private void showLocation()
        {
            STCTrDirectSJHd _stcSJHd = this._directBillOfLadingBL.GetSingleSTCTrDirectSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationNameDropDownList.Items.Clear();
            this.LocationNameDropDownList.DataTextField = "WLocationName";
            this.LocationNameDropDownList.DataValueField = "WLocationCode";
            this.LocationNameDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcSJHd.WrhsCode);
            this.LocationNameDropDownList.DataBind();
            this.LocationNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.LocationNameDropDownList.SelectedValue = _stcSJHd.LocationCode;

        }

        private void ShowData()
        {
            string _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _SONo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._SOKey), ApplicationConfig.EncryptionKey);

            STCTrDirectSJProduct _STCTrDirectSJProduct = this._directBillOfLadingBL.GetSingleSTCTrDirectSJProduct(_transNmbr, _SONo, _productCode);

            this.TransNoTextBox.Text = _transNmbr;
            this.SONoTextBox.Text = _SONo;
            this.ProductCodeTextBox.Text = _productCode;
            this.ProductNameTextBox.Text = _productBL.GetProductNameByCode(_productCode);
            this.QtyTextBox.Text = (_STCTrDirectSJProduct.Qty == 0) ? "0" : _STCTrDirectSJProduct.Qty.ToString("#,##0.##");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _soNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._SOKey), ApplicationConfig.EncryptionKey);

            

            STCTrDirectSJProduct _STCTrDirectSJProduct = this._directBillOfLadingBL.GetSingleSTCTrDirectSJProduct(_transNmbr, _soNo, _productNo);

            _STCTrDirectSJProduct.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _STCTrDirectSJProduct.LocationCode = this.LocationNameDropDownList.SelectedValue;


            bool _result = this._directBillOfLadingBL.EditSTCTrDirectSJProduct();

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}