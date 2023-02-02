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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockRejectIn
{
    public partial class StockRejectInDetailAdd : StockRejectInBase
    {
        private StockRejectInBL _stockRejectInBL = new StockRejectInBL();
        private StockRejectOutBL _stockRejectOutBL = new StockRejectOutBL();
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

            _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
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

                if (this.ProductPicker1.ProductCode == "")
                {
                    this.QtyTextBox.Text = "0";
                    this.UnitTextBox.Text = "";
                }
                else
                {
                    STCRejectInHd _stcRejectInHd = this._stockRejectInBL.GetSingleSTCRejectInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                    this.QtyTextBox.Text = _stockRejectInBL.GetQtyV_STRejectOutForRRByRejectCode(_stcRejectInHd.TransReff, this.ProductPicker1.ProductCode).ToString("#,##0.##");
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
            this.QtyTextBox.Attributes.Remove("ReadOnly");
            this.QtyTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
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
        //    STCRejectInHd _stcRejectInHd = this._stockRejectInBL.GetSingleSTCRejectInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._productBL.GetListProductFromV_STRejectOutForRR(_stcRejectInHd.TransReff);
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowLocation()
        {
            STCRejectInHd _stcRejectInHd = this._stockRejectInBL.GetSingleSTCRejectInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcRejectInHd.WrhsCode);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCRejectInDt _stcRejectInDt = new STCRejectInDt();
            MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode );

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _stcRejectInDt.TransNmbr = _transNo;
            _stcRejectInDt.ProductCode = this.ProductPicker1.ProductCode ;
            _stcRejectInDt.LocationCode = this.LocationDropDownList.SelectedValue;
            _stcRejectInDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcRejectInDt.Unit = _productBL.GetUnitCodeByCode(this.ProductPicker1.ProductCode);
            _stcRejectInDt.TotalCost = 0;
            _stcRejectInDt.PriceCost = 0;
            _stcRejectInDt.Remark = this.RemarkTextBox.Text;

            String _reffRejOut = this._stockRejectInBL.GetSingleSTCRejectInHd(_transNo).TransReff;
            STCRejectInDt _stcRejectInDt2 = this._stockRejectInBL.GetSingleSTCRejectInDtExist(_transNo, this.ProductPicker1.ProductCode);
            STCRejectOutDt _stcRejectOutDt = this._stockRejectOutBL.GetSingleSTCRejectOutDtForStcRejIn(_reffRejOut, this.ProductPicker1.ProductCode);

            if (_stcRejectInDt2 == null)
            {
                if (_stcRejectOutDt != null)
                {
                    decimal _qty = _stcRejectOutDt.Qty;
                    decimal _qtyIn = _stcRejectOutDt.QtyIn == null ? 0 : Convert.ToDecimal(_stcRejectOutDt.QtyIn);
                    decimal _totalQty = _qty - _qtyIn;

                    if (_totalQty >= Convert.ToDecimal(this.QtyTextBox.Text))
                    {
                        bool _result = this._stockRejectInBL.AddSTCRejectInDt(_stcRejectInDt);

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
                        this.WarningLabel.Text = "Qty more than in " + _reffRejOut;
                    }
                }
                else
                {
                    this.WarningLabel.Text = "Product is not Exist In " + _reffRejOut + " Or Product is Already Retur";
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
        //    if (this.ProductPicker1.ProductCode == "")
        //    {
        //        this.QtyTextBox.Text = "0";
        //        this.UnitTextBox.Text = "";
        //    }
        //    else
        //    {
        //        STCRejectInHd _stcRejectInHd = this._stockRejectInBL.GetSingleSTCRejectInHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //        this.QtyTextBox.Text = _stockRejectInBL.GetQtyV_STRejectOutForRRByRejectCode(_stcRejectInHd.TransReff, this.ProductPicker1.ProductCode).ToString("#,##0.##");
        //        this.UnitTextBox.Text = _productBL.GetUnitNameByCode(this.ProductPicker1.ProductCode );
        //    }
        //}
    }
}