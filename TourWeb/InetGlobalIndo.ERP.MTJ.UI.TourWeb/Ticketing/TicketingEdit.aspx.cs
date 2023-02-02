using System;
using System.Collections.Generic;
using System.Linq;
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
using InetGlobalIndo.ERP.MTJ.Common;
using BusinessRule.POS;

namespace InetGlobalIndo.ERP.MTJ.UI.Tour.Ticketing
{
    public partial class TicketingEdit : TicketingBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private AccountBL _accountBL = new AccountBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
        private TicketingBL _ticketingBL = new TicketingBL();
        private MemberBL _memberBL = new MemberBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

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

                this.ShowData();
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
            this.paymentDropDownList.Enabled = true;
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

        public void ShowData()
        {
            POSTrTicketingHd _posTrTicketingHd = this._ticketingBL.GetSinglePOSTrTicketingHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNmbrTextBox.Text = _posTrTicketingHd.TransNmbr;
            this.FileNoTextBox.Text = _posTrTicketingHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_posTrTicketingHd.TransDate);
            this.PaymentTypeDDL.SelectedValue = _posTrTicketingHd.PaymentType;
            this.BranchDropDownList.SelectedValue = _posTrTicketingHd.BranchCode.ToString();
            if (_posTrTicketingHd.PaymentType == "AR")
            {
                this.paymentDropDownList.Enabled = false;
            }
            else
            {
                this.paymentDropDownList.SelectedValue = _posTrTicketingHd.CashBankType;
            }
            this.CustCodeTextBox.Text = _posTrTicketingHd.CustCode;
            this.SalesDropDownList.SelectedValue = _posTrTicketingHd.SalesID;
            this.CustNameTextBox.Text = _posTrTicketingHd.CustName;
            this.RemarkTextBox.Text = _posTrTicketingHd.Remark;
            this.CustPhoneTextBox.Text = _posTrTicketingHd.CustPhone;
            this.CurrCodeDropDownList.SelectedValue = _posTrTicketingHd.CurrCode;
            this.MemberBarcodeTextBox.Text = _posTrTicketingHd.MemberCode;
            this.CurrRateTextBox.Text = ((Decimal)_posTrTicketingHd.ForexRate).ToString("#,##0.0");
            this.PPNPercentTextBox.Text = ((Decimal)_posTrTicketingHd.PPNPercentage).ToString("#,##0.0");
            this.PPNNoTextBox.Text = _posTrTicketingHd.FakturPajakNmbr;
            this.PPNDateTextBox.Text = DateFormMapper.GetValue(_posTrTicketingHd.FakturPajakDate);
            this.PPNRateTextBox.Text = ((Decimal)_posTrTicketingHd.FakturPajakRate).ToString("#,##0.0");
            this.DiscountPercentageTextBox.Text = ((Decimal)_posTrTicketingHd.DiscPercentage).ToString("#,##0.0");
            this.DiscForexTextBox.Text = ((Decimal)_posTrTicketingHd.DiscForex).ToString("#,##0.0");
            this.CurrTextBox.Text = this.CurrCodeDropDownList.SelectedValue;
            this.AmountBaseTextBox.Text = ((Decimal)_posTrTicketingHd.SubTotalForex).ToString("#,##0.0");
            this.PPNForexTextBox.Text = ((Decimal)_posTrTicketingHd.PPNForex).ToString("#,##0.0");
            this.OtherForexTextBox.Text = ((Decimal)_posTrTicketingHd.OtherForex).ToString("#,##0.0");
            this.TotalForexTextBox.Text = ((Decimal)_posTrTicketingHd.TotalForex).ToString("#,##0.0");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsMember _member = this._memberBL.GetSingleByBarcode(this.MemberBarcodeTextBox.Text);
            if ((this.PaymentTypeDDL.SelectedValue == "AR") || (this.PaymentTypeDDL.SelectedValue == "Cash") && this.paymentDropDownList.SelectedValue != "null")
            {
                //POSTrTicketingHd _posTrTicketingHd = new POSTrTicketingHd();
                POSTrTicketingHd _posTrTicketingHd = this._ticketingBL.GetSinglePOSTrTicketingHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _posTrTicketingHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                _posTrTicketingHd.TransType = AppModule.GetValue(TransactionType.Ticketing);
                _posTrTicketingHd.ReferenceNo = "";
                if (this.BranchDropDownList.SelectedValue != "null")
                {
                    _posTrTicketingHd.BranchCode = new Guid(this.BranchDropDownList.SelectedValue);
                }
                else if (this.BranchDropDownList.SelectedValue == "null")
                {
                    _posTrTicketingHd.BranchCode = null;
                }
                if (_member != null)
                {
                    _posTrTicketingHd.MemberCode = this.MemberBarcodeTextBox.Text;
                }
                else
                {
                    _posTrTicketingHd.MemberCode = "";
                }
                _posTrTicketingHd.CustCode = (this.CustCodeTextBox.Text == "" ? "CASH" : this.CustCodeTextBox.Text);
                _posTrTicketingHd.CustName = this.CustNameTextBox.Text;
                _posTrTicketingHd.CustPhone = this.CustPhoneTextBox.Text;
                _posTrTicketingHd.PaymentType = this.PaymentTypeDDL.SelectedValue;
                if (this.PaymentTypeDDL.SelectedValue == "Cash")
                    _posTrTicketingHd.CashBankType = this.paymentDropDownList.SelectedValue;
                else
                    _posTrTicketingHd.CashBankType = "";
                _posTrTicketingHd.SalesID = this.SalesDropDownList.SelectedValue;
                _posTrTicketingHd.OperatorID = HttpContext.Current.User.Identity.Name;
                _posTrTicketingHd.Remark = this.RemarkTextBox.Text;
                _posTrTicketingHd.IsVOID = false;
                _posTrTicketingHd.SendToSettlement = "Y";
                _posTrTicketingHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
                _posTrTicketingHd.ForexRate = this.CurrRateTextBox.Text == "" ? 1 : Convert.ToDecimal(this.CurrRateTextBox.Text);
                _posTrTicketingHd.SubTotalForex = this.AmountBaseTextBox.Text == "" ? 0 : Convert.ToDecimal(this.AmountBaseTextBox.Text);
                _posTrTicketingHd.DiscPercentage = this.DiscountPercentageTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscountPercentageTextBox.Text);
                _posTrTicketingHd.DiscForex = this.DiscForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
                _posTrTicketingHd.PPNPercentage = this.PPNPercentTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);
                _posTrTicketingHd.PPNForex = this.PPNForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
                _posTrTicketingHd.OtherForex = this.OtherForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.OtherForexTextBox.Text);
                _posTrTicketingHd.TotalForex = (this.TotalForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text));
                _posTrTicketingHd.DPForex = 0;
                _posTrTicketingHd.DPPaid = 0;
                _posTrTicketingHd.FakturPajakNmbr = this.PPNNoTextBox.Text;
                if (this.PPNDateTextBox.Text == "")
                    _posTrTicketingHd.FakturPajakDate = null;
                else
                    _posTrTicketingHd.FakturPajakDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
                _posTrTicketingHd.FakturPajakRate = (this.PPNRateTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text));
                _posTrTicketingHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);
                _posTrTicketingHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);
                _posTrTicketingHd.UserPrep = HttpContext.Current.User.Identity.Name;
                _posTrTicketingHd.DatePrep = DateTime.Now;

                bool _result = this._ticketingBL.EditPOSTrTicketingHd(_posTrTicketingHd);

                if (_result == true)
                {
                    Response.Redirect(_homePage);
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
            this.ShowData();
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

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            MsMember _member = this._memberBL.GetSingleByBarcode(this.MemberBarcodeTextBox.Text);

            if ((this.PaymentTypeDDL.SelectedValue == "AR") || (this.PaymentTypeDDL.SelectedValue == "Cash") && this.paymentDropDownList.SelectedValue != "null")
            {
                //POSTrTicketingHd _posTrTicketingHd = new POSTrTicketingHd();
                POSTrTicketingHd _posTrTicketingHd = this._ticketingBL.GetSinglePOSTrTicketingHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _posTrTicketingHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                _posTrTicketingHd.TransType = AppModule.GetValue(TransactionType.Ticketing);
                _posTrTicketingHd.ReferenceNo = "";
                if (this.BranchDropDownList.SelectedValue != "null")
                {
                    _posTrTicketingHd.BranchCode = new Guid(this.BranchDropDownList.SelectedValue);
                }
                else if (this.BranchDropDownList.SelectedValue == "null")
                {
                    _posTrTicketingHd.BranchCode = null;
                }

                if (_member != null)
                {
                    _posTrTicketingHd.MemberCode = this.MemberBarcodeTextBox.Text;
                }
                else
                {
                    _posTrTicketingHd.MemberCode = "";
                }

                _posTrTicketingHd.CustCode = (this.CustCodeTextBox.Text == "" ? "CASH" : this.CustCodeTextBox.Text);
                _posTrTicketingHd.CustName = this.CustNameTextBox.Text;
                _posTrTicketingHd.CustPhone = this.CustPhoneTextBox.Text;
                _posTrTicketingHd.PaymentType = this.PaymentTypeDDL.SelectedValue;
                if (this.PaymentTypeDDL.SelectedValue == "Cash")
                    _posTrTicketingHd.CashBankType = this.paymentDropDownList.SelectedValue;
                else
                    _posTrTicketingHd.CashBankType = "";
                _posTrTicketingHd.SalesID = this.SalesDropDownList.SelectedValue;
                _posTrTicketingHd.OperatorID = HttpContext.Current.User.Identity.Name;
                _posTrTicketingHd.Remark = this.RemarkTextBox.Text;
                _posTrTicketingHd.IsVOID = false;
                _posTrTicketingHd.SendToSettlement = "Y";
                _posTrTicketingHd.CurrCode = this.CurrCodeDropDownList.SelectedValue;
                _posTrTicketingHd.ForexRate = this.CurrRateTextBox.Text == "" ? 1 : Convert.ToDecimal(this.CurrRateTextBox.Text);
                _posTrTicketingHd.SubTotalForex = this.AmountBaseTextBox.Text == "" ? 0 : Convert.ToDecimal(this.AmountBaseTextBox.Text);
                _posTrTicketingHd.DiscPercentage = this.DiscountPercentageTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscountPercentageTextBox.Text);
                _posTrTicketingHd.DiscForex = this.DiscForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.DiscForexTextBox.Text);
                _posTrTicketingHd.PPNPercentage = this.PPNPercentTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNPercentTextBox.Text);
                _posTrTicketingHd.PPNForex = this.PPNForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
                _posTrTicketingHd.OtherForex = this.OtherForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.OtherForexTextBox.Text);
                _posTrTicketingHd.TotalForex = (this.TotalForexTextBox.Text == "" ? 0 : Convert.ToDecimal(this.TotalForexTextBox.Text));
                _posTrTicketingHd.DPForex = 0;
                _posTrTicketingHd.DPPaid = 0;
                _posTrTicketingHd.FakturPajakNmbr = this.PPNNoTextBox.Text;
                if (this.PPNDateTextBox.Text == "")
                    _posTrTicketingHd.FakturPajakDate = null;
                else
                    _posTrTicketingHd.FakturPajakDate = DateFormMapper.GetValue(this.PPNDateTextBox.Text);
                _posTrTicketingHd.FakturPajakRate = (this.PPNRateTextBox.Text == "" ? 0 : Convert.ToDecimal(this.PPNRateTextBox.Text));
                _posTrTicketingHd.DoneSettlement = POSTrSettlementDataMapper.GetDoneSettlement(POSDoneSettlementStatus.NotYet);
                _posTrTicketingHd.DeliveryStatus = POSTrSettlementDataMapper.GetDeliveryStatus(POSDeliveryStatus.NotYetDelivered);
                _posTrTicketingHd.UserPrep = HttpContext.Current.User.Identity.Name;
                _posTrTicketingHd.DatePrep = DateTime.Now;

                bool _result = this._ticketingBL.EditPOSTrTicketingHd(_posTrTicketingHd);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
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
    }
}