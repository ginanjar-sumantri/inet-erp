using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

public partial class General_JoinJobOrder : System.Web.UI.Page
{
    private POSRetailBL _retailBL = new POSRetailBL();
    private POSInternetBL _internetBL = new POSInternetBL();
    private POSReasonBL _reasonBL = new POSReasonBL();
    private POSCafeBL _cafeBL = new POSCafeBL();
    private TicketingBL _ticketingBL = new TicketingBL();
    private AirLineBL _airLineBL = new AirLineBL();
    private HotelBL _hotelBL = new HotelBL();
    private POSPrintingBL _printingBL = new POSPrintingBL();
    private POSPhotocopyBL _photocopyBL = new POSPhotocopyBL();
    private POSGraphicBL _graphicBL = new POSGraphicBL();
    private POSShippingBL _posShippingBL = new POSShippingBL();
    private POSConfigurationBL _posConfigurationBL = new POSConfigurationBL();
    private UnitBL _unitBL = new UnitBL();
    private CountryBL _countryBL = new CountryBL();
    //private ShippingBL _shippingBL = new ShippingBL();
    private VendorBL _vendorBL = new VendorBL();
    private CityBL _cityBL = new CityBL();

    private int _page;
    private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
    private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
    private int _no = 0;
    private int _nomor = 0;

    private int _page2;
    private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
    private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
    private int _no2 = 0;
    private int _nomor2 = 0;

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

            this.ClearLabel();
            this.ClearData();
            this.ChangeVisiblePanel(0);
            this.ShowData();
        }
    }

    public void ClearLabel()
    {
    }

    public void ClearData()
    {
        this.CustIDTextBox.Text = "";
        this.SearchFieldDDL.SelectedIndex = 0;
        this.SearchFieldTextBox.Text = "";

        this.TransNmbrHiddenField.Value = "";

        this.DetailListRepeater.DataSource = null;
        this.DetailListRepeater.DataBind();
        this.ReasonListRepeater.DataSource = null;
        this.ReasonListRepeater.DataBind();
    }

    protected void ChangeVisiblePanel(Byte _prmValue)
    {
        if (_prmValue == 0)
        {
            this.DetailListPanel.Visible = true;
            this.ReasonListPanel.Visible = false;
            this.PasswordPanel.Visible = false;
        }
        else if (_prmValue == 1)
        {
            this.DetailListPanel.Visible = false;
            this.ReasonListPanel.Visible = true;
            this.PasswordPanel.Visible = false;
        }
        else if (_prmValue == 2)
        {
            this.DetailListPanel.Visible = false;
            this.ReasonListPanel.Visible = false;
            this.PasswordPanel.Visible = true;
        }
    }

    public void ShowData()
    {
        try
        {
            if (Request.QueryString["pos"].ToString() == "retail")
            {
                this.JobOrderListRepeater.DataSource = this._retailBL.GetListRetailSendToCashier(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                this.JobOrderListRepeater.DataBind();
            }
            else if (Request.QueryString["pos"].ToString() == "internet")
            {
                this.JobOrderListRepeater.DataSource = this._internetBL.GetListInternetSendToCashier(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                this.JobOrderListRepeater.DataBind();
            }
            else if (Request.QueryString["pos"].ToString() == "cafe")
            {
                this.JobOrderListRepeater.DataSource = this._cafeBL.GetListCafeHdSendToCashier(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                this.JobOrderListRepeater.DataBind();
            }
            else if (Request.QueryString["pos"].ToString() == "ticketing")
            {
                this.JobOrderListRepeater.DataSource = this._ticketingBL.GetListTicketingHdSendToCashier(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                this.JobOrderListRepeater.DataBind();
            }
            else if (Request.QueryString["pos"].ToString() == "hotel")
            {
                this.JobOrderListRepeater.DataSource = this._hotelBL.GetListHotelHdSendToCashier(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                this.JobOrderListRepeater.DataBind();
            }
            else if (Request.QueryString["pos"].ToString() == "printing")
            {
                this.JobOrderListRepeater.DataSource = this._printingBL.GetListPrintingSendToCashier(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                this.JobOrderListRepeater.DataBind();
            }
            else if (Request.QueryString["pos"].ToString() == "photocopy")
            {
                this.JobOrderListRepeater.DataSource = this._photocopyBL.GetListPhotocopySendToCashier(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                this.JobOrderListRepeater.DataBind();
            }
            else if (Request.QueryString["pos"].ToString() == "graphic")
            {
                this.JobOrderListRepeater.DataSource = this._graphicBL.GetListGraphicSendToCashier(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                this.JobOrderListRepeater.DataBind();
            }
            else if (Request.QueryString["pos"].ToString() == "shipping")
            {
                this.JobOrderListRepeater.DataSource = this._posShippingBL.GetListShippingSendToCashier(this.CustIDTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                this.JobOrderListRepeater.DataBind();
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

    protected void JobOrderListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Request.QueryString["pos"].ToString() == "retail")
            {
                POSTrRetailHd _temp = (POSTrRetailHd)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no = _page * _maxrow;
                //_no += 1;
                //_no = _nomor + _no;
                //_noLiteral.Text = _no.ToString();
                //_nomor += 1;

                Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                Literal _jobCodeLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                DateTime _date = Convert.ToDateTime(_temp.TransDate);
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                ImageButton _cancelPaidButton = (ImageButton)e.Item.FindControl("CancelPaidButton");
                _cancelPaidButton.CommandName = "CancelPaid";
                _cancelPaidButton.CommandArgument = _temp.TransNmbr;

                ImageButton _viewJobOrderButton = (ImageButton)e.Item.FindControl("ViewJobOrderButton");
                _viewJobOrderButton.CommandName = "ViewDetail";
                _viewJobOrderButton.CommandArgument = _temp.TransNmbr;

                ImageButton _joinjobButton = (ImageButton)e.Item.FindControl("JoinJobOrderButton");
                _joinjobButton.CommandName = "Join";
                _joinjobButton.CommandArgument = _temp.TransNmbr;
                //_joinjobButton.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
            }
            else if (Request.QueryString["pos"].ToString() == "internet")
            {
                POSTrInternetHd _temp = (POSTrInternetHd)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no = _page * _maxrow;
                //_no += 1;
                //_no = _nomor + _no;
                //_noLiteral.Text = _no.ToString();
                //_nomor += 1;

                Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                Literal _jobCodeLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                DateTime _date = Convert.ToDateTime(_temp.TransDate);
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                ImageButton _cancelPaidButton = (ImageButton)e.Item.FindControl("CancelPaidButton");
                _cancelPaidButton.CommandName = "CancelPaid";
                _cancelPaidButton.CommandArgument = _temp.TransNmbr;

                ImageButton _viewJobOrderButton = (ImageButton)e.Item.FindControl("ViewJobOrderButton");
                _viewJobOrderButton.CommandName = "ViewDetail";
                _viewJobOrderButton.CommandArgument = _temp.TransNmbr;

                ImageButton _joinjobButton = (ImageButton)e.Item.FindControl("JoinJobOrderButton");
                _joinjobButton.CommandName = "Join";
                _joinjobButton.CommandArgument = _temp.TransNmbr;
                //_joinjobButton.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
            }
            else if (Request.QueryString["pos"].ToString() == "cafe")
            {
                POSTrCafeHd _temp = (POSTrCafeHd)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no = _page * _maxrow;
                //_no += 1;
                //_no = _nomor + _no;
                //_noLiteral.Text = _no.ToString();
                //_nomor += 1;

                Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                Literal _jobCodeLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                DateTime _date = Convert.ToDateTime(_temp.TransDate);
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                ImageButton _cancelPaidButton = (ImageButton)e.Item.FindControl("CancelPaidButton");
                _cancelPaidButton.CommandName = "CancelPaid";
                _cancelPaidButton.CommandArgument = _temp.TransNmbr;

                ImageButton _viewJobOrderButton = (ImageButton)e.Item.FindControl("ViewJobOrderButton");
                _viewJobOrderButton.CommandName = "ViewDetail";
                _viewJobOrderButton.CommandArgument = _temp.TransNmbr;

                ImageButton _joinjobButton = (ImageButton)e.Item.FindControl("JoinJobOrderButton");
                _joinjobButton.CommandName = "Join";
                _joinjobButton.CommandArgument = _temp.TransNmbr;
                //_joinjobButton.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
            }
            else if (Request.QueryString["pos"].ToString() == "ticketing")
            {
                POSTrTicketingHd _temp = (POSTrTicketingHd)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no = _page * _maxrow;
                //_no += 1;
                //_no = _nomor + _no;
                //_noLiteral.Text = _no.ToString();
                //_nomor += 1;

                Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                Literal _jobCodeLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                DateTime _date = Convert.ToDateTime(_temp.TransDate);
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                ImageButton _cancelPaidButton = (ImageButton)e.Item.FindControl("CancelPaidButton");
                _cancelPaidButton.CommandName = "CancelPaid";
                _cancelPaidButton.CommandArgument = _temp.TransNmbr;

                ImageButton _viewJobOrderButton = (ImageButton)e.Item.FindControl("ViewJobOrderButton");
                _viewJobOrderButton.CommandName = "ViewDetail";
                _viewJobOrderButton.CommandArgument = _temp.TransNmbr;

                ImageButton _joinjobButton = (ImageButton)e.Item.FindControl("JoinJobOrderButton");
                _joinjobButton.CommandName = "Join";
                _joinjobButton.CommandArgument = _temp.TransNmbr;
                //_joinjobButton.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");

            }
            else if (Request.QueryString["pos"].ToString() == "hotel")
            {
                POSTrHotelHd _temp = (POSTrHotelHd)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no = _page * _maxrow;
                //_no += 1;
                //_no = _nomor + _no;
                //_noLiteral.Text = _no.ToString();
                //_nomor += 1;

                Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                Literal _jobCodeLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                DateTime _date = Convert.ToDateTime(_temp.TransDate);
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                ImageButton _cancelPaidButton = (ImageButton)e.Item.FindControl("CancelPaidButton");
                _cancelPaidButton.CommandName = "CancelPaid";
                _cancelPaidButton.CommandArgument = _temp.TransNmbr;

                ImageButton _viewJobOrderButton = (ImageButton)e.Item.FindControl("ViewJobOrderButton");
                _viewJobOrderButton.CommandName = "ViewDetail";
                _viewJobOrderButton.CommandArgument = _temp.TransNmbr;

                ImageButton _joinjobButton = (ImageButton)e.Item.FindControl("JoinJobOrderButton");
                _joinjobButton.CommandName = "Join";
                _joinjobButton.CommandArgument = _temp.TransNmbr;
                //_joinjobButton.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
            }
            else if (Request.QueryString["pos"].ToString() == "printing")
            {
                POSTrPrintingHd _temp = (POSTrPrintingHd)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no = _page * _maxrow;
                //_no += 1;
                //_no = _nomor + _no;
                //_noLiteral.Text = _no.ToString();
                //_nomor += 1;

                Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                Literal _jobCodeLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                DateTime _date = Convert.ToDateTime(_temp.TransDate);
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                ImageButton _cancelPaidButton = (ImageButton)e.Item.FindControl("CancelPaidButton");
                _cancelPaidButton.CommandName = "CancelPaid";
                _cancelPaidButton.CommandArgument = _temp.TransNmbr;

                ImageButton _viewJobOrderButton = (ImageButton)e.Item.FindControl("ViewJobOrderButton");
                _viewJobOrderButton.CommandName = "ViewDetail";
                _viewJobOrderButton.CommandArgument = _temp.TransNmbr;

                ImageButton _joinjobButton = (ImageButton)e.Item.FindControl("JoinJobOrderButton");
                _joinjobButton.CommandName = "Join";
                _joinjobButton.CommandArgument = _temp.TransNmbr;
                //_joinjobButton.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
            }
            else if (Request.QueryString["pos"].ToString() == "photocopy")
            {
                POSTrPhotocopyHd _temp = (POSTrPhotocopyHd)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no = _page * _maxrow;
                //_no += 1;
                //_no = _nomor + _no;
                //_noLiteral.Text = _no.ToString();
                //_nomor += 1;

                Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                Literal _jobCodeLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                DateTime _date = Convert.ToDateTime(_temp.TransDate);
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                ImageButton _cancelPaidButton = (ImageButton)e.Item.FindControl("CancelPaidButton");
                _cancelPaidButton.CommandName = "CancelPaid";
                _cancelPaidButton.CommandArgument = _temp.TransNmbr;

                ImageButton _viewJobOrderButton = (ImageButton)e.Item.FindControl("ViewJobOrderButton");
                _viewJobOrderButton.CommandName = "ViewDetail";
                _viewJobOrderButton.CommandArgument = _temp.TransNmbr;

                ImageButton _joinjobButton = (ImageButton)e.Item.FindControl("JoinJobOrderButton");
                _joinjobButton.CommandName = "Join";
                _joinjobButton.CommandArgument = _temp.TransNmbr;
                //_joinjobButton.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
            }
            else if (Request.QueryString["pos"].ToString() == "graphic")
            {
                POSTrGraphicHd _temp = (POSTrGraphicHd)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no = _page * _maxrow;
                //_no += 1;
                //_no = _nomor + _no;
                //_noLiteral.Text = _no.ToString();
                //_nomor += 1;

                Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                Literal _jobCodeLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                DateTime _date = Convert.ToDateTime(_temp.TransDate);
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                ImageButton _cancelPaidButton = (ImageButton)e.Item.FindControl("CancelPaidButton");
                _cancelPaidButton.CommandName = "CancelPaid";
                _cancelPaidButton.CommandArgument = _temp.TransNmbr;

                ImageButton _viewJobOrderButton = (ImageButton)e.Item.FindControl("ViewJobOrderButton");
                _viewJobOrderButton.CommandName = "ViewDetail";
                _viewJobOrderButton.CommandArgument = _temp.TransNmbr;

                ImageButton _joinjobButton = (ImageButton)e.Item.FindControl("JoinJobOrderButton");
                _joinjobButton.CommandName = "Join";
                _joinjobButton.CommandArgument = _temp.TransNmbr;
                //_joinjobButton.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
            }
            else if (Request.QueryString["pos"].ToString() == "shipping")
            {
                POSTrShippingHd _temp = (POSTrShippingHd)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no = _page * _maxrow;
                //_no += 1;
                //_no = _nomor + _no;
                //_noLiteral.Text = _no.ToString();
                //_nomor += 1;

                Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                Literal _jobCodeLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                DateTime _date = Convert.ToDateTime(_temp.TransDate);
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                ImageButton _cancelPaidButton = (ImageButton)e.Item.FindControl("CancelPaidButton");
                _cancelPaidButton.CommandName = "CancelPaid";
                _cancelPaidButton.CommandArgument = _temp.TransNmbr;

                ImageButton _viewJobOrderButton = (ImageButton)e.Item.FindControl("ViewJobOrderButton");
                _viewJobOrderButton.CommandName = "ViewDetail";
                _viewJobOrderButton.CommandArgument = _temp.TransNmbr;

                ImageButton _joinjobButton = (ImageButton)e.Item.FindControl("JoinJobOrderButton");
                _joinjobButton.CommandName = "Join";
                _joinjobButton.CommandArgument = _temp.TransNmbr;
                //_joinjobButton.Attributes.Add("onclick", "returnValue('" + _temp.TransNmbr.Replace("'", "\\'") + "')");
            }
        }
    }

    protected void JobOrderListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            this.TransNmbrHiddenField.Value = e.CommandArgument.ToString();
            if (e.CommandName == "CancelPaid")
            {
                this.ActionHiddenField.Value = "CancelPaid";
                this.ChangeVisiblePanel(2);
                //this.ChangeVisiblePanel(1);
                //this.ReasonListRepeater.DataSource = this._reasonBL.GetList();
                //this.ReasonListRepeater.DataBind();
            }
            else if (e.CommandName == "ViewDetail")
            {
                this.ChangeVisiblePanel(0);
                if (Request.QueryString["pos"].ToString() == "retail")
                {
                    this.DetailListRepeater.DataSource = this._retailBL.GetListRetailDtByTransNmbr(e.CommandArgument.ToString());
                    this.DetailListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "internet")
                {
                    this.DetailListRepeater.DataSource = this._internetBL.GetListInternetDtByTransNmbr(e.CommandArgument.ToString());
                    this.DetailListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "cafe")
                {
                    this.DetailListRepeater.DataSource = this._cafeBL.GetListCafeDtByTransNmbr(e.CommandArgument.ToString());
                    this.DetailListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "ticketing")
                {
                    this.DetailListRepeater.DataSource = this._ticketingBL.GetListPOSTrTicketingDt(e.CommandArgument.ToString());
                    this.DetailListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "hotel")
                {
                    this.DetailListRepeater.DataSource = this._hotelBL.GetListPOSTrHotelDt(e.CommandArgument.ToString());
                    this.DetailListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "printing")
                {
                    this.DetailListRepeater.DataSource = this._printingBL.GetListPrintingDtByTransNmbr(e.CommandArgument.ToString());
                    this.DetailListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "photocopy")
                {
                    this.DetailListRepeater.DataSource = this._photocopyBL.GetListPhotocopyDtByTransNmbr(e.CommandArgument.ToString());
                    this.DetailListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "graphic")
                {
                    this.DetailListRepeater.DataSource = this._graphicBL.GetListGraphicDtByTransNmbr(e.CommandArgument.ToString());
                    this.DetailListRepeater.DataBind();
                }
                else if (Request.QueryString["pos"].ToString() == "shipping")
                {
                    this.DetailListRepeater.DataSource = this._posShippingBL.GetListShippingDtByTransNmbr(e.CommandArgument.ToString());
                    this.DetailListRepeater.DataBind();
                }
            }
            else if (e.CommandName == "Join")
            {
                this.ActionHiddenField.Value = "Join";
                this.ChangeVisiblePanel(2);
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void DetailListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (Request.QueryString["pos"].ToString() == "retail")
                {
                    POSTrRetailDt _temp = (POSTrRetailDt)e.Item.DataItem;

                    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                    _no2 = _page2 * _maxrow2;
                    _no2 += 1;
                    _no2 = _nomor2 + _no2;
                    _noLiteral.Text = _no2.ToString();
                    _nomor2 += 1;

                    Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                    _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.Qty.ToString());
                }
                else if (Request.QueryString["pos"].ToString() == "internet")
                {
                    POSTrInternetDt _temp = (POSTrInternetDt)e.Item.DataItem;

                    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                    _no2 = _page2 * _maxrow2;
                    _no2 += 1;
                    _no2 = _nomor2 + _no2;
                    _noLiteral.Text = _no2.ToString();
                    _nomor2 += 1;

                    Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                    _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.Qty.ToString());
                }
                else if (Request.QueryString["pos"].ToString() == "cafe")
                {
                    POSTrCafeDt _temp = (POSTrCafeDt)e.Item.DataItem;

                    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                    _no2 = _page2 * _maxrow2;
                    _no2 += 1;
                    _no2 = _nomor2 + _no2;
                    _noLiteral.Text = _no2.ToString();
                    _nomor2 += 1;

                    Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                    _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.Qty.ToString());
                }
                else if (Request.QueryString["pos"].ToString() == "ticketing")
                {
                    POSTrTicketingDt _temp = (POSTrTicketingDt)e.Item.DataItem;

                    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                    _no2 = _page2 * _maxrow2;
                    _no2 += 1;
                    _no2 = _nomor2 + _no2;
                    _noLiteral.Text = _no2.ToString();
                    _nomor2 += 1;

                    Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                    _productNameLiteral.Text = HttpUtility.HtmlEncode(this._airLineBL.GetSingleAirLine(_temp.AirlineCode).AirlineName);

                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.TotalGuest.ToString());
                }
                else if (Request.QueryString["pos"].ToString() == "hotel")
                {
                    POSTrHotelDt _temp = (POSTrHotelDt)e.Item.DataItem;

                    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                    _no2 = _page2 * _maxrow2;
                    _no2 += 1;
                    _no2 = _nomor2 + _no2;
                    _noLiteral.Text = _no2.ToString();
                    _nomor2 += 1;

                    Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                    _productNameLiteral.Text = HttpUtility.HtmlEncode(this._hotelBL.GetHotelNameByCode(_temp.HotelCode));

                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.TotalRoom.ToString());
                }
                else if (Request.QueryString["pos"].ToString() == "printing")
                {
                    POSTrPrintingDt _temp = (POSTrPrintingDt)e.Item.DataItem;

                    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                    _no2 = _page2 * _maxrow2;
                    _no2 += 1;
                    _no2 = _nomor2 + _no2;
                    _noLiteral.Text = _no2.ToString();
                    _nomor2 += 1;

                    Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                    _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.Qty.ToString());
                }
                else if (Request.QueryString["pos"].ToString() == "photocopy")
                {
                    POSTrPhotocopyDt _temp = (POSTrPhotocopyDt)e.Item.DataItem;

                    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                    _no2 = _page2 * _maxrow2;
                    _no2 += 1;
                    _no2 = _nomor2 + _no2;
                    _noLiteral.Text = _no2.ToString();
                    _nomor2 += 1;

                    Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                    _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.Qty.ToString());
                }
                else if (Request.QueryString["pos"].ToString() == "graphic")
                {
                    POSTrGraphicDt _temp = (POSTrGraphicDt)e.Item.DataItem;

                    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                    _no2 = _page2 * _maxrow2;
                    _no2 += 1;
                    _no2 = _nomor2 + _no2;
                    _noLiteral.Text = _no2.ToString();
                    _nomor2 += 1;

                    Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                    _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.Qty.ToString());
                }
                else if (Request.QueryString["pos"].ToString() == "shipping")
                {
                    POSTrShippingDt _temp = (POSTrShippingDt)e.Item.DataItem;

                    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                    _no2 = _page2 * _maxrow2;
                    _no2 += 1;
                    _no2 = _nomor2 + _no2;
                    _noLiteral.Text = _no2.ToString();
                    _nomor2 += 1;

                    POSTrShippingHd _posTrShippingHd = this._posShippingBL.GetSinglePOSTrShippingHd(_temp.TransNmbr);

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
                        
                        _productName = _vendor + "-" + this._posShippingBL.GetSinglePOSMsZone(_temp.ShippingTypeCode).ZoneName + "-" + _country + "." + _city;
                    }
                    else
                    {
                        POSMsShipping _posMsShipping = this._posShippingBL.GetPOSMsShipping(_temp.VendorCode, _temp.ShippingTypeCode, _temp.ProductShape, _cityCode);
                        _productName = _posMsShipping.VendorName + "-" + _posMsShipping.ShippingTypeName + "-" + _country + "." + _posMsShipping.CityName;
                    }

                    Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                    _productNameLiteral.Text = _productName;

                    String _unit = this._unitBL.GetSingle(_temp.Unit).UnitName;
                    String _productShape = _temp.ProductShape == "0" ? "Document" : "Non Document";
                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    _qtyLiteral.Text = _productShape + "-" + HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.Weight).ToString("#,#")) + " " + _unit;
                }
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
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
                if (Request.QueryString["pos"].ToString() == "retail")
                {
                    _result = this._retailBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                }
                else if (Request.QueryString["pos"].ToString() == "internet")
                {
                    _result = this._internetBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                }
                else if (Request.QueryString["pos"].ToString() == "cafe")
                {
                    _result = this._cafeBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                }
                else if (Request.QueryString["pos"].ToString() == "ticketing")
                {
                    _result = this._ticketingBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                }
                else if (Request.QueryString["pos"].ToString() == "hotel")
                {
                    _result = this._hotelBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                }
                else if (Request.QueryString["pos"].ToString() == "printing")
                {
                    _result = this._printingBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                }
                else if (Request.QueryString["pos"].ToString() == "photocopy")
                {
                    _result = this._photocopyBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                }
                else if (Request.QueryString["pos"].ToString() == "graphic")
                {
                    _result = this._graphicBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                }
                else if (Request.QueryString["pos"].ToString() == "shipping")
                {
                    _result = this._posShippingBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                }
                if (_result == true)
                {
                    this.ClearLabel();
                    this.ClearData();
                    this.ShowData();

                    this.ChangeVisiblePanel(0);
                }
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void errorhandler(Exception ex)
    {
        //ErrorHandler.Record(ex, EventLogEntryType.Error);
        String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "JOINJOBORDER");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
    }

    protected void OKButton_Click(object sender, EventArgs e)
    {
        try
        {
            String _password = this._posConfigurationBL.GetSingle("POSCancelPassword").SetValue;
            if (_password.ToLower() == this.PasswordTextBox.Text.ToLower())
            {
                if (this.ActionHiddenField.Value == "Join")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "returnValue('" + this.TransNmbrHiddenField.Value.Replace("'", "\\'") + "');", true);
                }
                else if (this.ActionHiddenField.Value == "CancelPaid")
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
}
