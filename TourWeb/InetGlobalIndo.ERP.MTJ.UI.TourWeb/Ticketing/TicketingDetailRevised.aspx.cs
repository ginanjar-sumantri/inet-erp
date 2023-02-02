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
    public partial class TicketingDetailRevised : TicketingBase
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

                this.ClearData();
                this.ShowData();

            }
        }

        private void SetAttribut()
        {
            this.BuyingPriceTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
            //this.SellingPriceTextBox.Attributes.Add("OnKeyUp", "if (isNaN(this.value) == true) this.value = 0;");
        }

        private void ShowData()
        {
            if (this.CodeHiddenField.Value != "")
            {
                String[] _tansCode = this.CodeHiddenField.Value.Split('|');
                POSTrTicketingDt _posTrTicketingDt = _ticketingBL.GetSinglePOSTrTicketingDt(_tansCode[0], Convert.ToInt32(_tansCode[1]));

                MsAirline _msAirline = _airlineBL.GetSingleAirLine(_posTrTicketingDt.AirlineCode);

                this.BookingCodeLabel.Text = _posTrTicketingDt.KodeBooking;

                //this.SellingPriceTextBox.Text = Convert.ToDecimal(_posTrTicketingDt.SellingPrice).ToString("#,##0.00");
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

            this.BookingCodeLabel.Text = "";
            //this.SellingPriceTextBox.Text = "0";
            this.SellingPriceHiddenField.Value = "0";
            this.BuyingPriceTextBox.Text = "0";
        }

        protected void YesButton_Click(object sender, EventArgs e)
        {
            string _error = "";

            if (this.CodeHiddenField.Value != "")
            {
                String[] _tansCode = this.CodeHiddenField.Value.Split('|');
                string _revised = _ticketingBL.Revised(_tansCode[0], Convert.ToInt32(_tansCode[1]), Convert.ToDecimal(this.BuyingPriceTextBox.Text), Convert.ToDecimal(this.SellingPriceHiddenField.Value), HttpContext.Current.User.Identity.Name);

                if (_revised == "")
                {
                    _error = "Resived Success";
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
                }
                else
                {
                    this.WarningLabel.Text = "Resived Failed";
                }
            }
        }

        protected void NoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}
