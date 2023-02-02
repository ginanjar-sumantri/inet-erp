using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using Microsoft.Reporting.WebForms;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.BillingBeritaAcara
{
    public partial class BeritaAcaraView : BillingBeritaAcaraBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private BeritaAcaraBL _beritaAcaraBL = new BeritaAcaraBL();
        private ReportBillingBL _reportBL = new ReportBillingBL();

        private String _reportPath1 = "BeritaAcara/BeritaAcaraPrintPreview.rdlc";

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

            if (Request.QueryString["updateStatus"] == "success")
                this.WarningLabel.Text = "Update Status Success.";

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/preview.jpg";
                switch (_beritaAcaraBL.GetBeritaAcaraStatus(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)))
                {
                    case 0:
                        this.ApproveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/get_approval.jpg";
                        break;
                    case 1:
                        this.ApprovedByCustomerNameTextBox.ReadOnly = false;
                        this.validatorCustomerApprover.Enabled = true;
                        this.ApproveButton.CausesValidation = true;
                        this.ApproveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/approve.jpg";
                        break;
                    case 2:
                        this.ApproveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/posting.jpg";
                        this.PreviewButton.Visible = true;
                        break;
                    case 3:
                        this.ApproveButton.Visible = false;
                        this.EditButton.Visible = false;
                        this.PreviewButton.Visible = true;
                        break;
                }
                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ApproveButton.Visible = false;
                }

                this.SetButtonPermission();
                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }

        }

        private void ShowData()
        {
            BILTrBeritaAcara _bilTrBeritaAcara = this._beritaAcaraBL.GetSingleBeritaAcara(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransDateTextBox.Text = Convert.ToDateTime(_bilTrBeritaAcara.TransDate).ToString("yyyy-MM-dd");

            this.SalesConfirmationTextBox.Text = _beritaAcaraBL.GetSalesConfirmationFileNumber(_bilTrBeritaAcara.SalesConfirmationNoRef);
            //this.ContractNoTextBox.Text = _beritaAcaraBL.GetContractFileNmbrByContractTransNmbr( _bilTrBeritaAcara.NoKontrak );
            this.TechincalAccountTextBox.Text = _bilTrBeritaAcara.TechnicalAccountMRTG;
            this.TechincalAccountPasswordTextBox.Text = _bilTrBeritaAcara.TechnicalAccountMRTGPassword;

            this.TechnicalIPAllocationTextBox.Text = _bilTrBeritaAcara.TechnicalIPAllocation;
            this.TechnicalTransmisionTextBox.Text = _bilTrBeritaAcara.TechnicalTransmision;
            this.TechnicalServiceSpesificationTextBox.Text = _bilTrBeritaAcara.TechnicalServiceSpesification;
            this.TechnicalBandwidthINTTextBox.Text = _bilTrBeritaAcara.TechnicalBandwidthINT.ToString();

            this.TechnicalBandwidthLocalLoopTextBox.Text = _bilTrBeritaAcara.TechnicalBandwidthLocalLoop.ToString();
            this.TechnicalBandwidthINTRatioUpstreamTextBox.Text = _bilTrBeritaAcara.TechnicalBandwidthINTRatioUpstream.ToString();
            this.TechnicalBandwidthINTRatioDownstreamTextBox.Text = _bilTrBeritaAcara.TechnicalBandwidthINTRatioDownstream.ToString();
            this.TechnicalTerminationPointTextBox.Text = _bilTrBeritaAcara.TechnicalTerminationPoint;

            this.MikrotikPPTPUserNameTextBox.Text = _bilTrBeritaAcara.MikrotikPPTPUserName;
            this.MikrotikPPPOEUserNameTextBox.Text = _bilTrBeritaAcara.MikrotikPPPOEUserName;
            this.MikrotikHotspotUserNameTextBox.Text = _bilTrBeritaAcara.MikrotikHotspotUserName;
            this.MikrotikQueueNameDownLinkTextBox.Text = _bilTrBeritaAcara.MikrotikQueueNameDownLink;

            this.MikrotikQueueNameUpLinkTextBox.Text = _bilTrBeritaAcara.MikrotikQueueNameUpLink;
            this.CollocationRackNoTextBox.Text = _bilTrBeritaAcara.CollocationRackNo;
            this.CollocationServerPositionNoTextBox.Text = _bilTrBeritaAcara.CollocationServerPositionNo;
            this.BGPASNumberTextBox.Text = _bilTrBeritaAcara.BGPASNumber;

            this.BGPIPAddressRouterTextBox.Text = _bilTrBeritaAcara.BGPIPAddressRouter;
            this.BGPAdvertiseIPTextBox.Text = _bilTrBeritaAcara.BGPAdvertiseIP;
            this.TypeReceivedRoutesTextBox.Text = _bilTrBeritaAcara.TypeReceivedRoutes;

            this.ApprovedByCustomerNameTextBox.Text = _bilTrBeritaAcara.ApprovedByCustomerName;
            this.TrialDayTextBox.Text = _bilTrBeritaAcara.TrialDay.ToString();

            this.RemarkTextBox.Text = _bilTrBeritaAcara.Remark;

            this.StatusHiddenField.Value = _bilTrBeritaAcara.Status.ToString();
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ApproveButton_Click(object sender, ImageClickEventArgs e)
        {
            String _resultMessage = this._beritaAcaraBL.ApproveBeritaAcara(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this.ApprovedByCustomerNameTextBox.Text, HttpContext.Current.User.Identity.Name, Convert.ToByte(this.StatusHiddenField.Value));
            if (_resultMessage == "")
            {
                Response.Redirect(this._viewPage + "?updateStatus=success&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = _resultMessage;
            }
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._reportBL.BeritaAcaraPrintPreview(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            ////Guid _compId = new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name);
            ////String _companyTag = new UserBL().GetCompanyTag(_compId);
            //String _jobTitleStatus = this._companyConfigBL.GetSingle(CompanyConfigure.ViewJobTitlePrintReport).SetValue;
            //String _termAndCondition = this.StripHTML(this._termAndConditionBL.GetSingle(TermAndConditionDataMapper.GetType(TermAndConditionType.SalesConfirmation)).Body);

            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[2];
            _reportParam[0] = new ReportParameter("prmTransNmbr", Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), true);
            _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), true);
            //_reportParam[2] = new ReportParameter("JobTitleStatus", _jobTitleStatus, false);
            //_reportParam[3] = new ReportParameter("TermAndCondition", _termAndCondition, false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

    }
}