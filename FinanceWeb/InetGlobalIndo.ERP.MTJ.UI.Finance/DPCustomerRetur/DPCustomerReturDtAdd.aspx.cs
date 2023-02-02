using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerRetur
{
    public partial class DPCustomerReturDtAdd : DPCustomerReturBase
    {
        private FINDPCustomerReturBL _finDPCustomerReturBL = new FINDPCustomerReturBL();
        private FINDPCustomerBL _finDPCustHdBL = new FINDPCustomerBL();
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

                this.ShowDPNo();

                this.SetAttribute();
                this.ClearData();
            }
        }

        private void SetAttributeRate()
        {
            this.BaseForexTextBox.Attributes.Add("OnBlur", "CalculateTotalForexFromBase(" + this.BaseForexTextBox.ClientID + "," + this.PPNTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNForexTextBox.Attributes.Add("OnBlur", "CalculateTotalForex(" + this.BaseForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void SetAttribute()
        {
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.PPNForexTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowDPNo()
        {
            string _custCodeHeader = _finDPCustomerReturBL.GetCustomerByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_custCodeHeader != "")
            {
                this.DPNoDropDownList.Items.Clear();
                this.DPNoDropDownList.DataTextField = "FileNmbr";
                this.DPNoDropDownList.DataValueField = "TransNmbr";
                this.DPNoDropDownList.DataSource = this._finDPCustomerReturBL.GetListDPNoFileNoForDDL(_custCodeHeader);
                this.DPNoDropDownList.DataBind();
                this.DPNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            else
            {
                this.DPNoDropDownList.Items.Clear();
                this.DPNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
        }

        public void ClearData()
        {
            this.ClearLabel();

            this.CurrCodeTextBox.Text = "";
            this.ForexRateTextBox.Text = "";
            this.BaseForexTextBox.Text = "0";
            this.PPNTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
        }

        protected void DPNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DPNoDropDownList.SelectedValue != "null")
            {
                FINDPCustHd _finDPCustHd = _finDPCustHdBL.GetSingleFINDPCustHd(this.DPNoDropDownList.SelectedValue);

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finDPCustHd.CurrCode);

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.CurrCodeTextBox.Text = _finDPCustHd.CurrCode;
                this.ForexRateTextBox.Text = _finDPCustHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.BaseForexTextBox.Text = (_finDPCustHd.BaseForex - Convert.ToDecimal(_finDPCustHd.Balance == null ? 0 : _finDPCustHd.Balance)).ToString("#,###.##");
                this.PPNTextBox.Text = (_finDPCustHd.PPN == 0) ? "0" : _finDPCustHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                if ((_finDPCustHd.PPNForex - Convert.ToDecimal(_finDPCustHd.BalancePPn == null ? 0 : _finDPCustHd.BalancePPn)) == 0)
                {
                    this.PPNForexTextBox.Text = "0";
                }
                else
                {
                    this.PPNForexTextBox.Text = (_finDPCustHd.PPNForex - Convert.ToDecimal(_finDPCustHd.BalancePPn == null ? 0 : _finDPCustHd.BalancePPn)).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                }


                this.TotalForexTextBox.Text = ((_finDPCustHd.BaseForex - Convert.ToDecimal(_finDPCustHd.Balance == null ? 0 : _finDPCustHd.Balance)) + (_finDPCustHd.PPNForex - Convert.ToDecimal(_finDPCustHd.BalancePPn == null ? 0 : _finDPCustHd.BalancePPn))).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.ClearData();
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            FINDPCustReturDt _finDPCustReturDt = new FINDPCustReturDt();

            _finDPCustReturDt.TransNmbr = _transNo;
            _finDPCustReturDt.DPNo = this.DPNoDropDownList.SelectedValue;
            _finDPCustReturDt.CurrCode = this.CurrCodeTextBox.Text;
            _finDPCustReturDt.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text == "" ? "0" : this.ForexRateTextBox.Text);
            _finDPCustReturDt.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text == "" ? "0" : this.BaseForexTextBox.Text);
            _finDPCustReturDt.PPN = Convert.ToDecimal(this.PPNTextBox.Text == "" ? "0" : this.PPNTextBox.Text);
            _finDPCustReturDt.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text == "" ? "0" : this.PPNForexTextBox.Text);
            _finDPCustReturDt.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text == "" ? "0" : this.TotalForexTextBox.Text);
            _finDPCustReturDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._finDPCustomerReturBL.AddFINDPCustReturDt(_finDPCustReturDt);

            if (_result == true)
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

        //private void ClearDataNumeric()
        //{
        //    this.ForexRateTextBox.Text = "0";
        //    this.DecimalPlaceHiddenField.Value = "";
        //    this.PPNTextBox.Text = "0";
        //    this.CurrCodeTextBox.Text = "";
        //    this.BaseForexTextBox.Text = "0";
        //    this.PPNForexTextBox.Text = "0";
        //    this.TotalForexTextBox.Text = "0";
        //}

    }
}