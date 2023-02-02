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
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Threading;

public partial class DeliveryOrder_CloseRaider : System.Web.UI.Page
{
    private CustomerDOBL _customerDOBL = new CustomerDOBL();
    private POSRetailBL _retailBL = new POSRetailBL();
    private POSInternetBL _internetBL = new POSInternetBL();
    private POSCafeBL _cafeBL = new POSCafeBL();
    private TicketingBL _ticketingBL = new TicketingBL();
    private AirLineBL _airLineBL = new AirLineBL();
    private HotelBL _hotelBL = new HotelBL();
    private ProductBL _msProductBL = new ProductBL();
    private EmployeeBL _employeeBL = new EmployeeBL();
    private POSReasonBL _reasonBL = new POSReasonBL();
    private CashierBL _cashierBL = new CashierBL();
    private POSConfigurationBL _posConfigurationBL = new POSConfigurationBL();

    protected NameValueCollectionExtractor _nvcExtractor;
    protected string _referenceNo = "referenceNo";
    protected string _codeKey = "code";
    protected string _settleType = "settleType";

    //private int _page;
    //private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
    //private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
    //private int _no = 0;
    //private int _nomor = 0;

    //private int _page2;
    //private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
    //private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
    //private int _no2 = 0;
    //private int _nomor2 = 0;

    //private int _page3;
    //private int _maxrow3 = Convert.ToInt32(ApplicationConfig.ListPageSize);
    //private int _maxlength3 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
    //private int _no3 = 0;
    //private int _nomor3 = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack == true)
        {
            this.ReferenceNoTextBox.Attributes["onclick"] = "ReferenceKeyBoard(this.id)";
            this.PasswordTextBox.Attributes["onclick"] = "PasswordKeyBoard(this.id)";
            String spawnJS = "<script type='text/javascript' language='JavaScript'>\n";
            //DECLARE FUNCTION FOR Calling KeyBoard Reference
            spawnJS += "function ReferenceKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findReference&titleinput=Reference&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Reference
            spawnJS += "function findReference(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.ReferenceNoTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR Calling KeyBoard Password
            spawnJS += "function PasswordKeyBoard(x) {\n";
            spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findPassword&titleinput=Password&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
            spawnJS += "}\n";

            //DECLARE FUNCTION FOR CATCHING ON Password
            spawnJS += "function findPassword(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.PasswordTextBox.ClientID + "').value = dataArray [0];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            spawnJS += "</script>\n";
            this.javascriptReceiver.Text = spawnJS;

            this.ClearLabel();
            this.ClearData();
            this.ChangeVisiblePanel(0);

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
            this.ReferenceNoHiddenField.Value = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._referenceNo), ApplicationConfig.EncryptionKey);
            this.ShowData();
        }
    }

    public void ClearLabel()
    {
        this.WarningLabel.Text = "";
    }

    public void ClearData()
    {
        this.ReferenceNoTextBox.Text = "";
        this.ReferenceNoHiddenField.Value = "";
        //this.CustomerCodeHiddenField.Value = "";
        //this.TransNumberHiddenField.Value = "";
    }

    public void ShowData()
    {
        try
        {
            String _referenceKey = "";
            if (this.ReferenceNoHiddenField.Value != "")
            {
                _referenceKey = this.ReferenceNoHiddenField.Value;
            }
            else
            {
                _referenceKey = this.ReferenceNoTextBox.Text;
            }
            List<POSTrDeliveryOrder> _posTrDeliveryOrder = this._customerDOBL.GetListPOSTrDeliveryOrder(_referenceKey, POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Delivering));
            List<POSTrDeliveryOrder> _posTrDeliveryOrderClose = this._customerDOBL.GetListPOSTrDeliveryOrder(_referenceKey, POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Close));
            foreach (var _row in _posTrDeliveryOrderClose)
            {
                _posTrDeliveryOrder.Add(_row);
            }
            this.DeliveryListRepeater.DataSource = _posTrDeliveryOrder;
            this.DeliveryListRepeater.DataBind();
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void ChangeVisiblePanel(Byte _prmValue)
    {
        if (_prmValue == 0)
        {
            this.ReasonListPanelCR.Visible = true;
            this.PasswordPanel.Visible = false;
        }
        else if (_prmValue == 1)
        {
            this.ReasonListPanelCR.Visible = true;
            this.PasswordPanel.Visible = false;
        }
        else if (_prmValue == 2)
        {
            this.ReasonListPanelCR.Visible = false;
            this.PasswordPanel.Visible = true;
        }
        //try
        //{
        //    if (_prmValue == 0)
        //    {
        //        this.ReasonListRepeater.DataSource = null;
        //    }
        //    else
        //    {
        //        this.ReasonListRepeater.DataSource = this._reasonBL.GetList();
        //    }
        //    this.ReasonListRepeater.DataBind();
        //}
        //catch (Exception ex)
        //{
        //    this.errorhandler(ex);
        //}
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.ClearLabel();
        //this.ClearData();
        this.ChangeVisiblePanel(0);
        this.ShowData();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.ClearLabel();
        this.ClearData();
        this.ChangeVisiblePanel(0);
        this.ShowData();
    }

    protected void BackButton_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("~/DeliveryOrder/ListDeliveryOrder.aspx");
        Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey)));
    }

    protected void AssignRider_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/DeliveryOrder/AssignRider.aspx");
    }

    protected void DeliveryListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSTrDeliveryOrder _temp = (POSTrDeliveryOrder)e.Item.DataItem;

                string _referenceNo = _temp.ReferenceNo.ToString();

                Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                POSTrDeliveryOrderLog _pOSTrDeliveryOrderLog = this._customerDOBL.GetSingleTrDeliveryOrderLogByDriver(_temp.ReferenceNo, POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Delivering));
                String _msEmployeeName = this._employeeBL.GetEmpNameByCode(_pOSTrDeliveryOrderLog.UserName.Trim());
                Literal _driverLiteral = (Literal)e.Item.FindControl("DriverLiteral");
                _driverLiteral.Text = _msEmployeeName;

                Literal _transDateLiteral = (Literal)e.Item.FindControl("DatetimeLiteral");
                DateTime _date = Convert.ToDateTime(_temp.TransDate);
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                if (_temp.Status == POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Delivering))
                {
                    //ImageButton _doneButton = (ImageButton)e.Item.FindControl("DoneButton");
                    //_doneButton.Visible = false;

                    ImageButton _receiptButton = (ImageButton)e.Item.FindControl("ReceiptButton");
                    _receiptButton.CommandName = "ReceiptButton";
                    _receiptButton.CommandArgument = _referenceNo;

                    ImageButton _cancelButton = (ImageButton)e.Item.FindControl("CancelButton");
                    _cancelButton.CommandName = "CancelButton";
                    _cancelButton.CommandArgument = _referenceNo;

                    ImageButton _paidButton = (ImageButton)e.Item.FindControl("PaidButton");
                    _paidButton.Visible = false;
                }
                else if (_temp.Status == POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Close))
                {
                    //ImageButton _doneButton = (ImageButton)e.Item.FindControl("DoneButton");
                    //_doneButton.Visible = false;

                    ImageButton _receiptButton = (ImageButton)e.Item.FindControl("ReceiptButton");
                    _receiptButton.Visible = false;

                    ImageButton _cancelButton = (ImageButton)e.Item.FindControl("CancelButton");
                    _cancelButton.Visible = false;

                    ImageButton _paidButton = (ImageButton)e.Item.FindControl("PaidButton");
                    _paidButton.CommandName = "PaidButton";
                    _paidButton.CommandArgument = _referenceNo;
                }
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void DeliveryListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            //bool _result = false;
            string[] _splitCode = e.CommandArgument.ToString().Split(',');
            this.ReferenceNoHiddenField.Value = _splitCode[0];
            if (e.CommandName == "ReceiptButton")
            {
                this.ChangeVisiblePanel(0);
                Response.Redirect("~/DeliveryOrder/ReceiptTime.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
            }

            if (e.CommandName == "CancelButton")
            {
                //this.ChangeVisiblePanel(1);
                this.ChangeVisiblePanel(2);
            }

            if (e.CommandName == "PaidButton")
            {
                this.ChangeVisiblePanel(0);
                String _strTransNmbr = "";
                List<POSTrDeliveryOrderRef> _posTrDeliveryOrderRef = this._customerDOBL.GetListPOSTrDeliveryOrderRef(this.ReferenceNoHiddenField.Value);
                foreach (var _row in _posTrDeliveryOrderRef)
                {
                    V_POSReferenceNotYetPayListAll _cek = this._cashierBL.GetSingleReferenceNotPayAll(_row.TransNmbr,_row.TransType);
                    if (_cek != null)
                    {
                        if (_strTransNmbr == "")
                        {
                            _strTransNmbr = _row.TransNmbr;
                        }
                        else
                        {
                            _strTransNmbr = _strTransNmbr + "," + _row.TransNmbr;
                        }
                    }
                }
                //Response.Redirect("~/General/Settlement.aspx");
                if (_strTransNmbr != "")
                {
                    //Response.Redirect("~/General/Settlement.aspx" + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_strTransNmbr, ApplicationConfig.EncryptionKey)) + "&" + this._settleType + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt("DOPaid", ApplicationConfig.EncryptionKey)));
                    Response.Redirect("~/General/Settlement.aspx" + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_strTransNmbr, ApplicationConfig.EncryptionKey)) + "&" + this._settleType + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "MsgBox", "alert('Nothing Transaction Found');", true);
                }
            }
        }
        catch (ThreadAbortException) { throw; }
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
                List<POSTrDeliveryOrderRef> _posTrDeliveryOrderRef = this._customerDOBL.GetListPOSTrDeliveryOrderRef(this.ReferenceNoHiddenField.Value);
                foreach (var _row in _posTrDeliveryOrderRef)
                {
                    if (_row.TransType == "RETAIL")
                    {
                        _result = this._retailBL.SetVOID(_row.TransNmbr, e.CommandArgument.ToString(), true);
                    }
                    else if (_row.TransType == "INTERNET")
                    {
                        _result = this._internetBL.SetVOID(_row.TransNmbr, e.CommandArgument.ToString(), true);
                    }
                    else if (_row.TransType == "CAFE")
                    {
                        _result = this._cafeBL.SetVOID(_row.TransNmbr, e.CommandArgument.ToString(), true);
                    }
                    else if (_row.TransType == "TICKETING")
                    {
                        _result = this._ticketingBL.SetVOID(_row.TransNmbr, e.CommandArgument.ToString(), true);
                    }
                    else if (_row.TransType == "HOTEL")
                    {
                        _result = this._hotelBL.SetVOID(_row.TransNmbr, e.CommandArgument.ToString(), true);
                    }
                }
                POSTrDeliveryOrder _posTrDeliveryOrder = this._customerDOBL.GetSingleTrDeliveryOrder(this.ReferenceNoHiddenField.Value);
                _posTrDeliveryOrder.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Cancel);
                //_posTrDeliveryOrder.TransDate = DateTime.Now;
                _posTrDeliveryOrder.IsVoid = true;
                _posTrDeliveryOrder.Reason = Convert.ToInt32(e.CommandArgument);
                _result = this._customerDOBL.UpdatePOSTrDeliveryOrder(_posTrDeliveryOrder);

                POSTrDeliveryOrderLog _pOSTrDeliveryOrderLog = new POSTrDeliveryOrderLog();
                _pOSTrDeliveryOrderLog.ReferenceNo = this.ReferenceNoHiddenField.Value;
                _pOSTrDeliveryOrderLog.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Close);
                _pOSTrDeliveryOrderLog.TransDate = DateTime.Now;
                _pOSTrDeliveryOrderLog.UserName = HttpContext.Current.User.Identity.Name;
                _result = this._customerDOBL.InsertPOSTrDeliveryOrderLog(_pOSTrDeliveryOrderLog);

                _pOSTrDeliveryOrderLog = new POSTrDeliveryOrderLog();
                _pOSTrDeliveryOrderLog.ReferenceNo = this.ReferenceNoHiddenField.Value;
                _pOSTrDeliveryOrderLog.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Cancel);
                _pOSTrDeliveryOrderLog.TransDate = DateTime.Now;
                _pOSTrDeliveryOrderLog.UserName = HttpContext.Current.User.Identity.Name;
                _result = this._customerDOBL.InsertPOSTrDeliveryOrderLog(_pOSTrDeliveryOrderLog);

                if (_result == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "MsgBox", "alert('Data Has Been Cancelled');", true);
                    this.ClearLabel();
                    this.ClearData();
                    this.ChangeVisiblePanel(0);
                    this.ReasonListRepeater.DataSource = "";
                    this.ReasonListRepeater.DataBind();
                    this.ShowData();
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
        String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "DO_CLOSERIDER");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
    }

    protected void OKButton_Click(object sender, EventArgs e)
    {
        try
        {
            String _password = this._posConfigurationBL.GetSingle("POSCancelPassword").SetValue;
            if (_password.ToLower() == this.PasswordTextBox.Text.ToLower())
            {
                this.ChangeVisiblePanel(1);
                this.ReasonListRepeater.DataSource = this._reasonBL.GetList();
                this.ReasonListRepeater.DataBind();
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

    protected void CancellButton_Click(object sender, EventArgs e)
    {

        this.ChangeVisiblePanel(0);
    }

}