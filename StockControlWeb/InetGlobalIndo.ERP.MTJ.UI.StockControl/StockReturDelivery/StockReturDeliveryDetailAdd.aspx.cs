using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockReturDelivery
{
    public partial class StockReturDeliveryDetailAdd : StockReturDeliveryBase
    {
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private RequestSalesReturBL _requestSalesReturBL = new RequestSalesReturBL();
        private UnitBL _unitBL = new UnitBL();
        private STCReturBL _stcReturBL = new STCReturBL();
        private PermissionBL _permBL = new PermissionBL();
        private ProductBL _productBL = new ProductBL();
        private bool check = false;

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
                this.SetAttribute();
                this.ShowLocation();
                //this.ShowProduct();
                this.ClearData();
            }

            if (this.ProductPicker1.ProductCode != "")
            {
                this.ClearLabel();
                this.ShowUnit();
                this.ShowQty();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.PageTitleLiteral.Text = this._pageTitleLiteral;

            this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
            this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
            this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

            this.QtyTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        //private void ShowProduct()
        //{
        //    string _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

        //    this.ProductNameDropDownList.Items.Clear();
        //    this.ProductNameDropDownList.DataTextField = "ProductName";
        //    this.ProductNameDropDownList.DataValueField = "ProductCode";
        //    this.ProductNameDropDownList.DataSource = this._requestSalesReturBL.GetListProductForDDL(_stcReturBL.GetSingleSTCReturSJHd(_transNmbr).ReqReturNo);
        //    this.ProductNameDropDownList.DataBind();
        //    this.ProductNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowLocation()
        {
            string _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            this.LocationNameDropDownList.Items.Clear();
            this.LocationNameDropDownList.DataTextField = "WLocationName";
            this.LocationNameDropDownList.DataValueField = "WLocationCode";
            this.LocationNameDropDownList.DataSource = this._warehouseBL.GetListWrhsLocation(0,1000,"","");
            this.LocationNameDropDownList.DataBind();
            this.LocationNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowUnit()
        {
            //String _stcReturSJDt = _stcReturBL.GetSingleSTCReturSJDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.ProductPicker1.ProductCode, this.LocationNameDropDownList.SelectedValue).ProductCode;
            //MKTReqReturDt _mktReqReturDt = _requestSalesReturBL.GetSingleMKTReqReturDt(_reqNo, this.ProductPicker1.ProductCode);
            //String _stcReturRRDt = _stcReturBL.GetSingleSTCReturRRDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.ProductPicker1.ProductCode, this.LocationNameDropDownList.SelectedValue).Unit;
            String _msProductsUnit = _productBL.GetSingleProduct(this.ProductPicker1.ProductCode).Unit;
            if (_msProductsUnit != null)
            {
                this.UnitCodeTextBox.Text = this._unitBL.GetUnitNameByCode(_msProductsUnit);
            }
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.LocationNameDropDownList.SelectedValue = "null";
            //this.ProductNameDropDownList.SelectedValue = "null";
            this.ProductPicker1.ProductCode = "";
            this.ProductPicker1.ProductName = "";
            this.UnitCodeTextBox.Text = "";
            this.QtyTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
        }

        private void ShowQty()
        {
            //string _reqNo = _stcReturBL.GetSingleSTCReturSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).ReqReturNo;
            //MKTReqReturDt _abc = _requestSalesReturBL.GetSingleMKTReqReturDt(_reqNo, this.ProductPicker1.ProductCode);
            String _stcReturSJHdRRReturNo = _stcReturBL.GetSingleSTCReturSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).RRReturNo;

            STCReturRRDt _stcReturRRDt = _stcReturBL.GetSingleSTCReturRRDt(_stcReturSJHdRRReturNo, this.ProductPicker1.ProductCode, "");
            //this.QtyTextBox.Text = "0";
            if (_stcReturRRDt != null)
            {
                Decimal? _qty = _stcReturRRDt.Qty - ((_stcReturRRDt.QtySJ == null) ? 0 : _stcReturRRDt.QtySJ);
                if (_qty >= Convert.ToDecimal((this.QtyTextBox.Text == "") ? "0" : this.QtyTextBox.Text))
                {
                    if (Convert.ToDecimal((this.QtyTextBox.Text == "") ? "0" : this.QtyTextBox.Text) == 0)
                    {
                        this.QtyTextBox.Text = Convert.ToDecimal(_qty).ToString("#,##0.##");
                    }
                }
                else
                {
                    check = false;
                    if (this.WarningLabel.Text == "")
                    {
                        this.WarningLabel.Text = "Qty must less than " + Convert.ToDecimal(_qty).ToString("#,##0.##") + ".";
                    }
                    else
                    {
                        this.WarningLabel.Text = this.WarningLabel.Text + "Qty must less than " + Convert.ToDecimal(_qty).ToString("#,##0.##") + ".";
                    }
                }
            }

        }

        private void CheckProduct()
        {
            STCReturSJDt _stcReturSJDt = _stcReturBL.GetSingleSTCReturSJDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.ProductPicker1.ProductCode, "");
            if (_stcReturSJDt != null)
            {
                check = false;
                if (this.WarningLabel.Text == "")
                {
                    this.WarningLabel.Text = "Product already exists in this Transaction Number.";
                }
                else
                {
                    this.WarningLabel.Text = this.WarningLabel.Text + "Product already exists in this Transaction Number.";
                }
            }
            else
            {
                String _stcReturSJHdRRReturNo = _stcReturBL.GetSingleSTCReturSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).RRReturNo;

                STCReturRRDt _stcReturRRDt = _stcReturBL.GetSingleSTCReturRRDt(_stcReturSJHdRRReturNo, this.ProductPicker1.ProductCode, "");
                if (_stcReturRRDt == null)
                {
                    check = false;

                    if (this.WarningLabel.Text == "")
                    {
                        this.WarningLabel.Text = "Products not found in RR Retur No." + Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                    }
                    else
                    {
                        this.WarningLabel.Text = this.WarningLabel.Text + "Products not found in RR Retur No." + Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                    }
                }
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            check = true;
            this.ShowQty();
            this.CheckProduct();
            if (Convert.ToDecimal(this.QtyTextBox.Text) > 0 & this.WarningLabel.Text == "")
            {
                STCReturSJDt _stcReturSJDt = new STCReturSJDt();
                //string _reqNo = _stcReturBL.GetSingleSTCReturSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).RRReturNo;
                //string _unitCode = _requestSalesReturBL.GetSingleMKTReqReturDt(_reqNo, this.ProductPicker1.ProductCode).Unit;

                _stcReturSJDt.TransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                _stcReturSJDt.ProductCode = this.ProductPicker1.ProductCode;
                _stcReturSJDt.LocationCode = this.LocationNameDropDownList.SelectedValue;
                _stcReturSJDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
                _stcReturSJDt.Unit = _unitBL.GetUnitCodeByName(this.UnitCodeTextBox.Text.Trim());
                _stcReturSJDt.Remark = this.RemarkTextBox.Text;

                bool _result = this._stcReturBL.AddSTCReturSJDt(_stcReturSJDt);

                if (_result != false)
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));

                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "Your Failed Add Data";
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        //protected void ProductNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ProductNameDropDownList.SelectedValue != "null")
        //    {
        //        this.ShowUnit();
        //        this.ShowQty();
        //    }
        //}
    }
}