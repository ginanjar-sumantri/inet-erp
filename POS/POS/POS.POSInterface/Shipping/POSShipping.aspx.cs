using System;
using System.Linq;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Threading;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace POS.POSInterface.Shipping
{
    public partial class POSShipping : POSShippingBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private POSReasonBL _reasonBL = new POSReasonBL();
        private POSConfigurationBL _pOSConfigurationBL = new POSConfigurationBL();
        private CountryBL _countryBL = new CountryBL();
        private CityBL _cityBL = new CityBL();
        private VendorBL _vendorBL = new VendorBL();
        //private ShippingBL _shippingBL = new ShippingBL();
        private TransportationRSBL _transportationRSBL = new TransportationRSBL();
        private OtherSurchargeBL _otherSurchargeBL = new OtherSurchargeBL();
        private POSShippingBL _posShippingBL = new POSShippingBL();
        private MemberBL _memberBL = new MemberBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();

        protected string _code = "code";
        protected string _itemno = "itemno";
        protected NameValueCollectionExtractor _nvcExtractor;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack == true)
                {
                    this.ChangeVisiblePanel(0);
                    this.ClearLabel();
                    this.ShowCountry();
                    this.ShowCity();
                    //this.ShowVendor();
                    this.VendorDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
                    this.ShowTRS();
                    this.ShowOS();
                    this.JavaScript();
                    this.SetAttributes();
                    this.Clear();
                    this.ClearDetail();
                    this.ViewData();
                    this.VisibleValidationSummary(0);
                    this.StatusOpenHiddenField.Value = "Open";
                    this.ProductShapeWithIntPriorityRBL.Visible = false;
                    this.SetButtonPermission();
                    //this.NewMemberButton.OnClientClick = "window.open('../Registration/Registration.aspx?valueCatcher=findProduct&configCode=product','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";
                }
                //join job order
                if (this.TransRefHiddenField.Value != "" & this.StatusOpenHiddenField.Value != "Open")
                {
                    this.ShowDataHd(this.TransRefHiddenField.Value);
                    this.ShowDataDt();
                    this.StatusOpenHiddenField.Value = "Open";
                }
                this.Calculate();
                this.ClearLabel();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ChangeVisiblePanel(Byte _prmValue)
        {
            if (_prmValue == 0)
            {
                this.FormPanel.Visible = true;
                this.ReasonListPanel.Visible = false;
                this.PasswordPanel.Visible = false;
            }
            else if (_prmValue == 1)
            {
                this.FormPanel.Visible = false;
                this.ReasonListPanel.Visible = true;
                this.PasswordPanel.Visible = false;
            }
            else if (_prmValue == 2)
            {
                this.FormPanel.Visible = true;
                this.ReasonListPanel.Visible = false;
                this.PasswordPanel.Visible = true;
            }
        }

        private void SetButtonPermission()
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDCashier, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                this.GotoCashierButton.Visible = false;
                this.CashierAbuButton.Visible = true;
                this.CashierAbuButton.Enabled = false;
            }
            else
            {
                this.GotoCashierButton.Visible = true;
                this.CashierAbuButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void Clear()
        {
            this.ReferenceNoTextBox.Text = "";
            this.MemberIDTextBox.Text = "";
            this.CustNameTextBox.Text = "";
            this.CustPhoneTextBox.Text = "";
            this.SenderNameTextBox.Text = "";
            this.SenderAddressTextBox.Text = "";
            this.SenderTelephoneTextBox.Text = "";
            this.SenderCityDDL.SelectedIndex = 0;
            this.SenderPostalCodeTextBox.Text = "";
            this.DeliverNameTextBox.Text = "";
            this.DeliverAddressTextBox.Text = "";
            this.DeliverTelephoneTextBox.Text = "";
            this.CountryDDL.SelectedIndex = 0;
            this.DeliverCityDDL.SelectedIndex = 0;
            this.DeliverPostalCodeTextBox.Text = "";

            this.TransNoTextBox.Text = "";
            this.DiscForexTextBox.Text = "0";
            this.AmountBaseTextBox.Text = "0";
            this.OtherForexTextBox.Text = "0";
            //this.PPNForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
            this.TransRefHiddenField.Value = "";
            this.MaxLiteral.Text = "";
        }

        protected void ClearDetail()
        {
            this.AirwayBillTextBox.Text = "";
            this.VendorDDL.SelectedIndex = 0;
            if (this.ShippingTypeDDL.SelectedIndex == -1)
                this.ShippingTypeDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.ShippingTypeDDL.SelectedIndex = 0;
            this.ProductShapeRBL.SelectedIndex = 0;
            this.Price1Literal.Text = "0";
            this.Price2Literal.Text = "0";
            this.WeightTextBox.Text = "0";
            this.DiscountTextBox.Text = "0";
            this.PaymentTypeRBL.SelectedIndex = 0;
            this.ShippingTRSDDL.SelectedIndex = 0;
            this.TRSTextBox.Text = "0";
            this.ShippingTRS2DDL.SelectedIndex = 0;
            this.TRS2TextBox.Text = "0";
            this.ShippingTRS3DDL.SelectedIndex = 0;
            this.TRS3TextBox.Text = "0";
            this.ShippingOSDDL.SelectedIndex = 0;
            this.OSTextBox.Text = "0";
            this.ShippingOS2DDL.SelectedIndex = 0;
            this.OS2TextBox.Text = "0";
            this.ShippingOS3DDL.SelectedIndex = 0;
            this.OS3TextBox.Text = "0";
            this.DFSValueLiteral.Text = "0";
            this.DFSValueHiddenField.Value = "0";
            this.OtherFeeTextBox.Text = "0";
            this.SubTotalLiteral.Text = "0";
            this.InsuranceCostTextBox.Text = "0";
            this.PackagingCostTextBox.Text = "0";
            this.TotalLiteral.Text = "0";
            this.NotesTextBox.Text = "";
            this.EstimationTimeLiteral.Text = "0 Days";
            this.UnitCodeHiddenField.Value = "";
            this.ItemNoHiddenField.Value = "";
        }

        protected void ShowCity()
        {
            try
            {
                String _countryCode = this._countryBL.GetCountryCodeWithFgHomeY();

                this.SenderCityDDL.Items.Clear();
                this.SenderCityDDL.DataSource = this._cityBL.GetListCityForDDLByCountry(_countryCode);
                this.SenderCityDDL.DataTextField = "CityName";
                this.SenderCityDDL.DataValueField = "CityCode";
                this.SenderCityDDL.DataBind();
                this.SenderCityDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ShowCityDeliver()
        {
            try
            {
                this.DeliverCityDDL.Items.Clear();
                this.DeliverCityDDL.DataSource = this._cityBL.GetListCityForDDLByCountry(this.CountryDDL.SelectedValue);
                this.DeliverCityDDL.DataTextField = "CityName";
                this.DeliverCityDDL.DataValueField = "CityCode";
                this.DeliverCityDDL.DataBind();
                this.DeliverCityDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ShowCountry()
        {
            try
            {
                this.CountryDDL.Items.Clear();
                this.CountryDDL.DataSource = this._countryBL.GetList();
                this.CountryDDL.DataTextField = "CountryName";
                this.CountryDDL.DataValueField = "CountryCode";
                this.CountryDDL.DataBind();
                this.CountryDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ShowVendor()
        {
            try
            {
                //string _countryFgHome = this._countryBL.GetCountryCodeWithFgHomeY();
                //this.VendorDDL.Items.Clear();
                //if (_countryFgHome.Trim() == this.CountryDDL.SelectedValue.Trim())
                //    this.VendorDDL.DataSource = this._posShippingBL.GetListByCountry(0, 1000, "FgHome", "");
                //else
                //    this.VendorDDL.DataSource = this._posShippingBL.GetListByCountry(0, 1000, "CountryCode", this.CountryDDL.SelectedValue);

                this.VendorDDL.Items.Clear();
                this.VendorDDL.DataSource = this._posShippingBL.GetListByCountryCity(0, 1000, "CountryCode,CityCode", this.CountryDDL.SelectedValue + "," + this.DeliverCityDDL.SelectedValue);
                this.VendorDDL.DataTextField = "VendorName";
                this.VendorDDL.DataValueField = "VendorCode";
                this.VendorDDL.DataBind();
                this.VendorDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ShowShippingType(String _prmVendorCode)
        {
            try
            {
                if (_prmVendorCode != "")
                {
                    this.ShippingTypeDDL.Items.Clear();
                    POSMsShippingVendor _posMsShippingVendor = this._vendorBL.GetSingle(_prmVendorCode);

                    if (_posMsShippingVendor.FgIntPriority == 'Y')
                    {
                        this.ProductShapeRBL.Visible = false;
                        this.ProductShapeWithIntPriorityRBL.Visible = true;
                    }
                    else
                    {
                        this.ProductShapeRBL.Visible = true;
                        this.ProductShapeWithIntPriorityRBL.Visible = false;
                    }

                    //if (_posMsShippingVendor.FgZone != 'Y')
                    //{
                    //    this.ShippingTypeDDL.DataSource = this._posShippingBL.GetShippingType(_prmVendorCode);
                    //    this.ShippingTypeDDL.DataTextField = "ShippingTypeName";
                    //    this.ShippingTypeDDL.DataValueField = "ShippingTypeCode";
                    //}
                    //else
                    //{
                    //    this.ShippingTypeDDL.DataSource = this._posShippingBL.GetShippingZone(_prmVendorCode);
                    //    this.ShippingTypeDDL.DataTextField = "ZoneName";
                    //    this.ShippingTypeDDL.DataValueField = "ZoneCode";
                    //}
                    this.ShippingTypeDDL.DataSource = this._posShippingBL.GetShippingZoneType(_prmVendorCode, this.CountryDDL.SelectedValue, this.DeliverCityDDL.SelectedValue);
                    this.ShippingTypeDDL.DataTextField = "ShippingZonaTypeName";
                    this.ShippingTypeDDL.DataValueField = "ShippingZonaTypeCode";

                    this.ShippingTypeDDL.DataBind();
                    this.ShippingTypeDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
                    this.GetShippingType();
                    this.Calculate();
                }
                else
                {
                    this.ShippingTypeDDL.Items.Clear();
                    this.ShippingTypeDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ShowTRS()
        {
            try
            {
                this.ShippingTRSDDL.Items.Clear();
                this.ShippingTRSDDL.DataSource = this._transportationRSBL.GetList(0, 1000, "", "");
                this.ShippingTRSDDL.DataTextField = "TRSName";
                this.ShippingTRSDDL.DataValueField = "TRSCode";
                this.ShippingTRSDDL.DataBind();
                this.ShippingTRSDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));

                this.ShippingTRS2DDL.Items.Clear();
                this.ShippingTRS2DDL.DataSource = this._transportationRSBL.GetList(0, 1000, "", "");
                this.ShippingTRS2DDL.DataTextField = "TRSName";
                this.ShippingTRS2DDL.DataValueField = "TRSCode";
                this.ShippingTRS2DDL.DataBind();
                this.ShippingTRS2DDL.Items.Insert(0, new ListItem("[Choose One]", "null"));

                this.ShippingTRS3DDL.Items.Clear();
                this.ShippingTRS3DDL.DataSource = this._transportationRSBL.GetList(0, 1000, "", "");
                this.ShippingTRS3DDL.DataTextField = "TRSName";
                this.ShippingTRS3DDL.DataValueField = "TRSCode";
                this.ShippingTRS3DDL.DataBind();
                this.ShippingTRS3DDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ShowOS()
        {
            try
            {
                this.ShippingOSDDL.Items.Clear();
                this.ShippingOSDDL.DataSource = this._otherSurchargeBL.GetList(0, 1000, "", "");
                this.ShippingOSDDL.DataTextField = "OtherSurchargeName";
                this.ShippingOSDDL.DataValueField = "OtherSurchargeCode";
                this.ShippingOSDDL.DataBind();
                this.ShippingOSDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));

                this.ShippingOS2DDL.Items.Clear();
                this.ShippingOS2DDL.DataSource = this._otherSurchargeBL.GetList(0, 1000, "", "");
                this.ShippingOS2DDL.DataTextField = "OtherSurchargeName";
                this.ShippingOS2DDL.DataValueField = "OtherSurchargeCode";
                this.ShippingOS2DDL.DataBind();
                this.ShippingOS2DDL.Items.Insert(0, new ListItem("[Choose One]", "null"));

                this.ShippingOS3DDL.Items.Clear();
                this.ShippingOS3DDL.DataSource = this._otherSurchargeBL.GetList(0, 1000, "", "");
                this.ShippingOS3DDL.DataTextField = "OtherSurchargeName";
                this.ShippingOS3DDL.DataValueField = "OtherSurchargeCode";
                this.ShippingOS3DDL.DataBind();
                this.ShippingOS3DDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void JavaScript()
        {
            String spawnJS = "<script type='text/javascript' language='JavaScript'>\n";
            //DECLARE FUNCTION FOR CATCHING ON HOLD SEARCH
            spawnJS += "function findTransNmbr(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.TransRefHiddenField.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard AirwayBill
            spawnJS += "function AirwayBillKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findAirwayBill&titleinput=Airway Bill&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON AirwayBill
            spawnJS += "function findAirwayBill(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.AirwayBillTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Reference
            spawnJS += "function ReferenceNoKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findReference&titleinput=Reference&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Reference
            spawnJS += "function findReference(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.ReferenceNoTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard CustName
            spawnJS += "function CustNameKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findCustName&titleinput=Name&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON CustName
            spawnJS += "function findCustName(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.CustNameTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard CustPhone
            spawnJS += "function CustPhoneKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findCustPhone&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON CustPhone
            spawnJS += "function findCustPhone(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.CustPhoneTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard SenderName
            spawnJS += "function SenderNameKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findSenderName&titleinput=Sender Name&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON SenderName
            spawnJS += "function findSenderName(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.SenderNameTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard SenderAddress
            spawnJS += "function SenderAddressKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findSenderAddress&titleinput=Sender Address&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON SenderAddress
            spawnJS += "function findSenderAddress(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.SenderAddressTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard SenderTelephone
            spawnJS += "function SenderTelephoneKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findSenderTelephone&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON SenderTelephone
            spawnJS += "function findSenderTelephone(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.SenderTelephoneTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard SenderPostalCode
            spawnJS += "function SenderPostalCodeKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findSenderPostalCode&titleinput=Sender PostalCode&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON SenderPostalCode
            spawnJS += "function findSenderPostalCode(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.SenderPostalCodeTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard DeliverName
            spawnJS += "function DeliverNameKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findDeliverName&titleinput=Deliver Name&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON DeliverName
            spawnJS += "function findDeliverName(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.DeliverNameTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard DeliverAddress
            spawnJS += "function DeliverAddressKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findDeliverAddress&titleinput=Deliver Address&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON DeliverAddress
            spawnJS += "function findDeliverAddress(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.DeliverAddressTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard DeliverTelephone
            spawnJS += "function DeliverTelephoneKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findDeliverTelephone&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON DeliverTelephone
            spawnJS += "function findDeliverTelephone(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.DeliverTelephoneTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard DeliverPostalCode
            spawnJS += "function DeliverPostalCodeKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findDeliverPostalCode&titleinput=Deliver PostalCode&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON DeliverPostalCode
            spawnJS += "function findDeliverPostalCode(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.DeliverPostalCodeTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Weight
            spawnJS += "function WeightKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findWeight&titleinput=Weight&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Weight
            spawnJS += "function findWeight(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.WeightTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Discount
            spawnJS += "function DiscountKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findDiscount&titleinput=Discount&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Discount
            spawnJS += "function findDiscount(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.DiscountTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard TRS
            spawnJS += "function TRSKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findTRS&titleinput=TRS&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON TRS
            spawnJS += "function findTRS(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.TRSTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard TRS2
            spawnJS += "function TRS2KeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findTRS2&titleinput=TRS&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON TRS2
            spawnJS += "function findTRS2(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.TRS2TextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard TRS3
            spawnJS += "function TRS3KeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findTRS3&titleinput=TRS&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON TRS3
            spawnJS += "function findTRS3(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.TRS3TextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard OS
            spawnJS += "function OSKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findOS&titleinput=Other Surcharge&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON OS
            spawnJS += "function findOS(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.OSTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard OS2
            spawnJS += "function OS2KeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findOS2&titleinput=Other Surcharge&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON OS2
            spawnJS += "function findOS2(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.OS2TextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard OS3
            spawnJS += "function OS3KeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findOS3&titleinput=Other Surcharge&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON OS3
            spawnJS += "function findOS3(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.OS3TextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard OtherFee
            spawnJS += "function OtherFeeKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findOtherFee&titleinput=Other Fee&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON OtherFee
            spawnJS += "function findOtherFee(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.OtherFeeTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard InsuranceCost
            spawnJS += "function InsuranceCostKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findInsuranceCost&titleinput=Insurance Cost&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON InsuranceCost
            spawnJS += "function findInsuranceCost(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.InsuranceCostTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard PackagingCost
            spawnJS += "function PackagingCostKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findPackagingCost&titleinput=Packaging Cost&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON PackagingCost
            spawnJS += "function findPackagingCost(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.PackagingCostTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Notes
            spawnJS += "function NotesKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findNotes&titleinput=Notes&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Notes
            spawnJS += "function findNotes(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.NotesTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Password
            spawnJS += "function PasswordKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findPassword&titleinput=Password&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Password
            spawnJS += "function findPassword(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.PasswordTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            spawnJS += "</script>\n";
            this.javascriptReceiver.Text = spawnJS;
        }

        protected void SetAttributes()
        {
            //this.NewMemberButton.OnClientClick = "window.open('../Registration/Registration.aspx','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";
            this.JoinJobOrderButton.OnClientClick = "window.open('../General/JoinJobOrder.aspx?valueCatcher=findTransNmbr&pos=shipping','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";
            this.CheckStatusButton.OnClientClick = "window.open('../General/CheckStatus.aspx?valueCatcher=findTransNmbr&pos=shipping','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";

            this.MemberIDTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.MemberIDTextBox.ClientID + ")");
            this.CustPhoneTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.CustPhoneTextBox.ClientID + ")");
            this.SenderTelephoneTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.SenderTelephoneTextBox.ClientID + ")");
            this.SenderPostalCodeTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.SenderPostalCodeTextBox.ClientID + ")");
            this.DeliverTelephoneTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.DeliverTelephoneTextBox.ClientID + ")");
            this.DeliverPostalCodeTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.DeliverPostalCodeTextBox.ClientID + ")");
            //this.WeightTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.WeightTextBox.ClientID + ")");
            this.DiscountTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.DiscountTextBox.ClientID + ")");
            this.TRSTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.TRSTextBox.ClientID + ")");
            this.TRS2TextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.TRS2TextBox.ClientID + ")");
            this.TRS3TextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.TRS3TextBox.ClientID + ")");
            this.OSTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.OSTextBox.ClientID + ")");
            this.OS2TextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.OS2TextBox.ClientID + ")");
            this.OS3TextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.OS3TextBox.ClientID + ")");
            this.OtherFeeTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.OtherFeeTextBox.ClientID + ")");
            this.InsuranceCostTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.InsuranceCostTextBox.ClientID + ")");
            this.PackagingCostTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.PackagingCostTextBox.ClientID + ")");

            this.AirwayBillTextBox.Attributes["onclick"] = "AirwayBillKeyBoard(this.id)";
            this.ReferenceNoTextBox.Attributes["onclick"] = "ReferenceNoKeyBoard(this.id)";
            this.CustNameTextBox.Attributes["onclick"] = "CustNameKeyBoard(this.id)";
            this.CustPhoneTextBox.Attributes["onclick"] = "CustPhoneKeyBoard(this.id)";
            this.SenderNameTextBox.Attributes["onclick"] = "SenderNameKeyBoard(this.id)";
            this.SenderAddressTextBox.Attributes["onclick"] = "SenderAddressKeyBoard(this.id)";
            this.SenderTelephoneTextBox.Attributes["onclick"] = "SenderTelephoneKeyBoard(this.id)";
            this.SenderPostalCodeTextBox.Attributes["onclick"] = "SenderPostalCodeKeyBoard(this.id)";
            this.DeliverNameTextBox.Attributes["onclick"] = "DeliverNameKeyBoard(this.id)";
            this.DeliverAddressTextBox.Attributes["onclick"] = "DeliverAddressKeyBoard(this.id)";
            this.DeliverTelephoneTextBox.Attributes["onclick"] = "DeliverTelephoneKeyBoard(this.id)";
            this.DeliverPostalCodeTextBox.Attributes["onclick"] = "DeliverPostalCodeKeyBoard(this.id)";
            this.WeightTextBox.Attributes["onclick"] = "WeightKeyBoard(this.id)";
            this.DiscountTextBox.Attributes["onclick"] = "DiscountKeyBoard(this.id)";
            this.TRSTextBox.Attributes["onclick"] = "TRSKeyBoard(this.id)";
            this.TRS2TextBox.Attributes["onclick"] = "TRS2KeyBoard(this.id)";
            this.TRS3TextBox.Attributes["onclick"] = "TRS3KeyBoard(this.id)";
            this.OSTextBox.Attributes["onclick"] = "OSKeyBoard(this.id)";
            this.OS2TextBox.Attributes["onclick"] = "OS2KeyBoard(this.id)";
            this.OS3TextBox.Attributes["onclick"] = "OS3KeyBoard(this.id)";
            this.OtherFeeTextBox.Attributes["onclick"] = "OtherFeeKeyBoard(this.id)";
            this.InsuranceCostTextBox.Attributes["onclick"] = "InsuranceCostKeyBoard(this.id)";
            this.PackagingCostTextBox.Attributes["onclick"] = "PackagingCostKeyBoard(this.id)";
            this.NotesTextBox.Attributes["onclick"] = "NotesKeyBoard(this.id)";
            this.PasswordTextBox.Attributes["onclick"] = "PasswordKeyBoard(this.id)";

            this.TransNoTextBox.Attributes.Add("ReadOnly", "True");
            this.DiscForexTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            //this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.OtherForexTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "SHIPPING");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }

        protected void VendorDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.VendorDDL.SelectedIndex != 0)
            {
                POSMsShippingVendor _posMsShippingVendor = this._posShippingBL.GetSinglePOSMsShippingVendor(this.VendorDDL.SelectedValue);
                this.FgZoneHiddenField.Value = (_posMsShippingVendor.FgZone == null) ? "N" : Convert.ToString(_posMsShippingVendor.FgZone);

                this.ShowShippingType(this.VendorDDL.SelectedValue);
                this.GetShippingType();
                this.Calculate();
            }
        }

        protected void ShippingTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetShippingType();
        }

        protected void GetShippingType()
        {
            try
            {
                if (this.ShippingTypeDDL.SelectedIndex != 0 & this.VendorDDL.SelectedIndex != 0)
                {
                    this.ClearLabel();
                    if (this.FgZoneHiddenField.Value == "N")
                    {
                        POSMsShipping _posMsShipping = this._posShippingBL.GetPOSMsShipping(this.VendorDDL.SelectedValue, this.ShippingTypeDDL.SelectedValue, this.ProductShapeRBL.SelectedIndex.ToString(), this.DeliverCityDDL.SelectedValue);
                        if (_posMsShipping != null)
                        {
                            this.Price1Literal.Text = Convert.ToDecimal(_posMsShipping.Price1).ToString("#,#.00");
                            this.Price2Literal.Text = Convert.ToDecimal(_posMsShipping.Price2).ToString("#,#.00");
                            this.DFSValueHiddenField.Value = Convert.ToDecimal(_posMsShipping.Percentage).ToString();
                            this.EstimationTimeLiteral.Text = _posMsShipping.EstimationTime.ToString() + " Days";
                            this.UnitCodeHiddenField.Value = _posMsShipping.UnitCode;
                        }
                        else
                        {
                            this.Price1Literal.Text = "0";
                            this.Price2Literal.Text = "0";
                            this.DFSValueHiddenField.Value = "0";
                            this.EstimationTimeLiteral.Text = "0 Days";
                            this.WarningLabel.Text = "Type Shipping Not Found in Master Shipping.";
                            this.UnitCodeHiddenField.Value = "";
                        }
                    }
                    else if (this.FgZoneHiddenField.Value == "Y")
                    {
                        this.WeightTextBox_TextChanged(null, null);
                    }
                }
                else
                {
                    this.Price1Literal.Text = "0";
                    this.Price2Literal.Text = "0";
                    this.DFSValueHiddenField.Value = "0";
                    this.EstimationTimeLiteral.Text = "0 Days";
                    this.UnitCodeHiddenField.Value = "";
                }
                this.Calculate();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void Calculate()
        {
            Decimal _dfsValue = 0;
            Decimal _totalPrice2 = 0;
            Decimal _totalShippingNoDFS = 0;

            if (Convert.ToDecimal(this.WeightTextBox.Text) > 0)
            {
                _totalPrice2 = (Convert.ToDecimal(this.WeightTextBox.Text) - 1) * Convert.ToDecimal(this.Price2Literal.Text);
                _totalShippingNoDFS = Convert.ToDecimal(this.Price1Literal.Text) + _totalPrice2 - Convert.ToDecimal(this.DiscountTextBox.Text);
                _dfsValue = _totalShippingNoDFS * (Convert.ToDecimal(this.DFSValueHiddenField.Value) / 100);
                _totalShippingNoDFS += Convert.ToDecimal(this.TRSTextBox.Text) + Convert.ToDecimal(this.TRS2TextBox.Text) + Convert.ToDecimal(this.TRS3TextBox.Text) + Convert.ToDecimal(this.OSTextBox.Text) + Convert.ToDecimal(this.OS2TextBox.Text) + Convert.ToDecimal(this.OS3TextBox.Text);
                this.DFSValueLiteral.Text = _dfsValue.ToString("#,#.00");
                this.SubTotalLiteral.Text = Convert.ToDecimal(_totalShippingNoDFS + Convert.ToDecimal(this.DFSValueLiteral.Text) + Convert.ToDecimal(this.InsuranceCostTextBox.Text) + Convert.ToDecimal(this.PackagingCostTextBox.Text)).ToString("#,#.00");
                this.TotalLiteral.Text = Convert.ToDecimal(Convert.ToDecimal(this.SubTotalLiteral.Text) + Convert.ToDecimal(this.OtherFeeTextBox.Text)).ToString("#,#.00");
            }
        }

        protected void CalculateHeader()
        {
            Decimal _allDiscon = 0;
            Decimal _allSubtotal = 0;
            Decimal _allOtherForex = 0;
            Decimal _allTotal = 0;

            if (this.TransRefHiddenField.Value != "")
            {
                List<POSTrShippingDt> _listPOSTrShippingDt = this._posShippingBL.GetListShippingDtByTransNmbr(this.TransRefHiddenField.Value);
                if (_listPOSTrShippingDt != null)
                {
                    foreach (var _row in _listPOSTrShippingDt)
                    {
                        _allDiscon += Convert.ToDecimal(_row.DiscForex);
                        _allSubtotal += Convert.ToDecimal(_row.AmountForex);
                        _allOtherForex += Convert.ToDecimal(_row.OtherFee);
                        _allTotal += Convert.ToDecimal(_row.LineTotalForex);
                    }
                }
                this.DiscForexTextBox.Text = _allDiscon.ToString("#,#.00");
                this.AmountBaseTextBox.Text = _allSubtotal.ToString("#,#.00");
                this.OtherForexTextBox.Text = _allOtherForex.ToString("#,#.00");
                this.TotalForexTextBox.Text = _allTotal.ToString("#,#.00");
            }
        }

        protected void CheckValidDataHeader()
        {
            this.ClearLabel();
            if (this.SenderCityDDL.SelectedIndex == 0)
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Choose One of Sender City.";
            }
            String _countryCode = this._countryBL.GetCountryCodeWithFgHomeY();
            if (this.CountryDDL.SelectedValue != _countryCode)
            {
                if (this.CountryDDL.SelectedIndex == 0)
                {
                    this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Choose One of Deliver Country.";
                }
            }
            if (this.DeliverCityDDL.SelectedIndex == 0)
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Choose One of Deliver City.";
            }
        }

        protected void CheckValidDataDetail()
        {
            this.ClearLabel();
            if (this.VendorDDL.SelectedIndex == 0)
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Choose One of Vendor.";
            }
            if (this.ShippingTypeDDL.SelectedIndex == 0)
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Choose One of Shipping Type.";
            }
            if (this.ShippingTRSDDL.SelectedIndex == 0 & Convert.ToDecimal(this.TRSTextBox.Text) > 0)
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Choose One of Transportation Related Surcharge.";
            }
            if (this.ShippingTRS2DDL.SelectedIndex == 0 & Convert.ToDecimal(this.TRS2TextBox.Text) > 0)
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Choose One of Transportation Related Surcharge 2.";
            }
            if (this.ShippingTRS3DDL.SelectedIndex == 0 & Convert.ToDecimal(this.TRS3TextBox.Text) > 0)
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Choose One of Transportation Related Surcharge 3.";
            }
            if (this.ShippingOSDDL.SelectedIndex == 0 & Convert.ToDecimal(this.OSTextBox.Text) > 0)
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Choose One of Other Surcharge.";
            }
            if (this.ShippingOS2DDL.SelectedIndex == 0 & Convert.ToDecimal(this.OS2TextBox.Text) > 0)
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Choose One of Other Surcharge 2.";
            }
            if (this.ShippingOS3DDL.SelectedIndex == 0 & Convert.ToDecimal(this.OS3TextBox.Text) > 0)
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Choose One of Other Surcharge 3.";
            }
        }

        protected void SaveImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.CheckValidDataHeader();
                this.CheckValidDataDetail();
                if (this.WarningLabel.Text == "")
                {
                    this.CalculateHeader();
                    this.SaveButton_Click(null, null);

                    POSTrShippingDt _posTrShippingDt = _posShippingBL.GetSinglePOSTrShippingDt(this.TransRefHiddenField.Value, Convert.ToInt32(this.ItemNoHiddenField.Value == "" ? "0" : this.ItemNoHiddenField.Value));
                    if (_posTrShippingDt != null)
                    {
                        bool _checkCashOnDelivery = false;
                        List<POSTrShippingDt> _listPOSTrShippingDt = _posShippingBL.GetListShippingDtByTransNmbr(this.TransRefHiddenField.Value);
                        foreach (var _item in _listPOSTrShippingDt)
                        {
                            if (_item.PaymentType == '1')
                                _checkCashOnDelivery = true;
                        }

                        if (_checkCashOnDelivery == true & this.PaymentTypeRBL.SelectedValue != "1")
                        {
                            this.WarningLabel.Text = "Transaction should be filled with one Payment Type ";
                        }
                        else
                        {
                            _posTrShippingDt.AirwayBill = this.AirwayBillTextBox.Text;
                            _posTrShippingDt.VendorCode = this.VendorDDL.SelectedValue;
                            _posTrShippingDt.ShippingTypeCode = this.ShippingTypeDDL.SelectedValue;
                            _posTrShippingDt.ProductShape = this.ProductShapeRBL.SelectedValue;
                            _posTrShippingDt.PaymentType = Convert.ToChar(this.PaymentTypeRBL.SelectedValue);
                            _posTrShippingDt.Weight = this.WeightTextBox.Text == "" ? 0 : Convert.ToDecimal(this.WeightTextBox.Text);
                            _posTrShippingDt.Unit = this.UnitCodeHiddenField.Value;
                            _posTrShippingDt.Price1 = this.Price1Literal.Text == "" ? 0 : Convert.ToDecimal(this.Price1Literal.Text);
                            _posTrShippingDt.Price2 = this.Price2Literal.Text == "" ? 0 : Convert.ToDecimal(this.Price2Literal.Text);
                            _posTrShippingDt.AmountForex = this.SubTotalLiteral.Text == "" ? 0 : Convert.ToDecimal(this.SubTotalLiteral.Text);
                            _posTrShippingDt.DiscForex = this.DiscountTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscountTextBox.Text);
                            _posTrShippingDt.PackageAmount = this.PackagingCostTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PackagingCostTextBox.Text);
                            _posTrShippingDt.InsuranceAmount = this.InsuranceCostTextBox.Text == "" ? 0 : Convert.ToDecimal(this.InsuranceCostTextBox.Text);
                            _posTrShippingDt.LineTotalForex = this.TotalLiteral.Text == "" ? 0 : Convert.ToDecimal(this.TotalLiteral.Text);
                            _posTrShippingDt.Remark = this.NotesTextBox.Text;
                            _posTrShippingDt.TRSCode = this.ShippingTRSDDL.SelectedValue;
                            _posTrShippingDt.TRSCode2 = this.ShippingTRS2DDL.SelectedValue;
                            _posTrShippingDt.TRSCode3 = this.ShippingTRS3DDL.SelectedValue;
                            _posTrShippingDt.TRS = this.TRSTextBox.Text == "" ? 0 : Convert.ToDecimal(this.TRSTextBox.Text);
                            _posTrShippingDt.TRS2 = this.TRS2TextBox.Text == "" ? 0 : Convert.ToDecimal(this.TRS2TextBox.Text);
                            _posTrShippingDt.TRS3 = this.TRS3TextBox.Text == "" ? 0 : Convert.ToDecimal(this.TRS3TextBox.Text);
                            _posTrShippingDt.OtherSurchargeCode = this.ShippingOSDDL.SelectedValue;
                            _posTrShippingDt.OtherSurchargeCode2 = this.ShippingOS2DDL.SelectedValue;
                            _posTrShippingDt.OtherSurchargeCode3 = this.ShippingOS3DDL.SelectedValue;
                            _posTrShippingDt.OtherSurcharge = this.OSTextBox.Text == "" ? 0 : Convert.ToDecimal(this.OSTextBox.Text); ;
                            _posTrShippingDt.OtherSurcharge2 = this.OS2TextBox.Text == "" ? 0 : Convert.ToDecimal(this.OS2TextBox.Text); ;
                            _posTrShippingDt.OtherSurcharge3 = this.OS3TextBox.Text == "" ? 0 : Convert.ToDecimal(this.OS3TextBox.Text); ;
                            _posTrShippingDt.DFSValue = this.DFSValueLiteral.Text == "" ? 0 : Convert.ToDecimal(this.DFSValueLiteral.Text); ;
                            _posTrShippingDt.OtherFee = this.OtherFeeTextBox.Text == "" ? 0 : Convert.ToDecimal(this.OtherFeeTextBox.Text); ;

                            bool _result = this._posShippingBL.EditPOSTrShippingDt(_posTrShippingDt);

                            if (_result == true)
                            {
                                this.WarningLabel.Text = "Your Success Edit Data";
                                this.ClearDetail();
                                this.ShowDataDt();
                                this.CalculateHeader();
                            }
                            else
                            {
                                this.WarningLabel.Text = "Your Failed Add Data";
                            }
                        }
                    }
                    else
                    {
                        bool _checkCashOnDelivery = false;
                        List<POSTrShippingDt> _listPOSTrShippingDt = _posShippingBL.GetListShippingDtByTransNmbr(this.TransRefHiddenField.Value);
                        if (_listPOSTrShippingDt != null)
                        {
                            foreach (var _item in _listPOSTrShippingDt)
                            {
                                if (_item.PaymentType == '1')
                                    _checkCashOnDelivery = true;
                            }
                        }

                        if (_checkCashOnDelivery == true & this.PaymentTypeRBL.SelectedValue != "1")
                        {
                            this.WarningLabel.Text = "Transaction should be filled with one Payment Type ";
                        }
                        else
                        {
                            int _posShippingDtCount = _posShippingBL.RowsCountPOSTrShippingDt(this.TransRefHiddenField.Value);

                            _posTrShippingDt = new POSTrShippingDt();
                            _posTrShippingDt.TransNmbr = this.TransRefHiddenField.Value;
                            _posTrShippingDt.ItemNo = _posShippingDtCount + 1;
                            _posTrShippingDt.AirwayBill = this.AirwayBillTextBox.Text;
                            _posTrShippingDt.VendorCode = this.VendorDDL.SelectedValue;
                            _posTrShippingDt.ShippingTypeCode = this.ShippingTypeDDL.SelectedValue;
                            _posTrShippingDt.ProductShape = this.ProductShapeRBL.SelectedValue;
                            _posTrShippingDt.PaymentType = Convert.ToChar(this.PaymentTypeRBL.SelectedValue);
                            _posTrShippingDt.Weight = this.WeightTextBox.Text == "" ? 0 : Convert.ToDecimal(this.WeightTextBox.Text);
                            _posTrShippingDt.Unit = this.UnitCodeHiddenField.Value;
                            _posTrShippingDt.Price1 = this.Price1Literal.Text == "" ? 0 : Convert.ToDecimal(this.Price1Literal.Text);
                            _posTrShippingDt.Price2 = this.Price2Literal.Text == "" ? 0 : Convert.ToDecimal(this.Price2Literal.Text);
                            _posTrShippingDt.Tax = 0;
                            _posTrShippingDt.AmountForex = this.SubTotalLiteral.Text == "" ? 0 : Convert.ToDecimal(this.SubTotalLiteral.Text);
                            _posTrShippingDt.DiscPercentage = 0;
                            _posTrShippingDt.DiscForex = this.DiscountTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscountTextBox.Text);
                            _posTrShippingDt.PackageAmount = this.PackagingCostTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PackagingCostTextBox.Text);
                            _posTrShippingDt.InsuranceAmount = this.InsuranceCostTextBox.Text == "" ? 0 : Convert.ToDecimal(this.InsuranceCostTextBox.Text);
                            _posTrShippingDt.LineTotalForex = this.TotalLiteral.Text == "" ? 0 : Convert.ToDecimal(this.TotalLiteral.Text);
                            _posTrShippingDt.Remark = this.NotesTextBox.Text;
                            _posTrShippingDt.FgConsignment = false;
                            if (this.ShippingTRSDDL.SelectedIndex != 0)
                            {
                                _posTrShippingDt.TRSCode = this.ShippingTRSDDL.SelectedValue;
                                _posTrShippingDt.TRS = this.TRSTextBox.Text == "" ? 0 : Convert.ToDecimal(this.TRSTextBox.Text);
                            }
                            if (this.ShippingTRS2DDL.SelectedIndex != 0)
                            {
                                _posTrShippingDt.TRSCode2 = this.ShippingTRS2DDL.SelectedValue;
                                _posTrShippingDt.TRS2 = this.TRS2TextBox.Text == "" ? 0 : Convert.ToDecimal(this.TRS2TextBox.Text);
                            }
                            if (this.ShippingTRS3DDL.SelectedIndex != 0)
                            {
                                _posTrShippingDt.TRSCode3 = this.ShippingTRS3DDL.SelectedValue;
                                _posTrShippingDt.TRS3 = this.TRS3TextBox.Text == "" ? 0 : Convert.ToDecimal(this.TRS3TextBox.Text);
                            }
                            _posTrShippingDt.Tax = 0;
                            if (this.ShippingOSDDL.SelectedIndex != 0)
                            {
                                _posTrShippingDt.OtherSurchargeCode = this.ShippingOSDDL.SelectedValue;
                                _posTrShippingDt.OtherSurcharge = this.OSTextBox.Text == "" ? 0 : Convert.ToDecimal(this.OSTextBox.Text); ;
                            }
                            if (this.ShippingOS2DDL.SelectedIndex != 0)
                            {
                                _posTrShippingDt.OtherSurchargeCode2 = this.ShippingOS2DDL.SelectedValue;
                                _posTrShippingDt.OtherSurcharge2 = this.OS2TextBox.Text == "" ? 0 : Convert.ToDecimal(this.OS2TextBox.Text); ;
                            }
                            if (this.ShippingOS3DDL.SelectedIndex != 0)
                            {
                                _posTrShippingDt.OtherSurchargeCode3 = this.ShippingOS3DDL.SelectedValue;
                                _posTrShippingDt.OtherSurcharge3 = this.OS3TextBox.Text == "" ? 0 : Convert.ToDecimal(this.OS3TextBox.Text); ;
                            }
                            _posTrShippingDt.DFSValue = this.DFSValueLiteral.Text == "" ? 0 : Convert.ToDecimal(this.DFSValueLiteral.Text); ;
                            _posTrShippingDt.OtherFee = this.OtherFeeTextBox.Text == "" ? 0 : Convert.ToDecimal(this.OtherFeeTextBox.Text); ;
                            _posTrShippingDt.IsVoid = false;

                            bool _result = this._posShippingBL.AddPOSTrShippingDt(_posTrShippingDt);

                            if (_result == true)
                            {
                                //Response.Redirect("POSShipping.aspx" + "?" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransRefHiddenField.Value)) + "&" + this._itemno + "=");
                                this.ClearDetail();
                                this.ShowDataDt();
                                this.CalculateHeader();
                                this.WarningLabel.Text = this.WarningLabel.Text + " Your Success Add Data Detail.";
                            }
                            else
                            {
                                this.WarningLabel.Text = "Your Failed Add Data";
                            }
                            this.SaveButton_Click(null, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ResetImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ItemNoHiddenField.Value != "")
            {
                Response.Redirect("POSShipping.aspx" + "?" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransRefHiddenField.Value)) + "&" + this._itemno + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ItemNoHiddenField.Value)));
            }
            else
            {
                this.ClearLabel();
                this.ClearDetail();
            }
        }

        protected void DeleteImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.TransRefHiddenField.Value != "" & this.ItemNoHiddenField.Value != "")
            {
                this.ChangeVisiblePanel(2);
            }
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Login.aspx");
        }

        protected void CancelAllButton_Click(object sender, EventArgs e)
        {
            if (this.TransRefHiddenField.Value != "")
            {
                this.ChangeVisiblePanel(2);
            }
        }

        protected void PrintPreviewButton_Click(object sender, EventArgs e)
        {

        }

        protected void JoinJobOrderButton_Click(object sender, EventArgs e)
        {
            this.StatusOpenHiddenField.Value = "Close";
        }

        protected void ReasonListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSMsReason _temp = (POSMsReason)e.Item.DataItem;

                String _reasonCode = _temp.ReasonCode.ToString();

                ImageButton _pickReasonButton = (ImageButton)e.Item.FindControl("PickReasonImageButton");
                _pickReasonButton.Attributes.Add("OnClick", "return CancelPaid();");
                _pickReasonButton.CommandName = "PickButton";
                _pickReasonButton.CommandArgument = _reasonCode;

                Literal _reasonLiteral = (Literal)e.Item.FindControl("ReasonLiteral");
                _reasonLiteral.Text = HttpUtility.HtmlEncode(_temp.ReasonName);
            }
        }

        protected void ReasonListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() == "PickButton")
                {
                    Boolean _result = false;
                    _result = this._posShippingBL.SetVOID(this.TransRefHiddenField.Value, e.CommandArgument.ToString(), true);
                    if (_result == true)
                    {
                        this.Clear();
                        this.ClearDetail();
                        this.TransRefHiddenField.Value = "";
                        this.ItemNoHiddenField.Value = "";
                        this.ChangeVisiblePanel(0);
                        this.ShowDataDt();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Process Cancel Success.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Process Cancel Failed');", true);
                        this.ChangeVisiblePanel(0);
                    }
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void Back2ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ChangeVisiblePanel(0);
        }

        protected void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                String _password = this._pOSConfigurationBL.GetSingle("POSCancelPassword").SetValue;
                if (_password.ToLower() == this.PasswordTextBox.Text.ToLower())
                {
                    if (this.TransRefHiddenField.Value != "" && this.ItemNoHiddenField.Value != "")
                    {
                        Boolean _resultDel = this._posShippingBL.DeletePOSTrShippingDt(this.TransRefHiddenField.Value, Convert.ToInt32(this.ItemNoHiddenField.Value));
                        if (_resultDel == true)
                        {
                            this.CalculateHeader();
                            this.SaveButton_Click(null, null);
                            this.ClearDetail();
                            this.ShowDataDt();
                            this.ItemNoHiddenField.Value = "";
                            this.ChangeVisiblePanel(0);
                            //Response.Redirect("POSShipping.aspx" + "?" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransRefHiddenField.Value)) + "&" + this._itemno + "=");
                            this.WarningLabel.Text = this.WarningLabel.Text + " You Success Deleted Data Detail.";
                        }
                        else
                        {
                            this.WarningLabel.Text = "Delete Failed";
                        }
                    }
                    else
                    {
                        this.ChangeVisiblePanel(1);
                        this.ReasonListRepeater.DataSource = this._reasonBL.GetList();
                        this.ReasonListRepeater.DataBind();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Incorrect Password.');", true);
                }
                this.PasswordTextBox.Text = "";
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.CheckValidDataHeader();
                if (this.WarningLabel.Text == "")
                {
                    MsMember _member = this._memberBL.GetSingleByBarcode(this.MemberIDTextBox.Text);
                    if (this.TransRefHiddenField.Value == "")
                    {
                        DateTime _now = DateTime.Now;
                        POSTrShippingHd _posTrShippingHd = new POSTrShippingHd();

                        _posTrShippingHd.TransType = POSTransTypeDataMapper.GetTransType(POSTransType.Shipping);
                        _posTrShippingHd.TransDate = new DateTime(_now.Year, _now.Month, _now.Day, _now.Hour, _now.Minute, _now.Second);
                        _posTrShippingHd.ReferenceNo = this.ReferenceNoTextBox.Text;
                        if (_member != null)
                            _posTrShippingHd.MemberID = this.MemberIDTextBox.Text;
                        else
                            _posTrShippingHd.MemberID = "";
                        _posTrShippingHd.CustName = this.CustNameTextBox.Text;
                        _posTrShippingHd.CustPhone = this.CustPhoneTextBox.Text;
                        _posTrShippingHd.OperatorID = HttpContext.Current.User.Identity.Name;
                        _posTrShippingHd.SenderName = this.SenderNameTextBox.Text;
                        _posTrShippingHd.SenderAddress = this.SenderAddressTextBox.Text;
                        _posTrShippingHd.SenderCityCode = this.SenderCityDDL.SelectedValue;
                        _posTrShippingHd.SenderPostalCode = this.SenderPostalCodeTextBox.Text;
                        _posTrShippingHd.SenderTelephone = this.SenderTelephoneTextBox.Text;
                        _posTrShippingHd.SenderHandphone = "";
                        _posTrShippingHd.DeliverName = this.DeliverNameTextBox.Text;
                        _posTrShippingHd.DeliverAddress = this.DeliverAddressTextBox.Text;
                        _posTrShippingHd.DeliveryCountryCode = this.CountryDDL.SelectedValue;
                        _posTrShippingHd.DeliverCityCode = this.DeliverCityDDL.SelectedValue;
                        _posTrShippingHd.DeliverPostalCode = this.DeliverPostalCodeTextBox.Text;
                        _posTrShippingHd.DeliverTelephone = this.DeliverTelephoneTextBox.Text;
                        _posTrShippingHd.DeliverHandphone = "";
                        _posTrShippingHd.Status = POSTrShippingDataMapper.GetStatus(POSTrShippingStatus.SendToCashier);
                        _posTrShippingHd.IsVOID = false;
                        _posTrShippingHd.SendToSettlement = "Y";
                        _posTrShippingHd.CurrCode = this._currencyBL.GetCurrDefault();
                        _posTrShippingHd.ForexRate = this._currencyRateBL.GetSingleLatestCurrRate(_posTrShippingHd.CurrCode) == 0 ? 1 : this._currencyRateBL.GetSingleLatestCurrRate(_posTrShippingHd.CurrCode);
                        _posTrShippingHd.SubTotalForex = this.AmountBaseTextBox.Text == "0" ? Convert.ToDecimal(this.SubTotalLiteral.Text) : Convert.ToDecimal(this.AmountBaseTextBox.Text);
                        _posTrShippingHd.DiscPercentage = 0;
                        _posTrShippingHd.DiscForex = this.DiscForexTextBox.Text == "0" ? Convert.ToDecimal(this.DiscountTextBox.Text) : Convert.ToDecimal(this.DiscForexTextBox.Text);
                        _posTrShippingHd.PPNPercentage = 0;// this.PPNPercentTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);
                        _posTrShippingHd.PPNForex = 0; // this.PPNForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
                        _posTrShippingHd.OtherForex = this.OtherForexTextBox.Text == "0" ? Convert.ToDecimal(this.OtherFeeTextBox.Text) : Convert.ToDecimal(this.OtherForexTextBox.Text);
                        _posTrShippingHd.TotalForex = (this.TotalForexTextBox.Text == "0" ? Convert.ToDecimal(this.TotalLiteral.Text) : Convert.ToDecimal(this.TotalForexTextBox.Text));
                        _posTrShippingHd.DPForex = 0;
                        _posTrShippingHd.Remark = "";
                        _posTrShippingHd.FakturPajakNmbr = "";// this.PPNNoTextBox.Text;
                        _posTrShippingHd.FakturPajakDate = _defaultdate;// DateFormMapper.GetValue(this.PPNDateTextBox.Text);
                        _posTrShippingHd.FakturPajakRate = 0; //(this.PPNRateTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text));
                        _posTrShippingHd.UserPrep = HttpContext.Current.User.Identity.Name;
                        _posTrShippingHd.DatePrep = DateTime.Now;
                        _posTrShippingHd.UserAppr = HttpContext.Current.User.Identity.Name;
                        _posTrShippingHd.DateAppr = DateTime.Now;
                        _posTrShippingHd.ServiceChargeAmount = 0;
                        _posTrShippingHd.DPPaid = 0;
                        _posTrShippingHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);
                        _posTrShippingHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);
                        _posTrShippingHd.PB1Forex = 0;
                        _posTrShippingHd.DeliveryOrderReff = "";
                        bool _result = this._posShippingBL.AddPOSTrShippingHd(_posTrShippingHd);
                        if (_result == true)
                        {
                            this.WarningLabel.Text = "Your Success Add Data Header";
                            this.TransRefHiddenField.Value = _posTrShippingHd.TransNmbr;
                            this.TransNoTextBox.Text = this.TransRefHiddenField.Value;
                        }
                        else
                        {
                            this.WarningLabel.Text = "Your Failed Add Data Header";
                        }
                    }
                    else
                    {
                        DateTime _now = DateTime.Now;
                        POSTrShippingHd _posTrShippingHd = this._posShippingBL.GetSinglePOSTrShippingHd(this.TransRefHiddenField.Value);
                        _posTrShippingHd.TransDate = new DateTime(_now.Year, _now.Month, _now.Day, _now.Hour, _now.Minute, _now.Second);
                        _posTrShippingHd.ReferenceNo = this.ReferenceNoTextBox.Text;
                        if (_member != null)
                            _posTrShippingHd.MemberID = this.MemberIDTextBox.Text;
                        else
                            _posTrShippingHd.MemberID = "";
                        _posTrShippingHd.CustName = this.CustNameTextBox.Text;
                        _posTrShippingHd.CustPhone = this.CustPhoneTextBox.Text;
                        _posTrShippingHd.OperatorID = HttpContext.Current.User.Identity.Name;
                        _posTrShippingHd.SenderName = this.SenderNameTextBox.Text;
                        _posTrShippingHd.SenderAddress = this.SenderAddressTextBox.Text;
                        _posTrShippingHd.SenderCityCode = this.SenderCityDDL.SelectedValue;
                        _posTrShippingHd.SenderPostalCode = this.SenderPostalCodeTextBox.Text;
                        _posTrShippingHd.SenderTelephone = this.SenderTelephoneTextBox.Text;
                        _posTrShippingHd.DeliverName = this.DeliverNameTextBox.Text;
                        _posTrShippingHd.DeliverAddress = this.DeliverAddressTextBox.Text;
                        _posTrShippingHd.DeliveryCountryCode = this.CountryDDL.SelectedValue;
                        _posTrShippingHd.DeliverCityCode = this.DeliverCityDDL.SelectedValue;
                        _posTrShippingHd.DeliverPostalCode = this.DeliverPostalCodeTextBox.Text;
                        _posTrShippingHd.DeliverTelephone = this.DeliverTelephoneTextBox.Text;
                        _posTrShippingHd.SubTotalForex = this.AmountBaseTextBox.Text == "0" ? 0 : Convert.ToDecimal(this.AmountBaseTextBox.Text);
                        _posTrShippingHd.DiscForex = this.DiscForexTextBox.Text == "0" ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
                        _posTrShippingHd.OtherForex = this.OtherForexTextBox.Text == "0" ? 0 : Convert.ToDecimal(this.OtherForexTextBox.Text);
                        _posTrShippingHd.TotalForex = (this.TotalForexTextBox.Text == "0" ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text));
                        _posTrShippingHd.UserPrep = HttpContext.Current.User.Identity.Name;
                        _posTrShippingHd.DatePrep = DateTime.Now;
                        _posTrShippingHd.UserAppr = HttpContext.Current.User.Identity.Name;
                        _posTrShippingHd.DateAppr = DateTime.Now;

                        bool _result = this._posShippingBL.EditPOSTrShippingHd(_posTrShippingHd);
                        if (_result == true)
                        {
                            this.WarningLabel.Text = "Your Success Edit Data Header";
                        }
                        else
                        {
                            this.WarningLabel.Text = "Your Failed Add Data Header";
                        }
                    }
                    this.DeliverEnabled(false);
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ShowDataHd(String _prmTransRef)
        {
            POSTrShippingHd _posTrShippingHd = this._posShippingBL.GetSinglePOSTrShippingHd(_prmTransRef);
            if (_posTrShippingHd != null)
            {
                this.ReferenceNoTextBox.Text = _posTrShippingHd.ReferenceNo;
                this.MemberIDTextBox.Text = _posTrShippingHd.MemberID;
                this.CustNameTextBox.Text = _posTrShippingHd.CustName;
                this.CustPhoneTextBox.Text = _posTrShippingHd.CustPhone;
                this.SenderNameTextBox.Text = _posTrShippingHd.SenderName;
                this.SenderAddressTextBox.Text = _posTrShippingHd.SenderAddress;
                this.SenderCityDDL.SelectedValue = _posTrShippingHd.SenderCityCode;
                this.SenderPostalCodeTextBox.Text = _posTrShippingHd.SenderPostalCode;
                this.SenderTelephoneTextBox.Text = _posTrShippingHd.SenderTelephone;
                this.DeliverNameTextBox.Text = _posTrShippingHd.DeliverName;
                this.DeliverAddressTextBox.Text = _posTrShippingHd.DeliverAddress;
                this.CountryDDL.SelectedValue = _posTrShippingHd.DeliveryCountryCode;
                this.DeliverCityDDL.SelectedValue = _posTrShippingHd.DeliverCityCode;
                this.DeliverPostalCodeTextBox.Text = _posTrShippingHd.DeliverPostalCode;
                this.DeliverTelephoneTextBox.Text = _posTrShippingHd.DeliverTelephone;
                this.TransNoTextBox.Text = _posTrShippingHd.TransNmbr;
                this.AmountBaseTextBox.Text = Convert.ToDecimal(_posTrShippingHd.SubTotalForex).ToString("#,#.00");
                this.DiscForexTextBox.Text = Convert.ToDecimal(_posTrShippingHd.DiscForex).ToString("#,#.00");
                this.OtherForexTextBox.Text = Convert.ToDecimal(_posTrShippingHd.OtherForex).ToString("#,#.00");
                this.TotalForexTextBox.Text = Convert.ToDecimal(_posTrShippingHd.TotalForex).ToString("#,#.00");

                this.DeliverEnabled(false);
            }
        }

        protected void ShowDataDt()
        {
            try
            {
                this.DetailItemRepeater.DataSource = this._posShippingBL.GetListShippingDtByTransNmbr(this.TransRefHiddenField.Value);
                this.DetailItemRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void ShowSingleDt(String _prmTransNo, String _itemNo)
        {
            try
            {
                POSTrShippingDt _posTrShippingDt = _posShippingBL.GetSinglePOSTrShippingDt(_prmTransNo, Convert.ToInt32(_itemNo));
                this.AirwayBillTextBox.Text = _posTrShippingDt.AirwayBill;
                this.VendorDDL.SelectedValue = _posTrShippingDt.VendorCode;
                this.VendorDDL_SelectedIndexChanged(null, null);
                this.ShippingTypeDDL.SelectedValue = _posTrShippingDt.ShippingTypeCode;
                //this.ShippingTypeDDL_SelectedIndexChanged(null, null);
                this.ProductShapeRBL.SelectedValue = _posTrShippingDt.ProductShape;
                this.PaymentTypeRBL.SelectedValue = _posTrShippingDt.PaymentType.ToString();
                this.WeightTextBox.Text = Convert.ToDecimal(_posTrShippingDt.Weight).ToString("#,#");
                this.UnitCodeHiddenField.Value = _posTrShippingDt.Unit;
                this.Price1Literal.Text = Convert.ToDecimal(_posTrShippingDt.Price1).ToString("#,#.00");
                this.Price2Literal.Text = Convert.ToDecimal(_posTrShippingDt.Price2).ToString("#,#.00");
                this.AmountBaseTextBox.Text = Convert.ToDecimal(_posTrShippingDt.AmountForex).ToString("#,#.00");
                this.DiscountTextBox.Text = Convert.ToDecimal(_posTrShippingDt.DiscForex).ToString("#,#.00");
                this.PackagingCostTextBox.Text = Convert.ToDecimal(_posTrShippingDt.PackageAmount).ToString("#,#.00");
                this.InsuranceCostTextBox.Text = Convert.ToDecimal(_posTrShippingDt.InsuranceAmount).ToString("#,#.00");
                this.TotalForexTextBox.Text = Convert.ToDecimal(_posTrShippingDt.LineTotalForex).ToString("#,#.00");
                this.NotesTextBox.Text = _posTrShippingDt.Remark;
                this.ShippingTRSDDL.SelectedValue = _posTrShippingDt.TRSCode;
                this.ShippingTRS2DDL.SelectedValue = _posTrShippingDt.TRSCode2;
                this.ShippingTRS3DDL.SelectedValue = _posTrShippingDt.TRSCode3;
                this.TRSTextBox.Text = Convert.ToDecimal(_posTrShippingDt.TRS).ToString("#,#.00");
                this.TRS2TextBox.Text = Convert.ToDecimal(_posTrShippingDt.TRS2).ToString("#,#.00");
                this.TRS3TextBox.Text = Convert.ToDecimal(_posTrShippingDt.TRS3).ToString("#,#.00");
                this.ShippingOSDDL.SelectedValue = _posTrShippingDt.OtherSurchargeCode;
                this.ShippingOS2DDL.SelectedValue = _posTrShippingDt.OtherSurchargeCode2;
                this.ShippingOS3DDL.SelectedValue = _posTrShippingDt.OtherSurchargeCode3;
                this.OSTextBox.Text = Convert.ToDecimal(_posTrShippingDt.OtherSurcharge).ToString("#,#.00");
                this.OS2TextBox.Text = Convert.ToDecimal(_posTrShippingDt.OtherSurcharge2).ToString("#,#.00");
                this.OS3TextBox.Text = Convert.ToDecimal(_posTrShippingDt.OtherSurcharge3).ToString("#,#.00");
                this.SubTotalLiteral.Text = Convert.ToDecimal(_posTrShippingDt.AmountForex).ToString("#,#.00");
                this.DFSValueLiteral.Text = Convert.ToDecimal(_posTrShippingDt.DFSValue).ToString("#,#.00");
                this.OtherFeeTextBox.Text = Convert.ToDecimal(_posTrShippingDt.OtherFee).ToString("#,#.00");
                this.TotalLiteral.Text = Convert.ToDecimal(_posTrShippingDt.LineTotalForex).ToString("#,#.00");
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void SendToCashierButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool _result = this._posShippingBL.SendToCashier(this.TransRefHiddenField.Value);
                if (_result == true)
                {
                    //Response.Redirect("POSShipping.aspx" + "?" + this._code + "=" + "&" + this._itemno + "=");
                    this.Clear();
                    this.ClearDetail();
                    this.ShowDataDt();
                    this.DeliverEnabled(true);
                    this.WarningLabel.Text = this.WarningLabel.Text + " You Success Send Data To Cashier.";
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void DetailItemRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string[] _splitCode = e.CommandArgument.ToString().Split('-');
                this.TransRefHiddenField.Value = _splitCode[0];
                this.ItemNoHiddenField.Value = _splitCode[1];
                this.ShowSingleDt(this.TransRefHiddenField.Value, this.ItemNoHiddenField.Value);
            }
        }

        protected void DetailItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    POSTrShippingDt _temp = (POSTrShippingDt)e.Item.DataItem;
                    string _code = _temp.ItemNo.ToString();

                    ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                    //_viewButton.PostBackUrl = "POSShipping.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" +
                    // this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr + "-" + _temp.ItemNo + "-" + "" + "-" + ""));

                    _viewButton.PostBackUrl = "POSShipping.aspx" + "?" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr + "-" + _temp.ItemNo + "-" + "" + "-" + ""));
                    _viewButton.CommandName = "View";
                    _viewButton.CommandArgument = _temp.TransNmbr + "-" + _code;

                    POSTrShippingHd _posTrShippingHd = this._posShippingBL.GetSinglePOSTrShippingHd(_temp.TransNmbr);
                    String _vendorName = "";
                    String _shippingZonaTypeName = "";
                    String _countryName = "";
                    String _cityName = "";

                    String _countryCode = this._countryBL.GetCountryCodeWithFgHomeY();
                    if (_countryCode != _posTrShippingHd.DeliveryCountryCode)
                    {
                        _countryCode = _posTrShippingHd.DeliveryCountryCode;
                        _shippingZonaTypeName = this._posShippingBL.GetSinglePOSMsZone(_temp.ShippingTypeCode).ZoneName;
                    }
                    else
                    {
                        POSMsShipping _posMsShipping = this._posShippingBL.GetPOSMsShipping(_temp.VendorCode, _temp.ShippingTypeCode, _temp.ProductShape, _posTrShippingHd.DeliverCityCode);
                        //_vendorName = _posMsShipping.VendorName;
                        //_cityName = _posMsShipping.CityName; 
                        _shippingZonaTypeName = _posMsShipping.ShippingTypeName;

                    }
                    _countryName = this._countryBL.GetCountryNameByCode(_countryCode);
                    _cityName = this._cityBL.GetCityNameByCode(_posTrShippingHd.DeliverCityCode);
                    _vendorName = this._vendorBL.GetSingle(_temp.VendorCode).VendorName;

                    Literal _vendor = (Literal)e.Item.FindControl("VendorLiteral");
                    _vendor.Text = _vendorName;

                    Literal _type = (Literal)e.Item.FindControl("TypeLiteral");
                    _type.Text = _shippingZonaTypeName;

                    Literal _location = (Literal)e.Item.FindControl("LocationLiteral");
                    _location.Text = _countryName + " - " + _cityName;
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ViewData()
        {
            if (Request.QueryString[this._code].ToString() != "") // & this.StatusOpenHiddenField.Value != "Open"
            {
                this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
                string _codeTransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._code), ApplicationConfig.EncryptionKey);
                if (_codeTransNmbr != "")
                {
                    string[] _splitCode = _codeTransNmbr.Split('-');
                    this.TransRefHiddenField.Value = _splitCode[0];
                    this.ShowDataHd(this.TransRefHiddenField.Value);
                    this.ShowDataDt();

                    string _codeItemno = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemno), ApplicationConfig.EncryptionKey);
                    if (_codeItemno != "")
                    {
                        _splitCode = _codeItemno.Split('-');
                        this.ItemNoHiddenField.Value = _splitCode[0];
                        this.ShowSingleDt(this.TransRefHiddenField.Value, this.ItemNoHiddenField.Value);
                    }
                    //this.StatusOpenHiddenField.Value = "Open";
                }
            }
        }

        protected void VisibleValidationSummary(Int16 _prmVisible)
        {
            if (_prmVisible == 0)
                this.ValidationSummary.Visible = false;
            else
                this.ValidationSummary.Visible = true;
        }

        protected void WeightTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.WeightTextBox.Text == "")
                this.WeightTextBox.Text = "0";
            if (this.ProductShapeWithIntPriorityRBL.Visible == false)
            {
                if (Convert.ToDecimal(this.WeightTextBox.Text) > 1)
                {
                    this.ProductShapeRBL.SelectedValue = "1";
                }
            }
            else
            {
                Decimal _max = 0;

                _max = this._posShippingBL.GetMaxWeightDocument(this.ShippingTypeDDL.SelectedValue, this.ProductShapeWithIntPriorityRBL.SelectedValue);

                if (this.ProductShapeWithIntPriorityRBL.SelectedValue == "0" & Convert.ToDecimal(this.WeightTextBox.Text) > _max)
                    this.ProductShapeWithIntPriorityRBL.SelectedValue = "1";
                //else if (this.ProductShapeWithIntPriorityRBL.SelectedValue == "1" & Convert.ToDecimal(this.WeightTextBox.Text) > _max)
                //    this.ProductShapeWithIntPriorityRBL.SelectedValue = "2";
            }

            if (this.FgZoneHiddenField.Value == "N")
            {
                this.GetShippingType();
            }
            else if (this.FgZoneHiddenField.Value == "Y")
            {
                General_TemporaryTable _temp = null;
                if (this.ProductShapeWithIntPriorityRBL.Visible == true)
                    _temp = this._posShippingBL.GetPOSMsZone(this.ShippingTypeDDL.SelectedValue, this.ProductShapeWithIntPriorityRBL.SelectedValue.ToString(), Convert.ToDouble(this.WeightTextBox.Text), this.CountryDDL.SelectedValue);
                else
                    _temp = this._posShippingBL.GetPOSMsZone(this.ShippingTypeDDL.SelectedValue, this.ProductShapeRBL.SelectedValue.ToString(), Convert.ToDouble(this.WeightTextBox.Text), this.CountryDDL.SelectedValue);

                if (_temp != null)
                {
                    if (Convert.ToDecimal(_temp.Field3) > 0)
                        this.MaxLiteral.Text = " (Max " + Convert.ToDecimal(_temp.Field1).ToString("#,#") + " Kg)";

                    this.Price1Literal.Text = Convert.ToDecimal(_temp.Field2).ToString("#,#.00");
                    this.Price2Literal.Text = Convert.ToDecimal(_temp.Field3).ToString("#,#.00");
                    this.DFSValueHiddenField.Value = Convert.ToDecimal(_temp.Field4).ToString();
                    this.EstimationTimeLiteral.Text = (_temp.Field5 == null) ? "0" : _temp.Field5.ToString() + " Days";
                    this.UnitCodeHiddenField.Value = _temp.Field6;
                }
                else
                {
                    this.Price1Literal.Text = "0";
                    this.Price2Literal.Text = "0";
                    this.DFSValueHiddenField.Value = "0";
                    this.EstimationTimeLiteral.Text = "0 Days";
                    this.WarningLabel.Text = "Type Shipping Not Found in Master Shipping Zone.";
                    this.UnitCodeHiddenField.Value = "";
                }
            }

            this.Calculate();
            this.CalculateHeader();
        }

        protected void NewMemberButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Registration/Registration.aspx");
        }

        protected void MemberIDTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.MemberIDTextBox.Text.Length == 13)
                {
                    MsMember _member = this._memberBL.GetSingleByBarcode(this.MemberIDTextBox.Text);

                    if (_member != null)
                    {
                        this.CustNameTextBox.Text = _member.MemberName;
                        this.CustNameTextBox.Attributes.Add("ReadOnly", "True");
                        this.CustNameTextBox.Attributes.Add("style", "color: #808080");

                        if (_member.Telephone1 == "" || _member.Telephone1 == null)
                        {
                            this.CustPhoneTextBox.Text = _member.HandPhone1;
                            this.CustPhoneTextBox.Attributes.Add("ReadOnly", "True");
                            this.CustPhoneTextBox.Attributes.Add("style", "color: #808080");
                        }
                        else
                        {
                            this.CustPhoneTextBox.Text = _member.Telephone1;
                            this.CustPhoneTextBox.Attributes.Add("ReadOnly", "True");
                            this.CustPhoneTextBox.Attributes.Add("style", "color: #808080");
                        }
                    }
                }
                else
                {
                    this.MemberIDTextBox.Text = "";
                    this.CustNameTextBox.Attributes.Remove("ReadOnly");
                    this.CustNameTextBox.Attributes.Add("style", "color: #000000");
                    this.CustPhoneTextBox.Attributes.Remove("ReadOnly");
                    this.CustPhoneTextBox.Attributes.Add("style", "color: #000000");
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void CountryDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowCityDeliver();
            this.ShowVendor();
            this.ShowShippingType("");
            this.GetShippingType();
            this.Calculate();
        }

        protected void DeliverCityDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowVendor();
            this.ShowShippingType("");
            this.GetShippingType();
            this.Calculate();
        }

        protected void DiscountTextBox_TextChanged(object sender, EventArgs e)
        {
            this.Calculate();
            this.CalculateHeader();
        }

        protected void DeliverEnabled(Boolean _prmValue)
        {
            if (_prmValue == false)
            {
                this.DeliverNameTextBox.Enabled = false;
                this.DeliverAddressTextBox.Enabled = false;
                this.CountryDDL.Enabled = false;
                this.DeliverCityDDL.Enabled = false;
                this.DeliverPostalCodeTextBox.Enabled = false;
                this.DeliverTelephoneTextBox.Enabled = false;
            }
            else if (_prmValue == true)
            {
                this.DeliverNameTextBox.Enabled = true;
                this.DeliverAddressTextBox.Enabled = true;
                this.CountryDDL.Enabled = true;
                this.DeliverCityDDL.Enabled = true;
                this.DeliverPostalCodeTextBox.Enabled = true;
                this.DeliverTelephoneTextBox.Enabled = true;
            }
        }

        protected void ProductShapeRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetShippingType();
            this.Calculate();
        }

        protected void ProductShapeWithIntPriorityRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetShippingType();
            this.Calculate();
        }
    }
}