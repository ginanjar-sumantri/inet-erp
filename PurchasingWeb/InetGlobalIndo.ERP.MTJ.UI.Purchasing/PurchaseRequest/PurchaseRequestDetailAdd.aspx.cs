using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRequest
{
    public partial class PurchaseRequestDetailAdd : PurchaseRequestBase
    {
        private ProductBL _productBL = new ProductBL();
        private PurchaseRequestBL _purchaseRequestBL = new PurchaseRequestBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private CompanyConfig _compConfig = new CompanyConfig();

        protected void Page_Load(object sender, EventArgs e)
        {
            string _pgYear = this._compConfig.GetSingle(CompanyConfigure.ActivePGYear).SetValue;

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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                //this.ShowProduct();
                this.ClearData();
                this.SetAttribute();
            }

            if (this.tempProductCode.Value != this.ProductPicker21.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker21.ProductCode;

                if (this.ProductPicker21.ProductCode != "null")
                {
                    this.UnitTextBox.Text = this._productBL.GetUnitNameByCode(this.ProductPicker21.ProductCode);
                    bool _isUsingPG = this._productBL.GetSingleProductType(this._productBL.GetSingleProduct(this.ProductPicker21.ProductCode).ProductType).IsUsingPG;

                    if (_isUsingPG == true)
                    {
                        Decimal _priceGroup = this._priceGroupBL.GetSingle(this._productBL.GetSingleProduct(this.ProductPicker21.ProductCode).PriceGroupCode, Convert.ToInt32(_pgYear.Trim())).AmountForex;
                        this.PriceGroupTextBox.Text = this._productBL.GetSingleProduct(this.ProductPicker21.ProductCode).PriceGroupCode;
                        this.EstPriceTextBox.Attributes.Add("ReadOnly", "True");
                        this.EstPriceTextBox.Attributes.Add("style", "background-color:#cccccc");
                        this.EstPriceTextBox.Text = _priceGroup.ToString("#,##0.00");
                    }
                    else
                    {
                        this.EstPriceTextBox.Attributes.Remove("ReadOnly");
                        this.EstPriceTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                        this.EstPriceTextBox.Text = "0";
                    }
                }
                else
                {
                    this.UnitTextBox.Text = "";
                    this.EstPriceTextBox.Attributes.Remove("ReadOnly");
                    this.EstPriceTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.EstPriceTextBox.Text = "0";
                }
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttribute()
        {
            this.QtyTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.QtyTextBox.ClientID + ");");
            this.UnitTextBox.Attributes.Add("ReadOnly", "true");
            this.DateTextBox.Attributes.Add("ReadOnly", "true");
            this.EstPriceTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.EstPriceTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
            this.PriceGroupTextBox.Attributes.Add("style", "background-color:#cccccc");
        }

        //public void ShowProduct()
        //{
        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._productBL.GetListProductForDDLActiveAndStock();
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void ClearData()
        {
            this.ClearLabel();

            string _currCode = this._purchaseRequestBL.GetSinglePRCRequestHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            //this.ProductDropDownList.SelectedValue = "null";
            this.SpecificationTextBox.Text = "";
            this.QtyTextBox.Text = "";
            this.UnitTextBox.Text = "";
            this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.RemarkTextBox.Text = "";

        }

        //protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ProductPicker21.ProductCode != "null")
        //    {
        //        this.UnitTextBox.Text = this._productBL.GetUnitNameByCode(this.ProductPicker21.ProductCode);
        //    }
        //    else
        //    {
        //        this.UnitTextBox.Text = "";
        //    }
        //}

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            PRCRequestDt _prcRequestDt = new PRCRequestDt();

            _prcRequestDt.TransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            _prcRequestDt.ProductCode = this.ProductPicker21.ProductCode;
            _prcRequestDt.Specification = this.SpecificationTextBox.Text;
            _prcRequestDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _prcRequestDt.EstPrice = Convert.ToDecimal(this.EstPriceTextBox.Text);
            _prcRequestDt.Unit = this._productBL.GetUnitCodeByCode(this.ProductPicker21.ProductCode);
            _prcRequestDt.RequireDate = DateFormMapper.GetValue(this.DateTextBox.Text);
            _prcRequestDt.Remark = this.RemarkTextBox.Text;
            _prcRequestDt.DoneClosing = PurchaseRequestDataMapper.GetStatusDetail(PurchaseRequestStatusDt.Open);

            _prcRequestDt.CreatedBy = HttpContext.Current.User.Identity.Name;
            _prcRequestDt.CreatedDate = DateTime.Now;
            _prcRequestDt.EditBy = HttpContext.Current.User.Identity.Name;
            _prcRequestDt.EditDate = DateTime.Now;

            bool _result = this._purchaseRequestBL.AddPRCRequestDt(_prcRequestDt);

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

    }
}