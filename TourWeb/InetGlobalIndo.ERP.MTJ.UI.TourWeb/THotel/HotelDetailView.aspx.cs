using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;

namespace InetGlobalIndo.ERP.MTJ.UI.Tour.THotel
{
    public partial class HotelDetailView : THotelBase
    {
        private HotelBL _ticketingBL = new HotelBL();
        private HotelBL _hotelBL = new HotelBL();
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

                //this.CheckInDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.CheckInDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                //this.CheckOutDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.CheckOutDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowData();
            }
        }

        private void ShowData()
        {
            if (this.CodeHiddenField.Value != "")
            {
                String[] _tansCode = this.CodeHiddenField.Value.Split('|');
                POSTrHotelDt _posTrHotelDt = _ticketingBL.GetSinglePOSTrHotelDt(_tansCode[0], Convert.ToInt32(_tansCode[1]));
                POSMsHotel _msHotel = this._hotelBL.GetSinglePOSMsHotel(_posTrHotelDt.HotelCode);

                this.VoucherNoTextBox.Text = _posTrHotelDt.VoucherNo;
                this.HotelTextBox.Text = _msHotel.HotelName;
                this.CheckInDateTextBox.Text = DateFormMapper.GetValue(_posTrHotelDt.VoucherStartDate);
                this.CheckOutDateTextBox.Text = DateFormMapper.GetValue(_posTrHotelDt.VoucherEndDate);
                this.QtyRoomTextBox.Text = Convert.ToDecimal(_posTrHotelDt.TotalRoom).ToString("#,##0.00");
                this.TotalTextBox.Text = Convert.ToDecimal(_posTrHotelDt.TotalBasicFare).ToString("#,##0.00");
                this.BasicFareTextBox.Text = Convert.ToDecimal(_posTrHotelDt.BasicFare).ToString("#,##0.00");
                this.DiscountTextBox.Text = Convert.ToDecimal(_posTrHotelDt.DiscountAmount).ToString("#,##0.00");
                this.SellingPriceTextBox.Text = Convert.ToDecimal(_posTrHotelDt.SellingPrice).ToString("#,##0.00");
                this.BuyingPriceTextBox.Text = Convert.ToDecimal(_posTrHotelDt.BuyingPrice).ToString("#,##0.00");
                this.GuestTextBox.Text = _posTrHotelDt.GuestName;
                this.VoucherInformationTextBox.Text = _posTrHotelDt.VoucherInformation;
                this.RemarkTextBox.Text = _posTrHotelDt.Remark;
            }
        }
   
        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

       
    }
}
