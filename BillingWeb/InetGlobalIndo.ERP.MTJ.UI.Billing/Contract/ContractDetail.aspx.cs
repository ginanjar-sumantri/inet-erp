using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common;
using Microsoft.Reporting.WebForms;
using System.IO;
using Microsoft.Office.Interop.Word;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Contract
{
    public partial class ContractDetail : ContractBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private ContractBL _contractBL = new ContractBL();
        private ContractTemplateBL _contractTemplateBL = new ContractTemplateBL();
        private SalesConfirmationBL _salesConfirmationBL = new SalesConfirmationBL();
        private ReportBillingBL _reportBillingBL = new ReportBillingBL();
        private ReportListBL _reportListBL = new ReportListBL();
        private UserBL _userBL = new UserBL();
        private CompanyConfig _companyConfigBL = new CompanyConfig();

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        //private string _imgPosting = "posting.jpg";
        //private string _imgUnposting = "unposting.jpg";
        private string _imgPreview = "preview.jpg";

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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                BILTrContract _bilTrContract = _contractBL.GetSingleContract(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                if (_bilTrContract.Status >= ContractDataMapper.GetStatus(TransStatus.Approved))
                {
                    this.EditButton.Visible = false;
                }
                else
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                }
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                //this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowContractTemplateDDL();
                this.ShowData();
            }
        }

        protected void ShowContractTemplateDDL()
        {
            this.ContractTemplateDDL.Items.Clear();
            this.ContractTemplateDDL.DataTextField = "TemplateName";
            this.ContractTemplateDDL.DataValueField = "TemplateID";
            this.ContractTemplateDDL.DataSource = this._contractTemplateBL.GetListContractTemplateForDDL();
            this.ContractTemplateDDL.DataBind();
            this.ContractTemplateDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            BILTrContract _bilTrContract = _contractBL.GetSingleContract(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            BILTrSalesConfirmation _bilTrSalesConfirmation = _salesConfirmationBL.GetSingleSalesConfirmation(_bilTrContract.SalesConfirmationNoRef);

            this.TransNmbrTextBox.Text = _bilTrContract.TransNmbr;
            this.FileNmbrTextBox.Text = _bilTrContract.FileNmbr;
            this.DateTextBox.Text = Convert.ToDateTime(_bilTrContract.TransDate).ToString("dd-MM-yyyy");
            this.SalesConfirmationNoRefTextBox.Text = _bilTrSalesConfirmation.FileNmbr;
            this.CompanyNameTextBox.Text = _bilTrContract.CompanyName;
            this.ResponsibleNameTextBox.Text = _bilTrContract.ResponsibleName;
            this.TitleNameTextBox.Text = _bilTrContract.TitleName;
            this.LetteProviderInformationTextBox.Text = _bilTrContract.LetterProviderInformation;
            this.LetteCustomerInformationTextBox.Text = _bilTrContract.LetterCustomerInformation;
            this.FinaceCustomerPICTextBox.Text = _bilTrContract.FinanceCustomerPIC;
            this.FinanceCustomerPhoneTextBox.Text = _bilTrContract.FinanceCustomerPhone;
            this.FinanceCustomerFaxTextBox.Text = _bilTrContract.FinanceCustomerFax;
            this.FinanceCustomerEmailTextBox.Text = _bilTrContract.FinanceCustomerEmail;
            this.StatusLable.Text = ContractDataMapper.GetStatusText(Convert.ToByte(_bilTrContract.Status));
            this.StatusHiddenField.Value = _bilTrContract.Status.ToString();

            this.ShowActionButton();
            this.ShowPreviewButton();

            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == ContractDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                this.EditButton.Visible = true;

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == ContractDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                this.EditButton.Visible = true;

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == ContractDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.Visible = false;
                //this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                //this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                //this.EditButton.Visible = true;

                //if (this._permPosting == PermissionLevel.NoAccess)
                //{
                //    this.ActionButton.Visible = false;
                //}
            }
            //else if (this.StatusHiddenField.Value.Trim().ToLower() == ContractDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            //{
            //    this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

            //    this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

            //    this.EditButton.Visible = false;

            //    if (this._permUnposting == PermissionLevel.NoAccess)
            //    {
            //        this.ActionButton.Visible = false;
            //    }
            //}
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            //string[] _date = this.DateTextBox.Text.Split('-');
            DateTime _date = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _date.Year; //Convert.ToInt32(_date[2]);
            int _period = _date.Month; //Convert.ToInt32(_date[1]);

            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == ContractDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                string _result = this._contractBL.GetAppr(this.TransNmbrTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == ContractDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result = this._contractBL.Approve(this.TransNmbrTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            //else if (this.StatusHiddenField.Value.Trim().ToLower() == DeliveryOrderDataMapper.GetStatus(DeliveryOrderStatus.Approved).ToString().ToLower())
            //{
            //    string _result = this._deliveryOrderBL.Posting(this.TransNmbrTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

            //    this.WarningLabel.Text = _result;
            //}
            //else if (this.StatusHiddenField.Value.Trim().ToLower() == DeliveryOrderDataMapper.GetStatus(DeliveryOrderStatus.Posted).ToString().ToLower())
            //{
            //    string _result = this._deliveryOrderBL.Unposting(this.TransNmbrTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

            //    this.WarningLabel.Text = _result;
            //}

            this.ShowData();
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ContractTemplateDDL.SelectedValue != "null")
            {
                BILMsContractTemplateHd _contractTemplateHd = this._contractTemplateBL.GetSingleContractTemplateHd(this.ContractTemplateDDL.SelectedValue);

                object missing = System.Reflection.Missing.Value;

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
                Microsoft.Office.Interop.Word.Document aDoc = null;

                object _fileName = ApplicationConfig.ContractTemplateImportPath + _contractTemplateHd.TemplateFileName;
                String[] _break = _contractTemplateHd.TemplateFileName.Split('.');
                object _fileName2 = ApplicationConfig.ContractTemplateImportPath + _break[0] + " edited" + "." + "pdf";
                String _filePath = ApplicationConfig.ContractTemplateVirDirPath + _break[0] + " edited" + "." + "pdf";

                if (File.Exists((String)_fileName))
                {
                    DateTime _today = DateTime.Now;

                    object _readOnly = false;
                    object _isVisible = false;

                    wordApp.Visible = false;

                    aDoc = wordApp.Documents.Open(ref _fileName, ref missing,
                        ref _readOnly, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref _isVisible, ref missing, ref missing,
                        ref missing, ref missing);

                    aDoc.Activate();

                    this.ReplaceWords(ref wordApp, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                    //aDoc.Content.InsertBefore("this is the beginning\r\n\r\n");
                    //aDoc.Content.InsertAfter("\r\n\r\nthis is the end");
                }
                else
                {
                    this.WarningLabel.Text = "File does not exist";
                    return;
                }

                //aDoc.SaveAs(ref _fileName2, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                //    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                aDoc.ExportAsFixedFormat((String)_fileName2, WdExportFormat.wdExportFormatPDF, false, WdExportOptimizeFor.wdExportOptimizeForPrint, WdExportRange.wdExportAllDocument, 1, 1, WdExportItem.wdExportDocumentContent, false, false, WdExportCreateBookmarks.wdExportCreateNoBookmarks, false, false, false, ref missing);

                object _saveChanges = false;

                if (aDoc != null)
                {
                    aDoc.Close(ref _saveChanges, ref missing, ref missing);
                    aDoc = null;
                }

                if (wordApp != null)
                {
                    wordApp.Quit(ref missing, ref missing, ref missing);
                    wordApp = null;
                }

                //Response.ContentType = "Application/pdf";
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + _filePath);
                //Response.TransmitFile(Server.MapPath((String)_fileName2));
                //Response.TransmitFile(_filePath);
                //Response.Clear();
                //Response.ContentType = "Application/pdf";
                //Response.WriteFile((String)_fileName2);
                //Response.Flush();
                //Response.End();
            }
            else
            {
                this.WarningLabel.Text = "Template Not Selected";
            }
        }

        private void ReplaceWords(ref Microsoft.Office.Interop.Word.Application wordApp, String _prmContractTransNmbr)
        {
            BILTrContract _bilTrContract = _contractBL.GetSingleContract(_prmContractTransNmbr);
            BILTrSalesConfirmation _bilTrSalesConfirmation = _salesConfirmationBL.GetSingleSalesConfirmation(_bilTrContract.SalesConfirmationNoRef);

            DateTime _now = DateTime.Now;
            //CustomerBL _custBL = new CustomerBL();
            //MsCustomer _cust = _custBL.GetSingleCust(_bilTrSalesConfirmation.CustCode);
            this.FindAndReplace(wordApp, "@COMPANYNAME@", _bilTrContract.CompanyName);
            this.FindAndReplace(wordApp, "@RESPONSIBLENAME@", _bilTrContract.ResponsibleName);
            this.FindAndReplace(wordApp, "@RESPONSIBLETITLE@", _bilTrContract.TitleName);
            
            this.FindAndReplace(wordApp, "@DateNowDay@", _now.Day.ToString());
            this.FindAndReplace(wordApp, "@DateNowDays@", DayMapper.GetDayNameINA(_now.DayOfWeek));
            this.FindAndReplace(wordApp, "@DateNowMonth@", _now.Month.ToString());
            this.FindAndReplace(wordApp, "@DateNowYear@", _now.Year.ToString());
            this.FindAndReplace(wordApp, "@DateNowLongString@", _now.ToString("dd - MM - yyyy"));

            this.FindAndReplace(wordApp, "@NoKontrak@", _bilTrContract.FileNmbr);
            this.FindAndReplace(wordApp, "@SCCustEmail@", _bilTrSalesConfirmation.ResponsibleEmailAddress);
            this.FindAndReplace(wordApp, "@SCCustFax@", _bilTrSalesConfirmation.CompanyFax);
            this.FindAndReplace(wordApp, "@SCCustPhone@", _bilTrSalesConfirmation.CompanyPhone);
            this.FindAndReplace(wordApp, "@SCCustAddress@", _bilTrSalesConfirmation.CompanyAddress);

            this.FindAndReplace(wordApp, "@ConFinanceCustPIC@", _bilTrContract.FinanceCustomerPIC);
            this.FindAndReplace(wordApp, "@ConFinanceCustPhone@", _bilTrContract.FinanceCustomerPhone);
            this.FindAndReplace(wordApp, "@ConFinanceCustEmail@", _bilTrContract.FinanceCustomerEmail);
            this.FindAndReplace(wordApp, "@SCTechCustName@", _bilTrSalesConfirmation.TechnicalPIC);
            this.FindAndReplace(wordApp, "@SCTechCustPhone@", _bilTrSalesConfirmation.TechnicalPhone);
            this.FindAndReplace(wordApp, "@SCTechCustEmail@", _bilTrSalesConfirmation.TechnicalEmail);
        }

        private void FindAndReplace(Microsoft.Office.Interop.Word.Application WordApp, object _findText, object _replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            WordApp.Selection.Find.Execute(ref _findText, ref matchCase, ref matchWholeWord, ref matchWildCards, ref matchSoundLike,
                ref matchAllWordForms, ref forward, ref wrap, ref format, ref _replaceWithText, ref replace, ref matchKashida,
                ref matchDiacritics, ref matchAlefHamza, ref matchControl);            
        }

        public void ShowPreviewButton()
        {
            this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._permPrintPreview == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == UnSubDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == UnSubDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == UnSubDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                //else if (this.StatusHiddenField.Value.Trim().ToLower() == UnSubDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                //{
                //    this.PreviewButton.Visible = true;
                //    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                //}
            }
        }
    }
}