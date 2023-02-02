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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockRejectOut
{
    public partial class StockRejectOutDetailAdd : StockRejectOutBase
    {
        private StockRejectOutBL _stockRejectOutBL = new StockRejectOutBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();
        private PurchaseReturBL _prcReturBL = new PurchaseReturBL();

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
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                //this.ShowProduct();
                this.ShowLocation();

                this.SetAttribute();
                this.ClearData();
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                if (this.ProductPicker1.ProductCode == "null")
                {
                    this.UnitTextBox.Text = "";
                }
                else
                {
                    this.UnitTextBox.Text = _productBL.GetUnitNameByCode(this.ProductPicker1.ProductCode);
                }
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

            this.UnitTextBox.Text = "";
            this.QtyTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
        }

        //private void ShowProduct()
        //{
        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._productBL.GetListProductForDDLActiveAndStock();
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowLocation()
        {
            STCRejectOutHd _stcRejectOutHd = this._stockRejectOutBL.GetSingleSTCRejectOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcRejectOutHd.WrhsCode);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCRejectOutDt _stcRejectOutDt = new STCRejectOutDt();
            MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode);

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _stcRejectOutDt.TransNmbr = _transNo;
            _stcRejectOutDt.ProductCode = this.ProductPicker1.ProductCode;
            _stcRejectOutDt.LocationCode = this.LocationDropDownList.SelectedValue;
            _stcRejectOutDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcRejectOutDt.Unit = _productBL.GetUnitCodeByCode(this.ProductPicker1.ProductCode);
            _stcRejectOutDt.TotalCost = 0;
            _stcRejectOutDt.PriceCost = 0;
            _stcRejectOutDt.Remark = this.RemarkTextBox.Text;
            _stcRejectOutDt.QtyIn = 0;

            String _reffTransRetur = this._stockRejectOutBL.GetSingleSTCRejectOutHd(_transNo).PurchaseRetur;
            STCRejectOutDt _stcRejectOutDt2 = this._stockRejectOutBL.GetSingleSTCRejectOutDtExist(_transNo, this.ProductPicker1.ProductCode);
            PRCReturDt _prcReturDt = this._prcReturBL.GetSinglePRCReturDtForStcRejOut(_reffTransRetur, this.ProductPicker1.ProductCode);
            if (_stcRejectOutDt2 == null)
            {
                if(_prcReturDt != null)
                {
                    decimal _qty = _prcReturDt.Qty;
                    decimal _qtySJ = _prcReturDt.QtySJ == null ? 0 : Convert.ToDecimal(_prcReturDt.QtySJ);
                    decimal _qtyClose = _prcReturDt.QtyClose == null ? 0 : Convert.ToDecimal(_prcReturDt.QtyClose);
                    decimal _totalQty = _qty - _qtyClose - _qtySJ;
                    
                    if(_totalQty >= Convert.ToDecimal(this.QtyTextBox.Text))
                    {
                        bool _result = this._stockRejectOutBL.AddSTCRejectOutDt(_stcRejectOutDt);

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
                        //this.WarningLabel.Text = "Qty must be <= " + _totalQty.ToString("#,#0");
                        this.WarningLabel.Text = "Qty more than in " + _reffTransRetur;
                    }
                    
                }
                else
                {
                    this.WarningLabel.Text = "Product is not Exist In " + _reffTransRetur + " or product is already retur";
                }
            }
            else
            {
                this.WarningLabel.Text = "The product is exist";
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
        //    if (this.ProductPicker1.ProductCode == "null")
        //    {
        //        this.UnitTextBox.Text = "";
        //    }
        //    else
        //    {
        //        this.UnitTextBox.Text = _productBL.GetUnitNameByCode(this.ProductPicker1.ProductCode);
        //    }
        //}
    }
}