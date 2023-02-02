using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.NCC;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.DirectSales
{
    public partial class DirectSalesDetailAdd2 : DirectSalesBase
    {
        private ReceivingPOBL _receivingPOBL = new ReceivingPOBL();
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private NCPBL _ncpBL = new NCPBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private DirectSalesBL _directSalesBL = new DirectSalesBL();

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

            String _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            String _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                //this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ShowSerialNumber(_productCode);

                this.ClearLabel();
            }
        }

        public void ShowSerialNumber(String _productCode)
        {
            this.SerialNumberDropDownList.Items.Clear();
            this.SerialNumberDropDownList.DataTextField = "SerialNumber";
            this.SerialNumberDropDownList.DataValueField = "SerialNumber";
            this.SerialNumberDropDownList.DataSource = this._ncpBL.GetListSerialNoForDDL(_productCode);
            this.SerialNumberDropDownList.DataBind();
            this.SerialNumberDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SetAttribute()
        {
            //this.DateTextBox.Attributes.Add("ReadOnly", "True");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _wrhsCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._Warehouse), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._Location), ApplicationConfig.EncryptionKey);

            SALTrDirectSalesDt_SerialNmbr _salTrDirectSalesSerialNumber = new SALTrDirectSalesDt_SerialNmbr();

            _salTrDirectSalesSerialNumber.ProductCode = _productCode;
            _salTrDirectSalesSerialNumber.TransNmbr = _transNo;
            _salTrDirectSalesSerialNumber.WrhsCode = _wrhsCode;
            _salTrDirectSalesSerialNumber.WLocationCode = _locationCode;
            _salTrDirectSalesSerialNumber.SerialNmbr = this.SerialNumberDropDownList.SelectedValue;

            bool _result = _directSalesBL.AddSerialNumber(_salTrDirectSalesSerialNumber);
            
            if (_result == true)
            {
                Response.Redirect(this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._Warehouse + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._Warehouse)) + "&" + this._Location + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._Location)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewDetailPage + "?" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._Warehouse + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._Warehouse)) + "&" + this._Location + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._Location)));
        }
    }
}