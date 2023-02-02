using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockReceivingOther
{
    public partial class StockReceivingOtherDetailAdd : StockReceivingOtherBase
    {
        private StockReceivingOtherBL _stockReceivingOtherBL = new StockReceivingOtherBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
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
                String spawnJS = "<script language='JavaScript'>\n";

                ////////////////////DECLARE FUNCTION FOR CALCULATING TOTALCOST
                spawnJS += "function calculateLineTotal(x) {\n";
                spawnJS += "if ( x.value != '' ) {\n";
                spawnJS += "document.getElementById('" + this.TotalCostTextBox.ClientID + "').value = (document.getElementById('" + this.QtyTextBox.ClientID + "').value * document.getElementById('" + this.PriceCostTextBox.ClientID + "').value).toFixed(2);\n";
                //spawnJS += "document.getElementById('" + this.txtLineTotal.ClientID + "').value = (document.getElementById('" + this.qty.ClientID + "').value * document.getElementById('" + this.price.ClientID + "').value).toFixed(2);\n";
                spawnJS += "}\n}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearLabel();
                this.SetAttribute();
                //this.ShowProduct();
                this.ShowLocation();
                this.ClearData();
            }

            if (this.ProductPicker1.ProductCode != "")
            {
                MsProduct _que = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode);
                if (_que.FgConsignment == true)
                {
                    this.PriceCostTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
                    this.TotalCostTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
                    this.PriceCostTextBox.Text = "0";
                    this.PriceCostTextBox.Attributes.Add("ReadOnly", "true");
                    this.tempFgConsigment.Value = _que.FgConsignment.ToString();
                }
                //this.QtyTextBox.Text = "5";
                else
                {
                    this.PriceCostTextBox.Text = ((_que.FgConsignment == null) ? _que.BuyingPrice.ToString("#0.##") : _que.BuyingPrice.ToString("#0.##"));
                    this.TotalCostTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
                    this.tempFgConsigment.Value = ((_que.FgConsignment == null) ? "false" : _que.FgConsignment.ToString());
                    this.PriceCostTextBox.Attributes.Clear();
                    this.PriceCostTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.PriceCostTextBox.ClientID + "," + this.PriceCostTextBox.ClientID + ",500" + ");");
                }
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                if (this.ProductPicker1.ProductCode != "")
                {
                    this.UnitTextBox.Text = this._productBL.GetUnitNameByCode(this.ProductPicker1.ProductCode);
                }
                else
                {
                    this.UnitTextBox.Text = "";
                }
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttribute()
        {
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return Numeric();");

            this.UnitTextBox.Attributes.Add("ReadOnly", "true");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.PriceCostTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.PriceCostTextBox.ClientID + "," + this.PriceCostTextBox.ClientID + ",500" + ");");
            this.QtyTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.QtyTextBox.ClientID + "," + this.QtyTextBox.ClientID + ",500" + ");");

            this.QtyTextBox.Attributes.Add("onkeyup", "numericInput(this);calculateLineTotal(this)");
            this.QtyTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterQty();}");

            this.PriceCostTextBox.Attributes.Add("onkeyup", "numericInput(this);calculateLineTotal(this)");
            this.PriceCostTextBox.Attributes.Add("OnBlur", "numericInput(this);calculateLineTotal(this)");
            this.PriceCostTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return enterQty();}");
        }

        //protected void ShowProduct()
        //{
        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._productBL.GetListProductForDDLActiveAndStock();
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        protected void ShowLocation()
        {
            string _tempWarehouseCode = this._stockReceivingOtherBL.GetWarehouseCodeByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_tempWarehouseCode);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ProductPicker1.ProductCode != "")
        //    {
        //        this.UnitTextBox.Text = this._productBL.GetUnitNameByCode(this.ProductPicker1.ProductCode);
        //    }
        //    else
        //    {
        //        this.UnitTextBox.Text = "";
        //    }
        //}

        protected void ClearData()
        {
            this.ClearLabel();

            //this.ProductDropDownList.SelectedValue = "null";
            this.ProductPicker1.ProductCode = "" ;
            this.ProductPicker1.ProductName = "";
            this.LocationDropDownList.SelectedValue = "null";
            this.QtyTextBox.Text = "0";
            this.PriceCostTextBox.Text = "0";
            this.TotalCostTextBox.Text = "0";
            this.UnitTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCRROtherDt _stcRROtherDt = new STCRROtherDt();

            _stcRROtherDt.TransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            _stcRROtherDt.ProductCode = this.ProductPicker1.ProductCode ;
            _stcRROtherDt.LocationCode = this.LocationDropDownList.SelectedValue;
            _stcRROtherDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcRROtherDt.Unit = this._productBL.GetUnitCodeByCode(this.ProductPicker1.ProductCode);
            _stcRROtherDt.Remark = this.RemarkTextBox.Text;
            _stcRROtherDt.PriceCost = Convert.ToDecimal(this.PriceCostTextBox.Text);
            _stcRROtherDt.TotalCost = Convert.ToDecimal(this.TotalCostTextBox.Text);
            _stcRROtherDt.FgConsignment = Convert.ToBoolean(this.tempFgConsigment.Value);

            bool _result = this._stockReceivingOtherBL.AddSTCRROtherDt(_stcRROtherDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
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

        
    }
}