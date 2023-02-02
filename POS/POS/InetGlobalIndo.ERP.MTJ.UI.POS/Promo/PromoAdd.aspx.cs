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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Promo
{
    public partial class PromoAdd : PromoBase
    {
        private PromoBL _promoBL = new PromoBL();
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
            this.MinimumPaymentTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.MinimumPaymentTextBox.ClientID + ")");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();
            this.PromoCodeTextBox.Text = "";

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

            this.FgMemberCheckBox.Checked = false;
            this.PaymentTypeDDL.SelectedIndex = 0;
            this.PaymentTypeDDL_SelectedIndexChanged(null, null);
            this.PromoIntervalTypeDropDownList.SelectedValue = "1";
            this.PromoIntervalTypeDropDownList_SelectedIndexChanged(null, null);
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
            this.ChooseButton.Visible = false;
            this.FgActiveCheckBox.Checked = false;
            this.FgPaymentCheckBox.Checked = false;
            this.FgPaymentCheckBox_CheckedChanged(null, null);
            this.MinimumPaymentTextBox.Text = "0";
            this.RemarkTextBox.Text = "";

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
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                List<String> _result1 = this._promoBL.CheckDatePromo(Convert.ToByte(this.PaymentTypeDDL.SelectedValue), Convert.ToDateTime(this.StartDateTextBox.Text), Convert.ToDateTime(this.EndDateTextBox.Text));
                if (_result1.Count == 0)
                {
                    POSMsPromo _posPromo = new POSMsPromo();

                    _posPromo.PromoCode = this.PromoCodeTextBox.Text;
                    _posPromo.StartDate = Convert.ToDateTime(this.StartDateTextBox.Text);
                    _posPromo.EndDate = Convert.ToDateTime(this.EndDateTextBox.Text);
                    _posPromo.FgMember = this.FgMemberCheckBox.Checked;
                    _posPromo.PaymentType = Convert.ToByte(this.PaymentTypeDDL.SelectedValue);
                    if (this.PaymentTypeDDL.SelectedValue == "1")
                    {
                        _posPromo.DebitCreditCode = this.CreditCardDDL.SelectedValue;
                        _posPromo.CreditCardTypeCode = this.CreditCardTypeDDL.SelectedValue;

                    }
                    else if (this.PaymentTypeDDL.SelectedValue == "2")
                    {
                        _posPromo.DebitCreditCode = this.DebitCardDDL.SelectedValue;
                    }
                    else
                    {
                    }
                    _posPromo.Status = POSPromoDataMapper.GetStatus(POSPromoStatus.OnHold);
                    _posPromo.PromoIntervalType = this.PromoIntervalTypeDropDownList.SelectedItem.Text;
                    if (this.PromoIntervalTypeDropDownList.SelectedItem.ToString() == "Daily")
                    {
                        DateTime _start = Convert.ToDateTime(this.StartDateTextBox.Text + " " + this.StartTimeHourDDL.SelectedValue + ":" + this.StartTimeMinuteDDL.SelectedValue + ":00");
                        _posPromo.StartTime = _start;
                        DateTime _end = Convert.ToDateTime(this.EndDateTextBox.Text + " " + this.EndTimeHourDDL.SelectedValue + ":" + this.EndTimeMinuteDDL.SelectedValue + ":00");
                        _posPromo.EndTime = _end;
                    }
                    else if (this.PromoIntervalTypeDropDownList.SelectedItem.ToString() == "Monthly")
                        _posPromo.DateInMonth = Convert.ToByte(((this.DateTextBox.Text == "") ? 0 : Convert.ToInt16(this.DateTextBox.Text)));
                    else if (this.PromoIntervalTypeDropDownList.SelectedItem.ToString() == "Weekly")
                    {
                        _posPromo.FgMon = this.fgMondayCheckBox.Checked;
                        _posPromo.FgTue = this.fgTuesdayCheckBox.Checked;
                        _posPromo.FgWed = this.fgWedCheckBox.Checked;
                        _posPromo.FgThu = this.fgThurCheckBox.Checked;
                        _posPromo.FgFri = this.fgFridayCheckBox.Checked;
                        _posPromo.FgSat = this.fgSatCheckBox.Checked;
                        _posPromo.FgSun = this.fgSundayCheckBox.Checked;
                    }
                    _posPromo.FgActive = (this.FgActiveCheckBox.Checked == false) ? 'N' : 'Y';
                    _posPromo.FgPayment = (this.FgPaymentCheckBox.Checked == false) ? 'N' : 'Y';
                    _posPromo.MinimumPayment = (this.MinimumPaymentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.MinimumPaymentTextBox.Text);

                    _posPromo.Remark = this.RemarkTextBox.Text;
                    _posPromo.CreatedBy = HttpContext.Current.User.Identity.Name;
                    _posPromo.CreatedDate = DateTime.Now;

                    bool _result = this._promoBL.Add(_posPromo);

                    if (_result == true)
                    {
                        Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.PromoCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
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

                    this.ClearLabel();
                    this.WarningLabel.Text = _que + " had at that Range Date with same Type Payment, Do you want to continue?";
                    this.ChooseButton.Visible = true;
                }
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

        protected void PromoIntervalTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PromoIntervalTypeDropDownList.SelectedValue == "1") //daily
            {
                ClearDataUpdatePanel();

                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = true;
            }
            else if (this.PromoIntervalTypeDropDownList.SelectedValue == "2") //weekly
            {
                ClearDataUpdatePanel();

                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = true;
                this.DailyTable.Visible = false;
            }
            else if (this.PromoIntervalTypeDropDownList.SelectedValue == "3") //monthly
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

        protected void Yes1Button_Click(object sender, EventArgs e)
        {
            POSMsPromo _posPromo = new POSMsPromo();

            _posPromo.PromoCode = this.PromoCodeTextBox.Text;
            _posPromo.StartDate = Convert.ToDateTime(this.StartDateTextBox.Text);
            _posPromo.EndDate = Convert.ToDateTime(this.EndDateTextBox.Text);
            _posPromo.FgMember = this.FgMemberCheckBox.Checked;
            _posPromo.PaymentType = Convert.ToByte(this.PaymentTypeDDL.SelectedValue);
            if (this.PaymentTypeDDL.SelectedValue == "1")
            {
                _posPromo.DebitCreditCode = this.CreditCardDDL.SelectedValue;
                _posPromo.CreditCardTypeCode = this.CreditCardTypeDDL.SelectedValue;
            }
            else if (this.PaymentTypeDDL.SelectedValue == "2")
            {
                _posPromo.DebitCreditCode = this.DebitCardDDL.SelectedValue;
            }
            else
            {
            }
            _posPromo.Status = POSPromoDataMapper.GetStatus(POSPromoStatus.OnHold);
            _posPromo.PromoIntervalType = this.PromoIntervalTypeDropDownList.SelectedItem.Text;
            if (this.PromoIntervalTypeDropDownList.SelectedItem.ToString() == "Daily")
            {
                DateTime _start = Convert.ToDateTime(this.StartDateTextBox.Text + " " + this.StartTimeHourDDL.SelectedValue + ":" + this.StartTimeMinuteDDL.SelectedValue + ":00");
                _posPromo.StartTime = _start;
                DateTime _end = Convert.ToDateTime(this.EndDateTextBox.Text + " " + this.EndTimeHourDDL.SelectedValue + ":" + this.EndTimeMinuteDDL.SelectedValue + ":00");
                _posPromo.EndTime = _end;
            }
            else if (this.PromoIntervalTypeDropDownList.SelectedItem.ToString() == "Monthly")
                _posPromo.DateInMonth = Convert.ToByte(((this.DateTextBox.Text == "") ? 0 : Convert.ToInt16(this.DateTextBox.Text)));
            else if (this.PromoIntervalTypeDropDownList.SelectedItem.ToString() == "Weekly")
            {
                _posPromo.FgMon = this.fgMondayCheckBox.Checked;
                _posPromo.FgTue = this.fgTuesdayCheckBox.Checked;
                _posPromo.FgWed = this.fgWedCheckBox.Checked;
                _posPromo.FgThu = this.fgThurCheckBox.Checked;
                _posPromo.FgFri = this.fgFridayCheckBox.Checked;
                _posPromo.FgSat = this.fgSatCheckBox.Checked;
                _posPromo.FgSun = this.fgSundayCheckBox.Checked;
            }
            _posPromo.FgActive = (this.FgActiveCheckBox.Checked == false) ? 'N' : 'Y';
            _posPromo.FgPayment = (this.FgPaymentCheckBox.Checked == false) ? 'N' : 'Y';
            _posPromo.MinimumPayment = (this.MinimumPaymentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.MinimumPaymentTextBox.Text);
            _posPromo.Remark = this.RemarkTextBox.Text;
            _posPromo.CreatedBy = HttpContext.Current.User.Identity.Name;
            _posPromo.CreatedDate = DateTime.Now;

            bool _result = this._promoBL.Add(_posPromo);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.PromoCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
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
            POSMsPromo _posPromo = this._promoBL.GetSingle(this.PromoCodeTextBox.Text);
            if (_posPromo != null)
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Promo Config Code already use by an other data.";
            }
            if (this.PaymentTypeDDL.SelectedValue == "1" & (this.CreditCardTypeDDL.SelectedValue == "null" | this.CreditCardDDL.SelectedValue == "null" | this.CreditCardDDL.SelectedValue == "")) //kredit
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Fill your Payment Type completely.";
            }
            if (this.PaymentTypeDDL.SelectedValue == "2" & (this.DebitCardDDL.SelectedValue == "null" | this.DebitCardDDL.SelectedValue == "")) //debit
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Fill your Payment Type completely.";
            }
            if (this.PromoIntervalTypeDropDownList.SelectedValue == "1" & this.EndTimeHourDDL.SelectedValue == "0" & this.EndTimeMinuteDDL.SelectedValue == "0")
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Check your End Time of Daily from Promo Interval Type.";
            }
            if (this.PromoIntervalTypeDropDownList.SelectedValue == "2" & this.fgMondayCheckBox.Checked == false & this.fgTuesdayCheckBox.Checked == false & this.fgWedCheckBox.Checked == false & this.fgThurCheckBox.Checked == false & this.fgFridayCheckBox.Checked == false & this.fgSatCheckBox.Checked == false & this.fgSundayCheckBox.Checked == false) //weekly
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Check the Days of Weekly from Promo Interval Type.";
            }
            if (this.PromoIntervalTypeDropDownList.SelectedValue == "3") //monthly
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
                    this.WarningLabel.Text = this.WarningLabel.Text + " " + "Please Fill Correctly the Date of Monthly from Promo Interval Type.";
                }
            }
        }

        protected void FgPaymentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.FgPaymentCheckBox.Checked == true)
            {
                this.MinimumPaymentTextBox.Attributes.Remove("ReadOnly");
                this.MinimumPaymentTextBox.Attributes.Add("Style", "background-color:");
            }
            else
            {
                this.MinimumPaymentTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
                this.MinimumPaymentTextBox.Attributes.Add("ReadOnly", "true");
                this.MinimumPaymentTextBox.Text = "0";
            }
        }
    }
}