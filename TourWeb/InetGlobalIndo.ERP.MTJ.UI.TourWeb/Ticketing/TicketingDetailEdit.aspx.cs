using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;

namespace InetGlobalIndo.ERP.MTJ.UI.Tour.Ticketing
{
    public partial class TicketingDetailEdit : TicketingBase
    {
        private TicketingBL _ticketingBL = new TicketingBL();
        private AirLineBL _airlineBL = new AirLineBL();
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
                this.CodeHiddenField.Value = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey) + '|' + Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemCode), ApplicationConfig.EncryptionKey);

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.btnSearchAirline.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findAirline&configCode=airline','_popSearch','width=1200,height=1000,toolbar=0,location=0,status=0,scrollbars=1')";

                String spawnJS = "<script language='JavaScript'>\n";

                spawnJS += "function findAirline(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.AirlineHiddenField.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.AirlineTextBox.ClientID + "').value = dataArray[1];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearData();
                this.SetAttribut();
                this.ShowData();

            }
        }

        private void ShowData()
        {
            if (this.CodeHiddenField.Value != "")
            {
                String[] _tansCode = this.CodeHiddenField.Value.Split('|');
                POSTrTicketingDt _posTrTicketingDt = _ticketingBL.GetSinglePOSTrTicketingDt(_tansCode[0], Convert.ToInt32(_tansCode[1]));

                MsAirline _msAirline = _airlineBL.GetSingleAirLine(_posTrTicketingDt.AirlineCode);

                this.BookingCodeTextBox.Text = _posTrTicketingDt.KodeBooking;
                this.TicketTypeRadioButtonList.SelectedValue = _posTrTicketingDt.TicketType;
                this.AirlineTextBox.Text = _msAirline.AirlineName;
                this.AirlineHiddenField.Value = _msAirline.AirlineCode;
                this.GuestTextBox.Text = _posTrTicketingDt.GuestName;
                this.QtyGuestTextBox.Text = _posTrTicketingDt.TotalGuest.ToString();
                this.TotalTextBox.Text = Convert.ToDecimal(_posTrTicketingDt.TotalBasicFare).ToString("#,##0.00");
                this.DateTextBox.Text = DateFormMapper.GetValue(_posTrTicketingDt.TicketDate);
                this.BasicFareTextBox.Text = Convert.ToDecimal(_posTrTicketingDt.BasicFare).ToString("#,##0.00");
                this.DiscountTextBox.Text = Convert.ToDecimal(_posTrTicketingDt.DiscountAmount).ToString("#,##0.00");
                this.FlightInformationTextBox.Text = _posTrTicketingDt.FlightInformation;
                this.SellingPriceTextBox.Text = Convert.ToDecimal(_posTrTicketingDt.SellingPrice).ToString("#,##0.00");
                this.BuyingPriceTextBox.Text = Convert.ToDecimal(_posTrTicketingDt.BuyingPrice).ToString("#,##0.00");

            }

        }


        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribut()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.AirlineTextBox.Attributes.Add("ReadOnly", "True");
            this.BasicFareTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.DiscountTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.SellingPriceTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.BuyingPriceTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.QtyGuestTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.QtyGuestTextBox.Attributes.Add("OnBlur", "Calculate(" + this.QtyGuestTextBox.ClientID + "," + this.BasicFareTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + "," + this.DiscountTextBox.ClientID + ")");
            this.BasicFareTextBox.Attributes.Add("OnBlur", "Calculate(" + this.QtyGuestTextBox.ClientID + "," + this.BasicFareTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + "," + this.DiscountTextBox.ClientID + ")");
            this.DiscountTextBox.Attributes.Add("Onblur", "Calculate2(" + this.DiscountTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + ")");

        }

        public void ClearData()
        {
            this.ClearLabel();

            this.BookingCodeTextBox.Text = "";
            this.TicketTypeRadioButtonList.SelectedValue = "Domestic";
            this.AirlineTextBox.Text = "";
            this.GuestTextBox.Text = "";
            this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.BasicFareTextBox.Text = "0";
            this.DiscountTextBox.Text = "0";
            this.SellingPriceTextBox.Text = "0";
            this.BuyingPriceTextBox.Text = "0";
            this.FlightInformationTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.CodeHiddenField.Value != "")
            {
                String[] _tansCode = this.CodeHiddenField.Value.Split('|');
                POSTrTicketingDt _posTrTicketingDt = _ticketingBL.GetSinglePOSTrTicketingDt(_tansCode[0], Convert.ToInt32(_tansCode[1]));
                POSTrTicketingHd _posTrTicketingHd = _ticketingBL.GetSinglePOSTrTicketingHd(_tansCode[0]);

                decimal _subTotalForex = Convert.ToDecimal(_posTrTicketingHd.SubTotalForex - _posTrTicketingDt.SellingPrice);

                string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

                Double _posTicketDtCount = _ticketingBL.RowsCountPOSTrTicketingDt(_transNo);

                _posTrTicketingDt.KodeBooking = this.BookingCodeTextBox.Text;
                _posTrTicketingDt.TicketType = this.TicketTypeRadioButtonList.SelectedValue;
                _posTrTicketingDt.TicketDate = DateFormMapper.GetValue(this.DateTextBox.Text);
                _posTrTicketingDt.AirlineCode = this.AirlineHiddenField.Value;
                _posTrTicketingDt.BasicFare = this.BasicFareTextBox.Text == "" ? 0 : Convert.ToDecimal(this.BasicFareTextBox.Text);
                _posTrTicketingDt.GuestName = this.GuestTextBox.Text;
                _posTrTicketingDt.DiscountAmount = this.DiscountTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscountTextBox.Text);
                _posTrTicketingDt.SellingPrice = this.SellingPriceTextBox.Text == "" ? 0 : Convert.ToDecimal(this.SellingPriceTextBox.Text);
                _posTrTicketingDt.BuyingPrice = this.BuyingPriceTextBox.Text == "" ? 0 : Convert.ToDecimal(this.BuyingPriceTextBox.Text);
                _posTrTicketingDt.FlightInformation = this.FlightInformationTextBox.Text;
                _posTrTicketingDt.EditBy = HttpContext.Current.User.Identity.Name;
                _posTrTicketingDt.EditDate = DateTime.Now;

                bool _result = this._ticketingBL.EditPOSTrTicketingDt(_posTrTicketingDt, _subTotalForex, this.SellingPriceTextBox.Text);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Add Data";
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
            this.ShowData();
        }
    }
}
