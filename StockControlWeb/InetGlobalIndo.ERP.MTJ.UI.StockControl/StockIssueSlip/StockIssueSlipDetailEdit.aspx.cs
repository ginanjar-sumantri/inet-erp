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
    public partial class StockIssueSlipDetailEdit : StockIssueSlipBase
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

                this.ShowStockType();

                this.SetAttribute();
                this.ClearLabel();
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
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            STCIssueSlipHd _stcIssueSlipHd = this._stockIssueSlipBL.GetSingleSTCIssueSlipHd(_transNo);
            STCIssueSlipDt _stcIssueSlipDt = this._stockIssueSlipBL.GetSingleSTCIssueSlipDt(_transNo, _productCode, _locationCode);

            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_stcIssueSlipDt.ProductCode);
            this.LocationTextBox.Text = _warehouseBL.GetWarehouseLocationNameByCode(_stcIssueSlipDt.LocationCode);
            this.QtyTextBox.Text = (_stcIssueSlipDt.Qty == 0) ? "0" : _stcIssueSlipDt.Qty.ToString("#,##0.##");
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_stcIssueSlipDt.Unit);
            this.RemarkTextBox.Text = _stcIssueSlipDt.Remark;
            this.StockTypeDropDownList.SelectedValue = _stcIssueSlipDt.StockType;
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
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            STCIssueSlipHd _stcIssueSlipHd = this._stockIssueSlipBL.GetSingleSTCIssueSlipHd(_transNo);
            STCIssueSlipDt _stcIssueSlipDt = this._stockIssueSlipBL.GetSingleSTCIssueSlipDt(_transNo, _productCode, _locationCode);
            STCRequestDt _stcRequestDt = this._stockIssueRequestBL.GetSingleSTCRequestDt(_stcIssueSlipHd.RequestNo, _stcIssueSlipDt.ProductCode);

            _stcIssueSlipDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcIssueSlipDt.StockType = this.StockTypeDropDownList.SelectedValue;
            _stcIssueSlipDt.Remark = this.RemarkTextBox.Text;

            if (Convert.ToDecimal(_stcRequestDt.Qty) >= Convert.ToDecimal(_stcIssueSlipDt.Qty))
            {
                bool _result = this._stockIssueSlipBL.EditSTCIssueSlipDt(_stcIssueSlipDt);

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
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Input your quantity over than the request";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}