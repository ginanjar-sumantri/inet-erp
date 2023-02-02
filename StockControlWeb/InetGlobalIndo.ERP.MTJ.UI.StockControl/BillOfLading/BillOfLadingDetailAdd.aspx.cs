using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.BillOfLading
{
    public partial class BillOfLadingDetailAdd : BillOfLadingBase
    {
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private RequestSalesReturBL _requestSalesReturBL = new RequestSalesReturBL();
        private UnitBL _unitBL = new UnitBL();
        private BillOfLadingBL _billOfLadingBL = new BillOfLadingBL();
        private ProductBL _productBL = new ProductBL();
        private DeliveryOrderBL _deliveryOrderBL = new DeliveryOrderBL();
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
                this.showLocation();
                this.ShowDONo();
                this.ShowProduct();

                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.QtyTextBox.Attributes.Add("OnBlur", "return ChangeFormat(" + this.QtyTextBox.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ShowDONo()
        {
            this.DONoDropDownList.Items.Clear();
            this.DONoDropDownList.DataTextField = "FileNmbr";
            this.DONoDropDownList.DataValueField = "TransNmbr";
            this.DONoDropDownList.DataSource = this._deliveryOrderBL.GetListDoForDDLForSJ(_billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).SONo);
            this.DONoDropDownList.DataBind();
            this.DONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowProduct()
        {
            this.ProductNameDropDownList.Items.Clear();
            this.ProductNameDropDownList.DataTextField = "ProductName";
            this.ProductNameDropDownList.DataValueField = "ProductCode";
            this.ProductNameDropDownList.DataSource = this._deliveryOrderBL.GetListProductForDDLForSJ(_billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).SONo);
            this.ProductNameDropDownList.DataBind();
            this.ProductNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void showLocation()
        {
            STCSJHd _stcSJHd = this._billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationNameDropDownList.Items.Clear();
            this.LocationNameDropDownList.DataTextField = "WLocationName";
            this.LocationNameDropDownList.DataValueField = "WLocationCode";
            this.LocationNameDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).WrhsCode);
            this.LocationNameDropDownList.DataBind();
            this.LocationNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            if (_stcSJHd.FgLocation == false)
            {
                this.LocationNameDropDownList.Enabled = true;
                this.LocationNameDropDownList.SelectedValue = "null";
            }
            else
            {
                this.LocationNameDropDownList.SelectedValue = _stcSJHd.LocationCode;
                this.LocationNameDropDownList.Enabled = false;
            }
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.ProductNameDropDownList.SelectedValue = "null";
            this.UnitCodeTextBox.Text = "";
            this.QtyTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCSJDt _stcSJDt = new STCSJDt();

            String _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            STCSJHd _stcSjHd = _billOfLadingBL.GetSingleSTCSJHd(_transNmbr);

            _stcSJDt.TransNmbr = _transNmbr;
            _stcSJDt.DONo = this.DONoDropDownList.SelectedValue;
            _stcSJDt.WrhsCode = _stcSjHd.WrhsCode;
            _stcSJDt.WrhsFgSubLed = Convert.ToChar(_stcSjHd.WrhsFgSubLed);
            _stcSJDt.WrhsSubLed = _stcSjHd.WrhsSubLed; 
            _stcSJDt.ProductCode = this.ProductNameDropDownList.SelectedValue;
            _stcSJDt.LocationCode = this.LocationNameDropDownList.SelectedValue;
            _stcSJDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcSJDt.Unit = this._productBL.GetSingleProduct(this.ProductNameDropDownList.SelectedValue).Unit;
            _stcSJDt.Remark = this.RemarkTextBox.Text;
            _stcSJDt.QtyLoss = 0;
            _stcSJDt.QtyRetur = 0;
            _stcSJDt.QtyReceive = Convert.ToDecimal(this.QtyTextBox.Text);

            bool _result = this._billOfLadingBL.AddSTCSJDt(_stcSJDt);

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

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void DONoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DONoDropDownList.SelectedValue != "null")
            {
                this.ShowProduct();
            }
            else
            {
                this.ProductNameDropDownList.Items.Clear(); 
                this.ProductNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
        }

        protected void ProductNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ProductNameDropDownList.SelectedValue != "null")
            {
                MKTDODt _mktDODt = _deliveryOrderBL.GetSingleMKTDODt(this.DONoDropDownList.SelectedValue, this.ProductNameDropDownList.SelectedValue);
                this.QtyTextBox.Text = _mktDODt.Qty.ToString("#,##0.##");
                this.UnitCodeTextBox.Text = _unitBL.GetUnitNameByCode(_mktDODt.Unit);
            }
            else
            {
                this.QtyTextBox.Text = "0";
                this.UnitCodeTextBox.Text = "";
            }
        }
    }
}