using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using BusinessRule.POS;

namespace InetGlobalIndo.ERP.MTJ.UI.Tour.THotel
{
    public partial class HotelAdd : THotelBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private AccountBL _accountBL = new AccountBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
        private HotelBL _ticketingBL = new HotelBL();
        private MemberBL _memberBL = new MemberBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                
                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.PPNDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.PPNDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.btnSearchCustomer.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findCustomer&configCode=customer','_popSearch','width=1200,height=1000,toolbar=0,location=0,status=0,scrollbars=1')";

                String spawnJS = "<script language='JavaScript'>\n";

                spawnJS += "function findCustomer(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.CustCodeTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.CustNameTextBox.ClientID + "').value = dataArray[1];\n";
                spawnJS += "document.getElementById('" + this.CustPhoneTextBox.ClientID + "').value = dataArray[4];\n";
                spawnJS += "document.forms[0].submit();\n";
                spawnJS += "}\n";

                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.btnSearchMember.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findMember&configCode=member','_popSearch','width=1200,height=1000,toolbar=0,location=0,status=0,scrollbars=1')";

                String spawnJS2 = "<script language='JavaScript'>\n";

                spawnJS2 += "function findMember(x) {\n";
                spawnJS2 += "dataArray = x.split ('|') ;\n";
                spawnJS2 += "document.getElementById('" + this.MemberBarcodeHiddenField.ClientID + "').value = dataArray[0];\n";
                //spawnJS2 += "document.getElementById('" + this.MemberBarcodeTextBox.ClientID + "').value = dataArray[1];\n";
                spawnJS2 += "document.forms[0].submit();\n";
                spawnJS2 += "}\n";

                spawnJS2 += "</script>\n";
                this.javascriptReceiver2.Text = spawnJS2;

                this.ShowBranchAccDropDownList();
                this.ShowCurr();

                this.ShowPayType();                           
                this.ShowSalesDropDownList();

                this.ClearData();
                this.SetAttribute();
                this.ShowMemberBarcode();
                this.MemberBarcodeHiddenField_OnValueChanged(null, null);
            }
        }

        protected void ShowMemberBarcode()
        {
            MsMember _member = this._memberBL.GetSingle(this.MemberBarcodeHiddenField.Value);
            if (_member != null)
            {
                this.MemberBarcodeTextBox.Text = _member.Barcode;
            }
            else
            {
                this.MemberBarcodeTextBox.Text = "";
            }
        }

        protected void MemberBarcodeHiddenField_OnValueChanged(object sender, EventArgs e)
        {
            this.ShowMemberBarcode();
        }

        private void ClearDataNumeric()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);

            //this.CurrRateTextBox.Text = "0";
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.PPNPercentTextBox.Text = "0";
            //this.PPNRateTextBox.Text = "0";
            //this.CurrTextBox.Text = "";
            this.PPNForexTextBox.Text = "0";
            //this.PPhPercentTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
            //this.PPhForexTextBox.Text = "0";
            this.AmountBaseTextBox.Text = "0";
            this.DiscForexTextBox.Text = "0";
            this.PPNForexTextBox.Text = "0";
            this.OtherForexTextBox.Text = "0";
            this.TotalForexTextBox.Text = "0";
            this.DiscountPercentageTextBox.Text = "0";
        }

        private void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();
            this.ClearDataNumeric();

            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.BranchDropDownList.SelectedValue = "null";
            this.CustCodeTextBox.Text = "";
            this.CustNameTextBox.Text = "";
            this.CustPhoneTextBox.Text = "";
            this.PaymentTypeDDL.SelectedValue = "AR";
            this.paymentDropDownList.Enabled = false;
            this.paymentDropDownList.SelectedValue = "null";
            this.RemarkTextBox.Text = "";
            this.SalesDropDownList.SelectedValue = "null";
            this.CurrCodeDropDownList.SelectedValue = _currencyBL.GetCurrDefault();
            //this.CurrRateTextBox.Text = "";
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.PPNNoTextBox.Text = "";
            this.PPNDateTextBox.Text = "";         
        }

        private void DisableRate()
        {
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.CurrRateTextBox.Text = "1";
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            this.PPNRateTextBox.Text = "1";
        }

        private void EnableRate()
        {
            this.CurrRateTextBox.Attributes.Remove("ReadOnly");
            this.CurrRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");
            this.PPNRateTextBox.Attributes.Remove("ReadOnly");
            this.PPNRateTextBox.Attributes.Add("style", "background-color:#FFFFFF");

        }

        private void SetCurrRate()
        {
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeDropDownList.SelectedValue);
            this.CurrRateTextBox.Text = this._currencyRateBL.GetSingleLatestCurrRate(this.CurrCodeDropDownList.SelectedValue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNRateTextBox.Text = this.CurrRateTextBox.Text;
            this.CurrTextBox.Text = this.CurrCodeDropDownList.SelectedValue;
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            if (this.CurrCodeDropDownList.SelectedValue.Trim().ToLower() == _currencyBL.GetCurrDefault().Trim().ToLower())
            {
                this.DisableRate();
            }
            else
            {
                this.EnableRate();
            }
        }

        private void ShowPayType()
        {
            this.paymentDropDownList.Items.Clear();
            this.paymentDropDownList.DataTextField = "PayName";
            this.paymentDropDownList.DataValueField = "PayCode";
            this.paymentDropDownList.DataSource = this._paymentBL.GetListDDLDPSuppPay(this.CurrCodeDropDownList.SelectedValue);
            this.paymentDropDownList.DataBind();
            this.paymentDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCurr()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.SelectedValue = _currencyBL.GetCurrDefault();
            this.CurrCodeDropDownListSelectedIndexChanged();
        }

        protected void ShowBranchAccDropDownList()
        {
            this.BranchDropDownList.Items.Clear();
            this.BranchDropDownList.DataSource = this._accountBL.GetListBranchAccountForDDL();
            this.BranchDropDownList.DataValueField = "BranchAccCode";
            this.BranchDropDownList.DataTextField = "BranchAccName";
            this.BranchDropDownList.DataBind();
            this.BranchDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowSalesDropDownList()
        {
            this.SalesDropDownList.Items.Clear();
            this.SalesDropDownList.DataSource = this._employeeBL.GetListEmpForDDL();
            this.SalesDropDownList.DataValueField = "EmpNumb";
            this.SalesDropDownList.DataTextField = "EmpName";
            this.SalesDropDownList.DataBind();
            this.SalesDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

        }

        private void CurrCodeDropDownListSelectedIndexChanged()
        {
            this.ClearDataNumeric();

            if (CurrCodeDropDownList.SelectedValue != "null")
            {
                this.SetCurrRate();
            }

            this.SetAttributeRate();
        }

        protected void CurrCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CurrCodeDropDownListSelectedIndexChanged();
        }

        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNDateTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBaseTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNNoTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.CurrRateTextBox.Attributes.Add("ReadOnly", "True");
            //this.PPhForexTextBox.Attributes.Add("ReadOnly", "True");
            //this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.CurrRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.CurrRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.PPNRateTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.PPNRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.PPNPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscountPercentageTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            //this.PPhPercentTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" + "," + this.PPhPercentTextBox.ClientID + "," + this.PPhForexTextBox.ClientID + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.DiscountPercentageTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscountPercentageTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            //this.DiscForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscountPercentageTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.OtherForexTextBox.Attributes.Add("OnBlur", "Calculate(" + this.PPNPercentTextBox.ClientID + "," + this.PPNNoTextBox.ClientID + "," + this.PPNDateTextBox.ClientID + "," + "button2" + "," + this.AmountBaseTextBox.ClientID + "," + this.DiscountPercentageTextBox.ClientID + "," + this.DiscForexTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + "," + this.OtherForexTextBox.ClientID + "," + this.TotalForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            MsMember _member = this._memberBL.GetSingleByBarcode(this.MemberBarcodeTextBox.Text);
            if ((this.PaymentTypeDDL.SelectedValue == "AR") || (this.PaymentTypeDDL.SelectedValue == "Cash") && this.paymentDropDownList.SelectedValue != "null")
            {
                POSTrHotelHd _posTrHotelHd = new POSTrHotelHd();

                _posTrHotelHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                _posTrHotelHd.Status = POSHotelDataMapper.GetStatus(TransStatus.OnHold);
                _posTrHotelHd.TransType = AppModule.GetValue(TransactionType.Hotel);
                _posTrHotelHd.ReferenceNo = "";
                if (this.BranchDropDownList.SelectedValue != "null")
                {
                    _posTrHotelHd.BranchCode = new Guid(this.BranchDropDownList.SelectedValue);
                }
                if (_member != null)
                {
                    _posTrHotelHd.MemberCode = this.MemberBarcodeTextBox.Text;
                }
                else
                {
                    _posTrHotelHd.MemberCode = "";
                }
                _posTrHotelHd.CustCode = (this.CustCodeTextBox.Text == "" ? "CASH" : this.CustCodeTextBox.Text);
                _posTrHotelHd.CustName = this.CustNameTextBox.Text;
                _posTrHotelHd.CustPhone = this.CustPhoneTextBox.Text;
                _posTrHotelHd.PaymentType = this.PaymentTypeDDL.SelectedValue;
                if (this.PaymentTypeDDL.SelectedValue == "AR")
                    _posTrHotelHd.CashBankType = "";
                else
                    _posTrHotelHd.CashBankType = this.paymentDropDownList.SelectedValue;
                _posTrHotelHd.SalesID = this.SalesDropDownList.SelectedValue;
                _posTrHotelHd.OperatorID = HttpContext.Current.User.Identity.Name;
                _posTrHotelHd.Remark = this.RemarkTextBox.Text;
                _posTrHotelHd.IsVOID = false;
                _posTrHotelHd.SendToSettlement = "Y";
                _posTrHotelHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
                _posTrHotelHd.ForexRate = this.CurrRateTextBox.Text == "" ? 1 : Convert.ToDecimal(this.CurrRateTextBox.Text);
                _posTrHotelHd.SubTotalForex = this.AmountBaseTextBox.Text == "" ? 0 : Convert.ToDecimal(this.AmountBaseTextBox.Text);
                _posTrHotelHd.DiscPercentage = this.DiscountPercentageTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscountPercentageTextBox.Text);
                _posTrHotelHd.DiscForex = this.DiscForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
                _posTrHotelHd.PPNPercentage = this.PPNPercentTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);
                _posTrHotelHd.PPNForex = this.PPNForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
                _posTrHotelHd.OtherForex = this.OtherForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.OtherForexTextBox.Text);
                _posTrHotelHd.TotalForex = (this.TotalForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text));
                //_posTrHotelHd.DPForex = 0;
                //_posTrHotelHd.DPPaid = 0;
                _posTrHotelHd.FakturPajakNmbr = this.PPNNoTextBox.Text;
                if (this.PPNDateTextBox.Text == "")
                    _posTrHotelHd.FakturPajakDate = null;
                else
                    _posTrHotelHd.FakturPajakDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
                _posTrHotelHd.FakturPajakRate = (this.PPNRateTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text));
                _posTrHotelHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);
                _posTrHotelHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);
                _posTrHotelHd.UserPrep = HttpContext.Current.User.Identity.Name;
                _posTrHotelHd.DatePrep = DateTime.Now;

                string _result = this._ticketingBL.AddPOSTrHotelHd(_posTrHotelHd);

                if (_result != "")
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Add Data";
                }
            }
            else
            {
                this.WarningLabel.Text = "Please, Select Payment";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }

        protected void PaymentTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PaymentTypeDDL.SelectedValue == "AR")
            {
                this.paymentDropDownList.Enabled = false;
            }
            else
            {
                this.paymentDropDownList.Enabled = true;
                this.paymentDropDownList.SelectedValue = "null";
            }
        }
    }
}