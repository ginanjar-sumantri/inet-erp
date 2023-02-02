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
    public partial class TicketingDetailAdd : TicketingBase
    {
        private TicketingBL _ticketingBL = new TicketingBL();

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
            this.TotalTextBox.Attributes.Add("ReadOnly", "True");
            this.BasicFareTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            //this.QtyGuestTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");            
            this.DiscountTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.SellingPriceTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.BuyingPriceTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");            
            this.QtyGuestTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.QtyGuestTextBox.Attributes.Add("OnBlur", "Calculate(" + this.QtyGuestTextBox.ClientID + "," + this.BasicFareTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + ","+ this.DiscountTextBox.ClientID + ")");
            this.BasicFareTextBox.Attributes.Add("OnBlur", "Calculate(" + this.QtyGuestTextBox.ClientID + "," + this.BasicFareTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + "," + this.DiscountTextBox.ClientID + ")");
            this.DiscountTextBox.Attributes.Add("OnBlur", "Calculate2(" + this.DiscountTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + ")");
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
            this.TotalTextBox.Text = "0";
            this.QtyGuestTextBox.Text = "0";
            this.FlightInformationTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            POSTrTicketingDt _posTrTicketingDt = new POSTrTicketingDt();

            Double _posTicketDtCount = _ticketingBL.RowsCountPOSTrTicketingDt(_transNo);

            _posTrTicketingDt.TransNmbr = _transNo;
            _posTrTicketingDt.ItemNo = Convert.ToInt32(_posTicketDtCount + 1);
            _posTrTicketingDt.KodeBooking = this.BookingCodeTextBox.Text;
            _posTrTicketingDt.TicketType = this.TicketTypeRadioButtonList.SelectedValue;
            _posTrTicketingDt.TicketDate = DateFormMapper.GetValue(this.DateTextBox.Text);
            _posTrTicketingDt.AirlineCode = this.AirlineHiddenField.Value;
            _posTrTicketingDt.BasicFare = this.BasicFareTextBox.Text == "" ? 0 : Convert.ToDecimal(this.BasicFareTextBox.Text);
            _posTrTicketingDt.GuestName = this.GuestTextBox.Text;
            _posTrTicketingDt.TotalGuest = Convert.ToInt32(this.QtyGuestTextBox.Text);
            _posTrTicketingDt.TotalBasicFare = Convert.ToInt32(this.TotalTextBox.Text);
            _posTrTicketingDt.DiscountAmount = this.DiscountTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscountTextBox.Text);
            _posTrTicketingDt.SellingPrice = this.SellingPriceTextBox.Text == "" ? 0 : Convert.ToDecimal(this.SellingPriceTextBox.Text);
            _posTrTicketingDt.BuyingPrice = this.BuyingPriceTextBox.Text == "" ? 0 : Convert.ToDecimal(this.BuyingPriceTextBox.Text);
            _posTrTicketingDt.FlightInformation = this.FlightInformationTextBox.Text;
            _posTrTicketingDt.InsertBy = HttpContext.Current.User.Identity.Name;
            _posTrTicketingDt.InsertDate = DateTime.Now;
            _posTrTicketingDt.EditBy = HttpContext.Current.User.Identity.Name;
            _posTrTicketingDt.EditDate = DateTime.Now;

            bool _result = this._ticketingBL.AddPOSTrTicketingDt(_posTrTicketingDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
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
    }
}
