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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockOpname
{
    public partial class StockOpnameDetailEdit : StockOpnameBase
    {
        private NameValueCollectionExtractor _nvcExtractor;
        private StockOpnameBL _stockOpnameBL = new StockOpnameBL();
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
            this.QtySystemTextBox.Attributes.Add("OnBlur", "Calculate(" + this.QtySystemTextBox.ClientID + "," + this.QtyActualTextBox.ClientID + "," + this.QtyOpnameTextBox.ClientID + ");");
            this.QtyActualTextBox.Attributes.Add("OnBlur", "Calculate(" + this.QtySystemTextBox.ClientID + "," + this.QtyActualTextBox.ClientID + "," + this.QtyOpnameTextBox.ClientID + ");");

            this.QtySystemTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.QtyActualTextBox.Attributes.Add("OnKeyDown", "return Numeric();");

            this.QtyOpnameTextBox.Attributes.Add("ReadOnly", "true");
            this.UnitTextBox.Attributes.Add("ReadOnly", "true");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            STCOpnameDt _stcOpnameDt = this._stockOpnameBL.GetSingleSTCOpnameDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey));

            this.ProductTextBox.Text = this._productBL.GetProductNameByCode(_stcOpnameDt.ProductCode);
            this.LocationTextBox.Text = this._warehouseBL.GetWarehouseLocationNameByCode(_stcOpnameDt.LocationCode);
            this.QtySystemTextBox.Text = (_stcOpnameDt.QtySystem == 0) ? "0" : _stcOpnameDt.QtySystem.ToString("###");
            this.QtyActualTextBox.Text = (_stcOpnameDt.QtyActual == 0) ? "0" : _stcOpnameDt.QtyActual.ToString("###");
            this.QtyOpnameTextBox.Text = (_stcOpnameDt.QtyOpname == 0) ? "0" : _stcOpnameDt.QtyOpname.ToString("###");
            this.UnitTextBox.Text = this._unitBL.GetUnitNameByCode(_stcOpnameDt.Unit);
            this.RemarkTextBox.Text = _stcOpnameDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCOpnameDt _stcOpnameDt = this._stockOpnameBL.GetSingleSTCOpnameDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey));

            _stcOpnameDt.QtySystem = Convert.ToDecimal(this.QtySystemTextBox.Text);
            _stcOpnameDt.QtyActual = Convert.ToDecimal(this.QtyActualTextBox.Text);
            _stcOpnameDt.QtyOpname = Convert.ToDecimal(this.QtyOpnameTextBox.Text);
            _stcOpnameDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stockOpnameBL.EditSTCOpnameDt(_stcOpnameDt);

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