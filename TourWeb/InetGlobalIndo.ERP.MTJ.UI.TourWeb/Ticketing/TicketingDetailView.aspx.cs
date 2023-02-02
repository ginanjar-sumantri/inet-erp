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
    public partial class TicketingDetailView : TicketingBase
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.CodeHiddenField.Value = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey) + '|' + Rijndael.Decrypt(this._nvcExtractor.GetValue(this._itemCode), ApplicationConfig.EncryptionKey);

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                //this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                //this.btnSearchAirline.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findAirline&configCode=airline','_popSearch','width=1200,height=1000,toolbar=0,location=0,status=0,scrollbars=1')";

                String spawnJS = "<script language='JavaScript'>\n";

                spawnJS += "function findAirline(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.AirlineTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                //this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                //this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearData();
                //this.SetAttribut();
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
                this.TicketTypeTextBox.Text = _posTrTicketingDt.TicketType;
                this.AirlineTextBox.Text = _msAirline.AirlineName;
                //this.AirlineHiddenField.Value = _msAirline.AirlineCode; 
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
         
        public void ClearData()
        {
            this.ClearLabel();

            this.BookingCodeTextBox.Text = "";
            this.TicketTypeTextBox.Text = "";
            this.AirlineTextBox.Text = "";
            this.GuestTextBox.Text = "";
            //this.DateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.BasicFareTextBox.Text = "0";
            this.DiscountTextBox.Text = "0";
            this.SellingPriceTextBox.Text = "0";
            this.BuyingPriceTextBox.Text = "0";
            this.FlightInformationTextBox.Text = "";
        }        

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }        
    }
}
