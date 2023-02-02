using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.DirectPurchase
{
    public partial class DirectPurchaseEdit : DirectPurchaseBase
    {
        private SupplierBL _suppBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private TermBL _termBL = new TermBL();
        private DirectPurchaseBL _DirectPurchaseBL = new DirectPurchaseBL();
        private PermissionBL _permBL = new PermissionBL();
        private PaymentBL _paymentBL = new PaymentBL();

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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.btnSearchSupplier.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findSupplier&configCode=supplier','_popSearch','width=400,height=550,toolbar=0,location=0,status=0,scrollbars=1')";

                String spawnJS = "<script language='JavaScript'>\n";

                ////////////////////DECLARE FUNCTION FOR CATCHING CUSTOMER SEARCH
                spawnJS += "function findSupplier(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.SuppNmbrTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.SupplierNameTextBox.ClientID + "').value = dataArray[1];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                ////////////////////ONCHANGE DISCOUNT AMOUNT
                spawnJS += "function setDiscAmount() {\n";
                spawnJS += "document.getElementById('" + this.discountValue.ClientID + "').value = Math.round(";
                spawnJS += "document.getElementById('" + this.discount.ClientID + "').value * ";
                spawnJS += "document.getElementById('" + this.BaseForexTextBox.ClientID + "').value / 100 ); \n";
                spawnJS += "document.getElementById('" + this.TotalAmountTextBox.ClientID + "').value = Math.round(";
                spawnJS += "parseInt(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value) - parseInt(document.getElementById('" + this.discountValue.ClientID + "').value) + ";
                spawnJS += "parseInt(document.getElementById('" + this.PPNAmountTextBox.ClientID + "').value) + parseInt(document.getElementById('" + this.PPHAmountTextBox.ClientID + "').value)); \n";
                spawnJS += "}\n";

                ////////////////////ONCHANGE DISCOUNT
                spawnJS += "function setDisc() {\n";
                spawnJS += "if (parseFloat(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value) != 0){\n";
                spawnJS += "document.getElementById('" + this.discount.ClientID + "').value = Math.round(";
                spawnJS += "(parseFloat(document.getElementById('" + this.discountValue.ClientID + "').value) / ";
                spawnJS += "parseFloat(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value)) * 100 ); \n";
                spawnJS += "}\n";
                spawnJS += "else {\n";
                spawnJS += "document.getElementById('" + this.discount.ClientID + "').value = 0; \n";
                spawnJS += "}\n";
                spawnJS += "document.getElementById('" + this.TotalAmountTextBox.ClientID + "').value = Math.round(";
                spawnJS += "parseFloat(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value) - parseFloat(document.getElementById('" + this.discountValue.ClientID + "').value) + ";
                spawnJS += "parseFloat(document.getElementById('" + this.PPNAmountTextBox.ClientID + "').value) + parseFloat(document.getElementById('" + this.PPHAmountTextBox.ClientID + "').value)); \n";
                spawnJS += "}\n";

                ////////////////////ONCHANGE PPN AMOUNT
                spawnJS += "function setPPNAmount() {\n";
                spawnJS += "document.getElementById('" + this.PPNAmountTextBox.ClientID + "').value = Math.round(";
                spawnJS += "document.getElementById('" + this.PPNTextBox.ClientID + "').value * ";
                spawnJS += "(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value - document.getElementById('" + this.discountValue.ClientID + "').value) / 100 ) ; \n";
                spawnJS += "document.getElementById('" + this.TotalAmountTextBox.ClientID + "').value = Math.round(";
                spawnJS += "parseFloat(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value) - parseFloat(document.getElementById('" + this.discountValue.ClientID + "').value) + ";
                spawnJS += "parseFloat(document.getElementById('" + this.PPNAmountTextBox.ClientID + "').value) + parseFloat(document.getElementById('" + this.PPHAmountTextBox.ClientID + "').value)); \n";
                spawnJS += "}\n";

                ////////////////////ONCHANGE PPH AMOUNT
                spawnJS += "function setPPHAmount() {\n";
                spawnJS += "document.getElementById('" + this.PPHAmountTextBox.ClientID + "').value = Math.round(";
                spawnJS += "document.getElementById('" + this.PPHTextBox.ClientID + "').value * ";
                spawnJS += "(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value - document.getElementById('" + this.discountValue.ClientID + "').value) / 100 ) ;\n";
                spawnJS += "document.getElementById('" + this.TotalAmountTextBox.ClientID + "').value = Math.round(";
                spawnJS += "parseFloat(document.getElementById('" + this.BaseForexTextBox.ClientID + "').value) - parseFloat(document.getElementById('" + this.discountValue.ClientID + "').value) + ";
                spawnJS += "parseFloat(document.getElementById('" + this.PPNAmountTextBox.ClientID + "').value) + parseFloat(document.getElementById('" + this.PPHAmountTextBox.ClientID + "').value)); \n";
                spawnJS += "}\n";



                spawnJS += "</script>\n";

                this.javascriptReceiver.Text = spawnJS;

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ClearLabel();
                this.SetAttribute();

                this.ShowData();
            }

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNAmountTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalAmountTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNAmountTextBox.Attributes.Add("ReadOnly", "True");
            this.PPHAmountTextBox.Attributes.Add("ReadOnly", "True");
            this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");

            this.PPNTextBox.Attributes.Add("onkeyup", "numericInput(this)");
            this.PPNTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return setPPNAmount();}");
            this.PPNTextBox.Attributes.Add("onchange", "numericInput(this);setPPNAmount();");

            this.PPHTextBox.Attributes.Add("onkeyup", "numericInput(this)");
            this.PPHTextBox.Attributes.Add("onkeydown", "if(event.keyCode==13){return setPPHAmount();}");
            this.PPHTextBox.Attributes.Add("onchange", "numericInput(this);setPPHAmount();");

            this.discount.Attributes.Add("onkeyup", "numericInput(this)");
            this.discount.Attributes.Add("onkeydown", "if(event.keyCode==13){return setDiscAmount();}");
            this.discount.Attributes.Add("onchange", "numericInput(this);setDiscAmount();");
            this.discountValue.Attributes.Add("onkeyup", "numericInput(this)");
            this.discountValue.Attributes.Add("onkeydown", "if(event.keyCode==13){return setDisc();}");
            this.discountValue.Attributes.Add("onchange", "numericInput(this);setDisc();");
        }

        private void ShowPayType()
        {
            this.PayTypeDropDownList.Items.Clear();
            this.PayTypeDropDownList.DataTextField = "PayName";
            this.PayTypeDropDownList.DataValueField = "PayCode";
            this.PayTypeDropDownList.DataSource = this._paymentBL.GetListDDLDPSuppPay(this.CurrTextBox.Text);
            this.PayTypeDropDownList.DataBind();
            this.PayTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            PRCTrDirectPurchaseHd _directPurchaseHd = this._DirectPurchaseBL.GetSingleDirectPurchaseHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_directPurchaseHd.CurrCode);
            string _currDefault = _currencyBL.GetCurrDefault();
            this.DecimalPlaceHiddenField.Value = _decimalPlace.ToString();

            this.TransNoTextBox.Text = _directPurchaseHd.TransNmbr;
            this.FileNmbrTextBox.Text = _directPurchaseHd.FileNmbr;
            this.TransTypeDDL.SelectedValue = _directPurchaseHd.TransType;
            if (_directPurchaseHd.TransType == "CS")
                this.PayTypeDropDownList.Enabled = true;
            else
                this.PayTypeDropDownList.Enabled = false;
            this.DateTextBox.Text = DateFormMapper.GetValue(_directPurchaseHd.TransDate);
            this.SuppNmbrTextBox.Text = _directPurchaseHd.SuppCode;
            this.SupplierNameTextBox.Text = _suppBL.GetSuppNameByCode(_directPurchaseHd.SuppCode);
            this.Remark.Text = _directPurchaseHd.Remark;
            this.CurrTextBox.Text = _directPurchaseHd.CurrCode;
            this.ForexRateTextBox.Text = _directPurchaseHd.ForexRate.ToString("###0.##");
            this.discount.Text = _directPurchaseHd.Disc.ToString("###0.##");
            this.BaseForexTextBox.Text = _directPurchaseHd.BaseForex.ToString("###0.##");
            this.discountValue.Text = _directPurchaseHd.DiscForex.ToString("###0.##");
            this.PPNAmountTextBox.Text = _directPurchaseHd.PPNForex.ToString("###0.##");
            this.PPNTextBox.Text = _directPurchaseHd.PPN.ToString("###0.##");
            this.PPHAmountTextBox.Text = _directPurchaseHd.PPhForex.ToString("###0.##");
            this.PPHTextBox.Text = _directPurchaseHd.PPh.ToString("###0.##");
            this.TotalAmountTextBox.Text = _directPurchaseHd.TotalForex.ToString("###0.##");
            this.ShowPayType();
            this.PayTypeDropDownList.SelectedValue = _directPurchaseHd.PayCode;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {

            bool _in = false;

            if (this.TransTypeDDL.SelectedValue == "CS")
            {
                if (this.PayTypeDropDownList.SelectedValue != "null")
                {
                    _in = true;
                }
            }
            else
            {
                _in = true;
            }

            if (_in)
            {
                PRCTrDirectPurchaseHd _directPurchaseHd = this._DirectPurchaseBL.GetSingleDirectPurchaseHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _directPurchaseHd.TransDate = DateFormMapper.GetValue(this.DateTextBox.Text);
                _directPurchaseHd.SuppCode = this.SuppNmbrTextBox.Text;
                _directPurchaseHd.Remark = this.Remark.Text;
                _directPurchaseHd.TransType = this.TransTypeDDL.SelectedValue;
                if (this.TransTypeDDL.SelectedValue == "CS")
                    _directPurchaseHd.PayCode = this.PayTypeDropDownList.SelectedValue;
                else
                    _directPurchaseHd.PayCode = "";
                //_directPurchaseHd.PayCode = this.PayTypeDropDownList.SelectedValue;
                _directPurchaseHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
                _directPurchaseHd.Disc = Convert.ToDecimal(this.discount.Text);
                _directPurchaseHd.DiscForex = Convert.ToDecimal(this.discountValue.Text);
                _directPurchaseHd.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
                _directPurchaseHd.PPNForex = Convert.ToDecimal(this.PPNAmountTextBox.Text);
                _directPurchaseHd.PPh = Convert.ToDecimal(this.PPHTextBox.Text);
                _directPurchaseHd.PPhForex = Convert.ToDecimal(this.PPHAmountTextBox.Text);
                _directPurchaseHd.TotalForex = Convert.ToDecimal(this.TotalAmountTextBox.Text);
                _directPurchaseHd.EditBy = HttpContext.Current.User.Identity.Name;
                _directPurchaseHd.EditDate = DateTime.Now;

                bool _result = this._DirectPurchaseBL.EditDirectPurchaseHd(_directPurchaseHd);

                if (_result == true)
                {
                    Response.Redirect(this._homePage);
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "Your Failed Edit Data";
                }
            }
            else
            {
                this.WarningLabel.Text = "Please choice Payment Type";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _in = false;

            if (this.TransTypeDDL.SelectedValue == "CS")
            {
                if (this.PayTypeDropDownList.SelectedValue != "null")
                {
                    _in = true;
                }
            }
            else
            {
                _in = true;
            }

            if (_in)
            {

                PRCTrDirectPurchaseHd _directPurchaseHd = this._DirectPurchaseBL.GetSingleDirectPurchaseHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _directPurchaseHd.TransDate = DateFormMapper.GetValue(this.DateTextBox.Text);
                _directPurchaseHd.SuppCode = this.SuppNmbrTextBox.Text;
                _directPurchaseHd.Remark = this.Remark.Text;
                _directPurchaseHd.TransType = this.TransTypeDDL.SelectedValue;
                if (this.TransTypeDDL.SelectedValue == "CS")
                    _directPurchaseHd.PayCode = this.PayTypeDropDownList.SelectedValue;
                else
                    _directPurchaseHd.PayCode = "";
                //_directPurchaseHd.PayCode = this.PayTypeDropDownList.SelectedValue;
                _directPurchaseHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
                _directPurchaseHd.Disc = Convert.ToDecimal(this.discount.Text);
                _directPurchaseHd.DiscForex = Convert.ToDecimal(this.discountValue.Text);
                _directPurchaseHd.PPN = Convert.ToDecimal(this.PPNTextBox.Text);
                _directPurchaseHd.PPNForex = Convert.ToDecimal(this.PPNAmountTextBox.Text);
                _directPurchaseHd.PPh = Convert.ToDecimal(this.PPHTextBox.Text);
                _directPurchaseHd.PPhForex = Convert.ToDecimal(this.PPHAmountTextBox.Text);
                _directPurchaseHd.TotalForex = Convert.ToDecimal(this.TotalAmountTextBox.Text);
                _directPurchaseHd.EditBy = HttpContext.Current.User.Identity.Name;
                _directPurchaseHd.EditDate = DateTime.Now;

                bool _result = this._DirectPurchaseBL.EditDirectPurchaseHd(_directPurchaseHd);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "Your Failed Edit Data";
                }
            }
            else
            {
                this.WarningLabel.Text = "Please choice Payment Type";
            }
        }

        protected void TransTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TransTypeDDL.SelectedValue == "CS")
            {
                this.PayTypeDropDownList.Enabled = true;
            }
            else
            {
                this.PayTypeDropDownList.Enabled = false;
            }
        }
    }
}