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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Threading;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using Microsoft.Reporting.WebForms;
using System.IO;
using BusinessRule.POS;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Text;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace POS.POSInterface.General
{
    public partial class Cashier : GeneralBase
    {
        private List<V_POSReferenceNotYetPayList> _list1 = new List<V_POSReferenceNotYetPayList>();
        private List<V_POSReferenceNotYetPayList> _list2 = new List<V_POSReferenceNotYetPayList>();
        private CashierBL _cashierBL = new CashierBL();
        private AirLineBL _airLineBL = new AirLineBL();
        private HotelBL _hotelBL = new HotelBL();
        private POSShippingBL _posShippingBL = new POSShippingBL();
        private UnitBL _unitBL = new UnitBL();
        private ProductBL _productBL = new ProductBL();
        private KitchenBL _kitchenBL = new KitchenBL();
        private CloseShiftBL _closeShiftBL = new CloseShiftBL();
        private DebitCardBL _debitCardBL = new DebitCardBL();
        private CreditCardBL _creditCardBL = new CreditCardBL();
        private CountryBL _countryBL = new CountryBL();
        //private ShippingBL _shippingBL = new ShippingBL();
        private VendorBL _vendorBL = new VendorBL();
        private CityBL _cityBL = new CityBL();
        //private CashierPrinterBL _cashierPrinterBL = new CashierPrinterBL();
        private CompanyBL _companyBL = new CompanyBL();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.ReferencesNumberTextBox.Attributes["onclick"] = "ReferenceKeyBoard(this.id)";
                String spawnJS = "<script type='text/javascript' language='JavaScript'>\n";
                //DECLARE FUNCTION FOR Calling KeyBoard Reference
                spawnJS += "function ReferenceKeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findReference&titleinput=Category&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON Reference
                spawnJS += "function findReference(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.ReferencesNumberTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.SetAttribute();
                this.ClearData();
                //this.CancelInActiveTransaction();
                this.ShowData();
                this.ChangeVisiblePanel(0);
            }
        }

        private void SetAttribute()
        {

        }

        private void ClearData()
        {
            this.ReferencesNumberTextBox.Text = "";
            this.SettleTypeHiddenField.Value = "";
        }

        private void CancelInActiveTransaction()
        {
            try
            {
                this._cashierBL.CancelTransaction();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void ClearListDetail()
        {
            this.ListRepeaterDetail.DataSource = null;
            this.ListRepeaterDetail.DataBind();
        }

        private void ClearListDetail2()
        {
            this.ListRepeater2Detail.DataSource = null;
            this.ListRepeater2Detail.DataBind();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (this.Category.Text == "Reference")
            {
                this.ShowData();
                this.ClearListDetail();
            }
            else if (this.Category.Text == "TransNmbr")
            {
                this.ShowDataPayedList();
                this.ClearListDetail2();
            }
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearData();
            this.ShowData();
            this.ClearListDetail();
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(ApplicationConfig.POSInterfaceWebAppURL + this._loginPage);
        }

        protected void ResetSelectionButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(ApplicationConfig.POSInterfaceWebAppURL + this._loginPage);
        }

        private void ShowData()
        {
            try
            {
                this._list1 = this._cashierBL.GetListReferenceNotPay(this.ReferencesNumberTextBox.Text);

                this.AssignHiddenField();

                this.DisplayData();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void ShowDataPayedList()
        {
            this.PayListRepeater.DataSource = this._cashierBL.GetListPaidPOSTrSettlementHd(0, 20, "TransNmbr", this.ReferencesNumberTextBox.Text);
            this.PayListRepeater.DataBind();
        }

        private void DisplayData()
        {
            this.ListRepeater.DataSource = this._list1;
            this.ListRepeater.DataBind();

            this.ListRepeater2.DataSource = this._list2;
            this.ListRepeater2.DataBind();
        }

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
                    this.ListRepeaterDetail.DataSource = _posShippingBL.GetListShippingDtByTransNmbr(_transNmbr);
                    this.ListRepeaterDetail.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void ShowDataDetailPayed(String _prmArgument)
        {
            try
            {
                String[] _break = _prmArgument.Split(',');
                String _transNmbr = _break[0];
                //String _transType = _break[1];
                this.AmountCashLiteral.Text = "0";
                this.PaymentDebitLiteral.Text = "";
                this.AmountDebitLiteral.Text = "0";
                this.PaymentCreditLiteral.Text = "";
                this.AmountCreditLiteral.Text = "0";
                this.PaymentVoucherLiteral.Text = "";
                this.AmountVoucherLiteral.Text = "0";

                this.TransNmbr2HiddenField.Value = _transNmbr;
                //this.DetailType2HiddenField.Value = _transType;
                List<POSTrSettlementDtPaymentType> _posTrSettlementDtPaymentType = this._closeShiftBL.GetListCloseShift(_transNmbr);
                foreach (var _row in _posTrSettlementDtPaymentType)
                {
                    if (_row.PaymentType == "CASH")
                    {
                        this.AmountCashLiteral.Text = Convert.ToDecimal(_row.PaymentAmount).ToString("#,#.00");
                    }
                    else if (_row.PaymentType == "DEBIT")
                    {
                        this.PaymentDebitLiteral.Text = this._debitCardBL.GetSingle(_row.CardType).DebitCardName;
                        this.AmountDebitLiteral.Text = Convert.ToDecimal(_row.PaymentAmount).ToString("#,#.00");
                    }
                    else if (_row.PaymentType == "CREDIT")
                    {
                        this.PaymentCreditLiteral.Text = this._creditCardBL.GetSingle(_row.CardType).CreditCardName;
                        this.AmountCreditLiteral.Text = Convert.ToDecimal(_row.PaymentAmount).ToString("#,#.00");
                    }
                    else if (_row.PaymentType == "VOUCHER")
                    {
                        this.PaymentVoucherLiteral.Text = _row.EDCReference;
                        this.AmountVoucherLiteral.Text = Convert.ToDecimal(_row.PaymentAmount).ToString("#,#.00");
                    }
                }
                Decimal _totalPayment = Convert.ToDecimal(this.AmountCashLiteral.Text) + Convert.ToDecimal(this.AmountDebitLiteral.Text) + Convert.ToDecimal(this.AmountCreditLiteral.Text) + Convert.ToDecimal(this.AmountVoucherLiteral.Text);
                this.TotalPaymentLiteral.Text = _totalPayment.ToString("#,#.00");
                this.ListRepeater2Detail.DataSource = this._cashierBL.GetListPOSTrSettlementDtProduct(0, 100, "TransNmbr", _transNmbr);
                this.ListRepeater2Detail.DataBind();
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

                //HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                //_tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                //_tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

                Literal _transNmbrLiteral = (Literal)e.Item.FindControl("TransNmbrLiteral");
                _transNmbrLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _divisiLiteral = (Literal)e.Item.FindControl("DivisiLiteral");
                _divisiLiteral.Text = HttpUtility.HtmlEncode(_temp.TransType);

                Literal _refLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _refLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewDetailImageButton");
                //_viewDetailButton.ImageUrl = ApplicationConfig.POSInterfaceWebAppURL + "images/view_detail.jpg";
                _viewDetailButton.CommandArgument = _code + "," + _temp.TransType;
                _viewDetailButton.CommandName = "ViewDetail";

                ImageButton _pickButton = (ImageButton)e.Item.FindControl("PickImageButton");
                //_pickButton.ImageUrl = ApplicationConfig.POSInterfaceWebAppURL + "images/arrowright.gif";
                _pickButton.CommandArgument = _code + "," + _temp.TransType + "," + e.Item.ItemIndex;
                _pickButton.CommandName = "PickDetail";


                ImageButton _DpButton = (ImageButton)e.Item.FindControl("DpImageButton");
                //_pickButton.ImageUrl = ApplicationConfig.POSInterfaceWebAppURL + "images/arrowright.gif";
                _DpButton.CommandArgument = _code + "," + _temp.TransType + "," + e.Item.ItemIndex;
                _DpButton.CommandName = "DpDetail";
            }
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ViewDetail")
                {
                    //this.ClearData();
                    this.ShowDataDetail(e.CommandArgument.ToString());
                }
                else if (e.CommandName == "PickDetail")
                {
                    if (this.SettleTypeHiddenField.Value == "" || this.SettleTypeHiddenField.Value == POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.Paid))
                    {
                        String[] _break = e.CommandArgument.ToString().Split(',');
                        String _transNmbr = _break[0];
                        String _transType = _break[1];
                        String _index = _break[2];

                        this.FillListFromHiddenField();

                        //add selected data to list2
                        this._list2.Add(this._list1.Find(_temp => _temp.TransNmbr == _transNmbr));

                        //remove selected data from list1
                        this._list1.RemoveAt(Convert.ToInt32(_index));

                        this.AssignHiddenField();

                        this.DisplayData();
                        this.SettleTypeHiddenField.Value = POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.Paid);
                    }
                }
                else if (e.CommandName == "DpDetail")
                {
                    if (this.SettleTypeHiddenField.Value == "" || this.SettleTypeHiddenField.Value == POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.DP))
                    {
                        String[] _break = e.CommandArgument.ToString().Split(',');
                        String _transNmbr = _break[0];
                        String _transType = _break[1];
                        String _index = _break[2];

                        //this.FillListFromHiddenField();

                        ////add selected data to list2
                        //this._list2.Add(this._list1.Find(_temp => _temp.TransNmbr == _transNmbr));

                        ////remove selected data from list1
                        //this._list1.RemoveAt(Convert.ToInt32(_index));

                        //this.AssignHiddenField();

                        //this.DisplayData();
                        this.SettleTypeHiddenField.Value = POSTrSettlementDataMapper.GetSettleType(POSTrSettleType.DP);
                        Response.Redirect(this._settlementPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_transNmbr, ApplicationConfig.EncryptionKey)) + "&" + this._settleType + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.SettleTypeHiddenField.Value, ApplicationConfig.EncryptionKey)));
                    }
                }
            }
            catch (ThreadAbortException) { throw; }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                V_POSReferenceNotYetPayList _temp = (V_POSReferenceNotYetPayList)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                //HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate2");
                //_tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                //_tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

                Literal _transNmbrLiteral = (Literal)e.Item.FindControl("TransNmbrLiteral");
                _transNmbrLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _divisiLiteral = (Literal)e.Item.FindControl("DivisiLiteral");
                _divisiLiteral.Text = HttpUtility.HtmlEncode(_temp.TransType);

                Literal _refLiteral = (Literal)e.Item.FindControl("ReferenceNoLiteral");
                _refLiteral.Text = HttpUtility.HtmlEncode(_temp.ReferenceNo);

                ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewDetailImageButton");
                //_viewDetailButton.ImageUrl = ApplicationConfig.POSInterfaceWebAppURL + "images/view_detail.jpg";
                _viewDetailButton.CommandArgument = _code + "," + _temp.TransType;
                _viewDetailButton.CommandName = "ViewDetail";

                ImageButton _pickButton = (ImageButton)e.Item.FindControl("ResetPickImageButton");
                //_pickButton.ImageUrl = ApplicationConfig.POSInterfaceWebAppURL + "images/arrowleft.gif";
                _pickButton.CommandArgument = _code + "," + _temp.TransType + "," + e.Item.ItemIndex;
                _pickButton.CommandName = "ResetPick";
            }
        }

        protected void ListRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                //this.ClearData();
                this.ShowDataDetail(e.CommandArgument.ToString());
            }
            else if (e.CommandName == "ResetPick")
            {
                String[] _break = e.CommandArgument.ToString().Split(',');
                String _transNmbr = _break[0];
                String _transType = _break[1];
                String _index = _break[2];

                this.FillListFromHiddenField();

                //add selected data to list1
                this._list1.Add(this._list2.Find(_temp => _temp.TransNmbr == _transNmbr));

                //remove selected data from list2
                this._list2.RemoveAt(Convert.ToInt32(_index));

                if (this._list2.Count == 0)
                {
                    this.SettleTypeHiddenField.Value = "";
                }
                this.AssignHiddenField();

                this.DisplayData();
            }
        }

        protected void PayListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSTrSettlementHd _temp = (POSTrSettlementHd)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();

                Literal _transNmbrLiteral = (Literal)e.Item.FindControl("TransNmbrLiteral");
                _transNmbrLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _fileNmbrLiteral = (Literal)e.Item.FindControl("FileNmbrLiteral");
                _fileNmbrLiteral.Text = HttpUtility.HtmlEncode(_temp.FileNmbr);

                Literal _transDateLiteral = (Literal)e.Item.FindControl("TransDateLiteral");
                _transDateLiteral.Text = _temp.TransDate.ToString();

                Literal _cashierLiteral = (Literal)e.Item.FindControl("CashierLiteral");
                _cashierLiteral.Text = HttpUtility.HtmlEncode(_temp.CashierID);

                Literal _settleTypeLiteral = (Literal)e.Item.FindControl("SettleTypeLiteral");
                _settleTypeLiteral.Text = HttpUtility.HtmlEncode(_temp.SettleType);

                Literal _totalLiteral = (Literal)e.Item.FindControl("TotalLiteral");
                _totalLiteral.Text = Convert.ToDecimal(_temp.TotalForex).ToString("#,#.00");

                ImageButton _viewDetailButton = (ImageButton)e.Item.FindControl("ViewDetailImageButton");
                _viewDetailButton.CommandArgument = _code;
                _viewDetailButton.CommandName = "ViewDetail";

                ImageButton _printCustomerButton = (ImageButton)e.Item.FindControl("PrintCustomerImageButton");
                _printCustomerButton.CommandArgument = _code;
                _printCustomerButton.CommandName = "PrintCustomer";

                ImageButton _printKitchenButton = (ImageButton)e.Item.FindControl("PrintKitchenImageButton");
                _printKitchenButton.CommandArgument = _code;
                _printKitchenButton.CommandName = "PrintKitchen";

            }
        }

        protected void PayListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                this.ShowDataDetailPayed(e.CommandArgument.ToString());
            }
            else if (e.CommandName == "PrintCustomer" | e.CommandName == "PrintKitchen")
            {
                this.Print(e.CommandName, e.CommandArgument.ToString());
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

                    //HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("DetailRepeaterItemTemplate");
                    //_tableRow.Attributes.Add("OnMouseOver", "this.className='TableRowOver';");
                    //_tableRow.Attributes.Add("OnMouseOut", "this.className='TableRow';");

                    Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                    _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.VendorCode);

                    String _productShape = _temp.ProductShape == "0" ? "Document" : "Non Document";

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

                        _productName = _vendor + "-" + this._posShippingBL.GetSinglePOSMsZone(_temp.ShippingTypeCode).ZoneName + "-" + _productShape + "-" + _country + "." + _city;
                    }
                    else
                    {
                        POSMsShipping _posMsShipping = this._posShippingBL.GetPOSMsShipping(_temp.VendorCode, _temp.ShippingTypeCode, _temp.ProductShape, _cityCode);
                        _productName = _posMsShipping.VendorName + "-" + _posMsShipping.ShippingTypeName + "-" + _productShape + "-" + _country + "." + _posMsShipping.CityName;
                    }

                    Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                    _descLiteral.Text = _productName;

                    //String _unit = this._unitBL.GetSingle(_temp.Unit).UnitName;
                    Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                    //_qtyLiteral.Text = _productShape + "-" + HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.Weight).ToString("#,#")) + " " + _unit;
                    _qtyLiteral.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.Weight).ToString("#,#"));

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

        protected void ListRepeater2Detail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSTrSettlementDtProduct _temp = (POSTrSettlementDtProduct)e.Item.DataItem;
                string _code = this.TransNmbr2HiddenField.Value;
                this.JobOrder2NoLiteral.Text = this.TransNmbr2HiddenField.Value;

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                String _productName = this._productBL.GetProductNameByCode(_temp.ProductCode);
                Literal _descLiteral = (Literal)e.Item.FindControl("DescriptionLiteral");
                _descLiteral.Text = _productName;

                int _qty = (_temp.Qty == null) ? 0 : Convert.ToInt32(_temp.Qty);
                Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                _qtyLiteral.Text = HttpUtility.HtmlEncode(_qty.ToString());

                Literal _unitLiteral = (Literal)e.Item.FindControl("UnitLiteral");
                _unitLiteral.Text = HttpUtility.HtmlEncode(_temp.Unit);

                Decimal _price = this._productBL.GetSingleProduct(_temp.ProductCode).SellingPrice;
                Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                _priceLiteral.Text = HttpUtility.HtmlEncode(_price.ToString("#,##0.00"));

                Decimal _totalForex = (_temp.TotalForex == null) ? 0 : Convert.ToDecimal(_temp.TotalForex);
                Literal _totalForexLiteral = (Literal)e.Item.FindControl("TotalForexLiteral");
                _totalForexLiteral.Text = HttpUtility.HtmlEncode(_totalForex.ToString("#,##0.00"));
            }
        }

        protected void SettlementButton_Click(object sender, EventArgs e)
        {
            String _strTransNmbr = "";
            if (this.List2HiddenField.Value != "")
            {
                String[] _dataRow2 = this.List2HiddenField.Value.Split('^');
                foreach (var _item2 in _dataRow2)
                {
                    String[] _field2 = _item2.Split(',');

                    if (_strTransNmbr == "")
                    {
                        _strTransNmbr = _field2[0];
                    }
                    else
                    {
                        _strTransNmbr += "," + _field2[0];
                    }
                }
                //Response.Redirect(this._settlementPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_strTransNmbr, ApplicationConfig.EncryptionKey)));
                Response.Redirect(this._settlementPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_strTransNmbr, ApplicationConfig.EncryptionKey)) + "&" + this._settleType + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.SettleTypeHiddenField.Value, ApplicationConfig.EncryptionKey)));
            }
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "CASHIER");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }

        protected void ChangeVisiblePanel(Byte _prmValue)
        {
            this.FormPanel.Visible = true;
            this.ReportPanel.Visible = false;
            if (_prmValue == 0)
            {
                this.NotYetPayListPanel.Visible = true;
                this.PayListPanel.Visible = false;
                this.Category.Text = "Reference";
                this.Back2Button.Visible = false;
            }
            else if (_prmValue == 1)
            {
                this.NotYetPayListPanel.Visible = false;
                this.PayListPanel.Visible = true;
                this.ShowDataPayedList();
                this.Category.Text = "TransNmbr";
                this.Back2Button.Visible = true;
            }
        }

        protected void ReprintStrookButton_Click(object sender, EventArgs e)
        {
            this.ChangeVisiblePanel(1);
            this.ClearData();
            this.ShowDataPayedList();
            this.ClearListDetail2();
        }

        protected void Back2Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cashier.aspx");
        }

        private IList<Stream> m_streams;
        private int m_currentPageIndex;
        private bool _prmDO = false;

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

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        protected void Print(String _prmPrintTo, String _prmTransNmbr)
        {
            String _companyAddress = this._companyBL.GetSingleDefault().PrimaryAddress;
            try
            {
                ReportDataSource _reportDataSource1 = new ReportDataSource();
                String _reportPath = "";
                ReportParameter[] _reportParam = new ReportParameter[1];
                LocalReport report = new LocalReport();

                if (_prmPrintTo == "PrintCustomer")
                {
                    _reportDataSource1 = this._cashierBL.ReportSendToCustomer(_prmTransNmbr, _companyAddress);

                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

                    _reportPath = "General/ReportSendToCustomer.rdlc";
                    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath;

                    _reportParam = new ReportParameter[2];
                    _reportParam[0] = new ReportParameter("TransNmbr", _prmTransNmbr, false);
                    _reportParam[1] = new ReportParameter("CompanyAddress", _companyAddress, false);
                    //_reportParam[1] = new ReportParameter("TransType", _item.TransType, false);

                    this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                    this.ReportViewer1.LocalReport.Refresh();

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

                        ////V_POSMsCashierPrinter _vPOSMsCashierPrinter = this._cashierPrinterBL.GetDefaultPrinter();
                        POSMsCashierPrinter _vPOSMsCashierPrinter = this._cashierBL.GetDefaultPrinter(hostIP);
                        if (_vPOSMsCashierPrinter != null)
                        {
                            printerName = "\\\\" + _vPOSMsCashierPrinter.IPAddress + "\\" + _vPOSMsCashierPrinter.PrinterName;
                        }
                        //else
                        //{
                        //    printerName = "\\\\" + "192.168.100.199" + "\\" + "Kasir4";
                        //}
                        ////String printerName = "\\\\" + "192.168.100.194" + "\\" + "Cashier02";

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
                    //}
                }
                else if (_prmPrintTo == "PrintKitchen")
                {
                    bool _hasil = this._cashierBL.CekFgSendToKitchen(_prmTransNmbr);
                    if (_hasil & _prmDO == false)
                    {
                        //List<POSTrSettlementDtRefTransaction> _refTrans = this._cashierBL.GetRefTrans(_prmTransNmbr);
                        //_refTrans = this._cashierBL.GetRefTrans(_prmTransNmbr);

                        //foreach (var _item in _refTrans)
                        //{
                        List<POSMsKitchen> _posMsKitchen = this._cashierBL.GetPrinterKitchen(_prmTransNmbr);

                        foreach (var _item in _posMsKitchen)
                        {
                            _reportDataSource1 = new ReportDataSource();

                            _reportDataSource1 = this._cashierBL.ReportSendToKitchen(_prmTransNmbr, _item.KitchenCode);

                            this.ReportViewer2.LocalReport.DataSources.Clear();
                            this.ReportViewer2.LocalReport.DataSources.Add(_reportDataSource1);

                            _reportPath = "General/ReportSendToKitchen.rdlc";
                            this.ReportViewer2.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath;

                            _reportParam = new ReportParameter[2];
                            _reportParam[0] = new ReportParameter("TransNmbr", _prmTransNmbr, false);
                            _reportParam[1] = new ReportParameter("kitchenCode", _item.KitchenCode, false);

                            this.ReportViewer2.LocalReport.SetParameters(_reportParam);
                            this.ReportViewer2.LocalReport.Refresh();

                            report = new LocalReport();
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

                                //String printerName = this.GetDefaultPrinter();
                                //String printerName = "\\\\192.168.100.16\\Generic";
                                //POSMsKitchen _pOSMsKitchen = this._kitchenBL.GetSingle("0001");
                                //String printerName = "\\\\" + _pOSMsKitchen.KitchenPrinterIPAddress + "\\" + _pOSMsKitchen.KitchenPrinterName;
                                String printerName = "\\\\" + _item.KitchenPrinterIPAddress + "\\" + _item.KitchenPrinterName;

                                //String printerName = _configBL.GetConfig("SetPrinterName");
                                ////PrinterSettings.InstalledPrinters[1].ToString();
                                ////new PrinterSettings().PrinterName;
                                ////this.GetDefaultPrinter();
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
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        public String GetDefaultPrinter()
        {
            //this.Label4.Text = "";
            PrinterSettings settings = new PrinterSettings();
            String printer;

            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                printer = PrinterSettings.InstalledPrinters[i];
                settings.PrinterName = printer;
                //this.Label4.Text = this.Label4.Text + " , " + i.ToString() + " " + settings.IsDefaultPrinter.ToString();
                //this.Label5.Text = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }

            //this.Label4.Text = PrinterSettings.InstalledPrinters[0];
            //this.Label4.Text = PrinterSettings.InstalledPrinters[1];
            //this.Label4.Text = PrinterSettings.InstalledPrinters[2];
            //this.Label4.Text = PrinterSettings.InstalledPrinters[3];
            //this.Label4.Text = PrinterSettings.InstalledPrinters[0];

            //foreach (String printer in PrinterSettings.InstalledPrinters)
            //{
            //    settings.PrinterName = printer;
            //    this.Label4.Text = this.Label4.Text + " , " + settings.PrinterName;
            //    if (settings.IsDefaultPrinter)
            //        return printer;
            //}
            return string.Empty;
        }

    }
}