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
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Text;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Threading;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
public partial class DeliveryOrder_CheckOrder : System.Web.UI.Page
{
    private CustomerDOBL _customerDOBL = new CustomerDOBL();
    //private POSRetailBL _retailBL = new POSRetailBL();
    //private POSInternetBL _internetBL = new POSInternetBL();
    //private POSCafeBL _cafeBL = new POSCafeBL();
    private ProductBL _msProductBL = new ProductBL();
    private CashierBL _cashierBL = new CashierBL();
    private KitchenBL _kitchenBL = new KitchenBL();
    //private CashierPrinterBL _cashierPrinterBL = new CashierPrinterBL();
    private AirLineBL _airLineBL = new AirLineBL();
    private HotelBL _hotelBL = new HotelBL();
    private POSConfigurationBL _posConfigurationBL = new POSConfigurationBL();
    private CompanyBL _companyBL = new CompanyBL();

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

    private int _page3;
    private int _maxrow3 = Convert.ToInt32(ApplicationConfig.ListPageSize);
    private int _maxlength3 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
    private int _no3 = 0;
    private int _nomor3 = 0;

    private string _referenceNo = "referenceNo";
    private string _code = "code";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack == true)
        {
            this.ReferenceNoTextBox.Attributes["onclick"] = "ReferenceKeyBoard(this.id)";
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
            spawnJS += "</script>\n";
            this.javascriptReceiver.Text = spawnJS;

            this.ClearLabel();
            this.ClearData();
            this.ShowStatusDDL();
            this.CheckIsVoid();
            this.ShowData();
            this.ClearDeliveryDetail();
            this.ChangeVisiblePanel(0);
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
        this.CustomerCodeHiddenField.Value = "";
        this.TransNumberHiddenField.Value = "";
    }

    protected void ShowStatusDDL()
    {
        List<String> _dOTypes = POSTrDeliveryOrderDataMapper.DOTypes;
        this.StatusDDL.Items.Clear();
        foreach (var _IDType in _dOTypes)
        {
            String[] _row = _IDType.Split(',');
            this.StatusDDL.Items.Add(new ListItem(_row[1], _row[0]));
        }
    }

    protected void ClearDeliveryDetail()
    {
        this.DetailItemRepeater.DataSource = null;
        this.DetailItemRepeater.DataBind();
        this.DeliveryLogRepeater.DataSource = null;
        this.DeliveryLogRepeater.DataBind();

        this.Address1Label.Text = "";
        this.Address2Label.Text = "";
        this.PhoneLabel.Text = "";
        this.CityLabel.Text = "";
    }

    public void ShowData()
    {
        try
        {
            this.DeliveryListRepeater.DataSource = this._customerDOBL.GetListPOSTrDeliveryOrder(this.ReferenceNoTextBox.Text, Convert.ToByte(this.StatusDDL.SelectedValue));
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
        this.ClearDeliveryDetail();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.ClearData();
        this.ShowData();
        this.ClearLabel();
        this.ClearDeliveryDetail();
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

    protected void ClosingRider_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/DeliveryOrder/CloseRider.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey)));
    }

    protected void DeliveryListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            POSTrDeliveryOrder _temp = (POSTrDeliveryOrder)e.Item.DataItem;

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

            Literal _nameLiteral = (Literal)e.Item.FindControl("NameLiteral");
            _nameLiteral.Text = HttpUtility.HtmlEncode(this._customerDOBL.GetMemberNameByCode(_temp.CustDOCode));

            Literal _statusLiteral = (Literal)e.Item.FindControl("StatusLiteral");
            _statusLiteral.Text = POSTrDeliveryOrderDataMapper.GetStatusText(Convert.ToByte(_temp.Status));

            ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
            _viewButton.CommandName = "ViewButton";
            _viewButton.CommandArgument = _code + "," + _temp.CustDOCode;

            ImageButton _updateButton = (ImageButton)e.Item.FindControl("UpdateButton");
            _updateButton.CommandName = "UpdateButton";
            _updateButton.CommandArgument = _code + "," + _temp.CustDOCode;

            ImageButton _changeButton = (ImageButton)e.Item.FindControl("ChangeButton");
            _changeButton.CommandName = "ChangeButton";
            _changeButton.CommandArgument = _code + "," + _temp.CustDOCode + "," + _statusLiteral.Text;

            if (_statusLiteral.Text == POSTrDeliveryOrderDataMapper.GetStatusText(POSDeliveryOrderStatus.Open))
            {
                //_updateButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/btn_process_DO.png";
                _updateButton.CssClass = "UpdateOpenButton";
                _changeButton.Visible = false;
            }
            else if (_statusLiteral.Text == POSTrDeliveryOrderDataMapper.GetStatusText(POSDeliveryOrderStatus.Process))
            {
                //_updateButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/btn_done_DO.png";
                _updateButton.CssClass = "UpdateProcessButton";
            }
            else
            {
                _updateButton.Visible = false;
            }
        }

    }

    protected void DeliveryListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string[] _splitCode = e.CommandArgument.ToString().Split(',');
            this.ReferenceNoHiddenField.Value = _splitCode[0];
            this.CustomerCodeHiddenField.Value = _splitCode[1];

            if (e.CommandName == "ViewButton")
            {
                this.ChangeVisiblePanel(0);
                POSTrDeliveryOrder _posTrDeliveryOrder = this._customerDOBL.GetSingleTrDeliveryOrder(this.ReferenceNoHiddenField.Value);
                POSMsCustomerDO _posMsCustomerDo = this._customerDOBL.GetSingle(this.CustomerCodeHiddenField.Value);

                this.Address1Label.Text = _posTrDeliveryOrder.DOAddress1;
                this.Address2Label.Text = _posTrDeliveryOrder.DOAddress2;
                this.CityLabel.Text = _posTrDeliveryOrder.DOCity;
                this.PhoneLabel.Text = _posMsCustomerDo.Phone;
                if (this.PhoneLabel.Text == "")
                {
                    this.PhoneLabel.Text = _posMsCustomerDo.HP;
                }

                if (this.ReferenceNoHiddenField.Value != "")
                {
                    this.DeliveryLogRepeater.DataSource = this._customerDOBL.GetListTrDeliveryOrderLog(this.ReferenceNoHiddenField.Value);
                    this.DeliveryLogRepeater.DataBind();
                }

                //POSTrDeliveryOrderRef _posTrDeliveryOrderRef = this._customerDOBL.GetSingleTrDeliveryOrderRef(this.ReferenceNoHiddenField.Value);
                //List<POSTrDeliveryOrder> _posTrDeliveryOrderRef = this._customerDOBL.GetListPOSTrDeliveryOrder(this.ReferenceNoHiddenField.Value);
                List<POSTrAll> _pOSTrAll = this._customerDOBL.GetListTrDeliveryOrderAll(this.ReferenceNoHiddenField.Value);
                this.DetailItemRepeater.DataSource = _pOSTrAll;
                this.DetailItemRepeater.DataBind();

                //this.TransNumberHiddenField.Value = _posTrDeliveryOrderRef.TransNmbr;

                //if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Internet))
                //{
                //    this.DetailItemRepeater.DataSource = this._internetBL.GetListInternetDtByTransNmbr(this.TransNumberHiddenField.Value);
                //    this.DetailItemRepeater.DataBind();
                //}
                //if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Retail))
                //{
                //    this.DetailItemRepeater.DataSource = this._retailBL.GetListRetailDtByTransNmbr(this.TransNumberHiddenField.Value);
                //    this.DetailItemRepeater.DataBind();
                //}
                //if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe))
                //{
                //    this.DetailItemRepeater.DataSource = this._cafeBL.GetListCafeDtByTransNmbr(this.TransNumberHiddenField.Value);
                //    this.DetailItemRepeater.DataBind();
                //}            
            }
            else if (e.CommandName == "UpdateButton")
            {

                //string[] _splitCode = e.CommandArgument.ToString().Split(',');
                bool _result = false;
                this.ReferenceNoHiddenField.Value = _splitCode[0];
                //this.CustomerCodeHiddenField.Value = _splitCode[1];
                POSTrDeliveryOrder _pOSTrDeliveryOrder = this._customerDOBL.GetSingleTrDeliveryOrder(_splitCode[0]);
                if (_pOSTrDeliveryOrder.Status == POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Open))
                {
                    _pOSTrDeliveryOrder.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Process);
                    _pOSTrDeliveryOrder.TransDate = DateTime.Now;

                    POSTrDeliveryOrderLog _pOSTrDeliveryOrderLog = new POSTrDeliveryOrderLog();
                    _pOSTrDeliveryOrderLog.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Process);
                    _pOSTrDeliveryOrderLog.ReferenceNo = _splitCode[0];
                    _pOSTrDeliveryOrderLog.TransDate = DateTime.Now;
                    _pOSTrDeliveryOrderLog.UserName = HttpContext.Current.User.Identity.Name;
                    this.PrintToKitchen(_splitCode[0]);

                    _result = this._customerDOBL.UpdatePOSTrDeliveryOrder(_pOSTrDeliveryOrder);
                    _result = this._customerDOBL.InsertPOSTrDeliveryOrderLog(_pOSTrDeliveryOrderLog);
                    if (_result == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "MsgBox", "alert('Update Success');", true);
                    }
                    this.ShowData();
                    this.ChangeVisiblePanel(0);
                }
                else if (_pOSTrDeliveryOrder.Status == POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Process))
                {
                    //_pOSTrDeliveryOrder.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Done);
                    //_pOSTrDeliveryOrderLog.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Done);
                    //this.PrintToCustomer(_splitCode[0]);
                    this.ChangeButtonVisible(1);
                    this.ChangeVisiblePanel(3);
                }
            }
            else if (e.CommandName == "ChangeButton")
            {
                //string[] _splitCode = e.CommandArgument.ToString().Split(',');

                if (_splitCode[2] == POSTrDeliveryOrderDataMapper.GetStatusText(POSDeliveryOrderStatus.Done) | _splitCode[2] == POSTrDeliveryOrderDataMapper.GetStatusText(POSDeliveryOrderStatus.Delivering) | _splitCode[2] == POSTrDeliveryOrderDataMapper.GetStatusText(POSDeliveryOrderStatus.Deliver))
                {
                    this.ChangeButtonVisible(1);
                }
                else
                {
                    this.ChangeButtonVisible(0);
                }
                this.ChangeVisiblePanel(3);
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void DeliveryLogRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            POSTrDeliveryOrderLog _temp = (POSTrDeliveryOrderLog)e.Item.DataItem;

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
            _no3 = _page3 * _maxrow3;
            _no3 += 1;
            _no3 = _nomor3 + _no3;
            _noLiteral.Text = _no3.ToString();
            _nomor3 += 1;

            Literal _statusLiteral = (Literal)e.Item.FindControl("StatusLiteral");
            _statusLiteral.Text = POSTrDeliveryOrderDataMapper.GetStatusText(Convert.ToByte(_temp.Status));

            Literal _timeLiteral = (Literal)e.Item.FindControl("TimeNameLiteral");
            DateTime _date = Convert.ToDateTime(_temp.TransDate);
            _timeLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

            Literal _userLiteral = (Literal)e.Item.FindControl("UserLiteral");
            _userLiteral.Text = HttpUtility.HtmlEncode(_temp.UserName);
        }
    }

    protected void DetailItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSTrAll _temp = (POSTrAll)e.Item.DataItem;

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no2 = _page2 * _maxrow2;
                _no2 += 1;
                _no2 = _nomor2 + _no2;
                _noLiteral.Text = _no2.ToString();
                _nomor2 += 1;

                Literal _transTypeLiteral = (Literal)e.Item.FindControl("TransTypeLiteral");
                _transTypeLiteral.Text = HttpUtility.HtmlEncode(_temp.TransType);

                Literal _transNmbrLiteral = (Literal)e.Item.FindControl("TransNmbrLiteral");
                _transNmbrLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                if (HttpUtility.HtmlEncode(_temp.TransType).ToUpper() == AppModule.GetValue(TransactionType.Ticketing).ToUpper())
                {
                    MsAirline _msAirline = this._airLineBL.GetSingleAirLine(_temp.ProductCode);
                    _productNameLiteral.Text = _msAirline.AirlineName;
                }
                else if (HttpUtility.HtmlEncode(_temp.TransType).ToUpper() == AppModule.GetValue(TransactionType.Hotel).ToUpper())
                {
                    POSMsHotel _pOSMsHotel = this._hotelBL.GetSinglePOSMsHotel(_temp.ProductCode);
                    _productNameLiteral.Text = _pOSMsHotel.HotelName;
                }
                else
                {
                    _productNameLiteral.Text = HttpUtility.HtmlEncode(_msProductBL.GetProductName(_temp.ProductCode));
                }

                Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                _qtyLiteral.Text = HttpUtility.HtmlEncode(Convert.ToString(_temp.Qty));

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                POSTrDeliveryOrder _posTrDeliveryOrder = this._customerDOBL.GetSingleTrDeliveryOrder(this.ReferenceNoHiddenField.Value);
                if (_posTrDeliveryOrder.Status != POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Close) & _posTrDeliveryOrder.Status != POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Cancel) & _posTrDeliveryOrder.Status != POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Paid))
                {
                    _editButton.CommandName = "EditButton";
                    _editButton.CommandArgument = HttpUtility.HtmlEncode(_temp.TransType) + "," + HttpUtility.HtmlEncode(_temp.TransNmbr);
                }
                else
                {
                    _editButton.Visible = false;
                }
                //if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Internet))
                //{
                //    POSTrInternetDt _temp = (POSTrInternetDt)e.Item.DataItem;

                //    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //    _no2 = _page2 * _maxrow2;
                //    _no2 += 1;
                //    _no2 = _nomor2 + _no2;
                //    _noLiteral.Text = _no2.ToString();
                //    _nomor2 += 1;

                //    Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                //    _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                //    Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                //    _productNameLiteral.Text = HttpUtility.HtmlEncode(_msProductBL.GetProductName(_temp.ProductCode));

                //    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                //    _qtyLiteral.Text = HttpUtility.HtmlEncode(Convert.ToString(_temp.Qty));
                //}
                //else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Retail))
                //{
                //    POSTrRetailDt _temp = (POSTrRetailDt)e.Item.DataItem;

                //    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //    _no2 = _page2 * _maxrow2;
                //    _no2 += 1;
                //    _no2 = _nomor2 + _no2;
                //    _noLiteral.Text = _no2.ToString();
                //    _nomor2 += 1;

                //    Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                //    _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                //    Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                //    _productNameLiteral.Text = HttpUtility.HtmlEncode(_msProductBL.GetProductName(_temp.ProductCode));

                //    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                //    _qtyLiteral.Text = HttpUtility.HtmlEncode(Convert.ToString(_temp.Qty));
                //}
                //else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe))
                //{
                //    POSTrCafeDt _temp = (POSTrCafeDt)e.Item.DataItem;

                //    Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //    _no2 = _page2 * _maxrow2;
                //    _no2 += 1;
                //    _no2 = _nomor2 + _no2;
                //    _noLiteral.Text = _no2.ToString();
                //    _nomor2 += 1;

                //    Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                //    _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                //    Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                //    _productNameLiteral.Text = HttpUtility.HtmlEncode(_msProductBL.GetProductName(_temp.ProductCode));

                //    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                //    _qtyLiteral.Text = HttpUtility.HtmlEncode(Convert.ToString(_temp.Qty));
                //}

            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void DetailItemRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditButton")
            {
                this.ChangeVisiblePanel(1);
                this.TransTypeHiddenField.Value = e.CommandArgument.ToString();
                //string[] _splitCode = e.CommandArgument.ToString().Split(',');
                //POSMsCustomerDO _posMsCustomerDo = this._customerDOBL.GetSingle(this.CustomerCodeHiddenField.Value);
                //if (_splitCode[0] == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe))
                //{
                //    Response.Redirect("~/Cafe/POSCafeChooseTable.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)));
                //}
                //else if (_splitCode[0] == POSTransTypeDataMapper.GetTransType(POSTransType.Internet))
                //{
                //    Response.Redirect("~/Internet/POSInternetChooseTable.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)));
                //}
                //else if (_splitCode[0] == POSTransTypeDataMapper.GetTransType(POSTransType.Retail))
                //{
                //    Response.Redirect("~/Retail/POSRetail.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)));
                //}
                //else if (_splitCode[0] == AppModule.GetValue(TransactionType.Ticketing))
                //{
                //    Response.Redirect("~/Ticketing/Ticketing.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + "");
                //}
                //else if (_splitCode[0] == AppModule.GetValue(TransactionType.Hotel))
                //{
                //    Response.Redirect("~/THotel/Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + "");
                //}
                //else if (_splitCode[0] == POSTransTypeDataMapper.GetTransType(POSTransType.Printing))
                //{
                //    Response.Redirect("~/Printing/POSPrinting.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)));
                //}
                //else if (_splitCode[0] == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy))
                //{
                //    Response.Redirect("~/Photocopy/POSPhotocopy.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)));
                //}
                //else if (_splitCode[0] == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic))
                //{
                //    Response.Redirect("~/Graphic/POSGraphic.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)));
                //}
            }
        }
        catch (ThreadAbortException) { throw; }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void PrintToCustomer(String _prmReferenceNo)
    {
        try
        {
            String _companyAddress = this._companyBL.GetSingleDefault().PrimaryAddress;

            ReportDataSource _reportDataSource1 = new ReportDataSource();
            _reportDataSource1 = this._cashierBL.ReportSendToCustomerDO(_prmReferenceNo, _companyAddress);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            String _reportPath = "General/ReportSendToCustomerDO.rdlc";
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath;

            ReportParameter[] _reportParam = new ReportParameter[2];
            _reportParam[0] = new ReportParameter("ReferenceNo", _prmReferenceNo, false);
            _reportParam[1] = new ReportParameter("CompanyAddress", _companyAddress, false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();

            LocalReport report = new LocalReport();
            report = this.ReportViewer1.LocalReport;

            try
            {
                string deviceInfo =
                              "<DeviceInfo>" +
                              "  <OutputFormat>EMF</OutputFormat>" +
                              "  <PageWidth>4in</PageWidth>" +
                              "  <PageHeight>7.5in</PageHeight>" +
                              "  <MarginTop>0.0in</MarginTop>" +
                              "  <MarginLeft>0.0in</MarginLeft>" +
                              "  <MarginRight>0.0in</MarginRight>" +
                              "  <MarginBottom>0.0in</MarginBottom>" +
                              "</DeviceInfo>";

                Warning[] warnings;
                m_streams = new List<Stream>();

                report.Render("Image", deviceInfo, CreateStream, out warnings);
                foreach (Stream stream in m_streams)
                    stream.Position = 0;

                m_currentPageIndex = 0;

                //String printerName = this.GetDefaultPrinter();
                //POSMsKitchen _pOSMsKitchen = this._kitchenBL.GetSingle("0001");
                //String printerName = "\\\\" + _pOSMsKitchen.KitchenPrinterIPAddress + "\\" + _pOSMsKitchen.KitchenPrinterName;

                String hostIP = Request.ServerVariables["REMOTE_ADDR"]; 
                String printerName = "";
                //V_POSMsCashierPrinter _vPOSMsCashierPrinter = this._cashierPrinterBL.GetDefaultPrinter();
                POSMsCashierPrinter _vPOSMsCashierPrinter = this._cashierBL.GetDefaultPrinter(hostIP);
                if (_vPOSMsCashierPrinter != null)
                {
                    printerName = "\\\\" + _vPOSMsCashierPrinter.IPAddress + "\\" + _vPOSMsCashierPrinter.PrinterName;
                }

                if (m_streams == null || m_streams.Count == 0) return;

                PrintDocument printDoc = new PrintDocument();
                printDoc.PrinterSettings.PrinterName = printerName;

                PaperSize _paperSize = new PaperSize();
                _paperSize.Width = 400; //850
                _paperSize.Height = 750;

                printDoc.DefaultPageSettings.PaperSize = _paperSize;

                if (!printDoc.PrinterSettings.IsValid)
                {
                    this.WarningLabel.Text = String.Format("Can't find printer \"{0}\".", printerName);
                    return;
                }

                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                printDoc.Print();

                if (m_streams != null)
                {
                    foreach (Stream stream in m_streams)
                        stream.Close();
                    m_streams = null;
                }
            }
            catch (Exception ex)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
                this.WarningLabel.Text = "Sorry, Printer Not Include. ";
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void PrintToKitchen(String _prmReferenceNo)
    {
        try
        {
            bool _hasil = this._cashierBL.CekFgSendToKitchenDO(_prmReferenceNo);
            if (_hasil)
            {
                List<POSMsKitchen> _posMsKitchen = this._cashierBL.GetPrinterKitchenDO(_prmReferenceNo);

                foreach (var _item in _posMsKitchen)
                {
                    ReportDataSource _reportDataSource1 = new ReportDataSource();
                    _reportDataSource1 = this._cashierBL.ReportSendToKitchenDO(_prmReferenceNo, _item.KitchenCode);

                    this.ReportViewer2.LocalReport.DataSources.Clear();
                    this.ReportViewer2.LocalReport.DataSources.Add(_reportDataSource1);

                    String _reportPath = "General/ReportSendToKitchenDO.rdlc";
                    this.ReportViewer2.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath;

                    ReportParameter[] _reportParam = new ReportParameter[2];
                    _reportParam[0] = new ReportParameter("ReferenceNo", _prmReferenceNo, false);
                    _reportParam[1] = new ReportParameter("KitchenCode", _item.KitchenCode, false);

                    this.ReportViewer2.LocalReport.SetParameters(_reportParam);
                    this.ReportViewer2.LocalReport.Refresh();

                    LocalReport report = new LocalReport();
                    report = this.ReportViewer2.LocalReport;

                    try
                    {
                        string deviceInfo =
                              "<DeviceInfo>" +
                              "  <OutputFormat>EMF</OutputFormat>" +
                              "  <PageWidth>4in</PageWidth>" +
                              "  <PageHeight>7.5in</PageHeight>" +
                              "  <MarginTop>0.0in</MarginTop>" +
                              "  <MarginLeft>0.0in</MarginLeft>" +
                              "  <MarginRight>0.0in</MarginRight>" +
                              "  <MarginBottom>0.0in</MarginBottom>" +
                              "</DeviceInfo>";

                        Warning[] warnings;
                        m_streams = new List<Stream>();

                        report.Render("Image", deviceInfo, CreateStream, out warnings);
                        foreach (Stream stream in m_streams)
                            stream.Position = 0;

                        m_currentPageIndex = 0;

                        //POSMsKitchen _pOSMsKitchen = this._kitchenBL.GetSingle("0001");
                        //String printerName = "\\\\" + _pOSMsKitchen.KitchenPrinterIPAddress + "\\" + _pOSMsKitchen.KitchenPrinterName;
                        String printerName = "\\\\" + _item.KitchenPrinterIPAddress + "\\" + _item.KitchenPrinterName;

                        if (m_streams == null || m_streams.Count == 0) return;

                        PrintDocument printDoc = new PrintDocument();
                        printDoc.PrinterSettings.PrinterName = printerName;

                        PaperSize _paperSize = new PaperSize();
                        _paperSize.Width = 400; //850
                        _paperSize.Height = 750;

                        printDoc.DefaultPageSettings.PaperSize = _paperSize;

                        if (!printDoc.PrinterSettings.IsValid)
                        {
                            this.WarningLabel.Text = String.Format("Can't find printer \"{0}\".", printerName);
                            return;
                        }

                        printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                        printDoc.Print();

                        if (m_streams != null)
                        {
                            foreach (Stream stream in m_streams)
                                stream.Close();
                            m_streams = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        foreach (Stream stream in m_streams)
                            stream.Close();
                        m_streams = null;

                        this.WarningLabel.Text = "Sorry, Printer Not Include. ";//+ ex.Message.ToString() + "\n" + ex.StackTrace.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    private int m_currentPageIndex;

    private IList<Stream> m_streams;

    private void PrintPage(object sender, PrintPageEventArgs ev)
    {
        Metafile pageImage = new
        Metafile(m_streams[m_currentPageIndex]);
        ev.Graphics.DrawImage(pageImage, ev.PageBounds);
        m_currentPageIndex++;
        ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
    }
    public String GetDefaultPrinter()
    {
        PrinterSettings settings = new PrinterSettings();
        String printer;
        for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
        {
            printer = PrinterSettings.InstalledPrinters[i];
            settings.PrinterName = printer;
            if (settings.IsDefaultPrinter)
                return printer;
        }
        return string.Empty;
    }
    private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
    {
        Stream stream = null;
        try
        {
            stream = new MemoryStream();
            m_streams.Add(stream);
        }
        catch (Exception Ex)
        {
        }
        return stream;
    }

    protected void errorhandler(Exception ex)
    {
        //ErrorHandler.Record(ex, EventLogEntryType.Error);
        String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "DO_CHECKORDER");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
    }

    protected void ChangeVisiblePanel(byte _prmValue)
    {
        this.DeliveryLogPanel.Visible = false;
        this.DetailCustomerInfoPanel.Visible = false;
        this.PasswordPanel.Visible = false;
        this.ChangePanel.Visible = false;

        if (_prmValue == 0)
        {
            this.DeliveryLogPanel.Visible = true;
            this.DetailCustomerInfoPanel.Visible = true;
        }
        else if (_prmValue == 1)
        {
            this.PasswordPanel.Visible = true;
            this.DetailCustomerInfoPanel.Visible = true;
        }
        else if (_prmValue == 3)
        {
            this.DeliveryLogPanel.Visible = true;
            this.ChangePanel.Visible = true;
        }
    }

    protected void OKButton_Click(object sender, EventArgs e)
    {
        try
        {
            String _password = this._posConfigurationBL.GetSingle("POSCancelPassword").SetValue;
            if (_password.ToLower() == this.PasswordTextBox.Text.ToLower())
            {
                string[] _splitCode = this.TransTypeHiddenField.Value.ToString().Split(',');
                POSMsCustomerDO _posMsCustomerDo = this._customerDOBL.GetSingle(this.CustomerCodeHiddenField.Value);
                if (_splitCode[1].ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).ToLower())
                {
                    Response.Redirect("~/Cafe/POSCafeChooseTable.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)));
                }
                else if (_splitCode[1].ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).ToLower())
                {
                    Response.Redirect("~/Internet/POSInternetChooseTable.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)));
                }
                else if (_splitCode[1].ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).ToLower())
                {
                    Response.Redirect("~/Retail/POSRetail.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)));
                }
                else if (_splitCode[1].ToLower() == AppModule.GetValue(TransactionType.Ticketing).ToLower())
                {
                    Response.Redirect("~/Ticketing/Ticketing.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + "");
                }
                else if (_splitCode[1].ToLower() == AppModule.GetValue(TransactionType.Hotel).ToLower())
                {
                    Response.Redirect("~/THotel/Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + "");
                }
                else if (_splitCode[1].ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Printing).ToLower())
                {
                    Response.Redirect("~/Printing/POSPrinting.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)));
                }
                else if (_splitCode[1].ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy).ToLower())
                {
                    Response.Redirect("~/Photocopy/POSPhotocopy.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)));
                }
                else if (_splitCode[1].ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic).ToLower())
                {
                    Response.Redirect("~/Graphic/POSGraphic.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value + "-" + _posMsCustomerDo.Name + "-" + this.PhoneLabel.Text, ApplicationConfig.EncryptionKey)));
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

    protected void CheckIsVoid()
    {
        bool _result = false;
        List<POSTrDeliveryOrder> _listPOSTrDeliveryOrder = this._customerDOBL.GetListPOSTrDeliveryOrder("", 1);
        foreach (var _row in _listPOSTrDeliveryOrder)
        {
            _result = this._customerDOBL.CheckIsVoidDO(_row.ReferenceNo);
            if (_result == false)
                _result = this._customerDOBL.SetVOID(_row.ReferenceNo, "6", true);
        }

        _listPOSTrDeliveryOrder = this._customerDOBL.GetListPOSTrDeliveryOrder("", 2);
        foreach (var _row in _listPOSTrDeliveryOrder)
        {
            _result = this._customerDOBL.CheckIsVoidDO(_row.ReferenceNo);
            if (_result == false)
                _result = this._customerDOBL.SetVOID(_row.ReferenceNo, "6", true);
        }

        _listPOSTrDeliveryOrder = this._customerDOBL.GetListPOSTrDeliveryOrder("", 3);
        foreach (var _row in _listPOSTrDeliveryOrder)
        {
            _result = this._customerDOBL.CheckIsVoidDO(_row.ReferenceNo);
            if (_result == false)
                _result = this._customerDOBL.SetVOID(_row.ReferenceNo, "6", true);
        }

        _listPOSTrDeliveryOrder = this._customerDOBL.GetListPOSTrDeliveryOrder("", 4);
        foreach (var _row in _listPOSTrDeliveryOrder)
        {
            _result = this._customerDOBL.CheckIsVoidDO(_row.ReferenceNo);
            if (_result == false)
                _result = this._customerDOBL.SetVOID(_row.ReferenceNo, "6", true);
        }
    }

    protected void CashImageButton_Click(object sender, ImageClickEventArgs e)
    {
        this.GetDiscountPromo(POSSettlementButtonType.Cash);
        this.UpdateLog();
    }

    protected void VoucherImageButton_Click(object sender, ImageClickEventArgs e)
    {
        this.GetDiscountPromo(POSSettlementButtonType.Voucher);
        this.UpdateLog();
    }

    protected void DebitImageButton_Click(object sender, ImageClickEventArgs e)
    {
        this.GetDiscountPromo(POSSettlementButtonType.Debit);
        this.UpdateLog();
    }

    protected void CreditImageButton_Click(object sender, ImageClickEventArgs e)
    {
        this.GetDiscountPromo(POSSettlementButtonType.CreditCard);
        this.UpdateLog();
    }

    protected void GetDiscountPromo(POSSettlementButtonType TypePayment)
    {
        try
        {
            int _typePayment = 0;
            Boolean _promo = false;
            Boolean _discount = false;

            if (TypePayment == POSSettlementButtonType.CreditCard)
            {
                _typePayment = 1;
            }
            else if (TypePayment == POSSettlementButtonType.Debit)
            {
                _typePayment = 2;
            }
            else if (TypePayment == POSSettlementButtonType.Voucher)
            {
                _typePayment = 3;
            }
            else if (TypePayment == POSSettlementButtonType.Cash)
            {
                _typePayment = 4;
            }

            List<POSTrDeliveryOrderRef> _pOSTrDeliveryOrderRef = this._customerDOBL.GetListPOSTrDeliveryOrderRef(this.ReferenceNoHiddenField.Value);
            DateTime _now = DateTime.Now;
            foreach (var _item in _pOSTrDeliveryOrderRef)
            {
                V_POSReferenceNotYetPayListAll _data = this._cashierBL.GetSingleReferenceNotPayAll(_item.TransNmbr, _item.TransType);
                Decimal _totalDO = this._cashierBL.GetTotalDO(this.ReferenceNoHiddenField.Value);
                List<POSTrPromoItem> _posTrPromoItem = this._cashierBL.GetPromo(_item.TransNmbr, _now, _typePayment, "", _totalDO, _item.TransType, _data.MemberID);
                foreach (var _itempromo in _posTrPromoItem)
                {
                    _promo = true;
                }
                if (_promo == false)
                {
                    List<POSTrAllDiscon> _pOSTrAllDiscon = this._cashierBL.GetDiscount(_item.TransNmbr, _now, _typePayment, "", _totalDO, _item.TransType, _data.MemberID);
                    foreach (var _items in _pOSTrAllDiscon)
                    {
                        _discount = true;
                    }
                }
                List<POSTrPromoItem> _posTrPromoItemByFgPayment = this._cashierBL.GetPromoByFgPayment(_item.TransNmbr, _now, _typePayment, "", _totalDO, _item.TransType, _data.MemberID);
                foreach (var _itempromo in _posTrPromoItemByFgPayment)
                {
                    _promo = true;
                }
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void UpdateLog()
    {
        POSTrDeliveryOrder _pOSTrDeliveryOrder = this._customerDOBL.GetSingleTrDeliveryOrder(this.ReferenceNoHiddenField.Value);
        if (_pOSTrDeliveryOrder.Status == POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Process))
        {
            _pOSTrDeliveryOrder.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Done);

            POSTrDeliveryOrderLog _pOSTrDeliveryOrderLog = new POSTrDeliveryOrderLog();
            _pOSTrDeliveryOrderLog.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Done);
            _pOSTrDeliveryOrderLog.ReferenceNo = this.ReferenceNoHiddenField.Value;
            _pOSTrDeliveryOrderLog.TransDate = DateTime.Now;
            _pOSTrDeliveryOrderLog.UserName = HttpContext.Current.User.Identity.Name;
            this.PrintToCustomer(this.ReferenceNoHiddenField.Value);

            bool _result = this._customerDOBL.UpdatePOSTrDeliveryOrder(_pOSTrDeliveryOrder);
            _result = this._customerDOBL.InsertPOSTrDeliveryOrderLog(_pOSTrDeliveryOrderLog);
            if (_result == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MsgBox", "alert('Update Success');", true);
            }
        }
        this.ShowData();
        this.ChangeVisiblePanel(0);
    }

    protected void ChangeButtonVisible(Int16 _prmValue)
    {
        if (_prmValue == 0)
        {
            this.TitleChangeLiteral.Visible = false;
            this.CashImageButton.Visible = false;
            this.DebitImageButton.Visible = false;
            this.CreditImageButton.Visible = false;
            this.VoucherImageButton.Visible = false;
        }
        else if (_prmValue == 1)
        {
            this.TitleChangeLiteral.Visible = true;
            this.CashImageButton.Visible = true;
            this.DebitImageButton.Visible = true;
            this.CreditImageButton.Visible = true;
            this.VoucherImageButton.Visible = true;
        }
    }

    protected void PrintCustomerImageButton_Click(object sender, ImageClickEventArgs e)
    {
        this.PrintToCustomer(this.ReferenceNoHiddenField.Value);
    }

    protected void PrintKitchenImageButton_Click(object sender, ImageClickEventArgs e)
    {
        this.PrintToKitchen(this.ReferenceNoHiddenField.Value);
    }
}
