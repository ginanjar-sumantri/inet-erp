using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Promo
{
    public partial class PromoEdit : PromoBase
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ClearData();
                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.PromoCodeTextBox.Attributes.Add("ReadOnly", "True");
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
            for (int i = 0; i < 24; i++)
            {
                this.StartTimeHourDDL.Items.Add(new ListItem(i.ToString()));
                this.EndTimeHourDDL.Items.Add(new ListItem(i.ToString()));
            }
            for (int i = 0; i < 60; i++)
            {
                this.StartTimeMinuteDDL.Items.Add(new ListItem(i.ToString()));
                this.EndTimeMinuteDDL.Items.Add(new ListItem(i.ToString()));
            }
            //this.FgMemberCheckBox.Checked = false;
            //this.PaymentTypeDDL.SelectedIndex = 0;
            //this.PaymentTypeDDL_SelectedIndexChanged(null, null);
            //this.PromoIntervalTypeDropDownList.SelectedValue = "1";
            //this.PromoIntervalTypeDropDownList_SelectedIndexChanged(null, null);
            //this.DateTextBox.Text = "";
            //this.fgMondayCheckBox.Checked = false;
            //this.fgTuesdayCheckBox.Checked = false;
            //this.fgWedCheckBox.Checked = false;
            //this.fgThurCheckBox.Checked = false;
            //this.fgFridayCheckBox.Checked = false;
            //this.fgSatCheckBox.Checked = false;
            //this.fgSundayCheckBox.Checked = false;

            //this.CreditCardTypeDDL.Visible = false;
            //this.CreditCardDDL.Visible = false;
            //this.DebitCardDDL.Visible = false;
            this.ChooseButton.Visible = false;
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

        public void ShowData()
        {
            POSMsPromo _posMsPromo = this._promoBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.PromoCodeTextBox.Text = _posMsPromo.PromoCode;
            this.StartDateTextBox.Text = Convert.ToDateTime(_posMsPromo.StartDate).ToString("yyyy-MM-dd");
            this.EndDateTextBox.Text = Convert.ToDateTime(_posMsPromo.EndDate).ToString("yyyy-MM-dd");
            this.FgMemberCheckBox.Checked = (_posMsPromo.FgMember == false) ? false : true;
            this.PaymentTypeDDL.SelectedValue = _posMsPromo.PaymentType.ToString();
            if (_posMsPromo.PaymentType == 1)
            {
                String _que = this._creditCardBL.GetCreditCardTypeDDL(_posMsPromo.DebitCreditCode);

                this.CreditCardTypeDDL.Visible = true;
                this.ShowCreditCardType();
                this.CreditCardTypeDDL.SelectedValue = _que;

                this.CreditCardDDL.Visible = true;
                this.ShowCreditCard();
                this.CreditCardDDL.SelectedValue = _posMsPromo.DebitCreditCode;
            }
            else if (_posMsPromo.PaymentType == 2)
            {
                this.DebitCardDDL.Visible = true;
                this.ShowDebitCard();
                this.DebitCardDDL.SelectedValue = _posMsPromo.DebitCreditCode;

            }

            String _date = DateTime.Now.ToString("yyyy-MM-dd");

            if (_posMsPromo.PromoIntervalType == "Daily")
            {
                this.PromoIntervalTypeDropDownList.SelectedValue = "1";
                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = true;

                this.StartTimeHourDDL.SelectedValue = Convert.ToDateTime(_posMsPromo.StartTime).Hour.ToString();
                this.EndTimeHourDDL.SelectedValue = Convert.ToDateTime(_posMsPromo.EndTime).Hour.ToString();
                this.StartTimeMinuteDDL.SelectedValue = Convert.ToDateTime(_posMsPromo.StartTime).Minute.ToString();
                this.EndTimeMinuteDDL.SelectedValue = Convert.ToDateTime(_posMsPromo.EndTime).Minute.ToString();

            }
            else if (_posMsPromo.PromoIntervalType == "Weekly")
            {
                this.PromoIntervalTypeDropDownList.SelectedValue = "2";

                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = true;
                this.DailyTable.Visible = false;

                this.fgMondayCheckBox.Checked = Convert.ToBoolean(_posMsPromo.FgMon);
                this.fgTuesdayCheckBox.Checked = Convert.ToBoolean(_posMsPromo.FgTue);
                this.fgWedCheckBox.Checked = Convert.ToBoolean(_posMsPromo.FgWed);
                this.fgThurCheckBox.Checked = Convert.ToBoolean(_posMsPromo.FgThu);
                this.fgFridayCheckBox.Checked = Convert.ToBoolean(_posMsPromo.FgFri);
                this.fgSatCheckBox.Checked = Convert.ToBoolean(_posMsPromo.FgSat);
                this.fgSundayCheckBox.Checked = Convert.ToBoolean(_posMsPromo.FgSun);
            }
            else if (_posMsPromo.PromoIntervalType == "Monthly")
            {
                this.PromoIntervalTypeDropDownList.SelectedValue = "3";

                this.MonthlyTable.Visible = true;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = false;

                this.DateTextBox.Text = _posMsPromo.DateInMonth.ToString();
            }
            this.FgPaymentCheckBox.Checked = (_posMsPromo.FgPayment == 'Y') ? true : false;
            this.FgPaymentCheckBox_CheckedChanged(null, null);
            this.MinimumPaymentTextBox.Text = (_posMsPromo.MinimumPayment == null) ? "0" : Convert.ToDecimal(_posMsPromo.MinimumPayment).ToString("#,#");
            this.FgActiveCheckBox.Checked = (_posMsPromo.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _posMsPromo.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                List<String> _result1 = this._promoBL.CheckDatePromo(Convert.ToByte(this.PaymentTypeDDL.SelectedValue), Convert.ToDateTime(this.StartDateTextBox.Text), Convert.ToDateTime(this.EndDateTextBox.Text));
                if (_result1.Count == 0)
                {
                    POSMsPromo _posMsPromo = this._promoBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                    _posMsPromo.PromoCode = this.PromoCodeTextBox.Text;
                    _posMsPromo.StartDate = Convert.ToDateTime(this.StartDateTextBox.Text);
                    _posMsPromo.EndDate = Convert.ToDateTime(this.EndDateTextBox.Text);
                    _posMsPromo.FgMember = this.FgMemberCheckBox.Checked;
                    _posMsPromo.PaymentType = Convert.ToByte(this.PaymentTypeDDL.SelectedValue);
                    if (this.PaymentTypeDDL.SelectedValue == "1")
                    {
                        _posMsPromo.DebitCreditCode = this.CreditCardDDL.SelectedValue;
                        _posMsPromo.CreditCardTypeCode = this.CreditCardTypeDDL.SelectedValue;
                    }
                    else if (this.PaymentTypeDDL.SelectedValue == "2")
                    {
                        _posMsPromo.DebitCreditCode = this.DebitCardDDL.SelectedValue;
                    }
                    else
                    {
                    }
                    _posMsPromo.PromoIntervalType = this.PromoIntervalTypeDropDownList.SelectedItem.Text;

                    if (this.PromoIntervalTypeDropDownList.SelectedItem.Text == "Daily")
                    {
                        DateTime _start = Convert.ToDateTime(this.StartDateTextBox.Text + " " + this.StartTimeHourDDL.SelectedValue + ":" + this.StartTimeMinuteDDL.SelectedValue + ":00");
                        _posMsPromo.StartTime = _start;
                        DateTime _end = Convert.ToDateTime(this.EndDateTextBox.Text + " " + this.EndTimeHourDDL.SelectedValue + ":" + this.EndTimeMinuteDDL.SelectedValue + ":00");
                        _posMsPromo.EndTime = _end;
                    }
                    else if (this.PromoIntervalTypeDropDownList.SelectedItem.Text == "Weekly")
                    {
                        _posMsPromo.FgMon = Convert.ToBoolean(this.fgMondayCheckBox.Checked);
                        _posMsPromo.FgTue = Convert.ToBoolean(this.fgTuesdayCheckBox.Checked);
                        _posMsPromo.FgWed = Convert.ToBoolean(this.fgWedCheckBox.Checked);
                        _posMsPromo.FgThu = Convert.ToBoolean(this.fgThurCheckBox.Checked);
                        _posMsPromo.FgFri = Convert.ToBoolean(this.fgFridayCheckBox.Checked);
                        _posMsPromo.FgSat = Convert.ToBoolean(this.fgSatCheckBox.Checked);
                        _posMsPromo.FgSun = Convert.ToBoolean(this.fgSundayCheckBox.Checked);
                    }
                    else if (this.PromoIntervalTypeDropDownList.SelectedItem.Text == "Monthly")
                    {
                        _posMsPromo.DateInMonth = Convert.ToByte(this.DateTextBox.Text);
                    }
                    _posMsPromo.FgPayment = (this.FgPaymentCheckBox.Checked == false) ? 'N' : 'Y';
                    _posMsPromo.MinimumPayment = (this.MinimumPaymentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.MinimumPaymentTextBox.Text);
                    _posMsPromo.FgActive = (this.FgActiveCheckBox.Checked == false) ? 'N' : 'Y';
                    _posMsPromo.Remark = this.RemarkTextBox.Text;
                    _posMsPromo.ModifiedBy = HttpContext.Current.User.Identity.Name;
                    _posMsPromo.ModifiedDate = DateTime.Now;

                    bool _result = false;

                    _result = this._promoBL.Edit(_posMsPromo);

                    if (_result == true)
                    {
                        Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                    }
                    else
                    {
                        this.ClearLabel();
                        this.WarningLabel.Text = "You Failed Edit Data";
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
                    this.WarningLabel.Text = _que + " had at that Range Date with same Type Payment, Do you want to continue?";
                    this.ChooseButton.Visible = true;
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void PromoIntervalTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PromoIntervalTypeDropDownList.SelectedValue == "1")
            {
                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = true;
            }
            else if (this.PromoIntervalTypeDropDownList.SelectedValue == "2")
            {
                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = true;
                this.DailyTable.Visible = false;
            }
            else if (this.PromoIntervalTypeDropDownList.SelectedValue == "3")
            {
                this.MonthlyTable.Visible = true;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = false;
            }
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
            bool _cek = false;

            if (this.PromoIntervalTypeDropDownList.SelectedItem.Text == "Monthly")
            {
                if (Convert.ToInt16(this.DateTextBox.Text) <= 31 && Convert.ToInt16(this.DateTextBox.Text) >= 1)
                {
                    _cek = true;
                }
            }
            else
            {
                _cek = true;
            }

            if (_cek)
            {

                POSMsPromo _posMsPromo = this._promoBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _posMsPromo.PromoCode = this.PromoCodeTextBox.Text;
                _posMsPromo.StartDate = Convert.ToDateTime(this.StartDateTextBox.Text);
                _posMsPromo.EndDate = Convert.ToDateTime(this.EndDateTextBox.Text);
                _posMsPromo.FgMember = this.FgMemberCheckBox.Checked;
                _posMsPromo.PaymentType = Convert.ToByte(this.PaymentTypeDDL.SelectedValue);
                if (this.PaymentTypeDDL.SelectedValue == "1")
                {
                    _posMsPromo.DebitCreditCode = this.CreditCardDDL.SelectedValue;
                    _posMsPromo.CreditCardTypeCode = this.CreditCardTypeDDL.SelectedValue;
                }
                else if (this.PaymentTypeDDL.SelectedValue == "2")
                {
                    _posMsPromo.DebitCreditCode = this.DebitCardDDL.SelectedValue;
                }
                else
                {
                }
                _posMsPromo.PromoIntervalType = this.PromoIntervalTypeDropDownList.SelectedItem.Text;

                if (this.PromoIntervalTypeDropDownList.SelectedItem.Text == "Daily")
                {
                    DateTime _start = Convert.ToDateTime(this.StartDateTextBox.Text + " " + this.StartTimeHourDDL.SelectedValue + ":" + this.StartTimeMinuteDDL.SelectedValue + ":00");
                    _posMsPromo.StartTime = _start;
                    DateTime _end = Convert.ToDateTime(this.EndDateTextBox.Text + " " + this.EndTimeHourDDL.SelectedValue + ":" + this.EndTimeMinuteDDL.SelectedValue + ":00");
                    _posMsPromo.EndTime = _end;
                }
                else if (this.PromoIntervalTypeDropDownList.SelectedItem.Text == "Weekly")
                {
                    _posMsPromo.FgMon = Convert.ToBoolean(this.fgMondayCheckBox.Checked);
                    _posMsPromo.FgTue = Convert.ToBoolean(this.fgTuesdayCheckBox.Checked);
                    _posMsPromo.FgWed = Convert.ToBoolean(this.fgWedCheckBox.Checked);
                    _posMsPromo.FgThu = Convert.ToBoolean(this.fgThurCheckBox.Checked);
                    _posMsPromo.FgFri = Convert.ToBoolean(this.fgFridayCheckBox.Checked);
                    _posMsPromo.FgSat = Convert.ToBoolean(this.fgSatCheckBox.Checked);
                    _posMsPromo.FgSun = Convert.ToBoolean(this.fgSundayCheckBox.Checked);
                }
                else if (this.PromoIntervalTypeDropDownList.SelectedItem.Text == "Monthly")
                {
                    _posMsPromo.DateInMonth = Convert.ToByte(this.DateTextBox.Text);
                }
                _posMsPromo.FgPayment = (this.FgPaymentCheckBox.Checked == false) ? 'N' : 'Y';
                _posMsPromo.MinimumPayment = (this.MinimumPaymentTextBox.Text == "") ? 0 : Convert.ToDecimal(this.MinimumPaymentTextBox.Text);
                _posMsPromo.FgActive = (this.FgActiveCheckBox.Checked == false) ? 'N' : 'Y';
                _posMsPromo.Remark = this.RemarkTextBox.Text;
                _posMsPromo.ModifiedBy = HttpContext.Current.User.Identity.Name;
                _posMsPromo.ModifiedDate = DateTime.Now;

                bool _result = false;
                _result = this._promoBL.Edit(_posMsPromo);

                if (_result == true)
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Edit Data";
                }
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
            if (Convert.ToDateTime(this.StartDateTextBox.Text) > Convert.ToDateTime(this.EndDateTextBox.Text))
            {
                this.WarningLabel.Text = this.WarningLabel.Text + " " + "Start Date must less than End Date.";
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