using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Web.UI.HtmlControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using POS.POSInterface;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Threading;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Web.Security;

public partial class Login : POSInterfaceBase
{
    RoleBL _roleBL = new RoleBL();
    PermissionBL _permissionBL = new PermissionBL();
    protected String _floor;

    protected void Page_Load(object sender, EventArgs e)
    {
        master_Company _masterCompany = new CompanyBL().GetCompanyIDForPOS();
        HiddenField _companyID = (HiddenField)this.Login1.FindControl("CompanyIDHiddenField");

        if (!this.Page.IsPostBack == true)
        {
            TextBox _userName1 = (TextBox)this.Login1.FindControl("UserName");
            _userName1.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id);");

            //this.UserNameTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id);");

            TextBox _password1 = (TextBox)this.Login1.FindControl("Password");
            _password1.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id);");

            //this.PasswordTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id);");

            this.bodyLogin.Attributes.Add("onload", "if (self.parent.frames.length != 0)self.parent.location=document.location;");

            this.AppNameLiteral.Text = ApplicationConfig.MembershipAppName + " :: Login";

            Literal _failureText = (Literal)this.Login1.FindControl("FailureText2");
            _failureText.Text = Request.QueryString["FailureText"];

            this.StyleSheetLiteral.Text = "<link type=\"text/css\" href=\"" + ApplicationConfig.HomeWebAppURL + "StyleSheet.css\" rel=\"Stylesheet\" />";

            _companyID.Value = _masterCompany.CompanyID.ToString();

            HiddenField _companyName = (HiddenField)this.Login1.FindControl("CompanyNameHiddenField");
            _companyName.Value = _masterCompany.Name;

            this.BindDataToInstanceDDL(new Guid(_companyID.Value));

            Panel _tourPanel = (Panel)this.Login1.FindControl("TourPanel");
            _tourPanel.Visible = false;

            HttpContext.Current.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);

            //String spawnJS = "<script language='JavaScript'>\n";
            //spawnJS += "$(document).ready(function() {\n";
            //spawnJS += "$('.table-container-scroll').dragscrollable({ dragSelector: '.dragger td', acceptPropagatedEvent: false });\n";
            //spawnJS += "$('.table-container-scroll2').dragscrollable({ dragSelector: '.dragger2 td', acceptPropagatedEvent: false });\n";
            //spawnJS += "$('.table-container-scroll3').dragscrollable({ dragSelector: '.dragger3 td', acceptPropagatedEvent: false });\n";
            //spawnJS += "$('#KeyboardToggle').click(function() {\n";
            //spawnJS += "if ($('#KeyboardSlider').css('display') == 'none')\n";
            //spawnJS += "$('#KeyboardSlider').slideDown('slow');\n";
            //spawnJS += "else\n";
            //spawnJS += "$('#KeyboardSlider').slideUp('slow');\n";
            //spawnJS += "});\n";
            //spawnJS += "var inputselected;\n";
            //spawnJS += "$('#" + _userName1.ClientID + "').click(function() {\n";
            //spawnJS += "inputselected = '#" + _userName1.ClientID + "';";
            //spawnJS += "});\n";
            //spawnJS += "$('#" + _password1.ClientID + "').click(function() {\n";
            //spawnJS += "inputselected = '#" + _password1.ClientID + "';\n";
            //spawnJS += "});\n";
            //spawnJS += "$('.KeyboardDiv input').click(function() {\n";
            //spawnJS += "if (this.value == '^^^^^') {\n";
            //spawnJS += "if ($('#KeyBoardDivID0').css('display') == 'none')\n";
            //spawnJS += "$('#KeyBoardDivID1').fadeOut('fast', function() { $('#KeyBoardDivID0').fadeIn('fast'); });\n";
            //spawnJS += "else\n";
            //spawnJS += "$('#KeyBoardDivID0').fadeOut('fast', function() { $('#KeyBoardDivID1').fadeIn('fast'); });\n";
            //spawnJS += "} else if (this.value == 'ENTER') {\n";
            //spawnJS += "$('#KeyboardSlider').slideUp('slow');\n";
            //spawnJS += "} else if (this.value == 'SPACE') {\n";
            //spawnJS += "$(inputselected).val($(inputselected).val() + ' ');\n";
            //spawnJS += "} else if (this.value == 'BACKSPACE') {\n";
            //spawnJS += "if ($(inputselected).val().length > 0)\n";
            //spawnJS += "$(inputselected).val($(inputselected).val().substr(0, $(inputselected).val().length - 1));\n";
            //spawnJS += "} else {\n";
            //spawnJS += "$(inputselected).val($(inputselected).val() + this.value);\n";
            //spawnJS += "}\n";
            //spawnJS += "});\n";
            //spawnJS += "});\n";
            //spawnJS += "</script>\n";

            //this.javascriptReceiver.Text = spawnJS;

        }

        _companyID.Value = _masterCompany.CompanyID.ToString();
        //TextBox _userName2 = (TextBox)this.Login1.FindControl("UserName");
        //_userName2.Text = this.UserNameTextBox.Text;        

        //TextBox _password2 = (TextBox)this.Login1.FindControl("Password");
        //_password2.Text = this.PasswordTextBox.Text;

        Button _loginbutton = (Button)this.Login1.FindControl("LoginButton");
        LoginStatus _logout = (LoginStatus)this.Login1.FindControl("LoginStatus1");
        HttpCookie cookie = Request.Cookies["Preferences"];
        if (cookie == null)// sebelum login
        {
            _loginbutton.Visible = true;
            _logout.Visible = false;
            this.ButtonOff();
            Button _openShiftButton = (Button)this.Login1.FindControl("OpenShiftButton");
            _openShiftButton.CssClass = "OpenShiftOff";
            _openShiftButton.Enabled = false;
        }
        else
        {
            if (cookie["Name"] != "")// sudah login
            {
                TextBox _userName = (TextBox)this.Login1.FindControl("UserName");
                DropDownList _connMode = (DropDownList)this.Login1.FindControl("ConnModeDropDownList");

                _userName.Text = cookie["Name"];
                _companyID.Value = cookie["Company"];
                this.BindDataToInstanceDDL(new Guid(_companyID.Value));
                _connMode.SelectedValue = cookie["Instance"];
                _loginbutton.Visible = false;
                _logout.Visible = true;
                this.ButtonOn(cookie["Name"]);
                this.CheckShift();
            }
            else // keluar login
            {
                _loginbutton.Visible = true;
                _logout.Visible = false;
                this.ButtonOff();
                Button _openShiftButton = (Button)this.Login1.FindControl("OpenShiftButton");
                _openShiftButton.CssClass = "OpenShiftOff";
                _openShiftButton.Enabled = false;
            }
        }

        
    }

    private void ButtonOff() ///////// Button Menu yang Off
    {
        //Button _memberButton = (Button)this.Login1.FindControl("MemberButton");
        //_memberButton.CssClass = "MemberButtonOff";
        //_memberButton.Enabled = false;

        Button _deliveryOrderButton = (Button)this.Login1.FindControl("DeliveryOrderButton");
        _deliveryOrderButton.CssClass = "DeliveryOrderButtonOff";
        _deliveryOrderButton.Enabled = false;

        Button _tourButton = (Button)this.Login1.FindControl("TourButton");
        _tourButton.CssClass = "TourButtonOff";
        _tourButton.Enabled = false;

        Button _ticketButton = (Button)this.Login1.FindControl("TicketingButton");
        _ticketButton.CssClass = "TicketingButtonOff";
        _ticketButton.Enabled = false;

        Button _hotelButton = (Button)this.Login1.FindControl("HotelButton");
        _hotelButton.CssClass = "HotelButtonOff";
        _hotelButton.Enabled = false;

        Button _printingButton = (Button)this.Login1.FindControl("PrintingButton");
        _printingButton.CssClass = "PrintingButtonOff";
        _printingButton.Enabled = false;

        Button _photoCopyButton = (Button)this.Login1.FindControl("PhotoCopyButton");
        _photoCopyButton.CssClass = "PhotoCopyButtonOff";
        _photoCopyButton.Enabled = false;

        Button _shippingButton = (Button)this.Login1.FindControl("ShippingButton");
        _shippingButton.CssClass = "ShippingButtonOff";
        _shippingButton.Enabled = false;

        Button _internetButton = (Button)this.Login1.FindControl("InternetButton");
        _internetButton.CssClass = "InternetButtonOff";
        _internetButton.Enabled = false;

        Button _stationaryButton = (Button)this.Login1.FindControl("StationaryButton");
        _stationaryButton.CssClass = "StationaryButtonOff";
        _stationaryButton.Enabled = false;

        Button _grafikDesainButton = (Button)this.Login1.FindControl("GrafikDesainButton");
        _grafikDesainButton.CssClass = "GrafikDesainButtonOff";
        _grafikDesainButton.Enabled = false;

        Button _voucherButton = (Button)this.Login1.FindControl("VoucherButton");
        _voucherButton.CssClass = "VoucherButtonOff";
        _voucherButton.Enabled = false;

        Button _cafeButton = (Button)this.Login1.FindControl("CafeButton");
        _cafeButton.CssClass = "CafeButtonOff";
        _cafeButton.Enabled = false;

        Button _cashierButton = (Button)this.Login1.FindControl("CashierButton");
        _cashierButton.CssClass = "CashierButtonOff";
        _cashierButton.Enabled = false;

        Button _monitoringButton = (Button)this.Login1.FindControl("MonitoringButton");
        _monitoringButton.CssClass = "MonitoringButtonOff";
        _monitoringButton.Enabled = false;

        Button _backButton = (Button)this.Login1.FindControl("BackButton");
        _backButton.CssClass = "BackButtonOff";
        _backButton.Enabled = false;

        //Button _openShiftButton = (Button)this.Login1.FindControl("OpenShiftButton");
        //_openShiftButton.CssClass = "";
        //_openShiftButton.Enabled = false;

    }

    private void ButtonOn(String _prmName) ///////// Button Menu yang On
    {
        try
        {
            //this._permAccessMember = this._permissionBL.PermissionValidation1(this._menuIDMember, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            //Button _memberButton = (Button)this.Login1.FindControl("MemberButton");
            //if (this._permAccessMember == PermissionLevel.NoAccess)
            //{
            //    _memberButton.CssClass = "MemberButtonOff";
            //    _memberButton.Enabled = false;
            //}
            //else
            //{
            //    _memberButton.CssClass = "";
            //    _memberButton.Enabled = true;
            //} 

            this._permDeliveryOrder = this._permissionBL.PermissionValidation1(this._menuIDDeliveryOrder, _prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            Button _deliveryOrderButton = (Button)this.Login1.FindControl("DeliveryOrderButton");
            if (this._permDeliveryOrder == PermissionLevel.NoAccess)
            {
                _deliveryOrderButton.CssClass = "DeliveryOrderButtonOff";
                _deliveryOrderButton.Enabled = false;
            }
            else
            {
                _deliveryOrderButton.CssClass = "";
                _deliveryOrderButton.Enabled = true;
            }

            Button _tourButton = (Button)this.Login1.FindControl("TourButton");
            _tourButton.CssClass = "";
            _tourButton.Enabled = true;

            Button _backButton = (Button)this.Login1.FindControl("BackButton");
            _backButton.CssClass = "";
            _backButton.Enabled = true;

            this._permAccessTicketing = this._permissionBL.PermissionValidation1(this._menuIDTicketing, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            Button _ticketingButton = (Button)this.Login1.FindControl("TicketingButton");
            if (this._permAccessTicketing == PermissionLevel.NoAccess)
            {
                _ticketingButton.CssClass = "TicketingButtonOff";
                _ticketingButton.Enabled = false;
            }
            else
            {
                _ticketingButton.CssClass = "";
                _ticketingButton.Enabled = true;
            }

            this._permAccessHotel = this._permissionBL.PermissionValidation1(this._menuIDHotel, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            Button _hotelButton = (Button)this.Login1.FindControl("HotelButton");
            if (this._permAccessHotel == PermissionLevel.NoAccess)
            {
                _hotelButton.CssClass = "HotelButtonOff";
                _hotelButton.Enabled = false;
            }
            else
            {
                _hotelButton.CssClass = "";
                _hotelButton.Enabled = true;
            }

            this._permAccessPrinting = this._permissionBL.PermissionValidation1(this._menuIDPrinting, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            Button _printingButton = (Button)this.Login1.FindControl("PrintingButton");
            if (this._permAccessPrinting == PermissionLevel.NoAccess)
            {
                _printingButton.CssClass = "PrintingButtonOff";
                _printingButton.Enabled = false;
            }
            else
            {
                _printingButton.CssClass = "";
                _printingButton.Enabled = true;
            }

            this._permAccessPhotocopy = this._permissionBL.PermissionValidation1(this._menuIDPhotocopy, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            Button _photoCopyButton = (Button)this.Login1.FindControl("PhotoCopyButton");
            if (this._permAccessPhotocopy == PermissionLevel.NoAccess)
            {
                _photoCopyButton.CssClass = "PhotoCopyButtonOff";
                _photoCopyButton.Enabled = false;
            }
            else
            {
                _photoCopyButton.CssClass = "";
                _photoCopyButton.Enabled = true;
            }

            this._permAccessShipping = this._permissionBL.PermissionValidation1(this._menuIDShipping, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            Button _shippingButton = (Button)this.Login1.FindControl("ShippingButton");
            if (this._permAccessShipping == PermissionLevel.NoAccess)
            {
                _shippingButton.CssClass = "ShippingButtonOff";
                _shippingButton.Enabled = false;
            }
            else
            {
                _shippingButton.CssClass = "";
                _shippingButton.Enabled = true;
            }

            this._permAccessInternet = this._permissionBL.PermissionValidation1(this._menuIDInternet, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            Button _internetButton = (Button)this.Login1.FindControl("InternetButton");
            if (this._permAccessInternet == PermissionLevel.NoAccess)
            {
                _internetButton.CssClass = "InternetButtonOff";
                _internetButton.Enabled = false;
            }
            else
            {
                _internetButton.CssClass = "";
                _internetButton.Enabled = true;
            }

            this._permAccessStationary = this._permissionBL.PermissionValidation1(this._menuIDStationary, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            Button _stationaryButton = (Button)this.Login1.FindControl("StationaryButton");
            if (this._permAccessStationary == PermissionLevel.NoAccess)
            {
                _stationaryButton.CssClass = "StationaryButtonOff";
                _stationaryButton.Enabled = false;
            }
            else
            {
                _stationaryButton.CssClass = "";
                _stationaryButton.Enabled = true;
            }

            this._permAccessGraphicDesign = this._permissionBL.PermissionValidation1(this._menuIDGraphicDesign, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            Button _grafikDesainButton = (Button)this.Login1.FindControl("GrafikDesainButton");
            if (this._permAccessGraphicDesign == PermissionLevel.NoAccess)
            {
                _grafikDesainButton.CssClass = "GrafikDesainButtonOff";
                _grafikDesainButton.Enabled = false;
            }
            else
            {
                _grafikDesainButton.CssClass = "";
                _grafikDesainButton.Enabled = true;
            }

            this._permAccessEVoucher = this._permissionBL.PermissionValidation1(this._menuIDEVoucher, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            Button _voucherButton = (Button)this.Login1.FindControl("VoucherButton");
            if (this._permAccessEVoucher == PermissionLevel.NoAccess)
            {
                _voucherButton.CssClass = "VoucherButtonOff";
                _voucherButton.Enabled = false;
            }
            else
            {
                _voucherButton.CssClass = "";
                _voucherButton.Enabled = true;
            }

            this._permAccessCafe = this._permissionBL.PermissionValidation1(this._menuIDCafe, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            Button _cafeButton = (Button)this.Login1.FindControl("CafeButton");
            if (this._permAccessCafe == PermissionLevel.NoAccess)
            {
                _cafeButton.CssClass = "CafeButtonOff";
                _cafeButton.Enabled = false;

            }
            else
            {
                _cafeButton.CssClass = "";
                _cafeButton.Enabled = true;
            }

            this._permAccessSendToCashier = this._permissionBL.PermissionValidation1(this._menuIDSendToCashier, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            Button _cashierButton = (Button)this.Login1.FindControl("CashierButton");
            if (this._permAccessSendToCashier == PermissionLevel.NoAccess)
            {
                _cashierButton.CssClass = "CashierButtonOff";
                _cashierButton.Enabled = false;
            }
            else
            {
                _cashierButton.CssClass = "";
                _cashierButton.Enabled = true;
            }

            this._permAccessMonitoring = this._permissionBL.PermissionValidation1(this._menuIDSendToCashier, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            Button _monitoringButton = (Button)this.Login1.FindControl("MonitoringButton");
            if (this._permAccessMonitoring == PermissionLevel.NoAccess)
            {
                _monitoringButton.CssClass = "MonitoringButtonOff";
                _monitoringButton.Enabled = false;
            }
            else
            {
                _monitoringButton.CssClass = "";
                _monitoringButton.Enabled = true;
            }

            //this._permAccessMonitoring = this._permissionBL.PermissionValidation1(this._menuIDSendToCashier, /*HttpContext.Current.User.Identity.Name*/_prmName, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);
            //Button _openShiftButton = (Button)this.Login1.FindControl("OpenShiftButton");
            //if (this._permAccessMonitoring == PermissionLevel.NoAccess)
            //{
            //    _openShiftButton.CssClass = "OpenShiftOff";
            //    _openShiftButton.Enabled = false;
            //}
            //else
            //{
            //    _openShiftButton.CssClass = "";
            //    _openShiftButton.Enabled = true;
            //}

        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        try
        {
            UserBL _user = new UserBL();

            TextBox _userName = (TextBox)this.Login1.FindControl("UserName");
            TextBox _password = (TextBox)this.Login1.FindControl("Password");
            DropDownList _connMode = (DropDownList)this.Login1.FindControl("ConnModeDropDownList");
            //DropDownList _compList = (DropDownList)this.Login1.FindControl("CompanyDropDownList");
            HiddenField _companyID = (HiddenField)this.Login1.FindControl("CompanyIDHiddenField");
            HiddenField _companyName = (HiddenField)this.Login1.FindControl("CompanyNameHiddenField");

            _user.SaveLastConnectionMode(_userName.Text, _companyID.Value, _connMode.SelectedValue);

            master_Company_aspnet_User _compAndUser = new CompanyBL().GetSingleCompanyUser(_companyID.Value, _user.GetUserIDByName(_userName.Text));
            if (_compAndUser == null)
            {
                Response.Redirect(ApplicationConfig.LoginPage + "?FailureText=" + System.Web.HttpUtility.UrlEncode("User not associated with the company(" + _companyName.Value + ")."));
            }
            else
            {
                Boolean _IsUserAssosiatedWithTheDB = new CompanyBL().GetSingleDatabaseUser(new Guid(_connMode.SelectedValue), _compAndUser.UserID);

                if (_IsUserAssosiatedWithTheDB == true)
                {
                    this.LoginButtonClick();
                    this.Login1.DestinationPageUrl = "Login.aspx";
                    Response.Redirect(this.Login1.DestinationPageUrl);
                }
                else
                {
                    Response.Redirect(ApplicationConfig.LoginPage + "?FailureText=" + System.Web.HttpUtility.UrlEncode("User not associated with the database(" + _connMode.SelectedItem.Text + ") of " + _companyName.Value + "."));
                }
            }
        }
        catch (ThreadAbortException) { throw; }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    //protected void CompanyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //DropDownList _compList = (DropDownList)this.Login1.FindControl("CompanyDropDownList");
    //    HiddenField _companyID = (HiddenField)this.Login1.FindControl("CompanyIDHiddenField");

    //    this.BindDataToInstanceDDL(new Guid(_companyID.Value));
    //}

    private Boolean BindDataToInstanceDDL(Guid _prmCompID)
    {
        Boolean _result = false;

        try
        {
            DropDownList _instanceList = (DropDownList)this.Login1.FindControl("ConnModeDropDownList");
            _instanceList.Items.Clear();

            List<master_Database> _database = new List<master_Database>();
            _database = new CompanyBL().GetListDatabaseByCompany(_prmCompID);
            foreach (var _item in _database)
            {
                _instanceList.Items.Add(new ListItem(ConnectionModeMapper.GetLabel(ConnectionModeMapper.MapThis(_item.Status)), _item.DatabaseID.ToString()));
            }
        }
        catch (Exception)
        {

        }

        return _result;
    }

    protected void LoginButtonClick()
    {
        TextBox _userName = (TextBox)this.Login1.FindControl("UserName");
        DropDownList _connMode = (DropDownList)this.Login1.FindControl("ConnModeDropDownList");
        //DropDownList _compList = (DropDownList)this.Login1.FindControl("CompanyDropDownList");
        HiddenField _CompanyID = (HiddenField)this.Login1.FindControl("CompanyIDHiddenField");

        HttpCookie cookie = Request.Cookies["Preferences"];
        if (cookie == null)
        {
            cookie = new HttpCookie("Preferences");
        }

        cookie["Name"] = _userName.Text;
        cookie["Instance"] = _connMode.SelectedValue;
        cookie["Company"] = _CompanyID.Value;
        //cookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt32(ApplicationConfig.LoginLifeTimeExpired));
        Response.Cookies.Add(cookie);
    }

    protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        HttpCookie cookie = Request.Cookies["Preferences"];
        if (cookie == null)
        {
            cookie = new HttpCookie("Preferences");
        }

        cookie["Name"] = "";
        cookie["Instance"] = "";
        cookie["Company"] = "";
        // cookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt32(ApplicationConfig.LoginLifeTimeExpired));
        Response.Cookies.Add(cookie);
        //Response.Cookies.Remove("");
    }

    protected void TourButton_Click(object sender, EventArgs e)
    {
        Panel _generalPanel = (Panel)this.Login1.FindControl("GeneralPanel");
        _generalPanel.Visible = false;

        Panel _tourPanel = (Panel)this.Login1.FindControl("TourPanel");
        _tourPanel.Visible = true;
    }

    protected void BackButton_Click(object sender, EventArgs e)
    {
        Panel _generalPanel = (Panel)this.Login1.FindControl("GeneralPanel");
        _generalPanel.Visible = true;

        Panel _tourPanel = (Panel)this.Login1.FindControl("TourPanel");
        _tourPanel.Visible = false;
    }

    protected void InternetButton_Click(object sender, EventArgs e)
    {
        try
        {
            POSInternetBL _internetBL = new POSInternetBL();
            _floor = _internetBL.GetSinglePOSMsInternetFloor("Internet");
            Response.Redirect("Internet/POSInternet.aspx" + "?" + "selectedFloor=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_floor, ApplicationConfig.EncryptionKey)));
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void CafeButton_Click(object sender, EventArgs e)
    {
        try
        {
            POSInternetBL _internetBL = new POSInternetBL();
            _floor = _internetBL.GetSinglePOSMsInternetFloor("Cafe");
            Response.Redirect("Cafe/POSCafe.aspx" + "?" + "selectedFloor=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_floor, ApplicationConfig.EncryptionKey)));
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void DeliveryOrderButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey)));
    }

    protected void PrintingButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Printing/POSPrinting.aspx" + "?" + "referenceNo=" +
                        HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey)));
    }

    protected void PhotoCopyButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Photocopy/POSPhotocopy.aspx" + "?" + "referenceNo=" +
                        HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey)));
    }

    protected void GrafikDesainButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Graphic/POSGraphic.aspx" + "?" + "referenceNo=" +
                        HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey)));
    }

    protected void errorhandler(Exception ex)
    {
        //ErrorHandler.Record(ex, EventLogEntryType.Error);
        String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "LOGIN");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
    }

    protected void CheckShift()
    {
        User_EmployeeBL _userEmployeeBL = new User_EmployeeBL();
        UserBL _userBL = new UserBL();
        CloseShiftBL _closeShiftBL = new CloseShiftBL();

        //String _employeeId = _userEmployeeBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name);
        //Guid _userId = _userEmployeeBL.GetUserIdEmpId(_employeeId);
        String _employeeId = _userEmployeeBL.GetEmployeeIDByUserName(HttpContext.Current.User.Identity.Name);
        Guid _userId = _userBL.GetUserIDByName(HttpContext.Current.User.Identity.Name);
        POSTrShiftLog _posTrShiftLog = _closeShiftBL.GetSinglePOSTrShiftLog(_employeeId, _userId);
        if (_posTrShiftLog != null)
        {
            Button _openShiftButton = (Button)this.Login1.FindControl("OpenShiftButton");
            _openShiftButton.CssClass = "OpenShiftOff";
            _openShiftButton.Enabled = false;
        }
        else
        {
            Button _openShiftButton = (Button)this.Login1.FindControl("OpenShiftButton");
            _openShiftButton.CssClass = "OpenShiftOn";
            _openShiftButton.Enabled = true;
            this.ButtonOff();
        }
    }
}