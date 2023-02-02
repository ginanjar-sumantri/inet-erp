using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.DeliveryOrder
{
    public partial class DeliveryOrderDetailAdd : DeliveryOrderBase
    {
        private DeliveryOrderBL _deliveryOrderBL = new DeliveryOrderBL();
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
        private ProductBL _productBL = new ProductBL();
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

                //this.ShowProduct();

                this.SetAttribute();
                this.ClearData();
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;
                MKTDOHd _mktDOHd = this._deliveryOrderBL.GetSingleMKTDOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode);

                this.QtyTextBox.Text = (_salesOrderBL.GetQtyFromVMKSOForDO(_mktDOHd.SONo, this.ProductPicker1.ProductCode) == 0) ? "0" : _salesOrderBL.GetQtyFromVMKSOForDO(_mktDOHd.SONo, this.ProductPicker1.ProductCode).ToString("#,###.##");
                this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_msProduct.Unit);
            }

        }

        protected void SetAttribute()
        {
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyTextBox.Attributes.Add("OnBlur", "return ChangeFormat(" + this.QtyTextBox.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            //this.ProductDropDownList.SelectedValue = "null";
            this.UnitTextBox.Text = "";
            this.QtyTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
        }

        //private void ShowProduct()
        //{
        //    MKTDOHd _mktDOHd = this._deliveryOrderBL.GetSingleMKTDOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._salesOrderBL.GetListProductForDDLDODt(_mktDOHd.SONo);
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MKTDODt _mktDODt = new MKTDODt();
            MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode);

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _mktDODt.TransNmbr = _transNo;
            _mktDODt.ProductCode = this.ProductPicker1.ProductCode;
            _mktDODt.Unit = _msProduct.Unit;
            _mktDODt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _mktDODt.Remark = this.RemarkTextBox.Text;
            _mktDODt.DoneClosing = DeliveryOrderDataMapper.GetStatusDetail(DeliveryOrderStatusDt.Open);
            _mktDODt.QtySJ = 0;

            bool _result = this._deliveryOrderBL.AddMKTDODt(_mktDODt);

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
        //    MKTDOHd _mktDOHd = this._deliveryOrderBL.GetSingleMKTDOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
        //    MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode);

        //    this.QtyTextBox.Text = (_salesOrderBL.GetQtyFromVMKSOForDO(_mktDOHd.SONo, this.ProductPicker1.ProductCode) == 0) ? "0" : _salesOrderBL.GetQtyFromVMKSOForDO(_mktDOHd.SONo, this.ProductPicker1.ProductCode).ToString("#,###.##");
        //    this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_msProduct.Unit);
        //}
    }
}