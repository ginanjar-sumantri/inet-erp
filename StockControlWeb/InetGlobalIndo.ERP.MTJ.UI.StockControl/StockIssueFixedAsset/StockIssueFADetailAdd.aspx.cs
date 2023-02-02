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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockIssueFixedAsset
{
    public partial class StockIssueToFADetailAdd : StockIssueFixedAssetBase
    {
        private StockIssueFixedAssetBL _stockIssueFABL = new StockIssueFixedAssetBL();
        private StockIssueRequestFABL _stockIssueReqFABL = new StockIssueRequestFABL();
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ClearData();                
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                string _reqNoHeader = _stockIssueFABL.GetReqNoByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                this.QtyTextBox.Text = _stockIssueReqFABL.GetQtyV_STRequestForIssuesByReqCode(_reqNoHeader, this.ProductPicker1.ProductCode).ToString("#,###.#");
                string _unitCode = _stockIssueReqFABL.GetUnitV_STRequestForIssuesByReqCode(_reqNoHeader, this.ProductPicker1.ProductCode);
                this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_unitCode);
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
            this.QtyTextBox.Attributes.Add("OnBlur", "return ChangeFormat(" + this.QtyTextBox.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearData()
        {
            this.ClearLabel();

            //this.ShowProduct();
            this.ShowLocation();

            this.QtyTextBox.Attributes.Remove("ReadOnly");
            this.QtyTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
            this.UnitTextBox.Text = "";
            this.QtyTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
        }

        //private void ShowProduct()
        //{
        //    string _reqNoHeader = _stockIssueFABL.GetReqNoByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._stockIssueReqFABL.GetListProductForDDLIssueFADt(_reqNoHeader);
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        private void ShowLocation()
        {
            STCIssueToFAHd _stcIssueToFAHd = this._stockIssueFABL.GetSingleSTCIssueToFAHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcIssueToFAHd.WrhsCode);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCIssueToFADt _stcIssueToFADt = new STCIssueToFADt();
            MsProduct _msProduct = this._productBL.GetSingleProduct(this.ProductPicker1.ProductCode );

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _reqNoHeader = _stockIssueFABL.GetReqNoByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _stcIssueToFADt.TransNmbr = _transNo;
            _stcIssueToFADt.ProductCode = this.ProductPicker1.ProductCode;
            _stcIssueToFADt.LocationCode = this.LocationDropDownList.SelectedValue;
            _stcIssueToFADt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcIssueToFADt.Unit = _stockIssueReqFABL.GetUnitV_STRequestForIssuesByReqCode(_reqNoHeader, this.ProductPicker1.ProductCode);
            _stcIssueToFADt.TotalCost = 0;
            _stcIssueToFADt.PriceCost = 0;
            _stcIssueToFADt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stockIssueFABL.AddSTCIssueToFADt(_stcIssueToFADt);

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
        //    string _reqNoHeader = _stockIssueFABL.GetReqNoByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //    this.QtyTextBox.Text = _stockIssueReqFABL.GetQtyV_STRequestForIssuesByReqCode(_reqNoHeader, this.ProductPicker1.ProductCode).ToString("#,###.#");
        //    string _unitCode = _stockIssueReqFABL.GetUnitV_STRequestForIssuesByReqCode(_reqNoHeader, this.ProductPicker1.ProductCode);
        //    this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_unitCode);
        //}
    }
}