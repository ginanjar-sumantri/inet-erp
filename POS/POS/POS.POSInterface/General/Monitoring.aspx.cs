using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Threading;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace POS.POSInterface.General
{
    public partial class Monitoring : GeneralBase
    {
        private List<V_POSReferenceNotYetPayList> _list1 = new List<V_POSReferenceNotYetPayList>();
        private List<V_POSReferenceNotYetPayList> _list2 = new List<V_POSReferenceNotYetPayList>();
        private POSRetailBL _retailBL = new POSRetailBL();
        private POSInternetBL _internetBL = new POSInternetBL();
        private POSCafeBL _cafeBL = new POSCafeBL();
        private HotelBL _hotelBL = new HotelBL();
        private TicketingBL _ticketingBL = new TicketingBL();
        private POSPrintingBL _printingBL = new POSPrintingBL();
        private POSPhotocopyBL _photocopyBL = new POSPhotocopyBL();
        private POSGraphicBL _graphicBL = new POSGraphicBL();
        private POSShippingBL _shippingBL = new POSShippingBL();
        private CashierBL _cashierBL = new CashierBL();
        private MemberBL _memberBL = new MemberBL();
        private AirLineBL _airLineBL = new AirLineBL();
        private POSReasonBL _reasonBL = new POSReasonBL();
        private POSConfigurationBL _pOSConfigurationBL = new POSConfigurationBL();

        private CountryBL _countryBL = new CountryBL();
        private VendorBL _vendorBL = new VendorBL();
        private CityBL _cityBL = new CityBL();

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private string _path = "General/PosSendToCashier.rdlc";
        private string _path2 = "General/PosCheckStatus.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                //this.RegistrationButton.OnClientClick = "window.open('../Registration/Registration.aspx','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";

                //this.SearchFieldTextBox.Attributes["onclick"] = "SearchFieldKeyBoard(this.id)";
                //this.PasswordTextBox.Attributes["onclick"] = "PasswordKeyBoard(this.id)";
                //String spawnJS = "<script type='text/javascript' language='JavaScript'>\n";
                ////DECLARE FUNCTION FOR Calling KeyBoard SearchField
                //spawnJS += "function SearchFieldKeyBoard(x) {\n";
                //spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findSearchField&titleinput=Search Key&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                //spawnJS += "}\n";

                ////DECLARE FUNCTION FOR CATCHING ON SearchField
                //spawnJS += "function findSearchField(x) {\n";
                //spawnJS += "dataArray = x.split ('|') ;\n";
                //spawnJS += "document.getElementById('" + this.SearchFieldTextBox.ClientID + "').value = dataArray [0];\n";
                //spawnJS += "document.forms[0].submit();\n";
                //spawnJS += "}\n";

                ////DECLARE FUNCTION FOR Calling KeyBoard Password
                //spawnJS += "function PasswordKeyBoard(x) {\n";
                //spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findPassword&titleinput=Password&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                //spawnJS += "}\n";

                ////DECLARE FUNCTION FOR CATCHING ON Password
                //spawnJS += "function findPassword(x) {\n";
                //spawnJS += "dataArray = x.split ('|') ;\n";
                //spawnJS += "document.getElementById('" + this.PasswordTextBox.ClientID + "').value = dataArray [0];\n";
                //spawnJS += "document.forms[0].submit();\n";
                //spawnJS += "}\n";

                //spawnJS += "</script>\n";
                //this.javascriptReceiver.Text = spawnJS; 

                this.SearchTitle2Literal.Text = "Start Date";
                this.SearchTitle3Literal.Text = "End Date";
                this.SearchField2TextBox.Attributes.Add("ReadOnly", "True");
                this.SearchField3TextBox.Attributes.Add("ReadOnly", "True");

                this.List3HiddenField.Value = "JobOrder";
                this.List4HiddenField.Value = "";
                this.DisplayPanel("Cashier");
                this.ChangeVisiblePanel(0);
            }
            if (this.SearchFieldDDL.SelectedValue != "TransDate")
            {
                this.ChangeSearch(0);
            }
            else
            {
                this.ChangeSearch(1);
            }
        }

        private void ClearData()
        {
            this.SearchFieldTextBox.Text = "";
            this.SearchField2TextBox.Text = "";
            this.SearchField3TextBox.Text = "";
        }

        protected void DisplayPanel(String _typePanel)
        {
            DateTime _now = DateTime.Now;
            String _dateNow = _now.Year + "-" + _now.Month + "-" + _now.Day;
            this.StartDateCashierTextBox.Text = _dateNow;
            this.StartDateCheckStatusTextBox.Text = _dateNow;
            this.EndDateCashierTextBox.Text = _dateNow;
            this.EndDateCheckStatusTextBox.Text = _dateNow;

            this.CashierPanel.Visible = false;
            this.DetailPanel.Visible = false;
            this.MemberPanel.Visible = false;
            this.CheckStatusPanel.Visible = false;
            this.SearchList(_typePanel);
            this.TypeHiddenField.Value = _typePanel;
            this.CashierPrintPreviewPanel.Visible = false;
            this.CheckStatusPrintPreviewPanel.Visible = false;
            if (_typePanel == "Cashier")
            {
                this.ShowDataCashier();
                this.CashierPanel.Visible = true;
                this.DetailPanel.Visible = true;
                this.CashierPrintPreviewButton.Visible = true;
                //this.MemberPanel.Visible = false;
                //this.CheckStatusPanel.Visible = false;
                //this.searchPanel.Visible = false;
                //this.ClearData();
            }
            if (_typePanel == "Product")
            {
                this.ShowDataCheckStatus();
                this.CheckStatusPanel.Visible = true;
                this.DetailPanel.Visible = true;
                this.CheckStatusPrintPreviewButton.Visible = true;
                //this.ClearData();
                //this.MemberPanel.Visible = false;
                //this.CashierPanel.Visible = false;
                //this.searchPanel.Visible = true;
                //this.RefNOPanel.Visible = true;
                //this.SearchFieldDDLPanel.Visible = true;
                //this.SearchFieldMemberDDLPanel.Visible = false;
            }
            if (_typePanel == "Member")
            {
                this.ShowMember();
                this.MemberPanel.Visible = true;
                //this.ClearData();
                //this.DetailPanel.Visible = false;
                //this.CashierPanel.Visible = false;
                //this.CheckStatusPanel.Visible = false;
                //this.searchPanel.Visible = true;
                //this.RefNOPanel.Visible = false;
                //this.SearchFieldDDLPanel.Visible = false;
                //this.SearchFieldMemberDDLPanel.Visible = true;
            }
            this.ClearData();
        }

        protected void ChangeVisiblePanel(Byte _prmValue)
        {
            if (_prmValue == 0) //normal
            {
                this.FormPanel.Visible = true;
                this.ReasonListPanel.Visible = false;
                this.PasswordPanel.Visible = false;
            }
            else if (_prmValue == 1) //password required
            {
                this.FormPanel.Visible = true;
                this.ReasonListPanel.Visible = false;
                this.PasswordPanel.Visible = true;
            }
            else if (_prmValue == 2) //reason
            {
                this.FormPanel.Visible = false;
                this.ReasonListPanel.Visible = true;
                this.PasswordPanel.Visible = false;
            }
        }

        protected void ChangeSearch(Byte _prmValue)
        {
            if (_prmValue == 0)
            {
                this.SearchFieldPanel.Visible = true;
                this.SearchFieldPanel2.Visible = false;
            }
            else if (_prmValue == 1)
            {
                this.SearchFieldPanel.Visible = false;
                this.SearchFieldPanel2.Visible = true;
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
                    if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).Trim().ToLower())
                        _result = this._retailBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).Trim().ToLower())
                        _result = this._internetBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).Trim().ToLower())
                        _result = this._cafeBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == AppModule.GetValue(TransactionType.Ticketing).Trim().ToLower().Trim().ToLower())
                        _result = this._ticketingBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == AppModule.GetValue(TransactionType.Hotel).Trim().ToLower().Trim().ToLower())
                        _result = this._hotelBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Printing).Trim().ToLower())
                        _result = this._printingBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy).Trim().ToLower())
                        _result = this._photocopyBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic).Trim().ToLower())
                        _result = this._graphicBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Shipping).Trim().ToLower())
                        _result = this._shippingBL.SetVOID(this.TransNmbrHiddenField.Value, e.CommandArgument.ToString(), true);

                    if (_result == true)
                    {
                        this.DisplayPanel("Cashier");
                        this.ChangeVisiblePanel(0);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Process Cancel Success.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Process Cancel Failed');", true);
                        this.ChangeVisiblePanel(0);
                    }
                }
            }
            catch (ThreadAbortException) { throw; }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void Back2ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ChangeVisiblePanel(0);
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "MONITORING");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }

        protected void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                String _password = this._pOSConfigurationBL.GetSingle("POSCancelPassword").SetValue;
                if (_password.ToLower() == this.PasswordTextBox.Text.ToLower())
                {
                    this.ChangeVisiblePanel(2);
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

        protected void SearchList(String _typePanel)
        {
            this.SearchFieldDDL.Items.Clear();
            if (_typePanel == "Cashier")
            {
                this.SearchFieldDDL.Items.Insert(0, new ListItem("Trans Number", "TransNmbr"));
                this.SearchFieldDDL.Items.Insert(1, new ListItem("Date", "TransDate"));
                this.SearchFieldDDL.Items.Insert(2, new ListItem("Divisi", "TransType"));
                this.SearchFieldDDL.Items.Insert(3, new ListItem("Reference", "ReferenceNo"));
                this.SearchFieldDDL.Items.Insert(4, new ListItem("MemberID", "MemberID"));
                this.SearchFieldDDL.Items.Insert(5, new ListItem("Customer Name", "CustName"));
            }
            if (_typePanel == "Product")
            {
                this.SearchFieldDDL.Items.Insert(0, new ListItem("Reference", "ReferenceNo"));
                this.SearchFieldDDL.Items.Insert(1, new ListItem("Settlement", "SettlementNo"));
                this.SearchFieldDDL.Items.Insert(2, new ListItem("Trans Number", "TransNmbr"));
                this.SearchFieldDDL.Items.Insert(3, new ListItem("Divisi", "TransType"));
                this.SearchFieldDDL.Items.Insert(4, new ListItem("Payment", "DoneSettlement"));
                this.SearchFieldDDL.Items.Insert(5, new ListItem("Customer Name", "CustName"));
                this.SearchFieldDDL.Items.Insert(6, new ListItem("Date", "TransDate"));
                this.SearchFieldDDL.Items.Insert(7, new ListItem("DP Paid", "DPPaid"));
            }
            if (_typePanel == "Member")
            {
                this.SearchFieldDDL.Items.Insert(0, new ListItem("Name", "MemberName"));
                this.SearchFieldDDL.Items.Insert(1, new ListItem("Address", "Address"));
                this.SearchFieldDDL.Items.Insert(2, new ListItem("Telephone", "Telephone"));
                this.SearchFieldDDL.Items.Insert(3, new ListItem("HandPhone", "HandPhone"));
                this.SearchFieldDDL.Items.Insert(4, new ListItem("Barcode", "Barcode"));
            }
        }


        //VIEW OUT STANDING PAYMENT

        private void FillListFromHiddenField()
        {
            if (this.List1HiddenField.Value != "")
            {
                String[] _dataRow = this.List1HiddenField.Value.Split('^');
                foreach (var _item in _dataRow)
                {
                    String[] _field = _item.Split(',');

                    this._list1.Add(new V_POSReferenceNotYetPayList(_field[0], _field[1], _field[2]));
                }
            }

            if (this.List2HiddenField.Value != "")
            {
                String[] _dataRow2 = this.List2HiddenField.Value.Split('^');
                foreach (var _item2 in _dataRow2)
                {
                    String[] _field2 = _item2.Split(',');

                    this._list2.Add(new V_POSReferenceNotYetPayList(_field2[0], _field2[1], _field2[2]));
                }
            }
        }

        private void AssignHiddenField()
        {
            String _strAccumData1 = "";
            foreach (var _item in this._list1)
            {
                if (_strAccumData1 == "")
                {
                    _strAccumData1 = _item.TransNmbr + "," + _item.TransType + "," + _item.ReferenceNo;
                }
                else
                {
                    _strAccumData1 += "^" + _item.TransNmbr + "," + _item.TransType + "," + _item.ReferenceNo;
                }
            }
            this.List1HiddenField.Value = _strAccumData1;

            String _strAccumData2 = "";
            foreach (var _item in this._list2)
            {
                if (_strAccumData2 == "")
                {
                    _strAccumData2 = _item.TransNmbr + "," + _item.TransType + "," + _item.ReferenceNo;
                }
                else
                {
                    _strAccumData2 += "^" + _item.TransNmbr + "," + _item.TransType + "," + _item.ReferenceNo;
                }
            }
            this.List2HiddenField.Value = _strAccumData2;
        }

        protected void CashierButton_Click(object sender, EventArgs e)
        {
            this.DisplayPanel("Cashier");
        }

        protected void BackImageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(ApplicationConfig.POSInterfaceWebAppURL + this._loginPage);
        }

        private void ShowDataCashier()
        {
            try
            {
                this._list1 = this._cashierBL.GetListReferenceNotPay(this.List1HiddenField.Value);

                this.AssignHiddenField();

                this.DisplayData();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void DisplayData()
        {
            String _prmSearchValue = "";
            DateTime _startDate = DateTime.Now.AddMonths(-1);
            DateTime _endDate = DateTime.Now;
            if (this.SearchFieldDDL.SelectedValue == "TransDate")
            {
                if (this.SearchField2TextBox.Text == "")
                    this.SearchField2TextBox.Text = _startDate.Year.ToString() + "-" + _startDate.Month.ToString() + "-" + _startDate.Day.ToString();
                if (this.SearchField3TextBox.Text == "")
                    this.SearchField3TextBox.Text = _endDate.Year.ToString() + "-" + _endDate.Month.ToString() + "-" + _endDate.Day.ToString();
                _prmSearchValue = this.SearchField2TextBox.Text + "|" + this.SearchField3TextBox.Text;
            }
            else
            {
                _prmSearchValue = this.SearchFieldTextBox.Text;
            }
            this.ListRepeater.DataSource = this._cashierBL.GetPOSReferenceNotYetPayList(this.SearchFieldDDL.SelectedValue, _prmSearchValue);
            this.ListRepeater.DataBind();
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
                    POSShippingBL _posShippingBL = new POSShippingBL();
                    this.ListRepeaterDetail.DataSource = _posShippingBL.GetListShippingDtByTransNmbr(_transNmbr);
                    this.ListRepeaterDetail.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                V_POSReferenceNotYetPayList _temp = (V_POSReferenceNotYetPayList)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral2");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;


                Literal _transNmbrLiteral = (Literal)e.Item.FindControl("TransNmbrLiteral");
                _transNmbrLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _divisiLiteral = (Literal)e.Item.FindControl("DivisiLiteral");
                _divisiLiteral.Text = HttpUtility.HtmlEncode(_temp.TransType);

                Literal _refLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _refLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                Literal _memberIDLiteral = (Literal)e.Item.FindControl("MemberIDLiteral");
                _memberIDLiteral.Text = HttpUtility.HtmlEncode(_temp.MemberID);

                Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                _memberNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                Literal _telephoneLiteral = (Literal)e.Item.FindControl("DateLiteral");
                _telephoneLiteral.Text = HttpUtility.HtmlEncode(_temp.date);

                ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewDetailImageButton");
                //_viewDetailButton.ImageUrl = ApplicationConfig.POSInterfaceWebAppURL + "images/view_detail.jpg";
                _viewDetailButton.CommandArgument = _code + "," + _temp.TransType;
                _viewDetailButton.CommandName = "ViewDetail";

                ImageButton _cancelImageButton = (ImageButton)e.Item.FindControl("CancelImageButton");
                _cancelImageButton.CommandArgument = _code + "," + _temp.TransType;
                _cancelImageButton.CommandName = "CancelDetail";

            }
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                this.ShowDataDetail(e.CommandArgument.ToString());
            }
            if (e.CommandName == "CancelDetail")
            {
                this.ChangeVisiblePanel(1);
                String[] _tempSplit = (e.CommandArgument.ToString()).Split(',');
                String _transNmbr = _tempSplit[0];
                String _transType = _tempSplit[1];

                this.TransNmbrHiddenField.Value = _transNmbr;
                this.DetailTypeHiddenField.Value = _transType;
            }
        }

        protected void ListRepeaterDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).Trim().ToLower())
                    {
                        POSTrRetailDt _temp = (POSTrRetailDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                        //HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("DetailRepeaterItemTemplate");
                        //_tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                        //_tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).Trim().ToLower())
                    {
                        POSTrInternetDt _temp = (POSTrInternetDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                        //HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("DetailRepeaterItemTemplate");
                        //_tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                        //_tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).Trim().ToLower())
                    {
                        POSTrCafeDt _temp = (POSTrCafeDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                        //HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("DetailRepeaterItemTemplate");
                        //_tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                        //_tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == AppModule.GetValue(TransactionType.Ticketing).Trim().ToLower())
                    {
                        POSTrTicketingDt _temp = (POSTrTicketingDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                        //HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("DetailRepeaterItemTemplate");
                        //_tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                        //_tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == AppModule.GetValue(TransactionType.Hotel).Trim().ToLower())
                    {
                        POSTrHotelDt _temp = (POSTrHotelDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                        //HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("DetailRepeaterItemTemplate");
                        //_tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                        //_tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Printing).Trim().ToLower())
                    {
                        POSTrPrintingDt _temp = (POSTrPrintingDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                        //HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("DetailRepeaterItemTemplate");
                        //_tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                        //_tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy).Trim().ToLower())
                    {
                        POSTrPhotocopyDt _temp = (POSTrPhotocopyDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                        //HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("DetailRepeaterItemTemplate");
                        //_tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                        //_tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic).Trim().ToLower())
                    {
                        POSTrGraphicDt _temp = (POSTrGraphicDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                        //HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("DetailRepeaterItemTemplate");
                        //_tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                        //_tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Shipping).Trim().ToLower())
                    {
                        POSTrShippingDt _temp = (POSTrShippingDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoLiteral.Text = this.TransNmbrHiddenField.Value;

                        String _citycode = this._shippingBL.GetSinglePOSTrShippingHd(_temp.TransNmbr).DeliverCityCode;

                        POSMsShipping _posMsShipping = this._shippingBL.GetPOSMsShipping(_temp.VendorCode, _temp.ShippingTypeCode, _temp.ProductShape, _citycode);

                        Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                        _productCodeLiteral.Text = _posMsShipping.VendorName.Trim();

                        String _productShape = "";
                        if (_posMsShipping.ProductShape == "0")
                            _productShape = "Document. ";
                        else
                            _productShape = "Non Document. ";

                        Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                        //_descLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);
                        _descLiteral.Text = _posMsShipping.ShippingTypeName.Trim() + ". " + _productShape + _posMsShipping.CityName.Trim();

                        int _qty = (_temp.Weight == null) ? 0 : Convert.ToInt32(_temp.Weight);
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
                    }
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void CashierPrintPreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.FormPanel.Visible = false;
            this.CashierPrintPreviewPanel.Visible = true;
            //this.ReasonListPanel.Visible = false;

            this.CashierReportViewer.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._cashierBL.CashierPrintPreview(Convert.ToDateTime(this.StartDateCashierTextBox.Text), (Convert.ToDateTime(this.EndDateCashierTextBox.Text)));

            this.CashierReportViewer.LocalReport.DataSources.Clear();
            this.CashierReportViewer.LocalReport.DataSources.Add(_reportDataSource1);

            this.CashierReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _path;
            this.CashierReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[3];
            _reportParam[0] = new ReportParameter("BeginDate", this.StartDateCashierTextBox.Text, true);
            _reportParam[1] = new ReportParameter("EndDate", this.EndDateCashierTextBox.Text, true);
            _reportParam[2] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

            this.CashierReportViewer.LocalReport.SetParameters(_reportParam);

            this.CashierReportViewer.LocalReport.Refresh();
        }


        //VIEW OUT STANDING PRODUCT     

        public void ShowDataCheckStatus()
        {
            try
            {
                //this.CheckStatusListRepeater.DataSource = this._retailBL.GetMonitoringNotDelivered(this.RefNoTextBox.Text, this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text);
                DateTime _startDate = DateTime.Now.AddMonths(-1);
                DateTime _endDate = DateTime.Now;
                String _prmSearchValue = "";
                if (this.SearchFieldDDL.SelectedValue == "DoneSettlement")
                {
                    if (this.SearchFieldTextBox.Text.Trim().ToLower().Substring(0, 1) == "d")
                        _prmSearchValue = "Y";
                    else
                        _prmSearchValue = "N";
                }
                else if (this.SearchFieldDDL.SelectedValue == "TransDate")
                {
                    if (this.SearchField2TextBox.Text == "")
                        this.SearchField2TextBox.Text = _startDate.Year.ToString() + "-" + _startDate.Month.ToString() + "-" + _startDate.Day.ToString();
                    if (this.SearchField3TextBox.Text == "")
                        this.SearchField3TextBox.Text = _endDate.Year.ToString() + "-" + _endDate.Month.ToString() + "-" + _endDate.Day.ToString();
                    _prmSearchValue = this.SearchField2TextBox.Text + "|" + this.SearchField3TextBox.Text;
                }
                else
                {
                    _prmSearchValue = this.SearchFieldTextBox.Text;
                }
                this.CheckStatusListRepeater.DataSource = this._retailBL.GetMonitoringNotDelivered(this.SearchFieldDDL.SelectedValue, _prmSearchValue);
                this.CheckStatusListRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void ShowCheckStatusDetail(String _prmArgument)
        {
            try
            {
                String[] _break = _prmArgument.Split(',');
                String _transNmbr = _break[0];
                String _transType = _break[1];

                this.TransNmbrHiddenField.Value = _break[2];
                this.DetailTypeHiddenField.Value = _transType;

                if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).Trim().ToLower())
                {
                    POSRetailBL _posRetailBL = new POSRetailBL();
                    this.CheckStatusListRepeaterDt.DataSource = _posRetailBL.GetListRetailDtByTransNmbr(_transNmbr);
                    this.CheckStatusListRepeaterDt.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).Trim().ToLower())
                {
                    POSInternetBL _posInternetBL = new POSInternetBL();
                    this.CheckStatusListRepeaterDt.DataSource = _posInternetBL.GetListInternetDtByTransNmbr(_transNmbr);
                    this.CheckStatusListRepeaterDt.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).Trim().ToLower())
                {
                    POSCafeBL _posCafeBL = new POSCafeBL();
                    this.CheckStatusListRepeaterDt.DataSource = _posCafeBL.GetListCafeDtByTransNmbr(_transNmbr);
                    this.CheckStatusListRepeaterDt.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Ticketing).Trim().ToLower())
                {
                    TicketingBL _ticketingBL = new TicketingBL();
                    this.CheckStatusListRepeaterDt.DataSource = _ticketingBL.GetListPOSTrTicketingDt(_transNmbr);
                    this.CheckStatusListRepeaterDt.DataBind();
                }
                else if (_transType.Trim().ToLower() == AppModule.GetValue(TransactionType.Ticketing).Trim().ToLower().Trim().ToLower())
                {
                    TicketingBL _ticketingBL = new TicketingBL();
                    this.CheckStatusListRepeaterDt.DataSource = _ticketingBL.GetListPOSTrTicketingDt(_transNmbr);
                    this.CheckStatusListRepeaterDt.DataBind();
                }
                else if (_transType.Trim().ToLower() == AppModule.GetValue(TransactionType.Hotel).Trim().ToLower().Trim().ToLower())
                {
                    HotelBL _hotelBL = new HotelBL();
                    this.CheckStatusListRepeaterDt.DataSource = _hotelBL.GetListPOSTrHotelDt(_transNmbr);
                    this.CheckStatusListRepeaterDt.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Printing).Trim().ToLower())
                {
                    POSPrintingBL _posPrintingBL = new POSPrintingBL();
                    this.CheckStatusListRepeaterDt.DataSource = _posPrintingBL.GetListPrintingDtByTransNmbr(_transNmbr);
                    this.CheckStatusListRepeaterDt.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy).Trim().ToLower())
                {
                    POSPhotocopyBL _posPhotocopyBL = new POSPhotocopyBL();
                    this.CheckStatusListRepeaterDt.DataSource = _posPhotocopyBL.GetListPhotocopyDtByTransNmbr(_transNmbr);
                    this.CheckStatusListRepeaterDt.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic).Trim().ToLower())
                {
                    POSGraphicBL _posGraphicBL = new POSGraphicBL();
                    this.CheckStatusListRepeaterDt.DataSource = _posGraphicBL.GetListGraphicDtByTransNmbr(_transNmbr);
                    this.CheckStatusListRepeaterDt.DataBind();
                }
                else if (_transType.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Shipping).Trim().ToLower())
                {
                    POSShippingBL _posShippingBL = new POSShippingBL();
                    this.CheckStatusListRepeaterDt.DataSource = _posShippingBL.GetListShippingDtByTransNmbr(_transNmbr);
                    this.CheckStatusListRepeaterDt.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void CheckStatusButton_Click(object sender, EventArgs e)
        {
            this.DisplayPanel("Product");
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            //if (this.SearchFieldDDLPanel.Visible == true)
            //    this.DisplayPanel("Product");
            //if (this.SearchFieldDDLPanel.Visible == false)
            //    this.DisplayPanel("Member");
            //this.DisplayPanel(this.TypeHiddenField.Value);
            if (this.TypeHiddenField.Value == "Cashier")
            {
                this.ShowDataCashier();
            }
            if (this.TypeHiddenField.Value == "Product")
            {
                this.ShowDataCheckStatus();
            }
            if (this.TypeHiddenField.Value == "Member")
            {
                this.ShowMember();
            }
        }

        protected void CheckStatusListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                V_POSCheckStatusMonitoring _temp = (V_POSCheckStatusMonitoring)e.Item.DataItem;

                string _code = _temp.SettlementNo.ToString();

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;


                Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                Literal _jobCodeLiteral = (Literal)e.Item.FindControl("JobOrderLiteral");
                _jobCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.SettlementNo);

                Literal _TransNmbrLiteral = (Literal)e.Item.FindControl("TransNmbrLiteral");
                _TransNmbrLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                //String _divisi = this._retailBL.GetDivisiByTransType(_temp.TransNmbr);
                Literal _divisiLiteral = (Literal)e.Item.FindControl("DivisiLiteral");
                _divisiLiteral.Text = HttpUtility.HtmlEncode(_temp.TransType);

                Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                _memberNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                Literal _paymentStatusLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                _paymentStatusLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(Convert.ToChar(_temp.DoneSettlement)));

                Literal _transDateLiteral = (Literal)e.Item.FindControl("DateTimeLiteral");
                DateTime _date = Convert.ToDateTime(_temp.TransDate);
                _transDateLiteral.Text = DateFormMapper.GetValue(_temp.TransDate) + "&nbsp;&nbsp;" + _date.Hour.ToString().PadLeft(2, '0') + ":" + _date.Minute.ToString().PadLeft(2, '0');

                Decimal _dpPaid = (_temp.DPPaid == null) ? 0 : Convert.ToDecimal(_temp.DPPaid);
                Literal _dpPaidLiteral = (Literal)e.Item.FindControl("DPPaidLiteral");
                _dpPaidLiteral.Text = HttpUtility.HtmlEncode(_dpPaid.ToString("#,##0.00"));

                ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewImageButton");
                _viewDetailButton.CommandName = "ViewDetail";
                _viewDetailButton.CommandArgument = _temp.TransNmbr + "," + _temp.TransType + "," + _code;


                //String _result = this._retailBL.GetTransTypeByTransNmbr(_temp.TransNmbr);
                //if (_result.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).Trim().ToLower())
                //{
                //    String _resultInternet = this._internetBL.GetRefNmbrInterByTransType(_temp.TransNmbr, _result);
                //    Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                //    _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_resultInternet);
                //}
                //else if (_result.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).Trim().ToLower())
                //{
                //    String _resultRetail = this._retailBL.GetRefNmbrRetailByTransType(_temp.TransNmbr, _result);
                //    Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                //    _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_resultRetail);
                //}
                //else if (_result.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).Trim().ToLower())
                //{
                //    String _resultRetail = this._cafeBL.GetRefNmbrInterByTransType(_temp.TransNmbr, _result);
                //    Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                //    _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_resultRetail);
                //}
                //else if (_result.Trim().ToLower() == AppModule.GetValue(TransactionType.Ticketing).Trim().ToLower())
                //{
                //    String _resultRetail = this._ticketingBL.GetRefNmbrInterByTransType(_temp.TransNmbr, _result);
                //    Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                //    _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_resultRetail);
                //}
                //else if (_result.Trim().ToLower() == AppModule.GetValue(TransactionType.Hotel).Trim().ToLower())
                //{
                //    String _resultRetail = this._hotelBL.GetRefNmbrInterByTransType(_temp.TransNmbr, _result);
                //    Literal _referenceNoLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                //    _referenceNoLiteral.Text = HttpUtility.HtmlEncode(_resultRetail);
                //}


                //String _transNumberDt = this._retailBL.GetRefNumberTrSettlementRefTransac(_code, _divisi);


                //if (_result.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).Trim().ToLower())
                //{
                //    String _memberName = this._retailBL.GetMemberNameByTransType(_temp.TransNmbr, _result);
                //    Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                //    _memberNameLiteral.Text = HttpUtility.HtmlEncode(_memberName);
                //}
                //else if (_result.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).Trim().ToLower())
                //{
                //    String _memberName = this._internetBL.GetMemberNameByTransType(_temp.TransNmbr, _result);
                //    Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                //    _memberNameLiteral.Text = HttpUtility.HtmlEncode(_memberName);
                //}
                //if (_result.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).Trim().ToLower())
                //{
                //    String _memberName = this._cafeBL.GetMemberNameByTransType(_temp.TransNmbr, _result);
                //    Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                //    _memberNameLiteral.Text = HttpUtility.HtmlEncode(_memberName);
                //}
                //else if (_result.Trim().ToLower() == AppModule.GetValue(TransactionType.Ticketing).Trim().ToLower())
                //{
                //    String _memberName = this._ticketingBL.GetMemberNameByTransType(_temp.TransNmbr, _result);
                //    Literal _memberNameLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                //    _memberNameLiteral.Text = HttpUtility.HtmlEncode(_memberName);
                //}
                //else if (_result.Trim().ToLower() == AppModule.GetValue(TransactionType.Hotel).Trim().ToLower())
                //{
                //    String _memberName = this._hotelBL.GetRefNmbrInterByTransType(_temp.TransNmbr, _result);
                //    Literal _memberNameLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                //    _memberNameLiteral.Text = HttpUtility.HtmlEncode(_memberName);
                //}


                //VIEW PAYMENT




                //if (_result.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).Trim().ToLower())
                //{
                //    Char _donePay = (this._internetBL.GetDonePayByTransType(_temp.TransNmbr, _result));
                //    Literal _paymentStatusLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                //    _paymentStatusLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_donePay)); ;
                //}
                //else if (_result.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).Trim().ToLower())
                //{
                //    Char _donePay = this._retailBL.GetDonePayByTransType(_temp.TransNmbr, _result);
                //    Literal _paymentStatusLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                //    _paymentStatusLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_donePay));
                //}
                //else if (_result.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).Trim().ToLower())
                //{
                //    Char _donePay = this._cafeBL.GetDonePayByTransType(_temp.TransNmbr, _result);
                //    Literal _paymentStatusLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                //    _paymentStatusLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_donePay));
                //}
                //else if (_result.Trim().ToLower() == AppModule.GetValue(TransactionType.Ticketing).Trim().ToLower())
                //{
                //    Char _donePay = this._ticketingBL.GetDonePayByTransType(_temp.TransNmbr, _result);
                //    Literal _paymentStatusLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                //    _paymentStatusLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_donePay));
                //}
                //else if (_result.Trim().ToLower() == AppModule.GetValue(TransactionType.Hotel).Trim().ToLower())
                //{
                //    Char _donePay = this._hotelBL.GetDonePayByTransType(_temp.TransNmbr, _result);
                //    Literal _paymentStatusLiteral = (Literal)e.Item.FindControl("PaymentStatusLiteral");
                //    _paymentStatusLiteral.Text = HttpUtility.HtmlEncode(POSTrSettlementDataMapper.GetDoneSettlementText(_donePay));
                //}
            }
        }

        protected void CheckStatusListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                this.ShowCheckStatusDetail(e.CommandArgument.ToString());
            }
        }

        protected void CheckStatusListRepeaterDt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Retail).Trim().ToLower())
                    {
                        POSTrRetailDt _temp = (POSTrRetailDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoCheckStatus.Text = this.TransNmbrHiddenField.Value;

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Internet).Trim().ToLower())
                    {
                        POSTrInternetDt _temp = (POSTrInternetDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoCheckStatus.Text = this.TransNmbrHiddenField.Value;

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Cafe).Trim().ToLower())
                    {
                        POSTrCafeDt _temp = (POSTrCafeDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoCheckStatus.Text = this.TransNmbrHiddenField.Value;

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == AppModule.GetValue(TransactionType.Ticketing).Trim().ToLower())
                    {
                        POSTrTicketingDt _temp = (POSTrTicketingDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoCheckStatus.Text = this.TransNmbrHiddenField.Value;

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == AppModule.GetValue(TransactionType.Hotel).Trim().ToLower())
                    {
                        POSTrHotelDt _temp = (POSTrHotelDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoCheckStatus.Text = this.TransNmbrHiddenField.Value;

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Printing).Trim().ToLower())
                    {
                        POSTrPrintingDt _temp = (POSTrPrintingDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoCheckStatus.Text = this.TransNmbrHiddenField.Value;

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Photocopy).Trim().ToLower())
                    {
                        POSTrPhotocopyDt _temp = (POSTrPhotocopyDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoCheckStatus.Text = this.TransNmbrHiddenField.Value;

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Graphic).Trim().ToLower())
                    {
                        POSTrGraphicDt _temp = (POSTrGraphicDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoCheckStatus.Text = this.TransNmbrHiddenField.Value;

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
                    }
                    else if (this.DetailTypeHiddenField.Value.Trim().ToLower() == POSTransTypeDataMapper.GetTransType(POSTransType.Shipping).Trim().ToLower())
                    {
                        POSTrShippingDt _temp = (POSTrShippingDt)e.Item.DataItem;
                        string _code = this.TransNmbrHiddenField.Value;
                        this.JobOrderNoCheckStatus.Text = this.TransNmbrHiddenField.Value;

                        //String _citycode = this._shippingBL.GetSinglePOSTrShippingHd(_temp.TransNmbr).DeliverCityCode;
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
                            _productName = _posMsShipping.VendorName + "-" + _posMsShipping.ShippingTypeName + "-" + _posMsShipping.CityName;
                        }

                        Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                        //_productCodeLiteral.Text = _posMsShipping.VendorName.Trim();
                        _productCodeLiteral.Text = _temp.VendorCode;

                        //String _productShape = "";
                        //if (_posMsShipping.ProductShape == "0")
                        //    _productShape = "Document. ";
                        //else
                        //    _productShape = "Non Document. ";

                        Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                        //_descLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);
                        //_descLiteral.Text = _posMsShipping.ShippingTypeName.Trim() + ". " + _productShape + _posMsShipping.CityName.Trim();
                        _descLiteral.Text = _productName;
                        
                        int _qty = (_temp.Weight == null) ? 0 : Convert.ToInt32(_temp.Weight);
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
                    }
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void CheckStatusPrintPreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.FormPanel.Visible = false;
            this.CheckStatusPrintPreviewPanel.Visible = true;
            //this.ReasonListPanel.Visible = false;

            this.CashierReportViewer.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._cashierBL.CheckStatusPrintPreview(Convert.ToDateTime(this.StartDateCheckStatusTextBox.Text), (Convert.ToDateTime(this.EndDateCheckStatusTextBox.Text)), (Convert.ToInt32(this.ReportTypeDDL.SelectedValue)));

            this.CheckStatusReportViewer.LocalReport.DataSources.Clear();
            this.CheckStatusReportViewer.LocalReport.DataSources.Add(_reportDataSource1);

            this.CheckStatusReportViewer.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _path2;
            this.CheckStatusReportViewer.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[4];
            _reportParam[0] = new ReportParameter("BeginDate", this.StartDateCheckStatusTextBox.Text, true);
            _reportParam[1] = new ReportParameter("EndDate", this.EndDateCheckStatusTextBox.Text, true);
            _reportParam[2] = new ReportParameter("FgReport", this.ReportTypeDDL.SelectedValue, true);
            _reportParam[3] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

            this.CheckStatusReportViewer.LocalReport.SetParameters(_reportParam);

            this.CheckStatusReportViewer.LocalReport.Refresh();
        }

        //VIEW MEMBER

        protected void ShowMember()
        {
            try
            {
                //this.MemberRepeater.DataSource = this._memberBL.GetListForMonitoring(this.SearchFieldMemberDDL.SelectedValue, this.SearchFieldTextBox.Text);
                this.MemberRepeater.DataSource = this._memberBL.GetListForMonitoring(this.SearchFieldDDL.SelectedValue, this.SearchFieldTextBox.Text); //this.SearchField2TextBox.Text
                this.MemberRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void MemberButton_Click(object sender, EventArgs e)
        {
            this.DisplayPanel("Member");
        }

        protected void MemberRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MsMember _temp = (MsMember)e.Item.DataItem;

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                Literal _memberNameLiteral = (Literal)e.Item.FindControl("MemberNameLiteral");
                _memberNameLiteral.Text = HttpUtility.HtmlEncode(_temp.MemberName);

                Literal _addressLiteral = (Literal)e.Item.FindControl("AddressLiteral");
                _addressLiteral.Text = HttpUtility.HtmlEncode(_temp.Address);

                Literal _telephoneLiteral = (Literal)e.Item.FindControl("TelephoneLiteral");
                _telephoneLiteral.Text = HttpUtility.HtmlEncode(_temp.Telephone1);

                Literal _handPhoneLiteral = (Literal)e.Item.FindControl("HandPhoneLiteral");
                _handPhoneLiteral.Text = HttpUtility.HtmlEncode(_temp.HandPhone1);

                Literal _barcodeLiteral = (Literal)e.Item.FindControl("BarcodeLiteral");
                _barcodeLiteral.Text = HttpUtility.HtmlEncode(_temp.Barcode);

            }
        }

        protected void RegistrationButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Registration/Registration.aspx");
        }


    }
}

