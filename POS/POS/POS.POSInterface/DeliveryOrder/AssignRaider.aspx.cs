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
public partial class DeliveryOrder_AssignRaider : System.Web.UI.Page
{
    private CustomerDOBL _customerDOBL = new CustomerDOBL();
    private POSRetailBL _retailBL = new POSRetailBL();
    private POSInternetBL _internetBL = new POSInternetBL();
    private POSCafeBL _cafeBL = new POSCafeBL();
    private ProductBL _msProductBL = new ProductBL();

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack == true)
        {
            this.ClearLabel();
            this.ClearData();
            this.ShowData();
            this.ClearDeliveryDetail();
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

    protected void ClearDeliveryDetail()
    {
        this.DetailItemRepeater.DataSource = null;
        this.DetailItemRepeater.DataBind();
        //this.DeliveryLogRepeater.DataSource = null;
        //this.DeliveryLogRepeater.DataBind();

        this.Address1Label.Text = "";
        this.Address2Label.Text = "";
        this.PhoneLabel.Text = "";
        this.CityLabel.Text = "";
    }

    public void ShowData()
    {
        //this.DeliveryListRepeater.DataSource = this._customerDOBL.GetListPOSTrDeliveryOrder(this.ReferenceNoTextBox.Text, Convert.ToByte(this.StatusDDL.SelectedValue));
        //this.DeliveryListRepeater.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.ShowData();
        this.ClearDeliveryDetail();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.ShowData();
        this.ClearData();
        this.ClearLabel();
        this.ClearDeliveryDetail();
    }

    protected void BackButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/DeliveryOrder/ListDeliveryOrde.aspx");
    }

    protected void AssignRider_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/DeliveryOrder/CustomerDo.aspx");
    }

    protected void ClosingRider_Click(object sender, ImageClickEventArgs e)
    {

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

            ImageButton _cancelPaidButton = (ImageButton)e.Item.FindControl("ViewButton");
            _cancelPaidButton.CommandName = "ViewButton";
            _cancelPaidButton.CommandArgument = _code + "," + _temp.CustDOCode;
        }

    }
    protected void DeliveryListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ViewButton")
        {
            string[] _splitCode = e.CommandArgument.ToString().Split(',');
            this.ReferenceNoHiddenField.Value = _splitCode[0];
            this.CustomerCodeHiddenField.Value = _splitCode[1];

            POSTrDeliveryOrder _posTrDeliveryOrder = this._customerDOBL.GetSingleTrDeliveryOrder(this.ReferenceNoHiddenField.Value);
            POSMsCustomerDO _posMsCustomerDo = this._customerDOBL.GetSingle(this.CustomerCodeHiddenField.Value);

            this.Address1Label.Text = _posTrDeliveryOrder.DOAddress1;
            this.Address2Label.Text = _posTrDeliveryOrder.DOAddress2;
            this.CityLabel.Text = _posTrDeliveryOrder.DOCity;
            this.PhoneLabel.Text = _posMsCustomerDo.Phone;

            POSTrDeliveryOrderRef _posTrDeliveryOrderRef = this._customerDOBL.GetSingleTrDeliveryOrderRef(this.ReferenceNoHiddenField.Value);

            //this.TransNumberHiddenField.Value = _posTrDeliveryOrderRef.TransNmbr;
            //this.TransTypeHiddenField.Value = _posTrDeliveryOrderRef.TransType;

            if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Internet))
            {
                this.DetailItemRepeater.DataSource = this._internetBL.GetListInternetDtByTransNmbr(this.TransNumberHiddenField.Value);
                this.DetailItemRepeater.DataBind();
            }
            if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Retail))
            {
                this.DetailItemRepeater.DataSource = this._retailBL.GetListRetailDtByTransNmbr(this.TransNumberHiddenField.Value);
                this.DetailItemRepeater.DataBind();
            }
            if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe))
            {
                this.DetailItemRepeater.DataSource = this._cafeBL.GetListCafeDtByTransNmbr(this.TransNumberHiddenField.Value);
                this.DetailItemRepeater.DataBind();
            }

            //if (this.ReferenceNoHiddenField.Value != "")
            //{
            //    this.DeliveryLogRepeater.DataSource = this._customerDOBL.GetListTrDeliveryOrderLog(this.ReferenceNoHiddenField.Value);
            //    this.DeliveryLogRepeater.DataBind();
            //}
        }
        else if (e.CommandName == "UpdateButton")
        {

            string[] _splitCode = e.CommandArgument.ToString().Split(',');
            this.ReferenceNoHiddenField.Value = _splitCode[0];
            this.CustomerCodeHiddenField.Value = _splitCode[1];
        }
    }

    protected void DriverRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

            Literal _statusLiteral = (Literal)e.Item.FindControl("DriverIDLiteral");
            _statusLiteral.Text = HttpUtility.HtmlEncode(Convert.ToString(_temp.Status));

            Literal _timeLiteral = (Literal)e.Item.FindControl("NameLiteral");
            DateTime _date = Convert.ToDateTime(_temp.TransDate);
            _timeLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

            Literal _userLiteral = (Literal)e.Item.FindControl("TelephoneLiteral");
            _userLiteral.Text = HttpUtility.HtmlEncode(_temp.UserName);
        }
    }

    protected void DetailItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Internet))
            {
                POSTrInternetDt _temp = (POSTrInternetDt)e.Item.DataItem;

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no2 = _page2 * _maxrow2;
                //_no2 += 1;
                //_no2 = _nomor2 + _no2;
                //_noLiteral.Text = _no2.ToString();
                //_nomor2 += 1;

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("Reference2Literal");
                
                _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                Literal _productNameLiteral = (Literal)e.Item.FindControl("Datetime2Literal");
                _productNameLiteral.Text = HttpUtility.HtmlEncode(_msProductBL.GetProductName(_temp.ProductCode));

                Literal _qtyLiteral = (Literal)e.Item.FindControl("Name2Literal");
                _qtyLiteral.Text = HttpUtility.HtmlEncode(Convert.ToString(_temp.Qty));
            }
            else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Retail))
            {
                POSTrRetailDt _temp = (POSTrRetailDt)e.Item.DataItem;

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no2 = _page2 * _maxrow2;
                //_no2 += 1;
                //_no2 = _nomor2 + _no2;
                //_noLiteral.Text = _no2.ToString();
                //_nomor2 += 1;

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("Reference2Literal");
                _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                Literal _productNameLiteral = (Literal)e.Item.FindControl("Datetime2Literal");
                _productNameLiteral.Text = HttpUtility.HtmlEncode(_msProductBL.GetProductName(_temp.ProductCode));

                Literal _qtyLiteral = (Literal)e.Item.FindControl("Name2Literal");
                _qtyLiteral.Text = HttpUtility.HtmlEncode(Convert.ToString(_temp.Qty));
            }
            else if (this.TransTypeHiddenField.Value == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe))
            {
                POSTrCafeDt _temp = (POSTrCafeDt)e.Item.DataItem;

                //Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                //_no2 = _page2 * _maxrow2;
                //_no2 += 1;
                //_no2 = _nomor2 + _no2;
                //_noLiteral.Text = _no2.ToString();
                //_nomor2 += 1;

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("Reference2Literal");
                _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                Literal _productNameLiteral = (Literal)e.Item.FindControl("Datetime2Literal");
                _productNameLiteral.Text = HttpUtility.HtmlEncode(_msProductBL.GetProductName(_temp.ProductCode));

                Literal _qtyLiteral = (Literal)e.Item.FindControl("Name2Literal");
                _qtyLiteral.Text = HttpUtility.HtmlEncode(Convert.ToString(_temp.Qty));
            }

        }
    }
}
