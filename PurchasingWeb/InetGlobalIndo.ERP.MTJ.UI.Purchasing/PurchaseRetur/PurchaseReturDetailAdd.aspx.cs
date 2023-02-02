using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRetur
{
    public partial class PurchaseReturDetailAdd : PurchaseReturBase
    {
        private PurchaseReturBL _purchaseReturBL = new PurchaseReturBL();
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
        private ReceivingPOBL _rcvPOBL = new ReceivingPOBL();
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

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                //this.ShowProduct();
                //this.ShowLocation();

                this.ClearData();
            }

            if (this.tempProductCode.Value != this.ProductPicker21.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker21.ProductCode;

                //this.ShowLocation();
                //this.UnitTextBox.Text = "";
                //this.QtyTextBox.Text = "0";
            }
        }

        protected void SetAttribute()
        {
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountForexTextBox.Attributes.Add("ReadOnly", "True");
            //this.QtyTextBox.Attributes.Add("OnBlur", "return ValidateQtyRemain(" + this.QtyTextBox.ClientID + "," + this.QtyRemainHidden.ClientID + ");");
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            //this.QTYSJTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            //this.QTYCloseTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.QtyRemainHidden.Value = "0";
            this.QtyTextBox.Text = "0";
            //this.QTYSJTextBox.Text = "0";
            //this.QTYCloseTextBox.Text = "0";
            this.UnitTextBox.Text = "";
            this.PriceTextBox.Text = "0";
            this.AmountForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
            //this.ProductDropDownList.SelectedValue = "null";
            //this.LocationDropDownList.SelectedValue = "null";
        }

        //private void ShowProduct()
        //{
        //    PRCReturHd _prcReturHd = this._purchaseReturBL.GetSinglePRCReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._rcvPOBL.GetListProductForDDLRRQtyRemain(_prcReturHd.RRNo);
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //private void ShowLocation()
        //{
        //    PRCReturHd _prcReturHd = this._purchaseReturBL.GetSinglePRCReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.LocationDropDownList.Items.Clear();
        //    this.LocationDropDownList.DataTextField = "WLocationName";
        //    this.LocationDropDownList.DataValueField = "WLocationCode";
        //    this.LocationDropDownList.DataSource = this._rcvPOBL.GetListDDLWrhsLocationByProductCode(_prcReturHd.RRNo, this.ProductPicker21.ProductCode);
        //    this.LocationDropDownList.DataBind();
        //    this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            //if (Convert.ToDecimal(this.QtyTextBox.Text) >= (Convert.ToDecimal(this.QTYSJTextBox.Text) + Convert.ToDecimal(this.QTYCloseTextBox.Text)))
            //this.QtyTextBox.Text = (Convert.ToDecimal(this.QTYSJTextBox.Text) + Convert.ToDecimal(this.QTYCloseTextBox.Text)).ToString("#,#0.##");
            //{
            PRCReturDt _prcReturDt = new PRCReturDt();            
            MsProduct _msProduct = _productBL.GetSingleProduct(this.ProductPicker21.ProductCode);
            PRCReturHd _prcReturHd = this._purchaseReturBL.GetSinglePRCReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            //if (Convert.ToDecimal(this.QtyTextBox.Text) <= _rcvPOBL.GetQtyFromReceiveDtForRR(_prcReturHd.RRNo, this.ProductPicker21.ProductCode, this.LocationDropDownList.SelectedValue))
            //{
            //_prcReturDt.LocationCode = this.LocationDropDownList.SelectedValue;
            //_prcReturDt.CreatedBy = HttpContext.Current.User.Identity.Name;
            //_prcReturDt.CreatedDate = DateTime.Now;
            //_prcReturDt.EditBy = HttpContext.Current.User.Identity.Name;
            //_prcReturDt.EditDate = DateTime.Now;

            _prcReturDt.TransNmbr = _transNo;
            _prcReturDt.ProductCode = this.ProductPicker21.ProductCode;
            _prcReturDt.Unit = _msProduct.Unit;
            _prcReturDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            //_prcReturDt.QtySJ = Convert.ToDecimal(this.QTYSJTextBox.Text);
            //_prcReturDt.QtyClose = Convert.ToDecimal(this.QTYCloseTextBox.Text);
            _prcReturDt.QtySJ = 0;
            _prcReturDt.QtyClose = 0;
            _prcReturDt.Price = Convert.ToDecimal(this.PriceTextBox.Text);
            _prcReturDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _prcReturDt.Remark = this.RemarkTextBox.Text;

            PRCReturDt _prcReturDetail = this._purchaseReturBL.GetSinglePRCReturDt(_transNo, _prcReturDt.ProductCode);
            if (_prcReturDetail == null)
            {
                bool _result = this._purchaseReturBL.AddPRCReturDt(_prcReturDt, Convert.ToDecimal(this.AmountForexTextBox.Text));

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
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Product Already Exist";
            }

            //bool _result = this._purchaseReturBL.AddPRCReturDt(_prcReturDt);

            //if (_result == true)
            //{
            //    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            //}
            //else
            //{
            //    this.ClearLabel();
            //    this.WarningLabel.Text = "Your Failed Add Data";
            //}
            //}
            //else
            //{
            //this.WarningLabel.Text = "Qty more than Qty in Receiving PO";
            //}
            //}
            //else
            //{
            //    this.WarningLabel.Text = "Qty Retur more than Qty SJ + Qty Close";
            //}
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
        //    this.ShowLocation();
        //    this.UnitTextBox.Text = "";
        //    this.QtyTextBox.Text = "0";
        //}

        //protected void LocationDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.LocationDropDownList.SelectedValue != "null")
        //    {
        //        PRCReturHd _prcReturHd = this._purchaseReturBL.GetSinglePRCReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
        //        STCReceiveDt _stcReceiveDt = this._rcvPOBL.GetSingleSTCReceiveDt(_prcReturHd.RRNo, this.ProductPicker21.ProductCode, this.LocationDropDownList.SelectedValue);

        //        this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_stcReceiveDt.Unit);
        //        this.QtyTextBox.Text = (_rcvPOBL.GetQtyFromReceiveDtForRR(_prcReturHd.RRNo, this.ProductPicker21.ProductCode, this.LocationDropDownList.SelectedValue) == 0) ? "0" : _rcvPOBL.GetQtyFromReceiveDtForRR(_prcReturHd.RRNo, this.ProductPicker21.ProductCode, this.LocationDropDownList.SelectedValue).ToString("#,###.##");
        //        this.QtyRemainHidden.Value = this.QtyTextBox.Text;
        //    }
        //    else
        //    {
        //        this.UnitTextBox.Text = "";
        //        this.QtyTextBox.Text = "0";
        //        this.QtyRemainHidden.Value = "0";
        //    }
        //}

        protected void QtyTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.QtyTextBox.Text == "")
                this.QtyTextBox.Text = "0";
            //if (this.QTYSJTextBox.Text == "")
            //    this.QTYSJTextBox.Text = "0";
            //if (this.QTYCloseTextBox.Text == "")
            //    this.QTYCloseTextBox.Text = "0";

            this.QtyTextBox.Text = Convert.ToDecimal(this.QtyTextBox.Text).ToString("#,#0.##");
            if (this.tempProductCode.Value != "")
                this.UnitTextBox.Text = this._productBL.GetUnitNameByCode(this.tempProductCode.Value);
        }

        protected void PriceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.PriceTextBox.Text == "")
                this.PriceTextBox.Text = "0";

            this.AmountForexTextBox.Text = (Convert.ToDecimal(this.QtyTextBox.Text) * Convert.ToDecimal(this.PriceTextBox.Text)).ToString("#,#0.##");
        }

        protected void tempProductCode_ValueChanged(object sender, EventArgs e)
        {
            this.UnitTextBox.Text = this._productBL.GetUnitNameByCode(this.tempProductCode.Value);
        }
    }
}