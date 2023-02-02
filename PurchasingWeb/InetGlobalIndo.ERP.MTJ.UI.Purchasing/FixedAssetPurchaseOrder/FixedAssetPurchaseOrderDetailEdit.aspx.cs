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

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.FixedAssetPurchaseOrder
{
    public partial class FixedAssetPurchaseOrderDetailEdit : FixedAssetPurchaseOrderBase
    {
        private FixedAssetPurchaseOrderBL _faPOBL = new FixedAssetPurchaseOrderBL();
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
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");

            this.QtyConvertionTextBox.Attributes.Add("OnBlur", "CalculateNetto(" + this.QtyConvertionTextBox.ClientID + ", " + this.PriceTextBox.ClientID + ", " + this.AmountTextBox.ClientID + ");");
            this.PriceTextBox.Attributes.Add("OnBlur", "CalculateNetto(" + this.QtyConvertionTextBox.ClientID + ", " + this.PriceTextBox.ClientID + ", " + this.AmountTextBox.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ShowUnit()
        {
            this.UnitDDL.Items.Clear();
            this.UnitDDL.DataTextField = "UnitName";
            this.UnitDDL.DataValueField = "UnitCode";
            this.UnitDDL.DataSource = this._unitBL.GetListForDDL();
            this.UnitDDL.DataBind();
            this.UnitDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

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
            string _faName = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._faKey), ApplicationConfig.EncryptionKey);

            PRCFAPODt _prcFAPODt = this._faPOBL.GetSinglePRCFAPODt(_transNo, _faName);

            this.FANameTextBox.Text = _faName;
            this.SpecificationTextBox.Text = _prcFAPODt.Specification;


            this.QtyConvertionTextBox.Text = (_prcFAPODt.Qty == 0) ? "0" : _prcFAPODt.Qty.ToString("#,###.##");

            this.ShowUnit();
            this.UnitDDL.SelectedValue = _prcFAPODt.Unit;

            //bool _isUsingPG = this._productBL.GetSingleProductType(this._productBL.GetSingleProduct(_product).ProductType).IsUsingPG;
            //if (_isUsingPG == true)
            //{
            //    this.PriceTextBox.Attributes.Add("ReadOnly", "True");
            //    this.PriceTextBox.Attributes.Add("style", "background-color:#cccccc");
            //}
            //else
            //{
            //    this.PriceTextBox.Attributes.Remove("ReadOnly");
            //    this.PriceTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            //}
            this.PriceTextBox.Text = (_prcFAPODt.PriceForex == 0) ? "0" : _prcFAPODt.PriceForex.ToString("#,##0.##");
            this.AmountTextBox.Text = (_prcFAPODt.AmountForex == 0) ? "0" : _prcFAPODt.AmountForex.ToString("#,##0.##");
            this.RemarkTextBox.Text = _prcFAPODt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _faName = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._faKey), ApplicationConfig.EncryptionKey);

            PRCFAPODt _prcFAPODt = this._faPOBL.GetSinglePRCFAPODt(_transNo, _faName);
            decimal _nettoOriginal = _prcFAPODt.AmountForex;

            _prcFAPODt.Specification = this.SpecificationTextBox.Text;
            _prcFAPODt.Qty = Convert.ToDecimal(this.QtyConvertionTextBox.Text);
            _prcFAPODt.Unit = this.UnitDDL.SelectedValue;
            _prcFAPODt.PriceForex = Convert.ToDecimal(this.PriceTextBox.Text);
            _prcFAPODt.AmountForex = Convert.ToDecimal(this.AmountTextBox.Text);
            _prcFAPODt.Remark = this.RemarkTextBox.Text;
            _prcFAPODt.EditBy = HttpContext.Current.User.Identity.Name;
            _prcFAPODt.EditDate = DateTime.Now;

            bool _result = this._faPOBL.EditPRCFAPODt(_prcFAPODt, _nettoOriginal);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }

        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}