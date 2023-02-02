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

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Promo
{
    public partial class PromoView : PromoBase
    {
        private PromoBL _promoBL = new PromoBL();
        private PermissionBL _permBL = new PermissionBL();
        private CreditCardBL _creditCardBL = new CreditCardBL();
        private DebitCardBL _debitCardBL = new DebitCardBL();
        private ProductBL _productBL = new ProductBL();
        private CreditCardTypeBL _creditCardTypeBL = new CreditCardTypeBL();

        private int _no = 0;
        private int _no2 = 0;
        private int _no3 = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater2_ctl";
        private string _akhir2 = "_ListCheckBox2";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        private string _awal3 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater3_ctl";
        private string _akhir3 = "_ListCheckBox3";
        private string _cbox3 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox3";
        private string _tempHidden3 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden3";

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
                this.SetAttribute();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
            this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
            this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
            this.AddButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
            this.AddButton3.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
            this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Delete.jpg";
            this.DeleteButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Delete.jpg";
            this.DeleteButton3.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Delete.jpg";

            this.PromoCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
            this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
            this.PaymentTypeTextBox.Attributes.Add("ReadOnly", "True");
            this.DebitCardTextBox.Attributes.Add("ReadOnly", "True");
            this.CreditCardTextBox.Attributes.Add("ReadOnly", "True");
            this.CreditCardTypeTextBox.Attributes.Add("ReadOnly", "True");
            this.PromoIntervalTypeTextBox.Attributes.Add("ReadOnly", "True");

            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.StartTimeHourTextBox.Attributes.Add("ReadOnly", "True");
            this.StartTimeMinuteTextBox.Attributes.Add("ReadOnly", "True");
            this.EndTimeHourTextBox.Attributes.Add("ReadOnly", "True");
            this.EndTimeMinuteTextBox.Attributes.Add("ReadOnly", "True");
            this.MinimumPaymentTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("ReadOnly", "True");
        }

        private void ClearWarningLabel()
        {
            this.WarningLabel.Text = "";
            this.WarningLabel1.Text = "";
            this.WarningLabel2.Text = "";
            this.WarningLabel3.Text = "";
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
            if (this.StatusHiddenField.Value.Trim().ToLower() == POSPromoDataMapper.GetStatus(POSPromoStatus.OnHold).ToString().ToLower())
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
            else if (this.StatusHiddenField.Value.Trim().ToLower() == POSPromoDataMapper.GetStatus(POSPromoStatus.WaitingForApproval).ToString().ToLower())
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
            else if (this.StatusHiddenField.Value.Trim().ToLower() == POSPromoDataMapper.GetStatus(POSPromoStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == POSPromoDataMapper.GetStatus(POSPromoStatus.Posting).ToString().ToLower())
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
            POSMsPromo _posMsPromo = this._promoBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.PromoCodeTextBox.Text = _posMsPromo.PromoCode;
            this.StartDateTextBox.Text = Convert.ToDateTime(_posMsPromo.StartDate).ToString("yyyy-MM-dd");
            this.EndDateTextBox.Text = Convert.ToDateTime(_posMsPromo.EndDate).ToString("yyyy-MM-dd");
            this.FgMemberCheckBox.Checked = (_posMsPromo.FgMember == false) ? false : true;
            if (Convert.ToBoolean(_posMsPromo.FgMember))
            {
                this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
            }
            else
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
            }
            this.PaymentTypeTextBox.Text = POSPromoPaymentTypeMapper.GetStatusText(Convert.ToInt16(_posMsPromo.PaymentType)).ToString();
            if (this.PaymentTypeTextBox.Text == "Kredit")
            {
                this.CreditCardTextBox.Text = (_creditCardBL.GetSingle(_posMsPromo.DebitCreditCode)).CreditCardName;
                this.CreditCardTypeTextBox.Text = (_creditCardTypeBL.GetSingle(_posMsPromo.CreditCardTypeCode)).CreditCardTypeName;
                this.DebitCardTextBox.Visible = false;
            }
            else if (this.PaymentTypeTextBox.Text == "Debit")
            {
                this.DebitCardTextBox.Text = (_debitCardBL.GetSingle(_posMsPromo.DebitCreditCode)).DebitCardName;
                this.CreditCardTextBox.Visible = false;
                this.CreditCardTypeTextBox.Visible = false;
            }
            else
            {
                this.CreditCardTextBox.Visible = false;
                this.DebitCardTextBox.Visible = false;
                this.CreditCardTypeTextBox.Visible = false;
            }

            this.StatusLabel.Text = POSPromoDataMapper.GetStatusText(Convert.ToInt16(_posMsPromo.Status)).ToString();
            this.PromoIntervalTypeTextBox.Text = _posMsPromo.PromoIntervalType;

            if (this.PromoIntervalTypeTextBox.Text == "Daily")
            {
                this.MonthlyTable.Visible = false;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = true;

                this.StartTimeHourTextBox.Text = Convert.ToDateTime(_posMsPromo.StartTime).Hour.ToString();
                this.StartTimeMinuteTextBox.Text = Convert.ToDateTime(_posMsPromo.StartTime).Minute.ToString();
                this.EndTimeHourTextBox.Text = Convert.ToDateTime(_posMsPromo.EndTime).Hour.ToString();
                this.EndTimeMinuteTextBox.Text = Convert.ToDateTime(_posMsPromo.EndTime).Minute.ToString();
            }
            else if (this.PromoIntervalTypeTextBox.Text == "Weekly")
            {
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
            else if (this.PromoIntervalTypeTextBox.Text == "Monthly")
            {
                this.MonthlyTable.Visible = true;
                this.WeeklyTable.Visible = false;
                this.DailyTable.Visible = false;

                this.DateTextBox.Text = _posMsPromo.DateInMonth.ToString();
            }

            this.StatusHiddenField.Value = _posMsPromo.Status.ToString();
            if (this.StatusHiddenField.Value.Trim().ToLower() == POSPromoDataMapper.GetStatus(POSPromoStatus.Posting).ToString().ToLower())
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
                this.AddButton2.Visible = false;
                this.DeleteButton2.Visible = false;
                this.AddButton3.Visible = false;
                this.DeleteButton3.Visible = false;
                this.EditButton.Visible = false;
            }
            else
            {
                this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
                this.AddButton2.Visible = true;
                this.DeleteButton2.Visible = true;
                this.DeleteButton3.Visible = true;
                this.EditButton.Visible = true;
                this.EditButton.Visible = true;
            }
            this.FgPaymentCheckBox.Checked = (_posMsPromo.FgPayment == 'Y') ? true : false;
            this.MinimumPaymentTextBox.Text = Convert.ToDecimal(_posMsPromo.MinimumPayment).ToString("#,#");
            this.FgActiveCheckBox.Checked = (_posMsPromo.FgActive == 'Y') ? true : false;
            this.RemarkTextBox.Text = _posMsPromo.Remark;

            ShowDataDetail1();
            ShowDataDetail2();
            ShowDataDetail3();
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
            this.ClearWarningLabel();
            POSMsPromo _posMsPromo = this._promoBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (Convert.ToInt16(this.StatusHiddenField.Value) == 3)
            {
                _posMsPromo.Status = Convert.ToByte(Convert.ToInt16(this.StatusHiddenField.Value) - 1);
            }
            else
            {
                _posMsPromo.Status = Convert.ToByte(Convert.ToInt16(this.StatusHiddenField.Value) + 1);
            }

            bool _result = this._promoBL.Edit(_posMsPromo);

            if (_result == true)
            {
                this.ShowData();
            }
            else
            {
                this.ClearWarningLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
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
                this.ListRepeater.DataSource = this._promoBL.GetListPOSMsPromoMember(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");
            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSMsPromoMember _temp = (POSMsPromoMember)e.Item.DataItem;

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

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                if (this._permView == PermissionLevel.NoAccess)
                {
                    _viewButton.Visible = false;
                }

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton.Visible = false;
                }
                else
                {
                    if (this.StatusHiddenField.Value.Trim().ToLower() == POSPromoDataMapper.GetStatus(POSPromoStatus.Posting).ToString().ToLower())
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

                Literal _memberType = (Literal)e.Item.FindControl("MemberTypeLiteral");
                _memberType.Text = HttpUtility.HtmlEncode(_temp.MemberType);

                Literal _amountType = (Literal)e.Item.FindControl("AmountTypeLiteral");
                _amountType.Text = (_temp.AmountType == 'P') ? "Percentage" : "Amount";

                Decimal _amount = (_temp.Amount == null) ? 0 : Convert.ToDecimal(_temp.Amount);
                Literal _amountLiteral = (Literal)e.Item.FindControl("AmountLiteral");
                _amountLiteral.Text = HttpUtility.HtmlEncode(_amount.ToString("#0.00"));
            }
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.FgMemberCheckBox.Checked == true)
            {
                Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Cannot Add Data Member, Please Checked Member Promo.');", true);
            }
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            this.WarningLabel.Text = "";
            this.WarningLabel1.Text = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearWarningLabel();

            bool _result = this._promoBL.DeleteMultiPOSMsPromoMember(_tempSplit, this.PromoCodeTextBox.Text);

            if (_result == true)
            {
                this.WarningLabel.Text = "Delete Success";
            }
            else
            {
                this.WarningLabel.Text = "Delete Failed";
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
                this.ListRepeater2.DataSource = this._promoBL.GetListPOSMsPromoProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater2.DataBind();

            this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + _tempHidden2 + "' );");
            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            POSMsPromoProduct _temp = (POSMsPromoProduct)e.Item.DataItem;

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

            ImageButton _viewButton2 = (ImageButton)e.Item.FindControl("ViewButton2");
            _viewButton2.PostBackUrl = this._viewDetailPage2 + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
            _viewButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                _viewButton2.Visible = false;
            }

            ImageButton _editButton2 = (ImageButton)e.Item.FindControl("EditButton2");
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                _editButton2.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == POSPromoDataMapper.GetStatus(POSPromoStatus.Posting).ToString().ToLower())
                {
                    _editButton2.Visible = false;
                }
                else
                {
                    _editButton2.PostBackUrl = this._editDetailPage2 + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
                    _editButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
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

            Literal _productCode = (Literal)e.Item.FindControl("ProductCodeLiteral");
            _productCode.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

            Literal _productName = (Literal)e.Item.FindControl("ProductNameLiteral");
            _productName.Text = this._productBL.GetProductNameByCode(_temp.ProductCode);

            Literal _freeProduct = (Literal)e.Item.FindControl("FreeProductLiteral");
            _freeProduct.Text = _temp.FreeProductCode + " - " + this._productBL.GetProductNameByCode(_temp.FreeProductCode);

            Literal _freeQty = (Literal)e.Item.FindControl("FreeQtyLiteral");
            _freeQty.Text = Convert.ToDecimal(_temp.FreeQty).ToString("#,#");

            Literal _discount = (Literal)e.Item.FindControl("DiscountLiteral");
            _discount.Text = "-";
            if (_temp.AmountType == "P" | _temp.AmountType == "A")
                _discount.Text = ((_temp.AmountType == "P") ? "Percentage : " : "Amount : ") + Convert.ToDecimal(_temp.Amount).ToString("#,#");
        }

        protected void AddButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.WarningLabel.Text = "";
            this.WarningLabel1.Text = "";
            this.WarningLabel2.Text = "";
            string _temp = this.CheckHidden2.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearWarningLabel();

            bool _result = this._promoBL.DeleteMultiPOSMsPromoProduct(_tempSplit, this.PromoCodeTextBox.Text);

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

        #region Detail3

        public void ShowDataDetail3()
        {
            this.TempHidden3.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater3.DataSource = null;
            }
            else
            {
                this.ListRepeater3.DataSource = this._promoBL.GetListPOSMsPromoFreeProduct(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater3.DataBind();

            this.AllCheckBox3.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox3.ClientID + ", " + this.CheckHidden3.ClientID + ", '" + _awal3 + "', '" + _akhir3 + "', '" + _tempHidden3 + "' );");
            this.DeleteButton3.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            POSMsPromoFreeProduct _temp = (POSMsPromoFreeProduct)e.Item.DataItem;

            string _code = _temp.FreeProductCode;

            if (this.TempHidden3.Value == "")
            {
                this.TempHidden3.Value = _code;
            }
            else
            {
                this.TempHidden3.Value += "," + _code;
            }

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral3");
            _no3 += 1;
            _noLiteral.Text = _no3.ToString();

            CheckBox _listCheckbox;
            _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox3");
            _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden3.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal3 + "', '" + _akhir3 + "', '" + _cbox3 + "')");

            ImageButton _viewButton3 = (ImageButton)e.Item.FindControl("ViewButton3");
            _viewButton3.PostBackUrl = this._viewDetailPage3 + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
            _viewButton3.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                _viewButton3.Visible = false;
            }

            ImageButton _editButton3 = (ImageButton)e.Item.FindControl("EditButton3");
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                _editButton3.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == POSPromoDataMapper.GetStatus(POSPromoStatus.Posting).ToString().ToLower())
                {
                    _editButton3.Visible = false;
                }
                else
                {
                    _editButton3.PostBackUrl = this._editDetailPage3 + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
                    _editButton3.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
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

            Literal _productName = (Literal)e.Item.FindControl("FreeProductCodeLiteral");
            _productName.Text = _temp.FreeProductCode;

            Literal _freeProduct = (Literal)e.Item.FindControl("FreeProductNameLiteral");
            _freeProduct.Text = this._productBL.GetProductNameByCode(_temp.FreeProductCode);

            Literal _freeQty = (Literal)e.Item.FindControl("FreeQtyLiteral");
            _freeQty.Text = Convert.ToDecimal(_temp.FreeQty).ToString("#,#");

        }

        protected void AddButton3_Click(object sender, ImageClickEventArgs e)
        {
            if (this.FgPaymentCheckBox.Checked == true)
                Response.Redirect(this._addDetailPage3 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msgbox", "javascript:alert('Cannot Add Data Free Product, Please Checked FgPayment.');", true);
        }

        protected void DeleteButton3_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden3.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearWarningLabel();

            bool _result = this._promoBL.DeleteMultiPOSMsPromoFreeProduct(_tempSplit, this.PromoCodeTextBox.Text);

            if (_result == true)
            {
                this.WarningLabel3.Text = "Delete Success";

            }
            else
            {
                this.WarningLabel3.Text = "Delete Failed";
            }

            this.CheckHidden3.Value = "";
            this.AllCheckBox3.Checked = false;

            this.ShowData();
        }

        #endregion
    }
}