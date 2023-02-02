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
using System.Web.UI.HtmlControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Discount
{
    public partial class DiscountView : DiscountBase
    {
        private POSDiscountBL _posDiscountBL = new POSDiscountBL();
        private PermissionBL _permBL = new PermissionBL();
        private CreditCardBL _creditCardBL = new CreditCardBL();
        private DebitCardBL _debitCardBL = new DebitCardBL();
        private ProductBL _productBL = new ProductBL();
        private CreditCardTypeBL _creditCardTypeBL = new CreditCardTypeBL();
        
        private int _no = 0;
        private int _no2 = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater2_ctl";
        private string _akhir2 = "_ListCheckBox2";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnPosting = "unposting.jpg";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.AddButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Delete.jpg";
                this.DeleteButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Delete.jpg";

                //this.SetButtonPermission();

                this.DiscountConfigCodeTextBox.Attributes.Add("ReadOnly", "True");
                this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
                this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
                this.PaymentTypeTextBox.Attributes.Add("ReadOnly", "True");
                this.DebitCardTextBox.Attributes.Add("ReadOnly", "True");
                this.CreditCardTextBox.Attributes.Add("ReadOnly", "True");
                this.CreditCardTypeTextBox.Attributes.Add("ReadOnly", "True");
                this.DiscountAmountTextBox.Attributes.Add("ReadOnly", "True");

                this.DiscountIntervalTypeTextBox.Attributes.Add("ReadOnly", "True");
                this.MinimumPaymentTextBox.Attributes.Add("ReadOnly", "True");
                this.DateTextBox.Attributes.Add("ReadOnly", "True");
                this.StartTimeHourTextBox.Attributes.Add("ReadOnly", "True");
                this.StartTimeMinuteTextBox.Attributes.Add("ReadOnly", "True");
                this.EndTimeHourTextBox.Attributes.Add("ReadOnly", "True");
                this.EndTimeMinuteTextBox.Attributes.Add("ReadOnly", "True");
                //this.ForcePrintOnDemandCheckBox.Attributes.Add("Enabled", "True");
                this.MinimumPaymentPanel.Visible = false;
                this.ShowData();
            }
        }

        private void ClearLabel()
        {
            this.Label.Text = "";
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == POSDiscountDataMapper.GetStatus(POSDiscountStatus.OnHold).ToString().ToLower())
            {
                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
                else
                {
                    this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == POSDiscountDataMapper.GetStatus(POSDiscountStatus.WaitingForApproval).ToString().ToLower())
            {
                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
                else
                {
                    this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == POSDiscountDataMapper.GetStatus(POSDiscountStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == POSDiscountDataMapper.GetStatus(POSDiscountStatus.Posting).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnPosting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
        }

        public void ShowData()
        {
            POSMsDiscountConfig _posMsDiscountConfig = this._posDiscountBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.DiscountConfigCodeTextBox.Text = _posMsDiscountConfig.DiscountConfigCode;
            this.StartDateTextBox.Text = Convert.ToDateTime(_posMsDiscountConfig.StartDate).ToString("yyyy-MM-dd");
            this.EndDateTextBox.Text = Convert.ToDateTime(_posMsDiscountConfig.EndDate).ToString("yyyy-MM-dd");
            this.AmountTypeLabel.Text = POSDiscountDataMapper.GetAmountTypeText(_posMsDiscountConfig.AmountType);

            this.DiscountAmountTextBox.Text = Convert.ToDecimal(_posMsDiscountConfig.DiscAmount).ToString("##.##");
            this.DiscountCalcTypeLabel.Text = POSDiscountDataMapper.GetCalcTypeText(_posMsDiscountConfig.DiscountCalcType);
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

            if (Convert.ToBoolean(_posMsDiscountConfig.fgMemberDiscount))
            {
                this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
            }
            else
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
            }
            this.ForcePrintOnDemandCheckBox.Checked = Convert.ToBoolean(_posMsDiscountConfig.ForcePrintOnDemand);
            this.PaymentTypeTextBox.Text = POSDiscountPaymentTypeMapper.GetStatusText(Convert.ToInt16(_posMsDiscountConfig.PaymentType)).ToString();
            if (this.PaymentTypeTextBox.Text == "Kredit")
            {
                this.CreditCardTextBox.Text = (_creditCardBL.GetSingle(_posMsDiscountConfig.DebitCreditCode)).CreditCardName;
                this.CreditCardTypeTextBox.Text = (_creditCardTypeBL.GetSingle(_posMsDiscountConfig.CreditCardTypeCode)).CreditCardTypeName;
                this.DebitCardTextBox.Visible = false;
            }
            else if (this.PaymentTypeTextBox.Text == "Debit")
            {
                this.DebitCardTextBox.Text = (_debitCardBL.GetSingle(_posMsDiscountConfig.DebitCreditCode)).DebitCardName;
                this.CreditCardTextBox.Visible = false;
                this.CreditCardTypeTextBox.Visible = false;
            }
            else
            {
                this.CreditCardTextBox.Visible = false;
                this.DebitCardTextBox.Visible = false;
                this.CreditCardTypeTextBox.Visible = false;
            }

            this.StatusLabel.Text = POSDiscountDataMapper.GetStatusText(Convert.ToInt16(_posMsDiscountConfig.Status)).ToString();
            this.DiscountIntervalTypeTextBox.Text = _posMsDiscountConfig.DiscountIntervalType;

            if (this.DiscountIntervalTypeTextBox.Text == "Daily")
            {
                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = true;

                this.StartTimeHourTextBox.Text = Convert.ToDateTime(_posMsDiscountConfig.StartTime).Hour.ToString();
                this.StartTimeMinuteTextBox.Text = Convert.ToDateTime(_posMsDiscountConfig.StartTime).Minute.ToString();
                this.EndTimeHourTextBox.Text = Convert.ToDateTime(_posMsDiscountConfig.EndTime).Hour.ToString();
                this.EndTimeMinuteTextBox.Text = Convert.ToDateTime(_posMsDiscountConfig.EndTime).Minute.ToString();
            }
            else if (this.DiscountIntervalTypeTextBox.Text == "Weekly")
            {
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
            else if (this.DiscountIntervalTypeTextBox.Text == "Monthly")
            {
                this.MonthlyTable.Visible = true;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = false;

                this.DateTextBox.Text = _posMsDiscountConfig.DateInMonth.ToString();
            }

            this.StatusHiddenField.Value = _posMsDiscountConfig.Status.ToString();
            if (this.StatusHiddenField.Value.Trim().ToLower() == POSDiscountDataMapper.GetStatus(POSDiscountStatus.Posting).ToString().ToLower())
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
                this.AddButton2.Visible = false;
                this.DeleteButton2.Visible = false;
                this.EditButton.Visible = false;
            }
            else
            {
                this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
                this.AddButton2.Visible = true;
                this.DeleteButton2.Visible = true;
                this.EditButton.Visible = true;
            }

            ShowDataDetail1();
            ShowDataDetail2();
            ShowActionButton();
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            POSMsDiscountConfig _posMsDiscountConfig = this._posDiscountBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (Convert.ToInt16(this.StatusHiddenField.Value) == 3)
            {
                _posMsDiscountConfig.Status = Convert.ToByte(Convert.ToInt16(this.StatusHiddenField.Value) - 1);
            }
            else
            {
                _posMsDiscountConfig.Status = Convert.ToByte(Convert.ToInt16(this.StatusHiddenField.Value) + 1);
            }

            bool _result = this._posDiscountBL.Edit(_posMsDiscountConfig);

            if (_result == true)
            {
                this.ShowData();
            }
            else
            {
                this.ClearLabel();
                this.Label.Text = "You Failed Edit Data";
            }
        }

        #region Detail1

        public void ShowDataDetail1()
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._posDiscountBL.GetListPOSMsDiscountConfigMember(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");
            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSMsDiscountConfigMember _temp = (POSMsDiscountConfigMember)e.Item.DataItem;

                string _code = _temp.MemberType;

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _noLiteral.Text = _no.ToString();

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");

                //ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                //_viewButton.PostBackUrl = this._viewDetailPage + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
                //_viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                //this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                //if (this._permView == PermissionLevel.NoAccess)
                //{
                //    _viewButton.Visible = false;
                //}

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton.Visible = false;
                }
                else
                {
                    if (this.StatusHiddenField.Value.Trim().ToLower() == POSDiscountDataMapper.GetStatus(POSDiscountStatus.Posting).ToString().ToLower())
                    {
                        _editButton.Visible = false;
                    }
                    else
                    {
                        _editButton.PostBackUrl = this._editDetailPage + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
                        _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                    }
                }

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");
                if (e.Item.ItemType == ListItemType.Item)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
                }
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
                }

                Literal _discountConfigCode = (Literal)e.Item.FindControl("DiscountConfigCodeLiteral");
                _discountConfigCode.Text = HttpUtility.HtmlEncode(_temp.DiscountConfigCode);

                Literal _memberType = (Literal)e.Item.FindControl("MemberTypeLiteral");
                _memberType.Text = HttpUtility.HtmlEncode(_temp.MemberType);


                Decimal _discountAmount = (_temp.DiscountAmount == null) ? 0 : Convert.ToDecimal(_temp.DiscountAmount);
                Literal _discountAmountLiteral = (Literal)e.Item.FindControl("DiscountAmountLiteral");
                _discountAmountLiteral.Text = HttpUtility.HtmlEncode(_discountAmount.ToString("#0.00"));
            }
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.fgMemberDiscountCheckBox.Checked == true)
            {
                Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Cannot Add Data Member, Please Checked Member Discount.');", true);
            }
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Label.Text = "";
            this.Label1.Text = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._posDiscountBL.DeleteMultiPOSMsDiscountConfigMember(_tempSplit, this.DiscountConfigCodeTextBox.Text);

            if (_result == true)
            {
                this.Label.Text = "Delete Success";
            }
            else
            {
                this.Label.Text = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            this.ShowData();
        }

        #endregion

        #region Detail2

        public void ShowDataDetail2()
        {
            this.TempHidden2.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater2.DataSource = null;
            }
            else
            {
                this.ListRepeater2.DataSource = this._posDiscountBL.GetListPOSMsDiscountProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater2.DataBind();

            this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + _tempHidden2 + "' );");
            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            POSMsDiscountProduct _temp = (POSMsDiscountProduct)e.Item.DataItem;

            string _code = _temp.ProductCode;

            if (this.TempHidden2.Value == "")
            {
                this.TempHidden2.Value = _code;
            }
            else
            {
                this.TempHidden2.Value += "," + _code;
            }

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral2");
            _no2 += 1;
            _noLiteral.Text = _no2.ToString();

            CheckBox _listCheckbox;
            _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox2");
            _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "')");

            //ImageButton _viewButton2 = (ImageButton)e.Item.FindControl("ViewButton2");
            //_viewButton2.PostBackUrl = this._viewDetailPage2 + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
            //_viewButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

            //this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            //if (this._permView == PermissionLevel.NoAccess)
            //{
            //    _viewButton2.Visible = false;
            //}

            //ImageButton _editButton2 = (ImageButton)e.Item.FindControl("EditButton2");
            //this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            //if (this._permEdit == PermissionLevel.NoAccess)
            //{
            //    _editButton2.Visible = false;
            //}
            //else
            //{
            //    if (this.StatusHiddenField.Value.Trim().ToLower() == SalaryPaymentDataMapper.GetStatus(SalaryPaymentStatus.Posting).ToString().ToLower())
            //    {
            //        _editButton2.Visible = false;
            //    }
            //    else
            //    {
            //        _editButton2.PostBackUrl = this._editDetailPage2 + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
            //        _editButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
            //    }
            //}

            HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
            _tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");
            if (e.Item.ItemType == ListItemType.Item)
            {
                _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
                _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
            }
            if (e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
                _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
            }

            Literal _discountConfigCode = (Literal)e.Item.FindControl("DiscountConfigCodeLiteral");
            _discountConfigCode.Text = HttpUtility.HtmlEncode(_temp.DiscountConfigCode);

            Literal _productCode = (Literal)e.Item.FindControl("ProductCodeLiteral");
            _productCode.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

            MsProduct _msProduct = this._productBL.GetSingleProduct(HttpUtility.HtmlEncode(_temp.ProductCode));
            
            Literal _productName = (Literal)e.Item.FindControl("ProductNameLiteral");
            _productName.Text = _msProduct.ProductName;

            Literal _sellingPrice = (Literal)e.Item.FindControl("SellingPriceLiteral");
            _sellingPrice.Text = Convert.ToDecimal(_msProduct.SellingPrice).ToString("#0.00");

            Literal _unit = (Literal)e.Item.FindControl("UnitLiteral");
            _unit.Text = _msProduct.Unit;
        }

        protected void AddButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.Label.Text = "";
            this.Label1.Text = "";
            this.WarningLabel2.Text = "";
            string _temp = this.CheckHidden2.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._posDiscountBL.DeleteMultiPOSMsDiscountProduct(_tempSplit, this.DiscountConfigCodeTextBox.Text);

            if (_result == true)
            {
                this.WarningLabel2.Text = "Delete Success";

            }
            else
            {
                this.WarningLabel2.Text = "Delete Failed";
            }

            this.CheckHidden2.Value = "";
            this.AllCheckBox2.Checked = false;

            this.ShowData();
        }

        #endregion
    }
}