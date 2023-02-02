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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockReturReceiving
{
    public partial class StockReturReceivingDetailAdd : StockReturReceivingBase
    {
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private RequestSalesReturBL _requestSalesReturBL = new RequestSalesReturBL();
        private UnitBL _unitBL = new UnitBL();
        private STCReturBL _stcReturBL = new STCReturBL();
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

                this.showLocation();
                //this.ShowProduct();

                this.ClearData();
                this.SetAttribute();
            }

            if (this.ProductPicker1.ProductCode  != "")
            {
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
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return Numeric();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        //private void ShowProduct()
        //{
        //    string _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

        //    this.ProductNameDropDownList.Items.Clear();
        //    this.ProductNameDropDownList.DataTextField = "ProductName";
        //    this.ProductNameDropDownList.DataValueField = "ProductCode";
        //    this.ProductNameDropDownList.DataSource = this._requestSalesReturBL.GetListProductForDDL(_stcReturBL.GetSingleSTCReturRRHd(_transNmbr).ReqReturNo);
        //    this.ProductNameDropDownList.DataBind();
        //    this.ProductNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void showLocation()
        {
            this.LocationNameDropDownList.Items.Clear();
            this.LocationNameDropDownList.DataTextField = "WLocationName";
            this.LocationNameDropDownList.DataValueField = "WLocationCode";
            this.LocationNameDropDownList.DataSource = this._warehouseBL.GetListWrhsLocation(0, 1000, "", "");
            this.LocationNameDropDownList.DataBind();
            this.LocationNameDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowUnit()
        {
            string _reqNo = _stcReturBL.GetSingleSTCReturRRHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).ReqReturNo;
            MKTReqReturDt _mktReqReturDt = _requestSalesReturBL.GetSingleMKTReqReturDt(_reqNo, this.ProductPicker1.ProductCode);

            if (_mktReqReturDt != null)
            {
                this.UnitCodeTextBox.Text = _unitBL.GetUnitNameByCode(_mktReqReturDt.Unit);
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
            string _reqNo = _stcReturBL.GetSingleSTCReturRRHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).ReqReturNo;
            MKTReqReturDt _abc = _requestSalesReturBL.GetSingleMKTReqReturDt(_reqNo, this.ProductPicker1.ProductCode);

            if (_abc != null)
            {
                this.QtyTextBox.Text = _abc.Qty.ToString("#,##0.##");
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCReturRRDt _stcReturDt = new STCReturRRDt();
            string _reqNo = _stcReturBL.GetSingleSTCReturRRHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).ReqReturNo;
            string _unitCode = _requestSalesReturBL.GetSingleMKTReqReturDt(_reqNo, this.ProductPicker1.ProductCode).Unit;

            _stcReturDt.TransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            _stcReturDt.ProductCode = this.ProductPicker1.ProductCode;
            _stcReturDt.LocationCode = this.LocationNameDropDownList.SelectedValue;
            _stcReturDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcReturDt.Unit = _unitCode;
            _stcReturDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stcReturBL.AddSTCReturRRDt(_stcReturDt);

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