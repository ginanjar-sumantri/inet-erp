using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockAdjustment
{
    public partial class StockAdjustmentDetailEdit : StockAdjustmentBase
    {
        private StockAdjustmentBL _stockAdjustBL = new StockAdjustmentBL();
        private StockOpnameBL _stockOpnameBL = new StockOpnameBL();
        private ProductBL _product = new ProductBL();
        private WarehouseBL _warehouse = new WarehouseBL();
        private UnitBL _unit = new UnitBL();
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

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
                //this.PriceTextBox.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.QtyTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.QtyTextBox.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
            this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab");
        }

        private void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            STCAdjustHd _stcAdjustHd = this._stockAdjustBL.GetSingleSTCAdjustHd(_transNo);
            STCAdjustDt _stcAdjustDt = this._stockAdjustBL.GetSingleSTCAdjustDt(_transNo, _productCode, _locationCode);
            MsProduct _msProduct = this._product.GetSingleProduct(_stcAdjustDt.ProductCode);

            this.ProductTextBox.Text = _product.GetProductNameByCode(_productCode);
            this.LocationTextBox.Text = _warehouse.GetWarehouseLocationNameByCode(_locationCode);

            if (_stcAdjustHd.OpnameNo != "null")
            {
                this.QtyTextBox.Attributes.Add("ReadOnly", "True");
                this.QtyTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
                this.QtyTextBox.Text = (_stockOpnameBL.GetQtyOpnameSTCOpnameDt(_stcAdjustHd.OpnameNo, _productCode, _locationCode) == 0) ? "0" : _stockOpnameBL.GetQtyOpnameSTCOpnameDt(_stcAdjustHd.OpnameNo, _productCode, _locationCode).ToString("#,##0.##");
                if (_stockOpnameBL.GetQtyOpnameSTCOpnameDt(_stcAdjustHd.OpnameNo, _productCode, _locationCode) > 0)
                {
                    this.FgAdjustDropDownList.SelectedValue = "+";
                }
                else
                {
                    this.FgAdjustDropDownList.SelectedValue = "-";
                }
                this.FgAdjustDropDownList.Attributes.Add("Disabled", "True");
                this.UnitTextBox.Text = _unit.GetUnitNameByCode(_stockOpnameBL.GetUnitSTCOpnameDt(_stcAdjustHd.OpnameNo));
            }
            else
            {
                this.QtyTextBox.Attributes.Remove("ReadOnly");
                this.QtyTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
                this.QtyTextBox.Text = (_stcAdjustDt.Qty == 0) ? "0" : _stcAdjustDt.Qty.ToString("#,##0.##");
                this.UnitTextBox.Text = _unit.GetUnitNameByCode(_msProduct.Unit);
                this.FgAdjustDropDownList.SelectedValue = _stcAdjustDt.FgAdjust.ToString();
                this.FgAdjustDropDownList.Attributes.Remove("Disabled");
            }

            if (this.FgAdjustDropDownList.Text == "+")
            {
                this.PriceDetailPanel.Visible = true;
                this.PriceTextBox.Text = _stcAdjustDt.PriceCost.ToString();
            }
            else
            {
                this.PriceDetailPanel.Visible = false;
            }

            //this.PriceTextBox.Text = _stcAdjustDt.PriceCost.ToString();
            this.RemarkTextBox.Text = _stcAdjustDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.PriceTextBox.Text == "")
            {
                this.PriceTextBox.Text = "0";
            }
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            STCAdjustDt _stcAdjustDt = this._stockAdjustBL.GetSingleSTCAdjustDt(_transNo, _productCode, _locationCode);

            _stcAdjustDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcAdjustDt.TotalCost = Convert.ToDecimal(this.PriceTextBox.Text) * Convert.ToDecimal(this.QtyTextBox.Text);
            _stcAdjustDt.PriceCost = Convert.ToDecimal(this.PriceTextBox.Text);
            _stcAdjustDt.FgAdjust = Convert.ToChar(this.FgAdjustDropDownList.SelectedValue);
            _stcAdjustDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stockAdjustBL.EditSTCAdjustDt(_stcAdjustDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
        protected void FgAdjustDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FgAdjustDropDownList.SelectedValue == "-")
            {
                this.PriceTextBox.Text = "0";
                this.PriceTextBox.Attributes.Add("ReadOnly", "True");
                this.PriceTextBox.Attributes.Add("style", "background-color:#cccccc");
            }
            else
            {
                this.PriceTextBox.Text = "0";
                this.PriceTextBox.Attributes.Remove("ReadOnly");
                this.PriceTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }
    }
}