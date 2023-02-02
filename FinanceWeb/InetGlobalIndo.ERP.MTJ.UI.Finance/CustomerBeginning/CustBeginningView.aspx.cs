using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;
using Microsoft.Reporting.WebForms;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerBeginning
{
    public partial class CustBeginningView : CustomerBeginningBase
    {
        private FINBeginCIBL _finBeginCIBL = new FINBeginCIBL();
        private CustomerBL _custBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();
        private ReportTourBL _reportTourBL = new ReportTourBL();

        private string _reportPath1 = "CustomerBeginning/JournalEntryPrintPreview.rdlc";
        private string _reportPath2 = "CustomerBeginning/JournalEntryPrintPreviewHomeCurr.rdlc";
        
        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgCreateJurnal = "view_journal.jpg.jpg";

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

                this.ClearLabel();
                this.ShowData();

                this.SetButtonPermission();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowData()
        {
            _finBeginCIBL = new FINBeginCIBL(); 
            FINBeginCI _FINBeginCI = this._finBeginCIBL.GetSingleFINBeginCI(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_FINBeginCI.CurrCode);

            this.InvoiceNoTextBox.Text = _FINBeginCI.InvoiceNo;
            this.DateTextBox.Text = DateFormMapper.GetValue(_FINBeginCI.TransDate);
            this.StatusLabel.Text = CustBeginningDataMapper.GetStatusText(_FINBeginCI.Status);
            this.StatusHiddenField.Value = _FINBeginCI.Status.ToString();
            this.CustTextBox.Text = _custBL.GetNameByCode(_FINBeginCI.CustCode);
            this.CurrCodeTextBox.Text = _FINBeginCI.CurrCode;
            this.CurrRateTextBox.Text = _FINBeginCI.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.CustPONoTextBox.Text = _FINBeginCI.CustPONo;
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_FINBeginCI.DueDate);
            this.BaseForexTextBox.Text = (_FINBeginCI.BaseForex == 0) ? "0" : _FINBeginCI.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNTextBox.Text = (_FINBeginCI.PPN == 0) ? "0" : _FINBeginCI.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_FINBeginCI.PPNForex == 0) ? "0" : _FINBeginCI.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_FINBeginCI.TotalForex == 0) ? "0" : _FINBeginCI.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.CurrTextBox.Text = _FINBeginCI.CurrCode;
            this.RemarkTextBox.Text = _FINBeginCI.Remark;
            this.TermTextBox.Text = _FINBeginCI.Term.ToString();
            this.PPNDateTextBox.Text = (_FINBeginCI.PPNDate == null) ? "" : DateFormMapper.GetValue(_FINBeginCI.PPNDate);
            this.PPNNoTextBox.Text = _FINBeginCI.PPNNo;
            this.PPNRateTextBox.Text = (_FINBeginCI.PPNRate == null) ? "0" : Convert.ToDecimal(_FINBeginCI.PPNRate).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.BillToTextBox.Text = _custBL.GetNameByCode(_FINBeginCI.BillTo);

            if (_FINBeginCI.Status != CustBeginningDataMapper.GetStatusByte(TransStatus.Posted))
            {
                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.EditButton.Visible = true;
            }
            else
            {
                this.EditButton.Visible = false;
            }
            this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

            this.ShowActionButton();

            this.ShowCreateJurnalButton();
            this.Panel1.Visible = true;
            this.Panel3.Visible = false;
            this.Panel4.Visible = false;
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == CustBeginningDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;
                
                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustBeginningDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustBeginningDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustBeginningDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
        }

        public void ShowCreateJurnalButton()
        {
            this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

            if (this._permPosting == PermissionLevel.NoAccess)
            {
                this.CreateJurnalImageButton.Visible = false;
            }
            else
            {
                this.CreateJurnalImageButton.Visible = false;
                this.CreateJurnalImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgCreateJurnal;

                if (this.StatusHiddenField.Value.Trim().ToLower() == POSTicketingDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    this.CreateJurnalImageButton.Visible = true;
                }
            }
        }

        protected void CreateJurnalImageButton_Click(object sender, ImageClickEventArgs e)
        {

            if (this.CreateJurnalDDL.SelectedValue == "1")
            {
                this.Panel1.Visible = false;
                this.Panel4.Visible = false;
                this.Panel3.Visible = true;

                this.ReportViewer2.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource1 = this._reportTourBL.JournalTicketingPrintPreview(this.InvoiceNoTextBox.Text);

                this.ReportViewer2.LocalReport.DataSources.Clear();
                this.ReportViewer2.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer2.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
                this.ReportViewer2.DataBind();
                ReportParameter[] _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("Nmbr", this.InvoiceNoTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                this.ReportViewer2.LocalReport.SetParameters(_reportParam);
                this.ReportViewer2.LocalReport.Refresh();
            }
            else if (this.CreateJurnalDDL.SelectedValue == "2")
            {
                this.Panel1.Visible = false;
                this.Panel4.Visible = true;
                this.Panel3.Visible = false;

                this.ReportViewer3.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource2 = this._reportTourBL.JournalTicketingPrintPreview(this.InvoiceNoTextBox.Text);

                this.ReportViewer3.LocalReport.DataSources.Clear();
                this.ReportViewer3.LocalReport.DataSources.Add(_reportDataSource2);
                this.ReportViewer3.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
                this.ReportViewer3.DataBind();
                ReportParameter[] _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("Nmbr", this.InvoiceNoTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                this.ReportViewer3.LocalReport.SetParameters(_reportParam);
                this.ReportViewer3.LocalReport.Refresh();
            }
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
            //string[] _date = this.DateTextBox.Text.Split('-');
            //int _year = Convert.ToInt32(_date[0]);
            //int _period = Convert.ToInt32(_date[1]);
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == CustBeginningDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                string _result2 = this._finBeginCIBL.GetAppr(this.InvoiceNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustBeginningDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result2 = this._finBeginCIBL.Approve(this.InvoiceNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustBeginningDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                string _result2 = this._finBeginCIBL.Posting(this.InvoiceNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result2;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustBeginningDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result2 = this._finBeginCIBL.Unposting(this.InvoiceNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                //this.WarningLabel.Text = _result2;
            }

            this.ShowData();
        }

        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string _result2 = this._finBeginCIBL.Unposting(this.InvoiceNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result2 == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.ARRate), this.InvoiceNoTextBox.Text, this.CustPONoTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
                if (_result1 == true)
                    this.WarningLabel.Text = _result2;
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
            }
            else
            {
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
                this.WarningLabel.Text = _result2;
            }

            this.ShowData();
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }
    }
}