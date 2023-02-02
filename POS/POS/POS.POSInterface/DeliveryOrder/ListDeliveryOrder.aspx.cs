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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

public partial class DeliveryOrder_listDeliveryOrder : System.Web.UI.Page
{
    private CustomerDOBL _customerDOBL = new CustomerDOBL();
    private POSReasonBL _reasonBL = new POSReasonBL();
    private POSRetailBL _retailBL = new POSRetailBL();
    private POSInternetBL _internetBL = new POSInternetBL();
    private POSCafeBL _cafeBL = new POSCafeBL();
    private TicketingBL _ticketingBL = new TicketingBL();
    private HotelBL _hotelBL = new HotelBL();
    //private PrintingBL _printingBL = new PrintingBL();

    private POSConfigurationBL _posConfigurationBL = new POSConfigurationBL();
    protected NameValueCollectionExtractor _nvcExtractor;

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
    private string _referenceNo = "referenceNo";
    private string _code = "code";

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
            this.ReferenceNoTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._referenceNo), ApplicationConfig.EncryptionKey);
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
        this.TransTypeHiddenField.Value = "";
        this.TransNumberHiddenField.Value = "";

        this.DetailListRepeater.DataSource = null;
        this.DetailListRepeater.DataBind();
        this.ReasonListRepeater.DataSource = null;
        this.ReasonListRepeater.DataBind();
    }

    protected void ChangeVisiblePanel(byte _prmValue)
    {
        if (_prmValue == 0)
        {
            this.ReasonListPanel.Visible = true;
            this.DetailListPanel.Visible = false;
            this.PasswordPanel.Visible = false;
        }
        else if (_prmValue == 1)
        {
            this.ReasonListPanel.Visible = false;
            this.DetailListPanel.Visible = true;
            this.PasswordPanel.Visible = false;
        }
        else if (_prmValue == 2)
        {
            this.ReasonListPanel.Visible = false;
            this.DetailListPanel.Visible = false;
            this.PasswordPanel.Visible = true;
        }
    }

    public void ShowData()
    {
        try
        {
            this.DeliveryListRepeater.DataSource = this._customerDOBL.GetListDeliveryOrderRef(this.ReferenceNoTextBox.Text);
            this.DeliveryListRepeater.DataBind();
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
        this.ReferenceNoTextBox.Text = "";
        this.ShowData();
        this.ClearData();
        this.ClearLabel();
    }

    protected void BackButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }

    protected void NewOrder_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/DeliveryOrder/CustomerDo.aspx");
    }

    protected void DeliveryListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            V_POSListDeliveryOrder _temp = (V_POSListDeliveryOrder)e.Item.DataItem;

            string _code = _temp.ReferenceNo.ToString();

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
            _no = _page * _maxrow;
            _no += 1;
            _no = _nomor + _no;
            _noLiteral.Text = _no.ToString();
            _nomor += 1;

            Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
            _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

            Literal _transDateLiteral = (Literal)e.Item.FindControl("DatetimeLiteral");
            DateTime _date = Convert.ToDateTime(_temp.TransDate);
            _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

            Literal _userLiteral = (Literal)e.Item.FindControl("UserLiteral");
            _userLiteral.Text = HttpUtility.HtmlEncode(_temp.userprep);

            Literal _divisiLiteral = (Literal)e.Item.FindControl("DivisiLiteral");
            _divisiLiteral.Text = HttpUtility.HtmlEncode(_temp.TransType);

            ImageButton _cancelPaidButton = (ImageButton)e.Item.FindControl("CancelPaidButton");
            _cancelPaidButton.CommandName = "CancelPaid";
            _cancelPaidButton.CommandArgument = _code + "," + _temp.TransType;

            ImageButton _viewJobOrderButton = (ImageButton)e.Item.FindControl("PickButton");
            _viewJobOrderButton.CommandName = "PickButton";
            _viewJobOrderButton.CommandArgument = _code + "," + _temp.TransType + "," + _temp.Transnmbr;
        }
    }

    protected void DeliveryListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "CancelPaid")
            {
                string[] _splitCode = e.CommandArgument.ToString().Split(',');
                this.ReferenceNoHiddenField.Value = _splitCode[0];
                //this.TransTypeHiddenField.Value = _splitCode[1]
                this.TransTypeHiddenField.Value = "Cancel";

                this.ChangeVisiblePanel(2);
                //this.ReasonListRepeater.DataSource = this._reasonBL.GetList();
                //this.ReasonListRepeater.DataBind();
                //this.ClearLabel();
                //this.TransNumberHiddenField.Value = "";

            }
            else if (e.CommandName == "PickButton")
            {
                this.ChangeVisiblePanel(1);

                string[] _splitCode = e.CommandArgument.ToString().Split(',');
                this.ReferenceNoHiddenField.Value = _splitCode[0];
                this.TransTypeHiddenField.Value = _splitCode[1];
                this.TransNumberHiddenField.Value = _splitCode[2];

                //if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Internet))
                //{
                //    this.DetailListRepeater.DataSource = this._internetBL.GetListInternetHdForDeliveryOrder(this.ReferenceNoHiddenField.Value);
                //    this.DetailListRepeater.DataBind();
                //}

                //else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Retail))
                //{
                //    this.DetailListRepeater.DataSource = this._retailBL.GetListRetailHdForDeliveryOrder(this.ReferenceNoHiddenField.Value);
                //    this.DetailListRepeater.DataBind();
                //}

                //else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe))
                //{
                //    this.DetailListRepeater.DataSource = this._cafeBL.GetListRetailHdForDeliveryOrder(this.ReferenceNoHiddenField.Value);
                //    this.DetailListRepeater.DataBind();
                //}
                //else if (this.TransTypeHiddenField.Value == AppModule.GetValue(TransactionType.Ticketing))
                //{
                //    this.DetailListRepeater.DataSource = this._ticketingBL.GetListTicketingForDeliveryOrder(this.ReferenceNoHiddenField.Value);
                //    this.DetailListRepeater.DataBind();
                //}
                //else if (this.TransTypeHiddenField.Value == AppModule.GetValue(TransactionType.Hotel))
                //{
                //    this.DetailListRepeater.DataSource = this._hotelBL.GetListHotelHdForDeliveryOrder(this.ReferenceNoHiddenField.Value);
                //    this.DetailListRepeater.DataBind();
                //}
                //else
                //{
                this.DetailListRepeater.DataSource = this._customerDOBL.GetTrDeliveryOrder(this.ReferenceNoHiddenField.Value.ToString());
                this.DetailListRepeater.DataBind();
                //}
                this.ClearLabel();
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

                //if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Retail))
                //{
                //    _result = this._retailBL.SetVOIDForDeliveryOrder(this.ReferenceNoHiddenField.Value, e.CommandArgument.ToString(), true);
                //}
                //else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Internet))
                //{
                //    _result = this._internetBL.SetVOIDForDeliveryOrder(this.ReferenceNoHiddenField.Value, e.CommandArgument.ToString(), true);
                //}
                //else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe))
                //{
                //    _result = this._cafeBL.SetVOIDForDeliveryOrder(this.ReferenceNoHiddenField.Value, e.CommandArgument.ToString(), true);
                //}
                List<POSTrDeliveryOrderRef> _posTrDeliveryOrderRef = this._customerDOBL.GetListPOSTrDeliveryOrderRef(this.ReferenceNoHiddenField.Value);
                foreach (var _row in _posTrDeliveryOrderRef)
                {
                    if (_row.TransType.ToUpper() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).ToUpper())
                    {
                        _result = this._retailBL.SetVOID(_row.TransNmbr, e.CommandArgument.ToString(), true);
                    }
                    else if (_row.TransType.ToUpper() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).ToUpper())
                    {
                        _result = this._internetBL.SetVOID(_row.TransNmbr, e.CommandArgument.ToString(), true);
                    }
                    else if (_row.TransType.ToUpper() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).ToUpper())
                    {
                        _result = this._cafeBL.SetVOID(_row.TransNmbr, e.CommandArgument.ToString(), true);
                    }
                    else if (_row.TransType.ToUpper() == AppModule.GetValue(TransactionType.Ticketing).ToUpper())
                    {
                        _result = this._ticketingBL.SetVOID(_row.TransNmbr, e.CommandArgument.ToString(), true);
                    }
                    else if (_row.TransType.ToUpper() == AppModule.GetValue(TransactionType.Hotel).ToUpper())
                    {
                        _result = this._hotelBL.SetVOID(_row.TransNmbr, e.CommandArgument.ToString(), true);
                    }
                }
                //POSTrDeliveryOrder _posTrDeliveryOrder = this._customerDOBL.GetSingleTrDeliveryOrder(this.ReferenceNoHiddenField.Value);
                //_posTrDeliveryOrder.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Cancel);
                //_posTrDeliveryOrder.TransDate = DateTime.Now;
                //_result = this._customerDOBL.UpdatePOSTrDeliveryOrder(_posTrDeliveryOrder);
                _result = this._customerDOBL.SetVOID(this.ReferenceNoHiddenField.Value, e.CommandArgument.ToString(), true);
                if (_result == true)
                {
                    this.ClearLabel();
                    this.ClearData();
                    this.ShowData();
                }
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
                //if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Internet))
                //{
                //    POSTrInternetHd _temp = (POSTrInternetHd)e.Item.DataItem;

                //    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //    _no2 = _page2 * _maxrow2;
                //    _no2 += 1;
                //    _no2 = _nomor2 + _no2;
                //    _noLiteral.Text = _no2.ToString();
                //    _nomor2 += 1;

                //    Literal _productNameLiteral = (Literal)e.Item.FindControl("CustNameLiteral");
                //    _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                //    Literal _qtyLiteral = (Literal)e.Item.FindControl("PhoneLiteral");
                //    _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.CustPhone.ToString());
                //}
                //else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Retail))
                //{
                //    POSTrRetailHd _temp = (POSTrRetailHd)e.Item.DataItem;

                //    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //    _no2 = _page2 * _maxrow2;
                //    _no2 += 1;
                //    _no2 = _nomor2 + _no2;
                //    _noLiteral.Text = _no2.ToString();
                //    _nomor2 += 1;

                //    Literal _productNameLiteral = (Literal)e.Item.FindControl("CustNameLiteral");
                //    _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                //    Literal _qtyLiteral = (Literal)e.Item.FindControl("PhoneLiteral");
                //    _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.CustPhone.ToString());
                //}
                //else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe))
                //{
                //    POSTrCafeHd _temp = (POSTrCafeHd)e.Item.DataItem;

                //    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //    _no2 = _page2 * _maxrow2;
                //    _no2 += 1;
                //    _no2 = _nomor2 + _no2;
                //    _noLiteral.Text = _no2.ToString();
                //    _nomor2 += 1;

                //    Literal _productNameLiteral = (Literal)e.Item.FindControl("CustNameLiteral");
                //    _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                //    Literal _qtyLiteral = (Literal)e.Item.FindControl("PhoneLiteral");
                //    _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.CustPhone.ToString());
                //}
                //else if (this.TransTypeHiddenField.Value == AppModule.GetValue(TransactionType.Ticketing))
                //{
                //    POSTrTicketingHd _temp = (POSTrTicketingHd)e.Item.DataItem;

                //    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //    _no2 = _page2 * _maxrow2;
                //    _no2 += 1;
                //    _no2 = _nomor2 + _no2;
                //    _noLiteral.Text = _no2.ToString();
                //    _nomor2 += 1;

                //    Literal _productNameLiteral = (Literal)e.Item.FindControl("CustNameLiteral");
                //    _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                //    Literal _qtyLiteral = (Literal)e.Item.FindControl("PhoneLiteral");
                //    _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.CustPhone.ToString());
                //}
                //else if (this.TransTypeHiddenField.Value == AppModule.GetValue(TransactionType.Hotel))
                //{
                //    POSTrHotelHd _temp = (POSTrHotelHd)e.Item.DataItem;

                //    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //    _no2 = _page2 * _maxrow2;
                //    _no2 += 1;
                //    _no2 = _nomor2 + _no2;
                //    _noLiteral.Text = _no2.ToString();
                //    _nomor2 += 1;

                //    Literal _productNameLiteral = (Literal)e.Item.FindControl("CustNameLiteral");
                //    _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                //    Literal _qtyLiteral = (Literal)e.Item.FindControl("PhoneLiteral");
                //    _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.CustPhone.ToString());
                //}
                //else
                //{
                POSTrDeliveryOrder _temp = (POSTrDeliveryOrder)e.Item.DataItem;

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no2 = _page2 * _maxrow2;
                _no2 += 1;
                _no2 = _nomor2 + _no2;
                _noLiteral.Text = _no2.ToString();
                _nomor2 += 1;

                POSMsCustomerDO _pOSMsCustomerDO = this._customerDOBL.GetSingle(_temp.CustDOCode);

                Literal _custNameLiteral = (Literal)e.Item.FindControl("CustNameLiteral");
                _custNameLiteral.Text = _pOSMsCustomerDO.Name;

                Literal _phoneLiteral = (Literal)e.Item.FindControl("PhoneLiteral");
                _phoneLiteral.Text = _pOSMsCustomerDO.Phone;
                if (_phoneLiteral.Text == "")
                {
                    _phoneLiteral.Text = _pOSMsCustomerDO.HP;
                }
                //}
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void Printing_Click(object sender, ImageClickEventArgs e)
    {
        if (this.ReferenceNoHiddenField.Value != "")
        {
            Boolean _cekDO = this.CekDO(POSTransTypeDataMapper.GetTransType(POSTransType.Printing));
            if (_cekDO)
            {
                this.TransTypeHiddenField.Value = POSTransTypeDataMapper.GetTransType(POSTransType.Printing).Trim().ToLower();
                this.ChangeVisiblePanel(2);
            }
            else
            {
                foreach (RepeaterItem _detailDO in this.DetailListRepeater.Items)
                {
                    Literal _CustName = (Literal)_detailDO.FindControl("CustNameLiteral");
                    Literal _CustPhone = (Literal)_detailDO.FindControl("PhoneLiteral");
                    Response.Redirect("~/Printing/POSPrinting.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)));
                }
            }
        }
        else
        {
            this.WarningLabel.Text = "You must pick Customer Delivery Order";
        }
    }

    protected void Stationary_Click(object sender, ImageClickEventArgs e)
    {
        if (this.ReferenceNoHiddenField.Value != "")
        {
            Boolean _cekDO = this.CekDO(POSTransTypeDataMapper.GetTransType(POSTransType.Retail));
            if (_cekDO)
            {
                this.TransTypeHiddenField.Value = POSTransTypeDataMapper.GetTransType(POSTransType.Retail).Trim().ToLower();
                this.ChangeVisiblePanel(2);
            }
            else
            {
                foreach (RepeaterItem _detailDO in this.DetailListRepeater.Items)
                {
                    Literal _CustName = (Literal)_detailDO.FindControl("CustNameLiteral");
                    Literal _CustPhone = (Literal)_detailDO.FindControl("PhoneLiteral");
                    Response.Redirect("~/Retail/POSRetail.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)));
                }
            }
        }
        else
        {
            this.WarningLabel.Text = "You must pick Customer Delivery Order";
        }
    }

    protected void EVoucher_Click(object sender, ImageClickEventArgs e)
    {
        //if (this.TransNumberHiddenField.Value != "")
        //{
        //    foreach (RepeaterItem _detailDO in this.DetailListRepeater.Items)
        //    {
        //        Literal _CustName = (Literal)_detailDO.FindControl("CustNameLiteral");
        //        Literal _CustPhone = (Literal)_detailDO.FindControl("PhoneLiteral");
        //        Response.Redirect("~/THotel/Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + "");
        //    }
        //}
        //else
        //{
        //    this.WarningLabel.Text = "You must pick Customer Delivery Order";
        //}
    }

    protected void Shipping_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void Photocopy_Click(object sender, ImageClickEventArgs e)
    {
        if (this.ReferenceNoHiddenField.Value != "")
        {
            Boolean _cekDO = this.CekDO(POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy));
            if (_cekDO)
            {
                this.TransTypeHiddenField.Value = POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy).Trim().ToLower();
                this.ChangeVisiblePanel(2);
            }
            else
            {
                foreach (RepeaterItem _detailDO in this.DetailListRepeater.Items)
                {
                    Literal _CustName = (Literal)_detailDO.FindControl("CustNameLiteral");
                    Literal _CustPhone = (Literal)_detailDO.FindControl("PhoneLiteral");
                    Response.Redirect("~/Photocopy/POSPhotocopy.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)));
                }
            }
        }
        else
        {
            this.WarningLabel.Text = "You must pick Customer Delivery Order";
        }
    }

    protected void Cafe_Click(object sender, ImageClickEventArgs e)
    {
        if (this.ReferenceNoHiddenField.Value != "")
        {
            Boolean _cekDO = this.CekDO(POSTransTypeDataMapper.GetTransType(POSTransType.Cafe));
            if (_cekDO)
            {
                this.TransTypeHiddenField.Value = POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).Trim().ToLower();
                this.ChangeVisiblePanel(2);
            }
            else
            {
                foreach (RepeaterItem _detailDO in this.DetailListRepeater.Items)
                {
                    Literal _CustName = (Literal)_detailDO.FindControl("CustNameLiteral");
                    Literal _CustPhone = (Literal)_detailDO.FindControl("PhoneLiteral");
                    Response.Redirect("~/Cafe/POSCafeChooseTable.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)));
                }
            }
        }
        else
        {
            this.WarningLabel.Text = "You must pick Customer Delivery Order";
        }
    }

    protected void Internet_Click(object sender, ImageClickEventArgs e)
    {
        if (this.ReferenceNoHiddenField.Value != "")
        {
            Boolean _cekDO = this.CekDO(POSTransTypeDataMapper.GetTransType(POSTransType.Internet));
            if (_cekDO)
            {
                this.TransTypeHiddenField.Value = POSTransTypeDataMapper.GetTransType(POSTransType.Internet).Trim().ToLower();
                this.ChangeVisiblePanel(2);
            }
            else
            {
                foreach (RepeaterItem _detailDO in this.DetailListRepeater.Items)
                {
                    Literal _CustName = (Literal)_detailDO.FindControl("CustNameLiteral");
                    Literal _CustPhone = (Literal)_detailDO.FindControl("PhoneLiteral");
                    Response.Redirect("~/Internet/POSInternetChooseTable.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)));
                }
            }
        }
        else
        {
            this.WarningLabel.Text = "You must pick Customer Delivery Order";
        }
    }

    protected void Tiketing_Click(object sender, ImageClickEventArgs e)
    {
        if (this.ReferenceNoHiddenField.Value != "")
        {
            Boolean _cekDO = this.CekDO(AppModule.GetValue(TransactionType.Ticketing));
            if (_cekDO)
            {
                this.TransTypeHiddenField.Value = AppModule.GetValue(TransactionType.Ticketing).Trim().ToLower();
                this.ChangeVisiblePanel(2);
            }
            else
            {
                foreach (RepeaterItem _detailDO in this.DetailListRepeater.Items)
                {
                    Literal _CustName = (Literal)_detailDO.FindControl("CustNameLiteral");
                    Literal _CustPhone = (Literal)_detailDO.FindControl("PhoneLiteral");
                    Response.Redirect("~/Ticketing/Ticketing.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + "");
                }
            }
        }
        else
        {
            this.WarningLabel.Text = "You must pick Customer Delivery Order";
        }
    }

    protected void GraphicDesain_Click(object sender, ImageClickEventArgs e)
    {
        if (this.ReferenceNoHiddenField.Value != "")
        {
            Boolean _cekDO = this.CekDO(POSTransTypeDataMapper.GetTransType(POSTransType.Graphic));
            if (_cekDO)
            {
                this.TransTypeHiddenField.Value = POSTransTypeDataMapper.GetTransType(POSTransType.Graphic).Trim().ToLower();
                this.ChangeVisiblePanel(2);
            }
            else
            {
                foreach (RepeaterItem _detailDO in this.DetailListRepeater.Items)
                {
                    Literal _CustName = (Literal)_detailDO.FindControl("CustNameLiteral");
                    Literal _CustPhone = (Literal)_detailDO.FindControl("PhoneLiteral");
                    Response.Redirect("~/Graphic/POSGraphic.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)));
                }
            }
        }
        else
        {
            this.WarningLabel.Text = "You must pick Customer Delivery Order";
        }
    }

    protected void CheckOrder_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/DeliveryOrder/CheckOrder.aspx");
    }

    protected void VoucherHotel_Click(object sender, ImageClickEventArgs e)
    {
        if (this.ReferenceNoHiddenField.Value != "")
        {
            Boolean _cekDO = this.CekDO(AppModule.GetValue(TransactionType.Hotel));
            if (_cekDO)
            {
                this.TransTypeHiddenField.Value = AppModule.GetValue(TransactionType.Hotel).Trim().ToLower();
                this.ChangeVisiblePanel(2);
            }
            else
            {
                foreach (RepeaterItem _detailDO in this.DetailListRepeater.Items)
                {
                    Literal _CustName = (Literal)_detailDO.FindControl("CustNameLiteral");
                    Literal _CustPhone = (Literal)_detailDO.FindControl("PhoneLiteral");
                    Response.Redirect("~/THotel/Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + "");
                }
            }
        }
        else
        {
            this.WarningLabel.Text = "You must pick Customer Delivery Order";
        }
    }

    protected void errorhandler(Exception ex)
    {
        //ErrorHandler.Record(ex, EventLogEntryType.Error);
        String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "DO_LISTDO");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
    }

    protected void OKButton_Click(object sender, EventArgs e)
    {
        try
        {
            String _password = this._posConfigurationBL.GetSingle("POSCancelPassword").SetValue;
            if (_password.ToLower() == this.PasswordTextBox.Text.ToLower())
            {
                if (this.TransTypeHiddenField.Value != "Cancel")
                {
                    foreach (RepeaterItem _detailDO in this.DetailListRepeater.Items)
                    {
                        Literal _CustName = (Literal)_detailDO.FindControl("CustNameLiteral");
                        Literal _CustPhone = (Literal)_detailDO.FindControl("PhoneLiteral");

                        if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Printing).Trim().ToLower())
                        {
                            Response.Redirect("~/Printing/POSPrinting.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)));
                        }
                        else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).Trim().ToLower())
                        {
                            Response.Redirect("~/Retail/POSRetail.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)));
                        }
                        else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy).Trim().ToLower())
                        {
                            Response.Redirect("~/Photocopy/POSPhotocopy.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)));
                        }
                        else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).Trim().ToLower())
                        {
                            Response.Redirect("~/Cafe/POSCafeChooseTable.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)));
                        }
                        else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).Trim().ToLower())
                        {
                            Response.Redirect("~/Internet/POSInternetChooseTable.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)));
                        }
                        else if (this.TransTypeHiddenField.Value == AppModule.GetValue(TransactionType.Ticketing).Trim().ToLower())
                        {
                            Response.Redirect("~/Ticketing/Ticketing.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + "");
                        }
                        else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic).Trim().ToLower())
                        {
                            Response.Redirect("~/Graphic/POSGraphic.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)));
                        }
                        else if (this.TransTypeHiddenField.Value == AppModule.GetValue(TransactionType.Hotel).Trim().ToLower())
                        {
                            Response.Redirect("~/THotel/Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _CustName.Text + "-" + _CustPhone.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + "");
                        }
                    }
                }
                else
                {
                    this.ChangeVisiblePanel(0);
                    this.ReasonListRepeater.DataSource = this._reasonBL.GetList();
                    this.ReasonListRepeater.DataBind();
                    this.ClearLabel();
                    this.TransNumberHiddenField.Value = "";
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

    protected void CancellButton_Click(object sender, EventArgs e)
    {

        this.ChangeVisiblePanel(0);
    }

    protected Boolean CekDO(String _transtype)
    {
        Boolean _cekDO = false;
        List<V_POSListDeliveryOrder> _vPOSListDeliveryOrder = this._customerDOBL.GetListDeliveryOrderRef(this.ReferenceNoTextBox.Text);
        foreach (V_POSListDeliveryOrder _data in _vPOSListDeliveryOrder)
        {
            if (_data.TransType.Trim().ToLower() == _transtype.Trim().ToLower())
            {
                _cekDO = true;
                break;
            }
        }
        return _cekDO;
    }
}
