using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierNote
{
    public partial class SupplierNoteDetailAdd : SupplierNoteBase
    {
        private SupplierNoteBL _supplierNoteBL = new SupplierNoteBL();
        private ReceivingPOBL _receivingPOBL = new ReceivingPOBL();
        private UnitBL _unitBL = new UnitBL();
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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

                this.ShowPO();
                this.ShowRR(" ");
                this.ClearData();
                this.SetAttribute();
            }

            if (this.tempProductCode.Value != this.ProductPicker1.ProductCode)
            {
                this.tempProductCode.Value = this.ProductPicker1.ProductCode;

                if (this.ProductPicker1.ProductCode != "null")
                {
                    this.PONoTextBox.Text = _receivingPOBL.GetFileNmbrFromVSTRRForSI(this.RRNoDropDownList.SelectedValue);
                    this.PONoHiddenField.Value = _receivingPOBL.GetPONoFromVSTRRForSI(this.RRNoDropDownList.SelectedValue);
                    this.QtyTextBox.Text = (_receivingPOBL.GetQtyFromVSTRRForSI(this.RRNoDropDownList.SelectedValue, this.ProductPicker1.ProductCode) == 0) ? "0" : _receivingPOBL.GetQtyFromVSTRRForSI(this.RRNoDropDownList.SelectedValue, this.ProductPicker1.ProductCode).ToString("#,###.##");
                    this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_receivingPOBL.GetUnitFromVSTRRForSI(this.RRNoDropDownList.SelectedValue, this.ProductPicker1.ProductCode));
                }
                else
                {
                    this.PONoTextBox.Text = "";
                    this.QtyTextBox.Text = "";
                    this.UnitTextBox.Text = "";
                }

                this.PriceTextBox.Text = "0";
                this.AmountTextBox.Text = "0";
            }
        }

        public void SetAttribute()
        {
            this.PONoTextBox.Attributes.Add("ReadOnly", "True");
            this.QtyTextBox.Attributes.Add("ReadOnly", "True");
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");
            this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PriceTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();

            this.RRNoDropDownList.SelectedValue = "null";
            //this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //this.ProductDropDownList.SelectedValue = "null";
            this.PONoTextBox.Text = "";
            this.PONoHiddenField.Value = "";
            this.QtyTextBox.Text = "0";
            this.UnitTextBox.Text = "";
            this.PriceTextBox.Text = "0";
            this.AmountTextBox.Text = "0";
            this.RemarkTextBox.Text = "";

            FINSuppInvHd _finSuppInvHd = _supplierNoteBL.GetSingleFINSuppInvHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finSuppInvHd.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
        }

        public void ShowPO()
        {
            this.PODropDownList.Items.Clear();
            this.PODropDownList.DataTextField = "FileNmbr";
            this.PODropDownList.DataValueField = "TransNmbr";
            this.PODropDownList.DataSource = this._receivingPOBL.GetListDDLPONoForSI();
            this.PODropDownList.DataBind();
            this.PODropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowRR(string _prmPO)
        {
            this.RRNoDropDownList.Items.Clear();
            this.RRNoDropDownList.DataTextField = "FileNmbr";
            this.RRNoDropDownList.DataValueField = "TransNmbr";
            this.RRNoDropDownList.DataSource = this._receivingPOBL.GetListDDLRRNoFromVSTRRForSI(_prmPO);
            this.RRNoDropDownList.DataBind();
            this.RRNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //public void ShowProduct()
        //{
        //    this.ProductDropDownList.Items.Clear();
        //    this.ProductDropDownList.DataTextField = "ProductName";
        //    this.ProductDropDownList.DataValueField = "ProductCode";
        //    this.ProductDropDownList.DataSource = this._receivingPOBL.GetListDDLProductFromVSTRRForSI(this.RRNoDropDownList.SelectedValue);
        //    this.ProductDropDownList.DataBind();
        //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            FINSuppInvDt _finSuppInvDt = new FINSuppInvDt();

            _finSuppInvDt.TransNmbr = _transNo;
            _finSuppInvDt.RRNo = this.RRNoDropDownList.SelectedValue;
            _finSuppInvDt.PONo = this.PONoHiddenField.Value;
            _finSuppInvDt.ProductCode = this.ProductPicker1.ProductCode ;
            _finSuppInvDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _finSuppInvDt.Unit = _receivingPOBL.GetUnitFromVSTRRForSI(this.RRNoDropDownList.SelectedValue, this.ProductPicker1.ProductCode);
            _finSuppInvDt.PriceForex = Convert.ToDecimal(this.PriceTextBox.Text);
            _finSuppInvDt.AmountForex = Convert.ToDecimal(this.AmountTextBox.Text);
            _finSuppInvDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._supplierNoteBL.AddFINSuppInvDt(_finSuppInvDt);

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

        protected void RRNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.RRNoDropDownList.SelectedValue != "null")
            //{
            //    this.ShowProduct();
            //}
            //else
            //{
            //    this.ProductDropDownList.Items.Clear();
            //    this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //    this.ProductDropDownList.SelectedValue = "null";
            //}
        }

        //protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ProductPicker1.ProductCode != "null")
        //    {
        //        this.PONoTextBox.Text = _receivingPOBL.GetFileNmbrFromVSTRRForSI(this.RRNoDropDownList.SelectedValue);
        //        this.PONoHiddenField.Value = _receivingPOBL.GetPONoFromVSTRRForSI(this.RRNoDropDownList.SelectedValue);
        //        this.QtyTextBox.Text = (_receivingPOBL.GetQtyFromVSTRRForSI(this.RRNoDropDownList.SelectedValue, this.ProductPicker1.ProductCode) == 0) ? "0" : _receivingPOBL.GetQtyFromVSTRRForSI(this.RRNoDropDownList.SelectedValue, this.ProductPicker1.ProductCode).ToString("#,###.##");
        //        this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_receivingPOBL.GetUnitFromVSTRRForSI(this.RRNoDropDownList.SelectedValue, this.ProductPicker1.ProductCode));
        //    }
        //    else
        //    {
        //        this.PONoTextBox.Text = "";
        //        this.QtyTextBox.Text = "";
        //        this.UnitTextBox.Text = "";
        //    }

        //    this.PriceTextBox.Text = "0";
        //    this.AmountTextBox.Text = "0";
        //}

        protected void PODropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowRR(this.PODropDownList.SelectedValue);
        }
    }
}