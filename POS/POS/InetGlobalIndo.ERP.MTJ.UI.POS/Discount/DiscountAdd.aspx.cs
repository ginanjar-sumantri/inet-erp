using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Discount
{
    public partial class DiscountAdd : DiscountBase
    {
        private POSDiscountBL _discountBL = new POSDiscountBL();
        private PermissionBL _permBL = new PermissionBL();
        private CreditCardTypeBL _creditCardTypeBL = new CreditCardTypeBL();
        private CreditCardBL _creditCardBL = new CreditCardBL();
        private DebitCardBL _debitCardBL = new DebitCardBL();

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

                this.ClearData();
                this.SetAttribute();
            }

        }

        private void SetAttribute()
        {
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
            this.DiscountAmountTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.DiscountAmountTextBox.ClientID + "," + this.DiscountAmountTextBox.ClientID + ",500" + ");");
            this.MinimumPaymentTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.MinimumPaymentTextBox.ClientID + "," + this.MinimumPaymentTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();
            this.DiscountConfigCodeTextBox.Text = "";

            String _firstdate = DateTime.Now.ToString("yyyy-MM-dd");
            this.StartDateTextBox.Text = _firstdate;

            DateTime lastDate = DateTime.Now.AddMonths(1);
            lastDate = lastDate.AddDays(-(lastDate.Day));
            String _lastdate = lastDate.ToString("yyyy-MM-dd");
            this.EndDateTextBox.Text = _lastdate;

            for (int i = 0; i < 24; i++)
            {
                this.StartTimeHourDDL.Items.Add(new ListItem(i.ToString()));
                this.EndTimeHourDDL.Items.Add(new ListItem(i.ToString()));
            }
            this.EndTimeHourDDL.SelectedValue = "23";

            for (int i = 0; i < 60; i++)
            {
                this.StartTimeMinuteDDL.Items.Add(new ListItem(i.ToString()));
                this.EndTimeMinuteDDL.Items.Add(new ListItem(i.ToString()));
            }
            this.EndTimeMinuteDDL.SelectedValue = "59";

            this.AmountTypeRBL.SelectedIndex = 0;
            this.DiscountAmountTextBox.Text = "";
            this.DiscountTypeRBL.SelectedIndex = 0;
            this.MinimumPaymentTextBox.Text = "";
            this.fgMemberDiscountCheckBox.Checked = false;
            this.ForcePrintOnDemandCheckBox.Checked = false;
            this.DiscountIntervalTypeDropDownList.SelectedValue = "1";
            this.DateTextBox.Text = "";
            this.fgMondayCheckBox.Checked = false;
            this.fgTuesdayCheckBox.Checked = false;
            this.fgWedCheckBox.Checked = false;
            this.fgThurCheckBox.Checked = false;
            this.fgFridayCheckBox.Checked = false;
            this.fgSatCheckBox.Checked = false;
            this.fgSundayCheckBox.Checked = false;

            this.CreditCardTypeDDL.Visible = false;
            this.CreditCardDDL.Visible = false;
            this.DebitCardDDL.Visible = false;
            this.MinimumPaymentPanel.Visible = false;
            this.ChooseButton.Visible = false;
            this.DiscountAmountRangeValidator.Visible = false;
        }

        private void ShowCreditCardType()
        {
            this.CreditCardTypeDDL.DataTextField = "CreditCardTypeName";
            this.CreditCardTypeDDL.DataValueField = "CreditCardTypeCode";
            this.CreditCardTypeDDL.DataSource = this._creditCardTypeBL.GetCreditCardTypeListDDL();
            this.CreditCardTypeDDL.DataBind();
            this.CreditCardTypeDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCreditCard()
        {
            this.CreditCardDDL.DataTextField = "CreditCardName";
            this.CreditCardDDL.DataValueField = "CreditCardCode";
            this.CreditCardDDL.DataSource = this._creditCardBL.GetCreditCardListDDL(this.CreditCardTypeDDL.SelectedValue);
            this.CreditCardDDL.DataBind();
            this.CreditCardDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowDebitCard()
        {
            this.DebitCardDDL.DataTextField = "DebitCardName";
            this.DebitCardDDL.DataValueField = "DebitCardCode";
            this.DebitCardDDL.DataSource = this._debitCardBL.GetDebitCardListDDL();
            this.DebitCardDDL.DataBind();
            this.DebitCardDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            //bool _cek = false;
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                //if (this.DiscountIntervalTypeDropDownList.SelectedItem.Text == "Monthly")
                //{
                //    if (Convert.ToInt16(this.DateTextBox.Text) <= 31 && Convert.ToInt16(this.DateTextBox.Text) >= 1)
                //    {
                //        _cek = true;
                //    }
                //}
                //else
                //{
                //    _cek = true;
                //}


                //if (_cek)
                //{
                List<String> _result1 = this._discountBL.CheckDateDisc(Convert.ToByte(this.PaymentTypeDDL.SelectedValue), Convert.ToDateTime(this.StartDateTextBox.Text), Convert.ToDateTime(this.EndDateTextBox.Text));
                if (_result1.Count == 0)
                {
                    POSMsDiscountConfig _posDiscountConfig = new POSMsDiscountConfig();

                    _posDiscountConfig.DiscountConfigCode = this.DiscountConfigCodeTextBox.Text;
                    _posDiscountConfig.StartDate = Convert.ToDateTime(this.StartDateTextBox.Text);
                    _posDiscountConfig.EndDate = Convert.ToDateTime(this.EndDateTextBox.Text);
                    _posDiscountConfig.AmountType = this.AmountTypeRBL.SelectedValue;
                    if (this.fgMemberDiscountCheckBox.Checked == false)
                    {
                        _posDiscountConfig.DiscAmount = Convert.ToDecimal(this.DiscountAmountTextBox.Text);
                    }
                    _posDiscountConfig.DiscountCalcType = this.DiscountTypeRBL.SelectedValue;
                    if (this.DiscountTypeRBL.SelectedValue == "S")
                    {
                        _posDiscountConfig.MinimumPayment = Convert.ToDecimal(this.MinimumPaymentTextBox.Text);
                    }
                    _posDiscountConfig.fgMemberDiscount = this.fgMemberDiscountCheckBox.Checked;
                    _posDiscountConfig.ForcePrintOnDemand = this.ForcePrintOnDemandCheckBox.Checked;
                    _posDiscountConfig.PaymentType = Convert.ToByte(this.PaymentTypeDDL.SelectedValue);
                    if (this.PaymentTypeDDL.SelectedValue == "1")
                    {
                        _posDiscountConfig.DebitCreditCode = this.CreditCardDDL.SelectedValue;
                        _posDiscountConfig.CreditCardTypeCode = this.CreditCardTypeDDL.SelectedValue;

                    }
                    else if (this.PaymentTypeDDL.SelectedValue == "2")
                    {
                        _posDiscountConfig.DebitCreditCode = this.DebitCardDDL.SelectedValue;
                    }
                    else
                    {
                    }
                    _posDiscountConfig.Status = POSDiscountDataMapper.GetStatus(POSDiscountStatus.OnHold);
                    _posDiscountConfig.DiscountIntervalType = this.DiscountIntervalTypeDropDownList.SelectedItem.Text;
                    _posDiscountConfig.DateInMonth = Convert.ToByte(((this.DateTextBox.Text == "") ? 0 : Convert.ToInt16(this.DateTextBox.Text)));
                    _posDiscountConfig.fgMonday = this.fgMondayCheckBox.Checked;
                    _posDiscountConfig.fgTuesday = this.fgTuesdayCheckBox.Checked;
                    _posDiscountConfig.fgWed = this.fgWedCheckBox.Checked;
                    _posDiscountConfig.fgThur = this.fgThurCheckBox.Checked;
                    _posDiscountConfig.fgFriday = this.fgFridayCheckBox.Checked;
                    _posDiscountConfig.fgSat = this.fgSatCheckBox.Checked;
                    _posDiscountConfig.fgSunday = this.fgSundayCheckBox.Checked;
                    DateTime _start = Convert.ToDateTime(this.StartDateTextBox.Text + " " + this.StartTimeHourDDL.SelectedValue + ":" + this.StartTimeMinuteDDL.SelectedValue + ":00");
                    _posDiscountConfig.StartTime = _start;
                    DateTime _end = Convert.ToDateTime(this.EndDateTextBox.Text + " " + this.EndTimeHourDDL.SelectedValue + ":" + this.EndTimeMinuteDDL.SelectedValue + ":00");
                    _posDiscountConfig.EndTime = _end;

                    bool _result = this._discountBL.Add(_posDiscountConfig);

                    if (_result == true)
                    {
                        Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.DiscountConfigCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
                    }
                    else
                    {
                        this.ClearLabel();
                        this.WarningLabel.Text = "You Failed Add Data";
                    }
                }
                else
                {
                    String _que = "";
                    foreach (var _item in _result1)
                    {
                        if (_que == "")
                        {
                            _que += _item;
                        }
                        else
                        {
                            _que += "," + _item;
                        }
                    }

                    //if(_result1.Count == 1)
                    //{
                    //     = _result1;
                    //}
                    //for (int i = 0; i < _result1.Count; i++)
                    //{                        
                    //    String _que = 
                    this.ClearLabel();
                    this.WarningLabel.Text = _que + " had at that Range Date with same Type Payment, Do you want to continue?";
                    this.ChooseButton.Visible = true;
                }
                //}
                //else
                //{
                //    this.WarningLabel.Text = "Maximum date in the input 31";
                //}
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

        protected void fgMemberDiscount_OnCheckedChanged(object sender, EventArgs e)
        {
            if (this.fgMemberDiscountCheckBox.Checked == true)
            {
                this.DiscountAmountTextBox.Text = "";
                this.DiscountAmountTable.Visible = false;
                this.DiscountAmountRequiredFieldValidator.Visible = false;
            }
            else
            {
                this.DiscountAmountTable.Visible = true;
                this.DiscountAmountTextBox.Text = "";
                this.DiscountAmountRequiredFieldValidator.Visible = true;
            }
        }

        protected void AmountTypeRBL_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AmountTypeRBL.SelectedValue == "P")
            {
                this.DiscountAmountRangeValidator.Visible = true;
                //this.DiscountAmountTextBox.Text="";
            }
            else
            {
                this.DiscountAmountRangeValidator.Visible = false;
                //this.DiscountAmountTextBox.Text = "";
            }

        }

        protected void DiscountIntervalTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DiscountIntervalTypeDropDownList.SelectedValue == "1") //daily
            {
                ClearDataUpdatePanel();

                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = true;
            }
            else if (this.DiscountIntervalTypeDropDownList.SelectedValue == "2") //weekly
            {
                ClearDataUpdatePanel();

                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = true;
                this.DailyTable.Visible = false;
            }
            else if (this.DiscountIntervalTypeDropDownList.SelectedValue == "3") //monthly
            {
                ClearDataUpdatePanel();

                this.MonthlyTable.Visible = true;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = false;
            }
        }

        protected void ClearDataUpdatePanel()
        {
            String _date = DateTime.Now.ToString("yyyy-MM-dd");

            this.DateTextBox.Text = "";
            this.fgMondayCheckBox.Checked = false;
            this.fgTuesdayCheckBox.Checked = false;
            this.fgWedCheckBox.Checked = false;
            this.fgThurCheckBox.Checked = false;
            this.fgFridayCheckBox.Checked = false;
            this.fgSatCheckBox.Checked = false;
            this.fgSundayCheckBox.Checked = false;
            //this.StartTimeTextBox.Text = _date;
            //this.EndTimeTextBox.Text = _date;
        }

        protected void PaymentTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PaymentTypeDDL.SelectedValue == "1")
            {
                this.CreditCardTypeDDL.Visible = true;
                this.ShowCreditCardType();
                this.CreditCardDDL.Visible = false;
                this.DebitCardDDL.Visible = false;
            }
            else if (this.PaymentTypeDDL.SelectedValue == "2")
            {
                this.DebitCardDDL.Visible = true;
                this.ShowDebitCard();
                this.CreditCardDDL.Visible = false;
                this.CreditCardTypeDDL.Visible = false;
            }
            else
            {
                this.DebitCardDDL.Visible = false;
                this.CreditCardDDL.Visible = false;
                this.CreditCardTypeDDL.Visible = false;
            }
        }

        protected void CreditCardTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CreditCardTypeDDL.SelectedValue == "null")
            {
                this.CreditCardDDL.Visible = false;
            }
            else
            {
                this.CreditCardDDL.Visible = true;
                this.ShowCreditCard();
            }
        }

        protected void DiscountTypeRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DiscountTypeRBL.SelectedValue == "S")
            {
                this.MinimumPaymentPanel.Visible = true;
            }
            else
            {
                this.MinimumPaymentPanel.Visible = false;
            }
        }

        protected void Yes1Button_Click(object sender, EventArgs e)
        {
            POSMsDiscountConfig _posDiscountConfig = new POSMsDiscountConfig();

            _posDiscountConfig.DiscountConfigCode = this.DiscountConfigCodeTextBox.Text;
            _posDiscountConfig.StartDate = Convert.ToDateTime(this.StartDateTextBox.Text);
            _posDiscountConfig.EndDate = Convert.ToDateTime(this.EndDateTextBox.Text);
            _posDiscountConfig.AmountType = this.AmountTypeRBL.SelectedValue;
            if (this.fgMemberDiscountCheckBox.Checked == false)
            {
                _posDiscountConfig.DiscAmount = Convert.ToDecimal(this.DiscountAmountTextBox.Text);
            }
            _posDiscountConfig.DiscountCalcType = this.DiscountTypeRBL.SelectedValue;
            if (this.DiscountTypeRBL.SelectedValue == "S")
            {
                _posDiscountConfig.MinimumPayment = Convert.ToDecimal(this.MinimumPaymentTextBox.Text);
            }
            _posDiscountConfig.fgMemberDiscount = this.fgMemberDiscountCheckBox.Checked;
            _posDiscountConfig.ForcePrintOnDemand = this.ForcePrintOnDemandCheckBox.Checked;
            _posDiscountConfig.PaymentType = Convert.ToByte(this.PaymentTypeDDL.SelectedValue);
            if (this.PaymentTypeDDL.SelectedValue == "1")
            {
                _posDiscountConfig.DebitCreditCode = this.CreditCardDDL.SelectedValue;
                _posDiscountConfig.CreditCardTypeCode = this.CreditCardTypeDDL.SelectedValue;
            }
            else if (this.PaymentTypeDDL.SelectedValue == "2")
            {
                _posDiscountConfig.DebitCreditCode = this.DebitCardDDL.SelectedValue;
            }
            else
            {
            }
            _posDiscountConfig.Status = POSDiscountDataMapper.GetStatus(POSDiscountStatus.OnHold);
            _posDiscountConfig.DiscountIntervalType = this.DiscountIntervalTypeDropDownList.SelectedItem.Text;
            _posDiscountConfig.DateInMonth = Convert.ToByte(((this.DateTextBox.Text == "") ? 0 : Convert.ToInt16(this.DateTextBox.Text)));
            _posDiscountConfig.fgMonday = this.fgMondayCheckBox.Checked;
            _posDiscountConfig.fgTuesday = this.fgTuesdayCheckBox.Checked;
            _posDiscountConfig.fgWed = this.fgWedCheckBox.Checked;
            _posDiscountConfig.fgThur = this.fgThurCheckBox.Checked;
            _posDiscountConfig.fgFriday = this.fgFridayCheckBox.Checked;
            _posDiscountConfig.fgSat = this.fgSatCheckBox.Checked;
            _posDiscountConfig.fgSunday = this.fgSundayCheckBox.Checked;
            DateTime _start = Convert.ToDateTime(this.StartDateTextBox.Text + " " + this.StartTimeHourDDL.SelectedValue + ":" + this.StartTimeMinuteDDL.SelectedValue + ":00");
            _posDiscountConfig.StartTime = _start;
            DateTime _end = Convert.ToDateTime(this.EndDateTextBox.Text + " " + this.EndTimeHourDDL.SelectedValue + ":" + this.EndTimeMinuteDDL.SelectedValue + ":00");
            _posDiscountConfig.EndTime = _end;

            bool _result = this._discountBL.Add(_posDiscountConfig);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.DiscountConfigCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void No1Button_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ChooseButton.Visible = false;
        }

        protected void CheckValidData()
        {
            this.ClearLabel();
            if (Convert.ToDateTime(this.StartDateTextBox.Text) < DateTime.Now.AddDays(-1))
            {
                this.WarningLabel.Text = "Start Date must more than Now.";
            }
            if (Convert.ToDateTime(this.StartDateTextBox.Text) > Convert.ToDateTime(this.EndDateTextBox.Text))
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Start Date must less than End Date.";
            }
            POSMsDiscountConfig _posDiscountConfig = this._discountBL.GetSingle(this.DiscountConfigCodeTextBox.Text);
            if (_posDiscountConfig != null)
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Discount Config Code already use by an other data.";
            }
            if (this.PaymentTypeDDL.SelectedValue == "1" & (this.CreditCardTypeDDL.SelectedValue == "null" | this.CreditCardDDL.SelectedValue == "null" | this.CreditCardDDL.SelectedValue == "")) //kredit
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Fill your Payment Type completely.";
            }
            if (this.PaymentTypeDDL.SelectedValue == "2" & (this.DebitCardDDL.SelectedValue == "null" | this.DebitCardDDL.SelectedValue == "")) //debit
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Fill your Payment Type completely.";
            }
            if (this.DiscountIntervalTypeDropDownList.SelectedValue == "1" & this.EndTimeHourDDL.SelectedValue == "0" & this.EndTimeMinuteDDL.SelectedValue == "0")
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Check your End Time of Daily from Discount Interval Type.";
            }
            if (this.DiscountIntervalTypeDropDownList.SelectedValue == "2" & this.fgMondayCheckBox.Checked == false & this.fgTuesdayCheckBox.Checked == false & this.fgWedCheckBox.Checked == false & this.fgThurCheckBox.Checked == false & this.fgFridayCheckBox.Checked == false & this.fgSatCheckBox.Checked == false & this.fgSundayCheckBox.Checked == false) //weekly
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Check the Days of Weekly from Discount Interval Type.";
            }
            if (this.DiscountIntervalTypeDropDownList.SelectedValue == "3") //monthly
            {
                bool checkdate = false;
                DateTime _start = Convert.ToDateTime(this.StartDateTextBox.Text);
                DateTime _end = Convert.ToDateTime(this.EndDateTextBox.Text);
                for (DateTime currDay = _start; currDay <= _end; currDay.AddDays(1))
                {
                    if (currDay.Day == Convert.ToInt16(this.DateTextBox.Text))
                    {
                        checkdate = true;
                        break;
                    }
                    currDay = currDay.AddDays(1);
                }
                if (checkdate == false)
                {
                    this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Fill Correctly the Date of Monthly from Discount Interval Type.";
                }
            }
        }

    }
}