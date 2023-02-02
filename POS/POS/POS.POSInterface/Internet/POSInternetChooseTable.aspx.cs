using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule.POS;
using BusinessRule.POSInterface;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Threading;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace POS.POSInterface.Internet
{
    public partial class POSInternetChooseTable : POSInternetBase
    {
        private POSInternetBL _posInternetBL = new POSInternetBL();
        private MemberBL _memberBL = new MemberBL();
        private ProductBL _productBL = new ProductBL();
        private POSDiscountBL _posDiscountBL = new POSDiscountBL();
        private CustomerDOBL _customerDOBL = new CustomerDOBL();
        private POSReasonBL _reasonBL = new POSReasonBL();
        private POSConfigurationBL _pOSConfigurationBL = new POSConfigurationBL();
        private PermissionBL _permBL = new PermissionBL();
        //private String _transNmbr = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.qtyProductTextBox.Focus();
                this.CashierLabel.Text = HttpContext.Current.User.Identity.Name;
                if (!this.Page.IsPostBack == true)
                {
                    this.ProductGroupPageHiddenField.Value = "0";
                    this.ProductSubGroupPageHiddenField.Value = "0";
                    this.ProductPageHiddenField.Value = "0";
                    this.IsEditedHiddenField.Value = "0";

                    this.UpdateButton.Enabled = false;
                    this.NotesTextBox.Enabled = false;
                    this.PrevProductSubGroupButton.Enabled = false;
                    this.NextProductSubGroupButton.Enabled = false;
                    this.ProductPrevButton.Enabled = false;
                    this.ProductNextButton.Enabled = false;
                    this.WarningLabel.Text = "";

                    this.MemberNoTextBox.Attributes.Add("onKeyDown", "return (event.keyCode||event.which) != 13;");                

                    //this.NewMemberButton.OnClientClick = "window.open('../Registration/Registration.aspx?valueCatcher=findProduct&configCode=product','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";
                    this.SearchImageButton.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=productPOS','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";

                    this.JoinJobOrderImageButton.OnClientClick = "window.open('../General/JoinJobOrder.aspx?valueCatcher=findTransNmbr&pos=internet','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1')";
                    //this.SearchMemberButton.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findMember&configCode=member','_popSearch','width=1024,height=764,toolbar=0,location=0,status=0,scrollbars=1')";

                    this.ReferenceTextBox.Attributes["onclick"] = "ReferenceKeyBoard(this.id)";
                    this.MemberNoTextBox.Attributes["onclick"] = "MemberNoKeyBoard(this.id)";
                    this.MemberNameTextBox.Attributes["onclick"] = "MemberNameKeyBoard(this.id)";
                    this.CustPhoneTextBox.Attributes["onclick"] = "CustPhoneKeyBoard(this.id)";
                    this.NotesTextBox.Attributes["onclick"] = "NotesKeyBoard(this.id)";
                    this.PasswordTextBox.Attributes["onclick"] = "PasswordKeyBoard(this.id)";

                    String spawnJS = "<script type='text/javascript' language='JavaScript'>\n";

                    // prodcode,qty,prodname,sellprice,disc,total,description
                    //DECLARE FUNCTION FOR CATCHING SUPPLIER SEARCH
                    spawnJS += "function findProduct(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "var qty = 1;\n";
                    spawnJS += "if ($('#qtyProductTextBox').val() > 0) qty = $('#qtyProductTextBox').val() ;\n";
                    spawnJS += "if ( $('#BoughtItemHiddenField').val() != '' ) $('#BoughtItemHiddenField').val($('#BoughtItemHiddenField').val() + '^') ;\n";
                    //spawnJS += "alert(dataArray[0] + '|' + qty + '|' + dataArray[1] + '|' + dataArray[2] + '|0|' + ( qty * dataArray[2] ) + '|' );\n";
                    spawnJS += "$('#BoughtItemHiddenField').val($('#BoughtItemHiddenField').val() + dataArray[0] + '|' + qty + '|' + dataArray[1] + '|' + dataArray[2] + '|0|' + ( qty * dataArray[2] ) + '|' ) ;\n";
                    //spawnJS += "$('#qtyProductTextBox').val('0');\n";
                    //dataArray[0] productCode, dataArray[1] productName,dataArray[2] sellingPrice
                    //spawnJS += "document.getElementById('" + this.SupplierTextBox.ClientID + "').value = dataArray[0];\n";
                    //spawnJS += "document.getElementById('" + this.SupplierLabel.ClientID + "').innerHTML = dataArray[1];\n";
                    spawnJS += "document.forms[0].submit();\n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR CATCHING ON HOLD SEARCH
                    spawnJS += "function findTransNmbr(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "document.getElementById('" + this.TransRefTextBox.ClientID + "').value = dataArray [0];\n";
                    spawnJS += "document.forms[0].submit();\n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR Calling KeyBoard Reference
                    spawnJS += "function ReferenceKeyBoard(x) {\n";
                    spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findReference&titleinput=Reference&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR CATCHING ON Reference
                    spawnJS += "function findReference(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "document.getElementById('" + this.ReferenceTextBox.ClientID + "').value = dataArray [0];\n";
                    spawnJS += "document.forms[0].submit();\n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR Calling KeyBoard MemberNo
                    spawnJS += "function MemberNoKeyBoard(x) {\n";
                    spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findMemberNo&titleinput=Member No&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR CATCHING ON MemberNo
                    spawnJS += "function findMemberNo(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "document.getElementById('" + this.MemberNoTextBox.ClientID + "').value = dataArray [0];\n";
                    spawnJS += "document.forms[0].submit();\n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR Calling KeyBoard MemberName
                    spawnJS += "function MemberNameKeyBoard(x) {\n";
                    spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findMemberName&titleinput=Name&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR CATCHING ON MemberName
                    spawnJS += "function findMemberName(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "document.getElementById('" + this.MemberNameTextBox.ClientID + "').value = dataArray [0];\n";
                    spawnJS += "document.forms[0].submit();\n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR Calling KeyBoard CustPhone
                    spawnJS += "function CustPhoneKeyBoard(x) {\n";
                    spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findCustPhone&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR CATCHING ON CustPhone
                    spawnJS += "function findCustPhone(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "document.getElementById('" + this.CustPhoneTextBox.ClientID + "').value = dataArray [0];\n";
                    spawnJS += "document.forms[0].submit();\n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR Calling KeyBoard Notes
                    spawnJS += "function NotesKeyBoard(x) {\n";
                    spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findNotes&titleinput=Notes&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                    spawnJS += "}\n";

                    //DECLARE FUNCTION FOR CATCHING ON Notes
                    spawnJS += "function findNotes(x) {\n";
                    spawnJS += "dataArray = x.split ('|') ;\n";
                    spawnJS += "document.getElementById('" + this.NotesTextBox.ClientID + "').value = dataArray [0];\n";
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

                    this.ChangeVisiblePanel(0);
                    this.SetButtonPermission();
                    //this.FormPanel.Visible = true;
                    //this.ReasonListPanel.Visible = false;
                    //this.MemberNoTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.MemberNoTextBox.ClientID + ")");
                    //this.MemberNameTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id); $(this).select();");
                    //this.CustPhoneTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id); $(this).select();");
                    //this.CustPhoneTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.CustPhoneTextBox.ClientID + ")");
                    //this.qtyProductTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id);");


                    //if (this.EditRowHiddenField.Value != "")
                    //{
                    //    String[] _boughtItems = this.BoughtItemHiddenField.Value.Split('^');
                    //    String[] _editData = _boughtItems[Convert.ToInt16(this.EditRowHiddenField.Value)].Split('|');
                    //    this.ProductRepeater.Visible = false;
                    //    this.UpdateButton.Enabled = true;
                    //    this.NotesTextBox.Enabled = true;
                    //    this.CancelAllImageButton.Enabled = false;
                    //    this.SearchImageButton.Enabled = false;
                    //    if (this.EditPostBackHiddenField.Value != this.EditRowHiddenField.Value)
                    //    {
                    //        this.NotesTextBox.Text = _editData[6];
                    //        this.qtyProductTextBox.Text = _editData[1];
                    //        this.EditPostBackHiddenField.Value = this.EditRowHiddenField.Value;
                    //    }

                    //}

                    //this.AddServiceCharge();

                    //if (this.TransRefTextBox.Text != "")
                    //{
                    //    if (this.IsEditedHiddenField.Value == "0")
                    //    {
                    //        //this.LoadHoldedData();
                    //    }
                    //}

                    //this.ComposeBoughtItem();
                }
                this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
                string _code = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._referenceNo), ApplicationConfig.EncryptionKey);
                if (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._referenceNo), ApplicationConfig.EncryptionKey) != "")
                {
                    string[] _splitCode = _code.Split('-');
                    this.ReferenceTextBox.Text = _splitCode[0];
                    this.ReferenceTextBox.Enabled = false;
                    this.ReferenceNoHiddenField.Value = _splitCode[0];
                    this.MemberNoTextBox.Enabled = false;
                    this.MemberNameTextBox.Text = _splitCode[1];
                    this.MemberNameTextBox.Enabled = false;
                    this.CustPhoneTextBox.Text = _splitCode[2];
                    this.CustPhoneTextBox.Enabled = false;
                    this.NewMemberButton.Enabled = false;
                    this.LayoutMapImageButton.Enabled = false;
                    POSTrDeliveryOrderRef _pOSTrDeliveryOrderRef = this._customerDOBL.GetSingleTrDeliveryOrderRefByReferenceNoTransType(_splitCode[0], POSTransTypeDataMapper.GetTransType(POSTransType.Internet));
                    if (_pOSTrDeliveryOrderRef != null)
                    {
                        this.TransRefTextBox.Text = _pOSTrDeliveryOrderRef.TransNmbr;
                    }
                    if (this.IsEditedHiddenField.Value == "0")
                    {
                        this.LoadHoldedData();
                        //this.IsEditedHiddenField.Value = "1";
                    }
                }
                else
                {
                    this.TableIDPerRoomHiddenField.Value = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._selectedTableKey), ApplicationConfig.EncryptionKey);
                    this.FloorNumberHiddenField.Value = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._selectedFloorKey), ApplicationConfig.EncryptionKey);
                    this.TableNumberLabel.Text = this._posInternetBL.GetTableNumber(this.FloorNumberHiddenField.Value, Convert.ToInt16(this.TableIDPerRoomHiddenField.Value));
                    int _statustable = this._posInternetBL.GetTableStatus(this.FloorNumberHiddenField.Value, Convert.ToInt16(this.TableIDPerRoomHiddenField.Value));
                    int _floorNmbr = this._posInternetBL.GetFloorNmbrByName(this.FloorNumberHiddenField.Value);
                    if (_statustable == 1)
                    {
                        POSTrInternetBookingHd _pOSTrInternetBookingHd = this._posInternetBL.GetSinglePOSTrInternetBookingHd(_floorNmbr, Convert.ToInt16(this.TableIDPerRoomHiddenField.Value));
                        if (_pOSTrInternetBookingHd != null)
                        {
                            this.CashierLabel.Text = _pOSTrInternetBookingHd.OperatorName;
                            this.ReferenceTextBox.Text = _pOSTrInternetBookingHd.ReferenceNo;
                            this.MemberNoTextBox.Text = _pOSTrInternetBookingHd.MemberCode;
                            this.MemberNameTextBox.Text = _pOSTrInternetBookingHd.CustName;
                            this.CustPhoneTextBox.Text = _pOSTrInternetBookingHd.PhoneNumber1;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Please open this table before you transaction.');", true);

                        }
                    }
                    else if (_statustable == 2)
                    {
                        //POSTrInternetHd _pOSTrInternetHd = this._posInternetBL.GetSinglePOSTrInternetHd(_floorNmbr, Convert.ToInt16(this.TableIDPerRoomHiddenField.Value));
                        POSTrInternetHd _pOSTrInternetHd = this._posInternetBL.GetSinglePOSTableStatusHistory(_floorNmbr, Convert.ToInt16(this.TableIDPerRoomHiddenField.Value));
                        if (_pOSTrInternetHd != null)
                        {
                            this.CashierLabel.Text = _pOSTrInternetHd.OperatorID;
                            this.ReferenceTextBox.Text = _pOSTrInternetHd.ReferenceNo;
                            this.MemberNoTextBox.Text = _pOSTrInternetHd.MemberID;
                            this.MemberNameTextBox.Text = _pOSTrInternetHd.CustName;
                            this.CustPhoneTextBox.Text = _pOSTrInternetHd.CustPhone;
                        }
                    }
                }


                this.MemberNoTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.MemberNoTextBox.ClientID + ")");
                this.MemberNameTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id); $(this).select();");
                this.CustPhoneTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id); $(this).select();");
                this.CustPhoneTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.CustPhoneTextBox.ClientID + ")");
                this.qtyProductTextBox.Attributes.Add("onfocus", "$('#currActiveInput').val(this.id);");
                this.qtyProductTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.qtyProductTextBox.ClientID + ")");

                if (this.EditRowHiddenField.Value != "" && this.BoughtItemHiddenField.Value != "")
                {
                    String[] _boughtItems = this.BoughtItemHiddenField.Value.Split('^');
                    String[] _editData = _boughtItems[Convert.ToInt16(this.EditRowHiddenField.Value)].Split('|');
                    this.ProductRepeater.Visible = false;
                    this.UpdateButton.Enabled = true;
                    this.NotesTextBox.Enabled = true;
                    this.CancelAllImageButton.Enabled = false;
                    this.SearchImageButton.Enabled = false;
                    if (this.EditPostBackHiddenField.Value != this.EditRowHiddenField.Value)
                    {
                        this.NotesTextBox.Text = _editData[6];
                        this.qtyProductTextBox.Text = _editData[1];
                        this.EditPostBackHiddenField.Value = this.EditRowHiddenField.Value;
                    }
                }
                if (this.TransRefTextBox.Text != "")
                {
                    if (this.IsEditedHiddenField.Value == "0")
                    {
                        this.LoadHoldedData();
                    }
                }
                this.ComposeBoughtItem();

                this.ProductGroupRepeater.DataSource = _posInternetBL.GetListProductGroup(Convert.ToInt16(this.ProductGroupPageHiddenField.Value), 6);
                this.ProductGroupRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }

        }

        private void SetButtonPermission()
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDCashier, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                this.CashierButton.Visible = false;
                this.CashierAbuButton.Visible = true;
                this.CashierAbuButton.Enabled = false;
            }
            else
            {
                this.CashierButton.Visible = true;
                this.CashierAbuButton.Visible = false;
            }
        }

        private void ClearLabelAndData()
        {
            this.TransRefHiddenField.Value = "";
            this.TransRefTextBox.Text = "";
            this.ReferenceTextBox.Text = "";
            this.MemberNoTextBox.Text = "";
            this.MemberNameTextBox.Text = "";
            this.CustPhoneTextBox.Text = "";
            this.DiscountLabel.Text = "0.00";
            this.SubTotalLabel.Text = "0.00";
            this.ServiceChargeLabel.Text = "0.00";
            this.TaxLabel.Text = "0.00";
            this.TotalLabel.Text = "0.00";
            this.TaxHiddenField.Value = "0";
            this.PB1HiddenField.Value = "0";
            this.WarningLabel.Text = "";

            this.BoughtItemHiddenField.Value = "";
        }

        private void AddServiceCharge()
        {
            try
            {
                if (this.BoughtItemHiddenField.Value != "")
                {
                    String[] _boughtItemRows = this.BoughtItemHiddenField.Value.Split('^');
                    //int ctrRow = 0;
                    this.BoughtItemHiddenField.Value = "";
                    //_transNmbr = "";
                    foreach (String _boughtItemRow in _boughtItemRows)
                    {
                        String[] _boughtItemFields = _boughtItemRow.Split('|');

                        MsProduct _msProduct = this._productBL.GetSingleProduct(_boughtItemFields[0]);
                        if (_msProduct != null)
                        {
                            MsProductType _msProductType = this._productBL.GetSingleProductType(_msProduct.ProductType);
                            Boolean _fgServiceCalculate = (_msProductType.fgServiceChargesCalculate == null) ? false : Convert.ToBoolean(_msProductType.fgServiceChargesCalculate);
                            Boolean _fgTax = (_msProductType.fgTax == null) ? false : Convert.ToBoolean(_msProductType.fgTax);
                            Decimal _serviceCharge = 0;
                            Decimal _taxValue = 0;
                            Decimal _pb1Value = 0;

                            Decimal _lineTotal = (Convert.ToDecimal(_boughtItemFields[1]) * Convert.ToDecimal(_boughtItemFields[3])) - Convert.ToDecimal(_boughtItemFields[4]);

                            Decimal _service = (_msProductType.ServiceCharges == null) ? 0 : Convert.ToDecimal(_msProductType.ServiceCharges);
                            _serviceCharge += _service / 100 * _lineTotal;
                            if (_msProductType.TaxTypeCode == "1")
                            {
                                if (_fgTax == false)
                                {
                                    _taxValue += 0;
                                }
                                else
                                {
                                    Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                                    if (_fgServiceCalculate == false)
                                    {
                                        _taxValue += _tax / 100 * _lineTotal;
                                    }
                                    else
                                    {
                                        _taxValue += _tax / 100 * (_lineTotal + _serviceCharge);
                                    }
                                }
                            }
                            else if (_msProductType.TaxTypeCode == "2")
                            {
                                Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                                _pb1Value = (_tax / 100 * Convert.ToDecimal(_boughtItemFields[3])) * Convert.ToDecimal(_boughtItemFields[1]);
                                //_taxValue += _pb1Value;
                            }

                            if (this.BoughtItemHiddenField.Value == "")
                            {
                                this.BoughtItemHiddenField.Value = _boughtItemFields[0] + "|" + _boughtItemFields[1] + "|";
                                this.BoughtItemHiddenField.Value += _boughtItemFields[2] + "|" + Convert.ToDecimal(_boughtItemFields[3]).ToString("#,##0.00") + "|";
                                this.BoughtItemHiddenField.Value += Convert.ToDecimal(_boughtItemFields[4]).ToString("#,##0.00") + "|" + ((Convert.ToDecimal(_boughtItemFields[1]) * Convert.ToDecimal(_boughtItemFields[3])) - Convert.ToDecimal(_boughtItemFields[4])).ToString();
                                this.BoughtItemHiddenField.Value += "|" + _boughtItemFields[6];
                                this.BoughtItemHiddenField.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString() + "|" + _pb1Value.ToString();
                            }
                            else
                            {
                                this.BoughtItemHiddenField.Value += "^" + _boughtItemFields[0] + "|" + _boughtItemFields[1] + "|";
                                this.BoughtItemHiddenField.Value += _boughtItemFields[2] + "|" + Convert.ToDecimal(_boughtItemFields[3]).ToString("#,##0.00") + "|";
                                this.BoughtItemHiddenField.Value += Convert.ToDecimal(_boughtItemFields[4]).ToString("#,##0.00") + "|" + ((Convert.ToDecimal(_boughtItemFields[1]) * Convert.ToDecimal(_boughtItemFields[3])) - Convert.ToDecimal(_boughtItemFields[4])).ToString();
                                this.BoughtItemHiddenField.Value += "|" + _boughtItemFields[6];
                                this.BoughtItemHiddenField.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString() + "|" + _pb1Value.ToString();
                            }
                        }
                        //else
                        //{
                        //    POSTrInternetHd _internetHd = this._posInternetBL.GetSinglePOSTrInternetHd(this.TransRefTextBox.Text);

                        //    this.ReferenceTextBox.Text = _internetHd.ReferenceNo;
                        //    this.MemberNoTextBox.Text = _internetHd.MemberID;

                        //    if (_internetHd.MemberID.ToString().Trim() != "")
                        //    {
                        //        this.MemberNameTextBox.Attributes.Add("ReadOnly", "True");
                        //        this.MemberNameTextBox.Attributes.Add("style", "color: #808080");
                        //        this.CustPhoneTextBox.Attributes.Add("ReadOnly", "True");
                        //        this.CustPhoneTextBox.Attributes.Add("style", "color: #808080");
                        //    }

                        //    this.MemberNameTextBox.Text = ((_internetHd.MemberID == "") ? "" : this._memberBL.GetSingleByBarcode(_internetHd.MemberID).MemberName);
                        //    this.CustPhoneTextBox.Text = _internetHd.CustPhone;

                        //    List<POSTrInternetDt> _listInternetDt = this._posInternetBL.GetListInternetDtByTransNmbr(this.TransRefTextBox.Text);
                        //    //this.BoughtItemHiddenField.Value = "";
                        //    foreach (var _row in _listInternetDt)
                        //    {
                        //        MsProduct _msProducts = this._productBL.GetSingleProduct(_row.ProductCode);
                        //        MsProductType _msProductType = this._productBL.GetSingleProductType(_msProducts.ProductType);
                        //        Boolean _fgServiceCalculate = (_msProductType.fgServiceChargesCalculate == null) ? false : Convert.ToBoolean(_msProductType.fgServiceChargesCalculate);
                        //        Boolean _fgTax = (_msProductType.fgTax == null) ? false : Convert.ToBoolean(_msProductType.fgTax);
                        //        Decimal _serviceCharge = 0;
                        //        Decimal _taxValue = 0;

                        //        Decimal _lineTotal = (Convert.ToDecimal(_row.Qty) * Convert.ToDecimal(_row.AmountForex)) - Convert.ToDecimal(_row.DiscForex);

                        //        Decimal _service = (_msProductType.ServiceCharges == null) ? 0 : Convert.ToDecimal(_msProductType.ServiceCharges);
                        //        _serviceCharge += _service / 100 * _lineTotal;
                        //        if (_fgTax == false)
                        //        {
                        //            _taxValue += 0;
                        //        }
                        //        else
                        //        {
                        //            Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                        //            if (_fgServiceCalculate == false)
                        //            {
                        //                _taxValue += _tax / 100 * _lineTotal;
                        //            }
                        //            else
                        //            {
                        //                _taxValue += _tax / 100 * (_lineTotal + _serviceCharge);
                        //            }
                        //        }

                        //        if (this.BoughtItemHiddenField.Value == "")
                        //        {
                        //            this.BoughtItemHiddenField.Value = _row.ProductCode + "|" + _row.Qty + "|";
                        //            this.BoughtItemHiddenField.Value += _row.Remark + "|" + Convert.ToDecimal(_row.AmountForex).ToString("#,##0.00") + "|";
                        //            this.BoughtItemHiddenField.Value += Convert.ToDecimal(_row.DiscForex).ToString("#,##0.00") + "|" + ((_row.Qty * _row.AmountForex) - _row.DiscForex).ToString();
                        //            this.BoughtItemHiddenField.Value += "|" + _row.Remark;
                        //            this.BoughtItemHiddenField.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString();
                        //        }
                        //        else
                        //        {
                        //            this.BoughtItemHiddenField.Value += "^" + _row.ProductCode + "|" + _row.Qty + "|";
                        //            this.BoughtItemHiddenField.Value += _row.Remark + "|" + Convert.ToDecimal(_row.AmountForex).ToString("#,##0.00") + "|";
                        //            this.BoughtItemHiddenField.Value += Convert.ToDecimal(_row.DiscForex).ToString("#,##0.00") + "|" + ((_row.Qty * _row.AmountForex) - _row.DiscForex).ToString();
                        //            this.BoughtItemHiddenField.Value += "|" + _row.Remark;
                        //            this.BoughtItemHiddenField.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString();
                        //        }
                        //    }

                        //    //this.ComposeBoughtItem();

                        //    //if (this.BoughtItemHiddenField.Value == "")
                        //    //{
                        //    //    this.BoughtItemHiddenField.Value = _boughtItemFields[0] + "|" + _boughtItemFields[1] + "|";
                        //    //    this.BoughtItemHiddenField.Value += _boughtItemFields[2] + "|" + Convert.ToDecimal(_boughtItemFields[3]).ToString("#,##0.00") + "|";
                        //    //    this.BoughtItemHiddenField.Value += Convert.ToDecimal(_boughtItemFields[4]).ToString("#,##0.00") + "|" + ((Convert.ToDecimal(_boughtItemFields[1]) * Convert.ToDecimal(_boughtItemFields[3])) - Convert.ToDecimal(_boughtItemFields[4])).ToString();
                        //    //    this.BoughtItemHiddenField.Value += "|" + _boughtItemFields[6];
                        //    //    this.BoughtItemHiddenField.Value += "|" + _boughtItemFields[7] + "|" + _boughtItemFields[8];
                        //    //}
                        //    //else
                        //    //{
                        //    //    this.BoughtItemHiddenField.Value += "^" + _boughtItemFields[0] + "|" + _boughtItemFields[1] + "|";
                        //    //    this.BoughtItemHiddenField.Value += _boughtItemFields[2] + "|" + Convert.ToDecimal(_boughtItemFields[3]).ToString("#,##0.00") + "|";
                        //    //    this.BoughtItemHiddenField.Value += Convert.ToDecimal(_boughtItemFields[4]).ToString("#,##0.00") + "|" + ((Convert.ToDecimal(_boughtItemFields[1]) * Convert.ToDecimal(_boughtItemFields[3])) - Convert.ToDecimal(_boughtItemFields[4])).ToString();
                        //    //    this.BoughtItemHiddenField.Value += "|" + _boughtItemFields[6];
                        //    //    this.BoughtItemHiddenField.Value += "|" + _boughtItemFields[7] + "|" + _boughtItemFields[8];
                        //    //}
                        //}

                        if (this.TransRefTextBox.Text != "" && this.IsEditedHiddenField.Value == "0" && this.TransRefHiddenField.Value == "")///untuk join job order setelah insert data
                        {
                            POSTrInternetHd _internetHd = this._posInternetBL.GetSinglePOSTrInternetHd(this.TransRefTextBox.Text);

                            this.ReferenceTextBox.Text = _internetHd.ReferenceNo;
                            this.MemberNoTextBox.Text = _internetHd.MemberID;

                            if (_internetHd.MemberID.ToString().Trim() != "")
                            {
                                this.MemberNameTextBox.Attributes.Add("ReadOnly", "True");
                                this.MemberNameTextBox.Attributes.Add("style", "color: #808080");
                                this.CustPhoneTextBox.Attributes.Add("ReadOnly", "True");
                                this.CustPhoneTextBox.Attributes.Add("style", "color: #808080");
                            }

                            this.MemberNameTextBox.Text = ((_internetHd.MemberID == "") ? "" : this._memberBL.GetSingleByBarcode(_internetHd.MemberID).MemberName);
                            this.CustPhoneTextBox.Text = _internetHd.CustPhone;

                            List<POSTrInternetDt> _listInternetDt = this._posInternetBL.GetListInternetDtByTransNmbr(this.TransRefTextBox.Text);
                            //this.BoughtItemHiddenField.Value = "";
                            foreach (var _row in _listInternetDt)
                            {
                                MsProduct _msProducts = this._productBL.GetSingleProduct(_row.ProductCode);
                                MsProductType _msProductType = this._productBL.GetSingleProductType(_msProducts.ProductType);
                                Boolean _fgServiceCalculate = (_msProductType.fgServiceChargesCalculate == null) ? false : Convert.ToBoolean(_msProductType.fgServiceChargesCalculate);
                                Boolean _fgTax = (_msProductType.fgTax == null) ? false : Convert.ToBoolean(_msProductType.fgTax);
                                Decimal _serviceCharge = 0;
                                Decimal _taxValue = 0;
                                Decimal _pb1Value = 0;

                                Decimal _lineTotal = (Convert.ToDecimal(_row.Qty) * Convert.ToDecimal(_row.AmountForex)) - Convert.ToDecimal(_row.DiscForex);

                                Decimal _service = (_msProductType.ServiceCharges == null) ? 0 : Convert.ToDecimal(_msProductType.ServiceCharges);
                                _serviceCharge += _service / 100 * _lineTotal;
                                if (_msProductType.TaxTypeCode == "1")
                                {
                                    if (_fgTax == false)
                                    {
                                        _taxValue += 0;
                                    }
                                    else
                                    {
                                        Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                                        if (_fgServiceCalculate == false)
                                        {
                                            _taxValue += _tax / 100 * _lineTotal;
                                        }
                                        else
                                        {
                                            _taxValue += _tax / 100 * (_lineTotal + _serviceCharge);
                                        }
                                    }
                                }
                                else if (_msProductType.TaxTypeCode == "2")
                                {
                                    Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                                    _pb1Value = (_tax / 100 * Convert.ToDecimal(_boughtItemFields[3])) * Convert.ToDecimal(_row.Qty);
                                    //_taxValue += _pb1Value;
                                }

                                if (this.BoughtItemHiddenField.Value == "")
                                {
                                    this.BoughtItemHiddenField.Value = _row.ProductCode + "|" + _row.Qty + "|";
                                    this.BoughtItemHiddenField.Value += _row.Remark + "|" + Convert.ToDecimal(_row.AmountForex).ToString("#,##0.00") + "|";
                                    this.BoughtItemHiddenField.Value += Convert.ToDecimal(_row.DiscForex).ToString("#,##0.00") + "|" + ((_row.Qty * _row.AmountForex) - _row.DiscForex).ToString();
                                    this.BoughtItemHiddenField.Value += "|" + _row.Remark;
                                    this.BoughtItemHiddenField.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString() + "|" + _pb1Value.ToString();
                                }
                                else
                                {
                                    this.BoughtItemHiddenField.Value += "^" + _row.ProductCode + "|" + _row.Qty + "|";
                                    this.BoughtItemHiddenField.Value += _row.Remark + "|" + Convert.ToDecimal(_row.AmountForex).ToString("#,##0.00") + "|";
                                    this.BoughtItemHiddenField.Value += Convert.ToDecimal(_row.DiscForex).ToString("#,##0.00") + "|" + ((_row.Qty * _row.AmountForex) - _row.DiscForex).ToString();
                                    this.BoughtItemHiddenField.Value += "|" + _row.Remark;
                                    this.BoughtItemHiddenField.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString() + "|" + _pb1Value.ToString();
                                }
                            }
                            this.TransRefHiddenField.Value = this.TransRefTextBox.Text;
                            //this.ComposeBoughtItem();
                        }

                        this.ComposeBoughtItem();
                    }
                }
                else if (this.TransRefTextBox.Text != "" && this.IsEditedHiddenField.Value == "0")//// join job order sebelum ada data
                {
                    POSTrInternetHd _internetHd = this._posInternetBL.GetSinglePOSTrInternetHd(this.TransRefTextBox.Text);

                    this.ReferenceTextBox.Text = _internetHd.ReferenceNo;
                    this.MemberNoTextBox.Text = _internetHd.MemberID;

                    if (_internetHd.MemberID.ToString().Trim() != "")
                    {
                        this.MemberNameTextBox.Attributes.Add("ReadOnly", "True");
                        this.MemberNameTextBox.Attributes.Add("style", "color: #808080");
                        this.CustPhoneTextBox.Attributes.Add("ReadOnly", "True");
                        this.CustPhoneTextBox.Attributes.Add("style", "color: #808080");
                    }

                    this.MemberNameTextBox.Text = ((_internetHd.MemberID == "") ? "" : this._memberBL.GetSingleByBarcode(_internetHd.MemberID).MemberName);
                    this.CustPhoneTextBox.Text = _internetHd.CustPhone;

                    List<POSTrInternetDt> _listInternetDt = this._posInternetBL.GetListInternetDtByTransNmbr(this.TransRefTextBox.Text);
                    //this.BoughtItemHiddenField.Value = "";
                    foreach (var _row in _listInternetDt)
                    {
                        MsProduct _msProducts = this._productBL.GetSingleProduct(_row.ProductCode);
                        MsProductType _msProductType = this._productBL.GetSingleProductType(_msProducts.ProductType);
                        Boolean _fgServiceCalculate = (_msProductType.fgServiceChargesCalculate == null) ? false : Convert.ToBoolean(_msProductType.fgServiceChargesCalculate);
                        Boolean _fgTax = (_msProductType.fgTax == null) ? false : Convert.ToBoolean(_msProductType.fgTax);
                        Decimal _serviceCharge = 0;
                        Decimal _taxValue = 0;
                        Decimal _pb1Value = 0;

                        Decimal _lineTotal = (Convert.ToDecimal(_row.Qty) * Convert.ToDecimal(_row.AmountForex)) - Convert.ToDecimal(_row.DiscForex);

                        Decimal _service = (_msProductType.ServiceCharges == null) ? 0 : Convert.ToDecimal(_msProductType.ServiceCharges);
                        _serviceCharge += _service / 100 * _lineTotal;
                        if (_msProductType.TaxTypeCode == "1")
                        {
                            if (_fgTax == false)
                            {
                                _taxValue += 0;
                            }
                            else
                            {
                                Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                                if (_fgServiceCalculate == false)
                                {
                                    _taxValue += _tax / 100 * _lineTotal;
                                }
                                else
                                {
                                    _taxValue += _tax / 100 * (_lineTotal + _serviceCharge);
                                }
                            }

                        }
                        else if (_msProductType.TaxTypeCode == "2")
                        {
                            Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                            _pb1Value = (_tax / 100 * Convert.ToDecimal(_row.AmountForex)) * Convert.ToDecimal(_row.Qty);
                            //_taxValue += _pb1Value;
                        }

                        if (this.BoughtItemHiddenField.Value == "")
                        {
                            this.BoughtItemHiddenField.Value = _row.ProductCode + "|" + _row.Qty + "|";
                            this.BoughtItemHiddenField.Value += _row.Remark + "|" + Convert.ToDecimal(_row.AmountForex).ToString("#,##0.00") + "|";
                            this.BoughtItemHiddenField.Value += Convert.ToDecimal(_row.DiscForex).ToString("#,##0.00") + "|" + ((_row.Qty * _row.AmountForex) - _row.DiscForex).ToString();
                            this.BoughtItemHiddenField.Value += "|" + _row.Remark;
                            this.BoughtItemHiddenField.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString() + "|" + _pb1Value.ToString();
                        }
                        else
                        {
                            this.BoughtItemHiddenField.Value += "^" + _row.ProductCode + "|" + _row.Qty + "|";
                            this.BoughtItemHiddenField.Value += _row.Remark + "|" + Convert.ToDecimal(_row.AmountForex).ToString("#,##0.00") + "|";
                            this.BoughtItemHiddenField.Value += Convert.ToDecimal(_row.DiscForex).ToString("#,##0.00") + "|" + ((_row.Qty * _row.AmountForex) - _row.DiscForex).ToString();
                            this.BoughtItemHiddenField.Value += "|" + _row.Remark;
                            this.BoughtItemHiddenField.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString() + "|" + _pb1Value.ToString();
                        }
                    }
                    this.TransRefHiddenField.Value = this.TransRefTextBox.Text;
                    this.ComposeBoughtItem();
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void LoadHoldedData()
        {
            try
            {
                POSTrInternetHd _internetHd = this._posInternetBL.GetSinglePOSTrInternetHd(this.TransRefTextBox.Text);
                if (_internetHd != null)
                {
                    if (this.MemberNameTextBox.Text == "")
                    {
                        this.ReferenceTextBox.Text = _internetHd.ReferenceNo;
                        this.MemberNoTextBox.Text = _internetHd.MemberID;
                        this.MemberNameTextBox.Text = ((_internetHd.MemberID == "") ? "" : this._memberBL.GetSingleByBarcode(_internetHd.MemberID).MemberName);
                        if (this.MemberNameTextBox.Text == "" && _internetHd.CustName != "")
                        {
                            this.MemberNameTextBox.Text = _internetHd.CustName;
                        }
                        this.CustPhoneTextBox.Text = _internetHd.CustPhone;
                    }
                    if (_internetHd.MemberID.ToString().Trim() != "")
                    {
                        this.MemberNameTextBox.Attributes.Add("ReadOnly", "True");
                        this.MemberNameTextBox.Attributes.Add("style", "color: #808080");
                        this.CustPhoneTextBox.Attributes.Add("ReadOnly", "True");
                        this.CustPhoneTextBox.Attributes.Add("style", "color: #808080");
                    }
                }

                List<POSTrInternetDt> _listInternetDt = this._posInternetBL.GetListInternetDtByTransNmbr(this.TransRefTextBox.Text);
                this.BoughtItemHiddenField.Value = "";
                foreach (var _row in _listInternetDt)
                {
                    MsProduct _msProduct = this._productBL.GetSingleProduct(_row.ProductCode);
                    MsProductType _msProductType = this._productBL.GetSingleProductType(_msProduct.ProductType);
                    Boolean _fgServiceCalculate = (_msProductType.fgServiceChargesCalculate == null) ? false : Convert.ToBoolean(_msProductType.fgServiceChargesCalculate);
                    Boolean _fgTax = (_msProductType.fgTax == null) ? false : Convert.ToBoolean(_msProductType.fgTax);
                    Decimal _serviceCharge = 0;
                    Decimal _taxValue = 0;
                    Decimal _pb1Value = 0;
                    Decimal _lineTotal = (Convert.ToDecimal(_row.Qty) * Convert.ToDecimal(_row.AmountForex)) - Convert.ToDecimal(_row.DiscForex);

                    Decimal _service = (_msProductType.ServiceCharges == null) ? 0 : Convert.ToDecimal(_msProductType.ServiceCharges);
                    _serviceCharge += _service / 100 * _lineTotal;
                    //if (_fgTax == false)
                    //{
                    //    _taxValue += 0;
                    //}
                    //else
                    //{
                    //    Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                    //    if (_fgServiceCalculate == false)
                    //    {
                    //        _taxValue += _tax / 100 * _lineTotal;
                    //    }
                    //    else
                    //    {
                    //        _taxValue += _tax / 100 * (_lineTotal + _serviceCharge);
                    //    }
                    //}
                    if (_msProductType.TaxTypeCode == "1")
                    {
                        if (_fgTax == false)
                        {
                            _taxValue += 0;
                        }
                        else
                        {
                            Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                            if (_fgServiceCalculate == false)
                            {
                                _taxValue += _tax / 100 * _lineTotal;
                            }
                            else
                            {
                                _taxValue += _tax / 100 * (_lineTotal + _serviceCharge);
                            }
                        }
                    }
                    else if (_msProductType.TaxTypeCode == "2")
                    {
                        Decimal _msProductPrice = this._productBL.GetProductSalesPriceForPOS(_row.ProductCode, _internetHd.CurrCode, _msProduct.Unit);
                        Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                        _pb1Value = (_tax / 100 * Convert.ToDecimal(_msProductPrice)) * Convert.ToDecimal(_row.Qty);
                    }
                    //if (this.BoughtItemHiddenField.Value == "")
                    //{
                    //    this.BoughtItemHiddenField.Value = _row.ItemNo + "|" + _row.Qty + "|";
                    //    this.BoughtItemHiddenField.Value += _row.Remark + "|" + Convert.ToDecimal(_row.AmountForex).ToString("#,##0.00") + "|";
                    //    this.BoughtItemHiddenField.Value += Convert.ToDecimal(_row.DiscForex).ToString("#,##0.00") + "|" + ((_row.Qty * _row.AmountForex) - _row.DiscForex).ToString();
                    //    this.BoughtItemHiddenField.Value += "|" + _row.Remark;
                    //    this.BoughtItemHiddenField.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString() + "|" + _pb1Value.ToString();
                    //}
                    //else
                    //{
                    //    this.BoughtItemHiddenField.Value += "^" + _row.ItemNo + "|" + _row.Qty + "|";
                    //    this.BoughtItemHiddenField.Value += _row.Remark + "|" + Convert.ToDecimal(_row.AmountForex).ToString("#,##0.00") + "|";
                    //    this.BoughtItemHiddenField.Value += Convert.ToDecimal(_row.DiscForex).ToString("#,##0.00") + "|" + ((_row.Qty * _row.AmountForex) - _row.DiscForex).ToString();
                    //    this.BoughtItemHiddenField.Value += "|" + _row.Remark;
                    //    this.BoughtItemHiddenField.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString() + "|" + _pb1Value.ToString();
                    //}
                    if (this.BoughtItemHiddenField.Value == "")
                    {
                        this.BoughtItemHiddenField.Value = _row.ProductCode + "|" + _row.Qty + "|";
                        this.BoughtItemHiddenField.Value += _row.Remark + "|" + Convert.ToDecimal(_row.AmountForex).ToString("#,##0.00") + "|";
                        this.BoughtItemHiddenField.Value += Convert.ToDecimal(_row.DiscForex).ToString("#,##0.00") + "|" + ((_row.Qty * _row.AmountForex) - _row.DiscForex).ToString();
                        this.BoughtItemHiddenField.Value += "|" + _row.Remark;
                        this.BoughtItemHiddenField.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString() + "|" + _pb1Value.ToString();
                    }
                    else
                    {
                        this.BoughtItemHiddenField.Value += "^" + _row.ProductCode + "|" + _row.Qty + "|";
                        this.BoughtItemHiddenField.Value += _row.Remark + "|" + Convert.ToDecimal(_row.AmountForex).ToString("#,##0.00") + "|";
                        this.BoughtItemHiddenField.Value += Convert.ToDecimal(_row.DiscForex).ToString("#,##0.00") + "|" + ((_row.Qty * _row.AmountForex) - _row.DiscForex).ToString();
                        this.BoughtItemHiddenField.Value += "|" + _row.Remark;
                        this.BoughtItemHiddenField.Value += "|" + _serviceCharge.ToString() + "|" + _taxValue.ToString() + "|" + _pb1Value.ToString();
                    }
                }

                this.ComposeBoughtItem();
                this.IsEditedHiddenField.Value = "1";
                this.JoinJobOrderImageButton.Enabled = false;
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ComposeBoughtItem()
        {
            try
            {
                String _composedTableContent = "";
                Decimal _discount = 0;
                Decimal _subTotal = 0;
                Decimal _serviceCharge = 0;
                Decimal _taxCharge = 0;
                Decimal _pb1Charge = 0;
                if (this.BoughtItemHiddenField.Value != "")
                {
                    String[] _boughtItemRows = this.BoughtItemHiddenField.Value.Split('^');
                    int ctrRow = 0;
                    foreach (String _boughtItemRow in _boughtItemRows)
                    {
                        String[] _boughtItemFields = _boughtItemRow.Split('|');
                        if (ctrRow.ToString() == this.EditRowHiddenField.Value)
                            _composedTableContent += "<tr><td>";
                        else
                            _composedTableContent += "<tr><td id='qty'>";
                        _composedTableContent += _boughtItemFields[1] + "</td><td id='product'>";
                        _composedTableContent += _boughtItemFields[2] + "</td><td id='price'>";
                        _composedTableContent += (Convert.ToDecimal(_boughtItemFields[3])).ToString("#,##0.00") + "</td><td id='disc'>";
                        _composedTableContent += _boughtItemFields[4] + "</td><td id='total'>";
                        _composedTableContent += (Convert.ToDecimal(_boughtItemFields[5])).ToString("#,##0.00") + "</td><td id='edit'>";
                        _composedTableContent += "<span onClick=\"editBoughtItem(" + ctrRow.ToString() + ");\"></span></td><td id='cancel'>";
                        _composedTableContent += "<span onClick=\"deleteBoughtItem(" + ctrRow.ToString() + ");\"></span>";
                        _composedTableContent += "</td></tr>";
                        ctrRow++;

                        _discount += Convert.ToDecimal(_boughtItemFields[4]);
                        _subTotal += Convert.ToDecimal(_boughtItemFields[5]);
                        //_serviceCharge += Convert.ToDecimal(_boughtItemFields[7]);
                        //_taxCharge += Convert.ToDecimal(_boughtItemFields[8]);
                        //_pb1Charge += Convert.ToDecimal(_boughtItemFields[9]);

                        MsProduct _msProduct = _posInternetBL.GetSingleProduct(_boughtItemFields[0]);
                        this.ProductHiddenField.Value = _msProduct.ProductCode;
                        Decimal _msProductPrice = this._productBL.GetProductSalesPriceForPOS(_msProduct.ProductCode, "IDR", _msProduct.Unit);

                        int _qty = 1;
                        if (this.qtyProductTextBox.Text != "")
                            if (Convert.ToInt32(this.qtyProductTextBox.Text) > 0)
                                _qty = Convert.ToInt32(this.qtyProductTextBox.Text);

                        MsProductType _msProductType = this._productBL.GetSingleProductType(_msProduct.ProductType);
                        Boolean _fgServiceCalculate = (_msProductType.fgServiceChargesCalculate == null) ? false : Convert.ToBoolean(_msProductType.fgServiceChargesCalculate);
                        Boolean _fgTax = (_msProductType.fgTax == null) ? false : Convert.ToBoolean(_msProductType.fgTax);

                        Decimal _service = (_msProductType.ServiceCharges == null) ? 0 : Convert.ToDecimal(_msProductType.ServiceCharges);
                        _serviceCharge += _service / 100 * Convert.ToDecimal(_boughtItemFields[5]);
                        if (_msProductType.TaxTypeCode == "1")
                        {
                            if (_fgTax == false)
                            {
                                _taxCharge += 0;
                            }
                            else
                            {
                                Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                                if (_fgServiceCalculate == false)
                                {
                                    _taxCharge += _tax / 100 * Convert.ToDecimal(_boughtItemFields[5]);
                                }
                                else
                                {
                                    _taxCharge += _tax / 100 * (Convert.ToDecimal(_boughtItemFields[5]) + _serviceCharge);
                                }
                            }
                        }
                        else if (_msProductType.TaxTypeCode == "2")
                        {
                            Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                            _pb1Charge = (_tax / 100 * _msProductPrice) * Convert.ToDecimal(_boughtItemFields[1]);
                        }
                    }
                }
                this.BoughtItemLiteral.Text = _composedTableContent;
                this.DiscountLabel.Text = _discount.ToString("#,##0.00");
                //int _service = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSServiceCharge).SetValue);
                //_serviceCharge = _service * _subTotal / 100;
                this.ServiceChargeLabel.Text = _serviceCharge.ToString("#,##0.00");
                //Decimal _tax = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSTaxCharge).SetValue);
                //_taxCharge = _tax * _subTotal / 100;
                this.TaxHiddenField.Value = _taxCharge.ToString("#,##0.00");
                this.PB1HiddenField.Value = _pb1Charge.ToString("#,##0.00");
                this.TaxLabel.Text = (_taxCharge + _pb1Charge).ToString("#,##0.00");
                this.SubTotalLabel.Text = _subTotal.ToString("#,##0.00");

                Decimal _total = _subTotal + _serviceCharge + _taxCharge + _pb1Charge;
                this.TotalLabel.Text = _total.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ProductGroupRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            MsProductGroup _temp = (MsProductGroup)e.Item.DataItem;
            Button _productGroupButton = (Button)e.Item.FindControl("ProductGroupButton");
            _productGroupButton.Text = _temp.ProductGrpName.Replace(' ', '\n');
            _productGroupButton.ToolTip = _temp.ProductGrpCode;
        }

        protected void ProductSubGroupRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            MsProductSubGroup _temp = (MsProductSubGroup)e.Item.DataItem;
            Button _productSubGroupButton = (Button)e.Item.FindControl("ProductSubGroupButton");
            _productSubGroupButton.Text = _temp.ProductSubGrpName.Replace(' ', '\n');
            _productSubGroupButton.ToolTip = _temp.ProductSubGrpCode;

        }

        protected void ProductRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            MsProduct _temp = (MsProduct)e.Item.DataItem;
            Button _productButton = (Button)e.Item.FindControl("ProductButton");
            _productButton.Text = _temp.ProductName.Replace(' ', '\n');
            _productButton.ToolTip = _temp.ProductCode;
        }

        protected void ProductGroupButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button _temp = (Button)sender;

                this.ProductSubGroupPageHiddenField.Value = "0";
                this.ProductGroupHiddenField.Value = _temp.ToolTip;
                this.ProductSubGroupRepeater.DataSource = _posInternetBL.GetListProductSubGroup(this.ProductGroupHiddenField.Value, Convert.ToInt16(this.ProductSubGroupPageHiddenField.Value), 2);
                this.ProductSubGroupRepeater.DataBind();
                this.ProductRepeater.DataSource = null;
                this.ProductRepeater.DataBind();

                this.PrevProductSubGroupButton.Enabled = true;
                this.NextProductSubGroupButton.Enabled = true;
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ProductSubGroupButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button _temp = (Button)sender;

                this.ProductPageHiddenField.Value = "0";
                this.ProductSubGroupHiddenField.Value = _temp.ToolTip;
                this.ProductRepeater.DataSource = _posInternetBL.GetListProduct(this.ProductSubGroupHiddenField.Value, Convert.ToInt16(this.ProductPageHiddenField.Value), 28);
                this.ProductRepeater.DataBind();

                this.ProductPrevButton.Enabled = true;
                this.ProductNextButton.Enabled = true;
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ProductButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button _temp = (Button)sender;
                MsProduct _msProduct = _posInternetBL.GetSingleProduct(_temp.ToolTip);
                this.ProductHiddenField.Value = _msProduct.ProductCode;
                Decimal _msProductPrice = this._productBL.GetProductSalesPriceForPOS(_msProduct.ProductCode, "IDR", _msProduct.Unit);
                Decimal _discTotal = 0;
                Decimal _serviceCharge = 0;
                Decimal _taxValue = 0;
                Decimal _pb1Value = 0;

                int _qty = 1;
                if (this.qtyProductTextBox.Text != "")
                    if (Convert.ToInt32(this.qtyProductTextBox.Text) > 0)
                        _qty = Convert.ToInt32(this.qtyProductTextBox.Text);

                //16/11/11 boby move to settlement
                //POSMsDiscountConfig _posMsDiscount = this._posDiscountBL.GetDiscountByProductCode(_msProduct.ProductCode);
                //if (_posMsDiscount != null)
                //{
                //    if (_posMsDiscount.AmountType == POSDiscountDataMapper.GetAmountType(POSDiscountAmountType.Amount))
                //    {
                //        _discTotal = (_posMsDiscount.DiscAmount == null) ? 0 : Convert.ToDecimal(_posMsDiscount.DiscAmount);
                //        _discTotal = _discTotal * _qty;
                //    }
                //    else
                //    {
                //        _discTotal = ((_posMsDiscount.DiscAmount == null) ? 0 : Convert.ToDecimal(_posMsDiscount.DiscAmount)) / 100 * _msProductPrice;
                //        _discTotal = _discTotal * _qty;
                //    }
                //}

                Decimal _lineTotal = ((_qty * _msProductPrice) - _discTotal);

                MsProductType _msProductType = this._productBL.GetSingleProductType(_msProduct.ProductType);
                Boolean _fgServiceCalculate = (_msProductType.fgServiceChargesCalculate == null) ? false : Convert.ToBoolean(_msProductType.fgServiceChargesCalculate);
                Boolean _fgTax = (_msProductType.fgTax == null) ? false : Convert.ToBoolean(_msProductType.fgTax);

                Decimal _service = (_msProductType.ServiceCharges == null) ? 0 : Convert.ToDecimal(_msProductType.ServiceCharges);
                _serviceCharge += _service / 100 * _lineTotal;
                if (_msProductType.TaxTypeCode == "1")
                {
                    if (_fgTax == false)
                    {
                        _taxValue += 0;
                    }
                    else
                    {
                        Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                        if (_fgServiceCalculate == false)
                        {
                            _taxValue += _tax / 100 * _lineTotal;
                        }
                        else
                        {
                            _taxValue += _tax / 100 * (_lineTotal + _serviceCharge);
                        }
                    }
                }
                else if (_msProductType.TaxTypeCode == "2")
                {
                    Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                    _pb1Value = (_tax / 100 * Convert.ToDecimal(_msProductPrice)) * Convert.ToDecimal(_qty);
                    //_taxValue += _pb1Value;
                }

                if (this.BoughtItemHiddenField.Value != "")
                    this.BoughtItemHiddenField.Value += "^";

                this.BoughtItemHiddenField.Value += this.ProductHiddenField.Value + "|" +
                    _qty.ToString() + "|" + _msProduct.ProductName + "|" +
                    _msProductPrice.ToString("#,##0.00") + "|" + _discTotal.ToString("#,##0.00") + "|" +
                    _lineTotal.ToString("#,##0.00") + "|";
                this.BoughtItemHiddenField.Value += "|" + _serviceCharge.ToString("#,##0.00") + "|" + _taxValue.ToString("#,##0.00") + "|" + _pb1Value.ToString("#,##0.00");
                // prodcode,qty,prodname,sellprice,disc,total,description
                this.ProductHiddenField.Value = "";
                this.qtyProductTextBox.Text = "0";

                this.ComposeBoughtItem();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void LayoutMapImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
            Response.Redirect("POSInternet.aspx" + "?" + this._selectedFloorKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._selectedFloorKey)));
        }

        protected void btnOKPanelNumber_Click(object sender, EventArgs e)
        {
        }

        protected void PrevProductGroupButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int _currPage = Convert.ToInt16(this.ProductGroupPageHiddenField.Value);
                if (_currPage > 0) _currPage--;
                this.ProductGroupPageHiddenField.Value = _currPage.ToString();

                this.ProductGroupRepeater.DataSource = _posInternetBL.GetListProductGroup(Convert.ToInt16(this.ProductGroupPageHiddenField.Value), 6);
                this.ProductGroupRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void NextProductGroupButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int _currPage = Convert.ToInt16(this.ProductGroupPageHiddenField.Value);
                if (_currPage < _posInternetBL.GetProductGroupMaxPage(6)) _currPage++;
                this.ProductGroupPageHiddenField.Value = _currPage.ToString();

                this.ProductGroupRepeater.DataSource = _posInternetBL.GetListProductGroup(Convert.ToInt16(this.ProductGroupPageHiddenField.Value), 6);
                this.ProductGroupRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void PrevProductSubGroupButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int _currPage = Convert.ToInt16(this.ProductSubGroupPageHiddenField.Value);
                if (_currPage > 0) _currPage--;
                this.ProductSubGroupPageHiddenField.Value = _currPage.ToString();

                this.ProductSubGroupRepeater.DataSource = _posInternetBL.GetListProductSubGroup(this.ProductGroupHiddenField.Value, Convert.ToInt16(this.ProductSubGroupPageHiddenField.Value), 2);
                this.ProductSubGroupRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void NextProductSubGroupButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int _currPage = Convert.ToInt16(this.ProductSubGroupPageHiddenField.Value);
                if (_currPage < _posInternetBL.GetProductSubGroupMaxPage(3, this.ProductGroupHiddenField.Value)) _currPage++;
                this.ProductSubGroupPageHiddenField.Value = _currPage.ToString();

                this.ProductSubGroupRepeater.DataSource = _posInternetBL.GetListProductSubGroup(this.ProductGroupHiddenField.Value, Convert.ToInt16(this.ProductSubGroupPageHiddenField.Value), 2);
                this.ProductSubGroupRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ProductNextButton_Click(object sender, EventArgs e)
        {
            try
            {
                int _currPage = Convert.ToInt16(this.ProductPageHiddenField.Value);
                if (_currPage > 0) _currPage--;
                this.ProductPageHiddenField.Value = _currPage.ToString();

                this.ProductRepeater.DataSource = _posInternetBL.GetListProduct(this.ProductSubGroupHiddenField.Value, Convert.ToInt16(this.ProductPageHiddenField.Value), 28);
                this.ProductRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void ProductPrevButton_Click(object sender, EventArgs e)
        {
            try
            {
                int _currPage = Convert.ToInt16(this.ProductPageHiddenField.Value);
                if (_currPage < _posInternetBL.GetProductMaxPage(40, this.ProductSubGroupHiddenField.Value)) _currPage++;
                this.ProductPageHiddenField.Value = _currPage.ToString();

                this.ProductRepeater.DataSource = _posInternetBL.GetListProduct(this.ProductSubGroupHiddenField.Value, Convert.ToInt16(this.ProductPageHiddenField.Value), 28);
                this.ProductRepeater.DataBind();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                String[] _boughtItems = this.BoughtItemHiddenField.Value.Split('^');
                String[] _editData = _boughtItems[Convert.ToInt16(this.EditRowHiddenField.Value)].Split('|');
                // prodcode,qty,prodname,sellprice,disc,total,description
                String _qty = "1";
                if (this.qtyProductTextBox.Text != "")
                    if (Convert.ToInt16(this.qtyProductTextBox.Text) > 0)
                        _qty = this.qtyProductTextBox.Text;
                _editData[1] = _qty;
                _editData[6] = this.NotesTextBox.Text;

                MsProduct _msProduct = this._productBL.GetSingleProduct(_editData[0]);
                MsProductType _msProductType = this._productBL.GetSingleProductType(_msProduct.ProductType);

                int _qtyNew = Convert.ToInt32(_qty);
                Decimal _discTotal = 0;
                //16/11/11 boby move to settlement
                //POSMsDiscountConfig _posMsDiscount = this._posDiscountBL.GetDiscountByProductCode(_msProduct.ProductCode);
                //if (_posMsDiscount != null)
                //{
                //    if (_posMsDiscount.AmountType == POSDiscountDataMapper.GetAmountType(POSDiscountAmountType.Amount))
                //    {
                //        _discTotal = (_posMsDiscount.DiscAmount == null) ? 0 : Convert.ToDecimal(_posMsDiscount.DiscAmount);
                //        _discTotal = _discTotal * _qtyNew;
                //    }
                //    else
                //    {
                //        _discTotal = ((_posMsDiscount.DiscAmount == null) ? 0 : Convert.ToDecimal(_posMsDiscount.DiscAmount)) / 100 * Convert.ToDecimal(_editData[3]);
                //        _discTotal = _discTotal * _qtyNew;
                //    }
                //}
                Decimal _lineTotal = (_qtyNew * Convert.ToDecimal(_editData[3])) - _discTotal;

                Boolean _fgServiceCalculate = (_msProductType.fgServiceChargesCalculate == null) ? false : Convert.ToBoolean(_msProductType.fgServiceChargesCalculate);
                Boolean _fgTax = (_msProductType.fgTax == null) ? false : Convert.ToBoolean(_msProductType.fgTax);
                Decimal _serviceCharge = 0;
                Decimal _taxValue = 0;
                Decimal _pb1Value = 0;

                Decimal _service = (_msProductType.ServiceCharges == null) ? 0 : Convert.ToDecimal(_msProductType.ServiceCharges);
                _serviceCharge += _service / 100 * _lineTotal;
                if (_msProductType.TaxTypeCode == "1")
                {
                    if (_fgTax == false)
                    {
                        _taxValue += 0;
                    }
                    else
                    {
                        Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                        if (_fgServiceCalculate == false)
                        {
                            _taxValue += _tax / 100 * _lineTotal;
                        }
                        else
                        {
                            _taxValue += _tax / 100 * (_lineTotal + _serviceCharge);
                        }
                    }
                }
                else if (_msProductType.TaxTypeCode == "2")
                {
                    Decimal _tax = (_msProductType.TaxValue == null) ? 0 : Convert.ToDecimal(_msProductType.TaxValue);
                    _pb1Value = (_tax / 100 * Convert.ToDecimal(_editData[3])) * Convert.ToDecimal(_qty);
                    //_taxValue += _pb1Value;
                }
                _editData[4] = _discTotal.ToString("#,##0.00");
                _editData[5] = _lineTotal.ToString("#,##0.00");
                //_editData[7] = _serviceCharge.ToString("#,##0.00");
                //_editData[8] = _taxValue.ToString("#,##0.00");
                //_editData[9] = _pb1Value.ToString("#,##0.00");

                String _tempBoughtItem = "";
                for (int i = 0; i < _boughtItems.Count(); i++)
                {
                    if (Convert.ToInt16(this.EditRowHiddenField.Value) == i)
                    {
                        _tempBoughtItem += "^" + _editData[0];
                        for (int j = 1; j < 10; j++)
                        {
                            if (j < 7)
                            {
                                _tempBoughtItem += "|" + _editData[j];
                            }
                            else
                            {
                                _tempBoughtItem += "|" + _serviceCharge.ToString("#,##0.00");
                                _tempBoughtItem += "|" + _taxValue.ToString("#,##0.00");
                                _tempBoughtItem += "|" + _pb1Value.ToString("#,##0.00");
                                j = 10;
                            }
                        }
                    }
                    else
                    {
                        _tempBoughtItem += "^" + _boughtItems[i];
                    }
                }
                this.BoughtItemHiddenField.Value = _tempBoughtItem.Substring(1);

                this.qtyProductTextBox.Text = "0";
                this.ProductRepeater.Visible = true;
                this.CancelAllImageButton.Enabled = true;
                this.SearchImageButton.Enabled = true;
                this.UpdateButton.Enabled = false;
                this.NotesTextBox.Text = "";
                this.NotesTextBox.Enabled = false;
                this.EditRowHiddenField.Value = "";
                this.EditPostBackHiddenField.Value = "-1";
                this.ComposeBoughtItem();
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void CancelAllImageButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.TransRefTextBox.Text != "")
                {
                    this.ChangeVisiblePanel(2);
                    //this.FormPanel.Visible = false;
                    //this.ReasonListPanel.Visible = true;
                    //this.ReasonListRepeater.DataSource = this._reasonBL.GetList();
                    //this.ReasonListRepeater.DataBind();
                }
                else
                {
                    this.ClearLabelAndData();
                    this.ComposeBoughtItem();
                    this.JoinJobOrderImageButton.Enabled = true;
                    this.IsEditedHiddenField.Value = "0";
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void MemberNoTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //this.MemberNameTextBox.Text = this._posInternetBL.GetMemberName(this.MemberNoTextBox.Text);
                //this.MemberNameTextBox.ReadOnly = this.MemberNameTextBox.Text != "";

                if (this.MemberNoTextBox.Text.Length == 13)
                {
                    MsMember _member = this._memberBL.GetSingleByBarcode(this.MemberNoTextBox.Text);

                    if (_member != null)
                    {
                        this.MemberNameTextBox.Text = _member.MemberName;
                        this.MemberNameTextBox.Attributes.Add("ReadOnly", "True");
                        this.MemberNameTextBox.Attributes.Add("style", "color: #808080");

                        if (_member.Telephone1 == "" || _member.Telephone1 == null)
                        {
                            this.CustPhoneTextBox.Text = _member.HandPhone1;
                            this.CustPhoneTextBox.Attributes.Add("ReadOnly", "True");
                            this.CustPhoneTextBox.Attributes.Add("style", "color: #808080");
                        }
                        else
                        {
                            this.CustPhoneTextBox.Text = _member.Telephone1;
                            this.CustPhoneTextBox.Attributes.Add("ReadOnly", "True");
                            this.CustPhoneTextBox.Attributes.Add("style", "color: #808080");
                        }
                    }
                }
                else
                {
                    this.MemberNoTextBox.Text = "";
                    this.MemberNameTextBox.Attributes.Remove("ReadOnly");
                    this.MemberNameTextBox.Attributes.Add("style", "color: #000000");
                    this.CustPhoneTextBox.Attributes.Remove("ReadOnly");
                    this.CustPhoneTextBox.Attributes.Add("style", "color: #000000");
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void SendToCashierButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ReferenceTextBox.Text != "")
                {
                    this.AddServiceCharge();
                    this.IsEditedHiddenField.Value = "0";
                    this.JoinJobOrderImageButton.Enabled = true;
                    if (this.BoughtItemHiddenField.Value != "")
                    {
                        if (this.TransRefTextBox.Text != "")
                        {
                            POSTrInternetHd _posTrInternetHd = this._posInternetBL.GetSinglePOSTrInternetHd(this.TransRefTextBox.Text);
                            if (_posTrInternetHd == null)
                            {
                                _posTrInternetHd = new POSTrInternetHd();
                            }
                            _posTrInternetHd.TransType = POSTransTypeDataMapper.GetTransType(POSTransType.Internet);
                            _posTrInternetHd.TransDate = DateTime.Now;
                            _posTrInternetHd.ReferenceNo = this.ReferenceTextBox.Text;
                            _posTrInternetHd.MemberID = this.MemberNoTextBox.Text;
                            _posTrInternetHd.CustName = this.MemberNameTextBox.Text;
                            _posTrInternetHd.CustPhone = this.CustPhoneTextBox.Text;
                            //_posTrInternetHd.OperatorID = this.CashierTextBox.Text;
                            //_posTrInternetHd.Status = POSTrInternetDataMapper.GetStatus(POSTrInternetStatus.SendToCashier);
                            if (this.ReferenceNoHiddenField.Value == "")
                            {
                                _posTrInternetHd.FloorNmbr = this._posInternetBL.GetFloorNmbrByName(this.FloorNumberHiddenField.Value);
                                _posTrInternetHd.TableNmbr = Convert.ToInt16(this.TableIDPerRoomHiddenField.Value);
                                _posTrInternetHd.Status = POSTrInternetDataMapper.GetStatus(POSTrInternetStatus.SendToCashier);
                            }
                            else
                            {
                                _posTrInternetHd.Status = POSTrInternetDataMapper.GetStatus(POSTrInternetStatus.DeliveryOrder);
                                _posTrInternetHd.DeliveryOrderReff = this.ReferenceTextBox.Text;
                            }

                            _posTrInternetHd.IsVOID = false;
                            _posTrInternetHd.CurrCode = "IDR";
                            _posTrInternetHd.ForexRate = 1;
                            _posTrInternetHd.SubTotalForex = Convert.ToDecimal(this.SubTotalLabel.Text);
                            _posTrInternetHd.ServiceChargeAmount = Convert.ToDecimal(this.ServiceChargeLabel.Text);
                            _posTrInternetHd.DiscPercentage = 0;
                            _posTrInternetHd.DiscForex = Convert.ToDecimal(this.DiscountLabel.Text);
                            //int _tax = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSTaxCharge).SetValue);
                            _posTrInternetHd.PPNPercentage = 0;
                            _posTrInternetHd.PPNForex = Convert.ToDecimal(this.TaxHiddenField.Value);//Convert.ToDecimal(this.TaxLabel.Text);
                            _posTrInternetHd.PB1Forex = Convert.ToDecimal(this.PB1HiddenField.Value);
                            _posTrInternetHd.OtherForex = 0;
                            _posTrInternetHd.TotalForex = Convert.ToDecimal(this.TotalLabel.Text);
                            _posTrInternetHd.Remark = this.NotesTextBox.Text;
                            _posTrInternetHd.FakturPajakNmbr = "";
                            _posTrInternetHd.FakturPajakDate = DateTime.Now;
                            _posTrInternetHd.FakturPajakRate = 0;
                            _posTrInternetHd.UserPrep = this.CashierLabel.Text;
                            _posTrInternetHd.DatePrep = DateTime.Now;
                            _posTrInternetHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);
                            _posTrInternetHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);

                            String _resultSendToCashier = this._posInternetBL.SendToCashier(_posTrInternetHd, this.BoughtItemHiddenField.Value);
                            // if DO then back to Page DO
                            if (this.ReferenceNoHiddenField.Value != "" & _resultSendToCashier != "")
                            {
                                Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
                            }
                        }
                        else
                        {
                            if (this.ReferenceNoHiddenField.Value == "")
                            {
                                POSTrInternetHd _posTrInternetHd = new POSTrInternetHd();
                                _posTrInternetHd.TransType = POSTransTypeDataMapper.GetTransType(POSTransType.Internet);
                                _posTrInternetHd.TransDate = DateTime.Now;
                                _posTrInternetHd.ReferenceNo = this.ReferenceTextBox.Text;
                                _posTrInternetHd.MemberID = this.MemberNoTextBox.Text;
                                _posTrInternetHd.CustName = this.MemberNameTextBox.Text;
                                _posTrInternetHd.CustPhone = this.CustPhoneTextBox.Text;
                                _posTrInternetHd.OperatorID = this.CashierLabel.Text;
                                _posTrInternetHd.FloorNmbr = this._posInternetBL.GetFloorNmbrByName(this.FloorNumberHiddenField.Value);
                                _posTrInternetHd.TableNmbr = Convert.ToInt16(this.TableIDPerRoomHiddenField.Value);
                                _posTrInternetHd.Status = POSTrInternetDataMapper.GetStatus(POSTrInternetStatus.SendToCashier);
                                _posTrInternetHd.IsVOID = false;
                                _posTrInternetHd.CurrCode = "IDR";
                                _posTrInternetHd.ForexRate = 1;
                                _posTrInternetHd.SubTotalForex = Convert.ToDecimal(this.SubTotalLabel.Text);
                                _posTrInternetHd.ServiceChargeAmount = Convert.ToDecimal(this.ServiceChargeLabel.Text);
                                _posTrInternetHd.DiscPercentage = 0;
                                _posTrInternetHd.DiscForex = Convert.ToDecimal(this.DiscountLabel.Text);
                                //int _tax = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSTaxCharge).SetValue);
                                _posTrInternetHd.PPNPercentage = 0;
                                _posTrInternetHd.PPNForex = Convert.ToDecimal(this.TaxHiddenField.Value);//Convert.ToDecimal(this.TaxLabel.Text);
                                _posTrInternetHd.PB1Forex = Convert.ToDecimal(this.PB1HiddenField.Value);
                                _posTrInternetHd.OtherForex = 0;
                                _posTrInternetHd.TotalForex = Convert.ToDecimal(this.TotalLabel.Text);
                                _posTrInternetHd.Remark = this.NotesTextBox.Text;
                                _posTrInternetHd.FakturPajakNmbr = "";
                                _posTrInternetHd.FakturPajakDate = DateTime.Now;
                                _posTrInternetHd.FakturPajakRate = 0;
                                _posTrInternetHd.UserPrep = this.CashierLabel.Text;
                                _posTrInternetHd.DatePrep = DateTime.Now;
                                _posTrInternetHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);
                                _posTrInternetHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);

                                String _resultSendToCashier = this._posInternetBL.SendToCashier(_posTrInternetHd, this.BoughtItemHiddenField.Value);
                                //String[] _returnResult = _resultSendToCashier.Split('|');
                                //this.TransRefTextBox.Text = _returnResult[0];
                            }
                            else
                            {
                                POSTrInternetHd _posTrInternetHd = new POSTrInternetHd();
                                POSTrDeliveryOrderRef _posTrDeliveryOrderRef = new POSTrDeliveryOrderRef();

                                _posTrInternetHd.TransType = POSTransTypeDataMapper.GetTransType(POSTransType.Internet);
                                _posTrInternetHd.TransDate = DateTime.Now;
                                _posTrInternetHd.ReferenceNo = this.ReferenceTextBox.Text;
                                _posTrInternetHd.MemberID = this.MemberNoTextBox.Text;
                                _posTrInternetHd.CustName = this.MemberNameTextBox.Text;
                                _posTrInternetHd.CustPhone = this.CustPhoneTextBox.Text;
                                _posTrInternetHd.OperatorID = this.CashierLabel.Text;
                                //_posTrInternetHd.FloorNmbr = this._posInternetBL.GetFloorNmbrByName(this.FloorNumberHiddenField.Value);
                                //_posTrInternetHd.TableNmbr = Convert.ToInt16(this.TableIDPerRoomHiddenField.Value);

                                //Status Value For DELIVERY ORDER = 0
                                _posTrInternetHd.Status = POSTrInternetDataMapper.GetStatus(POSTrInternetStatus.DeliveryOrder);
                                _posTrInternetHd.IsVOID = false;
                                _posTrInternetHd.CurrCode = "IDR";
                                _posTrInternetHd.ForexRate = 1;
                                _posTrInternetHd.SubTotalForex = Convert.ToDecimal(this.SubTotalLabel.Text);
                                _posTrInternetHd.ServiceChargeAmount = Convert.ToDecimal(this.ServiceChargeLabel.Text);
                                _posTrInternetHd.DiscPercentage = 0;
                                _posTrInternetHd.DiscForex = Convert.ToDecimal(this.DiscountLabel.Text);
                                //int _tax = Convert.ToInt32(new CompanyConfig().GetSingle(CompanyConfigure.POSTaxCharge).SetValue);
                                _posTrInternetHd.PPNPercentage = 0;
                                _posTrInternetHd.PPNForex = Convert.ToDecimal(this.TaxHiddenField.Value);//Convert.ToDecimal(this.TaxLabel.Text);
                                _posTrInternetHd.PB1Forex = Convert.ToDecimal(this.PB1HiddenField.Value);
                                _posTrInternetHd.OtherForex = 0;
                                _posTrInternetHd.TotalForex = Convert.ToDecimal(this.TotalLabel.Text);
                                _posTrInternetHd.Remark = this.NotesTextBox.Text;
                                _posTrInternetHd.FakturPajakNmbr = "";
                                _posTrInternetHd.FakturPajakDate = DateTime.Now;
                                _posTrInternetHd.FakturPajakRate = 0;
                                _posTrInternetHd.UserPrep = this.CashierLabel.Text;
                                _posTrInternetHd.DatePrep = DateTime.Now;
                                _posTrInternetHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);
                                _posTrInternetHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);
                                _posTrInternetHd.DeliveryOrderReff = this.ReferenceTextBox.Text;

                                _posTrDeliveryOrderRef.ReferenceNo = this.ReferenceTextBox.Text;
                                _posTrDeliveryOrderRef.TransType = POSTransTypeDataMapper.GetTransType(POSTransType.Internet);

                                String _resultSendToCashier = this._posInternetBL.SendToCashierForDeliveryOrder(_posTrInternetHd, this.BoughtItemHiddenField.Value, _posTrDeliveryOrderRef);
                                // if DO then back to Page DO
                                if (this.ReferenceNoHiddenField.Value != "" & _resultSendToCashier != "")
                                {
                                    Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
                                }
                            }
                        }

                        this.ClearLabelAndData();
                        this.ComposeBoughtItem();
                    }
                }
                else
                {
                    this.WarningLabel.Text = "Reference Number must be Filled";
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
                    _result = this._posInternetBL.SetVOID(this.TransRefTextBox.Text, e.CommandArgument.ToString(), true);
                    if (_result == true)
                    {
                        if (this.ReferenceNoHiddenField.Value != "")
                        {
                            Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
                        }
                        else
                        {
                            this.ClearLabelAndData();
                            this.ComposeBoughtItem();
                            this.JoinJobOrderImageButton.Enabled = true;
                            this.IsEditedHiddenField.Value = "0";
                            //this.FormPanel.Visible = true;
                            //this.ReasonListPanel.Visible = false;
                            this.ChangeVisiblePanel(0);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Process Cancel Success.');", true);
                        }
                    }
                    else
                    {
                        this.ChangeVisiblePanel(0);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Process Cancel Failed.');", true);
                        //this.FormPanel.Visible = true;
                        //this.ReasonListPanel.Visible = false;
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
            //this.FormPanel.Visible = true;
            //this.ReasonListPanel.Visible = false;
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "INTERNET");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }

        protected void ChangeVisiblePanel(Byte _prmValue)
        {
            if (_prmValue == 0)
            {
                this.FormPanel.Visible = true;
                this.ReasonListPanel.Visible = false;
                this.PasswordPanel.Visible = false;
            }
            else if (_prmValue == 1)
            {
                this.FormPanel.Visible = false;
                this.ReasonListPanel.Visible = true;
                this.PasswordPanel.Visible = false;
            }
            else if (_prmValue == 2)
            {
                this.FormPanel.Visible = true;
                this.ReasonListPanel.Visible = false;
                this.PasswordPanel.Visible = true;
            }
        }

        protected void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                String _password = this._pOSConfigurationBL.GetSingle("POSCancelPassword").SetValue;
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

        protected void BackImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ReferenceNoHiddenField.Value != "")
            {
                Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoHiddenField.Value, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }

        protected void NewMemberButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Registration/Registration.aspx");
        }
}
}