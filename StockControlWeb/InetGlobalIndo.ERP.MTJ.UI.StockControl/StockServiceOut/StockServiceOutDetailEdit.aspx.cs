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
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockServiceOut
{
    public partial class StockServiceOutDetailEdit : StockServiceOutBase
    {
        private StockServiceOutBL _stockServiceOutBL = new StockServiceOutBL();
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
            this.IMEINoTextBox.Attributes.Add("ReadOnly", "true");
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.QtyTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.QtyTextBox.ClientID + "," + this.QtyTextBox.ClientID + ",500" + ");");
            this.QtyTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterQty();}");
            this.UnitTextBox.Attributes.Add("ReadOnly", "true");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            //STCServiceOutDt _sTCServiceOutDt = this._stockServiceOutBL.GetSingleSTCServiceOutDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey));
            STCServiceOutDt _sTCServiceOutDt = this._stockServiceOutBL.GetSingleSTCServiceOutDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._imeiNoKey), ApplicationConfig.EncryptionKey));

            this.IMEINoTextBox.Text = _sTCServiceOutDt.ImeiNo;
            this.ProductTextBox.Text = this._productBL.GetProductNameByCode(_sTCServiceOutDt.ProductCode);
            this.LocationTextBox.Text = this._warehouseBL.GetWarehouseLocationNameByCode(_sTCServiceOutDt.LocationCode);
            this.QtyTextBox.Text = (_sTCServiceOutDt.Qty == 0) ? "0" : _sTCServiceOutDt.Qty.ToString("###");
            this.UnitTextBox.Text = this._unitBL.GetUnitNameByCode(_sTCServiceOutDt.Unit);
            this.RemarkTextBox.Text = _sTCServiceOutDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            //STCServiceOutDt _sTCServiceOutDt = this._stockServiceOutBL.GetSingleSTCServiceOutDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey));
            STCServiceOutDt _sTCServiceOutDt = this._stockServiceOutBL.GetSingleSTCServiceOutDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._imeiNoKey), ApplicationConfig.EncryptionKey));

            _sTCServiceOutDt.ImeiNo = this.IMEINoTextBox.Text.Trim();
            _sTCServiceOutDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _sTCServiceOutDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stockServiceOutBL.EditSTCServiceOutDt(_sTCServiceOutDt);

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