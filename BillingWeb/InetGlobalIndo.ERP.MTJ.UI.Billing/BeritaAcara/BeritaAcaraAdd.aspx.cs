using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.BillingBeritaAcara
{
    public partial class BeritaAcaraAdd : BillingBeritaAcaraBase
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

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowSalesConfirmation();
                this.ClearLabel();
                this.SetAttribute();
                this.ClearData();
            }

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.TransDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.TrialDayTextBox.Attributes.Add("OnKeyUp", "return formatangka(" + this.TechnicalBandwidthINTTextBox.ClientID + ");");
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
            this.SalesConfirmationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            this.SalesConfirmationDropDownList.SelectedValue = "null";
            //this.ContractNoTextBox.Text = "";
            this.TechincalAccountTextBox.Text = "";
            this.TechincalAccountPasswordTextBox.Text = "";

            this.TechnicalIPAllocationTextBox.Text = "";
            this.TechnicalTransmisionTextBox.Text = "";
            this.TechnicalServiceSpesificationTextBox.Text = "";
            this.TechnicalBandwidthINTTextBox.Text = "";

            this.TechnicalBandwidthLocalLoopTextBox.Text = "";
            this.TechnicalBandwidthINTRatioUpstreamTextBox.Text = "";
            this.TechnicalBandwidthINTRatioDownstreamTextBox.Text = "";
            this.TechnicalTerminationPointTextBox.Text = "";

            this.MikrotikPPTPUserNameTextBox.Text = "";
            this.MikrotikPPPOEUserNameTextBox.Text = "";
            this.MikrotikHotspotUserNameTextBox.Text = "";
            this.MikrotikQueueNameDownLinkTextBox.Text = "";

            this.MikrotikQueueNameUpLinkTextBox.Text = "";
            this.CollocationRackNoTextBox.Text = "";
            this.CollocationServerPositionNoTextBox.Text = "";
            this.BGPASNumberTextBox.Text = "";

            this.BGPIPAddressRouterTextBox.Text = "";
            this.BGPAdvertiseIPTextBox.Text = "";
            this.TypeReceivedRoutesTextBox.Text = "";
            //this.CreatedByTextBox.Text = "";

            //this.EditByTextBox.Text = "";
            //this.ApprovedByCustomerNameTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            String _code = "";
            if ( _beritaAcaraBL.AddBeritaAcara(this.ApprovedByCustomerNameTextBox.Text ,
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
                HttpContext.Current.User.Identity.Name, Convert.ToInt16(this.TrialDayTextBox.Text),
                this.RemarkTextBox.Text, ref _code) )
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) );
            }
            else {
                this.WarningLabel.Text = "Add Data Failed";
            }

        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

        protected void SalesConfirmationDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            String _trialDay = _beritaAcaraBL.GetTrialDayOfSalesConfirmation(this.SalesConfirmationDropDownList.SelectedValue);
            this.TrialDayHiddenField.Value = _trialDay;
            this.TrialDayTextBox.Text = _trialDay;
            //this.ContractNoHiddenField.Value = _beritaAcaraBL.GetContractNumberBySalesConfirmation(this.SalesConfirmationDropDownList.SelectedValue);
            //this.ContractNoTextBox.Text = _beritaAcaraBL.GetContractFileNmbrByContractTransNmbr(this.ContractNoHiddenField.Value);            
        }
    }
}