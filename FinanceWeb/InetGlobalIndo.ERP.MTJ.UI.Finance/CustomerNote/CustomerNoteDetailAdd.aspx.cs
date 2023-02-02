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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerNote
{
    public partial class CustomerNoteDetailAdd : CustomerNoteBase
    {
        private CustomerNoteBL _customerNoteBL = new CustomerNoteBL();
        private BillOfLadingBL _bolBL = new BillOfLadingBL();
        //private AccountBL _account = new AccountBL();
        private UnitBL _unitBL = new UnitBL();
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
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

                this.ShowSJ();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void SetAttributeRate()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _currCode = this._customerNoteBL.GetSingleFINCustInvHd(_transNo).CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.PriceTextBox.Attributes.Add("OnBlur", "Count(" + this.QtyTextBox.ClientID + "," + this.PriceTextBox.ClientID + "," + this.AmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

        }

        public void SetAttribute()
        {
            this.SONoTextBox.Attributes.Add("ReadOnly", "True");
            this.QtyTextBox.Attributes.Add("ReadOnly", "True");
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountTextBox.Attributes.Add("ReadOnly", "True");

            this.PriceTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        public void ClearData()
        {
            this.ClearLabel();

            this.SJNoDropDownList.SelectedValue = "null";
            this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.ProductDropDownList.SelectedValue = "null";
            this.SONoTextBox.Text = "";
            this.QtyTextBox.Text = "0";
            this.UnitTextBox.Text = "";
            this.PriceTextBox.Text = "0";
            this.AmountTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
        }

        public void ShowSJ()
        {
            this.SJNoDropDownList.Items.Clear();
            this.SJNoDropDownList.DataTextField = "FileNmbr";
            this.SJNoDropDownList.DataValueField = "TransNmbr";
            this.SJNoDropDownList.DataSource = this._bolBL.GetListDDLV_STSJForCI();
            this.SJNoDropDownList.DataBind();
            this.SJNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowProduct()
        {
            this.ProductDropDownList.Items.Clear();
            this.ProductDropDownList.DataTextField = "ProductName";
            this.ProductDropDownList.DataValueField = "ProductCode";
            this.ProductDropDownList.DataSource = this._bolBL.GetListDDLProductFromV_STSJForCI(this.SJNoDropDownList.SelectedValue);
            this.ProductDropDownList.DataBind();
            this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            STCSJDt _stcSJDt = _bolBL.GetSingleProductFromV_STSJForCI(this.SJNoDropDownList.SelectedValue, this.ProductDropDownList.SelectedValue);

            FINCustInvDt _finCustInvDt = new FINCustInvDt();
            _finCustInvDt.TransNmbr = _transNo;
            _finCustInvDt.SJNo = this.SJNoDropDownList.SelectedValue;
            _finCustInvDt.SONo = this.SoNoHiddenField.Value;
            //_finCustInvDt.SONo = this.SONoTextBox.Text;
            _finCustInvDt.ProductCode = this.ProductDropDownList.SelectedValue;
            _finCustInvDt.Qty = Convert.ToInt32(this.QtyTextBox.Text);
            _finCustInvDt.Unit = _stcSJDt.Unit;
            _finCustInvDt.PriceForex = Convert.ToDecimal(this.PriceTextBox.Text);
            _finCustInvDt.AmountForex = Convert.ToDecimal(this.AmountTextBox.Text);
            _finCustInvDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._customerNoteBL.AddFINCustInvDt(_finCustInvDt);

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

        protected void SJNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearLabel();

            if (this.SJNoDropDownList.SelectedValue != "null")
            {
                this.ShowProduct();
                if (this.ProductDropDownList.SelectedValue != "null")
                {
                    STCSJDt _stcSJDt = _bolBL.GetSingleProductFromV_STSJForCI(this.SJNoDropDownList.SelectedValue, this.ProductDropDownList.SelectedValue);

                    this.SONoTextBox.Text = _stcSJDt.SONo;
                    this.SoNoHiddenField.Value = _stcSJDt.SONo;
                    this.QtyTextBox.Text = (_stcSJDt.Qty == 0) ? "0" : _stcSJDt.Qty.ToString("#,###.##");
                    this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_stcSJDt.Unit);
                }
                else
                {
                    this.SONoTextBox.Text = "";
                    this.SoNoHiddenField.Value = "";
                    this.QtyTextBox.Text = "";
                    this.UnitTextBox.Text = "";
                    this.PriceTextBox.Text = "0";
                    this.AmountTextBox.Text = "0";
                }
            }
            else
            {
                this.ProductDropDownList.Items.Clear();
                this.ProductDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.ProductDropDownList.SelectedValue = "null";
                this.SONoTextBox.Text = "";
                this.SoNoHiddenField.Value = "";
                this.QtyTextBox.Text = "";
                this.UnitTextBox.Text = "";
                this.PriceTextBox.Text = "0";
                this.AmountTextBox.Text = "0";
            }
        }

        protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearLabel();

            if (this.ProductDropDownList.SelectedValue != "null")
            {
                STCSJDt _stcSJDt = _bolBL.GetSingleProductFromV_STSJForCI(this.SJNoDropDownList.SelectedValue, this.ProductDropDownList.SelectedValue);

                this.SONoTextBox.Text = this._salesOrderBL.GetFileNmbrMKTSOHd(_stcSJDt.SONo);
                this.SoNoHiddenField.Value = _stcSJDt.SONo;
                this.QtyTextBox.Text = (_stcSJDt.Qty == 0) ? "0" : _stcSJDt.Qty.ToString("#,###.##");
                this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_stcSJDt.Unit);
            }
            else
            {
                this.SONoTextBox.Text = "";
                this.SoNoHiddenField.Value = "";
                this.QtyTextBox.Text = "";
                this.UnitTextBox.Text = "";
                this.PriceTextBox.Text = "0";
                this.AmountTextBox.Text = "0";
            }
        }
    }
}