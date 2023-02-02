using System;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web.UI;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using System.Net.Mail;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.IO;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Notification
{
    public partial class NotifyBilInvNYSend : NotifyBase
    {
        private NotificationBL _notificationBL = new NotificationBL();
        private PermissionBL _permBL = new PermissionBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _currPageKey = "CurrentPage";

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDBilInvEmailNYSend, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleBilInvEmailNYSendLiteral;

                this.ViewState[this._currPageKey] = 0;

                this.SearchImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/searchBtn.jpg";
                this.SendEmailImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/send_email.jpg";

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData(0);
            }
        }

        protected void ClearLabel()
        {
            DateTime _now = DateTime.Now;

            this.WarningLabel.Text = "";
            this.PeriodTextBox.Text = _now.Month.ToString();
            this.YearTextBox.Text = _now.Year.ToString();
            this.InvoiceNoTextBox.Text = "";
            this.CustNameTextBox.Text = "";
        }

        private void SetAttribute()
        {
            this.YearTextBox.Attributes.Add("OnKeyDown", "return  NumericWithEnter();");
            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return  NumericWithEnter();");
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._notificationBL.RowCountBilInvEmailNYSend(Convert.ToInt32(this.PeriodTextBox.Text), Convert.ToInt32(this.YearTextBox.Text), this.InvoiceNoTextBox.Text, this.CustNameTextBox.Text);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private Boolean IsCheckedAll()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.ListRepeater.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.ListRepeater.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        private Boolean IsChecked(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHidden.Value.Split(',');

            for (int i = 0; i < _value.Length; i++)
            {
                if (_prmValue == _value[i])
                {
                    _result = true;
                    break;
                }
            }

            return _result;
        }

        private void ShowPage(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount();

            if (_prmCurrentPage - _maxlength > 0)
            {
                min = _prmCurrentPage - _maxlength;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength < q)
            {
                max = _prmCurrentPage + _maxlength + 1;
            }
            else
            {
                max = Convert.ToDecimal(q);
            }

            if (_prmCurrentPage > 0)
                _addElement += 2;

            if (_prmCurrentPage < q - 1)
                _addElement += 2;

            i = Convert.ToInt32(min);
            _pageNumber = new Int32[Convert.ToInt32(max - min) + _addElement];
            if (_pageNumber.Length != 0)
            {
                //NB: Prev Or First
                if (_prmCurrentPage > 0)
                {
                    this._navMark[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[0]);
                    _pageNumberElement++;

                    this._navMark[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag = true;
                    }

                    this._navMark[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();

                _flag = true;
                _nextFlag = false;
                _lastFlag = false;
                _navMark = _navMarkBackup;

                //this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                //                                          select _query;
                //this.DataPagerBottomRepeater.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();

                //this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                //                                          select _query;
                //this.DataPagerBottomRepeater.DataBind();
            }
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowData(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton");
                    _pageNumberLinkButton.CommandName = "DataPager";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark[0] = null;
                    }
                    else if (_pageNumber == this._navMark[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark[1] = null;
                    }
                    else if (_pageNumber == this._navMark[2] && this._flag == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark[2] = null;
                        this._nextFlag = true;
                        this._flag = true;
                    }
                    else if (_pageNumber == this._navMark[3] && this._flag == true && this._nextFlag == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark[3] = null;
                        this._lastFlag = true;
                    }
                    else
                    {
                        if (this._lastFlag == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark[2] && this._flag == true)
                            this._flag = false;
                    }
                }
            }
        }

        protected void DataPagerButton_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount()) - 1;
                        break;
                    }
                    else if (_reqPage < 0)
                    {
                        ((TextBox)_item.Controls[3]).Text = "1";
                        _reqPage = 0;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            this.ViewState[this._currPageKey] = _reqPage;

            this.ShowData(_reqPage);
        }

        private void ShowData(Int32 _prmCurrentPage)
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuIDBilInvEmailNYSend, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._notificationBL.GetListBilInvEmailNYSend(_prmCurrentPage, _maxrow, Convert.ToInt32(this.PeriodTextBox.Text), Convert.ToInt32(this.YearTextBox.Text), this.InvoiceNoTextBox.Text, this.CustNameTextBox.Text);
            }
            this.ListRepeater.DataBind();

            this.AllHidden.Value = this._notificationBL.GetListBilInvEmailNYSend(Convert.ToInt32(this.PeriodTextBox.Text), Convert.ToInt32(this.YearTextBox.Text), this.InvoiceNoTextBox.Text, this.CustNameTextBox.Text);

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Billing_InvoiceHd _temp = (Billing_InvoiceHd)e.Item.DataItem;
                string _code = _temp.InvoiceHd.ToString();

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_code);

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

                Literal _invoiceNoLiteral = (Literal)e.Item.FindControl("InvoiceNoLiteral");
                _invoiceNoLiteral.Text = HttpUtility.HtmlEncode(_temp.TransNmbr);

                Literal _periodLiteral = (Literal)e.Item.FindControl("PeriodLiteral");
                _periodLiteral.Text = HttpUtility.HtmlEncode(_temp.Period.ToString());

                Literal _yearLiteral = (Literal)e.Item.FindControl("YearLiteral");
                _yearLiteral.Text = HttpUtility.HtmlEncode(_temp.Year.ToString());

                Literal _custNameLiteral = (Literal)e.Item.FindControl("CustNameLiteral");
                _custNameLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                Literal _custMailLiteral = (Literal)e.Item.FindControl("EmailLiteral");
                _custMailLiteral.Text = HttpUtility.HtmlEncode(_temp.CustEmail);
            }
        }

        protected void SearchImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            this.WarningLabel.Text = "";
            this.ShowData(0);
        }

        protected void SendEmailImageButton_Click(object sender, ImageClickEventArgs e)
        {
            String _result = "";
            String _sendTransNmbr = "";

            if (this.GrapAllCheckBox.Checked == false)
            {
                _sendTransNmbr = this.CheckHidden.Value;
            }
            else
            {
                _sendTransNmbr = this.AllHidden.Value;
            }

            if (_sendTransNmbr != "")
            {
                Boolean _errorExist = false;
                String _errCodes = "";
                this.Panel2.Visible = false;

                EmailNotificationSetupBL _emailSetupBL = new EmailNotificationSetupBL();
                CustomerBL _custBL = new CustomerBL();
                BillingInvoiceBL _bilInvBL = new BillingInvoiceBL();


                String[] _split = _sendTransNmbr.Split(',');
                for (int i = 0; i < _split.Length; i++)
                {
                    String[] _codes = _split[i].Split('=');
                    String _code = _codes[0];
                    BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.BillingInvoiceEmailNotYetSent));

                    try
                    {
                        //render report viewer --------------------------------------------------------
                        ReportBillingBL _reportBillingBL = new ReportBillingBL();
                        CompanyConfig _compConfig = new CompanyConfig();
                        String _reportPath = _compConfig.GetSingle(CompanyConfigure.RDLCBillingInvoiceSendEmail).SetValue;
                        //String _reportPath1 = "Notification/BillingInvoicePrintPreview.rdlc";
                        this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                        ReportDataSource _reportDataSource1 = _reportBillingBL.BillingInvoicePrintPreview(_code);

                        this.ReportViewer1.LocalReport.EnableExternalImages = true;
                        this.ReportViewer1.LocalReport.DataSources.Clear();
                        this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

                        Guid _compId = new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name);
                        String _companyTag = new UserBL().GetCompanyTag(_compId);

                        this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath;

                        String _image = _compConfig.GetSingle(CompanyConfigure.BillingAuthorizedSignImage).SetValue;
                        String _headerImage = _compConfig.GetSingle(CompanyConfigure.BillingHeaderImage).SetValue;
                        String _leftImage = _compConfig.GetSingle(CompanyConfigure.BillingLeftImage).SetValue;
                        String _footerImage = _compConfig.GetSingle(CompanyConfigure.BillingFooterImage).SetValue;

                        this.ReportViewer1.DataBind();

                        ReportParameter[] _reportParam = new ReportParameter[6];
                        _reportParam[0] = new ReportParameter("InvoiceNo", _code, true);
                        _reportParam[1] = new ReportParameter("CompanyTag", _companyTag, true);
                        _reportParam[2] = new ReportParameter("Image", ApplicationConfig.HomeWebAppURL + "images/" + _image, false);
                        _reportParam[3] = new ReportParameter("HeaderImage", ApplicationConfig.HomeWebAppURL + "images/" + _headerImage, false);
                        _reportParam[4] = new ReportParameter("LeftImage", ApplicationConfig.HomeWebAppURL + "images/" + _leftImage, false);
                        _reportParam[5] = new ReportParameter("FooterImage", ApplicationConfig.HomeWebAppURL + "images/" + _footerImage, false);

                        this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                        this.ReportViewer1.LocalReport.Refresh();

                        String DeviceInfo = "<DeviceInfo>"
                                            + "  <OutputFormat>PDF</OutputFormat>"
                                            + "  <PageWidth>8.5in</PageWidth>"
                                            + "  <PageHeight>11.7in</PageHeight>"
                                            + "  <MarginTop>0.0in</MarginTop>"
                                            + "  <MarginLeft>0.0in</MarginLeft>"
                                            + "  <MarginRight>0.0in</MarginRight>"
                                            + "  <MarginBottom>0.0in</MarginBottom>"
                                            + "</DeviceInfo>";
                        String[] streamids;
                        String mimeType = "";
                        String encoding = "";
                        String extension = "";
                        Warning[] warnings;
                        Byte[] _reportOutput = this.ReportViewer1.LocalReport.Render("PDF", DeviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);

                        File.Delete(ApplicationConfig.PDFPath + "BillingInvoice.pdf");

                        FileStream _fileStream = new FileStream(ApplicationConfig.PDFPath + "BillingInvoice.pdf", FileMode.Create);
                        _fileStream.Write(_reportOutput, 0, _reportOutput.Length);
                        _fileStream.Flush();
                        _fileStream.Close();
                        _fileStream.Dispose();

                        String _custCode = _bilInvBL.GetSingleBillingInvoiceHd(new Guid(_code)).CustCode;
                        MsCustomer _msCust = _custBL.GetSingleCust(_custCode);
                        if (_msCust.Email != "")
                        {
                            //send mail to external
                            MailMessage _msgMail = new MailMessage();
                            _msgMail.To.Add(new MailAddress(_msCust.Email));
                            _msgMail.From = new MailAddress(_emailSetup.EmailFrom);
                            Attachment _attach = new Attachment(ApplicationConfig.PDFPath + "BillingInvoice.pdf");
                            _msgMail.Attachments.Add(_attach);
                            _msgMail.Subject = _emailSetup.Subject;
                            _msgMail.IsBodyHtml = true;

                            String _oldBody = _emailSetup.BodyMessage ?? "";
                            _msgMail.Body = _oldBody;

                            SmtpClient _smtp = new SmtpClient();
                            _smtp.Send(_msgMail);

                            _attach.Dispose();
                            File.Delete(ApplicationConfig.PDFPath + "BillingInvoice.pdf");

                            //update bill invoice already notified
                            _bilInvBL.UpdateFgSendEmail(_code, true);
                            this.WarningLabel.Text = "Sending " + (i + 1).ToString() + " of " + _split.Length.ToString() + " mail(s)";
                        }
                        else
                        {
                            this.WarningLabel.Text = "Some Transaction doesn't have email, Please fill in related information";
                        }
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        String _transNmbr = _bilInvBL.GetSingleBillingInvoiceHd(new Guid(_code)).TransNmbr;
                        if (_errCodes == "")
                        {
                            _errCodes = _transNmbr;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _transNmbr;
                        }
                    }

                    if (_errorExist == true)
                    {
                        _result = "Cannot send email(s) from transaction " + _errCodes + " due to technical error";
                    }
                }

                if (_result != "")
                {
                    this.WarningLabel.Text = _result;
                }
                else
                {
                    this.CheckHidden.Value = "";
                    this.WarningLabel.Text = "You succesfully sent email(s)";
                }

                this.ShowData(0);
            }
        }
    }
}