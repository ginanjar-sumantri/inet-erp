﻿using System;
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

namespace InetGlobalIndo.ERP.MTJ.UI.Tour.THotel
{
    public partial class HotelDetailAdd : THotelBase
    {
        private HotelBL _ticketingBL = new HotelBL();
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

                this.CheckInDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.CheckInDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.CheckOutDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.CheckOutDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.btnSearchHotel.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findHotel&configCode=hotel','_popSearch','width=1200,height=1000,toolbar=0,location=0,status=0,scrollbars=1')";

                String spawnJS = "<script language='JavaScript'>\n";

                spawnJS += "function findHotel(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.HotelHiddenField.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.HotelTextBox.ClientID + "').value = dataArray[1];\n";
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
            this.CheckInDateTextBox.Attributes.Add("ReadOnly", "True");
            this.CheckOutDateTextBox.Attributes.Add("ReadOnly", "True");
            this.HotelTextBox.Attributes.Add("ReadOnly", "True");
            this.SellingPriceTextBox.Attributes.Add("ReadOnly", "True");
            this.BasicFareTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.DiscountTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.SellingPriceTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.BuyingPriceTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            this.QtyRoomTextBox.Attributes.Add("OnKeyUp", "if(isNaN(this.value) == true) this.value = 0 ;");
            this.TotalTextBox.Attributes.Add("OnKeyUp", "if(isNaN(this.value) == true) this.value = 0;");
            this.QtyRoomTextBox.Attributes.Add("OnBLur", "Calculate(" + this.QtyRoomTextBox.ClientID + "," + this.BasicFareTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + "," + this.DiscountTextBox.ClientID + ")");
            this.BasicFareTextBox.Attributes.Add("OnBlur", "Calculate(" + this.QtyRoomTextBox.ClientID + "," + this.BasicFareTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + "," + this.DiscountTextBox.ClientID + ")");
            this.DiscountTextBox.Attributes.Add("OnBlur", "Calculate2(" + this.DiscountTextBox.ClientID + "," + this.TotalTextBox.ClientID + "," + this.SellingPriceTextBox.ClientID + ")");
        }

        public void ClearData()
        {
            this.ClearLabel();

            this.VoucherNoTextBox.Text = "";
            this.HotelTextBox.Text = "";
            this.HotelHiddenField.Value = "";
            this.CheckInDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.CheckOutDateTextBox.Text = DateFormMapper.GetValue(DateTime.Now);
            this.QtyRoomTextBox.Text = "";
            this.BasicFareTextBox.Text = "0";
            this.TotalTextBox.Text = "0";
            this.DiscountTextBox.Text = "0";
            this.SellingPriceTextBox.Text = "0";
            this.BuyingPriceTextBox.Text = "0";
            this.GuestTextBox.Text = "";
            this.VoucherInformationTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            POSTrHotelDt _posTrHotelDt = new POSTrHotelDt();

            Double _posTicketDtCount = _ticketingBL.RowsCountPOSTrHotelDt(_transNo);

            _posTrHotelDt.TransNmbr = _transNo;
            _posTrHotelDt.ItemNo = Convert.ToInt32(_posTicketDtCount + 1);
            _posTrHotelDt.VoucherNo = this.VoucherNoTextBox.Text;
            _posTrHotelDt.VoucherStartDate = DateFormMapper.GetValue(this.CheckInDateTextBox.Text);
            _posTrHotelDt.VoucherEndDate = DateFormMapper.GetValue(this.CheckOutDateTextBox.Text);
            _posTrHotelDt.HotelCode = this.HotelHiddenField.Value;
            _posTrHotelDt.TotalRoom = Convert.ToInt32(this.QtyRoomTextBox.Text);
            _posTrHotelDt.BasicFare = this.BasicFareTextBox.Text == "" ? 0 : Convert.ToDecimal(this.BasicFareTextBox.Text);
            _posTrHotelDt.TotalBasicFare = this.TotalTextBox.Text == "" ? 0 : Convert.ToDecimal(this.TotalTextBox.Text);
            _posTrHotelDt.DiscountAmount = this.DiscountTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscountTextBox.Text);
            _posTrHotelDt.SellingPrice = this.SellingPriceTextBox.Text == "" ? 0 : Convert.ToDecimal(this.SellingPriceTextBox.Text);
            _posTrHotelDt.BuyingPrice = this.BuyingPriceTextBox.Text == "" ? 0 : Convert.ToDecimal(this.BuyingPriceTextBox.Text);
            _posTrHotelDt.GuestName = this.GuestTextBox.Text;
            _posTrHotelDt.VoucherInformation = this.VoucherInformationTextBox.Text;
            _posTrHotelDt.Remark = this.RemarkTextBox.Text;
            _posTrHotelDt.InsertBy = HttpContext.Current.User.Identity.Name;
            _posTrHotelDt.InsertDate = DateTime.Now;
            _posTrHotelDt.EditBy = HttpContext.Current.User.Identity.Name;
            _posTrHotelDt.EditDate = DateTime.Now;

            bool _result = this._ticketingBL.AddPOSTrHotelDt(_posTrHotelDt);

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
