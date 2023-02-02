using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockIssueSlip
{
    public partial class StockIssueSlipDetailAdd : StockIssueSlipBase
    {
        private StockIssueSlipBL _stockIssueSlipBL = new StockIssueSlipBL();
        private StockIssueRequestBL _stockIssueRequestBL = new StockIssueRequestBL();
        private StockTypeBL _stockTypeBL = new StockTypeBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private UnitBL _unitBL = new UnitBL();
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

                this.ShowStockType();

                this.SetAttribute();
                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearData()
        {
            STCIssueSlipHd _stcIssueSlipHd = this._stockIssueSlipBL.GetSingleSTCIssueSlipHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_stcIssueSlipHd.RequestNo != "null")
            {
                //this.ShowProduct();
                this.ShowLocation();
                this.QtyTextBox.Text = _stockIssueRequestBL.GetQtyVSTRequestForIssuesByReqNo(_stcIssueSlipHd.RequestNo, this.ProductPicker1.ProductCode).ToString("#,##0.##");
                this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_stockIssueRequestBL.GetUnitVSTRequestForIssuesByReqNo(_stcIssueSlipHd.RequestNo, this.ProductPicker1.ProductCode));
            }
            else
            {
                //this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                //this.ProductDropDownList.SelectedValue = "null";
                this.ProductPicker1.ProductCode = "";
                this.ProductPicker1.ProductName = "";
                this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.LocationDropDownList.SelectedValue = "null";
                this.QtyTextBox.Text = "";
                this.UnitTextBox.Text = "";
            }
            this.RemarkTextBox.Text = "";
            this.StockTypeDropDownList.SelectedValue = "null";
        }

        //private void ShowProduct()
        //{
        //    STCIssueSlipHd _stcIssueSlipHd = this._stockIssueSlipBL.GetSingleSTCIssueSlipHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._stockIssueRequestBL.GetListProductForDDLIssueSlipDt(_stcIssueSlipHd.RequestNo);
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowLocation()
        {
            STCIssueSlipHd _stcIssueSlipHd = this._stockIssueSlipBL.GetSingleSTCIssueSlipHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcIssueSlipHd.WrhsCode);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowStockType()
        {
            this.StockTypeDropDownList.Items.Clear();
            this.StockTypeDropDownList.DataTextField = "StockTypeName";
            this.StockTypeDropDownList.DataValueField = "StockTypeCode";
            this.StockTypeDropDownList.DataSource = this._stockTypeBL.GetListForDDLStockIssueSlip();
            this.StockTypeDropDownList.DataBind();
            this.StockTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCIssueSlipDt _stcIssueSlipDt = new STCIssueSlipDt();
            STCIssueSlipHd _stcIssueSlipHd = this._stockIssueSlipBL.GetSingleSTCIssueSlipHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _stcIssueSlipDt.TransNmbr = _transNo;
            _stcIssueSlipDt.ProductCode = this.ProductPicker1.ProductCode;
            _stcIssueSlipDt.LocationCode = this.LocationDropDownList.SelectedValue;
            _stcIssueSlipDt.Unit = _stockIssueRequestBL.GetUnitVSTRequestForIssuesByReqNo(_stcIssueSlipHd.RequestNo, this.ProductPicker1.ProductCode);
            _stcIssueSlipDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcIssueSlipDt.StockType = this.StockTypeDropDownList.SelectedValue;
            _stcIssueSlipDt.TotalCost = 0;
            _stcIssueSlipDt.PriceCost = 0;
            _stcIssueSlipDt.Remark = this.RemarkTextBox.Text;

            STCIssueSlipDt _stcIssueSlip = this._stockIssueSlipBL.GetSingleSTCIssueSlipDt(_transNo, this.ProductPicker1.ProductCode, this.LocationDropDownList.SelectedValue);
            if (_stcIssueSlip == null)
            {
                bool _result = this._stockIssueSlipBL.AddSTCIssueSlipDt(_stcIssueSlipDt);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "Product not exits in stock issue request";
                }
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Product is exits";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            STCIssueSlipHd _stcIssueSlipHd = this._stockIssueSlipBL.GetSingleSTCIssueSlipHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.QtyTextBox.Text = _stockIssueRequestBL.GetQtyVSTRequestForIssuesByReqNo(_stcIssueSlipHd.RequestNo, this.ProductPicker1.ProductCode).ToString("#,##0.##");
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_stockIssueRequestBL.GetUnitVSTRequestForIssuesByReqNo(_stcIssueSlipHd.RequestNo, this.ProductPicker1.ProductCode));
        }
    }
}