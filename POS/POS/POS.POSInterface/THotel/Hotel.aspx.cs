using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using BusinessRule.POS;
using Microsoft.Reporting.WebForms;
using System.Threading;
using BusinessRule.POSInterface;


namespace POS.POSInterface.Hotel
{
    public partial class Hotel : POSInterfaceBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private AccountBL _accountBL = new AccountBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
        private HotelBL _hotelBL = new HotelBL();
        private AirLineBL _airLineBL = new AirLineBL();
        private MemberBL _memberBL = new MemberBL();
        private CustomerDOBL _customerDOBL = new CustomerDOBL();
        private ReportTourBL _reportTourBL = new ReportTourBL();
        private ReportListBL _reportListBL = new ReportListBL();
        private UserBL _userBL = new UserBL();
        private TicketingBL _ticketingBL = new TicketingBL();
        private User_EmployeeBL _userEmployeeBL = new User_EmployeeBL();
        private POSReasonBL _reasonBL = new POSReasonBL();
        private POSConfigurationBL _pOSConfigurationBL = new POSConfigurationBL();


        protected string _referenceNo = "referenceNo";
        protected string _code = "code";
        protected NameValueCollectionExtractor _nvcExtractor;

        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            if (!this.Page.IsPostBack == true)
            {
                this.JavaScript();
                //this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.btnSearchCustomer.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findCustomer&configCode=customer','_popSearch','width=1200,height=1000,toolbar=0,location=0,status=0,scrollbars=1')";
                this.CheckInDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.CheckInDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.CheckOutDateLiteral.Text = "<input id='button3' type='button' onclick='displayCalendar(" + this.CheckOutDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                //this.btnSearchHotel.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findHotel&configCode=hotel','_popSearch','width=1200,height=1000,toolbar=0,location=0,status=0,scrollbars=1')";
                this.JoinJobOrderButton.OnClientClick = "window.open('../General/JoinJobOrder.aspx?valueCatcher=findTransNmbr&pos=hotel','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";
                this.CheckStatusButton.OnClientClick = "window.open('../General/CheckStatus.aspx?valueCatcher=findTransNmbr&pos=hotel','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";
                this.ClearData();
                this.ClearDataDt();
                this.ClearLabel();
                this.SetAttribute();
                //this.SetAttributeRate();
                this.SetCurrRate();
                //this.SetAttributDt();
                //this.TransNoTextBoxt.Text = "TRN/11OCT28/00000004";
                //if (this.TransNoTextBoxt.Text != "")
                //{
                //    this.ShowData(this.TransNoTextBoxt.Text);
                //}
                this.ShowButtonDt();
                this.ShowDetail();
                this.ChangeVisiblePanel(0);
                this.SetButtonPermission();
                //this.PrintPreviewPanel.Visible = false;
                //this.ReasonListPanel.Visible = false;
                if (Request.QueryString[this._referenceNo].ToString() != "")
                {
                    this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
                    string _code = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._referenceNo), ApplicationConfig.EncryptionKey);
                    if (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._referenceNo), ApplicationConfig.EncryptionKey) != "")
                    {
                        string[] _splitCode = _code.Split('-');
                        this.ReferenceTextBox.Text = _splitCode[0];
                        if (_splitCode.Count() > 1)
                        {
                            this.CustNameTextBox.Text = _splitCode[1];
                            this.CustPhoneTextBox.Text = _splitCode[2];
                        }
                        this.ReferenceTextBox.Enabled = false;
                        this.CustNameTextBox.Enabled = false;
                        this.CustPhoneTextBox.Enabled = false;
                        this.btnSearchCustomer.Visible = false;
                        this.ReferenceNoHiddenField.Value = _splitCode[0];
                        POSTrDeliveryOrderRef _pOSTrDeliveryOrderRef = this._customerDOBL.GetSingleTrDeliveryOrderRefByReferenceNoTransType(_splitCode[0], AppModule.GetValue(TransactionType.Hotel));
                        if (_pOSTrDeliveryOrderRef != null)
                        {
                            this.TransNoTextBoxt.Text = _pOSTrDeliveryOrderRef.TransNmbr;
                            //this.ShowData(this.TransNoTextBoxt.Text);
                            //this.ShowDetail();
                        }
                    }
                }
            }
            this.TotalTextBox.Text = (Convert.ToDecimal(this.QtyRoomTextBox.Text) * Convert.ToDecimal(this.BasicFareTextBox.Text)).ToString();
            this.SellingPriceTextBox.Text = (Convert.ToDecimal(this.TotalTextBox.Text) - Convert.ToDecimal(this.DiscountTextBox.Text)).ToString();
            this.VoucherCodeTextBox_TextChanged(null, null);
            this.SaveHeaderButton_Click(null, null);
            this.SaveButton_Click(null, null);
            this.ClearLabel();

            if (Request.QueryString[this._code].ToString() != "")
            {
                this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
                string _code = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._code), ApplicationConfig.EncryptionKey);
                if (_code != "")
                {
                    string[] _splitCode = _code.Split('-');
                    this.TransNoHiddenField.Value = _splitCode[0];
                    if (_splitCode.Count() > 1)
                    {
                        this.ItemNoHiddenField.Value = _splitCode[1];
                        this.ReferenceTextBox.Text = _splitCode[2];
                        if (_splitCode[3] != "")
                            this.VoucherCodeTextBox.Text = _splitCode[3];
                    }
                    if (this.TransNoHiddenField.Value != "")
                    {
                        this.ShowData(this.TransNoHiddenField.Value);
                        this.ShowButtonDt();
                        //if (this.StatusOpenHiddenField.Value != "Open")
                        //{
                        this.ShowDetail();
                        if (this.ItemNoHiddenField.Value != "")
                            this.ShowSingleDt(this.TransNoHiddenField.Value, this.ItemNoHiddenField.Value);
                        this.StatusOpenHiddenField.Value = "Open";
                        //this.JoinJobOrderButton.Enabled = false;
                        //}
                    }
                }
            }
            else if (this.TransNoTextBoxt.Text != "" & this.StatusOpenHiddenField.Value != "Open") //if join job order = open then not show again
            {
                this.ShowData(this.TransNoTextBoxt.Text);
                this.ShowButtonDt();
                this.ShowDetail();
                this.StatusOpenHiddenField.Value = "Open";
                this.JoinJobOrderButton.Enabled = false;
            }
            //}
            //catch (Exception ex)
            //{
            //    this.errorhandler(ex);
            //}
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

        protected void JavaScript()
        {
            String spawnJS = "<script language='JavaScript'>\n";
            spawnJS += "function findCustomer(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.CustCodeTextBox.ClientID + "').value = dataArray[0];\n";
            spawnJS += "document.getElementById('" + this.CustNameTextBox.ClientID + "').value = dataArray[1];\n";
            spawnJS += "document.getElementById('" + this.CustPhoneTextBox.ClientID + "').value = dataArray[4];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";
            spawnJS += "</script>\n";
            this.javascriptReceiver.Text = spawnJS;

            //String spawnJSDt = "<script language='JavaScript'>\n";
            //spawnJSDt += "function findHotel(x) {\n";
            //spawnJSDt += "dataArray = x.split ('|') ;\n";
            //spawnJSDt += "document.getElementById('" + this.HotelHiddenField.ClientID + "').value = dataArray[0];\n";
            //spawnJSDt += "document.getElementById('" + this.HotelTextBox.ClientID + "').value = dataArray[1];\n";
            //spawnJSDt += "document.forms[0].submit();\n";
            //spawnJSDt += "}\n";
            //spawnJSDt += "</script>\n";
            //this.javascriptReceiverDt.Text = spawnJSDt;

            String spawnJSJo = "<script language='JavaScript'>\n";
            //////////////////////DECLARE FUNCTION FOR CATCHING PRODUCT SEARCH
            //spawnJSJo += "function findProduct(x) {\n";
            //spawnJSJo += "dataArray = x.split ('|') ;\n";
            //spawnJSJo += "document.getElementById('" + this.CustCodeTextBox.ClientID + "').value = dataArray [0];\n";
            //spawnJSJo += "document.forms[0].submit();\n";
            //spawnJSJo += "}\n";

            ////////////////////DECLARE FUNCTION FOR CATCHING ON HOLD SEARCH
            spawnJSJo += "function findTransNmbr(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.TransNoTextBoxt.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Reference
            spawnJSJo += "function ReferenceKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findReference&titleinput=Reference&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Reference
            spawnJSJo += "function findReference(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.ReferenceTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard CustName
            spawnJSJo += "function CustNameKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findCustName&titleinput=Name&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON CustName
            spawnJSJo += "function findCustName(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.CustNameTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard CustPhone
            spawnJSJo += "function CustPhoneKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findCustPhone&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON CustPhone
            spawnJSJo += "function findCustPhone(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.CustPhoneTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard PPNPercent
            spawnJSJo += "function PPNPercentKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findPPNPercent&titleinput=PPN&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON PPNPercent
            spawnJSJo += "function findPPNPercent(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.PPNPercentTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard DiscountPercentage
            spawnJSJo += "function DiscountPercentageKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findDiscountPercentage&titleinput=Discount&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON DiscountPercentage
            spawnJSJo += "function findDiscountPercentage(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.DiscountPercentageTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard OtherForex
            spawnJSJo += "function OtherForexKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findOtherForex&titleinput=Other&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON OtherForex
            spawnJSJo += "function findOtherForex(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.OtherForexTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Remark
            spawnJSJo += "function RemarkKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findRemark&titleinput=Remark&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Remark
            spawnJSJo += "function findRemark(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.RemarkTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard VoucherCode
            spawnJSJo += "function VoucherCodeKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findVoucherCode&titleinput=Voucher Code&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON VoucherCode
            spawnJSJo += "function findVoucherCode(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.VoucherCodeTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard QtyRoom
            spawnJSJo += "function QtyRoomKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findQtyRoom&titleinput=Total Guest&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON QtyRoom
            spawnJSJo += "function findQtyRoom(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.QtyRoomTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard BasicFare
            spawnJSJo += "function BasicFareKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findBasicFare&titleinput=Basic Fare&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON BasicFare
            spawnJSJo += "function findBasicFare(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.BasicFareTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Discount
            spawnJSJo += "function DiscountKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findDiscount&titleinput=Discount&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Discount
            spawnJSJo += "function findDiscount(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.DiscountTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard BuyingPrice
            spawnJSJo += "function BuyingPriceKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findBuyingPrice&titleinput=Buying Price&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON BuyingPrice
            spawnJSJo += "function findBuyingPrice(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.BuyingPriceTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Guest
            spawnJSJo += "function GuestKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findGuest&titleinput=Guest&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Guest
            spawnJSJo += "function findGuest(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.GuestTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard VoucherInformation
            spawnJSJo += "function VoucherInformationKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findVoucherInformation&titleinput=Voucher Info&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON VoucherInformation
            spawnJSJo += "function findVoucherInformation(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.VoucherInformationTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard RemarkDt
            spawnJSJo += "function RemarkDtKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findRemarkDt&titleinput=Remark&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON RemarkDt
            spawnJSJo += "function findRemarkDt(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.RemarkDtTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Password
            spawnJSJo += "function PasswordKeyBoard(x) {\n";
            spawnJSJo += "window.open('../General/KeyBoard.aspx?valueCatcher=findPassword&titleinput=Password&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJSJo += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Password
            spawnJSJo += "function findPassword(x) {\n";
            spawnJSJo += "dataArray = x.split ('|') ;\n";
            spawnJSJo += "document.getElementById('" + this.PasswordTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJSJo += "document.forms[0].submit();\n";
            spawnJSJo += "}\n";

            spawnJSJo += "</script>\n";
            this.javascriptReceiverJO.Text = spawnJSJo;

            String spawnJSCheck = "<script language='JavaScript'>\n";
            ////////////////////DECLARE FUNCTION FOR CATCHING ON HOLD SEARCH
            spawnJSCheck += "function findTransNmbr(x) {\n";
            spawnJSCheck += "dataArray = x.split ('|') ;\n";
            spawnJSCheck += "document.getElementById('" + this.TransNoTextBoxt.ClientID + "').value = dataArray [0];\n";
            spawnJSCheck += "document.forms[0].submit();\n";
            spawnJSCheck += "}\n";
            spawnJSCheck += "</script>\n";
            this.javascriptReceiverCheck.Text = spawnJSCheck;
        }

        private void ClearDataNumeric()
        {
            try
            {
                byte _decimalPlace = this._currencyBL.GetDecimalPlace("IDR");

                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
                this.PPNPercentTextBox.Text = "0";
                this.PPNForexTextBox.Text = "0";
                this.TotalForexTextBox.Text = "0";
                this.OtherForexTextBox.Text = "0";
                this.AmountBaseTextBox.Text = "0";
                this.DiscForexTextBox.Text = "0";
                this.TotalForexTextBox.Text = "0";
                this.DiscountPercentageTextBox.Text = "0";

                //this.PPhPercentTextBox.Text = "0";
                //this.PPNRateTextBox.Text = "0";
                //this.CurrTextBox.Text = "";
                //this.CurrRateTextBox.Text = "0";
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();
            this.ClearDataNumeric();
            this.CustCodeTextBox.Text = "";
            this.CustNameTextBox.Text = "";
            this.CustPhoneTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.PPNNoTextBox.Text = "";
            this.PPNDateTextBox.Text = "";
            this.ReferenceTextBox.Text = "";
            this.ShowSupplierDDL();
            this.ShowHotelDDL("");

            //this.SalesDropDownList.SelectedValue = "null";
            //this.CurrCodeDropDownList.SelectedValue = _currencyBL.GetCurrDefault();
            //this.CurrRateTextBox.Text = "";
            //this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            //this.PaymentTypeDDL.SelectedValue = "AR";
            //this.paymentDropDownList.Enabled = false;
            //this.paymentDropDownList.SelectedValue = "null";
        }

        private void DisableRate()
        {
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.PPNRateTextBox.Text = "1";

            //this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            //this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            //this.CurrRateTextBox.Text = "1";
        }

        private void EnableRate()
        {
            this.PPNRateTextBox.Attributes.Remove("ReadOnly");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");

            //this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            //this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
        }

        private void SetCurrRate()
        {
            try
            {
                String _currRate = "";
                byte _decimalPlace = this._currencyBL.GetDecimalPlace("IDR");
                _currRate = this._currencyRateBL.GetSingleLatestCurrRate("IDR").ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.PPNRateTextBox.Text = _currRate;
                this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

                if (("IDR").Trim().ToLower() == _currencyBL.GetCurrDefault().Trim().ToLower())
                {
                    this.DisableRate();
                }
                else
                {
                    this.EnableRate();
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        //private void ShowPayType()
        //{
        //    this.paymentDropDownList.Items.Clear();
        //    this.paymentDropDownList.DataTextField = "PayName";
        //    this.paymentDropDownList.DataValueField = "PayCode";
        //    this.paymentDropDownList.DataSource = this._paymentBL.GetListDDLDPSuppPay(this.CurrCodeDropDownList.SelectedValue);
        //    this.paymentDropDownList.DataBind();
        //    this.paymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //public void ShowCurr()
        //{
        //    this.CurrCodeDropDownList.Items.Clear();
        //    this.CurrCodeDropDownList.DataTextField = "CurrCode";
        //    this.CurrCodeDropDownList.DataValueField = "CurrCode";
        //    this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
        //    this.CurrCodeDropDownList.DataBind();
        //    this.CurrCodeDropDownList.SelectedValue = _currencyBL.GetCurrDefault();
        //    this.CurrCodeDropDownListSelectedIndexChanged();
        //}

        //protected void ShowSalesDropDownList()
        //{
        //    this.SalesDropDownList.Items.Clear();
        //    this.SalesDropDownList.DataSource = this._employeeBL.GetListEmpForDDL();
        //    this.SalesDropDownList.DataValueField = "EmpNumb";
        //    this.SalesDropDownList.DataTextField = "EmpName";
        //    this.SalesDropDownList.DataBind();
        //    this.SalesDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

        //}

        //private void CurrCodeDropDownListSelectedIndexChanged()
        //{
        //    this.ClearDataNumeric();

        //    if (CurrCodeDropDownList.SelectedValue != "null")
        //    {
        //        this.SetCurrRate();
        //    }

        //    this.SetAttributeRate();
        //}

        //protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.CurrCodeDropDownListSelectedIndexChanged();
        //}

        private void SetAttribute()
        {
            this.ReferenceTextBox.Attributes["onclick"] = "ReferenceKeyBoard(this.id)";
            this.CustNameTextBox.Attributes["onclick"] = "CustNameKeyBoard(this.id)";
            this.CustPhoneTextBox.Attributes["onclick"] = "CustPhoneKeyBoard(this.id)";
            this.PPNPercentTextBox.Attributes["onclick"] = "PPNPercentKeyBoard(this.id)";
            this.DiscountPercentageTextBox.Attributes["onclick"] = "DiscountPercentageKeyBoard(this.id)";
            this.OtherForexTextBox.Attributes["onclick"] = "OtherForexKeyBoard(this.id)";
            this.RemarkTextBox.Attributes["onclick"] = "RemarkKeyBoard(this.id)";
            this.VoucherCodeTextBox.Attributes["onclick"] = "VoucherCodeKeyBoard(this.id)";
            this.QtyRoomTextBox.Attributes["onclick"] = "QtyRoomKeyBoard(this.id)";
            this.BasicFareTextBox.Attributes["onclick"] = "BasicFareKeyBoard(this.id)";
            this.DiscountTextBox.Attributes["onclick"] = "DiscountKeyBoard(this.id)";
            this.BuyingPriceTextBox.Attributes["onclick"] = "BuyingPriceKeyBoard(this.id)";
            this.GuestTextBox.Attributes["onclick"] = "GuestKeyBoard(this.id)";
            this.VoucherInformationTextBox.Attributes["onclick"] = "VoucherInformationKeyBoard(this.id)";
            this.RemarkDtTextBox.Attributes["onclick"] = "RemarkDtKeyBoard(this.id)";
            this.PasswordTextBox.Attributes["onclick"] = "PasswordKeyBoard(this.id)";

            //this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CustCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.TransNoTextBoxt.Attributes.Add("ReadOnly", "True");
            this.CheckInDateTextBox.Attributes.Add("ReadOnly", "True");
            this.CheckOutDateTextBox.Attributes.Add("ReadOnly", "True");
            //this.HotelTextBox.Attributes.Add("ReadOnly", "True");
            this.SellingPriceTextBox.Attributes.Add("ReadOnly", "True");
            //this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            //this.PPhForexTextBox.Attributes.Add("ReadOnly", "True");
            //this.SetAttributeRate();

            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.SaveHeaderButton.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscountPercentageTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscountPercentageTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscountPercentageTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscountPercentageTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.OtherForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscountPercentageTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.QtyRoomTextBox.Attributes.Add("OnBLur", "Calculate3(" + this.QtyRoomTextBox.ClientID + "," + this.BasicFareTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + "," + this.DiscountTextBox.ClientID + ")");
            this.BasicFareTextBox.Attributes.Add("OnBlur", "Calculate3(" + this.QtyRoomTextBox.ClientID + "," + this.BasicFareTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + "," + this.DiscountTextBox.ClientID + ")");
            this.DiscountTextBox.Attributes.Add("OnBlur", "Calculate2(" + this.DiscountTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + ")");

            this.QtyRoomTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.BasicFareTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.DiscountTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.SellingPriceTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.BuyingPriceTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.QtyRoomTextBox.Attributes.Add("OnKeyUp", "if(isNaN(this.value) == true) this.value = 0 ;");
            this.TotalTextBox.Attributes.Add("OnKeyUp", "if(isNaN(this.value) == true) this.value = 0;");
            this.CustPhoneTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.PPNNoTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.PPNPercentTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.DiscountPercentageTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.OtherForexTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            //this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        //private void SetAttributeRate()
        //{
        //    this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        //    this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscountPercentageTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        //    this.DiscountPercentageTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscountPercentageTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        //    this.OtherForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscountPercentageTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        //    //this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        //}

        protected void VoucherCodeTextBox_TextChanged(object sender, System.EventArgs e)
        {
            //if (this.VoucherCodeTextBox.Text != "")
            //{
            //    this.btnSearchHotel.Visible = true;
            //}
            //else
            //{
            //    this.btnSearchHotel.Visible = false;
            //}
        }

        //protected void PaymentTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.PaymentTypeDDL.SelectedValue == "AR")
        //    {
        //        this.paymentDropDownList.Enabled = false;
        //    }
        //    else
        //    {
        //        this.paymentDropDownList.Enabled = true;
        //        this.paymentDropDownList.SelectedValue = "null";
        //    }
        //}

        protected void SaveTrHdWithDeliveryOrder()
        {
            try
            {
                MsMember _member = this._memberBL.GetSingleByBarcode(this.MemberBarcodeTextBox.Text);
                DateTime _now = DateTime.Now;

                POSTrHotelHd _posTrHotelHd = new POSTrHotelHd();
                POSTrDeliveryOrderRef _posTrDeliveryOrderRef = new POSTrDeliveryOrderRef();

                _posTrHotelHd.TransDate = new DateTime(_now.Year, _now.Month, _now.Day, _now.Hour, _now.Minute, _now.Second);
                _posTrHotelHd.Status = POSHotelDataMapper.GetStatus(TransStatus.Approved);
                _posTrHotelHd.TransType = AppModule.GetValue(TransactionType.Hotel);
                _posTrHotelHd.ReferenceNo = this.ReferenceTextBox.Text;
                _posTrHotelHd.DeliveryOrderReff = this.ReferenceTextBox.Text;
                //if (this.BranchDropDownList.SelectedValue != "null")
                //{
                //    _posTrHotelHd.BranchCode = new Guid(this.BranchDropDownList.SelectedValue);
                //}
                if (_member != null)
                {
                    _posTrHotelHd.MemberCode = this.MemberBarcodeTextBox.Text;
                }
                else
                {
                    _posTrHotelHd.MemberCode = "";
                }
                _posTrHotelHd.CustCode = (this.CustCodeTextBox.Text == "" ? "CASH" : this.CustCodeTextBox.Text);
                _posTrHotelHd.CustName = this.CustNameTextBox.Text;
                _posTrHotelHd.CustPhone = this.CustPhoneTextBox.Text;
                _posTrHotelHd.PaymentType = "CASH";
                //if (this.PaymentTypeDDL.SelectedValue == "AR")
                _posTrHotelHd.CashBankType = "";
                //else
                //_posTrHotelHd.CashBankType = this.paymentDropDownList.SelectedValue;
                _posTrHotelHd.SalesID = this._userEmployeeBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name);
                _posTrHotelHd.OperatorID = HttpContext.Current.User.Identity.Name;
                _posTrHotelHd.Remark = this.RemarkDtTextBox.Text;
                _posTrHotelHd.IsVOID = false;
                _posTrHotelHd.SendToSettlement = "Y";
                _posTrHotelHd.CurrCode = "IDR";
                _posTrHotelHd.ForexRate = Convert.ToDecimal(1);
                _posTrHotelHd.SubTotalForex = this.AmountBaseTextBox.Text == "" ? 0 : Convert.ToDecimal(this.AmountBaseTextBox.Text);
                _posTrHotelHd.DiscPercentage = this.DiscountPercentageTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscountPercentageTextBox.Text);
                _posTrHotelHd.DiscForex = this.DiscForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
                _posTrHotelHd.PPNPercentage = this.PPNPercentTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);
                _posTrHotelHd.PPNForex = this.PPNForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
                _posTrHotelHd.OtherForex = this.OtherForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.OtherForexTextBox.Text);
                _posTrHotelHd.TotalForex = (this.TotalForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text));
                //_posTrHotelHd.DPForex = 0;
                //_posTrHotelHd.DPPaid = 0;
                _posTrHotelHd.FakturPajakNmbr = this.PPNNoTextBox.Text;
                if (this.PPNDateTextBox.Text == "")
                    _posTrHotelHd.FakturPajakDate = null;
                else
                    _posTrHotelHd.FakturPajakDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
                _posTrHotelHd.FakturPajakRate = (this.PPNRateTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text));
                _posTrHotelHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);
                _posTrHotelHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);
                _posTrHotelHd.UserPrep = HttpContext.Current.User.Identity.Name;
                _posTrHotelHd.DatePrep = DateTime.Now;

                _posTrDeliveryOrderRef.ReferenceNo = this.ReferenceTextBox.Text;
                _posTrDeliveryOrderRef.TransType = AppModule.GetValue(TransactionType.Hotel);

                string _result = this._hotelBL.AddPOSTrHotelHdForDO(_posTrHotelHd, _posTrDeliveryOrderRef);

                if (_result != "")
                {
                    this.WarningLabel.Text = "Your Success Add Data";
                    this.ShowData(_posTrHotelHd.TransNmbr);
                    this.ShowDetail();
                    this.ShowButtonDt();
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Add Data";
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void SaveHeaderButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.CustNameTextBox.Text != "")
                {
                    MsMember _member = this._memberBL.GetSingleByBarcode(this.MemberBarcodeTextBox.Text);
                    if (this.TransNoTextBoxt.Text == "")
                    {
                        if (this.ReferenceNoHiddenField.Value == "")
                        {
                            DateTime _now = DateTime.Now;

                            POSTrHotelHd _posTrHotelHd = new POSTrHotelHd();

                            _posTrHotelHd.TransDate = new DateTime(_now.Year, _now.Month, _now.Day, _now.Hour, _now.Minute, _now.Second);
                            _posTrHotelHd.Status = POSHotelDataMapper.GetStatus(TransStatus.Approved);
                            _posTrHotelHd.TransType = AppModule.GetValue(TransactionType.Hotel);
                            _posTrHotelHd.ReferenceNo = this.ReferenceTextBox.Text;
                            _posTrHotelHd.DeliveryOrderReff = "";

                            if (_member != null)
                            {
                                _posTrHotelHd.MemberCode = this.MemberBarcodeTextBox.Text;
                            }
                            else
                            {
                                _posTrHotelHd.MemberCode = "";
                            }
                            _posTrHotelHd.CustCode = (this.CustCodeTextBox.Text == "" ? "CASH" : this.CustCodeTextBox.Text);
                            _posTrHotelHd.CustName = this.CustNameTextBox.Text;
                            _posTrHotelHd.CustPhone = this.CustPhoneTextBox.Text;
                            _posTrHotelHd.PaymentType = "CASH";
                            //if (this.PaymentTypeDDL.SelectedValue == "AR")
                            _posTrHotelHd.CashBankType = "";
                            //else
                            //_posTrHotelHd.CashBankType = this.paymentDropDownList.SelectedValue;
                            _posTrHotelHd.SalesID = this._userEmployeeBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name);
                            _posTrHotelHd.OperatorID = HttpContext.Current.User.Identity.Name;
                            _posTrHotelHd.Remark = this.RemarkDtTextBox.Text;
                            _posTrHotelHd.IsVOID = false;
                            _posTrHotelHd.SendToSettlement = "Y";
                            _posTrHotelHd.CurrCode = "IDR";
                            _posTrHotelHd.ForexRate = Convert.ToDecimal(1);
                            _posTrHotelHd.SubTotalForex = this.AmountBaseTextBox.Text == "" ? 0 : Convert.ToDecimal(this.AmountBaseTextBox.Text);
                            _posTrHotelHd.DiscPercentage = this.DiscountPercentageTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscountPercentageTextBox.Text);
                            _posTrHotelHd.DiscForex = this.DiscForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
                            _posTrHotelHd.PPNPercentage = this.PPNPercentTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);
                            _posTrHotelHd.PPNForex = this.PPNForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
                            _posTrHotelHd.OtherForex = this.OtherForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.OtherForexTextBox.Text);
                            _posTrHotelHd.TotalForex = (this.TotalForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text));
                            //_posTrHotelHd.DPForex = 0;
                            //_posTrHotelHd.DPPaid = 0;
                            _posTrHotelHd.FakturPajakNmbr = this.PPNNoTextBox.Text;
                            if (this.PPNDateTextBox.Text == "")
                                _posTrHotelHd.FakturPajakDate = null;
                            else
                                _posTrHotelHd.FakturPajakDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
                            _posTrHotelHd.FakturPajakRate = (this.PPNRateTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text));
                            _posTrHotelHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);
                            _posTrHotelHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);
                            _posTrHotelHd.UserPrep = HttpContext.Current.User.Identity.Name;
                            _posTrHotelHd.DatePrep = DateTime.Now;

                            string _result = this._hotelBL.AddPOSTrHotelHd(_posTrHotelHd);

                            if (_result != "")
                            {
                                this.WarningLabel.Text = "Your Success Add Data";
                                this.ShowData(_posTrHotelHd.TransNmbr);
                                this.ShowDetail();
                                this.ShowButtonDt();
                            }
                            else
                            {
                                this.WarningLabel.Text = "Your Failed Add Data";
                            }
                        }
                        else
                        {
                            this.SaveTrHdWithDeliveryOrder();
                        }

                    }
                    else
                    {
                        DateTime now = DateTime.Now;
                        this.DateTimeHiddenField.Value = DateFormMapper.GetValue(now);
                        //POSTrTicketingHd _posTrTicketingHd = new POSTrTicketingHd();
                        POSTrHotelHd _posTrHotelHd = this._hotelBL.GetSinglePOSTrHotelHd(this.TransNoTextBoxt.Text);

                        _posTrHotelHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTimeHiddenField.Value).Year, DateFormMapper.GetValue(this.DateTimeHiddenField.Value).Month, DateFormMapper.GetValue(this.DateTimeHiddenField.Value).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        _posTrHotelHd.TransType = AppModule.GetValue(TransactionType.Hotel);
                        _posTrHotelHd.ReferenceNo = this.ReferenceTextBox.Text;

                        if (_member != null)
                        {
                            _posTrHotelHd.MemberCode = this.MemberBarcodeTextBox.Text;
                        }
                        else
                        {
                            _posTrHotelHd.MemberCode = "";
                        }

                        _posTrHotelHd.CustCode = (this.CustCodeTextBox.Text == "" ? "CASH" : this.CustCodeTextBox.Text);
                        _posTrHotelHd.CustName = this.CustNameTextBox.Text;
                        _posTrHotelHd.CustPhone = this.CustPhoneTextBox.Text;
                        _posTrHotelHd.PaymentType = "CASH";
                        //if (this.PaymentTypeDDL.SelectedValue == "Cash")
                        //    _posTrHotelHd.CashBankType = this.paymentDropDownList.SelectedValue;
                        //else

                        _posTrHotelHd.CashBankType = "";
                        _posTrHotelHd.SalesID = this._userEmployeeBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name);
                        _posTrHotelHd.OperatorID = HttpContext.Current.User.Identity.Name;
                        _posTrHotelHd.Remark = this.RemarkTextBox.Text;
                        _posTrHotelHd.IsVOID = false;
                        _posTrHotelHd.SendToSettlement = "Y";
                        _posTrHotelHd.CurrCode = "IDR";
                        _posTrHotelHd.ForexRate = Convert.ToDecimal(1);
                        _posTrHotelHd.SubTotalForex = this.AmountBaseTextBox.Text == "" ? 0 : Convert.ToDecimal(this.AmountBaseTextBox.Text);
                        _posTrHotelHd.DiscPercentage = this.DiscountPercentageTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscountPercentageTextBox.Text);
                        _posTrHotelHd.DiscForex = this.DiscForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
                        _posTrHotelHd.PPNPercentage = this.PPNPercentTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);
                        _posTrHotelHd.PPNForex = this.PPNForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
                        _posTrHotelHd.OtherForex = this.OtherForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.OtherForexTextBox.Text);
                        _posTrHotelHd.TotalForex = (this.TotalForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text));
                        _posTrHotelHd.FakturPajakNmbr = this.PPNNoTextBox.Text;
                        if (this.PPNDateTextBox.Text == "")
                            _posTrHotelHd.FakturPajakDate = null;
                        else
                            _posTrHotelHd.FakturPajakDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
                        _posTrHotelHd.FakturPajakRate = (this.PPNRateTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text));
                        _posTrHotelHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);
                        _posTrHotelHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);
                        _posTrHotelHd.UserPrep = HttpContext.Current.User.Identity.Name;
                        _posTrHotelHd.DatePrep = DateTime.Now;

                        bool _result = this._hotelBL.EditPOSTrHotelHd(_posTrHotelHd);

                        if (_result == true)
                        {
                            this.WarningLabel.Text = "Your Success Edit Data";
                            this.ShowData(this.TransNoTextBoxt.Text);
                            this.ShowDetail();
                        }
                        else
                        {
                            this.WarningLabel.Text = "Your Failed Add Data";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ResetHeaderButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.TransNoTextBoxt.Text != "")
            {
                this.ShowData(this.TransNoTextBoxt.Text);
            }
            else
            {
                this.ClearData();
            }
            this.ClearLabel();

        }

        public void ShowData(String _prmTransNo)
        {
            try
            {
                POSTrHotelHd _posTrHotelHd = this._hotelBL.GetSinglePOSTrHotelHd(_prmTransNo);

                this.TransNoTextBoxt.Text = _posTrHotelHd.TransNmbr;
                //this.PaymentTypeDDL.SelectedValue = _posTrHotelHd.PaymentType;
                //if (_posTrHotelHd.PaymentType == "AR")
                //{
                //    this.paymentDropDownList.Enabled = false;
                //}
                //else
                //{
                //    this.paymentDropDownList.SelectedValue = _posTrTicketingHd.CashBankType;
                //}
                this.CustCodeTextBox.Text = _posTrHotelHd.CustCode;
                this.ReferenceTextBox.Text = _posTrHotelHd.ReferenceNo;
                //this.SalesDropDownList.SelectedValue = _posTrHotelHd.SalesID;
                this.CustNameTextBox.Text = _posTrHotelHd.CustName;
                this.RemarkTextBox.Text = _posTrHotelHd.Remark;
                this.CustPhoneTextBox.Text = _posTrHotelHd.CustPhone;
                this.MemberBarcodeTextBox.Text = _posTrHotelHd.MemberCode;
                //this.CurrCodeDropDownList.SelectedValue = _posTrHotelHd.CurrCode;
                //this.CurrRateTextBox.Text = ((Decimal)_posTrHotelHd.ForexRate).ToString("#,##0.0");
                this.PPNPercentTextBox.Text = ((Decimal)_posTrHotelHd.PPNPercentage).ToString("#,##0.0");
                this.PPNNoTextBox.Text = _posTrHotelHd.FakturPajakNmbr;
                this.PPNDateTextBox.Text = DateFormMapper.GetValue(_posTrHotelHd.FakturPajakDate);
                this.PPNRateTextBox.Text = ((Decimal)_posTrHotelHd.FakturPajakRate).ToString("#,##0.0");
                this.DiscountPercentageTextBox.Text = ((Decimal)_posTrHotelHd.DiscPercentage).ToString("#,##0.0");
                this.DiscForexTextBox.Text = ((Decimal)_posTrHotelHd.DiscForex).ToString("#,##0.0");
                this.AmountBaseTextBox.Text = ((Decimal)_posTrHotelHd.SubTotalForex).ToString("#,##0.0");
                this.PPNForexTextBox.Text = ((Decimal)_posTrHotelHd.PPNForex).ToString("#,##0.0");
                this.OtherForexTextBox.Text = ((Decimal)_posTrHotelHd.OtherForex).ToString("#,##0.0");
                this.TotalForexTextBox.Text = ((Decimal)_posTrHotelHd.TotalForex).ToString("#,##0.0");

                if (_posTrHotelHd.DeliveryOrderReff != "")
                {
                    this.ReferenceTextBox.Enabled = false;
                    this.CustNameTextBox.Enabled = false;
                    this.CustPhoneTextBox.Enabled = false;
                    this.btnSearchCustomer.Visible = false;
                }

                this.ShowBtnPrintPreview(_posTrHotelHd.Status);
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ShowBtnPrintPreview(Char _status)
        {
            if (_status == POSTicketingDataMapper.GetStatus(TransStatus.Posted))
            {
                this.PrintPreviewButton.Enabled = true;
                this.SaveButton.Enabled = false;
                this.SaveHeaderButton.Enabled = false;
                this.SendToCashierButton.Enabled = false;
                this.DeleteButton2.Enabled = false;
                this.CancelAllButton.Enabled = false;
            }
            else
            {
                this.PrintPreviewButton.Enabled = false;
                this.SaveButton.Enabled = true;
                this.SaveHeaderButton.Enabled = true;
                this.SendToCashierButton.Enabled = true;
            }
        }

        //METHOD DETAIL
        //private void SetAttributDt()
        //{
        //    this.CheckInDateTextBox.Attributes.Add("ReadOnly", "True");
        //    this.CheckOutDateTextBox.Attributes.Add("ReadOnly", "True");
        //    this.HotelTextBox.Attributes.Add("ReadOnly", "True");
        //    this.SellingPriceTextBox.Attributes.Add("ReadOnly", "True");
        //    this.QtyRoomTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        //    this.BasicFareTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        //    this.DiscountTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        //    this.SellingPriceTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        //    this.BuyingPriceTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        //    this.QtyRoomTextBox.Attributes.Add("OnKeyUp", "if(isNaN(this.value) == true) this.value = 0 ;");
        //    this.TotalTextBox.Attributes.Add("OnKeyUp", "if(isNaN(this.value) == true) this.value = 0;");
        //    this.QtyRoomTextBox.Attributes.Add("OnBLur", "Calculate3(" + this.QtyRoomTextBox.ClientID + "," + this.BasicFareTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + "," + this.DiscountTextBox.ClientID + ")");
        //    this.BasicFareTextBox.Attributes.Add("OnBlur", "Calculate3(" + this.QtyRoomTextBox.ClientID + "," + this.BasicFareTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + "," + this.DiscountTextBox.ClientID + ")");
        //    this.DiscountTextBox.Attributes.Add("OnBlur", "Calculate2(" + this.DiscountTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + ")");

        //    this.CustPhoneTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        //    this.PPNNoTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        //    this.PPNPercentTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        //    this.DiscountPercentageTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        //    this.OtherForexTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        //}


        protected void ShowSupplierDDL()
        {
            this.SupplierNameDLL.Items.Clear();
            this.SupplierNameDLL.DataTextField = "SupplierName";
            this.SupplierNameDLL.DataValueField = "SuppCode";
            this.SupplierNameDLL.DataSource = this._hotelBL.GetListSupplierPOSMsHotel();
            this.SupplierNameDLL.DataBind();
            this.SupplierNameDLL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowHotelDDL(String _prmSuppCode)
        {
            this.HotelNameDDL.Items.Clear();
            this.HotelNameDDL.DataTextField = "HotelName";
            this.HotelNameDDL.DataValueField = "HotelCode";
            this.HotelNameDDL.DataSource = this._hotelBL.GetListHotelBySuppCode(_prmSuppCode);
            this.HotelNameDDL.DataBind();
            this.HotelNameDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SupplierNameDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowHotelDDL(this.SupplierNameDLL.SelectedValue);
            this.SuplierCodeHiddenField.Value = this.SupplierNameDLL.SelectedValue;
        }

        public void ClearDataDt()
        {
            this.VoucherCodeTextBox.Text = "";
            this.SupplierNameDLL.SelectedValue = "null";
            this.HotelHiddenField.Value = "";
            this.CheckInDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.CheckOutDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.QtyRoomTextBox.Text = "0";
            this.BasicFareTextBox.Text = "0";
            this.TotalTextBox.Text = "0";
            this.DiscountTextBox.Text = "0";
            this.SellingPriceTextBox.Text = "0";
            this.BuyingPriceTextBox.Text = "0";
            this.GuestTextBox.Text = "";
            this.VoucherInformationTextBox.Text = "";
            this.RemarkDtTextBox.Text = "";
        }

        protected void ValidattionDt()
        {
            if (this.BasicFareTextBox.Text == "0")
            {
                this.WarningLabel.Text = "Basic Fire Must Be More than 0";
            }
            if (this.BuyingPriceTextBox.Text == "0")
            {
                this.WarningLabel.Text = "Buying Price Must Be More than 0";
            }
            if (this.VoucherCodeTextBox.Text == "" && this.HotelNameDDL.SelectedValue != "null")
            {
                this.WarningLabel.Text = "Voucher No Must Be Filled";
            }
            if (this.VoucherCodeTextBox.Text != "" && this.HotelNameDDL.SelectedValue == "null")
            {
                this.WarningLabel.Text = "Hotel Must Be Filled";
            }
            if (this.VoucherCodeTextBox.Text == "" && this.HotelNameDDL.SelectedValue == "null")
            {
                this.WarningLabel.Text = "Voucher No & Hotel Must Be Filled";
            }
        }

        protected void ShowButtonDt()
        {
            if (this.TransNoTextBoxt.Text != "")
            {
                this.SaveButton.Visible = true;
            }
            else
            {
                this.SaveButton.Visible = false;
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.ValidattionDt();
                if ((this.ItemNoHiddenField.Value != "") &&
                    (this.BasicFareTextBox.Text != "0"))
                {
                    POSTrHotelDt _posTrHotelDt = _hotelBL.GetSinglePOSTrHotelDt(this.TransNoHiddenField.Value, Convert.ToInt32(this.ItemNoHiddenField.Value));
                    POSTrHotelHd _posTrHotelHd = _hotelBL.GetSinglePOSTrHotelHd(this.TransNoHiddenField.Value);

                    decimal _subTotalForex = Convert.ToDecimal(_posTrHotelHd.SubTotalForex - _posTrHotelDt.SellingPrice);

                    //int _posTicketDtCount = _ticketingBL.RowsCountPOSTrTicketingDt(this.TransNoHiddenField.Value);

                    _posTrHotelDt.VoucherNo = this.VoucherCodeTextBox.Text;
                    if (this.HotelNameDDL.SelectedValue != "null")
                    {
                        _posTrHotelDt.HotelCode = this.HotelNameDDL.SelectedValue;
                    }
                    _posTrHotelDt.VoucherStartDate = DateFormMapper.GetValue(this.CheckInDateTextBox.Text);
                    _posTrHotelDt.VoucherEndDate = DateFormMapper.GetValue(this.CheckOutDateTextBox.Text);
                    _posTrHotelDt.TotalRoom = Convert.ToInt32(this.QtyRoomTextBox.Text);
                    _posTrHotelDt.BasicFare = this.BasicFareTextBox.Text == "" ? 0 : Convert.ToDecimal(this.BasicFareTextBox.Text);
                    _posTrHotelDt.TotalBasicFare = this.TotalTextBox.Text == "" ? 0 : Convert.ToDecimal(this.TotalTextBox.Text);
                    _posTrHotelDt.DiscountAmount = this.DiscountTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscountTextBox.Text);
                    _posTrHotelDt.SellingPrice = this.SellingPriceTextBox.Text == "" ? 0 : Convert.ToDecimal(this.SellingPriceTextBox.Text);
                    _posTrHotelDt.BuyingPrice = this.BuyingPriceTextBox.Text == "" ? 0 : Convert.ToDecimal(this.BuyingPriceTextBox.Text);
                    _posTrHotelDt.GuestName = this.GuestTextBox.Text;
                    _posTrHotelDt.VoucherInformation = this.VoucherInformationTextBox.Text;
                    _posTrHotelDt.Remark = this.RemarkDtTextBox.Text;
                    _posTrHotelDt.EditBy = HttpContext.Current.User.Identity.Name;
                    _posTrHotelDt.EditDate = DateTime.Now;

                    bool _result = this._hotelBL.EditPOSTrHotelDt(_posTrHotelDt, _subTotalForex, this.SellingPriceTextBox.Text);

                    if (_result == true)
                    {
                        this.WarningLabel.Text = "Your Success Edit Data";
                        //this.StatusOpenHiddenField.Value = "Close";
                        //this.JoinJobOrderButton.Enabled = true;
                        this.ShowData(this.TransNoTextBoxt.Text);
                        this.ShowDetail();
                        this.ShowSingleDt(_posTrHotelDt.TransNmbr, Convert.ToString(_posTrHotelDt.ItemNo));
                        this.ShowHotelDDL("");
                    }
                    else
                    {
                        this.WarningLabel.Text = "Your Failed Add Data";
                    }

                }
                else
                {

                    POSTrHotelDt _posTrHotelDt = new POSTrHotelDt();
                    if ((this.VoucherCodeTextBox.Text != "" && this.HotelNameDDL.SelectedValue != "null")
                        && (this.BasicFareTextBox.Text != "0") && (this.BuyingPriceTextBox.Text != "0")
                        )
                    {
                        double _posTicketDtCount = _hotelBL.RowsCountPOSTrHotelDt(this.TransNoTextBoxt.Text);

                        _posTrHotelDt.TransNmbr = this.TransNoTextBoxt.Text;
                        _posTrHotelDt.ItemNo = Convert.ToInt32(_posTicketDtCount + 1);
                        _posTrHotelDt.VoucherNo = this.VoucherCodeTextBox.Text;
                        _posTrHotelDt.VoucherStartDate = DateFormMapper.GetValue(this.CheckInDateTextBox.Text);
                        _posTrHotelDt.VoucherEndDate = DateFormMapper.GetValue(this.CheckOutDateTextBox.Text);
                        if (this.HotelNameDDL.SelectedValue != "null")
                        {
                            _posTrHotelDt.HotelCode = this.HotelNameDDL.SelectedValue;
                        }
                        _posTrHotelDt.TotalRoom = Convert.ToInt32(this.QtyRoomTextBox.Text);
                        _posTrHotelDt.BasicFare = this.BasicFareTextBox.Text == "" ? 0 : Convert.ToDecimal(this.BasicFareTextBox.Text);
                        _posTrHotelDt.TotalBasicFare = this.TotalTextBox.Text == "" ? 0 : Convert.ToDecimal(this.TotalTextBox.Text);
                        _posTrHotelDt.DiscountAmount = this.DiscountTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscountTextBox.Text);
                        _posTrHotelDt.SellingPrice = this.SellingPriceTextBox.Text == "" ? 0 : Convert.ToDecimal(this.SellingPriceTextBox.Text);
                        _posTrHotelDt.BuyingPrice = this.BuyingPriceTextBox.Text == "" ? 0 : Convert.ToDecimal(this.BuyingPriceTextBox.Text);
                        _posTrHotelDt.GuestName = this.GuestTextBox.Text;
                        _posTrHotelDt.VoucherInformation = this.VoucherInformationTextBox.Text;
                        _posTrHotelDt.Remark = this.RemarkDtTextBox.Text;
                        _posTrHotelDt.InsertBy = HttpContext.Current.User.Identity.Name;
                        _posTrHotelDt.InsertDate = DateTime.Now;
                        _posTrHotelDt.EditBy = HttpContext.Current.User.Identity.Name;
                        _posTrHotelDt.EditDate = DateTime.Now;

                        bool _result = this._hotelBL.AddPOSTrHotelDt(_posTrHotelDt);

                        if (_result == true)
                        {
                            //this.WarningLabel.Text = "Your Success Add Data";
                            //this.StatusOpenHiddenField.Value = "Close";
                            //this.JoinJobOrderButton.Enabled = true;
                            //this.ShowData(this.TransNoTextBoxt.Text);
                            //this.ShowDetail();
                            Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBoxt.Text)));
                        }
                        else
                        {
                            this.WarningLabel.Text = "Your Failed Add Data";
                        }
                    }
                }
            }
            catch (ThreadAbortException) { throw; }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void DetailItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSTrHotelDt _temp = (POSTrHotelDt)e.Item.DataItem;
                string _code = _temp.ItemNo.ToString();

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = "Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)) + "&" +
                this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_temp.TransNmbr + "-" + _temp.ItemNo + "-" + "" + "-" + ""));
                _viewButton.CommandName = "View";
                _viewButton.CommandArgument = _temp.TransNmbr + "-" + _code;

                Literal _productCode = (Literal)e.Item.FindControl("VoucherCodeLiteral");
                _productCode.Text = HttpUtility.HtmlEncode(_temp.VoucherNo);

                Literal _rrNo = (Literal)e.Item.FindControl("HotelLiteral");
                _rrNo.Text = HttpUtility.HtmlEncode(this._hotelBL.GetHotelNameByCode(_temp.HotelCode));

            }

        }

        protected void DetailItemRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string[] _splitCode = e.CommandArgument.ToString().Split('-');
                this.TransNoHiddenField.Value = _splitCode[0];
                this.ItemNoHiddenField.Value = _splitCode[1];

                this.ShowSingleDt(_splitCode[0], _splitCode[1]);
                this.ClearLabel();
            }
        }

        public void ShowDetail()
        {
            try
            {
                this.DetailItemRepeater.DataSource = this._hotelBL.GetListPOSTrHotelDt(this.TransNoTextBoxt.Text);

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
                if (this.TransNoHiddenField.Value != "")
                {
                    POSTrHotelDt _posTrHotelDt = _hotelBL.GetSinglePOSTrHotelDt(_prmTransNo, Convert.ToInt32(_itemNo));
                    POSMsHotel _msHotel = this._hotelBL.GetSinglePOSMsHotel(_posTrHotelDt.HotelCode);

                    this.VoucherCodeTextBox.Text = _posTrHotelDt.VoucherNo;

                    this.HotelNameTextBox.Text = this._hotelBL.GetHotelNameByCode(_posTrHotelDt.HotelCode);
                    this.HotelNameTextBox.Visible = true;

                    //this.ShowHotelDDL(_msHotel.SuppCode);

                    //if (this.SuplierCodeHiddenField.Value == "")
                    //{
                    //    this.SupplierNameDLL.SelectedValue = _msHotel.SuppCode;
                    //    if (this.SupplierNameDLL.SelectedValue != "")
                    //    {
                    //        this.ShowHotelDDL();
                    //    }
                    //}
                    this.HotelHiddenField.Value = _msHotel.HotelCode;
                    this.CheckInDateTextBox.Text = DateFormMapper.GetValue(_posTrHotelDt.VoucherStartDate);
                    this.CheckOutDateTextBox.Text = DateFormMapper.GetValue(_posTrHotelDt.VoucherEndDate);
                    this.QtyRoomTextBox.Text = Convert.ToString(_posTrHotelDt.TotalRoom);
                    this.TotalTextBox.Text = Convert.ToDecimal(_posTrHotelDt.TotalBasicFare).ToString("#,##0.00");
                    this.BasicFareTextBox.Text = Convert.ToDecimal(_posTrHotelDt.BasicFare).ToString("#,##0.00");
                    this.DiscountTextBox.Text = Convert.ToDecimal(_posTrHotelDt.DiscountAmount).ToString("#,##0.00");
                    this.SellingPriceTextBox.Text = Convert.ToDecimal(_posTrHotelDt.SellingPrice).ToString("#,##0.00");
                    this.BuyingPriceTextBox.Text = Convert.ToDecimal(_posTrHotelDt.BuyingPrice).ToString("#,##0.00");
                    this.GuestTextBox.Text = _posTrHotelDt.GuestName;
                    this.VoucherInformationTextBox.Text = _posTrHotelDt.VoucherInformation;
                    this.RemarkDtTextBox.Text = _posTrHotelDt.Remark;
                }
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
                // if DO then back to Page DO
                //if (this.ReferenceNoHiddenField.Value.Substring(0,2) == "DO")
                bool _result = this._hotelBL.SendToCashierPOSTrHotel(this.TransNoTextBoxt.Text);
                if (_result == true)
                {

                    if (this.ReferenceNoHiddenField.Value != "")
                    {
                        Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
                    }
                    else
                    {
                        Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + "&" + this._code + "=");
                    }
                }
                //this.ClearDataDt();
                //this.ClearLabel();
                //this.TransNoTextBoxt.Text = "";
                //this.ShowDetail();
                //this.ShowButtonDt();
            }
            catch (ThreadAbortException) { throw; }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ResetButton_Click2(object sender, ImageClickEventArgs e)
        {
            this.TransNoHiddenField.Value = "";
            this.ItemNoHiddenField.Value = "";
            if (this.TransNoTextBoxt.Text != "")
            {
                Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBoxt.Text)));
            }
            else
            {
                Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + "&" + this._code + "=");
            }
        }

        protected void PrintPreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TransNoTextBoxt.Text != "")
                {
                    this.ChangeVisiblePanel(3);
                    //this.FormPanel.Visible = false;
                    //this.PrintPreviewPanel.Visible = true;
                    //this.ReasonListPanel.Visible = false;

                    this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                    ReportDataSource _reportDataSource1 = this._reportTourBL.HotelPrintPreview(this.TransNoTextBoxt.Text);
                    //ReportDataSource _reportDataSource2 = this._reportTourBL.HotelPrintPreview2();

                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                    //this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource2);

                    string _path = _reportListBL.GetPathPrintPreview(AppModule.GetValue(TransactionType.Hotel), _userBL.GetCompanyId(HttpContext.Current.User.Identity.Name));

                    string _bank_Account = this._ticketingBL.GetValueForReport("VoucherHotelPrintPreviewWithHeader.rdlc", "Bank_Account");
                    string _bank_Account2 = this._ticketingBL.GetValueForReport("VoucherHotelPrintPreviewWithoutHeader.rdlc", "Bank_Account");
                    string _address1 = this._ticketingBL.GetValueForReport("VoucherHotelPrintPreviewWithHeader.rdlc", "Header_Address1");
                    string _address2 = this._ticketingBL.GetValueForReport("VoucherHotelPrintPreviewWithHeader.rdlc", "Header_Address2");

                    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _path;
                    this.ReportViewer1.DataBind();

                    if (_path != "THotel /VoucherHotelPrintPreviewWithoutHeader.rdlc")
                    {
                        ReportParameter[] _reportParam = new ReportParameter[4];
                        _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBoxt.Text, true);
                        _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                        _reportParam[2] = new ReportParameter("Bank_Account", _bank_Account, true);
                        _reportParam[3] = new ReportParameter("Header_Address1", _address1, true);
                        this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                    }
                    else
                    {
                        ReportParameter[] _reportParam = new ReportParameter[2];
                        _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBoxt.Text, true);
                        _reportParam[1] = new ReportParameter("Bank_Account", _bank_Account2, true);
                        this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                    }
                    this.ReportViewer1.LocalReport.Refresh();
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void JoinJobOrderButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + "&" + this._code + "=");
        }

        protected void CheckStatusButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + "&" + this._code + "=");
        }

        protected void btnSearchCustomer_Click(object sender, ImageClickEventArgs e)
        {
            //Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + this.CustNameTextBox.Text + "-" + this.CustPhoneTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBoxt.Text)));

            if (this.ReferenceTextBox.Text != "")
            {
                if (this.TransNoHiddenField.Value != "")
                {
                    Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + "&" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBoxt.Text)));
                }
                else
                {
                    Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + "&" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBoxt.Text + "-" + this.ItemNoHiddenField.Value + "-" + this.ReferenceTextBox.Text + "-" + this.VoucherCodeTextBox.Text)));
                }
            }
            else
            {
                this.WarningLabel.Text = "Reference Name Invoice Must Be Filled";
            }
        }

        //protected void btnSearchHotel_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (this.TransNoTextBoxt.Text != "" && this.ReferenceNoHiddenField.Value != "" && this.ItemNoHiddenField.Value != "")
        //    {
        //        Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + this.CustNameTextBox.Text + "-" + this.CustPhoneTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBoxt.Text + "-" + this.ItemNoHiddenField.Value + "-" + this.ReferenceTextBox.Text + "-" + this.VoucherCodeTextBox.Text)));
        //    }
        //    if (this.TransNoTextBoxt.Text != "" && this.ReferenceNoHiddenField.Value != "" && this.ItemNoHiddenField.Value == "")
        //    {
        //        Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + this.CustNameTextBox.Text + "-" + this.CustPhoneTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBoxt.Text + "-" + this.ItemNoHiddenField.Value + "-" + this.ReferenceTextBox.Text + "-" + this.VoucherCodeTextBox.Text)));
        //    }
        //    if (this.TransNoTextBoxt.Text != "" && this.ReferenceNoHiddenField.Value == "" && this.ItemNoHiddenField.Value == "")
        //    {
        //        Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + "&" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBoxt.Text + "-" + this.ItemNoHiddenField.Value + "-" + this.ReferenceTextBox.Text + "-" + this.VoucherCodeTextBox.Text)));
        //    }
        //}

        protected void DeleteButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.ChangeVisiblePanel(2);
            //try
            //{
            //    if (this.TransNoHiddenField.Value != "" && this.ItemNoHiddenField.Value != "")
            //    {
            //        bool _resultDel = this._hotelBL.DeletePOSTrHotelDt(this.TransNoHiddenField.Value, this.ItemNoHiddenField.Value);
            //        if (_resultDel == true)
            //        {
            //            this.ClearDataDt();
            //            this.ShowData(this.TransNoHiddenField.Value);
            //            this.TransNoHiddenField.Value = "";
            //            this.ItemNoHiddenField.Value = "";
            //            Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBoxt.Text)));
            //        }
            //        else
            //        {
            //            this.WarningLabel.Text = "Delete Failed";
            //        }
            //    }
            //}
            //catch (ThreadAbortException) { throw; }
            //catch (Exception ex)
            //{
            //    this.errorhandler(ex);
            //}
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
                    _result = this._hotelBL.SetVOID(this.TransNoTextBoxt.Text, e.CommandArgument.ToString(), true);
                    if (_result == true)
                    {
                        if (this.ReferenceNoHiddenField.Value != "")
                            Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
                        else
                            Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + "&" + this._code + "=");

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Process Cancel Failed');", true);
                        this.ChangeVisiblePanel(0);
                        //this.FormPanel.Visible = true;
                        //this.ReasonListPanel.Visible = false;
                    }
                }
            }
            catch (ThreadAbortException) { throw; }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void CancelAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TransNoTextBoxt.Text != "")
                {
                    this.ChangeVisiblePanel(2);
                    //this.FormPanel.Visible = false;
                    //this.PrintPreviewPanel.Visible = false;
                    //this.ReasonListPanel.Visible = true;
                    //this.ReasonListRepeater.DataSource = this._reasonBL.GetList();
                    //this.ReasonListRepeater.DataBind();
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
            //this.FormPanel.Visible = true;
            //this.ReasonListPanel.Visible = false;
            //this.PrintPreviewPanel.Visible = false;
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            if (this.ReferenceNoHiddenField.Value != "")
            {
                Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "HOTEL");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }

        protected void ChangeVisiblePanel(Byte _prmValue)
        {
            if (_prmValue == 0)
            {
                this.FormPanel.Visible = true;
                this.ReasonListPanel.Visible = false;
                this.PasswordPanel.Visible = false;
                this.PrintPreviewPanel.Visible = false;
            }
            else if (_prmValue == 1)
            {
                this.FormPanel.Visible = false;
                this.ReasonListPanel.Visible = true;
                this.PasswordPanel.Visible = false;
                this.PrintPreviewPanel.Visible = false;
            }
            else if (_prmValue == 2)
            {
                this.FormPanel.Visible = true;
                this.ReasonListPanel.Visible = false;
                this.PasswordPanel.Visible = true;
                this.PrintPreviewPanel.Visible = false;
            }
            else if (_prmValue == 3)
            {
                this.FormPanel.Visible = false;
                this.ReasonListPanel.Visible = false;
                this.PasswordPanel.Visible = false;
                this.PrintPreviewPanel.Visible = true;
            }
        }

        protected void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                String _password = this._pOSConfigurationBL.GetSingle("POSCancelPassword").SetValue;
                if (_password.ToLower() == this.PasswordTextBox.Text.ToLower())
                {
                    if (this.TransNoHiddenField.Value != "" && this.ItemNoHiddenField.Value != "")
                    {
                        bool _resultDel = this._hotelBL.DeletePOSTrHotelDt(this.TransNoHiddenField.Value, this.ItemNoHiddenField.Value);
                        if (_resultDel == true)
                        {
                            this.ClearDataDt();
                            this.ShowData(this.TransNoHiddenField.Value);
                            this.TransNoHiddenField.Value = "";
                            this.ItemNoHiddenField.Value = "";
                            Response.Redirect("Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBoxt.Text)));
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
            catch (ThreadAbortException) { throw; }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }
    }
}
