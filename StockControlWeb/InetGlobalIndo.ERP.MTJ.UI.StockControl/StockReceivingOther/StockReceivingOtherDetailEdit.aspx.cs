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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockReceivingOther
{
    public partial class StockReceivingOtherDetailEdit : StockReceivingOtherBase
    {
        private StockReceivingOtherBL _stockReceivingOtherBL = new StockReceivingOtherBL();
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
                String spawnJS = "<script language='JavaScript'>\n";

                ////////////////////DECLARE FUNCTION FOR CALCULATING TOTALCOST
                spawnJS += "function calculateLineTotal(x) {\n";
                spawnJS += "if ( x.value != '' ) {\n";
                spawnJS += "document.getElementById('" + this.TotalCostTextBox.ClientID + "').value = (document.getElementById('" + this.QtyTextBox.ClientID + "').value * document.getElementById('" + this.PriceCostTextBox.ClientID + "').value).toFixed(2);\n";
                //spawnJS += "document.getElementById('" + this.txtLineTotal.ClientID + "').value = (document.getElementById('" + this.qty.ClientID + "').value * document.getElementById('" + this.price.ClientID + "').value).toFixed(2);\n";
                spawnJS += "}\n}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttribute()
        {
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return Numeric();");

            this.UnitTextBox.Attributes.Add("ReadOnly", "true");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.PriceCostTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.PriceCostTextBox.ClientID + "," + this.PriceCostTextBox.ClientID + ",500" + ");");
            this.QtyTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.QtyTextBox.ClientID + "," + this.QtyTextBox.ClientID + ",500" + ");");

            this.QtyTextBox.Attributes.Add("onkeyup", "numericInput(this);calculateLineTotal(this)");
            this.QtyTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterQty();}");

            this.PriceCostTextBox.Attributes.Add("onkeyup", "numericInput(this);calculateLineTotal(this)");
            this.PriceCostTextBox.Attributes.Add("OnBlur", "numericInput(this);calculateLineTotal(this)");
            this.PriceCostTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterQty();}"); 
        }

        public void ShowData()
        {
            STCRROtherDt _stcRROtherDt = this._stockReceivingOtherBL.GetSingleSTCRROtherDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey));

            this.ProductTextBox.Text = this._productBL.GetProductNameByCode(_stcRROtherDt.ProductCode);
            this.LocationTextBox.Text = this._warehouseBL.GetWarehouseLocationNameByCode(_stcRROtherDt.LocationCode);
            this.QtyTextBox.Text = (_stcRROtherDt.Qty == 0) ? "0" : _stcRROtherDt.Qty.ToString("###");
            this.UnitTextBox.Text = this._unitBL.GetUnitNameByCode(_stcRROtherDt.Unit);
            this.RemarkTextBox.Text = _stcRROtherDt.Remark;            
            this.TotalCostTextBox.Text = _stcRROtherDt.TotalCost.ToString();
            if (_stcRROtherDt.FgConsignment == true)
            {
                this.PriceCostTextBox.Attributes.Add("ReadOnly", "true");
                this.PriceCostTextBox.Text = Convert.ToDecimal(_stcRROtherDt.PriceCost).ToString("#0.00");
            }
            else
            {
                this.PriceCostTextBox.Text = Convert.ToDecimal(_stcRROtherDt.PriceCost).ToString("#0.00");
            }
            this.TotalCostTextBox.Text = Convert.ToDecimal(_stcRROtherDt.TotalCost).ToString("#0.00");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCRROtherDt _stcRROtherDt = this._stockReceivingOtherBL.GetSingleSTCRROtherDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey));

            _stcRROtherDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcRROtherDt.Remark = this.RemarkTextBox.Text;
            _stcRROtherDt.PriceCost = Convert.ToDecimal(this.PriceCostTextBox.Text);
            _stcRROtherDt.TotalCost = Convert.ToDecimal(this.TotalCostTextBox.Text);

            bool _result = this._stockReceivingOtherBL.EditSTCRROtherDt(_stcRROtherDt);

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
            this.ShowData();
        }
    }
}