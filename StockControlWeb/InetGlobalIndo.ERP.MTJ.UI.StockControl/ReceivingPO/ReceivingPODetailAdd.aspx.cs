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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ReceivingPO
{
    public partial class ReceivingPODetailAdd : ReceivingPOBase
    {
        private ReceivingPOBL _receivingPOBL = new ReceivingPOBL();
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
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

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                //this.ShowProduct();
                this.ShowLocation();
                this.ClearData();
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                if (this.ProductPicker1.ProductCode != "null")
                {
                    MsProduct _msProduct = _productBL.GetSingleProduct(this.ProductPicker1.ProductCode);
                    STCReceiveHd _stcReceiveHd = this._receivingPOBL.GetSingleSTCReceiveHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                    PRCPODt _prcPODt = this._purchaseOrderBL.GetSinglePRCPODt(_stcReceiveHd.PONo, 0, this.ProductPicker1.ProductCode);
                    this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_msProduct.Unit);
                    this.QtyTextBox.Text = (_prcPODt.Qty - Convert.ToInt32(_prcPODt.QtyRR)).ToString("#,##0.##");
                }
                else
                {
                    this.UnitTextBox.Text = "";
                    this.QtyTextBox.Text = "0";
                }
            }
        }

        protected void SetAttribute()
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
            this.ClearLabel();

            this.QtyTextBox.Text = "0";
            this.UnitTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            //this.ProductDropDownList.SelectedValue = "null";
            //this.LocationDropDownList.SelectedValue = "null";
        }

        //private void ShowProduct()
        //{
        //    STCReceiveHd _stcReceiveHd = this._receivingPOBL.GetSingleSTCReceiveHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._purchaseOrderBL.GetListProductForDDLPODt(_stcReceiveHd.PONo);
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowLocation()
        {
            STCReceiveHd _stcReceiveHd = this._receivingPOBL.GetSingleSTCReceiveHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcReceiveHd.WrhsCode);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            if (_stcReceiveHd.FgLocation == false)
            {
                this.LocationDropDownList.Enabled = true;
            }
            else
            {
                this.LocationDropDownList.SelectedValue = _stcReceiveHd.LocationCode;
                this.LocationDropDownList.Enabled = false;
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCReceiveDt _stcReceiveDt = new STCReceiveDt();
            MsProduct _msProduct = _productBL.GetSingleProduct(this.ProductPicker1.ProductCode);

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _stcReceiveDt.TransNmbr = _transNo;
            _stcReceiveDt.ProductCode = this.ProductPicker1.ProductCode;
            _stcReceiveDt.LocationCode = this.LocationDropDownList.SelectedValue;
            _stcReceiveDt.Unit = _msProduct.Unit;
            _stcReceiveDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcReceiveDt.Remark = this.RemarkTextBox.Text;

            String _result = this._receivingPOBL.AddSTCReceiveDt(_stcReceiveDt);

            if (_result != "")
            {
                if (_result.Substring(0, 5) == ApplicationConfig.Error)
                {
                    this.WarningLabel.Text = _result.Substring(6, _result.Length - 6);
                }
            }
            else
            {
                Response.Redirect(this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ProductPicker1.ProductCode, ApplicationConfig.EncryptionKey)) + "&" + this._locationKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.LocationDropDownList.SelectedValue, ApplicationConfig.EncryptionKey)));
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
        //    if (this.ProductPicker1.ProductCode != "null")
        //    {
        //        MsProduct _msProduct = _productBL.GetSingleProduct(this.ProductPicker1.ProductCode);
        //        STCReceiveHd _stcReceiveHd = this._receivingPOBL.GetSingleSTCReceiveHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //        this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_msProduct.Unit);
        //        this.QtyTextBox.Text = (_purchaseOrderBL.GetQtyFromVPRPOForRR(_stcReceiveHd.PONo, this.ProductPicker1.ProductCode) == 0) ? "0" : _purchaseOrderBL.GetQtyFromVPRPOForRR(_stcReceiveHd.PONo, this.ProductPicker1.ProductCode).ToString("#,##0.##");
        //    }
        //    else
        //    {
        //        this.UnitTextBox.Text = "";
        //        this.QtyTextBox.Text = "0";
        //    }
        //}
    }
}