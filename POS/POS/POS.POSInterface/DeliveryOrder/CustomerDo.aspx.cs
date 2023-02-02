using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
//using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using BusinessRule.POSInterface;

namespace POS.POSInterface.DeliveryOrder
{
    public partial class CustomerDo : System.Web.UI.Page
    {
        private CustomerDOBL _customerDOBL = new CustomerDOBL();
        private CityBL _cityBL = new CityBL();
        protected string _referenceNo = "referenceNo";
        protected string _code = "code";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.NameTextBox.Attributes["onclick"] = "NameKeyBoard(this.id)";
                this.TelephoneTextBox.Attributes["onclick"] = "TelephoneKeyBoard(this.id)";
                this.HandphoneTextBox.Attributes["onclick"] = "HandphoneKeyBoard(this.id)";
                this.Address1TextBox.Attributes["onclick"] = "Address1KeyBoard(this.id)";
                this.Address2TextBox.Attributes["onclick"] = "Address2KeyBoard(this.id)";
                this.PostalCodeTextBox.Attributes["onclick"] = "PostalCodeKeyBoard(this.id)";
                this.DeliveryAddress1TextBox.Attributes["onclick"] = "DeliveryAddress1KeyBoard(this.id)";
                this.DeliveryAddress2TextBox.Attributes["onclick"] = "DeliveryAddress2KeyBoard(this.id)";
                this.PostalCodeDeliveryTextBox.Attributes["onclick"] = "PostalCodeDeliveryKeyBoard(this.id)";
                
                String spawnJS = "<script type='text/javascript' language='JavaScript'>\n";

                //DECLARE FUNCTION FOR Calling KeyBoard Name
                spawnJS += "function NameKeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findName&titleinput=Name&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON Name
                spawnJS += "function findName(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.NameTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard Telephone
                spawnJS += "function TelephoneKeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findTelephone&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON Telephone
                spawnJS += "function findTelephone(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.TelephoneTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard Handphone
                spawnJS += "function HandphoneKeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findHandphone&titleinput=Phone&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON Handphone
                spawnJS += "function findHandphone(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.HandphoneTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard Address1
                spawnJS += "function Address1KeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findAddress1&titleinput=Address 1&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON Address1
                spawnJS += "function findAddress1(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.Address1TextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard Address2
                spawnJS += "function Address2KeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findAddress2&titleinput=Address 2&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON Address2
                spawnJS += "function findAddress2(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.Address2TextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard PostalCode
                spawnJS += "function PostalCodeKeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findPostalCode&titleinput=Postal Code&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON PostalCode
                spawnJS += "function findPostalCode(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.PostalCodeTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard DeliveryAddress1
                spawnJS += "function DeliveryAddress1KeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findDeliveryAddress1&titleinput=Delivery Address1&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON DeliveryAddress1
                spawnJS += "function findDeliveryAddress1(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.DeliveryAddress1TextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard DeliveryAddress2
                spawnJS += "function DeliveryAddress2KeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findDeliveryAddress2&titleinput=Delivery Address2&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON DeliveryAddress2
                spawnJS += "function findDeliveryAddress2(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.DeliveryAddress2TextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR Calling KeyBoard PostalCodeDelivery
                spawnJS += "function PostalCodeDeliveryKeyBoard(x) {\n";
                spawnJS += "window.open('../General/KeyBoard.aspx?valueCatcher=findPostalCodeDelivery&titleinput=PostalCode Delivery&textbox=x','_popSearch','fullscreen=yes,toolbar=0,location=0,status=0,scrollbars=1') \n";
                spawnJS += "}\n";

                //DECLARE FUNCTION FOR CATCHING ON PostalCodeDelivery
                spawnJS += "function findPostalCodeDelivery(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.PostalCodeDeliveryTextBox.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";
                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.SetAttribute();
                this.ShowCityDDL();
                this.ClearData();
                //this.ReferenceNoLabel.Text = "DO000016";
            }
        }

        protected void SetAttribute()
        {
            this.PostalCodeTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.PostalCodeTextBox.ClientID + ")");
            this.PostalCodeDeliveryTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.PostalCodeDeliveryTextBox.ClientID + ")");
            this.TelephoneTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.TelephoneTextBox.ClientID + ")");
            this.HandphoneTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.HandphoneTextBox.ClientID + ")");
        }

        protected void ShowCityDDL()
        {
            try
            {
                this.CityDDL.Items.Clear();
                this.CityDDL.DataSource = this._cityBL.GetListCityForDDL();
                this.CityDDL.DataTextField = "CityName";
                this.CityDDL.DataValueField = "CityCode";
                this.CityDDL.DataBind();
                this.CityDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));

                this.CityDeliveryDDL.Items.Clear();
                this.CityDeliveryDDL.DataSource = this._cityBL.GetListCityForDDL();
                this.CityDeliveryDDL.DataTextField = "CityName";
                this.CityDeliveryDDL.DataValueField = "CityCode";
                this.CityDeliveryDDL.DataBind();
                this.CityDeliveryDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        private void ClearData()
        {
            DateTime _now = DateTime.Now;

            this.NameTextBox.Text = "";
            this.TelephoneTextBox.Text = "";
            this.HandphoneTextBox.Text = "";
            this.Address1TextBox.Text = "";
            this.Address2TextBox.Text = "";
            this.CityDDL.SelectedValue = "null";
            this.PostalCodeTextBox.Text = "";
            this.TelephoneTextBox.Text = "";
            this.HandphoneTextBox.Text = "";
            this.DeliveryAddress1TextBox.Text = "";
            this.DeliveryAddress2TextBox.Text = "";
            this.CityDeliveryDDL.SelectedValue = "null";
            this.PostalCodeTextBox.Text = "";
            this.PostalCodeDeliveryTextBox.Text = "";
            this.ReferenceNoLabel.Text = "";
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            //Response.Redirect("ListDeliveryOrder.aspx");
            Response.Redirect("../DeliveryOrder/ListDeliveryOrder.aspx" + "?" + "referenceNo=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoLabel.Text, ApplicationConfig.EncryptionKey)));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.CityDDL.SelectedValue != "null" & (this.TelephoneTextBox.Text != "" | this.HandphoneTextBox.Text != ""))
                {
                    string _custDOCode = "";
                    POSMsCustomerDO _msCustomerDo = null;
                    POSMsCustomerDO _telephone = this._customerDOBL.GetSingleForSearch(this.TelephoneTextBox.Text);
                    if (_telephone == null)
                    {
                        _telephone = this._customerDOBL.GetSingleForSearch(this.HandphoneTextBox.Text);
                        if (_telephone == null)
                        {
                            _msCustomerDo = new POSMsCustomerDO();
                            int _maxCustDOCode = this._customerDOBL.GetMaxCustDOCode();
                            _custDOCode = "C" + (_maxCustDOCode + 1).ToString().PadLeft(6, '0');

                            _msCustomerDo.CustDOCode = _custDOCode;
                            _msCustomerDo.Name = this.NameTextBox.Text;
                            _msCustomerDo.Phone = this.TelephoneTextBox.Text;
                            _msCustomerDo.HP = this.HandphoneTextBox.Text;
                            _msCustomerDo.Address1 = this.Address1TextBox.Text;
                            _msCustomerDo.Address2 = this.Address2TextBox.Text;
                            _msCustomerDo.City = this.CityDDL.SelectedValue;
                            _msCustomerDo.ZipCode = this.PostalCodeTextBox.Text;
                        }
                        else
                        {
                            _custDOCode = _telephone.CustDOCode;
                        }
                    }
                    else
                    {
                        _custDOCode = _telephone.CustDOCode;
                    }

                    POSTrDeliveryOrder _trDeliveryOrder = new POSTrDeliveryOrder();
                    POSTrDeliveryOrderLog _trDeliveryOrderLog = new POSTrDeliveryOrderLog();
                    DateTime _now = DateTime.Now;

                    int _maxReferensiNo = this._customerDOBL.GetMaxReferenceNo();
                    string _referenceNo = "DO" + (_maxReferensiNo + 1).ToString().PadLeft(6, '0');

                    _trDeliveryOrder.ReferenceNo = _referenceNo;
                    _trDeliveryOrder.TransDate = _now;
                    _trDeliveryOrder.Status = 1;
                    _trDeliveryOrder.CustDOCode = _custDOCode;
                    if (this.DeliveryAddress1TextBox.Text == "")
                    {
                        _trDeliveryOrder.DOAddress1 = this.Address1TextBox.Text;
                    }
                    else
                    {
                        _trDeliveryOrder.DOAddress1 = this.DeliveryAddress1TextBox.Text;
                    }
                    if (this.DeliveryAddress2TextBox.Text == "")
                    {
                        _trDeliveryOrder.DOAddress2 = this.DeliveryAddress2TextBox.Text;
                    }
                    else
                    {
                        _trDeliveryOrder.DOAddress2 = this.DeliveryAddress2TextBox.Text;
                    }
                    if (this.CityDeliveryDDL.SelectedValue == "null")
                    {
                        _trDeliveryOrder.DOCity = this.CityDDL.SelectedValue;
                    }
                    else
                    {
                        _trDeliveryOrder.DOCity = this.CityDeliveryDDL.SelectedValue;
                    }
                    if (this.PostalCodeDeliveryTextBox.Text == "")
                    {
                        _trDeliveryOrder.DOZipCode = this.PostalCodeTextBox.Text;
                    }
                    else
                    {
                        _trDeliveryOrder.DOZipCode = this.PostalCodeDeliveryTextBox.Text;
                    }
                    _trDeliveryOrder.IsVoid = false;
                    //_trDeliveryOrder.Reason = "";
                    _trDeliveryOrder.CreateBy = HttpContext.Current.User.Identity.Name;
                    _trDeliveryOrder.CreateDate = _now;

                    _trDeliveryOrderLog.ReferenceNo = _referenceNo;
                    _trDeliveryOrderLog.Status = 1;
                    _trDeliveryOrderLog.TransDate = _now;
                    _trDeliveryOrderLog.UserName = HttpContext.Current.User.Identity.Name;

                    //this.CustDOCodeHiddenField.Value = _msCustomerDo.CustDOCode;
                    this.CustDOCodeHiddenField.Value = _custDOCode;

                    bool _result = this._customerDOBL.AddTRDOWithMsCustomerDO(_msCustomerDo, _trDeliveryOrder, _trDeliveryOrderLog);
                    if (_result == true)
                    {
                        //this.WarningLabel.Text = "You Success Add Delivery Order & New CustomerDO ";
                        this.WarningLabel.Text = "You Success Add Delivery Order";
                        this.ReferenceNoLabel.Text = _referenceNo;

                    }
                    else
                    {
                        this.WarningLabel.Text = "You Failed Add CustomerDO";
                    }
                }
                else
                {
                    this.WarningLabel.Text = "You Must Fill City and Telephone/HandPhone.";
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }
        //else
        //{
        //    POSMsCustomerDO _getCustDOCode = this._customerDOBL.GetSingleForSearch(this.TelephoneTextBox.Text);

        //    POSTrDeliveryOrder _trDeliveryOrder = new POSTrDeliveryOrder();
        //    POSTrDeliveryOrderLog _trDeliveryOrderLog = new POSTrDeliveryOrderLog();
        //    int _maxReferensiNo = this._customerDOBL.GetMaxReferenceNo();
        //    string _referenceNo = "DO" + (_maxReferensiNo + 1).ToString().PadLeft(6, '0');

        //    _trDeliveryOrder.ReferenceNo = _referenceNo;
        //    _trDeliveryOrder.TransDate = _now;
        //    _trDeliveryOrder.Status = 1;
        //    _trDeliveryOrder.CustDOCode = _getCustDOCode.CustDOCode;
        //    _trDeliveryOrder.DOAddress1 = this.DeliveryAddress1TextBox.Text;
        //    _trDeliveryOrder.DOAddress2 = this.DeliveryAddress2TextBox.Text;
        //    _trDeliveryOrder.DOCity = this.CityDeliveryDDL.SelectedValue;
        //    _trDeliveryOrder.DOZipCode = this.PostalCodeDeliveryTextBox.Text;
        //    _trDeliveryOrder.IsVoid = false;
        //    //_trDeliveryOrder.Reason = "";
        //    _trDeliveryOrder.CreateBy = HttpContext.Current.User.Identity.Name;
        //    _trDeliveryOrder.CreateDate = _now;

        //    _trDeliveryOrderLog.ReferenceNo = _referenceNo;
        //    _trDeliveryOrderLog.Status = 1;
        //    _trDeliveryOrderLog.TransDate = _now;
        //    _trDeliveryOrderLog.UserName = HttpContext.Current.User.Identity.Name;

        //    this.CustDOCodeHiddenField.Value = _trDeliveryOrder.CustDOCode;

        //    bool _result = this._customerDOBL.AddTRDOWithoutMsCustomerDO(_trDeliveryOrder, _trDeliveryOrderLog);

        //    if (_result == true)
        //    {
        //        this.WarningLabel.Text = "You Success Add Delivery Order";
        //        this.ReferenceNoLabel.Text = _referenceNo;
        //    }
        //    else
        //    {
        //        this.WarningLabel.Text = "You Failed Add CustomerDO";
        //    }
        //}
        //}

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
            this.ClearLabel();
        }

        protected void SearchButton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                POSMsCustomerDO _msCustomerDO = null;
                if (this.TelephoneTextBox.Text != "" | this.HandphoneTextBox.Text != "")
                {
                    if (this.TelephoneTextBox.Text != "")
                    {
                        _msCustomerDO = this._customerDOBL.GetSingleForSearch(this.TelephoneTextBox.Text);
                    }
                    if (this.HandphoneTextBox.Text != "" & _msCustomerDO == null)
                    {
                        _msCustomerDO = this._customerDOBL.GetSingleForSearch(this.HandphoneTextBox.Text);
                    }
                    if (_msCustomerDO == null)
                    {
                        this.WarningLabel.Text = "Customer Not Found in Database Customer DO";
                    }
                    else
                    {
                        this.NameTextBox.Text = _msCustomerDO.Name;
                        this.NameTextBox.Attributes.Add("ReadOnly", "True");
                        this.NameTextBox.Attributes.Add("Style", "BackGround-Color:#CCCCCC");
                        this.TelephoneTextBox.Text = _msCustomerDO.Phone;
                        this.HandphoneTextBox.Text = _msCustomerDO.HP;
                        this.Address1TextBox.Text = _msCustomerDO.Address1;
                        this.Address2TextBox.Text = _msCustomerDO.Address2;
                        this.CityDDL.SelectedValue = _msCustomerDO.City;
                        this.PostalCodeTextBox.Text = _msCustomerDO.ZipCode;
                        this.CustDOCodeHiddenField.Value = _msCustomerDO.CustDOCode;
                        this.ClearLabel();
                    }
                }
                else
                {
                    this.WarningLabel.Text = "Telephone or HandPhone Must Be Filled";
                }
            }
            catch (Exception ex)
            {
                this.errorhandler(ex);
            }
        }

        protected void Printing_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ReferenceNoLabel.Text != "")
            {
                Response.Redirect("~/Printing/POSPrinting.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoLabel.Text + "-" + this.NameTextBox.Text + "-" + this.TelephoneTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You must create Customer Delivery Order";
            }
        }

        protected void Stationary_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ReferenceNoLabel.Text != "")
            {
                Response.Redirect("~/Retail/POSRetail.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoLabel.Text + "-" + this.NameTextBox.Text + "-" + this.TelephoneTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You should make delivery order";
            }
        }

        protected void EVoucher_Click(object sender, ImageClickEventArgs e)
        {
            //if (this.ReferenceNoLabel.Text != "")
            //{
            //    Response.Redirect("~/THotel/Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoLabel.Text + "-" + this.NameTextBox.Text + "-" + this.TelephoneTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + "");
            //}
            //else
            //{
            //    this.WarningLabel.Text = "You must create Customer Delivery Order";
            //}

        }

        protected void Shipping_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Photocopy_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ReferenceNoLabel.Text != "")
            {
                Response.Redirect("~/Photocopy/POSPhotocopy.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoLabel.Text + "-" + this.NameTextBox.Text + "-" + this.TelephoneTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You must create Customer Delivery Order";
            }
        }

        protected void Cafe_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ReferenceNoLabel.Text != "")
            {
                Response.Redirect("~/Cafe/POSCafeChooseTable.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoLabel.Text + "-" + this.NameTextBox.Text + "-" + this.TelephoneTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You must create Customer Delivery Order";
            }
        }

        protected void Internet_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ReferenceNoLabel.Text != "")
            {
                Response.Redirect("~/Internet/POSInternetChooseTable.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoLabel.Text + "-" + this.NameTextBox.Text + "-" + this.TelephoneTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You must create Customer Delivery Order";
            }
        }

        protected void Tiketing_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ReferenceNoLabel.Text != "")
            {
                Response.Redirect("~/Ticketing/Ticketing.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoLabel.Text + "-" + this.NameTextBox.Text + "-" + this.TelephoneTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + "");
            }
            else
            {
                this.WarningLabel.Text = "You must create Customer Delivery Order";
            }
        }

        protected void GraphicDesain_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ReferenceNoLabel.Text != "")
            {
                Response.Redirect("~/Graphic/POSGraphic.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoLabel.Text + "-" + this.NameTextBox.Text + "-" + this.TelephoneTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You must create Customer Delivery Order";
            }
        }

        protected void VoucherHotel_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ReferenceNoLabel.Text != "")
            {
                Response.Redirect("~/THotel/Hotel.aspx" + "?" + this._referenceNo + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.ReferenceNoLabel.Text + "-" + this.NameTextBox.Text + "-" + this.TelephoneTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._code + "=" + "");
            }
            else
            {
                this.WarningLabel.Text = "You must create Customer Delivery Order";
            }
        }

        protected void errorhandler(Exception ex)
        {
            //ErrorHandler.Record(ex, EventLogEntryType.Error);
            String _errorCode = new ErrorLogBL().CreateErrorLog(ex.Message, ex.StackTrace, HttpContext.Current.User.Identity.Name, "POS", "DO_CUSTOMERDO");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Found an Error on Your System. Please Contact Your Administrator.');", true);
        }
    }
}