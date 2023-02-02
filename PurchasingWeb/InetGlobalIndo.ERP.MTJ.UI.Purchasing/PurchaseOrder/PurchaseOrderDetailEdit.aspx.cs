using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseOrder
{
    public partial class PurchaseOrderDetailEdit : PurchaseOrderBase
    {
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
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
                this.ETDLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.ETDTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.ETALiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.ETATextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.ETDTextBox.Attributes.Add("ReadOnly", "True");
            this.ETATextBox.Attributes.Add("ReadOnly", "True");
            //this.QtyTextBox.Attributes.Add("ReadOnly", "True");
            //this.QtyTotalTextBox.Attributes.Add("ReadOnly", "True");
            //this.UnitTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");
            this.NettoTextBox.Attributes.Add("ReadOnly", "True");

            //this.QtyFreeTextBox.Attributes.Add("OnBlur", "CalculateQty(" + this.QtyTextBox.ClientID + ", " + this.QtyFreeTextBox.ClientID + ", " + this.QtyTotalTextBox.ClientID + ");");

            this.DiscPercentTextBox.Attributes.Add("OnBlur", "CalculateDiscountPercent(" + this.QtyConvertionTextBox.ClientID + ", " + this.PriceTextBox.ClientID + ", " + this.AmountTextBox.ClientID + ", " + this.DiscPercentTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.NettoTextBox.ClientID + ");");
            this.DiscTextBox.Attributes.Add("OnBlur", "CalculateDiscountForex(" + this.QtyConvertionTextBox.ClientID + ", " + this.PriceTextBox.ClientID + ", " + this.AmountTextBox.ClientID + ", " + this.DiscPercentTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.NettoTextBox.ClientID + ");");

            this.QtyConvertionTextBox.Attributes.Add("OnBlur", "CalculateNetto(" + this.QtyConvertionTextBox.ClientID + ", " + this.PriceTextBox.ClientID + ", " + this.AmountTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.NettoTextBox.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "Calculate(" + this.QtyConvertionTextBox.ClientID + ", " + this.PriceTextBox.ClientID + ", " + this.AmountTextBox.ClientID + ", " + this.DiscPercentTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.NettoTextBox.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        //protected void ShowUnit(string _prmUnitCode)
        //{
        //    this.UnitDDL.Items.Clear();
        //    this.UnitDDL.DataTextField = "UnitName";
        //    this.UnitDDL.DataValueField = "UnitCode";
        //    this.UnitDDL.DataSource = this._unitBL.GetListUnitConvertFromForDDL(_prmUnitCode);
        //    this.UnitDDL.DataBind();
        //    this.UnitDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //protected void UnitDDL_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.UnitDDL.SelectedValue != "null")
        //    {
        //        string _product = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);

        //        string _tempUnitCode = this._productBL.GetUnitCodeByCode(_product);

        //        decimal _totalQty = _unitBL.FindConvertionUnit(_product, _tempUnitCode, this.UnitDDL.SelectedValue) * Convert.ToDecimal(this.QtyTextBox.Text);

        //        this.QtyConvertionTextBox.Text = (_totalQty == 0) ? "0" : _totalQty.ToString("#,###.##");

        //        decimal _amount = _totalQty * Convert.ToDecimal(this.PriceTextBox.Text);
        //        this.AmountTextBox.Text = (_amount == 0) ? "0" : _amount.ToString("#,###.##");

        //        this.DiscPercentTextBox.Text = "0";
        //        this.DiscTextBox.Text = "0";

        //        this.NettoTextBox.Text = (_amount == 0) ? "0" : _amount.ToString("#,###.##");
        //    }
        //    else
        //    {
        //        this.QtyConvertionTextBox.Text = "0";
        //        this.AmountTextBox.Text = "0";

        //        this.DiscPercentTextBox.Text = "0";
        //        this.DiscTextBox.Text = "0";

        //        this.NettoTextBox.Text = "0";
        //    }
        //}

        protected void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _revisi = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisi), ApplicationConfig.EncryptionKey);
            string _product = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);

            PRCPODt _prcPODt = this._purchaseOrderBL.GetSinglePRCPODt(_transNo, Convert.ToInt32(_revisi), _product);

            this.ProductTextBox.Text = _product + " - " + this._productBL.GetProductNameByCode(_product);
            this.SpecificationTextBox.Text = _prcPODt.Specification;

            if (_prcPODt.ETD != null)
            {
                this.ETDTextBox.Text = DateFormMapper.GetValue(_prcPODt.ETD);
            }
            else
            {
                this.ETDTextBox.Text = "";
            }

            if (_prcPODt.ETA != null)
            {
                this.ETATextBox.Text = DateFormMapper.GetValue(_prcPODt.ETA);
            }
            else
            {
                this.ETATextBox.Text = "";
            }

            //this.QtyTextBox.Text = (_prcPODt.QtyWrhsPO == 0) ? "0" : _prcPODt.QtyWrhsPO.ToString("#,###.##");
            //this.QtyFreeTextBox.Text = (_prcPODt.QtyWrhsFree == 0) ? "0" : _prcPODt.QtyWrhsFree.ToString("#,###.##");
            //this.QtyTotalTextBox.Text = (_prcPODt.QtyWrhsTotal == 0) ? "0" : _prcPODt.QtyWrhsTotal.ToString("#,###.##");
            //this.UnitTextBox.Text = this._unitBL.GetUnitNameByCode(_prcPODt.UnitWrhs);
            this.QtyConvertionTextBox.Text = (_prcPODt.Qty == 0) ? "0" : _prcPODt.Qty.ToString("#,###.##");

            //this.ShowUnit(this._productBL.GetUnitCodeByCode(_product));
            //this.UnitDDL.SelectedValue = _prcPODt.Unit;
            this.UnitConvertionTextBox.Text = this._unitBL.GetUnitNameByCode(_prcPODt.Unit);

            bool _isUsingPG = this._productBL.GetSingleProductType(this._productBL.GetSingleProduct(_product).ProductType).IsUsingPG;
            if (_isUsingPG == true)
            {
                this.PriceTextBox.Attributes.Add("ReadOnly", "True");
                this.PriceTextBox.Attributes.Add("style", "background-color:#cccccc");
            }
            else
            {
                this.PriceTextBox.Attributes.Remove("ReadOnly");
                this.PriceTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }
            this.PriceTextBox.Text = (_prcPODt.PriceForex == 0) ? "0" : _prcPODt.PriceForex.ToString("#,###.##");
            this.AmountTextBox.Text = (_prcPODt.AmountForex == 0) ? "0" : _prcPODt.AmountForex.ToString("#,###.##");
            this.DiscPercentTextBox.Text = (_prcPODt.Disc == 0) ? "0" : _prcPODt.Disc.ToString("#,###.##");
            this.DiscTextBox.Text = (_prcPODt.DiscForex == 0) ? "0" : _prcPODt.DiscForex.ToString("#,###.##");
            this.NettoTextBox.Text = (_prcPODt.NettoForex == 0) ? "0" : _prcPODt.NettoForex.ToString("#,###.##");
            this.RemarkTextBox.Text = _prcPODt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _revisi = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisi), ApplicationConfig.EncryptionKey);
            string _product = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);

            PRCPODt _prcPODt = this._purchaseOrderBL.GetSinglePRCPODt(_transNo, Convert.ToInt32(_revisi), _product);

            _prcPODt.Specification = this.SpecificationTextBox.Text;

            if (this.ETDTextBox.Text != "")
            {
                _prcPODt.ETD = DateFormMapper.GetValue(this.ETDTextBox.Text);
            }
            else
            {
                _prcPODt.ETD = null;
            }

            if (this.ETATextBox.Text != "")
            {
                _prcPODt.ETA = DateFormMapper.GetValue(this.ETATextBox.Text);
            }
            else
            {
                _prcPODt.ETA = null;
            }

            _prcPODt.Qty = Convert.ToDecimal(this.QtyConvertionTextBox.Text);
            //_prcPODt.Unit = this.UnitDDL.SelectedValue;
            _prcPODt.PriceForex = Convert.ToDecimal(this.PriceTextBox.Text);
            _prcPODt.AmountForex = Convert.ToDecimal(this.AmountTextBox.Text);
            _prcPODt.Disc = Convert.ToDecimal(this.DiscPercentTextBox.Text);
            _prcPODt.DiscForex = Convert.ToDecimal(this.DiscTextBox.Text);
            decimal _nettoOriginal = _prcPODt.NettoForex;
            _prcPODt.NettoForex = Convert.ToDecimal(this.NettoTextBox.Text);
            //_prcPODt.QtyWrhsPO = Convert.ToDecimal(this.QtyTextBox.Text);
            //_prcPODt.QtyWrhsFree = Convert.ToDecimal(this.QtyFreeTextBox.Text);
            //_prcPODt.QtyWrhsTotal = Convert.ToDecimal(this.QtyTotalTextBox.Text);
            _prcPODt.Remark = this.RemarkTextBox.Text;

            _prcPODt.EditBy = HttpContext.Current.User.Identity.Name;
            _prcPODt.EditDate = DateTime.Now;

            bool _result = this._purchaseOrderBL.EditPRCPODt(_prcPODt, _nettoOriginal);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisi)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }

        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisi)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}