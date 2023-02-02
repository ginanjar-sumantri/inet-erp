using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierRetur
{
    public partial class DPSuppReturDetailAdd : DPSupplierReturBase
    {
        private FINDPSuppPayBL _finDPSuppPayBL = new FINDPSuppPayBL();
        private FINDPSuppReturBL _finDPSuppReturBL = new FINDPSuppReturBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
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
                this.ShowDP();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.BaseForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.BaseForexTextBox.ClientID + "," + this.PPNTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + ","+this.DecimalPlaceHiddenField.ClientID+");");
        }

        public void SetAttribute()
        {
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate(); 
        }

        public void ClearData()
        {
            this.ClearLabel();
            this.DPNoDropDownList.SelectedValue = "null";
            this.CurrCodeTextBox.Text = "";
            this.CurrRateTextBox.Text = "";
            this.BaseForexTextBox.Text = "";
            this.PPNTextBox.Text = "";
            this.PPNForexTextBox.Text = "";
            this.TotalForexTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void DPNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DPNoDropDownList.SelectedValue != "null")
            {
                FINDPSuppHd _finDPSuppHd = this._finDPSuppPayBL.GetSingleFINDPSuppHd(this.DPNoDropDownList.SelectedValue);

                this.CurrCodeTextBox.Text = _finDPSuppHd.CurrCode;

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeTextBox.Text);
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                string _defaultCurrency = this._currencyBL.GetCurrDefault();
                if (_finDPSuppHd.CurrCode.Trim().ToLower() == _defaultCurrency.Trim().ToLower())
                {
                    this.CurrRateTextBox.Text = "1";
                    this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#cccccc");
                }
                else
                {
                    this.CurrRateTextBox.Text = (_finDPSuppHd.ForexRate == 0) ? "0" : _finDPSuppHd.ForexRate.ToString("#,###.##");
                    this.CurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.CurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
                }

                decimal _tempBaseForex = _finDPSuppHd.BaseForex - Convert.ToDecimal((_finDPSuppHd.Balance == null) ? 0 : _finDPSuppHd.Balance);
                this.BaseForexTextBox.Text = (_tempBaseForex == 0) ? "0" : _tempBaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNTextBox.Text = (_finDPSuppHd.PPN == 0) ? "0" : _finDPSuppHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                decimal _tempPPNForex = _finDPSuppHd.PPNForex - Convert.ToDecimal((_finDPSuppHd.BalancePPn == null) ? 0 : _finDPSuppHd.BalancePPn);
                this.PPNForexTextBox.Text = (_tempPPNForex == 0) ? "0" : _tempPPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                decimal _tempTotalForex = _tempBaseForex + _tempPPNForex;
                this.TotalForexTextBox.Text = (_tempTotalForex == 0) ? "0" : _tempTotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.RemarkTextBox.Text = _finDPSuppHd.Remark;
            }
            else
            {
                this.ClearData();
            }
        }



        public void ShowDP()
        {
            string _tempSuppCode = this._finDPSuppReturBL.GetSuppCodeHeader(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.DPNoDropDownList.Items.Clear();
            this.DPNoDropDownList.DataTextField = "FileNmbr";
            this.DPNoDropDownList.DataValueField = "TransNmbr";
            this.DPNoDropDownList.DataSource = this._finDPSuppPayBL.GetListDPForDDL(_tempSuppCode);
            this.DPNoDropDownList.DataBind();
            this.DPNoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINDPSuppReturDt _finDPSuppReturDt = new FINDPSuppReturDt();

            _finDPSuppReturDt.TransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            _finDPSuppReturDt.DPNo = this.DPNoDropDownList.SelectedValue;
            _finDPSuppReturDt.CurrCode = this.CurrCodeTextBox.Text;
            _finDPSuppReturDt.ForexRate = Convert.ToDecimal(this.CurrRateTextBox.Text);
            _finDPSuppReturDt.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _finDPSuppReturDt.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
            _finDPSuppReturDt.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _finDPSuppReturDt.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _finDPSuppReturDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._finDPSuppReturBL.AddFINDPSuppReturDt(_finDPSuppReturDt);

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

    }
}