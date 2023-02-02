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
//using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

public partial class DeliveryOrder_AssignRider : System.Web.UI.Page
{
    private CustomerDOBL _customerDOBL = new CustomerDOBL();
    private POSRetailBL _retailBL = new POSRetailBL();
    private POSInternetBL _internetBL = new POSInternetBL();
    private POSCafeBL _cafeBL = new POSCafeBL();
    private ProductBL _msProductBL = new ProductBL();
    private EmployeeBL _msEmployeeBL = new EmployeeBL();

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

    private List<POSTrDeliveryOrder> _list1 = new List<POSTrDeliveryOrder>();
    private List<POSTrDeliveryOrder> _list2 = new List<POSTrDeliveryOrder>();

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
            this.ShowData();
            this.ClearDeliveryDetail();
            this.ShowDriver();
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
        //this.TransNumberHiddenField.Value = "";
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
        try
        {
            //string[] _tempSplit = _prmArgument.Split('-');
            //DateTime _date = DateTime.Now;
            //DateTime _date2 = _date.Date;
            //int _hour = _date.Hour;
            //int _minute = _date.Minute;

            this._list1 = this._customerDOBL.GetListPOSTrDeliveryOrder(this.ReferenceNoTextBox.Text, POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Done));
            this.DeliveryListRepeater.DataSource = this._list1;
            this.DeliveryListRepeater.DataBind();

            this.AssignHiddenField();
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    public void ShowDriver()
    {
        try
        {
            List<MsEmployee> _tempMsEmployee = this._msEmployeeBL.GetListEmpDriver();
            List<MsEmployee> _msEmployee = new List<MsEmployee>();

            foreach (var _temp in _tempMsEmployee)
            {
                bool _cekdriver = this._customerDOBL.DriverAvailable(_temp.EmpNumb);
                if (_cekdriver == true)
                {
                    _msEmployee.Add(_temp);
                }
            }
            this.DriverRepeater.DataSource = _msEmployee;
            this.DriverRepeater.DataBind();
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.ShowData();
        //this.ClearDeliveryDetail();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.ClearData();
        this.ClearLabel();
        this.ClearDeliveryDetail();
        this.ShowData();
    }

    protected void BackButton_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("~/DeliveryOrder/ListDeliveryOrder.aspx");
        Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey)));
    }

    //protected void AssignRider_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect("~/DeliveryOrder/CustomerDo.aspx");
    //}

    protected void ClosingRider_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("~/DeliveryOrder/CloseRider.aspx");
        Response.Redirect("~/DeliveryOrder/CloseRider.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt("", ApplicationConfig.EncryptionKey)));
    }

    protected void DeliveryListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
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

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.CommandName = "ViewButton";
                _viewButton.CommandArgument = _code + "," + _temp.CustDOCode;

                ImageButton _pickButton = (ImageButton)e.Item.FindControl("PickButton");
                _pickButton.CommandArgument = _code + "," + _temp.Status + "," + _temp.TransDate + "," + _temp.CustDOCode + "," + e.Item.ItemIndex;
                _pickButton.CommandName = "PickButton";
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

                if (this.PhoneLabel.Text == "")
                {
                    this.PhoneLabel.Text = _posMsCustomerDo.HP;
                }
                //POSTrDeliveryOrderRef _posTrDeliveryOrderRef = this._customerDOBL.GetSingleTrDeliveryOrderRef(this.ReferenceNoHiddenField.Value);

                ////this.TransNumberHiddenField.Value = _posTrDeliveryOrderRef.TransNmbr;
                ////this.TransTypeHiddenField.Value = _posTrDeliveryOrderRef.TransType;

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

                ////if (this.ReferenceNoHiddenField.Value != "")
                ////{
                ////    this.DeliveryLogRepeater.DataSource = this._customerDOBL.GetListTrDeliveryOrderLog(this.ReferenceNoHiddenField.Value);
                ////    this.DeliveryLogRepeater.DataBind();
                ////}
            }
            else if (e.CommandName == "PickButton")
            {
                String[] _break = e.CommandArgument.ToString().Split(',');
                String _referenceNo = _break[0];
                Byte _status = Convert.ToByte(_break[1]);
                DateTime _transDate = Convert.ToDateTime(_break[2]);
                String _custDOCode = _break[3];
                String _index = _break[4];

                this.FillListFromHiddenField();

                POSMsCustomerDO _posMsCustomerDo = this._customerDOBL.GetSingle(_custDOCode);

                this._list2.Add(this._list1.Find(_temp => _temp.ReferenceNo == _referenceNo));
                this._list1.RemoveAt(Convert.ToInt32(_index));
                this.AssignHiddenField();
                this.DisplayData();
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void DetailItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            POSTrDeliveryOrder _temp = (POSTrDeliveryOrder)e.Item.DataItem;

            string _referenceNo = _temp.ReferenceNo.ToString();

            Literal _referenceNoLiteral = (Literal)e.Item.FindControl("Reference2Literal");
            _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

            Literal _transDateLiteral = (Literal)e.Item.FindControl("Datetime2Literal");
            DateTime _date = Convert.ToDateTime(_temp.TransDate);
            _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

            Literal _nameLiteral = (Literal)e.Item.FindControl("Name2Literal");
            _nameLiteral.Text = HttpUtility.HtmlEncode(this._customerDOBL.GetMemberNameByCode(_temp.CustDOCode));

            ImageButton _pickButton = (ImageButton)e.Item.FindControl("PickButton");
            _pickButton.CommandArgument = _referenceNo + "," + _temp.Status + "," + _temp.TransDate + "," + _temp.CustDOCode + "," + e.Item.ItemIndex;
            _pickButton.CommandName = "PickButton";
        }
    }

    protected void DetailItemRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "PickButton")
            {
                String[] _break = e.CommandArgument.ToString().Split(',');
                String _referenceNo = _break[0];
                Byte _status = Convert.ToByte(_break[1]);
                DateTime _transDate = Convert.ToDateTime(_break[2]);
                String _custDOCode = _break[3];
                String _index = _break[4];

                this.FillListFromHiddenField();

                POSMsCustomerDO _posMsCustomerDo = this._customerDOBL.GetSingle(_custDOCode);

                this._list1.Add(this._list2.Find(_temp => _temp.ReferenceNo == _referenceNo));
                this._list2.RemoveAt(Convert.ToInt32(_index));
                this.AssignHiddenField();
                this.DisplayData();
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    protected void DriverRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            MsEmployee _temp = (MsEmployee)e.Item.DataItem;
            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
            _no3 = _page3 * _maxrow3;
            _no3 += 1;
            _no3 = _nomor3 + _no3;
            _noLiteral.Text = _no3.ToString();
            _nomor3 += 1;

            Literal _statusLiteral = (Literal)e.Item.FindControl("DriverIDLiteral");
            _statusLiteral.Text = HttpUtility.HtmlEncode(Convert.ToString(_temp.EmpNumb));

            Literal _nameLiteral = (Literal)e.Item.FindControl("NameLiteral");
            _nameLiteral.Text = HttpUtility.HtmlEncode(Convert.ToString(_temp.EmpName));

            Literal _telephoneLiteral = (Literal)e.Item.FindControl("TelephoneLiteral");
            _telephoneLiteral.Text = HttpUtility.HtmlEncode(_temp.Email);

            ImageButton _pickButton = (ImageButton)e.Item.FindControl("AssignButton");
            _pickButton.CommandArgument = _temp.EmpNumb;
            _pickButton.CommandName = "AssignButton";
        }
    }

    protected void DriverRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "AssignButton")
            {
                if (this.List2HiddenField.Value != "")
                {
                    bool _result = false;
                    string[] _splitCode = e.CommandArgument.ToString().Split(',');
                    String _empNum = _splitCode[0];

                    String[] _dataRow2 = this.List2HiddenField.Value.Split('^');
                    foreach (var _item2 in _dataRow2)
                    {
                        String[] _field2 = _item2.Split(',');

                        POSTrDeliveryOrder _pOSTrDeliveryOrder = this._customerDOBL.GetSingleTrDeliveryOrder(_field2[0]);
                        _pOSTrDeliveryOrder.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Delivering);
                        _pOSTrDeliveryOrder.TransDate = DateTime.Now;
                        _result = this._customerDOBL.UpdatePOSTrDeliveryOrder(_pOSTrDeliveryOrder);

                        POSTrDeliveryOrderLog _pOSTrDeliveryOrderLog = new POSTrDeliveryOrderLog();
                        _pOSTrDeliveryOrderLog.ReferenceNo = _field2[0];
                        _pOSTrDeliveryOrderLog.Status = POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Delivering);
                        _pOSTrDeliveryOrderLog.TransDate = DateTime.Now;
                        _pOSTrDeliveryOrderLog.UserName = _empNum; //driver code
                        _result = this._customerDOBL.InsertPOSTrDeliveryOrderLog(_pOSTrDeliveryOrderLog);
                    }
                    if (_result == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "MsgBox", "alert('Assign Success');", true);
                        this.ShowData();
                        this.ShowDriver();
                        this.DisplayData();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "MsgBox", "alert('Assign Failed');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "MsgBox", "alert('Please Pick Data From Ready For Deliver!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            this.errorhandler(ex);
        }
    }

    private void AssignHiddenField()
    {
        String _strAccumData1 = "";
        foreach (var _item in this._list1)
        {
            if (_strAccumData1 == "")
            {
                _strAccumData1 = _item.ReferenceNo + "," + _item.Status + "," + _item.TransDate + "," + _item.CustDOCode;
            }
            else
            {
                _strAccumData1 += "^" + _item.ReferenceNo + "," + _item.Status + "," + _item.TransDate + "," + _item.CustDOCode;
            }
        }
        this.List1HiddenField.Value = _strAccumData1;

        String _strAccumData2 = "";
        foreach (var _item in this._list2)
        {
            if (_strAccumData2 == "")
            {
                _strAccumData2 = _item.ReferenceNo + "," + _item.Status + "," + _item.TransDate + "," + _item.CustDOCode;
            }
            else
            {
                _strAccumData2 += "^" + _item.ReferenceNo + "," + _item.Status + "," + _item.TransDate + "," + _item.CustDOCode;
            }
        }
        this.List2HiddenField.Value = _strAccumData2;
    }

    private void DisplayData()
    {
        this._list1.Sort((x, y) => (x.ReferenceNo.CompareTo(y.ReferenceNo)));

        this.DeliveryListRepeater.DataSource = this._list1;
        this.DeliveryListRepeater.DataBind();

        //this._list2.Sort((x, y) => (x.TableIDPerRoom.CompareTo(y.TableIDPerRoom)));

        this.DetailItemRepeater.DataSource = this._list2;
        this.DetailItemRepeater.DataBind();
    }

    private void FillListFromHiddenField()
    {
        if (this.List1HiddenField.Value != "")
        {
            String[] _dataRow = this.List1HiddenField.Value.Split('^');
            foreach (var _item in _dataRow)
            {
                POSTrDeliveryOrder _pos = new POSTrDeliveryOrder();
                String[] _field = _item.Split(',');
                _pos.ReferenceNo = _field[0];
                _pos.Status = Convert.ToByte(_field[1]);
                _pos.TransDate = Convert.ToDateTime(_field[2]);
                _pos.CustDOCode = _field[3];
                this._list1.Add(_pos);
            }
        }

        if (this.List2HiddenField.Value != "")
        {
            String[] _dataRow2 = this.List2HiddenField.Value.Split('^');
            foreach (var _item2 in _dataRow2)
            {
                POSTrDeliveryOrder _pos = new POSTrDeliveryOrder();
                String[] _field2 = _item2.Split(',');
                _pos.ReferenceNo = _field2[0];
                _pos.Status = Convert.ToByte(_field2[1]);
                _pos.TransDate = Convert.ToDateTime(_field2[2]);
                _pos.CustDOCode = _field2[3];
                this._list2.Add(_pos);
            }
        }
    }

    protected void errorhandler(Exception ex)
    {
        //ErrorHandler.Record(ex, EventLogEntryType.Error);
        String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "DO_ASSIGNRIDER");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
    }
}
