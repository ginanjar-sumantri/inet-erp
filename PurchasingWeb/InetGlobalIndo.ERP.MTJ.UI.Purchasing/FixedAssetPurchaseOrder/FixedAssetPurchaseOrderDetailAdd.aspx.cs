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
    public partial class FixedAssetPurchaseOrderDetailAdd : FixedAssetPurchaseOrderBase
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

                this.ClearLabel();
                this.SetAttribute();
                this.ClearData();
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

        protected void ClearData()
        {
            this.FANameTextBox.Text = "";
            this.SpecificationTextBox.Text = "";
            this.QtyConvertionTextBox.Text = "0";

            this.ShowUnit();
            this.UnitDDL.SelectedValue = "null";
            this.PriceTextBox.Text = "0";
            this.AmountTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            PRCFAPODt _prcFAPODt = new PRCFAPODt();

            _prcFAPODt.TransNmbr = _transNo;
            _prcFAPODt.FANAme = this.FANameTextBox.Text;
            _prcFAPODt.Specification = this.SpecificationTextBox.Text;
            _prcFAPODt.Qty = Convert.ToDecimal(this.QtyConvertionTextBox.Text);
            _prcFAPODt.Unit = this.UnitDDL.SelectedValue;
            _prcFAPODt.PriceForex = Convert.ToDecimal(this.PriceTextBox.Text);
            _prcFAPODt.AmountForex = Convert.ToDecimal(this.AmountTextBox.Text);
            _prcFAPODt.Remark = this.RemarkTextBox.Text;
            _prcFAPODt.CreatedBy = HttpContext.Current.User.Identity.Name;
            _prcFAPODt.CreatedDate = DateTime.Now;
            _prcFAPODt.EditBy = HttpContext.Current.User.Identity.Name;
            _prcFAPODt.EditDate = DateTime.Now;

            bool _result = this._faPOBL.AddPRCFAPODt(_prcFAPODt);

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