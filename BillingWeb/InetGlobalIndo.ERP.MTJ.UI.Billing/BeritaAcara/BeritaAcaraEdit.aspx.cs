using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.BillingBeritaAcara
{
    public partial class BeritaAcaraEdit : BillingBeritaAcaraBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private BeritaAcaraBL _beritaAcaraBL = new BeritaAcaraBL();

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

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                
                this.ClearLabel();
                this.ShowSalesConfirmation();
                this.SetAttribute();

                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.TrialDayTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.TrialDayTextBox.ClientID + ");");
            this.TechnicalBandwidthINTTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.TechnicalBandwidthINTTextBox.ClientID + ");");
            this.TechnicalBandwidthLocalLoopTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.TechnicalBandwidthLocalLoopTextBox.ClientID + ");");
            this.TechnicalBandwidthINTRatioUpstreamTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.TechnicalBandwidthINTRatioUpstreamTextBox.ClientID + ");");
            this.TechnicalBandwidthINTRatioDownstreamTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.TechnicalBandwidthINTRatioDownstreamTextBox.ClientID + ");");
        }

        protected void ShowSalesConfirmation()
        {
            this.SalesConfirmationDropDownList.DataTextField = "Value";
            this.SalesConfirmationDropDownList.DataValueField = "Key";
            this.SalesConfirmationDropDownList.DataSource = _beritaAcaraBL.GetListSalesConfirmationForDDL();
            this.SalesConfirmationDropDownList.DataBind();
        }

        protected void ShowData()
        {
            BILTrBeritaAcara _bilTrBeritaAcara = this._beritaAcaraBL.GetSingleBeritaAcara(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransDateTextBox.Text = Convert.ToDateTime( _bilTrBeritaAcara.TransDate).ToString("yyyy-MM-dd");

            this.SalesConfirmationDropDownList.Items.Insert(0, new ListItem(_bilTrBeritaAcara.SalesConfirmationNoRef, _bilTrBeritaAcara.SalesConfirmationNoRef));

            this.SalesConfirmationDropDownList.SelectedValue = _bilTrBeritaAcara.SalesConfirmationNoRef;
            //this.ContractNoTextBox.Text = _beritaAcaraBL.GetContractFileNmbrByContractTransNmbr(_bilTrBeritaAcara.NoKontrak);
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
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (_beritaAcaraBL.UpdateBeritaAcara(
                Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey),
                Convert.ToDateTime(this.TransDateTextBox.Text),
                //this.SalesConfirmationDropDownList.SelectedValue, this.ContractNoHiddenField.Value,
                this.SalesConfirmationDropDownList.SelectedValue, "",
                this.TechincalAccountTextBox.Text, this.TechincalAccountPasswordTextBox.Text,
                this.TechnicalIPAllocationTextBox.Text, this.TechnicalTransmisionTextBox.Text,
                this.TechnicalServiceSpesificationTextBox.Text,
                Convert.ToInt32(this.TechnicalBandwidthINTTextBox.Text),
                Convert.ToInt32(this.TechnicalBandwidthLocalLoopTextBox.Text),
                Convert.ToInt32(this.TechnicalBandwidthINTRatioUpstreamTextBox.Text),
                Convert.ToInt32(this.TechnicalBandwidthINTRatioDownstreamTextBox.Text),
                this.TechnicalTerminationPointTextBox.Text,
                this.MikrotikPPTPUserNameTextBox.Text, this.MikrotikPPPOEUserNameTextBox.Text,
                this.MikrotikHotspotUserNameTextBox.Text, this.MikrotikQueueNameDownLinkTextBox.Text,
                this.MikrotikQueueNameDownLinkTextBox.Text, this.CollocationRackNoTextBox.Text,
                this.CollocationServerPositionNoTextBox.Text, this.BGPASNumberTextBox.Text,
                this.BGPIPAddressRouterTextBox.Text, this.BGPAdvertiseIPTextBox.Text, this.TypeReceivedRoutesTextBox.Text,
                this.ApprovedByCustomerNameTextBox.Text, Convert.ToInt16 (this.TrialDayTextBox.Text),
                this.RemarkTextBox.Text, HttpContext.Current.User.Identity.Name))
            {
                this.WarningLabel.Text = "Update Data Success";
            }
            else
            {
                this.WarningLabel.Text = "Update Data Failed";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void SalesConfirmationDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            String _trialDay = _beritaAcaraBL.GetTrialDayOfSalesConfirmation(this.SalesConfirmationDropDownList.SelectedValue);
            this.TrialDayHiddenField.Value = _trialDay;
            this.TrialDayTextBox.Text = _trialDay;
        //    this.ContractNoTextBox.Text = _beritaAcaraBL.GetContractFileNmbrByContractTransNmbr(_beritaAcaraBL.GetContractNumberBySalesConfirmation(this.SalesConfirmationDropDownList.SelectedValue));
        //    this.ContractNoHiddenField.Value = _beritaAcaraBL.GetContractNumberBySalesConfirmation(this.SalesConfirmationDropDownList.SelectedValue);
        }
        
}
}