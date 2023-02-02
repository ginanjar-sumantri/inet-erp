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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransferExternalSJ
{
    public partial class StockTransferExternalSJDetailAdd : StockTransferExternalSJBase
    {
        private StockTransferExternalSJBL _stcTransOutBL = new StockTransferExternalSJBL();
        private StockTransRequestBL _stockTransReqBL = new StockTransRequestBL();
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

                this.ShowLocation();
                //this.ShowProduct();

                this.SetAttribute();
                this.ClearData();
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                STCTransOutHd _stcTransOutHd = this._stcTransOutBL.GetSingleSTCTransOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                this.QtyTextBox.Text = _stockTransReqBL.GetQtyFromVSTTransferReqForSJ(_stcTransOutHd.RequestNo, this.ProductPicker1.ProductCode).ToString("#,##0.##");
                this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_stockTransReqBL.GetUnitFromVSTTransferReqForSJ(_stcTransOutHd.RequestNo, this.ProductPicker1.ProductCode));
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
            this.ClearLabel();

            //this.ProductDropDownList.SelectedValue = "null";
            this.ProductPicker1.ProductCode = "";
            this.ProductPicker1.ProductName = "";
            this.LocationDropDownList.SelectedValue = "null";
            this.QtyTextBox.Text = "0";
            this.UnitTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        //private void ShowProduct()
        //{
        //    STCTransOutHd _stcTransOutHd = this._stcTransOutBL.GetSingleSTCTransOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._productBL.GetListProductFromVSTTransferReqForSJ(_stcTransOutHd.RequestNo);
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowLocation()
        {
            STCTransOutHd _stcTransOutHd = this._stcTransOutBL.GetSingleSTCTransOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcTransOutHd.WrhsSrc);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTransOutDt _stcTransOutDt = new STCTransOutDt();
            STCTransOutHd _stcTransOutHd = this._stcTransOutBL.GetSingleSTCTransOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _stcTransOutDt.TransNmbr = _transNo;
            _stcTransOutDt.ProductCode = this.ProductPicker1.ProductCode ;
            _stcTransOutDt.LocationCode = this.LocationDropDownList.SelectedValue;
            _stcTransOutDt.Unit = _stockTransReqBL.GetUnitFromVSTTransferReqForSJ(_stcTransOutHd.RequestNo, this.ProductPicker1.ProductCode);
            _stcTransOutDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcTransOutDt.PriceCost = 0;
            _stcTransOutDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stcTransOutBL.AddSTCTransOutDt(_stcTransOutDt);

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
        //    STCTransOutHd _stcTransOutHd = this._stcTransOutBL.GetSingleSTCTransOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
        //    this.QtyTextBox.Text = _stockTransReqBL.GetQtyFromVSTTransferReqForSJ(_stcTransOutHd.RequestNo, this.ProductPicker1.ProductCode).ToString("#,##0.##");
        //    this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_stockTransReqBL.GetUnitFromVSTTransferReqForSJ(_stcTransOutHd.RequestNo, this.ProductPicker1.ProductCode));
        //}
    }
}