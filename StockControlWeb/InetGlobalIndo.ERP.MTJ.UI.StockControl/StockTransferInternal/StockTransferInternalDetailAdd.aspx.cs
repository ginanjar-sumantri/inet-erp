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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransferInternal
{
    public partial class StockTransferInternalDetailAdd : StockTransferInternalBase
    {
        private StockTransferInternalBL _stcTransInternalBL = new StockTransferInternalBL();
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

                this.SetAttribute();
                this.ShowLocation1();
                this.ShowLocation2();
                //this.ShowProduct();
                this.ClearLabel();
                this.ClearData();
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                STCTransferHd _stcTransInternalHd = this._stcTransInternalBL.GetSingleSTCTransferHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                this.UnitTextBox.Text = _productBL.GetUnitNameByCode(this.ProductPicker1.ProductCode);
            }
        }

        private void SetAttribute()
        {
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            //this.ProductDropDownList.SelectedValue = "null";
            this.ProductPicker1.ProductCode = "";
            this.ProductPicker1.ProductName = "";
            this.LocationSrcDropDownList.SelectedValue = "null";
            this.LocationDestDropDownList.SelectedValue = "null";
            this.QtyTextBox.Text = "0";
            this.UnitTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        //private void ShowProduct()
        //{
        //    STCTransferHd _stcTransInternalHd = this._stcTransInternalBL.GetSingleSTCTransferHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._productBL.GetListProductActiveForDDL();
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowLocation1()
        {
            STCTransferHd _stcTransInternalHd = this._stcTransInternalBL.GetSingleSTCTransferHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationSrcDropDownList.Items.Clear();
            this.LocationSrcDropDownList.DataTextField = "WLocationName";
            this.LocationSrcDropDownList.DataValueField = "WLocationCode";
            this.LocationSrcDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcTransInternalHd.WrhsSrc);
            this.LocationSrcDropDownList.DataBind();
            this.LocationSrcDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowLocation2()
        {
            STCTransferHd _stcTransInternalHd = this._stcTransInternalBL.GetSingleSTCTransferHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDestDropDownList.Items.Clear();
            this.LocationDestDropDownList.DataTextField = "WLocationName";
            this.LocationDestDropDownList.DataValueField = "WLocationCode";
            this.LocationDestDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcTransInternalHd.WrhsDest);
            this.LocationDestDropDownList.DataBind();
            this.LocationDestDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTransferDt _stcTransInternalDt = new STCTransferDt();
            STCTransferHd _stcTransInternalHd = this._stcTransInternalBL.GetSingleSTCTransferHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _stcTransInternalDt.TransNmbr = _transNo;
            _stcTransInternalDt.ProductCode = this.ProductPicker1.ProductCode;
            _stcTransInternalDt.LocationSrc = this.LocationSrcDropDownList.SelectedValue;
            _stcTransInternalDt.LocationDest = this.LocationDestDropDownList.SelectedValue;
            _stcTransInternalDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcTransInternalDt.Unit = _productBL.GetUnitCodeByCode(this.ProductPicker1.ProductCode);
            _stcTransInternalDt.PriceCost = 0;
            _stcTransInternalDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stcTransInternalBL.AddSTCTransferDt(_stcTransInternalDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
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

        //protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    STCTransferHd _stcTransInternalHd = this._stcTransInternalBL.GetSingleSTCTransferHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
        //    //this.QtyTextBox.Text = _stockTransReq.GetQtyFromVSTTransferReqForSJ(_stcTransInternalHd.RequestNo, this.ProductDropDownList.SelectedValue).ToString("#,##0.##") ;
        //    this.UnitTextBox.Text = _productBL.GetUnitNameByCode(this.ProductPicker1.ProductCode);
        //}
    }
}