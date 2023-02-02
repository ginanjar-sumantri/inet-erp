using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
//using BusinessRule.POS;
using System.Collections.Generic;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Threading;

public partial class DeliveryOrder_ReceiptTime : System.Web.UI.Page
{
    protected NameValueCollectionExtractor _nvcExtractor;
    protected string _referenceNo = "referenceNo";
    private CustomerDOBL _customerDOBL = new CustomerDOBL();
    private POSRetailBL _retailBL = new POSRetailBL();
    private POSInternetBL _internetBL = new POSInternetBL();
    private POSCafeBL _cafeBL = new POSCafeBL();
    private TicketingBL _ticketingBL = new TicketingBL();
    private HotelBL _hotelBL = new HotelBL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack == true)
        {
            TextBox _hour = (TextBox)this.HourTextBox;
            _hour.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id);");

            TextBox _minute = (TextBox)this.MinuteTextBox;
            _minute.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id);");

            this.SetButton();
            this.SetTime();
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
            this.ReferenceNoHiddenField.Value = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._referenceNo), ApplicationConfig.EncryptionKey);
        }
    }

    protected void SetButton()
    {
        this.SaveImageButton.ImageUrl = "~/CSS/orange/image/btn_save_receiptTime.png";
        this.CancelImageButton.ImageUrl = "~/CSS/orange/image/btn_cancel_receiptTime.png";
        this.BackImageButton.ImageUrl = "~/CSS/orange/image/btn_back_receiptTime.png";
    }

    protected void SetTime()
    {
        if (this.HourTextBox.Text == "")
        {
            this.HourTextBox.Text = (DateTime.Now.Hour - 1).ToString();
        }
        if (this.MinuteTextBox.Text == "")
        {
            this.MinuteTextBox.Text = "00";
        }
    }

    protected void CheckValidData()
    {
        this.WarningLabel.Text = "";
        this.SetTime();
        DateTime? _dateAssign = this._customerDOBL.GetSingleTrDeliveryOrderLog(this.ReferenceNoHiddenField.Value).TransDate;
        DateTime _now = DateTime.Now;
        DateTime _dateReceipt = new DateTime(_now.Year, _now.Month, _now.Day, Convert.ToInt32(this.HourTextBox.Text), Convert.ToInt32(this.MinuteTextBox.Text), 0);
        if (_dateReceipt < _dateAssign | _dateReceipt > _now)
            this.WarningLabel.Text = "Please Input Time Between Assign Time : " + Convert.ToDateTime(_dateAssign).Hour.ToString() + ":" + Convert.ToDateTime(_dateAssign).Minute.ToString() + " until " + _now.Hour + ":" + _now.Minute;

    }

    protected void CancelImageButton_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("~/DeliveryOrder/CloseRider.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
        this.HourTextBox.Text = "";
        this.MinuteTextBox.Text = "";
    }

    protected void BackImageButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/DeliveryOrder/CloseRider.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
    }

    protected void SaveImageButton_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.CheckValidData();
            //if (Convert.ToInt32(this.HourTextBox.Text) <= 23 && Convert.ToInt32(this.HourTextBox.Text) != 0 && Convert.ToInt32(this.MinuteTextBox.Text) <= 59)
            //{
            if (this.WarningLabel.Text == "")
            {
                bool _result = false;

                POSTrDeliveryOrder _posTrDeliveryOrder = this._customerDOBL.GetSingleTrDeliveryOrder(this.ReferenceNoHiddenField.Value);
                _posTrDeliveryOrder.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Close);
                _posTrDeliveryOrder.TransDate = DateTime.Now;
                _result = this._customerDOBL.UpdatePOSTrDeliveryOrder(_posTrDeliveryOrder);

                POSTrDeliveryOrderLog _pOSTrDeliveryOrderLog = new POSTrDeliveryOrderLog();
                _pOSTrDeliveryOrderLog.ReferenceNo = this.ReferenceNoHiddenField.Value;
                _pOSTrDeliveryOrderLog.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Deliver);
                DateTime _transDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(this.HourTextBox.Text), Convert.ToInt32(this.MinuteTextBox.Text), 0);
                _pOSTrDeliveryOrderLog.TransDate = _transDate;
                _pOSTrDeliveryOrderLog.UserName = HttpContext.Current.User.Identity.Name;
                _result = this._customerDOBL.InsertPOSTrDeliveryOrderLog(_pOSTrDeliveryOrderLog);

                _pOSTrDeliveryOrderLog = new POSTrDeliveryOrderLog();
                _pOSTrDeliveryOrderLog.ReferenceNo = this.ReferenceNoHiddenField.Value;
                _pOSTrDeliveryOrderLog.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Close);
                _pOSTrDeliveryOrderLog.TransDate = DateTime.Now;
                _pOSTrDeliveryOrderLog.UserName = HttpContext.Current.User.Identity.Name;
                _result = this._customerDOBL.InsertPOSTrDeliveryOrderLog(_pOSTrDeliveryOrderLog);

                List<POSTrDeliveryOrderRef> _pOSTrDeliveryOrderRef = this._customerDOBL.GetListPOSTrDeliveryOrderRef(this.ReferenceNoHiddenField.Value);
                foreach (var _item in _pOSTrDeliveryOrderRef)
                {
                    if (_item.TransType.ToUpper() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).ToUpper())
                    {
                        _result = this._retailBL.SetDelivery(_item.TransNmbr.ToString(), true);
                    }
                    else if (_item.TransType.ToUpper() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).ToUpper())
                    {
                        _result = this._internetBL.SetDelivery(_item.TransNmbr.ToString(), true);
                    }
                    else if (_item.TransType.ToUpper() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).ToUpper())
                    {
                        _result = this._cafeBL.SetDelivery(_item.TransNmbr.ToString(), true);
                    }
                    else if (_item.TransType.ToUpper() == AppModule.GetValue(TransactionType.Hotel).ToUpper())
                    {
                        _result = this._hotelBL.SetDelivery(_item.TransNmbr.ToString(), true);
                    }
                    else if (_item.TransType.ToUpper() == AppModule.GetValue(TransactionType.Ticketing).ToUpper())
                    {
                        _result = this._ticketingBL.SetDelivery(_item.TransNmbr.ToString(), true);
                    }
                }
                if (_result == true)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "MsgBox", "alert('Data Has Been Saved');", true);
                    Response.Redirect("~/DeliveryOrder/CloseRider.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "MsgBox", "alert('Data Fail To Save.');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MsgBox", "alert('Please Check Your Input Data');", true);
            }
        }
        catch (ThreadAbortException) { throw; }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void errorhandler(Exception ex)
    {
        //ErrorHandler.Record(ex, EventLogEntryType.Error);
        String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "DO_RECEIPTTIME");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
    }
}
