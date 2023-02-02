using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.CustomControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAssetMoving
{
    public partial class FixedAssetMovingDetail : FixedAssetMovingBase
    {
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
        private JournalEntryBL _journalEntryBL = new JournalEntryBL();
        private ReportBL _reportBL = new ReportBL();
        private PermissionBL _permBL = new PermissionBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();

        private string _reportPath1 = "FixedAssetMoving/FAMovePrintPreview.rdlc";       

        private int _no = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgPreview = "preview.jpg";        

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
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowData();
                this.ShowDataDetail();
            }
        }

        private void SetButtonPermission()
        {
            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.AddButton.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
            this.Label.Text = "";
        }

        public void ShowDataDetail()
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
                this.ListRepeater.DataSource = this._fixedAssetBL.GetListFixedAssetMovingDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.sortField.Value, Convert.ToBoolean(this.ascDesc.Value));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
        }

        public void ShowPreviewButton()
        {
            this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;

            this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._permPrintPreview == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.OnHold).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.Approved).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                }
            }
        }        

        public void ShowData()
        {
            this._fixedAssetBL = new FixedAssetsBL();
            GLFAMoveHd _glFAMoveHd = this._fixedAssetBL.GetSingleFixedAssetMoving(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.NoTextBox.Text = _glFAMoveHd.TransNmbr;
            this.FileNmbrTextBox.Text = _glFAMoveHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_glFAMoveHd.TransDate);
            this.SourceTypeDropDownList.SelectedValue = _glFAMoveHd.FALocationTypeSrc;
            this.SourceNameTextBox.Text = _fixedAssetBL.GetFALocNameByLocTypeAndCode(FixedAssetsDataMapper.GetValueFALocation(_glFAMoveHd.FALocationTypeSrc), _glFAMoveHd.FALocationCodeSrc).Name;

            this.DestinationTypeDropDownList.SelectedValue = _glFAMoveHd.FALocationTypeDest;
            this.DestinationNameTextBox.Text = _fixedAssetBL.GetFALocNameByLocTypeAndCode(FixedAssetsDataMapper.GetValueFALocation(_glFAMoveHd.FALocationTypeDest), _glFAMoveHd.FALocationCodeDest).Name;

            this.OperatorTextBox.Text = _glFAMoveHd.Operator;
            this.RemarkTextBox.Text = _glFAMoveHd.Remark;
            this.StatusLabel.Text = FixedAssetMovingStatus.GetStatusText(_glFAMoveHd.Status);
            this.StatusHiddenField.Value = _glFAMoveHd.Status.ToString();

            if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
                this.EditButton.Visible = false;
            }
            else
            {
                this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
                this.EditButton.Visible = true;
            }

            this.ShowActionButton();
            this.ShowPreviewButton();
            
            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            GLFAMoveDt _temp = (GLFAMoveDt)e.Item.DataItem;

            string _code = _temp.FACode.ToString();

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
            _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeTrans + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.NoTextBox.Text, ApplicationConfig.EncryptionKey));
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
                if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    _editButton.Visible = false;
                }
                else
                {
                    _editButton.PostBackUrl = this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeTrans + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.NoTextBox.Text, ApplicationConfig.EncryptionKey));
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

            Literal _faCode = (Literal)e.Item.FindControl("FACodeLiteral");
            _faCode.Text = HttpUtility.HtmlEncode(_temp.FACode);

            Literal _faName = (Literal)e.Item.FindControl("FANameLiteral");
            _faName.Text = HttpUtility.HtmlEncode(_temp.FAName);

            Literal _remark = (Literal)e.Item.FindControl("RemarkLiteral");
            _remark.Text = HttpUtility.HtmlEncode(_temp.Remark);
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._reportBL.FAMovePrintPreview(this.NoTextBox.Text);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[2];
            _reportParam[0] = new ReportParameter("Nmbr", this.NoTextBox.Text, true);
            _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                string _result2 = this._fixedAssetBL.GetApprovalFAMoving(this.NoTextBox.Text, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result2 = this._fixedAssetBL.ApproveFAMoving(this.NoTextBox.Text, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                string _result2 = this._fixedAssetBL.PostingFAMoving(this.NoTextBox.Text, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetMovingStatus.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result2 = this._fixedAssetBL.UnpostingFAMoving(this.NoTextBox.Text, HttpContext.Current.User.Identity.Name);

                //this.Label.Text = _result2;
            }

            this.ShowData();
            this.ShowDataDetail();
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_addDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _error = "";
            string _page = "0";

            this.ClearLabel();

            bool _result = this._fixedAssetBL.DeleteMultiFixedAssetMovingDt(_tempSplit, this.NoTextBox.Text);

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error + "&" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void field1_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Fixed Asset Code")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Fixed Asset Code";
                this.ascDesc.Value = "true";
            }
            this.ShowDataDetail();
        }
        protected void field2_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Fixed Asset Name")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Fixed Asset Name";
                this.ascDesc.Value = "true";
            }
            this.ShowDataDetail();
        }
        protected void field3_Click(object sender, EventArgs e)
        {
            if (this.sortField.Value == "Remark")
            {
                if (this.ascDesc.Value == "" || this.ascDesc.Value == "false")
                    this.ascDesc.Value = "true";
                else
                    this.ascDesc.Value = "false";
            }
            else
            {
                this.sortField.Value = "Remark";
                this.ascDesc.Value = "true";
            }
            this.ShowDataDetail();
        }        

        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string _result = this._fixedAssetBL.UnpostingFAMoving(this.NoTextBox.Text, HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.FAMoving), this.NoTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
                if (_result1 == true)
                    this.WarningLabel.Text = _result;
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
            }
            else
            {
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
                this.WarningLabel.Text = _result;
            }

            this.ShowData();
            this.ShowDataDetail();
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }
    }
}