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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Discount
{
    public partial class DiscountEdit : DiscountBase
    {
        private POSDiscountBL _posDiscountBL = new POSDiscountBL();
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
            this.DiscountConfigCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
            //this.ReferenceTextBox.Attributes.Add("ReadOnly", "True");
            this.DiscountAmountTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.DiscountAmountTextBox.ClientID + "," + this.DiscountAmountTextBox.ClientID + ",500" + ");");
            this.MinimumPaymentTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.MinimumPaymentTextBox.ClientID + "," + this.MinimumPaymentTextBox.ClientID + ",500" + ");");
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
            this.MinimumPaymentPanel.Visible = false;
            this.CreditCardTypeDDL.Visible = false;
            this.CreditCardDDL.Visible = false;
            this.DebitCardDDL.Visible = false;
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

        public void ShowData()
        {
            POSMsDiscountConfig _posMsDiscountConfig = this._posDiscountBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.DiscountConfigCodeTextBox.Text = _posMsDiscountConfig.DiscountConfigCode;
            this.StartDateTextBox.Text = Convert.ToDateTime(_posMsDiscountConfig.StartDate).ToString("yyyy-MM-dd");
            this.EndDateTextBox.Text = Convert.ToDateTime(_posMsDiscountConfig.EndDate).ToString("yyyy-MM-dd");
            this.DiscountAmountTextBox.Text = Convert.ToDecimal(_posMsDiscountConfig.DiscAmount).ToString("##.##");
            this.AmountTypeRBL.SelectedValue = _posMsDiscountConfig.AmountType;
            this.DiscountTypeRBL.SelectedValue = _posMsDiscountConfig.DiscountCalcType;
            if (_posMsDiscountConfig.DiscountCalcType == "S")
            {
                this.MinimumPaymentPanel.Visible = true;
                this.MinimumPaymentTextBox.Text = Convert.ToDecimal(_posMsDiscountConfig.MinimumPayment).ToString("##.##");
            }
            this.fgMemberDiscountCheckBox.Checked = Convert.ToBoolean(_posMsDiscountConfig.fgMemberDiscount);
            if (this.fgMemberDiscountCheckBox.Checked == false)
            {
                this.DiscountAmountTable.Visible = true;
            }
            else
            {
                this.DiscountAmountTable.Visible = false;
            }

            this.ForcePrintOnDemandCheckBox.Checked = Convert.ToBoolean(_posMsDiscountConfig.ForcePrintOnDemand);
            this.PaymentTypeDDL.SelectedValue = _posMsDiscountConfig.PaymentType.ToString();
            if (_posMsDiscountConfig.PaymentType == 1)
            {
                //this.CreditCardDDL.Visible = true;
                //this.ShowCreditCard();
                //this.CreditCardTypeDDL.SelectedValue = _posMsDiscountConfig.DebitCreditCode;
                String _que = this._creditCardBL.GetCreditCardTypeDDL(_posMsDiscountConfig.DebitCreditCode);

                this.CreditCardTypeDDL.Visible = true;
                this.ShowCreditCardType();
                this.CreditCardTypeDDL.SelectedValue = _que;

                this.CreditCardDDL.Visible = true;
                this.ShowCreditCard();
                this.CreditCardDDL.SelectedValue = _posMsDiscountConfig.DebitCreditCode;
            }
            else if (_posMsDiscountConfig.PaymentType == 2)
            {
                this.DebitCardDDL.Visible = true;
                this.ShowDebitCard();
                this.DebitCardDDL.SelectedValue = _posMsDiscountConfig.DebitCreditCode;

            }

            String _date = DateTime.Now.ToString("yyyy-MM-dd");

            //this.StartTimeTextBox.Text = _date;
            //this.EndTimeTextBox.Text = _date;


            if (_posMsDiscountConfig.DiscountIntervalType == "Daily")
            {
                this.DiscountIntervalTypeDropDownList.SelectedValue = "1";
                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = true;

                //DateTime _startHour = _posMsDiscountConfig.StartTime.ToString();
                this.StartTimeHourDDL.SelectedValue = Convert.ToDateTime(_posMsDiscountConfig.StartTime).Hour.ToString();
                this.EndTimeHourDDL.SelectedValue = Convert.ToDateTime(_posMsDiscountConfig.EndTime).Hour.ToString();
                this.StartTimeMinuteDDL.SelectedValue = Convert.ToDateTime(_posMsDiscountConfig.StartTime).Minute.ToString();
                this.EndTimeMinuteDDL.SelectedValue = Convert.ToDateTime(_posMsDiscountConfig.EndTime).Minute.ToString();

            }
            else if (_posMsDiscountConfig.DiscountIntervalType == "Weekly")
            {
                this.DiscountIntervalTypeDropDownList.SelectedValue = "2";

                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = true;
                this.DailyTable.Visible = false;

                this.fgMondayCheckBox.Checked = Convert.ToBoolean(_posMsDiscountConfig.fgMonday);
                this.fgTuesdayCheckBox.Checked = Convert.ToBoolean(_posMsDiscountConfig.fgTuesday);
                this.fgWedCheckBox.Checked = Convert.ToBoolean(_posMsDiscountConfig.fgWed);
                this.fgThurCheckBox.Checked = Convert.ToBoolean(_posMsDiscountConfig.fgThur);
                this.fgFridayCheckBox.Checked = Convert.ToBoolean(_posMsDiscountConfig.fgFriday);
                this.fgSatCheckBox.Checked = Convert.ToBoolean(_posMsDiscountConfig.fgSat);
                this.fgSundayCheckBox.Checked = Convert.ToBoolean(_posMsDiscountConfig.fgSunday);
            }
            else if (_posMsDiscountConfig.DiscountIntervalType == "Monthly")
            {
                this.DiscountIntervalTypeDropDownList.SelectedValue = "3";

                this.MonthlyTable.Visible = true;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = false;

                this.DateTextBox.Text = _posMsDiscountConfig.DateInMonth.ToString();
            }

        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            //bool _cek = false;

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
            //{}
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                List<String> _result1 = this._posDiscountBL.CheckDateDisc(Convert.ToByte(this.PaymentTypeDDL.SelectedValue), Convert.ToDateTime(this.StartDateTextBox.Text), Convert.ToDateTime(this.EndDateTextBox.Text));
                if (_result1.Count == 0)
                {
                    POSMsDiscountConfig _posMsDiscountConfig = this._posDiscountBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                    _posMsDiscountConfig.DiscountConfigCode = this.DiscountConfigCodeTextBox.Text;
                    _posMsDiscountConfig.StartDate = Convert.ToDateTime(this.StartDateTextBox.Text);
                    _posMsDiscountConfig.EndDate = Convert.ToDateTime(this.EndDateTextBox.Text);
                    _posMsDiscountConfig.AmountType = this.AmountTypeRBL.SelectedValue;
                    if (this.fgMemberDiscountCheckBox.Checked == false)
                    {
                        _posMsDiscountConfig.DiscAmount = Convert.ToDecimal(this.DiscountAmountTextBox.Text);
                    }
                    _posMsDiscountConfig.DiscountCalcType = this.DiscountTypeRBL.SelectedValue;
                    if (this.DiscountTypeRBL.SelectedValue == "S")
                    {
                        _posMsDiscountConfig.MinimumPayment = Convert.ToDecimal(this.MinimumPaymentTextBox.Text);
                    }
                    _posMsDiscountConfig.fgMemberDiscount = this.fgMemberDiscountCheckBox.Checked;
                    _posMsDiscountConfig.ForcePrintOnDemand = this.ForcePrintOnDemandCheckBox.Checked;
                    _posMsDiscountConfig.PaymentType = Convert.ToByte(this.PaymentTypeDDL.SelectedValue);
                    if (this.PaymentTypeDDL.SelectedValue == "1")
                    {
                        _posMsDiscountConfig.DebitCreditCode = this.CreditCardDDL.SelectedValue;
                        _posMsDiscountConfig.CreditCardTypeCode = this.CreditCardTypeDDL.SelectedValue;
                    }
                    else if (this.PaymentTypeDDL.SelectedValue == "2")
                    {
                        _posMsDiscountConfig.DebitCreditCode = this.DebitCardDDL.SelectedValue;
                    }
                    else
                    {
                    }
                    _posMsDiscountConfig.DiscountIntervalType = this.DiscountIntervalTypeDropDownList.SelectedItem.Text;

                    if (this.DiscountIntervalTypeDropDownList.SelectedItem.Text == "Daily")
                    {
                        DateTime _start = Convert.ToDateTime(this.StartDateTextBox.Text + " " + this.StartTimeHourDDL.SelectedValue + ":" + this.StartTimeMinuteDDL.SelectedValue + ":00");
                        _posMsDiscountConfig.StartTime = _start;
                        DateTime _end = Convert.ToDateTime(this.EndDateTextBox.Text + " " + this.EndTimeHourDDL.SelectedValue + ":" + this.EndTimeMinuteDDL.SelectedValue + ":00");
                        _posMsDiscountConfig.EndTime = _end;
                    }
                    else if (this.DiscountIntervalTypeDropDownList.SelectedItem.Text == "Weekly")
                    {
                        _posMsDiscountConfig.fgMonday = Convert.ToBoolean(this.fgMondayCheckBox.Checked);
                        _posMsDiscountConfig.fgTuesday = Convert.ToBoolean(this.fgTuesdayCheckBox.Checked);
                        _posMsDiscountConfig.fgWed = Convert.ToBoolean(this.fgWedCheckBox.Checked);
                        _posMsDiscountConfig.fgThur = Convert.ToBoolean(this.fgThurCheckBox.Checked);
                        _posMsDiscountConfig.fgFriday = Convert.ToBoolean(this.fgFridayCheckBox.Checked);
                        _posMsDiscountConfig.fgSat = Convert.ToBoolean(this.fgSatCheckBox.Checked);
                        _posMsDiscountConfig.fgSunday = Convert.ToBoolean(this.fgSundayCheckBox.Checked);
                    }
                    else if (this.DiscountIntervalTypeDropDownList.SelectedItem.Text == "Monthly")
                    {
                        _posMsDiscountConfig.DateInMonth = Convert.ToByte(this.DateTextBox.Text);
                    }


                    bool _result = false;

                    if (this.fgMemberDiscountCheckBox.Checked == false)
                    {

                        bool _res = _posDiscountBL.CekToPOSMSDiscountConfigMember(this.DiscountConfigCodeTextBox.Text);
                        if (_res == true)
                            _result = this._posDiscountBL.Edit(_posMsDiscountConfig);
                        else
                        {
                            this.WarningLabel.Text = "Please delete data in Discount Config";
                            return;
                        }


                    }
                    else
                        _result = this._posDiscountBL.Edit(_posMsDiscountConfig);

                    if (_result == true)
                    {
                        //Response.Redirect(this._homePage);
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
            if (this.DiscountIntervalTypeDropDownList.SelectedValue == "1")
            {
                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = true;
            }
            else if (this.DiscountIntervalTypeDropDownList.SelectedValue == "2")
            {
                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = true;
                this.DailyTable.Visible = false;
            }
            else if (this.DiscountIntervalTypeDropDownList.SelectedValue == "3")
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

        protected void DiscountTypeRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DiscountTypeRBL.SelectedValue == "S")
            {
                this.MinimumPaymentPanel.Visible = true;
                this.MinimumPaymentRequiredFieldValidator.Visible = true;
            }
            else
            {
                this.MinimumPaymentPanel.Visible = false;
                this.MinimumPaymentRequiredFieldValidator.Visible = false;
            }
        }

        protected void Yes1Button_Click(object sender, EventArgs e)
        {
            bool _cek = false;

            if (this.DiscountIntervalTypeDropDownList.SelectedItem.Text == "Monthly")
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

                POSMsDiscountConfig _posMsDiscountConfig = this._posDiscountBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _posMsDiscountConfig.DiscountConfigCode = this.DiscountConfigCodeTextBox.Text;
                _posMsDiscountConfig.StartDate = Convert.ToDateTime(this.StartDateTextBox.Text);
                _posMsDiscountConfig.EndDate = Convert.ToDateTime(this.EndDateTextBox.Text);
                _posMsDiscountConfig.AmountType = this.AmountTypeRBL.SelectedValue;
                if (this.fgMemberDiscountCheckBox.Checked == false)
                {
                    _posMsDiscountConfig.DiscAmount = Convert.ToDecimal(this.DiscountAmountTextBox.Text);
                }
                _posMsDiscountConfig.DiscountCalcType = this.DiscountTypeRBL.SelectedValue;
                if (this.DiscountTypeRBL.SelectedValue == "S")
                {
                    _posMsDiscountConfig.MinimumPayment = Convert.ToDecimal(this.MinimumPaymentTextBox.Text);
                }
                _posMsDiscountConfig.fgMemberDiscount = this.fgMemberDiscountCheckBox.Checked;
                _posMsDiscountConfig.ForcePrintOnDemand = this.ForcePrintOnDemandCheckBox.Checked;
                _posMsDiscountConfig.PaymentType = Convert.ToByte(this.PaymentTypeDDL.SelectedValue);
                if (this.PaymentTypeDDL.SelectedValue == "1")
                {
                    _posMsDiscountConfig.DebitCreditCode = this.CreditCardDDL.SelectedValue;
                    _posMsDiscountConfig.CreditCardTypeCode = this.CreditCardTypeDDL.SelectedValue;
                }
                else if (this.PaymentTypeDDL.SelectedValue == "2")
                {
                    _posMsDiscountConfig.DebitCreditCode = this.DebitCardDDL.SelectedValue;
                }
                else
                {
                }
                _posMsDiscountConfig.DiscountIntervalType = this.DiscountIntervalTypeDropDownList.SelectedItem.Text;

                if (this.DiscountIntervalTypeDropDownList.SelectedItem.Text == "Daily")
                {
                    DateTime _start = Convert.ToDateTime(this.StartDateTextBox.Text + " " + this.StartTimeHourDDL.SelectedValue + ":" + this.StartTimeMinuteDDL.SelectedValue + ":00");
                    _posMsDiscountConfig.StartTime = _start;
                    DateTime _end = Convert.ToDateTime(this.EndDateTextBox.Text + " " + this.EndTimeHourDDL.SelectedValue + ":" + this.EndTimeMinuteDDL.SelectedValue + ":00");
                    _posMsDiscountConfig.EndTime = _end;
                }
                else if (this.DiscountIntervalTypeDropDownList.SelectedItem.Text == "Weekly")
                {
                    _posMsDiscountConfig.fgMonday = Convert.ToBoolean(this.fgMondayCheckBox.Checked);
                    _posMsDiscountConfig.fgTuesday = Convert.ToBoolean(this.fgTuesdayCheckBox.Checked);
                    _posMsDiscountConfig.fgWed = Convert.ToBoolean(this.fgWedCheckBox.Checked);
                    _posMsDiscountConfig.fgThur = Convert.ToBoolean(this.fgThurCheckBox.Checked);
                    _posMsDiscountConfig.fgFriday = Convert.ToBoolean(this.fgFridayCheckBox.Checked);
                    _posMsDiscountConfig.fgSat = Convert.ToBoolean(this.fgSatCheckBox.Checked);
                    _posMsDiscountConfig.fgSunday = Convert.ToBoolean(this.fgSundayCheckBox.Checked);
                }
                else if (this.DiscountIntervalTypeDropDownList.SelectedItem.Text == "Monthly")
                {
                    _posMsDiscountConfig.DateInMonth = Convert.ToByte(this.DateTextBox.Text);
                }
                bool _result = false;

                if (this.fgMemberDiscountCheckBox.Checked == false)
                {

                    bool _res = _posDiscountBL.CekToPOSMSDiscountConfigMember(this.DiscountConfigCodeTextBox.Text);
                    if (_res == true)
                        _result = this._posDiscountBL.Edit(_posMsDiscountConfig);
                    else
                    {
                        this.WarningLabel.Text = "Please delete data in Discount Config";
                        return;
                    }


                }
                else
                    _result = this._posDiscountBL.Edit(_posMsDiscountConfig);

                if (_result == true)
                {
                    //Response.Redirect(this._homePage);
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