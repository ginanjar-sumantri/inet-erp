using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using BusinessRule.POS;

namespace InetGlobalIndo.ERP.MTJ.UI.POSInterface.General
{
    public partial class CheckStatus : System.Web.UI.Page
    {
        private POSRetailBL _retailBL = new POSRetailBL();
        private POSInternetBL _internetBL = new POSInternetBL();
        private TicketingBL _ticketingBL = new TicketingBL();
        private POSCafeBL _cafeBL = new POSCafeBL();
        private AirLineBL _airLineBL = new AirLineBL();
        private HotelBL _hotelBL = new HotelBL();
        private POSPrintingBL _printingBL = new POSPrintingBL();
        private POSPhotocopyBL _photocopyBL = new POSPhotocopyBL();
        private POSGraphicBL _graphicBL = new POSGraphicBL();
        private POSShippingBL _shippingBL = new POSShippingBL();
        private UnitBL _unitBL = new UnitBL();
        
        private CountryBL _countryBL = new CountryBL();
        private VendorBL _vendorBL = new VendorBL();
        private CityBL _cityBL = new CityBL();

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                String spawnJS = "<script language='JavaScript'>\n";
                spawnJS += "function returnValue (val) {\n";
                spawnJS += "window.opener." + Request.QueryString["valueCatcher"] + "(val);\n";
                spawnJS += "window.close();\n";
                spawnJS += "}\n";
                spawnJS += "</script>";
                this.javaScriptDeclaration.Text = spawnJS;

                this.CloseButton.OnClientClick = "window.close()";
                this.ShowData();
            }
        }

        public void ShowData()
        {
            try
            {
                if (Request.QueryString["pos"].ToString() == "retail")
                {
                    this.CheckStatusListRepeater.DataSource = this._retailBL.GetListRetailPayNotDelivered(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                    this.CheckStatusListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "internet")
                {
                    this.CheckStatusListRepeater.DataSource = this._internetBL.GetListInternetPayNotDelivered(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                    this.CheckStatusListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "cafe")
                {
                    this.CheckStatusListRepeater.DataSource = this._cafeBL.GetListCafePayNotDelivered(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                    this.CheckStatusListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "ticketing")
                {
                    this.CheckStatusListRepeater.DataSource = this._ticketingBL.GetListTicketingPayNotDelivered(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                    this.CheckStatusListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "hotel")
                {
                    this.CheckStatusListRepeater.DataSource = this._hotelBL.GetListHotelPayNotDelivered(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                    this.CheckStatusListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "printing")
                {
                    this.CheckStatusListRepeater.DataSource = this._printingBL.GetListPrintingPayNotDelivered(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                    this.CheckStatusListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "photocopy")
                {
                    this.CheckStatusListRepeater.DataSource = this._photocopyBL.GetListPhotocopyPayNotDelivered(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                    this.CheckStatusListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "graphic")
                {
                    this.CheckStatusListRepeater.DataSource = this._graphicBL.GetListGraphicPayNotDelivered(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                    this.CheckStatusListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "shipping")
                {
                    this.CheckStatusListRepeater.DataSource = this._shippingBL.GetListShippingPayNotDelivered(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                    this.CheckStatusListRepeater.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.CustIDTextBox.Text = "";
            this.SearchFieldTextBox.Text = "";
            this.ShowData();
        }

        protected void CheckStatusListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    if (Request.QueryString["pos"].ToString() == "retail")
                    {
                        POSTrRetailHd _temp = (POSTrRetailHd)e.Item.DataItem;

                        string _code = _temp.TransNmbr.ToString();

                        Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                        _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                        Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                        _no = _page * _maxrow;
                        _no += 1;
                        _no = _nomor + _no;
                        _noLiteral.Text = _no.ToString();
                        _nomor += 1;

                        Literal _jobCodeLiteral = (Literal)e.Item.FindControl("TransNumLiteral");
                        _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                        String _settlementNo = this._retailBL.GetTransnumbSettlement(_temp.TransNmbr);
                        Literal _setttlementLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                        _setttlementLiteral.Text = HttpUtility.HtmlEncode(_settlementNo);

                        Char _doneSettlement = (_temp.DoneSettlement == null) ? 'N' : Convert.ToChar(_temp.DoneSettlement);
                        Literal _doneSettlementLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                        _doneSettlementLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_doneSettlement));

                        //String _deliveryStatus = (_temp.DeliveryStatus == null || _temp.DeliveryStatus == false) ? "0" : "1";
                        //Literal _deliveryStatusLiteral = (Literal)e.Item.FindControl("DeliveryStatusLiteral");
                        //_deliveryStatusLiteral.Text = (_deliveryStatus == "0") ? "Not Yet Delivered" : "Delivered";

                        Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                        _memberNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                        Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                        DateTime _date = Convert.ToDateTime(_temp.TransDate);
                        _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                        //Decimal _dpForex = (_temp.DPForex == null) ? 0 : Convert.ToDecimal(_temp.DPForex);
                        //Literal _dpForexLiteral = (Literal)e.Item.FindControl("DPForexLiteral");
                        //_dpForexLiteral.Text = HttpUtility.HtmlEncode(_dpForex.ToString("#,##0.00"));

                        //Decimal _dpPaid = (_temp.DPForex == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                        Decimal _dpPaid = (Convert.ToDecimal(_temp.DPPaid) == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                        Literal _dpPaidLiteral = (Literal)e.Item.FindControl("DPPaidLiteral");
                        _dpPaidLiteral.Text = HttpUtility.HtmlEncode(_dpPaid.ToString("#,##0.00"));

                        ImageButton _changeButton = (ImageButton)e.Item.FindControl("ChangeImageButton");
                        _changeButton.Attributes.Add("OnClick", "return CancelOrder();");
                        _changeButton.CommandName = "ChangeDeliveryStatus";
                        _changeButton.CommandArgument = _code;
                        if (_doneSettlement == 'N')
                        {
                            _changeButton.Visible = false;
                        }

                        ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewImageButton");
                        _viewDetailButton.CommandName = "ViewDetail";
                        _viewDetailButton.CommandArgument = _code + "," + _temp.TransType;
                    }
                    else if (Request.QueryString["pos"].ToString() == "internet")
                    {
                        POSTrInternetHd _temp = (POSTrInternetHd)e.Item.DataItem;

                        string _code = _temp.TransNmbr.ToString();

                        Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                        _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                        Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                        _no = _page * _maxrow;
                        _no += 1;
                        _no = _nomor + _no;
                        _noLiteral.Text = _no.ToString();
                        _nomor += 1;

                        Literal _jobCodeLiteral = (Literal)e.Item.FindControl("TransNumLiteral");
                        _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                        String _settlementNo = this._internetBL.GetTransnumbSettlement(_temp.TransNmbr);
                        Literal _setttlementLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                        _setttlementLiteral.Text = HttpUtility.HtmlEncode(_settlementNo);

                        Char _doneSettlement = (_temp.DoneSettlement == null) ? 'N' : Convert.ToChar(_temp.DoneSettlement);
                        Literal _doneSettlementLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                        _doneSettlementLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_doneSettlement));

                        //String _deliveryStatus = (_temp.DeliveryStatus == null || _temp.DeliveryStatus == false) ? "0" : "1";
                        //Literal _deliveryStatusLiteral = (Literal)e.Item.FindControl("DeliveryStatusLiteral");
                        //_deliveryStatusLiteral.Text = (_deliveryStatus == "0") ? "Not Yet Delivered" : "Delivered";

                        Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                        _memberNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                        Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                        DateTime _date = Convert.ToDateTime(_temp.TransDate);
                        _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                        //Decimal _dpForex = (_temp.DPForex == null) ? 0 : Convert.ToDecimal(_temp.DPForex);
                        //Literal _dpForexLiteral = (Literal)e.Item.FindControl("DPForexLiteral");
                        //_dpForexLiteral.Text = HttpUtility.HtmlEncode(_dpForex.ToString("#,##0.00"));

                        Decimal _dpPaid = (_temp.DPPaid == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                        Literal _dpPaidLiteral = (Literal)e.Item.FindControl("DPPaidLiteral");
                        _dpPaidLiteral.Text = HttpUtility.HtmlEncode(_dpPaid.ToString("#,##0.00"));

                        ImageButton _changeButton = (ImageButton)e.Item.FindControl("ChangeImageButton");
                        _changeButton.Attributes.Add("OnClick", "return CancelOrder();");
                        _changeButton.CommandName = "ChangeDeliveryStatus";
                        _changeButton.CommandArgument = _code;
                        if (_doneSettlement == 'N')
                        {
                            _changeButton.Visible = false;
                        }

                        ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewImageButton");
                        _viewDetailButton.CommandName = "ViewDetail";
                        _viewDetailButton.CommandArgument = _code + "," + _temp.TransType;
                    }
                    else if (Request.QueryString["pos"].ToString() == "cafe")
                    {
                        POSTrCafeHd _temp = (POSTrCafeHd)e.Item.DataItem;

                        string _code = _temp.TransNmbr.ToString();

                        Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                        _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                        Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                        _no = _page * _maxrow;
                        _no += 1;
                        _no = _nomor + _no;
                        _noLiteral.Text = _no.ToString();
                        _nomor += 1;

                        Literal _jobCodeLiteral = (Literal)e.Item.FindControl("TransNumLiteral");
                        _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                        String _settlementNo = this._cafeBL.GetTransnumbSettlement(_temp.TransNmbr);
                        Literal _setttlementLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                        _setttlementLiteral.Text = HttpUtility.HtmlEncode(_settlementNo);

                        Char _doneSettlement = (_temp.DoneSettlement == null) ? 'N' : Convert.ToChar(_temp.DoneSettlement);
                        Literal _doneSettlementLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                        _doneSettlementLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_doneSettlement));

                        Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                        _memberNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                        Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                        DateTime _date = Convert.ToDateTime(_temp.TransDate);
                        _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                        //Decimal _dpPaid = (_temp.DPForex == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                        Decimal _dpPaid = (Convert.ToDecimal(_temp.DPPaid) == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                        Literal _dpPaidLiteral = (Literal)e.Item.FindControl("DPPaidLiteral");
                        _dpPaidLiteral.Text = HttpUtility.HtmlEncode(_dpPaid.ToString("#,##0.00"));

                        ImageButton _changeButton = (ImageButton)e.Item.FindControl("ChangeImageButton");
                        _changeButton.Attributes.Add("OnClick", "return CancelOrder();");
                        _changeButton.CommandName = "ChangeDeliveryStatus";
                        _changeButton.CommandArgument = _code;
                        if (_doneSettlement == 'N')
                        {
                            _changeButton.Visible = false;
                        }

                        ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewImageButton");
                        _viewDetailButton.CommandName = "ViewDetail";
                        _viewDetailButton.CommandArgument = _code + "," + _temp.TransType;
                    }
                    else if (Request.QueryString["pos"].ToString() == "ticketing")
                    {
                        POSTrTicketingHd _temp = (POSTrTicketingHd)e.Item.DataItem;

                        string _code = _temp.TransNmbr.ToString();

                        Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                        _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                        Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                        _no = _page * _maxrow;
                        _no += 1;
                        _no = _nomor + _no;
                        _noLiteral.Text = _no.ToString();
                        _nomor += 1;

                        Literal _jobCodeLiteral = (Literal)e.Item.FindControl("TransNumLiteral");
                        _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                        String _settlementNo = this._ticketingBL.GetTransnumbSettlement(_temp.TransNmbr);
                        Literal _setttlementLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                        _setttlementLiteral.Text = HttpUtility.HtmlEncode(_settlementNo);


                        Char _doneSettlement = (_temp.DoneSettlement == null) ? 'N' : Convert.ToChar(_temp.DoneSettlement);
                        Literal _doneSettlementLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                        _doneSettlementLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_doneSettlement));

                        Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                        _memberNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                        Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                        DateTime _date = Convert.ToDateTime(_temp.TransDate);
                        _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                        //Decimal _dpPaid = (_temp.DPForex == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                        Decimal _dpPaid = (Convert.ToDecimal(_temp.DPPaid) == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                        Literal _dpPaidLiteral = (Literal)e.Item.FindControl("DPPaidLiteral");
                        _dpPaidLiteral.Text = HttpUtility.HtmlEncode(_dpPaid.ToString("#,##0.00"));

                        ImageButton _changeButton = (ImageButton)e.Item.FindControl("ChangeImageButton");
                        _changeButton.Attributes.Add("OnClick", "return CancelOrder();");
                        _changeButton.CommandName = "ChangeDeliveryStatus";
                        _changeButton.CommandArgument = _code;
                        if (_doneSettlement == 'N' | _temp.DeliveryStatus == true)
                        {
                            _changeButton.Visible = false;
                        }

                        ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewImageButton");
                        _viewDetailButton.CommandName = "ViewDetail";
                        _viewDetailButton.CommandArgument = _code + "," + _temp.TransType;
                    }
                    else if (Request.QueryString["pos"].ToString() == "hotel")
                    {
                        POSTrHotelHd _temp = (POSTrHotelHd)e.Item.DataItem;

                        string _code = _temp.TransNmbr.ToString();

                        Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                        _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                        Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                        _no = _page * _maxrow;
                        _no += 1;
                        _no = _nomor + _no;
                        _noLiteral.Text = _no.ToString();
                        _nomor += 1;

                        Literal _jobCodeLiteral = (Literal)e.Item.FindControl("TransNumLiteral");
                        _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                        String _settlementNo = this._hotelBL.GetTransnumbSettlement(_temp.TransNmbr);
                        Literal _setttlementLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                        _setttlementLiteral.Text = HttpUtility.HtmlEncode(_settlementNo);

                        Char _doneSettlement = (_temp.DoneSettlement == null) ? 'N' : Convert.ToChar(_temp.DoneSettlement);
                        Literal _doneSettlementLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                        _doneSettlementLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_doneSettlement));

                        Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                        _memberNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                        Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                        DateTime _date = Convert.ToDateTime(_temp.TransDate);
                        _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                        //Decimal _dpPaid = (_temp.DPForex == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                        Decimal _dpPaid = (Convert.ToDecimal(_temp.DPPaid) == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                        Literal _dpPaidLiteral = (Literal)e.Item.FindControl("DPPaidLiteral");
                        _dpPaidLiteral.Text = HttpUtility.HtmlEncode(_dpPaid.ToString("#,##0.00"));

                        ImageButton _changeButton = (ImageButton)e.Item.FindControl("ChangeImageButton");
                        _changeButton.Attributes.Add("OnClick", "return CancelOrder();");
                        _changeButton.CommandName = "ChangeDeliveryStatus";
                        _changeButton.CommandArgument = _code;
                        if (_doneSettlement == 'N' | _temp.DeliveryStatus == true)
                        {
                            _changeButton.Visible = false;
                        }

                        ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewImageButton");
                        _viewDetailButton.CommandName = "ViewDetail";
                        _viewDetailButton.CommandArgument = _code + "," + _temp.TransType;
                    }
                    else if (Request.QueryString["pos"].ToString() == "printing")
                    {
                        POSTrPrintingHd _temp = (POSTrPrintingHd)e.Item.DataItem;

                        string _code = _temp.TransNmbr.ToString();

                        Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                        _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                        Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                        _no = _page * _maxrow;
                        _no += 1;
                        _no = _nomor + _no;
                        _noLiteral.Text = _no.ToString();
                        _nomor += 1;

                        Literal _jobCodeLiteral = (Literal)e.Item.FindControl("TransNumLiteral");
                        _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                        String _settlementNo = this._printingBL.GetTransnumbSettlement(_temp.TransNmbr);
                        Literal _setttlementLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                        _setttlementLiteral.Text = HttpUtility.HtmlEncode(_settlementNo);

                        Char _doneSettlement = (_temp.DoneSettlement == null) ? 'N' : Convert.ToChar(_temp.DoneSettlement);
                        Literal _doneSettlementLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                        _doneSettlementLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_doneSettlement));

                        //String _deliveryStatus = (_temp.DeliveryStatus == null || _temp.DeliveryStatus == false) ? "0" : "1";
                        //Literal _deliveryStatusLiteral = (Literal)e.Item.FindControl("DeliveryStatusLiteral");
                        //_deliveryStatusLiteral.Text = (_deliveryStatus == "0") ? "Not Yet Delivered" : "Delivered";

                        Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                        _memberNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                        Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                        DateTime _date = Convert.ToDateTime(_temp.TransDate);
                        _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                        //Decimal _dpForex = (_temp.DPForex == null) ? 0 : Convert.ToDecimal(_temp.DPForex);
                        //Literal _dpForexLiteral = (Literal)e.Item.FindControl("DPForexLiteral");
                        //_dpForexLiteral.Text = HttpUtility.HtmlEncode(_dpForex.ToString("#,##0.00"));

                        Decimal _dpPaid = (_temp.DPPaid == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                        Literal _dpPaidLiteral = (Literal)e.Item.FindControl("DPPaidLiteral");
                        _dpPaidLiteral.Text = HttpUtility.HtmlEncode(_dpPaid.ToString("#,##0.00"));

                        ImageButton _changeButton = (ImageButton)e.Item.FindControl("ChangeImageButton");
                        _changeButton.Attributes.Add("OnClick", "return CancelOrder();");
                        _changeButton.CommandName = "ChangeDeliveryStatus";
                        _changeButton.CommandArgument = _code;
                        if (_doneSettlement == 'N')
                        {
                            _changeButton.Visible = false;
                        }

                        ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewImageButton");
                        _viewDetailButton.CommandName = "ViewDetail";
                        _viewDetailButton.CommandArgument = _code + "," + _temp.TransType;
                    }
                    else if (Request.QueryString["pos"].ToString() == "photocopy")
                    {
                        POSTrPhotocopyHd _temp = (POSTrPhotocopyHd)e.Item.DataItem;

                        string _code = _temp.TransNmbr.ToString();

                        Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                        _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                        Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                        _no = _page * _maxrow;
                        _no += 1;
                        _no = _nomor + _no;
                        _noLiteral.Text = _no.ToString();
                        _nomor += 1;

                        Literal _jobCodeLiteral = (Literal)e.Item.FindControl("TransNumLiteral");
                        _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                        String _settlementNo = this._photocopyBL.GetTransnumbSettlement(_temp.TransNmbr);
                        Literal _setttlementLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                        _setttlementLiteral.Text = HttpUtility.HtmlEncode(_settlementNo);

                        Char _doneSettlement = (_temp.DoneSettlement == null) ? 'N' : Convert.ToChar(_temp.DoneSettlement);
                        Literal _doneSettlementLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                        _doneSettlementLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_doneSettlement));

                        //String _deliveryStatus = (_temp.DeliveryStatus == null || _temp.DeliveryStatus == false) ? "0" : "1";
                        //Literal _deliveryStatusLiteral = (Literal)e.Item.FindControl("DeliveryStatusLiteral");
                        //_deliveryStatusLiteral.Text = (_deliveryStatus == "0") ? "Not Yet Delivered" : "Delivered";

                        Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                        _memberNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                        Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                        DateTime _date = Convert.ToDateTime(_temp.TransDate);
                        _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                        //Decimal _dpForex = (_temp.DPForex == null) ? 0 : Convert.ToDecimal(_temp.DPForex);
                        //Literal _dpForexLiteral = (Literal)e.Item.FindControl("DPForexLiteral");
                        //_dpForexLiteral.Text = HttpUtility.HtmlEncode(_dpForex.ToString("#,##0.00"));

                        Decimal _dpPaid = (_temp.DPPaid == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                        Literal _dpPaidLiteral = (Literal)e.Item.FindControl("DPPaidLiteral");
                        _dpPaidLiteral.Text = HttpUtility.HtmlEncode(_dpPaid.ToString("#,##0.00"));

                        ImageButton _changeButton = (ImageButton)e.Item.FindControl("ChangeImageButton");
                        _changeButton.Attributes.Add("OnClick", "return CancelOrder();");
                        _changeButton.CommandName = "ChangeDeliveryStatus";
                        _changeButton.CommandArgument = _code;
                        if (_doneSettlement == 'N')
                        {
                            _changeButton.Visible = false;
                        }

                        ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewImageButton");
                        _viewDetailButton.CommandName = "ViewDetail";
                        _viewDetailButton.CommandArgument = _code + "," + _temp.TransType;
                    }
                    else if (Request.QueryString["pos"].ToString() == "graphic")
                    {
                        POSTrGraphicHd _temp = (POSTrGraphicHd)e.Item.DataItem;

                        string _code = _temp.TransNmbr.ToString();

                        Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                        _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                        Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                        _no = _page * _maxrow;
                        _no += 1;
                        _no = _nomor + _no;
                        _noLiteral.Text = _no.ToString();
                        _nomor += 1;

                        Literal _jobCodeLiteral = (Literal)e.Item.FindControl("TransNumLiteral");
                        _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                        String _settlementNo = this._graphicBL.GetTransnumbSettlement(_temp.TransNmbr);
                        Literal _setttlementLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                        _setttlementLiteral.Text = HttpUtility.HtmlEncode(_settlementNo);

                        Char _doneSettlement = (_temp.DoneSettlement == null) ? 'N' : Convert.ToChar(_temp.DoneSettlement);
                        Literal _doneSettlementLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                        _doneSettlementLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_doneSettlement));

                        //String _deliveryStatus = (_temp.DeliveryStatus == null || _temp.DeliveryStatus == false) ? "0" : "1";
                        //Literal _deliveryStatusLiteral = (Literal)e.Item.FindControl("DeliveryStatusLiteral");
                        //_deliveryStatusLiteral.Text = (_deliveryStatus == "0") ? "Not Yet Delivered" : "Delivered";

                        Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                        _memberNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                        Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                        DateTime _date = Convert.ToDateTime(_temp.TransDate);
                        _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                        //Decimal _dpForex = (_temp.DPForex == null) ? 0 : Convert.ToDecimal(_temp.DPForex);
                        //Literal _dpForexLiteral = (Literal)e.Item.FindControl("DPForexLiteral");
                        //_dpForexLiteral.Text = HttpUtility.HtmlEncode(_dpForex.ToString("#,##0.00"));

                        Decimal _dpPaid = (_temp.DPPaid == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                        Literal _dpPaidLiteral = (Literal)e.Item.FindControl("DPPaidLiteral");
                        _dpPaidLiteral.Text = HttpUtility.HtmlEncode(_dpPaid.ToString("#,##0.00"));

                        ImageButton _changeButton = (ImageButton)e.Item.FindControl("ChangeImageButton");
                        _changeButton.Attributes.Add("OnClick", "return CancelOrder();");
                        _changeButton.CommandName = "ChangeDeliveryStatus";
                        _changeButton.CommandArgument = _code;
                        if (_doneSettlement == 'N')
                        {
                            _changeButton.Visible = false;
                        }

                        ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewImageButton");
                        _viewDetailButton.CommandName = "ViewDetail";
                        _viewDetailButton.CommandArgument = _code + "," + _temp.TransType;
                    }
                    else if (Request.QueryString["pos"].ToString() == "shipping")
                    {
                        POSTrShippingHd _temp = (POSTrShippingHd)e.Item.DataItem;

                        string _code = _temp.TransNmbr.ToString();

                        Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                        _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                        Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                        _no = _page * _maxrow;
                        _no += 1;
                        _no = _nomor + _no;
                        _noLiteral.Text = _no.ToString();
                        _nomor += 1;

                        Literal _jobCodeLiteral = (Literal)e.Item.FindControl("TransNumLiteral");
                        _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                        String _settlementNo = this._shippingBL.GetTransnumbSettlement(_temp.TransNmbr);
                        Literal _setttlementLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                        _setttlementLiteral.Text = HttpUtility.HtmlEncode(_settlementNo);

                        Char _doneSettlement = (_temp.DoneSettlement == null) ? 'N' : Convert.ToChar(_temp.DoneSettlement);
                        Literal _doneSettlementLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                        _doneSettlementLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_doneSettlement));

                        //String _deliveryStatus = (_temp.DeliveryStatus == null || _temp.DeliveryStatus == false) ? "0" : "1";
                        //Literal _deliveryStatusLiteral = (Literal)e.Item.FindControl("DeliveryStatusLiteral");
                        //_deliveryStatusLiteral.Text = (_deliveryStatus == "0") ? "Not Yet Delivered" : "Delivered";

                        Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                        _memberNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                        Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                        DateTime _date = Convert.ToDateTime(_temp.TransDate);
                        _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                        //Decimal _dpForex = (_temp.DPForex == null) ? 0 : Convert.ToDecimal(_temp.DPForex);
                        //Literal _dpForexLiteral = (Literal)e.Item.FindControl("DPForexLiteral");
                        //_dpForexLiteral.Text = HttpUtility.HtmlEncode(_dpForex.ToString("#,##0.00"));

                        Decimal _dpPaid = (_temp.DPPaid == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                        Literal _dpPaidLiteral = (Literal)e.Item.FindControl("DPPaidLiteral");
                        _dpPaidLiteral.Text = HttpUtility.HtmlEncode(_dpPaid.ToString("#,##0.00"));

                        ImageButton _changeButton = (ImageButton)e.Item.FindControl("ChangeImageButton");
                        _changeButton.Attributes.Add("OnClick", "return CancelOrder();");
                        _changeButton.CommandName = "ChangeDeliveryStatus";
                        _changeButton.CommandArgument = _code;
                        if (_doneSettlement == 'N' | _temp.DeliveryStatus == true)
                        {
                            _changeButton.Visible = false;
                        }

                        ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewImageButton");
                        _viewDetailButton.CommandName = "ViewDetail";
                        _viewDetailButton.CommandArgument = _code + "," + _temp.TransType;
                    }
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void CheckStatusListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ChangeDeliveryStatus")
                {
                    Boolean _result = false;
                    if (Request.QueryString["pos"].ToString() == "retail")
                    {
                        _result = this._retailBL.SetDelivery(e.CommandArgument.ToString(), true);
                    }
                    else if (Request.QueryString["pos"].ToString() == "internet")
                    {
                        _result = this._internetBL.SetDelivery(e.CommandArgument.ToString(), true);
                    }
                    else if (Request.QueryString["pos"].ToString() == "cafe")
                    {
                        _result = this._cafeBL.SetDelivery(e.CommandArgument.ToString(), true);
                    }
                    else if (Request.QueryString["pos"].ToString() == "ticketing")
                    {
                        _result = this._ticketingBL.SetDelivery(e.CommandArgument.ToString(), true);
                    }
                    else if (Request.QueryString["pos"].ToString() == "hotel")
                    {
                        _result = this._hotelBL.SetDelivery(e.CommandArgument.ToString(), true);
                    }
                    else if (Request.QueryString["pos"].ToString() == "printing")
                    {
                        _result = this._printingBL.SetDelivery(e.CommandArgument.ToString(), true);
                    }
                    else if (Request.QueryString["pos"].ToString() == "photocopy")
                    {
                        _result = this._photocopyBL.SetDelivery(e.CommandArgument.ToString(), true);
                    }
                    else if (Request.QueryString["pos"].ToString() == "graphic")
                    {
                        _result = this._graphicBL.SetDelivery(e.CommandArgument.ToString(), true);
                    }
                    else if (Request.QueryString["pos"].ToString() == "shipping")
                    {
                        _result = this._shippingBL.SetDelivery(e.CommandArgument.ToString(), true);
                    }

                    if (_result == true)
                    {
                        this.ShowData();
                    }
                }
                if (e.CommandName == "ViewDetail")
                {
                    this.ShowDataDetail(e.CommandArgument.ToString());
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void ShowDataDetail(String _prmArgument)
        {
            try
            {
                String[] _break = _prmArgument.Split(',');
                String _transNmbr = _break[0];
                String _transType = _break[1];

                this.TransNmbrHiddenField.Value = _transNmbr;
                this.DetailTypeHiddenField.Value = _transType;

                if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).Trim().ToLower())
                {
                    POSRetailBL _posRetailBL = new POSRetailBL();
                    this.ListRepeaterDetail.DataSource = _posRetailBL.GetListRetailDtByTransNmbr(_transNmbr);
                    this.ListRepeaterDetail.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).Trim().ToLower())
                {
                    POSInternetBL _posInternetBL = new POSInternetBL();
                    this.ListRepeaterDetail.DataSource = _posInternetBL.GetListInternetDtByTransNmbr(_transNmbr);
                    this.ListRepeaterDetail.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).Trim().ToLower())
                {
                    POSCafeBL _posCafeBL = new POSCafeBL();
                    this.ListRepeaterDetail.DataSource = _posCafeBL.GetListCafeDtByTransNmbr(_transNmbr);
                    this.ListRepeaterDetail.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Ticketing).Trim().ToLower())
                {
                    TicketingBL _ticketingBL = new TicketingBL();
                    this.ListRepeaterDetail.DataSource = _ticketingBL.GetListPOSTrTicketingDt(_transNmbr);
                    this.ListRepeaterDetail.DataBind();
                }
                else if (_transType.Trim().ToLower() == AppModule.GetValue(TransactionType.Ticketing).Trim().ToLower().Trim().ToLower())
                {
                    TicketingBL _ticketingBL = new TicketingBL();
                    this.ListRepeaterDetail.DataSource = _ticketingBL.GetListPOSTrTicketingDt(_transNmbr);
                    this.ListRepeaterDetail.DataBind();
                }
                else if (_transType.Trim().ToLower() == AppModule.GetValue(TransactionType.Hotel).Trim().ToLower().Trim().ToLower())
                {
                    HotelBL _hotelBL = new HotelBL();
                    this.ListRepeaterDetail.DataSource = _hotelBL.GetListPOSTrHotelDt(_transNmbr);
                    this.ListRepeaterDetail.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Printing).Trim().ToLower())
                {
                    POSPrintingBL _posPrintingBL = new POSPrintingBL();
                    this.ListRepeaterDetail.DataSource = _posPrintingBL.GetListPrintingDtByTransNmbr(_transNmbr);
                    this.ListRepeaterDetail.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy).Trim().ToLower())
                {
                    POSPhotocopyBL _posPhotocopyBL = new POSPhotocopyBL();
                    this.ListRepeaterDetail.DataSource = _posPhotocopyBL.GetListPhotocopyDtByTransNmbr(_transNmbr);
                    this.ListRepeaterDetail.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic).Trim().ToLower())
                {
                    POSGraphicBL _posGraphicBL = new POSGraphicBL();
                    this.ListRepeaterDetail.DataSource = _posGraphicBL.GetListGraphicDtByTransNmbr(_transNmbr);
                    this.ListRepeaterDetail.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Shipping).Trim().ToLower())
                {
                    this.ListRepeaterDetail.DataSource = _shippingBL.GetListShippingDtByTransNmbr(_transNmbr);
                    this.ListRepeaterDetail.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ListRepeaterDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).Trim().ToLower())
                {
                    POSTrRetailDt _temp = (POSTrRetailDt)e.Item.DataItem;
                    string _code = this.TransNmbrHiddenField.Value;
                    this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                    Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                    _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                    Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                    _descLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                    int _qty = (_temp.Qty == null) ? 0 : Convert.ToInt32(_temp.Qty);
                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_qty.ToString());

                    Decimal _discForex = (_temp.DiscForex == null) ? 0 : Convert.ToDecimal(_temp.DiscForex);
                    Literal _discLiteral = (Literal)e.Item.FindControl("DiscLiteral");
                    _discLiteral.Text = HttpUtility.HtmlEncode(_discForex.ToString("#,##0.00"));

                    Decimal _price = (_temp.AmountForex == null) ? 0 : Convert.ToDecimal(_temp.AmountForex);
                    Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                    _priceLiteral.Text = HttpUtility.HtmlEncode(_price.ToString("#,##0.00"));

                    Decimal _lineTotal = (_temp.LineTotalForex == null) ? 0 : Convert.ToDecimal(_temp.LineTotalForex);
                    Literal _lineTotalLiteral = (Literal)e.Item.FindControl("LineTotalLiteral");
                    _lineTotalLiteral.Text = HttpUtility.HtmlEncode(_lineTotal.ToString("#,##0.00"));

                    ImageButton _pickPrintPreview = (ImageButton)e.Item.FindControl("PickPrintPreview");
                    _pickPrintPreview.Visible = false;
                }
                else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).Trim().ToLower())
                {
                    POSTrInternetDt _temp = (POSTrInternetDt)e.Item.DataItem;
                    string _code = this.TransNmbrHiddenField.Value;
                    this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                    Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                    _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                    Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                    _descLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                    int _qty = (_temp.Qty == null) ? 0 : Convert.ToInt32(_temp.Qty);
                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_qty.ToString());

                    Decimal _discForex = (_temp.DiscForex == null) ? 0 : Convert.ToDecimal(_temp.DiscForex);
                    Literal _discLiteral = (Literal)e.Item.FindControl("DiscLiteral");
                    _discLiteral.Text = HttpUtility.HtmlEncode(_discForex.ToString("#,##0.00"));

                    Decimal _price = (_temp.AmountForex == null) ? 0 : Convert.ToDecimal(_temp.AmountForex);
                    Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                    _priceLiteral.Text = HttpUtility.HtmlEncode(_price.ToString("#,##0.00"));

                    Decimal _lineTotal = (_temp.LineTotalForex == null) ? 0 : Convert.ToDecimal(_temp.LineTotalForex);
                    Literal _lineTotalLiteral = (Literal)e.Item.FindControl("LineTotalLiteral");
                    _lineTotalLiteral.Text = HttpUtility.HtmlEncode(_lineTotal.ToString("#,##0.00"));

                    ImageButton _pickPrintPreview = (ImageButton)e.Item.FindControl("PickPrintPreview");
                    _pickPrintPreview.Visible = false;
                }
                else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).Trim().ToLower())
                {
                    POSTrCafeDt _temp = (POSTrCafeDt)e.Item.DataItem;
                    string _code = this.TransNmbrHiddenField.Value;
                    this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                    Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                    _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                    Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                    _descLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                    int _qty = (_temp.Qty == null) ? 0 : Convert.ToInt32(_temp.Qty);
                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_qty.ToString());

                    Decimal _discForex = (_temp.DiscForex == null) ? 0 : Convert.ToDecimal(_temp.DiscForex);
                    Literal _discLiteral = (Literal)e.Item.FindControl("DiscLiteral");
                    _discLiteral.Text = HttpUtility.HtmlEncode(_discForex.ToString("#,##0.00"));

                    Decimal _price = (_temp.AmountForex == null) ? 0 : Convert.ToDecimal(_temp.AmountForex);
                    Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                    _priceLiteral.Text = HttpUtility.HtmlEncode(_price.ToString("#,##0.00"));

                    Decimal _lineTotal = (_temp.LineTotalForex == null) ? 0 : Convert.ToDecimal(_temp.LineTotalForex);
                    Literal _lineTotalLiteral = (Literal)e.Item.FindControl("LineTotalLiteral");
                    _lineTotalLiteral.Text = HttpUtility.HtmlEncode(_lineTotal.ToString("#,##0.00"));

                    ImageButton _pickPrintPreview = (ImageButton)e.Item.FindControl("PickPrintPreview");
                    _pickPrintPreview.Visible = false;
                }
                else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == AppModule.GetValue(TransactionType.Ticketing).Trim().ToLower())
                {
                    POSTrTicketingDt _temp = (POSTrTicketingDt)e.Item.DataItem;
                    string _code = this.TransNmbrHiddenField.Value;
                    this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                    Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                    _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.KodeBooking);

                    Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                    MsAirline _msAirline = _airLineBL.GetSingleAirLine(_temp.AirlineCode);
                    _descLiteral.Text = HttpUtility.HtmlEncode(_msAirline.AirlineName);

                    int _qty = (_temp.TotalGuest == null) ? 0 : Convert.ToInt32(_temp.TotalGuest);
                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_qty.ToString());

                    Decimal _discForex = (_temp.DiscountAmount == null) ? 0 : Convert.ToDecimal(_temp.DiscountAmount);
                    Literal _discLiteral = (Literal)e.Item.FindControl("DiscLiteral");
                    _discLiteral.Text = HttpUtility.HtmlEncode(_discForex.ToString("#,##0.00"));

                    Decimal _price = (_temp.BasicFare == null) ? 0 : Convert.ToDecimal(_temp.BasicFare);
                    Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                    _priceLiteral.Text = HttpUtility.HtmlEncode(_price.ToString("#,##0.00"));

                    Decimal _lineTotal = (_temp.TotalBasicFare == null) ? 0 : Convert.ToDecimal(_temp.TotalBasicFare);
                    Literal _lineTotalLiteral = (Literal)e.Item.FindControl("LineTotalLiteral");
                    _lineTotalLiteral.Text = HttpUtility.HtmlEncode(_lineTotal.ToString("#,##0.00"));

                    ImageButton _pickPrintPreview = (ImageButton)e.Item.FindControl("PickPrintPreview");
                    _pickPrintPreview.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
                    _pickPrintPreview.ToolTip = "Pick Print Preview";
                }
                else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == AppModule.GetValue(TransactionType.Hotel).Trim().ToLower())
                {
                    POSTrHotelDt _temp = (POSTrHotelDt)e.Item.DataItem;
                    string _code = this.TransNmbrHiddenField.Value;
                    this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                    Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                    _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.VoucherNo);

                    Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                    POSMsHotel _msHotel = this._hotelBL.GetSinglePOSMsHotel(_temp.HotelCode);
                    _descLiteral.Text = HttpUtility.HtmlEncode(_msHotel.HotelName);

                    int _qty = (_temp.TotalRoom == null) ? 0 : Convert.ToInt32(_temp.TotalRoom);
                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_qty.ToString());

                    Decimal _discForex = (_temp.DiscountAmount == null) ? 0 : Convert.ToDecimal(_temp.DiscountAmount);
                    Literal _discLiteral = (Literal)e.Item.FindControl("DiscLiteral");
                    _discLiteral.Text = HttpUtility.HtmlEncode(_discForex.ToString("#,##0.00"));

                    Decimal _price = (_temp.BasicFare == null) ? 0 : Convert.ToDecimal(_temp.BasicFare);
                    Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                    _priceLiteral.Text = HttpUtility.HtmlEncode(_price.ToString("#,##0.00"));

                    Decimal _lineTotal = (_temp.TotalBasicFare == null) ? 0 : Convert.ToDecimal(_temp.TotalBasicFare);
                    Literal _lineTotalLiteral = (Literal)e.Item.FindControl("LineTotalLiteral");
                    _lineTotalLiteral.Text = HttpUtility.HtmlEncode(_lineTotal.ToString("#,##0.00"));

                    ImageButton _pickPrintPreview = (ImageButton)e.Item.FindControl("PickPrintPreview");
                    _pickPrintPreview.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
                    _pickPrintPreview.ToolTip = "Pick Print Preview";
                }
                else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Printing).Trim().ToLower())
                {
                    POSTrPrintingDt _temp = (POSTrPrintingDt)e.Item.DataItem;
                    string _code = this.TransNmbrHiddenField.Value;
                    this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                    Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                    _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                    Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                    _descLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                    int _qty = (_temp.Qty == null) ? 0 : Convert.ToInt32(_temp.Qty);
                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_qty.ToString());

                    Decimal _discForex = (_temp.DiscForex == null) ? 0 : Convert.ToDecimal(_temp.DiscForex);
                    Literal _discLiteral = (Literal)e.Item.FindControl("DiscLiteral");
                    _discLiteral.Text = HttpUtility.HtmlEncode(_discForex.ToString("#,##0.00"));

                    Decimal _price = (_temp.AmountForex == null) ? 0 : Convert.ToDecimal(_temp.AmountForex);
                    Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                    _priceLiteral.Text = HttpUtility.HtmlEncode(_price.ToString("#,##0.00"));

                    Decimal _lineTotal = (_temp.LineTotalForex == null) ? 0 : Convert.ToDecimal(_temp.LineTotalForex);
                    Literal _lineTotalLiteral = (Literal)e.Item.FindControl("LineTotalLiteral");
                    _lineTotalLiteral.Text = HttpUtility.HtmlEncode(_lineTotal.ToString("#,##0.00"));

                    ImageButton _pickPrintPreview = (ImageButton)e.Item.FindControl("PickPrintPreview");
                    _pickPrintPreview.Visible = false;
                }
                else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy).Trim().ToLower())
                {
                    POSTrPhotocopyDt _temp = (POSTrPhotocopyDt)e.Item.DataItem;
                    string _code = this.TransNmbrHiddenField.Value;
                    this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                    Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                    _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                    Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                    _descLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                    int _qty = (_temp.Qty == null) ? 0 : Convert.ToInt32(_temp.Qty);
                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_qty.ToString());

                    Decimal _discForex = (_temp.DiscForex == null) ? 0 : Convert.ToDecimal(_temp.DiscForex);
                    Literal _discLiteral = (Literal)e.Item.FindControl("DiscLiteral");
                    _discLiteral.Text = HttpUtility.HtmlEncode(_discForex.ToString("#,##0.00"));

                    Decimal _price = (_temp.AmountForex == null) ? 0 : Convert.ToDecimal(_temp.AmountForex);
                    Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                    _priceLiteral.Text = HttpUtility.HtmlEncode(_price.ToString("#,##0.00"));

                    Decimal _lineTotal = (_temp.LineTotalForex == null) ? 0 : Convert.ToDecimal(_temp.LineTotalForex);
                    Literal _lineTotalLiteral = (Literal)e.Item.FindControl("LineTotalLiteral");
                    _lineTotalLiteral.Text = HttpUtility.HtmlEncode(_lineTotal.ToString("#,##0.00"));

                    ImageButton _pickPrintPreview = (ImageButton)e.Item.FindControl("PickPrintPreview");
                    _pickPrintPreview.Visible = false;
                }
                else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic).Trim().ToLower())
                {
                    POSTrGraphicDt _temp = (POSTrGraphicDt)e.Item.DataItem;
                    string _code = this.TransNmbrHiddenField.Value;
                    this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                    Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                    _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                    Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                    _descLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                    int _qty = (_temp.Qty == null) ? 0 : Convert.ToInt32(_temp.Qty);
                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_qty.ToString());

                    Decimal _discForex = (_temp.DiscForex == null) ? 0 : Convert.ToDecimal(_temp.DiscForex);
                    Literal _discLiteral = (Literal)e.Item.FindControl("DiscLiteral");
                    _discLiteral.Text = HttpUtility.HtmlEncode(_discForex.ToString("#,##0.00"));

                    Decimal _price = (_temp.AmountForex == null) ? 0 : Convert.ToDecimal(_temp.AmountForex);
                    Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                    _priceLiteral.Text = HttpUtility.HtmlEncode(_price.ToString("#,##0.00"));

                    Decimal _lineTotal = (_temp.LineTotalForex == null) ? 0 : Convert.ToDecimal(_temp.LineTotalForex);
                    Literal _lineTotalLiteral = (Literal)e.Item.FindControl("LineTotalLiteral");
                    _lineTotalLiteral.Text = HttpUtility.HtmlEncode(_lineTotal.ToString("#,##0.00"));

                    ImageButton _pickPrintPreview = (ImageButton)e.Item.FindControl("PickPrintPreview");
                    _pickPrintPreview.Visible = false;
                }
                else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Shipping).Trim().ToLower())
                {
                    POSTrShippingDt _temp = (POSTrShippingDt)e.Item.DataItem;
                    string _code = this.TransNmbrHiddenField.Value;
                    this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                    Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                    _productCodeLiteral.Text = _temp.VendorCode;

                    //String _cityCode = this._shippingBL.GetSinglePOSTrShippingHd(_temp.TransNmbr).DeliverCityCode;
                    //POSMsShipping _posMsShipping = this._shippingBL.GetPOSMsShipping(_temp.VendorCode, _temp.ShippingTypeCode, _temp.ProductShape, _cityCode);
                    POSTrShippingHd _posTrShippingHd = this._shippingBL.GetSinglePOSTrShippingHd(_temp.TransNmbr);

                    String _countryCode = _posTrShippingHd.DeliveryCountryCode;
                    String _cityCode = _posTrShippingHd.DeliverCityCode;
                    String _productName = "";

                    String _countryCodeFgHome = this._countryBL.GetCountryCodeWithFgHomeY();
                    String _country = this._countryBL.GetCountryNameByCode(_countryCodeFgHome);

                    if (_countryCode != _countryCodeFgHome)
                    {
                        String _vendor = this._vendorBL.GetSingle(_temp.VendorCode).VendorName;
                        String _city = this._cityBL.GetSingle(_cityCode).CityName;
                        _country = this._countryBL.GetCountryNameByCode(_countryCode);

                        _productName = _vendor + "-" + this._shippingBL.GetSinglePOSMsZone(_temp.ShippingTypeCode).ZoneName + "-" + _country + "." + _city;
                    }
                    else
                    {
                        POSMsShipping _posMsShipping = this._shippingBL.GetPOSMsShipping(_temp.VendorCode, _temp.ShippingTypeCode, _temp.ProductShape, _cityCode);
                        _productName = _posMsShipping.VendorName + "-" + _posMsShipping.ShippingTypeName + "-" + _country + "." + _posMsShipping.CityName;
                    }

                    Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                    //_descLiteral.Text = _posMsShipping.VendorName + "-" + _posMsShipping.ShippingTypeName + "-" + _posMsShipping.CityName;
                    _descLiteral.Text = _productName;


                    String _unit = this._unitBL.GetSingle(_temp.Unit).UnitName;
                    String _productShape = _temp.ProductShape == "0" ? "Document" : "Non Document";
                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = _productShape + "-" + HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.Weight).ToString("#,#")) + " " + _unit;

                    Decimal _discForex = (_temp.DiscForex == null) ? 0 : Convert.ToDecimal(_temp.DiscForex);
                    Literal _discLiteral = (Literal)e.Item.FindControl("DiscLiteral");
                    _discLiteral.Text = HttpUtility.HtmlEncode(_discForex.ToString("#,##0.00"));

                    Decimal _price = (_temp.AmountForex == null) ? 0 : Convert.ToDecimal(_temp.AmountForex);
                    Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                    _priceLiteral.Text = HttpUtility.HtmlEncode(_price.ToString("#,##0.00"));

                    Decimal _lineTotal = (_temp.LineTotalForex == null) ? 0 : Convert.ToDecimal(_temp.LineTotalForex);
                    Literal _lineTotalLiteral = (Literal)e.Item.FindControl("LineTotalLiteral");
                    _lineTotalLiteral.Text = HttpUtility.HtmlEncode(_lineTotal.ToString("#,##0.00"));

                    ImageButton _pickPrintPreview = (ImageButton)e.Item.FindControl("PickPrintPreview");
                    _pickPrintPreview.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
                    _pickPrintPreview.ToolTip = "Pick Print Preview";
                }
            }
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "CHECKSTATUS");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }
    }
}