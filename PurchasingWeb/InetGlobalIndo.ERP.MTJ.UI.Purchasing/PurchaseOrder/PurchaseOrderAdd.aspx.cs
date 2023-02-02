using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseOrder
{
    public partial class PurchaseOrderAdd : PurchaseOrderBase
    {
        private SupplierBL _supplierBL = new SupplierBL();
        private TermBL _termBL = new TermBL();
        private ShipmentBL _shipmentBL = new ShipmentBL();
        private DeliveryBL _deliverBL = new DeliveryBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
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

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DeliveryDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.DeliveryDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.btnSearchSupplier.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findSupplier&configCode=supplier','_popSearch','width=400,height=550,toolbar=0,location=0,status=0,scrollbars=1')";

                String spawnJS = "<script language='JavaScript'>\n";

                ////////////////////DECLARE FUNCTION FOR CATCHING SUPPLIER SEARCH
                spawnJS += "function findSupplier(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.SupplierTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.SupplierLabel.ClientID + "').innerHTML = dataArray[1];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                //this.ShowSupplier();
                //this.ShowAttn();
                this.ShowTerm();
                this.ShowShipment();
                this.ShowDelivery();
                this.ShowCurrency();
                this.ShowShippingCurrency();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.ForexRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.ShippingCurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ShippingCurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");

            //this.DPPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPHPercentTextBox.ClientID + ", " + this.PPHForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + ");");
            this.DPPercentTextBox.Attributes.Add("OnBlur", "CalculateDPPercent(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPHPercentTextBox.ClientID + ", " + this.PPHForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DPForexTextBox.Attributes.Add("OnBlur", "CalculateDPForex(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPHPercentTextBox.ClientID + ", " + this.PPHForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.DiscTextBox.Attributes.Add("OnBlur", "CalculateDiscountPercent(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPHPercentTextBox.ClientID + ", " + this.PPHForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "CalculateDiscountForex(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPHPercentTextBox.ClientID + ", " + this.PPHForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            //this.PPHPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPHPercentTextBox.ClientID + ", " + this.PPHForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPHPercentTextBox.ClientID + ", " + this.PPHForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.ShippingCurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ShippingCurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.ShippingForexTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ShippingForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");

        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.DeliveryDateTextBox.Attributes.Add("ReadOnly", "True");
            //this.DPForexTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPHForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");

            this.ShippingCurrRateTextBox.Attributes.Add("ReadOnly", "true");
            this.ShippingCurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
            this.ForexRateTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");

            this.SetAttributeRate();
        }

        protected void ClearData()
        {
            this.ClearLabel();

            this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            //this.SupplierDropDownList.SelectedValue = "null";
            this.SupplierLabel.Text = " ";
            this.SupplierTextBox.Text = "";
            this.AttnTextBox.Text = "";
            this.SupplierPONoTextBox.Text = "";
            this.TermDropDownList.SelectedValue = "null";
            this.ShipmentDropDownList.SelectedValue = "null";
            this.ShipmentNameTextBox.Text = "";
            this.DeliveryDropDownList.SelectedValue = "null";
            this.DeliveryDateTextBox.Text = "";
            this.CurrCodeDropDownList.SelectedValue = "null";
            this.ForexRateTextBox.Text = "";
            this.DPPercentTextBox.Text = "0";
            this.DPForexTextBox.Text = "0";
            this.CurrTextBox.Text = "";
            this.BaseForexTextBox.Text = "0";
            this.DiscTextBox.Text = "0";
            this.DiscForexTextBox.Text = "0";
            this.PPHPercentTextBox.Text = "0";
            this.PPHForexTextBox.Text = "0";
            this.PPNPercentTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
            this.SubjectTextBox.Text = "";

            this.ShippingCurrCodeDropDownList.SelectedValue = "null";
            this.ShippingCurrCodeDropDownList.Enabled = false;
            this.ShippingCurrRateTextBox.Text = "0";
            this.ShippingForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";
        }

        //protected void ShowSupplier()
        //{
        //    this.SupplierDropDownList.Items.Clear();
        //    this.SupplierDropDownList.DataTextField = "SuppName";
        //    this.SupplierDropDownList.DataValueField = "SuppCode";
        //    this.SupplierDropDownList.DataSource = this._supplierBL.GetListDDLSupp();
        //    this.SupplierDropDownList.DataBind();
        //    this.SupplierDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //protected void ShowAttn()
        //{
        //    this.AttnDropDownList.Items.Clear();
        //    this.AttnDropDownList.DataTextField = "SuppName";
        //    this.AttnDropDownList.DataValueField = "SuppCode";
        //    this.AttnDropDownList.DataSource = this._supplierBL.GetListDDLSupp();
        //    this.AttnDropDownList.DataBind();
        //    this.AttnDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void ShowTerm()
        {
            this.TermDropDownList.Items.Clear();
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowShipment()
        {
            this.ShipmentDropDownList.Items.Clear();
            this.ShipmentDropDownList.DataTextField = "ShipmentName";
            this.ShipmentDropDownList.DataValueField = "ShipmentCode";
            this.ShipmentDropDownList.DataSource = this._shipmentBL.GetListDDLShipment();
            this.ShipmentDropDownList.DataBind();
            this.ShipmentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowDelivery()
        {
            this.DeliveryDropDownList.Items.Clear();
            this.DeliveryDropDownList.DataTextField = "DeliveryName";
            this.DeliveryDropDownList.DataValueField = "DeliveryCode";
            this.DeliveryDropDownList.DataSource = this._deliverBL.GetListDeliveryForDDL();
            this.DeliveryDropDownList.DataBind();
            this.DeliveryDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCurrency()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowShippingCurrency()
        {
            this.ShippingCurrCodeDropDownList.Items.Clear();
            this.ShippingCurrCodeDropDownList.DataTextField = "CurrCode";
            this.ShippingCurrCodeDropDownList.DataValueField = "CurrCode";
            this.ShippingCurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.ShippingCurrCodeDropDownList.DataBind();
            this.ShippingCurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearDataNumeric();

            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                this.SetCurrRate();
            }
            else
            {
                this.ForexRateTextBox.Text = "0";
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        protected void SupplierTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.SupplierTextBox.Text != "")
            {
                string _currCode = this._supplierBL.GetCurr(this.SupplierTextBox.Text);
                string _termCode = this._supplierBL.GetTerm(this.SupplierTextBox.Text);
                decimal _currRate = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue);
                string _attn = this._supplierBL.GetSuppContact(this.SupplierTextBox.Text);

                this.AttnTextBox.Text = _attn;

                if (_currCode != "")
                {
                    this.CurrCodeDropDownList.SelectedValue = _currCode;
                    this.ForexRateTextBox.Text = (_currRate == 0) ? "1" : _currRate.ToString("#,###.##");
                }
                if (_termCode != "")
                {
                    this.TermDropDownList.SelectedValue = _termCode;
                }

                this.SupplierLabel.Text = _supplierBL.GetSuppNameByCode(this.SupplierTextBox.Text);
                this.SetCurrRate();
            }
            else
            {
                this.SupplierLabel.Text = " ";
                this.CurrTextBox.Text = "";
                this.CurrCodeDropDownList.SelectedValue = "null";
                this.TermDropDownList.SelectedValue = "null";

                this.ForexRateTextBox.Text = "0";
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        protected void ShippingForexTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(this.ShippingForexTextBox.Text) > 0)
            {
                this.ShippingCurrCodeDropDownList.Enabled = true;
            }
            else
            {
                this.ShippingCurrCodeDropDownList.Enabled = false;
                this.ShippingCurrCodeDropDownList.SelectedValue = "null";

                this.ShippingCurrRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ShippingCurrRateTextBox.Attributes.Add("style", "background-color:#cccccc");
                this.ShippingCurrRateTextBox.Text = "0";
            }
        }

        protected void ShippingCurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DecimalPlaceHiddenField2.Value = "";

            if (ShippingCurrCodeDropDownList.SelectedValue != "null")
            {
                string _currCodeHome = _currencyBL.GetCurrDefault();
                decimal _currRate = this._currencyRateBL.GetSingleLatestCurrRate(this.ShippingCurrCodeDropDownList.SelectedValue);

                byte _decimalPlace2 = this._currencyBL.GetDecimalPlace(this.ShippingCurrCodeDropDownList.SelectedValue);
                this.DecimalPlaceHiddenField2.Value = CurrencyDataMapper.GetDecimal(_decimalPlace2);

                if (this.ShippingCurrCodeDropDownList.SelectedValue == _currCodeHome)
                {
                    this.ShippingCurrRateTextBox.Attributes.Add("ReadOnly", "True");
                    this.ShippingCurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                    this.ShippingCurrRateTextBox.Text = "1";
                }
                else
                {
                    this.ShippingCurrRateTextBox.Attributes.Remove("ReadOnly");
                    this.ShippingCurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                    this.ShippingCurrRateTextBox.Text = (_currRate == 0) ? "1" : _currRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));
                }
            }
            else
            {
                this.ShippingCurrRateTextBox.Text = "";
                this.ShippingCurrRateTextBox.Attributes.Remove("ReadOnly");
                this.ShippingCurrRateTextBox.Attributes.Add("style", "background-color:#ffffff");
            }
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            PRCPOHd _prcPOHd = new PRCPOHd();

            _prcPOHd.Revisi = 0;
            _prcPOHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _prcPOHd.Status = PurchaseOrderDataMapper.GetStatus(TransStatus.OnHold);
            _prcPOHd.SuppCode = this.SupplierTextBox.Text;
            _prcPOHd.Attn = this.AttnTextBox.Text;
            _prcPOHd.SuppPONo = this.SupplierPONoTextBox.Text;

            if (Convert.ToDecimal(this.DPPercentTextBox.Text) > 0)
            {
                _prcPOHd.FgDP = YesNoDataMapper.GetYesNo(YesNo.Yes);
            }
            else
            {
                _prcPOHd.FgDP = YesNoDataMapper.GetYesNo(YesNo.No);
            }

            _prcPOHd.DP = Convert.ToDecimal(this.DPPercentTextBox.Text);
            _prcPOHd.DPForex = Convert.ToDecimal(this.DPForexTextBox.Text);
            _prcPOHd.Term = this.TermDropDownList.SelectedValue;
            _prcPOHd.ShipmentType = this.ShipmentDropDownList.SelectedValue;
            _prcPOHd.ShipmentName = this.ShipmentNameTextBox.Text;
            _prcPOHd.Delivery = this.DeliveryDropDownList.SelectedValue;

            if (this.DeliveryDateTextBox.Text != "")
            {
                _prcPOHd.DeliveryDate = DateFormMapper.GetValue(this.DeliveryDateTextBox.Text);
            }
            else
            {
                _prcPOHd.DeliveryDate = null;
            }

            _prcPOHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _prcPOHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _prcPOHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _prcPOHd.Disc = Convert.ToDecimal(this.DiscTextBox.Text);
            _prcPOHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
            _prcPOHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _prcPOHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            _prcPOHd.PPH = Convert.ToDecimal(this.PPHPercentTextBox.Text);
            _prcPOHd.PPHForex = Convert.ToDecimal(this.PPHForexTextBox.Text);
            _prcPOHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);
            _prcPOHd.Subject = this.SubjectTextBox.Text;

            if (this.ShippingCurrCodeDropDownList.SelectedValue != "null")
            {
                _prcPOHd.ShippingCurr = this.ShippingCurrCodeDropDownList.SelectedValue;
            }
            else
            {
                _prcPOHd.ShippingCurr = null;
            }

            if (Convert.ToDecimal(this.ShippingCurrRateTextBox.Text) > 0)
            {
                _prcPOHd.ShippingRate = Convert.ToDecimal(this.ShippingCurrRateTextBox.Text);
            }
            else
            {
                _prcPOHd.ShippingRate = null;
            }

            if (Convert.ToDecimal(this.ShippingForexTextBox.Text) > 0)
            {
                _prcPOHd.ShippingForex = Convert.ToDecimal(this.ShippingForexTextBox.Text);
            }
            else
            {
                _prcPOHd.ShippingForex = null;
            }

            _prcPOHd.Remark = this.RemarkTextBox.Text;

            _prcPOHd.CreatedBy = HttpContext.Current.User.Identity.Name;
            _prcPOHd.CreatedDate = DateTime.Now;
            _prcPOHd.EditBy = HttpContext.Current.User.Identity.Name;
            _prcPOHd.EditDate = DateTime.Now;

            _prcPOHd.FgActive = YesNoDataMapper.GetYesNo(YesNo.No);
            _prcPOHd.POType = AppModule.GetValue(TransactionType.PurchaseOrder);
            _prcPOHd.DoneInvoice = YesNoDataMapper.GetYesNo(YesNo.No);


            string _result = this._purchaseOrderBL.AddPRCPOHd(_prcPOHd);

            if (_result != "")
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0", ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        private void ClearDataNumeric()
        {
            this.ForexRateTextBox.Text = "";
            this.DPPercentTextBox.Text = "0";
            this.DPForexTextBox.Text = "0";
            this.CurrTextBox.Text = "";
            this.BaseForexTextBox.Text = "0";
            this.DiscTextBox.Text = "0";
            this.DiscForexTextBox.Text = "0";
            this.PPHPercentTextBox.Text = "0";
            this.PPHForexTextBox.Text = "0";
            this.PPNPercentTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";

            this.DecimalPlaceHiddenField.Value = "";
        }

        private void DisableRate()
        {
            this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
            this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.ForexRateTextBox.Text = "1";
        }

        private void EnableRate()
        {
            this.ForexRateTextBox.Attributes.Remove("ReadOnly");
            this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        }

        private void SetCurrRate()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
            this.ForexRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.CurrTextBox.Text = this.CurrCodeDropDownList.SelectedValue;
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (this.CurrCodeDropDownList.SelectedValue.Trim().ToLower() == _currencyBL.GetCurrDefault().Trim().ToLower())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
            }
        }
    }
}