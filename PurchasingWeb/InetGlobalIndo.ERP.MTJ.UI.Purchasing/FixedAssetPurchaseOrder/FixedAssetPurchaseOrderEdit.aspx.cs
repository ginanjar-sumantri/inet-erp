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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.FixedAssetPurchaseOrder
{
    public partial class FixedAssetPurchaseOrderEdit : FixedAssetPurchaseOrderBase
    {
        private SupplierBL _supplierBL = new SupplierBL();
        private TermBL _termBL = new TermBL();
        private ShipmentBL _shipmentBL = new ShipmentBL();
        private DeliveryBL _deliverBL = new DeliveryBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private FixedAssetPurchaseOrderBL _faPOBL = new FixedAssetPurchaseOrderBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.DeliveryDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.DeliveryDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.btnSearchSupplier.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findSupplier&configCode=supplier','_popSearch','width=500,height=300,toolbar=0,location=0,status=0,scrollbars=1')";

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

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
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
            this.DPPercentTextBox.Attributes.Add("OnBlur", "CalculateDPPercent(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + /*this.PPHPercentTextBox.ClientID"" + ", " + /*this.PPHForexTextBox.ClientID"" + ", " +*/ this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DPForexTextBox.Attributes.Add("OnBlur", "CalculateDPForex(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + /*this.PPHPercentTextBox.ClientID"" + ", " + /*this.PPHForexTextBox.ClientID"" + ", " + */this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.DiscTextBox.Attributes.Add("OnBlur", "CalculateDiscountPercent(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + /*this.PPHPercentTextBox.ClientID"" + ", " + /*this.PPHForexTextBox.ClientID"" + ", " +*/ this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscForexTextBox.Attributes.Add("OnBlur", "CalculateDiscountForex(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + /*this.PPHPercentTextBox.ClientID"" + ", " + /*this.PPHForexTextBox.ClientID"" + ", " +*/ this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            //this.PPHPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + this.PPHPercentTextBox.ClientID + ", " + this.PPHForexTextBox.ClientID + ", " + this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.DPPercentTextBox.ClientID + ", " + this.DPForexTextBox.ClientID + ", " + this.BaseForexTextBox.ClientID + ", " + this.DiscTextBox.ClientID + ", " + this.DiscForexTextBox.ClientID + ", " + /*this.PPHPercentTextBox.ClientID"" + ", " + /*this.PPHForexTextBox.ClientID"" + ", " +*/ this.PPNPercentTextBox.ClientID + ", " + this.PPNForexTextBox.ClientID + ", " + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.ShippingCurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ShippingCurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.ShippingForexTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.ShippingForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");

        }


        protected void SetAttribute()
        {
            //this.ViewDetailButton.Attributes.Add("OnClick", "return AskYouFirstToSave(" + this.CheckHidden.ClientID + ");");

            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.DeliveryDateTextBox.Attributes.Add("ReadOnly", "True");
            //this.DPForexTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrTextBox.Attributes.Add("ReadOnly", "True");
            this.BaseForexTextBox.Attributes.Add("ReadOnly", "True");
            //this.PPHForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
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

        protected void ShowData()
        {
            PRCFAPOHd _prcFAPOHd = this._faPOBL.GetSinglePRCFAPOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_prcFAPOHd.CurrCode);
            byte _decimalPlace2 = this._currencyBL.GetDecimalPlace(_prcFAPOHd.ShippingCurr);

            this.TransNoTextBox.Text = _prcFAPOHd.TransNmbr;
            this.FileNmbrTextBox.Text = _prcFAPOHd.FileNmbr;
            //this.RevisiLabel.Text = _prcPOHd.Revisi.ToString();
            this.DateTextBox.Text = DateFormMapper.GetValue(_prcFAPOHd.TransDate);
            this.SupplierTextBox.Text = _prcFAPOHd.SuppCode;
            this.SupplierLabel.Text = _supplierBL.GetSuppNameByCode(_prcFAPOHd.SuppCode);
            this.SubjectTextBox.Text = _prcFAPOHd.Subject;
            this.AttnTextBox.Text = _prcFAPOHd.Attn;
            this.SupplierPONoTextBox.Text = _prcFAPOHd.SuppPONo;
            this.TermDropDownList.SelectedValue = _prcFAPOHd.Term;
            this.ShipmentDropDownList.SelectedValue = _prcFAPOHd.ShipmentType;
            this.ShipmentNameTextBox.Text = _prcFAPOHd.ShipmentName;
            this.DeliveryDropDownList.SelectedValue = _prcFAPOHd.Delivery;

            if (_prcFAPOHd.DeliveryDate != null)
            {
                this.DeliveryDateTextBox.Text = DateFormMapper.GetValue(_prcFAPOHd.DeliveryDate);
            }
            else
            {
                this.DeliveryDateTextBox.Text = "";
            }

            this.CurrCodeDropDownList.SelectedValue = _prcFAPOHd.CurrCode;

            string _currCodeHome = _currencyBL.GetCurrDefault();

            if (this.CurrCodeDropDownList.SelectedValue == _currCodeHome)
            {
                this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                this.ForexRateTextBox.Text = "1";
            }
            else
            {
                this.ForexRateTextBox.Attributes.Remove("ReadOnly");
                this.ForexRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
                this.ForexRateTextBox.Text = (_prcFAPOHd.ForexRate == 0) ? "1" : _prcFAPOHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }

            this.DPPercentTextBox.Text = (_prcFAPOHd.DP == 0) ? "0" : _prcFAPOHd.DP.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DPForexTextBox.Text = (_prcFAPOHd.DPForex == 0) ? "0" : _prcFAPOHd.DPForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.CurrTextBox.Text = _prcFAPOHd.CurrCode;
            this.BaseForexTextBox.Text = (_prcFAPOHd.BaseForex == 0) ? "0" : _prcFAPOHd.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DiscTextBox.Text = (_prcFAPOHd.Disc == 0) ? "0" : _prcFAPOHd.Disc.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DiscForexTextBox.Text = (_prcFAPOHd.DiscForex == 0) ? "0" : _prcFAPOHd.DiscForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            //this.PPHPercentTextBox.Text = (_prcFAPOHd.PPH == 0) ? "0" : _prcFAPOHd.PPH.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            //this.PPHForexTextBox.Text = (_prcFAPOHd.PPHForex == 0) ? "0" : _prcFAPOHd.PPHForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNPercentTextBox.Text = (_prcFAPOHd.PPN == 0) ? "0" : _prcFAPOHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_prcFAPOHd.PPNForex == 0) ? "0" : _prcFAPOHd.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_prcFAPOHd.TotalForex == 0) ? "0" : _prcFAPOHd.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            if (_prcFAPOHd.ShippingCurr != null)
            {
                this.ShippingCurrCodeDropDownList.SelectedValue = _prcFAPOHd.ShippingCurr;
            }
            else
            {
                this.ShippingCurrCodeDropDownList.SelectedValue = "null";
            }

            if (this.ShippingCurrCodeDropDownList.SelectedValue == _currCodeHome)
            {
                this.ShippingCurrRateTextBox.Attributes.Add("ReadOnly", "True");
                this.ShippingCurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            }
            else
            {
                this.ShippingCurrRateTextBox.Attributes.Remove("ReadOnly");
                this.ShippingCurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            }

            decimal _tempShippingForex = Convert.ToDecimal((_prcFAPOHd.ShippingForex == null) ? 0 : _prcFAPOHd.ShippingForex);
            decimal _tempShippingRate = Convert.ToDecimal((_prcFAPOHd.ShippingRate == null) ? 0 : _prcFAPOHd.ShippingRate);

            if (_tempShippingForex > 0)
            {
                this.ShippingCurrCodeDropDownList.Enabled = true;
                this.ShippingCurrRateTextBox.Text = (_tempShippingRate == 0) ? "0" : _tempShippingRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));
            }
            else
            {
                this.ShippingCurrCodeDropDownList.Enabled = false;
                this.ShippingCurrRateTextBox.Text = "0";
            }


            this.ShippingForexTextBox.Text = (_tempShippingForex == 0) ? "0" : _tempShippingForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));

            this.RemarkTextBox.Text = _prcFAPOHd.Remark;

        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            PRCFAPOHd _prcFAPOHd = this._faPOBL.GetSinglePRCFAPOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _prcFAPOHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _prcFAPOHd.SuppCode = this.SupplierTextBox.Text;
            _prcFAPOHd.Attn = this.AttnTextBox.Text;
            _prcFAPOHd.SuppPONo = this.SupplierPONoTextBox.Text;
            _prcFAPOHd.Subject = this.SubjectTextBox.Text;
            if (Convert.ToDecimal(this.DPPercentTextBox.Text) > 0)
            {
                _prcFAPOHd.FgDP = YesNoDataMapper.GetYesNo(YesNo.Yes);
            }
            else
            {
                _prcFAPOHd.FgDP = YesNoDataMapper.GetYesNo(YesNo.No);
            }

            _prcFAPOHd.DP = Convert.ToDecimal(this.DPPercentTextBox.Text);
            _prcFAPOHd.DPForex = Convert.ToDecimal(this.DPForexTextBox.Text);
            _prcFAPOHd.Term = this.TermDropDownList.SelectedValue;
            _prcFAPOHd.ShipmentType = this.ShipmentDropDownList.SelectedValue;
            _prcFAPOHd.ShipmentName = this.ShipmentNameTextBox.Text;
            _prcFAPOHd.Delivery = this.DeliveryDropDownList.SelectedValue;

            if (this.DeliveryDateTextBox.Text != "")
            {
                _prcFAPOHd.DeliveryDate = DateFormMapper.GetValue(this.DeliveryDateTextBox.Text);
            }
            else
            {
                _prcFAPOHd.DeliveryDate = null;
            }

            _prcFAPOHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _prcFAPOHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _prcFAPOHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
            _prcFAPOHd.Disc = Convert.ToDecimal(this.DiscTextBox.Text);
            _prcFAPOHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
            _prcFAPOHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
            _prcFAPOHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
            //_prcFAPOHd.PPH = Convert.ToDecimal(this.PPHPercentTextBox.Text);
            //_prcFAPOHd.PPHForex = Convert.ToDecimal(this.PPHForexTextBox.Text);
            _prcFAPOHd.PPH = 0;
            _prcFAPOHd.PPHForex = 0;
            _prcFAPOHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);

            if (this.ShippingCurrCodeDropDownList.SelectedValue != "null")
            {
                _prcFAPOHd.ShippingCurr = this.ShippingCurrCodeDropDownList.SelectedValue;
            }
            else
            {
                _prcFAPOHd.ShippingCurr = null;
            }

            if (Convert.ToDecimal(this.ShippingCurrRateTextBox.Text) > 0)
            {
                _prcFAPOHd.ShippingRate = Convert.ToDecimal(this.ShippingCurrRateTextBox.Text);
            }
            else
            {
                _prcFAPOHd.ShippingRate = null;
            }

            if (Convert.ToDecimal(this.ShippingForexTextBox.Text) > 0)
            {
                _prcFAPOHd.ShippingForex = Convert.ToDecimal(this.ShippingForexTextBox.Text);
            }
            else
            {
                _prcFAPOHd.ShippingForex = null;
            }

            _prcFAPOHd.Remark = this.RemarkTextBox.Text;

            _prcFAPOHd.EditBy = HttpContext.Current.User.Identity.Name;
            _prcFAPOHd.EditDate = DateTime.Now;

            _prcFAPOHd.FgActive = YesNoDataMapper.GetYesNo(YesNo.No);
            _prcFAPOHd.POType = AppModule.GetValue(TransactionType.FixedAssetPurchaseOrder);
            _prcFAPOHd.DoneInvoice = YesNoDataMapper.GetYesNo(YesNo.No);


            bool _result = this._faPOBL.EditPRCFAPOHd(_prcFAPOHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
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
            if (this.Page.IsValid == true)
            {
                PRCFAPOHd _prcFAPOHd = this._faPOBL.GetSinglePRCFAPOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _prcFAPOHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                _prcFAPOHd.SuppCode = this.SupplierTextBox.Text;
                _prcFAPOHd.Attn = this.AttnTextBox.Text;
                _prcFAPOHd.SuppPONo = this.SupplierPONoTextBox.Text;
                _prcFAPOHd.Subject = this.SubjectTextBox.Text;

                if (Convert.ToDecimal(this.DPPercentTextBox.Text) > 0)
                {
                    _prcFAPOHd.FgDP = YesNoDataMapper.GetYesNo(YesNo.Yes);
                }
                else
                {
                    _prcFAPOHd.FgDP = YesNoDataMapper.GetYesNo(YesNo.No);
                }

                _prcFAPOHd.DP = Convert.ToDecimal(this.DPPercentTextBox.Text);
                _prcFAPOHd.DPForex = Convert.ToDecimal(this.DPForexTextBox.Text);
                _prcFAPOHd.Term = this.TermDropDownList.SelectedValue;
                _prcFAPOHd.ShipmentType = this.ShipmentDropDownList.SelectedValue;
                _prcFAPOHd.ShipmentName = this.ShipmentNameTextBox.Text;
                _prcFAPOHd.Delivery = this.DeliveryDropDownList.SelectedValue;

                if (this.DeliveryDateTextBox.Text != "")
                {
                    _prcFAPOHd.DeliveryDate = DateFormMapper.GetValue(this.DeliveryDateTextBox.Text);
                }
                else
                {
                    _prcFAPOHd.DeliveryDate = null;
                }

                _prcFAPOHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
                _prcFAPOHd.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
                _prcFAPOHd.BaseForex = Convert.ToDecimal(this.BaseForexTextBox.Text);
                _prcFAPOHd.Disc = Convert.ToDecimal(this.DiscTextBox.Text);
                _prcFAPOHd.DiscForex = Convert.ToDecimal(this.DiscForexTextBox.Text);
                _prcFAPOHd.PPN = Convert.ToDecimal(this.PPNPercentTextBox.Text);
                _prcFAPOHd.PPNForex = Convert.ToDecimal(this.PPNForexTextBox.Text);
                //_prcFAPOHd.PPH = Convert.ToDecimal(this.PPHPercentTextBox.Text);
                //_prcFAPOHd.PPHForex = Convert.ToDecimal(this.PPHForexTextBox.Text);
                _prcFAPOHd.PPH = 0;
                _prcFAPOHd.PPHForex = 0;
                _prcFAPOHd.TotalForex = Convert.ToDecimal(this.TotalForexTextBox.Text);

                if (this.ShippingCurrCodeDropDownList.SelectedValue != "null")
                {
                    _prcFAPOHd.ShippingCurr = this.ShippingCurrCodeDropDownList.SelectedValue;
                }
                else
                {
                    _prcFAPOHd.ShippingCurr = null;
                }

                if (Convert.ToDecimal(this.ShippingCurrRateTextBox.Text) > 0)
                {
                    _prcFAPOHd.ShippingRate = Convert.ToDecimal(this.ShippingCurrRateTextBox.Text);
                }
                else
                {
                    _prcFAPOHd.ShippingRate = null;
                }

                if (Convert.ToDecimal(this.ShippingForexTextBox.Text) > 0)
                {
                    _prcFAPOHd.ShippingForex = Convert.ToDecimal(this.ShippingForexTextBox.Text);
                }
                else
                {
                    _prcFAPOHd.ShippingForex = null;
                }

                _prcFAPOHd.Remark = this.RemarkTextBox.Text;

                _prcFAPOHd.EditBy = HttpContext.Current.User.Identity.Name;
                _prcFAPOHd.EditDate = DateTime.Now;

                _prcFAPOHd.FgActive = YesNoDataMapper.GetYesNo(YesNo.No);
                _prcFAPOHd.POType = AppModule.GetValue(TransactionType.FixedAssetPurchaseOrder);
                _prcFAPOHd.DoneInvoice = YesNoDataMapper.GetYesNo(YesNo.No);

                bool _result = this._faPOBL.EditPRCFAPOHd(_prcFAPOHd);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Edit Data";
                }
            }
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
            //this.PPHPercentTextBox.Text = "0";
            //this.PPHForexTextBox.Text = "0";
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